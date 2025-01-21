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
        private const string URLATTRIBUTE = "url";
        private const string CONTEXTATTRIBUTE = "context";
        private const string VALUESETATTRIBUTE = "valueSet";

        public static bool TryGetDuplicates(this Parameters parameters, out IEnumerable<string> duplicates)
        {
            duplicates = parameters.Parameter?.Select(p => p.Name)?
                          .GroupBy(x => x)
                          .Where(g => g.Count() > 1)
                          .Select(y => y.Key)
                          .ToList();

            return duplicates?.Any() == true;
        }

        internal static Parameters NoDuplicates(this Parameters parameters)
        {
            //No duplicate parameters allowed (http://hl7.org/fhir/valueset-operation-validate-code.html)
            if (parameters.TryGetDuplicates(out var duplicates) == true)
            {
                //422 Unproccesable Entity
                throw new FhirOperationException($"List of input parameters contains the following duplicates: {string.Join(", ", duplicates)}", (HttpStatusCode)422);
            }

            return parameters;
        }

        internal static void CheckForValidityOfValidateCodeParams(this Parameters parameters)
        {
            parameters.NoDuplicates();

            //This error was changed from system to url. See: https://chat.fhir.org/#narrow/channel/179202-terminology/topic/Required.20.24validate-code.20parameters/near/482250225
            //If a code is provided, an inline valueset, url or a context must be provided (http://hl7.org/fhir/valueset-operation-validate-code.html)
            if (parameters.HasParam(CODEATTRIBUTE) && !hasValueSet(parameters))
            {
                //422 Unproccesable Entity
                throw new FhirOperationException($"If a code is provided, a url or a context must be provided", (HttpStatusCode)422);
            }

            static bool hasValueSet(Parameters p) =>
                p.HasParam(URLATTRIBUTE) || p.HasParam(CONTEXTATTRIBUTE) || p.HasParam(VALUESETATTRIBUTE);

        }

        internal static bool HasParam(this Parameters parameters, string name)
            => parameters.Parameter.Any(p => p.Name == name);
    }
}