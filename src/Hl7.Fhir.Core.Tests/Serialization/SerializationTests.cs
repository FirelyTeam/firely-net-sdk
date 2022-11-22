/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Model.DSTU2;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Introspection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
    public class SerializationTests
    {
        private const string metaXml = "<meta xmlns=\"http://hl7.org/fhir\"><versionId value=\"3141\" /><lastUpdated value=\"2014-12-24T16:30:56.031+01:00\" /></meta>";
        private const string metaJson = "{\"versionId\":\"3141\",\"lastUpdated\":\"2014-12-24T16:30:56.031+01:00\"}";
        private readonly Meta metaPoco = new Meta { LastUpdated = new DateTimeOffset(2014, 12, 24, 16, 30, 56, 31, new TimeSpan(1, 0, 0)), VersionId = "3141" };

        [TestMethod]
        public void SerializeMetaXml()
        {
            var xml = FhirDstu2XmlSerializer.SerializeToString(metaPoco,root:"meta");
            Assert.AreEqual(metaXml, xml);
        }


        [TestMethod]
        public void SerializeMetaJson()
        {
            var json = FhirDstu2JsonSerializer.SerializeToString(metaPoco);
            Assert.AreEqual(metaJson, json);
        }

        [TestMethod]
        public void ParseMetaXml()
        {
            var poco = (Meta)(FhirDstu2XmlParser.Parse(metaXml, typeof(Meta)));
            var xml = FhirDstu2XmlSerializer.SerializeToString(poco,root:"meta");

            Assert.IsTrue(poco.IsExactly(metaPoco));
            Assert.AreEqual(metaXml, xml);
        }

        [TestMethod]
        public void ParsePatientXmlNullType()
        {
            string xmlPacientTest = TestDataHelper.ReadTestData("TestPatient.xml");
            
            var poco = FhirDstu2XmlParser.Parse(xmlPacientTest);
            
            Assert.AreEqual(((Patient)poco).Id, "pat1");
            Assert.AreEqual(((Patient)poco).Contained.First().Id, "1");
            Assert.AreEqual(((Patient)poco).Name.First().Family.First(), "Donald");
            Assert.AreEqual(((Patient)poco).ManagingOrganization.Reference, "Organization/1");
        }

        internal FhirXmlSerializer FhirDstu2XmlSerializer = new FhirXmlSerializer(Fhir.Model.Version.DSTU2);
        internal FhirJsonSerializer FhirDstu2JsonSerializer = new FhirJsonSerializer(Fhir.Model.Version.DSTU2);

        [TestMethod]
        public void ParseMetaJson()
        {
            var poco = (Meta)(FhirDstu2JsonParser.Parse(metaJson, typeof(Meta)));
            var json = FhirDstu2JsonSerializer.SerializeToString(poco);

            Assert.IsTrue(poco.IsExactly(metaPoco));
            Assert.AreEqual(metaJson, json);
        }

        [TestMethod]
        public void ParsePatientJsonNullType()
        {
            string jsonPatient = TestDataHelper.ReadTestData("TestPatient.json");

            var poco = FhirDstu2JsonParser.Parse(jsonPatient);

            Assert.AreEqual(((Patient)poco).Id, "pat1");
            Assert.AreEqual(((Patient)poco).Contained.First().Id, "1");
            Assert.AreEqual(((Patient)poco).Name.First().Family.First(), "Donald");
            Assert.AreEqual(((Patient)poco).ManagingOrganization.Reference, "Organization/1");
        }

        [TestMethod]
        public void AvoidBOMUse()
        {
            Bundle b = new Bundle() { Total = 1000 };

            var data = FhirDstu2JsonSerializer.SerializeToBytes(b);
            Assert.IsFalse(data[0] == Encoding.UTF8.GetPreamble()[0]);

            data = FhirDstu2XmlSerializer.SerializeToBytes(b);
            Assert.IsFalse(data[0] == Encoding.UTF8.GetPreamble()[0]);

            Patient p = new Patient() { Active = true };

            data = FhirDstu2JsonSerializer.SerializeToBytes(p);
            Assert.IsFalse(data[0] == Encoding.UTF8.GetPreamble()[0]);

            data = FhirDstu2XmlSerializer.SerializeToBytes(p);
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
        public void TestElements()
        {
            var p = new Patient
            {
                BirthDate = "1972-11-30",
                Photo = new List<Attachment>() { new Attachment() { ContentType = "text/plain" } }
            };
            var elements = new[] { "photo" };

            var summaryElements = FhirDstu2XmlSerializer.SerializeToString(p, Fhir.Rest.SummaryType.False, elements: elements);
            Assert.IsFalse(summaryElements.Contains("<birthDate"));
            Assert.IsTrue(summaryElements.Contains("<photo"));

            var noSummarySpecified = FhirDstu2XmlSerializer.SerializeToString(p, elements: elements);
            Assert.IsFalse(noSummarySpecified.Contains("<birthDate"));
            Assert.IsTrue(noSummarySpecified.Contains("<photo"));

            Assert.ThrowsException<ArgumentException>(() => FhirDstu2XmlSerializer.SerializeToString(p, Fhir.Rest.SummaryType.True, elements: elements));
            Assert.ThrowsException<ArgumentException>(() => FhirDstu2XmlSerializer.SerializeToString(p, Fhir.Rest.SummaryType.Count, elements: elements));
            Assert.ThrowsException<ArgumentException>(() => FhirDstu2XmlSerializer.SerializeToString(p, Fhir.Rest.SummaryType.Data, elements: elements));
            Assert.ThrowsException<ArgumentException>(() => FhirDstu2XmlSerializer.SerializeToString(p, Fhir.Rest.SummaryType.Text, elements: elements));
        }

        [TestMethod]
        public void TestWithMetadata()
        {
            var p = new Patient
            {
                BirthDate = "1972-11-30"
            };

            var pSum = FhirDstu2XmlSerializer.SerializeToString(p, summary: Fhir.Rest.SummaryType.True);
            Assert.IsNull(p.Meta, "Meta should not be there");

            p.Meta = new Meta { VersionId = "v2" }; // introducing meta data ourselves. 

            pSum = FhirDstu2XmlSerializer.SerializeToString(p, summary: Fhir.Rest.SummaryType.True);
            Assert.IsNotNull(p.Meta, "Meta should still be there");
            Assert.AreEqual(0, p.Meta.Tag.Where(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED").Count(), "Subsetted Tag should not still be there.");
        }

        private FhirXmlParser FhirDstu2XmlParser = new FhirXmlParser(Fhir.Model.Version.DSTU2);
        private FhirJsonParser FhirDstu2JsonParser = new FhirJsonParser(Fhir.Model.Version.DSTU2);

        [TestMethod]
        public void TestBundleSummary()
        {
            var p = new Patient
            {
                BirthDate = "1972-11-30",     // present in both summary and full
                Photo = new List<Attachment>() { new Attachment() { ContentType = "text/plain" } }
            };

            var b = new Bundle();
            b.AddResourceEntry(p, "http://nu.nl/fhir/Patient/1");
            b.Total = 1;
            b.Type = BundleType.Searchset;

            var full = FhirDstu2XmlSerializer.SerializeToString(b);
            Assert.IsTrue(full.Contains("<entry"));
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));
            Assert.IsTrue(full.Contains("<total"));

            var summ = FhirDstu2XmlSerializer.SerializeToString(b, summary: Fhir.Rest.SummaryType.True);
            Assert.IsTrue(summ.Contains("<entry"));
            Assert.IsTrue(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));
            Assert.IsTrue(summ.Contains("<total"));

            summ = FhirDstu2XmlSerializer.SerializeToString(b, summary: Fhir.Rest.SummaryType.Count);
            Assert.IsFalse(summ.Contains("<entry"));
            Assert.IsFalse(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));
            Assert.IsTrue(summ.Contains("<total"));
            Assert.IsTrue(summ.Contains("<type"));
        }


        [TestMethod]
        public void BundleLinksUnaltered()
        {
            var b = new Bundle
            {
                NextLink = new Uri("Organization/123456/_history/123456", UriKind.Relative)
            };

            var xml = FhirDstu2XmlSerializer.SerializeToString(b);

            b = FhirDstu2XmlParser.Parse<Bundle>(xml);

            Assert.IsTrue(!b.NextLink.ToString().EndsWith("/"));
        }

        [TestMethod]
        public void TestIdInSummary()
        {
            var p = new Patient
            {
                Id = "test-id-1",
                BirthDate = "1972-11-30",     // present in both summary and full
                Photo = new List<Attachment>() { new Attachment() { ContentType = "text/plain", Creation = "45" } },
                ManagingOrganization = new ResourceReference() { Display = "temp org", Reference = "#temp" },

                Text = new Narrative
                {
                    Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">Some test narrative</div>"
                },
                Meta = new Meta(),
                Contained = new List<Resource>
                {
                    new Organization() { Id = "temp", Name = "temp org", Active = true }
                }
            };

            p.AddExtension("http://example.org/ext", new FhirString("dud"));

            var full = FhirDstu2XmlSerializer.SerializeToString(p);
            Assert.IsTrue(full.Contains("narrative"));
            Assert.IsTrue(full.Contains("dud"));
            Assert.IsTrue(full.Contains("temp org"));
            Assert.IsTrue(full.Contains("<id value="));
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));
            Assert.IsTrue(full.Contains("text/plain"));

            full = FhirDstu2XmlSerializer.SerializeToString(p, summary: Hl7.Fhir.Rest.SummaryType.False);
            Assert.IsTrue(full.Contains("narrative"));
            Assert.IsTrue(full.Contains("dud"));
            Assert.IsTrue(full.Contains("temp org"));
            Assert.IsTrue(full.Contains("contain"));
            Assert.IsTrue(full.Contains("<id value="));
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));
            Assert.IsTrue(full.Contains("text/plain"));

            var summ = FhirDstu2XmlSerializer.SerializeToString(p, summary: Fhir.Rest.SummaryType.True);
            Assert.IsFalse(summ.Contains("narrative"));
            Assert.IsFalse(summ.Contains("dud"));
            Assert.IsFalse(summ.Contains("contain"));
            Assert.IsTrue(summ.Contains("temp org"));
            Assert.IsTrue(summ.Contains("<id value="));
            Assert.IsTrue(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));

            var data = FhirDstu2XmlSerializer.SerializeToString(p, summary: Hl7.Fhir.Rest.SummaryType.Data);
            Assert.IsFalse(data.Contains("narrative"));
            Assert.IsTrue(data.Contains("contain"));
            Assert.IsTrue(data.Contains("dud"));
            Assert.IsTrue(data.Contains("temp org"));
            Assert.IsTrue(data.Contains("<id value="));
            Assert.IsTrue(data.Contains("<birthDate"));
            Assert.IsTrue(data.Contains("<photo"));
        }

        [TestMethod]
        public void TestVersionDependentInSummary()
        {
            var p = new Parameters
            {
                Parameter = new List<Parameters.ParameterComponent>
                {
                    new Parameters.ParameterComponent
                    {
                        Name = "N",
                        Value = new FhirString("V")
                    }
                }
            };
            var completeR4Data = new FhirXmlSerializer(Fhir.Model.Version.R4).SerializeToString(p, Fhir.Rest.SummaryType.False);
            Assert.IsTrue(completeR4Data.Contains("<name value=\"N\""));
            Assert.IsTrue(completeR4Data.Contains("<valueString value=\"V\""));
            var summaryR4Data = new FhirXmlSerializer(Fhir.Model.Version.R4).SerializeToString(p, Fhir.Rest.SummaryType.True);
            Assert.IsTrue(summaryR4Data.Contains("<name value=\"N\""));
            Assert.IsTrue(summaryR4Data.Contains("<valueString value=\"V\""));
            var completeStu3Data = new FhirXmlSerializer(Fhir.Model.Version.STU3).SerializeToString(p, Fhir.Rest.SummaryType.False);
            Assert.IsTrue(completeStu3Data.Contains("<name value=\"N\""));
            Assert.IsTrue(completeStu3Data.Contains("<valueString value=\"V\""));
            var summaryStu3Data = new FhirXmlSerializer(Fhir.Model.Version.STU3).SerializeToString(p, Fhir.Rest.SummaryType.True);
            Assert.IsTrue(summaryStu3Data.Contains("<name value=\"N\""));
            Assert.IsTrue(summaryStu3Data.Contains("<valueString value=\"V\""));
            var completeDstu2Data = FhirDstu2XmlSerializer.SerializeToString(p, Fhir.Rest.SummaryType.False);
            Assert.IsTrue(completeDstu2Data.Contains("<name value=\"N\""));
            Assert.IsTrue(completeDstu2Data.Contains("<valueString value=\"V\""));
            var summaryDstu2Data = FhirDstu2XmlSerializer.SerializeToString(p, Fhir.Rest.SummaryType.True);
            Assert.IsFalse(summaryDstu2Data.Contains("<name"));
            Assert.IsFalse(summaryDstu2Data.Contains("<value"));
        }

        [TestMethod]
        public void TestDecimalPrecisionSerializationInJson()
        {
            var dec6 = 6m;
            var dec60 = 6.0m;
            var ext = new FhirDecimal(dec6);
            var obs = new Observation();
            obs.AddExtension("http://example.org/DecimalPrecision", ext);
            
            var json = FhirDstu2JsonSerializer.SerializeToString(obs);
            var obs2 = FhirDstu2JsonParser.Parse<Observation>(json);

            Assert.AreEqual("6", ((FhirDecimal)obs2.GetExtension("http://example.org/DecimalPrecision").Value).Value.Value.ToString(CultureInfo.InvariantCulture));

            ext = new FhirDecimal(dec60);
            obs = new Observation();
            obs.AddExtension("http://example.org/DecimalPrecision", ext);

            json = FhirDstu2JsonSerializer.SerializeToString(obs);
            obs2 = FhirDstu2JsonParser.Parse<Observation>(json);

            Assert.AreEqual("6.0", ((FhirDecimal)obs2.GetExtension("http://example.org/DecimalPrecision").Value).Value.Value.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestLongDecimalSerialization()
        {
            var dec = 3.1415926535897932384626433833m;
            var ext = new FhirDecimal(dec);
            var obs = new Observation();
            obs.AddExtension("http://example.org/DecimalPrecision", ext);

            var json = FhirDstu2JsonSerializer.SerializeToString(obs);
            var obs2 = FhirDstu2JsonParser.Parse<Observation>(json);

            Assert.AreEqual(dec.ToString(CultureInfo.InvariantCulture), ((FhirDecimal)obs2.GetExtension("http://example.org/DecimalPrecision").Value).Value.Value.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestParseUnkownPolymorphPropertyInJson()
        {
            var dec6 = 6m;
            var ext = new FhirDecimal(dec6);
            var obs = new Observation{ Value = new FhirDecimal(dec6) };
            var json = FhirDstu2JsonSerializer.SerializeToString(obs);
            try
            {
                var obs2 = FhirDstu2JsonParser.Parse<Observation>(json);
                Assert.Fail("valueDecimal is not a known type for Observation");
            }
            catch (FormatException)
            {

            }
        }

        [TestMethod]
        public void TryScriptInject()
        {
            var x = new Patient();

            x.Name.Add(HumanName.ForFamily("<script language='javascript'></script>"));

            var xml = FhirDstu2XmlSerializer.SerializeToString(x);
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
                FhirDstu2XmlParser.Parse<Resource>(input);
                Assert.Fail();
            }
            catch (FormatException e)
            {
                Assert.IsTrue(e.Message.Contains("DTD is prohibited"));
            }
        }

        [TestMethod]
        public void SerializeUnknownEnums()
        {
            string xml = TestDataHelper.ReadTestData("TestPatient.xml");
            var pser = FhirDstu2XmlParser;
            var p = pser.Parse<Patient>(xml);
            string outp = FhirDstu2XmlSerializer.SerializeToString(p);
            Assert.IsTrue(outp.Contains("\"male\""));

            // Pollute the data with an incorrect administrative gender
            p.GenderElement.ObjectValue = "superman";

            outp = FhirDstu2XmlSerializer.SerializeToString(p);
            Assert.IsFalse(outp.Contains("\"male\""));
            Assert.IsTrue(outp.Contains("\"superman\""));
        }


        [TestMethod]
        public void TestNullExtensionRemoval()
        {
            var p = new Patient
            {
                Extension = new List<Extension>
                {
                    new Extension("http://hl7.org/fhir/Profile/iso-21090#qualifier", new Code("VV")),
                    null
                },

                Contact = new List<Patient.ContactComponent>
                {
                    null,
                    new Patient.ContactComponent { Name = HumanName.ForFamily("Kramer") },
                }
            };

            var xml = FhirDstu2XmlSerializer.SerializeToString(p);

            var p2 = FhirDstu2XmlParser.Parse<Patient>(xml);
            Assert.AreEqual(1, p2.Extension.Count);
            Assert.AreEqual(1, p2.Contact.Count);
        }

        //An empty object is not allowed
        //[TestMethod]
        //public void SerializeEmptyParams()
        //{
        //    var par = new Parameters();
        //    var xml = FhirDstu2XmlSerializer.SerializeToString(par);

        //    var par2 = FhirDstu2XmlParser.Parse<Parameters>(xml);
        //    Assert.AreEqual(0, par2.Parameter.Count);
        //}

        [TestMethod]
        public void SerializeJsonWithPlainDiv()
        {
            // var res = new ValueSet() { Url = "http://example.org/fhir/ValueSet/MyValueSetExample" };

            string json = TestDataHelper.ReadTestData(@"valueset-v2-0717.json");
            Assert.IsNotNull(json);
            var parser = new FhirJsonParser(Fhir.Model.Version.DSTU2) { Settings = { PermissiveParsing = true} };
            var vs = parser.Parse<ValueSet>(json);
            Assert.IsNotNull(vs);

            var xml = FhirDstu2XmlSerializer.SerializeToString(vs);
            Assert.IsNotNull(xml);
        }

        [FhirType(Fhir.Model.Version.DSTU2, "CustomBundle", IsResource = true)]
        //[DataContract]
        public class CustomBundle : Bundle
        {
            public CustomBundle() : base() { }
        }

        // [WMR 20170825] Richard Kavanagh: runtime exception while serializating derived PoCo classes
        // Workaround: add the FhirType attribute to derived class
        [TestMethod]
        public void TestDerivedPoCoSerialization()
        {
            var bundle = new CustomBundle()
            {
                Type = BundleType.Collection,
                Id = "MyBundle"
            };

            var xml = FhirDstu2XmlSerializer.SerializeToString(bundle);
            Assert.IsNotNull(xml);

            var json = FhirDstu2JsonSerializer.SerializeToString(bundle);
            Assert.IsNotNull(json);
        }

        // #if NET45
        // [WMR 20180409] NEW: Serialize to XmlDocument
        [TestMethod]
        public void TestSerializeToXmlDocument()
        {
            var patientOne = new Patient
            {

                Id = "patient-one",
                Text = new Narrative { Status = Narrative.NarrativeStatus.Generated, Div = "<div>A great blues player</div>" },
                Meta = new Meta { ElementId = "eric-clapton", VersionId = "1234" },

                Name = new List<HumanName> { new HumanName { Family = new[] { "Clapton" }, Use = NameUse.Official } },

                Active = true,
                BirthDate = "2015-07-09",
                Gender = AdministrativeGender.Male
            };

            var doc = FhirDstu2XmlSerializer.SerializeToDocument(patientOne);
            Assert.IsNotNull(doc);

            var root = doc.Root;
            Assert.AreEqual("Patient", root.Name.LocalName);
            Assert.IsTrue(root.HasElements);
            Assert.AreEqual(7, root.Elements().Count());
        }
        // #endif

        // [WMR 20180409] NEW: Serialize to JObject
        [TestMethod]
        public void TestSerializeToJsonDocument()
        {
            // Note: output order is defined by core resource/datatype definitions!

            var patientOne = new Patient
            {
                Id = "patient-one",
                Meta = new Meta { ElementId = "eric-clapton", VersionId = "1234" },
                Text = new Narrative { Status = Narrative.NarrativeStatus.Generated, Div = "<div>A great blues player</div>" },
                Active = true,
                Name = new List<HumanName> { new HumanName { Use = NameUse.Official, Family = new[] { "Clapton" } } },
                Gender = AdministrativeGender.Male,
                BirthDate = "2015-07-09",
            };

            var serializer = FhirDstu2JsonSerializer;
            var jsonText = serializer.SerializeToString(patientOne);
            Assert.IsNotNull(jsonText);

            var doc = JObject.Parse(jsonText);
            Assert.AreEqual(8, doc.Count); // Including resourceType

            JToken assertProperty(JToken t, string expectedName)
            {
                Assert.AreEqual(JTokenType.Property, t.Type);
                var p = t as JProperty;
                Assert.IsNotNull(p);
                Assert.AreEqual(expectedName, p.Name);
                return t;
            }

            JToken assertValue(JToken t, JTokenType expectedType, object expectedValue)
            {
                Assert.AreEqual(expectedType, t.Type);
                var v = t as JValue;
                Assert.IsNotNull(v);
                Assert.AreEqual(expectedValue, v.Value);
                return t;
            }

            JToken assertPrimitiveProperty(JToken t, string expectedName, JTokenType expectedType, object expectedValue)
            {
                var p = t as JProperty;
                Assert.IsNotNull(p);
                Assert.AreEqual(expectedName, p.Name);
                Assert.AreEqual(expectedType, p.Value.Type);
                var v = p.Value as JValue;
                Assert.IsNotNull(v);
                Assert.AreEqual(expectedValue, v.Value);
                return t;
            }

            JToken assertStringProperty(JToken t, string expectedName, object expectedValue)
                => assertPrimitiveProperty(t, expectedName, JTokenType.String, expectedValue);

            Assert.AreEqual(JTokenType.Property, doc.First.Type);
            var token = assertStringProperty(doc.First, "resourceType", "Patient");
            token = assertStringProperty(token.Next, "id", patientOne.Id);

            token = assertProperty(token.Next, "meta");
            Assert.IsTrue(token.HasValues);
            var childToken = assertStringProperty(token.Values().First(), "id", patientOne.Meta.ElementId);
            childToken = assertStringProperty(childToken.Next, "versionId", patientOne.Meta.VersionId);

            token = assertProperty(token.Next, "text");
            Assert.IsTrue(token.HasValues);
            childToken = assertStringProperty(token.Values().First(), "status", patientOne.Text.Status.GetLiteral());
            childToken = assertStringProperty(childToken.Next, "div", patientOne.Text.Div);

            token = assertPrimitiveProperty(token.Next, "active", JTokenType.Boolean, patientOne.Active);

            token = assertProperty(token.Next, "name");
            var values = token.First;
            Assert.IsNotNull(values);
            Assert.AreEqual(JTokenType.Array, values.Type);
            childToken = values.First;
            var grandChildToken = assertStringProperty(childToken.First, "use", patientOne.Name[0].Use.GetLiteral());

            grandChildToken = assertProperty(grandChildToken.Next, "family");
            values = grandChildToken.First;
            Assert.IsNotNull(values);
            Assert.AreEqual(JTokenType.Array, values.Type);
            assertValue(values.First, JTokenType.String, "Clapton");

            token = assertStringProperty(token.Next, "gender", patientOne.Gender.GetLiteral());

            token = assertStringProperty(token.Next, "birthDate", patientOne.BirthDate);

        }

        /// <summary>
        /// This test proves issue 583: https://github.com/FirelyTeam/fhir-net-api/issues/583
        /// </summary>
        [TestMethod]
        public void SummarizeSerializingTest()
        {
            var patient = new Patient();
            var telecom = new ContactPoint(ContactPointSystem.Phone, ContactPointUse.Work, "0471 144 099");
            telecom.AddExtension("http://healthconnex.com.au/hcxd/Phone/IsMain", new FhirBoolean(true));
            patient.Telecom.Add(telecom);

            var doc = FhirDstu2XmlSerializer.SerializeToString(patient, Fhir.Rest.SummaryType.True);

            Assert.IsFalse(doc.Contains("<extension"), "In the summary there must be no extension section.");

            doc = FhirDstu2XmlSerializer.SerializeToString(patient, Fhir.Rest.SummaryType.False);
            Assert.IsTrue(doc.Contains("<extension"), "Extension exists when Summary = false");
        }

        /// <summary>
        /// This test proves issue 657: https://github.com/FirelyTeam/fhir-net-api/issues/657
        /// </summary>
        [TestMethod]
        public void DateTimeOffsetAccuracyTest()
        {
            var patient = new Patient { Meta = new Meta { LastUpdated = DateTimeOffset.UtcNow } };
            var json = FhirDstu2JsonSerializer.SerializeToString(patient);
            var res = FhirDstu2JsonParser.Parse<Patient>(json);
            Assert.IsTrue(patient.IsExactly(res), "1");

            // Is the parsing still correct without milliseconds?
            patient = new Patient { Meta = new Meta { LastUpdated = new DateTimeOffset(2018, 8, 13, 13, 41, 56, TimeSpan.Zero)} };
            json = "{\"resourceType\":\"Patient\",\"meta\":{\"lastUpdated\":\"2018-08-13T13:41:56+00:00\"}}";
            res = FhirDstu2JsonParser.Parse<Patient>(json);
            Assert.IsTrue(patient.IsExactly(res), "2");

            // Is the serialization still correct without milliseconds?
            var json2 = FhirDstu2JsonSerializer.SerializeToString(patient);
            Assert.AreEqual(json, json2, "3");

            // Is the parsing still correct with a few milliseconds and TimeZone?
            patient = new Patient { Meta = new Meta { LastUpdated = new DateTimeOffset(2018, 8, 13, 13, 41, 56, 12, TimeSpan.Zero) } };
            json = "{\"resourceType\":\"Patient\",\"meta\":{\"lastUpdated\":\"2018-08-13T13:41:56.012+00:00\"}}";
            res = FhirDstu2JsonParser.Parse<Patient>(json);
            Assert.IsTrue(patient.IsExactly(res), "4");

            // Is the serialization still correct with a few milliseconds?
            json2 = FhirDstu2JsonSerializer.SerializeToString(patient);
            Assert.AreEqual(json, json2, "5");
        }

        [TestMethod]
        public void SerializerHandlesEmptyChildObjects()
        {
            var fhirJsonParser = FhirDstu2JsonParser;

            string json = TestDataHelper.ReadTestData("TestPatient.json");
            var poco = fhirJsonParser.Parse<Patient>(json);

            Assert.AreEqual(1, poco.Name.Count);

            poco.Meta = new Meta();

            var reserialized = poco.ToJson(Fhir.Model.Version.DSTU2);

            var newPoco = fhirJsonParser.Parse<Patient>(reserialized);

            Assert.AreEqual(1, newPoco.Name.Count);
        }

        [TestMethod]
        public void MultiVersionXmlSerialization()
        {
            var r4bundle = new Fhir.Model.R4.Bundle { Id = "101" };
            var fhirR4XmlSerializer = new FhirXmlSerializer(Fhir.Model.Version.R4);
            var xml = fhirR4XmlSerializer.SerializeToString(r4bundle);
            var fhirR4XmlParser = new FhirXmlParser(Fhir.Model.Version.R4);
            var deserializedR4bundle = fhirR4XmlParser.Parse<Resource>(xml) as Fhir.Model.R4.Bundle;
            Assert.IsNotNull(deserializedR4bundle);
            Assert.AreEqual(r4bundle.Id, deserializedR4bundle.Id);

            var stu3bundle = new Fhir.Model.STU3.Bundle { Id = "101" };
            var fhirStu3XmlSerializer = new FhirXmlSerializer(Fhir.Model.Version.STU3);
            xml = fhirStu3XmlSerializer.SerializeToString(stu3bundle);
            var fhirStu3XmlParser = new FhirXmlParser(Fhir.Model.Version.STU3);
            var deserializedStu3bundle = fhirStu3XmlParser.Parse<Resource>(xml) as Fhir.Model.STU3.Bundle;
            Assert.IsNotNull(deserializedStu3bundle);
            Assert.AreEqual(stu3bundle.Id, deserializedStu3bundle.Id);

            var dstu2bundle = new Bundle { Id = "101" };
            xml = FhirDstu2XmlSerializer.SerializeToString(dstu2bundle);
            var deserializedDstu2bundle = FhirDstu2XmlParser.Parse<Resource>(xml) as Bundle;
            Assert.IsNotNull(deserializedDstu2bundle);
            Assert.AreEqual(dstu2bundle.Id, deserializedDstu2bundle.Id);
        }

        [TestMethod]
        public void MultiVersionJsonSerialization()
        {
            var r4bundle = new Fhir.Model.R4.Bundle { Id = "101" };
            var fhirR4JsonSerializer = new FhirJsonSerializer(Fhir.Model.Version.R4);
            var json = fhirR4JsonSerializer.SerializeToString(r4bundle);
            var fhirR4JsonParser = new FhirJsonParser(Fhir.Model.Version.R4);
            var deserializedR4bundle = fhirR4JsonParser.Parse<Resource>(json) as Fhir.Model.R4.Bundle;
            Assert.IsNotNull(deserializedR4bundle);
            Assert.AreEqual(r4bundle.Id, deserializedR4bundle.Id);

            var stu3bundle = new Fhir.Model.STU3.Bundle { Id = "101" };
            var fhirStu3JsonSerializer = new FhirJsonSerializer(Fhir.Model.Version.STU3);
            json = fhirStu3JsonSerializer.SerializeToString(stu3bundle);
            var fhirStu3JsonParser = new FhirJsonParser(Fhir.Model.Version.STU3);
            var deserializedStu3bundle = fhirStu3JsonParser.Parse<Resource>(json) as Fhir.Model.STU3.Bundle;
            Assert.IsNotNull(deserializedStu3bundle);
            Assert.AreEqual(stu3bundle.Id, deserializedStu3bundle.Id);

            var dstu2bundle = new Bundle { Id = "101" };
            json = FhirDstu2JsonSerializer.SerializeToString(dstu2bundle);
            var deserializedDstu2bundle = FhirDstu2JsonParser.Parse<Resource>(json) as Bundle;
            Assert.IsNotNull(deserializedDstu2bundle);
            Assert.AreEqual(dstu2bundle.Id, deserializedDstu2bundle.Id);
        }

        [TestMethod]
        public void OperationOutcomeXmlSerialization()
        {
            var outcome = new OperationOutcome
            {
                Text = new Narrative { Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">not found!</div>", Status = Narrative.NarrativeStatus.Generated },
                Issue = new List<OperationOutcome.IssueComponent>
                {
                    new OperationOutcome.IssueComponent
                    {
                        Code = IssueType.NotFound.GetLiteral(),
                        Details = new CodeableConcept("mysystem","mycode","not found!"),
                        Diagnostics = "diag",
                        Location = new[] { "loc1, loc2" },
                        Severity = IssueSeverity.Error, 
                        Expression = new[]{ "/loc1/loc2" },
                    }
                }
            };
            var xml = FhirDstu2XmlSerializer.SerializeToString(outcome);
            var dstu2outcome = FhirDstu2XmlParser.Parse<OperationOutcome>(xml);
            Assert.AreEqual(outcome.Text.Div, dstu2outcome.Text.Div);
            Assert.AreEqual(outcome.Issue.Count, dstu2outcome.Issue.Count);
            var outcomeIssue = outcome.Issue[0];
            var dstu2outcomeIssue = dstu2outcome.Issue[0];
            Assert.AreEqual(outcomeIssue.Code, dstu2outcomeIssue.Code);
            Assert.AreEqual(outcomeIssue.Details.Text, dstu2outcomeIssue.Details.Text);
            Assert.AreEqual(outcomeIssue.Details.Coding.Count, dstu2outcomeIssue.Details.Coding.Count);
            Assert.AreEqual(outcomeIssue.Details.Coding[0].System, dstu2outcomeIssue.Details.Coding[0].System);
            Assert.AreEqual(outcomeIssue.Details.Coding[0].Code, dstu2outcomeIssue.Details.Coding[0].Code);
            Assert.AreEqual(outcomeIssue.Diagnostics, dstu2outcomeIssue.Diagnostics);
            Assert.IsTrue(Enumerable.SequenceEqual(outcomeIssue.Location, dstu2outcomeIssue.Location));
            Assert.AreEqual(outcomeIssue.Severity, dstu2outcomeIssue.Severity);
            Assert.IsFalse(dstu2outcomeIssue.Expression.Any());

            var fhirStu3XmlSerializer = new FhirXmlSerializer(Fhir.Model.Version.STU3);
            var fhirStu3XmlParser = new FhirXmlParser(Fhir.Model.Version.STU3);

            xml = fhirStu3XmlSerializer.SerializeToString(outcome);
            var stu3outcome = fhirStu3XmlParser.Parse<Fhir.Model.OperationOutcome>(xml);
            Assert.AreEqual(outcome.Text.Div, stu3outcome.Text.Div);
            Assert.AreEqual(outcome.Issue.Count, stu3outcome.Issue.Count);
            var stu3outcomeIssue = stu3outcome.Issue[0];
            Assert.AreEqual(outcomeIssue.Code, stu3outcomeIssue.Code);
            Assert.AreEqual(outcomeIssue.Details.Text, stu3outcomeIssue.Details.Text);
            Assert.AreEqual(outcomeIssue.Details.Coding.Count, stu3outcomeIssue.Details.Coding.Count);
            Assert.AreEqual(outcomeIssue.Details.Coding[0].System, stu3outcomeIssue.Details.Coding[0].System);
            Assert.AreEqual(outcomeIssue.Details.Coding[0].Code, stu3outcomeIssue.Details.Coding[0].Code);
            Assert.AreEqual(outcomeIssue.Diagnostics, stu3outcomeIssue.Diagnostics);
            Assert.IsTrue(Enumerable.SequenceEqual(outcomeIssue.Location, stu3outcomeIssue.Location));
            Assert.AreEqual(outcomeIssue.Severity, stu3outcomeIssue.Severity);
            Assert.IsTrue(Enumerable.SequenceEqual(outcomeIssue.Expression, stu3outcomeIssue.Expression));

            var fhirR4XmlSerializer = new FhirXmlSerializer(Fhir.Model.Version.R4);
            var fhirR4XmlParser = new FhirXmlParser(Fhir.Model.Version.R4);

            xml = fhirR4XmlSerializer.SerializeToString(outcome);
            var r4outcome = fhirR4XmlParser.Parse<Fhir.Model.OperationOutcome>(xml);
            Assert.AreEqual(outcome.Text.Div, stu3outcome.Text.Div);
            Assert.AreEqual(outcome.Issue.Count, stu3outcome.Issue.Count);
            var r4outcomeIssue = r4outcome.Issue[0];
            Assert.AreEqual(outcomeIssue.Code, r4outcomeIssue.Code);
            Assert.AreEqual(outcomeIssue.Details.Text, r4outcomeIssue.Details.Text);
            Assert.AreEqual(outcomeIssue.Details.Coding.Count, r4outcomeIssue.Details.Coding.Count);
            Assert.AreEqual(outcomeIssue.Details.Coding[0].System, r4outcomeIssue.Details.Coding[0].System);
            Assert.AreEqual(outcomeIssue.Details.Coding[0].Code, r4outcomeIssue.Details.Coding[0].Code);
            Assert.AreEqual(outcomeIssue.Diagnostics, r4outcomeIssue.Diagnostics);
            Assert.IsTrue(Enumerable.SequenceEqual(outcomeIssue.Location, r4outcomeIssue.Location));
            Assert.AreEqual(outcomeIssue.Severity, r4outcomeIssue.Severity);
            Assert.IsTrue(Enumerable.SequenceEqual(outcomeIssue.Expression, r4outcomeIssue.Expression));

            var exception = Assert.ThrowsException<FormatException>(() => FhirDstu2XmlParser.Parse<OperationOutcome>(xml));
            Assert.IsTrue(exception.Message.Contains("Encountered unknown element 'expression'"));
        }

        [TestMethod]
        public void OperationOutcomeJsonSerialization()
        {
            var outcome = new OperationOutcome
            {
                Text = new Narrative { Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">not found!</div>", Status = Narrative.NarrativeStatus.Generated },
                Issue = new List<OperationOutcome.IssueComponent>
                {
                    new OperationOutcome.IssueComponent
                    {
                        Code = IssueType.NotFound.GetLiteral(),
                        Details = new CodeableConcept("mysystem","mycode","not found!"),
                        Diagnostics = "diag",
                        Location = new[] { "loc1, loc2" },
                        Severity = IssueSeverity.Error,
                        Expression = new[] { "/loc1", "/loc2"}
                    }
                }
            };
            var json = FhirDstu2JsonSerializer.SerializeToString(outcome);
            var dstu2outcome = FhirDstu2JsonParser.Parse<OperationOutcome>(json);
            Assert.AreEqual(outcome.Text.Div, dstu2outcome.Text.Div);
            Assert.AreEqual(outcome.Issue.Count, dstu2outcome.Issue.Count);
            var outcomeIssue = outcome.Issue[0];
            var dstu2outcomeIssue = dstu2outcome.Issue[0];
            Assert.AreEqual(outcomeIssue.Code, dstu2outcomeIssue.Code);
            Assert.AreEqual(outcomeIssue.Details.Text, dstu2outcomeIssue.Details.Text);
            Assert.AreEqual(outcomeIssue.Details.Coding.Count, dstu2outcomeIssue.Details.Coding.Count);
            Assert.AreEqual(outcomeIssue.Details.Coding[0].System, dstu2outcomeIssue.Details.Coding[0].System);
            Assert.AreEqual(outcomeIssue.Details.Coding[0].Code, dstu2outcomeIssue.Details.Coding[0].Code);
            Assert.AreEqual(outcomeIssue.Diagnostics, dstu2outcomeIssue.Diagnostics);
            Assert.IsTrue(Enumerable.SequenceEqual(outcomeIssue.Location, dstu2outcomeIssue.Location));
            Assert.AreEqual(outcomeIssue.Severity, dstu2outcomeIssue.Severity);
            Assert.IsFalse(dstu2outcomeIssue.Expression.Any());

            var fhirStu3JsonSerializer = new FhirJsonSerializer(Fhir.Model.Version.STU3);
            var fhirStu3JsonParser = new FhirJsonParser(Fhir.Model.Version.STU3);
            json = fhirStu3JsonSerializer.SerializeToString(outcome);
            var stu3outcome = fhirStu3JsonParser.Parse<OperationOutcome>(json);
            Assert.AreEqual(outcome.Text.Div, stu3outcome.Text.Div);
            Assert.AreEqual(outcome.Issue.Count, stu3outcome.Issue.Count);
            var stu3outcomeIssue = stu3outcome.Issue[0];
            Assert.AreEqual(outcomeIssue.Code, stu3outcomeIssue.Code);
            Assert.AreEqual(outcomeIssue.Details.Text, stu3outcomeIssue.Details.Text);
            Assert.AreEqual(outcomeIssue.Details.Coding.Count, stu3outcomeIssue.Details.Coding.Count);
            Assert.AreEqual(outcomeIssue.Details.Coding[0].System, stu3outcomeIssue.Details.Coding[0].System);
            Assert.AreEqual(outcomeIssue.Details.Coding[0].Code, stu3outcomeIssue.Details.Coding[0].Code);
            Assert.AreEqual(outcomeIssue.Diagnostics, stu3outcomeIssue.Diagnostics);
            Assert.IsTrue(Enumerable.SequenceEqual(outcomeIssue.Location, stu3outcomeIssue.Location));
            Assert.AreEqual(outcomeIssue.Severity, stu3outcomeIssue.Severity);
            Assert.IsTrue(Enumerable.SequenceEqual(outcomeIssue.Expression, stu3outcomeIssue.Expression));

            var fhirR4JsonSerializer = new FhirJsonSerializer(Fhir.Model.Version.R4);
            var fhirR4JsonParser = new FhirJsonParser(Fhir.Model.Version.R4);
            json = fhirR4JsonSerializer.SerializeToString(outcome);
            var r4outcome = fhirR4JsonParser.Parse<OperationOutcome>(json);
            Assert.AreEqual(outcome.Text.Div, r4outcome.Text.Div);
            Assert.AreEqual(outcome.Issue.Count, r4outcome.Issue.Count);
            var r4outcomeIssue = stu3outcome.Issue[0];
            Assert.AreEqual(outcomeIssue.Code, r4outcomeIssue.Code);
            Assert.AreEqual(outcomeIssue.Details.Text, r4outcomeIssue.Details.Text);
            Assert.AreEqual(outcomeIssue.Details.Coding.Count, r4outcomeIssue.Details.Coding.Count);
            Assert.AreEqual(outcomeIssue.Details.Coding[0].System, r4outcomeIssue.Details.Coding[0].System);
            Assert.AreEqual(outcomeIssue.Details.Coding[0].Code, r4outcomeIssue.Details.Coding[0].Code);
            Assert.AreEqual(outcomeIssue.Diagnostics, r4outcomeIssue.Diagnostics);
            Assert.IsTrue(Enumerable.SequenceEqual(outcomeIssue.Location, r4outcomeIssue.Location));
            Assert.AreEqual(outcomeIssue.Severity, r4outcomeIssue.Severity);
            Assert.IsTrue(Enumerable.SequenceEqual(outcomeIssue.Expression, r4outcomeIssue.Expression));

            var exception = Assert.ThrowsException<FormatException>(() => FhirDstu2JsonParser.Parse<OperationOutcome>(json));
            Assert.IsTrue(exception.Message.Contains("Encountered unknown element 'expression'"));
        }
    }
}