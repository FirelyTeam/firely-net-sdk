using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using static Hl7.Fhir.Tests.Rest.FhirClientTests;
using FluentAssertions;
using System.Collections.Generic;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestClass]
    public class FhirClientSearchAsyncTests
    {
        private static readonly string ENDPOINT = TestEndpoint.OriginalString;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            CreateItems();
        }

        [TestMethod]
        [TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        [DataRow("GET")]
        [DataRow("ASYNC")]
        [DataRow("POST")]
        public async Task Search_UsingSearchParams_SearchReturnedHttpClient(string method)
        {
            using var client = new FhirClient(ENDPOINT);

            var srch = new SearchParams()
                .Where("name=Donald")
                .LimitTo(10)
                .SummaryOnly();

#pragma warning disable CS0618 // Type or member is obsolete
            var result1 = method switch
            {
                "GET" => client.Search<Patient>(srch),
                "ASYNC" => await client.SearchAsync<Patient>(srch),
                "POST" => await client.SearchUsingPostAsync<Patient>(srch),
                _ => throw new Exception()
            };
#pragma warning restore CS0618 // Type or member is obsolete

            await check(client, result1);
        }

        private static async Task check(FhirClient client, Bundle result1)
        {
            Assert.IsTrue(result1.Entry.Count >= 1);

            while (result1 != null)
            {
                foreach (var e in result1.Entry)
                    e.Resource.Should().BeOfType<Patient>();

                result1 = await client.ContinueAsync(result1, PageDirection.Next);
            }
        }

        [TestMethod]
        [TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async Task SearchMultiple_UsingSearchParams_SearchReturnedHttpClient()
        {
            var client = new FhirClient(ENDPOINT);
            var srchParams = new SearchParams()
                .Where("name=Donald")
                .LimitTo(10)
                .SummaryOnly()
                .OrderBy("birthdate",
                    SortOrder.Descending);

            var tasks = new List<System.Threading.Tasks.Task<Bundle>>();
            for (var i = 0; i < 5; i++)
                tasks.Add(client.SearchAsync<Patient>(srchParams));

            var results = await Task.WhenAll(tasks);
            results.All(r => r is not null).Should().BeTrue();
        }
     
        [TestMethod]
        [TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async Task SearchWithCriteria_SyncContinue_SearchReturnedHttpClient()
        {
            using var client = new FhirClient(ENDPOINT);
           
            var result1 = await client.SearchAsync<Patient>(new[] { "family=Donald" });
            await check(client, result1);
        }

        [TestMethod]
        [Ignore("FS does not like to continue after a search with post - getting GONE")]
        [TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async Task SearchUsingPostWithCriteria_SyncContinue_SearchReturnedHttpClient()
        {
            using var client = new FhirClient(ENDPOINT);                  
            var result1 = await client.SearchUsingPostAsync<Patient>(new[] { "family=Donald" }, pageSize: 5);
            await check(client, result1);
        }

        [TestMethod]
        [TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async Task SearchWithCriteria_AsyncContinue_SearchReturnedHttpClient()
        {
            using var client = new FhirClient(ENDPOINT);          
            var result1 = await client.SearchAsync<Patient>(new[] { "family=Donald" }, null, 5);
            await check(client, result1);
        }

        [TestMethod]
        [Ignore("FS does not like to continue after a search with post - getting GONE")]
        [TestCategory("IntegrationTest"), TestCategory("FhirClient")]
        public async Task SearchUsingPostWithCriteria_AsyncContinue_SearchReturnedHttpClient()
        {
            using var client = new FhirClient(ENDPOINT);           
            var result1 = await client.SearchUsingPostAsync<Patient>(new[] { "family=Donald" }, null, 1);
            await check(client, result1);
        }
    }
}