using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Hl7.Fhir.Model
{
    public static class ParametersExtensions
    {
        private const string CODEATTRIBUTE = "code";
        private const string SYSTEMATTRIBUTE = "system";
        private const string CONTEXTATTRIBUTE = "context";

        public static bool TryGetDuplicates(this Parameters parameters, out IEnumerable<string> duplicates)
        {
            duplicates = parameters.Parameter?.Select(p => p.Name)?
                          .GroupBy(x => x)
                          .Where(g => g.Count() > 1)
                          .Select(y => y.Key)
                          .ToList();

            return duplicates?.Any() == true;
        }

        internal static void CheckForValidityOfValidateCodeParams(this Parameters parameters)
        {

            //No duplicate parameters allowed (http://hl7.org/fhir/valueset-operation-validate-code.html)
            if (parameters.TryGetDuplicates(out var duplicates) == true)
            {
                //422 Unproccesable Entity
                throw new FhirOperationException($"List of input parameters contains the following duplicates: {string.Join(", ", duplicates)}", (HttpStatusCode)422);
            }
            //If a code is provided, a system or a context must be provided (http://hl7.org/fhir/valueset-operation-validate-code.html)
            if (parameters.Parameter.Any(p => p.Name == CODEATTRIBUTE) && !(parameters.Parameter.Any(p => p.Name == SYSTEMATTRIBUTE) ||
                                                                                    parameters.Parameter.Any(p => p.Name == CONTEXTATTRIBUTE)))
            {
                //422 Unproccesable Entity
                throw new FhirOperationException($"If a code is provided, a system or a context must be provided", (HttpStatusCode)422);
            }
        }
    }
}
