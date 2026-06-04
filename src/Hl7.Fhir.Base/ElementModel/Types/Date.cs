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
using System.Text.RegularExpressions;

namespace Hl7.Fhir.ElementModel.Types;

public class Date : Any, IComparable, ICqlEquatable, ICqlOrderable
{
    private Date(DateTimeOffset value, DateTimePrecision precision, bool hasOffset)
    {
        if (precision > DateTimePrecision.Day) throw new ArgumentException($"Invalid precision {precision}, cannot be more than {nameof(DateTimePrecision.Day)}.", nameof(precision));

        _value = DateTime.RoundToPrecision(value, precision, hasOffset);
        Precision = precision;
        HasOffset = hasOffset;
    }

    public static Date Parse(string representation) =>
        TryParse(representation, out var result) ? result : throw new FormatException($"String '{representation}' was not recognized as a valid date.");

    public static bool TryParse(string representation, [NotNullWhen(true)] out Date? value) => tryParse(representation, out value);

    public static Date FromDateTimeOffset(DateTimeOffset dto, DateTimePrecision prec = DateTimePrecision.Day,
        bool includeOffset = false) => new(dto, prec, includeOffset);

    public DateTime ToDateTime() => DateTime.FromDateTimeOffset(_value, Precision, HasOffset);

    public static Date Today(bool includeOffset = false) => FromDateTimeOffset(DateTimeOffset.Now, includeOffset: includeOffset);

    /// <summary>
    /// The precision of the date available.
    /// </summary>
    public DateTimePrecision Precision { get; }

    public int? Years => Precision >= DateTimePrecision.Year ? _value.Year : null;
    public int? Months => Precision >= DateTimePrecision.Month ? _value.Month : null;
    public int? Days => Precision >= DateTimePrecision.Day ? _value.Day : null;

    /// <summary>
    /// The span of time ahead/behind UTC
    /// </summary>
    public TimeSpan? Offset => HasOffset ? _value.Offset : null;

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
    /// Converts the date to a full DateTimeOffset instance.
    /// </summary>
    public DateTimeOffset ToDateTimeOffset(TimeSpan defaultOffset) =>
        HasOffset ? _value : new DateTimeOffset(_value.Ticks, defaultOffset);

    /// <summary>
    /// Converts the date to a full DateTimeOffset instance.
    /// </summary>
    /// <param name="hours"></param>
    /// <param name="minutes"></param>
    /// <param name="seconds"></param>
    /// <param name="defaultOffset">Offset used when the datetime does not specify one.</param>
    /// <returns></returns>
    public DateTimeOffset ToDateTimeOffset(int hours, int minutes, int seconds, TimeSpan defaultOffset) =>
        ToDateTimeOffset(hours, minutes, seconds, 0, defaultOffset);

    /// <summary>
    /// Converts the date to a full DateTimeOffset instance.
    /// </summary>
    /// <param name="hours"></param>
    /// <param name="minutes"></param>
    /// <param name="seconds"></param>
    /// <param name="milliseconds"></param>
    /// <param name="defaultOffset">Offset used when the datetime does not specify one.</param>
    /// <returns></returns>
    public DateTimeOffset ToDateTimeOffset(int hours, int minutes, int seconds, int milliseconds, TimeSpan defaultOffset) =>
        new(_value.Year, _value.Month, _value.Day, hours, minutes, seconds, milliseconds,
            HasOffset ? _value.Offset : defaultOffset);

    private static readonly string DATEFORMAT =
        $"(?<year>[0-9]{{4}}) ((?<month>-[0-9][0-9]) ((?<day>-[0-9][0-9]) )?)? {Time.OFFSETFORMAT}?";
    public static readonly Regex PARTIALDATEREGEX = new("^" + DATEFORMAT + "$",
        RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

    /// <summary>
    /// Converts the date to a full DateTimeOffset instance.
    /// </summary>
    /// <returns></returns>
    private static bool tryParse(string representation, out Date? value)
    {
        if (representation is null) throw new ArgumentNullException(nameof(representation));

        var matches = PARTIALDATEREGEX.Match(representation);
        if (!matches.Success)
        {
            value = null;
            return false;
        }

        var y = matches.Groups["year"];
        var m = matches.Groups["month"];
        var d = matches.Groups["day"];
        var offset = matches.Groups["offset"];

        var prec =
            d.Success ? DateTimePrecision.Day :
            m.Success ? DateTimePrecision.Month :
            DateTimePrecision.Year;

        var parseableDt = y.Value +
                          (m.Success ? m.Value : "-01") +
                          (d.Success ? d.Value : "-01") +
                          "T" + "00:00:00" +
                          (offset.Success ? offset.Value : "Z");

        var success = DateTimeOffset.TryParse(parseableDt, out var parsedValue);
        value = new Date(parsedValue, prec, offset.Success)
        {
            OriginalParsedString = representation
        };

        return success;
    }

    public static Date operator -(Date dateValue, Quantity subtractValue)
    {
        if (dateValue is null) throw new ArgumentNullException(nameof(dateValue));
        if (subtractValue is null) throw new ArgumentNullException(nameof(subtractValue));

        return add(dateValue, -subtractValue.Value, subtractValue.Unit);
    }

    public static Date operator +(Date dateValue, Quantity addValue)
    {
        if (dateValue is null) throw new ArgumentNullException(nameof(dateValue));
        if (addValue is null) throw new ArgumentNullException(nameof(addValue));

        return add(dateValue, addValue.Value, addValue.Unit);
    }

    private static Date add(Date dateValue, decimal value, string unit)
    {
        // Based on the discussion on equality/comparisons here:
        // https://chat.fhir.org/#narrow/stream/179266-fhirpath/topic/Date.2FTime.20comparison.20vs.20equality
        // We have also allowed addition to use the definitve UCUM units of 'wk', 'd' as if they are a calendar unit of
        // 'week'/'day' respectively.
        var dto = unit switch
        {
            // we can ignore precision, as the precision will "trim" it anyway, and if we add 13 months, then the year can tick over nicely
            "years" or "year" => dateValue._value.AddYears((int)value),
            "month" or "months" => dateValue.Precision == DateTimePrecision.Year
                ? dateValue._value.AddYears((int)(value / 12))
                : dateValue._value.AddMonths((int)value),
            "week" or "weeks" or "wk" => dateValue.Precision switch
            {
                DateTimePrecision.Year => dateValue._value.AddYears((int)(value / 52)),
                DateTimePrecision.Month => dateValue._value.AddMonths((int)(value * 7 / 30)),
                _ => dateValue._value.AddDays(((int)value) * 7)
            },
            "day" or "days" or "d" => dateValue.Precision switch
            {
                DateTimePrecision.Year => dateValue._value.AddYears((int)(value / 365)),
                DateTimePrecision.Month => dateValue._value.AddMonths((int)(value / 30)),
                _ => dateValue._value.AddDays((int)value)
            },
            _ => throw new ArgumentException($"'{unit}' is not a valid time-valued unit", nameof(unit)),
        };
        var result = FromDateTimeOffset(dto, dateValue.Precision);
        return result;
    }

    /// <summary>
    /// Determines if two dates are equal according to CQL equality rules.
    /// </summary>
    /// <returns>returns true if the values are both dates, have the same precision and each date component is exactly the same.
    /// Dates with timezones are normalized to zulu before comparison is done.</returns>
    /// <remarks>See <see cref="TryCompareTo"/> for more details.</remarks>
    public override bool Equals(object? obj) => obj is Any other && TryEquals(other, out var result) && result.Value;

    public bool TryEquals(Any other, [NotNullWhen(true)] out bool? result)
    {
        result = other is Date
            ? TryCompareTo(other, out var comparison) ? comparison == 0 : null
            : false;

        return result is not null;
    }

    public static bool operator ==(Date a, Date b) => Equals(a, b);
    public static bool operator !=(Date a, Date b) => !Equals(a, b);

    /// <summary>
    /// Compare two dates according to CQL equality rules
    /// </summary>
    /// <remarks>See <see cref="TryCompareTo"/> for more details.</remarks>
    public int CompareTo(object? obj) => obj is Date p && TryCompareTo(p, out var result)
        ? result.Value
        : throw new InvalidOperationException("The operands are not comparable.");

    /// <summary>
    /// Compares two dates according to CQL ordering rules.
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
            Date p => DateTime.TryCompareDateTimeParts(_value, Precision, HasOffset, p._value, p.Precision, p.HasOffset),
            _ => null
        };

        return result is not null;
    }

    public static bool operator <(Date a, Date b) => a.CompareTo(b) < 0;
    public static bool operator <=(Date a, Date b) => a.CompareTo(b) <= 0;
    public static bool operator >(Date a, Date b) => a.CompareTo(b) > 0;
    public static bool operator >=(Date a, Date b) => a.CompareTo(b) >= 0;

    public override int GetHashCode() => _value.GetHashCode();
    public override string ToString() => OriginalParsedString ?? DateTime.ToStringWithPrecision(_value, Precision, HasOffset);

    public static implicit operator string(Date d) => d.ToString();
    public static implicit operator DateTime(Date pd) => RunCast<DateTime>(pd);
    public static explicit operator Date(DateTimeOffset dto) => FromDateTimeOffset(dto);
    public static explicit operator String(Date d) => RunCast<String>(d);

    bool? ICqlEquatable.IsEqualTo(Any? other) => other is not null && TryEquals(other, out var result) ? result : null;

    // Note that, in contrast to equals, this will return false if operators cannot be compared (as described by the spec)
    bool ICqlEquatable.IsEquivalentTo(Any? other) => other is not null && TryEquals(other, out var result) && result.Value;

    int? ICqlOrderable.CompareTo(Any? other) => other is not null && TryCompareTo(other, out var result) ? result : null;

    public override bool TryConvertTo(Type to, [NotNullWhen(true)] out Any? result)
    {
        result = null;

        if(to == typeof(Date))
            result = this;
        else if (to == typeof(DateTime))
            result = ToDateTime();
        else if (to == typeof(String))
            result = new String(ToString());

        return result is not null;
    }
}