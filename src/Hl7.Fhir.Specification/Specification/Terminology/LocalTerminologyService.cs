/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    public class LocalTerminologyService : ITerminologyService
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private readonly IAsyncResourceResolver _resolver;
        private readonly ValueSetExpander _expander;

        public LocalTerminologyService(IAsyncResourceResolver resolver, ValueSetExpanderSettings expanderSettings = null)
        {
            _resolver = resolver ?? throw Error.ArgumentNull(nameof(resolver));

            var settings = expanderSettings ?? ValueSetExpanderSettings.CreateDefault();
            if (settings.ValueSetSource == null) settings.ValueSetSource = resolver;

            _expander = new ValueSetExpander(settings);
        }

        internal async T.Task<ValueSet> FindValueset(string canonical)
        {
            return await _resolver.FindValueSetAsync(canonical).ConfigureAwait(false);
        }

        public async T.Task<Parameters> ValueSetValidateCode(Parameters parameters, string id = null, bool useGet = false)
        {
            var validCodeParams = new ValidateCodeParameters(parameters);

            var valueSet = validCodeParams.ValueSet;
            if (valueSet is null)
            {
                if (validCodeParams.Url is null)
                    throw Error.Argument("Have to supply either a canonical url or a valueset.");

                try
                {
                    valueSet = await FindValueset(validCodeParams.Url.Value).ConfigureAwait(false);
                }
                catch
                {
                    // valueSet remains null
                }

                if (valueSet is null)
                    throw new ValueSetUnknownException($"Cannot retrieve valueset '{validCodeParams.Url.Value}'");
            }

            if (validCodeParams.CodeableConcept is { })
                return await validateCodeVS(valueSet, validCodeParams.CodeableConcept, validCodeParams.Abstract?.Value).ConfigureAwait(false);
            else if (validCodeParams.Coding is { })
                return await validateCodeVS(valueSet, validCodeParams.Coding, validCodeParams.Abstract?.Value).ConfigureAwait(false);
            else
                return await validateCodeVS(valueSet, validCodeParams.Code?.Value, validCodeParams.System?.Value, validCodeParams.Display?.Value, validCodeParams.Abstract?.Value).ConfigureAwait(false);
        }

        #region Not implemented methods
        public T.Task<Parameters> CodeSystemValidateCode(Parameters parameters, string id = null, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        public T.Task<Resource> Expand(Parameters parameters, string id = null, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        public T.Task<Parameters> Lookup(Parameters parameters, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        public T.Task<Parameters> Translate(Parameters parameters, string id = null, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        public T.Task<Parameters> Subsumes(Parameters parameters, string id = null, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        public T.Task<Resource> Closure(Parameters parameters, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }
        #endregion

        [Obsolete("This method is obsolete, use method with signature 'ValueSetValidateCode(Parameters, string, bool)'")]
        public OperationOutcome ValidateCode(string canonical = null, string context = null, ValueSet valueSet = null,
            string code = null, string system = null, string version = null, string display = null,
            Coding coding = null, CodeableConcept codeableConcept = null, FhirDateTime date = null,
            bool? @abstract = null, string displayLanguage = null)
        {
            var parameters = new ValidateCodeParameters()
                .WithValueSet(url: canonical, context: context, valueSet: valueSet)
                .WithCode(code: code, system: system, systemVersion: version, display: display, displayLanguage: displayLanguage)
                .WithCoding(coding)
                .WithCodeableConcept(codeableConcept)
                .WithDate(date)
                .WithAbstract(@abstract)
                .Build();

            var parms = TaskHelper.Await(() => ValueSetValidateCode(parameters));
            return parms.ToOperationOutcome();
        }

        private async T.Task<Parameters> validateCodeVS(ValueSet vs, CodeableConcept cc, bool? abstractAllowed)
        {
            var result = new Parameters();

            // Maybe just a text, but if there are no codings, that's a positive result
            if (!cc.Coding.Any())
            {
                result.Add("result", new FhirBoolean(true));
                return result;
            }

            // If we have just 1 coding, we better handle this using the simpler version of ValidateBinding
            if (cc.Coding.Count == 1)
                return await validateCodeVS(vs, cc.Coding.Single(), abstractAllowed).ConfigureAwait(false);


            // Else, look for one succesful match in any of the codes in the CodeableConcept
            var callResults = await T.Task.WhenAll(cc.Coding.Select(coding => validateCodeVS(vs, coding, abstractAllowed)));
            var anySuccesful = callResults.Any(p => p.GetSingleValue<FhirBoolean>("result")?.Value == true);

            if (anySuccesful == false)
            {
                var messages = new StringBuilder();
                messages.AppendLine("None of the Codings in the CodeableConcept were valid for the binding. Details follow.");

                // gathering the messages of all calls
                foreach (var msg in callResults.Select(cr => cr.GetSingleValue<FhirString>("message")?.Value).Where(m => m is { }))
                {
                    messages.AppendLine(msg);
                }

                result.Add("message", new FhirString(messages.ToString()));
                result.Add("result", new FhirBoolean(false));
            }
            else
            {
                result.Add("result", new FhirBoolean(true));
            }

            return result;
        }

        private async T.Task<Parameters> validateCodeVS(ValueSet vs, Coding coding, bool? abstractAllowed)
        {
            return await validateCodeVS(vs, coding.Code, coding.System, coding.Display, abstractAllowed).ConfigureAwait(false);
        }

        private async T.Task<Parameters> validateCodeVS(ValueSet vs, string code, string system, string display, bool? abstractAllowed)
        {

            if (code is null)
            {
                var resultParam = new Parameters();
                resultParam.Add("message", new FhirString("No code supplied."));
                resultParam.Add("result", new FhirBoolean(false));
                return resultParam;
            }

            await _semaphore.WaitAsync();
            try
            {
                // We might have a cached or pre-expanded version brought to us by the _source
                if (!vs.HasExpansion)
                {
                    // This will expand te vs - since we do not deepcopy() it, it will change the instance
                    // as it was passed to us from the source
                    await _expander.ExpandAsync(vs).ConfigureAwait(false);
                }
            }
            finally
            {
                _semaphore.Release();
            }

            var component = vs.FindInExpansion(code, system);
            var codeLabel = $"Code '{code}' from system '{system}'";
            var result = new Parameters();
            var success = true;
            var messages = new StringBuilder();

            if (component is null)
            {
                messages.AppendLine($"{codeLabel} does not exist in valueset '{vs.Url}'");
                success = false;
            }
            else
            {
                if (component.Abstract == true && abstractAllowed == false)  // will be ignored if abstractAllowed == null
                {
                    messages.AppendLine($"{codeLabel} is abstract, which is not allowed here");
                    success = false;
                }

                if (display != null && component.Display != null && display != component.Display)
                {
                    // this is only a warning (so success is still true)
                    messages.AppendLine($"{codeLabel} has incorrect display '{display}', should be '{component.Display}'");
                }

                var displ = component.Display ?? display;
                if (displ is { })
                    result.Add("display", new FhirString(displ));
            }

            result.Add("result", new FhirBoolean(success));
            if (messages.Length > 0)
                result.Add("message", new FhirString(messages.ToString()));
            return result;
        }
    }
}

