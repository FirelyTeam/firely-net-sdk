using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Support.Poco.Tests;

#nullable enable

[TestClass]
public class OverflowErrorTests
{
    [TestMethod]
    public void SettingInvalidElementValue_Should_ThrowOnAccess()
    {
        TestOnPrimitiveElement(new Patient(), COVE.EXPECTED_PRIMITIVE_NOT_OBJECT_CODE);
        TestOnPrimitiveElement(new Integer(10), COVE.TYPE_MISMATCH_CODE);
        TestOnPrimitiveElement(new List<Patient>(), COVE.EXPECTED_PRIMITIVE_NOT_ARRAY_CODE);
        TestOnPrimitiveElement(new FhirBoolean(true), null);
        TestOnArrayElement(new List<Patient>(), COVE.TYPE_MISMATCH_CODE);
        TestOnArrayElement(new Patient(), COVE.EXPECTED_ARRAY_NOT_OBJECT_CODE);
        TestOnArrayElement(new FhirBoolean(true), COVE.EXPECTED_ARRAY_NOT_PRIMITIVE_CODE);
        TestOnArrayElement(new List<HumanName>(), null);
        TestOnObjectElement(new FhirBoolean(false), COVE.EXPECTED_OBJECT_NOT_PRIMITIVE_CODE);
        TestOnObjectElement(new List<HumanName>(), COVE.EXPECTED_OBJECT_NOT_ARRAY_CODE);
        TestOnObjectElement(new Patient(), COVE.TYPE_MISMATCH_CODE);
        TestOnObjectElement(new Narrative("<div> this div is not centered </div>"), null);
    }

    private static void TestOnPrimitiveElement(object value, string? coveCode)
    {
        var pat = new Patient();
        pat.SetValue("active", value);

        var act = () => pat.ActiveElement;
        if (coveCode is null)
            act.Should().NotThrow();
        else 
            act.Should().Throw<COVE>().Which.ErrorCode.Should().Be(coveCode);
    }

    private static void TestOnArrayElement(object value, string? coveCode)
    {
        var pat = new Patient();
        pat.SetValue("name", value);

        var act = () => pat.Name;
        if (coveCode is null)
            act.Should().NotThrow();
        else 
            act.Should().Throw<COVE>().Which.ErrorCode.Should().Be(coveCode);
    }

    private static void TestOnObjectElement(object value, string? coveCode)
    {
        var pat = new Patient();
        pat.SetValue("text", value);

        var act = () => pat.Text;
        if (coveCode is null)
            act.Should().NotThrow();
        else 
            act.Should().Throw<COVE>().Which.ErrorCode.Should().Be(coveCode);
    }
}