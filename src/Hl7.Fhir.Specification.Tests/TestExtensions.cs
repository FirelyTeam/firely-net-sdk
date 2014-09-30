using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Profiling;
using Hl7.Fhir.Specification.Model;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class TestExtensions
    {
        [TestMethod,Ignore]
        public void Extensions()
        {
            SpecificationWorkspace spec = Factory.GetExtendedPatientSpec(expand: false, online: false);
            var resource = Factory.LoadResource("TestData\\patient.extended.valid.xml");
            Report report = spec.Validate(resource);

            Assert.IsTrue(report.IsValid);
        }
    }
}
