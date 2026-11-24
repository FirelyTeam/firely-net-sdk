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
using System.Text.RegularExpressions;
using System.Xml;

namespace Hl7.Fhir.ElementModel.Types;

public class Decimal : Any, IComparable, ICqlEquatable, ICqlOrderable
{
    private readonly decimal _value;

    // Set when the value is outside the range of System.Decimal (e.g. 1e999999999 or 1e-999999999).
    private readonly OutOfRangeValue? _outOfRange;

    public Decimal(decimal value = 0)
    {
        _value = value;
    }

    private Decimal(OutOfRangeValue outOfRange)
    {
        _outOfRange = outOfRange;
    }

    internal static readonly char[] EXPONENT_MARKERS = ['e', 'E'];

    private Decimal(decimal value, int negativeScale)
    {
        _value = value;
        _negativeScale = negativeScale;
    }

    // Set when the decimal was parsed from a literal whose exponent makes it precise to fewer decimal places than
    // its value shows, which happens when that number of decimal places is below zero: 9.5e2 is precise to tens
    // (two significant digits, so -1 decimal places), but its value 950 shows units.
    private readonly int? _negativeScale;

    /// <summary>
    /// The number of decimal places to which this decimal is precise: 2 for 1.50, 0 for 950 and -1 for 9.5e2 (which
    /// is precise to tens). This is the scale of <see cref="Value"/>, unless the decimal was parsed from a literal with
    /// an exponent that leaves it precise to fewer than zero decimal places, which a .NET decimal cannot express.
    /// </summary>
    /// <remarks>Not meaningful for a value outside the range of a .NET decimal (see <see cref="IsOutOfRange"/>).</remarks>
    internal int PrecisionScale => _negativeScale ?? (decimal.GetBits(_value)[3] >> 16) & 0xFF;

    /// <summary>
    /// The value of this decimal as a .NET <see cref="decimal"/>.
    /// </summary>
    /// <exception cref="OverflowException">The value is too large for a <see cref="decimal"/>
    /// (see <see cref="IsOutOfRange"/>).</exception>
    /// <remarks>A value too small for a <see cref="decimal"/> (e.g. 1e-999999999) is returned as 0.</remarks>
    public decimal Value => _outOfRange switch
    {
        null => _value,
        { IsHuge: true } => throw new OverflowException($"The decimal {this} is outside the range of a .NET decimal."),
        _ => 0m
    };

    /// <summary>
    /// True when the value is outside the range of a .NET <see cref="decimal"/>, either because
    /// its magnitude is too large (e.g. 1e999999999) or too small (e.g. 1e-999999999). Such values can be
    /// compared and converted to a string, but not used in arithmetic.
    /// </summary>
    public bool IsOutOfRange => _outOfRange is not null;

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

        value = succ && NegativeScaleOf(representation) is { } negativeScale ? new Decimal(val, negativeScale) : new Decimal(val);
        return succ;
    }

    /// <summary>
    /// The number of decimal places to which a literal with an exponent is precise, when that is below zero: -1 for
    /// 9.5e2 (one decimal place in the mantissa, moved two places to the right), and null for 9.5e1 (0) or 9.5 (1).
    /// </summary>
    /// <remarks>The literal must already be known to parse. Zero has no precision to keep: as in the reference
    /// implementation, its boundaries ignore the exponent (0e2 is like 0).</remarks>
    internal static int? NegativeScaleOf(string literal)
    {
        var e = literal.IndexOfAny(EXPONENT_MARKERS);
        if (e == -1) return null;

        // The digits of the mantissa are all zeroes: zero, whatever its exponent (which may not even fit an int).
        if (literal.Take(e).All(c => c is '0' or '.' or '-')) return null;

        // Any other exponent that does not fit an int gives a value outside the range of a .NET decimal, so never gets here.
        if (!int.TryParse(literal.Substring(e + 1), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var exponent))
            return null;

        var point = literal.IndexOf('.', 0, e);
        var fractionDigits = point == -1 ? 0 : e - point - 1;
        var scale = (long)fractionDigits - exponent;
        return scale < 0 ? (int)scale : null;
    }

    private static readonly Regex DECIMAL_LITERAL =
        new(@"^(?<sign>-)?(?<int>[0-9]+)(\.(?<frac>[0-9]+))?([eE](?<exp>[+-]?[0-9]+))?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

    // System.Decimal holds values from 10^-28 up to about 7.9 x 10^28: a literal whose (normalised) exponent is within these
    // bounds either parses or is malformed. Beyond them, decimal.Parse decides whether the value fits, up to a limit.
    private const long MAX_EXPONENT = 27;
    private const long MIN_EXPONENT = -27;
    private const long PARSE_EXPONENT_LIMIT = 40;

    /// <summary>
    /// Parses a decimal like <see cref="TryParse(string, out Decimal?)"/>, but also accepts values outside the range
    /// of a .NET <see cref="decimal"/>. These are returned as a <see cref="Decimal"/> for which
    /// <see cref="IsOutOfRange"/> is true, instead of failing (too large) or being rounded to 0 (too small).
    /// </summary>
    /// <remarks>Never expands the exponent, so 1e999999999 is parsed in constant time and space.</remarks>
    internal static bool TryParseExtended(string representation, [NotNullWhen(true)] out Decimal? value)
    {
        value = null;
        if (representation is null) return false;

        var match = DECIMAL_LITERAL.Match(representation);
        if (!match.Success) return false;

        var intPart = match.Groups["int"].Value;
        var fracPart = match.Groups["frac"].Value;
        var expGroup = match.Groups["exp"];

        long exponent = 0;
        if (expGroup.Success && !long.TryParse(expGroup.Value, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out exponent))
            return false;

        // Normalise to d.ddd x 10^exponent, with no leading or trailing zeroes in the digits.
        var allDigits = intPart + fracPart;
        var firstNonZero = 0;
        while (firstNonZero < allDigits.Length && allDigits[firstNonZero] == '0') firstNonZero++;

        if (firstNonZero == allDigits.Length)
        {
            // The value is zero, whatever its exponent. Keep its scale (e.g. 0.00) when decimal.Parse can handle it.
            value = exponent is > -PARSE_EXPONENT_LIMIT and < PARSE_EXPONENT_LIMIT && TryParse(representation, out var zero)
                ? zero
                : new Decimal(0m);
            return true;
        }

        long normalizedExponent;
        try
        {
            normalizedExponent = checked(exponent + intPart.Length - 1 - firstNonZero);
        }
        catch (OverflowException)
        {
            return false;
        }

        // Only call decimal.Parse when the exponent is small: it does not cope well with huge exponents.
        if (normalizedExponent is > -PARSE_EXPONENT_LIMIT and < PARSE_EXPONENT_LIMIT)
        {
            // A literal that decimal.Parse turns into 0 underflowed (the digits are not all zero, see above).
            var parsedOk = TryParse(representation, out var parsed);
            if (parsedOk && parsed!.Value != 0m)
            {
                value = parsed;
                return true;
            }

            // Within the range of System.Decimal, a failure means a malformed literal rather than an overflow.
            if (normalizedExponent is >= MIN_EXPONENT and <= MAX_EXPONENT && !parsedOk)
                return false;
        }

        var digits = allDigits.Substring(firstNonZero).TrimEnd('0');
        value = new Decimal(new OutOfRangeValue(match.Groups["sign"].Success, digits, normalizedExponent));
        return true;
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

        if (IsOutOfRange || otherD.IsOutOfRange)
            return comparisonType switch
            {
                DecimalComparison.Strict => Equals(_outOfRange, otherD._outOfRange) && _value == otherD._value,
                // Values below the step size of 10^-8 compare as 0, see eq() below.
                _ => compareRounded(this, otherD) == 0,
            };

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
            Decimal d => compareRounded(this, d),

            _ => throw NotSameTypeComparison(this, obj)
        };
    }

    /// <summary>
    /// Compares two decimals after rounding them to 8 decimal places (the step size of FHIRPath decimals). A value too
    /// small for System.Decimal therefore compares as 0, and a value too large is beyond every System.Decimal.
    /// </summary>
    private static int compareRounded(Decimal a, Decimal b)
    {
        var aHuge = a._outOfRange is { IsHuge: true } ? a._outOfRange : null;
        var bHuge = b._outOfRange is { IsHuge: true } ? b._outOfRange : null;

        return (aHuge, bHuge) switch
        {
            (null, null) => decimal.Compare(Math.Round(a.Value, 8), Math.Round(b.Value, 8)),
            ({ } x, null) => x.Negative ? -1 : 1,
            (null, { } y) => y.Negative ? 1 : -1,
            ({ } x, { } y) => x.CompareTo(y)
        };
    }

    public static bool operator <(Decimal a, Decimal b) => a.CompareTo(b) < 0;
    public static bool operator <=(Decimal a, Decimal b) => a.CompareTo(b) <= 0;
    public static bool operator >(Decimal a, Decimal b) => a.CompareTo(b) > 0;
    public static bool operator >=(Decimal a, Decimal b) => a.CompareTo(b) >= 0;


    public override int GetHashCode() => _outOfRange is { IsHuge: true } huge ? huge.GetHashCode() : Value.GetHashCode();

    /// <remarks>A value outside the range of System.Decimal is written in scientific notation, e.g. 1E+999999999.</remarks>
    public override string ToString() => _outOfRange?.ToString() ?? XmlConvert.ToString(_value);

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
            result = new Quantity(this, Quantity.UCUM_UNIT, QuantityUnitSystem.UCUM);
        else if (to == typeof(Boolean))
            result = IsOutOfRange ? null : Value switch
            {
                1 => Boolean.True,
                0 => Boolean.False,
                _ => null
            };
        else if (to == typeof(String))
            result = new String(ToString());

        return result is not null;
    }

    /// <summary>
    /// A value outside the range of System.Decimal: sign x d.ddd x 10^exponent, where <see cref="Digits"/>
    /// holds the digits without leading or trailing zeroes.
    /// </summary>
    private sealed record OutOfRangeValue(bool Negative, string Digits, long Exponent) : IComparable<OutOfRangeValue>
    {
        // Too large values have a positive exponent, too small values a negative one.
        public bool IsHuge => Exponent >= 0;

        public int CompareTo(OutOfRangeValue? other)
        {
            if (other is null) return 1;
            if (Negative != other.Negative) return Negative ? -1 : 1;

            // Both have the same sign: compare the magnitudes, then flip for negative values.
            var magnitude = Exponent != other.Exponent
                ? Exponent.CompareTo(other.Exponent)
                : Math.Sign(string.CompareOrdinal(Digits, other.Digits));

            return Negative ? -magnitude : magnitude;
        }

        public override string ToString() =>
            (Negative ? "-" : "") + Digits.Substring(0, 1) + (Digits.Length > 1 ? "." + Digits.Substring(1) : "") +
            "E" + (Exponent >= 0 ? "+" : "-") + Exponent.ToString(CultureInfo.InvariantCulture).TrimStart('-');
    }

    bool? ICqlEquatable.IsEqualTo(Any? other) => other is not null ? Equals(other, CQL_EQUALS_COMPARISON) : null;
    bool ICqlEquatable.IsEquivalentTo(Any? other) => other is not null && Equals(other, CQL_EQUIVALENCE_COMPARISON);
    int? ICqlOrderable.CompareTo(Any? other) => other is not null ? CompareTo(other) : null;
}