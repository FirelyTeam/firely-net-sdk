using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace Hl7.Fhir.Model;

#nullable enable

[Flags]
public enum NodeType
{
    Resource = 1,
    Bundle = 1 << 1,
    DomainResource = 1 << 2,
    Primitive = 1 << 3,
    Reference = 1 << 4,
}

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
    /// A flag enum indicating the type of the node.
    /// </summary>
    NodeType Type { get; }

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

    /// <summary>
    /// Resolves a reference from this node to another resource. This node should be a Bundle.
    /// </summary>
    /// <param name="fullUrl"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool TryResolveBundleEntry(string fullUrl, [NotNullWhen(true)] out IScopedNode? result);
    
    /// <summary>
    /// Resolves a reference from this node to another resource. This node should be a DomainResource.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool TryResolveContainedEntry(string id, [NotNullWhen(true)] out IScopedNode? result);

    /// <summary>
    /// Resolve a resource reference within the context of this node given a url (for bundles) or id (for contained).
    /// </summary>
    /// <param name="url">The relative URL to resolve.</param>
    /// <param name="result">Contains the referenced instance, or null if the operation failed</param>
    /// <remarks>Does not create a copy. The resolved resource will be part of the IScopedNode-tree that was passed to this function</remarks>
    /// <returns>t</returns>
    public bool TryResolveLocalReference(string url, [NotNullWhen(true)] out IScopedNode? result)
    {
        for(var scan = this; scan is not null; scan = scan.Parent)
        {
            if (scan.Type.HasFlag(NodeType.Bundle)) // if we do not find it in the closest bundle, the reference is invalid
            {
                return scan.TryResolveBundleEntry(url, out result);
            }
            
            if (scan.Type.HasFlag(NodeType.DomainResource) && scan.TryResolveContainedEntry(url, out result)) 
            {
                // if we encounter a DomainResource, try to resolve the contained reference.
                // If it fails, higher domain resources could still contain it!
                return true;
            }

            if (scan.Children("id").FirstOrDefault()?.Value as string == url[1..])
            {
                // if we encounter a resource with the correct id, return it
                result = scan;
                return true;
            }
        }
        
        result = null;
        return false;
    }
}