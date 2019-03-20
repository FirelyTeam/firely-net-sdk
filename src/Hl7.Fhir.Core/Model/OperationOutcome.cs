using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Diagnostics;

/*
    Copyright (c) 2011+, HL7, Inc.
    All rights reserved.

    Redistribution and use in source and binary forms, with or without modification, 
    are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this 
        list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, 
        this list of conditions and the following disclaimer in the documentation 
        and/or other materials provided with the distribution.
    * Neither the name of HL7 nor the names of its contributors may be used to 
        endorse or promote products derived from this software without specific 
        prior written permission.

    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
    IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
    INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
    NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
    PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
    WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
    ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
    POSSIBILITY OF SUCH DAMAGE.


*/
#pragma warning disable 1591 // suppress XML summary warnings

namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay(@"\{{ToString()}}")]
    public partial class OperationOutcome
    {
        [Obsolete("You should now pass in the IssueType. This now defaults to IssueType.Processing")]
        public static OperationOutcome ForMessage(string message, IssueSeverity severity = IssueSeverity.Error)
        {
            return ForMessage(message, R4.IssueType.Processing, severity);
        }

        public static OperationOutcome ForMessage<TIssueType>(string message, TIssueType code, IssueSeverity severity = IssueSeverity.Error) where TIssueType : Enum
        {
            return new OperationOutcome()
            {
                Issue = new List<IssueComponent>
                {
                    new IssueComponent { Severity = severity, Code = Utility.EnumUtility.GetLiteral( code ), Diagnostics = message }
                }
            };
        }

        [Obsolete("You should now pass in the IssueType. This now defaults to IssueType.Processing")]
        public static OperationOutcome ForException(Exception e, IssueSeverity severity = IssueSeverity.Error)
        {
            return ForException(e, R4.IssueType.Processing, severity);
        }

        public static OperationOutcome ForException<TIssueType>(Exception e, TIssueType type, IssueSeverity severity = IssueSeverity.Error) where TIssueType : Enum
        {
            var result = ForMessage(e.Message, type, severity);
            var ie = e.InnerException;

            while (ie != null)
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
                textBuilder.AppendFormat("Overall result: FAILURE ({0} errors and {1} warnings)", Errors + Fatals, Warnings);
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
