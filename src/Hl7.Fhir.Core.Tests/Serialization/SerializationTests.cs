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
            var poco = (Meta)(new FhirXmlParser().Parse(metaXml, typeof(Meta)));
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
            Assert.IsFalse(SerializationUtil.ProbeIsJson("this is nothing"));
            Assert.IsFalse(SerializationUtil.ProbeIsJson("  crap { "));
            Assert.IsFalse(SerializationUtil.ProbeIsJson("<element/>"));
            Assert.IsTrue(SerializationUtil.ProbeIsJson("   { x:5 }"));

            Assert.IsFalse(SerializationUtil.ProbeIsXml("this is nothing"));
            Assert.IsFalse(SerializationUtil.ProbeIsXml("  crap { "));
            Assert.IsFalse(SerializationUtil.ProbeIsXml(" < crap  "));
            Assert.IsFalse(SerializationUtil.ProbeIsXml("   { x:5 }"));
            Assert.IsTrue(SerializationUtil.ProbeIsXml("   <element/>"));
            Assert.IsTrue(SerializationUtil.ProbeIsXml("<?xml />"));
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

            var summ = FhirSerializer.SerializeResourceToXml(p, summary: Fhir.Rest.SummaryType.True);
            Assert.IsTrue(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));

            var q = new Questionnaire();
            q.Text = new Narrative() { Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">Test Questionnaire</div>" };
            q.Status = Questionnaire.QuestionnaireStatus.Published;
            q.Date = "2015-09-27";
            q.Group = new Questionnaire.GroupComponent();
            q.Group.Title = "TITLE";
            q.Group.Text = "TEXT";
            q.Group.LinkId = "linkid";

            var qfull = FhirSerializer.SerializeResourceToXml(q);
            Console.WriteLine("summary: Fhir.Rest.SummaryType.False");
            Console.WriteLine(qfull);
            Assert.IsTrue(qfull.Contains("Test Questionnaire"));
            Assert.IsTrue(qfull.Contains("<status value=\"published\""));
            Assert.IsTrue(qfull.Contains("<date value=\"2015-09-27\""));
            Assert.IsTrue(qfull.Contains("<title value=\"TITLE\""));
            Assert.IsTrue(qfull.Contains("<text value=\"TEXT\""));
            Assert.IsTrue(qfull.Contains("<linkId value=\"linkid\""));

            var qSum = FhirSerializer.SerializeResourceToXml(q, summary: Fhir.Rest.SummaryType.True);
            Console.WriteLine("summary: Fhir.Rest.SummaryType.True");
            Console.WriteLine(qSum);
            Assert.IsFalse(qSum.Contains("Test Questionnaire"));
            Assert.IsTrue(qSum.Contains("<status value=\"published\""));
            Assert.IsTrue(qSum.Contains("<date value=\"2015-09-27\""));
            Assert.IsTrue(qSum.Contains("<title value=\"TITLE\""));
            Assert.IsFalse(qSum.Contains("<text value=\"TEXT\""));
            Assert.IsFalse(qSum.Contains("<linkId value=\"linkid\""));

            var qData = FhirSerializer.SerializeResourceToXml(q, summary: Fhir.Rest.SummaryType.Data);
            Console.WriteLine("summary: Fhir.Rest.SummaryType.Data");
            Console.WriteLine(qData);
            Assert.IsFalse(qData.Contains("Test Questionnaire"));
            Assert.IsTrue(qData.Contains("<meta"));
            Assert.IsTrue(qData.Contains("<text value=\"TEXT\""));
            Assert.IsTrue(qData.Contains("<status value=\"published\""));
            Assert.IsTrue(qData.Contains("<date value=\"2015-09-27\""));
            Assert.IsTrue(qData.Contains("<title value=\"TITLE\""));
            Assert.IsTrue(qData.Contains("<linkId value=\"linkid\""));

            var qText = FhirSerializer.SerializeResourceToXml(q, summary: Fhir.Rest.SummaryType.Text);
            Console.WriteLine("summary: Fhir.Rest.SummaryType.Text");
            Console.WriteLine(qText);
            Assert.IsTrue(qText.Contains("Test Questionnaire"));
            Assert.IsTrue(qText.Contains("<meta"));
            Assert.IsTrue(qText.Contains("<status value=\"published\""));
            Assert.IsFalse(qText.Contains("<text value=\"TEXT\""));
            Assert.IsFalse(qText.Contains("<date value=\"2015-09-27\""));
            Assert.IsFalse(qText.Contains("<title value=\"TITLE\""));
            Assert.IsFalse(qText.Contains("<linkId value=\"linkid\""));
            Assert.AreEqual(0, q.Meta.Tag.Where(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED").Count(), "Subsetted Tag should not still be there.");

            // Verify that reloading the content into an object...
            var qInflate = FhirXmlParser.Parse<Questionnaire>(qText);
            Assert.AreEqual(1, qInflate.Meta.Tag.Where(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED").Count(), "Subsetted Tag should not still be there.");
        }


        private FhirXmlParser FhirXmlParser = new FhirXmlParser();
        private FhirJsonParser FhirJsonParser = new FhirJsonParser();

        [TestMethod]
        public void TestBundleSummary()
        {
            var p = new Patient();

            p.BirthDate = "1972-11-30";     // present in both summary and full
            p.Photo = new List<Attachment>() { new Attachment() { ContentType = "text/plain" } };

            var b = new Bundle();
            b.AddResourceEntry(p, "http://nu.nl/fhir/Patient/1");
            b.Total = 1;

            var full = FhirSerializer.SerializeResourceToXml(b);
            Assert.IsTrue(full.Contains("<entry"));
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));
            Assert.IsTrue(full.Contains("<total"));

            var summ = FhirSerializer.SerializeResourceToXml(b, summary: Fhir.Rest.SummaryType.True);
            Assert.IsTrue(summ.Contains("<entry"));
            Assert.IsTrue(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));
            Assert.IsTrue(summ.Contains("<total"));

            summ = FhirSerializer.SerializeResourceToXml(b, summary: Fhir.Rest.SummaryType.Count);
            Assert.IsFalse(summ.Contains("<entry"));
            Assert.IsFalse(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));
            Assert.IsTrue(summ.Contains("<total"));
        }


        [TestMethod]
        public void HandleCommentsJson()
        {
            string json = File.ReadAllText(@"TestData\TestPatient.json");

            var pat = FhirJsonParser.Parse<Patient>(json);

            Assert.AreEqual(1, pat.Telecom[0].FhirCommentsElement.Count);
            Assert.AreEqual("   home communication details aren't known   ", pat.Telecom[0].FhirComments.First());

            pat.Telecom[0].FhirCommentsElement.Add(new FhirString("A second line"));

            json = FhirSerializer.SerializeResourceToJson(pat);
            pat = FhirJsonParser.Parse<Patient>(json);

            Assert.AreEqual(2, pat.Telecom[0].FhirCommentsElement.Count);
            Assert.AreEqual("   home communication details aren't known   ", pat.Telecom[0].FhirComments.First());
            Assert.AreEqual("A second line", pat.Telecom[0].FhirComments.Skip(1).First());
        }

        [TestMethod, Ignore]
        public void HandleCommentsXml()
        {
            string xml = File.ReadAllText(@"TestData\TestPatient.xml");

            var pat = FhirXmlParser.Parse<Patient>(xml);

            Assert.AreEqual(1, pat.Name[0].FhirCommentsElement.Count);
            Assert.AreEqual("See if this is roundtripped", pat.Name[0].FhirComments.First());

            pat.Name[0].FhirCommentsElement.Add(new FhirString("A second line"));

            xml = FhirSerializer.SerializeResourceToXml(pat);
            pat = FhirXmlParser.Parse<Patient>(xml);

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

            b = FhirXmlParser.Parse<Bundle>(xml);

            Assert.IsTrue(!b.NextLink.ToString().EndsWith("/"));
        }


        [TestMethod]
        public void TestIdInSummary()
        {
            var p = new Patient();
            p.Text = new Narrative();
            p.Text.Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">Some test narrative</div>";
            p.Meta = new Meta();
            p.Contained = new List<Resource>();
            p.Contained.Add(new Organization() { Id = "temp", Name = "temp org", Active = true });
            p.AddExtension("http://example.org/ext", new FhirString("dud"));
            p.Id = "test-id-1";
            p.BirthDate = "1972-11-30";     // present in both summary and full
            p.Photo = new List<Attachment>() { new Attachment() { ContentType = "text/plain", Creation = "45" } };
            p.ManagingOrganization = new ResourceReference() { Display = "temp org", Reference = "#temp" };

            var full = FhirSerializer.SerializeResourceToXml(p);
            Assert.IsTrue(full.Contains("narrative"));
            Assert.IsTrue(full.Contains("dud"));
            Assert.IsTrue(full.Contains("temp org"));
            Assert.IsTrue(full.Contains("<id value="));
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));
            Assert.IsTrue(full.Contains("text/plain"));

            full = FhirSerializer.SerializeResourceToXml(p, summary: Hl7.Fhir.Rest.SummaryType.False);
            Assert.IsTrue(full.Contains("narrative"));
            Assert.IsTrue(full.Contains("dud"));
            Assert.IsTrue(full.Contains("temp org"));
            Assert.IsTrue(full.Contains("contain"));
            Assert.IsTrue(full.Contains("<id value="));
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));
            Assert.IsTrue(full.Contains("text/plain"));

            var summ = FhirSerializer.SerializeResourceToXml(p, summary: Fhir.Rest.SummaryType.True);
            Assert.IsFalse(summ.Contains("narrative"));
            Assert.IsFalse(summ.Contains("dud"));
            Assert.IsFalse(summ.Contains("contain"));
            Assert.IsTrue(summ.Contains("temp org"));
            Assert.IsTrue(summ.Contains("<id value="));
            Assert.IsTrue(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));

            var data = FhirSerializer.SerializeResourceToXml(p, summary: Hl7.Fhir.Rest.SummaryType.Data);
            Assert.IsFalse(data.Contains("narrative"));
            Assert.IsTrue(data.Contains("contain"));
            Assert.IsTrue(data.Contains("dud"));
            Assert.IsTrue(data.Contains("temp org"));
            Assert.IsTrue(data.Contains("<id value="));
            Assert.IsTrue(data.Contains("<birthDate"));
            Assert.IsTrue(data.Contains("<photo"));
        }

        [TestMethod]
        public void TestDecimalPrecisionSerializationInJson()
        {
            var dec6 = 6m;
            var dec60 = 6.0m;

            var obs = new Observation { Value = new FhirDecimal(dec6) };
            var json = FhirSerializer.SerializeResourceToJson(obs);
            var obs2 = FhirJsonParser.Parse<Observation>(json);
            Assert.AreEqual("6", ((FhirDecimal)obs2.Value).Value.Value.ToString(CultureInfo.InvariantCulture));

            obs = new Observation { Value = new FhirDecimal(dec60) };
            json = FhirSerializer.SerializeResourceToJson(obs);
            obs2 = FhirJsonParser.Parse<Observation>(json);
            Assert.AreEqual("6.0", ((FhirDecimal)obs2.Value).Value.Value.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestLongDecimalSerialization()
        {
            var dec = 3.1415926535897932384626433833m;
            var obs = new Observation { Value = new FhirDecimal(dec) };
            var json = FhirSerializer.SerializeResourceToJson(obs);
            var obs2 = FhirJsonParser.Parse<Observation>(json);
            Assert.AreEqual(dec.ToString(CultureInfo.InvariantCulture), ((FhirDecimal)obs2.Value).Value.Value.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TryScriptInject()
        {
            var x = new Patient();

            x.Name.Add(HumanName.ForFamily("<script language='javascript'></script>"));

            var xml = FhirSerializer.SerializeResourceToXml(x);
            Assert.IsFalse(xml.Contains("<script"));
        }


        [TestMethod]
        public void TryXXEExploit()
        {
            var input =
                "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\n" +
                "<!DOCTYPE foo [  \n" +
                "<!ELEMENT foo ANY >\n" +
                "<!ENTITY xxe SYSTEM \"file:///etc/passwd\" >]>" +
                "<Patient xmlns=\"http://hl7.org/fhir\">" +
                    "<text>" +
                        "<div xmlns=\"http://www.w3.org/1999/xhtml\">TEXT &xxe; TEXT</div>\n" +
                    "</text>" +
                    "<address>" +
                        "<line value=\"FOO\"/>" +
                    "</address>" +
                "</Patient>";

            try
            {
                FhirXmlParser.Parse<Resource>(input);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Contains("DTD is prohibited"));
            }
        }

        [TestMethod]
        public void SerializeUnknownEnums()
        {
            string xml = File.ReadAllText(@"TestData\TestPatient.xml");
            var pser = new FhirXmlParser();
            var p = pser.Parse<Patient>(xml);
            string outp = FhirSerializer.SerializeResourceToXml(p);
            Assert.IsTrue(outp.Contains("\"male\""));

            // Pollute the data with an incorrect administrative gender
            p.GenderElement.ObjectValue = "superman";

            outp = FhirSerializer.SerializeResourceToXml(p);
            Assert.IsFalse(outp.Contains("\"male\""));
            Assert.IsTrue(outp.Contains("\"superman\""));
        }
    }
}
