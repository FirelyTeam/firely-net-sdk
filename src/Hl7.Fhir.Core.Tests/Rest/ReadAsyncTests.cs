using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Rest.Legacy;
using Hl7.Fhir.Tests.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestClass]
    public class FhirClientReadAsyncTests
    {
        private static string _endpoint = FhirClientTests.testEndpoint.OriginalString;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var client = new LegacyFhirClient(_endpoint);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
            


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
        public async System.Threading.Tasks.Task Read_UsingResourceIdentity_ResultReturnedWebClient()
        {
            var client = new LegacyFhirClient(_endpoint);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;          

            await readUsingResourceId(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task Read_UsingResourceIdentity_ResultReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
                await readUsingResourceId(client);
            }          
        }


        private static async System.Threading.Tasks.Task readUsingResourceId(BaseFhirClient client)
        {
            Patient p = await client.ReadAsync<Patient>(new ResourceIdentity("/Patient/pat1"));
            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name[0].Given);
            Assert.IsNotNull(p.Name[0].Family);
            Console.WriteLine($"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task Read_UsingLocationString_ResultReturnedWebClient()
        {
            var client = new LegacyFhirClient(_endpoint);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
            

            await readUsingLocationString(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task Read_UsingLocationString_ResultReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
                await readUsingLocationString(client);
            }            
        }

        private static async System.Threading.Tasks.Task readUsingLocationString(BaseFhirClient client)
        {
            Patient p = await client.ReadAsync<Patient>("/Patient/pat1");
            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name[0].Given);
            Assert.IsNotNull(p.Name[0].Family);
            Console.WriteLine($"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
            Console.WriteLine("Test Completed");
        }
    }
}