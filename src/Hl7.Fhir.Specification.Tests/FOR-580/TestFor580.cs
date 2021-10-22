using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using NUnit.Framework;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    [TestFixture]
    public class TestFor580
    {
        private readonly SnapshotGeneratorSettings _settings = new()
        {
            ForceRegenerateSnapshots = true,            // ForceExpandAll
            GenerateAnnotationsOnConstraints = false,   // AnnotateDifferentialConstraints
            GenerateExtensionsOnConstraints = false,    // MarkChanges
            GenerateElementIds = true,                  // STU3
            GenerateSnapshotForExternalProfiles = true, // ExpandExternalProfiles
        };

        [TestCase("https://example.org/fhir/StructureDefinition/MyMedicationAdministration", "MedicationAdministration.dosage.rate[x].extension:range")]
        [TestCase("https://example.org/fhir/StructureDefinition/MyMedicationAdministration2", "MedicationAdministration.dosage.rate[x].extension:range")]
        public void SnapshotGeneratorTest(string url, string elementId)
        {
            // Arrange
            using var context = new TestContextFor580();

            var sd = context.GetResource<StructureDefinition>(url);

            var snapshotGenerator = new SnapshotGenerator(context.Resolver, _settings);

            // Act
            var elements = snapshotGenerator.GenerateAsync(sd).Result;

            // Assert
            snapshotGenerator.Outcome.Should().BeNull();

            var extensionElement = elements.SingleOrDefault(x => x.ElementId == elementId);

            extensionElement.Should().NotBeNull();
        }
    }
}
