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
        private (Patient, string) getEdgecases()
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

            var serializer = new BaseFhirXmlPocoSerializer(Specification.FhirRelease.STU3);
            var actual = SerializationUtil.WriteXmlToString(poco, (o, w) => serializer.Serialize(o, w));

            XmlAssert.AreSame("edgecases", expected, actual, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public void SerializesInvalidData()
        {
            var serializer = new BaseFhirXmlPocoSerializer(Specification.FhirRelease.STU3);
            FhirBoolean b = new() { ObjectValue = "treu" };
            var xdoc = XDocument.Parse(SerializationUtil.WriteXmlToString(b, (o, w) => serializer.Serialize(o, w)));
            Assert.AreEqual("treu", xdoc.Root.Attribute(XName.Get("value")).Value);

            Patient p = new() { Contact = new() { new Patient.ContactComponent() } };
            xdoc = XDocument.Parse(SerializationUtil.WriteXmlToString(p, (o, w) => serializer.Serialize(o, w)));
            var contactArray = xdoc.Root.Elements(XName.Get("contact", XmlNs.FHIR));
            contactArray.Count().Should().Be(1);
            contactArray.First().Elements().Should().BeEmpty();
        }

        [TestMethod]
        public void CanUseFilterFactory()
        {
            var patient = new Patient
            {
                Id = "test-patient",
                Active = true,
                Name = new() { new HumanName { Given = new[] { "John" }, Family = "Doe" } },
                Gender = AdministrativeGender.Male
            };

            var serializer = new BaseFhirXmlPocoSerializer(Specification.FhirRelease.STU3);

            // Test the new factory-based method
            var elementsFactory = SerializationFilter.CreateElementsFactory(new[] { "id", "active" });
            var xmlWithFactory = serializer.SerializeToString(patient, elementsFactory);

            // Test the obsolete method for comparison
#pragma warning disable CS0618 // Type or member is obsolete
            var filter = SerializationFilter.ForElements(new[] { "id", "active" });
            var xmlWithFilter = serializer.SerializeToString(patient, filter);
#pragma warning restore CS0618 // Type or member is obsolete

            // Both methods should produce identical output
            xmlWithFactory.Should().Be(xmlWithFilter);

            // Verify that filtering actually works (should only contain id and active)
            var xdoc = XDocument.Parse(xmlWithFactory);
            var patientElement = xdoc.Root;
            
            // Should contain id and active elements
            patientElement.Elements(XName.Get("id", XmlNs.FHIR)).Should().HaveCount(1);
            patientElement.Elements(XName.Get("active", XmlNs.FHIR)).Should().HaveCount(1);
            
            // Should NOT contain name or gender (they were filtered out)
            patientElement.Elements(XName.Get("name", XmlNs.FHIR)).Should().BeEmpty();
            patientElement.Elements(XName.Get("gender", XmlNs.FHIR)).Should().BeEmpty();
        }

        [TestMethod]
        public void FilterFactoryCreatesNewInstancesEachTime()
        {
            var elementsFactory = SerializationFilter.CreateElementsFactory(new[] { "id", "active" });
            
            // Each call should return a new instance
            var filter1 = elementsFactory();
            var filter2 = elementsFactory();
            
            filter1.Should().NotBeSameAs(filter2);
        }
    }
}