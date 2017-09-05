using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
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
            foreach(var index in this)
            {
                //builder.AppendLine("  " + index.ToString());
                builder.AppendLine($"  [{index.Key}] at relative position {index.Position}");
            }

            return builder.ToString();
        }
    }


    internal class IndexEntry : IComparable<IndexEntry>
    {
        public readonly string Key;
        public readonly long Position;

        public IndexEntry(string key, long position)
        {
            Key = key;
            Position = position;
        }

        public int CompareTo(IndexEntry other) => String.Compare(Key, other?.Key);

        public override bool Equals(object obj) => (obj is IndexEntry ie) ? Equals(ie) : false;

        public bool Equals(IndexEntry ie)
        {
            if (Object.ReferenceEquals(ie, null)) return false;
            if (Object.ReferenceEquals(ie, this)) return true;
            return this.Key == ie.Key && this.Position == ie.Position;
        }

        public override int GetHashCode() => (Key, Position).GetHashCode();

        public override string ToString()
        {
            return $"[{Key}] at relative position {Position}";
        }
    }
}
#endif