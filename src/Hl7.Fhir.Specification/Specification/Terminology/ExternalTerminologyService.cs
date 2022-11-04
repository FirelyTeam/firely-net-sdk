/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Hl7.Fhir.Support.Poco.Model;
using Hl7.Fhir.Utility;
using System;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    public class ExternalTerminologyService : ITerminologyService
    {
        public ExternalTerminologyService(BaseFhirClient client)
        {
            Endpoint = client;
        }

        public BaseFhirClient Endpoint { get; set; }

        ///<inheritdoc />
        public async Task<Parameters> ValueSetValidateCode(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync<ValueSet>(RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false) as Parameters;
            else
                return await Endpoint.InstanceOperationAsync(constructUri<ValueSet>(id), RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false) as Parameters;
        }

        ///<inheritdoc />
        public async Task<Parameters> CodeSystemValidateCode(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync<CodeSystem>(RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false) as Parameters;
            else
                return await Endpoint.InstanceOperationAsync(constructUri<CodeSystem>(id), RestOperation.VALIDATE_CODE, parameters, useGet).ConfigureAwait(false) as Parameters;
        }

        private Uri constructUri<T>(string id) where T : Resource =>
            ResourceIdentity.Build(ModelInfoExtensions.GetFhirTypeNameForType(typeof(T)), id);

        ///<inheritdoc />
        public async Task<Resource> Expand(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync<ValueSet>(RestOperation.EXPAND_VALUESET, parameters, useGet).ConfigureAwait(false);
            else
                return await Endpoint.InstanceOperationAsync(constructUri<ValueSet>(id), RestOperation.EXPAND_VALUESET, parameters, useGet).ConfigureAwait(false);
        }

        ///<inheritdoc />
        public async Task<Parameters> Lookup(Parameters parameters, bool useGet = false)
        {
            return await Endpoint.TypeOperationAsync<CodeSystem>(RestOperation.CONCEPT_LOOKUP, parameters, useGet).ConfigureAwait(false) as Parameters;
        }

        ///<inheritdoc />
        public async Task<Parameters> Translate(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync(RestOperation.TRANSLATE, FhirTypeNames.CONCEPTMAP_NAME, parameters, useGet).ConfigureAwait(false) as Parameters;
            else
                return await Endpoint.InstanceOperationAsync(ResourceIdentity.Build(FhirTypeNames.CONCEPTMAP_NAME, id), RestOperation.TRANSLATE, parameters, useGet).ConfigureAwait(false) as Parameters;
        }

        ///<inheritdoc />
        public async Task<Parameters> Subsumes(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return await Endpoint.TypeOperationAsync<CodeSystem>(RestOperation.SUBSUMES, parameters, useGet).ConfigureAwait(false) as Parameters;
            else
                return await Endpoint.InstanceOperationAsync(constructUri<CodeSystem>(id), RestOperation.SUBSUMES, parameters, useGet).ConfigureAwait(false) as Parameters;
        }

        /// <inheritdoc />
        public async Task<Resource> Closure(Parameters parameters, bool useGet = false)
        {
            return await Endpoint.WholeSystemOperationAsync(RestOperation.CLOSURE, parameters, useGet).ConfigureAwait(false);
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
                    var jsonSer = new CommonFhirJsonSerializer(ModelInspector.ForAssembly(typeof(Coding).Assembly));
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
