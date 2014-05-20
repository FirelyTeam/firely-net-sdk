/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System.Text;
using System.Collections.Generic;

namespace Hl7.Fhir.Test.Serialization
{
    [TestClass]
#if PORTABLE45
	public class PortableSerializationTests
#else
	public class SerializationTests
#endif
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

        [TestMethod]
        public void TestSummary()
        {
            var p = new Patient();

            p.BirthDate = "1972-11-30";     // present in both summary and full
            p.Photo = new List<Attachment>() { new Attachment() { ContentType = "text/plain" } };

            var full = FhirSerializer.SerializeResourceToXml(p);
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));

            var summ = FhirSerializer.SerializeResourceToXml(p, summary: true);
            Assert.IsTrue(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));
        }

    }
}
