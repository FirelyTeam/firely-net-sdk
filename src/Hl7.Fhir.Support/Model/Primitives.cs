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
        //public static readonly string[] PRIMITIVE_TYPES = new[] { "boolean", "integer", "unsignedInt",
        //        "positiveInt","time","instant","date","dateTime","decimal",
        //        "string","code","id","uri","oid","uuid","canonical","url","markdown",
        //         "base64Binary", "xhtml" };

        public static bool IsPrimitive(string typeName) => Char.IsLower(typeName[0]);
        // PRIMITIVE_TYPES.Contains(typeName);


        /// <summary>
        /// Derives the basic FHIR type name from a C# primitive.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>This function maps a primitive .NET value unto the subset of types supported by FhirPath.</remarks>
        public static string GetPrimitiveTypeName(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value is Boolean)
                return "boolean";
            else if (value is Int32 || value is Int16 || value is Int64 || value is UInt16 || value is UInt32 || value is UInt64)
                return "integer";
            else if (value is PartialTime)
                return "time";
            else if (value is PartialDateTime || value is DateTimeOffset)
                return "dateTime";
            else if (value is float || value is double || value is Decimal)
                return "decimal";
            else if (value is String || value is char || value is Uri)
                return "string";
            else
                throw Error.NotSupported($"Don't know which primitive ITypedElement value represents an instance of .NET type {value.GetType().Name} (with value '{value}').");
        }


        public static object ToPrimitiveValue(object value)
        {
            object Value;

            if (value is Boolean)
                Value = value;
            else if (value is Int32 || value is Int16 || value is UInt16 || value is UInt32 || value is Int64 || value is UInt64)
                Value = Convert.ToInt64(value);
            else if (value is PartialTime)
                Value = value;
            else if (value is DateTimeOffset)
                Value = PartialDateTime.FromDateTime((DateTimeOffset)value);
            else if (value is PartialDateTime)
                Value = value;
            else if (value is float || value is double || value is Decimal)
                Value = Convert.ToDecimal(value);
            else if (value is String)
                Value = value;
            else if (value is char)
                Value = new String((char)value, 1);
            else if (value is Uri)
                Value = ((Uri)value).OriginalString;
            else
                throw Error.NotSupported($"Don't know how to convert an instance of .NET type {value.GetType().Name} (with value '{value}') to a primitive ITypedElement value");
            return Value;
        }

        /// <summary>
        /// Returns the .NET type used to represent the given FHIR primitive type.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
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
                case "xhtml":
                    return typeof(string);
                default:
                    throw Error.Argument(nameof(typeName), $"Type '{typeName}' is not a primitive type and its value cannot be parsed.");
            }
        }
    }
}
