using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Model
{
    public static class OutcomeExtensions
    {
        public static bool Success(this OperationOutcome outcome)
        {
            if (outcome.Issue == null)
                return true;

            foreach (var issue in outcome.Issue)
            {
                if (issue.Severity == OperationOutcome.IssueSeverity.Error ||
                    issue.Severity == OperationOutcome.IssueSeverity.Fatal)
                    return false;
            }
            return true;
        }
    }
}
