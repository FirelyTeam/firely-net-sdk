/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable


using Hl7.Fhir.Utility;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using static Hl7.Fhir.Utility.Result;

namespace Hl7.Fhir.ElementModel.Types
{
    /// <summary>
    /// UCUM does not contain codes for calendar units. To support both the UCUM 'a' and 'mo' and
    /// the calender year and month, we keep track of multiple coding system for units.
    /// </summary>
    public enum QuantityUnitSystem
    {
        /// <summary>
        /// Unit is taken from the UCUM coding system (default).
        /// </summary>
        UCUM,

        /// <summary>
        /// Unit is taken from the set of calendar units (year or month)
        /// </summary>
        CalendarDuration
    }


    public class Quantity : Any, IComparable, ICqlEquatable, ICqlOrderable, ICqlConvertible
    {
        public const string UCUM = "http://unitsofmeasure.org";
        public const string UCUM_UNIT = "1";

        public decimal Value { get; }
        public string Unit { get; }

        public QuantityUnitSystem System { get; private set; }

        public Quantity(decimal value, string? unit = UCUM_UNIT)
            : this(value, unit, QuantityUnitSystem.UCUM)
        {
            // nothing
        }

        public Quantity(decimal value, string? unit, QuantityUnitSystem system)
        {
            Value = value;
            Unit = unit ?? UCUM_UNIT;
            System = system;
        }

        /// <summary>
        /// Construct a non-UCUM calendar duration (currently only 'year' and 'month').
        /// </summary>
        /// <param name="value"></param>
        /// <param name="calendarUnit"></param>
        /// <returns></returns>
        public static Quantity ForCalendarDuration(decimal value, string calendarUnit)
        {
            return calendarUnit is not null
                ? new Quantity(value, calendarUnit, QuantityUnitSystem.CalendarDuration)
                : throw new ArgumentNullException(nameof(calendarUnit));
        }

        private static readonly string QUANTITY_BASE_REGEX =
           @"(?'value'(\+|-)?\d+(\.\d+)?)\s*(('(?'unit'[^\']+)')|(?'time'[a-zA-Z]+))";

        public static readonly Regex QUANTITYREGEX =
           new(QUANTITY_BASE_REGEX, RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        internal static readonly Regex QUANTITYREGEX_FOR_PARSE =
            new($"^{QUANTITY_BASE_REGEX}?$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public static Quantity Parse(string representation) =>
                TryParse(representation, out var result) ? result : throw new FormatException($"String '{representation}' was not recognized as a valid quantity.");

        public static bool TryParse(string representation, out Quantity quantity)
        {
            if (representation is null) throw new ArgumentNullException(nameof(representation));

            quantity = new Quantity(default);

            var result = QUANTITYREGEX_FOR_PARSE.Match(representation);
            if (!result.Success) return false;

            if (!Decimal.TryParse(result.Groups["value"].Value, out var value))
                return false;

            if (result.Groups["unit"].Success)
            {
                quantity = new Quantity(value!, result.Groups["unit"].Value);
                return true;
            }
            else if (result.Groups["time"].Success)
            {
                if (TryParseTimeUnit(result.Groups["time"].Value, out var tv, out var isCalendarUnit))
                {
                    quantity = isCalendarUnit ? ForCalendarDuration(value!, tv!)
                        : new Quantity(value!, tv!);
                    return true;
                }
                else
                    return false;
            }
            else
            {
                quantity = new Quantity(value!, unit: UCUM_UNIT);
                return true;
            }
        }

        /// <summary>
        /// Parses the literal time units either to UCUM or to a non-UCUM calendar unit.
        /// </summary>
        /// <param name="unitLiteral">The time unit as found in a quantity literal</param>
        /// <param name="unit">The parsed unit, either as a UCUM code or a non-UCUM calender unit.</param>
        /// <param name="isCalendarUnit">True is this is a non-UCUM calendar unit.</param>
        /// <returns>True if this is a recognized time unit literal, false otherwise.</returns>
        public static bool TryParseTimeUnit(string unitLiteral, out string? unit, out bool isCalendarUnit)
        {
            if (unitLiteral is null) throw new ArgumentNullException(nameof(unitLiteral));

            unit = parse(out isCalendarUnit);
            return unit != null;

            string? parse(out bool isCalendarUnit)
            {
                isCalendarUnit = false;

                switch (unitLiteral)
                {
                    case "year":
                    case "years":
                        isCalendarUnit = true;
                        return "year";
                    case "month":
                    case "months":
                        isCalendarUnit = true;
                        return "month";
                    case "week":
                    case "weeks":
                        isCalendarUnit = true;
                        return "week";
                    case "day":
                    case "days":
                        isCalendarUnit = true;
                        return "day";
                    case "hour":
                    case "hours":
                        isCalendarUnit = true;
                        return "hour";
                    case "minute":
                    case "minutes":
                        isCalendarUnit = true;
                        return "minute";
                    case "second":
                    case "seconds":
                        isCalendarUnit = true;
                        return "second";
                    case "millisecond":
                    case "milliseconds":
                        isCalendarUnit = true;
                        return "millisecond";
                    default:
                        return null;
                }
            }
        }

        public const QuantityComparison CQL_EQUALS_COMPARISON = QuantityComparison.None;
        public const QuantityComparison CQL_EQUIVALENCE_COMPARISON = QuantityComparison.CompareCalendarUnits;

        /// <summary>
        /// Compare two quantities based on CQL equality rules.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true if the values have comparable units, and the converted values are the same according to decimal equality rules.
        /// </returns>
        /// <remarks>See <see cref="TryCompareTo(Any)"/> for more details.
        /// According to the .NET documentation Equals(object obj) cannot throw
        /// an exception. That is why we make sure that a bool is always returned. See 
        /// <see href="https://docs.microsoft.com/en-us/dotnet/api/system.object.equals"/></remarks>
        public override bool Equals(object? obj) => obj is Any other && Equals(other, CQL_EQUALS_COMPARISON);

        public bool Equals(Any other, QuantityComparison comparisonType) =>
            other is Quantity q && TryEquals(q, comparisonType).ValueOrDefault(false);

        /// <summary>
        /// Compares two quantities according to CQL equivalence rules.
        /// </summary>
        /// <remarks>For time-valued quantities, the comparison of
        /// calendar durations and definite quantity durations above seconds is determined by the <paramref name="comparisonType"/></remarks>
        public Result<bool> TryEquals(Any other, QuantityComparison comparisonType) => other is Quantity ?
            TryCompareTo(other, comparisonType).Select(i => i == 0) : false;

        public static bool operator ==(Quantity a, Quantity b) => Equals(a, b);
        public static bool operator !=(Quantity a, Quantity b) => !Equals(a, b);

        /// <summary>
        /// Compare two datetimes based on CQL equivalence rules
        /// </summary>
        /// <remarks>See <see cref="TryCompareTo(Any)"/> for more details.</remarks>
        public int CompareTo(object? obj) => obj is Quantity q ?
            TryCompareTo(q, CQL_EQUIVALENCE_COMPARISON).ValueOrThrow() : throw NotSameTypeComparison(this, obj);

        public static bool operator <(Quantity a, Quantity b) => a.CompareTo(b) < 0;
        public static bool operator <=(Quantity a, Quantity b) => a.CompareTo(b) <= 0;
        public static bool operator >(Quantity a, Quantity b) => a.CompareTo(b) > 0;
        public static bool operator >=(Quantity a, Quantity b) => a.CompareTo(b) >= 0;

        /// <summary>
        /// Compares two quantities according to CQL ordering rules.
        /// </summary> 
        /// <returns>the result of the comparison: 0 if this and other are equal, 
        /// -1 if this is smaller than other and +1 if this is bigger than other.</returns>
        /// <remarks>the dimensions of each quantity must be the same, but not necessarily the unit. For example, units of 'cm' and 'm' can be compared, 
        /// but units of 'cm2' and 'cm' cannot. The comparison will be made using the most granular unit of either input. 
        /// Quantities with invalid units cannot be compared. 
        /// NOTE: in the current normative specification, there is a difference between comparing incompatible duration units (result: {}) 
        /// and performing the equals operator on incompatible units (result: false). This is going to be corrected
        /// (see https://jira.hl7.org/browse/FHIR-28144), and this code already reflects this decision.</remarks>
        public Result<int> TryCompareTo(Any other, QuantityComparison comparisonType)
        {
            if (other is null) return 1; // as defined by the .NET framework guidelines
            if (other is not Quantity otherQ) throw NotSameTypeComparison(this, other);

            if (IsDuration && otherQ.IsDuration) return doDurationComparison(otherQ, comparisonType);

            if (System != QuantityUnitSystem.UCUM || otherQ.System != QuantityUnitSystem.UCUM)
            {
                return Fail<int>(Error.NotSupported("Comparing quantities with system other than UCUM is not supported"));
            }

            Quantity? left = this;
            Quantity? right = otherQ;

            if (Unit != otherQ.Unit)
            {
                // align units with each other
                if (!this.TryCanonicalize(out left)) left = this;
                if (!otherQ.TryCanonicalize(out right)) right = otherQ;
            }

            return (left!.Unit == right!.Unit)
                ? decimal.Compare(Math.Round(left.Value, 8), Math.Round(right.Value, 8))   // aligns with Decimal
                : Fail<int>(Error.InvalidOperation($"UCUM quanties with unit '{left.Unit}' and '{right.Unit}' cannot be compared."));
        }

        private Result<int> doDurationComparison(Quantity other, QuantityComparison comparisonType)
        {
            var l = normalizeToUcum(this);
            var r = normalizeToUcum(other);

            if (l.Unit != r.Unit)
                return Fail<int>(Error.InvalidOperation($"Durations of {l.Unit} and {r.Unit} cannot be compared,"));
            else
                return decimal.Compare(Math.Round(l.Value, 8), Math.Round(r.Value, 8));   // aligns with Decimal

            Quantity normalizeToUcum(Quantity orig)
            {
                // UCUM definite durations are already in their comparable form
                if (orig.IsDefiniteDuration) return orig;

                var ucumUnit = orig.Unit switch
                {
                    "year" when comparisonType == QuantityComparison.CompareCalendarUnits => "a",
                    "year" => "year",
                    "month" when comparisonType == QuantityComparison.CompareCalendarUnits => "mo",
                    "month" => "month",
                    "week" => "wk",
                    "day" => "d",
                    "hour" => "h",
                    "minute" => "min",
                    "second" => "s",
                    "millisecond" => "ms",
                    _ => throw new InvalidOperationException($"Unit '{orig.Unit}' is not a known calendar duration.")
                };

                return new(orig.Value, ucumUnit, QuantityUnitSystem.UCUM);
            }
        }

        // The UCUM library probably has a more generic method for doing this.
        public bool IsDefiniteDuration => Unit is "a" or "mo" or "wk" or "d" or "h" or "min" or "s" or "ms";

        public bool IsCalendarDuration => System == QuantityUnitSystem.CalendarDuration;

        public bool IsDuration => IsDefiniteDuration || IsCalendarDuration;

        /// <summary>
        /// Compares two quantities according to CQL ordering rules.
        /// </summary> 
        /// <remarks>By default, calendar units (except year and month) are considered comparable to
        /// definite time units (execpt y and mo). Use the <see cref="TryCompareTo(Any, QuantityComparison)"/> overload
        /// to specify comparison behaviour for date comparisons.</remarks>
        public Result<int> TryCompareTo(Any other) => TryCompareTo(other, CQL_EQUIVALENCE_COMPARISON);

        public static bool operator +(Quantity a, Quantity b) => throw Error.NotSupported("Adding two quantites is not yet supported");
        public static bool operator -(Quantity a, Quantity b) => throw Error.NotSupported("Subtracting two quantites is not yet supported");

        public static bool operator *(Quantity a, Quantity b) => throw Error.NotSupported("Multiplying two quantites is not yet supported");
        public static bool operator /(Quantity a, Quantity b) => throw Error.NotSupported("Dividing two quantites is not yet supported");

        public override int GetHashCode() => (Unit, Value).GetHashCode();

        public override string ToString() => $"{Value.ToString(CultureInfo.InvariantCulture)} '{Unit}'";

        bool? ICqlEquatable.IsEqualTo(Any other) =>
            other is { } && TryEquals(other, CQL_EQUALS_COMPARISON) is Ok<bool> ok ? ok.Value : (bool?)null;

        // Note that, in contrast to equals, this will return false if operators cannot be compared (as described by the spec)
        bool ICqlEquatable.IsEquivalentTo(Any other) => other is { } && TryEquals(other, CQL_EQUIVALENCE_COMPARISON).ValueOrDefault(false);

        int? ICqlOrderable.CompareTo(Any other) => other is { } && TryCompareTo(other) is Ok<int> ok ? ok.Value : (int?)null;

        public static explicit operator String(Quantity q) => ((ICqlConvertible)q).TryConvertToString().ValueOrThrow();

        Result<String> ICqlConvertible.TryConvertToString() => Ok(new String(ToString()));

        Result<Quantity> ICqlConvertible.TryConvertToQuantity() => Ok(this);

        Result<Code> ICqlConvertible.TryConvertToCode() => CannotCastTo<Code>(this);
        Result<Boolean> ICqlConvertible.TryConvertToBoolean() => CannotCastTo<Boolean>(this);
        Result<Date> ICqlConvertible.TryConvertToDate() => CannotCastTo<Date>(this);
        Result<DateTime> ICqlConvertible.TryConvertToDateTime() => CannotCastTo<DateTime>(this);
        Result<Decimal> ICqlConvertible.TryConvertToDecimal() => CannotCastTo<Decimal>(this);
        Result<Integer> ICqlConvertible.TryConvertToInteger() => CannotCastTo<Integer>(this);
        Result<Long> ICqlConvertible.TryConvertToLong() => CannotCastTo<Long>(this);
        Result<Ratio> ICqlConvertible.TryConvertToRatio() => CannotCastTo<Ratio>(this);
        Result<Time> ICqlConvertible.TryConvertToTime() => CannotCastTo<Time>(this);
        Result<Concept> ICqlConvertible.TryConvertToConcept() => CannotCastTo<Concept>(this);
    }

    /// <summary>Specifies the comparison rules for quantities.</summary>
    /// <remarks>Options are aligned with the equality and equivalence  operations for quantities
    /// defined in the CQL specification. See https://cql.hl7.org/09-b-cqlreference.html#comparison-operators-4 
    /// for more details.
    /// </remarks>
    [Flags]
    public enum QuantityComparison
    {
        None = 0,

        /// <summary>
        /// For time-valued quantities: calendar durations and definite quantity durations are considered comparable (and equivalent).
        /// </summary>
        CompareCalendarUnits = 1
    }

}
