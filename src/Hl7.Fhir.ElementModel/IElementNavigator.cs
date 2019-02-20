/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// A type-aware navigator across a tree representing FHIR data, independent of serialization format or FHIR version.
    /// </summary>
    /// <remarks>
    /// Since this navigator associates type information with each (known) element, the element names are represented using
    /// their defined name (without type suffix) and the underlying raw value is parsed into a native .NET representation
    /// </remarks>
    [Obsolete("IElementNavigator should be replaced by the ITypedElement interface, which is returned by the parsers. Where an " +
        "IElementNavigator is still needed, ITypedElement can be turned into an IElementNavigator using the ToElementNavigator() " +
        "extension method.")]
    public interface IElementNavigator
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
        IElementNavigator Clone();

        /// <summary>
        /// Name of the node, e.g. "active", "value".
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Type of the node. If a FHIR type, this is just a simple string, otherwise a StructureDefinition url for a type defined as a logical model.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// The value of the node (if it represents a primitive FHIR value)
        /// </summary>
        /// <remarks>
        /// FHIR primitives are mapped to underlying C# types as follows:
        ///
        /// instant         Hl7.Fhir.Model.Primitive.PartialDateTime
        /// time            Hl7.Fhir.Model.Primitive.PartialTime
        /// date, dateTime  Hl7.Fhir.Model.Primitive.PartialDateTime
        /// decimal         decimal
        /// boolean         bool
        /// integer         long
        /// unsignedInt     long
        /// positiveInt     long
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
        /// An indication of the location of this node within the data represented by the navigator.
        /// </summary>
        /// <remarks>The format of the location is the dotted name of the property, including indices to make
        /// sure repeated occurences of an element can be distinguished. It needs to be sufficiently precise to aid 
        /// the user in locating issues in the data.</remarks>
        string Location { get; }
    }

}