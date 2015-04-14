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
using System.IO;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
#if PORTABLE45
	public class PortableSerializationTests
#else
	public class SerializationTests
#endif
    {
        private const string metaXml = "<meta xmlns=\"http://hl7.org/fhir\"><versionId value=\"3141\" /><lastUpdated value=\"2014-12-24T16:30:56.031+01:00\" /></meta>";
        private readonly Meta metaPoco = new Meta { LastUpdated = new DateTimeOffset(2014, 12, 24, 16, 30, 56, 31, new TimeSpan(1, 0, 0)), VersionId = "3141" };

        [TestMethod]
        public void SerializeMeta()
        {
            var xml = FhirSerializer.SerializeToXml(metaPoco,root:"meta");
            Assert.AreEqual(metaXml, xml);
        }


        [TestMethod]
        public void ParseMeta()
        {
            var poco = (Meta)FhirParser.ParseFromXml(metaXml, typeof(Meta));
            var xml = FhirSerializer.SerializeToXml(poco,root:"meta");

            Assert.IsTrue(poco.IsExactly(metaPoco));
            Assert.AreEqual(metaXml, xml);
        }


        [TestMethod]
        public void AvoidBOMUse()
        {
            Bundle b = new Bundle();

            var data = FhirSerializer.SerializeResourceToJsonBytes(b);
            Assert.IsFalse(data[0] == Encoding.UTF8.GetPreamble()[0]);

            data = FhirSerializer.SerializeResourceToXmlBytes(b);
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

        [TestMethod]
        public void TestBundleSummary()
        {
            var p = new Patient();

            p.BirthDate = "1972-11-30";     // present in both summary and full
            p.Photo = new List<Attachment>() { new Attachment() { ContentType = "text/plain" } };

            var b = new Bundle();
            b.AddResourceEntry(p);

            var full = FhirSerializer.SerializeResourceToXml(b);
            Assert.IsTrue(full.Contains("<entry"));
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));

            var summ = FhirSerializer.SerializeResourceToXml(b, summary: true);
            Assert.IsTrue(summ.Contains("<entry"));
            Assert.IsTrue(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));
        }

    }
}
