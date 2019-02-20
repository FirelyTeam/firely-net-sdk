/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
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
        /// Creates a default non-cached artifact resolver
        /// Default only searches in the executable directory files and the core zip. 
        /// This non-cached resolver is primary for testing purposes.
        /// </summary>
        public static IResourceResolver CreateDefault()
        {
            return new MultiResolver(new DirectorySource(new DirectorySourceSettings { IncludeSubDirectories = true } ), 
                ZipSource.CreateValidationSource(), new WebResolver());
        }

        /// <summary>
        /// Creates an offline non-cached artifact resolver
        /// Default only searches in the executable directory files and the core zip. 
        /// </summary>
        public static IResourceResolver CreateOffline()
        {
            // Making requests to a WebArtifactSource is time consuming. So for performance we have an Offline Resolver.
            return new MultiResolver(new DirectorySource(new DirectorySourceSettings { IncludeSubDirectories = true }), 
                ZipSource.CreateValidationSource());
        }

        /// <summary>
        /// Creates a default cached artifact resolver
        /// Default only searches in the executable directory files and the core zip. 
        /// </summary>
        public static IResourceResolver CreateCachedDefault()
        {
            return new CachedResolver(CreateDefault());
        }

        /// <summary>
        /// Creates a default cached artifact resolver
        /// Default only searches in the executable directory files and the core zip. 
        /// </summary>
        public static IResourceResolver CreateCachedOffline()
        {
            return new CachedResolver(CreateOffline());
        }
    }
}
