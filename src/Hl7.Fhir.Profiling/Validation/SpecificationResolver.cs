using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Introspection.Source;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Introspection
{
    public class SpecificationResolver
    {
        ArtifactResolver resolver = new ArtifactResolver();

        private void cache(IArtifactSource source)
        {
            CachedArtifactSource cache = new CachedArtifactSource(source);
            resolver.AddSource(cache);
        }

        public SpecificationResolver()
        {
            // always add a web artifact source to resolve uri's
            cache(new WebArtifactSource());
        }
        
        public void Add(params string[] paths)
        {
            foreach (string path in paths)
            {
                Add(new CoreZipArtifactSource(path), new FileArtifactSource(path));
            }
        }

        public void Add(params IArtifactSource[] sources)
        {
            foreach (IArtifactSource source in sources)
            {
                cache(source);
            }
        }

        public T Get<T>(Uri uri) where T : Resource
        {
            Resource resource = resolver.ReadResourceArtifact(uri);
            return (T)resource;
        }

    }
}
