/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Xml;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Serialization
{
    public static class PrimitiveTypeConverter
    {
        private static readonly string[] _forbiddenDecimalPrefixes = new[] { "+", ".", "00" };

        public static T ConvertTo<T>(object value) => (T)ConvertTo(value, typeof(T));

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
            else if (value is string str)
            {
                return convertXmlStringToPrimitive(to, str);
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

        private static string convertToXmlString(object value)
        {
            return value switch
            {
                bool bl => XmlConvert.ToString(bl),
                Byte by => XmlConvert.ToString(by),// Not used in FHIR serialization
                Char cr => XmlConvert.ToString(cr),// Not used in FHIR serialization
                DateTime dt => XmlConvert.ToString(dt, FMT_FULL),// Obsolete: use DateTimeOffset instead!!
                decimal dec => XmlConvert.ToString(dec),
                Double dbl => XmlConvert.ToString(dbl),
                Int16 i16 => XmlConvert.ToString(i16),
                Int32 i32 => XmlConvert.ToString(i32),
                Int64 i64 => XmlConvert.ToString(i64),// Not used in FHIR serialization
                SByte sb => XmlConvert.ToString(sb),// Not used in FHIR serialization
                Single sing => XmlConvert.ToString(sing),// Not used in FHIR serialization
                UInt16 uint16 => XmlConvert.ToString(uint16),// Not used in FHIR serialization
                UInt32 uint32 => XmlConvert.ToString(uint32),// Not used in FHIR serialization
                UInt64 uint64 => XmlConvert.ToString(uint64),// Not used in FHIR serialization
                byte[] barr => System.Convert.ToBase64String(barr),
                DateTimeOffset dto => XmlConvert.ToString(dto, FMT_FULL),
                Uri uri => uri.ToString(),
                P.DateTime pdt => pdt.ToString(),
                P.Time pt => pt.ToString(),
                P.Date pd => pd.ToString(),
                Enum en => en.GetLiteral(),
                BigInteger bi => bi.ToString(),
                P.Quantity q => q.ToString(),
                _ => throw Error.NotSupported($"Cannot convert '{value.GetType().Name}' value '{value}' to string"),
            };
        }

        private static object convertXmlStringToPrimitive(Type to, string value)
        {
            if (typeof(bool) == to)
                return XmlConvert.ToBoolean(value);
            if (typeof(Byte) == to)
                return XmlConvert.ToByte(value);        // Not used in FHIR serialization
            if (typeof(Char) == to)
                return XmlConvert.ToChar(value);        // Not used in FHIR serialization
            if (typeof(DateTime) == to)
                return convertToDatetimeOffset(value).UtcDateTime;  // Obsolete: use DateTimeOffset instead!!
            if (typeof(decimal) == to)
            {
                if (_forbiddenDecimalPrefixes.Any(prefix => value.StartsWith(prefix)) || value.EndsWith("."))
                {
                    // decimal cannot start with '+', '-' or '00' and cannot end with '.'
                    throw new FormatException("Input string was not in a correct format.");
                }
                return decimal.Parse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
            }
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
                return convertToDatetimeOffset(value);
            if (typeof(System.Uri) == to)
                return new Uri(value, UriKind.RelativeOrAbsolute);
            if (typeof(P.DateTime) == to)
                return P.DateTime.Parse(value);
            if (typeof(P.Date) == to)
                return P.Date.Parse(value);
            if (typeof(P.Time) == to)
                return P.Time.Parse(value);
            if (typeof(P.Quantity) == to)
                return P.Quantity.Parse(value);
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

        private static DateTimeOffset convertToDatetimeOffset(string value)
        {
            // May not be just a time spec (without a date ). Look for values like Thh:mm or hh:mm
            if (value.IndexOf(":") == 2 || value.IndexOf(":") == 3)
                throw Error.Format("A date(time) cannot contain just a time");

            if (!value.Contains("T") && value.Length <= 10)
            {
                // MV: when there is no time-part, consider this then as a UTC datetime by adding Zulu = UTC(+0)
                return XmlConvert.ToDateTimeOffset(value + "Z");
            }
            return XmlConvert.ToDateTimeOffset(value);
        }

        public static bool CanConvert(Type type)
        {
            if (Type.GetTypeCode(type) != TypeCode.Object) return true;

            // And some specific complex native types
            return
                type == typeof(byte[]) ||
                type == typeof(string) ||
                type == typeof(DateTimeOffset) ||
                type == typeof(Uri) ||
                type == typeof(P.DateTime) ||
                type == typeof(P.Date) ||
                type == typeof(P.Time) ||
                type == typeof(BigInteger);
        }
    }
}
