using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Rest.Legacy;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Tests.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Core.AsyncTests
{
    public class UpdateRefreshDeleteAsyncTests
    {
        private static string _endpoint = FhirClientTests.testEndpoint.OriginalString;

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task UpdateDelete_UsingResourceIdentity_ResultReturnedWebClient()
        {
            var client = new LegacyFhirClient(_endpoint);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;            

            await updateDelete(client);
        }
        
        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task UpdateDelete_UsingResourceIdentity_ResultReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
                await updateDelete(client);
            }           
        }


        private static async System.Threading.Tasks.Task updateDelete(BaseFhirClient client)
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
            await ExceptionAssert.Throws<FhirOperationException>(act);

            Console.WriteLine("Test Completed");
        }
    }
}