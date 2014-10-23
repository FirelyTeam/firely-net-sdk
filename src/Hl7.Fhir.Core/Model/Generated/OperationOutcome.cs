using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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

//
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information about the success/failure of an action
    /// </summary>
    [FhirType("OperationOutcome", IsResource=true)]
    [DataContract]
    public partial class OperationOutcome : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// A coded expression of the type of issue
        /// </summary>
        [FhirEnumeration("IssueType")]
        public enum IssueType
        {
            /// <summary>
            /// Content invalid against Specification or Profile.
            /// </summary>
            [EnumLiteral("invalid")]
            Invalid,
            /// <summary>
            /// content structural issue.
            /// </summary>
            [EnumLiteral("structure")]
            Structure,
            /// <summary>
            /// required element missing.
            /// </summary>
            [EnumLiteral("required")]
            Required,
            /// <summary>
            /// element value invalid.
            /// </summary>
            [EnumLiteral("value")]
            Value,
            /// <summary>
            /// schematron rule.
            /// </summary>
            [EnumLiteral("invariant")]
            Invariant,
            /// <summary>
            /// authorization/permissions issue.
            /// </summary>
            [EnumLiteral("security")]
            Security,
            /// <summary>
            /// the client needs to initiate the authentication process ().
            /// </summary>
            [EnumLiteral("login")]
            Login,
            /// <summary>
            /// user/system not able to be authenticated.
            /// </summary>
            [EnumLiteral("unknown")]
            Unknown,
            /// <summary>
            /// user session expired.
            /// </summary>
            [EnumLiteral("expired")]
            Expired,
            /// <summary>
            /// user rights failure.
            /// </summary>
            [EnumLiteral("forbidden")]
            Forbidden,
            /// <summary>
            /// processing issues.
            /// </summary>
            [EnumLiteral("processing")]
            Processing,
            /// <summary>
            /// resource not supported.
            /// </summary>
            [EnumLiteral("not-supported")]
            NotSupported,
            /// <summary>
            /// duplicate resource.
            /// </summary>
            [EnumLiteral("duplicate")]
            Duplicate,
            /// <summary>
            /// reference not found.
            /// </summary>
            [EnumLiteral("not-found")]
            NotFound,
            /// <summary>
            /// existing content too long.
            /// </summary>
            [EnumLiteral("too-long")]
            TooLong,
            /// <summary>
            /// code could not be understood.
            /// </summary>
            [EnumLiteral("code-unknown")]
            CodeUnknown,
            /// <summary>
            /// extension not recognized.
            /// </summary>
            [EnumLiteral("extension")]
            Extension,
            /// <summary>
            /// operation denied to protect server resources.
            /// </summary>
            [EnumLiteral("too-costly")]
            TooCostly,
            /// <summary>
            /// content failed to pass some business rule.
            /// </summary>
            [EnumLiteral("business-rule")]
            BusinessRule,
            /// <summary>
            /// content could not be accepted because of an edit conflict (i.e. version aware updates).
            /// </summary>
            [EnumLiteral("conflict")]
            Conflict,
            /// <summary>
            /// transient processing issues.
            /// </summary>
            [EnumLiteral("transient")]
            Transient,
            /// <summary>
            /// resource/record locking failure.
            /// </summary>
            [EnumLiteral("lock-error")]
            LockError,
            /// <summary>
            /// persistent store unavailable.
            /// </summary>
            [EnumLiteral("no-store")]
            NoStore,
            /// <summary>
            /// unexpected internal error.
            /// </summary>
            [EnumLiteral("exception")]
            Exception,
            /// <summary>
            /// internal timeout.
            /// </summary>
            [EnumLiteral("timeout")]
            Timeout,
            /// <summary>
            /// The system is not prepared to handle this request due to load management.
            /// </summary>
            [EnumLiteral("throttled")]
            Throttled,
        }
        
        /// <summary>
        /// How the issue affects the success of the action
        /// </summary>
        [FhirEnumeration("IssueSeverity")]
        public enum IssueSeverity
        {
            /// <summary>
            /// The issue caused the action to fail, and no further checking could be performed.
            /// </summary>
            [EnumLiteral("fatal")]
            Fatal,
            /// <summary>
            /// The issue is sufficiently important to cause the action to fail.
            /// </summary>
            [EnumLiteral("error")]
            Error,
            /// <summary>
            /// The issue is not important enough to cause the action to fail, but may cause it to be performed suboptimally or in a way that is not as desired.
            /// </summary>
            [EnumLiteral("warning")]
            Warning,
            /// <summary>
            /// The issue has no relation to the degree of success of the action.
            /// </summary>
            [EnumLiteral("information")]
            Information,
        }
        
        [FhirType("OperationOutcomeIssueComponent")]
        [DataContract]
        public partial class OperationOutcomeIssueComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
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
                    if(value == null)
                      SeverityElement = null; 
                    else
                      SeverityElement = new Code<Hl7.Fhir.Model.OperationOutcome.IssueSeverity>(value);
                    OnPropertyChanged("Severity");
                }
            }
            
            /// <summary>
            /// Error or warning code
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Additional description of the issue
            /// </summary>
            [FhirElement("details", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DetailsElement
            {
                get { return _DetailsElement; }
                set { _DetailsElement = value; OnPropertyChanged("DetailsElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DetailsElement;
            
            /// <summary>
            /// Additional description of the issue
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Details
            {
                get { return DetailsElement != null ? DetailsElement.Value : null; }
                set
                {
                    if(value == null)
                      DetailsElement = null; 
                    else
                      DetailsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Details");
                }
            }
            
            /// <summary>
            /// XPath of element(s) related to issue
            /// </summary>
            [FhirElement("location", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> LocationElement
            {
                get { return _LocationElement; }
                set { _LocationElement = value; OnPropertyChanged("LocationElement"); }
            }
            private List<Hl7.Fhir.Model.FhirString> _LocationElement;
            
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
                    if(value == null)
                      LocationElement = null; 
                    else
                      LocationElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Location");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OperationOutcomeIssueComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SeverityElement != null) dest.SeverityElement = (Code<Hl7.Fhir.Model.OperationOutcome.IssueSeverity>)SeverityElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(DetailsElement != null) dest.DetailsElement = (Hl7.Fhir.Model.FhirString)DetailsElement.DeepCopy();
                    if(LocationElement != null) dest.LocationElement = new List<Hl7.Fhir.Model.FhirString>(LocationElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new OperationOutcomeIssueComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OperationOutcomeIssueComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(DetailsElement, otherT.DetailsElement)) return false;
                if( !DeepComparable.Matches(LocationElement, otherT.LocationElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OperationOutcomeIssueComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(DetailsElement, otherT.DetailsElement)) return false;
                if( !DeepComparable.IsExactly(LocationElement, otherT.LocationElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// A single issue associated with the action
        /// </summary>
        [FhirElement("issue", Order=70)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.OperationOutcome.OperationOutcomeIssueComponent> Issue
        {
            get { return _Issue; }
            set { _Issue = value; OnPropertyChanged("Issue"); }
        }
        private List<Hl7.Fhir.Model.OperationOutcome.OperationOutcomeIssueComponent> _Issue;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as OperationOutcome;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Issue != null) dest.Issue = new List<Hl7.Fhir.Model.OperationOutcome.OperationOutcomeIssueComponent>(Issue.DeepCopy());
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
        
    }
    
}
