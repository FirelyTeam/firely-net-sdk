/*
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable
namespace Hl7.Fhir.ElementModel.Types;

public interface ICqlEquatable
{
    /// <summary>
    /// Whether one instance of Any is equal to another instance of Any according to CQL equality rule for that type.
    /// </summary>
    bool? IsEqualTo(Any? other);

    /// <summary>
    /// Whether one instance of Any is equivalent to another instance of Any according to CQL equivalence rule for that type.
    /// </summary>
    bool IsEquivalentTo(Any? other);
}