using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Profiling;
using Hl7.Fhir.Specification.Model;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class TestSimpleValidation
    {
        static SpecificationWorkspace spec;
        
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            spec = Factory.GetPatientSpec(expand: false, online: false);
        }

        [TestMethod]
        public void InvalidElement()
        {
            var resource = Factory.LoadResource("TestData\\Patient.InvalidElement.xml");
            Report report = spec.Validate(resource);
            
            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Element, Status.Unknown));
        }

        [TestMethod]
        public void ConstraintError()
        {
            var resource = Factory.LoadResource("TestData\\Patient.ConstraintError.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Constraint, Status.Failed));
        }

        
    }
}
