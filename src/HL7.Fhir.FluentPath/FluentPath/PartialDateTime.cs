/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Support;
using System;
using System.Linq;
using System.Xml;

namespace Hl7.Fhir.FluentPath
{

    //TODO: Merge with FhirDateTime from Model namespace?s
    public class PartialDateTime
    {
        private PartialDateTime()
        {

        }

        private string _value;

        public static PartialDateTime Parse(string value)
        {
            try
            {
                var dummy = XmlConvert.ToDateTimeOffset(value);
            }
            catch
            {
                throw new FormatException("Partial is in an invalid format, should use ISO8601 YYYY-MM-DDThh:mm:ss+TZ notation");
            }

            // Look for values like Thh:mm or hh:mm
            if (value.IndexOf(":") == 2 || value.IndexOf(":") == 3)
                throw Error.NotSupported("Partial date times cannot contain just a time");

            return new PartialDateTime { _value = value };
        }

        public static bool TryParse(string representation, out PartialDateTime value)
        {
            try
            {
                value = PartialDateTime.Parse(representation);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }

        // overload operator <
        public static bool operator < (PartialDateTime a, PartialDateTime b)
        {
            return XmlConvert.ToDateTimeOffset(a._value).ToUniversalTime() < XmlConvert.ToDateTimeOffset(b._value).ToUniversalTime();
        }

        // overload operator >
        public static bool operator >(PartialDateTime a, PartialDateTime b)
        {
            return XmlConvert.ToDateTimeOffset(a._value).ToUniversalTime() > XmlConvert.ToDateTimeOffset(b._value).ToUniversalTime();
        }

        public override bool Equals(object obj)
        {
            if (obj is PartialDateTime)
                return (obj as PartialDateTime)._value == _value;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }



        public override string ToString()
        {
            return _value;
        }

        public static PartialDateTime Now()
        {
            return FromDateTime(DateTimeOffset.Now);
        }

        public static PartialDateTime Today()
        {
            return new PartialDateTime { _value = DateTimeOffset.Now.ToString("yyyy-MM-dd") };
        }

        public static PartialDateTime FromDateTime(DateTimeOffset dto)
        {
            return new PartialDateTime { _value = XmlConvert.ToString(dto) };
        }

        public static PartialDateTime FromDateTime(DateTime dt)
        {
            return new PartialDateTime { _value = XmlConvert.ToString(dt,XmlDateTimeSerializationMode.RoundtripKind) };
        }

    }
}
