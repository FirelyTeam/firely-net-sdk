#nullable enable

using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.Json;
using ERR = Hl7.Fhir.Serialization.FhirJsonException;

namespace Hl7.Fhir.Serialization.Tests;

[TestClass]
public class RoundTripAttachments
{
#if R5 || R6
        private readonly string _attachmentJson = "{\"size\":\"12\"}";

        private static IEnumerable<object[]> attachmentSource()
        {
            yield return new object[] { "{\"size\":\"12\", \"title\": \"Correct Attachment\"}", 12L, null! };
            yield return new object[] { "{\"size\":12, \"title\": \"An incorrect Attachment\"}", null!, ERR.LONG_INCORRECT_FORMAT_CODE };
            yield return new object[] { "{\"size\":25.345, \"title\": \"An incorrect Attachment\"}", null!, ERR.NUMBER_CANNOT_BE_PARSED_CODE };
            yield return new object[] { "{\"size\":\"12.345\", \"title\": \"An incorrect Attachment\"}", null!, ERR.LONG_CANNOT_BE_PARSED_CODE };
        }
#else
    private readonly string _attachmentJson = "{\"size\":12}";

    private static IEnumerable<object[]> attachmentSource()
    {
        yield return new object[] { "{\"size\":12, \"title\": \"Correct Attachment\"}", 12L, null! };
        yield return new object[]
        {
            "{\"size\":12.345, \"title\": \"An incorrect Attachment\"}", null!, ERR.NUMBER_CANNOT_BE_PARSED_CODE
        };
        yield return new object[]
        {
            "{\"size\":\"12\", \"title\": \"An incorrect Attachment\"}", null!, ERR.LONG_INCORRECT_FORMAT_CODE
        };
        yield return new object[]
        {
            "{\"size\":\"12.345\", \"title\": \"An incorrect Attachment\"}", null!,
            ERR.LONG_INCORRECT_FORMAT_CODE
        };
    }
#endif

        
    [TestMethod]
    public void RoundTripAttachmentWithSize()
    {
        var options = new JsonSerializerOptions().ForFhir(ModelInfo.ModelInspector);
        var attachment = JsonSerializer.Deserialize<Attachment>(_attachmentJson, options);
        attachment.Should().BeOfType<Attachment>().Subject.Size.Should().Be(12L);
        var json = JsonSerializer.Serialize(attachment, options);
        json.Should().Be(_attachmentJson);
    }
    
    [TestMethod]
    public void RoundTripAttachmentWithSizeOldParser()
    {
        var parser = new FhirJsonParser(new ParserSettings() { PermissiveParsing = false });
        var attachment = parser.Parse<Attachment>(_attachmentJson);
        attachment.Size.Should().Be(12L);
        var serializer = new FhirJsonSerializer();
        var result = serializer.SerializeToString(attachment);
        result.Should().Be(_attachmentJson);
    }

    [DataTestMethod]
    [DynamicData(nameof(attachmentSource), DynamicDataSourceType.Method)]
    public void ParseAttachment(string input, long? expectedAttachmentSize, string? errorCode)
    {
        var options = new JsonSerializerOptions().ForFhir(ModelInfo.ModelInspector);
        if (errorCode is not null)
        {
            Action action = () => JsonSerializer.Deserialize<Attachment>(input, options);

            action.Should().Throw<DeserializationFailedException>().Which.Exceptions.Should()
                .OnlyContain(e => e.ErrorCode == errorCode);
        }
        else
        {
            var attachment = JsonSerializer.Deserialize<Attachment>(input, options);
            attachment.Should().NotBeNull();
            attachment!.Size.Should().Be(expectedAttachmentSize!.Value);
        }
    }
}