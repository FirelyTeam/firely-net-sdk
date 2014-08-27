using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fhir.IO;
using System.Linq;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class TestProfileResources
    {
        static Specification spec;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            spec = Factory.GetProfileSpec(resolve: false);
            
        }

        [TestMethod]
        public void LipidProfile()
        {
            // todo: Resolving does not resolve custom profiles (for testing)
            // therefore these profiles are loaded the old way.

            var resource = FhirFile.LoadResource("TestData\\lipid.profile.xml");
            Report report = spec.Validate(resource);
            var errors = report.Errors.ToList();
            Assert.IsTrue(report.IsValid);
        }
    }
}
