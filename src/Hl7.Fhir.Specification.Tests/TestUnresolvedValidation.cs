using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class TestSimpleValidation
    {
        static SpecificationWorkspace spec;
        
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            spec = SpecificationFactory.Create("http://hl7.org/fhir/Profile/Patient");
        }

        [TestMethod]
        public void InvalidElement()
        {
            var resource = TestProvider.LoadResource("TestData\\Patient.InvalidElement.xml");
            Report report = spec.Validate(resource);
            
            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Element, Status.Unknown));
        }

        [TestMethod]
        public void ConstraintError()
        {
            var resource = TestProvider.LoadResource("TestData\\Patient.ConstraintError.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Constraint, Status.Failed));
        }

        
    }
}
