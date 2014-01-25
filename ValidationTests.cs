using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Tests
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        public void TestIdValidation()
        {
            Id id = new Id("az23");

            Validator.ValidateObject(id, new ValidationContext(id), true);

            id = new Id("!notgood!");
            validateErrorOrFail(id);

            id = new Id("NotGood");
            validateErrorOrFail(id);

            id = new Id("1234567890123456789012345678901234567");
            validateErrorOrFail(id);
        }


        private void validateErrorOrFail(object instance)
        {
            try
            {
                // should throw error
                Validator.ValidateObject(instance, new ValidationContext(instance), true);
                Assert.Fail();
            }
            catch (ValidationException) { }
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
        public void ValidateResourceWithIncorrectChildElement()
        {
            FhirDateTime dt = new FhirDateTime();
            dt.Value = "Ewout Kramer";

            Observation o = new Observation { Applies = dt };
            DiagnosticReport rep = new DiagnosticReport();
            rep.Contained = new List<Resource> { o };

            validateErrorOrFail(rep);
        }


        [TestMethod]
        public void TestAllowedChoices()
        {
            Patient p = new Patient();

            p.Deceased = new FhirBoolean(true);
            Validator.ValidateObject(p, new ValidationContext(p), true);

            p.Deceased = new FhirUri();
            validateErrorOrFail(p);
        }


        [TestMethod]
        public void TestCardinality()
        {
            OperationOutcome oo = new OperationOutcome();
            validateErrorOrFail(oo);

            oo.Issue = new List<OperationOutcome.OperationOutcomeIssueComponent>();
            validateErrorOrFail(oo);

            var issue = new OperationOutcome.OperationOutcomeIssueComponent();

            oo.Issue.Add(issue);
            validateErrorOrFail(oo);

            issue.Severity = OperationOutcome.IssueSeverity.Information;
            Validator.ValidateObject(issue, new ValidationContext(issue), true);
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
        public void TestContainedConstraints()
        {
            var pat = new Patient();
            var patn = new Patient();
            pat.Contained = new List<Resource> { patn } ;
            patn.Contained = new List<Resource> { new Patient() };

            validateErrorOrFail(pat);

            patn.Contained = null;
            Validator.ValidateObject(pat, new ValidationContext(pat), true);

            patn.Text = new Narrative();
            patn.Text.Div = "<div />";

            validateErrorOrFail(pat);
        }


        [TestMethod]
        public void ValidateBundleEntry()
        {
            var e = new ResourceEntry<Patient>();
            e.Id = new Uri("http://someserver.org/fhir/patient/@1");
            e.Title = "Some title";

            // Validates mandatory fields?
            validateErrorOrFail(e);
            e.LastUpdated = DateTimeOffset.Now;
            e.Resource = new Patient();
            Validator.ValidateObject(e, new ValidationContext(e), true);

            // Checks nested errors on resource content?
            e.Resource = new Patient { Deceased = new FhirUri() };
            validateErrorOrFail(e);

            e.Resource = new Patient();

            var f = new Bundle() { Title = "Some feed title" };
            f.Id = new Uri("http://someserver.org/fhir/feed/@1424234232342");

            // Validates mandatory fields?
            validateErrorOrFail(f);
            f.LastUpdated = DateTimeOffset.Now;
            Validator.ValidateObject(f, new ValidationContext(f), true);

            // Checks nested errors on nested bundle element?
            f.Entries.Add(e);
            e.Id = null;
            validateErrorOrFail(f);
        }
    }
}
