using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class TestCustomProfiles
    {
        [TestMethod]
        public void CorrectFixedValue()
        {
            var resource = FhirFile.LoadResource("TestData\\lipid.fixvalue.xml");
            Report report = lipidSpec.Validate(resource);
            Assert.IsTrue(report.IsValid);
        }

        [TestMethod]
        public void IncorrectFixedValue()
        {
            var resource = FhirFile.LoadResource("TestData\\lipid.fixvalue.wrong.xml");
            Report report = lipidSpec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Value, Status.Failed));
        }
    }
}
