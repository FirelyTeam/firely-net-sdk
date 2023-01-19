/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public partial class RoundtripTest
    {
        private readonly string _attachmentJson = "{\"size\":\"12\"}";

        [TestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public void FullRoundtripOfAllExamplesXmlPoco()
        {
            FullRoundtripOfAllExamples("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: true, provider: null);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public async Tasks.Task FullRoundtripOfAllExamplesXmlPocoAsync()
        {
            await FullRoundtripOfAllExamplesAsync("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: true, provider: null);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public void FullRoundtripOfAllExamplesJsonPoco()
        {
            FullRoundtripOfAllExamples("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: true, provider: null);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public async Tasks.Task FullRoundtripOfAllExamplesJsonPocoAsync()
        {
            await FullRoundtripOfAllExamplesAsync("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: true, provider: null);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public void FullRoundtripOfAllExamplesXmlNavPocoProvider()
        {
            FullRoundtripOfAllExamples("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: false, provider: new PocoStructureDefinitionSummaryProvider());
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public async Tasks.Task FullRoundtripOfAllExamplesXmlNavPocoProviderAsync()
        {
            await FullRoundtripOfAllExamplesAsync("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: false, provider: new PocoStructureDefinitionSummaryProvider());
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public void FullRoundtripOfAllExamplesJsonNavPocoProvider()
        {
            FullRoundtripOfAllExamples("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: false, provider: new PocoStructureDefinitionSummaryProvider());
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public async Tasks.Task FullRoundtripOfAllExamplesJsonNavPocoProviderAsync()
        {
            await FullRoundtripOfAllExamplesAsync("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: false, provider: new PocoStructureDefinitionSummaryProvider());
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public void FullRoundtripOfAllExamplesXmlNavSdProvider()
        {
            var source = new CachedResolver(ZipSource.CreateValidationSource());
            FullRoundtripOfAllExamples("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: false, provider: new StructureDefinitionSummaryProvider(source));
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public async Tasks.Task FullRoundtripOfAllExamplesXmlNavSdProviderAsync()
        {
            var source = new CachedResolver(ZipSource.CreateValidationSource());
            await FullRoundtripOfAllExamplesAsync("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: false, provider: new StructureDefinitionSummaryProvider(source));
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public void FullRoundtripOfAllExamplesJsonNavSdProvider()
        {
            var source = new CachedResolver(ZipSource.CreateValidationSource());
            FullRoundtripOfAllExamples("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: false, provider: new StructureDefinitionSummaryProvider(source));
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public async Tasks.Task FullRoundtripOfAllExamplesJsonNavSdProviderAsync()
        {
            var source = new CachedResolver(ZipSource.CreateValidationSource());
            await FullRoundtripOfAllExamplesAsync("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: false, provider: new StructureDefinitionSummaryProvider(source));
        }
    }
}
