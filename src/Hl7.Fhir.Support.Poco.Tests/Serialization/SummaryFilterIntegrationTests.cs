using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        public void SummaryHasNoEmptyObject()
        {
            var patient = new Patient
            {
                BirthDateElement = new Date("1990")
                {
                    Extension = [new Extension("birthTime", new Instant(DateTimeOffset.Now))]
                }
            };
            var (_, summarized) = runSummarize<Patient>(patient, SerializationFilter.ForSummary);
            summarized.Extension.Should().BeEmpty();
        }
        
        [TestMethod]
        public void SerializationWontCrashWithFilterAndNullListElements()
        {
            var patient = new Patient
            {
                BirthDateElement = new Date("1990")
                {
                    Extension = [ null, new Extension("birthTime", new Instant(DateTimeOffset.Now)) ]
                }
            };
            var (_, summarized) = runSummarize<Patient>(patient, SerializationFilter.ForSummary);
            summarized.Extension.Should().BeEmpty();
        }
        
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
            Patient p = new()
            {
                Active = true,
                MaritalStatus = new CodeableConcept("http://nu.nl", "123"),
            };

            p.Identifier.Add(new Identifier("http://nu.nl", "abc"));
            p.Communication.Add(new Patient.CommunicationComponent { Language = new CodeableConcept("x", "nl-nl"), Preferred = true });

            // This nested bundle also will have only its "identifier" pass the filter
            Bundle nestedB = new()
            {
                Identifier = new Identifier("http://nu.nl", "abc"),
                Type = Bundle.BundleType.Collection
            };

            b.Entry.Add(new Bundle.EntryComponent { Resource = p });
            b.Entry.Add(new Bundle.EntryComponent { Resource = nestedB });

            static SerializationFilter filter() => new BundleFilter(new TopLevelFilter(
                new ElementMetadataFilter
                {
                    IncludeNames = ["communication", "type"],
                },
                new ElementMetadataFilter
                {
                    IncludeMandatory = true,
                    IncludeInSummary = true,
                }
                ));

            var options = new JsonSerializerOptions()
                .ForFhir(new FhirJsonConverterOptions { SummaryFilterFactory = filter })
                .Pretty();
            string actual = JsonSerializer.Serialize(b, options);

            // Root bundle should not have been filtered at all
            var bp = FhirJsonNode.Parse(actual).ToPoco<Bundle>();
            assertIdentifier(bp.Identifier);
            bp.Type.Value.Should().Be(Bundle.BundleType.Batch);
            bp.Count().Should().Be(4);

            // The nested Patient should only its "communication" element included
            var pat = bp.Entry[0].Resource as Patient;
            pat.Count().Should().Be(1);
            pat.Communication.Should().NotBeNull();
            var communication = pat.Communication.Single();

            // Communication should just have its mandatory "language" set.
            communication.Count().Should().Be(1);

            // Communication.language is a CodeableConcept, all of its field are in summary...
            communication.Language.IsExactly(new CodeableConcept("x", "nl-nl")).Should().BeTrue();

            // The nested Bundle should only its "type" present
            var nb = bp.Entry[1].Resource as Bundle;
            nb.Count().Should().Be(1);
            nb.Type.Should().NotBeNull();

            // Non-bundle root resources should be filtered normally too 
            actual = JsonSerializer.Serialize(p, options);
            pat = FhirJsonNode.Parse(actual).ToPoco<Patient>();
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
            var (_, summarized) = runSummarize<CodeSystem>("mask-text.xml", SerializationFilter.ForSummary);
            var codeSystemCm = ModelInfo.ModelInspector.FindClassMapping(typeof(CodeSystem))!;

            summarized.EnumerateElements().All(element => codeSystemCm.FindMappedElementByName(element.Key)!.InSummary).Should().BeTrue();
            summarized.Count().Should().BeLessThan(codeSystemCm.PropertyMappings.Count(pm => pm.InSummary));
        }

        [TestMethod]
        public void SummaryText()
        {
            var (full, summarized) = runSummarize<CodeSystem>("mask-text.xml", SerializationFilter.ForText);

            traverse(summarized).Count().Should().Be(1 +
                traverse(full.IdElement).Count() +
                traverse(full.Text).Count() +
                traverse(full.Meta).Count() +
                traverse(full.StatusElement).Count());
        }

        [TestMethod]
        public void SummaryData()
        {
            var (full, summarized) = runSummarize<CodeSystem>("mask-text.xml", SerializationFilter.ForData);

            traverse(summarized).Count().Should().Be(traverse(full).Count() - traverse(full.Text).Count());
        }

        [TestMethod]
        public void SummaryElements()
        {
            // This is actually equivalent to "text" (if elements also includes mandatory)
            var (full, summarized) = runSummarize<CodeSystem>("mask-text.xml",
                SerializationFilter.ForElementsFactory(["id", "text", "meta"]));

            traverse(summarized).Count().Should().Be(1 +
                traverse(full.IdElement).Count() +
                traverse(full.Text).Count() +
                traverse(full.Meta).Count() +
                traverse(full.StatusElement).Count());
        }

        [TestMethod]
        public void SummaryCount()
        {
            var (_, summarized) = runSummarize<Bundle>("simple-bundle.xml", SerializationFilter.ForCount);
            
            // check if result contains the link
            traverse(summarized).Should().ContainKey("link");
        }
        
        private (T full, T summarized) runSummarize<T>(T full, Func<SerializationFilter> filterFactory) where T : Resource
        {
            var options = new JsonSerializerOptions()
                .ForFhir(new FhirJsonConverterOptions { SummaryFilterFactory = filterFactory })
                .Pretty();
            string summarizedJson = JsonSerializer.Serialize(full, options);

            var summarized = FhirJsonNode.Parse(summarizedJson).ToPoco<T>();

            return (full, summarized);
        }

        private (T full, T summarized) runSummarize<T>(string filename, Func<SerializationFilter> filterFactory) where T : Resource
        {
            var fullXml = File.ReadAllText(Path.Combine("TestData", filename));
            var full = FhirXmlNode.Parse(fullXml).ToPoco<T>();

            return runSummarize(full, filterFactory);
        }

        private static IEnumerable<KeyValuePair<string, object>> traverse(Base x)
        {
            return childrenAndMe(KeyValuePair.Create("(root)", (object)x));

            static IEnumerable<KeyValuePair<string, object>> childrenAndMe(KeyValuePair<string, object> y) =>
                (y.Value switch
                {
                    Meta m => [], // skip Meta, since we've added SUBSETTED tags, so don't count those
                    ICollection array => array.Cast<object>().SelectMany(bsi => childrenAndMe(KeyValuePair.Create(y.Key, bsi))),
                    Base obj => obj.EnumerateElements().SelectMany(childrenAndMe),
                    _ => []
                }).Prepend(y).ToList();
        }
    }

    file static class EnumerableShim
    {
        public static int Count(this Base b) => b.EnumerateElements().Count();
    }
}