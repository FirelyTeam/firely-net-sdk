using System;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using Hl7.Fhir.Model.R4;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestClass]
    public class ReadAsyncTests
    {
        private readonly string _endpoint = "https://server.fire.ly/r4";

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task Read_UsingResourceIdentity_ResultReturned()
        {
            var client = new FhirR4Client(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                ReturnFullResource = true
            };

            Patient p = await client.ReadAsync<Patient>(new ResourceIdentity("/Patient/example-r4"));
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
            var client = new FhirR4Client(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                ReturnFullResource = true
            };

            Patient p = await client.ReadAsync<Patient>("/Patient/example-r4");
            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name[0].Given);
            Assert.IsNotNull(p.Name[0].Family);
            Console.WriteLine($"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
            Console.WriteLine("Test Completed");
        }
    }
}