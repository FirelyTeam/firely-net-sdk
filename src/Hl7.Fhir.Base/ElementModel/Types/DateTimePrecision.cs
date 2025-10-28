/*
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

namespace Hl7.Fhir.ElementModel.Types;

/// <summary>
/// An indication of the maximal precision of a date or time value.
/// </summary>
/// <remarks>If e.g. a date is known to be accurate to the day, but not to the hour, the precision is Day.</remarks>
public enum DateTimePrecision
{
    Year,
    Month,
    Day,
    Hour,
    Minute,
    Second,

    /// <summary>
    /// Milliseconds and fractions
    /// </summary>
    Fraction
}