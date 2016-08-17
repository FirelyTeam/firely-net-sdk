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

namespace Hl7.FluentPath
{
    public struct Time
    {
        private string _value;

        public static Time Parse(string value)
        {
            try
            {
                var dummy = XmlConvert.ToDateTimeOffset(toDTOParseable(value));
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
                value = default(Time);
                return false;
            }
        }

        private static string toDTOParseable(string value)
        {
            if (value.Length == 3)
                value += ":00";
            if (value.Length == 6)
                value += ":00";

            return "2016-01-01" + value;
        }

        
        private DateTimeOffset toDTO()
        {
            return XmlConvert.ToDateTimeOffset(toDTOParseable(_value)).ToUniversalTime();
        }

     
        // overload operator <
        public static bool operator <(Time a, Time b)
        {
            return a.toDTO() < b.toDTO();
        }

        public static bool operator <=(Time a, Time b)
        {
            return a.toDTO() <= b.toDTO();
        }


        // overload operator >
        public static bool operator >(Time a, Time b)
        {
            return a.toDTO() > b.toDTO();
        }

        public static bool operator >=(Time a, Time b)
        {
            return a.toDTO() >= b.toDTO();
        }


        public static bool operator ==(Time a, Time b)
        {
            return Object.Equals(a, b);
        }

        public static bool operator !=(Time a, Time b)
        {
            return !Object.Equals(a, b);
        }

        public bool IsEquivalentTo(Time other)
        {
            if (other == null) return false;

            var left = toDTO();
            var right = other.toDTO();

            return   (left.Year == right.Year) && (left.Month == right.Month) && (left.Day == right.Day)              
                            && (left.Hour == right.Hour) && (left.Minute == right.Minute);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (obj is Time)
                return ((Time)obj).toDTO() == toDTO();
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
