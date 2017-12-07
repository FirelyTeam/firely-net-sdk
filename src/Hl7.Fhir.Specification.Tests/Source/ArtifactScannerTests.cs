#if false

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
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
    [Obsolete("ArtifactScanner is obsolete, instead use INavigatorStream")]
    public class ArtifactScannerTests
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

        private void assertBundle(ArtifactScanner scanner, string origin)
        {
            var list = scanner.List();
            Assert.AreEqual(4, list.Count);

            Assert.AreEqual(ResourceType.StructureDefinition, list[0].ResourceType);
            Assert.AreEqual("http://test.org/StructureDefinition/sd", list[0].ResourceUri);
            var crs = list[0] as ConformanceResourceSummary;
            Assert.IsNotNull(crs);
            Assert.AreEqual("http://test.org/StructureDefinition/sd", crs.Canonical);
            Assert.AreEqual(origin, list[0].Origin);

            Assert.AreEqual(ResourceType.ValueSet, list[1].ResourceType);
            Assert.AreEqual("http://test.org/ValueSet/vs", list[1].ResourceUri);
            var vss = list[1] as ValueSetSummary;
            Assert.IsNotNull(vss);
            Assert.AreEqual("http://test.org/ValueSet/vs", vss.Canonical);
            Assert.AreEqual("http://test.org/vs/testsystem", vss.ValueSetSystem);
            Assert.AreEqual(origin, list[1].Origin);

            Assert.AreEqual(ResourceType.NamingSystem, list[2].ResourceType);
            Assert.AreEqual("http://test.org/NamingSystem/ns", list[2].ResourceUri);
            var nss = list[2] as NamingSystemSummary;
            Assert.IsNotNull(nss);
            Assert.AreEqual("http://test.org/ns/testname1", nss.UniqueIds.First());
            Assert.AreEqual("http://test.org/ns/testname2", nss.UniqueIds.Skip(1).First());
            Assert.AreEqual(origin, list[2].Origin);

            Assert.AreEqual(ResourceType.ConceptMap, list[3].ResourceType);
            Assert.AreEqual("http://test.org/ConceptMap/cm", list[3].ResourceUri);
            var cms = list[3] as ConceptMapSummary;
            Assert.IsNotNull(cms);
            Assert.AreEqual("http://test.org/ConceptMap/cm", cms.Canonical);
            Assert.AreEqual("http://test.org/source", cms.ConceptMapSource);
            Assert.AreEqual("http://test.org/target", cms.ConceptMapTarget);
            Assert.AreEqual(origin, list[3].Origin);
        }

        [TestMethod]
        public void TestBundle()
        {
            var tmp = Path.GetTempFileName();
            var tmpx = tmp + ".xml";
            var tmpj = tmp + ".json";

            (Bundle b, var _, var __) = makeTestData();
            var bXml = FhirSerializer.SerializeResourceToXml(b);
            var bJson = FhirSerializer.SerializeResourceToJson(b);

            File.WriteAllText(tmpx, bXml);
            File.WriteAllText(tmpj, bJson);

            var x = new XmlArtifactScanner(tmpx, DefaultArtifactSummaryHarvester.Harvest);
            var j = new JsonArtifactScanner(tmpj, DefaultArtifactSummaryHarvester.Harvest);

            assertBundle(x, tmpx);
            assertBundle(j, tmpj);

            doiets(bJson);
        }

        private void assertSingle(ArtifactScanner scanner, string origin)
        {
            var list = scanner.List();
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(ResourceType.StructureDefinition, list[0].ResourceType);
            Assert.AreEqual("http://test.org/StructureDefinition/sd", list[0].ResourceUri);
            var crs = list[0] as ConformanceResourceSummary;
            Assert.IsNotNull(crs);
            Assert.AreEqual("http://test.org/StructureDefinition/sd", crs.Canonical);
            Assert.AreEqual(origin, list[0].Origin);
        }

        [TestMethod]
        public void TestSingle()
        {
            var tmp = Path.GetTempFileName();
            var tmpx = tmp + ".xml";
            var tmpj = tmp + ".json";

            (_, Resource r, var __) = makeTestData();
            var rXml = FhirSerializer.SerializeResourceToXml(r);
            var rJson = FhirSerializer.SerializeResourceToJson(r);

            File.WriteAllText(tmpx, rXml);
            File.WriteAllText(tmpj, rJson);

            var x = new XmlArtifactScanner(tmpx, DefaultArtifactSummaryHarvester.Harvest);
            var j = new JsonArtifactScanner(tmpj, DefaultArtifactSummaryHarvester.Harvest);

            assertSingle(x, tmpx);
            assertSingle(j, tmpj);
        }

        private void assertExample(ArtifactScanner scanner, string origin)
        {
            var list = scanner.List();
            Assert.AreEqual(1, list.Count);

            Assert.AreEqual(ResourceType.Patient, list[0].ResourceType);
            Assert.AreEqual("http://example.org/Patient/1234", list[0].ResourceUri);
            Assert.AreEqual(typeof(ArtifactSummary), list[0].GetType());
            Assert.AreEqual(origin, list[0].Origin);
        }


        [TestMethod]
        public void TestExample()
        {
            var tmp = Path.GetTempFileName();
            var tmpx = tmp + ".xml";
            var tmpj = tmp + ".json";

            (var _, var __, Patient p) = makeTestData();
            var rXml = FhirSerializer.SerializeResourceToXml(p);
            var rJson = FhirSerializer.SerializeResourceToJson(p);

            File.WriteAllText(tmpx, rXml);
            File.WriteAllText(tmpj, rJson);

            var x = new XmlArtifactScanner(tmpx, DefaultArtifactSummaryHarvester.Harvest);
            var j = new JsonArtifactScanner(tmpj, DefaultArtifactSummaryHarvester.Harvest);

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
