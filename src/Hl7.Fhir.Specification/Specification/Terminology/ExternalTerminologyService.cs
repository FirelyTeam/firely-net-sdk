/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Terminology
{
    public class ExternalTerminologyService : ITerminologyService
    {
        public ExternalTerminologyService(IFhirClient client)
        {
            Endpoint = client;
        }

        public IFhirClient Endpoint { get; set; }

        public OperationOutcome ValidateCode(string canonical, string code, string system, string display = null, bool abstractAllowed = false)
                => invokeValidate(null, canonical, code, system, display, abstractAllowed);

        public OperationOutcome ValidateCode(ValueSet vs, string code, string system, string display = null, bool abstractAllowed = false)
                => invokeValidate(vs, null, code, system, display, abstractAllowed);


        public OperationOutcome invokeValidate(ValueSet vs, string canonical, string code, string system, string display = null, bool abstractAllowed = false)
        {
            var coding = new Coding(system, code, display);

            try
            {
                var resultValidateCode = vs != null ? Endpoint.ValidateCode(valueSet: vs, coding: coding, @abstract: new FhirBoolean(abstractAllowed))
                                            : Endpoint.ValidateCode(identifier: new FhirUri(canonical), coding: coding, @abstract: new FhirBoolean(abstractAllowed));

                OperationOutcome outcome = processResult(code, system, display, resultValidateCode );

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

        private OperationOutcome processResult(string code, string system, string display, ValidateCodeResult result)
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
                    outcome.AddIssue($"Validation of code '{code}', system '{system}' and display '{display}' failed, but" +
                                $"the terminology service at {Endpoint.Endpoint.ToString()} did not provide further details.",
                                Issue.TERMINOLOGY_CODE_NOT_IN_VALUESET);
            }

            return outcome;
        }
    }
}
