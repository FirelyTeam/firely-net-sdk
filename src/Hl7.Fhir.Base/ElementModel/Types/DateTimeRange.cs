/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;

namespace Hl7.Fhir.ElementModel.Types;

/// <summary>
/// The range of time covered by a <see cref="Date"/> or <see cref="DateTime"/> at its precision:
/// a half-open <see cref="Interval{T}"/> of instants [<see cref="Start"/>, <see cref="End"/>),
/// together with the precision and lexical value it came from.
/// A <c>null</c> bound means that side of the range is unbounded.
/// </summary>
/// <remarks>
/// Two ranges are equal when their precision is equal and their bounds denote the same instants; <see cref="OriginalValue"/>
/// is not compared. Bounds are compared as instants, ignoring their offsets, so <c>[10:00+01:00, 10:00:01+01:00)</c>
/// and <c>[09:00Z, 09:00:01Z)</c> are equal although they print differently.
/// <para>A bound that falls outside what <see cref="DateTimeOffset"/> can represent, such as the end of
/// <c>9999-12-31</c>, makes that side unbounded. This is exact for every representable instant, whereas clamping to
/// <see cref="DateTimeOffset.MaxValue"/> would wrongly exclude that instant from a half-open range, and throwing would
/// make valid values (often used as an "indefinitely" sentinel) unusable.</para>
/// </remarks>
public sealed class DateTimeRange : IEquatable<DateTimeRange>
{
    internal DateTimeRange(string? originalValue, DateTimePrecision precision, DateTimeOffset? start, DateTimeOffset? end)
    {
        if (start is null && end is null) throw new ArgumentException("A range must have at least one bound.");

        OriginalValue = originalValue;
        Precision = precision;
        Interval = Interval<DateTimeOffset>.Create(start is not null, start.GetValueOrDefault(), true,
            end is not null, end.GetValueOrDefault(), false);
    }

    /// <summary>
    /// The lexical value this range was created from, or the start's for a range created by <see cref="Between"/>.
    /// </summary>
    public string? OriginalValue { get; }

    /// <summary>
    /// The precision of the value, or the coarser of both precisions for a range created by <see cref="Between"/>.
    /// </summary>
    public DateTimePrecision Precision { get; }

    /// <summary>
    /// The instants covered, as an interval closed below and open above.
    /// </summary>
    public Interval<DateTimeOffset> Interval { get; }

    /// <summary>
    /// The first instant covered (inclusive), or <c>null</c> when the range is unbounded below.
    /// </summary>
    public DateTimeOffset? Start => Interval.HasLow ? Interval.Low : null;

    /// <summary>
    /// The first instant after the range (exclusive), or <c>null</c> when the range is unbounded above.
    /// </summary>
    public DateTimeOffset? End => Interval.HasHigh ? Interval.High : null;

    /// <summary>
    /// Whether <paramref name="instant"/> lies within this range.
    /// </summary>
    public bool Contains(DateTimeOffset instant) => Interval.Contains(instant);

    /// <summary>
    /// Whether this range and <paramref name="other"/> share at least one instant.
    /// </summary>
    public bool Overlaps(DateTimeRange other) =>
        Interval.Overlaps((other ?? throw new ArgumentNullException(nameof(other))).Interval);

    /// <summary>
    /// Whether every instant of <paramref name="other"/> lies within this range.
    /// </summary>
    public bool Covers(DateTimeRange other) =>
        Interval.Covers((other ?? throw new ArgumentNullException(nameof(other))).Interval);

    /// <summary>
    /// Creates a range from the lower bound of <paramref name="start"/> to the upper bound of
    /// <paramref name="end"/>, with the coarser of both precisions. A <c>null</c> argument leaves that side unbounded.
    /// </summary>
    /// <exception cref="ArgumentException">Neither argument contributes a bound, or the end lies before the start.</exception>
    public static DateTimeRange Between(DateTimeRange? start, DateTimeRange? end)
    {
        if (start?.Start is null && end?.End is null)
            throw new ArgumentException("At least one of start and end must contribute a bound.");
        if (start?.Start is { } lower && end?.End is { } upper && upper <= lower)
            throw new ArgumentException("The end of the range lies before its start.");

        var precision = (start, end) switch
        {
            ({ } s, { } e) => s.Precision < e.Precision ? s.Precision : e.Precision,
            ({ } s, null) => s.Precision,
            (null, { } e) => e.Precision,
            _ => throw new InvalidOperationException()
        };

        return new DateTimeRange(start?.OriginalValue ?? end?.OriginalValue, precision, start?.Start, end?.End);
    }

    public bool Equals(DateTimeRange? other) =>
        other is not null && Precision == other.Precision && Interval.Equals(other.Interval);

    public override bool Equals(object? obj) => obj is DateTimeRange other && Equals(other);

    public static bool operator ==(DateTimeRange? left, DateTimeRange? right) => left is null ? right is null : left.Equals(right);

    public static bool operator !=(DateTimeRange? left, DateTimeRange? right) => !(left == right);

    public override int GetHashCode() => HashCode.Combine(Precision, Interval);

    public override string ToString() => Interval.ToString(v => v.ToString("o"));

    /// <summary>
    /// Builds the range for a value whose start has already been rounded to its precision.
    /// <paramref name="start"/> keeps its own offset when <paramref name="zone"/> is <c>null</c>.
    /// Otherwise it is a wall-clock time and each bound gets the offset <paramref name="zone"/> has at that moment,
    /// so a range that crosses a daylight saving change ends with a different offset than it starts.
    /// </summary>
    /// <remarks>
    /// A wall-clock time that a daylight saving change skips or repeats has two candidate offsets.
    /// The start takes the one giving the earlier instant and the end the one giving the later instant,
    /// so the range is widened, never narrowed.
    /// </remarks>
    internal static DateTimeRange Create(string? originalValue, DateTimePrecision precision, int fractionDigits,
        DateTimeOffset start, TimeZoneInfo? zone)
    {
        var end = addUnit(start, precision, fractionDigits);

        return zone is null
            ? new DateTimeRange(originalValue, precision, start, end)
            : new DateTimeRange(originalValue, precision, inZone(start.DateTime, zone, earliest: true),
                end is { } e ? inZone(e.DateTime, zone, earliest: false) : null);
    }

    /// <summary>
    /// Builds the range for a value whose start has already been rounded to its precision, reading
    /// <paramref name="start"/> as a wall-clock time at the fixed <paramref name="offset"/>.
    /// </summary>
    internal static DateTimeRange Create(string? originalValue, DateTimePrecision precision, int fractionDigits,
        DateTimeOffset start, TimeSpan offset)
    {
        var startAtOffset = at(start.DateTime, offset);
        var end = addUnit(start.DateTime, precision, fractionDigits) is { } e ? at(e, offset) : null;
        return new DateTimeRange(originalValue, precision, startAtOffset, end);
    }

    private static DateTimeOffset? inZone(System.DateTime wallClock, TimeZoneInfo zone, bool earliest)
    {
        var local = System.DateTime.SpecifyKind(wallClock, DateTimeKind.Unspecified);

        var current = zone.GetUtcOffset(local);
        // Compare with the offsets one day before and after. This catches gaps of up to a day (such as Pacific/Apia
        // skipping 2011-12-30, which TimeZoneInfo.IsInvalidTime does not report on every platform) and assumes
        // no zone has two transitions within a day.
        // The probes are clamped to the calendar's limits.
        var before = zone.GetUtcOffset(local.Ticks >= TimeSpan.TicksPerDay ? local.AddDays(-1) : System.DateTime.MinValue);
        var after = zone.GetUtcOffset(System.DateTime.MaxValue.Ticks - local.Ticks >= TimeSpan.TicksPerDay ? local.AddDays(1) : System.DateTime.MaxValue);
        if (before == current && after == current) return at(local, current);

        // Keep the candidates under which this wall-clock time really occurs; if there are none, the time was skipped
        // and both surrounding offsets are candidates.
        var validBefore = occursAt(local, before, zone);
        var validAfter = occursAt(local, after, zone);
        var (a, b) = (validBefore, validAfter) switch
        {
            (true, false) => (before, before),
            (false, true) => (after, after),
            _ => (before, after)
        };

        // A larger offset gives an earlier instant for the same wall-clock time.
        return at(local, earliest ? (a > b ? a : b) : (a < b ? a : b));
    }

    private static bool occursAt(System.DateTime local, TimeSpan offset, TimeZoneInfo zone) =>
        at(local, offset) is { } instant && zone.GetUtcOffset(instant.UtcDateTime) == offset;

    /// <summary>
    /// The instant of wall-clock time <paramref name="local"/> at <paramref name="offset"/>, or <c>null</c> when it lies
    /// outside the range of <see cref="DateTimeOffset"/>, which leaves that side of the range unbounded.
    /// </summary>
    private static DateTimeOffset? at(System.DateTime local, TimeSpan offset)
    {
        var utcTicks = local.Ticks - offset.Ticks;
        return utcTicks < 0 || utcTicks > System.DateTime.MaxValue.Ticks
            ? null
            : new DateTimeOffset(System.DateTime.SpecifyKind(local, DateTimeKind.Unspecified), offset);
    }

    /// <summary>
    /// The start of the next unit at <paramref name="precision"/>, or <c>null</c> when it lies beyond the calendar's
    /// limits, which leaves the end of the range unbounded.
    /// </summary>
    private static DateTimeOffset? addUnit(DateTimeOffset start, DateTimePrecision precision, int fractionDigits) =>
        addUnit(start.DateTime, precision, fractionDigits) is { } end ? at(end, start.Offset) : null;

    private static System.DateTime? addUnit(System.DateTime start, DateTimePrecision precision, int fractionDigits)
    {
        try
        {
            return addUnitUnchecked(start, precision, fractionDigits);
        }
        catch (ArgumentOutOfRangeException) when (precision is >= DateTimePrecision.Year and <= DateTimePrecision.Fraction)
        {
            return null;
        }
    }

    private static System.DateTime addUnitUnchecked(System.DateTime start, DateTimePrecision precision, int fractionDigits) => precision switch
    {
        DateTimePrecision.Year => start.AddYears(1),
        DateTimePrecision.Month => start.AddMonths(1),
        DateTimePrecision.Day => start.AddDays(1),
        DateTimePrecision.Hour => start.AddHours(1),
        DateTimePrecision.Minute => start.AddMinutes(1),
        DateTimePrecision.Second => start.AddSeconds(1),
        // .0 spans 100 ms, .000 spans 1 ms, .0000000 (and beyond) spans one tick of 100 ns.
        DateTimePrecision.Fraction => start.AddTicks(fractionTicks(fractionDigits)),
        _ => throw new ArgumentOutOfRangeException(nameof(precision))
    };

    private static long fractionTicks(int digits)
    {
        long ticks = TimeSpan.TicksPerSecond;
        for (var i = 0; i < Math.Min(digits, 7); i++) ticks /= 10;
        return Math.Max(ticks, 1);
    }
}
