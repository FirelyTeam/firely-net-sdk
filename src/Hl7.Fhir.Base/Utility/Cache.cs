/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Utility
{
    internal class CacheItem<V>
    {
        public CacheItem(V value) => _value = value;

        public DateTimeOffset LastAccessed { get; private set; } = DateTimeOffset.Now;

        private V _value;
        internal V Value
        {
            get
            {
                LastAccessed = DateTimeOffset.Now;
                return _value;
            }
            set
            {
                _value = value;
                if (value == null)
                {
                    throw new ArgumentException($"Do not set the {nameof(Value)} to null.");
                }
            }
        }
    }

    public class CacheSettings
    {
        public int MaxCacheSize { get; set; } = 500;
        public static CacheSettings CreateDefault() => new CacheSettings();

        public CacheSettings() { }

        public CacheSettings(CacheSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        private void CopyTo(CacheSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.MaxCacheSize = MaxCacheSize;
        }

        public CacheSettings Clone() => new CacheSettings(this);
    }


    public class Cache<K, V> where V : class
    {
        private readonly ConcurrentDictionary<K, CacheItem<V>> _cached;
        private readonly int _minimumCacheSize;
        private readonly Func<K, CacheItem<V>> _retriever;

        /// <summary>
        /// The settings for changing the behaviour of the case. Passed into the constructor and readonly here.
        /// </summary>
        public CacheSettings Settings { get; private set; }

        /// <summary>
        /// The function that will be called when a cache miss is detected. If null, cache misses result in 
        /// a returned value of null for the given key.
        /// </summary>
        public Func<K, V> Retrieve { get; }

        public Cache() : this(CacheSettings.CreateDefault()) { }

        public Cache(Func<K, V> retrieveFunction) : this(retrieveFunction, CacheSettings.CreateDefault()) { }

        public Cache(CacheSettings settings) : this(null, CacheSettings.CreateDefault()) { }

        public Cache(Func<K, V> retrieveFunction, CacheSettings settings)
        {
            if (settings is null) throw new ArgumentNullException(nameof(settings));

            _cached = new ConcurrentDictionary<K, CacheItem<V>>();
            Retrieve = retrieveFunction;
            _retriever = Retrieve != null ?
                key => new CacheItem<V>(retrieveFunction(key))
                : default(Func<K, CacheItem<V>>);
            Settings = settings.Clone();
            _minimumCacheSize = (int)Math.Floor(Settings.MaxCacheSize * 0.9);
        }

        /// <summary>
        /// Retrieves a value from the cahce by key. If missing, the retrieve function passed to the constructor will be called.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The requested value. If there is no retriever function set, this may be <c>null</c>.</returns>
        public V GetValue(K key)
        {
            if (_retriever == null)
            {
                return _cached.TryGetValue(key, out var foundItem) ? foundItem.Value : null;
            }
            else
            {
                var cachedItem = _cached.GetOrAdd(key, _retriever);
                enforceMaxItems();
                return cachedItem.Value;
            }
        }

        /// <summary>
        /// Retrieves a value from the cache by key. If missing, the passed <paramref name="value"/> is returned and added to the cache.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public V GetValueOrAdd(K key, V value)
        {
            var cachedItem = _cached.GetOrAdd(key, new CacheItem<V>(value));
            enforceMaxItems();

            return cachedItem.Value;
        }

        private void enforceMaxItems()
        {
            var currentCount = _cached.Count();
            if (currentCount > Settings.MaxCacheSize)
            {
                // first copy the key value pairs in an array. Otherwise we could have a race condition. See for more information:
                // https://stackoverflow.com/questions/11692389/getting-argument-exception-in-concurrent-dictionary-when-sorting-and-displaying
                var copy = _cached.ToArray();
                var oldestItems = copy.OrderByDescending(entry => entry.Value.LastAccessed).Skip(_minimumCacheSize);
                foreach (var item in oldestItems)
                {
                    _cached.TryRemove(item.Key, out _);
                }
            }
        }
    }
}