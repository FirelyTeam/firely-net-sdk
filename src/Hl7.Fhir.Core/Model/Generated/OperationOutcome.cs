using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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

//
// Generated on Thu, Apr 2, 2015 14:21+0200 for FHIR v0.5.0
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
        /// A coded expression of the type of issue
        /// </summary>
        [FhirEnumeration("IssueType")]
        public enum IssueType
        {
            /// <summary>
            /// Content invalid against the Specification or a Profile.
            /// </summary>
            [EnumLiteral("invalid")]
            Invalid,
            /// <summary>
            /// A structural issue in the content such as wrong namespace, or unable to parse the content completely, or invalid json syntax.
            /// </summary>
            [EnumLiteral("structure")]
            Structure,
            /// <summary>
            /// A required element is missing.
            /// </summary>
            [EnumLiteral("required")]
            Required,
            /// <summary>
            /// element value invalid.
            /// </summary>
            [EnumLiteral("value")]
            Value,
            /// <summary>
            /// A content validation rule failed - e.g. a schematron rule.
            /// </summary>
            [EnumLiteral("invariant")]
            Invariant,
            /// <summary>
            /// An authentication/authorization/permissions issueof some kind.
            /// </summary>
            [EnumLiteral("security")]
            Security,
            /// <summary>
            /// the client needs to initiate an authentication process.
            /// </summary>
            [EnumLiteral("login")]
            Login,
            /// <summary>
            /// The user or system was not able to be authenticated (either there is no process, or the proferred token is unacceptable).
            /// </summary>
            [EnumLiteral("unknown")]
            Unknown,
            /// <summary>
            /// User session expired; a login may be required.
            /// </summary>
            [EnumLiteral("expired")]
            Expired,
            /// <summary>
            /// The user does not have the rights to perform this action.
            /// </summary>
            [EnumLiteral("forbidden")]
            Forbidden,
            /// <summary>
            /// Some information was not or may not have been returned due to business rules, consent or privacy rules, or access permission constraints.  This information may be accessible through alternate processes.
            /// </summary>
            [EnumLiteral("suppressed")]
            Suppressed,
            /// <summary>
            /// Processing issues. These are expected to be final e.g. there is no point resubmitting the same content unchanged.
            /// </summary>
            [EnumLiteral("processing")]
            Processing,
            /// <summary>
            /// The resource or profile is not supported.
            /// </summary>
            [EnumLiteral("not-supported")]
            NotSupported,
            /// <summary>
            /// An attempt was made to create a duplicate record.
            /// </summary>
            [EnumLiteral("duplicate")]
            Duplicate,
            /// <summary>
            /// The reference provided was not found. In a pure RESTful environment, this would be an HTTP 404 error, but this code may be used where the content is not found further into the application architecture.
            /// </summary>
            [EnumLiteral("not-found")]
            NotFound,
            /// <summary>
            /// Provided content is too long (typically, this is a denial of service protection type of error).
            /// </summary>
            [EnumLiteral("too-long")]
            TooLong,
            /// <summary>
            /// The code or system could not be understood, or it was not valid in the context of a particular ValueSet.
            /// </summary>
            [EnumLiteral("code-invalid")]
            CodeInvalid,
            /// <summary>
            /// An extension was found that was not acceptable, or that could not be resolved, or a modifierExtension that was not recognised.
            /// </summary>
            [EnumLiteral("extension")]
            Extension,
            /// <summary>
            /// The operation was stopped to protect server resources. E.g. a request for a value set expansion on all of SNOMED CT.
            /// </summary>
            [EnumLiteral("too-costly")]
            TooCostly,
            /// <summary>
            /// The content/operation failed to pass some business rule, and so could not proceed.
            /// </summary>
            [EnumLiteral("business-rule")]
            BusinessRule,
            /// <summary>
            /// content could not be accepted because of an edit conflict (i.e. version aware updates) (In a pure RESTful environment, this would be an HTTP 404 error, but this code may be used where the conflict is discovered further into the application architecture).
            /// </summary>
            [EnumLiteral("conflict")]
            Conflict,
            /// <summary>
            /// Not all data sources typically accessed could be reached, or responded in time, so the returned information may not be complete.
            /// </summary>
            [EnumLiteral("incomplete")]
            Incomplete,
            /// <summary>
            /// Transient processing issues. The system receiving the error may be able to resubmit the same content once an underlying issue is resolved.
            /// </summary>
            [EnumLiteral("transient")]
            Transient,
            /// <summary>
            /// A resource/record locking failure (usually in an underlying database).
            /// </summary>
            [EnumLiteral("lock-error")]
            LockError,
            /// <summary>
            /// The persistent store unavailable. E.g. the database is down for maintenance or similar.
            /// </summary>
            [EnumLiteral("no-store")]
            NoStore,
            /// <summary>
            /// An unexpected internal error.
            /// </summary>
            [EnumLiteral("exception")]
            Exception,
            /// <summary>
            /// An internal timeout occurred.
            /// </summary>
            [EnumLiteral("timeout")]
            Timeout,
            /// <summary>
            /// The system is not prepared to handle this request due to load management.
            /// </summary>
            [EnumLiteral("throttled")]
            Throttled,
            /// <summary>
            /// A message unrelated to the processing success of the completed operation (Examples of the latter include things like reminders of password expiry, system maintenance times, etc.).
            /// </summary>
            [EnumLiteral("informational")]
            Informational,
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
        public partial class OperationOutcomeIssueComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "OperationOutcomeIssueComponent"; } }
            
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
            [FhirElement("code", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Additional diagnostic information about the issue
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
            /// Additional diagnostic information about the issue
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
                get { if(_LocationElement==null) _LocationElement = new List<Hl7.Fhir.Model.FhirString>(); return _LocationElement; }
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
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
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
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
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
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(DetailsElement, otherT.DetailsElement)) return false;
                if( !DeepComparable.IsExactly(LocationElement, otherT.LocationElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// A single issue associated with the action
        /// </summary>
        [FhirElement("issue", Order=90)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.OperationOutcome.OperationOutcomeIssueComponent> Issue
        {
            get { if(_Issue==null) _Issue = new List<Hl7.Fhir.Model.OperationOutcome.OperationOutcomeIssueComponent>(); return _Issue; }
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
