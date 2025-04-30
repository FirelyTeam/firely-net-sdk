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
        private T getXmlPocoAnnotated<T>(string xml) where T : Resource
        {
            try
            {
                return (T)new FhirXmlPocoDeserializer(new FhirXmlPocoDeserializerSettings(){ AnnotateLineInfo = true }).DeserializeResource(xml);
            }
            catch (DeserializationFailedException e)
            {
                return (T)e.PartialResult;
            }
        }
        
        private T getJsonPocoAnnotated<T>(string json) where T : Resource
        {
            try
            {
                
                return (T)new FhirJsonPocoDeserializer(new FhirJsonPocoDeserializerSettings() { AnnotateLineInfo = true }).DeserializeResource(json);
            }
            catch (DeserializationFailedException e)
            {
                return (T)e.PartialResult;
            }
        }

        [TestMethod]
        public void HasLineNumbers_PocoFromXml()
        {
            var xml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlPocoAnnotated<Patient>(xml);

            foreach (var c in nav.Children)
            {
                CheckAllElementsAnnotated<XmlSerializationDetails>(c);
            }
        }

        [TestMethod]
        public void HasLineNumbers_PocoFromJson()
        {
            var json = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.json"));
            var nav = getJsonPocoAnnotated<Patient>(json);

            foreach (var c in nav.Children)
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
                
                foreach (var (_, baseChild) in baseElement)
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