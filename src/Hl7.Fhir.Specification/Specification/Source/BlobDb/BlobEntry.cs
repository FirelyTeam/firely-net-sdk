using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    public class Index
    {
        public readonly string Name;
        public readonly SortedSet<IndexEntry> Entries = new SortedSet<IndexEntry>(new IndexEntryComparer());

        public Index(string name)
        {
            Name = name;
        }

        public void Add(IndexEntry entry)
        {
            Entries.Add(entry);
        }

        private class IndexEntryComparer : IComparer<IndexEntry>
        {
            public int Compare(IndexEntry x, IndexEntry y) => String.Compare(x.Key, y.Key);
        }
    }

    public class IndexEntry
    {
        public readonly string Key;
        public readonly long Position;

        public IndexEntry(string key, long position)
        {
            Key = key;
            Position = position;
        }

    }

    public class Blob
    {
        public readonly string Data;

        public Blob(string data)
        {
            Data = data;
        }
    }

    public class BlobDatabase
    {
        public static BlobDatabase OpenFromFile(string path) => throw new NotImplementedException();

        public string Get(string indexName, string key) => throw new NotImplementedException();

        public string DumpInfo() => throw new NotImplementedException();
    }




    public class BlobDatabaseWriter : IDisposable
    {
        private BinaryWriter DataFile;
        private BinaryWriter IndexFile;
        private Dictionary<string, Index> Indices = new Dictionary<string, Index>();

        public static BlobDatabaseWriter Create(string path)
        {
            var name = Path.GetFileNameWithoutExtension(path);
            var dataFilePath = Path.Combine(Path.GetDirectoryName(path), name + ".dat");
            var indexFilePath = Path.Combine(Path.GetDirectoryName(path), name + ".idx");

            var result = new BlobDatabaseWriter();
            result.DataFile = new BinaryWriter(new FileStream(dataFilePath, FileMode.Create, FileAccess.Write), Encoding.UTF8);
            result.IndexFile = new BinaryWriter(new FileStream(indexFilePath, FileMode.Create, FileAccess.Write), Encoding.UTF8);

            return result;
        }


        private Index getIndex(string name)
        {
            if (!Indices.TryGetValue(name, out Index result))
            {
                result = new Index(name);
                Indices.Add(name, result);
            }

            return result;
        }

        public void Add(string data, IndexInfo[] indexInfo)
        {
            var blob = new Blob(data);
            var filePos = DataFile.BaseStream.Position;     // must be flushed I guess - if that does not work, have to calculate ourselves by keeping a tally


            foreach (var ii in indexInfo)
            {
                var entry = new IndexEntry(ii.Key, filePos);
                var index = getIndex(ii.IndexName);
                index.Add(entry);
            }

            DataFile.WriteBlob(blob);
        }

        public void Commit()
        {
            IndexFile.WriteIndices(Indices.Values);

            if (DataFile != null)
            {
                DataFile.Flush();
                DataFile.Dispose();
            }

            if (IndexFile != null)
            {
                IndexFile.Flush();
                IndexFile.Dispose();
            }
        }

        bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BlobDatabaseWriter()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (DataFile != null) DataFile.Dispose();
                if (IndexFile != null) IndexFile.Dispose();
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        public struct IndexInfo
        {
            public string IndexName;
            public string Key;
        }
    }

    internal static class BinaryWriterExtensions
    {
        public static void WriteBlob(this BinaryWriter writer, Blob b)
        {
            writer.Write(b.Data);
            writer.Flush();
        }

        public static void WriteIndices(this BinaryWriter writer, IEnumerable<Index> indices)
        {
            foreach (var index in indices)
                writer.WriteIndex(index);
        }

        public static void WriteIndex(this BinaryWriter writer, Index index)
        {
            writer.Write(index.Name);
            foreach (var entry in index.Entries)
                writeIndexEntry(entry);
            writer.Flush();

            void writeIndexEntry(IndexEntry e)
            {
                writer.Write(e.Key);
                writer.Write(e.Position);
            }
        }

    }


}
#endif