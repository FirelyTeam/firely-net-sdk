#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;


namespace Hl7.Fhir.Serialization.Tests
{
    internal interface IRoundTripper
    {
        string RoundTripXml(string original);
        string RoundTripJson(string original);
    }

    internal class FhirSerializationEngineRoundtripper(IFhirSerializationEngine engine) : IRoundTripper
    {
        public string RoundTripXml(string original) => 
            engine.SerializeToXml(
                engine.DeserializeFromJson(
                    engine.SerializeToJson(
                        (engine.DeserializeFromXml(original))!))!);
        public string RoundTripJson(string original) => 
            engine.SerializeToJson(
                (engine.DeserializeFromXml(
                    engine.SerializeToXml(
                        engine.DeserializeFromJson(original)!)))!);
    }
    
    internal class TypedElementBasedRoundtripper(IStructureDefinitionSummaryProvider provider) : IRoundTripper
    {
        public string RoundTripXml(string original)
        {
            var nav = XmlParsingHelpers.ParseToTypedElement(original, provider,
                new FhirXmlParsingSettings { PermissiveParsing = true });
            var json = nav.ToJson();
            var nav2 = JsonParsingHelpers.ParseToTypedElement(json, provider,
                settings: new FhirJsonParsingSettings { AllowJsonComments = true, PermissiveParsing = true });
            return nav2.ToXml();
        }

        public string RoundTripJson(string original)
        {
            var nav = JsonParsingHelpers.ParseToTypedElement(original, provider,
                settings: new FhirJsonParsingSettings { AllowJsonComments = true, PermissiveParsing = true });
            var xml = nav.ToXml();
            var nav2 = XmlParsingHelpers.ParseToTypedElement(xml, provider,
                new FhirXmlParsingSettings { PermissiveParsing = true });
            return nav2.ToJson();
        }
    }
    
    [TestClass]
    public class RoundtripAllSerializers
    {
        private static readonly IFhirSerializationEngine NEW_POCO_ENGINE =
                FhirSerializationEngineFactory.Ostrich(ModelInfo.ModelInspector);

        private static readonly IRoundTripper NEW_POCO_ROUNDTRIPPER =
            new FhirSerializationEngineRoundtripper(NEW_POCO_ENGINE);

        private static readonly IRoundTripper OLD_POCO_ROUNDTRIPPER =
            new FhirSerializationEngineRoundtripper(
                FhirSerializationEngineFactory.Legacy.Ostrich(ModelInfo.ModelInspector));

        private static readonly IRoundTripper TYPEDELEM_POCOSDPROV =
            new TypedElementBasedRoundtripper(new PocoStructureDefinitionSummaryProvider());

        private static readonly IResourceResolver SOURCE = new CachedResolver(ZipSource.CreateValidationSource());
        
        private static readonly IRoundTripper TYPEDELEM_SDPROV =
            new TypedElementBasedRoundtripper(new StructureDefinitionSummaryProvider(SOURCE));
        
        private const string XML_EXAMPLE_ZIP_NAME = "examples.zip";
        private const string JSON_EXAMPLE_ZIP_NAME = "examples-json.zip";
        
        [DynamicData(nameof(prepareExampleZipFiles), DynamicDataSourceType.Method,
            DynamicDataDisplayName = nameof(GetTestDisplayNames))]
        [DataTestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesNewPocoSerializer(ZipArchiveEntry entry)
        {
            doRoundTrip(entry, NEW_POCO_ROUNDTRIPPER);
        }

        [DynamicData(nameof(prepareExampleZipFiles), DynamicDataSourceType.Method,
            DynamicDataDisplayName = nameof(GetTestDisplayNames))]
        [DataTestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesOldPocoSerializer(ZipArchiveEntry entry)
        {
            doRoundTrip(entry, OLD_POCO_ROUNDTRIPPER);
        }
        
        [DynamicData(nameof(prepareExampleZipFiles), DynamicDataSourceType.Method,
            DynamicDataDisplayName = nameof(GetTestDisplayNames))]
        [DataTestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesPocoSdProvTypedElementSerializer(ZipArchiveEntry entry)
        {
            doRoundTrip(entry, TYPEDELEM_POCOSDPROV);
        }

        [DynamicData(nameof(prepareExampleZipFiles), DynamicDataSourceType.Method,
            DynamicDataDisplayName = nameof(GetTestDisplayNames))]
        [DataTestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesSdProvTypedElementSerializer(ZipArchiveEntry entry)
        {
            doRoundTrip(entry, TYPEDELEM_SDPROV);
        }
        
        private static IEnumerable<object[]> prepareExampleZipFiles()
        {
            var xmlExampleArchive = openTestZip(XML_EXAMPLE_ZIP_NAME);
            var jsonExampleArchive = openTestZip(JSON_EXAMPLE_ZIP_NAME);

            var relevantEntries = xmlExampleArchive.Entries.Concat(jsonExampleArchive.Entries)
                .Where(e => e.Name.EndsWith(".xml") || e.Name.EndsWith(".json"))
                .Where(f => !skipFile(f.Name))
                .ToList();

            return relevantEntries.Select(z => (object[]) [z])
                .ToList();
        }

        private static bool skipFile(string file)
        {
            if (file.Contains("notification-") || file.Contains("subscriptionstatus-"))
                return true; // These are Subscription resources that have invalid data in R5.
            if (file.Contains("examplescenario-example"))
                return
                    true; // this resource has a property name resourceType (which is reserved in the .net json serializer)
            if (file.Contains("json-edge-cases"))
                return true; // known issues with binary contained resource having content, not data
            if (file.Contains("observation-decimal"))
                return true; // exponential number example is tooo big (and too small)
            if (file.Contains("package-min-ver"))
                return true; // not a resource
            if (file.Contains("profiles-other"))
                return true;
            if (file.Contains("profiles-resources"))
                return true;
            if (file.Contains("profiles-types"))
                return true;
            if (file.Contains("dataelements"))
                return true;
            if (file.Contains("valuesets"))
                return true;
            // https://chat.fhir.org/#narrow/stream/48-terminology/subject/v2.20Table.20
            if (file.Contains("xver-paths-4.6") || file.Contains("hl7.fhir.r5.corexml.manifest") ||
                file.Contains("hl7.fhir.r5.expansions.manifest") || file.Contains("hl7.fhir.r5.core.manifest") ||
                file.Contains("uml"))
                return true; // non-fhir-files in the R5 examples.zip
#if R4
            if (file.Contains("v2-tables"))
                return true; // this file is known to have a single dud valueset - have reported on Zulip
#endif
#if R5
            // These examples contain resourceType which cannot be handled by our serializers
            if (file.Contains("subscription-example")) return true;
            if (file.Contains("consent-example-smartonfhir")) return true;
#endif
            return false;
        }

        public static string GetTestDisplayNames(MethodInfo _, object[] values) =>
            ((ZipArchiveEntry)values[0]).Name;

        private static void doRoundTrip(ZipArchiveEntry entry, IRoundTripper roundtripper)
        {
            var errors = new List<string>();

            using var dataStream = entry.Open();
            var streamReader = new StreamReader(dataStream);
            var input = streamReader.ReadToEnd();
            var name = entry.Name;

            var result = roundtripFile(input, name, roundtripper, errors);
            if(result is not null)
                compare(input, result, name, errors);

            Assert.AreEqual(0, errors.Count, "Errors were encountered comparing converted content");
        }

        private static ZipArchive openTestZip(string filename)
        {
            var fullPath = Path.Combine("TestData", filename);
            return ZipFile.OpenRead(fullPath);
        }

        private static string? roundtripFile(string input, string name, IRoundTripper roundtripper, ICollection<string> errors)
        {
            var isXml = name.EndsWith(".xml");
            
            try
            {
                return isXml ?
                    roundtripper.RoundTripXml(input) :
                    roundtripper.RoundTripJson(input);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return null;
            }
        }
        
        private static void compare(string expectedData, string actualData, string name, List<string> errors)
        {
            if (name.EndsWith(".xml"))
            {
                try
                {
                    XmlAssert.AreSame(name, expectedData, actualData);
                }
                catch (Exception e)
                {
                    errors.Add(e.Message);
                }
            }
            else
            {
                JsonAssert.AreSame(name, expectedData, actualData, errors);
            }
        }
        
        [DynamicData(nameof(prepareExampleZipFiles), DynamicDataSourceType.Method,
            DynamicDataDisplayName = nameof(GetTestDisplayNames))]
        [DataTestMethod]
        [TestCategory("LongRunner")]
        public void TestMatchAndExactly(ZipArchiveEntry entry)
        {
            using var dataStream = entry.Open();
            var streamReader = new StreamReader(dataStream);
            var input = streamReader.ReadToEnd();
            var name = entry.Name;

            var resource = name.EndsWith(".xml")
                ? NEW_POCO_ENGINE.DeserializeFromXml(input)
                : NEW_POCO_ENGINE.DeserializeFromJson(input);

            var r2 = (Resource)resource!.DeepCopy();
            Assert.IsTrue(resource.Matches(r2),
                "Serialization of " + name + " did not match output - Matches test");
            Assert.IsTrue(resource.IsExactly(r2),
                "Serialization of " + name + " did not match output - IsExactly test");
            Assert.IsFalse(resource.Matches(null), "Serialization of " + name + " matched null - Matches test");
            Assert.IsFalse(resource.IsExactly(null),
                "Serialization of " + name + " matched null - IsExactly test");
        }
    }
}