using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model;

#nullable enable

public static class ScopedNodeExtensions
{
    // wrote this, but it never gets picked over the ElementNodeExtensions version which is a shame. Let's keep it here for now.
    public static IEnumerable<IScopedNode> Children(this IEnumerable<IScopedNode> node, string? name = null) =>
        node.SelectMany(n => n.Children(name));
    
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
    /// <param name="node">A node representing a reference</param>
    /// <param name="externalResolver">An external resolver</param>
    /// <returns></returns>
    public static IScopedNode? Resolve(this IScopedNode? node, Func<string, IScopedNode?>? externalResolver = null)
    {
        if (node is null) return null;
        
        string? url = node switch
        {
            { Value: string s } => s, // canonicals can be references
            { Type: NodeType.Reference } => node.ParseResourceReference().Reference,
            _ => throw new ArgumentException($"Error occurred during reference resolution: Parameter {nameof(node)} is not a reference.")
        };

        return url is null ? null : Resolve(node, url, externalResolver);
    }
    
    public static IScopedNode? Resolve(this IScopedNode? node, string url, Func<string, IScopedNode?>? externalResolver = null)
    {
        if (node is null) return null;
        
        if(url == "#") return node.getContainer();
                
        var identity = node.MakeAbsolute(new ResourceIdentity(url));
        if (node.TryResolveLocalReference(identity.ToString(), out var localResult)) return localResult;

        return externalResolver?.Invoke(url);
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