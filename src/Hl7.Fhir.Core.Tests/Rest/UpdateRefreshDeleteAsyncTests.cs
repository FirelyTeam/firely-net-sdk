using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Rest.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestClass]
    public class UpdateRefreshDeleteAsyncTests
    {
        private string _endpoint = "https://api.hspconsortium.org/rpineda/open";

        private FhirHttpClient createHttpClient() => new FhirHttpClient(_endpoint, new FhirClientSettings { PreferredFormat = ResourceFormat.Json, PreferredReturn = Prefer.ReturnRepresentation });
        private FhirClient createClient() => new FhirClient(_endpoint, new FhirClientSettings { PreferredFormat = ResourceFormat.Json, PreferredReturn = Prefer.ReturnRepresentation });

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task UpdateDelete_UsingResourceIdentity_ResultReturnedWebClient()
        {
            using (var client = createClient())
            {

                var pat = new Patient()
                {
                    Name = new List<HumanName>()
                {
                    new HumanName()
                    {
                        Given = new List<string>() {"test_given"},
                        Family = "test_family",
                    }
                },
                    Id = "async-test-patient"
                };
                // Create the patient
                Console.WriteLine("Creating patient...");
                Patient p = await client.UpdateAsync<Patient>(pat);
                Assert.IsNotNull(p);

                // Refresh the patient
                Console.WriteLine("Refreshing patient...");
                await client.RefreshAsync(p);

                // Delete the patient
                Console.WriteLine("Deleting patient...");
                await client.DeleteAsync(p);

                Console.WriteLine("Reading patient...");
                async System.Threading.Tasks.Task act()
                {
                    await client.ReadAsync<Patient>(new ResourceIdentity("/Patient/async-test-patient"));
                }

                // VERIFY //
                Assert.ThrowsException<FhirOperationException>(act, "the patient is no longer on the server");


                Console.WriteLine("Test Completed");
            }
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task UpdateDelete_UsingResourceIdentity_ResultReturnedHttpClient()
        {
            using (var client = createHttpClient())
            {
                var pat = new Patient()
                {
                    Name = new List<HumanName>()
                {
                    new HumanName()
                    {
                        Given = new List<string>() {"test_given"},
                        Family = "test_family",
                    }
                },
                    Id = "async-test-patient"
                };
                // Create the patient
                Console.WriteLine("Creating patient...");
                Patient p = await client.UpdateAsync<Patient>(pat);
                Assert.IsNotNull(p);

                // Refresh the patient
                Console.WriteLine("Refreshing patient...");
                await client.RefreshAsync(p);

                // Delete the patient
                Console.WriteLine("Deleting patient...");
                await client.DeleteAsync(p);

                Console.WriteLine("Reading patient...");
                async System.Threading.Tasks.Task act()
                {
                    await client.ReadAsync<Patient>(new ResourceIdentity("/Patient/async-test-patient"));
                }

                // VERIFY //
                Assert.ThrowsException<FhirOperationException>(act, "the patient is no longer on the server");


                Console.WriteLine("Test Completed");
            }
        }
    }
}