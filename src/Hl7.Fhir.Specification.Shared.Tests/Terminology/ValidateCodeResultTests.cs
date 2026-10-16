using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Terminology;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class ValidateCodeResultTests
    {
        [TestMethod]
        public void NewPropertiesRoundTripThroughParameters()
        {
            var cc = new CodeableConcept("http://example.org/system", "code1");
            var result = ValidateCodeResult.ForResult(true, code: "code1", system: "http://example.org/system");
            result.CodeableConcept = cc;
            result.NormalizedCode = new Code("CODE1");
            result.CausedByUnknownSystem = [new Canonical("http://example.org/a"), new Canonical("http://example.org/b")];
            result.UnknownSystem = new Canonical("http://example.org/unknown");
            result.Inactive = new FhirBoolean(true);
            result.Status = new Code("retired");

            // Read back through a fresh instance over the same parameters, as a client would
            var roundTripped = new ValidateCodeResult(new Parameters { Parameter = result.Parameter });

            roundTripped.CodeableConcept!.Matches(cc).Should().BeTrue();
            roundTripped.NormalizedCode!.Value.Should().Be("CODE1");
            roundTripped.CausedByUnknownSystem!.Select(c => c.Value).Should().Equal("http://example.org/a", "http://example.org/b");
            roundTripped.UnknownSystem!.Value.Should().Be("http://example.org/unknown");
            roundTripped.Inactive!.Value.Should().BeTrue();
            roundTripped.Status!.Value.Should().Be("retired");

            roundTripped.Get(ValidateCodeResult.CODEABLECONCEPT_ATTRIBUTE).Should().ContainSingle();
            roundTripped.Get(ValidateCodeResult.NORMALIZED_CODE_ATTRIBUTE).Should().ContainSingle();
            roundTripped.Get(ValidateCodeResult.CAUSED_BY_UNKNOWN_SYSTEM_ATTRIBUTE).Should().HaveCount(2);
            roundTripped.Get(ValidateCodeResult.UNKNOWN_SYSTEM_ATTRIBUTE).Should().ContainSingle();
            roundTripped.Get(ValidateCodeResult.INACTIVE_ATTRIBUTE).Should().ContainSingle();
            roundTripped.Get(ValidateCodeResult.STATUS_ATTRIBUTE).Should().ContainSingle();
        }

        [TestMethod]
        public void SettersReplaceAndNullRemoves()
        {
            var result = ValidateCodeResult.ForResult(true);

            result.Status = new Code("deprecated");
            result.Status = new Code("retired");
            result.Get(ValidateCodeResult.STATUS_ATTRIBUTE).Should().ContainSingle()
                .Which.Value.Should().BeOfType<Code>().Which.Value.Should().Be("retired");

            result.CausedByUnknownSystem = [new Canonical("http://example.org/a")];
            result.CausedByUnknownSystem = [];
            result.CausedByUnknownSystem.Should().BeNull();

            result.CodeableConcept = new CodeableConcept("http://example.org/system", "code1");
            result.NormalizedCode = new Code("x");
            result.UnknownSystem = new Canonical("http://example.org/unknown");
            result.Inactive = new FhirBoolean(false);

            result.CodeableConcept = null;
            result.NormalizedCode = null;
            result.UnknownSystem = null;
            result.Inactive = null;
            result.Status = null;
            result.CausedByUnknownSystem = null;

            result.Parameter.Select(p => p.Name).Should().Equal(ValidateCodeResult.RESULT_ATTRIBUTE);
        }

        [TestMethod]
        public void IssueHelpersCreateAndExtendTheIssuesOutcome()
        {
            var result = ValidateCodeResult.ForResult(false);
            result.Issues.Should().BeNull();

            var first = result.AddCodeNotInValueSet("Not in the value set", "Coding.code");
            var second = result.AddCodeComment("Just a remark");

            var outcome = result.Issues.Should().BeOfType<OperationOutcome>().Subject;
            outcome.Issue.Should().Equal(first, second);
            result.Get(ValidateCodeResult.ISSUES_ATTRIBUTE).Should().ContainSingle();

            first.Severity.Should().Be(OperationOutcome.IssueSeverity.Error);
            first.Code.Should().Be(OperationOutcome.IssueType.CodeInvalid);
            first.Details.Text.Should().Be("Not in the value set");
            first.Details.Coding.Should().ContainSingle().Which.Should().Match<Coding>(c =>
                c.System == "http://hl7.org/fhir/tools/CodeSystem/tx-issue-type" && c.Code == "not-in-vs");
            first.Expression.Should().Equal("Coding.code");

            second.Severity.Should().Be(OperationOutcome.IssueSeverity.Warning);
            second.Details.Coding.Single().Code.Should().Be("code-comment");
            second.Expression.Should().BeEmpty();
        }

        [TestMethod]
        [DataRow("incomplete", OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid, "invalid-data")]
        [DataRow("not-in-system", OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.CodeInvalid, "invalid-code")]
        [DataRow("display", OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Invalid, "invalid-display")]
        [DataRow("display-strict", OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid, "invalid-display")]
        [DataRow("unprocessable", OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.BusinessRule, "code-rule")]
        [DataRow("unresolvable", OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.NotFound, "not-found")]
        public void IssueHelpersUseTheIssueTypeOfTheirCause(string cause, OperationOutcome.IssueSeverity severity, OperationOutcome.IssueType issueType, string txIssueType)
        {
            var result = ValidateCodeResult.ForResult(false);
            var issue = cause switch
            {
                "incomplete" => result.AddIncompleteCodedParameter("text"),
                "not-in-system" => result.AddCodeNotInSystem("text"),
                "display" => result.AddInvalidDisplay("text", lenient: true),
                "display-strict" => result.AddInvalidDisplay("text", lenient: false),
                "unprocessable" => result.AddUnprocessableCodedParameter("text"),
                _ => result.AddUnresolvable("text"),
            };

            issue.Severity.Should().Be(severity);
            issue.Code.Should().Be(issueType);
            issue.Details.Coding.Single().Code.Should().Be(txIssueType);
        }

        [TestMethod]
        public void ObsoleteAddInvalidDisplayIsLenient()
        {
            var result = ValidateCodeResult.ForResult(true);
#pragma warning disable CS0618 // Type or member is obsolete: the overload without 'lenient' keeps its 6.x behaviour.
            var issue = result.AddInvalidDisplay("text", "Coding.display");
#pragma warning restore CS0618

            issue.Severity.Should().Be(OperationOutcome.IssueSeverity.Warning);
            issue.Details.Coding.Single().Code.Should().Be("invalid-display");
            issue.Expression.Should().Equal("Coding.display");
        }
    }
}
