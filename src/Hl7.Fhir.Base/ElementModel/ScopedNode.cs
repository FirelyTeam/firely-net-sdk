/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.ElementModel
{
    public class ScopedNode : ITypedElement, IShortPathGenerator, IAnnotated, IExceptionSource
    {
        private class Cache
        {
            public readonly object _lock = new();

            public string? Id;
            public ReferencedResourceCache? ContainedResources;
            public ReferencedResourceCache? BundledResources;

            public string? InstanceUri;
        }

        private readonly Cache _cache = new();

        public readonly ITypedElement Current;

        public ScopedNode(ITypedElement wrapped, string? instanceUri = null)
        {
            Current = wrapped;
            InstanceUri = instanceUri;

            if (Current is IExceptionSource ies && ies.ExceptionHandler == null)
                ies.ExceptionHandler = (o, a) => ExceptionHandler.NotifyOrThrow(o, a);
        }

        private ScopedNode(ScopedNode parentNode, ScopedNode? parentResource, ITypedElement wrapped, string? fullUrl)
        {
            Current = wrapped;
            ExceptionHandler = parentNode.ExceptionHandler;
            ParentResource = parentNode.AtResource ? parentNode : parentResource;
            Parent = parentNode;

            _fullUrl = fullUrl;

            if (Current.Name == "entry")
                _fullUrl = Current.Children("fullUrl").FirstOrDefault()?.Value as string ?? _fullUrl;
        }

        public ExceptionNotificationHandler? ExceptionHandler { get; set; }

        /// <summary>
        /// Represents the most direct resource parent in which the current node is located.
        /// </summary>
        /// <remarks>
        /// When the node is the inital root, there is no parent and this property returns null.
        /// When the node is a (contained/bundled) resource itself, this property still returns
        /// that resource's most direct parent.
        /// </remarks>
        public readonly ScopedNode? ParentResource;

        /// <summary>
        /// The resource or element which is the direct parent of this node.
        /// </summary>
        public ScopedNode? Parent { get; }

        /// <summary>
        /// Returns the location of the current element within its most direct parent resource or datatype.
        /// </summary>
        /// <remarks>
        /// For deeper paths, this would return the direct path within the encompassing type, e.g. 
        /// for an element at Patient.identifier.use this property would return Identifier.use.
        /// </remarks>
        public string LocalLocation => ParentResource == null ? Location :
                        $"{ParentResource.InstanceType}.{Location.Substring(ParentResource.Location.Length + 1)}";

        /// <inheritdoc/>
        public string Name => Current.Name;

        /// <inheritdoc/>
        public string? InstanceType => Current.InstanceType;

        /// <inheritdoc/>
        public object? Value => Current.Value;

        /// <inheritdoc/>
        public string Location => Current.Location;

        public bool TryResolveBundleEntry(string fullUrl, [NotNullWhen(true)] out ScopedNode? result)
            => (result = ((ReferencedResourceCache)this.BundledResources()).ResolveReference(fullUrl)) is not null;

        public bool TryResolveContainedEntry(string id, [NotNullWhen(true)] out ScopedNode? result) 
            => (result = (this.ContainedResourcesWithId()).ResolveReference(id)) is not null;

        /// <summary>
        /// Whether this node is a root element of a Resource.
        /// </summary>
        public bool AtResource => Current.Definition?.IsResource ?? Current is IResourceTypeSupplier rt && rt.ResourceType is not null;

        /// <summary>
        /// The instance type of the resource this element is part of.
        /// </summary>
        public string? NearestResourceType => ParentResource == null ? Location : ParentResource.InstanceType;

        /// <summary>
        /// The %resource context, as defined by FHIRPath
        /// </summary>
        /// <remarks>
        /// This is the original resource the current context is part of. When evaluating a datatype, 
        /// this would be the resource the element is part of. Do not go past a root resource into a bundle, 
        /// if it is contained in a bundle.
        /// </remarks>
        public ITypedElement ResourceContext
        {
            get
            {
                var scan = this;

                while (scan.ParentResource != null && scan.ParentResource.InstanceType != "Bundle")
                {
                    scan = scan.ParentResource;
                }

                return scan;
            }
        }

        /// <inheritdoc />
        public IElementDefinitionSummary? Definition => Current.Definition;

        /// <summary>
        /// Get the list of container parents in a list, nearest parent first.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ScopedNode> ParentResources()
        {
            var scan = ParentResource;

            while (scan != null)
            {
                yield return scan;

                scan = scan.ParentResource;
            }
        }

        /// <summary>
        /// Returns the Id of the resource, if the current node is a resource.
        /// </summary>
        /// <returns></returns>
        public string? Id()
        {
            if (_cache.Id == null)
            {
                _cache.Id = AtResource ? "#" + Current.Children("id").FirstOrDefault()?.Value : null;
            }

            return _cache.Id;
        }

        /// <summary>
        /// When this node is the node of a resource, it will return its contained resources, if any.
        /// </summary>
        public IEnumerable<ScopedNode> ContainedResources()
        {
            return getOrInitContainedCache().Resources;
        }
        
        internal ReferencedResourceCache ContainedResourcesWithId()
        {
            return getOrInitContainedCache();
        }

        private ReferencedResourceCache getOrInitContainedCache()
        {
            if (_cache.ContainedResources != null) return _cache.ContainedResources;
            
            if (AtResource)
            {
                var referenceEntryPairs = from contained in this.Children("contained")
                    let id = contained.Children("id").FirstOrDefault()?.Value as string
                    let resource = contained as ScopedNode
                    select new KeyValuePair<string, ScopedNode?>(id, resource);
                _cache.ContainedResources = new ReferencedResourceCache(referenceEntryPairs);
            }
            else
                _cache.ContainedResources = new ReferencedResourceCache([]);

            return _cache.ContainedResources;
        }

        /// <summary>
        /// A tuple of a bundled resource plus its Bundle.entry.fullUrl property.
        /// </summary>
        public class BundledResource()
        {
            public BundledResource(string? fullUrl, ScopedNode? resource) : this()
            {
                FullUrl = fullUrl;
                Resource = resource;
            }
            
            public string? FullUrl;
            public ScopedNode? Resource;
        }

        /// <summary>
        /// When this node is the root of a Bundle, retrieves the bundled resources in its Bundle.entry.
        /// </summary>
        public IEnumerable<BundledResource> BundledResources()
        {
            if (_cache.BundledResources != null) return _cache.BundledResources;
            
            if (InstanceType == "Bundle")
            {
                var referenceEntryPairs = new List<KeyValuePair<string?, ScopedNode>>();
                var versionedEntries = this.Children("entry").Where(entry => entry.Children("resource").Children("meta").Children("versionId").Any());
                foreach (var versionedResourceGroup in versionedEntries.GroupBy(entry => entry.Children("fullUrl").FirstOrDefault()?.Value as string))
                {
                    referenceEntryPairs.Add(new KeyValuePair<string?, ScopedNode>(versionedResourceGroup.Key!, versionedResourceGroup.First().Children("resource").First()));
                    referenceEntryPairs.AddRange(
                        versionedResourceGroup.Select(
                            entry => new KeyValuePair<string, ScopedNode>(
                                (versionedResourceGroup.Key + "/_history/" + entry.Children("resource").Children("meta").Children("versionId").First().Value), 
                                entry.Children("resource").Single()
                            )
                        )!
                    );
                }
                var unversionedEntries = this.Children("entry").Where(entry => !entry.Children("resource").Children("meta").Children("versionId").Any());
                referenceEntryPairs.AddRange(unversionedEntries.Select(entry => new KeyValuePair<string?, ScopedNode>(
                    (entry.Children("fullUrl").FirstOrDefault()?.Value as string), 
                    entry.Children("resource").First()
                )));
                _cache.BundledResources = new ReferencedResourceCache(referenceEntryPairs);
            }
                    
            else
                _cache.BundledResources = new ReferencedResourceCache([]);

            return _cache.BundledResources;
        }

        private readonly string? _fullUrl = null;

        /// <summary>
        /// The full url of the resource this element is part of (if in a Bundle)
        /// </summary>
        /// <returns></returns>
        public string? FullUrl() => _fullUrl;

        /// <summary>
        /// The full uri from where the instance this node is part of was retrieved.
        /// </summary>
        /// <remarks>The initial (parent) ScopedNode must have been created supplying the instanceUri parameter
        /// of the constructor.</remarks>
        public string? InstanceUri
        {
            get
            {
                // Scan up until the first parent that knowns the instance uri (at the last the
                // root, if it has been supplied).
                _cache.InstanceUri ??= ParentResources().SkipWhile(p => p.InstanceUri is null).FirstOrDefault()?.InstanceUri;

                return _cache.InstanceUri;
            }

            private set
            {
                _cache.InstanceUri = value;
            }
        }

        /// <inheritdoc />
        public IEnumerable<object> Annotations(Type type) => type == typeof(ScopedNode) ? (new[] { this }) : Current.Annotations(type);

        /// <inheritdoc />
        IEnumerable<ITypedElement> ITypedElement.Children(string? name) =>
            Current.Children(name).Select(c => new ScopedNode(this, ParentResource, c, _fullUrl));
        
        /// <inheritdoc />
        public IEnumerable<ScopedNode> Children(string? name = null) =>
            Current.Children(name).Select(c => new ScopedNode(this, ParentResource, c, _fullUrl));

        public string ShortPath => Current is ElementNode en ? en.ShortPath : Current.Location;
    }
}