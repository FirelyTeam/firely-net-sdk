using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Tests
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
