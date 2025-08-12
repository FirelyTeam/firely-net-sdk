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

    private static void assertValid(Base o, string? errorCode = null)
    {
        var validationResult = o.Validate();
        if (errorCode is null)
            validationResult.Should().BeEmpty();
        else
            validationResult.Should().ContainSingle(vr => vr.ErrorCode == errorCode);
    }
}