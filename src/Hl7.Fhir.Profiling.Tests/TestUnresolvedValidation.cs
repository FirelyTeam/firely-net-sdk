using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fhir.Profiling;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class TestSimpleValidation
    {
        static Specification spec;
        
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            spec = Factory.GetPatientSpec(expand: false, online: false);
        }

        [TestMethod]
        public void InvalidElement()
        {
            var resource = FhirFile.LoadResource("TestData\\Patient.InvalidElement.xml");
            Report report = spec.Validate(resource);
            
            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Element, Status.Unknown));
        }

        [TestMethod]
        public void ConstraintError()
        {
            var resource = FhirFile.LoadResource("TestData\\Patient.ConstraintError.xml");
            Report report = spec.Validate(resource);

            Assert.IsFalse(report.IsValid);
            Assert.AreEqual(1, report.ErrorCount);
            Assert.IsTrue(report.Contains(Group.Constraint, Status.Failed));
        }

        
    }
}
