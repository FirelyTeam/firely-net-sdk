using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Tests
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        public void TestIdValidation()
        {
            Id id = new Id("az23");

            FhirValidator.Validate(id);
            FhirValidator.Validate(id,true);        // recursive checking shouldnt matter

            id = new Id("!notgood!");
            validateErrorOrFail(id);

            id = new Id("NotGood");
            validateErrorOrFail(id);

            id = new Id("1234567890123456789012345678901234567");
            validateErrorOrFail(id);
        }


        private void validateErrorOrFail(object instance, bool recurse=false, string membername=null)
        {
            try
            {
                // should throw error
                FhirValidator.Validate(instance, recurse);
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
            FhirValidator.Validate(p);

            // Deceased can either be boolean or dateTime, not FhirUri
            p.Deceased = new FhirUri();
            validateErrorOrFail(p);
        }


        [TestMethod]
        public void TestCardinality()
        {
            OperationOutcome oo = new OperationOutcome();
            validateErrorOrFail(oo,true);

            oo.Issue = new List<OperationOutcome.OperationOutcomeIssueComponent>();
            validateErrorOrFail(oo,true);

            var issue = new OperationOutcome.OperationOutcomeIssueComponent();

            oo.Issue.Add(issue);
            validateErrorOrFail(oo,true);

            issue.Severity = OperationOutcome.IssueSeverity.Information;
            FhirValidator.Validate(oo, true);
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
            FhirValidator.Validate(pr);
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
            FhirValidator.Validate(pat);

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
            enc.Status = Encounter.EncounterState.Planned;
            validateErrorOrFail(enc, membername: "ClassElement");
            validateErrorOrFail(enc,true);  // recursive checking shouldn't matter

            enc.Class = Encounter.EncounterClass.Ambulatory;

            // Now, it should work
            FhirValidator.Validate(enc);
            FhirValidator.Validate(enc, true);  // recursive checking shouldnt matter

            // Hide an incorrect datetime deep into the Encounter
            FhirDateTime dt = new FhirDateTime();
            dt.Value = "Ewout Kramer";  // clearly, a wrong datetime

            enc.Hospitalization = new Encounter.EncounterHospitalizationComponent();
            enc.Hospitalization.Period = new Period() { StartElement = dt };

            // When we do not validate recursively, we should still be ok
            FhirValidator.Validate(enc);

            // When we recurse, this should fail
            validateErrorOrFail(enc, true, membername: "Value");
        }

        [TestMethod]
        public void ValidateEntry()
        {
            var pe = new ResourceEntry<Patient>(new Uri("http://www.nu.nl/fhir/Patient/1"), DateTimeOffset.Now, new Patient());
            Assert.IsNotNull(pe.Id);
            Assert.IsNotNull(pe.Title);
            Assert.IsNotNull(pe.LastUpdated);
            Assert.IsNotNull(pe.Resource);
            FhirValidator.Validate(pe);

            var b = new Bundle("A test feed", DateTimeOffset.Now);
            b.AuthorName = "Ewout";

            Assert.IsNotNull(pe.Id);
            Assert.IsNotNull(pe.Title);
            Assert.IsNotNull(pe.LastUpdated);
            b.Entries.Add(pe);
            FhirValidator.Validate(b);
        }



        [TestMethod]
        public void ValidateBundleEntry()
        {
            var e = new ResourceEntry<Patient>();
            e.Id = new Uri("http://someserver.org/fhir/Patient/1");
            e.Title = "Some title";

            // Validates mandatory fields?
            validateErrorOrFail(e);
            e.LastUpdated = DateTimeOffset.Now;
            e.Resource = new Patient();
            FhirValidator.Validate(e);

            // Checks nested errors on resource content?
            e.Resource = new Patient { Deceased = new FhirUri() };
            validateErrorOrFail(e, true);

            e.Resource = new Patient();

            var bundle = new Bundle() { Title = "Some feed title" };
            bundle.Id = new Uri("http://someserver.org/fhir/feed/1424234232342");

            // Validates mandatory fields?
            validateErrorOrFail(bundle);
            bundle.LastUpdated = DateTimeOffset.Now;
            FhirValidator.Validate(bundle);

            // Checks nested errors on nested bundle element?
            bundle.Entries.Add(e);
            e.Id = null;
            validateErrorOrFail(bundle,true);
        }


        [TestMethod]
        public void TestXhtmlValidation()
        {
            var p = new Patient();

            p.Text = new Narrative() { Div = "<div xmlns='http://www.w3.org/1999/xhtml'><p>should be valid</p></div>", Status = Narrative.NarrativeStatus.Generated  };
            FhirValidator.Validate(p,true);

            p.Text.Div = "<div xmlns='http://www.w3.org/1999/xhtml'><p>should not be valid<p></div>";
            validateErrorOrFail(p,true);

            p.Text.Div = "<div xmlns='http://www.w3.org/1999/xhtml'><img onmouseover='bigImg(this)' src='smiley.gif' alt='Smiley' /></div>";
            validateErrorOrFail(p,true);
        }
    }
}
