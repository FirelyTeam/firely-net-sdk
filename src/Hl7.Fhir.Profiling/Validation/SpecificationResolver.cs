using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Introspection.Source;
using Hl7.Fhir.Model;
using Fhir.Profiling;
using Fhir.Profiling.IO;


namespace Hl7.Fhir.Introspection
{
    public class SpecificationResolver
    {

        ArtifactResolver resolver = new ArtifactResolver();
        SpecificationBuilder builder;
        SpecificationLoader loader;
        
        public SpecificationResolver(SpecificationBuilder builder) 
        {
            this.builder = builder;
            this.loader = new SpecificationLoader(builder);
        }

        public SpecificationResolver(string path)
        {
            CoreZipArtifactSource source = new CoreZipArtifactSource(path);
            resolver.AddSource(source);
        }

        public SpecificationResolver(params IArtifactSource[] sources)
        {
            foreach(IArtifactSource source in sources)
            {
                resolver.AddSource(source);
            }
        }

        public Profile GetProfile(Uri uri)
        {
            Resource resource = resolver.ReadResourceArtifact(uri);
            
            Profile profile = (Profile)resource;
            return profile;
        }

        public Hl7.Fhir.Model.ValueSet GetValueSet(Uri uri)
        {
            Resource resource = resolver.ReadResourceArtifact(uri);
            Hl7.Fhir.Model.ValueSet valueset = (Hl7.Fhir.Model.ValueSet)resource;
            return (Hl7.Fhir.Model.ValueSet)resource;
        }

       

    }
}
