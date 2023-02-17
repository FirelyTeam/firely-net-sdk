using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Hl7.Fhir.Support.Poco.Tests
{
    [TestClass]
    public class SummaryFilterIntegrationTests
    {
        [TestMethod]
        public void Basics()
        {
            // This bundle should get through unfiltered
            Bundle b = new()
            {
                Identifier = new Identifier("http://nu.nl", "abc"),
                Type = Bundle.BundleType.Batch,
                Total = 1000
            };

            // This organization will have only its "identifier" pass the filter
            TestPatient p = new()
            {
                Active = true,
                MaritalStatus = new CodeableConcept("http://nu.nl", "123"),
            };

            p.Identifier.Add(new Identifier("http://nu.nl", "abc"));
            p.Communication.Add(new TestPatient.CommunicationComponent { Language = new CodeableConcept("x", "nl-nl"), Preferred = true });

            // This nested bundle also will have only its "identifier" pass the filter
            Bundle nestedB = new()
            {
                Identifier = new Identifier("http://nu.nl", "abc"),
                Type = Bundle.BundleType.Collection
            };

            b.Entry.Add(new Bundle.EntryComponent { Resource = p });
            b.Entry.Add(new Bundle.EntryComponent { Resource = nestedB });

            var filter = new BundleFilter(new TopLevelFilter(
                new ElementMetadataFilter
                {
                    IncludeNames = new[] { "communication", "type" },
                },
                new ElementMetadataFilter
                {
                    IncludeMandatory = true,
                    IncludeInSummary = true,
                }
                ));

            var options = new JsonSerializerOptions()
                .ForFhir(typeof(TestPatient).Assembly, new FhirJsonPocoSerializerSettings() { SummaryFilter = filter })
                .Pretty();
            string actual = JsonSerializer.Serialize(b, options);

            // Root bundle should not have been filtered at all
            var bp = FhirJsonNode.Parse(actual).ToPoco<Bundle>(ModelInspector.ForType<TestPatient>());
            assertIdentifier(bp.Identifier);
            bp.Type.Value.Should().Be(Bundle.BundleType.Batch);
            bp.Count().Should().Be(4);

            // The nested Patient should only its "communication" element included
            var pat = bp.Entry[0].Resource as TestPatient;
            pat.Count().Should().Be(1);
            pat.Communication.Should().NotBeNull();
            var communication = pat.Communication.Single();

            // Communication should just have its mandatory "language" set.
            communication.Count().Should().Be(1);

            // Communication.language is a CodeableConcept, all of its field are in summary...
            communication.Language.Should().BeEquivalentTo(new CodeableConcept("x", "nl-nl"));

            // The nested Bundle should only its "type" present
            var nb = bp.Entry[1].Resource as Bundle;
            nb.Count().Should().Be(1);
            nb.Type.Should().NotBeNull();

            // Non-bundle root resources should be filtered normally too 
            actual = JsonSerializer.Serialize(p, options);
            pat = FhirJsonNode.Parse(actual).ToPoco<TestPatient>(ModelInspector.ForType<TestPatient>());
            pat.Count().Should().Be(1);
            pat.Communication.Should().NotBeNull();

            static void assertIdentifier(Identifier ide)
            {
                ide.Should().NotBeNull();
                ide.System.Should().Be("http://nu.nl");
                ide.Value.Should().Be("abc");
                ide.Count().Should().Be(2);
            }
        }

        [TestMethod]
        public void AllSummaryIndeed()
        {
            var (full, summarized) = runSummarize<TestCodeSystem>("mask-text.xml", SerializationFilter.ForSummary());
            var codeSystemCM = ModelInspector.ForAssembly(typeof(TestPatient).Assembly).FindClassMapping(typeof(TestCodeSystem));

            summarized.All(element => codeSystemCM.FindMappedElementByName(element.Key).InSummary).Should().BeTrue();
            summarized.Count().Should().BeLessThan(codeSystemCM.PropertyMappings.Where(pm => pm.InSummary).Count());
        }

        [TestMethod]
        public void SummaryText()
        {
            var (full, summarized) = runSummarize<TestCodeSystem>("mask-text.xml", SerializationFilter.ForText());

            traverse(summarized).Count().Should().Be(1 +
                traverse(full.IdElement).Count() +
                traverse(full.Text).Count() +
                traverse(full.Meta).Count() +
                traverse(full.StatusElement).Count());
        }

        [TestMethod]
        public void SummaryData()
        {
            var (full, summarized) = runSummarize<TestCodeSystem>("mask-text.xml", SerializationFilter.ForData());

            traverse(summarized).Count().Should().Be(traverse(full).Count() - traverse(full.Text).Count());
        }

        [TestMethod]
        public void SummaryElements()
        {
            // This is actually equivalent to "text" (if elements also includes mandatory)
            var (full, summarized) = runSummarize<TestCodeSystem>("mask-text.xml", SerializationFilter.ForElements(new[] { "id", "text", "meta" }));

            traverse(summarized).Count().Should().Be(1 +
                traverse(full.IdElement).Count() +
                traverse(full.Text).Count() +
                traverse(full.Meta).Count() +
                traverse(full.StatusElement).Count());
        }

        private (T full, T summarized) runSummarize<T>(string filename, SerializationFilter filter) where T : Resource
        {
            var fullXml = File.ReadAllText(Path.Combine("TestData", filename));
            var full = FhirXmlNode.Parse(fullXml).ToPoco<T>(ModelInspector.ForType<T>());

            var options = new JsonSerializerOptions()
                .ForFhir(typeof(TestPatient).Assembly, new FhirJsonPocoSerializerSettings { SummaryFilter = filter })
                .Pretty();
            string summarizedJson = JsonSerializer.Serialize(full, options);

            var summarized = FhirJsonNode.Parse(summarizedJson).ToPoco<T>(ModelInspector.ForType<T>());

            return (full, summarized);
        }

        private static IEnumerable<KeyValuePair<string, object>> traverse(Base x)
        {
            return childrenAndMe(KeyValuePair.Create("(root)", (object)x));

            static IEnumerable<KeyValuePair<string, object>> childrenAndMe(KeyValuePair<string, object> y) =>
                (y.Value switch
                {
                    ICollection array => array.Cast<object>().SelectMany(bsi => childrenAndMe(KeyValuePair.Create(y.Key, bsi))),
                    Base obj => obj.SelectMany(oc => childrenAndMe(oc)),
                    var z => Enumerable.Empty<KeyValuePair<string, object>>()
                }).Prepend(y).ToList();
        }
    }

}
