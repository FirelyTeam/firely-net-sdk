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
            return (FhirXmlNode.Parse(expected).ToPoco<Patient>(), expected);
        }

        [TestMethod]
        public void CanSerializeEdgeCases()
        {
            var (poco, expected) = getEdgecases();

            var serializer = new BaseFhirXmlSerializer(ModelInfo.ModelInspector);
            var actual = SerializationUtil.WriteXmlToString(w => serializer.Serialize(poco, w));

            XmlAssert.AreSame("edgecases", expected, actual, ignoreSchemaLocation: true);
        }

        [TestMethod]
        public void SerializesInvalidData()
        {
            var serializer = new BaseFhirXmlSerializer(ModelInfo.ModelInspector);
            FhirBoolean b = new() { JsonValue = "treu" };
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
            var serializer = new BaseFhirXmlSerializer(ModelInfo.ModelInspector);
            FhirBoolean b = new() { JsonValue = "treu" };

            serializer.SerializeToString(b).Should().StartWith("<boolean");
            serializer.SerializeToString(b, rootName: "active").Should().StartWith("<active");

            Patient p = new() { ActiveElement = b };
            serializer.SerializeToString(p).Should().StartWith("<Patient");
            serializer.SerializeToString(p, rootName: "contact").Should().StartWith("<contact");
        }
        

        [TestMethod]
        public void CanUseFilterFactory()
        {
            var patient = new Patient
            {
                Id = "test-patient",
                Active = true,
                Name = [new HumanName { Given = ["John"], Family = "Doe" }],
                Gender = AdministrativeGender.Male
            };

            BaseFhirXmlSerializer serializer = FhirXmlSerializer.Default;

            // Test the new factory-based method
            var elementsFactory = SerializationFilter.ForElementsFactory(["id", "active"]);
            var xmlWithFactory = serializer.SerializeToString(patient, filterFactory: elementsFactory);

            // Test the obsolete method for comparison
#pragma warning disable CS0618 // Type or member is obsolete
            var filter = SerializationFilter.ForElementsFactory(["id", "active"]);
            var xmlWithFilter = serializer.SerializeToString(patient, filterFactory: filter);
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
            var elementsFactory = SerializationFilter.ForElementsFactory(["id", "active"]);
            
            // Each call should return a new instance
            var filter1 = elementsFactory();
            var filter2 = elementsFactory();
            
            filter1.Should().NotBeSameAs(filter2);
        }
    }
}