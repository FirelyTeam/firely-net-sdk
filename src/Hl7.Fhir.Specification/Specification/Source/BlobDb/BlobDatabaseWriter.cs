using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    public class BlobDatabaseWriter : IDisposable
    {
        private readonly IndexCollection _indices = new IndexCollection();

        private FileStream _dataStream;

        private bool _buildCalled = false;

        public string DataFilePath { get; private set; }        
        public string OutputFilePath { get; private set; }

        public BlobDatabaseWriter(string path)
        {
            var name = Path.GetFileNameWithoutExtension(path);
            var baseDir = Path.GetDirectoryName(path);

            DataFilePath =  Path.Combine(baseDir, name + ".dat");
            OutputFilePath = path;

            _dataStream = new FileStream(DataFilePath, FileMode.Create, FileAccess.Write);
        }


        public void Add(Blob data, (string indexName, string key, int position)[] keys) 
        {
            if (_buildCalled)
                throw new InvalidOperationException("Cannot add more data after Build() has been called. ");

            var filePos = _dataStream.Position;     // must be flushed I guess - if that does not work, have to calculate ourselves by keeping a tally

            foreach (var key in keys)
            {
                var entry = new IndexEntry(key.key, key.position);
                _indices.Add(key.indexName, entry);
            }

            _dataStream.WriteBlob(data);
            _dataStream.Flush();
        }


        public void Build()
        {
            _buildCalled = true;

            // First, flush and write to make sure all blobs are written
            _dataStream.Flush();
            _dataStream.Seek(0, SeekOrigin.Begin);

            // Now, create the output file...
            using (var outputStream = new FileStream(OutputFilePath, FileMode.Create, FileAccess.Write))
            {
                long dataPosition = -1;

                //  Write the manifest....
                dataPosition = outputStream.WriteManifest(1, _indices.ToArray());

                // ..and finally append the blob data
                outputStream.Seek(dataPosition, SeekOrigin.Begin);
                _dataStream.CopyTo(outputStream);
                outputStream.Flush();
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
                if (_dataStream != null) _dataStream.Dispose();

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }
    }


}
#endif