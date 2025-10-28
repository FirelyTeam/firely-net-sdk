using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Support.Poco.Tests;

#nullable enable

[TestClass]
public class OverflowErrorTests
{
    [TestMethod]
    public void SettingInvalidElementValue_Should_ThrowOnAccess()
    {
        TestOnPrimitiveElement(new Patient(), ["Patient", "boolean"]);
        TestOnPrimitiveElement(new Integer(10), ["integer", "boolean"]);
        TestOnPrimitiveElement(new List<Patient>(), ["collection of Patient", "boolean"]);
        TestOnPrimitiveElement(new FhirBoolean(true), null);
        TestOnArrayElement(new List<Patient>(), ["collection of Patient", "collection of HumanName"]);
        TestOnArrayElement(new Patient(), ["Patient", "collection of HumanName"]);
        TestOnArrayElement(new FhirBoolean(true), ["boolean", "collection of HumanName"]);
        TestOnArrayElement(new List<HumanName>(), null);
        TestOnObjectElement(new FhirBoolean(false), ["boolean", "Narrative"]);
        TestOnObjectElement(new List<HumanName>(), ["collection of HumanName", "Narrative"]);
        TestOnObjectElement(new Patient(), ["Patient", "Narrative"]);
        TestOnObjectElement(new Narrative("<div> this div is not centered </div>"), null);
    }

    private static void TestOnPrimitiveElement(object value, string[]? shouldBeInErrorMsg)
    {
        var pat = new Patient();
        pat["active"] = value;

        var act = () => pat.ActiveElement;
        if (shouldBeInErrorMsg is null)
            act.Should().NotThrow();
        else 
            act.Should().Throw<COVE>().Which.Should().Match<COVE>(
                e => e.ErrorCode == COVE.PROPERTY_TYPE_MISMATCH_CODE && 
                shouldBeInErrorMsg.All(substring => e.Message.Contains(substring))
            );
    }

    private static void TestOnArrayElement(object value, string[]? shouldBeInErrorMsg)
    {
        var pat = new Patient();
        pat.SetValue("name", value);

        var act = () => pat.Name;
        if (shouldBeInErrorMsg is null)
            act.Should().NotThrow();
        else 
            act.Should().Throw<COVE>().Which.Should().Match<COVE>(
                e => e.ErrorCode == COVE.PROPERTY_TYPE_MISMATCH_CODE && 
                     shouldBeInErrorMsg.All(substring => e.Message.Contains(substring))
            );
    }

    private static void TestOnObjectElement(object value, string[]? shouldBeInErrorMsg)
    {
        var pat = new Patient();
        pat.SetValue("text", value);

        var act = () => pat.Text;
        if (shouldBeInErrorMsg is null)
            act.Should().NotThrow();
        else 
            act.Should().Throw<COVE>().Which.Should().Match<COVE>(
                e => e.ErrorCode == COVE.PROPERTY_TYPE_MISMATCH_CODE && 
                     shouldBeInErrorMsg.All(substring => e.Message.Contains(substring))
            );
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
        act.Should().Throw<COVE>().Which.ErrorCode.Should().Be(COVE.PROPERTY_TYPE_MISMATCH_CODE);
        var act2 = () => pat.Gender;
        act2.Should().Throw<COVE>().Which.ErrorCode.Should().Be(COVE.PROPERTY_TYPE_MISMATCH_CODE);
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