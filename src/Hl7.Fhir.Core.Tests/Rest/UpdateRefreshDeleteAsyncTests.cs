using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Core.AsyncTests
{
    public partial class FhirClientAsyncTests
    {
#pragma warning disable CS0618

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task UpdateDelete_UsingResourceIdentity_ResultReturned()
        {
            var client = new FhirClient(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                PreferredReturn = Prefer.ReturnRepresentation
            };

            await updateDelete(client);
        }
        
        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task UpdateDelete_UsingResourceIdentity_ResultReturnedHttpClient()
        {
            using (var client = new FhirHttpClient(_endpoint))
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
            Func<System.Threading.Tasks.Task> act = async () =>
            {
                await client.ReadAsync<Patient>(new ResourceIdentity("/Patient/async-test-patient"));
            };

            // VERIFY //
            await ExceptionAssert.Throws<FhirOperationException>(act);

            Console.WriteLine("Test Completed");
        }
    }
}