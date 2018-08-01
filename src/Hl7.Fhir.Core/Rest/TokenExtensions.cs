using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    internal static class TokenExtensions
    {
        internal static string ToToken(this Coding coding)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(coding.System))
            {
                result += $"{coding.System}|";
            }
            return result += coding.Code;
        }

        internal static string ToToken(this CodeableConcept codeableConcept)
        {
            Coding coding = codeableConcept.Coding.FirstOrDefault();
            return (coding != null) ? coding.ToToken() : string.Empty;
        }

        internal static string ToToken(this Identifier identifier)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(identifier.System))
            {
                result += $"{identifier.System}|";
            }
            return result += identifier.Value;
        }

        internal static string ToToken(this ContactPoint contactPoint)
        {
            var result = string.Empty;
            if (contactPoint.Use.HasValue)
            {
                result += $"{contactPoint.Use}|";
            }
            return result += contactPoint.Value;
        }
    }
}
