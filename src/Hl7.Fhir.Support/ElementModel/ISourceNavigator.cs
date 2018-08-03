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
    public interface ISourceNavigator
    {
        /// <summary>
        /// Move to the next sibling of the current element.
        /// </summary>
        /// <returns>false when there is no next sibling, true otherwise.</returns>
        /// <remarks>Elements with the same name will always be direct siblings.</remarks>
        bool MoveToNext(string nameFilter = null);

        /// <summary>
        /// Move to the first child of the current element.
        /// </summary>
        /// <param name="nameFilter">
        /// If the value is provided, then only elements that have this value for the name should
        /// be considered by the navigator (during MoveNext())
        /// </param>
        /// <returns>false if the element has no children, true otherwise</returns>
        bool MoveToFirstChild(string nameFilter = null);

        /// <summary>
        /// Clone the current navigator
        /// </summary>
        /// <returns>A navigator that is positioned at the same location as the cloned navigator.</returns>
        ISourceNavigator Clone();

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
    }
}