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
    using Specification.Validation.Model;
    using Model = Hl7.Fhir.Model;


    /// <summary>
    /// This class provides access to external artifacts provided by the IArtifactSource
    /// Functionally equivalent to the ArtifactResolver class, but also converts the
    /// conformance resources to the internal representation (e.g. a ValueSet to a Validation.ValueSet)
    /// via the SpecificationHarvester.
    /// </summary>
    public class SpecificationProvider
    {
        public IArtifactSource source;
        SpecificationHarvester harvester;
        //StructureLoader loader;
        

        public SpecificationProvider(IArtifactSource source)
        {
            this.source = source;
            this.harvester = new SpecificationHarvester();
        }
       
        //private T Resolve<T>(Uri uri) where T : Model.Resource
        //{
        //    Model.Resource resource = source.LoadConformanceResourceByUrl(uri.ToString());
        //    return (T)resource;
        //}
        

        public Structure GetStructure(Uri uri)
        {
            Model.StructureDefinition sd = source.LoadConformanceResourceByUrl(uri.ToString()) as Model.StructureDefinition;

            if (sd != null)
            {
                Structure structure = harvester.HarvestStructure(sd, uri);
                return structure;
            }

            return null;
        }
         
        //public IEnumerable<Structure> GetStructures(Uri uri)
        //{
        //    Structure structure = GetStructure(uri);
        //    yield return structure;
        //}

        //public IEnumerable<Structure> GetStructures(string uri)
        //{
        //    return GetStructures(new Uri(uri));
        //}

        //public IEnumerable<Structure> GetStructures(TypeRef typeref)
        //{
        //    Uri uri = typeref.Uri;
        //    return GetStructures(uri);
        //}

        
        public ValueSet GetValueSet(Uri uri)
        {
            var valueset = source.LoadConformanceResourceByUrl(uri.ToString()) as Model.ValueSet;

            if (valueset != null)
            {
                ValueSet target = ValueSet.HarvestValueSet(valueset);
                return target;
            }
            return null;           
        }

        //public IEnumerable<ValueSet> GetValueSets(IEnumerable<Uri> uris)
        //{
        //    foreach (Uri uri in uris)
        //    {
        //        ValueSet valueset = GetValueSet(uri);
        //        if (valueset != null) yield return valueset;

        //    }
        //}
    }
}
