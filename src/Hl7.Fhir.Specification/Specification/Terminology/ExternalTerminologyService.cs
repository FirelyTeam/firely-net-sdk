using Hl7.Fhir.Specification.Terminology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Specification.Terminology
{
    internal class ExternalTerminologyService : ITerminologyService
    {
        public ExternalTerminologyService(FhirClient client)
        {
            _server = client;
        }

        FhirClient _server;

        public OperationOutcome ValidateCode(string uri, string code, string system, string display = null, bool abstractAllowed = false)
        {
            OperationOutcome outcome = new OperationOutcome();
            try
            {
                if (string.IsNullOrEmpty(system))
                {
                    outcome.AddIssue($"No system supplied to resolve {code} in valueset {uri}", Issue.TERMINOLOGY_SYSTEM_VALUE_MISSING);
                    return outcome;
                }
                var resultValidateCode = _server.ValidateCode(uri, new Coding(system, code, display));
                var result = resultValidateCode.GetSingleValue<FhirBoolean>("result");
                if (result.Value.HasValue)
                {
                    if (!result.Value.Value)
                    {
                        string message = (resultValidateCode.Parameter.Where(p => p.Name == "message")?.FirstOrDefault()?.Value as FhirString).Value;
                        outcome.AddIssue(message, Issue.TERMINOLOGY_CODE_NOT_IN_VALUESET);
                    }
                }
                return outcome;
            }
            catch(FhirOperationException ex)
            {
                return ex.Outcome;
            }
        }
    }
}
