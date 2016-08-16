using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Core.ElementModel;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    internal static class TypeRefExtensions
    {
        public static string ProfileUri(this ElementDefinition.TypeRefComponent typeRef)
        {
            if (typeRef.Profile.Any())
            {
                return typeRef.Profile.First();
            }
            else
                return "http://hl7.org/fhir/StructureDefinition/" + typeRef.Code.GetLiteral();
        }

        public static string ToHumanReadable(this ElementDefinition.TypeRefComponent typeRef)
        {
            var result = typeRef.Code.GetLiteral();

            if (typeRef.Profile != null)
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