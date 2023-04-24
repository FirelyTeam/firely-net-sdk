/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System.Linq;

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
            var coding = codeableConcept.Coding.FirstOrDefault();
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

#nullable restore