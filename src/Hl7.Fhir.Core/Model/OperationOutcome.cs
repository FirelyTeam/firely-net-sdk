using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Model
{
    public partial class OperationOutcome
    {
        public static OperationOutcome ForMessage(string message, OperationOutcome.IssueSeverity severity = IssueSeverity.Error)
        {
            return new OperationOutcome() {
                      Issue = new List<OperationOutcome.OperationOutcomeIssueComponent>()
                            { new OperationOutcome.OperationOutcomeIssueComponent() 
                                    { Severity = severity, Details = message } 
                            } };
        }

        public static OperationOutcome ForException(Exception e, OperationOutcome.IssueSeverity severity = IssueSeverity.Error)
        {
            var result = OperationOutcome.ForMessage(e.Message);
            var ie = e.InnerException;

            while(ie != null)
            {
                result.Issue.Add(new OperationOutcomeIssueComponent { Details = ie.Message, Severity = IssueSeverity.Information });
                ie = ie.InnerException;
            }

            return result;
        }
    }
}
