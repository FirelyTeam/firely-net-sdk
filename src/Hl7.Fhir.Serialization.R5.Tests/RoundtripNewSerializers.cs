#nullable enable
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

        [DataTestMethod]
        [DataRow("{\"size\":\"12\", \"title\": \"Correct Attachment\"}", 12L, null)]
        [DataRow("{\"size\":12, \"title\": \"An incorrect Attachment\"}", null, "*Json number '12' cannot be parsed as a Integer64*")]
        [DataRow("{\"size\":12.345, \"title\": \"An incorrect Attachment\"}", null, "*Json number '12.345' cannot be parsed as a Int64*")]
        [DataRow("{\"size\":\"12.345\", \"title\": \"An incorrect Attachment\"}", null, "*Json number '12.345' cannot be parsed as a Integer64*")]

        public void ParseAttachment(string input, long? expectedAttachmentSize, string? errorMessage)
        {
            var options = new JsonSerializerOptions().ForFhir(ModelInfo.ModelInspector);
            if (errorMessage is not null)
            {
                Action action = () => JsonSerializer.Deserialize<Attachment>(input, options);

                action.Should().Throw<DeserializationFailedException>()
                    .WithMessage(errorMessage);
            }
            else
            {
                var attachment = JsonSerializer.Deserialize<Attachment>(input, options);
                attachment.Should().NotBeNull();
                attachment!.Size.Should().Be(expectedAttachmentSize!.Value);
            }
        }
    }
}
#nullable restore