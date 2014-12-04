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
            var spec = SpecificationFactory.Create("http://here.there/patient.extended.profile.xml", "http://here.there/type-Extension.profile.xml");

            var resource = TestProvider.LoadResource("TestData\\patient.extended.valid.xml");
            Report report = spec.Validate(resource);

            Assert.IsTrue(report.IsValid);
        }
    }
}
