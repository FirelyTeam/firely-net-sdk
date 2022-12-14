using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Model
{
    public static class ParametersExtensions
    {
        public static bool TryGetDuplicates(this Parameters parameters , out IEnumerable<string> duplicates)
        {
            duplicates =  parameters.Parameter?.Select(p => p.Name)?
                          .GroupBy(x => x)
                          .Where(g => g.Count() > 1)
                          .Select(y => y.Key)
                          .ToList();

            return duplicates?.Any() == true;
        }
    }
}
