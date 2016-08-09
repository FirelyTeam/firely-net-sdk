using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Source;

namespace Hl7.Fhir.Validation
{
    using Hl7.Fhir.Specification.Model;
    using Model = Hl7.Fhir.Model;
    

    /// <summary>
    /// This class provides access to external artifacts provided by the IArtifactSource
    /// Functionally equivalent to the ArtifactResolver class, but also converts the
    /// conformance resources to the internal representation (e.g. a ValueSet to a Validation.ValueSet)
    /// </summary>
    public class SpecificationProvider
    {
        public IArtifactSource source;
        //SpecificationHarvester harvester;
        //StructureLoader loader;
        

        public SpecificationProvider(IArtifactSource source)
        {
            this.source = source;
            //this.loader = new StructureLoader(source);
            //this.harvester = new SpecificationHarvester();
        }

        public static SpecificationProvider CreateDefault()
        {
            IArtifactSource source = ArtifactResolver.CreateCachedDefault();
            return new SpecificationProvider(source);
        }

        public static SpecificationProvider CreateOffline(params IArtifactSource[] sources)
        {
            IArtifactSource cache = ArtifactResolver.CreateOffline();
            return new SpecificationProvider(cache);
        }

        
        private T Resolve<T>(Uri uri) where T : Model.Resource
        {
            //loader.LocateStructure(uri);
            //Model.Resource resource = source.ReadResourceArtifact(uri);
            //return (T)resource;

            throw new NotImplementedException();
        }
        

        public Structure GetStructure(Uri uri)
        {
            //Model.Profile.ProfileStructureComponent component = loader.LocateStructure(uri);
            //if (component != null)
            //{
            //    Structure structure = harvester.HarvestStructure(component, uri);
            //    return structure;
            //}
            return null;
        }
         


        public static IEnumerable<T> Singleton<T>(T item)
        {
            if (item != null)
                yield return item;
            else
                yield break;
        }

        public IEnumerable<Structure> GetStructures(Uri uri)
        {
            Structure structure = GetStructure(uri);
            return Singleton(structure);
        }

        /*
        public IEnumerable<Structure> GetStructures(Uri uri)
        {
            Model.Profile profile = Resolve<Model.Profile>(uri);
            if (profile != null)
            {
                IEnumerable<Structure> structures = harvester.HarvestStructures(profile);
                UriHelper.SetStructureIdentification(structures, uri);
                return structures;
            }
            else
            {
                return Enumerable.Empty<Structure>();
            }
        }
        */

        public IEnumerable<Structure> GetStructures(string uri)
        {
            return GetStructures(new Uri(uri));
        }

        public IEnumerable<Structure> GetStructures(TypeRef typeref)
        {
            Uri uri = typeref.Uri;
            return GetStructures(uri);
        }

        
        public ValueSet GetValueSet(Uri uri)
        {
            //var valueset = source.LoadConformanceResourceByUrl(uri.ToString()) as Model.ValueSet;
           
            //if (valueset != null)
            //{
            //    ValueSet target = harvester.HarvestValueSet(valueset, uri);
            //    return target;
            //}
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
