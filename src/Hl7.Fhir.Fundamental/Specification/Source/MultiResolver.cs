/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using System.Linq;
using Hl7.Fhir.Utility;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from a list of other IArtifactSources
    /// </summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class MultiResolver : IResourceResolver, IAsyncResourceResolver
    {
#pragma warning disable CS0618 // Type or member is obsolete
        private readonly List<ISyncOrAsyncResourceResolver> _sources = new List<ISyncOrAsyncResourceResolver>();

        public MultiResolver(IEnumerable<ISyncOrAsyncResourceResolver> sources)
        {
            if (sources == null) throw Error.ArgumentNull(nameof(sources));

            _sources.AddRange(sources);
        }

        public MultiResolver(params ISyncOrAsyncResourceResolver[] sources) : this((IEnumerable<ISyncOrAsyncResourceResolver>)sources) { }

        public void AddSource(ISyncOrAsyncResourceResolver source)
        {
            _sources.Add(source);
        }

        public void RemoveSource(ISyncOrAsyncResourceResolver source)
        {
            _sources.Remove(source);
        }

        public void Push(ISyncOrAsyncResourceResolver source)
        {
            _sources.Insert(0, source);
        }
#pragma warning restore CS0618 // Type or member is obsolete

        public void Pop()
        {
            if (_sources.Any()) _sources.RemoveAt(0);
        }

        /// <summary>
        /// Return all child resolvers that support synchronous resolution.
        /// </summary>
        /// <remarks>The collections returned by the <see cref="Sources"/> and <see cref="AsyncSources" /> properties are 
        /// not necessarily disjunct.</remarks>
        public IList<IResourceResolver> Sources => _sources.OfType<IResourceResolver>().ToList();

        /// <summary>
        /// Return all child resolvers that support asynchronous resolution.
        /// </summary>
        /// <remarks>The collections returned by the <see cref="AsyncSources"/> and <see cref="Sources" /> properties are 
        /// not necessarily disjunct.</remarks>
        public IList<IAsyncResourceResolver> AsyncSources => _sources.OfType<IAsyncResourceResolver>().ToList();

        private IEnumerable<IAsyncResourceResolver> allSourcesAsAsync() => _sources.Select(src => src.AsAsync());


        [Obsolete("MultiResolver now works best with asynchronous resolvers. Use ResolveByUriAsync() instead.")]
        public Resource ResolveByUri(string uri) => TaskHelper.Await(() => ResolveByUriAsync(uri));

        public async Task<Resource> ResolveByUriAsync(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));

            foreach (IAsyncResourceResolver source in allSourcesAsAsync())
            {
                try
                {
                    var result = await source.ResolveByUriAsync(uri).ConfigureAwait(false);

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

        [Obsolete("MultiResolver now works best with asynchronous resolvers. Use ResolveByCanonicalUriAsync() instead.")]
        public Resource ResolveByCanonicalUri(string uri) => TaskHelper.Await(() => ResolveByCanonicalUriAsync(uri));

        public async Task<Resource> ResolveByCanonicalUriAsync(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));

            foreach (var source in allSourcesAsAsync())
            {
                try
                {
                    var result = await source.ResolveByCanonicalUriAsync(uri).ConfigureAwait(false);

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
            => $"{GetType().Name} for {_sources.Count} sources: {string.Join(" | ", _sources.Select(s => s.DebuggerDisplayString()))}";
    }
}
