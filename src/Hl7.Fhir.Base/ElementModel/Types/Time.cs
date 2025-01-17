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

public class Time : Any, IComparable, ICqlEquatable, ICqlOrderable
{
    private Time(DateTimeOffset parsedValue, DateTimePrecision precision, bool hasOffset)
    {
        if (precision < DateTimePrecision.Hour) throw new ArgumentException($"Invalid precision {precision}, cannot be less than {nameof(DateTimePrecision.Hour)}.", nameof(precision));

        _value = DateTime.RoundToPrecision(parsedValue, precision, hasOffset);
        Precision = precision;
        HasOffset = hasOffset;
    }

    public static Time Parse(string representation) =>
        TryParse(representation, out var result) ? result : throw new FormatException($"String '{representation}' was not recognized as a valid time.");

    public static bool TryParse(string representation, [NotNullWhen(true)] out Time? value) => tryParse(representation, out value);

    public static Time FromDateTimeOffset(DateTimeOffset dto, DateTimePrecision prec = DateTimePrecision.Fraction,
        bool includeOffset = false) => new(dto, prec, includeOffset);

    public static Time Now(bool includeOffset = false) => FromDateTimeOffset(DateTimeOffset.Now, includeOffset: includeOffset);

    /// <summary>
    /// The precision of the time available. 
    /// </summary>
    public DateTimePrecision Precision { get; }

    public int? Hours => Precision >= DateTimePrecision.Hour ? _value.Hour : null;
    public int? Minutes => Precision >= DateTimePrecision.Minute ? _value.Minute : null;
    public int? Seconds => Precision >= DateTimePrecision.Second ? _value.Second : null;
    public int? Millis => Precision >= DateTimePrecision.Fraction ? _value.Millisecond : null;

    /// <summary>
    /// The span of time ahead/behind UTC
    /// </summary>
    public TimeSpan? Offset => HasOffset ? _value.Offset : null;

    /// <summary>
    /// Whether the time specifies an offset to UTC
    /// </summary>
    public bool HasOffset { get; }

    private string? _originalParsedString;
    private readonly DateTimeOffset _value;

    /// <summary>
    /// Converts the time to a full DateTimeOffset instance.
    /// </summary>
    /// <param name="year">Year used to turn a time into a date</param>
    /// <param name="month">Month used to turn a time into a date</param>
    /// <param name="day">Day used to turn a time into a date</param>
    /// <param name="defaultOffset">Offset used when the time does not specify one.</param>
    /// <returns></returns>
    public DateTimeOffset ToDateTimeOffset(int year, int month, int day, TimeSpan defaultOffset)
    {
        // Since the DTO constructor only takes milliseconds, we would lose the sub-millisecond
        // information from time. So, let's get the micro/nanosecond info from the ticks within
        // the current millisecond, and add that to the newly created DTO.
        var remainderTicks = _value.Ticks % TimeSpan.TicksPerMillisecond;
        var result = new DateTimeOffset(year, month, day, _value.Hour,
            _value.Minute, _value.Second, _value.Millisecond,
            HasOffset ? _value.Offset : defaultOffset).AddTicks(remainderTicks);

        return result;
    }

    /// <summary>
    /// Converts the time to a TimeSpan instance, filled out to the whole hour.
    /// </summary>
    public TimeSpan ToTimeSpan() => new(_value.Hour, _value.Minute, _value.Second);


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

    private static bool tryParse(string representation, out Time? value)
    {
        if (representation is null) throw new ArgumentNullException(nameof(representation));

        var matches = PARTIALTIMEREGEX.Match(representation);
        if (!matches.Success)
        {
            value = null;
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

        var parseableDt = $"2016-01-01T" +
                          (hrg.Success ? hrg.Value : "00") +
                          (ming.Success ? ming.Value : ":00") +
                          (secg.Success ? secg.Value : ":00") +
                          (fracg.Success ? fracg.Value : "") +
                          (offset.Success ? offset.Value : "Z");

        var success = DateTimeOffset.TryParse(parseableDt, out var parsedValue);
        value = new Time(parsedValue, prec, offset.Success)
        {
            _originalParsedString = representation
        };

        return success;
    }

    /// <summary>
    /// Compare two times based on CQL equality rules
    /// </summary>
    /// <returns>returns true if the values have the same precision, and each date component is exactly the same. Datetimes with timezones are normalized
    /// to zulu before comparison is done. Throws an <see cref="ArgumentException"/> if the arguments differ in precision.</returns>
    /// <remarks>See <see cref="TryCompareTo"/> for more details.</remarks>
    public override bool Equals(object? obj) => obj is Any other && TryEquals(other, out var result) && result.Value;

    public bool TryEquals(Any other, [NotNullWhen(true)] out bool? result)
    {
        result = other is Time
            ? TryCompareTo(other, out var comparison) ? comparison == 0 : null
            : false;

        return result is not null;

    }

    public static bool operator ==(Time a, Time b) => Equals(a, b);
    public static bool operator !=(Time a, Time b) => !Equals(a, b);

    /// <summary>
    /// Compare two times based on CQL equality rules
    /// </summary>
    /// <remarks>See <see cref="TryCompareTo"/> for more details.</remarks>
    public int CompareTo(object? obj) => obj is Time p && TryCompareTo(p, out var result)
        ? result.Value
        : throw new InvalidOperationException("The operands are not comparable.");

    /// <summary>
    /// Compares two times according to CQL ordering rules.
    /// </summary> 
    /// <param name="other"></param>
    /// <param name="result">The reseult of the comparison: 0 if this and other are equal,
    /// -1 if this is smaller than other and +1 if this is bigger than other, or the other is null. If the values are incomparable
    /// this function returns false.</param>
    /// <remarks>The comparison is performed by considering each precision in order, beginning with hours. 
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
            Time p => compareWith(p),
            _ => null
        };

        return result is not null;

        int? compareWith(Time p)
        {
            // Since the day part is not relevant, normalize this to a random date
            // before comparing (since the CompareDateTimeParts() WILL compare the dates when
            // we have time precision.
            var left = ToDateTimeOffset(1972, 11, 30, TimeSpan.Zero);
            var right = p.ToDateTimeOffset(1972, 11, 30, TimeSpan.Zero);

            return DateTime.TryCompareDateTimeParts(left, Precision, HasOffset, right, p.Precision, p.HasOffset);
        }
    }

    public static bool operator <(Time a, Time b) => a.CompareTo(b) < 0;
    public static bool operator <=(Time a, Time b) => a.CompareTo(b) <= 0;
    public static bool operator >(Time a, Time b) => a.CompareTo(b) > 0;
    public static bool operator >=(Time a, Time b) => a.CompareTo(b) >= 0;


    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString()
    {
        if (_originalParsedString is not null) return _originalParsedString;

        string formatString = Precision switch
        {
            DateTimePrecision.Hour => "HH",
            DateTimePrecision.Minute => "HH:mm",
            DateTimePrecision.Second => "HH:mm:ss",
            _ => "HH:mm:ss.FFFFFFF",
        };

        if (HasOffset) formatString += "K";

        return _value.ToString(formatString, CultureInfo.InvariantCulture);
    }

 bool? ICqlEquatable.IsEqualTo(Any? other) => other is not null && TryEquals(other, out var result) ? result : null;

    // Note that, in contrast to equals, this will return false if operators cannot be compared (as described by the spec)
    bool ICqlEquatable.IsEquivalentTo(Any? other) => other is not null && TryEquals(other, out var result) && result.Value;

    int? ICqlOrderable.CompareTo(Any? other) => other is not null && TryCompareTo(other, out var result) ? result : null;

    public static explicit operator Time(DateTimeOffset dto) => FromDateTimeOffset(dto);
    public static explicit operator String(Time dt) => RunCast<String>(dt);

    public override bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result)
    {
        result = null;

        if(to == typeof(Time))
            result = this;
        else if(to == typeof(String))
            result = new String(ToString());

        return result is not null;
    }
}