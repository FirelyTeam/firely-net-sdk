#nullable enable

using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.Json;
using ERR = Hl7.Fhir.Serialization.FhirJsonException;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Serialization.Tests;

[TestClass]
public class RoundTripAttachments
{
#if R5
        private readonly string _attachmentJson = "{\"size\":\"12\"}";

        private static IEnumerable<object[]> attachmentSource()
        {
            yield return ["{\"size\":\"12\", \"title\": \"Correct Attachment\"}", 12L, null!];
            yield return ["{\"size\":12, \"title\": \"An incorrect Attachment\"}", null!, COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE
            ];
            yield return ["{\"size\":25.345, \"title\": \"An incorrect Attachment\"}", null!, COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE
            ];
            yield return ["{\"size\":\"12.345\", \"title\": \"An incorrect Attachment\"}", null!, COVE.LITERAL_INVALID_CODE
            ];
        }
#else
    private readonly string _attachmentJson = "{\"size\":12}";

    private static IEnumerable<object[]> attachmentSource()
    {
        yield return ["{\"size\":12, \"title\": \"Correct Attachment\"}", 12, null!];
        yield return
        [
            "{\"size\":12.345, \"title\": \"An incorrect Attachment\"}", null!, COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE
        ];
        yield return
        [
            "{\"size\":\"12\", \"title\": \"An incorrect Attachment\"}", null!, COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE
        ];
    }
#endif

        
    [TestMethod]
    public void RoundTripAttachmentWithSize()
    {
        var options = new JsonSerializerOptions().ForFhir(ModelInfo.ModelInspector);
        var attachment = JsonSerializer.Deserialize<Attachment>(_attachmentJson, options)!;
#if R5
        attachment.Size.Should().Be(12L);
#else
        attachment.SizeUnsignedInt.Should().Be(12);
#endif
        var json = JsonSerializer.Serialize(attachment, options);
        json.Should().Be(_attachmentJson);
    }
    
    [TestMethod]
    public void RoundTripAttachmentWithSizeOldParser()
    {
        var parser = new FhirJsonDeserializer();
        var attachment = parser.Deserialize<Attachment>(_attachmentJson);
#if R5
        attachment.Size.Should().Be(12L);
#else
        attachment.SizeUnsignedInt.Should().Be(12);
#endif
        var serializer = new FhirJsonSerializer();
        var result = serializer.SerializeToString(attachment);
        result.Should().Be(_attachmentJson);
    }

    [TestMethod]
    [DynamicData(nameof(attachmentSource))]
    public void ParseAttachment(string input, object? expectedAttachmentSize, string? errorCode)
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
            var attachment = JsonSerializer.Deserialize<Attachment>(input, options)!;
            attachment.Should().NotBeNull();
#if R5
            attachment.Size.Should().Be((long)expectedAttachmentSize!);
#else
            attachment.SizeUnsignedInt.Should().Be((int)expectedAttachmentSize!);
#endif

        }
    }
}