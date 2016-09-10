/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Linq;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Validation
{
    internal static class TypeRefExtensions
    {
        public static string ProfileUri(this ElementDefinition.TypeRefComponent typeRef)
        {
            if (!string.IsNullOrEmpty(typeRef.Profile))
            {
                return typeRef.Profile;
            }
            else
            {
                return "http://hl7.org/fhir/StructureDefinition/" + typeRef.Code;
            }
        }

        public static string ToHumanReadable(this ElementDefinition.TypeRefComponent typeRef)
        {
            var result = typeRef.Code;

            if (!string.IsNullOrEmpty(typeRef.Profile))
                result += " ({0})".FormatWith(typeRef.Profile);

            return result;
        }

        public static string GetPrimitiveValueRegEx(this ElementDefinition.TypeRefComponent typeRef)
        {
            var regex = typeRef.GetStringExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-regex");

            if (regex == null) return null;

            if (regex.StartsWith("urn:oid:")) regex = regex.Substring(8);
            return regex;
        }
    }

}