/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Linq;
using System.Collections.Generic;
using Hl7.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;

namespace Hl7.Fhir.Validation
{

    internal delegate bool Condition();

    internal static class OperationOutcomeExtensions
    {
        [Obsolete("Use if() with Trace() instead")]
        public static bool Verify(this OperationOutcome outcome, Condition condition, string message, Issue issue, INamedNode location)
        {
            if (!condition())
            {
                outcome.AddIssue(issue.ToIssueComponent(message, location));
                return false;
            }
            else
                return true;
        }

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
    }

}