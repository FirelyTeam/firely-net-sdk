using System;
using System.Linq;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestClass]
    public class ReadAsyncTests
    {
        private string _endpoint = "https://api.hspconsortium.org/rpineda/open";

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task Read_UsingResourceIdentity_ResultReturned()
        {
            var client = new FhirClient(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                ReturnFullResource = true
            };

            Patient p = await client.ReadAsync<Patient>(new ResourceIdentity("/Patient/SMART-1288992"));
            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name[0].Given);
            Assert.IsNotNull(p.Name[0].Family);
            Console.WriteLine($"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task Read_UsingLocationString_ResultReturned()
        {
            var client = new FhirClient(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                ReturnFullResource = true
            };

            Patient p = await client.ReadAsync<Patient>("/Patient/SMART-1288992");
            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name[0].Given);
            Assert.IsNotNull(p.Name[0].Family);
            Console.WriteLine($"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
            Console.WriteLine("Test Completed");
        }
    }
}