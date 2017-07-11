using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using NUnit.Framework;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestFixture]
    public class UpdateRefreshDeleteAsyncTests
    {
        private string _endpoint = "https://api.hspconsortium.org/rpineda/open";

        [Test]
        public async Task UpdateDelete_UsingResourceIdentity_ResultReturned()
        {
            var client = new FhirClient(_endpoint);
            client.PreferredFormat = ResourceFormat.Json;
            client.ReturnFullResource = true;

            var pat = new Patient()
            {
                Name = new List<HumanName>()
                {
                    new HumanName()
                    {
                        Given = new List<string>() {"test_given"},
                        Family = new List<string>() {"test_family"},
                    }
                },
                Id = "async-test-patient"
            };
            // Create the patient
            Console.WriteLine("Creating patient...");
            Patient p = await client.UpdateAsync<Patient>(pat);
            p.Should().NotBeNull();

            // Refresh the patient
            Console.WriteLine("Refreshing patient...");
            await client.RefreshAsync(p);

            // Delete the patient
            Console.WriteLine("Deleting patient...");
            await client.DeleteAsync(p);

            Console.WriteLine("Reading patient...");
            Func<Task> act = async () =>
            {
                await client.ReadAsync<Patient>(new ResourceIdentity("/Patient/async-test-patient"));
            };

            // VERIFY //
            act.ShouldThrow<FhirOperationException>("the patient is no longer on the server");
            
            
            Console.WriteLine("Test Completed");
        }
        
    }
}