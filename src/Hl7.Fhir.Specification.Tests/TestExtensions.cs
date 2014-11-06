using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class TestExtensions
    {
        [TestMethod]
        public void Extensions()
        {
            SpecificationWorkspace spec = Factory.GetExtendedPatientSpec(expand: false, online: false);
            var resource = Factory.LoadResource("TestData\\patient.extended.valid.xml");
            Report report = spec.Validate(resource);

            Assert.IsTrue(report.IsValid);
        }
    }
}
