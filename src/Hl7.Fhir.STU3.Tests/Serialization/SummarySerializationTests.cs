/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
    public class SummarySerializationTests
    {
        private readonly FhirXmlSerializer FhirXmlSerializer = new FhirXmlSerializer();
        private readonly FhirJsonSerializer FhirJsonSerializer = new FhirJsonSerializer();
        private readonly FhirXmlDeserializer _fhirXmlDeserializer = new FhirXmlDeserializer();

        [TestMethod]
        public void TestConstructSystemTextJsonSerializer()
        {
            JsonSerializerOptions options = new JsonSerializerOptions().ForFhir();
            var p = new Patient
            {
                BirthDate = "1972-11-30",     // present in both summary and full
                Photo = [new Attachment() { ContentType = "text/plain" }]
            };


            var jsonText = JsonSerializer.Serialize(p, options);
            Assert.Contains("birthDate", jsonText);
        }

        [TestMethod] // Old tests, I'm note sure we need them anymore
        public async Tasks.Task TestSummary()
        {
            var p = new Patient
            {
                BirthDate = "1972-11-30",     // present in both summary and full
                Photo = [new Attachment() { ContentType = "text/plain" }]
            };

            var full = FhirXmlSerializer.SerializeToString(p);
            Assert.Contains("<birthDate", full);
            Assert.Contains("<photo", full);
            Assert.IsNull(p.Meta, "Meta element should not be introduced here.");

            var summ = FhirXmlSerializer.SerializeToString(p, summary: Fhir.Rest.SummaryType.True);
            Assert.Contains("<birthDate", summ);
            Assert.DoesNotContain("<photo", summ);
            Assert.IsNull(p.Meta, "Meta element should not be introduced here.");

            var q = new Questionnaire
            {
                Text = new Narrative()
                {
                    Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">Test Questionnaire</div>"
                },
                Status = PublicationStatus.Active,
                Date = "2015-09-27",
                Title = "TITLE",
                Item =
                [
                    new Questionnaire.ItemComponent() { LinkId = "linkid", Text = "TEXT" }

                ]
            };

            Assert.IsNull(q.Meta, "Meta element has not been created.");
            var qfull = FhirXmlSerializer.SerializeToString(q);
            Assert.IsNull(q.Meta, "Meta element should not be introduced here.");
            Console.WriteLine("summary: Fhir.Rest.SummaryType.False");
            Console.WriteLine(qfull);
            Assert.Contains("Test Questionnaire", qfull);
            Assert.Contains("<status value=\"active\"", qfull);
            Assert.Contains("<date value=\"2015-09-27\"", qfull);
            Assert.Contains("<title value=\"TITLE\"", qfull);
            Assert.Contains("<text value=\"TEXT\"", qfull);
            Assert.Contains("<linkId value=\"linkid\"", qfull);

            var qSum = FhirXmlSerializer.SerializeToString(q, summary: Fhir.Rest.SummaryType.True);
            Console.WriteLine("summary: Fhir.Rest.SummaryType.True");
            Console.WriteLine(qSum);
            Assert.DoesNotContain("Test Questionnaire", qSum);
            Assert.Contains("<status value=\"active\"", qSum);
            Assert.Contains("<date value=\"2015-09-27\"", qSum);
            Assert.Contains("<title value=\"TITLE\"", qSum);
            Assert.DoesNotContain("<text value=\"TEXT\"", qSum);
            Assert.DoesNotContain("<linkId value=\"linkid\"", qSum);

            var qData = FhirXmlSerializer.SerializeToString(q, summary: Fhir.Rest.SummaryType.Data);
            Console.WriteLine("summary: Fhir.Rest.SummaryType.Data");
            Console.WriteLine(qData);
            Assert.DoesNotContain("Test Questionnaire", qData);
            Assert.Contains("<meta", qData);
            Assert.Contains("<text value=\"TEXT\"", qData);
            Assert.Contains("<status value=\"active\"", qData);
            Assert.Contains("<date value=\"2015-09-27\"", qData);
            Assert.Contains("<title value=\"TITLE\"", qData);
            Assert.Contains("<linkId value=\"linkid\"", qData);

            q.Meta = new Meta { VersionId = "v2" };
            var qText = FhirXmlSerializer.SerializeToString(q, summary: Fhir.Rest.SummaryType.Text);
            Console.WriteLine("summary: Fhir.Rest.SummaryType.Text");
            Console.WriteLine(qText);
            Assert.Contains("Test Questionnaire", qText);
            Assert.Contains("<meta", qText);
            Assert.Contains("<status value=\"active\"", qText);
            Assert.DoesNotContain("<text value=\"TEXT\"", qText);
            Assert.DoesNotContain("<date value=\"2015-09-27\"", qText);
            Assert.DoesNotContain("<title value=\"TITLE\"", qText);
            Assert.DoesNotContain("<linkId value=\"linkid\"", qText);
            Assert.AreEqual(0, q.Meta.Tag.Count(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED"), "Subsetted Tag should not still be there.");

            // Verify that reloading the content into an object...
            // make sure we accept the crappy output with empty groups
            var nav = await FhirXmlNode.ParseAsync(qText, new FhirXmlParsingSettings { PermissiveParsing = true });

            var qInflate = nav.ToPoco<Questionnaire>();
            Assert.AreEqual(1, qInflate.Meta.Tag.Count(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED"), "Subsetted Tag should not still be there.");
        }

        [TestMethod]
        public void TestIncludeMandatory()
        {
            var l = new Library
            {
                Type = new CodeableConcept { TextElement = new FhirString("testMandatoryComplexType") }, Id = "testId",
                Language = "testLang"
            };
            var summaryElements = FhirXmlSerializer.SerializeToString(l, Fhir.Rest.SummaryType.Count);

            Assert.DoesNotContain("<language", summaryElements);
            Assert.Contains("<type>", summaryElements);
            Assert.Contains("<id value=\"testId\"", summaryElements);

            var customMaskingNode = new MaskingNode(new ScopedNode(l.ToTypedElement()), new MaskingNodeSettings
            {
                IncludeMandatory = true,
                PreserveBundle = MaskingNodeSettings.PreserveBundleMode.All
            });

            var result = customMaskingNode.ToXml();

            Assert.DoesNotContain("<language>", result);
            Assert.Contains("<type>", result);
            Assert.DoesNotContain("<id value=\"testId\"", result);

            var b = new Bundle
            {
                TypeElement = new Code<Bundle.BundleType> { Value = Bundle.BundleType.Collection },
                Entry = new List<Bundle.EntryComponent>()
                {
                    new Bundle.EntryComponent { Resource = l }
                },
                Id = "bundle-id"
            };

            var customMaskingNodeForBundle = new MaskingNode(new ScopedNode(b.ToTypedElement()), new MaskingNodeSettings
            {
                IncludeMandatory = true,
                PreserveBundle = MaskingNodeSettings.PreserveBundleMode.None
            });

            result = customMaskingNodeForBundle.ToXml();

            Assert.Contains("<type value=\"collection\"", result);
            Assert.DoesNotContain("<id value=\"bundle-id\"", result);
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

            var summaryElements = FhirXmlSerializer.SerializeToString(p, SummaryType.False, elements: elements);
            Assert.DoesNotContain("<birthDate", summaryElements);
            Assert.Contains("<photo", summaryElements);

            var noSummarySpecified = FhirXmlSerializer.SerializeToString(p, SummaryType.False, elements: elements);
            Assert.DoesNotContain("<birthDate", noSummarySpecified);
            Assert.Contains("<photo", noSummarySpecified);
        }

        [TestMethod]
        public void TestWithMetadata()
        {
            var p = new Patient
            {
                BirthDate = "1972-11-30"
            };

            var pSum = FhirXmlSerializer.SerializeToString(p, summary: SummaryType.True);
            Assert.IsNull(p.Meta, "Meta should not be there");

            p.Meta = new Meta { VersionId = "v2" }; // introducing meta data ourselves. 

            pSum = FhirXmlSerializer.SerializeToString(p, summary: Fhir.Rest.SummaryType.True);
            Assert.IsNotNull(p.Meta, "Meta should still be there");
            Assert.AreEqual(0, p.Meta.Tag.Where(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED").Count(), "Subsetted Tag should not still be there.");
        }


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
            b.Type = Bundle.BundleType.Searchset;

            var full = FhirXmlSerializer.SerializeToString(b);
            Assert.Contains("<entry", full);
            Assert.Contains("<birthDate", full);
            Assert.Contains("<photo", full);
            Assert.Contains("<total", full);

            var summ = FhirXmlSerializer.SerializeToString(b, summary: Fhir.Rest.SummaryType.True);
            Assert.Contains("<entry", summ);
            Assert.Contains("<birthDate", summ);
            Assert.DoesNotContain("<photo", summ);
            Assert.Contains("<total", summ);

            summ = FhirXmlSerializer.SerializeToString(b, summary: Fhir.Rest.SummaryType.Count);
            Assert.DoesNotContain("<entry", summ);
            Assert.DoesNotContain("<birthDate", summ);
            Assert.DoesNotContain("<photo", summ);
            Assert.Contains("<total", summ);
            Assert.Contains("<type", summ);
        }

        [TestMethod]
        [DataRow("summary/bundle-summary-true.json", SummaryType.True)]
        [DataRow("summary/bundle-summary-false.json", SummaryType.False)]
        [DataRow("summary/bundle-summary-data.json", SummaryType.Data)]
        [DataRow("summary/bundle-summary-text.json", SummaryType.Text)]
        [DataRow("summary/bundle-summary-count.json", SummaryType.Count)]
        [DataRow("summary/bundle-summary-true.xml", SummaryType.True)]
        [DataRow("summary/bundle-summary-false.xml", SummaryType.False)]
        [DataRow("summary/bundle-summary-data.xml", SummaryType.Data)]
        [DataRow("summary/bundle-summary-text.xml", SummaryType.Text)]
        [DataRow("summary/bundle-summary-count.xml", SummaryType.Count)]
        public void TestBundleWithSummaryJson(string expectedFile, SummaryType mode)
        {
            var patientOne = new Patient
            {

                Id = "patient-one",
                Text = new Narrative { Div = "<div xmlns='http://www.w3.org/1999/xhtml'>A great blues player</div>" },
                Meta = new Meta { VersionId = "eric-clapton" },

                Name = [new HumanName { Family = "Clapton", Use = HumanName.NameUse.Official }],

                Active = true,
                BirthDate = "2015-07-09",
                Gender = AdministrativeGender.Male
            };

            var patientTwo = new Patient()
            {
                Id = "patient-two",
                Active = true,
                Text = new Narrative { Div = "<div xmlns='http://www.w3.org/1999/xhtml'>Another great blues player</div>", Status = Narrative.NarrativeStatus.Additional },
                Meta = new Meta { VersionId = "bb-king" },
                Name = [new HumanName { Family = "King", Use = HumanName.NameUse.Nickname }]
            };

            var bundle = new Bundle()
            {
                Id = "my-bundle",
                Total = 1803,
                Type = Bundle.BundleType.Searchset,
                Entry =
                [
                    new()
                    {
                        Resource = patientOne,
                        FullUrl = "http://base/Patient/patient-one",
                        Search = new Bundle.SearchComponent() { Mode = Bundle.SearchEntryMode.Match }
                    },
                    new()
                    {
                        Resource = patientTwo,
                        FullUrl = "http://base/Patient/patient-two",
                        Search = new Bundle.SearchComponent() { Mode = Bundle.SearchEntryMode.Match }
                    }
                ]
            };

            bool inJson = Path.GetExtension(expectedFile) == ".json";
            var actualData = inJson ? FhirJsonSerializer.SerializeToString(bundle, mode) :
                                FhirXmlSerializer.SerializeToString(bundle, mode);
            var expectedData = TestDataHelper.ReadTestData(expectedFile);

            if(inJson)
                JsonAssert.AreSame(expectedFile, expectedData, actualData);
            else
                XmlAssert.AreSame(expectedFile, XDocument.Parse(expectedData), XDocument.Parse(actualData));
        }

        [TestMethod]
        [DataRow("summary/summary-true.json", SummaryType.True)]
        [DataRow("summary/summary-false.json", SummaryType.False)]
        [DataRow("summary/summary-data.json", SummaryType.Data)]
        [DataRow("summary/summary-text.json", SummaryType.Text)]
        [DataRow("summary/summary-true.xml", SummaryType.True)]
        [DataRow("summary/summary-false.xml", SummaryType.False)]
        [DataRow("summary/summary-data.xml", SummaryType.Data)]
        [DataRow("summary/summary-text.xml", SummaryType.Text)]
        public void TestResourceWithSummary(string expectedFile, SummaryType mode)
        {
            var patientOne = new Patient
            {
                Id = "patient-one",
                Text = new Narrative { Status = Narrative.NarrativeStatus.Generated, Div = "<div xmlns='http://www.w3.org/1999/xhtml'>A great blues player</div>" },
                Meta = new Meta { ElementId = "eric-clapton", VersionId = "1234" },

                Name = [new HumanName { Family = "Clapton", Use = HumanName.NameUse.Official }],

                Active = true,
                BirthDate = "2015-07-09",
                Gender = AdministrativeGender.Male
            };

            bool inJson = Path.GetExtension(expectedFile) == ".json";
            var actualData = inJson ? FhirJsonSerializer.SerializeToString(patientOne, mode) :
                                FhirXmlSerializer.SerializeToString(patientOne, mode);
            var expectedData = TestDataHelper.ReadTestData(expectedFile);

            if(inJson)
                JsonAssert.AreSame(expectedFile, expectedData, actualData);
            else
                XmlAssert.AreSame(expectedFile, XDocument.Parse(expectedData), XDocument.Parse(actualData));
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

            var full = FhirXmlSerializer.SerializeToString(p);
            Assert.Contains("narrative", full);
            Assert.Contains("dud", full);
            Assert.Contains("temp org", full);
            Assert.Contains("<id value=", full);
            Assert.Contains("<birthDate", full);
            Assert.Contains("<photo", full);
            Assert.Contains("text/plain", full);

            full = FhirXmlSerializer.SerializeToString(p, summary: Hl7.Fhir.Rest.SummaryType.False);
            Assert.Contains("narrative", full);
            Assert.Contains("dud", full);
            Assert.Contains("temp org", full);
            Assert.Contains("contain", full);
            Assert.Contains("<id value=", full);
            Assert.Contains("<birthDate", full);
            Assert.Contains("<photo", full);
            Assert.Contains("text/plain", full);

            var summ = FhirXmlSerializer.SerializeToString(p, summary: Fhir.Rest.SummaryType.True);
            Assert.DoesNotContain("narrative", summ);
            Assert.DoesNotContain("dud", summ);
            Assert.DoesNotContain("contain", summ);
            Assert.Contains("temp org", summ);
            Assert.Contains("<id value=", summ);
            Assert.Contains("<birthDate", summ);
            Assert.DoesNotContain("<photo", summ);

            var data = FhirXmlSerializer.SerializeToString(p, summary: Hl7.Fhir.Rest.SummaryType.Data);
            Assert.DoesNotContain("narrative", data);
            Assert.Contains("contain", data);
            Assert.Contains("dud", data);
            Assert.Contains("temp org", data);
            Assert.Contains("<id value=", data);
            Assert.Contains("<birthDate", data);
            Assert.Contains("<photo", data);
        }
    }
}