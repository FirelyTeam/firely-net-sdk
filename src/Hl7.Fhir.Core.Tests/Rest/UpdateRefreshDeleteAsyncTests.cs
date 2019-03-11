using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestClass]
    public class UpdateRefreshDeleteAsyncTests
    {
        private readonly string _endpoint = "https://api.hspconsortium.org/rpineda/open";

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async System.Threading.Tasks.Task UpdateDelete_UsingResourceIdentity_ResultReturnedWebClient()
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
            Assert.ThrowsException<FhirOperationException>(act, "the patient is no longer on the server");


            Console.WriteLine("Test Completed");
        }
    }
}