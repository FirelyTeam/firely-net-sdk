using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Utility;
using NUnit.Framework;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    [TestFixture]
    public class TestFor568
    {
        private readonly SnapshotGeneratorSettings _settings = new()
        {
            ForceRegenerateSnapshots = true,            // ForceExpandAll
            GenerateAnnotationsOnConstraints = false,   // AnnotateDifferentialConstraints
            GenerateExtensionsOnConstraints = false,    // MarkChanges
            GenerateElementIds = true,                  // STU3
            GenerateSnapshotForExternalProfiles = true, // ExpandExternalProfiles
        };

        public sealed class TestAnnotation
        {
            public TestAnnotation(StructureDefinition baseStructure, ElementDefinition baseElemDef)
            {
                BaseStructureDefinition = baseStructure;
                BaseElementDefinition = baseElemDef;
            }

            public StructureDefinition BaseStructureDefinition { get; }
            public ElementDefinition BaseElementDefinition { get; }
        }

        [TestCase("Patient.address", "Patient.address.country.extension:countryCode.value[x]:valueCodeableConcept.coding")]
        [TestCase("Patient.contact.address", "Patient.contact.address.country.extension:countryCode.value[x]:valueCodeableConcept.coding")]
        public void SnapshotGeneratorTest(string parentId, string elementId)
        {
            // Arrange
            using var context = new TestContextFor568();

            // Profile derived from nl-core-patient.json
            var sd = context.GetResource<StructureDefinition>("https://example.org/fhir/StructureDefinition/Mynl-core-patient");

            sd.Differential.Element.Should().HaveCount(0); // No changes in derived profile

            var snapshotGenerator = new SnapshotGenerator(context.Resolver, _settings);

            snapshotGenerator.PrepareElement += delegate (object _, SnapshotElementEventArgs e)
            {
                e.Element.Should().NotBeNull();

                if (e.Element.Annotation<TestAnnotation>() != null)
                    e.Element.RemoveAnnotations<TestAnnotation>();

                e.Element.AddAnnotation(new TestAnnotation(e.BaseStructure, e.BaseElement));
            };

            var elements = snapshotGenerator.GenerateAsync(sd).Result;

            snapshotGenerator.Outcome.Should().BeNull();

            var parentElement = elements.Single(x => x.ElementId == parentId);

            // Act
            elements = snapshotGenerator.ExpandElementAsync(elements, parentElement).Result.ToList();

            // Assert
            var slicingElement = elements.Single(x => x.ElementId == elementId);

            slicingElement.Slicing.Should().NotBeNull();
            slicingElement.Slicing.Discriminator.Should().HaveCount(1);
            slicingElement.Slicing.Discriminator[0].Type.Should().Be(ElementDefinition.DiscriminatorType.Pattern);
            slicingElement.Slicing.Discriminator[0].Path.Should().Be("$this");

            var baseElement = slicingElement.Annotation<TestAnnotation>().BaseElementDefinition;

            baseElement.Slicing.Should().NotBeNull();
            baseElement.Slicing.Discriminator.Should().HaveCount(1);
            baseElement.Slicing.Discriminator[0].Type.Should().Be(ElementDefinition.DiscriminatorType.Pattern);
            baseElement.Slicing.Discriminator[0].Path.Should().Be("$this");
        }
    }
}
