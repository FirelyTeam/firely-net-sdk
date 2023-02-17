#nullable enable

namespace Hl7.Fhir.Specification.Source
{
    using Hl7.Fhir.Model;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class InMemoryResourceResolver : IAsyncResourceResolver, IResourceResolver
    {
        private List<(string? Uri, string? Url, Resource Resource)> _resources = new();

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
            _resources = new();
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
            Reload(Enumerable.Empty<Resource>());
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
                _resources.Add(new(getUri(resource), conformance.Url, resource));
            }
            else
            {
                _resources.Add(new(getUri(resource), null, resource));
            }
        }

        private string? getUri(Resource resource)
        {
            return !string.IsNullOrEmpty(resource.Id)
                ? $"{resource.TypeName}/{resource.Id}"
                : null;
        }

        ///<inheritdoc/>
        public Resource? ResolveByCanonicalUri(string uri)
        {
            return _resources.Where(r => r.Url == uri)?.Select(r => r.Resource).FirstOrDefault();
        }

        ///<inheritdoc/>
        public Task<Resource?> ResolveByCanonicalUriAsync(string uri)
        {
            return Task.FromResult(ResolveByCanonicalUri(uri));
        }

        ///<inheritdoc/>
        public Resource? ResolveByUri(string uri)
        {
            return _resources.Where(r => r.Uri == uri)?.Select(r => r.Resource).FirstOrDefault();
        }

        ///<inheritdoc/>
        public Task<Resource?> ResolveByUriAsync(string uri)
        {
            return Task.FromResult(ResolveByUri(uri));
        }
    }
}

#nullable restore

