using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    public class BlobDatabase : IDisposable
    {
        private Header _header = null;
        private Dictionary<string, ILookup<string,IndexEntry>> _indices = new Dictionary<string, ILookup<string,IndexEntry>>();

        private Stream _dataStream = null;

        public BlobDatabase(string path)
        {
            _dataStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public Blob[] Get(string indexName, string key)
        {
            var result = new List<Blob>();

            if (_indices.TryGetValue(indexName, out var index))
            {
                var positions = index[key];

                foreach(var position in positions)
                {
                    var realPosition = position.Position + _header.DataBlockPosition;
                    _dataStream.Seek(realPosition, SeekOrigin.Begin);
                    var blob = _dataStream.ReadBlob();
                    result.Add(blob);
                }
            }

            return result.ToArray();
        }

        private bool _initialized = false;
        private void initialize()
        {
            if (_initialized) return;

            _header = _dataStream.ReadManifest(out var indices);
            foreach (var index in indices)
                _indices.Add(index.Name, index.ToLookup(ie => ie.Key));

            _initialized = true;
        }

        public string Dump()
        {
            initialize();

            var builder = new StringBuilder();

            builder.AppendLine($"=== BlobDb Dump ===");
            builder.AppendLine(_header.ToString());
            builder.AppendLine();

            builder.AppendLine("= Indices =");
            foreach (var index in _indices)
                builder.Append(index.ToString());
            builder.AppendLine();

            builder.AppendLine("= Blobs =");
            _dataStream.Seek(_header.DataBlockPosition, SeekOrigin.Begin);

            for(int i = 0; i < _header.BlobCount; i++)
            {
                var blob = _dataStream.ReadBlob();
                builder.AppendLine(blob.ToString());
            }

            return builder.ToString();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if(_dataStream != null)
                    {
                        _dataStream.Dispose();
                        _dataStream = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BlobDatabase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }


}
#endif