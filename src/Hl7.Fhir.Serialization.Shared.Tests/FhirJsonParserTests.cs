using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
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
        var res = new FhirJsonParser(new()
        {
            PreserveWhitespaceInValues = true
        }).Parse<Patient>(json);
        
        res.Id.Should().Be(" whitespace ");
    }
    
    [TestMethod]
    public void FhirJsonParser_TrimsWhitespaceByDefault()
    {
        string json = """
                      {
                        "resourceType": "Patient",
                        "id": " whitespace "
                      }
                      """;
        var res = new FhirJsonParser().Parse<Patient>(json);
        
        res.Id.Should().Be("whitespace");
    }
    
      [TestMethod]
  public void FhirJsonParserSerializer_KeepsWhitespace()
    {
      string json = """
  {
    "resourceType": "Practitioner",
    "id": " resourceID",
    "identifier": [
        {
          "use": "usual",
          "type": {
              "text": "INTERNAL"
          },
          "system": "urn:oid:1.2.840.114350.1.13.211.3.7.2.697780",
          "value": " identifier"
        }
    ],
    "active": true,
    "gender": "female"
  }
  """;
  
      var res = new FhirJsonParser(new()
      {
        PreserveWhitespaceInValues = true
      }).Parse<Practitioner>(json);

      res.Id.Should().Be(" resourceID");

      var internalId = res.Identifier?.FirstOrDefault(i => i.Type?.Text == "INTERNAL")?.Value;
      internalId.Should().Be(" identifier");
      var str = new FhirJsonSerializer().SerializeToString(res);
      str.Should().Contain("\"value\":\"identifier\"");
      str = new FhirJsonSerializer(new SerializerSettings { TrimWhiteSpacesInJson = false }).SerializeToString(res);
      str.Should().Contain("\"value\":\" identifier\"");
    }
}