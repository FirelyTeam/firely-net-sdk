/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System.Collections.Generic;

namespace Hl7.FhirPath.Analysis;

/// <summary>
/// Describes what is statically known about the environment an expression will be evaluated in, for use by
/// <see cref="FhirPathAnalyzer"/>. Every piece of information is optional: anything left out is treated as
/// "unknown", which never leads to an error.
/// </summary>
/// <remarks>Type names are FHIR type names (e.g. <c>Patient</c>, <c>Period</c>, <c>dateTime</c>), optionally
/// qualified (<c>FHIR.Period</c>), or FHIRPath System types qualified with <c>System.</c> (e.g. <c>System.String</c>).</remarks>
public class FhirPathAnalysisContext
{
    /// <summary>
    /// The possible types of the items in the input collection (the initial <c>$this</c>), or <c>null</c> if unknown.
    /// </summary>
    public IReadOnlyCollection<string>? InputTypes { get; set; }

    /// <summary>
    /// The possible types of <c>%resource</c>, or <c>null</c> if unknown.
    /// </summary>
    public IReadOnlyCollection<string>? ResourceTypes { get; set; }

    /// <summary>
    /// The possible types of <c>%rootResource</c>, or <c>null</c> to use <see cref="ResourceTypes"/>.
    /// </summary>
    public IReadOnlyCollection<string>? RootResourceTypes { get; set; }

    /// <summary>
    /// The possible types of other environment variables, by name (without the leading <c>%</c>).
    /// Variables not listed here have an unknown type.
    /// </summary>
    public IDictionary<string, IReadOnlyCollection<string>> Variables { get; } = new Dictionary<string, IReadOnlyCollection<string>>();

    /// <summary>
    /// Whether to report the use of functions that depend on the order of a collection (such as <c>first()</c>,
    /// <c>skip()</c> or an indexer) on a collection that has no defined order (the result of <c>children()</c> or
    /// <c>descendants()</c>). Off by default.
    /// </summary>
    public bool CheckOrderedFunctions { get; set; }
}
