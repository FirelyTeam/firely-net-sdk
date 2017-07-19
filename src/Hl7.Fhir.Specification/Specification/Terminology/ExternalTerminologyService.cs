/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification.Terminology;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;

namespace Furore.Fhir.ValidationDemo
{
    // [WMR 20170414] Copied from Hl7.Fhir.Specification (private...?)
    sealed class ExternalTerminologyService : ITerminologyService
    {
        public ExternalTerminologyService(FhirClient client)
        {
            _endpoint = client;
        }

        FhirClient _endpoint;

        public OperationOutcome ValidateCode(string uri, string code, string system, string display = null, bool abstractAllowed = false)
        {
            if (string.IsNullOrEmpty(uri)) throw Error.ArgumentNullOrEmpty(nameof(uri));
            if (string.IsNullOrEmpty(code)) throw Error.ArgumentNullOrEmpty(nameof(code));
            if (string.IsNullOrEmpty(system)) throw Error.ArgumentNullOrEmpty(nameof(system));

            Parameters resultValidateCode;

            try
            {
                resultValidateCode = _endpoint.ValidateCode(uri, new Coding(system, code, display), new FhirBoolean(abstractAllowed));
            }
            catch (FhirOperationException ex)
            {
                return ex.Outcome;
            }

            OperationOutcome outcome = processResult(code, system, display, resultValidateCode);

            return outcome;
        }

        public OperationOutcome ValidateCode(ValueSet vs, string code, string system, string display = null, bool abstractAllowed = false)
        {
            if (vs == null) throw Error.ArgumentNull(nameof(vs));
            if (string.IsNullOrEmpty(code)) throw Error.ArgumentNullOrEmpty(nameof(code));
            if (string.IsNullOrEmpty(system)) throw Error.ArgumentNullOrEmpty(nameof(system));

            Parameters resultValidateCode;

            try
            {
                resultValidateCode = _endpoint.ValidateCode(vs, new Coding(system, code, display), new FhirBoolean(abstractAllowed));
            }
            catch (FhirOperationException ex)
            {
                return ex.Outcome;
            }

            OperationOutcome outcome = processResult(code, system, display, resultValidateCode);

            return outcome;
        }


        private OperationOutcome processResult(string code, string system, string display, Parameters resultValidateCode)
        {
            var result = resultValidateCode.GetSingleValue<FhirBoolean>("result").Value;
            if (result == null)
                throw Error.InvalidOperation($"Terminology service at {_endpoint.Endpoint.ToString()} did not return a result.");

            var outcome = new OperationOutcome();

            if (!result.Value)
            {
                string message = (resultValidateCode.Parameter
                        .Where(p => p.Name == "message")?
                        .FirstOrDefault()?.Value as FhirString)?.Value;

                if (message != null)
                    outcome.AddIssue(message, Issue.TERMINOLOGY_CODE_NOT_IN_VALUESET);
                else
                    outcome.AddIssue($"Validation of code '{code}', system '{system}' and display '{display}' failed, but" +
                                $"the terminology service at {_endpoint.Endpoint.ToString()} did not provide further details.",
                                Issue.TERMINOLOGY_CODE_NOT_IN_VALUESET);
            }

            return outcome;
        }
    }
}
