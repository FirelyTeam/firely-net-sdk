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
            if (vs == null && canonical == null) throw Error.ArgumentNull($"Either {nameof(vs)} or {nameof(canonical)} should be supploed");
            if (string.IsNullOrEmpty(code)) throw Error.ArgumentNullOrEmpty(nameof(code));
            if (string.IsNullOrEmpty(system)) throw Error.ArgumentNullOrEmpty(nameof(system));

            Parameters resultValidateCode;
            var coding = new Coding(system, code, display);

            try
            {
                resultValidateCode = vs != null ? Endpoint.ValidateCode(vs, coding, new FhirBoolean(abstractAllowed))
                                            : Endpoint.ValidateCode(new FhirUri(canonical), coding, new FhirBoolean(abstractAllowed));
            }
            catch (FhirOperationException ex)
            {
                if (ex.Status == System.Net.HttpStatusCode.NotFound)
                    throw new ValueSetUnknownException(ex.Message);
                else
                    return ex.Outcome;
            }

            OperationOutcome outcome = processResult(code, system, display, resultValidateCode);

            return outcome;
        }

        private OperationOutcome processResult(string code, string system, string display, Parameters resultValidateCode)
        {
            var result = resultValidateCode.GetSingleValue<FhirBoolean>("result").Value;
            if (result == null)
                throw Error.InvalidOperation($"Terminology service at {Endpoint.Endpoint.ToString()} did not return a result.");

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
                                $"the terminology service at {Endpoint.Endpoint.ToString()} did not provide further details.",
                                Issue.TERMINOLOGY_CODE_NOT_IN_VALUESET);
            }

            return outcome;
        }
    }
}
