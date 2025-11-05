#pragma warning disable CS0618 // Type or member is obsolete

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Tests.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestClass]
    public class FhirClientSearchAsyncTests
    {
        private static string _endpoint = FhirClientTests.testEndpoint.OriginalString;

        public async Task Search_UsingSearchParams_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.ReturnPreference = ReturnPreference.Representation;
                await searchUsingParam(client);
            }
        }

        private static async Task searchUsingParam(BaseFhirClient client)
        {
            var srch = new SearchParams()
                .Where("name=Donald")
                .LimitTo(10)
                .SummaryOnly();

            var result1 = await client.SearchAsync<Patient>(srch);
            Assert.IsGreaterThanOrEqualTo(1, result1.Entry.Count);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = await client.ContinueAsync(result1, PageDirection.Next);
            }

            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchUsingPost_UsingSearchParams_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.ReturnPreference = ReturnPreference.Representation;
                await searchUsingPost(client);
            }
        }

        private static async Task searchUsingPost(BaseFhirClient client)
        {
            var srch = new SearchParams()
                            .Where("name=Donald")
                            .LimitTo(5)
                            .SummaryOnly();

            var result1 = await client.SearchUsingPostAsync<Patient>(srch);
            Assert.IsGreaterThanOrEqualTo(1, result1.Entry.Count);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = await client.ContinueAsync(result1, PageDirection.Next);
            }

            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void SearchSync_UsingSearchParams_SearchReturnedHttpCLient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.ReturnPreference = ReturnPreference.Representation;
                searchSync(client);
            }
        }

        private static void searchSync(BaseFhirClient client)
        {
            var srch = new SearchParams()
                .Where("name=Donald")
                .LimitTo(10)
                .SummaryOnly();

            var result1 = client.SearchAsync<Patient>(srch).WaitResult();

            Assert.IsGreaterThanOrEqualTo(1, result1.Entry.Count);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = client.ContinueAsync(result1, PageDirection.Next).WaitResult();
            }

            Console.WriteLine("Test Completed");
        }

        public async Task SearchMultiple_UsingSearchParams_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.ReturnPreference = ReturnPreference.Representation;
                await searchMultiple(client);
            }
        }

        private static async Task searchMultiple(BaseFhirClient client)
        {
            var srchParams = new SearchParams()
                .Where("name=Donald")
                .LimitTo(10)
                .SummaryOnly();

            var task1 = client.SearchAsync<Patient>(srchParams);
            var task2 = client.SearchAsync<Patient>(srchParams);
            var task3 = client.SearchAsync<Patient>(srchParams);

            await task1;
            await task2;
            await task3;

            var result1 = task1.Result;

            Assert.IsGreaterThanOrEqualTo(1, result1.Entry.Count);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = await client.ContinueAsync(result1, PageDirection.Next);
            }

            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchUsingPostMultiple_UsingSearchParams_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.ReturnPreference = ReturnPreference.Representation;
                await searchMultipleUsingPost(client);
            }
        }

        private static async Task searchMultipleUsingPost(BaseFhirClient client)
        {
            var srchParams = new SearchParams()
                .Where("name=Donald")
                .LimitTo(10)
                .SummaryOnly();

            var task1 = client.SearchUsingPostAsync<Patient>(srchParams);
            var task2 = client.SearchUsingPostAsync<Patient>(srchParams);
            var task3 = client.SearchUsingPostAsync<Patient>(srchParams);

            await task1;
            await task2;
            await task3;

            var result1 = task1.Result;

            Assert.IsGreaterThanOrEqualTo(1, result1.Entry.Count);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = await client.ContinueAsync(result1, PageDirection.Next);
            }

            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchWithCriteria_AsyncContinue_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.ReturnPreference = ReturnPreference.Representation;
                await searchWithCriteriaAsyncContinue(client);
            }
        }

        private static async Task searchWithCriteriaAsyncContinue(BaseFhirClient client)
        {
            var result1 = await client.SearchAsync<Patient>(new[] { "family=Donald" }, null, 1);

            Assert.IsGreaterThanOrEqualTo(1, result1.Entry.Count);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                Console.WriteLine("Fetching more results...");
                result1 = await client.ContinueAsync(result1);
            }

            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchUsingPostWithCriteria_AsyncContinue_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.ReturnPreference = ReturnPreference.Representation;
                await searchUsingPostAsyncContinue(client);
            }
        }


        private static async Task searchUsingPostAsyncContinue(BaseFhirClient client)
        {
            var result1 = await client.SearchAsync<Patient>(new[] { "family=Donald" }, null, 1);

            Assert.IsGreaterThanOrEqualTo(1, result1.Entry.Count);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                Console.WriteLine("Fetching more results...");
                result1 = await client.ContinueAsync(result1);
            }

            Console.WriteLine("Test Completed");
        }
    }
}

#pragma warning disable CS0618 // Type or member is obsolete