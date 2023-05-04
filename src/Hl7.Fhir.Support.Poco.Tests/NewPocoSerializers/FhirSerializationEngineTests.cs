#nullable enable

using FluentAssertions;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Hl7.Fhir.Support.Poco.Tests
{

    [TestClass]
    public class FhirSerializationEngineTests
    {
        private static readonly ModelInspector TESTINSPECTOR = ModelInspector.ForType(typeof(TestPatient));

        private const string CORRECTXML = """<Patient xmlns="http://hl7.org/fhir"><active value="true"  /></Patient>""";
        private const string PERMISSIVEXML = """<Patient xmlns="http://hl7.org/fhir"><gender value=""  /></Patient>""";
        private const string BWCOMPATIBLEXML = """<Patient xmlns="http://hl7.org/fhir"><activex value="false" /></Patient>""";
        private const string WRONGXML = """<Patient xmlns="http://hl7.org/fhir"><multipleBirthString value="crazy" /></Patient>""";
        
        private const string CORRECTJSON = """{ "resourceType": "Patient",  "active": true }""";
        private const string PERMISSIVEJSON = """{ "resourceType": "Patient",  "gender": "" }""";
        private const string BWCOMPATIBLEJSON = """{ "resourceType": "Patient",  "activex": "true" }""";
        private const string WRONGJSON = """{ "resourceType": "Patient", "multipleBirthString": "crazy" }""";

        private const string EM_UNKNOWN_ELEMENT = "*Encountered unknown element 'activex'*";
        private const string EM_INCORRECT_CHOICE = "*suffixed with unexpected type 'string'*";
        private const string EM_EMPTY_VALUE = "*'gender' has an empty*value*";

        [TestMethod]
        [DataRow(CORRECTXML, null, null, null)]
        [DataRow(PERMISSIVEXML, null, EM_EMPTY_VALUE, EM_EMPTY_VALUE)]
        [DataRow(BWCOMPATIBLEXML, EM_UNKNOWN_ELEMENT, EM_UNKNOWN_ELEMENT, null)]
        [DataRow(WRONGXML, EM_INCORRECT_CHOICE, EM_INCORRECT_CHOICE, EM_INCORRECT_CHOICE)]
        [DataRow(CORRECTJSON, null, null, null)]
        [DataRow(PERMISSIVEJSON, null, EM_EMPTY_VALUE, EM_EMPTY_VALUE)]
        [DataRow(BWCOMPATIBLEJSON, EM_UNKNOWN_ELEMENT, EM_UNKNOWN_ELEMENT, null)]
        [DataRow(WRONGJSON, EM_INCORRECT_CHOICE, EM_INCORRECT_CHOICE, EM_INCORRECT_CHOICE)]
        public void TestParsingEM(string data, string? permissive, string? strict, string? bw)
        {
            test(data, FhirSerializationEngineFactory.Legacy.Permissive(TESTINSPECTOR), permissive);
            test(data, FhirSerializationEngineFactory.Legacy.Strict(TESTINSPECTOR), strict);
            test(data, FhirSerializationEngineFactory.Legacy.BackwardsCompatible(TESTINSPECTOR), bw);
            test(data, FhirSerializationEngineFactory.Legacy.Ostrich(TESTINSPECTOR), null);  // there should never be an error ;-)                          
        }

        private const string POCO_UNKNOWN_ELEMENT = "*Encountered unrecognized * 'activex'*";
        private const string POCO_INCORRECT_CHOICE = "*Value is of type 'string', which is not an allowed choice*";
        private const string POCO_EMPTY_VALUE = "* cannot be empty*. Either they are absent*";

        [TestMethod]
        [DataRow(CORRECTXML, null, null, null)]
        [DataRow(PERMISSIVEXML, null, POCO_EMPTY_VALUE, POCO_EMPTY_VALUE)]
        [DataRow(BWCOMPATIBLEXML, POCO_UNKNOWN_ELEMENT, POCO_UNKNOWN_ELEMENT, null)]
        [DataRow(WRONGXML, POCO_INCORRECT_CHOICE, POCO_INCORRECT_CHOICE, POCO_INCORRECT_CHOICE)]
        [DataRow(CORRECTJSON, null, null, null)]
        [DataRow(PERMISSIVEJSON, null, POCO_EMPTY_VALUE, POCO_EMPTY_VALUE)]
        [DataRow(BWCOMPATIBLEJSON, POCO_UNKNOWN_ELEMENT, POCO_UNKNOWN_ELEMENT, null)]
        [DataRow(WRONGJSON, POCO_INCORRECT_CHOICE, POCO_INCORRECT_CHOICE, POCO_INCORRECT_CHOICE)]
        public void TestParsingPoco(string data, string? permissive, string? strict, string? bw)
        {
            test(data, FhirSerializationEngineFactory.Recoverable(TESTINSPECTOR), permissive);
            test(data, FhirSerializationEngineFactory.Strict(TESTINSPECTOR), strict);
            test(data, FhirSerializationEngineFactory.BackwardsCompatible(TESTINSPECTOR), bw);
            test(data, FhirSerializationEngineFactory.Ostrich(TESTINSPECTOR), null);  // there should never be an error ;-)
        }

        private static void test(string data, IFhirSerializationEngine engine, string? error, [CallerArgumentExpression(nameof(engine))] string? engineName = default)
        {
            try
            {
                _ = SerializationUtil.ProbeIsXml(data) ? engine.DeserializeFromXml(data) : engine.DeserializeFromJson(data);

                if (error is not null)
                    Assert.Fail($"Engine {engineName} should have thrown message with {error}");
            }
            catch (DeserializationFailedException dfe)
            {
                error.Should().NotBeNull($"Did not expect exception {dfe.Message} for engine {engineName}.");
                dfe.Message.Should().Match(error, because: $"that should be the message for engine {engineName}");
            }
        }
    }
}

#nullable restore