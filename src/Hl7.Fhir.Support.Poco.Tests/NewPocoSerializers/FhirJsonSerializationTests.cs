using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
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
            Assert.AreEqual(0, errors.Count, "Errors were encountered comparing converted content");
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
            Assert.IsTrue(prettyWS > compactWS * 2);
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
    }

    
}