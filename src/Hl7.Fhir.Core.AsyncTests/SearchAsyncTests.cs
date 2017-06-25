using System;
using System.Linq;
using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using NUnit.Framework;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestFixture]
    public class SearchAsyncTests
    {
        private string _endpoint = "https://api.hspconsortium.org/rpineda/open";
        [Test]
        public async Task Search_UsingSearchParams_SearchReturned()
        {
            var client = new FhirClient(_endpoint);
            client.PreferredFormat = ResourceFormat.Json;
            client.ReturnFullResource = true;

            var srch = new SearchParams()
                .Where("name=Daniel")
                .LimitTo(10)
                .SummaryOnly()
                .OrderBy("birthdate",
                    SortOrder.Descending);

            var result1 = await client.SearchAsync<Patient>(srch);
            result1.Entry.Count.Should().BeGreaterOrEqualTo(1);
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
        [Test]
        public async Task SearchSync_UsingSearchParams_SearchReturned()
        {
            var client = new FhirClient(_endpoint);
            client.PreferredFormat = ResourceFormat.Json;
            client.ReturnFullResource = true;

            var srch = new SearchParams()
                .Where("name=Daniel")
                .LimitTo(10)
                .SummaryOnly()
                .OrderBy("birthdate",
                    SortOrder.Descending);

            var result1 = client.Search<Patient>(srch);
            result1.Entry.Count.Should().BeGreaterOrEqualTo(1);
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
        [Test]
        public async Task SearchMultiple_UsingSearchParams_SearchReturned()
        {
            var client = new FhirClient(_endpoint);
            client.PreferredFormat = ResourceFormat.Json;
            client.ReturnFullResource = true;

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

            result1.Entry.Count.Should().BeGreaterOrEqualTo(1);
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
        [Test]
        public async Task SearchWithCriteria_SyncContinue_SearchReturned()
        {
            var client = new FhirClient(_endpoint);
            client.PreferredFormat = ResourceFormat.Json;
            client.ReturnFullResource = true;
            
            var result1 = await client.SearchAsync<Patient>(new []{"family=clark"});

            result1.Entry.Count.Should().BeGreaterThan(1);

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
        [Test]
        public async Task SearchWithCriteria_AsyncContinue_SearchReturned()
        {
            var client = new FhirClient(_endpoint);
            client.PreferredFormat = ResourceFormat.Json;
            client.ReturnFullResource = true;

            var result1 = await client.SearchAsync<Patient>(new[] { "family=clark" },null,1);

            result1.Entry.Count.Should().Be(1);// only one page one result

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
