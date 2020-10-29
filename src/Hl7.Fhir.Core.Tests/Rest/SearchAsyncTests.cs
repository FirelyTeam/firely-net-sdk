using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Rest.Legacy;
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
        private readonly string _endpointSupportingSearchUsingPost = "http://localhost:4080/";

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task Search_UsingSearchParams_SearchReturnedWebClient()
        {
            var client = new LegacyFhirClient(_endpoint);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;

            await searchUsingParam(client);
        }

        public async Task Search_UsingSearchParams_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
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
            Assert.IsTrue(result1.Entry.Count >= 1);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = client.Continue(result1, PageDirection.Next);
            }

            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchUsingPost_UsingSearchParams_SearchReturned()
        {
            var client = new LegacyFhirClient(_endpointSupportingSearchUsingPost);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;

            await searchUsingPost(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchUsingPost_UsingSearchParams_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
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
            Assert.IsTrue(result1.Entry.Count >= 1);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = client.Continue(result1, PageDirection.Next);
            }

            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void SearchSync_UsingSearchParams_SearchReturnedWebClient()
        {
            var client = new LegacyFhirClient(_endpoint);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;

            searchSync(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void SearchSync_UsingSearchParams_SearchReturnedHttpCLient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
                searchSync(client);
            }
        }

        private static void searchSync(BaseFhirClient client)
        {
            var srch = new SearchParams()
                .Where("name=Donald")
                .LimitTo(10)
                .SummaryOnly();

            var result1 = client.Search<Patient>(srch);

            Assert.IsTrue(result1.Entry.Count >= 1);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = client.Continue(result1, PageDirection.Next);
            }

            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public void SearchUsingPostSync_UsingSearchParams_SearchReturnedHttpClient()
        {
            var client = new LegacyFhirClient(_endpointSupportingSearchUsingPost);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;

            var srch = new SearchParams()
                .Where("name=Donald")
                .LimitTo(10)
                .SummaryOnly();

            var result1 = client.SearchUsingPost<Patient>(srch);

            Assert.IsTrue(result1.Entry.Count >= 1);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = client.Continue(result1, PageDirection.Next);
            }

            Console.WriteLine("Test Completed");

        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchMultiple_UsingSearchParams_SearchReturnedWebClient()
        {
            var client = new LegacyFhirClient(_endpoint);

            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;

            await searchMultiple(client);
        }

        public async Task SearchMultiple_UsingSearchParams_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
                await searchMultiple(client);
            }
        }

        private static async Task searchMultiple(BaseFhirClient client)
        {
            var srchParams = new SearchParams()
                .Where("name=Donald")
                .LimitTo(10)
                .SummaryOnly()
                .OrderBy("birthdate",
                    SortOrder.Descending);

            var task1 = client.SearchAsync<Patient>(srchParams);
            var task2 = client.SearchAsync<Patient>(srchParams);
            var task3 = client.SearchAsync<Patient>(srchParams);

            await task1;
            await task2;
            await task3;

            var result1 = task1.Result;

            Assert.IsTrue(result1.Entry.Count >= 1);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = client.Continue(result1, PageDirection.Next);
            }

            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchUsingPostMultiple_UsingSearchParams_SearchReturned()
        {
            var client = new LegacyFhirClient(_endpointSupportingSearchUsingPost);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;

            await searchMultipleUsingPost(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchUsingPostMultiple_UsingSearchParams_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
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

            Assert.IsTrue(result1.Entry.Count >= 1);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = client.Continue(result1, PageDirection.Next);
            }

            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchWithCriteria_SyncContinue_SearchReturnedWebClient()
        {
            var client = new LegacyFhirClient(_endpoint);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;


            await searchWithCriteria(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchWithCriteria_SyncContinue_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
                await searchWithCriteria(client);
            }
        }

        private static async Task searchWithCriteria(BaseFhirClient client)
        {
            var result1 = await client.SearchAsync<Patient>(new[] { "family=Donald" });

            Assert.IsTrue(result1.Entry.Count >= 1);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = client.Continue(result1, PageDirection.Next);
            }

            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchUsingPostWithCriteria_SyncContinue_SearchReturned()
        {
            var client = new LegacyFhirClient(_endpointSupportingSearchUsingPost);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;

            await searchUsingPostWithCriteria(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchUsingPostWithCriteria_SyncContinue_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
                await searchUsingPostWithCriteria(client);
            }
        }


        private static async Task searchUsingPostWithCriteria(BaseFhirClient client)
        {
            var result1 = await client.SearchUsingPostAsync<Patient>(new[] { "family=Donald" }, pageSize: 5);

            Assert.IsTrue(result1.Entry.Count >= 1);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                {
                    Patient p = (Patient)e.Resource;
                    Console.WriteLine(
                        $"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
                }
                result1 = client.Continue(result1, PageDirection.Next);
            }

            Console.WriteLine("Test Completed");
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchWithCriteria_AsyncContinue_SearchReturnedWebClient()
        {
            var client = new LegacyFhirClient(_endpoint);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;

            await searchWithCriteriaAsynContinue(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchWithCriteria_AsyncContinue_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
                await searchWithCriteriaAsynContinue(client);
            }


        }

        private static async Task searchWithCriteriaAsynContinue(BaseFhirClient client)
        {
            var result1 = await client.SearchAsync<Patient>(new[] { "family=Donald" }, null, 1);

            Assert.IsTrue(result1.Entry.Count >= 1);

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
        public async Task SearchUsingPostWithCriteria_AsyncContinue_SearchReturned()
        {
            var client = new LegacyFhirClient(_endpointSupportingSearchUsingPost);
            client.Settings.PreferredFormat = ResourceFormat.Json;
            client.Settings.PreferredReturn = Prefer.ReturnRepresentation;

            await searchUsingPostAsyncContinue(client);
        }

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task SearchUsingPostWithCriteria_AsyncContinue_SearchReturnedHttpClient()
        {
            using (var client = new FhirClient(_endpoint))
            {
                client.Settings.PreferredFormat = ResourceFormat.Json;
                client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
                await searchUsingPostAsyncContinue(client);
            }
        }


        private static async Task searchUsingPostAsyncContinue(BaseFhirClient client)
        {
            var result1 = await client.SearchAsync<Patient>(new[] { "family=Donald" }, null, 1);

            Assert.IsTrue(result1.Entry.Count >= 1);

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
#pragma warning restore CS0618 // Type or member is obsolete
}
