using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Hl7.Fhir.Specification.Tests
{
    internal class TestDataHelper
    {
        public static string GetFullPathForExample(string filename)
        {
            string location = typeof(TestDataHelper).GetTypeInfo().Assembly.Location;
            var path = Path.GetDirectoryName(location);
            return Path.Combine(path, "TestData", filename);
        }

        public static string ReadTestData(string filename)
        {
            string file = GetFullPathForExample(filename);
            return File.ReadAllText(file);
        }

        public static ZipArchive ReadTestZip(string filename)
        {
            string file = GetFullPathForExample(filename);
            return ZipFile.OpenRead(file);
        }

    }
}
