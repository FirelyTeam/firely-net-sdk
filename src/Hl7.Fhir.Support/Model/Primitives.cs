using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Support.Model
{
    public class Primitives
    {
        public static readonly string[] PRIMITIVE_TYPES = new[] { "boolean", "integer", "unsignedInt",
                "positiveInt","time","instant","date","dateTime","decimal",
                "string","code","id","uri","oid","uuid","canonical","url","markdown",
                 "base64Binary", "xhtml" };

        public static bool IsPrimitive(string typeName) => Char.IsLower(typeName[0]);
            // PRIMITIVE_TYPES.Contains(typeName);

        public static Type GetNativeRepresentation(string typeName)
        {
            switch (typeName)
            {
                case "boolean":
                    return typeof(bool);
                case "integer":
                case "unsignedInt":
                case "positiveInt":
                    return typeof(long);
                case "time":
                    return typeof(PartialTime);
                case "instant":
                case "date":
                case "dateTime":
                    return typeof(PartialDateTime);
                case "decimal":
                    return typeof(decimal);
                case "string":
                case "code":
                case "id":
                case "uri":
                case "oid":
                case "uuid":
                case "canonical":
                case "url":
                case "markdown":
                case "base64Binary":
                    return typeof(string);
                case "xhtml":
                    return typeof(string);
                default:
                    throw Error.Argument(nameof(typeName), $"Type '{typeName}' is not a primitive and its value cannot be parsed.");
            }
        }
    }
}
