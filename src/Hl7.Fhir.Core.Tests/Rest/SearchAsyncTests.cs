using System;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Task = System.Threading.Tasks.Task;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestClass]
    public class SearchAsyncTests
    {
        private string _endpoint = "https://api.hspconsortium.org/rpineda/open";

        [TestMethod]
        [TestCategory("IntegrationTest")]
        public async Task Search_UsingSearchParams_SearchReturned()
        {
            var client = new FhirClient(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                ReturnFullResource = true
            };

            var srch = new SearchParams()
                .Where("name=Daniel")
                .LimitTo(10)
                .SummaryOnly()
                .OrderBy("birthdate",
                    SortOrder.Descending);

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
        public void SearchSync_UsingSearchParams_SearchReturned()
        {
            var client = new FhirClient(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                ReturnFullResource = true
            };

            var srch = new SearchParams()
                .Where("name=Daniel")
                .LimitTo(10)
                .SummaryOnly()
                .OrderBy("birthdate",
                    SortOrder.Descending);

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
        public async Task SearchMultiple_UsingSearchParams_SearchReturned()
        {
            var client = new FhirClient(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                ReturnFullResource = true
            };

            var srchParams = new SearchParams()
                .Where("name=Daniel")
                .LimitTo(10)
                .SummaryOnly()
                .OrderBy("birthdate",
                    SortOrder.Descending);
            
            var task1 = client.SearchAsync<Patient>(srchParams);
            var task2 = client.SearchAsync<Patient>(srchParams);
            var task3 = client.SearchAsync<Patient>(srchParams);

            await Task.WhenAll(task1, task2, task3);
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
        public async Task SearchWithCriteria_SyncContinue_SearchReturned()
        {
            var client = new FhirClient(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                ReturnFullResource = true
            };

            var result1 = await client.SearchAsync<Patient>(new []{"family=clark"});

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
        public async Task SearchWithCriteria_AsyncContinue_SearchReturned()
        {
            var client = new FhirClient(_endpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                ReturnFullResource = true
            };

            var result1 = await client.SearchAsync<Patient>(new[] { "family=clark" },null,1);

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
}
