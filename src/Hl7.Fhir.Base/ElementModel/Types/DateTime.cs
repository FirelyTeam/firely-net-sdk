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
using System.Text.RegularExpressions;

namespace Hl7.Fhir.ElementModel.Types;

public class DateTime : Any, IComparable, ICqlEquatable, ICqlOrderable
{
    private DateTime(DateTimeOffset value, DateTimePrecision precision, bool includeOffset)
    {
        _value = RoundToPrecision(value, precision, includeOffset);
        Precision = precision;
        HasOffset = includeOffset;
    }

    public static DateTime Parse(string representation) =>
        TryParse(representation, out var result) ? result : throw new FormatException($"String '{representation}' was not recognized as a valid datetime.");

    public static bool TryParse(string representation, [NotNullWhen(true)] out DateTime? value) => tryParse(representation, out value);

    public static string FormatDateTimeOffset(DateTimeOffset dto) => dto.ToString(FMT_FULL, CultureInfo.InvariantCulture);

    /// <summary>
    /// Rounds the contents of a <see cref="DateTimeOffset"/> to the given precision, unused precision if filled out
    /// as midnight, the first of january, GMT.
    /// </summary>
    /// <param name="source">The <see cref="DateTimeOffset"/> to round.</param>
    /// <param name="precision">The precision to round down to.</param>
    /// <param name="withOffset">Whether to use the timezone specified, or round it to <see cref="TimeSpan.Zero"/>.</param>
    internal static DateTimeOffset RoundToPrecision(DateTimeOffset source, DateTimePrecision precision, bool withOffset) => precision switch
    {
        DateTimePrecision.Year => new DateTimeOffset(source.Year, 1, 1, 0, 0, 0, withOffset ? source.Offset : TimeSpan.Zero),
        DateTimePrecision.Month => new DateTimeOffset(source.Year, source.Month, 1, 0, 0, 0, withOffset ? source.Offset : TimeSpan.Zero),
        DateTimePrecision.Day => new DateTimeOffset(source.Year, source.Month, source.Day, 0, 0, 0, withOffset ? source.Offset : TimeSpan.Zero),
        DateTimePrecision.Hour => new DateTimeOffset(source.Year, source.Month, source.Day, source.Hour, 0, 0, withOffset ? source.Offset : TimeSpan.Zero),
        DateTimePrecision.Minute => new DateTimeOffset(source.Year, source.Month, source.Day, source.Hour, source.Minute, 0, withOffset ? source.Offset : TimeSpan.Zero),
        DateTimePrecision.Second => new DateTimeOffset(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, withOffset ? source.Offset : TimeSpan.Zero),
        _ => new DateTimeOffset(source.Ticks, withOffset ? source.Offset : TimeSpan.Zero),
    };

    public static DateTime FromDateTimeOffset(DateTimeOffset dto, DateTimePrecision prec = DateTimePrecision.Fraction, bool includeOffset = true) =>
        new(dto, prec, includeOffset);

    public static DateTime Now() => FromDateTimeOffset(DateTimeOffset.Now);

    public static DateTime Today(bool includeOffset = true) => new(DateTimeOffset.Now, DateTimePrecision.Day, includeOffset);

    public Date TruncateToDate() => Date.FromDateTimeOffset(_value, Precision > DateTimePrecision.Day ? DateTimePrecision.Day : Precision, HasOffset);

    /// <summary>
    /// Whether this DateTime represents a precise instant in time, according to FHIR rules.
    /// </summary>
    public bool IsInstant => HasOffset && Precision >= DateTimePrecision.Second;

    public int? Years => Precision >= DateTimePrecision.Year ? _value.Year : null;
    public int? Months => Precision >= DateTimePrecision.Month ? _value.Month : null;
    public int? Days => Precision >= DateTimePrecision.Day ? _value.Day : null;
    public int? Hours => Precision >= DateTimePrecision.Hour ? _value.Hour : null;
    public int? Minutes => Precision >= DateTimePrecision.Minute ? _value.Minute : null;
    public int? Seconds => Precision >= DateTimePrecision.Second ? _value.Second : null;
    public int? Millis => Precision >= DateTimePrecision.Fraction ? _value.Millisecond : null;

    /// <summary>
    /// The span of time ahead/behind UTC
    /// </summary>
    public TimeSpan? Offset => HasOffset ? _value.Offset : null;

    /// <summary>
    /// The precision of the date and time available.
    /// </summary>
    public DateTimePrecision Precision { get; }

    /// <summary>
    /// Whether the time specifies an offset to UTC
    /// </summary>
    public bool HasOffset { get; }

    /// <summary>
    /// If this instance was constructed using Parse(), this is the original
    /// raw input to the parse. Used to guarantee roundtrippability.
    /// </summary>
    private string? OriginalParsedString { get; init; }

    private readonly DateTimeOffset _value;

    /// <summary>
    /// Converts the datetime to a full DateTimeOffset instance.
    /// </summary>
    /// <param name="defaultOffset">Offset used when the datetime does not specify one.</param>
    /// <returns></returns>
    public DateTimeOffset ToDateTimeOffset(TimeSpan defaultOffset) =>
        HasOffset ? _value : new DateTimeOffset(_value.Ticks, defaultOffset);

    public const string FMT_FULL = "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFK";

    private static readonly string DATETIMEFORMAT =
        $"(?<year>[0-9]{{4}}) ((?<month>-[0-9][0-9]) ((?<day>-[0-9][0-9]) (T{Time.TIMEFORMAT})?)?)? {Time.OFFSETFORMAT}?";
    private static readonly Regex DATETIMEREGEX =
        new("^" + DATETIMEFORMAT + "$",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

    private static bool tryParse(string representation, out DateTime? value)
    {
        if (representation is null) throw new ArgumentNullException(nameof(representation));

        var matches = DATETIMEREGEX.Match(representation);
        if (!matches.Success)
        {
            value = null;
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

        var parseableDt = yrg.Value +
                          (mong.Success ? mong.Value : "-01") +
                          (dayg.Success ? dayg.Value : "-01") +
                          (hrg.Success ? "T" + hrg.Value : "T00") +
                          (ming.Success ? ming.Value : ":00") +
                          (secg.Success ? secg.Value : ":00") +
                          (fracg.Success ? fracg.Value : "") +
                          (offset.Success ? offset.Value : "Z");

        var success = DateTimeOffset.TryParse(parseableDt, out var parsedValue);
        value = new DateTime(parsedValue, prec, offset.Success)
        {
            OriginalParsedString = representation
        };

        return success;
    }

    public static DateTime operator -(DateTime dateTimeValue, Quantity subtractValue)
    {
        if (dateTimeValue is null) throw new ArgumentNullException(nameof(dateTimeValue));
        if (subtractValue is null) throw new ArgumentNullException(nameof(subtractValue));

        return add(dateTimeValue, -subtractValue.Value, subtractValue.Unit);
    }

    public static DateTime operator +(DateTime dateTimeValue, Quantity addValue)
    {
        if (dateTimeValue is null) throw new ArgumentNullException(nameof(dateTimeValue));
        if (addValue is null) throw new ArgumentNullException(nameof(addValue));

        return add(dateTimeValue, addValue.Value, addValue.Unit);
    }

    private static DateTime add(DateTime dateTimeValue, decimal value, string unit)
    {
        // Based on the discussion on equality/comparisons here:
        // https://chat.fhir.org/#narrow/stream/179266-fhirpath/topic/Date.2FTime.20comparison.20vs.20equality
        // We have also allowed addition to use the definitve UCUM units of 'wk', 'd', 'h', 'min'  as if they are a calendar unit of
        // 'week'/'day'/'hour'/'minute' respectively.
        var dto = unit switch
        {
            // we can ignore precision, as the precision will "trim" it anyway, and if we add 13 months, then the year can tick over nicely
            "years" or "year" => dateTimeValue._value.AddYears((int)value),
            "month" or "months" => dateTimeValue.Precision == DateTimePrecision.Year
                ? dateTimeValue._value.AddYears((int)(value / 12))
                : dateTimeValue._value.AddMonths((int)value),
            "week" or "weeks" or "wk" => dateTimeValue.Precision switch
            {
                DateTimePrecision.Year => dateTimeValue._value.AddYears((int)(value / 52)),
                DateTimePrecision.Month => dateTimeValue._value.AddMonths((int)(value * 7 / 30)),
                _ => dateTimeValue._value.AddDays(((int)value) * 7)
            },
            "day" or "days" or "d" => dateTimeValue.Precision switch
            {
                DateTimePrecision.Year => dateTimeValue._value.AddYears((int)(value / 365)),
                DateTimePrecision.Month => dateTimeValue._value.AddMonths((int)(value / 30)),
                _ => dateTimeValue._value.AddDays((int)value)
            },

            // NOT ignoring precision on time based stuff if there is no time component
            // if no time component, don't modify result
            "hour" or "hours" or "h" => dateTimeValue.Precision > DateTimePrecision.Day
                ? dateTimeValue._value.AddHours((double)value)
                : dateTimeValue._value,
            "minute" or "minutes" or "min" => dateTimeValue.Precision > DateTimePrecision.Day
                ? dateTimeValue._value.AddMinutes((double)value)
                : dateTimeValue._value,
            "s" or "second" or "seconds" => dateTimeValue.Precision > DateTimePrecision.Day
                ? dateTimeValue._value.AddSeconds((double)value)
                : dateTimeValue._value,
            "ms" or "millisecond" or "milliseconds" => dateTimeValue.Precision > DateTimePrecision.Day
                ? dateTimeValue._value.AddMilliseconds((double)value)
                : dateTimeValue._value,
            _ => throw new ArgumentException($"'{unit}' is not a valid time-valued unit", nameof(unit)),
        };

        var resultRepresentation = dto.ToString(FMT_FULL, CultureInfo.InvariantCulture);
        var originalRepresentation = dateTimeValue.ToString();

        if (resultRepresentation.Length > originalRepresentation.Length)
        {
            // need to trim appropriately.
            if (dateTimeValue.Precision <= DateTimePrecision.Minute)
                resultRepresentation = resultRepresentation.Substring(0, originalRepresentation.Length);
            else
            {
                if (!dateTimeValue.HasOffset)
                {
                    // trim the offset from it
                    resultRepresentation = dto.ToString("yyyy-MM-dd'T'HH:mm:ss.FFFFFFF");
                }
            }
        }

        return Parse(resultRepresentation);
    }

    /// <summary>
    /// Compare two datetimes based on CQL equality rules
    /// </summary>
    /// <returns>returns true if the values have the same precision, and each date component is exactly the same. Datetimes with timezones are normalized
    /// to zulu before comparison is done. Throws an <see cref="ArgumentException"/> if the arguments differ in precision.</returns>
    /// <remarks>See <see cref="TryCompareTo"/> for more details.</remarks>
    public override bool Equals(object? obj) => obj is Any other && TryEquals(other, out var result)
        ? result.Value
        : throw new ArgumentException("The operands are not equatable.");

    public override int GetHashCode() => _value.GetHashCode();

    public bool TryEquals(Any other, [NotNullWhen(true)] out bool? result)
    {
        result = other is DateTime
            ? TryCompareTo(other, out var comparison) ? comparison == 0 : null
            : false;

        return result is not null;
    }

    public static bool operator ==(DateTime a, DateTime b) => Equals(a, b);
    public static bool operator !=(DateTime a, DateTime b) => !Equals(a, b);


    /// <summary>
    /// Compare two datetimes based on CQL equality rules
    /// </summary>
    /// <remarks>See <see cref="TryCompareTo"/> for more details.</remarks>
    public int CompareTo(object? obj) =>
        obj is DateTime p && TryCompareTo(p, out var result)
            ? result.Value
            : throw new InvalidOperationException("The operands are not comparable.");

    /// <summary>
    /// Compares two datetimes according to CQL ordering rules.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="result">0 if this and other are equal,
    /// -1 if this is smaller than other and +1 if this is bigger than other, or the other is null. Will be null if the values are
    /// incomparable.</param>
    /// <returns>true if the comparison could be performed, false otherwise.</returns>
    /// <remarks>The comparison is performed by considering each precision in order, beginning with years.
    /// If the values are the same, comparison proceeds to the next precision;
    /// if the values are different, the comparison stops and the result is false. If one input has a value
    /// for the precision and the other does not, the comparison stops and the values cannot be compared; if neither
    /// input has a value for the precision, or the last precision has been reached, the comparison stops
    /// and the result is true.</remarks>
    public bool TryCompareTo(Any other, [NotNullWhen(true)] out int? result)
    {
        result = other switch
        {
            null => 1,
            DateTime p => TryCompareDateTimeParts(_value, Precision, HasOffset, p._value, p.Precision, p.HasOffset),
            _ => null
        };

        return result is not null;
    }

    internal static int? TryCompareDateTimeParts(
        DateTimeOffset l, DateTimePrecision lPrec, bool lHasOffset,
        DateTimeOffset r, DateTimePrecision rPrec, bool rHasOffset)
    {
        l = l.ToUniversalTime();
        r = r.ToUniversalTime();
        //var error = new InvalidOperationException($"The operands {l} and {r} do not have the same precision and therefore cannot be compared.");

        if (l.Year != r.Year) return l.Year.CompareTo(r.Year);

        if (lPrec < DateTimePrecision.Month ^ rPrec < DateTimePrecision.Month) return null;
        if (l.Month != r.Month) return l.Month.CompareTo(r.Month);

        if (lPrec < DateTimePrecision.Day ^ rPrec < DateTimePrecision.Day) return null;
        if (l.Day != r.Day) return l.Day.CompareTo(r.Day);

        if (lPrec < DateTimePrecision.Hour ^ rPrec < DateTimePrecision.Hour) return null;

        // Before we compare the times, let's first check whether this is possible at all.
        // Actually, this could still influence the dates too, but I don't think people would expect that to
        // be significant.  You'd like now() > Patient.birthday to work, even if one has a timezone,
        // and the other is just a date in the past.
        if ((lHasOffset && !rHasOffset) || (!lHasOffset && rHasOffset))
            return null;
        //throw new InvalidOperationException($"One of the operands {l} and {r} has a timezone, but not the other.");

        if (l.Hour != r.Hour) return l.Hour.CompareTo(r.Hour);

        if (lPrec < DateTimePrecision.Minute ^ rPrec < DateTimePrecision.Minute) return null;
        if (l.Minute != r.Minute) return l.Minute.CompareTo(r.Minute);

        if (lPrec < DateTimePrecision.Second ^ rPrec < DateTimePrecision.Second) return null;

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
        if (l.Second != r.Second) return l.Second.CompareTo(r.Second);
        if (l.Millisecond != r.Millisecond) return l.Millisecond.CompareTo(r.Millisecond);

        return 0;
    }

    public static bool operator <(DateTime a, DateTime b) => a.CompareTo(b) < 0;
    public static bool operator <=(DateTime a, DateTime b) => a.CompareTo(b) <= 0;
    public static bool operator >(DateTime a, DateTime b) => a.CompareTo(b) > 0;
    public static bool operator >=(DateTime a, DateTime b) => a.CompareTo(b) >= 0;

    public static explicit operator DateTime(DateTimeOffset dto) => FromDateTimeOffset(dto);
    public static explicit operator Date(DateTime dt) => RunCast<Date>(dt);
    public static explicit operator String(DateTime dt) => RunCast<String>(dt);

    bool? ICqlEquatable.IsEqualTo(Any? other) => other is not null && TryEquals(other, out var result) ? result : null;

    // Note that, in contrast to equals, this will return false if operators cannot be compared (as described by the spec)
    bool ICqlEquatable.IsEquivalentTo(Any? other) => other is not null && TryEquals(other, out var result) && result.Value;

    int? ICqlOrderable.CompareTo(Any? other) => other is not null && TryCompareTo(other, out var result) ? result : null;

    public override bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result)
    {
        result = null;

        if(to == typeof(DateTime))
            result = this;
        else if (to == typeof(Date))
            result = TruncateToDate();
        else if (to == typeof(String))
            result = new String(ToString());

        return result is not null;
    }


    public override string ToString() => OriginalParsedString ?? ToStringWithPrecision(_value, Precision, HasOffset);
    internal static string ToStringWithPrecision(DateTimeOffset dto, DateTimePrecision prec, bool includeOffset)
    {
        // "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFK";
        var length = prec switch
        {
            DateTimePrecision.Year => 4,
            DateTimePrecision.Month => 7,
            DateTimePrecision.Day => 10,
            DateTimePrecision.Hour => 15,
            DateTimePrecision.Minute => 18,
            DateTimePrecision.Second => 21,
            DateTimePrecision.Fraction => 29,
            _ => 29
        };

        var format = FMT_FULL.Substring(0, length);
        if (includeOffset) format += 'K';
        return dto.ToString(format, CultureInfo.InvariantCulture);
    }
}