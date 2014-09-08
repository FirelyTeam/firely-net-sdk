using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fhir.Profiling;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class TestExtensions
    {
        [TestMethod]
        public void Extensions()
        {
            Specification patientSpec = Factory.GetExtendedPatientSpec(expand: false, online: false);
            var resource = FhirFile.LoadResource("TestData\\patient.extended.valid.xml");
            Report report = patientSpec.Validate(resource);

            Assert.IsTrue(report.IsValid);
        }
    }
}
