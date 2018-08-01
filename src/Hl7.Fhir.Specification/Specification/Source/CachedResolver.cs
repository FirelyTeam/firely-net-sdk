/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using System.Collections.Generic;
using Hl7.Fhir.Utility;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Defines options for the <see cref="CachedResolver"/> that determine how to load a resource.</summary>
    public enum CachedResolverLoadingStrategy
    {
        /// <summary>Return from cache, if present. Otherwise (re-)load from source and update cache.</summary>
        LoadOnDemand = 0,
        /// <summary>Return from cache, if present. Do NOT (re-)load from source. Do NOT update cache.</summary>
        LoadFromCache = 1,
        /// <summary>Force (re-)load from source and update cache.</summary>
        LoadFromSource = 2
    }

    /// <summary>Reads and caches FHIR artifacts (Profiles, ValueSets, ...) from an internal <see cref="IResourceResolver"/> instance.</summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
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
            Source = source ?? throw Error.ArgumentNull(nameof(source));
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
            return _resourcesByUri.Get(url, CachedResolverLoadingStrategy.LoadOnDemand);
        }

        /// <summary>Retrieve the conformance resource with the specified canonical url.</summary>
        /// <param name="url">The canonical url of the target conformance resource.</param>
        /// <returns>A conformance <see cref="Resource"/> instance, or <c>null</c> if unavailable.</returns>
        /// <remarks>Return data from memory cache if available, otherwise load on demand from the internal artifact source.</remarks>
        public Resource ResolveByCanonicalUri(string url)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));
            return _resourcesByCanonical.Get(url, CachedResolverLoadingStrategy.LoadOnDemand);
        }

        /// <summary>Retrieve the artifact with the specified url.</summary>
        /// <param name="url">The url of the target artifact.</param>
        /// <param name="strategy">Option flag to control the loading strategy.</param>
        /// <returns>A <see cref="Resource"/> instance, or <c>null</c> if unavailable.</returns>
        /// <remarks>Return data from memory cache if available, otherwise load on demand from the internal artifact source.</remarks>
        public Resource ResolveByUri(string url, CachedResolverLoadingStrategy strategy)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));
            return _resourcesByUri.Get(url, strategy);
        }

        /// <summary>Retrieve the conformance resource with the specified canonical url.</summary>
        /// <param name="url">The canonical url of the target conformance resource.</param>
        /// <param name="strategy">Option flag to control the loading strategy.</param>
        /// <returns>A conformance <see cref="Resource"/> instance, or <c>null</c> if unavailable.</returns>
        /// <remarks>Return data from memory cache if available, otherwise load on demand from the internal artifact source.</remarks>
        public Resource ResolveByCanonicalUri(string url, CachedResolverLoadingStrategy strategy)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));
            return _resourcesByCanonical.Get(url, strategy);
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

        /// <summary>Clear the memory cache by removing all existing cache entries.</summary>
        public void Clear()
        {
            _resourcesByUri.Clear();
            _resourcesByCanonical.Clear();
        }

        /// <summary>Determines if the memory cache contains a resource with the specified url.</summary>
        /// <returns><c>true</c> if the resource is cached, or <c>false</c> otherwise.</returns>
        public bool IsCachedUri(string url) => ResolveByUri(url, CachedResolverLoadingStrategy.LoadFromCache) != null;

        /// <summary>Determines if the memory cache contains a conformance resource with the specified canonical url.</summary>
        /// <returns><c>true</c> if the conformance resource is cached, or <c>false</c> otherwise.</returns>
        public bool IsCachedCanonicalUri(string url) => ResolveByCanonicalUri(url, CachedResolverLoadingStrategy.LoadFromCache) != null;

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

        // Allow derived classes to override
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal protected virtual string DebuggerDisplay
            => $"{GetType().Name} for {Source.DebuggerDisplayString()}";


        private class Cache<T>
        {
            readonly Func<string,T> _onCacheMiss;
            readonly int _duration;

            readonly Object _getLock = new Object();
            readonly Dictionary<string, CacheEntry<T>> _cache = new Dictionary<string, CacheEntry<T>>();

            public Cache(Func<string,T> onCacheMiss, int duration)
            {
                _onCacheMiss = onCacheMiss;
                _duration = duration;
            }

            public T Get(string identifier, CachedResolverLoadingStrategy strategy)
            {
                lock (_getLock)
                {
                    var cache = _cache;

                    // Check the cache
                    if (strategy != CachedResolverLoadingStrategy.LoadFromSource)
                    {
                        if (cache.TryGetValue(identifier, out CacheEntry<T> entry))
                        {
                            // If we still have a fresh entry, return it
                            if (!entry.IsExpired)
                            {
                                return entry.Data;
                            }

                            // Remove entry if it's too old
                            cache.Remove(identifier);
                        }
                    }

                    // Load from source
                    if (strategy != CachedResolverLoadingStrategy.LoadFromCache)
                    {
                        // Otherwise, fetch it and cache it.
                        T newData = _onCacheMiss(identifier);

                        // Add new entry or update existing entry
                        cache[identifier] = new CacheEntry<T>(newData, identifier, DateTime.Now.AddSeconds(_duration));

                        return newData;
                    }

                    return default(T);
                }
            }

            public bool Invalidate(string identifier)
            {
                lock (_getLock)
                {
                    return _cache.Remove(identifier);
                }
            }

            public void Clear()
            {
                lock (_getLock)
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
            public bool IsExpired => DateTime.Now > Expires;
        }
    }
}
