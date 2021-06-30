using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    /// <summary>
    /// All this is to do is read all the unit test data to ensure that they are all compatible with STU3
    /// (By just trying to de-serialize all the content)
    /// </summary>
    [TestClass]
    public class SpecificationTestDataVersionCheck
    {
        [TestMethod]
        public async Tasks.Task VerifyAllTestDataSpecification()
        {
            string location = typeof(TestDataHelper).GetTypeInfo().Assembly.Location;
            var path = Path.GetDirectoryName(location) + "\\TestData";
            Console.WriteLine(path);
            List <string> issues = new List<string>();
            await ValidateFolder(path, path, issues);
            Assert.AreEqual(0, issues.Count);
        }

        private async Tasks.Task ValidateFolder(string basePath, string path, List<string> issues)
        {
            if (path.Contains("grahame-validation-examples"))
                return;
            if (path.Contains("source-test"))
                return;
            if (path.Contains("Type Slicing"))
                return;
            if (path.Contains("validation-test-suite"))
                return;


            var xmlParser = new Hl7.Fhir.Serialization.FhirXmlParser();
            var jsonParser = new Serialization.FhirJsonParser();
            Console.WriteLine($"Validating test files in {path.Replace(basePath, "")}");
            foreach (var item in Directory.EnumerateFiles(path))
            {
                string content = File.ReadAllText(item);
                Hl7.Fhir.Model.Resource resource = null;
                try
                {
                    if (item.EndsWith(".dll"))
                        continue;
                    if (item.EndsWith(".exe"))
                        continue;
                    if (item.EndsWith(".pdb"))
                        continue;
                    if (item.EndsWith("manifest.json"))
                        continue;
                    if (new FileInfo(item).Extension == ".xml")
                    {
                        // Console.WriteLine($"    {item.Replace(path + "\\", "")}");
                        resource = await xmlParser.ParseAsync<Resource>(content);
                    }
                    else if (new FileInfo(item).Extension == ".json")
                    {
                        // Console.WriteLine($"    {item.Replace(path + "\\", "")}");
                        resource = await jsonParser.ParseAsync<Resource>(content);
                    }
                    else
                    {
                        Console.WriteLine($"    {item} (unknown content)");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"    {item} (parse error)");
                    Console.WriteLine($"        --> {ex.Message}");
                    issues.Add($"        --> {ex.Message}");
                }
            }
            foreach (var item in Directory.EnumerateDirectories(path))
            {
                await ValidateFolder(basePath, item, issues);
            }
        }

        private void RenameElement(XmlElement element, string oldValue, string newValue, XmlNamespaceManager nm)
        {
            var nodes = element.SelectNodes("fhir:" +oldValue, nm);
            foreach (XmlElement elem in nodes)
            {
                XmlElement n = element.OwnerDocument.CreateElement(newValue, "http://hl7.org/fhir");
                n.InnerXml = elem.InnerXml;
                foreach (XmlAttribute attr in elem.Attributes)
                    n.Attributes.Append(attr);
                element.InsertBefore(n, elem);
                element.RemoveChild(elem);
            }
        }
    }
}