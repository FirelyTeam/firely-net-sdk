/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Specification.Terminology
{
    public class LocalTerminologyService : ITerminologyService
    {
        private readonly IResourceResolver _resolver;
        private readonly ValueSetExpander _expander;

        public LocalTerminologyService(IResourceResolver resolver, ValueSetExpanderSettings expanderSettings = null)
        {
            _resolver = resolver ?? throw Error.ArgumentNull(nameof(resolver));

            var settings = expanderSettings ?? ValueSetExpanderSettings.CreateDefault();
            if (settings.ValueSetSource == null) settings.ValueSetSource = resolver;

            _expander = new ValueSetExpander(settings);
        }

        internal ValueSet FindValueset(string canonical)
        {
            // Don't want to redo ITerminologyService yet
#pragma warning disable CS0618 // Type or member is obsolete
            return _resolver.FindValueSet(canonical);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public Parameters ValueSetValidateCode(Parameters parameters, string id = null, bool useGet = false)
        {
            var validCodeParams = new ValidateCodeParameters(parameters);

            var valueSet = validCodeParams.ValueSet;
            if (valueSet is null)
            {
                if (validCodeParams.Url is null)
                    throw Error.Argument("Have to supply either a canonical url or a valueset.");

                try
                {
                    valueSet = FindValueset(validCodeParams.Url.Value);
                }
                catch
                {
                    // valueSet remains null
                }

                if (valueSet is null)
                    throw new ValueSetUnknownException($"Cannot retrieve valueset '{validCodeParams.Url.Value}'");
            }

            if (validCodeParams.CodeableConcept is { })
                return validateCodeVS(valueSet, validCodeParams.CodeableConcept, validCodeParams.Abstract?.Value);
            else if (validCodeParams.Coding is { })
                return validateCodeVS(valueSet, validCodeParams.Coding, validCodeParams.Abstract?.Value);
            else
                return validateCodeVS(valueSet, validCodeParams.Code?.Value, validCodeParams.System?.Value, validCodeParams.Display?.Value, validCodeParams.Abstract?.Value);
        }

        #region Not implemented methods
        public Parameters CodeSystemValidateCode(Parameters parameters, string id = null, bool useGet = false)
        {
            throw new NotImplementedException();
        }

        public Resource Expand(Parameters parameters, string id = null, bool useGet = false)
        {
            throw new NotImplementedException();
        }

        public Parameters Lookup(Parameters parameters, bool useGet = false)
        {
            throw new NotImplementedException();
        }

        public Parameters Translate(Parameters parameters, string id = null, bool useGet = false)
        {
            throw new NotImplementedException();
        }

        public Parameters Subsumes(Parameters parameters, string id = null, bool useGet = false)
        {
            throw new NotImplementedException();
        }

        public Resource Closure(Parameters parameters, bool useGet = false)
        {
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

            return ValueSetValidateCode(parameters).ToOperationOutcome();
        }

        private Parameters validateCodeVS(ValueSet vs, CodeableConcept cc, bool? abstractAllowed)
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
                return validateCodeVS(vs, cc.Coding.Single(), abstractAllowed);

            // Else, look for one succesful match in any of the codes in the CodeableConcept
            var callResults = cc.Coding.Select(coding => validateCodeVS(vs, coding, abstractAllowed));
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

        private Parameters validateCodeVS(ValueSet vs, Coding coding, bool? abstractAllowed)
        {
            return validateCodeVS(vs, coding.Code, coding.System, coding.Display, abstractAllowed);
        }

        private Parameters validateCodeVS(ValueSet vs, string code, string system, string display, bool? abstractAllowed)
        {
            if (code is null)
            {
                var resultParam = new Parameters();
                resultParam.Add("message", new FhirString("No code supplied."));
                resultParam.Add("result", new FhirBoolean(false));
                return resultParam;
            }

            lock (vs.SyncLock)
            {
                // We might have a cached or pre-expanded version brought to us by the _source
                if (!vs.HasExpansion)
                {
                    // This will expand te vs - since we do not deepcopy() it, it will change the instance
                    // as it was passed to us from the source
                    // Don't want to redo ITerminologyService yet
#pragma warning disable CS0618 // Type or member is obsolete
                    _expander.Expand(vs);
#pragma warning restore CS0618 // Type or member is obsolete
                }
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

