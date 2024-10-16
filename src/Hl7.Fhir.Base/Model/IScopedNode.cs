using Hl7.Fhir.ElementModel;
using System.Collections.Generic;

namespace Hl7.Fhir.Model;

#nullable enable

/// <summary>
/// An element within a tree of typed FHIR data with also a parent element.
/// </summary>
/// <remarks>
/// This interface represents FHIR data as a tree of elements, including type information either present in
/// the instance or derived from fully aware of the FHIR definitions and types
/// </remarks>
#pragma warning disable CS0618 // Type or member is obsolete
public interface IScopedNode : ITypedElement, IShortPathGenerator
#pragma warning restore CS0618 // Type or member is obsolete
{
    /// <summary>
    /// The parent node of this node, or null if this is the root node.
    /// </summary>
    IScopedNode? Parent { get; }

    /// <summary>
    /// Enumerates the children of this instance. If name is given, only the children with that name are returned.
    /// </summary>
    /// <param name="name">If given, specifies the children to return based on this parameter.</param>
    /// <returns>An IEnumerable containing the children of this instance, including scope data.</returns>
    new IEnumerable<IScopedNode> Children(string? name = null);
    
    /// <summary>
    /// Name of the node, e.g. "active", "value".
    /// </summary>
    new string Name { get; }

    /// <summary>
    /// Type of the node. If a FHIR type, this is just a simple string, otherwise a StructureDefinition url for a type defined as a logical model.
    /// </summary>
    new string? InstanceType { get; }

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
    new object? Value { get; }

    /// <summary>
    /// An indication of the location of this node within the data represented by the <c>ITypedElement</c>.
    /// </summary>
    /// <remarks>The format of the location is the dotted name of the property, including indices to make
    /// sure repeated occurrences of an element can be distinguished. It needs to be sufficiently precise to aid 
    /// the user in locating issues in the data.</remarks>
    new string Location { get; }
}