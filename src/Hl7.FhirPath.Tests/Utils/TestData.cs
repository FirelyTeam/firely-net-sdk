using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Hl7.FhirPath.Tests 
{
    public static class TestData
    {
        public static string ReadTextFile(string name)
        {
            //   string location = typeof(TestData).GetTypeInfo().Assembly.Location;
            //  var path = Path.GetDirectoryName(location);
            //   string file = Path.Combine(path, "TestData", name);
            string file = Path.Combine("TestData", name);
            return File.ReadAllText(file);
        }

        public static string GetTestDataBasePath()
        {
            //  string location = typeof(TestData).GetTypeInfo().Assembly.Location;
            //  var path = Path.GetDirectoryName(location);
            //  return Path.Combine(path, "TestData");
            return "TestData";
        }
    }
}
