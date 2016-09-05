/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) using a list of other IArtifactSources
    /// </summary>    
    public class SourceFactory
    {
        /// <summary>
        /// Creates a default non-cached ArtifactResolver
        /// Default only searches in the executable directory files and the core zip. 
        /// This non-cached resolver is primary for testing purposes.
        /// </summary>
        public static IConformanceStore CreateDefault()
        {
            return new MultiSource(new DirectorySource(true), ZipSource.CreateValidationSource(), new WebSource());
        }

        /// <summary>
        /// Creates an offline non-cached ArtifactResolver
        /// Default only searches in the executable directory files and the core zip. 
        /// </summary>
        public static IConformanceStore CreateOffline()
        {
            // Making requests to a WebArtifactSource is time consuming. So for performance we have an Offline Resolver.
            return new MultiSource(new DirectorySource(true), ZipSource.CreateValidationSource());
        }

        /// <summary>
        /// Creates a default cached ArtifactResolver
        /// Default only searches in the executable directory files and the core zip. 
        /// </summary>
        public static IConformanceStore CreateCachedDefault()
        {
            return new CachedResolver(CreateDefault());
        }

        /// <summary>
        /// Creates a default cached ArtifactResolver
        /// Default only searches in the executable directory files and the core zip. 
        /// </summary>
        public static IConformanceStore CreateCachedDefault()
        {
            return new CachedResolver(CreateOffline());
        }
    }
}
