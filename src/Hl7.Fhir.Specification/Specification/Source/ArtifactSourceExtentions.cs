using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    public static class ArtifactSourceExtentions
    {
        public static StructureDefinition GetExtensionDefinition(this IArtifactSource source, string url, bool requireSnapshot = true)
        {
            var cr = source.LoadConformanceResourceByUrl(url) as StructureDefinition;
            if (cr == null) return null;

            if (!cr.IsExtension)
                throw Error.Argument("url", "Given url exists as a StructureDefinition, but is not an extension");

            if (cr.Snapshot == null && requireSnapshot)
                return null;

            return cr;
        }

        public static StructureDefinition GetStructureDefinition(this IArtifactSource source, string url)
        {
            return source.LoadConformanceResourceByUrl(url) as StructureDefinition;
        }

        public static StructureDefinition GetStructureDefinitionForCoreType(this IArtifactSource source, string typename)
        {
            var url = ResourceIdentity.Build(new Uri(XmlNs.FHIR), "StructureDefinition", typename).ToString();
            return source.GetStructureDefinition(url);
        }

        public static StructureDefinition GetStructureDefinitionForCoreType(this IArtifactSource source, FHIRDefinedType type)
        {
            return source.GetStructureDefinitionForCoreType(ModelInfo.FhirTypeToFhirTypeName(type));
        }

        /// <summary>
        /// Return canonical urls of all the core Resource/datatype/primitive StructureDefinitions available in the IArtifactSource
        /// </summary>
        public static IEnumerable<string> GetCoreModelUrls(this IArtifactSource source)
        {
            return source.ListConformanceResources()
                .Select(ci => ci.Canonical)
                .Where(uri => uri != null && uri.StartsWith(XmlNs.FHIR) && ModelInfo.IsCoreModelType(new ResourceIdentity(uri).Id));
        }


        public static IEnumerable<ConceptMap> GetConceptMaps(this IArtifactSource source)
        {
            //Note: we assume this ArtifactSource caches the conceptmaps. Otherwise this is expensive.

            var conceptMapUrls = source.ListConformanceResources().Where(info => info.Type == ResourceType.ConceptMap).Select(info => info.Canonical);

            return conceptMapUrls.Select(url => (ConceptMap)source.LoadConformanceResourceByUrl(url));
        }

        public static IEnumerable<ConceptMap> GetConceptMapsForSource(this IArtifactSource source, string uri)
        {
            return source.GetConceptMaps().Where(cm => cm.SourceAsString() == uri);
        }

        public static IEnumerable<ConceptMap> GetConceptMapsForSource(this IArtifactSource source, ValueSet vs)
        {
            return source.GetConceptMapsForSource(vs.Url);
        }

        public static IEnumerable<ConceptMap> GetConceptMapsForSource(this IArtifactSource source, StructureDefinition vs)
        {
            return source.GetConceptMapsForSource(vs.Url);
        }

        public static ValueSet GetValueSet(this IArtifactSource source, string url)
        {
            return source.LoadConformanceResourceByUrl(url) as ValueSet;
        }

        public static ValueSet GetValueSetBySystem(this IArtifactSource source, string system)
        {
            var vsInfo = source.ListConformanceResources().Where(ci => ci.ValueSetSystem == system).SingleOrDefault();

            if (vsInfo != null)
                return source.GetValueSet(vsInfo.Canonical);

            return null;
        }

    }
}
