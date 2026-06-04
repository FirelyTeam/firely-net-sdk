/*
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable
namespace Hl7.Fhir.ElementModel.Types;

/// <summary>
/// UCUM does not contain codes for calendar units. To support both the UCUM 'a' and 'mo' and
/// the calender year and month, we keep track of multiple coding system for units.
/// </summary>
public enum QuantityUnitSystem
{
    /// <summary>
    /// Unit is taken from the UCUM coding system (default).
    /// </summary>
    UCUM,

    /// <summary>
    /// Unit is taken from the set of calendar units (year or month)
    /// </summary>
    CalendarDuration,

    /// <summary>
    /// Unit is not specified to be part of any coding system.
    /// </summary>
    Unknown
}