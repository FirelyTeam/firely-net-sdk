using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Model;

#nullable enable

public static class PocoNodeExtensions
{
    private static bool tryResolveBundleEntry(this PocoNode? node, ResourceIdentity identity, [NotNullWhen(true)] out PocoNode? result)
    {
        result = node?.Poco is Bundle b
            ? node
                .Child<PocoListNode>("entry")
                ?.FirstOrDefault<Bundle.EntryComponent>(entry => entry.Resource?.ResourceIdentity(fullUrl: entry.FullUrl)?.IsTargetOf(identity) is true)
                ?.Child<PocoNode>("resource")
            : null;
        return result is not null;
    }

    private static bool tryResolveContainedEntry(this PocoNode? node, ResourceIdentity identity, [NotNullWhen(true)] out PocoNode? result)
    {
        result = node?.Poco is DomainResource
            ? node
                .Child<PocoListNode>("contained")
                ?.FirstOrDefault<Resource>(contained => contained.ResourceIdentity().IsTargetOf(identity))
            : null;
        return result is not null;
    }
    
    /// <summary>
    /// Resolve a resource reference within the context of this node given a url (for bundles) or id (for contained).
    /// </summary>
    /// <param name="node">this node</param>
    /// <param name="identity">the identity to resolve</param>
    /// <param name="result">Contains the referenced instance, or null if the operation failed</param>
    /// <remarks>Does not create a copy. The resolved resource will be part of the PocoNode-tree that was passed to this function</remarks>
    /// <returns>t</returns>
    internal static bool TryResolveLocalReference(this PocoNode? node, ResourceIdentity identity, [NotNullWhen(true)] out PocoNode? result)
    {
        result = null;
        
        for(var scan = node; scan is not null; scan = scan.Parent)
        {
            if (scan.Poco is Bundle) // if we do not find it in the closest bundle, the reference is invalid
            {
                return !identity.IsLocal && scan.tryResolveBundleEntry(identity, out result);
            }
            
            if (scan.Poco is DomainResource && scan.tryResolveContainedEntry(identity, out result)) 
            {
                // if we encounter a DomainResource, try to resolve the contained reference.
                // If it fails, higher domain resources could still contain it!
                return true;
            }

            if (scan.Child<PrimitiveNode>("id")?.Value is string s && s == identity.Id && (identity.ResourceType is null || scan.Poco.TypeName == identity.ResourceType))
            {
                // if we encounter a resource with the correct id and type, return it
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
    /// <remarks>Does not create a copy. The resolved resource will be part of the PocoNode-tree that was passed to this function</remarks>
    public static PocoNode? Resolve(this PocoNode? node, Func<string, PocoNode?>? externalResolver = null)
    {
        if (node is null) return null;
        
        string? url = node.Poco switch
        {
            Canonical c => c.Value, // canonicals can be references
            ResourceReference r => r.Reference,
            PrimitiveType {JsonValue: string s} => s,
            _ => throw new ArgumentException($"Error occurred during reference resolution: Parameter {nameof(node)} is not a reference.")
        };

        return url is null ? null : Resolve(node, url, externalResolver);
    }
    
    /// <summary>
    /// Resolve the given url within the context of the given node
    /// </summary>
    /// <param name="node">The context for the reference resolution</param>
    /// <param name="url">The reference to be resolved</param>
    /// <param name="externalResolver"></param>
    /// <remarks>Does not create a copy. The resolved resource will be part of the PocoNode-tree that was passed to this function</remarks>
    /// <returns></returns>
    public static PocoNode? Resolve(this PocoNode? node, string url, Func<string, PocoNode?>? externalResolver = null)
    {
        if (node is null) return null;
        
        if(url == "#") return node.getContainer();
                
        var identity = node.MakeAbsolute(new ResourceIdentity(url));
        if (node.TryResolveLocalReference(identity, out var localResult)) return localResult;

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
    public static string? FindFullUrl(this PocoNodeOrList node)
    {
        var entry = node.parents()
            .Select(x => x.Poco)
            .OfType<Bundle.EntryComponent>()
            .FirstOrDefault();
        return entry?.FullUrl;
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
    
    /// <summary>
    /// Turn a relative reference into an absolute url, based on the fullUrl of the parent resource
    /// </summary>
    /// <remarks>See https://www.hl7.org/fhir/bundle.html#references for more information</remarks>
    public static string MakeAbsolute(this PocoNode node, string reference) =>
        node.MakeAbsolute(new ResourceIdentity(reference)).ToString();

    /// <summary>
    /// Gets the parent resource node of the specified PocoNodeOrList if it exists.
    /// </summary>
    /// <param name="node">The node for which the parent resource is to be identified.</param>
    /// <returns>The parent PocoNode that represents a resource, or null if no parent resource is found.</returns>
    public static PocoNode? GetParentResource(this PocoNodeOrList node) => node.parents().FirstOrDefault(parentNode => parentNode is { Poco: Resource });

    /// <summary>
    /// Gets the location of this node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public static string GetLocation(this PocoNode node) => ((ITypedElement)node).Location;

    /// <summary>
    /// Returns the common location of all nodes in the given collection. This only works if all nodes were originally constructed together as a collection.
    /// </summary>
    /// <param name="nodeList"></param>
    /// <returns></returns>
    public static string GetCommonLocation(this PocoListNode nodeList) =>
        nodeList.Parent?.GetLocation() + nodeList.Name;
    
    internal static string GetLocalLocation(this PocoNode node) =>
        node.Parent is null 
            ? node.GetLocation()
            : $"{((IResourceTypeSupplier)node.GetParentResource()!).ResourceType}.{node.GetLocation()[(node.GetParentResource()!.GetLocation().Length + 1)..]}";

    /// <summary>
    /// Returns the contained resources of a DomainResource node
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public static IEnumerable<PocoNode> ContainedResources(this PocoNode node) => node.Child("contained") ?? Enumerable.Empty<PocoNode>();
    
    /// <summary>
    /// Returns the bundle entries of a Bundle node
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public static IEnumerable<PocoNode> BundledResources(this PocoNode node) => node.Child("entry") ?? Enumerable.Empty<PocoNode>();
    
    /// <summary>
    /// Gets the "Legacy" ITypedElement.Value of a PocoNode without having to do an explicit cast. Uses a function signature to indicate its potential cost.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public static object? GetValue(this PocoNode node) => ((ITypedElement)node).Value;

    /// <summary>
    /// Searches all given nodes for the specified children and flattens the results.
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IEnumerable<PocoNode> FlatChildren(this IEnumerable<PocoNode> nodes, string name) => nodes.SelectMany(node => node.Child(name) ?? Enumerable.Empty<PocoNode>());
    
    /// <summary>
    /// Finds all descendants of this node and flattens the result.
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public static IEnumerable<PocoNode> Descendants(this IEnumerable<PocoNode> nodes) => nodes.SelectMany(descendants);

    /// <summary>
    /// Finds this node and all descendants of this node and flattens the result.
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public static IEnumerable<PocoNode> DescendantsAndSelf(this IEnumerable<PocoNode> nodes) => nodes.Descendants().Concat(nodes);

    private static IEnumerable<PocoNode> descendants(this PocoNode node) => node.Children().SelectMany(singleOrList => singleOrList).DescendantsAndSelf();
    
    internal static T? Child<T>(this PocoNode? node, string name) where T : PocoNodeOrList => node?.Child(name) as T;
    
    /// <summary>
    /// Filters a list of nodes based on a predicate to be evaluated on their internal POCOs.
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<PocoNode> Where<T>(this IEnumerable<PocoNode> nodes, Func<T, bool> predicate) where T : Base =>
        nodes is PocoListNode pln 
            ? pln.Where(predicate)
            : nodes.Where(n => n.Poco is T t && predicate(t));

    internal static IEnumerable<PocoNode> Where<T>(this PocoListNode pln, Func<T, bool> predicate) where T : Base =>
        pln.Pocos.OfType<T>().Where(predicate).Select((poco, index) => new PocoNode(poco, pln.Parent, index, pln.Name));

    /// <summary>
    /// Finds the first node in a list of nodes that satisfies a predicate to be evaluated on their internal POCOs.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static PocoNode? FirstOrDefault<T>(this IEnumerable<PocoNode> node, Func<T, bool> predicate) where T : Base =>
        node is PocoListNode pln
            ? pln.FirstOrDefault(predicate)
            : node.FirstOrDefault(n => n.Poco is T t && predicate(t));
    
    /// <summary>
    /// Navigates to a child node or set of child nodes using a path. The path is a string that can contain dot-separated names and array indices, much like navigation in FhirPath.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static IEnumerable<PocoNode> NavigateTo(this PocoNode node, string path)
    {
        var parts = path.Split(['.', '[', ']'], StringSplitOptions.RemoveEmptyEntries);

        return parts.Aggregate<string, IEnumerable<PocoNode>>(node, (current, part) =>
            int.TryParse(part, out var index)
                ? current is PocoListNode pln ? pln[index] : current.Skip(index).FirstOrDefault() ?? Enumerable.Empty<PocoNode>()
                : current.FlatChildren(part)
        );
    }
    
    internal static PocoNode? FirstOrDefault<T>(this PocoListNode pln, Func<T, bool> predicate) where T : Base
    {
        for (int index = 0; index < pln.Pocos.Count; index++)
        {
            if (pln.Pocos[index] is T item && predicate(item))
                return new PocoNode(item, pln.Parent, index, pln.Name);
        }

        return null;
    }
    
    internal static ModelInspector? FindInspector(this PocoNode node) => ((IAnnotated)node).Annotation<ModelInspector>() ?? node.Parent?.SingleOrDefault()?.FindInspector();

    /// <summary>
    /// Converts a PocoNode instance into an XML string representation.
    /// </summary>
    /// <param name="pn">The PocoNode instance to convert.</param>
    /// <param name="pretty">A boolean value indicating whether the XML output should be formatted prettily (indented) or compact.</param>
    /// <returns>A string containing the XML representation of the given PocoNode instance.</returns>
    public static string ToXml(this PocoNode pn, bool pretty = false)
    {
#pragma warning disable CS0618// Type or member is obsolete
        var serializer = new BaseFhirXmlSerializer(pn.FindInspector() ?? ModelInspector.ForAssembly(pn.Poco.GetType().Assembly));
#pragma warning restore CS0618 // Type or member is obsolete

        // If we are serializing a subtree of a resource, then if the current node is a datatype or a nested resource,
        // we need to pick a name for this root element.
        var pickElementName = pn.Poco is not Resource || pn.Parent is not null;
        var rootName = pickElementName ? pn.Name : null;

        return serializer.SerializeToString(pn.Poco, pretty, rootName: rootName);
    }

    /// <summary>
    /// Converts the specified PocoNode instance to its JSON representation.
    /// </summary>
    /// <param name="pn">The PocoNode instance to serialize.</param>
    /// <param name="pretty">Indicates whether the JSON output should be formatted for readability.</param>
    /// <returns>A JSON string representing the given PocoNode.</returns>
    public static string ToJson(this PocoNode pn, bool pretty = false)
    {
#pragma warning disable CS0618// Type or member is obsolete
        var inspector = pn.FindInspector() ?? ModelInspector.ForType(pn.Poco.GetType());
#pragma warning restore CS0618 // Type or member is obsolete
        
        var ser = new BaseFhirJsonSerializer(inspector);
        return ser.SerializeToString(pn.Poco, pretty);
    }
}