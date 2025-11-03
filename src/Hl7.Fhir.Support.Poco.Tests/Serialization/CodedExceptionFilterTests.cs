using FluentAssertions;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;
using ERR = Hl7.Fhir.Serialization.FhirJsonException;

namespace Hl7.Fhir.Support.Poco.Tests;

[TestClass]
public class CodedExceptionFilterTests
{
    [TestMethod]
    [DataRow(CodedValidationException.INCORRECT_CARDINALITY_MAX_CODE, true)]
    [DataRow(CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE, true)]
    [DataRow(CodedValidationException.INVALID_BASE64_VALUE_CODE, false)]
    [DataRow(CodedValidationException.UNKNOWN_ELEMENT_CODE, false)]
    public void NoOverflowFilterDetectsOverflowIssue(string code, bool shouldBeFiltered)
    {
        CodedValidationException test = new(code, "test message");
        CodedExceptionFilters.FilterNoOverflowIssues(test).Should().Be(shouldBeFiltered);
    }

    [TestMethod]
    public void NoOverflowFilterDetectsFatalIssue()
    {
        Utf8JsonReader reader = new();
        var fatal = ERR.DUPLICATE_PROPERTY(ref reader, "test", "test");

        CodedExceptionFilters.FilterNoOverflowIssues(fatal).Should().Be(false);
    }
}