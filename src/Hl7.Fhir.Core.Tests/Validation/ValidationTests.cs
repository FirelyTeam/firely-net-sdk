/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Tests.Validation
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        public void TestIdValidation()
        {
            Id id = new Id("az23");

            DotNetAttributeValidation.Validate(id);
            DotNetAttributeValidation.Validate(id,true);        // recursive checking shouldnt matter

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

            DotNetAttributeValidation.Validate(hn);
        }


        private void validateErrorOrFail(object instance, bool recurse=false, string membername=null)
        {
            try
            {
                // should throw error
                DotNetAttributeValidation.Validate(instance, recurse);
                Assert.Fail();
            }
            catch (ValidationException ve) 
            {
                if (membername != null)
                    Assert.IsTrue(ve.ValidationResult.MemberNames.Contains(membername));
            }
        }
     
        [TestMethod]
        public void OIDandUUIDUrls()
        {
            var oidUrl = "urn:oid:1.2.3";
            var illOidUrl = "urn:oid:datmagdusniet";
            var uuidUrl = "urn:uuid:a5afddf4-e880-459b-876e-e4591b0acc11";
            var illUuidUrl = "urn:uuid:ooknietgoed";
            var oidWithZero = "urn:oid:1.2.0.3.4";

            FhirUri uri = new FhirUri(oidUrl);
            Validator.ValidateObject(uri, new ValidationContext(uri), true);

            uri = new FhirUri(illOidUrl);
            validateErrorOrFail(uri);

            uri = new FhirUri(uuidUrl);
            Validator.ValidateObject(uri, new ValidationContext(uri), true);

            uri = new FhirUri(illUuidUrl);
            validateErrorOrFail(uri);

            uri = new FhirUri(oidWithZero);
            Validator.ValidateObject(uri, new ValidationContext(uri), true);

            Assert.IsTrue(Uri.Equals(new Uri("http://nu.nl"), new Uri("http://nu.nl")));
        }

   

        [TestMethod]
        public void TestAllowedChoices()
        {
            Patient p = new Patient();

            p.Deceased = new FhirBoolean(true);
            DotNetAttributeValidation.Validate(p);

            // Deceased can either be boolean or dateTime, not FhirUri
            p.Deceased = new FhirUri();
            validateErrorOrFail(p);
        }


        [TestMethod]
        public void TestCardinality()
        {
            OperationOutcome oo = new OperationOutcome();
            validateErrorOrFail(oo,true);

            oo.Issue = new List<OperationOutcome.IssueComponent>();
            validateErrorOrFail(oo,true);

            var issue = new OperationOutcome.IssueComponent();

            oo.Issue.Add(issue);
            validateErrorOrFail(oo,true);

            issue.Severity = OperationOutcome.IssueSeverity.Information;
            validateErrorOrFail(oo, true);

            issue.Code = OperationOutcome.IssueType.Forbidden;

            DotNetAttributeValidation.Validate(oo, true);
        }

        [TestMethod]
        public void TestEmptyCollectionValidation()
        {
            var p = new Patient();
            p.Identifier = new List<Identifier>();
            p.Identifier.Add(null);

            validateErrorOrFail(p);
        }

        [TestMethod]
        public void ContainedResourcesAreValidatedToo()
        {
            Patient p = new Patient();
            // Deceased can either be boolean or dateTime, not FhirUri
            p.Deceased = new FhirUri();

            var pr = new Patient();
            pr.Contained = new List<Resource> { p };

            validateErrorOrFail(pr,true);
            DotNetAttributeValidation.Validate(pr);
        }

        [TestMethod]
        public void TestContainedConstraints()
        {
            var pat = new Patient();
            var patn = new Patient();
            pat.Contained = new List<Resource> { patn } ;
            patn.Contained = new List<Resource> { new Patient() };

            // Contained resources should not themselves contain resources
            validateErrorOrFail(pat);

            patn.Contained = null;
            DotNetAttributeValidation.Validate(pat);

            patn.Text = new Narrative();
            patn.Text.Div = "<div>Narrative in contained resource</div>";

            // Contained resources should not contain narrative
            validateErrorOrFail(pat);
        }

        [TestMethod]
        public void ValidateResourceWithIncorrectChildElement()
        {
            // First create an incomplete encounter (class not supplied)
            var enc = new Encounter();
            validateErrorOrFail(enc, membername: "StatusElement");
            validateErrorOrFail(enc,true);  // recursive checking shouldn't matter

            enc.Status = Encounter.EncounterStatus.Planned;

            // Now, it should work
            DotNetAttributeValidation.Validate(enc);
            DotNetAttributeValidation.Validate(enc, true);  // recursive checking shouldnt matter

            // Hide an incorrect datetime deep into the Encounter
            FhirDateTime dt = new FhirDateTime();
            dt.Value = "Ewout Kramer";  // clearly, a wrong datetime

            enc.Period = new Period() { StartElement = dt };

            // When we do not validate recursively, we should still be ok
            DotNetAttributeValidation.Validate(enc);

            // When we recurse, this should fail
            validateErrorOrFail(enc, true, membername: "Value");
        }

#if NET_XSD_SCHEMA
        [TestMethod]    // XHtml validation not available in portable library
        public void TestXhtmlValidation()
        {
            var p = new Patient();

            p.Text = new Narrative() { Div = "<div xmlns='http://www.w3.org/1999/xhtml'><p>should be valid</p></div>", Status = Narrative.NarrativeStatus.Generated  };
            DotNetAttributeValidation.Validate(p,true);

            p.Text.Div = "<div xmlns='http://www.w3.org/1999/xhtml'><p>should not be valid<p></div>";
            validateErrorOrFail(p,true);

            p.Text.Div = "<div xmlns='http://www.w3.org/1999/xhtml'><img onmouseover='bigImg(this)' src='smiley.gif' alt='Smiley' /></div>";
            validateErrorOrFail(p,true);
        }
#endif       
    }
}
