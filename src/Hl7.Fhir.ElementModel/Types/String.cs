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
using static Hl7.Fhir.Utility.Result;

namespace Hl7.Fhir.ElementModel.Types
{
    public class String : Any, IComparable, ICqlEquatable, ICqlOrderable, ICqlConvertible
    {
        public String() : this(string.Empty) { }

        public String(string value) => Value = value;

        public string Value { get; }

        public static String Parse(string value) =>
            TryParse(value, out var result) ? result : throw new FormatException($"String '{value}' was not recognized as a valid string.");

        // Actually, it's not that trivial, since CQL strings accept a subset of C#'s escape sequences,
        // we *could* validate those here.
        public static bool TryParse(string representation, out String value)
        {
            if (representation == null) throw new ArgumentNullException(nameof(representation));

            value = new String(representation);   // a bit obvious
            return true;
        }

        public override bool Equals(object? obj) => obj is Any other && Equals(other, CQL_EQUALS_COMPARISON);
        public static bool operator ==(String a, String b) => Equals(a, b);
        public static bool operator !=(String a, String b) => !Equals(a, b);

        /// <summary>
        /// Compares two strings according to CQL equivalence rules.
        /// </summary>
        public bool Equals(Any other, StringComparison comparisonType)
        {
            if (!(other is String otherS)) return false;

            if (comparisonType == StringComparison.Unicode)
                return string.CompareOrdinal(Value, otherS.Value) == 0;

            var l = comparisonType.HasFlag(StringComparison.NormalizeWhitespace) ? normalizeWS(Value) : Value;
            var r = comparisonType.HasFlag(StringComparison.NormalizeWhitespace) ? normalizeWS(otherS.Value) : otherS.Value;

            var compareOptions = CompareOptions.None;
            if (comparisonType.HasFlag(StringComparison.IgnoreCase)) compareOptions |= CompareOptions.IgnoreCase;
            if (comparisonType.HasFlag(StringComparison.IgnoreDiacritics)) compareOptions |= CompareOptions.IgnoreNonSpace;

            return string.Compare(l, r, CultureInfo.InvariantCulture, compareOptions) == 0;
        }

        public const StringComparison CQL_EQUALS_COMPARISON = StringComparison.Unicode;
        public const StringComparison CQL_EQUIVALENCE_COMPARISON = StringComparison.IgnoreCase | StringComparison.IgnoreDiacritics | StringComparison.NormalizeWhitespace;

        private static string normalizeWS(string data)
        {
            var dataAsChars = data.ToCharArray();
            for (var ix = 0; ix < dataAsChars.Length; ix++)
            {
                if (char.IsWhiteSpace(dataAsChars[ix]))
                    dataAsChars[ix] = ' ';
            }

            return new string(dataAsChars);
        }


        public int CompareTo(object? obj)
        {
            return obj switch
            {
                null => 1,
                String s => string.CompareOrdinal(Value, s.Value),
                _ => throw NotSameTypeComparison(this, obj)
            };
        }

        public static bool operator <(String a, String b) => a.CompareTo(b) < 0;
        public static bool operator <=(String a, String b) => a.CompareTo(b) <= 0;
        public static bool operator >(String a, String b) => a.CompareTo(b) > 0;
        public static bool operator >=(String a, String b) => a.CompareTo(b) >= 0;

        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value;

        public static implicit operator string(String s) => s.Value;
        public static explicit operator String(string s) => new String(s);
        public static explicit operator Boolean(String s) => ((ICqlConvertible)s).TryConvertToBoolean().ValueOrThrow();
        public static explicit operator DateTime(String s) => ((ICqlConvertible)s).TryConvertToDateTime().ValueOrThrow();
        public static explicit operator Date(String s) => ((ICqlConvertible)s).TryConvertToDate().ValueOrThrow();
        public static explicit operator Time(String s) => ((ICqlConvertible)s).TryConvertToTime().ValueOrThrow();
        public static explicit operator Decimal(String s) => ((ICqlConvertible)s).TryConvertToDecimal().ValueOrThrow();
        public static explicit operator Integer(String s) => ((ICqlConvertible)s).TryConvertToInteger().ValueOrThrow();
        public static explicit operator Long(String s) => ((ICqlConvertible)s).TryConvertToLong().ValueOrThrow();
        public static explicit operator Quantity(String s) => ((ICqlConvertible)s).TryConvertToQuantity().ValueOrThrow();

        bool? ICqlEquatable.IsEqualTo(Any other) => other is { } ? Equals(other, CQL_EQUALS_COMPARISON) : (bool?)null;
        bool ICqlEquatable.IsEquivalentTo(Any other) => Equals(other, CQL_EQUIVALENCE_COMPARISON);
        int? ICqlOrderable.CompareTo(Any other) => other is { } ? CompareTo(other) : (int?)null;

        Result<Boolean> ICqlConvertible.TryConvertToBoolean()
        {
            switch (Value.ToLower())
            {
                case "true":
                case "t":
                case "yes":
                case "y":
                case "1":
                case "1.0":
                    return Ok(new Boolean(true));
                case "false":
                case "f":
                case "no":
                case "n":
                case "0":
                case "0.0":
                    return Ok(new Boolean(false));
                default:
                    return CannotCastTo<Boolean>(this);
            }
        }

        Result<Decimal> ICqlConvertible.TryConvertToDecimal() =>
            Decimal.TryParse(Value, out var result) ? Ok(result!) : CannotCastTo<Decimal>(this);

        Result<Integer> ICqlConvertible.TryConvertToInteger() =>
            Integer.TryParse(Value, out var result) ? Ok(result!) : CannotCastTo<Integer>(this);

        Result<Long> ICqlConvertible.TryConvertToLong() =>
            Long.TryParse(Value, out var result) ? Ok(result!) : CannotCastTo<Long>(this);

        Result<DateTime> ICqlConvertible.TryConvertToDateTime() =>
            DateTime.TryParse(Value, out var result) ? Ok(result!) : CannotCastTo<DateTime>(this);

        Result<Date> ICqlConvertible.TryConvertToDate() =>
            Date.TryParse(Value, out var result) ? Ok(result!) : CannotCastTo<Date>(this);

        Result<Time> ICqlConvertible.TryConvertToTime() =>
            Time.TryParse(Value, out var result) ? Ok(result!) : CannotCastTo<Time>(this);

        Result<Quantity> ICqlConvertible.TryConvertToQuantity() =>
            Quantity.TryParse(Value, out var result) ? Ok(result!) : CannotCastTo<Quantity>(this);

        Result<String> ICqlConvertible.TryConvertToString() => Ok(this);

        Result<Ratio> ICqlConvertible.TryConvertToRatio() =>
            Ratio.TryParse(Value, out var result) ? Ok(result!) : CannotCastTo<Ratio>(this);

        Result<Code> ICqlConvertible.TryConvertToCode() => CannotCastTo<Code>(this);
        Result<Concept> ICqlConvertible.TryConvertToConcept() => CannotCastTo<Concept>(this);
    }

    /// <summary>Specifies the comparison rules for string.</summary>
    /// <remarks>Options are aligned with the equality and equivalence  operations for string
    /// defined in the CQL specification. See https://cql.hl7.org/09-b-cqlreference.html#comparison-operators-4 
    /// for more details.
    /// </remarks>
    [Flags]
    public enum StringComparison
    {
        /// <summary>
        /// Both strings are the same based on the Unicode values for the individual 
        /// characters in the strings.
        /// </summary>
        Unicode = 0,

        /// <summary>
        /// Ignore casing when comparing strings
        /// </summary>
        IgnoreCase = 1,

        /// <summary>
        /// All whitespace characters are treated as equivalent.
        /// </summary>
        NormalizeWhitespace = 2,

        /// <summary>
        /// Ignore all Unicode non-spacing characters when comparing string
        /// </summary>
        IgnoreDiacritics = 4
    }
}


