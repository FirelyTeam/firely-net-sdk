/*
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Fhir.Metrics;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
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

        if (l.Unit != r.Unit)
            return null;

        return decimal.Compare(Math.Round(l.Value, 8), Math.Round(r.Value, 8)); // aligns with Decimal

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
                _ => definiteCalendarUnitToUcum(orig.Unit)
                     ?? throw new InvalidOperationException($"Unit '{orig.Unit}' is not a known calendar duration.")
            };

            return new Quantity(orig.Value, ucumUnit, QuantityUnitSystem.UCUM);
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

    public override int GetHashCode() => (Unit, Value).GetHashCode();

    public override string ToString() => $"{Value.ToString(CultureInfo.InvariantCulture)} '{Unit}'";

    bool? ICqlEquatable.IsEqualTo(Any? other) =>
        other is not null && TryEquals(other, CQL_EQUALS_COMPARISON, out var result) ? result: null;

    // Note that, in contrast to equals, this will return false if operators cannot be compared (as described by the spec)
    bool ICqlEquatable.IsEquivalentTo(Any? other) =>
        other is not null && TryEquals(other, CQL_EQUIVALENCE_COMPARISON, out var result) && result.Value;

    int? ICqlOrderable.CompareTo(Any? other) =>
        other is not null && TryCompareTo(other, out var result)
            ? result
            : null;

    // Maps the calendar durations that have a definite length to their UCUM equivalent.
    private static string? definiteCalendarUnitToUcum(string calendarUnit) => calendarUnit switch
    {
        "week" => "wk",
        "day" => "d",
        "hour" => "h",
        "minute" => "min",
        "second" => "s",
        "millisecond" => "ms",
        _ => null
    };

    /// <summary>
    /// Multiplies two quantities, as defined by the FHIRPath <c>*</c> operator.
    /// </summary>
    /// <remarks>
    /// <para>When one of the operands has the unity unit '1', its value just scales the other operand, which
    /// keeps its unit (e.g. <c>5 'mg' * 2 '1'</c> gives <c>10 'mg'</c>). Otherwise, both operands are converted
    /// to canonical UCUM units before multiplying, so the result is expressed in canonical UCUM units
    /// (e.g. <c>2.0 'cm' * 2.0 'm'</c> gives <c>0.040000 'm2'</c>, and <c>2 'kg' * 3 'm'</c> gives <c>6000 'g.m'</c>).</para>
    /// <para>The multiplication is not possible, and <c>false</c> is returned, when a unit cannot be converted
    /// to canonical UCUM units. This includes non-UCUM units, the calendar durations 'year' and 'month',
    /// units with an offset (like 'Cel'), arbitrary units (like '[IU]'), and results that do not fit in a decimal.
    /// Annotations (like '{tbl}') carry no meaning in UCUM and are dropped from the result.</para>
    /// </remarks>
    public static bool TryMultiply(Quantity left, Quantity right, [NotNullWhen(true)] out Quantity? result) =>
        tryCalculate(left, right, multiply: true, out result);

    /// <summary>
    /// Divides two quantities, as defined by the FHIRPath <c>/</c> operator.
    /// </summary>
    /// <remarks>
    /// <para>When the divisor has the unity unit '1', the dividend keeps its unit (e.g. <c>6 'kg' / 3 '1'</c> gives
    /// <c>2 'kg'</c>). Otherwise, both operands are converted to canonical UCUM units before dividing, so the
    /// result is expressed in canonical UCUM units (e.g. <c>4.0 'g' / 2.0 'm'</c> gives <c>2 'g.m-1'</c>,
    /// and <c>1.0 'm' / 1.0 'm'</c> gives <c>1 '1'</c>).</para>
    /// <para>A unity dividend (e.g. <c>2 '1' / 4 'm'</c>) gives the inverse unit (<c>0.5 'm-1'</c>), so it needs a divisor
    /// that can be converted to canonical UCUM units. Division by zero is not possible, and neither are the cases listed for
    /// <see cref="TryMultiply(Quantity, Quantity, out Quantity?)"/>: in these cases <c>false</c> is returned.</para>
    /// </remarks>
    public static bool TryDivide(Quantity left, Quantity right, [NotNullWhen(true)] out Quantity? result) =>
        tryCalculate(left, right, multiply: false, out result);

    private static bool tryCalculate(Quantity left, Quantity right, bool multiply, [NotNullWhen(true)] out Quantity? result)
    {
        if (left is null) throw new ArgumentNullException(nameof(left));
        if (right is null) throw new ArgumentNullException(nameof(right));

        result = null;

        if (!multiply && right.Value == 0) return false;

        try
        {
            // Scaling by a unity quantity keeps the unit (and system) of the other operand.
            if (isUnity(right))
            {
                result = new Quantity(multiply ? left.Value * right.Value : left.Value / right.Value, left.Unit, left.System);
                return true;
            }

            if (multiply && isUnity(left))
            {
                result = new Quantity(left.Value * right.Value, right.Unit, right.System);
                return true;
            }

            if (toUcumUnit(left) is not { } leftUnit || toUcumUnit(right) is not { } rightUnit) return false;

            var l = (left.Value, leftUnit, UCUM);
            var r = (right.Value, rightUnit, UCUM);
            (decimal value, string unit, string? codesystem)? calculated;
            var success = multiply
                ? metricService.TryMultiply(l, r, out calculated)
                : metricService.TryDivide(l, r, out calculated);
            if (!success || calculated is null) return false;

            var (value, unit, _) = calculated.Value;
            result = new Quantity(value, cleanupCanonicalUnit(unit), QuantityUnitSystem.UCUM);
            return true;
        }
        catch (Exception e) when (e is ArithmeticException or ArgumentException or InvalidOperationException)
        {
            result = null;
            return false;
        }

        static bool isUnity(Quantity q) => q.System == QuantityUnitSystem.UCUM && q.Unit == UCUM_UNIT;

        // Returns the UCUM unit to use in the calculation. Fhir.Metrics does not interpret '1' as unity,
        // so this returns the empty string for it.
        static string? toUcumUnit(Quantity q) => q.System switch
        {
            QuantityUnitSystem.UCUM when q.Unit == UCUM_UNIT => "",
            QuantityUnitSystem.UCUM when !containsArbitraryUnit(q.Unit) => q.Unit,
            // Calendar years and months have no definite UCUM equivalent.
            QuantityUnitSystem.CalendarDuration => definiteCalendarUnitToUcum(q.Unit),
            _ => null
        };

        // Fhir.Metrics represents unity (and units consisting only of an annotation, like '{tbl}') as a
        // '1' factor in its canonical form (e.g. '1.g-1' or '12'). Remove those, and use '1' for a dimensionless result.
        static string cleanupCanonicalUnit(string? unit)
        {
            var parts = (unit ?? "").Split('.')
                .Where(p => p.Length > 0 && !isUnityFactor(p))
                .ToArray();
            return parts.Length == 0 ? UCUM_UNIT : string.Join(".", parts);

            static bool isUnityFactor(string part) =>
                part[0] == '1' && part.Skip(1).All(c => c == '-' || char.IsDigit(c));
        }
    }

    private static IMetricService metricService => FhirMetricService.Instance.Value;

    // UCUM arbitrary units (UCUM §4.5, "arbitrary units", marked as such in ucum-essence.xml) are not
    // commensurable with any other unit, so they cannot be converted to canonical units.
    private static readonly string[] ARBITRARY_UNITS =
    [
        "[IU]", "[iU]", "[arb'U]", "[USP'U]", "[GPL'U]", "[MPL'U]", "[APL'U]", "[beth'U]", "[anti'Xa'U]",
        "[todd'U]", "[dye'U]", "[smgy'U]", "[bdsk'U]", "[ka'U]", "[knk'U]", "[mclg'U]", "[tb'U]",
        "[CCID_50]", "[TCID_50]", "[EID_50]", "[PFU]", "[FFU]", "[CFU]", "[IR]", "[BAU]", "[AU]",
        "[Amb'a'1'U]", "[PNU]", "[Lf]", "[D'ag'U]", "[FEU]", "[ELU]", "[EU]"
    ];

    private static bool containsArbitraryUnit(string unit) =>
        unit.IndexOf('[') >= 0 && ARBITRARY_UNITS.Any(au => unit.Contains(au));

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