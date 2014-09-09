using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Introspection.Source;

namespace Hl7.Fhir.Profiling
{
    using Model = Hl7.Fhir.Model;

    public class SpecificationProvider
    {
        IArtifactSource source;
        SpecificationHarvester harvester;

        public SpecificationProvider(IArtifactSource source)
        {
            this.source = source;
            this.harvester = new SpecificationHarvester();
        }

        public static SpecificationProvider CreateDefault()
        {
            IArtifactSource source = ArtifactResolver.CreateCachedDefault();
            return new SpecificationProvider(source);
        }

        public static SpecificationProvider CreateOffline()
        {
            IArtifactSource source = new ArtifactResolver(new CoreZipArtifactSource(), new FileArtifactSource());
            IArtifactSource cache = new CachedArtifactSource(source);
            return new SpecificationProvider(cache);
        }

        private T Resolve<T>(Uri uri) where T : Model.Resource
        {
            Model.Resource resource = source.ReadResourceArtifact(uri);
            return (T)resource;
        }

        public IEnumerable<Structure> GetStructures(Uri uri)
        {
            Model.Profile profile = Resolve<Model.Profile>(uri);
            if (profile != null)
            {
                return harvester.HarvestStructures(profile);
            }
            else
            {
                return Enumerable.Empty<Structure>();
            }
        }

        public IEnumerable<Structure> GetStructures(string uri)
        {
            return GetStructures(new Uri(uri));
        }

        public IEnumerable<Structure> GetStructures(TypeRef typeref)
        {
            Uri uri = UriHelper.ResolvingUri(typeref);
            return GetStructures(uri);
        }

        public ValueSet GetValueSet(Uri uri)
        {
            Model.ValueSet source = Resolve<Model.ValueSet>(uri);
            if (source != null)
            {
                ValueSet target = harvester.HarvestValueSet(source);
                return target;
            }
            return null;
        }

        public IEnumerable<ValueSet> GetValueSets(IEnumerable<Uri> uris)
        {
            foreach (Uri uri in uris)
            {
                ValueSet valueset = GetValueSet(uri);
                if (valueset != null) yield return valueset;

            }
        }
    }
}
