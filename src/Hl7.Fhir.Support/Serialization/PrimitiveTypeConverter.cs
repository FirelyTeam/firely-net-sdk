/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Xml;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Support.Model;
using System.Numerics;

namespace Hl7.Fhir.Serialization
{
    public static class PrimitiveTypeConverter
    {
        public static object FromSerializedValue(string value, string primitiveType)
        {
            var type = Primitives.GetNativeRepresentation(primitiveType);
            return ConvertTo(value, type);
        }


        public static T ConvertTo<T>(object value)
        {
            return (T)ConvertTo(value, typeof(T));
        }

        public static object ConvertTo(object value, Type to)
        {
            if (to == null) throw Error.ArgumentNull(nameof(to));
            if (value == null) throw Error.ArgumentNull(nameof(value));

            // No conversion necessary...
            if (value.GetType() == to) return value;

            // Convert TO string (mostly Xml serialization, some additional schemes used)
            if (to == typeof(string))
                return convertToXmlString(value);

            // Convert FROM string
            else if (value is string)
            {
                return convertXmlStringToPrimitive(to, (string)value);
                // Include enum parsing here
            }
            else
                // For non-string primitives use the .NET conversions to convert
                // to the desired type in the class model. Note that the xml/json readers
                // will either produce strings or primitives as values, so no other
                // conversion should be necessary, however this .NET conversion supports
                // conversion from any type implementing IConvertable
                return System.Convert.ChangeType(value, to, null);
        }

        public const string FMT_FULL = "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFK";
        private const string FMT_YEAR = "{0:D4}";
        private const string FMT_YEARMONTH = "{0:D4}-{1:D2}";
        private const string FMT_YEARMONTHDAY = "{0:D4}-{1:D2}-{2:D2}";

        private static string convertToXmlString(object value)
        {
            if (value is Boolean)
                return XmlConvert.ToString((bool)value);
            if (value is Byte)
                return XmlConvert.ToString((byte)value);        // Not used in FHIR serialization
            if (value is Char)
                return XmlConvert.ToString((char)value);        // Not used in FHIR serialization
            if (value is DateTime)
                return XmlConvert.ToString((DateTime)value, FMT_FULL);    // TODO: validate format, not used in model
            if (value is Decimal)
                return XmlConvert.ToString((decimal)value);
            if (value is Double)
                return XmlConvert.ToString((double)value);
            if (value is Int16)
                return XmlConvert.ToString((short)value);
            if (value is Int32)
                return XmlConvert.ToString((int)value);
            if (value is Int64)
                return XmlConvert.ToString((long)value);       // Not used in FHIR serialization
            if (value is SByte)
                return XmlConvert.ToString((sbyte)value);       // Not used in FHIR serialization
            if (value is Single)
                return XmlConvert.ToString((float)value);      // Not used in FHIR serialization
            if (value is UInt16)
                return XmlConvert.ToString((ushort)value);      // Not used in FHIR serialization
            if (value is UInt32)
                return XmlConvert.ToString((uint)value);      // Not used in FHIR serialization
            if (value is UInt64)
                return XmlConvert.ToString((ulong)value);      // Not used in FHIR serialization
            if (value is byte[])
                return System.Convert.ToBase64String((byte[])value);
            if (value is DateTimeOffset)
                return XmlConvert.ToString((DateTimeOffset)value, FMT_FULL);
            if (value is Uri)
                return ((Uri)value).ToString();
            if (value is PartialDateTime pdt)
                return pdt.ToString();
            if (value is PartialTime pt)
                return pt.ToString();
            if (value is Enum en)
                return en.GetLiteral();
            if (value is BigInteger bi)
                return bi.ToString();

            throw Error.NotSupported($"Cannot convert '{value.GetType().Name}' value '{value}' to string");
        }

        private static object convertXmlStringToPrimitive(Type to, string value)
        {
            if (typeof(Boolean) == to)
                return XmlConvert.ToBoolean(value);
            if (typeof(Byte) == to)
                return XmlConvert.ToByte(value);        // Not used in FHIR serialization
            if (typeof(Char) == to)
                return XmlConvert.ToChar(value);        // Not used in FHIR serialization
            if (typeof(DateTime) == to)
                return ConvertToDatetimeOffset(value).UtcDateTime;
            if (typeof(Decimal) == to)
                return XmlConvert.ToDecimal(value);
            if (typeof(Double) == to)
                return XmlConvert.ToDouble(value);      // Could lead to loss in precision
            if (typeof(Int16) == to)
                return XmlConvert.ToInt16(value);       // Could lead to loss in precision
            if (typeof(Int32) == to)
                return XmlConvert.ToInt32(value);
            if (typeof(Int64) == to)
                return XmlConvert.ToInt64(value);       // Not used in FHIR serialization
            if (typeof(SByte) == to)
                return XmlConvert.ToSByte(value);       // Not used in FHIR serialization
            if (typeof(Single) == to)
                return XmlConvert.ToSingle(value);      // Not used in FHIR serialization
            if (typeof(UInt16) == to)
                return XmlConvert.ToUInt16(value);      // Not used in FHIR serialization
            if (typeof(UInt32) == to)
                return XmlConvert.ToUInt32(value);      // Not used in FHIR serialization
            if (typeof(UInt64) == to)
                return XmlConvert.ToUInt64(value);      // Not used in FHIR serialization
            if (typeof(byte[]) == to)
                return System.Convert.FromBase64String(value);
            if (typeof(DateTimeOffset) == to)
                return ConvertToDatetimeOffset(value);
            if (typeof(System.Uri) == to)
                return new Uri(value, UriKind.RelativeOrAbsolute);
            if (typeof(PartialDateTime) == to)
                return PartialDateTime.Parse(value);
            if (typeof(PartialTime) == to)
                return PartialTime.Parse(value);
            if (typeof(BigInteger) == to)
                return BigInteger.Parse(value);
            if (to.IsEnum())
            {
                var result = EnumUtility.ParseLiteral(value, to);
                if (result == null)
                    throw Error.NotSupported($"String value '{value}' is not a known literal in enum '{to.Name}'");

                return result;
            }

            throw Error.NotSupported($"Cannot convert string value '{value}' to a {to.Name}");
        }

        private static DateTimeOffset ConvertToDatetimeOffset(string value)
        {
            if (!value.Contains("T") && value.Length <= 10)
            {
                // MV: when there is no time-part, consider this then as a UTC datetime by adding Zulu = UTC(+0)
                return XmlConvert.ToDateTimeOffset(value + "Z");
            }
            return XmlConvert.ToDateTimeOffset(value);
        }

        public static bool CanConvert(Type type)
        {
#if !DOTNETFW
			// We support all primitive .NET types in the serializer
			if (type == typeof(Boolean)
				|| type == typeof(Byte)
				|| type == typeof(char)
				|| type == typeof(DateTime)
				|| type == typeof(Decimal)
				|| type == typeof(double)
				|| type == typeof(short)
				|| type == typeof(int)
				|| type == typeof(long)
				|| type == typeof(sbyte)
				|| type == typeof(float)
				|| type == typeof(ushort)
				|| type == typeof(uint)
				|| type == typeof(ulong))
				return true;

#else
            if (Type.GetTypeCode(type) != TypeCode.Object) return true;
#endif

            // And some specific complex native types
            if (type == typeof(byte[]) ||
                 type == typeof(string) ||
                 type == typeof(DateTimeOffset) ||
                 type == typeof(Uri))
                return true;

            return false;
        }


        //public static string GetValueAsString(this Primitive p)
        //{
        //    if (p == null) return null;

        //    if (p.ObjectValue != null)
        //        return ConvertTo<string>(p.ObjectValue);
        //    else
        //        return null;
        //}

    }
}
