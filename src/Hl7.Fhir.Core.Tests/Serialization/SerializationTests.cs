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
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Globalization;

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

            var q = new Questionnaire();

            q.Status = Questionnaire.QuestionnaireStatus.Published;
            q.Date = "2015-09-27";
            q.Group = new Questionnaire.GroupComponent();
            q.Group.Title = "TITLE";
            q.Group.Text = "TEXT";
            q.Group.LinkId = "linkid";

            var qfull = FhirSerializer.SerializeResourceToXml(q);
            Console.WriteLine(qfull);
            Assert.IsTrue(qfull.Contains("<status value=\"published\""));
            Assert.IsTrue(qfull.Contains("<date value=\"2015-09-27\""));
            Assert.IsTrue(qfull.Contains("<title value=\"TITLE\""));
            Assert.IsTrue(qfull.Contains("<text value=\"TEXT\""));
            Assert.IsTrue(qfull.Contains("<linkId value=\"linkid\""));

            var qSum = FhirSerializer.SerializeResourceToXml(q, summary: true);
            Console.WriteLine(qSum);
            Assert.IsTrue(qSum.Contains("<status value=\"published\""));
            Assert.IsTrue(qSum.Contains("<date value=\"2015-09-27\""));
            Assert.IsTrue(qSum.Contains("<title value=\"TITLE\""));
            Assert.IsFalse(qSum.Contains("<text value=\"TEXT\""));
            Assert.IsFalse(qSum.Contains("<linkId value=\"linkid\""));
        }

        [TestMethod]
        public void TestBundleSummary()
        {
            var p = new Patient();

            p.BirthDate = "1972-11-30";     // present in both summary and full
            p.Photo = new List<Attachment>() { new Attachment() { ContentType = "text/plain" } };

            var b = new Bundle();
            b.AddResourceEntry(p, "http://nu.nl/fhir/Patient/1");

            var full = FhirSerializer.SerializeResourceToXml(b);
            Assert.IsTrue(full.Contains("<entry"));
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));

            var summ = FhirSerializer.SerializeResourceToXml(b, summary: true);
            Assert.IsTrue(summ.Contains("<entry"));
            Assert.IsTrue(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));
        }


        [TestMethod]
        public void HandleCommentsJson()
        {
            string json = File.ReadAllText(@"TestData\TestPatient.json");

            var pat = FhirParser.ParseFromJson(json) as Patient;

            Assert.AreEqual(1, pat.Telecom[0].FhirCommentsElement.Count);
            Assert.AreEqual("   home communication details aren't known   ", pat.Telecom[0].FhirComments.First());

            pat.Telecom[0].FhirCommentsElement.Add(new FhirString("A second line"));

            json = FhirSerializer.SerializeResourceToJson(pat);
            pat = FhirParser.ParseFromJson(json) as Patient;

            Assert.AreEqual(2, pat.Telecom[0].FhirCommentsElement.Count);
            Assert.AreEqual("   home communication details aren't known   ", pat.Telecom[0].FhirComments.First());
            Assert.AreEqual("A second line", pat.Telecom[0].FhirComments.Skip(1).First());
        }

        [TestMethod, Ignore]
        public void HandleCommentsXml()
        {
            string xml = File.ReadAllText(@"TestData\TestPatient.xml");

            var pat = FhirParser.ParseFromXml(xml) as Patient;

            Assert.AreEqual(1, pat.Name[0].FhirCommentsElement.Count);
            Assert.AreEqual("See if this is roundtripped", pat.Name[0].FhirComments.First());

            pat.Name[0].FhirCommentsElement.Add(new FhirString("A second line"));

            xml = FhirSerializer.SerializeResourceToXml(pat);
            pat = FhirParser.ParseFromXml(xml) as Patient;

            Assert.AreEqual(2, pat.Name[0].FhirCommentsElement.Count);
            Assert.AreEqual("See if this is roundtripped", pat.Name[0].FhirComments.First());
            Assert.AreEqual("A second line", pat.Name[0].FhirComments.Skip(1).First());
        }


        [TestMethod]
        public void BundleLinksUnaltered()
        {
            var b = new Bundle();

            b.NextLink = new Uri("Organization/123456/_history/123456", UriKind.Relative);

            var xml = FhirSerializer.SerializeToXml(b);

            b = (Bundle)FhirParser.ParseFromXml(xml);

            Assert.IsTrue(!b.NextLink.ToString().EndsWith("/"));
        }


        [TestMethod]
        public void TestIdInSummary()
        {
            var p = new Patient();

            p.Id = "test-id-1";
            p.BirthDate = "1972-11-30";     // present in both summary and full
            p.Photo = new List<Attachment>() { new Attachment() { ContentType = "text/plain" } };

            var full = FhirSerializer.SerializeResourceToXml(p);
            Assert.IsTrue(full.Contains("<id value="));
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));

            var summ = FhirSerializer.SerializeResourceToXml(p, summary: true);
            Assert.IsTrue(summ.Contains("<id value="));
            Assert.IsTrue(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));
        }

        [TestMethod]
        public void TestDecimalPrecisionSerializationInJson()
        {
            var dec6 = 6m;
            var dec60 = 6.0m;

            var obs = new Observation { Value = new FhirDecimal(dec6) };
            var json = FhirSerializer.SerializeResourceToJson(obs);
            var obs2 = (Observation)FhirParser.ParseFromJson(json);
            Assert.AreEqual("6", ((FhirDecimal)obs2.Value).Value.Value.ToString(CultureInfo.InvariantCulture));

            obs = new Observation { Value = new FhirDecimal(dec60) };
            json = FhirSerializer.SerializeResourceToJson(obs);
            obs2 = (Observation)FhirParser.ParseFromJson(json);
            Assert.AreEqual("6.0", ((FhirDecimal)obs2.Value).Value.Value.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestLongDecimalSerialization()
        {
            var dec = 3.1415926535897932384626433833m;
            var obs = new Observation { Value = new FhirDecimal(dec) };
            var json = FhirSerializer.SerializeResourceToJson(obs);
            var obs2 = (Observation)FhirParser.ParseFromJson(json);
            Assert.AreEqual(dec.ToString(CultureInfo.InvariantCulture), ((FhirDecimal)obs2.Value).Value.Value.ToString(CultureInfo.InvariantCulture));
        }

    }
}
