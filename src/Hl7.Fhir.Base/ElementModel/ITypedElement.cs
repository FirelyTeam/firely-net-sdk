/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Specification;
using System.Collections.Generic;

#nullable enable


namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// A element within a tree of typed FHIR data.
    /// </summary>
    /// <remarks>
    /// This interface represents FHIR data as a tree of elements, including type information either present in 
    /// the instance or derived from fully aware of the FHIR definitions and types
    /// </remarks>

#pragma warning disable CS0618 // Type or member is obsolete
    public interface ITypedElement
#pragma warning restore CS0618 // Type or member is obsolete
    {
        /// <summary>
        /// Enumerate the child nodes present in the source representation (if any)
        /// </summary>
        /// <param name="name">Return only the children with the given name.</param>
        /// <returns></returns>
        IEnumerable<ITypedElement> Children(string? name = null);

        /// <summary>
        /// Name of the node, e.g. "active", "value".
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Type of the node. If a FHIR type, this is just a simple string, otherwise a StructureDefinition url for a type defined as a logical model.
        /// </summary>
        /// <remarks>May be <c>null</c> if the type is not known. This is uncommon when the data is valid,
        /// but might happen when working with unknown elements or unknown choice types in an instance.</remarks>
        string? InstanceType { get; }

        /// <summary>
        /// The value of the node (if it represents a primitive FHIR value)
        /// </summary>
        /// <remarks>
        /// FHIR primitives are mapped to underlying C# types as follows:
        ///
        /// <list type="table">
        ///   <listheader>
        ///      <term>InstanceType</term>
        ///      <description>Value</description>
        ///   </listheader>
        ///   <item>
        ///      <term>instant</term>
        ///      <description>Hl7.Fhir.ElementModel.Types.DateTime</description>
        ///   </item>
        ///   <item>
        ///      <term>time</term>
        ///      <description>Hl7.Fhir.ElementModel.Types.Time</description>
        ///   </item>
        ///   <item>
        ///      <term>date</term>
        ///      <description>Hl7.Fhir.ElementModel.Types.Date</description>
        ///   </item>
        ///   <item>
        ///      <term>dateTime</term>
        ///      <description>Hl7.Fhir.ElementModel.Types.DateTime</description>
        ///   </item>
        ///   <item>
        ///      <term>decimal</term>
        ///      <description>decimal</description>
        ///   </item>
        ///   <item>
        ///      <term>boolean</term>
        ///      <description>bool</description>
        ///   </item>
        ///   <item>
        ///      <term>integer</term>
        ///      <description>int</description>
        ///   </item>
        ///   <item>
        ///      <term>unsignedInt</term>
        ///      <description>int</description>
        ///   </item>
        ///   <item>
        ///      <term>positiveInt</term>
        ///      <description>int</description>
        ///   </item>
        ///   <item>
        ///      <term>integer64</term>
        ///      <description>long</description>
        ///   </item>
        ///   <item>
        ///      <term>string</term>
        ///      <description>string</description>
        ///   </item>
        ///   <item>
        ///      <term>code</term>
        ///      <description>string</description>
        ///   </item>
        ///   <item>
        ///      <term>id</term>
        ///      <description>string</description>
        ///   </item>
        ///   <item>
        ///      <term>uri, oid, uuid, canonical, url</term>
        ///      <description>string</description>
        ///   </item>
        ///   <item>
        ///      <term>markdown</term>
        ///      <description>string</description>
        ///   </item>
        ///   <item>
        ///      <term>base64Binary</term>
        ///      <description>string (uuencoded)</description>
        ///   </item>
        ///   <item>
        ///      <term>xhtml</term>
        ///      <description>string</description>
        ///   </item>
        /// </list>
        /// </remarks>
        object? Value { get; }

        /// <summary>
        /// An indication of the location of this node within the data represented by the <c>ITypedElement</c>.
        /// </summary>
        /// <remarks>The format of the location is the dotted name of the property, including indices to make
        /// sure repeated occurrences of an element can be distinguished. It needs to be sufficiently precise to aid 
        /// the user in locating issues in the data.</remarks>
        string Location { get; }

        IElementDefinitionSummary? Definition { get; }
    }
}