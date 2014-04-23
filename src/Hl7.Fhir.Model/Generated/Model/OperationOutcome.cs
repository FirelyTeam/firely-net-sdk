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
// Generated on Thu, Apr 17, 2014 11:39+0200 for FHIR v0.80
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
            [EnumLiteral("invalid")]
            Invalid, // Content invalid against Specification or Profile.
            [EnumLiteral("structure")]
            Structure, // content structural issue.
            [EnumLiteral("required")]
            Required, // required element missing.
            [EnumLiteral("value")]
            Value, // element value invalid.
            [EnumLiteral("invariant")]
            Invariant, // schematron rule.
            [EnumLiteral("security")]
            Security, // authorization/permissions issue.
            [EnumLiteral("login")]
            Login, // the client needs to initiate the authentication process ().
            [EnumLiteral("unknown")]
            Unknown, // user/system not able to be authenticated.
            [EnumLiteral("expired")]
            Expired, // user session expired.
            [EnumLiteral("forbidden")]
            Forbidden, // user rights failure.
            [EnumLiteral("processing")]
            Processing, // processing issues.
            [EnumLiteral("not-supported")]
            NotSupported, // resource not supported.
            [EnumLiteral("duplicate")]
            Duplicate, // duplicate resource.
            [EnumLiteral("not-found")]
            NotFound, // reference not found.
            [EnumLiteral("too-long")]
            TooLong, // existing content too long.
            [EnumLiteral("code-unknown")]
            CodeUnknown, // code could not be understood.
            [EnumLiteral("extension")]
            Extension, // extension not recognized.
            [EnumLiteral("too-costly")]
            TooCostly, // operation denied to protect server resources.
            [EnumLiteral("business-rule")]
            BusinessRule, // content failed to pass some business rule.
            [EnumLiteral("conflict")]
            Conflict, // content could not be accepted because of an edit conflict (i.e. version aware updates).
            [EnumLiteral("transient")]
            Transient, // transient processing issues.
            [EnumLiteral("lock-error")]
            LockError, // resource/record locking failure.
            [EnumLiteral("no-store")]
            NoStore, // persistent store unavailable.
            [EnumLiteral("exception")]
            Exception, // unexpected internal error.
            [EnumLiteral("timeout")]
            Timeout, // internal timeout.
            [EnumLiteral("throttled")]
            Throttled, // The system is not prepared to handle this request due to load management.
        }
        
        /// <summary>
        /// How the issue affects the success of the action
        /// </summary>
        [FhirEnumeration("IssueSeverity")]
        public enum IssueSeverity
        {
            [EnumLiteral("fatal")]
            Fatal, // The issue caused the action to fail, and no further checking could be performed.
            [EnumLiteral("error")]
            Error, // The issue is sufficiently important to cause the action to fail.
            [EnumLiteral("warning")]
            Warning, // The issue is not important enough to cause the action to fail, but may cause it to be performed suboptimally or in a way that is not as desired.
            [EnumLiteral("information")]
            Information, // The issue has no relation to the degree of success of the action.
        }
        
        /// <summary>
        /// null
        /// </summary>
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
        
    }
    
}
