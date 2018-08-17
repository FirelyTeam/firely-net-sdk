using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;

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

//
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information about the success/failure of an action
    /// </summary>
    [FhirType("OperationOutcome", IsResource=true)]
    [DataContract]
    public partial class OperationOutcome : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.OperationOutcome; } }
        [NotMapped]
        public override string TypeName { get { return "OperationOutcome"; } }
        
        /// <summary>
        /// How the issue affects the success of the action.
        /// (url: http://hl7.org/fhir/ValueSet/issue-severity)
        /// </summary>
        [FhirEnumeration("IssueSeverity")]
        public enum IssueSeverity
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-severity)
            /// </summary>
            [EnumLiteral("fatal", "http://hl7.org/fhir/issue-severity"), Description("Fatal")]
            Fatal,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-severity)
            /// </summary>
            [EnumLiteral("error", "http://hl7.org/fhir/issue-severity"), Description("Error")]
            Error,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-severity)
            /// </summary>
            [EnumLiteral("warning", "http://hl7.org/fhir/issue-severity"), Description("Warning")]
            Warning,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-severity)
            /// </summary>
            [EnumLiteral("information", "http://hl7.org/fhir/issue-severity"), Description("Information")]
            Information,
        }

        /// <summary>
        /// A code that describes the type of issue.
        /// (url: http://hl7.org/fhir/ValueSet/issue-type)
        /// </summary>
        [FhirEnumeration("IssueType")]
        public enum IssueType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("invalid", "http://hl7.org/fhir/issue-type"), Description("Invalid Content")]
            Invalid,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("structure", "http://hl7.org/fhir/issue-type"), Description("Structural Issue")]
            Structure,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("required", "http://hl7.org/fhir/issue-type"), Description("Required element missing")]
            Required,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("value", "http://hl7.org/fhir/issue-type"), Description("Element value invalid")]
            Value,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("invariant", "http://hl7.org/fhir/issue-type"), Description("Validation rule failed")]
            Invariant,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("security", "http://hl7.org/fhir/issue-type"), Description("Security Problem")]
            Security,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("login", "http://hl7.org/fhir/issue-type"), Description("Login Required")]
            Login,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("unknown", "http://hl7.org/fhir/issue-type"), Description("Unknown User")]
            Unknown,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("expired", "http://hl7.org/fhir/issue-type"), Description("Session Expired")]
            Expired,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("forbidden", "http://hl7.org/fhir/issue-type"), Description("Forbidden")]
            Forbidden,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("suppressed", "http://hl7.org/fhir/issue-type"), Description("Information  Suppressed")]
            Suppressed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("processing", "http://hl7.org/fhir/issue-type"), Description("Processing Failure")]
            Processing,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("not-supported", "http://hl7.org/fhir/issue-type"), Description("Content not supported")]
            NotSupported,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("duplicate", "http://hl7.org/fhir/issue-type"), Description("Duplicate")]
            Duplicate,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("not-found", "http://hl7.org/fhir/issue-type"), Description("Not Found")]
            NotFound,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("too-long", "http://hl7.org/fhir/issue-type"), Description("Content Too Long")]
            TooLong,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("code-invalid", "http://hl7.org/fhir/issue-type"), Description("Invalid Code")]
            CodeInvalid,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("extension", "http://hl7.org/fhir/issue-type"), Description("Unacceptable Extension")]
            Extension,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("too-costly", "http://hl7.org/fhir/issue-type"), Description("Operation Too Costly")]
            TooCostly,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("business-rule", "http://hl7.org/fhir/issue-type"), Description("Business Rule Violation")]
            BusinessRule,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("conflict", "http://hl7.org/fhir/issue-type"), Description("Edit Version Conflict")]
            Conflict,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("incomplete", "http://hl7.org/fhir/issue-type"), Description("Incomplete Results")]
            Incomplete,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("transient", "http://hl7.org/fhir/issue-type"), Description("Transient Issue")]
            Transient,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("lock-error", "http://hl7.org/fhir/issue-type"), Description("Lock Error")]
            LockError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("no-store", "http://hl7.org/fhir/issue-type"), Description("No Store Available")]
            NoStore,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("exception", "http://hl7.org/fhir/issue-type"), Description("Exception")]
            Exception,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("timeout", "http://hl7.org/fhir/issue-type"), Description("Timeout")]
            Timeout,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("throttled", "http://hl7.org/fhir/issue-type"), Description("Throttled")]
            Throttled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/issue-type)
            /// </summary>
            [EnumLiteral("informational", "http://hl7.org/fhir/issue-type"), Description("Informational Note")]
            Informational,
        }

        [FhirType("IssueComponent")]
        [DataContract]
        public partial class IssueComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "IssueComponent"; } }
            
            /// <summary>
            /// fatal | error | warning | information
            /// </summary>
            [FhirElement("severity", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.OperationOutcome.IssueSeverity> SeverityElement
            {
                get { return _SeverityElement; }
                set { _SeverityElement = value; OnPropertyChanged("SeverityElement"); }
            }
            
            private Code<Hl7.Fhir.Model.OperationOutcome.IssueSeverity> _SeverityElement;
            
            /// <summary>
            /// fatal | error | warning | information
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.OperationOutcome.IssueSeverity? Severity
            {
                get { return SeverityElement != null ? SeverityElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SeverityElement = null; 
                    else
                        SeverityElement = new Code<Hl7.Fhir.Model.OperationOutcome.IssueSeverity>(value);
                    OnPropertyChanged("Severity");
                }
            }
            
            /// <summary>
            /// Error or warning code
            /// </summary>
            [FhirElement("code", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.OperationOutcome.IssueType> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.OperationOutcome.IssueType> _CodeElement;
            
            /// <summary>
            /// Error or warning code
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.OperationOutcome.IssueType? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CodeElement = null; 
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.OperationOutcome.IssueType>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Additional details about the error
            /// </summary>
            [FhirElement("details", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Details
            {
                get { return _Details; }
                set { _Details = value; OnPropertyChanged("Details"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Details;
            
            /// <summary>
            /// Additional diagnostic information about the issue
            /// </summary>
            [FhirElement("diagnostics", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DiagnosticsElement
            {
                get { return _DiagnosticsElement; }
                set { _DiagnosticsElement = value; OnPropertyChanged("DiagnosticsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DiagnosticsElement;
            
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
                        DiagnosticsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Diagnostics");
                }
            }
            
            /// <summary>
            /// Path of element(s) related to issue
            /// </summary>
            [FhirElement("location", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> LocationElement
            {
                get { if(_LocationElement==null) _LocationElement = new List<Hl7.Fhir.Model.FhirString>(); return _LocationElement; }
                set { _LocationElement = value; OnPropertyChanged("LocationElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _LocationElement;
            
            /// <summary>
            /// Path of element(s) related to issue
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
                        LocationElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Location");
                }
            }
            
            /// <summary>
            /// FHIRPath of element(s) related to issue
            /// </summary>
            [FhirElement("expression", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ExpressionElement
            {
                get { if(_ExpressionElement==null) _ExpressionElement = new List<Hl7.Fhir.Model.FhirString>(); return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _ExpressionElement;
            
            /// <summary>
            /// FHIRPath of element(s) related to issue
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Expression
            {
                get { return ExpressionElement != null ? ExpressionElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ExpressionElement = null; 
                    else
                        ExpressionElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Expression");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as IssueComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SeverityElement != null) dest.SeverityElement = (Code<Hl7.Fhir.Model.OperationOutcome.IssueSeverity>)SeverityElement.DeepCopy();
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.OperationOutcome.IssueType>)CodeElement.DeepCopy();
                    if(Details != null) dest.Details = (Hl7.Fhir.Model.CodeableConcept)Details.DeepCopy();
                    if(DiagnosticsElement != null) dest.DiagnosticsElement = (Hl7.Fhir.Model.FhirString)DiagnosticsElement.DeepCopy();
                    if(LocationElement != null) dest.LocationElement = new List<Hl7.Fhir.Model.FhirString>(LocationElement.DeepCopy());
                    if(ExpressionElement != null) dest.ExpressionElement = new List<Hl7.Fhir.Model.FhirString>(ExpressionElement.DeepCopy());
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
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
                
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
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
                
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
                    foreach (var elem in ExpressionElement) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SeverityElement != null) yield return new ElementValue("severity", SeverityElement);
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (Details != null) yield return new ElementValue("details", Details);
                    if (DiagnosticsElement != null) yield return new ElementValue("diagnostics", DiagnosticsElement);
                    foreach (var elem in LocationElement) { if (elem != null) yield return new ElementValue("location", elem); }
                    foreach (var elem in ExpressionElement) { if (elem != null) yield return new ElementValue("expression", elem); }
                }
            }

            
        }
        
        
        /// <summary>
        /// A single issue associated with the action
        /// </summary>
        [FhirElement("issue", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.OperationOutcome.IssueComponent> Issue
        {
            get { if(_Issue==null) _Issue = new List<Hl7.Fhir.Model.OperationOutcome.IssueComponent>(); return _Issue; }
            set { _Issue = value; OnPropertyChanged("Issue"); }
        }
        
        private List<Hl7.Fhir.Model.OperationOutcome.IssueComponent> _Issue;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as OperationOutcome;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Issue != null) dest.Issue = new List<Hl7.Fhir.Model.OperationOutcome.IssueComponent>(Issue.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new OperationOutcome());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as OperationOutcome;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Issue, otherT.Issue)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as OperationOutcome;
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

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Issue) { if (elem != null) yield return new ElementValue("issue", elem); }
            }
        }

    }
    
}
