/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model;

/// <summary>
/// Bridges from the POCO <see cref="Period"/> to <see cref="P.DateTimeRange"/>. This lives on the POCO side,
/// so the core types never depend on the POCO model.
/// </summary>
public static class PeriodRangeExtensions
{
    /// <summary>
    /// Gets the range from the start of <see cref="Period.StartElement"/> to the end of <see cref="Period.EndElement"/>.
    /// A missing start or end leaves that side open. Fails when both are missing, either is invalid, or the end lies before the start.
    /// </summary>
    /// <remarks>
    /// This overload is only for periods whose bounds are full instants with an explicit offset, such as audit or
    /// provenance data. It also fails for any bound without an offset, which includes every date-only value
    /// (e.g. <c>"2026-03-15"</c>) and so most periods in practice. Use
    /// <see cref="TryGetRange(Period, TimeZoneInfo, out P.DateTimeRange?)"/> unless a missing offset must be rejected.
    /// </remarks>
    public static bool TryGetRange(this Period period, [NotNullWhen(true)] out P.DateTimeRange? range) =>
        tryGetRange(period, dt => dt.TryGetRange(out var r) ? r : null, out range);

    /// <summary>
    /// Gets the range from the start of <see cref="Period.StartElement"/> to the end of <see cref="Period.EndElement"/>,
    /// reading values without an offset in <paramref name="assumedZone"/>. A missing start or end leaves that side open.
    /// Fails when both are missing, either is invalid, or the end lies before the start.
    /// </summary>
    public static bool TryGetRange(this Period period, TimeZoneInfo assumedZone, [NotNullWhen(true)] out P.DateTimeRange? range) =>
        tryGetRange(period, dt => dt.GetRange(assumedZone), out range);

    private static bool tryGetRange(Period period, Func<P.DateTime, P.DateTimeRange?> toRange, out P.DateTimeRange? range)
    {
        range = null;
        if (!side(period.StartElement, out var start) || !side(period.EndElement, out var end)) return false;
        if (start is null && end is null) return false;
        if (start?.Start is { } s && end?.End is { } e && e <= s) return false; // end before start: not a usable period

        range = P.DateTimeRange.Between(start, end);
        return true;

        // A missing side is open; a present side must convert.
        bool side(FhirDateTime? element, out P.DateTimeRange? r)
        {
            r = null;
            if (element?.Value is null) return true;
            if (!element.TryToSystemDateTime(out var dt)) return false;
            r = toRange(dt);
            return r is not null;
        }
    }
}
