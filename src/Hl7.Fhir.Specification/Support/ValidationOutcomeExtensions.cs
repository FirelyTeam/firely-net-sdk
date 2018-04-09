/*
* Copyright (c) 2016, Firely (info@fire.ly) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.Fhir.Support
{

    public static class ValidationOutcomeExtensions
    {
        public static OperationOutcome.IssueComponent AddIssue(this OperationOutcome outcome, OperationOutcome.IssueComponent issue)
        {
            outcome.Issue.Add(issue);
            return issue;
        }

        public static void AddIssue(this OperationOutcome outcome, params OperationOutcome.IssueComponent[] issue)
        {
            outcome.AddIssue((IEnumerable<OperationOutcome.IssueComponent>)issue);
        }

        public static void AddIssue(this OperationOutcome outcome, IEnumerable<OperationOutcome.IssueComponent> issues)
        {
            outcome.Issue.AddRange(issues);
        }

        public static void Add(this OperationOutcome outcome, OperationOutcome other)
        {
            outcome.AddIssue(other.Issue);
            //foreach (var issue in other.Issue)
            //{
            //    //var myIssue = (OperationOutcome.IssueComponent)issue.DeepCopy();
            //    var myIssue = issue;
            //    outcome.AddIssue(myIssue);
            //}
        }


        public static Stopwatch OUTCOME_INCLUDE_TIMER = new Stopwatch();

        public static void Include(this OperationOutcome outcome, OperationOutcome other)
        {               
            foreach (var issue in other.Issue)
            {
                // var myIssue = (OperationOutcome.IssueComponent)issue.DeepCopy();
                var myIssue = issue;
                myIssue.SetHierarchyLevel(myIssue.GetHierarchyLevel() + 1);
                outcome.AddIssue(myIssue);
            }
        }

        public static void Clear(this OperationOutcome outcome)
        {
            outcome.Issue.Clear();
        }

        public static IEnumerable<OperationOutcome.IssueComponent> ListErrors(this OperationOutcome outcome)
        {
            return outcome.Issue.Where(issue => !issue.Success);
        }

        public static IEnumerable<OperationOutcome.IssueComponent> IssuesAt(this OperationOutcome outcome, IElementNavigator node)
        {
            return outcome.Issue.Where(issue => issue.IsAt(node));
        }

        public static IEnumerable<OperationOutcome.IssueComponent> IssuesAt(this OperationOutcome outcome, string path)
        {
            return outcome.Issue.Where(issue => issue.IsAt(path));
        }

        public static IEnumerable<OperationOutcome.IssueComponent> ErrorsAt(this OperationOutcome outcome, IElementNavigator node)
        {
            return outcome.ListErrors().Where(issue => issue.IsAt(node));
        }

        public static IEnumerable<OperationOutcome.IssueComponent> ErrorsAt(this OperationOutcome outcome, string path)
        {
            return outcome.ListErrors().Where(issue => issue.IsAt(path));
        }

        public static IEnumerable<OperationOutcome.IssueComponent> Where(this OperationOutcome outcome,
                OperationOutcome.IssueSeverity? severity=null, OperationOutcome.IssueType? type=null, int? issueCode = null)
        {
            IEnumerable<OperationOutcome.IssueComponent> result = outcome.Issue;

            if (severity != null) result = result.Where(i => i.Severity == severity);
            if (type != null) result = result.Where(i => i.Code == type);
            if (issueCode != null) result = result.Where(i => i.Details.Matches(Issue.ToCodeableConcept(issueCode.Value)));

            return result;
        }

        public static IEnumerable<OperationOutcome.IssueComponent> AtLevel(this OperationOutcome outcome, int level)
        {
            return outcome.Issue.Where(i => i.GetHierarchyLevel() == level);
        }

        public const string OPERATIONOUTCOME_ISSUE_HIERARCHY = "http://hl7.org/fhir/StructureDefinition/operationoutcome-issue-hierarchy";

        public static void SetHierarchyLevel(this OperationOutcome.IssueComponent issue, int level)
        {
            issue.SetIntegerExtension(OPERATIONOUTCOME_ISSUE_HIERARCHY, level);
        }

        public static int GetHierarchyLevel(this OperationOutcome.IssueComponent issue)
        {
            return issue.GetIntegerExtension(OPERATIONOUTCOME_ISSUE_HIERARCHY).GetValueOrDefault(0);
        }

        public static bool IsAt(this OperationOutcome.IssueComponent issue, string path)
        {
            if (issue.Location != null)
            {
                return issue.Location.Contains(path);
            }

            return false;
        }

        public static bool IsAt(this OperationOutcome.IssueComponent issue, IElementNavigator location)
        {
            return issue.IsAt(location.Location);
        }

    }
}
