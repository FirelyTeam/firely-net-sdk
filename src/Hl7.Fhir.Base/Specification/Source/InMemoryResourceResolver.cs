#nullable enable

/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source;

public class InMemoryResourceResolver : IAsyncResourceResolver, IResourceResolver
{
    private record Entry(string? Uri, string? Url, Resource Resource);

    private List<Entry> _resources = new();

    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, CodeSystems etc.) from memory.
    /// </summary>
    public InMemoryResourceResolver()
    {
    }

    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, CodeSystems etc.) from memory.
    /// </summary>
    /// <param name="resources">Resources to be loaded in memory.</param>
    public InMemoryResourceResolver(IEnumerable<Resource> resources)
    {
        Add(resources);
    }

    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, CodeSystems etc.) from memory.
    /// </summary>
    /// <param name="resource">One or more resources to be loaded in memory..</param>
    public InMemoryResourceResolver(params Resource[] resource) : this(resource.AsEnumerable()) { }


    /// <summary>
    /// Reloads the memory resource provider with new resources
    /// </summary>
    /// <param name="resources">resources to be loaded in memory</param>
    public void Reload(IEnumerable<Resource> resources)
    {
        _resources = [];
        Add(resources);
    }

    /// <summary>
    /// Reloads the memory resource provider with new resources
    /// </summary>
    /// <param name="resources">resources to be loaded in memory</param>
    public void Reload(params Resource[] resources) => Reload(resources.AsEnumerable());

    /// <summary>
    /// Clears the memory of the resource provider.
    /// </summary>
    public void Clear()
    {
        Reload();
    }

    /// <summary>
    /// Adds a resource to memory
    /// </summary>
    /// <param name="resources">Resouces to be loaden in memory</param>
    public void Add(IEnumerable<Resource> resources)
    {
        foreach (var resource in resources)
            add(resource);
    }

    /// <summary>
    /// Adds a resource to memory
    /// </summary>
    /// <param name="resources">One or multiple resouces to be loaded in memory</param>
    public void Add(params Resource[] resources) => this.Add(resources.AsEnumerable());


    private void add(Resource resource)
    {
        if (resource is IConformanceResource conformance)
        {
            _resources.Add(new Entry(getUri(resource), conformance.Url, resource));
        }
        else
        {
            _resources.Add(new Entry(getUri(resource), null, resource));
        }
    }

    private string? getUri(Resource resource) =>
        !string.IsNullOrEmpty(resource.Id)
            ? $"{resource.TypeName}/{resource.Id}"
            : null;

    ///<inheritdoc/>
    public ResolverResult TryResolveByUri(string uri)
    {
        var resource = resolveBestCandidate(uri, (r,u) => r.Uri == u);

        return resource is not null
            ? new ResolverResult(resource)
            : new ResolverResult(ResolverException.NotFound());
    }

    ///<inheritdoc/>
    public ResolverResult TryResolveByCanonicalUri(string uri)
    {
        var resource = resolveBestCandidate(uri, (r,u) => r.Url == u);

        return resource is not null
            ? new ResolverResult(resource)
            : new ResolverResult(ResolverException.NotFound());
    }


    private Resource? resolveBestCandidate(string uri, Func<Entry,string?,bool> urlFilter)
    {
        var canonical = new Canonical(uri);
        var url = canonical.Uri;

        // Filter by canonical URL first
        var candidateResources = _resources.Where(e => urlFilter(e,url));

        if(canonical.Version is not null)
        {
            // If a version is specified, filter by version as well
            candidateResources = candidateResources.Where(r => r.Resource is IVersionableConformanceResource versionable &&
                                                               Canonical.MatchesVersion(versionable.Version, canonical.Version));
        };

        return candidateResources.Select(r => r.Resource).FirstOrDefault();
    }

    ///<inheritdoc/>
    public Resource? ResolveByCanonicalUri(string uri) => TryResolveByCanonicalUri(uri).Value;

    ///<inheritdoc/>
    public Task<Resource?> ResolveByCanonicalUriAsync(string uri) => Task.FromResult(ResolveByCanonicalUri(uri));

    ///<inheritdoc/>
    public Resource? ResolveByUri(string uri) => TryResolveByUri(uri).Value;

    ///<inheritdoc/>
    public Task<Resource?> ResolveByUriAsync(string uri) => Task.FromResult(ResolveByUri(uri));

    ///<inheritdoc/>
    public Task<ResolverResult> TryResolveByUriAsync(string uri) => Task.FromResult(TryResolveByUri(uri));

    ///<inheritdoc/>
    public Task<ResolverResult> TryResolveByCanonicalUriAsync(string uri) =>
        Task.FromResult(TryResolveByCanonicalUri(uri));
}