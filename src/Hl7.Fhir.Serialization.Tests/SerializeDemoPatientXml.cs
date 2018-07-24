using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public IElementNavigator getXmlNav(string xml, FhirXmlNavigatorSettings s = null) => 
            FhirXmlNavigator.ForResource(xml, new PocoSerializationInfoProvider(), s);

        [TestMethod]
        public void CanSerializeThroughNavigatorAndCompare()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");

            var nav = getXmlNav(tpXml);

            var xmlBuilder = new StringBuilder();

            // Do the serialization without relying on present xml details from the source,
            // so serialization will only be based on the supplied type information
            var serializer = new FhirXmlWriter();
            using (var writer = XmlWriter.Create(xmlBuilder, new XmlWriterSettings { Indent = true } ))
            {
                serializer.Write(nav, writer);
            }

            var output = xmlBuilder.ToString();
            XmlAssert.AreSame("fp-test-patient.xml", tpXml, output, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public void TestPruneEmptyNodes()
        {
            var tpXml = File.ReadAllText(@"TestData\test-empty-nodes.xml");

            // Make sure permissive parsing is on - otherwise the parser will complain about all those empty nodes
            var nav = getXmlNav(tpXml, new FhirXmlNavigatorSettings { PermissiveParsing = true });

            var xmlBuilder = new StringBuilder();
            var serializer = new FhirXmlWriter();
            using (var writer = XmlWriter.Create(xmlBuilder, new XmlWriterSettings { Indent = true }))
            {
                serializer.Write(nav, writer);
            }

            var output = xmlBuilder.ToString();
            var doc = XDocument.Parse(output).Root;
            Assert.AreEqual(10, doc.DescendantNodesAndSelf().Count());  // only 8 nodes + 2 comments left after pruning
        }

        [TestMethod]
        public void TestElementReordering()
        {
            var tpXml = File.ReadAllText(@"TestData\patient-out-of-order.xml");
            var nav = getXmlNav(tpXml);

            var xmlBuilder = new StringBuilder();
            var serializer = new FhirXmlWriter();
            using (var writer = XmlWriter.Create(xmlBuilder, new XmlWriterSettings { Indent = true }))
            {
                serializer.Write(nav, writer);
            }

            var output = xmlBuilder.ToString();
            var root = XDocument.Parse(output).Root;

            var orderedNames = root.Elements().Select(e => e.Name.LocalName).ToList();
            CollectionAssert.AreEqual(new[] { "id", "text", "identifier", "identifier", "active", "name", "telecom"}, orderedNames);

            var orderedNameNames = root.Element("{http://hl7.org/fhir}name")
                                    .Elements().Select(e => e.Name.LocalName).ToList();
            CollectionAssert.AreEqual(new[] { "use", "family", "given" }, orderedNameNames);
        }

        [TestMethod]
        public void CanSerializeFromPoco()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var pser = new FhirXmlParser(new ParserSettings { DisallowXsiAttributesOnRoot = false } );
            var pat = pser.Parse<Patient>(tpXml);

            var nav = new PocoNavigator(pat);
            var xmlBuilder = new StringBuilder();
            var serializer = new FhirXmlWriter();
            using (var writer = XmlWriter.Create(xmlBuilder))
            {
                serializer.Write(nav, writer);
            }

            var output = xmlBuilder.ToString();
            XmlAssert.AreSame("fp-test-patient.xml", tpXml, output, ignoreSchemaLocation: true);
        }

    }
}