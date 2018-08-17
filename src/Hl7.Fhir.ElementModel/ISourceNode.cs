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
    /// A node within a tree of FHIR data.
    /// </summary>
    /// <remarks>
    /// <para>This interface is typically implemented by a parser for one of the low-level serialization formats for FHIR, i.e.
    /// FHIR xml/json/rdf or v3 XML. The interface does not depend on the availability of FHIR metadata and definitions
    /// (in contrast to <see cref="ITypedElement" />), so the names of the nodes will have their type suffixes (for choice types) 
    /// and all primitives values are represented as strings, instead of native objects.</para>
    /// <para>Implementations of this interface that want to report errors while parsing should only do so on the 
    /// <see cref="Children(string)"/> function and <see cref="Text"/> getter.</para>
    /// </remarks>
    public interface ISourceNode
    {
        /// <summary>
        /// Gets the name of the node, e.g. "active", "valueQuantity".
        /// </summary>
        /// <remarks>Since the node has no type information, choice elements are represented as their 
        /// name on the wire, possibly including the type suffix for choice elements.
        /// </remarks>
        string Name { get; }

        /// <summary>
        /// Gets the text of the primitive value of the node
        /// </summary>
        /// <value>Returns the raw textual value as represented in the serialization, or null if there is no value in this node.</value>
        string Text { get; }

        /// <summary>
        /// Gets the location of this node within the tree of data.
        /// </summary>
        /// <value>A string of dot-separated names representing the path to the node within the tree, including indices
        /// to distinguish repeated occurences of an element.</value>
        string Location { get; }

        /// <summary>
        /// Enumerates the direct child nodes of the current node (if any).
        /// </summary>
        /// <param name="name">Optional. The name filter for the children. Can be omitted to not filter by name.</param>
        /// <returns>The children of the node matching the given filter, or all children if no filter was specified.
        /// If no children match the given filter, the function returns an empty enumerable.</returns>
        /// <remarks>
        /// <para>If the <paramref name="name"/>parameter ends in an asterix ('*'),
        /// the function will return the children of which the name starts with the given name.</para>
        /// <para>Repeating elements will always be returned consecutively.</para></remarks>
        IEnumerable<ISourceNode> Children(string name = null);
    }
}