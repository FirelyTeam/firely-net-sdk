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
        public DateTimeOffset LastAccessed { get; private set; } = DateTimeOffset.Now;

        private V  _value;
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

    public class Cache<K, V>
    {
        private readonly ConcurrentDictionary<K, CacheItem<V>> _cached;
        private readonly int _minimumCacheSize;

        public CacheSettings Settings { get; private set; }

        public Func<K, V> Retrieve { get; }

        public Cache(Func<K, V> retrieveFunction) : this(retrieveFunction, CacheSettings.CreateDefault())  { }

        public Cache(Func<K, V> retrieveFunction, CacheSettings settings)
        {
            _cached = new ConcurrentDictionary<K, CacheItem<V>>();
            Retrieve = retrieveFunction;
            Settings = settings.Clone();
            _minimumCacheSize = (int)Math.Floor(Settings.MaxCacheSize * 0.9);
        }

        public V GetValue(K key)
        {
            if (!_cached.TryGetValue(key, out var result))
            {
                result = new CacheItem<V>() { Value = Retrieve(key) };
                _cached.TryAdd(key, result);
                EnforceMaxItems();
            }

            return result.Value;
        }

        private void EnforceMaxItems()
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