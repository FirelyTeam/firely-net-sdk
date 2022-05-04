using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests
{
    public class VisitResolver : IResourceResolver
    {
        private readonly Resource _example;
        public List<string> Visits = new();

        public VisitResolver(Resource example = null)
        {
            _example = example;
        }

        public Resource ResolveByCanonicalUri(string uri)
        {
            Visits.Add(uri);
            return _example;
        }

        public Resource ResolveByUri(string uri)
        {
            Visits.Add(uri);
            return _example;
        }

        internal bool Visited(string uri) => Visits.Contains(uri);
    }
}

