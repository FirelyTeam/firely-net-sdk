/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace Hl7.Fhir.ElementModel.Types;

public class Decimal(decimal value = 0) : Any, IComparable, ICqlEquatable, ICqlOrderable
{
    public decimal Value { get; } = value;

    // private static readonly string[] FORBIDDEN_DECIMAL_PREFIXES = new[] { "+", ".", "00" };
    // [20190819] EK Consolidated this syntax with CQL and FhirPath, which will allow leading zeroes
    private static readonly string[] FORBIDDEN_DECIMAL_PREFIXES = ["+", "."];

    public static Decimal Parse(string value) =>
        TryParse(value, out var result) ? result : throw new FormatException($"String '{value}' was not recognized as a valid decimal.");

    public static bool TryParse(string representation, [NotNullWhen(true)] out Decimal? value)
    {
        if (representation == null) throw new ArgumentNullException(nameof(representation));

        value = null;

        if (FORBIDDEN_DECIMAL_PREFIXES.Any(representation.StartsWith) || representation.EndsWith("."))
            return false;

        var (succ, val) = Any.DoConvert(() =>
            decimal.Parse(representation,
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture));

        value = new Decimal(val);
        return succ;
    }


    /// <summary>
    /// Determines if two decimals are equal according to CQL equality rules.
    /// </summary>
    /// <remarks>The same as <see cref="Equals(Any, DecimalComparison)" />
    /// with comparison type <see cref="CQL_EQUALS_COMPARISON"/>. For decimals, CQL and .NET equality
    /// rules are aligned.
    /// </remarks>
    public override bool Equals(object? obj) => obj is Decimal d && Equals(d, CQL_EQUALS_COMPARISON);
    public static bool operator ==(Decimal a, Decimal b) => Equals(a, b);
    public static bool operator !=(Decimal a, Decimal b) => !Equals(a, b);


    /// <summary>
    /// Determines equality of two decimals using the specified type of decimal comparsion.
    /// </summary>
    public bool Equals(Any other, DecimalComparison comparisonType)
    {
        if (!(other is Decimal otherD)) return false;

        return comparisonType switch
        {
            DecimalComparison.Strict =>
                (Scale(this.Value, ignoreTrailingZeroes: false) == Scale(otherD.Value, ignoreTrailingZeroes: false)) &&
                eq(Value, otherD.Value),
            DecimalComparison.IgnoreTrailingZeroes =>
                eq(Value, otherD.Value),      // default .NET decimal behaviour
            DecimalComparison.RoundToSmallestScale => scaleEq(Value, otherD.Value),
            _ => throw new NotImplementedException(),  // cannot happen, just to keep the compiler happy
        };

        // The CQL and FhirPath spec talk about 'precision' (number of digits), but might mean 'scale'
        // (number of decimals). Since the first has no native support on .NET, I'll be sloppy and
        // assume scale is meant.
        static bool scaleEq(decimal a, decimal b)
        {
            var roundPrec = Math.Min(Scale(a, true), Scale(b, true));
            var lr = Math.Round(a, roundPrec);
            var rr = Math.Round(b, roundPrec);
            return eq(lr, rr);
        }

        // From the spec: The Decimal type represents real values in the range (-10^28+1)/108 to (10^28-1)/10^8 with a step size of 10^-8.
        // This range is defined based on a survey of decimal-value implementations and is based on the most useful lowest common denominator.
        // This means we should round comparison to the 8th position after the decimal, everything beyond this is beyond the "step size".
        static bool eq(decimal a, decimal b) =>
            Math.Round(a, 8) == Math.Round(b, 8);
    }

    public const DecimalComparison CQL_EQUALS_COMPARISON = DecimalComparison.IgnoreTrailingZeroes;
    public const DecimalComparison CQL_EQUIVALENCE_COMPARISON = DecimalComparison.RoundToSmallestScale;


    /// <summary>
    /// Calculates the scale of a decimal, which is the number of digits after the decimal separator.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="ignoreTrailingZeroes">If true, trailing zeroes are ignored when counting
    /// the number of digits after the separator.</param>
    /// <returns></returns>
    public static int Scale(decimal d, bool ignoreTrailingZeroes)
    {
        var sr = d.ToString(CultureInfo.InvariantCulture);
        var pointPos = sr.IndexOf('.');
        if (pointPos == -1) return 0;

        if (ignoreTrailingZeroes) sr = sr.TrimEnd('0');

        return sr.Length - pointPos - 1;   // -1 for the decimal separator
    }

    /// <summary>
    /// Compares two decimals according to CQL equality rules
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    /// <remarks>For decimals, CQL and .NET comparison rules are aligned.</remarks>
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            // as defined by the .NET framework guidelines
            null => 1,

            // The comparison rules for decimals are underdocumented - assume normal dotnet
            // comparison, which disregards trailing zeroes (= equality comparison according
            // to CQL).
            Decimal d => decimal.Compare(Math.Round(Value, 8), Math.Round(d.Value, 8)),

            _ => throw NotSameTypeComparison(this, obj)
        };
    }

    public static bool operator <(Decimal a, Decimal b) => a.CompareTo(b) < 0;
    public static bool operator <=(Decimal a, Decimal b) => a.CompareTo(b) <= 0;
    public static bool operator >(Decimal a, Decimal b) => a.CompareTo(b) > 0;
    public static bool operator >=(Decimal a, Decimal b) => a.CompareTo(b) >= 0;


    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => XmlConvert.ToString(Value);

    public static implicit operator decimal(Decimal d) => d.Value;
    public static explicit operator Decimal(decimal d) => new(d);
    public static implicit operator Quantity(Decimal d) => RunCast<Quantity>(d);
    public static explicit operator Boolean(Decimal d) => RunCast<Boolean>(d);
    public static implicit operator String(Decimal d) => RunCast<String>(d);

    public override bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result)
    {
        result = null;

        if (to == typeof(Decimal))
            result = this;
        else if (to == typeof(Quantity))
            result = new Quantity(Value, Quantity.UCUM_UNIT);
        else if (to == typeof(Boolean))
            result = Value switch
            {
                1 => Boolean.True,
                0 => Boolean.False,
                _ => null
            };
        else if (to == typeof(String))
            result = new String(ToString());

        return result is not null;
    }

    bool? ICqlEquatable.IsEqualTo(Any? other) => other is not null ? Equals(other, CQL_EQUALS_COMPARISON) : null;
    bool ICqlEquatable.IsEquivalentTo(Any? other) => other is not null && Equals(other, CQL_EQUIVALENCE_COMPARISON);
    int? ICqlOrderable.CompareTo(Any? other) => other is not null ? CompareTo(other) : null;
}