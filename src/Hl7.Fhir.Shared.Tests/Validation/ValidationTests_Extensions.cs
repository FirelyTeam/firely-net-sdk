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

namespace Hl7.Fhir.Tests.Validation
{
    [TestClass]
    public class ValidationTests_Extensions
    {
        [TestMethod]
        public void Will_validate_recursively()
        {
            var organization = new Organization
            {
                Text = new Narrative { Div = "<wrong />", Status = Narrative.NarrativeStatus.Generated }
            };

            var patient = new Patient();
            patient.Contained.Add(organization);
            
            patient.Validate().Should().NotBeEmpty();
            
            organization.Text = null;

            // Try again
            patient.Validate().Should().OnlyContain(cove => cove.ErrorCode == CodedValidationException.ELEMENT_CANNOT_BE_EMPTY_CODE);
        }

        [TestMethod]
        public void TestIdValidation()
        {
            Id id = new("az23");
            id.Validate().Should().BeEmpty();

            id = new Id("!notgood!");
            validateErrorOrFail(id);

            id = new Id("NotGood!");
            validateErrorOrFail(id);

            id = new Id("123456789012345678901234567890123456745290123456745290123456745290123456745290");
            validateErrorOrFail(id);
        }

        [TestMethod]
        public void IdIsNowAString()
        {
            HumanName hn = HumanName.ForFamily("Kramer");
            hn.ElementId = "This/may:contain.all$kinds%of@characters_now";

            hn.Validate().Should().BeEmpty();
        }

        [TestMethod]
        public void ValidatesResourceTag()
        {
            var p = new Patient
            {
                Meta = new Meta()
                {
                }
            };

            p.Meta.Tag.Add(new Coding("http://system", "  illegal    _  code "));

            p.Validate().Should().NotBeEmpty();
        }

        private static void validateErrorOrFail(Base instance, bool recurse = false, string membername = null)
        {
            instance.Validate().Should().NotBeEmpty();
            if (membername != null)
                instance.Validate().Should().Contain(err => err.InstancePath.EndsWith(membername));
        }

        [TestMethod]
        public void OIDandUUIDUrls()
        {
            var oidUrl = "urn:oid:1.2.3";
            var illOidUrl = "urn:oid:datmagdusniet";
            var uuidUrl = "urn:uuid:a5afddf4-e880-459b-876e-e4591b0acc11";
            var illUuidUrl = "urn:uuid:ooknietgoed";
            var oidWithZero = "urn:oid:1.2.0.3.4";

            FhirUri uri = new(oidUrl);
            uri.Validate().Should().BeEmpty(); // should not throw

            uri = new FhirUri(illOidUrl);
            validateErrorOrFail(uri);

            uri = new FhirUri(uuidUrl);
            uri.Validate().Should().BeEmpty(); // should not throw

            uri = new FhirUri(illUuidUrl);
            validateErrorOrFail(uri);

            uri = new FhirUri(oidWithZero);
            uri.Validate().Should().BeEmpty();
        }



        [TestMethod]
        public void TestAllowedChoices()
        {
            Patient p = new()
            {
                Deceased = new FhirBoolean(true)
            };
            p.Validate().Should().BeEmpty();

            // Deceased can either be boolean or dateTime, not FhirUri
            p.Deceased = new FhirUri();
            validateErrorOrFail(p);
        }


        [TestMethod]
        public void TestCardinality()
        {
            OperationOutcome oo = new();
            validateErrorOrFail(oo, true);

            oo.Issue = new List<OperationOutcome.IssueComponent>();
            validateErrorOrFail(oo, true);

            var issue = new OperationOutcome.IssueComponent();

            oo.Issue.Add(issue);
            validateErrorOrFail(oo, true);

            issue.Severity = OperationOutcome.IssueSeverity.Information;
            validateErrorOrFail(oo, true);

            issue.Code = OperationOutcome.IssueType.Forbidden;

            oo.Validate().Should().BeEmpty();
        }

        [TestMethod]
        public void TestEmptyCollectionValidation()
        {
            var p = new Patient
            {
                Identifier = new List<Identifier>()
            };
            p.Identifier.Add(null);

            validateErrorOrFail(p);
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

            validateErrorOrFail(pr, true);
        }

        [TestMethod]
        public void TestContainedConstraints()
        {
            var pat = new Patient();
            var patn = new Patient();
            pat.Contained = new List<Resource> { patn };
            patn.Contained = new List<Resource> { new Patient() };

            // Contained resources should not themselves contain resources
            validateErrorOrFail(pat);
        }

        [TestMethod]
        public void ValidateResourceWithIncorrectChildElement()
        {
            // First create an incomplete Observation (status and code not supplied)
            var obs = new Observation();
            validateErrorOrFail(obs, membername: "status");
            validateErrorOrFail(obs, true);  // recursive checking shouldn't matter

            obs.Status = ObservationStatus.Final;
            obs.Code = new CodeableConcept("http://snomed.info/sct", "27113001", "Body weight");

            // Now, it should work
            obs.Validate().Should().BeEmpty();

            // Hide an incorrect datetime deep into the Observation
            FhirDateTime dt = new()
            {
                Value = "Ewout Kramer"  // clearly, a wrong datetime
            };

            obs.Effective = new Period() { StartElement = dt };

            // When we recurse, this should fail
            validateErrorOrFail(obs, true, membername: "start");
        }

#if !NETSTANDARD1_6
        [TestMethod]    // XHtml validation not available in portable library
        public void TestXhtmlValidation()
        {
            var p = new Patient
            {
                Text = new Narrative() { Div = "<div xmlns='http://www.w3.org/1999/xhtml'><p>should be valid</p></div>", Status = Narrative.NarrativeStatus.Generated }
            };
            p.Validate().Should().BeEmpty();

            p.Text.Div = "<div xmlns='http://www.w3.org/1999/xhtml'><p>should not be valid<p></div>";
            validateErrorOrFail(p, true);

            p.Text.Div = "<div xmlns='http://www.w3.org/1999/xhtml'><img onmouseover='bigImg(this)' src='smiley.gif' alt='Smiley' /></div>";
            validateErrorOrFail(p, true);
        }
#endif

        [TestMethod]
        public void Test_Is_Aware_Of_Version_Differences()
        {
            var bin = new Binary
            {
                ContentType = "text/plain",
#if STU3
                Content = [0, 1, 2, 3],
#else
                Data = [0, 1, 2, 3]
#endif
            };

            bin.Validate(inspector: ModelInfo.ModelInspector).Should().BeEmpty();

            //We removed  the cardinality validation for the Content property for issue #2821
            bin = new Binary
            {
                ContentType = "text/plain",
            };

            bin.Validate(inspector: ModelInfo.ModelInspector).Should().BeEmpty();

            bin = new Binary
            {
                ContentType = "text/plain",

                // Used R4 element in R3 and vice versa
#if STU3
                Data = [0, 1, 2, 3]
#else
                Content = [0, 1, 2, 3],
#endif
            };

            bin.Validate(inspector: ModelInfo.ModelInspector).Should().NotBeEmpty();
        }
        
        [TestMethod]
        public void ValidationReportsIndexes()
        {
            var bdl = new Bundle()
            {
                Entry = 
                [
                    new() { Resource = new Patient { ActiveElement = new() { JsonValue = "true" } } },
                    new() { Resource = new Patient { ActiveElement = new() { JsonValue = true } } },
                    new() { Resource = new Patient { ActiveElement = new() { JsonValue = "false" } } },
                ] 
            };
            var errors = bdl.Validate();
            errors.Should().HaveCount(3);
            errors.Should().Contain(x => x.InstancePath == "Bundle.entry[0].resource.active");
            errors.Should().Contain(x => x.InstancePath == "Bundle.entry[2].resource.active");
        }
    }
}