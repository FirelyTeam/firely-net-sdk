using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    class InMemoryProfileResolver : IResourceResolver
    {
        ILookup<string, Resource> _resources;

        public InMemoryProfileResolver(params IConformanceResource[] profiles) : this(profiles.AsEnumerable()) { }

        public InMemoryProfileResolver(IEnumerable<IConformanceResource> profiles)
        {
            _resources = profiles.ToLookup(r => r.Url, r => r as Resource);
        }

        public InMemoryProfileResolver(IConformanceResource profile) : this(new IConformanceResource[] { profile }) { }

        public Resource ResolveByCanonicalUri(string uri) => _resources[uri].FirstOrDefault();

        public Resource ResolveByUri(string uri) => null;
    }

}
