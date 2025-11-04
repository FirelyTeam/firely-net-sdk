/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Tests.Validation;

[TestClass]
public class ValidationTests_LowLevelApi
{
    private readonly FhirAttributeValidator validator = new ();
    
    private void assertInstanceValidationErrors(Base instance, params string?[] expectedErrorCodes)
    {
        var errors = validator.ValidateObject(
            instance,
            ModelInfo.ModelInspector.FindClassMapping(instance.GetType())!,
            new PocoValidationContext(
                instance,
                ModelInfo.ModelInspector,
                () => "",
                0, 0,
                NarrativeValidationKind.FhirXhtml
            )
        );
        if (expectedErrorCodes.All(error => error != null))
            errors.Select(e => e.ErrorCode).Should().BeEquivalentTo(expectedErrorCodes);
    }

    private void assertPropertyValidationErrors(Base instance, string propNameToValidate, params string?[] expectedErrorCodes)
    {
        var errors = validator.ValidateProperty(
            propNameToValidate,
            instance[propNameToValidate],
            ModelInfo.ModelInspector.FindClassMapping(instance.GetType())?.FindMappedElementByName(propNameToValidate),
            new PocoValidationContext(
                instance,
                ModelInfo.ModelInspector,
                () => "",
                0, 0,
                NarrativeValidationKind.FhirXhtml
            )
        );
        if (expectedErrorCodes.All(error => error != null))
            errors.Select(e => e.ErrorCode).Should().BeEquivalentTo(expectedErrorCodes);
    }
        
    [TestMethod]
    [DataRow("az23", null)]
    [DataRow("!notgood!", CodedValidationException.LITERAL_INVALID_CODE)]
    [DataRow("NotGood!", CodedValidationException.LITERAL_INVALID_CODE)]
    [DataRow("123456789012345678901234567890123456745290123456745290123456745290123456745290", CodedValidationException.LITERAL_INVALID_CODE)]
    public void TestIdValidation(string idVal, string? expectedError)
    {
        var id = new Id(idVal);
        assertInstanceValidationErrors(id, expectedError);
    }

    [TestMethod]
    public void IdIsNowAString()
    {
        HumanName hn = HumanName.ForFamily("Kramer");
        hn.ElementId = "This/may:contain.all$kinds%of@characters_now";

        assertInstanceValidationErrors(hn);
    }

    [TestMethod]
    public void ValidatesResourceTag()
    {
        var p = new Patient
        {
            Meta = new Meta()
        };

        p.Meta.Tag.Add(new Coding("http://system", "  illegal    _  code "));

        assertPropertyValidationErrors(p.Meta, "tag"); // throws no errors because this code validation is not run here
    }

    [TestMethod]
    public void ValidatesInvalidListType()
    {
        const string prop = "profile";
        var meta = new Meta();
        meta.SetValue(prop, new List<FhirUri>());

        assertPropertyValidationErrors(meta, prop, "PVAL127");
    }

    [TestMethod]
    [DataRow("urn:oid:1.2.3", null)]
    [DataRow("urn:oid:datmagdusniet", CodedValidationException.LITERAL_INVALID_CODE)]
    [DataRow("urn:uuid:a5afddf4-e880-459b-876e-e4591b0acc11", null)]
    [DataRow("urn:uuid:ooknietgoed", CodedValidationException.LITERAL_INVALID_CODE)]
    [DataRow("urn:oid:1.2.0.3.4", null)]
    public void OIDandUUIDUrls(string oidUrl, string? expectedError)
    {
        var uri = new FhirUri(oidUrl);
        assertInstanceValidationErrors(uri, expectedError);
    }

    [TestMethod]
    public void TestAllowedChoices()
    {
        Patient p = new()
        {
            Deceased = new FhirBoolean(true)
        };
        assertInstanceValidationErrors(p);
        assertPropertyValidationErrors(p, "deceased");

        // Deceased can either be boolean or dateTime, not FhirUri
        p.Deceased = new FhirUri();
        assertPropertyValidationErrors(p, "deceased", CodedValidationException.CHOICE_TYPE_NOT_ALLOWED_CODE);
    }


    [TestMethod]
    public void TestCardinality()
    {
        OperationOutcome oo = new(){ImplicitRules = "no rules here, just making sure the oo contains something"};
        assertInstanceValidationErrors(oo, CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);

        oo.Issue = [];
        assertInstanceValidationErrors(oo, CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);

        var issue = new OperationOutcome.IssueComponent(){ElementId = "issue1"};

        oo.Issue.Add(issue); 
        assertInstanceValidationErrors(oo);
        assertInstanceValidationErrors(oo.Issue.First(), CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE, CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);

        issue.Severity = OperationOutcome.IssueSeverity.Information;
        assertInstanceValidationErrors(oo.Issue.First(), CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);

        issue.Code = OperationOutcome.IssueType.Forbidden;
        assertInstanceValidationErrors(oo.Issue.First());

        assertInstanceValidationErrors(oo);
    }

    [TestMethod]
    public void TestEmptyCollectionValidation()
    {
        var p = new Patient
        {
            Identifier = new List<Identifier>()
        };
        p.Identifier.Add(null!);

        assertInstanceValidationErrors(p);
    }

    [TestMethod]
    public void ContainedResourcesAreValidatedToo()
    {
        Patient p = new()
        {
            // Deceased can either be boolean or dateTime, not FhirUri
            Deceased = new FhirUri()
        };

        var pr = new Patient
        {
            Contained = new List<Resource> { p }
        };

        assertInstanceValidationErrors(pr);
    }

    [TestMethod]
    public void TestContainedConstraints()
    {
        var pat = new Patient();
        var patn = new Patient();
        pat.Contained = new List<Resource> { patn };
        patn.Contained = new List<Resource> { new Patient() };

        // Contained resources should not themselves contain resources
        assertInstanceValidationErrors(pat, CodedValidationException.CONTAINED_RESOURCES_CANNOT_BE_NESTED_CODE);
    }

    [TestMethod]
    public void ValidateResourceWithIncorrectChildElement()
    {
        // First create an incomplete Observation (status and code not supplied)
        var obs = new Observation(){Id = "obs1"};
        assertInstanceValidationErrors(obs, CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE, CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);

        obs.Status = ObservationStatus.Final;
        obs.Code = new CodeableConcept("http://snomed.info/sct", "27113001", "Body weight");

        // Now, it should work
        assertInstanceValidationErrors(obs);

        // Hide an incorrect datetime deep into the Observation
        FhirDateTime dt = new()
        {
            Value = "Ewout Kramer"  // clearly, a wrong datetime
        };

        obs.Effective = new Period() { StartElement = dt };

        // Since we do not validate recursively, we should still be ok
        assertInstanceValidationErrors(obs);

        // When we navigate to it, we should fail
        assertInstanceValidationErrors(((Period)obs.Effective).StartElement!, CodedValidationException.LITERAL_INVALID_CODE);
    }

#if !NETSTANDARD1_6
    [TestMethod]    // XHtml validation not available in portable library
    public void TestXhtmlValidation()
    {
        var p = new Patient
        {
            Text = new Narrative() { Div = "<div xmlns='http://www.w3.org/1999/xhtml'><p>should be valid</p></div>", Status = Narrative.NarrativeStatus.Generated }
        };
        assertInstanceValidationErrors(p);

        p.Text.Div = "<div xmlns='http://www.w3.org/1999/xhtml'><p>should not be valid<p></div>";
        assertInstanceValidationErrors(p.Text.DivElement!, CodedValidationException.NARRATIVE_XML_IS_MALFORMED_CODE);

        p.Text.Div = "<div xmlns='http://www.w3.org/1999/xhtml'><img onmouseover='bigImg(this)' src='smiley.gif' alt='Smiley' /></div>";
        assertInstanceValidationErrors(p.Text.DivElement!, CodedValidationException.NARRATIVE_XML_IS_INVALID_CODE);
    }
#endif

    [TestMethod]
    public void TestBinaryContentCardinalityValidation()
    {
        var bin = new Binary
        {
            ContentType = "text/plain",
            Content = [0, 1, 2, 3],
            Data = [0, 1, 2, 3]
        };

        assertInstanceValidationErrors(bin);

        //We removed  the cardinality validation for the Content property for issue #2821
        bin = new Binary
        {
            ContentType = "text/plain",
            Data = [0, 1, 2, 3]
        };

        assertInstanceValidationErrors(bin);

        bin = new Binary
        {
            Data = [0, 1, 2, 3]
        };

        assertInstanceValidationErrors(bin, CodedValidationException.MANDATORY_ELEMENT_MUST_BE_PRESENT_CODE);
    }
}