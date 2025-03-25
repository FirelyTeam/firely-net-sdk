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
        TestOnPrimitiveElement(new Integer(10), COVE.PROPERTY_TYPE_MISMATCH_CODE);
        TestOnPrimitiveElement(new List<Patient>(), COVE.EXPECTED_PRIMITIVE_NOT_ARRAY_CODE);
        TestOnPrimitiveElement(new FhirBoolean(true), null);
        TestOnArrayElement(new List<Patient>(), COVE.PROPERTY_TYPE_MISMATCH_CODE);
        TestOnArrayElement(new Patient(), COVE.EXPECTED_ARRAY_NOT_OBJECT_CODE);
        TestOnArrayElement(new FhirBoolean(true), COVE.EXPECTED_ARRAY_NOT_PRIMITIVE_CODE);
        TestOnArrayElement(new List<HumanName>(), null);
        TestOnObjectElement(new FhirBoolean(false), COVE.EXPECTED_OBJECT_NOT_PRIMITIVE_CODE);
        TestOnObjectElement(new List<HumanName>(), COVE.EXPECTED_OBJECT_NOT_ARRAY_CODE);
        TestOnObjectElement(new Patient(), COVE.PROPERTY_TYPE_MISMATCH_CODE);
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

    [TestMethod]
    public void SettingProperty_Should_ImpactHelperProperty()
    {
        var pat = new Patient();
        pat.Gender = AdministrativeGender.Male;
        pat.GenderElement.Should().BeEquivalentTo(new Code<AdministrativeGender>(AdministrativeGender.Male));

        pat.SetValue("gender", null);
        
        pat.GenderElement.Should().BeNull();
        pat.Gender.Should().BeNull();

        pat.SetValue("gender", new Patient());

        var act = () => pat.GenderElement;
        act.Should().Throw<COVE>().Which.ErrorCode.Should().Be(COVE.EXPECTED_PRIMITIVE_NOT_OBJECT_CODE);
        var act2 = () => pat.Gender;
        act2.Should().Throw<COVE>().Which.ErrorCode.Should().Be(COVE.EXPECTED_PRIMITIVE_NOT_OBJECT_CODE);
    }

    [TestMethod]
    public void SettingCommonProperty_Should_HandleTypesCorrectly()
    {
        var att = new Attachment();
        att.SizeElement = new Integer(5);
        att.SizeElement.Should().BeEquivalentTo(new Integer(5));
        att.SizeElement = new Integer64(5);
        att.SizeElement.Should().BeEquivalentTo(new Integer64(5));
        // att.SizeElement = new FhirString("5");
        // var act = () => att.SizeElement;
        // act.Should().Throw<COVE>().Which.ErrorCode.Should().Be(COVE.PROPERTY_TYPE_MISMATCH_CODE);
    }
}