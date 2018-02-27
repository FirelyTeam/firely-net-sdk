using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

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
    /// <summary>
    /// Version-independent information about the success/failure of an action
    /// </summary>
    /// <remarks>Must be compatible with the OperationOutcome classes of the specific versions - e.g. serialzations of this class must be de-serializable in the 
    /// version-specific OperationOutcome classes</remarks>
    [FhirType(Version.All, "OperationOutcome", IsResource=true)]
    [DataContract]
    [System.Diagnostics.DebuggerDisplay(@"\{{ToString()}}")]
    public partial class CommonOperationOutcome : DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.OperationOutcome; } }
        [NotMapped]
        public override string TypeName { get { return "OperationOutcome"; } }
    
    
        [FhirType(Version.All, "IssueComponent")]
        [DataContract]
        [System.Diagnostics.DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        public partial class IssueComponent : BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            public IssueComponent()
            { }

            public IssueComponent(DSTU2.OperationOutcome.IssueComponent issue)
            {
                Code = issue?.Code;
                Severity = issue?.Severity;
                Details = (CodeableConcept)issue?.Details.DeepCopy();
                Diagnostics = issue.Diagnostics;
            }

            public IssueComponent(STU3.OperationOutcome.IssueComponent issue)
            {
                Code = issue?.Code;
                Severity = issue?.Severity;
                Details = (CodeableConcept)issue?.Details.DeepCopy();
                Diagnostics = issue.Diagnostics;
            }

            public DSTU2.OperationOutcome.IssueComponent ToDstu2()
            {
                return new DSTU2.OperationOutcome.IssueComponent
                {
                    Code = Code,
                    Severity = Severity,
                    Details = (CodeableConcept)Details.DeepCopy(),
                    Diagnostics = Diagnostics
                };
            }

            public STU3.OperationOutcome.IssueComponent ToStu3()
            {
                return new STU3.OperationOutcome.IssueComponent
                {
                    Code = Code,
                    Severity = Severity,
                    Details = (CodeableConcept)Details.DeepCopy(),
                    Diagnostics = Diagnostics
                };
            }

            [NotMapped]
            public override string TypeName { get { return "IssueComponent"; } }
            
            /// <summary>
            /// fatal | error | warning | information
            /// </summary>
            [FhirElement("severity", InSummary=new[]{Version.All}, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<IssueSeverity> SeverityElement
            {
                get { return _SeverityElement; }
                set { _SeverityElement = value; OnPropertyChanged("SeverityElement"); }
            }
            
            private Code<IssueSeverity> _SeverityElement;
            
            /// <summary>
            /// fatal | error | warning | information
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IssueSeverity? Severity
            {
                get { return SeverityElement != null ? SeverityElement.Value : null; }
                set
                {
                    if (value == null)
                        SeverityElement = null;
                    else
                        SeverityElement = new Code<IssueSeverity>(value);
                    OnPropertyChanged("Severity");
                }
            }
            
            /// <summary>
            /// Error or warning code
            /// </summary>
            [FhirElement("code", InSummary=new[]{Version.All}, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<IssueType> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<IssueType> _CodeElement;
            
            /// <summary>
            /// Error or warning code
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IssueType? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Code<IssueType>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Additional details about the error
            /// </summary>
            [FhirElement("details", InSummary=new[]{Version.All}, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public CodeableConcept Details
            {
                get { return _Details; }
                set { _Details = value; OnPropertyChanged("Details"); }
            }
            
            private CodeableConcept _Details;
            
            /// <summary>
            /// Additional diagnostic information about the issue
            /// </summary>
            [FhirElement("diagnostics", InSummary=new[]{Version.All}, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public FhirString DiagnosticsElement
            {
                get { return _DiagnosticsElement; }
                set { _DiagnosticsElement = value; OnPropertyChanged("DiagnosticsElement"); }
            }
            
            private FhirString _DiagnosticsElement;
            
            /// <summary>
            /// Additional diagnostic information about the issue
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Diagnostics
            {
                get { return DiagnosticsElement != null ? DiagnosticsElement.Value : null; }
                set
                {
                    if (value == null)
                        DiagnosticsElement = null;
                    else
                        DiagnosticsElement = new FhirString(value);
                    OnPropertyChanged("Diagnostics");
                }
            }
            
            /// <summary>
            /// XPath of element(s) related to issue
            /// </summary>
            [FhirElement("location", InSummary=new[]{Version.All}, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<FhirString> LocationElement
            {
                get { if(_LocationElement==null) _LocationElement = new List<FhirString>(); return _LocationElement; }
                set { _LocationElement = value; OnPropertyChanged("LocationElement"); }
            }
            
            private List<FhirString> _LocationElement;
            
            /// <summary>
            /// XPath of element(s) related to issue
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Location
            {
                get { return LocationElement != null ? LocationElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        LocationElement = null;
                    else
                        LocationElement = new List<FhirString>(value.Select(elem=>new FhirString(elem)));
                    OnPropertyChanged("Location");
                }
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as IssueComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SeverityElement != null) dest.SeverityElement = (Code<IssueSeverity>)SeverityElement.DeepCopy();
                    if(CodeElement != null) dest.CodeElement = (Code<IssueType>)CodeElement.DeepCopy();
                    if(Details != null) dest.Details = (CodeableConcept)Details.DeepCopy();
                    if(DiagnosticsElement != null) dest.DiagnosticsElement = (FhirString)DiagnosticsElement.DeepCopy();
                    if(LocationElement != null) dest.LocationElement = new List<FhirString>(LocationElement.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new IssueComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as IssueComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Details, otherT.Details)) return false;
                if( !DeepComparable.Matches(DiagnosticsElement, otherT.DiagnosticsElement)) return false;
                if( !DeepComparable.Matches(LocationElement, otherT.LocationElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as IssueComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Details, otherT.Details)) return false;
                if( !DeepComparable.IsExactly(DiagnosticsElement, otherT.DiagnosticsElement)) return false;
                if( !DeepComparable.IsExactly(LocationElement, otherT.LocationElement)) return false;
            
                return true;
            }
             
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SeverityElement != null) yield return SeverityElement;
                    if (CodeElement != null) yield return CodeElement;
                    if (Details != null) yield return Details;
                    if (DiagnosticsElement != null) yield return DiagnosticsElement;
                    foreach (var elem in LocationElement) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SeverityElement != null) yield return new ElementValue("severity", false, SeverityElement);
                    if (CodeElement != null) yield return new ElementValue("code", false, CodeElement);
                    if (Details != null) yield return new ElementValue("details", false, Details);
                    if (DiagnosticsElement != null) yield return new ElementValue("diagnostics", false, DiagnosticsElement);
                    foreach (var elem in LocationElement) { if (elem != null) yield return new ElementValue("location", true, elem); }
                }
            }

            [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
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


        /// <summary>
        /// A single issue associated with the action
        /// </summary>
        [FhirElement("issue", InSummary=new[]{Version.All}, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<IssueComponent> Issue
        {
            get { if(_Issue==null) _Issue = new List<IssueComponent>(); return _Issue; }
            set { _Issue = value; OnPropertyChanged("Issue"); }
        }
        
        private List<IssueComponent> _Issue;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as CommonOperationOutcome;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Issue != null) dest.Issue = new List<IssueComponent>(Issue.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new CommonOperationOutcome());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as CommonOperationOutcome;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Issue, otherT.Issue)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as CommonOperationOutcome;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Issue, otherT.Issue)) return false;
        
            return true;
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Issue) { if (elem != null) yield return elem; }
            }
        }
        public static CommonOperationOutcome ForMessage(string message, IssueType code, IssueSeverity severity = IssueSeverity.Error)
        {
            return new CommonOperationOutcome
            {
                Issue = new List<CommonOperationOutcome.IssueComponent>()
                            { new CommonOperationOutcome.IssueComponent()
                                    { Severity = severity, Code = code, Diagnostics = message }
                            }
            };
        }

        [Obsolete("You should now pass in the IssueType. This now defaults to IssueType.Processing")]
        public static CommonOperationOutcome ForException(Exception e, IssueSeverity severity = IssueSeverity.Error)
        {
            return ForException(e, IssueType.Processing, severity);
        }

        public static CommonOperationOutcome ForException(Exception e, IssueType type, IssueSeverity severity = IssueSeverity.Error)
        {
            var result = CommonOperationOutcome.ForMessage(e.Message, type, severity);
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

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Issue) { if (elem != null) yield return new ElementValue("issue", true, elem); }
            }
        }
    
    }

}
