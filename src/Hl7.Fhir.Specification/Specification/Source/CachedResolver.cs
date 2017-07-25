/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using System.Collections.Generic;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Reads and caches FHIR artifacts (Profiles, ValueSets, ...) from an internal <see cref="IResourceResolver"/> instance.</summary>
    public class CachedResolver : IResourceResolver
    {
        /// <summary>Default expiration time for cached entries.</summary>
        public const int DEFAULT_CACHE_DURATION = 4 * 3600;     // 4 hours

        readonly Cache<Resource> _resourcesByUri;
        readonly Cache<Resource> _resourcesByCanonical;

        /// <summary>Creates a new artifact resolver that caches loaded resources in memory.</summary>
        /// <param name="source">Internal resolver from which artifacts are initially resolved on a cache miss.</param>
        /// <param name="cacheDuration">Default expiration time of a cache entry, in seconds.</param>
        public CachedResolver(IResourceResolver source, int cacheDuration = DEFAULT_CACHE_DURATION)
        {
            Source = source;
            CacheDuration = cacheDuration;

            _resourcesByUri = new Cache<Resource>(id => InternalResolveByUri(id), CacheDuration);
            _resourcesByCanonical = new Cache<Resource>(id => InternalResolveByCanonicalUri(id), CacheDuration);
        }

        /// <summary>Returns a reference to the internal artifact source.</summary>
        public IResourceResolver Source { get; }

        /// <summary>Gets the default expiration time of a cache entry.</summary>
        public int CacheDuration { get; }

        /// <summary>Retrieve the artifact with the specified url.</summary>
        /// <param name="url">The url of the target artifact.</param>
        /// <returns>A <see cref="Resource"/> instance, or <c>null</c> if unavailable.</returns>
        /// <remarks>Return data from memory cache if available, otherwise load on demand from the internal artifact source.</remarks>
        public Resource ResolveByUri(string url)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));
            return _resourcesByUri.Get(url, true);
        }

        /// <summary>Retrieve the conformance resource with the specified canonical url.</summary>
        /// <param name="url">The canonical url of the target conformance resource.</param>
        /// <returns>A conformance <see cref="Resource"/> instance, or <c>null</c> if unavailable.</returns>
        /// <remarks>Return data from memory cache if available, otherwise load on demand from the internal artifact source.</remarks>
        public Resource ResolveByCanonicalUri(string url)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));
            return _resourcesByCanonical.Get(url, true);
        }

        /// <summary>Clear the memory cache by removing all existing cache entries.</summary>
        public void Clear()
        {
            _resourcesByUri.Clear();
            _resourcesByCanonical.Clear();
        }

        /// <summary>Clear the cache entry for the artifact with the specified url, if it exists.</summary>
        /// <param name="url">The url of the target resource.</param>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public bool InvalidateByUri(string url)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));
            return _resourcesByUri.Invalidate(url);
        }

        /// <summary>Clear the cache entry for the conformance resource with the specified canonical uri, if it exists.</summary>
        /// <param name="url">The canonical url of the target conformance resource.</param>
        /// <returns><c>true</c> if succesful, <c>false</c> otherwise.</returns>
        public bool InvalidateByCanonicalUri(string url)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));
            return _resourcesByCanonical.Invalidate(url);
        }

        /// <summary>Retrieve the artifact with the specified url from memory cache, if present. Do not load on demand.</summary>
        /// <returns>A cached <see cref="Resource"/> instance, or <c>null</c>.</returns>
        /// <remarks>Does NOT load resource on demand from the internal artifact resolver.</remarks>
        public Resource GetCachedByUri(string url)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));
            return _resourcesByUri.Get(url, false);
        }

        /// <summary>Retrieve the conformance resource with the specified canonical url from memory cache, if present. Do not load on demand.</summary>
        /// <returns>A cached conformance <see cref="Resource"/> instance, or <c>null</c>.</returns>
        /// <remarks>Does NOT load conformance resource on demand from the internal artifact resolver.</remarks>
        public Resource GetCachedByCanonicalUri(string url)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));
            return _resourcesByCanonical.Get(url, false);
        }

        /// <summary>Determines if the memory cache contains a resource with the specified url.</summary>
        /// <returns><c>true</c> if the resource is cached, or <c>false</c> otherwise.</returns>
        public bool IsCachedUri(string url) => GetCachedByUri(url) != null;

        /// <summary>Determines if the memory cache contains a conformance resource with the specified canonical url.</summary>
        /// <returns><c>true</c> if the conformance resource is cached, or <c>false</c> otherwise.</returns>
        public bool IsCachedCanonicalUri(string url) => GetCachedByCanonicalUri(url) != null;

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
        public delegate void LoadResourceEventHandler(object sender, LoadResourceEventArgs e);

        /// <summary>Occurs when a artifact is loaded into the cache.</summary>
        public event LoadResourceEventHandler Load;

        /// <summary>Called when an artifact is loaded into the cache.</summary>
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

        // [WMR 20170724] Add class constraint so we can test if Data is initialized (!= null)
        private class Cache<T> where T : class
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

            // [WMR 20170725] Add loadOnDemand parameter
            public T Get(string identifier, bool loadOnDemand)
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
                    else if (loadOnDemand)
                    {
                        // Otherwise, fetch it and cache it.
                        T newData = default(T);

                        newData = _onCacheMiss(identifier);
                        // _cache.Add(identifier, new CacheEntry<T> { Data = newData, Identifier = identifier, Expires = DateTime.Now.AddSeconds(_duration) });
                        _cache.Add(identifier, new CacheEntry<T>(newData, identifier, DateTime.Now.AddSeconds(_duration)));

                        return newData;
                    }
                    return null; // default(T)
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

        // [WMR 20170724] Add class constraint so we can test if Data is initialized (!= null)
        private class CacheEntry<T> where T : class
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
