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
using System.Text.RegularExpressions;
using static Hl7.Fhir.Utility.Result;

namespace Hl7.Fhir.ElementModel.Types
{
    public class Time : Any, IComparable, ICqlEquatable, ICqlOrderable, ICqlConvertible
    {
        private Time(string original, DateTimeOffset parsedValue, DateTimePrecision precision, bool hasOffset)
        {
            _original = original;
            _parsedValue = parsedValue;
            Precision = precision;
            HasOffset = hasOffset;
        }

        public static Time Parse(string representation) =>
            TryParse(representation, out var result) ? result : throw new FormatException($"String '{representation}' was not recognized as a valid time.");

        public static bool TryParse(string representation, out Time value) => tryParse(representation, out value);

        public static Time FromDateTimeOffset(DateTimeOffset dto, DateTimePrecision prec = DateTimePrecision.Fraction,
        bool includeOffset = false)
        {
            string formatString = prec switch
            {
                DateTimePrecision.Hour => "HH",
                DateTimePrecision.Minute => "HH:mm",
                DateTimePrecision.Second => "HH:mm:ss",
                _ => "HH:mm:ss.FFFFFFF",
            };

            if (includeOffset) formatString += "K";

            var representation = dto.ToString(formatString);
            return Parse(representation);
        }

        public static Time Now(bool includeOffset = false) => FromDateTimeOffset(DateTimeOffset.Now, includeOffset: includeOffset);

        public int? Hours => Precision >= DateTimePrecision.Hour ? _parsedValue.Hour : (int?)null;
        public int? Minutes => Precision >= DateTimePrecision.Minute ? _parsedValue.Minute : (int?)null;
        public int? Seconds => Precision >= DateTimePrecision.Second ? _parsedValue.Second : (int?)null;
        public int? Millis => Precision >= DateTimePrecision.Fraction ? _parsedValue.Millisecond : (int?)null;

        /// <summary>
        /// The span of time ahead/behind UTC
        /// </summary>
        public TimeSpan? Offset => HasOffset ? _parsedValue.Offset : null;

        private readonly string _original;
        private readonly DateTimeOffset _parsedValue;

        /// <summary>
        /// The precision of the time available. 
        /// </summary>
        public DateTimePrecision Precision { get; private set; }

        /// <summary>
        /// Whether the time specifies an offset to UTC
        /// </summary>
        public bool HasOffset { get; private set; }

        // Our regex is pretty flexible, it does not bother to capture rules about semantics (12:64 would be legal here).
        // Additional semantic checks will be verified using the built-in DateTimeOffset .NET parser.
        // Also, it accept the superset of formats specified by FHIR, CQL, FhirPath and the mapping language. Each of these
        // specific implementations may add additional constraints (e.g. about minimum precision or presence of timezones).

        internal static readonly string PARTIALTIMEFORMAT = $"{TIMEFORMAT}{OFFSETFORMAT}?";
        internal const string TIMEFORMAT =
            "(?<time>(?<hours>[0-9][0-9]) ((?<minutes>:[0-9][0-9]) ((?<seconds>:[0-9][0-9]) ((?<frac>.[0-9]+))?)?)?)";
        internal const string OFFSETFORMAT = "(?<offset>Z | (\\+|-) [0-9][0-9]:[0-9][0-9])";

        private static readonly Regex PARTIALTIMEREGEX =
            new Regex("^" + PARTIALTIMEFORMAT + "$", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Converts the time to a full DateTimeOffset instance.
        /// </summary>
        /// <param name="year">Year used to turn a time into a date</param>
        /// <param name="month">Month used to turn a time into a date</param>
        /// <param name="day">Day used to turn a time into a date</param>
        /// <param name="defaultOffset">Offset used when the time does not specify one.</param>
        /// <returns></returns>
        public DateTimeOffset ToDateTimeOffset(int year, int month, int day, TimeSpan defaultOffset) =>
            new DateTimeOffset(year, month, day, _parsedValue.Hour,
                    _parsedValue.Minute, _parsedValue.Second, _parsedValue.Millisecond,
                    HasOffset ? _parsedValue.Offset : defaultOffset);


        private static bool tryParse(string representation, out Time value)
        {
            if (representation is null) throw new ArgumentNullException(nameof(representation));

            var matches = PARTIALTIMEREGEX.Match(representation);
            if (!matches.Success)
            {
                value = new Time(representation, default, default, default);
                return false;
            }

            var hrg = matches.Groups["hours"];
            var ming = matches.Groups["minutes"];
            var secg = matches.Groups["seconds"];
            var fracg = matches.Groups["frac"];
            var offset = matches.Groups["offset"];

            var prec =
                        fracg.Success ? DateTimePrecision.Fraction :
                        secg.Success ? DateTimePrecision.Second :
                        ming.Success ? DateTimePrecision.Minute :
                        DateTimePrecision.Hour;

            var parseableDT = $"2016-01-01T" +
                    (hrg.Success ? hrg.Value : "00") +
                    (ming.Success ? ming.Value : ":00") +
                    (secg.Success ? secg.Value : ":00") +
                    (fracg.Success ? fracg.Value : "") +
                    (offset.Success ? offset.Value : "Z");

            var success = DateTimeOffset.TryParse(parseableDT, out var parsedValue);
            value = new Time(representation, parsedValue, prec, offset.Success);

            return success;
        }

        /// <summary>
        /// Compare two times based on CQL equality rules
        /// </summary>
        /// <returns>returns true if the values have the same precision, and each date component is exactly the same. Datetimes with timezones are normalized
        /// to zulu before comparison is done. Throws an <see cref="ArgumentException"/> if the arguments differ in precision.</returns>
        /// <remarks>See <see cref="TryCompareTo(Any)"/> for more details.</remarks>
        public override bool Equals(object? obj) => obj is Any other && TryEquals(other).ValueOrDefault(false);

        public Result<bool> TryEquals(Any other) => other is Time ? TryCompareTo(other).Select(i => i == 0) : false;

        public static bool operator ==(Time a, Time b) => Equals(a, b);
        public static bool operator !=(Time a, Time b) => !Equals(a, b);

        /// <summary>
        /// Compare two times based on CQL equality rules
        /// </summary>
        /// <remarks>See <see cref="TryCompareTo(Any)"/> for more details.</remarks>
        public int CompareTo(object? obj) => obj is Time p ?
            TryCompareTo(p).ValueOrThrow() : throw NotSameTypeComparison(this, obj);

        /// <summary>
        /// Compares two times according to CQL ordering rules.
        /// </summary> 
        /// <param name="other"></param>
        /// <returns>An <see cref="Utility.Ok{T}"/> with an integer value representing the reseult of the comparison: 0 if this and other are equal, 
        /// -1 if this is smaller than other and +1 if this is bigger than other, or the other is null. If the values are incomparable
        /// this function returns a <see cref="Utility.Fail{T}"/> with the reason why the comparison between the two values was impossible.
        /// </returns>
        /// <remarks>The comparison is performed by considering each precision in order, beginning with hours. 
        /// If the values are the same, comparison proceeds to the next precision; 
        /// if the values are different, the comparison stops and the result is false. If one input has a value 
        /// for the precision and the other does not, the comparison stops and the values cannot be compared; if neither
        /// input has a value for the precision, or the last precision has been reached, the comparison stops
        /// and the result is true.</remarks>
        public Result<int> TryCompareTo(Any other)
        {
            return other switch
            {
                null => 1,
                Time p => DateTime.CompareDateTimeParts(_parsedValue, Precision, HasOffset, p._parsedValue, p.Precision, p.HasOffset),
                _ => throw NotSameTypeComparison(this, other)
            };
        }

        public static bool operator <(Time a, Time b) => a.CompareTo(b) < 0;
        public static bool operator <=(Time a, Time b) => a.CompareTo(b) <= 0;
        public static bool operator >(Time a, Time b) => a.CompareTo(b) > 0;
        public static bool operator >=(Time a, Time b) => a.CompareTo(b) >= 0;


        public override int GetHashCode() => _original.GetHashCode();
        public override string ToString() => _original;

        public static explicit operator Time(DateTimeOffset dto) => FromDateTimeOffset(dto);
        public static explicit operator String(Time dt) => ((ICqlConvertible)dt).TryConvertToString().ValueOrThrow();

        bool? ICqlEquatable.IsEqualTo(Any other) => other is { } && TryEquals(other) is Ok<bool> ok ? ok.Value : (bool?)null;

        // Note that, in contrast to equals, this will return false if operators cannot be compared (as described by the spec)
        bool ICqlEquatable.IsEquivalentTo(Any other) => other is { } pd && TryEquals(pd).ValueOrDefault(false);

        int? ICqlOrderable.CompareTo(Any other) => other is { } && TryCompareTo(other) is Ok<int> ok ? ok.Value : (int?)null;

        Result<String> ICqlConvertible.TryConvertToString() => Ok(new String(ToString()));

        Result<Time> ICqlConvertible.TryConvertToTime() => Ok(this);

        Result<Code> ICqlConvertible.TryConvertToCode() => CannotCastTo<Code>(this);
        Result<Boolean> ICqlConvertible.TryConvertToBoolean() => CannotCastTo<Boolean>(this);
        Result<Date> ICqlConvertible.TryConvertToDate() => CannotCastTo<Date>(this);
        Result<DateTime> ICqlConvertible.TryConvertToDateTime() => CannotCastTo<DateTime>(this);
        Result<Decimal> ICqlConvertible.TryConvertToDecimal() => CannotCastTo<Decimal>(this);
        Result<Integer> ICqlConvertible.TryConvertToInteger() => CannotCastTo<Integer>(this);
        Result<Long> ICqlConvertible.TryConvertToLong() => CannotCastTo<Long>(this);
        Result<Quantity> ICqlConvertible.TryConvertToQuantity() => CannotCastTo<Quantity>(this);
        Result<Ratio> ICqlConvertible.TryConvertToRatio() => CannotCastTo<Ratio>(this);
        Result<Concept> ICqlConvertible.TryConvertToConcept() => CannotCastTo<Concept>(this);
    }
}



