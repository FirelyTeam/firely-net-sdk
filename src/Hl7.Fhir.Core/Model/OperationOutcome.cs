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
    }
}
