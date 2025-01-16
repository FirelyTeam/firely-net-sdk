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

/// <summary>Specifies the comparison rules for quantities.</summary>
/// <remarks>Options are aligned with the equality and equivalence  operations for quantities
/// defined in the CQL specification. See https://cql.hl7.org/09-b-cqlreference.html#comparison-operators-4
/// for more details.
/// </remarks>
[Flags]
public enum QuantityComparison
{
    None = 0,

    /// <summary>
    /// For time-valued quantities: calendar durations and definite quantity durations are considered comparable (and equivalent).
    /// </summary>
    CompareCalendarUnits = 1
}