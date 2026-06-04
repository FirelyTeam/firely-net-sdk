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
                .ForFhir(ModelInfo.ModelInspector, new FhirJsonConverterOptions
                { 
                    SummaryFilterFactory = SerializationFilter.ForElementsFactory(["id", "active"])
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
        public void AllFactoryMethods_ShouldCreateFreshInstancesPerCall()
        {
            // Verify that each factory method creates a new instance per call
            // (this ensures no state is shared between serialization operations)
            var summaryFactory = SerializationFilter.ForSummary;
            var textFactory = SerializationFilter.ForText;
            var countFactory = SerializationFilter.ForCount;
            var dataFactory = SerializationFilter.ForData;
            var elementsFactory = SerializationFilter.ForElementsFactory(["id", "name"]);

            // Each call should return a different instance
            summaryFactory().Should().NotBeSameAs(summaryFactory());
            textFactory().Should().NotBeSameAs(textFactory());
            countFactory().Should().NotBeSameAs(countFactory());
            dataFactory().Should().NotBeSameAs(dataFactory());
            elementsFactory().Should().NotBeSameAs(elementsFactory());
        }
    }
}