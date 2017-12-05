using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source.Summary;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class ArtifactSummaryTests
    {

        [TestMethod]
        public void TestPatientXmlSummary() => TestPatientSummary(@"TestData\TestPatient.xml");

        [TestMethod]
        public void TestPatientJsonSummary() => TestPatientSummary(@"TestData\TestPatient.json");

        void TestPatientSummary(string path)
        {
            var summary = assertSummary(path);
            Assert.AreEqual(ResourceType.Patient.GetLiteral(), summary.ResourceTypeName);
        }

        [TestMethod]
        public void TestPatientXmlSummaryWithCustomHarvester()
            => TestPatientSummaryWithCustomHarvester(@"TestData\TestPatient.xml", "Donald");

        [TestMethod]
        public void TestPatientJsonSummaryWithCustomHarvester()
            => TestPatientSummaryWithCustomHarvester(@"TestData\TestPatient.json", "Chalmers");

        void TestPatientSummaryWithCustomHarvester(string path, params string[] expectedNames)
        {
            // Combine default harvesters and custom harvester
            var harvesters = new ArtifactSummaryHarvester[ArtifactSummaryGenerator.DefaultHarvesters.Length + 1];
            Array.Copy(ArtifactSummaryGenerator.DefaultHarvesters, harvesters, ArtifactSummaryGenerator.DefaultHarvesters.Length);
            harvesters[ArtifactSummaryGenerator.DefaultHarvesters.Length] = HarvestPatientSummary;

            var summary = assertSummary(path, harvesters);
            Assert.AreEqual(ResourceType.Patient.GetLiteral(), summary.ResourceTypeName);
            var familyNames = summary.GetValueOrDefault<string[]>(PatientFamilyNameKey);
            Assert.IsNotNull(familyNames);
            Assert.AreEqual(1, familyNames.Length);
            Assert.IsTrue(expectedNames.SequenceEqual(familyNames));
        }

        // Custom artifact summary harvester implementation to extract family name(s) from Patient resources
        const string PatientFamilyNameKey = "Patient.name.family";
        static bool HarvestPatientSummary(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (properties.GetTypeName() == ResourceType.Patient.GetLiteral())
            {
                nav.HarvestValues(properties, PatientFamilyNameKey, "name", "family");
                return true;
            }
            return false;
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
            Assert.AreEqual(ConformanceResourceStatus.Draft.GetLiteral(), summary.GetConformanceStatus());

            Assert.IsNotNull(summary.GetValueSetSystem());
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

                var fi = new FileInfo(path);
                Assert.AreEqual(fi.Length, summary.FileSize);
                Assert.AreEqual(fi.LastWriteTimeUtc, summary.LastModified);

                Assert.AreEqual(ResourceType.StructureDefinition.GetLiteral(), summary.ResourceTypeName);
                Assert.IsTrue(summary.ResourceType == ResourceType.StructureDefinition);

                // Conformance resource properties
                Assert.IsNotNull(summary.GetConformanceCanonicalUrl());
                Assert.IsTrue(summary.GetConformanceCanonicalUrl().ToString().StartsWith("http://hl7.org/fhir/StructureDefinition/"));
                Assert.IsNotNull(summary.GetConformanceName());
                Assert.AreEqual(ConformanceResourceStatus.Draft.GetLiteral(), summary.GetConformanceStatus());

                //Debug.WriteLine($"{summary.ResourceType} | {summary.Canonical()} | {summary.Name()}");

                // StructureDefinition properties
                Assert.IsNotNull(summary.GetStructureDefinitionFhirVersion());
                Assert.AreEqual(ModelInfo.Version, summary.GetStructureDefinitionFhirVersion());

                Assert.AreEqual(StructureDefinition.StructureDefinitionKind.Datatype.GetLiteral(), summary.GetStructureDefinitionKind());
                // If this is a constraining StructDef, then Base should also be specified
                Assert.IsTrue(summary.GetStructureDefinitionConstrainedType() == null || summary.GetStructureDefinitionBase() != null);
            }
        }

        ArtifactSummary assertSummary(string path, params ArtifactSummaryHarvester[] harvesters)
        {
            var summaries = ArtifactSummaryGenerator.Generate(path, harvesters);
            Assert.IsNotNull(summaries);
            Assert.AreEqual(1, summaries.Count);
            var summary = summaries[0];
            Assert.IsFalse(summary.IsFaulted);
            Assert.AreEqual(path, summary.Origin);

            var fi = new FileInfo(path);
            Assert.AreEqual(fi.Length, summary.FileSize);
            Assert.AreEqual(fi.LastWriteTimeUtc, summary.LastModified);

            return summary;
        }
    }
}
