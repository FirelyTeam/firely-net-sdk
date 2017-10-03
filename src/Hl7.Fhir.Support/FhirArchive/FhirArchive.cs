using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#if NET_COMPRESSION
using System.IO.Compression;

namespace Hl7.Fhir.Support.FhirArchive
{
    public partial class FhirArchive : IDisposable
    {
        private bool _buildCalled = false;

#if NET_FILESYSTEM
        private FileStream _fs = null;
        private ZipArchive _archive = null;

        public FhirArchive(string path)
        {          
            _fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            open(_fs);
        }
#endif

        public FhirArchive(Stream stream)
        {
            if (!stream.CanWrite)
                throw Error.InvalidOperation("Stream must be writeable");

            open(stream);
        }


        private void open(Stream s)
        {
            _archive = new ZipArchive(s, ZipArchiveMode.Create, leaveOpen: true);
        }


        private readonly List<EntrySummary> _summaries = new List<EntrySummary>();


        public void Add(string data, string name, IEnumerable<SummaryItem> summary)
        {
            var binaryData = Encoding.UTF8.GetBytes(data);

            using (var sw = new MemoryStream(binaryData))
                Add(sw, name, summary);
        }

        public void Add(Stream data, string name, IEnumerable<SummaryItem> summary) 
        {
            if (_buildCalled)
                throw new InvalidOperationException("Cannot add more data after Build() has been called. ");

            // Add the data to the zip archive
            addNewEntry(name, data);

            // Update list of summaries by adding one for this new entry
            var newSummary = new EntrySummary { EntryName = name, Summary = summary.ToList() };
            _summaries.Add(newSummary);

            // Update the indices to include this entry
            foreach (var item in newSummary.Summary.Where(md => md.Indexed == true))
                _indices.Add(item.Name, item.Value, newSummary.EntryName);
        }

        private ZipArchiveEntry addNewEntry(string entryName, Stream data)
        {
            var newZipEntry = _archive.CreateEntry(entryName);

            using (var writer = newZipEntry.Open())
            {
                data.CopyTo(writer);
            }

            return newZipEntry;
        }


        private readonly IndexCollection _indices = new IndexCollection();

        public void Build()
        {
            _buildCalled = true;

            // Write all metadata in directory __meta
            foreach (var meta in _summaries)
            {
                var metaEntryName = "__summary/" + meta.EntryName + ".json";
                var metaEntry = _archive.CreateEntry(metaEntryName);

                using (var writer = metaEntry.Open())
                {
                    using (var stringWriter = new StreamWriter(writer))
                        stringWriter.WriteLine(meta.ToJson());
                }
            }

            // Write all indices in directory __index
            foreach(var index in _indices)
            {
                var indexEntryName = "__index/" + index.Name + ".json";
                var indexEntry = _archive.CreateEntry(indexEntryName);
                index.Sort();

                using (var writer = indexEntry.Open())
                {
                    using (var stringWriter = new StreamWriter(writer))
                        stringWriter.WriteLine(index.ToJson());
                }
            }
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
                    if (_archive != null)
                    {
                        _archive.Dispose();
                        _archive = null;
                    }

#if NET_FILESYSTEM
                    if(_fs != null)
                    {
                        _fs.Dispose();
                        _fs = null;
                    }
#endif
                }

                // release any unmanaged objects
                // set the object references to null

                _disposed = true;
            }
        }
    }
}
#endif
