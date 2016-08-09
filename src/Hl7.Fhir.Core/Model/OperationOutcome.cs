using Hl7.Fhir.Introspection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay(@"\{{ToString()}}")]
    public partial class OperationOutcome
    {
        [Obsolete("You should now pass in the IssueType. This now defaults to IssueType.Processing")]
        public static OperationOutcome ForMessage(string message, OperationOutcome.IssueSeverity severity = IssueSeverity.Error)
        {
            return ForMessage(message, IssueType.Processing, severity);
        }

        public static OperationOutcome ForMessage(string message, OperationOutcome.IssueType code, OperationOutcome.IssueSeverity severity = IssueSeverity.Error)
        {
            return new OperationOutcome() {
                      Issue = new List<OperationOutcome.IssueComponent>()
                            { new OperationOutcome.IssueComponent() 
                                    { Severity = severity, Diagnostics = message } 
                            } };
        }

        [Obsolete("You should now pass in the IssueType. This now defaults to IssueType.Processing")]
        public static OperationOutcome ForException(Exception e, OperationOutcome.IssueSeverity severity = IssueSeverity.Error)
        {
            return ForException(e, IssueType.Processing, severity);
        }
        public static OperationOutcome ForException(Exception e, OperationOutcome.IssueType type, OperationOutcome.IssueSeverity severity = IssueSeverity.Error)
        {
            var result = OperationOutcome.ForMessage(e.Message, type, severity);
            var ie = e.InnerException;

            while(ie != null)
            {
                result.Issue.Add(new IssueComponent { Diagnostics = ie.Message, Severity = IssueSeverity.Information });
                ie = ie.InnerException;
            }

            return result;
        }

        public override string ToString()
        {
            if (Text != null && !string.IsNullOrEmpty(Text.Div))
            {
                return Text.Div;
            }

            var text = String.Empty;
            if (Issue != null)
            {
                foreach (var issue in Issue)
                {
                    if (!String.IsNullOrEmpty(text))
                        text += " ------------- ";  // Add divider after each issue

                    if (issue.Severity != null)
                    {
                        text += issue.Severity.ToString() + ": ";
                    }

                    if (issue.Diagnostics != null)
                    {
                        text += issue.Diagnostics;
                    }
                    else
                    {
                        text += "No diagnostics";
                    }
                }
            }

            return text;
        }

        [System.Diagnostics.DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        public partial class IssueComponent
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [NotMapped]
            private string DebuggerDisplay
            {
                get
                {
                    return String.Format("Code=\"{0}\" {1}", this.Code, _Details.DebuggerDisplay("Details."));
                }
            }
        }
    }
}
