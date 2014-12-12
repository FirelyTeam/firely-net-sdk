using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class TestCustomProfiles
    {
        static SpecificationWorkspace spec;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            spec = SpecificationFactory.Create("http://hl7.org/fhir/Profile/Profile");
        }
       
        [TestMethod]
        public void LipidProfile()
        {
            var resource = TestProvider.LoadResource("TestData\\lipid.profile.xml");
            Report report = Validator.Validate(resource);

            var errors = report.Errors.ToList();
            Assert.IsTrue(report.IsValid);
        }
    }
}
