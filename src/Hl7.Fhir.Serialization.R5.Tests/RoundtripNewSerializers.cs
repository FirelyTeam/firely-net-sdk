using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public partial class RoundTripNewSerializers
    {
        private readonly string _attachmentJson = "{\"size\":\"12\"}";

        [DynamicData(nameof(prepareExampleZipFilesXml), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayNames))]
        [DataTestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public void FullRoundtripOfAllExamplesXmlNewSerializer(string file, string baseTestPath, FhirXmlPocoSerializer xmlSerializer, FhirXmlPocoDeserializer xmlDeserializer, JsonSerializerOptions jsonOptions)
        {
            doRoundTrip(baseTestPath, file, xmlSerializer, xmlDeserializer, jsonOptions);
        }

        [DynamicData(nameof(prepareExampleZipFilesJson), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayNames))]
        [DataTestMethod]
        [TestCategory("LongRunner")]
        [Ignore("Because of incorrect example files in R5 (5.0.0-snapshot3).")]
        public void FullRoundtripOfAllExamplesJsonNewSerializer(string file, string baseTestPath, FhirXmlPocoSerializer xmlSerializer, FhirXmlPocoDeserializer xmlDeserializer, JsonSerializerOptions jsonOptions)
        {
            doRoundTrip(baseTestPath, file, xmlSerializer, xmlDeserializer, jsonOptions);
        }

        [TestMethod]
        public void ParseIncorrectAttachment()
        {
            var attachmentWithIncorrectSizeFormat = "{\"size\":12, \"title\": \"An incorrect Attachment\"}";
            var options = new JsonSerializerOptions().ForFhir(ModelInfo.ModelInspector);

            Action action = () => JsonSerializer.Deserialize<Attachment>(attachmentWithIncorrectSizeFormat, options);

            action.Should().Throw<DeserializationFailedException>()
                .WithMessage("*Json number '12' cannot be parsed as a Int64. Json token should be string*");
        }
    }
}