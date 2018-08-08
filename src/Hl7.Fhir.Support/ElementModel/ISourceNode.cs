/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;
using System.Collections.Generic;

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// A navigator across a tree representing FHIR data, independent of serialization format or FHIR version.
    /// </summary>
    /// <remarks>
    /// This interface is typically implemented by a parser for one of the low-level serialization formats for FHIR, i.e.
    /// FHIR xml/json/rdf of v3 XML.  This interface assumes there is no type information available (in contrast to
    /// IElementNavigator), so the names of the nodes still may have their type suffixes (for choice types) and all 
    /// primitives values are represented as strings, instead of native objects.
    /// </remarks>
    public interface ISourceNode
    {
        /// <summary>
        /// Name of the node, e.g. "active", "valueQuantity".
        /// </summary>
        /// <remarks>Since the navigator has no type information, choice elements are represented as their 
        /// "raw" name on the wire.
        /// </remarks>
        string Name { get; }

        /// <summary>
        /// The raw text of the primitive value of the node (if it represents a primitive FHIR value)
        /// </summary>
        string Text { get; }

        /// <summary>
        /// An indication of the location of this node within the data represented by the navigator.
        /// </summary>
        /// <remarks>The format of the location is the dotted name of the property, including indices to make
        /// sure repeated occurences of an element can be distinguished. It needs to be sufficiently precise to aid 
        /// the user in locating issues in the data.</remarks>
        string Location { get; }

        /// <summary>
        /// Enumerate the child nodes present in the source representation (if any)
        /// </summary>
        /// <param name="name">Return only the children with the given name.</param>
        /// <returns></returns>
        /// <remarks>If the <paramref name="name"/> parameter ends in an asterix ('*'),
        /// the function will return the children of which the name starts with the given name</remarks>
        IEnumerable<ISourceNode> Children(string name = null);
    }
}