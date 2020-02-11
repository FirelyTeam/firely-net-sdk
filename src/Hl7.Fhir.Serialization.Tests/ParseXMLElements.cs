using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseXMLElements
    {
        [TestMethod]
        public void ParseDifferentXMLElements()
        {
            var testXml = File.ReadAllText(Path.Combine("TestData", "xml-with-different-elements.xml"));
            var sourceNode = FhirXmlNode.Parse(testXml);

            Assert.AreEqual(sourceNode.Children("example1").First().Text, "TestValue1");
            Assert.AreEqual(sourceNode.Children("example2").First().Text, "TestValue2");
        }
    }
}
