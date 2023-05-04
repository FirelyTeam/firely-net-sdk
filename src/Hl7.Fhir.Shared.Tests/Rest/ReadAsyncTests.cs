using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Tests.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using T = System.Threading.Tasks;
using static Hl7.Fhir.Tests.Rest.FhirClientTests;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestClass]
    public class FhirClientReadAsyncTests
    {
        private static readonly string ENDPOINT = FhirClientTests.TestEndpoint.OriginalString;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            CreateItems();
        }

        [TestMethod]
        [TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async T.Task Read_UsingResourceIdentity_ResultReturnedHttpClient()
        {
            using var client = new FhirClient(ENDPOINT);

            Patient p = await client.ReadAsync<Patient>(ResourceIdentity.Build("Patient", PATIENTID));
            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name[0].Given);
            Assert.IsNotNull(p.Name[0].Family);
            Console.WriteLine($"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async T.Task Read_UsingLocationString_ResultReturnedHttpClient()
        {
            using var client = new FhirClient(ENDPOINT);
            
            Patient p = await client.ReadAsync<Patient>(PATIENTIDEP);
            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name[0].Given);
            Assert.IsNotNull(p.Name[0].Family);
            Console.WriteLine($"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
            Console.WriteLine("Test Completed");
        }
    }
}