/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// A navigator across a tree representing FHIR data, independent of serialization format or FHIR version.
    /// </summary>
    /// <remarks>
    /// An implementation of this interface may be either type-aware or not, depending on whether it has access to FHIR type
    /// information. This influences the way the properties Name, Type and Value need to be interpreted. See each property for
    /// more details. 
    /// 
    /// <para>Initially, the navigator will be placed on a "root" which has the same name as the type of the instance data
    /// (often the name of the resource type, e.g. 'Patient'), but if the tree is a fragment it may be the name of a data type as well). Note
    /// that contained resources will have a node called "contained" as their root.</para>
    /// 
    /// <para>Since for most uses, Clone() will be called often, implementations should normally be a struct, not a class</para>
    /// </remarks>
    public interface IElementNavigator
    {
        /// <summary>
        /// Move to the next sibling of the current element.
        /// </summary>
        /// <returns>false when there is no next sibling, true otherwise.</returns>
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
        /// <remarks>Depending on whether the navigator has type information for this element, choice elements may be represented as their 
        /// "raw" name on the wire or not. e.g. it may be 'value' (with Type == "CodeableConcept") or 'valueCodeableConcept' (and Type == null).
        /// </remarks>
        string Name { get; }

        /// <summary>
        /// Type of the node. If a FHIR type, this is just a simple string, otherwise a StructureDefinition url for a type defined as a logical model.
        /// </summary>
        /// <remarks>This property will be null if the navigator has no type information for this element.
        /// </remarks>
        /// 
        string Type { get; }

        /// <summary>
        /// The value of the node (if it represents a primitive FHIR value)
        /// </summary>
        /// <remarks>If The underlying source has type information for this element, this property will have typed data (string, integer, etc),
        /// else this is a raw string from the FHIR wire representation.
        /// 
        /// <para>
        /// If the data is typed, FHIR primitives are mapped to underlying C# types as follows:
        ///
        /// instant         Hl7.Fhir.Model.Primitive.PartialDateTime
        /// time            Hl7.Fhir.Model.Primitive.PartialTime
        /// date, dateTime  Hl7.Fhir.Model.Primitive.PartialDateTime
        /// decimal         decimal
        /// boolean         bool
        /// integer         long
        /// unsignedInt     long
        /// positiveInt     long
        /// code            string
        /// uri, oid, id    string
        /// markdown        string
        /// base64Binary    string (uuencoded)
        /// </para>
        /// </remarks>
        object Value { get; }

        /// <summary>
        /// An indication of the location of this node within the data represented by the navigator.
        /// </summary>
        /// <remarks>The format of the location is dependent on the source represented by this interface, e.g. this might be an FhirPath-type location,
        /// a line/position indication, or the dotted name of a property in a POCO. It needs to be sufficiently precise to aid the user in
        /// locating issues in the data.</remarks>
        string Location { get; }
    }

}