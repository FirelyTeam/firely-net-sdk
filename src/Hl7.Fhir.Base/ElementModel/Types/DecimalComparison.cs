/*
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

namespace Hl7.Fhir.ElementModel.Types;

/// <summary>Specifies the comparison rules for decimals.</summary>
/// <remarks>Options are aligned with the equality and equivalence  operations for decimals
/// defined in the CQL specification. See https://cql.hl7.org/09-b-cqlreference.html#comparison-operators-4
/// for more details.
/// </remarks>
public enum DecimalComparison
{
    Strict,

    /// <summary>
    /// Trailing zeroes after the decimal are ignored in determining precision.
    /// </summary>
    IgnoreTrailingZeroes,

    /// <summary>
    /// Comparison is done on values rounded to the scale of the
    /// least precise operand. Implies <see cref="DecimalComparison.IgnoreTrailingZeroes" />.
    /// </summary>
    RoundToSmallestScale
}