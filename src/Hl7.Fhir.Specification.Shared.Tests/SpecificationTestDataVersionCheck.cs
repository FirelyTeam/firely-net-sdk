using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
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
    /// All this is to do is read all the unit test data to ensure that they are all compatible with R4
    /// (By just trying to de-serialize all the content)
    /// </summary>
    [TestClass]
    public partial class SpecificationTestDataVersionCheck
    {
        [TestMethod]
        public async Tasks.Task VerifyAllTestDataSpecification()
        {
            string location = typeof(TestDataHelper).GetTypeInfo().Assembly.Location;
            var path = Path.GetDirectoryName(location) + "/TestData";
            Console.WriteLine(path);
            List<string> issues = [];
            await ValidateFolder(path, path, issues);
            Assert.IsEmpty(issues);
        }

        private async Tasks.Task ValidateFolder(string basePath, string path, List<string> issues)
        {
            if (skipFiles(path)) return;

            var xmlParser = FhirXmlDeserializer.OSTRICH;
            var jsonParser = FhirJsonDeserializer.OSTRICH;
            Console.WriteLine($"Validating test files in {path.Replace(basePath, "")}");
            foreach (var item in Directory.EnumerateFiles(path))
            {
                if (skipFiles(item)) continue;

                string content = File.ReadAllText(item);

                try
                {
                    if (new FileInfo(item).Extension == ".xml")
                    {
                        // Console.WriteLine($"    {item.Replace(path + "\\", "")}");
                        xmlParser.Deserialize<Resource>(content);
                    }
                    else if (new FileInfo(item).Extension == ".json")
                    {
                        // Console.WriteLine($"    {item.Replace(path + "\\", "")}");
                        jsonParser.Deserialize<Resource>(content);
                    }
                    else
                    {
                        Console.WriteLine($"    {item} (unknown content)");
                    }
                }
                catch (Exception ex)
                {
                    if (new FileInfo(item).Extension == ".xml")
                    {
                        // migrate the content

                        var xmlDoc = new XmlDocument { PreserveWhitespace = true };
                        xmlDoc.LoadXml(content);
                        XmlNamespaceManager nm = new XmlNamespaceManager(xmlDoc.NameTable);
                        nm.AddNamespace("fhir", "http://hl7.org/fhir");
                        // RenameElement(xmlDoc.DocumentElement, "display", "title", nm);

                        // Update the context properties
                        XmlElement ctxt_type = xmlDoc.DocumentElement.SelectSingleNode("fhir:contextType", nm) as XmlElement;
                        if (ctxt_type != null)
                        {
                            xmlDoc.DocumentElement.RemoveChild(ctxt_type.NextSibling);
                            xmlDoc.DocumentElement.RemoveChild(ctxt_type);
                        }
                        foreach (XmlElement ctxt in xmlDoc.DocumentElement.SelectNodes("fhir:context", nm))
                        {
                            if (ctxt != null && ctxt_type != null)
                            {
                                XmlElement typ = ctxt.AppendChild(xmlDoc.CreateElement("type", "http://hl7.org/fhir")) as XmlElement;
                                switch (ctxt_type.GetAttribute("value"))
                                {
                                    case "resource":
                                        typ.SetAttribute("value", "fhirpath");
                                        break;
                                    case "datatype":
                                        typ.SetAttribute("value", "element");
                                        break;
                                    case "extension":
                                        typ.SetAttribute("value", "extension");
                                        break;
                                }
                                ctxt.InsertBefore(xmlDoc.CreateWhitespace("\r\n    "), typ);
                                string contextTypeValue = ctxt.GetAttribute("value");
                                if (!string.IsNullOrEmpty(contextTypeValue))
                                {
                                    ctxt.RemoveAttribute("value");
                                    XmlElement expr = ctxt.AppendChild(xmlDoc.CreateElement("expression", "http://hl7.org/fhir")) as XmlElement;
                                    expr.SetAttribute("value", contextTypeValue);
                                    ctxt.InsertBefore(xmlDoc.CreateWhitespace("\r\n    "), expr);
                                    ctxt.InsertAfter(xmlDoc.CreateWhitespace("\r\n  "), expr);
                                }
                            }
                        }

                        // Update all the Element Definitions
                        var elems = xmlDoc.DocumentElement.SelectNodes("(fhir:snapshot | fhir:differential)/fhir:element", nm);
                        foreach (XmlElement elem in elems)
                        {
                            foreach (XmlElement elemBinding in xmlDoc.SelectNodes("//fhir:binding", nm))
                            {
                                // replace the URI bindings
                                RenameElement(elemBinding, "valueSetUri", "valueSet", nm);

                                // replace the reference bindings
                                XmlElement vsr = elemBinding.SelectSingleNode("fhir:valueSetReference", nm) as XmlElement;
                                if (vsr != null)
                                {
                                    XmlElement refValue = vsr.SelectSingleNode("fhir:reference", nm) as XmlElement;
                                    string value = refValue.GetAttribute("value");
                                    vsr.RemoveAll();
                                    vsr.SetAttribute("value", value);
                                    RenameElement(elemBinding, "valueSetReference", "valueSet", nm);
                                }
                            }
                        }

                        try
                        {
                            // and parse this
                            xmlParser.Deserialize<Resource>(xmlDoc.OuterXml);
                            Console.WriteLine($"        conversion to {ModelInfo.Version} success {new FileInfo(item).Name}");

                            // Save this back to the filesystem since it works!
                            File.WriteAllText(item.Replace(@"bin\Debug\net462\", ""), xmlDoc.InnerXml);
                        }
                        catch (Exception ex3)
                        {
                            Console.WriteLine($"        conversion to {ModelInfo.Version} failed {new FileInfo(item).Name}");
                            Console.WriteLine($"            --> {ex3.Message}");
                            issues.Add($"        --> {ex.Message} (conversion failed too) {ex3.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"    {item} (JSON parse error)");
                        Console.WriteLine($"        --> {ex.Message}");
                        issues.Add($"        --> {ex.Message}");
                    }
                }
            }
            foreach (var item in Directory.EnumerateDirectories(path))
            {
                await ValidateFolder(basePath, item, issues);
            }
        }

        private void RenameElement(XmlElement element, string oldValue, string newValue, XmlNamespaceManager nm)
        {
            var nodes = element.SelectNodes("fhir:" + oldValue, nm);
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