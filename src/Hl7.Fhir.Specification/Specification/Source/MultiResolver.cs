/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using System.Linq;
using Hl7.Fhir.Utility;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from a list of other IArtifactSources
    /// </summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class MultiResolver : IResourceResolver
    {
        private readonly List<IResourceResolver> _sources = new List<IResourceResolver>();

        /// <summary>
        /// Custom implementation of the artifact resolver
        /// </summary>
        /// <param name="sources">A custom set of IArtifact sources. Resolving occurs in order of input</param>
        public MultiResolver(IEnumerable<IResourceResolver> sources)
        {
            if (sources == null) throw Error.ArgumentNull(nameof(sources));

            _sources.AddRange(sources);
        }

        /// <summary>
        /// Custom implementation of the artifact resolver
        /// </summary>
        /// <param name="sources">A custom set of IArtifact sources. Resolving occurs in order of input</param>
        public MultiResolver(params IResourceResolver[] sources)
        {
            if (sources == null) throw Error.ArgumentNull(nameof(sources));

            _sources.AddRange(sources);
        }

        public void AddSource(IResourceResolver source)
        {
            Sources.Add(source);
        }

        public void RemoveSource(IResourceResolver source)
        {
            Sources.Remove(source);
        }


        public void Push(IResourceResolver source)
        {
            Sources.Insert(0, source);
        }

        public void Pop()
        {
            if (Sources.Any()) _sources.RemoveAt(0);
        }

        public IList<IResourceResolver> Sources
        { 
            get { return _sources; } 
        }

   
        public Resource ResolveByUri(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));

            foreach (var source in Sources)
            {
                try
                {
                    var result = source.ResolveByUri(uri);

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


        public Resource ResolveByCanonicalUri(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));

            foreach (var source in Sources)
            {
                try
                {
                    var result = source.ResolveByCanonicalUri(uri);

                    if (result != null) return result;
                }
                catch (NotImplementedException)
                {
                    // Don't do anything, just try the next IArtifactSource
                }
            }

            // None of the IArtifactSources succeeded in returning a result
            return null;
        }

        // Allow derived classes to override
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal protected virtual string DebuggerDisplay
            => $"{GetType().Name} for {Sources.Count} sources: {string.Join(" | ", Sources.Select(s => s.DebuggerDisplayString()))}";
    }
}
