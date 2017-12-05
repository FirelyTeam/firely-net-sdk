// [WMR 20171011] OBSOLETE; see ArtifactScannerTests
#if false

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Serialization;
using System.IO;
using Hl7.Fhir.Specification.Source;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class ConformanceScannerTests
    {
        private (Bundle b, Resource r, Patient p) makeTestData()
        {
            var b = new Bundle
            {
                Entry = new List<Bundle.EntryComponent>
                {
                    new Bundle.EntryComponent
                    {
                        FullUrl = "http://test.org/StructureDefinition/sd",
                        Resource = new StructureDefinition { Url = "http://test.org/StructureDefinition/sd" }
                    },

                    new Bundle.EntryComponent
                    {
                        FullUrl = "http://test.org/ValueSet/vs",
                        Resource = new ValueSet { Url = "http://test.org/ValueSet/vs",
                                CodeSystem = new ValueSet.CodeSystemComponent { System = "http://test.org/vs/testsystem"} }
                    },

                    new Bundle.EntryComponent
                    {
                        FullUrl = "http://test.org/NamingSystem/ns",
                        Resource = new NamingSystem
                        {
                            UniqueId = new List<NamingSystem.UniqueIdComponent>
                            {
                                new NamingSystem.UniqueIdComponent { Value = "http://test.org/ns/testname1" },
                                new NamingSystem.UniqueIdComponent { Value = "http://test.org/ns/testname2" },
                            }
                        }
                    },

                    new Bundle.EntryComponent
                    {
                        FullUrl = "http://test.org/ConceptMap/cm",
                        Resource = new ConceptMap { Url = "http://test.org/ConceptMap/cm",
                            Source = new FhirUri("http://test.org/source"),
                            Target = new ResourceReference { Reference = "http://test.org/target"} }
                    },

                    new Bundle.EntryComponent
                    {
                        // No full url, should be skipped
                        Resource = new ConceptMap { Url = "http://test.org/ConceptMap/cm",
                            Source = new FhirUri("http://test.org/source"),
                            Target = new ResourceReference { Reference = "http://test.org/target"} }
                    },
                }
            };

            var r = b.Entry[0].Resource;

            var p = new Patient { Id = "1234" };

            return (b, r, p);
        }

        private void assertBundle(IConformanceScanner scanner, string origin)
        {
            var list = scanner.List();
            Assert.AreEqual(4, list.Count);

            Assert.AreEqual(ResourceType.StructureDefinition, list[0].ResourceType);
            Assert.AreEqual("http://test.org/StructureDefinition/sd", list[0].ResourceUri);
            Assert.AreEqual("http://test.org/StructureDefinition/sd", list[0].Canonical);
            Assert.AreEqual(origin, list[0].Origin);

            Assert.AreEqual(ResourceType.ValueSet, list[1].ResourceType);
            Assert.AreEqual("http://test.org/ValueSet/vs", list[1].ResourceUri);
            Assert.AreEqual("http://test.org/ValueSet/vs", list[1].Canonical);
            Assert.AreEqual("http://test.org/vs/testsystem", list[1].ValueSetSystem);
            Assert.AreEqual(origin, list[1].Origin);

            Assert.AreEqual(ResourceType.NamingSystem, list[2].ResourceType);
            Assert.AreEqual("http://test.org/NamingSystem/ns", list[2].ResourceUri);
            Assert.AreEqual("http://test.org/ns/testname1", list[2].UniqueIds.First());
            Assert.AreEqual("http://test.org/ns/testname2", list[2].UniqueIds.Skip(1).First());
            Assert.AreEqual(origin, list[2].Origin);

            Assert.AreEqual(ResourceType.ConceptMap, list[3].ResourceType);
            Assert.AreEqual("http://test.org/ConceptMap/cm", list[3].ResourceUri);
            Assert.AreEqual("http://test.org/ConceptMap/cm", list[3].Canonical);
            Assert.AreEqual("http://test.org/source", list[3].ConceptMapSource);
            Assert.AreEqual("http://test.org/target", list[3].ConceptMapTarget);
            Assert.AreEqual(origin, list[3].Origin);
        }

        internal FhirXmlSerializer FhirXmlSerializer = new FhirXmlSerializer();
        internal FhirJsonSerializer FhirJsonSerializer = new FhirJsonSerializer();


        [TestMethod]
        public void TestBundle()
        {
            var tmp = Path.GetTempFileName();
            var tmpx = tmp + ".xml";
            var tmpj = tmp + ".json";

            (Bundle b, var _, var __) = makeTestData();
            var bXml = FhirXmlSerializer.SerializeToString(b);
            var bJson = FhirJsonSerializer.SerializeToString(b);

            File.WriteAllText(tmpx, bXml);
            File.WriteAllText(tmpj, bJson);

            var x = new XmlFileConformanceScanner(tmpx);
            var j = new JsonFileConformanceScanner(tmpj);

            assertBundle(x, tmpx);
            assertBundle(j, tmpj);

            doiets(bJson);
        }

        private void assertSingle(IConformanceScanner scanner, string origin)
        {
            var list = scanner.List();
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(ResourceType.StructureDefinition, list[0].ResourceType);
            Assert.AreEqual("http://test.org/StructureDefinition/sd", list[0].ResourceUri);
            Assert.AreEqual("http://test.org/StructureDefinition/sd", list[0].Canonical);
            Assert.AreEqual(origin, list[0].Origin);
        }

        [TestMethod]
        public void TestSingle()
        {
            var tmp = Path.GetTempFileName();
            var tmpx = tmp + ".xml";
            var tmpj = tmp + ".json";

            (_, Resource r, var __) = makeTestData();
            var rXml = FhirXmlSerializer.SerializeToString(r);
            var rJson = FhirJsonSerializer.SerializeToString(r);

            File.WriteAllText(tmpx, rXml);
            File.WriteAllText(tmpj, rJson);

            var x = new XmlFileConformanceScanner(tmpx);
            var j = new JsonFileConformanceScanner(tmpj);

            assertSingle(x, tmpx);
            assertSingle(j, tmpj);
        }

        private void assertExample(IConformanceScanner scanner, string origin)
        {
            var list = scanner.List();
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(ResourceType.Patient, list[0].ResourceType);
            Assert.AreEqual("http://example.org/Patient/1234", list[0].ResourceUri);
            Assert.IsNull(list[0].Canonical);
            Assert.AreEqual(origin, list[0].Origin);
        }


        [TestMethod]
        public void TestExample()
        {
            var tmp = Path.GetTempFileName();
            var tmpx = tmp + ".xml";
            var tmpj = tmp + ".json";

            (var _, var __, Patient p) = makeTestData();
            var rXml = FhirXmlSerializer.SerializeToString(p);
            var rJson = FhirJsonSerializer.SerializeToString(p);

            File.WriteAllText(tmpx, rXml);
            File.WriteAllText(tmpj, rJson);

            var x = new XmlFileConformanceScanner(tmpx);
            var j = new JsonFileConformanceScanner(tmpj);

            assertExample(x, tmpx);
            assertExample(j, tmpj);
        }

        private void doiets(string json)
        {
            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                reader.SupportMultipleContent = true;

                var serializer = new JsonSerializer();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        var p = reader.Path;
                        if (Regex.IsMatch(p, @"^entry\[(\d+)\]\.resource$"))
                        {
                            var r = JObject.ReadFrom(reader);
                        }
                    }
                }
            }
        }
    }
}

#endif