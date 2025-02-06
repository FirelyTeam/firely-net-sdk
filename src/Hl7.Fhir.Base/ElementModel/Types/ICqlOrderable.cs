/*
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable
namespace Hl7.Fhir.ElementModel.Types;

public interface ICqlOrderable
{
    /// <summary>
    /// How one instance of Any compares to another according to CQL comparison logic for that type.
    /// </summary>
    int? CompareTo(Any? other);
}