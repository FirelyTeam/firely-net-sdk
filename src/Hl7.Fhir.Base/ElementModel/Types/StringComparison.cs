/*
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable
using System;

namespace Hl7.Fhir.ElementModel.Types;

/// <summary>Specifies the comparison rules for string.</summary>
/// <remarks>Options are aligned with the equality and equivalence  operations for string
/// defined in the CQL specification. See https://cql.hl7.org/09-b-cqlreference.html#comparison-operators-4
/// for more details.
/// </remarks>
[Flags]
public enum StringComparison
{
    /// <summary>
    /// Both strings are the same based on the Unicode values for the individual
    /// characters in the strings.
    /// </summary>
    Unicode = 0,

    /// <summary>
    /// Ignore casing when comparing strings
    /// </summary>
    IgnoreCase = 1,

    /// <summary>
    /// All whitespace characters are treated as equivalent.
    /// </summary>
    NormalizeWhitespace = 2,

    /// <summary>
    /// Ignore all Unicode non-spacing characters when comparing string
    /// </summary>
    IgnoreDiacritics = 4
}