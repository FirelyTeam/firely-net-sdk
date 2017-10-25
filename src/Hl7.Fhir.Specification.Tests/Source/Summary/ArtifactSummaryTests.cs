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
                Assert.AreEqual(path, summary.Origin);
                Assert.AreEqual(ResourceType.StructureDefinition.GetLiteral(), summary.ResourceType);
                
                // TODO: Provide extension methods on SummaryDetails
                Assert.AreEqual(StructureDefinition.StructureDefinitionKind.Datatype.GetLiteral(), summary[StructureDefinitionSummaryDetails.KindKey]);
                // If this is a constraining StructDef, then Base should also be specified
                Assert.IsTrue(
                    summary[StructureDefinitionSummaryDetails.ConstrainedTypeKey] == null
                    || summary[StructureDefinitionSummaryDetails.BaseKey] != null
                );
                Debug.WriteLine($"{summary.ResourceType} | {summary[StructureDefinitionSummaryDetails.ConstrainedTypeKey]} | {summary[StructureDefinitionSummaryDetails.BaseKey]}");
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
