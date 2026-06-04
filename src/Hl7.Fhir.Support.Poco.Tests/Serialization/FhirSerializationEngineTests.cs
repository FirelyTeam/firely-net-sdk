#nullable enable

using FluentAssertions;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.CompilerServices;

namespace Hl7.Fhir.Support.Poco.Tests;

[TestClass]
public class FhirSerializationEngineTests
{
    private static readonly ModelInspector TESTINSPECTOR = ModelInfo.ModelInspector;

    // Shared test data for EM+Poco
    private const string CORRECTXML = """<Patient xmlns="http://hl7.org/fhir"><active value="true"  /></Patient>""";
    private const string CORRECTJSON = """{ "resourceType": "Patient",  "active": true }""";
    private const string BWCOMPATIBLEXML = """<Patient xmlns="http://hl7.org/fhir"><activex value="false" /></Patient>""";
    private const string BWCOMPATIBLEJSON = """{ "resourceType": "Patient",  "activex": "true" }""";

    // Test data for EM
    private const string PERMISSIVEXML = """<Patient xmlns="http://hl7.org/fhir"><gender value=""  /></Patient>""";
    private const string PERMISSIVEJSON = """{ "resourceType": "Patient",  "gender": "" }""";
    private const string EM_WRONGJSON = """{ "resourceType": "Patient", "deceasedImaginary": "2i" }""";
    private const string EM_WRONGXML = """<Patient xmlns="http://hl7.org/fhir"><deceasedImaginary value="1i" /></Patient>""";
    private const string EM_UNKNOWN_ELEMENT = "*Encountered unknown element 'activex'*";
    private const string EM_INCORRECT_CHOICE = "*Choice element 'deceasedImaginary' is suffixed with unexpected type 'Imaginary'*";
    private const string EM_EMPTY_VALUE = "*'gender' has an empty*value*";

    [TestMethod]
    [DataRow(CORRECTXML, null, null, null, DisplayName = "Correct XML")]
    [DataRow(PERMISSIVEXML, null, EM_EMPTY_VALUE, EM_EMPTY_VALUE, DisplayName = "Permissive XML")]
    [DataRow(BWCOMPATIBLEXML, EM_UNKNOWN_ELEMENT, EM_UNKNOWN_ELEMENT, null, DisplayName = "Backwards-compatible XML")]
    [DataRow(EM_WRONGXML, EM_INCORRECT_CHOICE, EM_INCORRECT_CHOICE, EM_INCORRECT_CHOICE, DisplayName = "Wrong XML")]
    [DataRow(CORRECTJSON, null, null, null, DisplayName = "Correct JSON")]
    [DataRow(PERMISSIVEJSON, null, EM_EMPTY_VALUE, EM_EMPTY_VALUE, DisplayName = "Permissive JSON")]
    [DataRow(BWCOMPATIBLEJSON, EM_UNKNOWN_ELEMENT, EM_UNKNOWN_ELEMENT, null, DisplayName = "Backwards-compatible JSON")]
    [DataRow(EM_WRONGJSON, EM_INCORRECT_CHOICE, EM_INCORRECT_CHOICE, EM_INCORRECT_CHOICE, DisplayName = "Wrong JSON")]
    public void TestParsingEM(string data, string? permissive, string? strict, string? bw)
    {
        test(data, FhirSerializationEngineFactory.Legacy.Permissive(TESTINSPECTOR), permissive);
        test(data, FhirSerializationEngineFactory.Legacy.Strict(TESTINSPECTOR), strict);
        test(data, FhirSerializationEngineFactory.Legacy.BackwardsCompatible(TESTINSPECTOR), bw);
        test(data, FhirSerializationEngineFactory.Legacy.Ostrich(TESTINSPECTOR), null);  // there should never be an error ;-)
    }

    private const string RECOVERABLEXML = """<Patient xmlns="http://hl7.org/fhir"><gender value=""  /></Patient>""";
    private const string RECOVERABLEJSON = """{ "resourceType": "Patient",  "gender": "" }""";

    private const string POCO_UNKNOWN_ELEMENT = "*Found unknown * 'activex'*";
    private const string POCO_DUPLICATE_PROP = "*Encountered duplicate property 'propA'*";
    private const string POCO_CONTAINED_WITH_ATTR = "*Encountered unexpected attribute*";
    private const string POCO_EMPTY_VALUE = "*'' is not a correct literal for a code*";
    private const string POCO_UNRECOVERABLEXML = """<Patient xmlns="http://hl7.org/fhir"><contained value="1i" /></Patient>""";
    private const string POCO_UNRECOVERABLEJSON = """{ "propA": "hi!", "propA": "there" }""";

    [TestMethod]
    [DataRow(CORRECTXML, null, null, null, null, DisplayName = "Correct XML")]
    [DataRow(RECOVERABLEXML, null, POCO_EMPTY_VALUE, POCO_EMPTY_VALUE, null, DisplayName = "Recoverable XML")]
    [DataRow(BWCOMPATIBLEXML, null, POCO_UNKNOWN_ELEMENT, null, null, DisplayName = "Backwards-compatible XML")]
    [DataRow(POCO_UNRECOVERABLEXML, POCO_CONTAINED_WITH_ATTR, POCO_CONTAINED_WITH_ATTR, POCO_CONTAINED_WITH_ATTR, POCO_CONTAINED_WITH_ATTR, DisplayName = "Unrecoverable XML")]
    [DataRow(CORRECTJSON, null, null, null, null, DisplayName = "Correct JSON")]
    [DataRow(RECOVERABLEJSON, null, POCO_EMPTY_VALUE, POCO_EMPTY_VALUE, null, DisplayName = "Recoverable JSON")]
    [DataRow(BWCOMPATIBLEJSON, null, POCO_UNKNOWN_ELEMENT, null, null, DisplayName = "Backwards-compatible JSON")]
    [DataRow(POCO_UNRECOVERABLEJSON, POCO_DUPLICATE_PROP, POCO_DUPLICATE_PROP, POCO_DUPLICATE_PROP, POCO_DUPLICATE_PROP, DisplayName = "Unrecoverable JSON")]
    public void TestParsingPoco(string data, string? recoverable, string? strict, string? bw, string? syntax)
    {
        test(data, FhirSerializationEngineFactory.Recoverable(TESTINSPECTOR), recoverable);
        test(data, FhirSerializationEngineFactory.Strict(TESTINSPECTOR), strict);
        test(data, FhirSerializationEngineFactory.BackwardsCompatible(TESTINSPECTOR), bw);
        test(data, FhirSerializationEngineFactory.Ostrich(TESTINSPECTOR), null);  // there should never be an error ;-)
        test(data, FhirSerializationEngineFactory.SyntaxOnly(TESTINSPECTOR), syntax);  // there should never be an error ;-)
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