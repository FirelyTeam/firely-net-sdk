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

        [Obsolete("Use the extension method with the same name on an IArtifactSource")]
        public StructureDefinition GetExtensionDefinition(string url, bool requireSnapshot=true)
        {
            return Source.GetExtensionDefinition(url, requireSnapshot);
        }

        [Obsolete("Use the extension method with the same name on an IArtifactSource")]
        public StructureDefinition GetStructureDefinition(string url)
        {
            return Source.GetStructureDefinition(url);            
        }

        [Obsolete("Use the extension method with the same name on an IArtifactSource")]
        public StructureDefinition GetStructureDefinitionForCoreType(string typename)
        {
            return Source.GetStructureDefinitionForCoreType(typename);
        }

        [Obsolete("Use the extension method with the same name on an IArtifactSource")]
        public StructureDefinition GetStructureDefinitionForCoreType(FHIRDefinedType type)
        {
            return Source.GetStructureDefinitionForCoreType(type);
        }

        [Obsolete("Use the extension method with the same name on an IArtifactSource")]
        public IEnumerable<string> GetCoreModelUrls()
        {
            return Source.GetCoreModelUrls();
        }

        [Obsolete("Use the extension method with the same name on an IArtifactSource")]
        public IEnumerable<ConceptMap> GetConceptMaps()
        {
            return Source.GetConceptMaps();
        }

        [Obsolete("Use the extension method with the same name on an IArtifactSource")]
        public IEnumerable<ConceptMap> GetConceptMapsForSource(string uri)
        {
            return Source.GetConceptMapsForSource(uri);
        }

        [Obsolete("Use the extension method with the same name on an IArtifactSource")]
        public IEnumerable<ConceptMap> GetConceptMapsForSource(ValueSet source)
        {
            return Source.GetConceptMapsForSource(source);
        }

        [Obsolete("Use the extension method with the same name on an IArtifactSource")]
        public IEnumerable<ConceptMap> GetConceptMapsForSource(StructureDefinition source)
        {
            return Source.GetConceptMapsForSource(source);
        }

        [Obsolete("Use the extension method with the same name on an IArtifactSource")]
        public ValueSet GetValueSet(string url)
        {
            return Source.GetValueSet(url);
        }

        [Obsolete("Use the extension method with the same name on an IArtifactSource")]
        public ValueSet GetValueSetBySystem(string system)
        {
            return Source.GetValueSetBySystem(system);
        }

    }
}
