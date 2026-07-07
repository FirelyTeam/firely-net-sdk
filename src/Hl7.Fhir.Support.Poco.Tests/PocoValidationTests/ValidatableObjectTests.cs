using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.Json;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

#nullable enable

namespace Hl7.Fhir.Support.Poco.Tests;

[TestClass]
public class ValidatableObjectTests
{
    [TestMethod]
    public void TestCodeOfT()
    {
        var c = new Code<FilterOperator>(null);
        // assertValid(c); NOT valid, no value or children!
        c.Value.Should().BeNull();

        c = new Code<FilterOperator>(FilterOperator.DescendentOf);
        assertValid(c);
        c.Value.Should().Be(FilterOperator.DescendentOf);

        c.JsonValue = null;
        // assertValid(c); Idem
        c.Value.Should().BeNull();

        c.JsonValue = FilterOperator.ChildOf.GetLiteral();
        assertValid(c);
        c.Value.Should().Be(FilterOperator.ChildOf);

        c.JsonValue = "wrong";
        assertValid(c, errorCode: COVE.INVALID_CODED_VALUE_CODE);
        Assert.ThrowsExactly<COVE>(() => _ = c.Value);

        c.JsonValue = 4;
        assertValid(c, errorCode: COVE.INCORRECT_LITERAL_VALUE_TYPE_CODE);
        Assert.ThrowsExactly<COVE>(() => _ = c.Value);
    }

    [TestMethod]
    public void WrongCasedDictionaryKey_IsReportedByValidation()
    {
        // A wrong-cased element name used with the dictionary interface does not match the POCO
        // property, so the data ends up in the overflow. Validation reports the casing violation.
        var patient = new Patient();
        patient.SetValue("Active", new FhirBoolean(true));

        var errors = patient.Validate();
        errors.Should().Contain(e => e.ErrorCode == COVE.WRONG_CASED_ELEMENT_CODE && e.Message.Contains("'active'"));
    }

    [TestMethod]
    public void WrongCasedChoiceDictionaryKey_IsReportedByValidation()
    {
        // Same for a suffixed choice-element key: it is unknown to the POCO, but validation
        // detects that it only differs from a correct choice name by casing, and reports the
        // casing violation instead of a generic unknown element.
        var patient = new Patient();
        patient.SetValue("DeceasedBoolean", new FhirBoolean(true));

        var errors = patient.Validate();
        errors.Should().Contain(e => e.ErrorCode == COVE.WRONG_CASED_ELEMENT_CODE && e.Message.Contains("'deceasedBoolean'"));
        errors.Should().NotContain(e => e.ErrorCode == COVE.UNKNOWN_ELEMENT_CODE);
    }

    [TestMethod]
    public void CorrectlyCasedDictionaryKey_NoCaseError()
    {
        var patient = new Patient();
        patient.SetValue("active", new FhirBoolean(true));

        var errors = patient.Validate();
        errors.Should().NotContain(e => e.ErrorCode == COVE.WRONG_CASED_ELEMENT_CODE);
    }

    private static void assertValid(Base o, string? errorCode = null)
    {
        var validationResult = o.Validate();
        if (errorCode is null)
            validationResult.Should().BeEmpty();
        else
            validationResult.Should().ContainSingle(vr => vr.ErrorCode == errorCode);
    }
}