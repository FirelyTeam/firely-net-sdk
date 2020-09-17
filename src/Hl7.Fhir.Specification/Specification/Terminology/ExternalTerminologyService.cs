/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Specification.Terminology;

namespace Hl7.Fhir.Specification.Terminology
{
    public class ExternalTerminologyService : ITerminologyService
    {
        public ExternalTerminologyService(BaseFhirClient client)
        {
            Endpoint = client;
        }

        public BaseFhirClient Endpoint { get; set; }

        public Parameters ValidateCode(Parameters parameters, string typeName, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(typeName)) throw Error.ArgumentNullOrEmpty(nameof(typeName));
            if (typeName != FHIRAllTypes.CodeSystem.GetLiteral() && typeName != FHIRAllTypes.ValueSet.GetLiteral())
                throw Error.Argument(nameof(Type), "Valid values for argument typeName is 'CodeSystem' and 'ValueSet'");

            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation(RestOperation.VALIDATE_CODE, typeName, parameters, useGet) as Parameters;
            else
                return Endpoint.InstanceOperation(new Uri($"{typeName}/{id}", UriKind.Relative), RestOperation.VALIDATE_CODE, parameters, useGet) as Parameters;
        }

        public Resource Expand(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation(RestOperation.EXPAND_VALUESET, FHIRAllTypes.ValueSet.GetLiteral(), parameters, useGet);
            else
                return Endpoint.InstanceOperation(new Uri($"{FHIRAllTypes.ValueSet.GetLiteral()}/{id}", UriKind.Relative), RestOperation.EXPAND_VALUESET, parameters, useGet);
        }

        public Parameters Lookup(Parameters parameters, bool useGet = false)
        {
            return Endpoint.TypeOperation<CodeSystem>(RestOperation.CONCEPT_LOOKUP, parameters, useGet) as Parameters;
        }

        public Parameters Translate(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation<ConceptMap>(RestOperation.TRANSLATE, parameters, useGet) as Parameters;
            else
                return Endpoint.InstanceOperation(new Uri($"{FHIRAllTypes.ConceptMap.GetLiteral()}/{id}", UriKind.Relative), RestOperation.TRANSLATE, parameters, useGet) as Parameters;
        }

        public Resource Subsumes(Parameters parameters, string id = null, bool useGet = false)
        {
            if (string.IsNullOrEmpty(id))
                return Endpoint.TypeOperation(RestOperation.SUBSUMES, FHIRAllTypes.CodeSystem.GetLiteral(), parameters, useGet);
            else
                return Endpoint.InstanceOperation(new Uri($"{FHIRAllTypes.CodeSystem.GetLiteral()}/{id}", UriKind.Relative), RestOperation.SUBSUMES, parameters, useGet);
        }

        public Resource Closure(Parameters parameters, bool useGet = false)
        {
            return Endpoint.WholeSystemOperation(RestOperation.CLOSURE, parameters, useGet);
        }

        [Obsolete("This method is obsolete, use method with signature 'ValidateCode(Parameters, string, string, bool)'")]
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
