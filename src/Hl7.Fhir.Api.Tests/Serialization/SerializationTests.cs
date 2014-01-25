using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System.Text;

namespace Hl7.Fhir.Test.Serialization
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void AvoidBOMUse()
        {
            Bundle b = new Bundle();

            var data = FhirSerializer.SerializeBundleToJsonBytes(b);
            Assert.IsFalse(data[0] == Encoding.UTF8.GetPreamble()[0]);

            data = FhirSerializer.SerializeBundleToXmlBytes(b);
            Assert.IsFalse(data[0] == Encoding.UTF8.GetPreamble()[0]);

            Patient p = new Patient();

            data = FhirSerializer.SerializeResourceToJsonBytes(p);
            Assert.IsFalse(data[0] == Encoding.UTF8.GetPreamble()[0]);

            data = FhirSerializer.SerializeResourceToXmlBytes(p);
            Assert.IsFalse(data[0] == Encoding.UTF8.GetPreamble()[0]);
        }

        [TestMethod]
        public void TestProbing()
        {
            Assert.IsFalse(FhirParser.ProbeIsJson("this is nothing"));
            Assert.IsFalse(FhirParser.ProbeIsJson("  crap { "));
            Assert.IsFalse(FhirParser.ProbeIsJson("<element/>"));
            Assert.IsTrue(FhirParser.ProbeIsJson("   { x:5 }"));

            Assert.IsFalse(FhirParser.ProbeIsXml("this is nothing"));
            Assert.IsFalse(FhirParser.ProbeIsXml("  crap { "));
            Assert.IsFalse(FhirParser.ProbeIsXml(" < crap  "));
            Assert.IsFalse(FhirParser.ProbeIsXml("   { x:5 }"));
            Assert.IsTrue(FhirParser.ProbeIsXml("   <element/>"));
            Assert.IsTrue(FhirParser.ProbeIsXml("<?xml />"));
        }
    }
}
