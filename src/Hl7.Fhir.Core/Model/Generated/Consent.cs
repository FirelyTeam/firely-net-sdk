﻿using System;
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
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A healthcare consumer's  choices to permit or deny recipients or roles to perform actions for specific purposes and periods of time
    /// </summary>
    [FhirType("Consent", IsResource=true)]
    [DataContract]
    public partial class Consent : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Consent; } }
        [NotMapped]
        public override string TypeName { get { return "Consent"; } }
        
        /// <summary>
        /// Indicates the state of the consent.
        /// (url: http://hl7.org/fhir/ValueSet/consent-state-codes)
        /// </summary>
        [FhirEnumeration("ConsentState")]
        public enum ConsentState
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-state-codes)
            /// </summary>
            [EnumLiteral("draft", "http://hl7.org/fhir/consent-state-codes"), Description("Pending")]
            Draft,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-state-codes)
            /// </summary>
            [EnumLiteral("proposed", "http://hl7.org/fhir/consent-state-codes"), Description("Proposed")]
            Proposed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-state-codes)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/consent-state-codes"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-state-codes)
            /// </summary>
            [EnumLiteral("rejected", "http://hl7.org/fhir/consent-state-codes"), Description("Rejected")]
            Rejected,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-state-codes)
            /// </summary>
            [EnumLiteral("inactive", "http://hl7.org/fhir/consent-state-codes"), Description("Inactive")]
            Inactive,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-state-codes)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/consent-state-codes"), Description("Entered in Error")]
            EnteredInError,
        }

        /// <summary>
        /// How a rule statement is applied, such as adding additional consent or removing consent.
        /// (url: http://hl7.org/fhir/ValueSet/consent-provision-type)
        /// </summary>
        [FhirEnumeration("ConsentProvisionType")]
        public enum ConsentProvisionType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-provision-type)
            /// </summary>
            [EnumLiteral("deny", "http://hl7.org/fhir/consent-provision-type"), Description("Opt Out")]
            Deny,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-provision-type)
            /// </summary>
            [EnumLiteral("permit", "http://hl7.org/fhir/consent-provision-type"), Description("Opt In")]
            Permit,
        }

        /// <summary>
        /// How a resource reference is interpreted when testing consent restrictions.
        /// (url: http://hl7.org/fhir/ValueSet/consent-data-meaning)
        /// </summary>
        [FhirEnumeration("ConsentDataMeaning")]
        public enum ConsentDataMeaning
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-data-meaning)
            /// </summary>
            [EnumLiteral("instance", "http://hl7.org/fhir/consent-data-meaning"), Description("Instance")]
            Instance,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-data-meaning)
            /// </summary>
            [EnumLiteral("related", "http://hl7.org/fhir/consent-data-meaning"), Description("Related")]
            Related,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-data-meaning)
            /// </summary>
            [EnumLiteral("dependents", "http://hl7.org/fhir/consent-data-meaning"), Description("Dependents")]
            Dependents,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-data-meaning)
            /// </summary>
            [EnumLiteral("authoredby", "http://hl7.org/fhir/consent-data-meaning"), Description("AuthoredBy")]
            Authoredby,
        }

        [FhirType("PolicyComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PolicyComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PolicyComponent"; } }
            
            /// <summary>
            /// Enforcement source for policy
            /// </summary>
            [FhirElement("authority", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri AuthorityElement
            {
                get { return _AuthorityElement; }
                set { _AuthorityElement = value; OnPropertyChanged("AuthorityElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _AuthorityElement;
            
            /// <summary>
            /// Enforcement source for policy
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Authority
            {
                get { return AuthorityElement != null ? AuthorityElement.Value : null; }
                set
                {
                    if (value == null)
                        AuthorityElement = null; 
                    else
                        AuthorityElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Authority");
                }
            }
            
            /// <summary>
            /// Specific policy covered by this consent
            /// </summary>
            [FhirElement("uri", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UriElement
            {
                get { return _UriElement; }
                set { _UriElement = value; OnPropertyChanged("UriElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UriElement;
            
            /// <summary>
            /// Specific policy covered by this consent
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uri
            {
                get { return UriElement != null ? UriElement.Value : null; }
                set
                {
                    if (value == null)
                        UriElement = null; 
                    else
                        UriElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Uri");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PolicyComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AuthorityElement != null) dest.AuthorityElement = (Hl7.Fhir.Model.FhirUri)AuthorityElement.DeepCopy();
                    if(UriElement != null) dest.UriElement = (Hl7.Fhir.Model.FhirUri)UriElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PolicyComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PolicyComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AuthorityElement, otherT.AuthorityElement)) return false;
                if( !DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PolicyComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AuthorityElement, otherT.AuthorityElement)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (AuthorityElement != null) yield return AuthorityElement;
                    if (UriElement != null) yield return UriElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (AuthorityElement != null) yield return new ElementValue("authority", AuthorityElement);
                    if (UriElement != null) yield return new ElementValue("uri", UriElement);
                }
            }

            
        }
        
        
        [FhirType("VerificationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class VerificationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "VerificationComponent"; } }
            
            /// <summary>
            /// Has been verified
            /// </summary>
            [FhirElement("verified", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean VerifiedElement
            {
                get { return _VerifiedElement; }
                set { _VerifiedElement = value; OnPropertyChanged("VerifiedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _VerifiedElement;
            
            /// <summary>
            /// Has been verified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Verified
            {
                get { return VerifiedElement != null ? VerifiedElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        VerifiedElement = null; 
                    else
                        VerifiedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Verified");
                }
            }
            
            /// <summary>
            /// Person who verified
            /// </summary>
            [FhirElement("verifiedWith", Order=50)]
            [CLSCompliant(false)]
			[References("Patient","RelatedPerson")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference VerifiedWith
            {
                get { return _VerifiedWith; }
                set { _VerifiedWith = value; OnPropertyChanged("VerifiedWith"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _VerifiedWith;
            
            /// <summary>
            /// When consent verified
            /// </summary>
            [FhirElement("verificationDate", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime VerificationDateElement
            {
                get { return _VerificationDateElement; }
                set { _VerificationDateElement = value; OnPropertyChanged("VerificationDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _VerificationDateElement;
            
            /// <summary>
            /// When consent verified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string VerificationDate
            {
                get { return VerificationDateElement != null ? VerificationDateElement.Value : null; }
                set
                {
                    if (value == null)
                        VerificationDateElement = null; 
                    else
                        VerificationDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("VerificationDate");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as VerificationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(VerifiedElement != null) dest.VerifiedElement = (Hl7.Fhir.Model.FhirBoolean)VerifiedElement.DeepCopy();
                    if(VerifiedWith != null) dest.VerifiedWith = (Hl7.Fhir.Model.ResourceReference)VerifiedWith.DeepCopy();
                    if(VerificationDateElement != null) dest.VerificationDateElement = (Hl7.Fhir.Model.FhirDateTime)VerificationDateElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new VerificationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as VerificationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(VerifiedElement, otherT.VerifiedElement)) return false;
                if( !DeepComparable.Matches(VerifiedWith, otherT.VerifiedWith)) return false;
                if( !DeepComparable.Matches(VerificationDateElement, otherT.VerificationDateElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as VerificationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(VerifiedElement, otherT.VerifiedElement)) return false;
                if( !DeepComparable.IsExactly(VerifiedWith, otherT.VerifiedWith)) return false;
                if( !DeepComparable.IsExactly(VerificationDateElement, otherT.VerificationDateElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (VerifiedElement != null) yield return VerifiedElement;
                    if (VerifiedWith != null) yield return VerifiedWith;
                    if (VerificationDateElement != null) yield return VerificationDateElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (VerifiedElement != null) yield return new ElementValue("verified", VerifiedElement);
                    if (VerifiedWith != null) yield return new ElementValue("verifiedWith", VerifiedWith);
                    if (VerificationDateElement != null) yield return new ElementValue("verificationDate", VerificationDateElement);
                }
            }

            
        }
        
        
        [FhirType("provisionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class provisionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "provisionComponent"; } }
            
            /// <summary>
            /// deny | permit
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Consent.ConsentProvisionType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Consent.ConsentProvisionType> _TypeElement;
            
            /// <summary>
            /// deny | permit
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Consent.ConsentProvisionType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.Consent.ConsentProvisionType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Timeframe for this rule
            /// </summary>
            [FhirElement("period", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
            
            /// <summary>
            /// Who|what controlled by this rule (or group, by role)
            /// </summary>
            [FhirElement("actor", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Consent.provisionActorComponent> Actor
            {
                get { if(_Actor==null) _Actor = new List<Hl7.Fhir.Model.Consent.provisionActorComponent>(); return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private List<Hl7.Fhir.Model.Consent.provisionActorComponent> _Actor;
            
            /// <summary>
            /// Actions controlled by this rule
            /// </summary>
            [FhirElement("action", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Action;
            
            /// <summary>
            /// Security Labels that define affected resources
            /// </summary>
            [FhirElement("securityLabel", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> SecurityLabel
            {
                get { if(_SecurityLabel==null) _SecurityLabel = new List<Hl7.Fhir.Model.Coding>(); return _SecurityLabel; }
                set { _SecurityLabel = value; OnPropertyChanged("SecurityLabel"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _SecurityLabel;
            
            /// <summary>
            /// Context of activities covered by this rule
            /// </summary>
            [FhirElement("purpose", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Purpose
            {
                get { if(_Purpose==null) _Purpose = new List<Hl7.Fhir.Model.Coding>(); return _Purpose; }
                set { _Purpose = value; OnPropertyChanged("Purpose"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Purpose;
            
            /// <summary>
            /// e.g. Resource Type, Profile, CDA, etc.
            /// </summary>
            [FhirElement("class", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Class
            {
                get { if(_Class==null) _Class = new List<Hl7.Fhir.Model.Coding>(); return _Class; }
                set { _Class = value; OnPropertyChanged("Class"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Class;
            
            /// <summary>
            /// e.g. LOINC or SNOMED CT code, etc. in the content
            /// </summary>
            [FhirElement("code", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Code
            {
                get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Code;
            
            /// <summary>
            /// Timeframe for data controlled by this rule
            /// </summary>
            [FhirElement("dataPeriod", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Period DataPeriod
            {
                get { return _DataPeriod; }
                set { _DataPeriod = value; OnPropertyChanged("DataPeriod"); }
            }
            
            private Hl7.Fhir.Model.Period _DataPeriod;
            
            /// <summary>
            /// Data controlled by this rule
            /// </summary>
            [FhirElement("data", InSummary=true, Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Consent.provisionDataComponent> Data
            {
                get { if(_Data==null) _Data = new List<Hl7.Fhir.Model.Consent.provisionDataComponent>(); return _Data; }
                set { _Data = value; OnPropertyChanged("Data"); }
            }
            
            private List<Hl7.Fhir.Model.Consent.provisionDataComponent> _Data;
            
            /// <summary>
            /// Nested Exception Rules
            /// </summary>
            [FhirElement("provision", Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Consent.provisionComponent> Provision
            {
                get { if(_Provision==null) _Provision = new List<Hl7.Fhir.Model.Consent.provisionComponent>(); return _Provision; }
                set { _Provision = value; OnPropertyChanged("Provision"); }
            }
            
            private List<Hl7.Fhir.Model.Consent.provisionComponent> _Provision;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as provisionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Consent.ConsentProvisionType>)TypeElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Actor != null) dest.Actor = new List<Hl7.Fhir.Model.Consent.provisionActorComponent>(Actor.DeepCopy());
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.CodeableConcept>(Action.DeepCopy());
                    if(SecurityLabel != null) dest.SecurityLabel = new List<Hl7.Fhir.Model.Coding>(SecurityLabel.DeepCopy());
                    if(Purpose != null) dest.Purpose = new List<Hl7.Fhir.Model.Coding>(Purpose.DeepCopy());
                    if(Class != null) dest.Class = new List<Hl7.Fhir.Model.Coding>(Class.DeepCopy());
                    if(Code != null) dest.Code = new List<Hl7.Fhir.Model.CodeableConcept>(Code.DeepCopy());
                    if(DataPeriod != null) dest.DataPeriod = (Hl7.Fhir.Model.Period)DataPeriod.DeepCopy();
                    if(Data != null) dest.Data = new List<Hl7.Fhir.Model.Consent.provisionDataComponent>(Data.DeepCopy());
                    if(Provision != null) dest.Provision = new List<Hl7.Fhir.Model.Consent.provisionComponent>(Provision.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new provisionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as provisionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                if( !DeepComparable.Matches(SecurityLabel, otherT.SecurityLabel)) return false;
                if( !DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
                if( !DeepComparable.Matches(Class, otherT.Class)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(DataPeriod, otherT.DataPeriod)) return false;
                if( !DeepComparable.Matches(Data, otherT.Data)) return false;
                if( !DeepComparable.Matches(Provision, otherT.Provision)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as provisionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                if( !DeepComparable.IsExactly(SecurityLabel, otherT.SecurityLabel)) return false;
                if( !DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
                if( !DeepComparable.IsExactly(Class, otherT.Class)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(DataPeriod, otherT.DataPeriod)) return false;
                if( !DeepComparable.IsExactly(Data, otherT.Data)) return false;
                if( !DeepComparable.IsExactly(Provision, otherT.Provision)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (Period != null) yield return Period;
                    foreach (var elem in Actor) { if (elem != null) yield return elem; }
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                    foreach (var elem in SecurityLabel) { if (elem != null) yield return elem; }
                    foreach (var elem in Purpose) { if (elem != null) yield return elem; }
                    foreach (var elem in Class) { if (elem != null) yield return elem; }
                    foreach (var elem in Code) { if (elem != null) yield return elem; }
                    if (DataPeriod != null) yield return DataPeriod;
                    foreach (var elem in Data) { if (elem != null) yield return elem; }
                    foreach (var elem in Provision) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (Period != null) yield return new ElementValue("period", Period);
                    foreach (var elem in Actor) { if (elem != null) yield return new ElementValue("actor", elem); }
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                    foreach (var elem in SecurityLabel) { if (elem != null) yield return new ElementValue("securityLabel", elem); }
                    foreach (var elem in Purpose) { if (elem != null) yield return new ElementValue("purpose", elem); }
                    foreach (var elem in Class) { if (elem != null) yield return new ElementValue("class", elem); }
                    foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                    if (DataPeriod != null) yield return new ElementValue("dataPeriod", DataPeriod);
                    foreach (var elem in Data) { if (elem != null) yield return new ElementValue("data", elem); }
                    foreach (var elem in Provision) { if (elem != null) yield return new ElementValue("provision", elem); }
                }
            }

            
        }
        
        
        [FhirType("provisionActorComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class provisionActorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "provisionActorComponent"; } }
            
            /// <summary>
            /// How the actor is involved
            /// </summary>
            [FhirElement("role", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            /// <summary>
            /// Resource for the actor (or group, by role)
            /// </summary>
            [FhirElement("reference", Order=50)]
            [CLSCompliant(false)]
			[References("Device","Group","CareTeam","Organization","Patient","Practitioner","RelatedPerson","PractitionerRole")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Reference;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as provisionActorComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new provisionActorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as provisionActorComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as provisionActorComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Role != null) yield return Role;
                    if (Reference != null) yield return Reference;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Role != null) yield return new ElementValue("role", Role);
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                }
            }

            
        }
        
        
        [FhirType("provisionDataComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class provisionDataComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "provisionDataComponent"; } }
            
            /// <summary>
            /// instance | related | dependents | authoredby
            /// </summary>
            [FhirElement("meaning", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Consent.ConsentDataMeaning> MeaningElement
            {
                get { return _MeaningElement; }
                set { _MeaningElement = value; OnPropertyChanged("MeaningElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Consent.ConsentDataMeaning> _MeaningElement;
            
            /// <summary>
            /// instance | related | dependents | authoredby
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Consent.ConsentDataMeaning? Meaning
            {
                get { return MeaningElement != null ? MeaningElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        MeaningElement = null; 
                    else
                        MeaningElement = new Code<Hl7.Fhir.Model.Consent.ConsentDataMeaning>(value);
                    OnPropertyChanged("Meaning");
                }
            }
            
            /// <summary>
            /// The actual data reference
            /// </summary>
            [FhirElement("reference", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References()]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Reference;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as provisionDataComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(MeaningElement != null) dest.MeaningElement = (Code<Hl7.Fhir.Model.Consent.ConsentDataMeaning>)MeaningElement.DeepCopy();
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new provisionDataComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as provisionDataComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(MeaningElement, otherT.MeaningElement)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as provisionDataComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(MeaningElement, otherT.MeaningElement)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (MeaningElement != null) yield return MeaningElement;
                    if (Reference != null) yield return Reference;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (MeaningElement != null) yield return new ElementValue("meaning", MeaningElement);
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                }
            }

            
        }
        
        
        /// <summary>
        /// Identifier for this record (external references)
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// draft | proposed | active | rejected | inactive | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Consent.ConsentState> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Consent.ConsentState> _StatusElement;
        
        /// <summary>
        /// draft | proposed | active | rejected | inactive | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Consent.ConsentState? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Consent.ConsentState>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Which of the four areas this resource covers (extensible)
        /// </summary>
        [FhirElement("scope", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Scope
        {
            get { return _Scope; }
            set { _Scope = value; OnPropertyChanged("Scope"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Scope;
        
        /// <summary>
        /// Classification of the consent statement - for indexing/retrieval
        /// </summary>
        [FhirElement("category", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// Who the consent applies to
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// When this Consent was created or indexed
        /// </summary>
        [FhirElement("dateTime", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateTimeElement
        {
            get { return _DateTimeElement; }
            set { _DateTimeElement = value; OnPropertyChanged("DateTimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateTimeElement;
        
        /// <summary>
        /// When this Consent was created or indexed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DateTime
        {
            get { return DateTimeElement != null ? DateTimeElement.Value : null; }
            set
            {
                if (value == null)
                  DateTimeElement = null; 
                else
                  DateTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("DateTime");
            }
        }
        
        /// <summary>
        /// Who is agreeing to the policy and rules
        /// </summary>
        [FhirElement("performer", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Organization","Patient","Practitioner","RelatedPerson","PractitionerRole")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Performer
        {
            get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Performer;
        
        /// <summary>
        /// Custodian of the consent
        /// </summary>
        [FhirElement("organization", InSummary=true, Order=160)]
        [CLSCompliant(false)]
		[References("Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Organization
        {
            get { if(_Organization==null) _Organization = new List<Hl7.Fhir.Model.ResourceReference>(); return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Organization;
        
        /// <summary>
        /// Source from which this consent is taken
        /// </summary>
        [FhirElement("source", InSummary=true, Order=170, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private Hl7.Fhir.Model.Element _Source;
        
        /// <summary>
        /// Policies covered by this consent
        /// </summary>
        [FhirElement("policy", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Consent.PolicyComponent> Policy
        {
            get { if(_Policy==null) _Policy = new List<Hl7.Fhir.Model.Consent.PolicyComponent>(); return _Policy; }
            set { _Policy = value; OnPropertyChanged("Policy"); }
        }
        
        private List<Hl7.Fhir.Model.Consent.PolicyComponent> _Policy;
        
        /// <summary>
        /// Regulation that this consents to
        /// </summary>
        [FhirElement("policyRule", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept PolicyRule
        {
            get { return _PolicyRule; }
            set { _PolicyRule = value; OnPropertyChanged("PolicyRule"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _PolicyRule;
        
        /// <summary>
        /// Consent Verified by patient or family
        /// </summary>
        [FhirElement("verification", InSummary=true, Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Consent.VerificationComponent> Verification
        {
            get { if(_Verification==null) _Verification = new List<Hl7.Fhir.Model.Consent.VerificationComponent>(); return _Verification; }
            set { _Verification = value; OnPropertyChanged("Verification"); }
        }
        
        private List<Hl7.Fhir.Model.Consent.VerificationComponent> _Verification;
        
        /// <summary>
        /// Constraints to the base Consent.policyRule
        /// </summary>
        [FhirElement("provision", InSummary=true, Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Consent.provisionComponent Provision
        {
            get { return _Provision; }
            set { _Provision = value; OnPropertyChanged("Provision"); }
        }
        
        private Hl7.Fhir.Model.Consent.provisionComponent _Provision;
        

        public static ElementDefinition.ConstraintComponent Consent_PPC_4 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "patient.exists() or scope.coding.where(system='something' and code='adr').exists().not()",
            Key = "ppc-4",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "IF Scope=adr, there must be a patient",
            Xpath = "exists(f:patient) or not(exists(f:scope/f:coding[f:system/@value='something' and f:code/@value='adr'])))"
        };

        public static ElementDefinition.ConstraintComponent Consent_PPC_5 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "patient.exists() or scope.coding.where(system='something' and code='treatment').exists().not()",
            Key = "ppc-5",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "IF Scope=treatment, there must be a patient",
            Xpath = "exists(f:patient) or not(exists(f:scope/f:coding[f:system/@value='something' and f:code/@value='treatment'])))"
        };

        public static ElementDefinition.ConstraintComponent Consent_PPC_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "patient.exists() or scope.coding.where(system='something' and code='patient-privacy').exists().not()",
            Key = "ppc-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "IF Scope=privacy, there must be a patient",
            Xpath = "exists(f:patient) or not(exists(f:scope/f:coding[f:system/@value='something' and f:code/@value='patient-privacy'])))"
        };

        public static ElementDefinition.ConstraintComponent Consent_PPC_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "patient.exists() or scope.coding.where(system='something' and code='research').exists().not()",
            Key = "ppc-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "IF Scope=research, there must be a patient",
            Xpath = "exists(f:patient) or not(exists(f:scope/f:coding[f:system/@value='something' and f:code/@value='research'])))"
        };

        public static ElementDefinition.ConstraintComponent Consent_PPC_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "policy.exists() or policyRule.exists()",
            Key = "ppc-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Either a Policy or PolicyRule",
            Xpath = "exists(f:policy) or exists(f:policyRule)"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(Consent_PPC_4);
            InvariantConstraints.Add(Consent_PPC_5);
            InvariantConstraints.Add(Consent_PPC_2);
            InvariantConstraints.Add(Consent_PPC_3);
            InvariantConstraints.Add(Consent_PPC_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Consent;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Consent.ConsentState>)StatusElement.DeepCopy();
                if(Scope != null) dest.Scope = (Hl7.Fhir.Model.CodeableConcept)Scope.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(DateTimeElement != null) dest.DateTimeElement = (Hl7.Fhir.Model.FhirDateTime)DateTimeElement.DeepCopy();
                if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ResourceReference>(Performer.DeepCopy());
                if(Organization != null) dest.Organization = new List<Hl7.Fhir.Model.ResourceReference>(Organization.DeepCopy());
                if(Source != null) dest.Source = (Hl7.Fhir.Model.Element)Source.DeepCopy();
                if(Policy != null) dest.Policy = new List<Hl7.Fhir.Model.Consent.PolicyComponent>(Policy.DeepCopy());
                if(PolicyRule != null) dest.PolicyRule = (Hl7.Fhir.Model.CodeableConcept)PolicyRule.DeepCopy();
                if(Verification != null) dest.Verification = new List<Hl7.Fhir.Model.Consent.VerificationComponent>(Verification.DeepCopy());
                if(Provision != null) dest.Provision = (Hl7.Fhir.Model.Consent.provisionComponent)Provision.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Consent());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Consent;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Scope, otherT.Scope)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(DateTimeElement, otherT.DateTimeElement)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Policy, otherT.Policy)) return false;
            if( !DeepComparable.Matches(PolicyRule, otherT.PolicyRule)) return false;
            if( !DeepComparable.Matches(Verification, otherT.Verification)) return false;
            if( !DeepComparable.Matches(Provision, otherT.Provision)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Consent;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Scope, otherT.Scope)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(DateTimeElement, otherT.DateTimeElement)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Policy, otherT.Policy)) return false;
            if( !DeepComparable.IsExactly(PolicyRule, otherT.PolicyRule)) return false;
            if( !DeepComparable.IsExactly(Verification, otherT.Verification)) return false;
            if( !DeepComparable.IsExactly(Provision, otherT.Provision)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (Scope != null) yield return Scope;
				foreach (var elem in Category) { if (elem != null) yield return elem; }
				if (Patient != null) yield return Patient;
				if (DateTimeElement != null) yield return DateTimeElement;
				foreach (var elem in Performer) { if (elem != null) yield return elem; }
				foreach (var elem in Organization) { if (elem != null) yield return elem; }
				if (Source != null) yield return Source;
				foreach (var elem in Policy) { if (elem != null) yield return elem; }
				if (PolicyRule != null) yield return PolicyRule;
				foreach (var elem in Verification) { if (elem != null) yield return elem; }
				if (Provision != null) yield return Provision;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Scope != null) yield return new ElementValue("scope", Scope);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (DateTimeElement != null) yield return new ElementValue("dateTime", DateTimeElement);
                foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                foreach (var elem in Organization) { if (elem != null) yield return new ElementValue("organization", elem); }
                if (Source != null) yield return new ElementValue("source", Source);
                foreach (var elem in Policy) { if (elem != null) yield return new ElementValue("policy", elem); }
                if (PolicyRule != null) yield return new ElementValue("policyRule", PolicyRule);
                foreach (var elem in Verification) { if (elem != null) yield return new ElementValue("verification", elem); }
                if (Provision != null) yield return new ElementValue("provision", Provision);
            }
        }

    }
    
}
