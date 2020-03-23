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
    public partial class FhirClientAsyncTests
    {
        private string _endpoint = "http://localhost:4080/";
        
        [ClassInitialize]
        public void ClassInitialize(TestContext context)
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
                Id = "pat1",
                Identifier = new List<Identifier>()
                {
                    new Identifier()
                    {
                        System = "urn:oid:1.2.36.146.595.217.0.1",
                        Value = "12345"
                    }
                }
            };

            var loc = new Location()
            {
                Address = new Address()
                {
                    City = "Den Burg"
                },
                Id = "1"
            };

            // Create the patient
            Console.WriteLine("Creating patient...");
            Patient p = client.Update(pat);
            Location l = client.Update(loc);
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