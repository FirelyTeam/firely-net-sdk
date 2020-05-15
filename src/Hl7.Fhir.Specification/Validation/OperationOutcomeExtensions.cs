/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation.Schema;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Validation
{

    internal delegate bool Condition();

    internal static class OperationOutcomeExtensions
    {
        public static void MakeInformational(this OperationOutcome outcome)
        {
            var result = outcome;

            foreach (var issue in result.Issue)
                if (!issue.Success)
                    issue.Severity = OperationOutcome.IssueSeverity.Information;
        }

        public static void Flatten(this OperationOutcome outcome)
        {
            foreach (var issue in outcome.Issue)
            {
                issue.RemoveExtension(ValidationOutcomeExtensions.OPERATIONOUTCOME_ISSUE_HIERARCHY);
            }
        }

        public static OperationOutcome RemoveDuplicateMessages(this OperationOutcome outcome)
        {
            var comparer = new IssueComparer();
            outcome.Issue = outcome.Issue.Distinct(comparer).ToList();
            return outcome;
        }

        public static OperationOutcome ToOperationOutcome(this Assertions assertions)
        {
            var outcome = new OperationOutcome();

            var evidenceIssues = assertions.Result.Evidence.OfType<IssueAssertion>();

            var issues = assertions.OfType<IssueAssertion>();

            foreach (var item in issues.Concat(evidenceIssues))
            {
                var issue = Issue.Create(item.IssueNumber, ConvertToSeverity(item.Severity), OperationOutcome.IssueType.Invalid);
                outcome.AddIssue(item.Message, issue, item.Location);
            }

            // TODO Refactor VALIDATION
            return outcome;
        }

        private static OperationOutcome.IssueSeverity ConvertToSeverity(IssueSeverity? severity)
        {
            switch (severity)
            {
                case IssueSeverity.Fatal:
                    return OperationOutcome.IssueSeverity.Fatal;
                case IssueSeverity.Error:
                    return OperationOutcome.IssueSeverity.Error;
                case IssueSeverity.Warning:
                    return OperationOutcome.IssueSeverity.Warning;
                case IssueSeverity.Information:
                default:
                    return OperationOutcome.IssueSeverity.Information;
            }
        }

        private static IssueSeverity? ConvertToSeverity(OperationOutcome.IssueSeverity? severity)
        {
            switch (severity)
            {
                case OperationOutcome.IssueSeverity.Fatal:
                    return IssueSeverity.Fatal;
                case OperationOutcome.IssueSeverity.Error:
                    return IssueSeverity.Error;
                case OperationOutcome.IssueSeverity.Warning:
                    return IssueSeverity.Warning;
                case OperationOutcome.IssueSeverity.Information:
                default:
                    return IssueSeverity.Information;
            }
        }

        public static Assertions ToAssertions(this OperationOutcome outcome)
        {
            var assertions = outcome?.Issue?.Select(i => new IssueAssertion(GetIssueNumber(i.Details), i.Location.FirstOrDefault(), i.Details.Text, ConvertToSeverity(i.Severity)));
            return new Assertions(assertions);

            int GetIssueNumber(CodeableConcept concept)
            {
                var issueCode = concept.Coding.FirstOrDefault(cd => cd.System == Issue.API_OPERATION_OUTCOME_SYSTEM)?.Code;

                if (int.TryParse(issueCode, out var issueNumber))
                    return issueNumber;
                return -1;
            }
        }

        internal class IssueComparer : IEqualityComparer<OperationOutcome.IssueComponent>
        {
            public bool Equals(OperationOutcome.IssueComponent x, OperationOutcome.IssueComponent y)
            {
                if (x is null && y is null)
                    return true;
                else if (x is null || y is null)
                    return false;
                else if (x.Location?.FirstOrDefault() == y.Location?.FirstOrDefault() && x.Details?.Text == y.Details?.Text)
                    return true;
                else
                    return false;
            }

            public int GetHashCode(OperationOutcome.IssueComponent issue)
            {
                var hash = unchecked(issue?.Location?.FirstOrDefault()?.GetHashCode() ^ issue?.Details?.Text?.GetHashCode());
                return (hash is null) ? 0 : hash.Value;
            }

        }

    }
}