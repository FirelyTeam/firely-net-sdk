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
using Hl7.Fhir.Rest;
using System.IO;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
    public class SummarySerializationTests
    {
        private readonly FhirXmlSerializer FhirXmlSerializer = new FhirXmlSerializer();
        private readonly FhirJsonSerializer FhirJsonSerializer = new FhirJsonSerializer();
        private readonly FhirXmlParser FhirXmlParser = new FhirXmlParser();

        [TestMethod] // Old tests, I'm note sure we need them anymore
        public async T.Task TestSummary()
        {
            var p = new Patient
            {
                BirthDate = "1972-11-30",     // present in both summary and full
                Photo = new List<Attachment>() { new Attachment() { ContentType = "text/plain" } }
            };

            var full = await FhirXmlSerializer.SerializeToStringAsync(p);
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));
            Assert.IsNull(p.Meta, "Meta element should not be introduced here.");

            var summ = await FhirXmlSerializer.SerializeToStringAsync(p, summary: Fhir.Rest.SummaryType.True);
            Assert.IsTrue(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));
            Assert.IsNull(p.Meta, "Meta element should not be introduced here.");

            var q = new Questionnaire();
            q.Text = new Narrative() { Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">Test Questionnaire</div>" };
            q.Status = PublicationStatus.Active;
            q.Date = "2015-09-27";
            q.Title = "TITLE";
            q.Item = new List<Questionnaire.ItemComponent>();
            q.Item.Add(new Questionnaire.ItemComponent()
            {
                LinkId = "linkid",
                Text = "TEXT"
            });
            
            Assert.IsNull(q.Meta, "Meta element has not been created.");
            var qfull = await FhirXmlSerializer.SerializeToStringAsync(q);
            Assert.IsNull(q.Meta, "Meta element should not be introduced here.");
            Console.WriteLine("summary: Fhir.Rest.SummaryType.False");
            Console.WriteLine(qfull);
            Assert.IsTrue(qfull.Contains("Test Questionnaire"));
            Assert.IsTrue(qfull.Contains("<status value=\"active\""));
            Assert.IsTrue(qfull.Contains("<date value=\"2015-09-27\""));
            Assert.IsTrue(qfull.Contains("<title value=\"TITLE\""));
            Assert.IsTrue(qfull.Contains("<text value=\"TEXT\""));
            Assert.IsTrue(qfull.Contains("<linkId value=\"linkid\""));

            var qSum = await FhirXmlSerializer.SerializeToStringAsync(q, summary: Fhir.Rest.SummaryType.True);
            Console.WriteLine("summary: Fhir.Rest.SummaryType.True");
            Console.WriteLine(qSum);
            Assert.IsFalse(qSum.Contains("Test Questionnaire"));
            Assert.IsTrue(qSum.Contains("<status value=\"active\""));
            Assert.IsTrue(qSum.Contains("<date value=\"2015-09-27\""));
            Assert.IsTrue(qSum.Contains("<title value=\"TITLE\""));
            Assert.IsFalse(qSum.Contains("<text value=\"TEXT\""));
            Assert.IsFalse(qSum.Contains("<linkId value=\"linkid\""));

            var qData = await FhirXmlSerializer.SerializeToStringAsync(q, summary: Fhir.Rest.SummaryType.Data);
            Console.WriteLine("summary: Fhir.Rest.SummaryType.Data");
            Console.WriteLine(qData);
            Assert.IsFalse(qData.Contains("Test Questionnaire"));
            Assert.IsTrue(qData.Contains("<meta"));
            Assert.IsTrue(qData.Contains("<text value=\"TEXT\""));
            Assert.IsTrue(qData.Contains("<status value=\"active\""));
            Assert.IsTrue(qData.Contains("<date value=\"2015-09-27\""));
            Assert.IsTrue(qData.Contains("<title value=\"TITLE\""));
            Assert.IsTrue(qData.Contains("<linkId value=\"linkid\""));

            q.Meta = new Meta { VersionId = "v2" };
            var qText = await FhirXmlSerializer.SerializeToStringAsync(q, summary: Fhir.Rest.SummaryType.Text);
            Console.WriteLine("summary: Fhir.Rest.SummaryType.Text");
            Console.WriteLine(qText);
            Assert.IsTrue(qText.Contains("Test Questionnaire"));
            Assert.IsTrue(qText.Contains("<meta"));
            Assert.IsTrue(qText.Contains("<status value=\"active\""));
            Assert.IsFalse(qText.Contains("<text value=\"TEXT\""));
            Assert.IsFalse(qText.Contains("<date value=\"2015-09-27\""));
            Assert.IsFalse(qText.Contains("<title value=\"TITLE\""));
            Assert.IsFalse(qText.Contains("<linkId value=\"linkid\""));
            Assert.AreEqual(0, q.Meta.Tag.Where(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED").Count(), "Subsetted Tag should not still be there.");

            // Verify that reloading the content into an object...
            // make sure we accept the crappy output with empty groups
            var nav = await FhirXmlNode.ParseAsync(qText, new FhirXmlParsingSettings { PermissiveParsing = true });

            var qInflate = FhirXmlParser.Parse<Questionnaire>(nav);
            Assert.AreEqual(1, qInflate.Meta.Tag.Where(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED").Count(), "Subsetted Tag should not still be there.");
        }

        [TestMethod]
        public async T.Task TestIncludeMandatory()
        {
            var l = new Library();
            l.Type = new CodeableConcept { TextElement = new FhirString("testMandatoryComplexType") };
            l.Id = "testId";
            l.Language = "testLang";
            var summaryElements = await FhirXmlSerializer.SerializeToStringAsync(l, Fhir.Rest.SummaryType.Count);
            
            Assert.IsFalse(summaryElements.Contains("<language"));
            Assert.IsTrue(summaryElements.Contains("<type>"));
            Assert.IsTrue(summaryElements.Contains("<id value=\"testId\""));
            
            var customMaskingNode = new MaskingNode(new ScopedNode(l.ToTypedElement()), new MaskingNodeSettings
            {
                IncludeMandatory = true,
                PreserveBundle = MaskingNodeSettings.PreserveBundleMode.All
            });

            var result = await customMaskingNode.ToXmlAsync(settings: new FhirXmlSerializationSettings());

            Assert.IsFalse(result.Contains("<language>"));
            Assert.IsTrue(result.Contains("<type>"));
            Assert.IsFalse(result.Contains("<id value=\"testId\""));

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

            result = await customMaskingNodeForBundle.ToXmlAsync(settings: new FhirXmlSerializationSettings());
            
            Assert.IsTrue(result.Contains("<type value=\"collection\""));
            Assert.IsFalse(result.Contains("<id value=\"bundle-id\""));
        }

        [TestMethod]
        public async T.Task TestElements()
        {
            var p = new Patient
            {
                BirthDate = "1972-11-30",
                Photo = new List<Attachment>() { new Attachment() { ContentType = "text/plain" } }
            };
            var elements = new[] { "photo" };
            
            var summaryElements = await FhirXmlSerializer.SerializeToStringAsync(p, Fhir.Rest.SummaryType.False, elements: elements);
            Assert.IsFalse(summaryElements.Contains("<birthDate"));
            Assert.IsTrue(summaryElements.Contains("<photo"));

            var noSummarySpecified = await FhirXmlSerializer.SerializeToStringAsync(p, elements: elements);
            Assert.IsFalse(noSummarySpecified.Contains("<birthDate"));
            Assert.IsTrue(noSummarySpecified.Contains("<photo"));

            await ExceptionAssert.Throws<ArgumentException>(async () => await FhirXmlSerializer.SerializeToStringAsync(p, Fhir.Rest.SummaryType.True, elements: elements));
            await ExceptionAssert.Throws<ArgumentException>(async () => await FhirXmlSerializer.SerializeToStringAsync(p, Fhir.Rest.SummaryType.Count, elements: elements));
            await ExceptionAssert.Throws<ArgumentException>(async () => await FhirXmlSerializer.SerializeToStringAsync(p, Fhir.Rest.SummaryType.Data, elements: elements));
            await ExceptionAssert.Throws<ArgumentException>(async () => await FhirXmlSerializer.SerializeToStringAsync(p, Fhir.Rest.SummaryType.Text, elements: elements));
        }

        [TestMethod]
        public async T.Task TestWithMetadata()
        {
            var p = new Patient
            {
                BirthDate = "1972-11-30"
            };

            var pSum = await FhirXmlSerializer.SerializeToStringAsync(p, summary: Fhir.Rest.SummaryType.True);
            Assert.IsNull(p.Meta, "Meta should not be there");

            p.Meta = new Meta { VersionId = "v2" }; // introducing meta data ourselves. 

            pSum = await FhirXmlSerializer.SerializeToStringAsync(p, summary: Fhir.Rest.SummaryType.True);
            Assert.IsNotNull(p.Meta, "Meta should still be there");
            Assert.AreEqual(0, p.Meta.Tag.Where(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED").Count(), "Subsetted Tag should not still be there.");
        }


        [TestMethod]
        public async T.Task TestBundleSummary()
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

            var full = await FhirXmlSerializer.SerializeToStringAsync(b);
            Assert.IsTrue(full.Contains("<entry"));
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));
            Assert.IsTrue(full.Contains("<total"));

            var summ = await FhirXmlSerializer.SerializeToStringAsync(b, summary: Fhir.Rest.SummaryType.True);
            Assert.IsTrue(summ.Contains("<entry"));
            Assert.IsTrue(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));
            Assert.IsTrue(summ.Contains("<total"));

            summ = await FhirXmlSerializer.SerializeToStringAsync(b, summary: Fhir.Rest.SummaryType.Count);
            Assert.IsFalse(summ.Contains("<entry"));
            Assert.IsFalse(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));
            Assert.IsTrue(summ.Contains("<total"));
            Assert.IsTrue(summ.Contains("<type"));
        }

        [TestMethod]
        public async T.Task TestBundleWithSummaryJson()
        {
            Dictionary<string, SummaryType> data = new Dictionary<string, SummaryType>
            {
                { "summary\\bundle-summary-true.json", SummaryType.True },
                { "summary\\bundle-summary-false.json", SummaryType.False },
                { "summary\\bundle-summary-data.json", SummaryType.Data },
                { "summary\\bundle-summary-text.json", SummaryType.Text },
                { "summary\\bundle-summary-count.json", SummaryType.Count },
                { "summary\\bundle-summary-true.xml", SummaryType.True },
                { "summary\\bundle-summary-false.xml", SummaryType.False },
                { "summary\\bundle-summary-data.xml", SummaryType.Data },
                { "summary\\bundle-summary-text.xml", SummaryType.Text },
                { "summary\\bundle-summary-count.xml", SummaryType.Count }
            };

            foreach (var pair in data)
            {
                var expectedFile = pair.Key;
                var mode = pair.Value;

                var patientOne = new Patient
                {

                    Id = "patient-one",
                    Text = new Narrative { Div = "<div xmlns='http://www.w3.org/1999/xhtml'>A great blues player</div>" },
                    Meta = new Meta { VersionId = "eric-clapton" },

                    Name = new List<HumanName> { new HumanName { Family = "Clapton", Use = HumanName.NameUse.Official } },

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
                    Name = new List<HumanName> { new HumanName { Family = "King", Use = HumanName.NameUse.Nickname } }
                };

                var bundle = new Bundle()
                {
                    Id = "my-bundle",
                    Total = 1803,
                    Type = Bundle.BundleType.Searchset,
                    Entry = new List<Bundle.EntryComponent> {
                        new Bundle.EntryComponent { Resource = patientOne, FullUrl = "http://base/Patient/patient-one", Search = new Bundle.SearchComponent() { Mode = Bundle.SearchEntryMode.Match } },
                        new Bundle.EntryComponent { Resource = patientTwo, FullUrl = "http://base/Patient/patient-two", Search = new Bundle.SearchComponent() { Mode = Bundle.SearchEntryMode.Match } }
                    }
                };

                bool inJson = Path.GetExtension(expectedFile) == ".json";
                var actualData = inJson ? await FhirJsonSerializer.SerializeToStringAsync(bundle, mode) :
                                    await FhirXmlSerializer.SerializeToStringAsync(bundle, mode);
                var expectedData = TestDataHelper.ReadTestData(expectedFile);
                Assert.AreEqual(actualData, expectedData);
            }
        }

        [TestMethod]
        public async T.Task TestResourceWithSummary()
        {
            Dictionary<string, SummaryType> data = new Dictionary<string, SummaryType>
            {
                { "summary\\summary-true.json", SummaryType.True },
                { "summary\\summary-false.json", SummaryType.False },
                { "summary\\summary-data.json", SummaryType.Data },
                { "summary\\summary-text.json", SummaryType.Text },
                { "summary\\summary-true.xml", SummaryType.True },
                { "summary\\summary-false.xml", SummaryType.False },
                { "summary\\summary-data.xml", SummaryType.Data },
                { "summary\\summary-text.xml", SummaryType.Text }
            };

            foreach (var pair in data)
            {
                var expectedFile = pair.Key;
                var mode = pair.Value;

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

                // Properties with IsSummary == true -> Id, Meta, Active, BirthDate, Gender, Name
                bool inJson = Path.GetExtension(expectedFile) == ".json";
                var actualData = inJson ? await FhirJsonSerializer.SerializeToStringAsync(patientOne, mode) :
                                    await FhirXmlSerializer.SerializeToStringAsync(patientOne, mode);
                var expectedData = TestDataHelper.ReadTestData(expectedFile);
                Assert.AreEqual(expectedData, actualData);
            }
        }

        [TestMethod]
        public async T.Task TestIdInSummary()
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

            var full = await FhirXmlSerializer.SerializeToStringAsync(p);
            Assert.IsTrue(full.Contains("narrative"));
            Assert.IsTrue(full.Contains("dud"));
            Assert.IsTrue(full.Contains("temp org"));
            Assert.IsTrue(full.Contains("<id value="));
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));
            Assert.IsTrue(full.Contains("text/plain"));

            full = await FhirXmlSerializer.SerializeToStringAsync(p, summary: Hl7.Fhir.Rest.SummaryType.False);
            Assert.IsTrue(full.Contains("narrative"));
            Assert.IsTrue(full.Contains("dud"));
            Assert.IsTrue(full.Contains("temp org"));
            Assert.IsTrue(full.Contains("contain"));
            Assert.IsTrue(full.Contains("<id value="));
            Assert.IsTrue(full.Contains("<birthDate"));
            Assert.IsTrue(full.Contains("<photo"));
            Assert.IsTrue(full.Contains("text/plain"));

            var summ = await FhirXmlSerializer.SerializeToStringAsync(p, summary: Fhir.Rest.SummaryType.True);
            Assert.IsFalse(summ.Contains("narrative"));
            Assert.IsFalse(summ.Contains("dud"));
            Assert.IsFalse(summ.Contains("contain"));
            Assert.IsTrue(summ.Contains("temp org"));
            Assert.IsTrue(summ.Contains("<id value="));
            Assert.IsTrue(summ.Contains("<birthDate"));
            Assert.IsFalse(summ.Contains("<photo"));

            var data = await FhirXmlSerializer.SerializeToStringAsync(p, summary: Hl7.Fhir.Rest.SummaryType.Data);
            Assert.IsFalse(data.Contains("narrative"));
            Assert.IsTrue(data.Contains("contain"));
            Assert.IsTrue(data.Contains("dud"));
            Assert.IsTrue(data.Contains("temp org"));
            Assert.IsTrue(data.Contains("<id value="));
            Assert.IsTrue(data.Contains("<birthDate"));
            Assert.IsTrue(data.Contains("<photo"));
        }
    }
}
