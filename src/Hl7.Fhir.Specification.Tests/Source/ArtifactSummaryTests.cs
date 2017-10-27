using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source.Summary;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class ArtifactSummaryTests
    {

        [TestMethod]
        public void TestPatientXmlSummary()
        {
            const string path = @"TestData\TestPatient.xml";
            var summary = assertSummary(path);
            Assert.AreEqual(ResourceType.Patient.GetLiteral(), summary.ResourceTypeName);
        }

        [TestMethod]
        public void TestPatientJsonSummary()
        {
            const string path = @"TestData\TestPatient.json";
            var summary = assertSummary(path);
            Assert.AreEqual(ResourceType.Patient.GetLiteral(), summary.ResourceTypeName);
        }

        [TestMethod]
        public void TestValueSetXmlSummary()
        {
            const string path = @"TestData\validation\SectionTitles.valueset.xml";
            var summary = assertSummary(path);

            // Common properties
            Assert.AreEqual(ResourceType.ValueSet.GetLiteral(), summary.ResourceTypeName);
            Assert.IsTrue(summary.ResourceType == ResourceType.ValueSet);

            // Conformance resource properties
            Assert.IsNotNull(summary.GetConformanceCanonicalUrl());
            Assert.AreEqual(@"http://example.org/ValueSet/SectionTitles", summary.GetConformanceCanonicalUrl());
            Assert.AreEqual("MainBundle Section title codes", summary.GetConformanceName());
            Assert.AreEqual(PublicationStatus.Draft.GetLiteral(), summary.GetConformanceStatus());
        }

        [TestMethod]
        public void TestProfilesTypesJson()
        {
            const string path = @"TestData\profiles-types.json";

            var summaries = ArtifactSummaryGenerator.Generate(path);
            Assert.IsNotNull(summaries);
            Assert.AreNotEqual(0, summaries.Count);
            for (int i = 0; i < summaries.Count; i++)
            {
                var summary = summaries[i];
                Assert.IsFalse(summary.IsFaulted);

                // Common properties
                Assert.AreEqual(path, summary.Origin);
                Assert.AreEqual(ResourceType.StructureDefinition.GetLiteral(), summary.ResourceTypeName);
                Assert.IsTrue(summary.ResourceType == ResourceType.StructureDefinition);

                // Conformance resource properties
                Assert.IsNotNull(summary.GetConformanceCanonicalUrl());
                Assert.IsTrue(summary.GetConformanceCanonicalUrl().ToString().StartsWith("http://hl7.org/fhir/StructureDefinition/"));
                Assert.IsNotNull(summary.GetConformanceName());
                Assert.IsNotNull(summary.GetConformanceStatus());
                Assert.AreEqual(PublicationStatus.Draft.GetLiteral(), summary.GetConformanceStatus());

                //Debug.WriteLine($"{summary.ResourceType} | {summary.Canonical()} | {summary.Name()}");

                // StructureDefinition properties

                Assert.IsNotNull(summary.GetStructureDefinitionFhirVersion());
                Assert.AreEqual(ModelInfo.Version, summary.GetStructureDefinitionFhirVersion());

                // For profiles-types, we expect Kind = ComplexType | PrimitiveType
                Assert.IsNotNull(summary.GetStructureDefinitionKind());
                Assert.IsTrue(
                    summary.GetStructureDefinitionKind() == StructureDefinition.StructureDefinitionKind.ComplexType.GetLiteral()
                    ||
                    summary.GetStructureDefinitionKind() == StructureDefinition.StructureDefinitionKind.PrimitiveType.GetLiteral()
                );

                Assert.IsNotNull(summary.GetStructureDefinitionType());
                // If this is a specializing StructDef, then BaseDefinition should also be specified
                Assert.IsTrue(
                    summary.GetStructureDefinitionDerivation() != StructureDefinition.TypeDerivationRule.Specialization.GetLiteral()
                    || summary.GetStructureDefinitionBaseDefinition() != null);
            }
        }

        ArtifactSummary assertSummary(string path)
        {
            var summaries = ArtifactSummaryGenerator.Generate(path);
            Assert.IsNotNull(summaries);
            Assert.AreEqual(1, summaries.Count);
            var summary = summaries[0];
            Assert.IsFalse(summary.IsFaulted);
            Assert.AreEqual(path, summary.Origin);
            return summary;
        }
    }
}
