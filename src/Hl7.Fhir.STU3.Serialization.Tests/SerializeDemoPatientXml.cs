using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class SerializeDemoPatientXml
    {
        public ITypedElement getXmlElement(string xml, FhirXmlParsingSettings s = null) =>
            XmlParsingHelpers.ParseToTypedElement(xml, new PocoStructureDefinitionSummaryProvider(), s);
        public async Tasks.Task<ITypedElement> getJsonElement(string json, FhirJsonParsingSettings s = null) =>
            await JsonParsingHelpers.ParseToTypedElementAsync(json, new PocoStructureDefinitionSummaryProvider(), settings: s);


        [TestMethod]
        public async Tasks.Task CanSerializeThroughNavigatorAndCompare()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlElement(tpXml);
            var output = await nav.ToXmlAsync();
            XmlAssert.AreSame("fp-test-patient.xml", tpXml, output, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public void TestPruneEmptyNodes()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "test-empty-nodes.xml"));

            // Make sure permissive parsing is on - otherwise the parser will complain about all those empty nodes
            var nav = getXmlElement(tpXml, new FhirXmlParsingSettings { PermissiveParsing = true });
            var doc = nav.ToXDocument().Root;
            Assert.AreEqual(10, doc.DescendantNodesAndSelf().Count());  // only 8 nodes + 2 comments left after pruning
        }

        [TestMethod]
        public void TestElementReordering()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "patient-out-of-order.xml"));
            var nav = getXmlElement(tpXml, new FhirXmlParsingSettings { PermissiveParsing = true });  // since the order is incorrect
            var root = nav.ToXDocument().Root;

            var orderedNames = root.Elements().Select(e => e.Name.LocalName).ToList();
            CollectionAssert.AreEqual(new[] { "id", "text", "identifier", "identifier", "active", "name", "telecom" }, orderedNames);

            var orderedNameNames = root.Element("{http://hl7.org/fhir}name")
                                    .Elements().Select(e => e.Name.LocalName).ToList();
            CollectionAssert.AreEqual(new[] { "use", "family", "given" }, orderedNameNames);
        }

        [TestMethod]
        public async Tasks.Task CanSerializeFromPoco()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var pser = new FhirXmlParser(new ParserSettings { DisallowXsiAttributesOnRoot = false });
            var pat = await pser.ParseAsync<Patient>(tpXml);

            var nav = pat.ToTypedElement();
            var output = await nav.ToXmlAsync();
            XmlAssert.AreSame("fp-test-patient.xml", tpXml, output, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public async Tasks.Task CompareSubtrees()
        {
            var tpXml = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.xml"));
            var tpJson = await File.ReadAllTextAsync(Path.Combine("TestData", "fp-test-patient.json"));
            // If on a Unix platform replace \\r\\n in json strings to \\n.
            if(Environment.NewLine == "\n")
                tpJson = tpJson.Replace(@"\r\n", @"\n");
            var pat = await (new FhirXmlParser()).ParseAsync<Patient>(tpXml);

            var navXml = getXmlElement(tpXml);
            var navJson = await getJsonElement(tpJson);
            var navPoco = pat.ToTypedElement();
            assertAreAllEqual(navXml, navJson, navPoco);

            // A subtree that's a normal datatype
            var subnavXml = navXml.Children("photo").First();
            var subnavJson = navJson.Children("photo").First();
            var subnavPoco = navPoco.Children("photo").First();
            assertAreAllEqual(subnavXml, subnavJson, subnavPoco);
        }

        private void assertAreAllEqual(ITypedElement subnavXml, ITypedElement subnavJson, ITypedElement subnavPoco)
        {
            Assert.IsTrue(subnavXml.IsEqualTo(subnavJson).Success);
            Assert.IsTrue(subnavJson.IsEqualTo(subnavPoco).Success);
            Assert.IsTrue(subnavPoco.IsEqualTo(subnavXml).Success);
        }

        [TestMethod]
        public async Tasks.Task DoesPretty()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));

            var nav = getXmlElement(xml);
            var output = await nav.ToXmlAsync();
            Assert.IsFalse(output.Substring(0, 50).Contains('\n'));
            var pretty = await nav.ToXmlAsync(new FhirXmlSerializationSettings { Pretty = true });
            Assert.IsTrue(pretty.Substring(0, 50).Contains('\n'));

            var p = await (new FhirXmlParser()).ParseAsync<Patient>(xml);
            output = await (new FhirXmlSerializer(new SerializerSettings { Pretty = false })).SerializeToStringAsync(p);
            Assert.IsFalse(output.Substring(0, 50).Contains('\n'));
            pretty = await (new FhirXmlSerializer(new SerializerSettings { Pretty = true })).SerializeToStringAsync(p);
            Assert.IsTrue(pretty.Substring(0, 50).Contains('\n'));
        }

        [TestMethod]
        public async Tasks.Task TestAppendNewLine()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));

            var nav = getXmlElement(xml);
            var output = await nav.ToXmlAsync();
            Assert.IsFalse(output.Substring(0, 50).Contains('\n'));
            var pretty = await nav.ToXmlAsync(new FhirXmlSerializationSettings { Pretty = true });
            Assert.IsTrue(pretty.Substring(0, 50).Contains('\n'));
            var lastLine = pretty.Split('\n').Last();
            Assert.IsFalse(string.IsNullOrEmpty(lastLine));

            var p = await (new FhirXmlParser()).ParseAsync<Patient>(xml);
            output = await (new FhirXmlSerializer(new SerializerSettings { Pretty = false, AppendNewLine = true })).SerializeToStringAsync(p);
            lastLine = output.Split('\n').Last();
            Assert.IsTrue(string.IsNullOrEmpty(lastLine));
            pretty = await (new FhirXmlSerializer(new SerializerSettings { Pretty = true, AppendNewLine = true })).SerializeToStringAsync(p);
            lastLine = pretty.Split('\n').Last();
            Assert.IsTrue(string.IsNullOrEmpty(lastLine));
        }
    }
}
