using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                        "id": " whitespace ",
                      }
                      """;
        var res = new FhirJsonParser(new()
        {
            PersistWhitespacesInValues = true
        }).Parse<Patient>(json);
        
        res.Id.Should().Be(" whitespace ");
    }
    
    [TestMethod]
    public void FhirJsonParser_TrimsWhitespaceByDefault()
    {
        string json = """
                      {
                        "resourceType": "Patient",
                        "id": " whitespace ",
                      }
                      """;
        var res = new FhirJsonParser().Parse<Patient>(json);
        
        res.Id.Should().Be("whitespace");
    }
}