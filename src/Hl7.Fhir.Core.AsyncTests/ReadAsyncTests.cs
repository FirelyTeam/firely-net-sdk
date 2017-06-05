using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using NUnit.Framework;

namespace Hl7.Fhir.Core.AsyncTests
{
    [TestFixture]
    public class ReadAsyncTests
    {
        private string _endpoint = "https://api.hspconsortium.org/rpineda/open";
        [Test]
        public async Task Read_UsingResourceIdentity_ResultReturned()
        {
            var client = new FhirClient(_endpoint);
            client.PreferredFormat = ResourceFormat.Json;
            client.ReturnFullResource = true;

            Patient p = await client.ReadAsync<Patient>(new ResourceIdentity("/Patient/SMART-1288992"));
            p.Should().NotBeNull();
            p.Name[0].Given.Should().NotBeNull();
            p.Name[0].Family.Should().NotBeNull();
            Console.WriteLine($"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
            Console.WriteLine("Test Completed");
        }
        [Test]
        public async Task Read_UsingLocationString_ResultReturned()
        {
            var client = new FhirClient(_endpoint);
            client.PreferredFormat = ResourceFormat.Json;
            client.ReturnFullResource = true;

            Patient p = await client.ReadAsync<Patient>("/Patient/SMART-1288992");
            p.Should().NotBeNull();
            p.Name[0].Given.Should().NotBeNull();
            p.Name[0].Family.Should().NotBeNull();
            Console.WriteLine($"NAME: {p.Name[0].Given.FirstOrDefault()} {p.Name[0].Family.FirstOrDefault()}");
            Console.WriteLine("Test Completed");
        }
    }
}