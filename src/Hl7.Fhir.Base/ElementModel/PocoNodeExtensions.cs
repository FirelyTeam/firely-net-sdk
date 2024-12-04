using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Hl7.Fhir.ElementModel;

#nullable enable

public static class PocoNodeExtensions
{
    private static bool TryResolveBundleEntry(this SinglePocoElementNode? node, string fullUrl, [NotNullWhen(true)] out SinglePocoElementNode? result)
    {
        result = node?.Poco is Bundle
            ? node
                .Child<RepeatingPocoElementNode>("entry")
                ?.FirstOrDefault<Bundle.EntryComponent>(entry =>
                    entry.FullUrl == fullUrl)
                ?.Child<SinglePocoElementNode>("resource")
            : null;
        return result is not null;
    }

    private static bool TryResolveContainedEntry(this SinglePocoElementNode? node, string? id, [NotNullWhen(true)] out SinglePocoElementNode? result)
    {
        result = node?.Poco is DomainResource
            ? node
                .Child<RepeatingPocoElementNode>("contained")
                ?.FirstOrDefault<Resource>(contained => $"#{contained.Id}" == id)
            : null;
        return result is not null;
    }
    
    /// <summary>
    /// Resolve a resource reference within the context of this node given a url (for bundles) or id (for contained).
    /// </summary>
    /// <param name="node">this node</param>
    /// <param name="url">The relative URL to resolve.</param>
    /// <param name="result">Contains the referenced instance, or null if the operation failed</param>
    /// <remarks>Does not create a copy. The resolved resource will be part of the IScopedNode-tree that was passed to this function</remarks>
    /// <returns>t</returns>
    internal static bool TryResolveLocalReference(this SinglePocoElementNode? node, string url, [NotNullWhen(true)] out SinglePocoElementNode? result)
    {
        for(var scan = node; scan is not null; scan = scan.Parent)
        {
            if (scan.Poco is Bundle) // if we do not find it in the closest bundle, the reference is invalid
            {
                return scan.TryResolveBundleEntry(url, out result);
            }
            
            if (scan.Poco is DomainResource && scan.TryResolveContainedEntry(url, out result)) 
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

    private static IEnumerable<SinglePocoElementNode> parents(this PocoElementNode2 node)
    {
        for(var scan = node.Parent; scan is not null; scan = scan.Parent)
        {
            yield return scan;
        }
    }
    
    private static SinglePocoElementNode? getContainer(this PocoElementNode2 node)
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
    public static SinglePocoElementNode? Resolve(this SinglePocoElementNode? node, Func<string, SinglePocoElementNode>? externalResolver = null)
    {
        if (node is null) return null;
        
        string? url = node.Poco switch
        {
            Canonical c => c.Value, // canonicals can be references
            ResourceReference r => r.Reference,
            _ => throw new ArgumentException($"Error occurred during reference resolution: Parameter {nameof(node)} is not a reference.")
        };

        return url is null ? null : Resolve(node, url, externalResolver);
    }
    
    public static SinglePocoElementNode? Resolve(this SinglePocoElementNode? node, string url, Func<string, SinglePocoElementNode?>? externalResolver = null)
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
    internal static SinglePocoElementNode? GetResourceContext(this PocoElementNode2? node) => node switch
    {
        RepeatingPocoElementNode { Parent: null } => null, // if parent is null, do not go further. If we are repeating and we don't have a parent, something went seriously wrong
        SinglePocoElementNode { Parent: null } single => single,
        SinglePocoElementNode { Poco: Resource } single => single, // if resource, return itself
        _ => node?.Parent?.GetResourceContext() // otherwise, go to parent
    };
    
    /// <summary>
    /// Extract the %rootResource variable from this IScopedNode
    /// </summary>
    internal static SinglePocoElementNode? GetRootResourceContext(this PocoElementNode2 node) => node.GetResourceContext() switch
    {
        { Name : "contained" } containedResource => containedResource.Parent!, // if contained, return container
        { } resource => resource, // otherwise return %resource
        _ => null
    };

    /// <summary>
    /// Find the fullUrl of the bundle entry that contains this node
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    internal static string? FindFullUrl(this PocoElementNode2 node)
    {
        var entry = node.parents().FirstOrDefault(n => n.Poco is Bundle.EntryComponent);
    }
    
    /// <summary>
    /// Turn a relative reference into an absolute url, based on the fullUrl of the parent resource
    /// </summary>
    /// <remarks>See https://www.hl7.org/fhir/bundle.html#references for more information</remarks>
    internal static ResourceIdentity MakeAbsolute(this SinglePocoElementNode node, ResourceIdentity identity)
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
    
    public static string MakeAbsolute(this SinglePocoElementNode node, string reference) =>
        node.MakeAbsolute(new ResourceIdentity(reference)).ToString();

    internal static SinglePocoElementNode? GetParentResource(this PocoElementNode2 node) => node.parents().FirstOrDefault(parentNode => parentNode is { Poco: Resource });

    internal static string GetLocalLocation(this IScopedNode node) =>
        node.Parent is null 
            ? node.Location 
            : $"{node.GetParentResource()!.InstanceType}.{node.Location[(node.GetParentResource()!.Location.Length + 1)..]}";

    public static IEnumerable<IScopedNode> ContainedResources(this IScopedNode node) => node.Children("contained");
    
    public static IEnumerable<IScopedNode> BundledResources(this IScopedNode node) => node.Children("entry");
}