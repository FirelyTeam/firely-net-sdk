/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.IO;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from a list of other IArtifactSources
    /// </summary>
    public class MultiSource : IArtifactSource
    {
        private readonly List<IArtifactSource> _sources = new List<IArtifactSource>();

        /// <summary>
        /// Custom implementation of the artifact resolver
        /// </summary>
        /// <param name="sources">A custom set of IArtifact sources. Resolving occurs in order of input</param>
        public MultiSource(IEnumerable<IArtifactSource> sources)
        {
            if (sources == null) throw Error.ArgumentNull("sources");

            _sources.AddRange(sources);
        }

        /// <summary>
        /// Custom implementation of the artifact resolver
        /// </summary>
        /// <param name="sources">A custom set of IArtifact sources. Resolving occurs in order of input</param>
        public MultiSource(params IArtifactSource[] sources)
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

   
        public Stream LoadArtifactByName(string name)
        {
            foreach (var source in Sources)
            {
                try
                {
                    var result = source.LoadArtifactByName(name);

                    if (result != null) return result;
                }
                catch(NotImplementedException)
                {
                    // Don't do anything, just try the next IArtifactSource
                }
            }

            // None of the IArtifactSources succeeded in returning a result
            return null;
        }

        public IEnumerable<string> ListArtifactNames()
        {
            var result = new List<string>();

            foreach (var source in Sources)
            {
                try
                {
                    result.AddRange(source.ListArtifactNames());
                }
                catch (NotImplementedException)
                {
                    // Don't do anything, just try the next IArtifactSource
                }
            }

            return result;
        }

        public Resource LoadConformanceResourceByUrl(string url)
        {
            if (url == null) throw Error.ArgumentNull("url");

            foreach (var source in Sources)
            {
                try
                {
                    var result = source.LoadConformanceResourceByUrl(url);

                    if (result != null) return result;
                }
                catch(NotImplementedException)
                {
                    // Don't do anything, just try the next IArtifactSource
                }
            }

            // None of the IArtifactSources succeeded in returning a result
            return null;
        }

        public IEnumerable<ConformanceInformation> ListConformanceResources()
        {
            var result = new List<ConformanceInformation>();

            foreach (var source in Sources)
            {
                try
                {
                    result.AddRange(source.ListConformanceResources());
                }
                catch (NotImplementedException)
                {
                    // Don't do anything, just try the next IArtifactSource
                }
            }

            return result;
        }
    }
}
