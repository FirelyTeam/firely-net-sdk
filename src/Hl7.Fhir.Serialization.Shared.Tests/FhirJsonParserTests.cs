using FluentAssertions;
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
}