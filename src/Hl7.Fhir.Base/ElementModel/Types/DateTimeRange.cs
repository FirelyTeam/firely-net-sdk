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
/// The range of time covered by a <see cref="Date"/> or <see cref="DateTime"/> at its precision,
/// as the half-open interval [<see cref="Start"/>, <see cref="End"/>).
/// A <c>null</c> bound means that side of the range is open.
/// </summary>
public sealed class DateTimeRange
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
    /// The first instant covered (inclusive), or <c>null</c> when the start is open.
    /// </summary>
    public DateTimeOffset? Start { get; }

    /// <summary>
    /// The first instant after the range (exclusive), or <c>null</c> when the end is open.
    /// </summary>
    public DateTimeOffset? End { get; }

    /// <summary>
    /// Creates a range from the lower bound of <paramref name="start"/> to the upper bound of
    /// <paramref name="end"/>, with the coarser of both precisions. A <c>null</c> argument leaves that side open.
    /// </summary>
    public static DateTimeRange Between(DateTimeRange? start, DateTimeRange? end)
    {
        if (start is null && end is null) throw new ArgumentException("At least one of start and end must be given.");

        var precision = (start, end) switch
        {
            ({ } s, { } e) => s.Precision < e.Precision ? s.Precision : e.Precision,
            ({ } s, null) => s.Precision,
            (null, { } e) => e.Precision,
            _ => throw new InvalidOperationException()
        };

        return new DateTimeRange(start?.OriginalValue ?? end?.OriginalValue, precision, start?.Start, end?.End);
    }

    public override string ToString() => $"[{Start?.ToString("o") ?? ""}, {End?.ToString("o") ?? ""})";

    /// <summary>
    /// Builds the range for a value whose start has been rounded to its precision.
    /// When the value has no offset, <paramref name="startWallClock"/> is a wall-clock time and
    /// both bounds get the offset that <paramref name="zone"/> has at that moment, so a range that
    /// crosses a daylight saving change ends with a different offset than it starts.
    /// </summary>
    internal static DateTimeRange Create(string? originalValue, DateTimePrecision precision, int fractionDigits,
        DateTimeOffset startWallClock, TimeZoneInfo? zone)
    {
        var endWallClock = addUnit(startWallClock, precision, fractionDigits);

        return zone is null
            ? new DateTimeRange(originalValue, precision, startWallClock, endWallClock)
            : new DateTimeRange(originalValue, precision, inZone(startWallClock, zone), inZone(endWallClock, zone));
    }

    private static DateTimeOffset inZone(DateTimeOffset wallClock, TimeZoneInfo zone)
    {
        // TODO: decide how to treat wall-clock times that are invalid or ambiguous because of a DST change.
        var local = System.DateTime.SpecifyKind(wallClock.DateTime, DateTimeKind.Unspecified);
        return new DateTimeOffset(local, zone.GetUtcOffset(local));
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
