/*
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.ElementModel.Types;

public class Quantity(decimal value, string? unit, QuantityUnitSystem system)
    : Any, IComparable, ICqlEquatable, ICqlOrderable
{
    public const string UCUM = "http://unitsofmeasure.org";
    public const string UCUM_UNIT = "1";

    public decimal Value { get; } = value;
    public string Unit { get; } = unit ?? UCUM_UNIT;

    public QuantityUnitSystem System { get; } = system;

    public Quantity(decimal value, string? unit = UCUM_UNIT)
        : this(value, unit, QuantityUnitSystem.UCUM)
    {
        // nothing
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
        // The lookbehind rejects a line break that is only followed by whitespace at the end: without a unit,
        // the \s* in the base regex would otherwise swallow it (e.g. "40.0\n").
        new($@"^{QUANTITY_BASE_REGEX}?(?<![\r\n]\s*)\z", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    public static Quantity Parse(string representation) =>
        TryParse(representation, out var result) ? result : throw new FormatException($"String '{representation}' was not recognized as a valid quantity.");

    public static bool TryParse(string representation, [NotNullWhen(true)] out Quantity? quantity)
    {
        if (representation is null) throw new ArgumentNullException(nameof(representation));

        quantity = null;

        var result = QUANTITYREGEX_FOR_PARSE.Match(representation);
        if (!result.Success) return false;

        if (!Decimal.TryParse(result.Groups["value"].Value, out var value))
            return false;

        if (result.Groups["unit"].Success)
        {
            quantity = new Quantity(value, result.Groups["unit"].Value);
            return true;
        }
        else if (result.Groups["time"].Success)
        {
            if (TryParseTimeUnit(result.Groups["time"].Value, out var tv, out var isCalendarUnit))
            {
                quantity = isCalendarUnit
                    ? ForCalendarDuration(value, tv)
                    : new Quantity(value, tv);
                return true;
            }
            else
                return false;
        }
        else
        {
            quantity = new Quantity(value, unit: UCUM_UNIT);
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
    public static bool TryParseTimeUnit(string unitLiteral, [NotNullWhen(true)] out string? unit, out bool isCalendarUnit)
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
    /// <remarks>See <see cref="TryCompareTo(Hl7.Fhir.ElementModel.Types.Any?,Hl7.Fhir.ElementModel.Types.QuantityComparison,out int?)"/> for more details.
    /// According to the .NET documentation Equals(object obj) cannot throw
    /// an exception. That is why we make sure that a bool is always returned. See
    /// <see href="https://docs.microsoft.com/en-us/dotnet/api/system.object.equals"/></remarks>
    public override bool Equals(object? obj) => obj is Any other && Equals(other, CQL_EQUALS_COMPARISON);

    public bool Equals(Any other, QuantityComparison comparisonType) =>
        other is Quantity q && TryEquals(q, comparisonType, out var result) && result.Value;

    /// <summary>
    /// Compares two quantities according to CQL equivalence rules.
    /// </summary>
    /// <remarks>For time-valued quantities, the comparison of
    /// calendar durations and definite quantity durations above seconds is determined by the <paramref name="comparisonType"/></remarks>
    public bool TryEquals(Any other, QuantityComparison comparisonType, [NotNullWhen(true)] out bool? result)
    {
        result = other is Quantity
            ? TryCompareTo(other, comparisonType, out var comparison) ? comparison == 0 : null
            : false;

        return result is not null;
    }

    public static bool operator ==(Quantity a, Quantity b) => a.CompareTo(b) == 0;
    public static bool operator !=(Quantity a, Quantity b) => a.CompareTo(b) != 0;

    /// <summary>
    /// Compare two datetimes based on CQL equivalence rules
    /// </summary>
    /// <remarks>See <see cref="TryCompareTo(Hl7.Fhir.ElementModel.Types.Any?,Hl7.Fhir.ElementModel.Types.QuantityComparison,out int?)"/> for more details.</remarks>
    public int CompareTo(object? obj) =>
        obj is Quantity q && TryCompareTo(q, CQL_EQUIVALENCE_COMPARISON, out var result)
        ? result.Value
        : throw new InvalidOperationException("The operands are not comparable.");

    public static bool operator <(Quantity a, Quantity b) => a.CompareTo(b) < 0;
    public static bool operator <=(Quantity a, Quantity b) => a.CompareTo(b) <= 0;
    public static bool operator >(Quantity a, Quantity b) => a.CompareTo(b) > 0;
    public static bool operator >=(Quantity a, Quantity b) => a.CompareTo(b) >= 0;

    /// <summary>
    /// Compares two quantities according to CQL ordering rules.
    /// </summary>
    /// <param name="comparisonType"></param>
    /// <param name="result">The result of the comparison: 0 if this and other are equal,
    /// -1 if this is smaller than other and +1 if this is bigger than other.</param>
    /// <param name="other"></param>
    /// <remarks>the dimensions of each quantity must be the same, but not necessarily the unit. For example, units of 'cm' and 'm' can be compared,
    /// but units of 'cm2' and 'cm' cannot. Time-valued quantities are converted using the UCUM definite duration conversion factors.
    /// Quantities with invalid units cannot be compared.
    /// NOTE: in the current normative specification, there is a difference between comparing incompatible duration units (result: {})
    /// and performing the equals operator on incompatible units (result: false). This is going to be corrected
    /// (see https://jira.hl7.org/browse/FHIR-28144), and this code already reflects this decision.</remarks>
    public bool TryCompareTo(Any? other, QuantityComparison comparisonType, [NotNullWhen(true)] out int? result)
    {
        if (other is null)
        {
            result = 1; // as defined by the .NET framework guidelines
            return true;
        }

        if (other is not Quantity otherQ) throw NotSameTypeComparison(this, other);

        if (IsDuration && otherQ.IsDuration)
        {
            result = doDurationComparison(otherQ, comparisonType);
            return result is not null;
        }

        // Cannot compare quantities with different systems or units.
        if (System != otherQ.System || Unit != otherQ.Unit)
        {
            result = null;
            return false;
        }

        result = decimal.Compare(Math.Round(Value, 8), Math.Round(otherQ.Value, 8)); // aligns with Decimal
        return true;
    }

    private int? doDurationComparison(Quantity other, QuantityComparison comparisonType)
    {
        var l = normalizeToUcum(this);
        var r = normalizeToUcum(other);

        if (l.Unit == r.Unit)
            return decimal.Compare(Math.Round(l.Value, 8), Math.Round(r.Value, 8)); // aligns with Decimal

        // Definite durations can be converted into each other using the UCUM conversion factors.
        if (!DEFINITE_DURATION_SECONDS.TryGetValue(l.Unit, out var lFactor) ||
            !DEFINITE_DURATION_SECONDS.TryGetValue(r.Unit, out var rFactor))
            return null;

        // A calendar year or month (which only gets here as 'a' or 'mo' when comparing for equivalence) does not
        // have a fixed number of days, so it is only compared with years and months, not with shorter durations.
        if ((isCalendarYearOrMonth(this) || isCalendarYearOrMonth(other)) && !(isYearOrMonth(l) && isYearOrMonth(r)))
            return null;

        // Round the inputs as we do when the units are the same, then compare the converted values without
        // rounding them again: rounding after the conversion would hide real differences (e.g. 1 'wk' vs 604800001 'ms').
        var lValue = Math.Round(l.Value, 8);
        var rValue = Math.Round(r.Value, 8);

        try
        {
            return decimal.Compare(lValue * lFactor, rValue * rFactor);
        }
        catch (OverflowException)
        {
            // Values this large cannot be expressed in seconds; convert to the least granular unit instead.
            return lFactor >= rFactor
                ? decimal.Compare(lValue, rValue * (rFactor / lFactor))
                : decimal.Compare(lValue * (lFactor / rFactor), rValue);
        }

        static bool isCalendarYearOrMonth(Quantity q) => q.IsCalendarDuration && q.Unit is "year" or "month";
        static bool isYearOrMonth(Quantity q) => q.Unit is "a" or "mo";

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

            return new Quantity(orig.Value, ucumUnit, QuantityUnitSystem.UCUM);
        }
    }

    /// <summary>
    /// The UCUM definite duration units, with their size in seconds.
    /// </summary>
    private static readonly Dictionary<string, decimal> DEFINITE_DURATION_SECONDS = new()
    {
        ["a"] = 31557600m, // 365.25 'd'
        ["mo"] = 2629800m, // 1/12 'a'
        ["wk"] = 604800m,
        ["d"] = 86400m,
        ["h"] = 3600m,
        ["min"] = 60m,
        ["s"] = 1m,
        ["ms"] = 0.001m,
    };

    // The UCUM library probably has a more generic method for doing this.
    public bool IsDefiniteDuration => Unit is "a" or "mo" or "wk" or "d" or "h" or "min" or "s" or "ms";

    public bool IsCalendarDuration => System == QuantityUnitSystem.CalendarDuration;

    public bool IsDuration => IsDefiniteDuration || IsCalendarDuration;

    /// <summary>
    /// Compares two quantities according to CQL ordering rules.
    /// </summary>
    /// <remarks>By default, calendar units (except year and month) are considered comparable to
    /// definite time units (execpt y and mo). Use the <see cref="TryCompareTo(Hl7.Fhir.ElementModel.Types.Any?,Hl7.Fhir.ElementModel.Types.QuantityComparison,out int?)"/> overload
    /// to specify comparison behaviour for date comparisons.</remarks>
    public bool TryCompareTo(Any other, [NotNullWhen(true)] out int? result) =>
        TryCompareTo(other, CQL_EQUIVALENCE_COMPARISON, out result);

    public override int GetHashCode() => (Unit, Value).GetHashCode();

    /// <summary>
    /// Returns the FHIRPath string representation of the quantity: UCUM units are quoted (<c>53 'km'</c>),
    /// calendar duration keywords are not (<c>1 week</c>, <c>4 days</c>).
    /// </summary>
    public override string ToString()
    {
        var value = Value.ToString(CultureInfo.InvariantCulture);
        return IsCalendarDuration && TryParseTimeUnit(Unit, out var keyword, out var isCalendarUnit) && isCalendarUnit && keyword == Unit
            ? $"{value} {(Value == 1m ? Unit : Unit + "s")}"
            : $"{value} '{Unit}'";
    }

    bool? ICqlEquatable.IsEqualTo(Any? other) =>
        other is not null && TryEquals(other, CQL_EQUALS_COMPARISON, out var result) ? result: null;

    // Note that, in contrast to equals, this will return false if operators cannot be compared (as described by the spec)
    bool ICqlEquatable.IsEquivalentTo(Any? other) =>
        other is not null && TryEquals(other, CQL_EQUIVALENCE_COMPARISON, out var result) && result.Value;

    // Ordering uses the same rules as equality: calendar years and months cannot be compared to the UCUM 'a' and 'mo'.
    int? ICqlOrderable.CompareTo(Any? other) =>
        other is not null && TryCompareTo(other, CQL_EQUALS_COMPARISON, out var result)
            ? result
            : null;

    public static explicit operator String(Quantity q) => RunCast<String>(q);

    public override bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result)
    {
        result = null;

        if(to == typeof(Quantity))
            result = this;
        else if (to == typeof(String))
            result = new String(ToString());

        return result is not null;
    }
}