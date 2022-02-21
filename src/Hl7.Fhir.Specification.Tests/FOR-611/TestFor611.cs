using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Utility;
using NUnit.Framework;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    [TestFixture]
    public class TestFor611
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

        [TestCase("Patient.extension", "Patient.extension:birthPlace")]
        public void SnapshotGeneratorTest(string parentId, string elementId)
        {
            // Arrange
            using var context = new TestContextFor611();

            var sd = context.GetResource<StructureDefinition>("https://example.org/fhir/StructureDefinition/MyPatient");

            sd.Differential.Element.Should().HaveCount(2);

            var extensionElement = sd.Differential.Element.Single(x => x.ElementId == elementId);

            extensionElement.Min.Should().Be(0);
            extensionElement.Max.Should().BeNull();

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
            extensionElement = elements.Single(x => x.ElementId == elementId);

            extensionElement.Min.Should().Be(0);
            extensionElement.Max.Should().Be("1");

            var baseElement = extensionElement.Annotation<TestAnnotation>().BaseElementDefinition;

            baseElement.Min.Should().Be(0);
            baseElement.Max.Should().Be("1");
        }
    }
}
