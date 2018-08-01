/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Specification.Terminology
{
    public class ExternalTerminologyService : ITerminologyService
    {
        public ExternalTerminologyService(IFhirClient client)
        {
            Endpoint = client;
        }

        public IFhirClient Endpoint { get; set; }

        public OperationOutcome ValidateCode(string canonical = null, string context = null, ValueSet valueSet = null, 
            string code = null, string system = null, string version = null, string display = null, 
            Coding coding = null, CodeableConcept codeableConcept = null, FhirDateTime date = null, 
            bool? @abstract = null, string displayLanguage = null)
        {
            if (!String.IsNullOrEmpty(displayLanguage))
                throw Error.NotSupported($"The '{nameof(displayLanguage)}' parameter is not supported in DSTU2 terminology services.");

            try
            {
                var resultValidateCode =
                    Endpoint.ValidateCode(
                                identifier: canonical != null ? new FhirUri(canonical) : null,
                                context: context != null ? new FhirUri(context) : null,
                                valueSet: valueSet, 
                                code: code != null ? new Code(code) : null,
                                system: system != null ? new FhirUri(system) : null,
                                version: version != null ? new FhirString(version) : null,
                                display: display != null ? new FhirString(display) : null,
                                coding: coding, codeableConcept: codeableConcept, date: date, 
                                @abstract: @abstract != null ? new FhirBoolean(@abstract) : null);

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
                throw Error.InvalidOperation($"Terminology service at {Endpoint.Endpoint.ToString()} did not return a result.");

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
                                $"the terminology service at {Endpoint.Endpoint.ToString()} did not provide further details.",
                                Issue.TERMINOLOGY_CODE_NOT_IN_VALUESET);
                }
            }

            return outcome;
        }
    }
}
