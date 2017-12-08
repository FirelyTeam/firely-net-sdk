using Hl7.Fhir.Model;
using Hl7.Fhir.Model.DSTU2;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Source.Summary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    class InMemoryProfileResolver : IResourceResolver, IConformanceSource
    {
        ILookup<string, Resource> _resources;

        public InMemoryProfileResolver(IEnumerable<IMetadataResource> profiles)
        {
            Reload(profiles);
        }

        public InMemoryProfileResolver(params IMetadataResource[] profiles) : this(profiles.AsEnumerable()) { }

        public InMemoryProfileResolver(IMetadataResource profile) : this(new IMetadataResource[] { profile }) { }

        public void Reload(IEnumerable<IMetadataResource> profiles)
        {
            _resources = profiles.ToLookup(r => r.Url, r => r as Resource);
        }

        public void Reload(IMetadataResource[] profiles) => Reload(profiles.AsEnumerable());

        public void Reload(IMetadataResource profile) => Reload(new IMetadataResource[] { profile });

        public void Clear() => Reload(Enumerable.Empty<IMetadataResource>());

        #region IResourceResolver

        public Resource ResolveByCanonicalUri(string uri) => _resources[uri].FirstOrDefault();

        public Resource ResolveByUri(string uri) => null;

        #endregion

        #region IMetadataResource

        public ValueSet FindValueSetBySystem(string valueSetUri)
            => throw new NotImplementedException();

        public IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
            => throw new NotImplementedException();

        public NamingSystem FindNamingSystem(string uniqueid)
            => throw new NotImplementedException();

        public IEnumerable<string> ListResourceUris(ResourceType? filter = default(ResourceType?))
            => _resources.Select(g => g.Key);

        public IEnumerable<ArtifactSummary> ListSummaries() => throw new NotImplementedException();

        #endregion
    }

}
