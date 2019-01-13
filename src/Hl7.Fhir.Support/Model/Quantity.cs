/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Model.Primitives
{
    public struct Quantity : IComparable
    {
        public const string UCUM = "http://unitsofmeasure.org";

        public decimal Value { get; }
        public string Unit { get; }
        public string System { get; }

        public Quantity(double value, string unit = "1", string system = UCUM) : this((decimal)value, unit, system)
        {
            // call other constructor
        }

        public Quantity(long value, string unit = "1", string system = UCUM) : this((decimal)value, unit, system)
        {
            // call other constructor
        }

        public Quantity(decimal value, string unit = "1", string system = UCUM)
        {
            Value = value;
            Unit = unit ?? "1";
            System = system;
        }

        private static readonly string QUANTITY_BASE_REGEX = 
            @"(?'value'(\+|-)?\d+(\.\d+)?)\s*(('(?'unit'[^\']+)')|(?'time'[a-zA-Z]+))";

        public static readonly Regex QUANTITYREGEX =
           new Regex(QUANTITY_BASE_REGEX,
#if NETSTANDARD1_1
                        RegexOptions.ExplicitCapture);
#else
                        RegexOptions.ExplicitCapture | RegexOptions.Compiled);
#endif

        internal static readonly Regex QUANTITYREGEX_FOR_PARSE =
            new Regex($"^{QUANTITY_BASE_REGEX}?$",
#if NETSTANDARD1_1
                        RegexOptions.ExplicitCapture);
#else
                        RegexOptions.ExplicitCapture | RegexOptions.Compiled);
#endif

        public static Quantity Parse(string value)
        {
            var success = TryParse(value, out Quantity? quantity);
            return success ? quantity.Value
                : throw new FormatException("Quantity is in an invalid format");
        }

        public static bool TryParse(string representation, out Quantity? quantity)
        {
            quantity = null;

            var result = QUANTITYREGEX_FOR_PARSE.Match(representation);
            if (!result.Success) return false;

            if (!decimal.TryParse(result.Groups["value"].Value, NumberStyles.Number,
                    CultureInfo.InvariantCulture, out var value)) return false;

            if (result.Groups["unit"].Success)
            {
                quantity = new Quantity(value, result.Groups["unit"].Value);
                return true;
            }
            else if(result.Groups["time"].Success)
            {
                var tv = ParseTimeUnit(result.Groups["time"].Value);
                if (tv == null) return false;   // invalid time unit
                quantity = new Quantity(value, tv);
                return true;
            }
            else
            {
                quantity = new Quantity(value, unit: "1");
                return true;
            }
        }

        public static string ParseTimeUnit(string timeUnit)
        {
            switch (timeUnit)
            {
                case "year":
                case "years":
                    return "a";
                case "month":
                case "months":
                    return "mo";
                case "week":
                case "weeks":
                    return "wk";
                case "day":
                case "days":
                    return "d";
                case "hour":
                case "hours":
                    return "h";
                case "minute":
                case "minutes":
                    return "min";
                case "second":
                case "seconds":
                    return "s";
                case "millisecond":
                case "milliseconds":
                    return "ms";
                default:
                    return null;
            }
        }


        public static bool operator <(Quantity a, Quantity b)
        {
            enforceSameUnits(a, b);

            return a.Value < b.Value;
        }

        public static bool operator <=(Quantity a, Quantity b)
        {
            enforceSameUnits(a, b);

            return a.Value <= b.Value;
        }


        public static bool operator >(Quantity a, Quantity b)
        {
            enforceSameUnits(a, b);

            return a.Value > b.Value;
        }

        public static bool operator >=(Quantity a, Quantity b)
        {
            enforceSameUnits(a, b);

            return a.Value >= b.Value;
        }


        public static bool operator ==(Quantity a, Quantity b)
        {
            enforceSameUnits(a, b);

            return Object.Equals(a, b);
        }

        public static bool operator !=(Quantity a, Quantity b)
        {
            enforceSameUnits(a, b);

            return !Object.Equals(a, b);
        }


        private static void enforceSameUnits(Quantity a, Quantity b)
        {
            if (a.Unit + a.System != b.Unit + b.System)
                throw Error.NotSupported("Comparing quantities with different units is not yet supported");
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (obj is Quantity q)
            {
                return q.Unit == this.Unit
                        && q.Value == this.Value
                        && q.System == this.System;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Unit.GetHashCode() ^ Value.GetHashCode() ^ System.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is Quantity p)
            {
                if (this < p) return -1;
                if (this > p) return 1;
                return 0;
            }
            else
                throw Error.Argument(nameof(obj), "Must be a Quantity");
        }

        public override string ToString() => $"{Value.ToString(CultureInfo.InvariantCulture)} '{Unit}'";
    }
}
