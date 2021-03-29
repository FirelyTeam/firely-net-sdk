/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    public class ExternalTerminologyService : ITerminologyService
    {
        private readonly string _contextAttribute = "context";
        private readonly string _codeAttribute = "code";
        private readonly string _systemAttribute = "system";

        private readonly string _codeASubsumesAttribute = "codeA";
        private readonly string _codeBSubsumesAttribute = "codeB";
        private readonly string _codingASubsumesAttribute = "codingA";
        private readonly string _codingBSubsumesAttribute = "codingB";
  
        public ExternalTerminologyService(BaseFhirClient client)
        {
            Endpoint = client;
        }

        public BaseFhirClient Endpoint { get; set; }

        public async Task<Parameters> ValueSetValidateCode(Parameters parameters, string id = null, bool useGet = false)
        {
            if (parameters.Parameter.Any(p => p.Name == _codeAttribute) && !(parameters.Parameter.Any(p => p.Name == _systemAttribute) ||
                                                                                    parameters.Parameter.Any(p => p.Name == _contextAttribute)))
            {
                throw Error.Argument($"If a code is provided, a system or a context must be provided");
            }
            else
            {
                if (string.IsNullOrEmpty(id))
                    return await Endpoint.TypeOperationAsync<ValueSet>(RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false) as Parameters;
                else
                    return await Endpoint.InstanceOperationAsync(constructUri<ValueSet>(id), RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false) as Parameters;
            }           
        }

        public async Task<Parameters> CodeSystemValidateCode(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync<CodeSystem>(RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false) as Parameters;
            else
                return await Endpoint.InstanceOperationAsync(constructUri<CodeSystem>(id), RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false) as Parameters;
        }

        private Uri constructUri<T>(string id) =>
            ResourceIdentity.Build(ModelInfo.GetFhirTypeNameForType(typeof(T)), id);

        public async Task<Resource> Expand(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync<ValueSet>(RestOperation.EXPAND_VALUESET, parameters, useGet).ConfigureAwait(false);
            else
                return await Endpoint.InstanceOperationAsync(constructUri<ValueSet>(id), RestOperation.EXPAND_VALUESET, parameters, useGet).ConfigureAwait(false);
        }

        public async Task<Parameters> Lookup(Parameters parameters, bool useGet = false)
        {
            if (parameters.Parameter.Any(p => p.Name == _codeAttribute) && !parameters.Parameter.Any(p => p.Name == _systemAttribute))
            {
                throw Error.Argument($"If a code is provided, a system must be provided");
            }
            return await Endpoint.TypeOperationAsync<CodeSystem>(RestOperation.CONCEPT_LOOKUP, parameters, useGet).ConfigureAwait(false) as Parameters;
        }

        public async Task<Parameters> Translate(Parameters parameters, string id = null, bool useGet = false)
        {
            if (parameters.Parameter.Any(p => p.Name == _codeAttribute) && !parameters.Parameter.Any(p => p.Name == _systemAttribute))
            {
                throw Error.Argument($"If a code is provided, a system must be provided");
            }
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync<ConceptMap>(RestOperation.TRANSLATE, parameters, useGet).ConfigureAwait(false) as Parameters;
            else
                return await Endpoint.InstanceOperationAsync(constructUri<ConceptMap>(id), RestOperation.TRANSLATE, parameters, useGet).ConfigureAwait(false) as Parameters;
        }

        public async Task<Parameters> Subsumes(Parameters parameters, string id = null, bool useGet = false)
        {
            validateSubsumptionParameters(parameters);            
           
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync<CodeSystem>(RestOperation.SUBSUMES, parameters, useGet).ConfigureAwait(false) as Parameters;
            else
                return await Endpoint.InstanceOperationAsync(constructUri<CodeSystem>(id), RestOperation.SUBSUMES, parameters, useGet).ConfigureAwait(false) as Parameters;       
        }

        private void validateSubsumptionParameters(Parameters parameters)
        {
            if (parameters.Parameter.Any(p => p.Name == _codeASubsumesAttribute))
            {
                if (!parameters.Parameter.Any(p => p.Name == _codeBSubsumesAttribute))
                {
                    throw Error.Argument($"If '{_codeASubsumesAttribute}' is provided, '{_codeBSubsumesAttribute}' must be provided");
                }
                else if (!parameters.Parameter.Any(p => p.Name == _systemAttribute))
                {
                    throw Error.Argument($"If codes are provided, a system must be provided");
                }
            }
            else if (parameters.Parameter.Any(p => p.Name == _codingASubsumesAttribute) && !parameters.Parameter.Any(p => p.Name == _codingBSubsumesAttribute))
            {
                throw Error.Argument($"If '{_codingASubsumesAttribute}' is provided, '{_codingBSubsumesAttribute}' must be provided");
            }
        }

        public async Task<Resource> Closure(Parameters parameters, bool useGet = false)
        {
            return await Endpoint.WholeSystemOperationAsync(RestOperation.CLOSURE, parameters, useGet).ConfigureAwait(false);
        }

        [Obsolete("This method is obsolete, use method with signature 'ValueSetValidateCode(Parameters, string, bool)'")]
        public OperationOutcome ValidateCode(string canonical = null, string context = null, ValueSet valueSet = null,
            string code = null, string system = null, string version = null, string display = null,
            Coding coding = null, CodeableConcept codeableConcept = null, FhirDateTime date = null,
            bool? @abstract = null, string displayLanguage = null)
        {
            try
            {
                var resultValidateCode =
                    Endpoint.ValidateCode(
                                url: canonical != null ? new FhirUri(canonical) : null,
                                context: context != null ? new FhirUri(context) : null,
                                valueSet: valueSet,
                                code: code != null ? new Code(code) : null,
                                system: system != null ? new FhirUri(system) : null,
                                version: version != null ? new FhirString(version) : null,
                                display: display != null ? new FhirString(display) : null,
                                coding: coding, codeableConcept: codeableConcept, date: date,
                                @abstract: @abstract != null ? new FhirBoolean(@abstract) : null,
                                displayLanguage: displayLanguage != null ? new Code(displayLanguage) : null);

                OperationOutcome outcome = processResult(code, system, display, coding, codeableConcept, resultValidateCode);

                return outcome;

            }
            catch (FhirOperationException ex)
            {
                // Special case, if term service returns 404, turn that into a more explicit exception
                if (ex.Status == System.Net.HttpStatusCode.NotFound)
                    throw new ValueSetUnknownException(ex.Message);
                else
                    return ex.Outcome;
            }
        }

        private OperationOutcome processResult(string code, string system, string display, Coding coding, CodeableConcept codeableConcept, ValidateCodeResult result)
        {
            if (result?.Result?.Value == null)
                throw Error.InvalidOperation($"Terminology service at {Endpoint.Endpoint} did not return a result.");

            var outcome = new OperationOutcome();

            if (result?.Result?.Value == false)
            {
                string message = result?.Message?.Value;

                if (message != null)
                    outcome.AddIssue(message, Issue.TERMINOLOGY_CODE_NOT_IN_VALUESET);
                else
                {
                    if (code != null && coding == null)
                        coding = new Coding(system, code, display);

                    // Serialize the code or coding to json for display purposes in the issue
                    var jsonSer = new FhirJsonSerializer();
                    var codeDisplay = codeableConcept != null ? jsonSer.SerializeToString(codeableConcept)
                                            : jsonSer.SerializeToString(coding);

                    outcome.AddIssue($"Validation of '{codeDisplay}' failed, but" +
                                $"the terminology service at {Endpoint.Endpoint} did not provide further details.",
                                Issue.TERMINOLOGY_CODE_NOT_IN_VALUESET);
                }
            }

            return outcome;
        }
    }
}
