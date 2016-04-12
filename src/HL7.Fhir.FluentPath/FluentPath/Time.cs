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
    public class Time
    {
        private Time()
        {

        }

        private string _value;

        public static Time Parse(string value)
        {
            try
            {
                var dummy = XmlConvert.ToDateTimeOffset("2016-01-01" + value);
            }
            catch
            {
                throw new FormatException("Time value is in an invalid format, should conform to the time part (including the 'T') of ISO8601");
            }

            return new Time { _value = value };
        }

        public static bool TryParse(string representation, out Time value)
        {
            try
            {
                value = Time.Parse(representation);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }

     
        // overload operator <
        public static bool operator <(Time a, Time b)
        {
            return XmlConvert.ToDateTimeOffset("2016-01-01" + a._value).ToUniversalTime() < XmlConvert.ToDateTimeOffset("2016-01-01" + b._value).ToUniversalTime();
        }

        // overload operator >
        public static bool operator >(Time a, Time b)
        {
            return XmlConvert.ToDateTimeOffset("2016-01-01" + a._value).ToUniversalTime() < XmlConvert.ToDateTimeOffset("2016-01-01" + b._value).ToUniversalTime();
        }

        public override bool Equals(object obj)
        {
            if (obj is Time)
                return (obj as Time)._value == _value;
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

        public static Time Now()
        {
            return new Time { _value = XmlConvert.ToString(DateTimeOffset.Now).Substring(10) };
        }
    }
}
