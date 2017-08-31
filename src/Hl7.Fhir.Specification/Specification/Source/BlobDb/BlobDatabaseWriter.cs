using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    public class BlobDatabaseWriter : IDisposable
    {
        private readonly IndexCollection _indices = new IndexCollection();

        private BinaryWriter _dataFile;
        private bool _buildCalled = false;

        public string DataFilePath { get; private set; }
        public string IndexFilePath { get; private set; }
        public string OutputFilePath { get; private set; }

        public BlobDatabaseWriter(string path)
        {
            var name = Path.GetFileNameWithoutExtension(path);
            var baseDir = Path.GetDirectoryName(path);

            DataFilePath =  Path.Combine(baseDir, name + ".dat");
            IndexFilePath = Path.Combine(baseDir, name + ".idx");
            OutputFilePath = Path.Combine(baseDir, name + ".bin");

            _dataFile = new BinaryWriter(new FileStream(DataFilePath, FileMode.Create, FileAccess.Write), Encoding.UTF8);
        }


        public void Add(Blob data, (string indexName, string key, int position)[] keys) 
        {
            if (_buildCalled)
                throw new InvalidOperationException("Cannot add more data after Build() has been called. ");

            var filePos = _dataFile.BaseStream.Position;     // must be flushed I guess - if that does not work, have to calculate ourselves by keeping a tally

            foreach (var key in keys)
            {
                var entry = new IndexEntry(key.key, key.position);
                _indices.Add(key.indexName, entry);
            }

            _dataFile.WriteBlob(data);        
        }


        public void Build()
        {
            _buildCalled = true;

            writeIndices(_indices);

            if (_dataFile != null)
            {
                _dataFile.Flush();
                _dataFile.Dispose();
                _dataFile = null;
            }

            Utility.SerializationUtil.JoinFiles(new string[] { IndexFilePath, DataFilePath }, OutputFilePath);

        }

        private void writeIndices(IEnumerable<Index> indices)
        {
            using (var indexFile = new BinaryWriter(new FileStream(IndexFilePath, FileMode.Create, FileAccess.Write), Encoding.UTF8))
            {
                indexFile.WriteIndices(_indices);
                indexFile.Flush();
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
                if (_dataFile != null) _dataFile.Dispose();

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }
    }


}
#endif