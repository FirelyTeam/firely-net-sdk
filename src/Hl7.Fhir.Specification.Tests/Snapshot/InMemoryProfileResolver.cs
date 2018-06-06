using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    class InMemoryProfileResolver : IResourceResolver, IConformanceSource
    {
        ILookup<string, Resource> _resources;

        public InMemoryProfileResolver(IEnumerable<IConformanceResource> profiles)
        {
            Reload(profiles);
        }

        public InMemoryProfileResolver(params IConformanceResource[] profiles) : this(profiles.AsEnumerable()) { }

        public InMemoryProfileResolver(IConformanceResource profile) : this(new IConformanceResource[] { profile }) { }

        public void Reload(IEnumerable<IConformanceResource> profiles)
        {
            _resources = profiles.ToLookup(r => r.Url, r => r as Resource);
        }

        public void Reload(IConformanceResource[] profiles) => Reload(profiles.AsEnumerable());

        public void Reload(IConformanceResource profile) => Reload(new IConformanceResource[] { profile });

        public void Clear() => Reload(Enumerable.Empty<IConformanceResource>());

        #region IResourceResolver

        public Resource ResolveByCanonicalUri(string uri) => _resources[uri].FirstOrDefault();

        public Resource ResolveByUri(string uri) => null;

        #endregion

        #region IConformanceResource

        public ValueSet FindValueSetBySystem(string valueSetUri)
            => throw new NotImplementedException();

        public IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
            => throw new NotImplementedException();

        public NamingSystem FindNamingSystem(string uniqueid)
            => throw new NotImplementedException();

        public IEnumerable<string> ListResourceUris(ResourceType? filter = default(ResourceType?))
            => _resources.Select(g => g.Key);

        #endregion
    }

}
