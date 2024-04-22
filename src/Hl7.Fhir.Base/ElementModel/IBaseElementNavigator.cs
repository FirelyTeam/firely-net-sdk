/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections.Generic;

#nullable enable

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// The base interface for <see cref="ITypedElement"/>."/>
    /// </summary>
    /// <typeparam name="TDerived"></typeparam>
    [Obsolete("WARNING! Intended for internal API usage exclusively, this interface ideally should be kept internal. " +
        "However, due to its derivation by the public interface ITypedElement, maintaining its internal status is impossible.")]
    public interface IBaseElementNavigator<TDerived> where TDerived : IBaseElementNavigator<TDerived>
    {
        /// <summary>
        /// Enumerate the child nodes present in the source representation (if any)
        /// </summary>
        /// <param name="name">Return only the children with the given name.</param>
        /// <returns></returns>
        IEnumerable<TDerived> Children(string? name = null);

        /// <summary>
        /// Name of the node, e.g. "active", "value".
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Type of the node. If a FHIR type, this is just a simple string, otherwise a StructureDefinition url for a type defined as a logical model.
        /// </summary>
        string? InstanceType { get; }

        /// <summary>
        /// The value of the node (if it represents a primitive FHIR value)
        /// </summary>
        /// <remarks>
        /// FHIR primitives are mapped to underlying C# types as follows:
        ///
        /// instant         Hl7.Fhir.ElementModel.Types.DateTime
        /// time            Hl7.Fhir.ElementModel.Types.Time
        /// date            Hl7.Fhir.ElementModel.Types.Date
        /// dateTime        Hl7.Fhir.ElementModel.Types.DateTime
        /// decimal         decimal
        /// boolean         bool
        /// integer         int
        /// unsignedInt     int
        /// positiveInt     int
        /// long/integer64  long (name will be finalized in R5)
        /// string          string
        /// code            string
        /// id              string
        /// uri, oid, uuid, 
        /// canonical, url  string
        /// markdown        string
        /// base64Binary    string (uuencoded)
        /// xhtml           string
        /// </remarks>
        object? Value { get; }
    }
}

#nullable restore