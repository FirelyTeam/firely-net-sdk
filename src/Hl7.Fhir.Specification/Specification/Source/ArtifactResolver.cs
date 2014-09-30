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

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) using a list of other IArtifactSources
    /// </summary>
    public class ArtifactResolver : IArtifactSource
    {
        private readonly List<IArtifactSource> _sources = new List<IArtifactSource>();

        public ArtifactResolver()
        {            
        }

        /// <summary>
        /// Creates a default non-cached ArtifactResolver
        /// Default only searches in the executable directory files and the core zip. 
        /// This non-cached resolver is primary for testing purposes.
        /// </summary>
        public static IArtifactSource CreateDefault()
        {
            return new ArtifactResolver(new FileArtifactSource(true), new CoreZipArtifactSource(), new WebArtifactSource());
        }

        /// <summary>
        /// Creates a default offline cached ArtifactResolver
        /// Default only searches in the executable directory files and the core zip. 
        /// </summary>
        public static IArtifactSource CreateOffline()
        {
            // Making requests to a WebArtifactSource is time consuming. So for performance we have an Offline Resolver.
            IArtifactSource resolver = new ArtifactResolver(new FileArtifactSource(true), new CoreZipArtifactSource());
            return new CachedArtifactSource(resolver);
            
        }

        /// <summary>
        /// Creates a default cached ArtifactResolver
        /// Default only searches in the executable directory files and the core zip. 

        /// </summary>
        public static IArtifactSource CreateCachedDefault()
        {
            IArtifactSource resolver = ArtifactResolver.CreateDefault();
            return new CachedArtifactSource(resolver);
        }

        /// <summary>
        /// Custom implementation of the artifact resolver
        /// </summary>
        /// <param name="sources">A custom set of IArtifact sources. Resolving occurs in order of input</param>
        public ArtifactResolver(IEnumerable<IArtifactSource> sources)
        {
            if (sources == null) throw Error.ArgumentNull("sources");

            _sources.AddRange(sources);
        }

        /// <summary>
        /// Custom implementation of the artifact resolver
        /// </summary>
        /// <param name="sources">A custom set of IArtifact sources. Resolving occurs in order of input</param>
        public ArtifactResolver(params IArtifactSource[] sources)
        {
            if (sources == null) throw Error.ArgumentNull("sources");

            _sources.AddRange(sources);
        }

        public void AddSource(IArtifactSource source)
        {
            _sources.Add(source);
        }

        public void RemoveSource(IArtifactSource source)
        {
            _sources.Remove(source);
        }

        public IList<IArtifactSource> Sources
        { 
            get { return _sources; } 
        }

        private bool _prepared = false;

        public void Prepare()
        {
            if (!_prepared)
            {
                foreach (var source in Sources) source.Prepare();
                _prepared = true;
            }
        }

        public Stream ReadContentArtifact(string name)
        {
            if (!_prepared) Prepare();

            foreach (var source in Sources)
            {
                try
                {
                    var result = source.ReadContentArtifact(name);

                    if (result != null) return result;
                }
                catch
                {
                    // Don't do anything, just try the next IArtifactSource
                }
            }

            // None of the IArtifactSources succeeded in returning a result
            return null;
        }

        public Resource ReadResourceArtifact(Uri artifactId)
        {
            if (!_prepared) Prepare();

            foreach (var source in Sources)
            {
                try
                {
                    var result = source.ReadResourceArtifact(artifactId);

                    if (result != null) return result;
                }
                catch
                {
                    // Don't do anything, just try the next IArtifactSource
                }
            }

            // None of the IArtifactSources succeeded in returning a result
            return null;
        }
    }
}
