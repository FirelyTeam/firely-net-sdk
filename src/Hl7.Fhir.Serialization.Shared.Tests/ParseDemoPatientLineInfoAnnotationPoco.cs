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
        public T getXmlPocoAnnotated<T>(string xml) where T : Base
        {
            try
            {
                return new FhirXmlDeserializer(new DeserializerSettings(){ AnnotateLineInfo = true }).Deserialize<T>(xml);
            }
            catch (DeserializationFailedException e)
            {
                return (T)e.PartialResult;
            }
        }
        
        public T getJsonPocoAnnotated<T>(string json) where T : Base
        {
            try
            {
                return new FhirJsonDeserializer(new DeserializerSettings(){ AnnotateLineInfo = true }).Deserialize<T>(json);
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
            if (element is Base b)
            {
                var posInfo = b.Annotation<T>();
                Assert.IsNotNull(posInfo);
                Assert.AreNotEqual(-1, posInfo.LineNumber);
                Assert.AreNotEqual(-1, posInfo.LinePosition);
                Assert.AreNotEqual(0, posInfo.LineNumber);
                Assert.AreNotEqual(0, posInfo.LinePosition);
                foreach (var (name, c) in b.EnumerateElements())
                {
                    CheckAllElementsAnnotated<T>(c);
                }
            }

            if (element is IList l)
            {
                foreach (var e in l)
                {
                    CheckAllElementsAnnotated<T>(e);
                }
            }

        }
    }
}