using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseCDA
    {
        [TestMethod]
        public void GetValuesInCDAElements()
        {
            var testXml = File.ReadAllText(Path.Combine("TestData", "xml-with-different-cda-elements.xml"));
            var sourceNode = FhirXmlNode.Parse(testXml);

            Assert.AreEqual(getValue("given"), "Richard");
            Assert.AreEqual(getValue("effectiveTime"), "201507221800-0500");
            Assert.AreEqual(sourceNode.Children("statusCode").Children("code").First().Text, "completed");

            var narrative = getValue("text");
            Assert.IsTrue(narrative.TrimStart().StartsWith("History"));
            Assert.IsFalse(narrative.Contains("above"));
            Assert.IsFalse(narrative.Contains("somewhere else"));

            Assert.IsNull(getValue("para"));

            Assert.AreEqual(sourceNode.Children("translation").Children("type").First().Text, "CD");
            Assert.AreEqual(sourceNode.Children("raceCode").Children("nullFlavor").First().Text, "UNK");

            string getValue(string name) => sourceNode.Children(name).First().Text;
        }

        [TestMethod]
        public void RetrieveCDANarrative()
        {
            var testXml = File.ReadAllText(Path.Combine("TestData", "xml-with-different-cda-elements.xml"));
            var sourceNode = FhirXmlNode.Parse(testXml);

            var text = getRaw("text");
            Assert.IsTrue(text.StartsWith("<text"));
            Assert.IsTrue(text.Contains("History of"));
            Assert.IsTrue(text.Contains("<linkHtml"));
            Assert.IsTrue(text.Contains("somewhere"));

            var para = getRaw("para");
            Assert.IsTrue(para.StartsWith("<para"));
            Assert.IsTrue(para.Contains("<p>See below:"));
            Assert.IsTrue(para.Contains("<thead>"));
            Assert.IsTrue(para.Contains("Status"));
            
            string getRaw(string name) => (sourceNode.Children(name).First() as FhirXmlNode).RawXml;
        }

        [TestMethod]
        public void WalkCDANarrative()
        {
            // SourceNode does not know about CDA narrative, so you can just walk into it.

            var testXml = File.ReadAllText(Path.Combine("TestData", "xml-with-different-cda-elements.xml"));
            var sourceNode = FhirXmlNode.Parse(testXml);

            var ths = sourceNode.Children("para").Children("table").Children("thead").Children("tr").Children("th");
            Assert.AreEqual(4, ths.Count());
            Assert.AreEqual("Name", ths.First().Text);
        }

        [TestMethod]
        public void HasCDANamespaces()
        {
            var testXml = File.ReadAllText(Path.Combine("TestData", "xml-with-different-cda-elements.xml"));
            var sourceNode = FhirXmlNode.Parse(testXml);

            var details = getDetails("given");
            Assert.AreEqual("urn:hl7-org:v3", details.Namespace);

            details = getDetails("text");
            Assert.AreEqual("urn:hl7-org:v3", details.Namespace);

            details = getDetails("raceCode");
            Assert.AreEqual("urn:hl7-org:sdtc", details.Namespace);

            details = sourceNode.Children("translation").Children("type").Single().Annotation<XmlSerializationDetails>();
            Assert.AreEqual("http://www.w3.org/2001/XMLSchema-instance", details.Namespace);

            XmlSerializationDetails getDetails(string name) => sourceNode.Children(name).First().Annotation<XmlSerializationDetails>();
        }
    }
}
