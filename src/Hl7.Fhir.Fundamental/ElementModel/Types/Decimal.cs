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
using System.Linq;
using System.Xml;
using static Hl7.Fhir.Utility.Result;

namespace Hl7.Fhir.ElementModel.Types
{
    public class Decimal : Any, IComparable, ICqlEquatable, ICqlOrderable, ICqlConvertible
    {
        public Decimal() : this(default) { }

        public Decimal(decimal value) => Value = value;

        public decimal Value { get; }

        // private static readonly string[] FORBIDDEN_DECIMAL_PREFIXES = new[] { "+", ".", "00" };
        // [20190819] EK Consolidated this syntax with CQL and FhirPath, which will allow leading zeroes 
        private static readonly string[] FORBIDDEN_DECIMAL_PREFIXES = new[] { "+", "." };

        public static Decimal Parse(string value) =>
            TryParse(value, out var result) ? result! : throw new FormatException($"String '{value}' was not recognized as a valid decimal.");

        public static bool TryParse(string representation, out Decimal? value)
        {
            if (representation == null) throw new ArgumentNullException(nameof(representation));

            value = default;

            if (FORBIDDEN_DECIMAL_PREFIXES.Any(prefix => representation.StartsWith(prefix)) || representation.EndsWith("."))
                return false;

            (var succ, var val) = Any.DoConvert(() => decimal.Parse(representation, NumberStyles.AllowDecimalPoint |
                   NumberStyles.AllowExponent |
                   NumberStyles.AllowLeadingSign,
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

            return pointPos == -1 ? 0 : sr.Length - pointPos - 1;   // -1 for the decimal separator
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
        public static implicit operator Quantity(Decimal d) => new Quantity(d.Value, Quantity.UCUM_UNIT);
        public static explicit operator Decimal(decimal d) => new Decimal(d);
        public static explicit operator Boolean(Decimal d) => ((ICqlConvertible)d).TryConvertToBoolean().ValueOrThrow();
        public static explicit operator String(Decimal d) => ((ICqlConvertible)d).TryConvertToString().ValueOrThrow();

        bool? ICqlEquatable.IsEqualTo(Any other) => other is { } ? Equals(other, CQL_EQUALS_COMPARISON) : (bool?)null;
        bool ICqlEquatable.IsEquivalentTo(Any other) => Equals(other, CQL_EQUIVALENCE_COMPARISON);
        int? ICqlOrderable.CompareTo(Any other) => other is { } ? CompareTo(other) : (int?)null;

        Result<Boolean> ICqlConvertible.TryConvertToBoolean() =>
          Value switch
          {
              1 => Ok((Boolean)true),
              0 => Ok((Boolean)false),
              _ => CannotCastTo<Boolean>(this)
          };

        Result<Decimal> ICqlConvertible.TryConvertToDecimal() => Ok(this);

        Result<Quantity> ICqlConvertible.TryConvertToQuantity() => Ok(new Quantity(Value));

        Result<String> ICqlConvertible.TryConvertToString() => Ok(new String(ToString()));

        Result<Integer> ICqlConvertible.TryConvertToInteger() => CannotCastTo<Integer>(this);
        Result<Long> ICqlConvertible.TryConvertToLong() => CannotCastTo<Long>(this);
        Result<Ratio> ICqlConvertible.TryConvertToRatio() => CannotCastTo<Ratio>(this);
        Result<Time> ICqlConvertible.TryConvertToTime() => CannotCastTo<Time>(this);
        Result<Code> ICqlConvertible.TryConvertToCode() => CannotCastTo<Code>(this);
        Result<Concept> ICqlConvertible.TryConvertToConcept() => CannotCastTo<Concept>(this);
        Result<Date> ICqlConvertible.TryConvertToDate() => CannotCastTo<Date>(this);
        Result<DateTime> ICqlConvertible.TryConvertToDateTime() => CannotCastTo<DateTime>(this);


    }


    /// <summary>Specifies the comparison rules for decimals.</summary>
    /// <remarks>Options are aligned with the equality and equivalence  operations for decimals
    /// defined in the CQL specification. See https://cql.hl7.org/09-b-cqlreference.html#comparison-operators-4 
    /// for more details.
    /// </remarks>
    public enum DecimalComparison
    {
        Strict,

        /// <summary>
        /// Trailing zeroes after the decimal are ignored in determining precision.
        /// </summary>
        IgnoreTrailingZeroes,

        /// <summary>
        /// Comparison is done on values rounded to the scale of the
        /// least precise operand. Implies <see cref="DecimalComparison.IgnoreTrailingZeroes" />.
        /// </summary>
        RoundToSmallestScale
    }
}
