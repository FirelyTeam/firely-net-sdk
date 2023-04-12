using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class SerializeDemoPatientXml
    {
        public ITypedElement getXmlElement(string xml, FhirXmlParsingSettings s = null) =>
            XmlParsingHelpers.ParseToTypedElement(xml, new PocoStructureDefinitionSummaryProvider(Model.Version.DSTU2), s);
        public ITypedElement getJsonElement(string json, FhirJsonParsingSettings s = null) =>
            JsonParsingHelpers.ParseToTypedElement(json, new PocoStructureDefinitionSummaryProvider(Model.Version.DSTU2), settings: s);


        [TestMethod]
        public void CanSerializeThroughNavigatorAndCompare()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlElement(tpXml);
            var output = nav.ToXml();
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
        public void CanSerializeFromPoco()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var pser = new FhirXmlParser(new ParserSettings(Model.Version.DSTU2) { DisallowXsiAttributesOnRoot = false });
            var pat = pser.Parse<Model.DSTU2.Patient>(tpXml);

            var nav = pat.ToTypedElement(Model.Version.DSTU2);
            var output = nav.ToXml();
            XmlAssert.AreSame("fp-test-patient.xml", tpXml, output, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public void CanStreamingSerializeFromPoco()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var pser = new FhirXmlParser(new ParserSettings(Model.Version.DSTU2) { DisallowXsiAttributesOnRoot = false });
            var pat = pser.Parse<Model.DSTU2.Patient>(tpXml);

            var output = new FhirXmlFastSerializer(Model.Version.DSTU2).SerializeToString(pat);
            XmlAssert.AreSame("fp-test-patient.xml", tpXml, output, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public void CompareSubtrees()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var tpJson = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));
            // If on a Unix platform replace \\r\\n in json strings to \\n.
            if(Environment.NewLine == "\n")
                tpJson = tpJson.Replace(@"\r\n", @"\n");
            var pat = (new FhirXmlParser(Model.Version.DSTU2)).Parse<Model.DSTU2.Patient>(tpXml);

            var navXml = getXmlElement(tpXml);
            var navJson = getJsonElement(tpJson);
            var navPoco = pat.ToTypedElement(Model.Version.DSTU2);
            assertAreAllEqual(navXml, navJson, navPoco);

            // A subtree that's a normal datatype
            var subnavXml = navXml.Children("photo").First();
            var subnavJson = navJson.Children("photo").First();
            var subnavPoco = navPoco.Children("photo").First();
            assertAreAllEqual(subnavXml, subnavJson, subnavPoco);
        }

        private void assertAreAllEqual(ITypedElement subnavXml, ITypedElement subnavJson, ITypedElement subnavPoco)
        {
            var result = subnavXml.IsEqualTo(subnavJson);
            Assert.IsTrue(result.Success, result.Details + " at " + result.FailureLocation);
            result = subnavJson.IsEqualTo(subnavPoco);
            Assert.IsTrue(result.Success, result.Details + " at " + result.FailureLocation);
            result = subnavPoco.IsEqualTo(subnavXml);
            Assert.IsTrue(result.Success, result.Details + " at " + result.FailureLocation);
        }

        [TestMethod]
        public void DoesPretty()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));

            var nav = getXmlElement(xml);
            var output = nav.ToXml();
            Assert.IsFalse(output.Substring(0, 50).Contains('\n'));
            var pretty = nav.ToXml(new FhirXmlSerializationSettings { Pretty = true });
            Assert.IsTrue(pretty.Substring(0, 50).Contains('\n'));

            var p = (new FhirXmlParser(Model.Version.DSTU2)).Parse<Model.DSTU2.Patient>(xml);
            output = (new FhirXmlSerializer(new SerializerSettings(Model.Version.DSTU2) { Pretty = false })).SerializeToString(p);
            Assert.IsFalse(output.Substring(0, 50).Contains('\n'));
            pretty = (new FhirXmlSerializer(new SerializerSettings(Model.Version.DSTU2) { Pretty = true })).SerializeToString(p);
            Assert.IsTrue(pretty.Substring(0, 50).Contains('\n'));
        }

        [TestMethod]
        public void DoesPrettyStreaming()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));

            var nav = getXmlElement(xml);
            var output = nav.ToXml();
            Assert.IsFalse(output.Substring(0, 50).Contains('\n'));
            var pretty = nav.ToXml(new FhirXmlSerializationSettings { Pretty = true });
            Assert.IsTrue(pretty.Substring(0, 50).Contains('\n'));

            var p = (new FhirXmlParser(Model.Version.DSTU2)).Parse<Model.DSTU2.Patient>(xml);
            output = (new FhirXmlFastSerializer(new SerializerSettings(Model.Version.DSTU2) { Pretty = false })).SerializeToString(p);
            Assert.IsFalse(output.Substring(0, 50).Contains('\n'));
            pretty = (new FhirXmlFastSerializer(new SerializerSettings(Model.Version.DSTU2) { Pretty = true })).SerializeToString(p);
            Assert.IsTrue(pretty.Substring(0, 50).Contains('\n'));
        }

    }
}