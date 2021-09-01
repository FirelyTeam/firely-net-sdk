/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using System;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    public class FallbackTerminologyService : ITerminologyService
    {
        private readonly LocalTerminologyService _localService;
        private readonly ITerminologyService _fallbackService;

        public FallbackTerminologyService(LocalTerminologyService local, ITerminologyService fallback)
        {
            _localService = local;
            _fallbackService = fallback;
        }

        public async Task<Parameters> ValueSetValidateCode(Parameters parameters, string id = null, bool useGet = false)
        {
            try
            {
                // First, try the local service
                return await _localService.ValueSetValidateCode(parameters, id, useGet).ConfigureAwait(false);
            }
            catch (FhirOperationException)
            {
                // If that fails, call the fallback
                try
                {
                    return await _fallbackService.ValueSetValidateCode(parameters, id, useGet).ConfigureAwait(false);
                }
                catch (FhirOperationException vse) when (vse.Status == System.Net.HttpStatusCode.NotFound)
                {
                    // The fall back service does not know the valueset. If our local service
                    // does, try get the VS from there, and retry by sending the vs inline
                    var url = parameters.GetSingleValue<FhirUri>("url")?.Value;
                    var valueSet = await _localService.FindValueset(url).ConfigureAwait(false);
                    if (valueSet == null) throw;

                    parameters.Remove("valueSet");
                    parameters.Add("valueSet", valueSet);

                    return await _fallbackService.ValueSetValidateCode(parameters, id, useGet).ConfigureAwait(false);
                }
            }
        }

        public Task<Parameters> CodeSystemValidateCode(Parameters parameters, string id = null, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        public Task<Resource> Expand(Parameters parameters, string id = null, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        public Task<Parameters> Lookup(Parameters parameters, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        public Task<Parameters> Translate(Parameters parameters, string id = null, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        public Task<Parameters> Subsumes(Parameters parameters, string id = null, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        public Task<Resource> Closure(Parameters parameters, bool useGet = false)
        {
            // make this method async, when implementing
            throw new NotImplementedException();
        }

        [Obsolete("This method is obsolete, use method with signature 'ValueSetValidateCode(Parameters, string, bool)'")]
        public OperationOutcome ValidateCode(string canonical = null, string context = null, ValueSet valueSet = null,
            string code = null, string system = null, string version = null, string display = null,
            Coding coding = null, CodeableConcept codeableConcept = null, FhirDateTime date = null,
            bool? @abstract = default, string displayLanguage = null)
        {

            try
            {
                // First, try the local service
                return _localService.ValidateCode(canonical, context, valueSet, code, system, version, display,
                    coding, codeableConcept, date, @abstract, displayLanguage);
            }
            catch (TerminologyServiceException)
            {
                // If that fails, call the fallback
                try
                {
                    return _fallbackService.ValidateCode(canonical, context, valueSet, code, system, version, display,
                        coding, codeableConcept, date, @abstract, displayLanguage);
                }
                catch (ValueSetUnknownException)
                {
                    // The fall back service does not know the valueset. If our local service
                    // does, try get the VS from there, and retry by sending the vs inline
                    valueSet = TaskHelper.Await(() => _localService.FindValueset(canonical));
                    if (valueSet == null) throw;

                    return _fallbackService.ValidateCode(null, context, valueSet, code, system, version, display,
                        coding, codeableConcept, date, @abstract, displayLanguage);
                }
            }
        }
    }
}
