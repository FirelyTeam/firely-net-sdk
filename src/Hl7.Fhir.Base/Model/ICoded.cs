/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model;

/// <summary>
/// Maps a FHIR datatype to a (list of) Coding, according to https://hl7.org/fhir/terminologies.html#4.1
/// </summary>
public interface ICoded
{
    /// <summary>
    /// Maps a FHIR datatype to a (list of) Coding, according to https://hl7.org/fhir/terminologies.html#4.1
    /// </summary>
    IReadOnlyCollection<Coding> ToCodings();
}

/// <summary>
/// Represents a resource that can be coded.
/// </summary>
/// <typeparam name="T">The type that is used to codify the resource, usually a (list of) <see cref="Coding"/> or <see cref="CodeableConcept"/>.</typeparam>
/// <remarks>This interface is primarily used in the context of CQL, where every resource is assigned an element that represents that
/// element as a code.</remarks>
public interface ICoded<T> : ICoded
{
    T Code { get; set; }
}


/// <summary>
/// Helper methods for working with coded types.
/// </summary>
public static class CodedExtensions
{
    /// <summary>
    /// Maps a list of FHIR datatypes to a list of <see cref="Coding"/>.
    /// </summary>
    public static IReadOnlyCollection<Coding> ToCodings(this IEnumerable<DataType>? dts) => dts?.SelectMany(dt => dt.ToCodings()).ToList() ?? [];

    /// <inheritdoc cref="ICoded.ToCodings()"/>
    public static IReadOnlyCollection<Coding> ToCodings(this DataType? dt) => dt switch
    {
        ICoded c => c.ToCodings(),
        _ => []
    };
}