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
using Issue = Hl7.Fhir.Support.Issue;

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

            foreach (var item in assertions.GetIssueAssertions())
            {
                var issue = Issue.Create(item.IssueNumber, convertToSeverity(item.Severity), convertToType(item.Type));
                outcome.AddIssue(item.Message, issue, item.Location);
            }

            // TODO Refactor VALIDATION
            return outcome;
        }

        private static OperationOutcome.IssueSeverity convertToSeverity(IssueSeverity? severity)
        {
            return severity switch
            {
                IssueSeverity.Fatal => OperationOutcome.IssueSeverity.Fatal,
                IssueSeverity.Error => OperationOutcome.IssueSeverity.Error,
                IssueSeverity.Warning => OperationOutcome.IssueSeverity.Warning,
                _ => OperationOutcome.IssueSeverity.Information,
            };
        }

        private static IssueSeverity? ConvertToSeverity(OperationOutcome.IssueSeverity? severity)
        {
            return severity switch
            {
                OperationOutcome.IssueSeverity.Fatal => IssueSeverity.Fatal,
                OperationOutcome.IssueSeverity.Error => IssueSeverity.Error,
                OperationOutcome.IssueSeverity.Warning => IssueSeverity.Warning,
                _ => IssueSeverity.Information,
            };
        }

        private static OperationOutcome.IssueType convertToType(IssueType? type) => type switch
        {
            IssueType.BusinessRule => OperationOutcome.IssueType.BusinessRule,
            IssueType.Invalid => OperationOutcome.IssueType.Invalid,
            IssueType.Invariant => OperationOutcome.IssueType.Invariant,
            IssueType.Incomplete => OperationOutcome.IssueType.Incomplete,
            IssueType.Structure => OperationOutcome.IssueType.Structure,
            IssueType.NotSupported => OperationOutcome.IssueType.NotSupported,
            IssueType.Informational => OperationOutcome.IssueType.Informational,
            IssueType.Exception => OperationOutcome.IssueType.Exception,
            IssueType.CodeInvalid => OperationOutcome.IssueType.CodeInvalid,
            _ => OperationOutcome.IssueType.Invalid,
        };

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