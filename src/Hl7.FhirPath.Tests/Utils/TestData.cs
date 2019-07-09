using System.IO;

namespace Hl7.FhirPath.Tests
{
    public static class TestData
    {
        public static string ReadTextFile(string name)
        {
            string file = Path.Combine("TestData", name);
            return File.ReadAllText(file);
        }

        public static string GetTestDataBasePath()
        {
            return "TestData";
        }
    }
}
