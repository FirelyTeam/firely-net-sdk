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
        /// Derives the basic FHIR type name from a C# primitive type.
        /// </summary>
        /// <param name="dotNetType">Value to determine the type for.</param>
        /// <returns></returns>
        /// <remarks>This function maps a primitive .NET value unto the subset of types supported by FhirPath.</remarks>
        public static string GetPrimitiveTypeName(Type dotNetType)
        {
            if (dotNetType == null) throw new ArgumentNullException(nameof(dotNetType));

            if (TryGetPrimitiveTypeName(dotNetType, out string result))
                return result;            
            else
                throw Error.NotSupported($"Don't know which primitive ITypedElement value represents an instance of .NET type {dotNetType.Name}.");
        }


        /// <summary>
        /// Derives the basic FHIR type name from a C# primitive type.
        /// </summary>
        /// <param name="dotNetType">The Value to determine the type for.</param>
        /// <param name="typeName">Primitive type name for the .NET primitive, or null.</param>
        /// <returns>Returns false if the function was unable to map the .NET type to a FHIR type.</returns>
        /// <remarks>This function maps a primitive .NET value unto the subset of types supported by FhirPath.</remarks>
        public static bool TryGetPrimitiveTypeName(Type dotNetType, out string typeName)
        {
            if (dotNetType == null) throw new ArgumentNullException(nameof(dotNetType));

            if (t<Boolean>())
                typeName = "boolean";
            else if (t<Int32>() || t<Int16>() || t<Int64>() || t<UInt16>() || t<UInt32>() || t<UInt64>())
                typeName = "integer";
            else if (t<PartialTime>())
                typeName = "time";
            else if (t<PartialDateTime>() || t<DateTimeOffset>())
                typeName = "dateTime";
            else if (t<float>() || t<double>() || t<Decimal>() )
                typeName = "decimal";
            else if (t<string>() || t<char>() || t<Uri>() )
                typeName = "string";
            else
                typeName = null;

            return typeName != null;

            bool t<A>() => dotNetType == typeof(A);
        }


        /// <summary>
        /// Converts a primitive .NET value to a primitive FHIR-supported primitive value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>A primitive value that is directly supported in FHIR and the .Value attribute of ITypedElement.</returns>
        public static object ConvertToPrimitiveValue(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (TryConvertToPrimitiveValue(value, out object result))
                return result;
            else
                throw Error.NotSupported($"Don't know how to convert an instance of .NET type {value.GetType().Name} (with value '{value}') to a primitive ITypedElement value");
        }

        /// <summary>
        /// Tries to converts a primitive .NET value to a primitive FHIR-supported primitive value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="primitiveValue">A primitive value that is directly supported in FHIR and the .Value attribute of ITypedElement.</param>
        /// <returns>Whether the conversion succeeded.</returns>
        public static bool TryConvertToPrimitiveValue(object value, out object primitiveValue)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value is Boolean)
                primitiveValue = value;
            else if (value is Int32 || value is Int16 || value is UInt16 || value is UInt32 || value is Int64 || value is UInt64)
                primitiveValue = Convert.ToInt64(value);
            else if (value is PartialTime)
                primitiveValue = value;
            else if (value is DateTimeOffset)
                primitiveValue = PartialDateTime.FromDateTime((DateTimeOffset)value);
            else if (value is PartialDateTime)
                primitiveValue = value;
            else if (value is float || value is double || value is Decimal)
                primitiveValue = Convert.ToDecimal(value);
            else if (value is String)
                primitiveValue = value;
            else if (value is char)
                primitiveValue = new String((char)value, 1);
            else if (value is Uri)
                primitiveValue = ((Uri)value).OriginalString;
            else
                primitiveValue = null;

            return primitiveValue != null;
        }

        /// <summary>
        /// Returns the .NET type used to represent the given FHIR primitive type.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Type GetNativeRepresentation(string typeName)
        {
            if (typeName == null) throw new ArgumentNullException(nameof(typeName));

            if (TryGetNativeRepresentation(typeName, out Type result))
                return result;
            else
                throw Error.Argument(nameof(typeName), $"Type '{typeName}' is not a primitive type and its value cannot be parsed.");         
        }


        /// <summary>
        /// Returns the .NET type used to represent the given FHIR primitive type.
        /// </summary>
        /// <param name="typeName">A FHIR type name</param>
        /// <param name="nativeType">The corresponding .NET type used by this library to represent the given type.</param>
        /// <returns></returns>
        public static bool TryGetNativeRepresentation(string typeName, out Type nativeType)
        {
            if (typeName == null) throw new ArgumentNullException(nameof(typeName));

            nativeType = switchOnType(typeName);
            return nativeType != null;

            Type switchOnType(string tn)
            {
                switch (tn)
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
                        return null;
                }
            }
        }
    }
}
