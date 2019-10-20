using System;
using System.Collections.Generic;
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
        private string _endpoint = "http://localhost:4080/";
        
        public ReadAsyncTests()
        {
            var client = new FhirClient(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                PreferredReturn = Prefer.ReturnRepresentation
            };

            var pat = new Patient()
            {
                Name = new List<HumanName>()
                {
                    new HumanName()
                    {
                        Given = new List<string>() {"test_given"},
                        Family = "Donald",
                    }
                },
                Id = "pat1"
            };
            // Create the patient
            Console.WriteLine("Creating patient...");
            Patient p = client.Update<Patient>(pat);
            Assert.IsNotNull(p);
        }


        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task Read_UsingResourceIdentity_ResultReturned()
        {
            var client = new FhirClient(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                PreferredReturn = Prefer.ReturnRepresentation
            };
            
            Patient p = await client.ReadAsync<Patient>(new ResourceIdentity("/Patient/pat1"));
            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name[0].Given);
            Assert.IsNotNull(p.Name[0].Family);
            Console.WriteLine($"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task Read_UsingLocationString_ResultReturned()
        {
            var client = new FhirClient(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                PreferredReturn = Prefer.ReturnRepresentation
            };

            Patient p = await client.ReadAsync<Patient>("/Patient/pat1");
            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name[0].Given);
            Assert.IsNotNull(p.Name[0].Family);
            Console.WriteLine($"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
            Console.WriteLine("Test Completed");
        }
    }
}