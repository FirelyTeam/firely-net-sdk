using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Tests.Source.Summary;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

