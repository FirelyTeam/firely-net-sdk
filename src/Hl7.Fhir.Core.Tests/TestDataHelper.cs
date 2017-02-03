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
        public static string ReadTestData(string filename)
        {
            string location = typeof(TestDataHelper).GetTypeInfo().Assembly.Location;
            var path = Path.GetDirectoryName(location);
            string file = Path.Combine(path, "TestData", filename);
            return File.ReadAllText(file);
        }

        public static ZipArchive ReadTestZip(string filename)
        {
            string location = typeof(TestDataHelper).GetTypeInfo().Assembly.Location;
            var path = Path.GetDirectoryName(location);
            string file = Path.Combine(path, "TestData", filename);

            return ZipFile.OpenRead(file);
        }

    }
}
