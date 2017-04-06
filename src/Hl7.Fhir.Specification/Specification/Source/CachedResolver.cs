/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) using a list of other IArtifactSources
    /// </summary>
    public class CachedResolver : IResourceResolver
    {
        public const int DEFAULT_CACHE_DURATION = 4 * 3600;     // 4 hours

        readonly Cache<Resource> _resourcesByUri;
        readonly Cache<Resource> _resourcesByCanonical;

        /// <summary>Artifact resolver decorator to cache loaded resources in memory.</summary>
        /// <param name="source">ArtifactSource that will be used to get data from on a cache miss</param>
        /// <param name="cacheDuration">Duration before trying to refresh the cache, in seconds</param>
        public CachedResolver(IResourceResolver source, int cacheDuration = DEFAULT_CACHE_DURATION)
        {
            Source = source;
            CacheDuration = cacheDuration;

            _resourcesByUri = new Cache<Resource>(id => InternalResolveByUri(id), CacheDuration);
            _resourcesByCanonical = new Cache<Resource>(id => InternalResolveByCanonicalUri(id), CacheDuration);
        }

        public IResourceResolver Source { get; private set; }

        public int CacheDuration { get; set; }

        public Resource ResolveByUri(string url)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));

            return _resourcesByUri.Get(url);
        }

        public Resource ResolveByCanonicalUri(string url)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));

            return _resourcesByCanonical.Get(url);
        }

        /// <summary>Remove the resource with the specified uri from the cache, if it exists.</summary>
        /// <param name="url">The resource uri.</param>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public bool InvalidateUri(string url) => _resourcesByUri.Invalidate(url);

        /// <summary>Remove the resource with the specified canonical uri from the cache, if it exists.</summary>
        /// <param name="url">The canonical resource uri.</param>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public bool InvalidateCanonicalUri(string url) => _resourcesByCanonical.Invalidate(url);

        /// <summary>Clear the cache by removing all existing cache entries.</summary>
        public void Clear()
        {
            _resourcesByUri.Clear();
            _resourcesByCanonical.Clear();
        }

        /// <summary>Event arguments for the <see cref="LoadResourceEventHandler"/> delegate.</summary>
        public class LoadResourceEventArgs : EventArgs
        {
            public LoadResourceEventArgs(string url, Resource resource) : base()
            {
                Url = url;
                Resource = resource;
            }

            /// <summary>Returns the url of the cached resource.</summary>
            public string Url { get; }

            /// <summary>Returns a reference to the cached resource.</summary>
            public Resource Resource { get; }
        }

        /// <summary>Handles the <see cref="Load"/> event that is fired when a new resources is loaded into the cache.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void LoadResourceEventHandler(object sender, LoadResourceEventArgs e);

        /// <summary>Occurs when a resource is loaded into the cache.</summary>
        public event LoadResourceEventHandler Load;

        protected virtual void OnLoad(string url, Resource resource) => Load?.Invoke(this, new LoadResourceEventArgs(url, resource));

        Resource InternalResolveByUri(string url)
        {
            var resource = Source.ResolveByUri(url);
            OnLoad(url, resource);
            return resource;
        }

        Resource InternalResolveByCanonicalUri(string url)
        {
            var resource = Source.ResolveByCanonicalUri(url);
            OnLoad(url, resource);
            return resource;
        }

        private class Cache<T>
        {
            readonly Func<string,T> _onCacheMiss;
            readonly int _duration;

            public Cache(Func<string,T> onCacheMiss, int duration)
            {
                _onCacheMiss = onCacheMiss;
                _duration = duration;
            }

           // private SynchronizedCollection<CacheEntry<T>> _cache = new SynchronizedCollection<CacheEntry<T>>();
            private Object getLock = new Object();
            private Dictionary<string, CacheEntry<T>> _cache = new Dictionary<string, CacheEntry<T>>();

            public T Get(string identifier)
            {
                lock (getLock)
                {
                    // Check the cache
                    CacheEntry<T> entry;
                    bool success = _cache.TryGetValue(identifier, out entry);
                    //var entry = _cache.TryGetValue(Where(ce => ce.Identifier == identifier).SingleOrDefault();

                    // Remove entry if it's too old
                    if (success && entry.Expired)
                    {
                        _cache.Remove(identifier);
                        entry = null;
                        // [WMR 20170406] Clear flag so we (try to) re-create the entry
                        success = false;
                    }

                    // If we still have a fresh entry, return it
                    if (success)
                    {
                        return entry.Data;
                    }
                    else
                    {
                        // Otherwise, fetch it and cache it.
                        T newData = default(T);

                        newData = _onCacheMiss(identifier);
                        // _cache.Add(identifier, new CacheEntry<T> { Data = newData, Identifier = identifier, Expires = DateTime.Now.AddSeconds(_duration) });
                        _cache.Add(identifier, new CacheEntry<T>(newData, identifier, DateTime.Now.AddSeconds(_duration)));

                        return newData;
                    }
                }
            }

            public bool Invalidate(string identifier)
            {
                lock (getLock)
                {
                    return _cache.Remove(identifier);
                }
            }

            public void Clear()
            {
                lock (getLock)
                {
                    _cache.Clear();
                }
            }
        }

        private class CacheEntry<T>
        {
            public readonly T Data;
            public readonly string Identifier;
            public readonly DateTime Expires;

            public CacheEntry(T data, string identifier, DateTime expires)
            {
                Data = data;
                Identifier = identifier;
                Expires = expires;
            }

            /// <summary>Returns a boolean value that indicates if the cache entry is expired.</summary>
            public bool Expired => DateTime.Now > Expires;
        }    
    }
}
