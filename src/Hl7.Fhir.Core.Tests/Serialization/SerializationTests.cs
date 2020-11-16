/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
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
            var xml = new FhirXmlSerializer().SerializeToString(metaPoco, root: "meta");
            Assert.AreEqual(metaXml, xml);
        }


        [TestMethod]
        public void SerializeMetaJson()
        {
            var json = new FhirJsonSerializer().SerializeToString(metaPoco);
            Assert.AreEqual(metaJson, json);
        }

        [TestMethod]
        public void ParseMetaXml()
        {
            var poco = (Meta)(new FhirXmlParser().Parse(metaXml, typeof(Meta)));
            var xml = new FhirXmlSerializer().SerializeToString(poco, root: "meta");

            Assert.IsTrue(poco.IsExactly(metaPoco));
            Assert.AreEqual(metaXml, xml);
        }

        [TestMethod]
        public void ParsePatientXmlNullType()
        {
            string xmlPacientTest = TestDataHelper.ReadTestData("TestPatient.xml");
            
            var poco = new FhirXmlParser().Parse(xmlPacientTest);
            
            Assert.AreEqual(((Patient)poco).Id, "pat1");
            Assert.AreEqual(((Patient)poco).Contained.First().Id, "1");
            Assert.AreEqual(((Patient)poco).Name.First().Family, "Donald");
            Assert.AreEqual(((Patient)poco).ManagingOrganization.Reference, "Organization/1");
        }

        internal FhirXmlSerializer FhirXmlSerializer = new FhirXmlSerializer();
        internal FhirJsonSerializer FhirJsonSerializer = new FhirJsonSerializer();

        [TestMethod]
        public void ParseMetaJson()
        {
            var poco = (Meta)(new FhirJsonParser().Parse(metaJson, typeof(Meta)));
            var json = FhirJsonSerializer.SerializeToString(poco);

            Assert.IsTrue(poco.IsExactly(metaPoco));
            Assert.AreEqual(metaJson, json);
        }

        [TestMethod]
        public void ParsePatientJsonNullType()
        {
            string jsonPatient = TestDataHelper.ReadTestData("TestPatient.json");

            var poco = new FhirJsonParser().Parse(jsonPatient);

            Assert.AreEqual(((Patient)poco).Id, "pat1");
            Assert.AreEqual(((Patient)poco).Contained.First().Id, "1");
            Assert.AreEqual(((Patient)poco).Name.First().Family, "Donald");
            Assert.AreEqual(((Patient)poco).ManagingOrganization.Reference, "Organization/1");
        }

        [TestMethod]
        public void AvoidBOMUse()
        {
            Bundle b = new Bundle() { Total = 1000 };

            var data = FhirJsonSerializer.SerializeToBytes(b);
            Assert.IsFalse(data[0] == Encoding.UTF8.GetPreamble()[0]);

            data = FhirXmlSerializer.SerializeToBytes(b);
            Assert.IsFalse(data[0] == Encoding.UTF8.GetPreamble()[0]);

            Patient p = new Patient() { Active = true };

            data = FhirJsonSerializer.SerializeToBytes(p);
            Assert.IsFalse(data[0] == Encoding.UTF8.GetPreamble()[0]);

            data = FhirXmlSerializer.SerializeToBytes(p);
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

        private FhirXmlParser FhirXmlParser = new FhirXmlParser();
        private FhirJsonParser FhirJsonParser = new FhirJsonParser();


        //[TestMethod]
        //public void HandleCommentsJson()
        //{
        //    string json = TestDataHelper.ReadTestData("TestPatient.json");

        //    var pat = FhirJsonParser.Parse<Patient>(json);

        //    Assert.AreEqual(1, pat.Telecom[0].FhirCommentsElement.Count);
        //    Assert.AreEqual("   home communication details aren't known   ", pat.Telecom[0].FhirComments.First());

        //    pat.Telecom[0].FhirCommentsElement.Add(new FhirString("A second line"));

        //    json = FhirJsonSerializer.SerializeToString(pat);
        //    pat = FhirJsonParser.Parse<Patient>(json);

        //    Assert.AreEqual(2, pat.Telecom[0].FhirCommentsElement.Count);
        //    Assert.AreEqual("   home communication details aren't known   ", pat.Telecom[0].FhirComments.First());
        //    Assert.AreEqual("A second line", pat.Telecom[0].FhirComments.Skip(1).First());
        //}

        //[TestMethod, Ignore]
        //public void HandleCommentsXml()
        //{
        //    string xml = TestDataHelper.ReadTestData("TestPatient.xml");

        //    var pat = FhirXmlParser.Parse<Patient>(xml);

        //    Assert.AreEqual(1, pat.Name[0].FhirCommentsElement.Count);
        //    Assert.AreEqual("See if this is roundtripped", pat.Name[0].FhirComments.First());

        //    pat.Name[0].FhirCommentsElement.Add(new FhirString("A second line"));

        //    xml = FhirXmlSerializer.SerializeToString(pat);

        //    Assert.AreEqual(2, pat.Name[0].FhirCommentsElement.Count);
        //    Assert.AreEqual("See if this is roundtripped", pat.Name[0].FhirComments.First());
        //    Assert.AreEqual("A second line", pat.Name[0].FhirComments.Skip(1).First());
        //}


        [TestMethod]
        public void BundleLinksUnaltered()
        {
            var b = new Bundle
            {
                NextLink = new Uri("Organization/123456/_history/123456", UriKind.Relative)
            };

            var xml = new FhirXmlSerializer().SerializeToString(b);

            b = FhirXmlParser.Parse<Bundle>(xml);

            Assert.IsTrue(!b.NextLink.ToString().EndsWith("/"));
        }

        [TestMethod]
        public void TestDecimalPrecisionSerializationInJson()
        {
            var dec6 = 6m;
            var dec60 = 6.0m;
            var ext = new FhirDecimal(dec6);
            var obs = new Observation();
            obs.AddExtension("http://example.org/DecimalPrecision", ext);
            
            var json = FhirJsonSerializer.SerializeToString(obs);
            var obs2 = FhirJsonParser.Parse<Observation>(json);

            Assert.AreEqual("6", ((FhirDecimal)obs2.GetExtension("http://example.org/DecimalPrecision").Value).Value.Value.ToString(CultureInfo.InvariantCulture));

            ext = new FhirDecimal(dec60);
            obs = new Observation();
            obs.AddExtension("http://example.org/DecimalPrecision", ext);

            json = FhirJsonSerializer.SerializeToString(obs);
            obs2 = FhirJsonParser.Parse<Observation>(json);

            Assert.AreEqual("6.0", ((FhirDecimal)obs2.GetExtension("http://example.org/DecimalPrecision").Value).Value.Value.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestLongDecimalSerialization()
        {
            var dec = 3.1415926535897932384626433833m;
            var ext = new FhirDecimal(dec);
            var obs = new Observation();
            obs.AddExtension("http://example.org/DecimalPrecision", ext);

            var json = FhirJsonSerializer.SerializeToString(obs);
            var obs2 = FhirJsonParser.Parse<Observation>(json);

            Assert.AreEqual(dec.ToString(CultureInfo.InvariantCulture), ((FhirDecimal)obs2.GetExtension("http://example.org/DecimalPrecision").Value).Value.Value.ToString(CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void TestParseUnkownPolymorphPropertyInJson()
        {
            var dec6 = 6m;
            var ext = new FhirDecimal(dec6);
            var obs = new Observation{ Value = new FhirDecimal(dec6) };
            var json = FhirJsonSerializer.SerializeToString(obs);
            try
            {
                var obs2 = FhirJsonParser.Parse<Observation>(json);
                Assert.Fail("valueDecimal is not a known type for Observation");
            }
            catch (FormatException)
            {

            }
        }

        [TestMethod]
        public void TestSerializeWhitespacesInXml()
        {
            var patient = new Patient
            {
                Name = new List<HumanName>
                {
                    new HumanName { Family = " Smith\r\n\t" }
                }
            };

            var trimmed = FhirXmlSerializer.SerializeToString(patient);
            Assert.IsFalse(trimmed.Contains(" Smith"));
            Assert.IsFalse(trimmed.Contains("Smith&#xD;&#xA;&#x9;"));
            Assert.IsTrue(trimmed.Contains("\"Smith\""));

            var notTrimmed = new FhirXmlSerializer(new SerializerSettings { TrimWhiteSpacesInXml = false }).SerializeToString(patient);
            Assert.IsTrue(notTrimmed.Contains(" Smith"));
            Assert.IsTrue(notTrimmed.Contains("Smith&#xD;&#xA;&#x9;"));
            Assert.IsFalse(notTrimmed.Contains("\"Smith\""));

        }

        [TestMethod]
        public void TryScriptInject()
        {
            var x = new Patient();

            x.Name.Add(HumanName.ForFamily("<script language='javascript'></script>"));

            var xml = FhirXmlSerializer.SerializeToString(x);
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
            catch (FormatException e)
            {
                Assert.IsTrue(e.Message.Contains("DTD is prohibited"));
            }
        }

        [TestMethod]
        public void SerializeUnknownEnums()
        {
            string xml = TestDataHelper.ReadTestData("TestPatient.xml");
            var pser = new FhirXmlParser();
            var p = pser.Parse<Patient>(xml);
            string outp = FhirXmlSerializer.SerializeToString(p);
            Assert.IsTrue(outp.Contains("\"male\""));

            // Pollute the data with an incorrect administrative gender
            p.GenderElement.ObjectValue = "superman";

            outp = FhirXmlSerializer.SerializeToString(p);
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

            var xml = FhirXmlSerializer.SerializeToString(p);

            var p2 = (new FhirXmlParser()).Parse<Patient>(xml);
            Assert.AreEqual(1, p2.Extension.Count);
            Assert.AreEqual(1, p2.Contact.Count);
        }

        //An empty object is not allowed
        //[TestMethod]
        //public void SerializeEmptyParams()
        //{
        //    var par = new Parameters();
        //    var xml = FhirXmlSerializer.SerializeToString(par);

        //    var par2 = (new FhirXmlParser()).Parse<Parameters>(xml);
        //    Assert.AreEqual(0, par2.Parameter.Count);
        //}

        [TestMethod]
        public void SerializeJsonWithPlainDiv()
        {
            // var res = new ValueSet() { Url = "http://example.org/fhir/ValueSet/MyValueSetExample" };

            string json = TestDataHelper.ReadTestData(@"valueset-v2-0717.json");
            Assert.IsNotNull(json);
            var parser = new FhirJsonParser { Settings = { PermissiveParsing = true} };
            var vs = parser.Parse<ValueSet>(json);
            Assert.IsNotNull(vs);

            var xml = FhirXmlSerializer.SerializeToString(vs);
            Assert.IsNotNull(xml);
        }

        /// <summary>
        /// This test verifies that the parser can handle a backbone element that has a property of resourceType
        /// (only found in the ExampleScenario resource in R4 - used to be in Claim)
        /// </summary>
        [TestMethod]
        public void TestExampleScenarioJsonSerialization()
        {
            var es = new ExampleScenario();
            es.Instance.Add(new ExampleScenario.InstanceComponent()
            {
                ResourceType = ResourceType.ExampleScenario,
                Name = "brian"
            });

            string json = FhirJsonSerializer.SerializeToString(es);
            var c2 = new FhirJsonParser().Parse<ExampleScenario>(json);
            Assert.AreEqual("brian", c2.Instance[0].Name);
            Assert.AreEqual("ExampleScenario", c2.Instance[0].ResourceTypeElement.ObjectValue as string);
        }

        [FhirType("Bundle")]
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
            ModelInfo.GetStructureDefinitionSummaryProvider().ImportType(typeof(CustomBundle));

            var bundle = new CustomBundle()
            {
                Type = Bundle.BundleType.Collection,
                Id = "MyBundle"
            };

            var xml = FhirXmlSerializer.SerializeToString(bundle);
            Assert.IsNotNull(xml);

            var json = FhirJsonSerializer.SerializeToString(bundle);
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
                Text = new Narrative { Status = Narrative.NarrativeStatus.Generated, Div = "<div xmlns='http://www.w3.org/1999/xhtml'>A great blues player</div>" },
                Meta = new Meta { ElementId = "eric-clapton", VersionId = "1234" },

                Name = new List<HumanName> { new HumanName { Family = "Clapton", Use = HumanName.NameUse.Official } },

                Active = true,
                BirthDate = "2015-07-09",
                Gender = AdministrativeGender.Male
            };

            var doc = FhirXmlSerializer.SerializeToDocument(patientOne);
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
                Text = new Narrative { Status = Narrative.NarrativeStatus.Generated, Div = "<div xmlns='http://www.w3.org/1999/xhtml'>A great blues player</div>" },
                Active = true,
                Name = new List<HumanName> { new HumanName { Use = HumanName.NameUse.Official, Family = "Clapton" } },
                Gender = AdministrativeGender.Male,
                BirthDate = "2015-07-09",
            };

            var serializer = new FhirJsonSerializer();
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

            JToken assertStringProperty(JToken t, string expectedName, object expectedValue) => assertPrimitiveProperty(t, expectedName, JTokenType.String, expectedValue);

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
            grandChildToken = assertStringProperty(grandChildToken.Next, "family", patientOne.Name[0].Family);

            token = assertStringProperty(token.Next, "gender", patientOne.Gender.GetLiteral());

            token = assertStringProperty(token.Next, "birthDate", patientOne.BirthDate);

        }

        /// <summary>
        /// This test proves issue 583: https://github.com/FirelyTeam/firely-net-sdk/issues/583
        /// </summary>
        [TestMethod]
        public void SummarizeSerializingTest()
        {
            var patient = new Patient();
            var telecom = new ContactPoint(ContactPoint.ContactPointSystem.Phone, ContactPoint.ContactPointUse.Work, "0471 144 099");
            telecom.AddExtension("http://healthconnex.com.au/hcxd/Phone/IsMain", new FhirBoolean(true));
            patient.Telecom.Add(telecom);

            var doc = FhirXmlSerializer.SerializeToString(patient, Fhir.Rest.SummaryType.True);

            Assert.IsFalse(doc.Contains("<extension"), "In the summary there must be no extension section.");

            doc = FhirXmlSerializer.SerializeToString(patient, Fhir.Rest.SummaryType.False);
            Assert.IsTrue(doc.Contains("<extension"), "Extension exists when Summary = false");
        }

        /// <summary>
        /// This test proves issue 657: https://github.com/FirelyTeam/firely-net-sdk/issues/657
        /// </summary>
        [TestMethod]
        public void DateTimeOffsetAccuracyTest()
        {
            var patient = new Patient { Meta = new Meta { LastUpdated = DateTimeOffset.UtcNow } };
            var json = new FhirJsonSerializer().SerializeToString(patient);
            var res = new FhirJsonParser().Parse<Patient>(json);
            Assert.IsTrue(patient.IsExactly(res), "1");

            // Is the parsing still correct without milliseconds?
            patient = new Patient { Meta = new Meta { LastUpdated = new DateTimeOffset(2018, 8, 13, 13, 41, 56, TimeSpan.Zero)} };
            json = "{\"resourceType\":\"Patient\",\"meta\":{\"lastUpdated\":\"2018-08-13T13:41:56+00:00\"}}";
            res = new FhirJsonParser().Parse<Patient>(json);
            Assert.IsTrue(patient.IsExactly(res), "2");

            // Is the serialization still correct without milliseconds?
            var json2 = new FhirJsonSerializer().SerializeToString(patient);
            Assert.AreEqual(json, json2, "3");

            // Is the parsing still correct with a few milliseconds and TimeZone?
            patient = new Patient { Meta = new Meta { LastUpdated = new DateTimeOffset(2018, 8, 13, 13, 41, 56, 12, TimeSpan.Zero) } };
            json = "{\"resourceType\":\"Patient\",\"meta\":{\"lastUpdated\":\"2018-08-13T13:41:56.012+00:00\"}}";
            res = new FhirJsonParser().Parse<Patient>(json);
            Assert.IsTrue(patient.IsExactly(res), "4");

            // Is the serialization still correct with a few milliseconds?
            json2 = new FhirJsonSerializer().SerializeToString(patient);
            Assert.AreEqual(json, json2, "5");
        }

        [TestMethod]
        public void SerializerHandlesEmptyChildObjects()
        {
            var fhirJsonParser = new FhirJsonParser();

            string json = TestDataHelper.ReadTestData("TestPatient.json");
            var poco = fhirJsonParser.Parse<Patient>(json);

            Assert.AreEqual(1, poco.Name.Count);

            poco.Meta = new Meta();

            var reserialized = poco.ToJson();

            var newPoco = fhirJsonParser.Parse<Patient>(reserialized);

            Assert.AreEqual(1, newPoco.Name.Count);
        }
    }
}