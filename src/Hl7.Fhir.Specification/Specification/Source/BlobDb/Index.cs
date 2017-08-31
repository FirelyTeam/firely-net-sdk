using System;
using System.Collections;
using System.Collections.Generic;

#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    internal class Index : SortedSet<IndexEntry>
    {
        public readonly string Name;

        public Index(string name) : base(new IndexEntryComparer())
        {
            Name = name;
        }

        private class IndexEntryComparer : IComparer<IndexEntry>
        {
            public int Compare(IndexEntry x, IndexEntry y) => String.Compare(x.Key, y.Key);
        }
    }


    internal class IndexEntry
    {
        public readonly string Key;
        public readonly long Position;

        public IndexEntry(string key, long position)
        {
            Key = key;
            Position = position;
        }
    }

    internal class IndexCollection : IEnumerable<Index>
    {
        private Dictionary<string, Index> _indices = new Dictionary<string, Index>();

        public Index this[string name]
        {
            get
            {
                if (!_indices.TryGetValue(name, out Index result))
                {
                    result = new Index(name);
                    _indices.Add(name, result);
                }

                return result;
            }

            set
            {
                _indices[name] = value;
            }
        }

        public void Add(string name, IndexEntry entry)
        {
            var index = this[name];
            index.Add(entry);
        }

        IEnumerator<Index> IEnumerable<Index>.GetEnumerator() => _indices.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _indices.Values.GetEnumerator();
    }


}
#endif