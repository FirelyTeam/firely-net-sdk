#nullable enable

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Support.Tests.Serialization;

[TestClass]
public class DeserializerPresetTests
{
    // A resource with an element that is not defined on Patient: the data can only be kept by
    // putting it in overflow, so NoOverflow must report it while Recoverable may accept it.
    private const string OVERFLOWING_JSON = """
                                            {
                                              "resourceType": "Patient",
                                              "notAPatientElement": "some value"
                                            }
                                            """;

    private const string OVERFLOWING_XML =
        """<Patient xmlns="http://hl7.org/fhir"><notAPatientElement value="some value" /></Patient>""";

    private static FieldInfo? preset(Type deserializer, string name) =>
        deserializer.GetField(name, BindingFlags.Public | BindingFlags.Static);

    [TestMethod]
    [DataRow(typeof(FhirXmlDeserializer), DisplayName = "xml")]
    [DataRow(typeof(FhirJsonDeserializer), DisplayName = "json")]
    public void EveryDeserializationModeHasAPreset(Type deserializer)
    {
        var missing = Enum.GetNames<DeserializationMode>()
            .Where(mode => preset(deserializer, mode.ToUpperInvariant()) is null)
            .ToList();

        missing.Should().BeEmpty(
            "every member of DeserializationMode should be reachable as a static preset on {0}",
            deserializer.Name);
    }

    [TestMethod]
    [DataRow(typeof(FhirXmlDeserializer), DisplayName = "xml")]
    [DataRow(typeof(FhirJsonDeserializer), DisplayName = "json")]
    public void PresetsAreReadOnlyInstancesOfTheirOwnDeserializer(Type deserializer)
    {
        foreach (var mode in Enum.GetNames<DeserializationMode>())
        {
            var field = preset(deserializer, mode.ToUpperInvariant());
            field.Should().NotBeNull();
            field!.IsInitOnly.Should().BeTrue("preset {0} should be static readonly", field.Name);
            field.GetValue(null).Should().BeOfType(deserializer);
        }
    }

    [TestMethod]
    public void JsonNoOverflowPresetRejectsOverflowButRecoverableAcceptsIt()
    {
        FhirJsonDeserializer.NOOVERFLOW.Invoking(d => d.Deserialize<Patient>(OVERFLOWING_JSON))
            .Should().Throw<DeserializationFailedException>();

        FhirJsonDeserializer.RECOVERABLE.Deserialize<Patient>(OVERFLOWING_JSON)
            .HasOverflow.Should().BeTrue();
    }

    [TestMethod]
    public void XmlNoOverflowPresetRejectsOverflowButRecoverableAcceptsIt()
    {
        FhirXmlDeserializer.NOOVERFLOW.Invoking(d => d.Deserialize<Patient>(OVERFLOWING_XML))
            .Should().Throw<DeserializationFailedException>();

        FhirXmlDeserializer.RECOVERABLE.Deserialize<Patient>(OVERFLOWING_XML)
            .HasOverflow.Should().BeTrue();
    }
}
