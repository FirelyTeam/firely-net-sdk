using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.IO;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseDemoPatientLineInfoAnnotationPoco
    {
        private static T getXmlPocoAnnotated<T>(string xml) where T : Base
        {
            return new FhirXmlDeserializer(new DeserializerSettings() { AnnotateLineInfo = true }.UsingMode(DeserializationMode.Ostrich)).Deserialize<T>(xml);
        }

        private static T getJsonPocoAnnotated<T>(string json) where T : Base
        {
            return new FhirJsonDeserializer(new DeserializerSettings() { AnnotateLineInfo = true }.UsingMode(DeserializationMode.Ostrich)).Deserialize<T>(json);
        }

        [TestMethod]
        public void HasLineNumbers_PocoFromXml()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlPocoAnnotated<Patient>(xml);

            foreach (var (name, c) in nav.EnumerateElements())
            {
                CheckAllElementsAnnotated<XmlSerializationDetails>(c);
            }
        }

        [TestMethod]
        public void HasLineNumbers_PocoFromJson()
        {
            var json = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));
            var nav = getJsonPocoAnnotated<Patient>(json);

            foreach (var (name, c) in nav.EnumerateElements())
            {
                CheckAllElementsAnnotated<JsonSerializationDetails>(c);
            }
        }
        
        public void CheckAllElementsAnnotated<T>(object element) where T : IPositionInfo
        {
            Assert.IsNotNull(element);
            if (element is Base baseElement)
            {
                var posInfo = baseElement.Annotation<T>();

                posInfo.Should().NotBeNull();
                posInfo.LineNumber.Should().NotBe(-1).And.NotBe(0);
                posInfo.LinePosition.Should().NotBe(-1).And.NotBe(0);
                
                foreach (var (_, baseChild) in baseElement.EnumerateElements())
                {
                    CheckAllElementsAnnotated<T>(baseChild);
                }
            }

            if (element is IList list)
            {
                foreach (var listElement in list)
                {
                    CheckAllElementsAnnotated<T>(listElement);
                }
            }
        }
    }
}