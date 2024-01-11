﻿/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    public class LocalTerminologyService : ITerminologyService
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        private readonly IAsyncResourceResolver _resolver;
        private readonly ValueSetExpander _expander;

        public LocalTerminologyService(IAsyncResourceResolver resolver, ValueSetExpanderSettings? expanderSettings = null)
        {
            _resolver = resolver ?? throw Error.ArgumentNull(nameof(resolver));

            var settings = expanderSettings ?? ValueSetExpanderSettings.CreateDefault();
            settings.ValueSetSource ??= resolver;

            _expander = new ValueSetExpander(settings);
        }

        /// <summary>
        /// Creates a MultiTerminologyService, which combines a LocalTerminologyService to retrieve the core FHIR resources with custom services to validate some implicit core ValueSets.
        /// </summary>
        /// <param name="coreResourceResolver">Resource resolves to resolve FHIR core artifacts</param>
        /// <param name="expanderSettings">ValueSet expansion settings</param>
        /// <returns>A MultiTerminologyService, which combines a LocalTerminologyService to retrieve the core FHIR resources with custom services to validate some implicit core ValueSets</returns>
        public static MultiTerminologyService CreateDefaultForCore(IAsyncResourceResolver coreResourceResolver, ValueSetExpanderSettings? expanderSettings = null)
        {
            return TerminologyServiceFactory.CreateDefaultForCore(coreResourceResolver, expanderSettings);
        }

        internal async T.Task<ValueSet?> FindValueSet(string canonical)
        {
            var valueset = await _resolver.FindValueSetAsync(canonical).ConfigureAwait(false);

#if STU3
            if (valueset == null && _resolver is IConformanceSource source)
#else
            if (valueset == null && _resolver is ICommonConformanceSource source)
#endif
            {
                var cs = source.FindCodeSystemByValueSet(canonical);
                if (cs != null)
                {
                    valueset = new ValueSet
                    {
                        Url = canonical,
                        Status = cs.Status,  // mandatory field

#if !STU3
                        ApprovalDate = cs.ApprovalDate,
                        Author = cs.Author,
                        CopyrightLabel = cs.CopyrightLabel,
                        Editor = cs.Editor,
                        EffectivePeriod = cs.EffectivePeriod,
                        Endorser = cs.Endorser,
                        LastReviewDate = cs.LastReviewDate,
                        RelatedArtifact = cs.RelatedArtifact,
                        Reviewer = cs.Reviewer,
                        Topic = cs.Topic,
                        VersionAlgorithm = cs.VersionAlgorithm,
#endif
                        Contact = cs.Contact,
                        Copyright = cs.Copyright,
                        Date = cs.Date,
                        Description = cs.Description,
                        Experimental = cs.Experimental,
                        Id = cs.Id,
                        Jurisdiction = cs.Jurisdiction,
                        Language = cs.Language,
                        Name = cs.Name,
                        Publisher = cs.Publisher,
                        Purpose = cs.Purpose,
                        Title = cs.Title,
                        UseContext = cs.UseContext,
                        Version = cs.Version,

                        Compose = new()
                        {
                            Include = new()
                            {
                                new()
                                {
                                    System = cs.Url
                                }
                            }
                        }
                    };

                    return valueset;
                }
            }
            return valueset;
        }

        private async Task<ValueSet> getExpandedValueSet(ValueSet vs, string operation)
        {
            try
            {
                if (!vs.HasExpansion)
                {
                    try
                    {
                        await _semaphore.WaitAsync().ConfigureAwait(false);
                        await _expander.ExpandAsync(vs).ConfigureAwait(false);
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                }
            }
            catch (TerminologyServiceException e)
            {
                // Unprocessable entity
                throw new FhirOperationException(
                    $"Operation {operation} failed: creating the required expansion failed mith message \"{e.Message}\".",
                    (HttpStatusCode)422);
            }

            return vs;
        }

        private async Task<ValueSet> getExpandedValueSet(string canonical, string operation)
        {
            var vs = await FindValueSet(canonical).ConfigureAwait(false);

            if (vs is null)
                throw new FhirOperationException($"Operation {operation} failed: valueset '{canonical}' is unknown.", HttpStatusCode.NotFound);
            else
                return await getExpandedValueSet(vs, operation).ConfigureAwait(false);
        }

        ///<inheritdoc />
        public async T.Task<Parameters> ValueSetValidateCode(Parameters parameters, string? id = null, bool useGet = false)
        {
            parameters.CheckForValidityOfValidateCodeParams();

            var validateCodeParams = new ValidateCodeParameters(parameters);
            var valueSet = validateCodeParams.ValueSet as ValueSet;
            if (valueSet is null && validateCodeParams.Url is null)
                throw new FhirOperationException("Have to supply either a canonical url or a valueset.", (HttpStatusCode)422); // Unprocessable entity

            try
            {
                valueSet = valueSet is null
                    ? await getExpandedValueSet(validateCodeParams.Url.Value, "validate code").ConfigureAwait(false)
                    : await getExpandedValueSet(valueSet, "validate code").ConfigureAwait(false);

                if (validateCodeParams.CodeableConcept is { })
                    return await validateCodeVS(valueSet, validateCodeParams.CodeableConcept, validateCodeParams.Abstract?.Value).ConfigureAwait(false);
                else if (validateCodeParams.Coding is { })
                    return await validateCodeVS(valueSet, validateCodeParams.Coding, validateCodeParams.Abstract?.Value).ConfigureAwait(false);
                else
                    return await validateCodeVS(valueSet, validateCodeParams.Code?.Value, validateCodeParams.System?.Value, validateCodeParams.Display?.Value, validateCodeParams.Abstract?.Value).ConfigureAwait(false);
            }
            catch (Exception e) when (e is not FhirOperationException)
            {
                //500 internal server error
                throw new FhirOperationException(e.Message, (HttpStatusCode)500);
            }
        }

        ///<inheritdoc />
        public async T.Task<Resource> Expand(Parameters parameters, string? id = null, bool useGet = false)
        {
            parameters.NoDuplicates();

            var url = parameters.GetSingleValue<FhirUri>("url")?.Value ?? parameters.GetSingleValue<FhirString>("url")?.Value;
            var valueSet = parameters.GetSingle("valueSet")?.Resource as ValueSet;

            if (valueSet is null && url is null)
                throw new FhirOperationException("Have to supply either a canonical url or a valueset.", (HttpStatusCode)422); // Unprocessable entity

            try
            {
                return valueSet is null
                    ? await getExpandedValueSet(url!, "expand").ConfigureAwait(false)
                    : await getExpandedValueSet(valueSet, "expand").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //500 internal server error
                throw new FhirOperationException(e.Message, (HttpStatusCode)500);
            }
        }

        #region Not implemented methods
        ///<inheritdoc />
        public T.Task<Parameters> CodeSystemValidateCode(Parameters parameters, string? id = null, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        ///<inheritdoc />
        public T.Task<Parameters> Lookup(Parameters parameters, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        ///<inheritdoc />
        public T.Task<Parameters> Translate(Parameters parameters, string? id = null, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        ///<inheritdoc />
        public T.Task<Parameters> Subsumes(Parameters parameters, string? id = null, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        ///<inheritdoc />
        public T.Task<Resource> Closure(Parameters parameters, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }
        #endregion

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
            var callResults = await T.Task.WhenAll(cc.Coding.Select(coding => validateCodeVS(vs, coding, abstractAllowed))).ConfigureAwait(false);
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

        private async Task<Parameters> validateCodeVS(ValueSet vs, string? code, string? system, string? display, bool? abstractAllowed)
        {
            if (code is null)
            {
                var resultParam = new Parameters
                {
                    { "message", new FhirString("No code supplied.") },
                    { "result", new FhirBoolean(false) }
                };
                return resultParam;
            }

            var component = vs.FindInExpansion(code, system);
            var codeLabel = $"Code '{code}'" + (string.IsNullOrEmpty(system) ? string.Empty : $" from system '{system}'");
            var result = new Parameters();
            var success = true;
            var messages = new StringBuilder();

            if (component is null)
            {
                await messageForCodeNotFound(vs, system, codeLabel, messages).ConfigureAwait(false);
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
                result.Add("message", new FhirString(messages.ToString().TrimEnd()));
            return result;
        }

        private async T.Task messageForCodeNotFound(ValueSet vs, string? system, string codeLabel, StringBuilder messages)
        {
            if (system is not null && await isValueSet(system).ConfigureAwait(false))
            {
                messages.AppendLine($"The Coding references a value set, not a code system ('{system}')");
            }
            else
            {
                messages.AppendLine($"{codeLabel} does not exist in the value set '{vs.Title ?? vs.Name}' ({vs.Url})");
            }

            async T.Task<bool> isValueSet(string system)
            {
                // First, conduct a quick initial check, and if that fails, proceed with a more comprehensive approach.
                return (system.Contains(@"/ValueSet/") || await _resolver.FindValueSetAsync(system).ConfigureAwait(false) is not null);
            }
        }
    }
}

#nullable restore