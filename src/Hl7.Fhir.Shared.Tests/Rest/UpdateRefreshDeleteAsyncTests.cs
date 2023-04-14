using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Tests.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestClass]
    public class UpdateRefreshDeleteAsyncTests
    {
        private static string _endpoint = FhirClientTests.TestEndpoint.OriginalString;

        [TestMethod]
        [TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        [DataRow(DecompressionMethods.None)]
        //[DataRow(DecompressionMethods.GZip)], Ignore("FS returns 'oops something went wrong'")]
        public async System.Threading.Tasks.Task UpdateDelete_UsingResourceIdentity_ResultReturnedHttpClient(DecompressionMethods method)
        {
            using var client = new FhirClient(_endpoint, new FhirClientSettings {  RequestBodyCompressionMethod = method });

            var patId = "async-test-patient." + FhirClientTests.FhirReleaseString;
            var pat = new Patient()
            {
                Name = new List<HumanName>()
                {
                    new HumanName()
                    {
                        Given = new List<string>() { "test_given" },
                        Family = "test_family",
                    }
                },
                Id = patId
            };

            // Create the patient
            Console.WriteLine("Creating patient...");
            Patient p = await client.UpdateAsync(pat);
            Assert.IsNotNull(p);

            // Refresh the patient
            Console.WriteLine("Refreshing patient...");
            await client.RefreshAsync(p);

            // Delete the patient
            Console.WriteLine("Deleting patient...");
            await client.DeleteAsync(p);

            Console.WriteLine("Reading patient...");
            var act = () => client.ReadAsync<Patient>(ResourceIdentity.Build("Patient", patId));

            // VERIFY //
            await act.Should().ThrowAsync<FhirOperationException>();
        }
    }
}