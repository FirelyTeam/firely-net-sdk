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

public static class ScopedNodeHelpers
{
    private static IScopedNode? getContainer(this IScopedNode node)
    {
        var scan = node;
        while(scan is not (null or { Name: "contained" }))
        {
            scan = scan.Parent; // navigate up to "contained"
        }

        return scan?.Parent; // return the container (DomainResource around contained)
    }

    /// <summary>
    /// Resolve the given reference within the context of the given node. This node should be of type reference.
    /// </summary>
    /// <param name="maybeScopedNode">A node representing a reference</param>
    /// <param name="externalResolver">An external resolver</param>
    /// <returns></returns>
    /// TODO: This method should not be generic! It should work on IScopedNode ONLY! this is just for testing
    public static T? Resolve<T>(this T? maybeScopedNode, Func<string, T?>? externalResolver = null) where T : ITypedElement
    {
        if (maybeScopedNode is null) return (T?)(object?)null;
        
        if (maybeScopedNode is IScopedNode node)
        {
            string? url = node switch
            {
                { Value: string s } => s, // canonicals can be references
                { Type: NodeType.Reference } => node.ParseResourceReference().Reference,
                _ => throw new ArgumentException($"Error occurred during reference resolution: Parameter {nameof(node)} is not a reference.")
            };

            if (url is null) return (T?)(object?)null;
            
            return Resolve(maybeScopedNode, url, externalResolver);
        }
        
        return externalResolver is null ? (T?)(object?)null : externalResolver((maybeScopedNode.Value as string)!);
    }
    
    public static T? Resolve<T>(this T? maybeScopedNode, string url, Func<string, T?>? externalResolver = null) where T : ITypedElement
    {
        if (maybeScopedNode is null) return (T?)(object?)null;
        
        if (maybeScopedNode is IScopedNode node)
        {
            if(url == "#") return (T?)node.getContainer();
                    
            var identity = node.MakeAbsolute(new ResourceIdentity(url));

            if (node.TryResolveLocalReference(identity.ToString(), out var localResult)) return (T?)localResult;
        }

        return externalResolver is null ? (T?)(object?)null : externalResolver(url);
    }
    
    /// <summary>
    /// Extract the %resource variable from this IScopedNode
    /// </summary> 
    internal static IScopedNode GetResourceContext(this IScopedNode node) => node switch
    {
        { Parent: null } => node, // if parent is null, do not go further
        { Parent: { } p } when p.Type.HasFlag(NodeType.Bundle) => node, // if parent is bundle, do not go further
        { Type: var type } when type.HasFlag(NodeType.Resource) => node, // if resource, return itself
        _ => node.Parent!.GetResourceContext() // otherwise, go to parent
    };
    
    /// <summary>
    /// Extract the %rootResource variable from this IScopedNode
    /// </summary>
    internal static IScopedNode GetRootResourceContext(this IScopedNode node) => node.GetResourceContext() switch
    {
        { Name : "contained" } containedResource => containedResource.Parent!, // if contained, return container
        { } resource => resource // otherwise return %resource
    };

    internal static string? FindFullUrl(this IScopedNode node)
    {
        if(node.Name == "entry") return node.Children("fullUrl").FirstOrDefault()?.Value as string;
        
        return node.Parent?.FindFullUrl();
    }
    
    /// <summary>
    /// Turn a relative reference into an absolute url, based on the fullUrl of the parent resource
    /// </summary>
    /// <remarks>See https://www.hl7.org/fhir/bundle.html#references for more information</remarks>
    internal static ResourceIdentity MakeAbsolute(this IScopedNode node, ResourceIdentity identity)
    {
        if (!identity.IsRelativeRestUrl) return identity;
        // Relocate the relative url on the base given in the fullUrl of the entry (if applicable)
        var fullUrl = node.FindFullUrl();

        if (fullUrl == null) return identity;
            
        var parentIdentity = new ResourceIdentity(fullUrl);

        if (parentIdentity.IsAbsoluteRestUrl)
            identity = identity.WithBase(parentIdentity.BaseUri);
        else if (parentIdentity.IsUrn)
            identity = new ResourceIdentity($"{parentIdentity}/{identity.Id}");

        // Return the identity - will remain relative if we did not find a fullUrl              

        return identity;
    }
    
    public static string MakeAbsolute(this ScopedNode node, string reference) =>
        node.MakeAbsolute(new ResourceIdentity(reference)).ToString();
}