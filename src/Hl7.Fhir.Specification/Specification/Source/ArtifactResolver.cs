/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.github.com/furore-fhir/spark/master/LICENSE
 */


using System;
using System.Collections.Generic;
using System.IO;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Linq;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) using a list of other IArtifactSources
    /// </summary>
    public class ArtifactResolver : IArtifactSource
    {        
        public ArtifactResolver(IArtifactSource source)
        {
            Source = source;
        }


        public IArtifactSource Source
        {
            get;
            private set;
        }


        /// <summary>
        /// Creates a default non-cached ArtifactResolver
        /// Default only searches in the executable directory files and the core zip. 
        /// This non-cached resolver is primary for testing purposes.
        /// </summary>
        public static ArtifactResolver CreateDefault()
        {
            return new ArtifactResolver(new MultiArtifactSource(new FileDirectoryArtifactSource(true), ZipArtifactSource.CreateValidationSource(), new WebArtifactSource()));            
        }

        /// <summary>
        /// Creates a default offline cached ArtifactResolver
        /// Default only searches in the executable directory files and the core zip. 
        /// </summary>
        public static ArtifactResolver CreateOffline()
        {
            // Making requests to a WebArtifactSource is time consuming. So for performance we have an Offline Resolver.
            IArtifactSource multi = new MultiArtifactSource(new FileDirectoryArtifactSource(true), ZipArtifactSource.CreateValidationSource());
            return new ArtifactResolver(new CachedArtifactSource(multi));
        }

        /// <summary>
        /// Creates a default cached ArtifactResolver
        /// Default only searches in the executable directory files and the core zip. 
        /// </summary>
        public static ArtifactResolver CreateCachedDefault()
        {
            var resolver = ArtifactResolver.CreateDefault();

            // Wrap a cache around the default source
            resolver.Source = new CachedArtifactSource(resolver.Source);

            return resolver;
        }

   
        public Stream LoadArtifactByName(string name)
        {
            return Source.LoadArtifactByName(name);
        }

        public IEnumerable<string> ListArtifactNames()
        {
            return Source.ListArtifactNames();
        }

        public Resource LoadConformanceResourceByUrl(string identifier)
        {
            return Source.LoadConformanceResourceByUrl(identifier);
        }

        public IEnumerable<ConformanceInformation> ListConformanceResources()
        {
            return Source.ListConformanceResources();
        }


        public StructureDefinition GetExtensionDefinition(string url, bool requireSnapshot=true)
        {
            var cr = LoadConformanceResourceByUrl(url) as StructureDefinition;
            if (cr == null) return null;

            if (!cr.IsExtension)
                throw Error.Argument("url", "Given url exists as a StructureDefinition, but is not an extension");

            if (cr.Snapshot == null && requireSnapshot)
                return null;

            return cr;
        }

        public StructureDefinition GetStructureDefinition(string url)
        {
            return LoadConformanceResourceByUrl(url) as StructureDefinition;
        }

        public StructureDefinition GetStructureDefinitionForCoreType(string typename)
        {
            var url = ResourceIdentity.Build(new Uri(XmlNs.FHIR), "StructureDefinition", typename).ToString();
            return GetStructureDefinition(url);
        }

        /// <summary>
        /// Return canonical urls of all the core Resource/datatype/primitive StructureDefinitions available in the IArtifactSource
        /// </summary>
        public IEnumerable<string> GetCoreModelUrls()
        {
            return ListConformanceResources()
                .Select(ci => ci.Url)
                .Where(uri => uri != null && uri.StartsWith(XmlNs.FHIR) && ModelInfo.IsCoreModelType(new ResourceIdentity(uri).Id));
        }


        public IEnumerable<ConceptMap> GetConceptMaps()
        {
            //Note: we assume this ArtifactSource caches the conceptmaps. Otherwise this is expensive.

            var conceptMapUrls = ListConformanceResources().Where(info => info.Type == ResourceType.ConceptMap).Select(info => info.Url);

            return conceptMapUrls.Select(url => (ConceptMap)LoadConformanceResourceByUrl(url));
        }

        public IEnumerable<ConceptMap> GetConceptMapsForSource(string uri)
        {
            return GetConceptMaps().Where(cm => cm.SourceAsString() == uri);
        }

        public IEnumerable<ConceptMap> GetConceptMapsForSource(ValueSet source)
        {
            return GetConceptMapsForSource(source.Url);
        }

        public IEnumerable<ConceptMap> GetConceptMapsForSource(StructureDefinition source)
        {
            return GetConceptMapsForSource(source.Url);
        }

        public ValueSet GetValueSet(string url)
        {
            return LoadConformanceResourceByUrl(url) as ValueSet;
        }

        public ValueSet GetValueSetBySystem(string system)
        {
            var vsInfo = ListConformanceResources().Where(ci => ci.ValueSetSystem == system).SingleOrDefault();

            if(vsInfo != null)
                return GetValueSet(vsInfo.Url);

            return null;
        }

    }
}
