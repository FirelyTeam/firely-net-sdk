using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NET_COMPRESSION
namespace Hl7.Fhir.Support.FhirArchive
{
    internal class IndexCollection : IEnumerable<Index>
    {
        private readonly List<Index> _indices = new List<Index>();

        public void Add(string indexName, string value, string entryName)
        {
            var newIndexEntry = new IndexEntry(value, entryName);
            getOrCreateIndex(indexName).Add(newIndexEntry);
        }

        public IEnumerator<Index> GetEnumerator() => ((IEnumerable<Index>)_indices).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Index>)_indices).GetEnumerator();

        private Index getOrCreateIndex(string indexName)
        {
            Index index = _indices.SingleOrDefault(i => i.Name == indexName);
            if (index == null)
            {
                index = new Index(indexName);
                _indices.Add(index);
            }

            return index;
        }
    }

    internal class Index : List<IndexEntry>
    {
        public readonly string Name;

        public Index(string name) : base()
        {
            Name = name;
        }

        public override bool Equals(object obj) => (obj is Index ix) ? Equals(ix) : false;

        public bool Equals(Index ix)
        {
            if (Object.ReferenceEquals(ix, null)) return false;
            if (Object.ReferenceEquals(ix, this)) return true;
            return Name == ix.Name && this.SequenceEqual(ix);
        }

        public override int GetHashCode() => (Name, this).GetHashCode();

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Index '{Name}' contains {Count} entries:");
            foreach(var entry in this)
            {
                builder.AppendLine("  " + entry.ToString());
            }

            return builder.ToString();
        }
    }


    internal class IndexEntry : IComparable<IndexEntry>
    {
        public readonly string Value;
        public readonly string EntryName;

        public IndexEntry(string value, string entryName)
        {

            Value = value;
            EntryName = entryName;
        }

        public int CompareTo(IndexEntry other) => String.Compare(Value, other?.Value);

        public override bool Equals(object obj) => (obj is IndexEntry ie) ? Equals(ie) : false;

        public bool Equals(IndexEntry ie)
        {
            if (Object.ReferenceEquals(ie, null)) return false;
            if (Object.ReferenceEquals(ie, this)) return true;
            return this.Value == ie.Value && this.EntryName == ie.EntryName;
        }

        public override int GetHashCode() => (Value, EntryName).GetHashCode();

        public override string ToString()
        {
            return $"[{Value}] at entry {EntryName}";
        }
    }
}
#endif