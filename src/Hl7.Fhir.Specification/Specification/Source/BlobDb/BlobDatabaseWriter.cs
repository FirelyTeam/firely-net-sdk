using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    public class BlobDatabaseWriter : IDisposable
    {
        private readonly List<Index> _indices = new List<Index>();

        private FileStream _tempDataStream;

        private bool _buildCalled = false;
        private int _blobCount = 0;

        public string TempDataFilePath { get; private set; }        
        public string OutputFilePath { get; private set; }

        public BlobDatabaseWriter(string path)
        {
            OutputFilePath = path;
            TempDataFilePath = Path.GetTempFileName();
            
            _tempDataStream = new FileStream(TempDataFilePath, FileMode.Create, FileAccess.ReadWrite);
        }


        public void Add(Blob data, (string indexName, string key)[] keys) 
        {
            if (_buildCalled)
                throw new InvalidOperationException("Cannot add more data after Build() has been called. ");

            _blobCount += 1;
            var filePos = _tempDataStream.Position;     // if this does not work, have to calculate ourselves by keeping a tally

            foreach (var key in keys)
            {
                var newEntry = new IndexEntry(key.key, filePos);
                getOrCreateIndex(key.indexName).Add(newEntry);
            }

            _tempDataStream.WriteBlob(data);
            _tempDataStream.Flush();

            Index getOrCreateIndex(string name)
            {
                Index index = _indices.SingleOrDefault(i => i.Name == name);
                if (index == null)
                {
                    index = new Index(name);
                    _indices.Add(index);
                }

                return index;
            }
        }
      
        public void Build()
        {
            _buildCalled = true;

            // First, flush and write to make sure all blobs are written
            _tempDataStream.Flush();
            _tempDataStream.Seek(0, SeekOrigin.Begin);

            // Now, create the output file...
            using (var outputStream = new FileStream(OutputFilePath, FileMode.Create, FileAccess.Write))
            {
                long dataPosition = -1;

                //  Write the manifest....
                var indices = _indices.ToArray();

                dataPosition = outputStream.WriteManifest(1, _blobCount, _indices.ToArray());

                // ..and finally append the blob data
                outputStream.Seek(dataPosition, SeekOrigin.Begin);
                _tempDataStream.CopyTo(outputStream);
                outputStream.Flush();
            }

            _tempDataStream.Dispose();
            _tempDataStream = null;

            File.Delete(TempDataFilePath);
        }

        bool _disposed;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_tempDataStream != null)
                    {
                        _tempDataStream.Dispose();
                        _tempDataStream = null;
                    }
                }

                // release any unmanaged objects
                // set the object references to null

                _disposed = true;
            }
        }
    }
}
#endif