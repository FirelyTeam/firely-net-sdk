/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Linq;

namespace Hl7.Fhir.ElementModel.Types;

/// <summary>
/// The range of time covered by a <see cref="Date"/> or <see cref="DateTime"/> at its precision,
/// as the half-open interval [<see cref="Start"/>, <see cref="End"/>).
/// A <c>null</c> bound means that side of the range is unbounded.
/// </summary>
/// <remarks>Two ranges are equal when their bounds and precision are equal; <see cref="OriginalValue"/> is not compared.</remarks>
public sealed class DateTimeRange : IEquatable<DateTimeRange>
{
    internal DateTimeRange(string? originalValue, DateTimePrecision precision, DateTimeOffset? start, DateTimeOffset? end)
    {
        if (start is null && end is null) throw new ArgumentException("A range must have at least one bound.");

        OriginalValue = originalValue;
        Precision = precision;
        Start = start;
        End = end;
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
    /// The first instant covered (inclusive), or <c>null</c> when the range is unbounded below.
    /// </summary>
    public DateTimeOffset? Start { get; }

    /// <summary>
    /// The first instant after the range (exclusive), or <c>null</c> when the range is unbounded above.
    /// </summary>
    public DateTimeOffset? End { get; }

    /// <summary>
    /// Whether <paramref name="instant"/> lies within this range.
    /// </summary>
    public bool Contains(DateTimeOffset instant) =>
        (Start is null || Start.Value <= instant) && (End is null || instant < End.Value);

    /// <summary>
    /// Whether this range and <paramref name="other"/> share at least one instant.
    /// </summary>
    public bool Overlaps(DateTimeRange other)
    {
        if (other is null) throw new ArgumentNullException(nameof(other));

        return (Start is null || other.End is null || Start.Value < other.End.Value)
            && (other.Start is null || End is null || other.Start.Value < End.Value);
    }

    /// <summary>
    /// Whether every instant of <paramref name="other"/> lies within this range.
    /// </summary>
    public bool Covers(DateTimeRange other)
    {
        if (other is null) throw new ArgumentNullException(nameof(other));

        return (Start is null || (other.Start is not null && Start.Value <= other.Start.Value))
            && (End is null || (other.End is not null && other.End.Value <= End.Value));
    }

    /// <summary>
    /// Creates a range from the lower bound of <paramref name="start"/> to the upper bound of
    /// <paramref name="end"/>, with the coarser of both precisions. A <c>null</c> argument leaves that side unbounded.
    /// </summary>
    /// <exception cref="ArgumentException">Neither argument contributes a bound.</exception>
    public static DateTimeRange Between(DateTimeRange? start, DateTimeRange? end)
    {
        if (start?.Start is null && end?.End is null)
            throw new ArgumentException("At least one of start and end must contribute a bound.");

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
        other is not null && Precision == other.Precision && Start == other.Start && End == other.End;

    public override bool Equals(object? obj) => obj is DateTimeRange other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Precision, Start, End);

    public override string ToString() =>
        (Start is { } s ? $"[{s:o}" : "(-∞") + ", " + (End is { } e ? $"{e:o})" : "+∞)");

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
            : new DateTimeRange(originalValue, precision, inZone(start, zone, earliest: true), inZone(end, zone, earliest: false));
    }

    /// <summary>
    /// Builds the range for a value whose start has already been rounded to its precision, reading
    /// <paramref name="start"/> as a wall-clock time at the fixed <paramref name="offset"/>.
    /// </summary>
    internal static DateTimeRange Create(string? originalValue, DateTimePrecision precision, int fractionDigits,
        DateTimeOffset start, TimeSpan offset)
    {
        var startAtOffset = new DateTimeOffset(start.DateTime, offset);
        return new DateTimeRange(originalValue, precision, startAtOffset, addUnit(startAtOffset, precision, fractionDigits));
    }

    private static DateTimeOffset inZone(DateTimeOffset wallClock, TimeZoneInfo zone, bool earliest)
    {
        var local = System.DateTime.SpecifyKind(wallClock.DateTime, DateTimeKind.Unspecified);

        TimeSpan[] candidates =
            zone.IsAmbiguousTime(local) ? zone.GetAmbiguousTimeOffsets(local)
            // A skipped time lies in a gap of at most a few hours; take the offsets on either side of it.
            : zone.IsInvalidTime(local) ? new[] { zone.GetUtcOffset(local.AddHours(-6)), zone.GetUtcOffset(local.AddHours(6)) }
            : new[] { zone.GetUtcOffset(local) };

        // A larger offset gives an earlier instant for the same wall-clock time.
        var offset = earliest ? candidates.Max() : candidates.Min();
        return new DateTimeOffset(local, offset);
    }

    private static DateTimeOffset addUnit(DateTimeOffset start, DateTimePrecision precision, int fractionDigits) => precision switch
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
