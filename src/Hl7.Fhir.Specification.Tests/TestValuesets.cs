using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Specification.Model;
using Hl7.Fhir.Profiling;

namespace Fhir.Profiling.Tests
{
    [TestClass]
    public class DunmailTests
    {
        static SpecificationWorkspace spec;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            spec = SpecificationFactory.Create("http://hl7.org/fhir/Profile/Conformance");
        }

        [TestMethod]
        public void Dunmail()
        {
            var resource = TestProvider.LoadResource("TestData/Dunmail.xml");
            Report report = spec.Validate(resource);

            Assert.IsTrue(report.Errors.Count() == 0);
        }
    }
}
