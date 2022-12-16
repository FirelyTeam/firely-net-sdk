/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Specification;
using System.Collections.Generic;

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// A element within a tree of typed FHIR data.
    /// </summary>
    /// <remarks>
    /// This interface represents FHIR data as a tree of elements, including type information either present in 
    /// the instance or derived from fully aware of the FHIR definitions and types
    /// </remarks>

    public interface ITypedElement
    {
        /// <summary>
        /// Enumerate the child nodes present in the source representation (if any)
        /// </summary>
        /// <param name="name">Return only the children with the given name.</param>
        /// <returns></returns>
        IEnumerable<ITypedElement> Children(string name = null);

        /// <summary>
        /// Name of the node, e.g. "active", "value".
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Type of the node. If a FHIR type, this is just a simple string, otherwise a StructureDefinition url for a type defined as a logical model.
        /// </summary>
        string InstanceType { get; }

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
        object Value { get; }

        /// <summary>
        /// An indication of the location of this node within the data represented by the <c>ITypedElement</c>.
        /// </summary>
        /// <remarks>The format of the location is the dotted name of the property, including indices to make
        /// sure repeated occurences of an element can be distinguished. It needs to be sufficiently precise to aid 
        /// the user in locating issues in the data.</remarks>
        string Location { get; }

        IElementDefinitionSummary Definition { get; }
    }
}