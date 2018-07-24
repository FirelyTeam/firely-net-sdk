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
                                    { Severity = severity, Code = code, Diagnostics = message } 
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

            var textBuilder = new StringBuilder();

            if (Success)
                textBuilder.Append("Overall result: SUCCESS");
            else
                textBuilder.AppendFormat("Overall result: FAILURE ({0} errors and {1} warnings)", Errors+Fatals, Warnings);
            textBuilder.AppendLine();

            if (Issue.Any())
            {
                foreach (var issue in Issue)
                {
                    textBuilder.Append(' ', issue.HierarchyLevel * 2);
                    textBuilder.AppendLine();
                    issue.ToStringBuilder(textBuilder);
                }
            }

            return textBuilder.ToString();
        }

        [NotMapped]
        public bool Success
        {
            get
            {
                return !Issue.Any(i => !i.Success);
            }
        }


        [NotMapped]
        public int Fatals
        {
            get
            {
                return Issue.Where(i => i.Severity == IssueSeverity.Fatal).Count();
            }
        }

        [NotMapped]
        public int Errors
        {
            get
            {
                return Issue.Where(i => i.Severity == IssueSeverity.Error).Count();
            }
        }

        [NotMapped]
        public int Warnings
        {
            get
            {
                return Issue.Where(i => i.Severity == IssueSeverity.Warning).Count();
            }
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

            [NotMapped]
            public bool Success
            {
                get
                {
                    return Severity != null && (Severity.Value == IssueSeverity.Information || Severity.Value == IssueSeverity.Warning);
                }
            }

            internal void ToStringBuilder(StringBuilder buffer)
            {
                if (Severity != null)
                {
                    buffer.Append("[");
                    buffer.Append(Severity.ToString().ToUpper());
                    buffer.Append("] ");
                }

                buffer.Append(Details?.Text ?? "(no details)");

                if (Diagnostics != null)
                {
                    buffer.Append("(further diagnostics: ");
                    buffer.Append(Diagnostics);
                    buffer.Append(")");
                }

                if (Location.Any())
                {
                    buffer.Append(" (at ");
                    buffer.Append(String.Join(" via ", Location));
                    buffer.Append(")");
                }
            }

            public override string ToString()
            {
                var textBuffer = new StringBuilder();
                ToStringBuilder(textBuffer);
                return textBuffer.ToString();
            }

            public const string OPERATIONOUTCOME_ISSUE_HIERARCHY = "http://hl7.org/fhir/StructureDefinition/operationoutcome-issue-hierarchy";

            [NotMapped]
            public int HierarchyLevel
            {
                get
                {
                    return this.GetIntegerExtension(OPERATIONOUTCOME_ISSUE_HIERARCHY).GetValueOrDefault(0);
                }

                set
                {
                    this.SetIntegerExtension(OPERATIONOUTCOME_ISSUE_HIERARCHY, value);
                }
            }
        }
    }
}
