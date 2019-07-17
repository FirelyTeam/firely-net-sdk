using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
#if NET40
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;
#endif

namespace Hl7.Fhir.Tests
{
    internal class TestDataHelper
    {
        public static string GetFullPathForExample(string filename)
        {
            //string location = typeof(TestDataHelper).GetTypeInfo().Assembly.Location;
            //var path = Path.GetDirectoryName(location);
            //return Path.Combine(path, "TestData", filename);
            return Path.Combine("TestData",filename);
        }

        public static string ReadTestData(string filename)
        {
            string file = GetFullPathForExample(filename);
            return File.ReadAllText(file);
        }

#if NET40
        public static ZipArchive ReadTestZip(string filename)
        {
            string file = GetFullPathForExample(filename);
            return new ZipArchive(new ZipFile(file));
        }
#else
        public static ZipArchive ReadTestZip(string filename)
        {
            string file = GetFullPathForExample(filename);
            return ZipFile.OpenRead(file);
        }
#endif
    }

#if NET40
    internal class ZipArchiveEntry
    {
        private ZipFile _zip;
        private ZipEntry _zipEntry;

        public ZipArchiveEntry(ZipEntry zipEntry, ZipFile zipFile)
        {
            _zipEntry = zipEntry;
            _zip = zipFile;
        }

        public string Name
        {
            get
            {
                return _zipEntry.Name;
            }
        }

        public Stream Open()
        {
            return _zip.GetInputStream(_zipEntry);
        }
    }

    internal class ZipArchive : IDisposable
    {
        private ZipFile _zip;

        public ZipArchive(ZipFile zip)
        {
            _zip = zip;
        }

        public IEnumerable<ZipArchiveEntry> Entries
        {
            get
            {
                return _zip.Cast<ZipEntry>().Select(e => new ZipArchiveEntry(e, _zip));
            }
        }

        public void Dispose()
        {
            ((IDisposable)_zip).Dispose();
        }
    }
#endif
}
