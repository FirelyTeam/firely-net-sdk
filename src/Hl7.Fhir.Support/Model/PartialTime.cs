/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Xml;

namespace Hl7.Fhir.Model.Primitives
{
    public struct PartialTime : IComparable
    {
        private string _value;

        public static PartialTime Parse(string value)
        {
            try
            {
                var dummy = PrimitiveTypeConverter.ConvertTo<DateTimeOffset>(toDTOParseable(value));
            }
            catch
            {
                throw new FormatException("Time value is in an invalid format, should conform to the time part of ISO8601");
            }

            return new PartialTime { _value = value };
        }

        public static bool TryParse(string representation, out PartialTime value)
        {
            try
            {
                value = PartialTime.Parse(representation);
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }

        private static string toDTOParseable(string value)
        {
            if (value.Length == 2)
                value += ":00";
            if (value.Length == 5)
                value += ":00";

            return "2016-01-01T" + value;
        }

        
        private DateTimeOffset toDTO()
        {
            return PrimitiveTypeConverter.ConvertTo<DateTimeOffset>(toDTOParseable(_value)).ToUniversalTime();
        }

     
        // overload operator <
        public static bool operator <(PartialTime a, PartialTime b)
        {
            return a.toDTO() < b.toDTO();
        }

        public static bool operator <=(PartialTime a, PartialTime b)
        {
            return a.toDTO() <= b.toDTO();
        }


        // overload operator >
        public static bool operator >(PartialTime a, PartialTime b)
        {
            return a.toDTO() > b.toDTO();
        }

        public static bool operator >=(PartialTime a, PartialTime b)
        {
            return a.toDTO() >= b.toDTO();
        }


        public static bool operator ==(PartialTime a, PartialTime b)
        {
            return Object.Equals(a, b);
        }

        public static bool operator !=(PartialTime a, PartialTime b)
        {
            return !Object.Equals(a, b);
        }

        public bool IsEquivalentTo(PartialTime other)
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

            if (obj is PartialTime)
                return ((PartialTime)obj).toDTO() == toDTO();
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

        public static PartialTime Now()
        {
            return new PartialTime { _value = PrimitiveTypeConverter.ConvertTo<string>(DateTimeOffset.Now).Substring(10) };
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is PartialTime p)
            {
                if (this < p) return -1;
                if (this > p) return 1;
                return 0;
            }
            else
                throw Error.Argument(nameof(obj), "Must be a Time");
        }

    }
}
