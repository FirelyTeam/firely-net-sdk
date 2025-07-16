using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hl7.Fhir.Support.Poco.Tests
{
    [TestClass]
    public class SummaryFilterThreadSafetyTests
    {
        [TestMethod]
        public void ConcurrentSerializationWithFactory_ShouldBeThreadSafe()
        {
            // Arrange
            var options = new JsonSerializerOptions()
                .ForFhir(typeof(Patient).Assembly, new FhirJsonPocoSerializerSettings 
                { 
                    SummaryFilterFactory = SerializationFilter.CreateElementsFactory(["id", "active"]) 
                })
                .Pretty();

            var patient = new Patient
            {
                Id = "123",
                Active = true,
                Name = [new() { Family = "Doe", Given = ["John"] }],
                MultipleBirth = new FhirBoolean(false),
            };
            var bundle = new Bundle
            {
                Type = Bundle.BundleType.Collection,
                Entry = [new() { Resource = patient }]
            };

            ConcurrentBag<string> serialized = [];

            // Act
            Parallel.For(0, 100, i =>
            {
                serialized.Add(JsonSerializer.Serialize(bundle, options));
            });

            // Assert
            serialized.Count.Should().Be(100);
            
            // All results should include the entry field
            var resultsWithEntry = serialized.Where(json => json.Contains("\"entry\"")).Count();
            resultsWithEntry.Should().Be(100, "all results should contain the entry field");
            
            // No results should contain unfiltered fields
            var resultsWithUnfilteredFields = serialized.Where(json => 
                json.Contains("\"name\"") || json.Contains("\"multipleBirthBoolean\"")).Count();
            resultsWithUnfilteredFields.Should().Be(0, "no results should contain unfiltered fields");
            
            // All results should contain the filtered fields
            var resultsWithId = serialized.Where(json => json.Contains("\"id\": \"123\"")).Count();
            var resultsWithActive = serialized.Where(json => json.Contains("\"active\": true")).Count();
            resultsWithId.Should().Be(100, "all results should contain the id field");
            resultsWithActive.Should().Be(100, "all results should contain the active field");
        }

        [TestMethod]
        public void ConcurrentSerializationWithLegacyFilter_ShouldShowInconsistentResults()
        {
            // This test documents the issue with the legacy approach
            // and demonstrates why the factory approach is needed
            
            // Arrange
#pragma warning disable CS0618 // Type or member is obsolete
            var options = new JsonSerializerOptions()
                .ForFhir(typeof(Patient).Assembly, new FhirJsonPocoSerializerSettings 
                { 
                    SummaryFilter = SerializationFilter.ForElements(["id", "active"]) 
                })
                .Pretty();
#pragma warning restore CS0618 // Type or member is obsolete

            var patient = new Patient
            {
                Id = "123",
                Active = true,
                Name = [new() { Family = "Doe", Given = ["John"] }],
                MultipleBirth = new FhirBoolean(false),
            };
            var bundle = new Bundle
            {
                Type = Bundle.BundleType.Collection,
                Entry = [new() { Resource = patient }]
            };

            ConcurrentBag<string> serialized = [];

            // Act
            Parallel.For(0, 100, i =>
            {
                serialized.Add(JsonSerializer.Serialize(bundle, options));
            });

            // Assert
            serialized.Count.Should().Be(100);
            
            // With legacy approach, many results will be missing the entry field due to race conditions
            var resultsWithEntry = serialized.Where(json => json.Contains("\"entry\"")).Count();
            resultsWithEntry.Should().BeLessThan(100, "legacy approach should show inconsistent results due to race conditions");
        }

        [TestMethod]
        public void AllFactoryMethods_ShouldCreateNewInstances()
        {
            // Verify that each factory method call creates a new instance
            var summaryFactory = SerializationFilter.CreateSummaryFactory();
            var textFactory = SerializationFilter.CreateTextFactory();
            var countFactory = SerializationFilter.CreateCountFactory();
            var dataFactory = SerializationFilter.CreateDataFactory();
            var elementsFactory = SerializationFilter.CreateElementsFactory(["id", "name"]);

            // Each call should return a different instance
            summaryFactory().Should().NotBeSameAs(summaryFactory());
            textFactory().Should().NotBeSameAs(textFactory());
            countFactory().Should().NotBeSameAs(countFactory());
            dataFactory().Should().NotBeSameAs(dataFactory());
            elementsFactory().Should().NotBeSameAs(elementsFactory());
        }
    }
}