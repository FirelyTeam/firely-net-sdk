using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class NullInListSerializationTest
    {
        [TestMethod]
        public void TestSerializeWithNullInExtensionList()
        {
            var patient = new Patient { Id = "example" };
            patient.Extension = new List<Extension> { null, new Extension("http://test", new FhirString("abcd")) };

            // This should not throw a NullReferenceException
            var serializer = new FhirJsonPocoSerializer();
            var json = serializer.SerializeToString(patient);
            
            System.Console.WriteLine("JSON output: " + json);
            
            // The null should be filtered out
            Assert.IsFalse(json.Contains("null"));
            Assert.IsTrue(json.Contains("http://test"));
            Assert.IsTrue(json.Contains("abcd"));
        }

        [TestMethod]
        public void TestSerializeXmlWithNullInExtensionList()
        {
            var patient = new Patient { Id = "example" };
            patient.Extension = new List<Extension> { null, new Extension("http://test", new FhirString("abcd")) };

            // This should not throw a NullReferenceException
            var serializer = new FhirXmlPocoSerializer();
            var xml = serializer.SerializeToString(patient);
            
            System.Console.WriteLine("XML output: " + xml);
            
            // The null should be filtered out
            Assert.IsTrue(xml.Contains("http://test"));
            Assert.IsTrue(xml.Contains("abcd"));
        }

        [TestMethod]
        public void TestSerializeWithNullInIdentifierList()
        {
            var patient = new Patient { Id = "example" };
            patient.Identifier = new List<Identifier> { null, new Identifier { System = "http://example.org", Value = "12345" } };

            // This should not throw a NullReferenceException
            var serializer = new FhirJsonPocoSerializer();
            var json = serializer.SerializeToString(patient);
            
            System.Console.WriteLine("JSON output: " + json);
            
            // The null should be filtered out
            Assert.IsFalse(json.Contains("null"));
            Assert.IsTrue(json.Contains("http://example.org"));
            Assert.IsTrue(json.Contains("12345"));
        }

        [TestMethod]
        public void TestDeepCopyWithNullInExtensionList()
        {
            var patient = new Patient { Id = "example" };
            patient.Extension = new List<Extension> { null, new Extension("http://test", new FhirString("abcd")) };

            // This should not throw a NullReferenceException
            var copy = (Patient)patient.DeepCopy();
            
            // The null should be filtered out in the copy
            Assert.IsNotNull(copy);
            Assert.AreEqual(1, copy.Extension.Count);
            Assert.AreEqual("http://test", copy.Extension[0].Url);
        }
    }
}
