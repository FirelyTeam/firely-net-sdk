using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Tests.Source.Summary;
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
            Assert.AreEqual(ResourceType.Patient.GetLiteral(), summary.ResourceType);
        }

        [TestMethod]
        public void TestPatientJsonSummary()
        {
            const string path = @"TestData\TestPatient.json";
            var summary = assertSummary(path);
            Assert.AreEqual(ResourceType.Patient.GetLiteral(), summary.ResourceType);
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
                Assert.AreEqual(ResourceType.StructureDefinition.GetLiteral(), summary.ResourceType);

                // Conformance resource properties
                Assert.IsNotNull(summary.Canonical());
                Assert.IsTrue(summary.Canonical().ToString().StartsWith("http://hl7.org/fhir/StructureDefinition/"));
                Assert.IsNotNull(summary.Name());
                Assert.AreEqual(ConformanceResourceStatus.Draft.GetLiteral(), summary.Status());

                //Debug.WriteLine($"{summary.ResourceType} | {summary.Canonical()} | {summary.Name()}");

                // StructureDefinition properties
                Assert.AreEqual(StructureDefinition.StructureDefinitionKind.Datatype.GetLiteral(), summary.Kind());
                // If this is a constraining StructDef, then Base should also be specified
                Assert.IsTrue(summary.ConstrainedType() == null || summary.Base() != null);
            }
        }

        // TODO: Verify that Generate throws FileNotFound exception for invalid path

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
