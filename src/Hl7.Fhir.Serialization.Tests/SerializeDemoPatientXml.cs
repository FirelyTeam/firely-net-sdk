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
        public ITypedElement getXmlElement(string xml, FhirXmlNodeSettings s = null) =>
            XmlParsingHelpers.ParseToTypedElement(xml, new PocoStructureDefinitionSummaryProvider(), s);
        public ITypedElement getJsonElement(string json, FhirJsonNodeSettings s = null) =>
            JsonParsingHelpers.ParseToTypedElement(json, new PocoStructureDefinitionSummaryProvider(), settings: s);


        [TestMethod]
        public void CanSerializeThroughNavigatorAndCompare()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlElement(tpXml);
            var output = nav.ToXml();
            XmlAssert.AreSame("fp-test-patient.xml", tpXml, output, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public void TestPruneEmptyNodes()
        {
            var tpXml = File.ReadAllText(@"TestData\test-empty-nodes.xml");

            // Make sure permissive parsing is on - otherwise the parser will complain about all those empty nodes
            var nav = getXmlElement(tpXml, new FhirXmlNodeSettings { PermissiveParsing = true });
            var doc = nav.ToXDocument().Root;
            Assert.AreEqual(10, doc.DescendantNodesAndSelf().Count());  // only 8 nodes + 2 comments left after pruning
        }

        [TestMethod]
        public void TestElementReordering()
        {
            var tpXml = File.ReadAllText(@"TestData\patient-out-of-order.xml");
            var nav = getXmlElement(tpXml, new FhirXmlNodeSettings { PermissiveParsing = true });  // since the order is incorrect
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
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var pser = new FhirXmlParser(new ParserSettings { DisallowXsiAttributesOnRoot = false });
            var pat = pser.Parse<Patient>(tpXml);

            var nav = pat.ToTypedElement();
            var output = nav.ToXml();
            XmlAssert.AreSame("fp-test-patient.xml", tpXml, output, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public void CompareSubtrees()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var tpJson = File.ReadAllText(@"TestData\fp-test-patient.json");
            var pat = (new FhirXmlParser()).Parse<Patient>(tpXml);

            var navXml = getXmlElement(tpXml);
            var navJson = getJsonElement(tpJson);
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
        public void DoesPretty()
        {
            var xml = File.ReadAllText(@"TestData\fp-test-patient.xml");

            var nav = getXmlElement(xml);
            var output = nav.ToXml();
            Assert.IsFalse(output.Substring(0, 50).Contains('\n'));
            var pretty = nav.ToXml(new FhirXmlWriterSettings { Pretty = true });
            Assert.IsTrue(pretty.Substring(0, 50).Contains('\n'));

            var p = (new FhirXmlParser()).Parse<Patient>(xml);
            output = (new FhirXmlSerializer(new SerializerSettings { Pretty = false })).SerializeToString(p);
            Assert.IsFalse(output.Substring(0, 50).Contains('\n'));
            pretty = (new FhirXmlSerializer(new SerializerSettings { Pretty = true })).SerializeToString(p);
            Assert.IsTrue(pretty.Substring(0, 50).Contains('\n'));
        }

    }
}