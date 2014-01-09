using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Serialization
{
    public static class PrimitiveTypeConverter
    {
        public static object ConvertTo(this object value, Type to)
        {
            return Convert(value, to);            
        }

        public static object Convert(object value, Type to)
        {
            if(to == null) throw Error.ArgumentNull("to");
            if(value == null) throw Error.ArgumentNull("value");

            // No conversion necessary...
            if (value.GetType() == to) return value;

            // FHIR uses string xml serialization to serialize TO string for most types
            if (to == typeof(string))
            {
                var valueType = value.GetType();
                var valueTypeCode = Type.GetTypeCode(value.GetType());

                if (valueTypeCode != TypeCode.Object)
                {
                    // For primitive types, these string serializations conform to
                    // the Xml primitive serialization, both for json and xml
                    return convertPrimitiveToXmlString(value);
                }
                else
                {
                    // Some specific conversions from a string representation 
                    // to types in the class model are also supported
                    if (valueType == typeof(byte[]))
                        return System.Convert.ToBase64String((byte[])value);
                    else if (typeof(DateTimeOffset).IsAssignableFrom(valueType))
                        return XmlConvert.ToString((DateTimeOffset)value, FMT_FULL);
                    else if (typeof(Uri).IsAssignableFrom(valueType))
                        return ((Uri)value).ToString();
                    else
                        //Or: use default ToString()? This is more explicit
                        throw Error.NotSupported("Cannot convert from type {0} to a string", valueType.Name);
                }
            }


            // FHIR uses Xml serialization to parse FROM strings for most types
            else if (value.GetType() == typeof(string))
            {                
                var typeCode = Type.GetTypeCode(to);
                var stringValue = (string)value;

                if (typeCode != TypeCode.Object)
                {
                    // For primitive types, these string serializations conform to
                    // the Xml primitive serialization, both for json and xml
                    return convertXmlStringToPrimitive(typeCode, stringValue);
                }
                else
                {
                    // Some specific conversions from a string representation 
                    // to types in the class model are also supported
                    if (to == typeof(byte[]))
                        return System.Convert.FromBase64String(stringValue);
                    else if (to == typeof(DateTimeOffset))
                        return XmlConvert.ToDateTimeOffset(stringValue);
                    else if (to == typeof(System.Uri))
                        return new Uri(stringValue, UriKind.RelativeOrAbsolute);
                    else
                        throw Error.NotSupported("Cannot convert from string to type {0}", to.Name);
                    //TODO: enum parsing
                }
            }
            else
                // For non-string primitives use the .NET conversions to convert
                // to the desired type in the class model. Note that the xml/json readers
                // will either produce strings or primitives as values, so no other
                // conversion should be necessary, however this .NET conversion supports
                // conversion from any type implementing IConvertable
                return System.Convert.ChangeType(value, to, null);
        }


        public const string FMT_FULL = "yyyy-MM-dd'T'HH:mm:ssK";
        private const string FMT_YEAR = "{0:D4}";
        private const string FMT_YEARMONTH = "{0:D4}-{1:D2}";
        private const string FMT_YEARMONTHDAY = "{0:D4}-{1:D2}-{2:D2}";

        private static string convertPrimitiveToXmlString(object value)
        {
            TypeCode from = Type.GetTypeCode(value.GetType());

            switch (from)
            {
                case TypeCode.Boolean:
                    return XmlConvert.ToString((bool)value);
                case TypeCode.Byte:
                    return XmlConvert.ToString((byte)value);        // Not used in FHIR serialization
                case TypeCode.Char:
                    return XmlConvert.ToString((char)value);        // Not used in FHIR serialization
                case TypeCode.DateTime:
                    return XmlConvert.ToString((DateTime)value, FMT_FULL);    // TODO: validate format, not used in model
                case TypeCode.Decimal:
                    return XmlConvert.ToString((decimal)value);
                case TypeCode.Double:
                    return XmlConvert.ToString((double)value); 
                case TypeCode.Int16:
                    return XmlConvert.ToString((short)value); 
                case TypeCode.Int32:
                    return XmlConvert.ToString((int)value);
                case TypeCode.Int64:
                    return XmlConvert.ToString((long)value);       // Not used in FHIR serialization
                case TypeCode.SByte:
                    return XmlConvert.ToString((sbyte)value);       // Not used in FHIR serialization
                case TypeCode.Single:
                    return XmlConvert.ToString((float)value);      // Not used in FHIR serialization
                case TypeCode.UInt16:
                    return XmlConvert.ToString((ushort)value);      // Not used in FHIR serialization
                case TypeCode.UInt32:
                    return XmlConvert.ToString((uint)value);      // Not used in FHIR serialization
                case TypeCode.UInt64:
                    return XmlConvert.ToString((ulong)value);      // Not used in FHIR serialization
                default:
                    throw Error.NotSupported("Cannot convert {0} value '{1}' to string", from, value);
            }
        }
        
        private static object convertXmlStringToPrimitive(TypeCode to, string value)
        {
            switch(to)
            {
                case TypeCode.Boolean:
                    return XmlConvert.ToBoolean(value);
                case TypeCode.Byte:
                    return XmlConvert.ToByte(value);        // Not used in FHIR serialization
                case TypeCode.Char:
                    return XmlConvert.ToChar(value);        // Not used in FHIR serialization
                case TypeCode.DateTime:
                    return XmlConvert.ToDateTimeOffset(value); // TODO: should handle FHIR's "instant" datatype
                case TypeCode.Decimal:
                    return XmlConvert.ToDecimal(value);
                case TypeCode.Double:
                    return XmlConvert.ToDouble(value);      // Could lead to loss in precision
                case TypeCode.Int16:
                    return XmlConvert.ToInt16(value);       // Could lead to loss in precision
                case TypeCode.Int32:
                    return XmlConvert.ToInt32(value);
                case TypeCode.Int64:
                    return XmlConvert.ToInt64(value);       // Not used in FHIR serialization
                case TypeCode.SByte:
                    return XmlConvert.ToSByte(value);       // Not used in FHIR serialization
                case TypeCode.Single:
                    return XmlConvert.ToSingle(value);      // Not used in FHIR serialization
                case TypeCode.UInt16:
                    return XmlConvert.ToUInt16(value);      // Not used in FHIR serialization
                case TypeCode.UInt32:
                    return XmlConvert.ToUInt32(value);      // Not used in FHIR serialization
                case TypeCode.UInt64:
                    return XmlConvert.ToUInt64(value);      // Not used in FHIR serialization
                default:
                    throw Error.NotSupported("Cannot convert string value '{0}' to an {1}", value, to);
            }
        }

        public static T Convert<T>(object value)
        {
            return (T)Convert(value,typeof(T));
        }



        public static bool CanConvert(Type type)
        {
            var typeCode = Type.GetTypeCode(type);

            // We support all primitive .NET types in the serializer
            if (typeCode != TypeCode.Object) return true;

            // And some specific complex native types
            if (type == typeof(byte[]) ||
                 type == typeof(string) ||
                 type == typeof(DateTimeOffset) ||
                 type == typeof(Uri))
                return true;

            return false;
        }
    }
}
