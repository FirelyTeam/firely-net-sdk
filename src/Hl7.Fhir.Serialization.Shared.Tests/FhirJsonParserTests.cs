using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hl7.Fhir.Support.Tests.Serialization;

[TestClass]
public class FhirJsonParserTests
{
    [TestMethod]
    public void FhirJsonParser_WillKeepWhitespace()
    {
        string json = """
                      {
                        "resourceType": "Patient",
                        "id": " whitespace "
                      }
                      """;
        var res = new FhirJsonDeserializer(new DeserializerSettings().Ignoring([CodedValidationException.LITERAL_INVALID_CODE])).Deserialize<Patient>(json);

        res.Id.Should().Be(" whitespace ");
    }

    [TestMethod]
    public void FhirJsonParser_LargeIntegerValueQuantity_DoesNotThrow()
    {
        // Regression test for https://github.com/FirelyTeam/firely-net-sdk/issues/3457
        // Large long values (e.g. timestamps used as quantities) previously caused an
        // OverflowException when NormalizeValue tried Convert.ToInt32() unconditionally.
        string json = """
                      {
                        "resourceType": "Observation",
                        "status": "final",
                        "code": { "text": "test" },
                        "valueQuantity": {
                          "value": 20231128235900
                        }
                      }
                      """;

        var node = FhirJsonNode.Parse(json);
        var valueQuantity = node.Children("valueQuantity").FirstOrDefault();
        valueQuantity.Should().NotBeNull();

        var value = valueQuantity.Children("value").FirstOrDefault();
        value.Should().NotBeNull();

        var details = value.Annotation<JsonSerializationDetails>();
        details.Should().NotBeNull();
        details.OriginalValue.Should().Be(20231128235900L);
    }

    [TestMethod]
    public void FhirJsonParser_SmallIntegerValue_NormalizesToInt32()
    {
        string json = """
                      {
                        "resourceType": "Observation",
                        "status": "final",
                        "code": { "text": "test" },
                        "valueQuantity": {
                          "value": 42
                        }
                      }
                      """;

        var node = FhirJsonNode.Parse(json);
        var value = node.Children("valueQuantity").First().Children("value").First();

        var details = value.Annotation<JsonSerializationDetails>();
        details.Should().NotBeNull();
        details.OriginalValue.Should().Be(42);
        details.OriginalValue.Should().BeOfType<int>();
    }

    [TestMethod]
    public void FhirJsonParser_LargeIntegerValue_RoundtripsAsNumber()
    {
        // Regression test: large longs must roundtrip as JSON numbers, not strings.
        string json = """
                      {
                        "resourceType": "Observation",
                        "status": "final",
                        "code": { "text": "test" },
                        "valueQuantity": {
                          "value": 20231128235900
                        }
                      }
                      """;

        var node = FhirJsonNode.Parse(json);
        var roundtripped = node.ToJson();

        roundtripped.Should().Contain("20231128235900");
        roundtripped.Should().NotContain("\"20231128235900\"");
    }
}
