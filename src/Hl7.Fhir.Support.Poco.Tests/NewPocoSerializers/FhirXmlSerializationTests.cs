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
        private static (Patient, string) getEdgecases()
        {
            var filename = Path.Combine("TestData", "fp-test-patient.xml");
            var expected = File.ReadAllText(filename);

            // For now, deserialize with the existing deserializer, until we have completed
            // the dynamicserializer too.
            return (FhirXmlNode.Parse(expected).ToPoco<Patient>(ModelInspector.ForType<Patient>()), expected);
        }

        [TestMethod]
        public void CanSerializeEdgeCases()
        {
            var (poco, expected) = getEdgecases();

            var serializer = new BaseFhirXmlPocoSerializer(ModelInfo.ModelInspector);
            var actual = SerializationUtil.WriteXmlToString(w => serializer.Serialize(poco, w));

            XmlAssert.AreSame("edgecases", expected, actual, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public void SerializesInvalidData()
        {
            var serializer = new BaseFhirXmlPocoSerializer(ModelInfo.ModelInspector);
            FhirBoolean b = new() { ObjectValue = "treu" };
            var xdoc = XDocument.Parse(SerializationUtil.WriteXmlToString(w => serializer.Serialize(b, w)));
            Assert.AreEqual("treu", xdoc.Root.Attribute(XName.Get("value")).Value);

            Patient p = new() { Contact = new() { new Patient.ContactComponent() } };
            xdoc = XDocument.Parse(SerializationUtil.WriteXmlToString(w => serializer.Serialize(p, w)));
            var contactArray = xdoc.Root.Elements(XName.Get("contact", XmlNs.FHIR));
            contactArray.Count().Should().Be(1);
            contactArray.First().Elements().Should().BeEmpty();
        }

        [TestMethod]
        public void SerializesSubtree()
        {
            var serializer = new BaseFhirXmlPocoSerializer(ModelInfo.ModelInspector);
            FhirBoolean b = new() { ObjectValue = "treu" };

            serializer.SerializeToString(b).Should().StartWith("<boolean");
            serializer.SerializeToString(b, rootName: "active").Should().StartWith("<active");

            Patient p = new() { ActiveElement = b };
            serializer.SerializeToString(p).Should().StartWith("<Patient");
            serializer.SerializeToString(p, rootName: "contact").Should().StartWith("<contact");
        }
    }
}