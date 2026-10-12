#nullable enable

#if R5
using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Support.Tests.Serialization;

// integer64 only exists from R5 on, so this cannot be covered by the STU3-based tests in Hl7.Fhir.Support.Poco.Tests.
[TestClass]
public class PrimitiveValueConverterTests
{
    private static readonly DeserializerSettings SETTINGS = new()
    {
        PrimitiveValueConverter = (v, _, m) =>
            m.NativeType == typeof(Integer64) && v is string s && s.EndsWith(".0") ? s[..^2] : v
    };

    [TestMethod]
    public void CanFixInteger64FormatInJson()
    {
        const string json = """{"resourceType": "Patient", "extension": [{"url": "http://example.org/i64", "valueInteger64": "42.0"}]}""";

        new FhirJsonDeserializer(SETTINGS).TryDeserializeResource(json, out var resource, out var issues).Should().BeTrue();
        resource.Should().BeOfType<Patient>().Which.Extension.Single().Value.Should().BeOfType<Integer64>().Which.Value.Should().Be(42);

        new FhirJsonDeserializer().TryDeserializeResource(json, out _, out issues);
        issues.Should().ContainSingle(i => i.ErrorCode == COVE.LITERAL_INVALID_CODE);
    }

    [TestMethod]
    public void CanFixInteger64FormatInXml()
    {
        const string xml = """<Patient xmlns="http://hl7.org/fhir"><extension url="http://example.org/i64"><valueInteger64 value="42.0"/></extension></Patient>""";

        new FhirXmlDeserializer(SETTINGS).TryDeserializeResource(xml, out var resource, out var issues).Should().BeTrue();
        resource.Should().BeOfType<Patient>().Which.Extension.Single().Value.Should().BeOfType<Integer64>().Which.Value.Should().Be(42);

        new FhirXmlDeserializer().TryDeserializeResource(xml, out _, out issues);
        issues.Should().ContainSingle(i => i.ErrorCode == COVE.LITERAL_INVALID_CODE);
    }
}
#endif
