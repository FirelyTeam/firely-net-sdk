using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Hl7.FhirPath.Tests
{
    /// <summary>
    /// All this is to do is read all the unit test data to ensure that they are all compatible with STU3
    /// (By just trying to de-serialize all the content)
    /// </summary>
    [TestClass]
    public class FhirPathTestDataVersionCheck
    {
        [TestMethod]
        public void VerifyAllTestDataFhirPath()
        {
            string location = typeof(FhirPathTest).GetTypeInfo().Assembly.Location;
            var path = Path.GetDirectoryName(location) + "\\TestData";
            Console.WriteLine(path);
            List <string> issues = new List<string>();
            ValidateFolder(path, path, issues);
            Assert.AreEqual(0, issues.Count);
        }

        private void ValidateFolder(string basePath, string path, List<string> issues)
        {
            if (path.Contains("grahame-validation-examples"))
                return;
            if (path.Contains("source-test"))
                return;

            var xmlParser = new Hl7.Fhir.Serialization.FhirXmlParser();
            var jsonParser = new Fhir.Serialization.FhirJsonParser();
            Console.WriteLine($"Validating test files in {path.Replace(basePath, "")}");
            foreach (var item in Directory.EnumerateFiles(path))
            {
                string content = File.ReadAllText(item);
                Hl7.Fhir.Model.Resource resource = null;
                try
                {
                    // Exclude the fhirpath unit test config files
                    if (item.Contains("csharp-tests"))
                        continue;
                    if (item.Contains("tests-fhir-r2"))
                        continue;
                    if (item.Contains("tests-fhir-r4"))
                        continue;

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
                        resource = xmlParser.Parse<Resource>(content);
                    }
                    else if (new FileInfo(item).Extension == ".json")
                    {
                        // Console.WriteLine($"    {item.Replace(path + "\\", "")}");
                        resource = jsonParser.Parse<Resource>(content);
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
                ValidateFolder(basePath, item, issues);
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