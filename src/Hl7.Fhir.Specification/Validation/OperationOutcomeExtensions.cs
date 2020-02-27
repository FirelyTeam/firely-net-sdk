/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
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
                if(!issue.Success) 
                    issue.Severity = OperationOutcome.IssueSeverity.Information;
        }

        public static void Flatten(this OperationOutcome outcome)
        {
            foreach(var issue in outcome.Issue)
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
    }

    internal class IssueComparer : IEqualityComparer<OperationOutcome.IssueComponent>
    {        
        public bool Equals(OperationOutcome.IssueComponent x, OperationOutcome.IssueComponent y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else if (x.Location.FirstOrDefault() == x.Location.FirstOrDefault() && x.Details.Text == y.Details.Text)
                return true;
            else
                return false;
        }

        public int GetHashCode(OperationOutcome.IssueComponent issue)
        {
            var hash = issue.Location.FirstOrDefault().GetHashCode() ^ issue.Details.Text.GetHashCode();
            return hash;           
        }
        
    }

}