/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.IO;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Linq;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) using a list of other IArtifactSources
    /// </summary>
    public class CachedResolver : IResourceResolver
    {
        public const int DEFAULT_CACHE_DURATION = 4 * 3600;     // 4 hours

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">ArtifactSource that will be used to get data from on a cache miss</param>
        /// <param name="cacheDuration">Duration before trying to refresh the cache, in seconds</param>
        public CachedResolver(IResourceResolver source, int cacheDuration = DEFAULT_CACHE_DURATION)
        {
            Source = source;
            CacheDuration = cacheDuration;

            _resourcesByUri = new Cache<Resource>(id => Source.ResolveByUri(id), CacheDuration);
            _resourcesByCanonical = new Cache<Resource>(id => Source.ResolveByCanonicalUri(id), CacheDuration);
        }

        public IResourceResolver Source { get; private set; }

        public int CacheDuration { get; set; }

        private Cache<Resource> _resourcesByUri;
        private Cache<Resource> _resourcesByCanonical;

        public Resource ResolveByUri(string url)
        {
            if (url == null) throw Error.ArgumentNull("url");

            return _resourcesByUri.Get(url);
        }

        public Resource ResolveByCanonicalUri(string url)
        {
            if (url == null) throw Error.ArgumentNull("url");

            return _resourcesByCanonical.Get(url);
        }


        private class Cache<T>
        {
            private Func<string,T> _onCacheMiss;
            private int _duration;

            public Cache(Func<string,T> onCacheMiss, int duration)
            {
                _onCacheMiss = onCacheMiss;
                _duration = duration;
            }

            private SynchronizedCollection<CacheEntry<T>> _cache = new SynchronizedCollection<CacheEntry<T>>();
            private Object getLock = new Object();


            public T Get(string identifier)
            {
                lock (getLock)
                {
                    // Check the cache
                    var entry = _cache.Where(ce => ce.Identifier == identifier).SingleOrDefault();

                    // Remove entry if it's too old
                    if (entry != null && entry.Expired)
                    {
                        _cache.Remove(entry);
                        entry = null;
                    }

                    // If we still have a fresh entry, return it
                    if (entry != null)
                        return entry.Data;
                    else
                    {
                        // Otherwise, fetch it and cache it.
                        T newData = default(T);

                        newData = _onCacheMiss(identifier);
                        _cache.Add(new CacheEntry<T> { Data = newData, Identifier = identifier, Expires = DateTime.Now.AddSeconds(_duration) });

                        return newData;
                    }
                }
            }
        }

        private class CacheEntry<T>
        {
            public T Data;
            public DateTime Expires;
            public string Identifier;

            public bool Expired
            {
                get
                {
                    return DateTime.Now > Expires;
                }
            }
        }    
    }
}
