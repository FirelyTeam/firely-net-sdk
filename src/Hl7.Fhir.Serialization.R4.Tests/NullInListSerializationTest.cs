using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
    }
}
