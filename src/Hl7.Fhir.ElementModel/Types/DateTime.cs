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
    public class DateTime : Any, IComparable, ICqlEquatable, ICqlOrderable, ICqlConvertible
    {
        internal DateTime(string original, DateTimeOffset parsedValue, DateTimePrecision precision, bool hasOffset)
        {
            _original = original;
            _parsedValue = parsedValue;
            Precision = precision;
            HasOffset = hasOffset;
        }

        public static DateTime Parse(string representation) =>
            TryParse(representation, out var result) ? result : throw new FormatException($"String '{representation}' was not recognized as a valid datetime.");

        public static bool TryParse(string representation, out DateTime value) => tryParse(representation, out value);

        public static string FormatDateTimeOffset(DateTimeOffset dto) => dto.ToString(FMT_FULL);

        public static DateTime FromDateTimeOffset(DateTimeOffset dto)
        {
            var representation = FormatDateTimeOffset(dto);
            return Parse(representation);
        }

        [Obsolete("FromDateTime() has been renamed to FromDateTimeOffset()")]
        public static DateTime FromDateTime(DateTimeOffset dto) => FromDateTimeOffset(dto);

        public static DateTime Now() => FromDateTimeOffset(DateTimeOffset.Now);

        public static DateTime Today() => DateTime.Parse(DateTimeOffset.Now.ToString("yyyy-MM-ddK"));

        public Date TruncateToDate() => Date.FromDateTimeOffset(
            ToDateTimeOffset(_parsedValue.Offset), Precision, includeOffset: HasOffset);

        public int? Years => Precision >= DateTimePrecision.Year ? _parsedValue.Year : null;
        public int? Months => Precision >= DateTimePrecision.Month ? _parsedValue.Month : null;
        public int? Days => Precision >= DateTimePrecision.Day ? _parsedValue.Day : null;
        public int? Hours => Precision >= DateTimePrecision.Hour ? _parsedValue.Hour : null;
        public int? Minutes => Precision >= DateTimePrecision.Minute ? _parsedValue.Minute : null;
        public int? Seconds => Precision >= DateTimePrecision.Second ? _parsedValue.Second : null;
        public int? Millis => Precision >= DateTimePrecision.Fraction ? _parsedValue.Millisecond : null;

        public static DateTime operator +(DateTime dateTimeValue, Quantity addValue)
        {
            if (dateTimeValue is null) throw new ArgumentNullException(nameof(dateTimeValue));
            if (addValue is null) throw new ArgumentNullException(nameof(addValue));

            // Based on the discussion on equality/comparisons here:
            // https://chat.fhir.org/#narrow/stream/179266-fhirpath/topic/Date.2FTime.20comparison.20vs.20equality
            // We have also allowed addition to use the definitve UCUM units of 'wk', 'd', 'h', 'min'  as if they are a calendar unit of
            // 'week'/'day'/'hour'/'minute' respectively.
            var dto = addValue.Unit switch
            {
                // we can ignore precision, as the precision will "trim" it anyway, and if we add 13 months, then the year can tick over nicely
                "years" or "year" => dateTimeValue._parsedValue.AddYears((int)addValue.Value),
                "month" or "months" => dateTimeValue.Precision == DateTimePrecision.Year
                    ? dateTimeValue._parsedValue.AddYears((int)(addValue.Value / 12))
                    : dateTimeValue._parsedValue.AddMonths((int)addValue.Value),
                "week" or "weeks" or "wk" => dateTimeValue.Precision switch
                {
                    DateTimePrecision.Year => dateTimeValue._parsedValue.AddYears((int)(addValue.Value / 52)),
                    DateTimePrecision.Month => dateTimeValue._parsedValue.AddMonths((int)(addValue.Value * 7 / 30)),
                    _ => dateTimeValue._parsedValue.AddDays(((int)addValue.Value) * 7)
                },
                "day" or "days" or "d" => dateTimeValue.Precision switch
                {
                    DateTimePrecision.Year => dateTimeValue._parsedValue.AddYears((int)(addValue.Value / 365)),
                    DateTimePrecision.Month => dateTimeValue._parsedValue.AddMonths((int)(addValue.Value / 30)),
                    _ => dateTimeValue._parsedValue.AddDays((int)addValue.Value)
                },

                // NOT ignoring precision on time based stuff if there is no time component
                // if no time component, don't modify result
                "hour" or "hours" or "h" => dateTimeValue.Precision > DateTimePrecision.Day
                                        ? dateTimeValue._parsedValue.AddHours((double)addValue.Value)
                                        : dateTimeValue._parsedValue,
                "minute" or "minutes" or "min" => dateTimeValue.Precision > DateTimePrecision.Day
                    ? dateTimeValue._parsedValue.AddMinutes((double)addValue.Value)
                    : dateTimeValue._parsedValue,
                "s" or "second" or "seconds" => dateTimeValue.Precision > DateTimePrecision.Day
                                        ? dateTimeValue._parsedValue.AddSeconds((double)addValue.Value)
                                        : dateTimeValue._parsedValue,
                "ms" or "millisecond" or "milliseconds" => dateTimeValue.Precision > DateTimePrecision.Day
                                        ? dateTimeValue._parsedValue.AddMilliseconds((double)addValue.Value)
                                        : dateTimeValue._parsedValue,
                _ => throw new ArgumentException($"'{addValue.Unit}' is not a valid time-valued unit", nameof(addValue)),
            };

            string representation = dto.ToString(FMT_FULL);
            if (representation.Length > dateTimeValue._original.Length)
            {
                // need to trim appropriately.
                if (dateTimeValue.Precision <= DateTimePrecision.Minute)
                    representation = representation.Substring(0, dateTimeValue._original.Length);
                else
                {
                    if (!dateTimeValue.HasOffset)
                    {
                        // trim the offset from it
                        representation = dto.ToString("yyyy-MM-dd'T'HH:mm:ss.FFFFFFF");
                    }
                }
            }

            var result = new DateTime(representation, dto, dateTimeValue.Precision, dateTimeValue.HasOffset);
            return result;
        }

        /// <summary>
        /// The span of time ahead/behind UTC
        /// </summary>
        public TimeSpan? Offset => HasOffset ? _parsedValue.Offset : null;

        private readonly string _original;
        private readonly DateTimeOffset _parsedValue;

        /// <summary>
        /// The precision of the date and time available. 
        /// </summary>
        public DateTimePrecision Precision { get; private set; }

        /// <summary>
        /// Whether the time specifies an offset to UTC
        /// </summary>
        public bool HasOffset { get; private set; }

        private static readonly string DATETIMEFORMAT =
            $"(?<year>[0-9]{{4}}) ((?<month>-[0-9][0-9]) ((?<day>-[0-9][0-9]) (T{Time.TIMEFORMAT})?)?)? {Time.OFFSETFORMAT}?";
        private static readonly Regex DATETIMEREGEX =
                new("^" + DATETIMEFORMAT + "$",
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Converts the datetime to a full DateTimeOffset instance.
        /// </summary>
        /// <param name="defaultOffset">Offset used when the datetime does not specify one.</param>
        /// <returns></returns>
        public DateTimeOffset ToDateTimeOffset(TimeSpan defaultOffset) =>
             new(_parsedValue.Year, _parsedValue.Month, _parsedValue.Day,
                 _parsedValue.Hour, _parsedValue.Minute, _parsedValue.Second, _parsedValue.Millisecond,
                    HasOffset ? _parsedValue.Offset : defaultOffset);

        public const string FMT_FULL = "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFK";

        private static bool tryParse(string representation, out DateTime value)
        {
            if (representation is null) throw new ArgumentNullException(nameof(representation));

            var matches = DATETIMEREGEX.Match(representation);
            if (!matches.Success)
            {
                value = new DateTime(representation, default, default, default);
                return false;
            }

            var yrg = matches.Groups["year"];
            var mong = matches.Groups["month"];
            var dayg = matches.Groups["day"];
            var hrg = matches.Groups["hours"];
            var ming = matches.Groups["minutes"];
            var secg = matches.Groups["seconds"];
            var fracg = matches.Groups["frac"];
            var offset = matches.Groups["offset"];

            var prec =
                    fracg.Success ? DateTimePrecision.Fraction :
                    secg.Success ? DateTimePrecision.Second :
                    ming.Success ? DateTimePrecision.Minute :
                    hrg.Success ? DateTimePrecision.Hour :
                    dayg.Success ? DateTimePrecision.Day :
                    mong.Success ? DateTimePrecision.Month :
                    DateTimePrecision.Year;

            var parseableDT = yrg.Value +
                  (mong.Success ? mong.Value : "-01") +
                  (dayg.Success ? dayg.Value : "-01") +
                  (hrg.Success ? "T" + hrg.Value : "T00") +
                  (ming.Success ? ming.Value : ":00") +
                  (secg.Success ? secg.Value : ":00") +
                  (fracg.Success ? fracg.Value : "") +
                  (offset.Success ? offset.Value : "Z");

            var success = DateTimeOffset.TryParse(parseableDT, out var parsedValue);
            value = new DateTime(representation, parsedValue, prec, offset.Success);
            return success;
        }

        /// <summary>
        /// Compare two datetimes based on CQL equality rules
        /// </summary>
        /// <returns>returns true if the values have the same precision, and each date component is exactly the same. Datetimes with timezones are normalized
        /// to zulu before comparison is done. Throws an <see cref="ArgumentException"/> if the arguments differ in precision.</returns>
        /// <remarks>See <see cref="TryCompareTo(Any)"/> for more details.</remarks>
        public override bool Equals(object? obj) => obj is Any other && TryEquals(other).ValueOrDefault(false);

        public Result<bool> TryEquals(Any other) => other is DateTime ? TryCompareTo(other).Select(i => i == 0) : false;

        public static bool operator ==(DateTime a, DateTime b) => Equals(a, b);
        public static bool operator !=(DateTime a, DateTime b) => !Equals(a, b);


        /// <summary>
        /// Compare two datetimes based on CQL equality rules
        /// </summary>
        /// <remarks>See <see cref="TryCompareTo(Any)"/> for more details.</remarks>
        public int CompareTo(object? obj) => obj is DateTime p ?
            TryCompareTo(p).ValueOrThrow() : throw NotSameTypeComparison(this, obj);

        /// <summary>
        /// Compares two datetimes according to CQL ordering rules.
        /// </summary> 
        /// <param name="other"></param>
        /// <returns>An <see cref="Utility.Ok{T}"/> with an integer value representing the reseult of the comparison: 0 if this and other are equal, 
        /// -1 if this is smaller than other and +1 if this is bigger than other, or the other is null. If the values are incomparable
        /// this function returns a <see cref="Utility.Fail{T}"/> with the reason why the comparison between the two values was impossible.
        /// </returns>
        /// <remarks>The comparison is performed by considering each precision in order, beginning with years. 
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
                DateTime p => DateTime.CompareDateTimeParts(_parsedValue, Precision, HasOffset, p._parsedValue, p.Precision, p.HasOffset),
                _ => throw NotSameTypeComparison(this, other)
            };
        }

        internal static Result<int> CompareDateTimeParts(DateTimeOffset l, DateTimePrecision lPrec, bool lHasOffset, DateTimeOffset r, DateTimePrecision rPrec, bool rHasOffset)
        {
            l = l.ToUniversalTime();
            r = r.ToUniversalTime();
            var error = new Fail<int>(new InvalidOperationException($"The operands {l} and {r} do not have the same precision and therefore cannot be compared."));

            if (l.Year != r.Year) return Ok(l.Year.CompareTo(r.Year));

            if (lPrec < DateTimePrecision.Month ^ rPrec < DateTimePrecision.Month) return error;
            if (l.Month != r.Month) return Ok(l.Month.CompareTo(r.Month));

            if (lPrec < DateTimePrecision.Day ^ rPrec < DateTimePrecision.Day) return error;
            if (l.Day != r.Day) return Ok(l.Day.CompareTo(r.Day));

            if (lPrec < DateTimePrecision.Hour ^ rPrec < DateTimePrecision.Hour) return error;

            // Before we compare the times, let's first check whether this is possible at all.
            // Actually, this could still influence the dates too, but I don't think people would expect that to
            // be significant.  You'd like now() > Patient.birthday to work, even if one has a timezone,
            // and the other is just a date in the past.
            if ((lHasOffset && !rHasOffset) || (!lHasOffset && rHasOffset))
                return new Fail<int>(new InvalidOperationException($"One of the operands {l} and {r} has a timezone, but not the other."));

            if (l.Hour != r.Hour) return Ok(l.Hour.CompareTo(r.Hour));

            if (lPrec < DateTimePrecision.Minute ^ rPrec < DateTimePrecision.Minute) return error;
            if (l.Minute != r.Minute) return Ok(l.Minute.CompareTo(r.Minute));

            if (lPrec < DateTimePrecision.Second ^ rPrec < DateTimePrecision.Second) return error;

            // Note that DateTimeOffset rounds fractional
            // parts to millis (i.e. 12:00:00.12345 would be rounded to 12:00:00.123),
            // so I am not going to bother with the subtle decimal comparison semantics in ordering 
            // as described by the spec ("Note that for the purposes of comparison, seconds and milliseconds
            // are combined as a single precision using a decimal, with *decimal comparison semantics*.")
            // as "decimal comparison semantics" aren't specified anyway. The spec describes
            // equals/equivalence for decimals, but not ordering as far as I can see. I will
            // consider second/millisecond precision to be a single precision, i.e.  12:00:01 == 12:00:01.1
            // is false, rather than null.
            //
            // These simplifications makes my life easier here, otherwise I'd have to create ordering
            // and equivalence as separate functions.
            if (l.Second != r.Second) return Ok(l.Second.CompareTo(r.Second));
            if (l.Millisecond != r.Millisecond) return Ok(l.Millisecond.CompareTo(r.Millisecond));

            return Ok(0);
        }

        public static bool operator <(DateTime a, DateTime b) => a.CompareTo(b) < 0;
        public static bool operator <=(DateTime a, DateTime b) => a.CompareTo(b) <= 0;
        public static bool operator >(DateTime a, DateTime b) => a.CompareTo(b) > 0;
        public static bool operator >=(DateTime a, DateTime b) => a.CompareTo(b) >= 0;


        public override int GetHashCode() => _original.GetHashCode();
        public override string ToString() => _original;

        public static explicit operator DateTime(DateTimeOffset dto) => FromDateTimeOffset(dto);
        public static explicit operator Date(DateTime dt) => ((ICqlConvertible)dt).TryConvertToDate().ValueOrThrow();
        public static explicit operator String(DateTime dt) => ((ICqlConvertible)dt).TryConvertToString().ValueOrThrow();

        bool? ICqlEquatable.IsEqualTo(Any other) => other is { } && TryEquals(other) is Ok<bool> ok ? ok.Value : (bool?)null;

        // Note that, in contrast to equals, this will return false if operators cannot be compared (as described by the spec)
        bool ICqlEquatable.IsEquivalentTo(Any other) => other is { } pd && TryEquals(pd).ValueOrDefault(false);

        int? ICqlOrderable.CompareTo(Any other) => other is { } && TryCompareTo(other) is Ok<int> ok ? ok.Value : (int?)null;

        Result<DateTime> ICqlConvertible.TryConvertToDateTime() => Ok(this);

        Result<String> ICqlConvertible.TryConvertToString() => Ok(new String(ToString()));


        Result<Date> ICqlConvertible.TryConvertToDate() => TruncateToDate();

        Result<Boolean> ICqlConvertible.TryConvertToBoolean() => CannotCastTo<Boolean>(this);
        Result<Decimal> ICqlConvertible.TryConvertToDecimal() => CannotCastTo<Decimal>(this);
        Result<Integer> ICqlConvertible.TryConvertToInteger() => CannotCastTo<Integer>(this);
        Result<Long> ICqlConvertible.TryConvertToLong() => CannotCastTo<Long>(this);
        Result<Quantity> ICqlConvertible.TryConvertToQuantity() => CannotCastTo<Quantity>(this);
        Result<Ratio> ICqlConvertible.TryConvertToRatio() => CannotCastTo<Ratio>(this);
        Result<Time> ICqlConvertible.TryConvertToTime() => CannotCastTo<Time>(this);
        Result<Code> ICqlConvertible.TryConvertToCode() => CannotCastTo<Code>(this);
        Result<Concept> ICqlConvertible.TryConvertToConcept() => CannotCastTo<Concept>(this);

    }
}
