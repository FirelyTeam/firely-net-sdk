using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Hl7.Fhir.Support.Poco.Tests
{
    [TestClass]
    public class FhirJsonSerializationTests
    {
        public JsonSerializerOptions BaseOptions = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly);

        private (Patient, string) getEdgecases()
        {
            var filename = Path.Combine("TestData", "json-edge-cases.json");
            var expected = File.ReadAllText(filename);

            try
            {
                var parsed = JsonSerializer.Deserialize<Patient>(expected, BaseOptions);
                return (parsed, expected);
            }
            catch (DeserializationFailedException dfe)
            {
                if (dfe.Exceptions.All(e => e.ErrorCode == CodedValidationException.CONTAINED_RESOURCE_CANNOT_HAVE_NARRATIVE_CODE))
                    return (dfe.PartialResult as Patient, expected);
                else
                    throw;
            }

        }

        [TestMethod]
        public void RoundtripEdgeCases()
        {
            var (poco, expected) = getEdgecases();

            var options = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly).Pretty();

            string actual = JsonSerializer.Serialize(poco, options);

            var errors = new List<string>();
            JsonAssert.AreSame("edgecases", expected, actual, errors);
            Assert.IsEmpty(errors, "Errors were encountered comparing converted content");
        }

        [TestMethod]
        public void PrintsPretty()
        {
            var (poco, _) = getEdgecases();

            var optionsCompact = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly);
            string compact = JsonSerializer.Serialize(poco, optionsCompact);
            var compactWS = compact.Where(c => char.IsWhiteSpace(c)).Count();

            var optionsPretty = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly).Pretty();
            string pretty = JsonSerializer.Serialize(poco, optionsPretty);
            var prettyWS = pretty.Where(c => char.IsWhiteSpace(c)).Count();

            // much more whitespace, in fact...
            Assert.IsGreaterThan(compactWS * 2, prettyWS);
        }

        [TestMethod]
        public void SerializesInvalidData()
        {
            var options = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly);

            FhirBoolean b = new() { ObjectValue = "treu" };
            var jdoc = JsonDocument.Parse(JsonSerializer.Serialize(b, options));
            Assert.AreEqual("treu", jdoc.RootElement.GetProperty("value").GetString());

            Patient p = new() { Contact = new() { new Patient.ContactComponent() } };
            jdoc = JsonDocument.Parse(JsonSerializer.Serialize(p, options));
            var contactArray = jdoc.RootElement.GetProperty("contact");
            contactArray.GetArrayLength().Should().Be(1);
            contactArray[0].EnumerateObject().Should().BeEmpty();
        }

        /// <summary>
        /// Regression test: types derived from FHIR POCOs (e.g. ValidateCodeParameters which derives
        /// from Parameters) must be serializable even though they do not carry a [FhirType] attribute
        /// themselves. See https://github.com/FirelyTeam/firely-net-sdk/issues/3458
        /// </summary>
        [TestMethod]
        public void CanSerializeDerivedParametersType()
        {
            var options = new JsonSerializerOptions().ForFhir();

            var parameters = new ValidateCodeParameters
            {
                Code = new Code("active"),
            };

            // Must not throw
            var json = JsonSerializer.Serialize(parameters, options);
            json.Should().Contain("\"resourceType\":\"Parameters\"");
            json.Should().Contain("\"active\"");
        }

        /// <summary>
        /// Regression test: a round-trip through JSON must produce an equivalent Parameters instance.
        /// </summary>
        [TestMethod]
        public void RoundtripDerivedParametersType()
        {
            var options = new JsonSerializerOptions().ForFhir();

            var parameters = new ValidateCodeParameters
            {
                Code = new Code("active"),
                System = new FhirUri("http://hl7.org/fhir/ValueSet/example"),
            };

            var json = JsonSerializer.Serialize(parameters, options);

            // Deserializes back to Parameters (the registered FHIR type)
            var deserialized = JsonSerializer.Deserialize<Parameters>(json, options);
            deserialized.Should().NotBeNull();

            var vcParams = new ValidateCodeParameters(deserialized!);
            vcParams.Code?.Value.Should().Be("active");
            vcParams.System?.Value.Should().Be("http://hl7.org/fhir/ValueSet/example");
        }
    }

    
}