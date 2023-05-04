/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization.Tests
{

    [TestClass]
    public partial class RoundtripTest
    {
        private readonly string _attachmentJson = "{\"size\":\"12\"}";


        private static IEnumerable<object[]> getEngines()
        {
            yield return new[] { FhirSerializationEngineFactory.Strict(ModelInspector.ForType(typeof(Subscription))) };
            yield return new[] { FhirSerializationEngineFactory.Legacy.Strict(ModelInspector.ForType(typeof(Subscription))) };
        }

        [TestMethod]
        [DynamicData(nameof(getEngines), DynamicDataSourceType.Method)]
        public void TestAcceptsResourceTypeElementInDatatype(IFhirSerializationEngine engine)
        {
            var sub = new Subscription { ChannelType = new("http://nu.nl", "test'"), Status = SubscriptionStatusCodes.Active, Topic = "Test" };
            sub.FilterBy.Add(new() { ResourceType = "TestResource", FilterParameter = "test", Value = "test" });

            var json = engine.SerializeToJson(sub);
            json.Should().Contain("\"resourceType\":\"TestResource\"");

            var xml = engine.SerializeToXml(sub);
            xml.Should().Contain("resourceType");

            check(engine.DeserializeFromJson(json));
            check(engine.DeserializeFromXml(xml));

            static void check(Resource r)
            {
                r.Should().BeOfType<Subscription>().Which.FilterBy[0].ResourceType.Should().Be("TestResource");
            }
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesXmlPoco()
        {
            FullRoundtripOfAllExamples("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: true, provider: null);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public async Tasks.Task FullRoundtripOfAllExamplesXmlPocoAsync()
        {
            await FullRoundtripOfAllExamplesAsync("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: true, provider: null);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesJsonPoco()
        {
            FullRoundtripOfAllExamples("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: true, provider: null);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public async Tasks.Task FullRoundtripOfAllExamplesJsonPocoAsync()
        {
            await FullRoundtripOfAllExamplesAsync("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: true, provider: null);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesXmlNavPocoProvider()
        {
            FullRoundtripOfAllExamples("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: false, provider: new PocoStructureDefinitionSummaryProvider());
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public async Tasks.Task FullRoundtripOfAllExamplesXmlNavPocoProviderAsync()
        {
            await FullRoundtripOfAllExamplesAsync("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: false, provider: new PocoStructureDefinitionSummaryProvider());
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesJsonNavPocoProvider()
        {
            FullRoundtripOfAllExamples("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: false, provider: new PocoStructureDefinitionSummaryProvider());
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public async Tasks.Task FullRoundtripOfAllExamplesJsonNavPocoProviderAsync()
        {
            await FullRoundtripOfAllExamplesAsync("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: false, provider: new PocoStructureDefinitionSummaryProvider());
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesXmlNavSdProvider()
        {
            var source = new CachedResolver(ZipSource.CreateValidationSource());
            FullRoundtripOfAllExamples("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: false, provider: new StructureDefinitionSummaryProvider(source));
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public async Tasks.Task FullRoundtripOfAllExamplesXmlNavSdProviderAsync()
        {
            var source = new CachedResolver(ZipSource.CreateValidationSource());
            await FullRoundtripOfAllExamplesAsync("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: false, provider: new StructureDefinitionSummaryProvider(source));
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesJsonNavSdProvider()
        {
            var source = new CachedResolver(ZipSource.CreateValidationSource());
            FullRoundtripOfAllExamples("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: false, provider: new StructureDefinitionSummaryProvider(source));
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public async Tasks.Task FullRoundtripOfAllExamplesJsonNavSdProviderAsync()
        {
            var source = new CachedResolver(ZipSource.CreateValidationSource());
            await FullRoundtripOfAllExamplesAsync("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: false, provider: new StructureDefinitionSummaryProvider(source));
        }
    }
}
