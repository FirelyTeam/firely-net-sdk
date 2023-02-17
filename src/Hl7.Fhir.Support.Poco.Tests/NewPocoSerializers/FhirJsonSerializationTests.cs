using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Hl7.Fhir.Support.Poco.Tests
{
    [TestClass]
    public class FhirJsonSerializationTests
    {
        public JsonSerializerOptions BaseOptions = new JsonSerializerOptions().ForFhir(typeof(TestPatient).Assembly);

        private (TestPatient, string) getEdgecases()
        {
            var filename = Path.Combine("TestData", "json-edge-cases.json");
            var expected = File.ReadAllText(filename);

            try
            {
                var parsed = JsonSerializer.Deserialize<TestPatient>(expected, BaseOptions);
                return (parsed, expected);
            }
            catch (DeserializationFailedException dfe)
            {
                if (dfe.Exceptions.All(e => e.ErrorCode == CodedValidationException.CONTAINED_RESOURCE_CANNOT_HAVE_NARRATIVE_CODE))
                    return (dfe.PartialResult as TestPatient, expected);
                else
                    throw;
            }

        }

        [TestMethod]
        public void RoundtripEdgeCases()
        {
            var (poco, expected) = getEdgecases();

            var options = BaseOptions.Pretty();
            string actual = JsonSerializer.Serialize(poco, options);

            var errors = new List<string>();
            JsonAssert.AreSame("edgecases", expected, actual, errors);
            Assert.AreEqual(0, errors.Count, "Errors were encountered comparing converted content");
        }

        [TestMethod]
        public void PrintsPretty()
        {
            var (poco, _) = getEdgecases();

            var optionsCompact = new JsonSerializerOptions().ForFhir(typeof(TestPatient).Assembly);
            string compact = JsonSerializer.Serialize(poco, optionsCompact);
            var compactWS = compact.Where(c => char.IsWhiteSpace(c)).Count();

            var optionsPretty = new JsonSerializerOptions().ForFhir(typeof(TestPatient).Assembly).Pretty();
            string pretty = JsonSerializer.Serialize(poco, optionsPretty);
            var prettyWS = pretty.Where(c => char.IsWhiteSpace(c)).Count();

            // much more whitespace, in fact...
            Assert.IsTrue(prettyWS > compactWS * 2);
        }

        [TestMethod]
        public void SerializesInvalidData()
        {
            var options = new JsonSerializerOptions().ForFhir(typeof(TestPatient).Assembly);

            FhirBoolean b = new() { ObjectValue = "treu" };
            var jdoc = JsonDocument.Parse(JsonSerializer.Serialize(b, options));
            Assert.AreEqual("treu", jdoc.RootElement.GetProperty("value").GetString());

            TestPatient p = new() { Contact = new() { new TestPatient.ContactComponent() } };
            jdoc = JsonDocument.Parse(JsonSerializer.Serialize(p, options));
            var contactArray = jdoc.RootElement.GetProperty("contact");
            contactArray.GetArrayLength().Should().Be(1);
            contactArray[0].EnumerateObject().Should().BeEmpty();
        }
    }

}
