using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Hl7.Fhir.Support.Poco.Tests
{
    [TestClass]
    public class FhirXmlSerializationTests
    {
        private (TestPatient, string) getEdgecases()
        {
            var filename = Path.Combine("TestData", "fp-test-patient.xml");
            var expected = File.ReadAllText(filename);

            // For now, deserialize with the existing deserializer, until we have completed
            // the dynamicserializer too.
            return (FhirXmlNode.Parse(expected).ToPoco<TestPatient>(ModelInspector.ForType<TestPatient>()), expected);
        }

        [TestMethod]
        public void CanSerializeEdgeCases()
        {
            var (poco, expected) = getEdgecases();

            var serializer = new FhirXmlPocoSerializer(Specification.FhirRelease.STU3);
            var actual = SerializationUtil.WriteXmlToString(poco, (o, w) => serializer.Serialize(o, w));

            XmlAssert.AreSame("edgecases", expected, actual, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public void SerializesInvalidData()
        {
            var serializer = new FhirXmlPocoSerializer(Specification.FhirRelease.STU3);
            FhirBoolean b = new() { ObjectValue = "treu" };
            var xdoc = XDocument.Parse(SerializationUtil.WriteXmlToString(b, (o, w) => serializer.Serialize(o, w)));
            Assert.AreEqual("treu", xdoc.Root.Attribute(XName.Get("value")).Value);

            TestPatient p = new() { Contact = new() { new TestPatient.ContactComponent() } };
            xdoc = XDocument.Parse(SerializationUtil.WriteXmlToString(p, (o, w) => serializer.Serialize(o, w)));
            var contactArray = xdoc.Root.Elements(XName.Get("contact", XmlNs.FHIR));
            contactArray.Count().Should().Be(1);
            contactArray.First().Elements().Should().BeEmpty();
        }
    }
}
