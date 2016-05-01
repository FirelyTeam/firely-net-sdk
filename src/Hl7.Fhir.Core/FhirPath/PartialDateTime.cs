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

namespace Hl7.Fhir.FhirPath
{

    //TODO: Merge with FhirDateTime from Model namespace?s
    public class PartialDateTime
    {
        private PartialDateTime()
        {
        }

        internal PartialDateTime(DateTimeOffset value, Precision prec)
        {
            Value = value;
            Prec = prec;
        }

        internal PartialDateTime(DateTimeOffset dto)
        {
            Value = dto;
            Prec = Precision.Time;
        }
        public enum Precision
        {
            Year = 0,
            Month = 1,
            Day = 2,
            Time = 3,
        }

        public DateTimeOffset Value;
        public Precision Prec;

        public static PartialDateTime Parse(string value)
        {
            var dtValue = XmlConvert.ToDateTimeOffset(value);

            if (value.Contains(":") && !value.Contains("T"))
                throw Error.NotSupported("Partial date times cannot contain just a time");

            var precCount = value.Count(c => c == '-') + (value.Contains("T") ? 1 : 0);
            var prec = (PartialDateTime.Precision)precCount;

            return new PartialDateTime { Value = dtValue, Prec = prec };
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
            return a.Value < b.Value;
        }

        // overload operator >
        public static bool operator >(PartialDateTime a, PartialDateTime b)
        {
            return a.Value > b.Value;
        }

        // overload operator <=
        public static bool operator <=(PartialDateTime a, PartialDateTime b)
        {
            return a.Value <= b.Value;
        }

        // overload operator >=
        public static bool operator >=(PartialDateTime a, PartialDateTime b)
        {
            return a.Value >= b.Value;
        }

        // overload operator ==
        public static bool operator ==(PartialDateTime a, PartialDateTime b)
        {
            return a.Value == b.Value;
        }

        // overload operator !=
        public static bool operator !=(PartialDateTime a, PartialDateTime b)
        {
            return a.Value != b.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is PartialDateTime)
                return (obj as PartialDateTime).Value == Value;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            var representation = XmlConvert.ToString(Value);

            switch (Prec)
            {
                case Precision.Year:
                    return representation.Substring(0, 4);
                case Precision.Month:
                    return representation.Substring(0, 7);
                case Precision.Day:
                    return representation.Substring(0, 10);
                case Precision.Time:
                    return representation;
                default:
                    return representation;
            }
        }

        public static PartialDateTime Now()
        {
            return new PartialDateTime { Value = DateTimeOffset.Now, Prec = Precision.Time };
        }
    }
}
