/*
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Collections.Concurrent;
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

    /// <summary>
    /// The human-readable unit text of a FHIR Quantity this quantity was converted from (null for literals).
    /// </summary>
    /// <remarks>Like the reference (Java) engine, a FHIR Quantity without a code is ordered by its unit text.</remarks>
    internal string? UnitText { get; init; }

    /// <summary>
    /// False when this quantity was converted from a FHIR Quantity that has unit text, but no code.
    /// </summary>
    internal bool HasCode { get; init; } = true;

    /// <summary>
    /// The system of the unit code: UCUM for quantity literals, or the system of the FHIR Quantity this quantity was
    /// converted from (null when that has no code). As in the reference (Java) engine, only UCUM codes are converted.
    /// </summary>
    internal string? UnitSystem { get; init; } = UCUM;

    /// <summary>
    /// Creates a copy of this quantity with another value.
    /// </summary>
    internal Quantity WithValue(decimal value) =>
        new(value, Unit, System) { UnitText = UnitText, HasCode = HasCode, UnitSystem = UnitSystem };

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
            {
                // As in the reference engine: the unit text is the calendar word, and year and month have no UCUM code.
                UnitText = calendarUnit,
                UnitSystem = calendarUnit is "year" or "month" ? null : UCUM
            }
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
        result = other switch
        {
            // As in the reference engine, a FHIR Quantity without a code only equals another one without a code
            // (with the same unit text); only ordering also matches it to a coded quantity with that unit text.
            Quantity q when HasCode != q.HasCode => null,
            Quantity q => tryCompare(q, comparisonType, forOrdering: false, out var comparison) ? comparison == 0 : null,
            _ => false
        };

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
    /// but units of 'cm2' and 'cm' cannot. The comparison will be made using the most granular unit of either input.
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

        return tryCompare(otherQ, comparisonType, forOrdering: true, out result);
    }

    private bool tryCompare(Quantity other, QuantityComparison comparisonType, bool forOrdering, [NotNullWhen(true)] out int? result)
    {
        if (!tryGetComparableValues(other, comparisonType, forOrdering, out var left, out var right, out _))
        {
            result = null;
            return false;
        }

        result = decimal.Compare(Math.Round(left, 8), Math.Round(right, 8)); // aligns with Decimal
        return true;
    }

    /// <summary>
    /// Gets the values of this and the other quantity, expressed in the same unit, so they can be compared.
    /// </summary>
    /// <param name="other">The quantity to compare with.</param>
    /// <param name="comparisonType">How calendar durations are normalized to UCUM.</param>
    /// <param name="forOrdering">True when ordering (rather than testing equality), where plural calendar words in the
    /// unit text of a quantity without a code match the singular ones.</param>
    /// <param name="left">The value of this quantity.</param>
    /// <param name="right">The value of the other quantity, in the same unit as <paramref name="left"/>.</param>
    /// <remarks>This follows the reference (Java) engine: quantities with the same unit are compared as-is, where a
    /// FHIR Quantity without a code is matched on its unit text. Otherwise, UCUM quantities with the same canonical unit
    /// (e.g. 'g' and 'mg') are converted to the smaller of the two units. Durations are first normalized to UCUM
    /// (taking <paramref name="comparisonType"/> into account), and calendar durations are only comparable to
    /// durations with the same unit after normalization.</remarks>
    /// <param name="converted">True when the values had different units, and one of them was converted.</param>
    private bool tryGetComparableValues(Quantity other, QuantityComparison comparisonType, bool forOrdering,
        out decimal left, out decimal right, out bool converted)
    {
        left = Value;
        right = other.Value;
        converted = false;

        // A FHIR quantity with only unit text can only be compared with a quantity with the same unit text.
        if (!HasCode || !other.HasCode)
            // As in the reference engine, plural calendar words only match the singular ones when ordering.
            return forOrdering
                ? calendarWordToSingular(UnitText) == calendarWordToSingular(other.UnitText)
                : UnitText == other.UnitText;

        var l = this;
        var r = other;

        if (l.IsDuration && r.IsDuration)
        {
            l = normalizeDurationToUcum(l, comparisonType);
            r = normalizeDurationToUcum(r, comparisonType);
        }

        if (l.System == r.System && l.Unit == r.Unit) return true;

        // Comparing calendar durations with UCUM durations follows its own rules, see normalizeDurationToUcum().
        if (IsCalendarDuration || other.IsCalendarDuration) return false;

        if (!tryGetUcumConversion(l, out var lc) || !tryGetUcumConversion(r, out var rc) || lc.CanonicalUnit != rc.CanonicalUnit)
            return false;

        // Express the operand with the larger unit in the smaller unit, so no precision is lost. If that
        // does not fit in a decimal, express the other operand in the larger unit instead.
        converted = true;
        var convertLeft = lc.Factor > rc.Factor;
        try
        {
            if (convertLeft) left = l.Value * lc.Factor / rc.Factor;
            else right = r.Value * rc.Factor / lc.Factor;
        }
        catch (OverflowException)
        {
            try
            {
                if (convertLeft) right = r.Value * rc.Factor / lc.Factor;
                else left = l.Value * lc.Factor / rc.Factor;
            }
            catch (OverflowException)
            {
                return false;
            }
        }

        return true;
    }

    // Like the reference engine, the plural calendar words are the same unit text as the singular ones.
    private static string? calendarWordToSingular(string? unitText) =>
        unitText is "years" or "months" or "weeks" or "days" or "hours" or "minutes" or "seconds" or "milliseconds"
            ? unitText.Substring(0, unitText.Length - 1)
            : unitText;

    /// <summary>
    /// Determines whether the units of two quantities are comparable, following the reference (Java) engine's comparable():
    /// both need a unit code from the same system, and the codes must be the same, or UCUM codes of the same dimension.
    /// </summary>
    internal bool IsComparableTo(Quantity other)
    {
        var (ls, lu) = unitCode(this);
        var (rs, ru) = unitCode(other);

        if (ls is null || rs is null || ls != rs) return false;
        if (lu == ru) return true;

        return ls == UCUM && tryGetUcumConversion(lu, out var lc) && tryGetUcumConversion(ru, out var rc)
               && lc.CanonicalUnit == rc.CanonicalUnit;

        static (string? system, string unit) unitCode(Quantity q)
        {
            if (!q.HasCode) return (null, q.Unit);

            if (q.IsCalendarDuration)
            {
                try
                {
                    var normalized = normalizeDurationToUcum(q, QuantityComparison.None);
                    return (normalized.IsDefiniteDuration ? UCUM : null, normalized.Unit);
                }
                catch (InvalidOperationException)
                {
                    return (null, q.Unit);
                }
            }

            return (q.UnitSystem, q.Unit);
        }
    }

    private static Quantity normalizeDurationToUcum(Quantity orig, QuantityComparison comparisonType)
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

    private sealed record UcumConversion(decimal Factor, string CanonicalUnit);

    // Converting a unit with the UCUM library is relatively expensive, so the conversion of each unit is cached
    // (including units that cannot be converted).
    private static readonly ConcurrentDictionary<string, UcumConversion?> UCUM_CONVERSIONS = new();

    private const int MAX_CACHED_UCUM_CONVERSIONS = 10_000;

    /// <summary>
    /// Gets the factor to convert the UCUM unit of a quantity to its canonical unit (e.g. 0.001 to convert 'mg' to 'g').
    /// </summary>
    private static bool tryGetUcumConversion(Quantity q, [NotNullWhen(true)] out UcumConversion? conversion)
    {
        conversion = null;
        return q.System == QuantityUnitSystem.UCUM && q.UnitSystem == UCUM && q.HasCode
               && tryGetUcumConversion(q.Unit, out conversion);
    }

    private static bool tryGetUcumConversion(string unit, [NotNullWhen(true)] out UcumConversion? conversion)
    {
        if (!UCUM_CONVERSIONS.TryGetValue(unit, out conversion))
        {
            conversion = getUcumConversion(unit);

            // Units can come from data, so don't let the cache grow without bounds.
            if (UCUM_CONVERSIONS.Count >= MAX_CACHED_UCUM_CONVERSIONS) UCUM_CONVERSIONS.Clear();
            UCUM_CONVERSIONS.TryAdd(unit, conversion);
        }

        return conversion is not null;

        static UcumConversion? getUcumConversion(string unit)
        {
            try
            {
                // Units that do not convert linearly (like 'Cel') cannot be canonicalized by the library, so
                // converting 1 unit gives the factor for any value.
                if (global::Fhir.Metrics.MetricServiceExtensions.TryCanonicalize(
                        global::Fhir.Metrics.FhirMetricService.Instance.Value, (1m, unit, UCUM), out var result)
                    && result is { } c && c.Item1 != 0)
                {
                    // The library represents a dimensionless canonical unit as an empty string, FHIRPath uses '1'.
                    return new UcumConversion(c.Item1, string.IsNullOrEmpty(c.Item2) ? UCUM_UNIT : c.Item2);
                }
            }
            catch (Exception)
            {
                // The UCUM library throws a variety of exceptions for units it cannot parse or convert. Such units
                // are treated as not convertible.
            }

            return null;
        }
    }

    /// <summary>
    /// Determines whether two quantities are equivalent: their values must be equal after rounding to the precision of
    /// the least precise of the two (as for Decimal equivalence), where the precision is that of the values as written.
    /// </summary>
    /// <remarks>Quantities with the same unit are compared directly. Otherwise, UCUM quantities with the same dimension
    /// are compared in the unit of the least precise value, rounded to its precision: 4 'g' is precise to the gram, so
    /// 4 'g' ~ 4040 'mg' is true (4.04 g rounds to 4 g), while 4 'g' ~ 4600 'mg' is false.</remarks>
    private bool isEquivalentTo(Quantity other)
    {
        if (HasCode != other.HasCode) return false;

        if (!tryGetComparableValues(other, CQL_EQUIVALENCE_COMPARISON, forOrdering: false, out var left, out var right, out var converted))
            return false;

        // The same unit (possibly after normalizing durations): plain Decimal equivalence.
        if (!converted)
            return new Decimal(left).Equals(new Decimal(right), Decimal.CQL_EQUIVALENCE_COMPARISON);

        // Different UCUM units: one value was expressed in the smaller unit of the two.
        if (!tryGetUcumConversion(this, out var lc) || !tryGetUcumConversion(other, out var rc))
            return false;

        var leftIsCoarse = lc.Factor > rc.Factor;
        var (coarse, fine) = leftIsCoarse ? (Value, other.Value) : (other.Value, Value);
        var ratio = leftIsCoarse ? lc.Factor / rc.Factor : rc.Factor / lc.Factor;
        var coarseScale = Decimal.Scale(coarse, ignoreTrailingZeroes: true);
        var fineScale = Decimal.Scale(fine, ignoreTrailingZeroes: true);

        try
        {
            // Round in the unit of the least precise value, to the precision of that value. The step of the
            // coarse value (expressed in the fine unit) is ratio / 10^coarseScale, that of the fine value 1 / 10^fineScale.
            var coarseIsLessPrecise = ratio * pow10(fineScale) >= pow10(coarseScale);

            return coarseIsLessPrecise
                ? Math.Round(coarse, coarseScale) == Math.Round(fine / ratio, coarseScale)
                : Math.Round(coarse * ratio, fineScale) == Math.Round(fine, fineScale);
        }
        catch (OverflowException)
        {
            return false;
        }

        static decimal pow10(int exponent)
        {
            var result = 1m;
            for (var i = 0; i < exponent; i++) result *= 10;
            return result;
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
    /// definite time units (execpt y and mo). Use the <see cref="TryCompareTo(Hl7.Fhir.ElementModel.Types.Any?,Hl7.Fhir.ElementModel.Types.QuantityComparison,out int?)"/> overload
    /// to specify comparison behaviour for date comparisons.</remarks>
    public bool TryCompareTo(Any other, [NotNullWhen(true)] out int? result) =>
        TryCompareTo(other, CQL_EQUIVALENCE_COMPARISON, out result);

    /// <remarks>Quantities with different units can be equal (e.g. 4 'g' and 4000 'mg', or a FHIR Quantity with only
    /// unit text and one with a code), and equality rounds the values, so all quantities have the same hash code.</remarks>
    public override int GetHashCode() => nameof(Quantity).GetHashCode();

    public override string ToString() => $"{Value.ToString(CultureInfo.InvariantCulture)} '{Unit}'";

    bool? ICqlEquatable.IsEqualTo(Any? other) =>
        other is not null && TryEquals(other, CQL_EQUALS_COMPARISON, out var result) ? result: null;

    // Note that, in contrast to equals, this will return false if operators cannot be compared (as described by the spec)
    bool ICqlEquatable.IsEquivalentTo(Any? other) =>
        other is Quantity q && isEquivalentTo(q);

    int? ICqlOrderable.CompareTo(Any? other) =>
        other is not null && TryCompareTo(other, out var result)
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