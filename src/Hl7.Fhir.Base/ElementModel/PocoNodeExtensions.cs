using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;

namespace Hl7.Fhir.ElementModel;

#nullable enable

public static class PocoNodeExtensions
{
    private static bool tryResolveBundleEntry(this PocoNode? node, string fullUrl, [NotNullWhen(true)] out PocoNode? result)
    {
        result = node?.Poco is Bundle
            ? node
                .Child<PocoListNode>("entry")
                ?.FirstOrDefault<Bundle.EntryComponent>(entry =>
                    entry.FullUrl == fullUrl)
                ?.Child<PocoNode>("resource")
            : null;
        return result is not null;
    }

    private static bool tryResolveContainedEntry(this PocoNode? node, string? id, [NotNullWhen(true)] out PocoNode? result)
    {
        result = node?.Poco is DomainResource
            ? node
                .Child<PocoListNode>("contained")
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
    /// <remarks>Does not create a copy. The resolved resource will be part of the PocoNode-tree that was passed to this function</remarks>
    /// <returns>t</returns>
    internal static bool TryResolveLocalReference(this PocoNode? node, string url, [NotNullWhen(true)] out PocoNode? result)
    {
        for(var scan = node; scan is not null; scan = scan.Parent)
        {
            if (scan.Poco is Bundle) // if we do not find it in the closest bundle, the reference is invalid
            {
                return scan.tryResolveBundleEntry(url, out result);
            }
            
            if (scan.Poco is DomainResource && scan.tryResolveContainedEntry(url, out result)) 
            {
                // if we encounter a DomainResource, try to resolve the contained reference.
                // If it fails, higher domain resources could still contain it!
                return true;
            }

            if (scan.Child<PrimitiveNode>("id")?.Value as string == url[1..])
            {
                // if we encounter a resource with the correct id, return it
                result = scan;
                return true;
            }
        }
        
        result = null;
        return false;
    }

    private static IEnumerable<PocoNode> parents(this PocoNodeOrList node)
    {
        for(var scan = node.Parent; scan is not null; scan = scan.Parent)
        {
            yield return scan;
        }
    }
    
    private static PocoNode? getContainer(this PocoNodeOrList node)
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
    public static PocoNode? Resolve(this PocoNode? node, Func<string, PocoNode>? externalResolver = null)
    {
        if (node is null) return null;
        
        string? url = node.Poco switch
        {
            Canonical c => c.Value, // canonicals can be references
            ResourceReference r => r.Reference,
            PrimitiveType {ObjectValue: string s} => s,
            _ => throw new ArgumentException($"Error occurred during reference resolution: Parameter {nameof(node)} is not a reference.")
        };

        return url is null ? null : Resolve(node, url, externalResolver);
    }
    
    public static PocoNode? Resolve(this PocoNode? node, string url, Func<string, PocoNode?>? externalResolver = null)
    {
        if (node is null) return null;
        
        if(url == "#") return node.getContainer();
                
        var identity = node.MakeAbsolute(new ResourceIdentity(url));
        if (node.TryResolveLocalReference(identity.ToString(), out var localResult)) return localResult;

        return externalResolver?.Invoke(url);
    }
    
    /// <summary>
    /// Extract the %resource variable from this PocoNode
    /// </summary> 
    internal static PocoNode? GetResourceContext(this PocoNodeOrList? node) => node switch
    {
        PocoListNode { Parent: null } => null, // if parent is null, do not go further. If we are repeating and we don't have a parent, something went seriously wrong
        PocoNode { Parent: null } single => single,
        PocoNode { Poco: Resource } single => single, // if resource, return itself
        _ => node?.Parent?.GetResourceContext() // otherwise, go to parent
    };
    
    /// <summary>
    /// Extract the %rootResource variable from this PocoNode
    /// </summary>
    internal static PocoNode? GetRootResourceContext(this PocoNodeOrList node) => node.GetResourceContext() switch
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
    internal static string? FindFullUrl(this PocoNodeOrList node)
    {
        var entry = node.parents().FirstOrDefault(n => n.Poco is Bundle.EntryComponent);
        return entry?.Child<PrimitiveNode>("fullUrl")?.Value as string;
    }
    
    /// <summary>
    /// Turn a relative reference into an absolute url, based on the fullUrl of the parent resource
    /// </summary>
    /// <remarks>See https://www.hl7.org/fhir/bundle.html#references for more information</remarks>
    internal static ResourceIdentity MakeAbsolute(this PocoNode node, ResourceIdentity identity)
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
    
    public static string MakeAbsolute(this PocoNode node, string reference) =>
        node.MakeAbsolute(new ResourceIdentity(reference)).ToString();

    internal static PocoNode? GetParentResource(this PocoNodeOrList node) => node.parents().FirstOrDefault(parentNode => parentNode is { Poco: Resource });

    internal static string GetLocation(this PocoNode node) => ((ITypedElement)node).Location;
    
    internal static string GetLocalLocation(this PocoNode node) =>
        node.Parent is null 
            ? node.GetLocation()
            : $"{((IResourceTypeSupplier)node.GetParentResource()!).ResourceType}.{node.GetLocation()[(node.GetParentResource()!.GetLocation().Length + 1)..]}";

    public static IEnumerable<PocoNode> ContainedResources(this PocoNode node) => node.Child("contained") ?? Enumerable.Empty<PocoNode>();
    
    public static IEnumerable<PocoNode> BundledResources(this PocoNode node) => node.Child("entry") ?? Enumerable.Empty<PocoNode>();

    public static object? GetValue(this PocoNode node) => ((ITypedElement)node).Value;

    public static IEnumerable<PocoNode> FindSubChildren(this IEnumerable<PocoNode> nodes, string name) => nodes.SelectMany(node => node.Child(name) ?? Enumerable.Empty<PocoNode>());
    
    public static IEnumerable<PocoNode> Descendants(this IEnumerable<PocoNode> nodes) => nodes.SelectMany(descendants);

    public static IEnumerable<PocoNode> DescendantsAndSelf(this IEnumerable<PocoNode> nodes) => nodes.Descendants().Concat(nodes);

    private static IEnumerable<PocoNode> descendants(this PocoNode node) => node.Children().SelectMany(singleOrList => singleOrList).DescendantsAndSelf();
    
    public static T? Child<T>(this PocoNode? node, string name) where T : PocoNodeOrList => node?.Child(name) as T;
    
    public static IEnumerable<PocoNode> Where<T>(this IEnumerable<PocoNode> nodes, Func<T, bool> predicate) where T : Base =>
        nodes is PocoListNode pln 
            ? pln.Where(predicate)
            : nodes.Where(n => n.Poco is T t && predicate(t));

    internal static IEnumerable<PocoNode> Where<T>(this PocoListNode pln, Func<T, bool> predicate) where T : Base =>
        pln.Pocos.OfType<T>().Where(predicate).Select((poco, index) => new PocoNode(poco, pln.Parent, index, pln.Name));

    public static PocoNode? FirstOrDefault<T>(this IEnumerable<PocoNode> node, Func<T, bool> predicate) where T : Base =>
        node.FirstOrDefault(n => n.Poco is T t && predicate(t));
    
    internal static PocoNode? FirstOrDefault<T>(this PocoListNode pln, Func<T, bool> predicate) where T : Base
    {
        for (int index = 0; index < pln.Pocos.Count; index++)
        {
            if (pln.Pocos[index] is T item && predicate(item))
                return new PocoNode(item, pln.Parent, index, pln.Name);
        }

        return null;
    }
}