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
    /// A healthcare consumer's policy choices to permits or denies recipients or roles to perform actions for specific purposes and periods of time
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
        /// Indicates the state of the consent
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
        /// How a resource reference is interpreted when testing consent restrictions
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

        /// <summary>
        /// How an exception statement is applied, such as adding additional consent or removing consent
        /// (url: http://hl7.org/fhir/ValueSet/consent-except-type)
        /// </summary>
        [FhirEnumeration("ConsentExceptType")]
        public enum ConsentExceptType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-except-type)
            /// </summary>
            [EnumLiteral("deny", "http://hl7.org/fhir/consent-except-type"), Description("Opt Out")]
            Deny,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-except-type)
            /// </summary>
            [EnumLiteral("permit", "http://hl7.org/fhir/consent-except-type"), Description("Opt In")]
            Permit,
        }

        [FhirType("ActorComponent")]
        [DataContract]
        public partial class ActorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ActorComponent"; } }
            
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
			[References("Device","Group","CareTeam","Organization","Patient","Practitioner","RelatedPerson")]
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
                var dest = other as ActorComponent;
                
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
                return CopyTo(new ActorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActorComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActorComponent;
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
        
        
        [FhirType("PolicyComponent")]
        [DataContract]
        public partial class PolicyComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
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
        
        
        [FhirType("DataComponent")]
        [DataContract]
        public partial class DataComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "DataComponent"; } }
            
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
                var dest = other as DataComponent;
                
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
                return CopyTo(new DataComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DataComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(MeaningElement, otherT.MeaningElement)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DataComponent;
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
        
        
        [FhirType("ExceptComponent")]
        [DataContract]
        public partial class ExceptComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ExceptComponent"; } }
            
            /// <summary>
            /// deny | permit
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Consent.ConsentExceptType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Consent.ConsentExceptType> _TypeElement;
            
            /// <summary>
            /// deny | permit
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Consent.ConsentExceptType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.Consent.ConsentExceptType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Timeframe for this exception
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
            /// Who|what controlled by this exception (or group, by role)
            /// </summary>
            [FhirElement("actor", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Consent.ExceptActorComponent> Actor
            {
                get { if(_Actor==null) _Actor = new List<Hl7.Fhir.Model.Consent.ExceptActorComponent>(); return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private List<Hl7.Fhir.Model.Consent.ExceptActorComponent> _Actor;
            
            /// <summary>
            /// Actions controlled by this exception
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
            /// Context of activities covered by this exception
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
            /// e.g. Resource Type, Profile, or CDA etc
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
            /// e.g. LOINC or SNOMED CT code, etc in the content
            /// </summary>
            [FhirElement("code", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Code
            {
                get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.Coding>(); return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Code;
            
            /// <summary>
            /// Timeframe for data controlled by this exception
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
            /// Data controlled by this exception
            /// </summary>
            [FhirElement("data", InSummary=true, Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Consent.ExceptDataComponent> Data
            {
                get { if(_Data==null) _Data = new List<Hl7.Fhir.Model.Consent.ExceptDataComponent>(); return _Data; }
                set { _Data = value; OnPropertyChanged("Data"); }
            }
            
            private List<Hl7.Fhir.Model.Consent.ExceptDataComponent> _Data;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ExceptComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Consent.ConsentExceptType>)TypeElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Actor != null) dest.Actor = new List<Hl7.Fhir.Model.Consent.ExceptActorComponent>(Actor.DeepCopy());
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.CodeableConcept>(Action.DeepCopy());
                    if(SecurityLabel != null) dest.SecurityLabel = new List<Hl7.Fhir.Model.Coding>(SecurityLabel.DeepCopy());
                    if(Purpose != null) dest.Purpose = new List<Hl7.Fhir.Model.Coding>(Purpose.DeepCopy());
                    if(Class != null) dest.Class = new List<Hl7.Fhir.Model.Coding>(Class.DeepCopy());
                    if(Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                    if(DataPeriod != null) dest.DataPeriod = (Hl7.Fhir.Model.Period)DataPeriod.DeepCopy();
                    if(Data != null) dest.Data = new List<Hl7.Fhir.Model.Consent.ExceptDataComponent>(Data.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ExceptComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ExceptComponent;
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
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ExceptComponent;
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
                }
            }

            
        }
        
        
        [FhirType("ExceptActorComponent")]
        [DataContract]
        public partial class ExceptActorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ExceptActorComponent"; } }
            
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
			[References("Device","Group","CareTeam","Organization","Patient","Practitioner","RelatedPerson")]
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
                var dest = other as ExceptActorComponent;
                
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
                return CopyTo(new ExceptActorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ExceptActorComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ExceptActorComponent;
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
        
        
        [FhirType("ExceptDataComponent")]
        [DataContract]
        public partial class ExceptDataComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ExceptDataComponent"; } }
            
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
                var dest = other as ExceptDataComponent;
                
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
                return CopyTo(new ExceptDataComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ExceptDataComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(MeaningElement, otherT.MeaningElement)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ExceptDataComponent;
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
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
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
        /// Classification of the consent statement - for indexing/retrieval
        /// </summary>
        [FhirElement("category", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
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
        [FhirElement("patient", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Period that this consent applies
        /// </summary>
        [FhirElement("period", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
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
        /// Who is agreeing to the policy and exceptions
        /// </summary>
        [FhirElement("consentingParty", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Organization","Patient","Practitioner","RelatedPerson")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ConsentingParty
        {
            get { if(_ConsentingParty==null) _ConsentingParty = new List<Hl7.Fhir.Model.ResourceReference>(); return _ConsentingParty; }
            set { _ConsentingParty = value; OnPropertyChanged("ConsentingParty"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ConsentingParty;
        
        /// <summary>
        /// Who|what controlled by this consent (or group, by role)
        /// </summary>
        [FhirElement("actor", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Consent.ActorComponent> Actor
        {
            get { if(_Actor==null) _Actor = new List<Hl7.Fhir.Model.Consent.ActorComponent>(); return _Actor; }
            set { _Actor = value; OnPropertyChanged("Actor"); }
        }
        
        private List<Hl7.Fhir.Model.Consent.ActorComponent> _Actor;
        
        /// <summary>
        /// Actions controlled by this consent
        /// </summary>
        [FhirElement("action", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Action
        {
            get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Action; }
            set { _Action = value; OnPropertyChanged("Action"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Action;
        
        /// <summary>
        /// Custodian of the consent
        /// </summary>
        [FhirElement("organization", InSummary=true, Order=180)]
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
        [FhirElement("source", InSummary=true, Order=190, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
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
        [FhirElement("policy", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Consent.PolicyComponent> Policy
        {
            get { if(_Policy==null) _Policy = new List<Hl7.Fhir.Model.Consent.PolicyComponent>(); return _Policy; }
            set { _Policy = value; OnPropertyChanged("Policy"); }
        }
        
        private List<Hl7.Fhir.Model.Consent.PolicyComponent> _Policy;
        
        /// <summary>
        /// Policy that this consents to
        /// </summary>
        [FhirElement("policyRule", InSummary=true, Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri PolicyRuleElement
        {
            get { return _PolicyRuleElement; }
            set { _PolicyRuleElement = value; OnPropertyChanged("PolicyRuleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _PolicyRuleElement;
        
        /// <summary>
        /// Policy that this consents to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PolicyRule
        {
            get { return PolicyRuleElement != null ? PolicyRuleElement.Value : null; }
            set
            {
                if (value == null)
                  PolicyRuleElement = null; 
                else
                  PolicyRuleElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("PolicyRule");
            }
        }
        
        /// <summary>
        /// Security Labels that define affected resources
        /// </summary>
        [FhirElement("securityLabel", InSummary=true, Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> SecurityLabel
        {
            get { if(_SecurityLabel==null) _SecurityLabel = new List<Hl7.Fhir.Model.Coding>(); return _SecurityLabel; }
            set { _SecurityLabel = value; OnPropertyChanged("SecurityLabel"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _SecurityLabel;
        
        /// <summary>
        /// Context of activities for which the agreement is made
        /// </summary>
        [FhirElement("purpose", InSummary=true, Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Purpose
        {
            get { if(_Purpose==null) _Purpose = new List<Hl7.Fhir.Model.Coding>(); return _Purpose; }
            set { _Purpose = value; OnPropertyChanged("Purpose"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Purpose;
        
        /// <summary>
        /// Timeframe for data controlled by this consent
        /// </summary>
        [FhirElement("dataPeriod", InSummary=true, Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.Period DataPeriod
        {
            get { return _DataPeriod; }
            set { _DataPeriod = value; OnPropertyChanged("DataPeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _DataPeriod;
        
        /// <summary>
        /// Data controlled by this consent
        /// </summary>
        [FhirElement("data", InSummary=true, Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Consent.DataComponent> Data
        {
            get { if(_Data==null) _Data = new List<Hl7.Fhir.Model.Consent.DataComponent>(); return _Data; }
            set { _Data = value; OnPropertyChanged("Data"); }
        }
        
        private List<Hl7.Fhir.Model.Consent.DataComponent> _Data;
        
        /// <summary>
        /// Additional rule -  addition or removal of permissions
        /// </summary>
        [FhirElement("except", InSummary=true, Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Consent.ExceptComponent> Except
        {
            get { if(_Except==null) _Except = new List<Hl7.Fhir.Model.Consent.ExceptComponent>(); return _Except; }
            set { _Except = value; OnPropertyChanged("Except"); }
        }
        
        private List<Hl7.Fhir.Model.Consent.ExceptComponent> _Except;
        

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

            InvariantConstraints.Add(Consent_PPC_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Consent;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Consent.ConsentState>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(DateTimeElement != null) dest.DateTimeElement = (Hl7.Fhir.Model.FhirDateTime)DateTimeElement.DeepCopy();
                if(ConsentingParty != null) dest.ConsentingParty = new List<Hl7.Fhir.Model.ResourceReference>(ConsentingParty.DeepCopy());
                if(Actor != null) dest.Actor = new List<Hl7.Fhir.Model.Consent.ActorComponent>(Actor.DeepCopy());
                if(Action != null) dest.Action = new List<Hl7.Fhir.Model.CodeableConcept>(Action.DeepCopy());
                if(Organization != null) dest.Organization = new List<Hl7.Fhir.Model.ResourceReference>(Organization.DeepCopy());
                if(Source != null) dest.Source = (Hl7.Fhir.Model.Element)Source.DeepCopy();
                if(Policy != null) dest.Policy = new List<Hl7.Fhir.Model.Consent.PolicyComponent>(Policy.DeepCopy());
                if(PolicyRuleElement != null) dest.PolicyRuleElement = (Hl7.Fhir.Model.FhirUri)PolicyRuleElement.DeepCopy();
                if(SecurityLabel != null) dest.SecurityLabel = new List<Hl7.Fhir.Model.Coding>(SecurityLabel.DeepCopy());
                if(Purpose != null) dest.Purpose = new List<Hl7.Fhir.Model.Coding>(Purpose.DeepCopy());
                if(DataPeriod != null) dest.DataPeriod = (Hl7.Fhir.Model.Period)DataPeriod.DeepCopy();
                if(Data != null) dest.Data = new List<Hl7.Fhir.Model.Consent.DataComponent>(Data.DeepCopy());
                if(Except != null) dest.Except = new List<Hl7.Fhir.Model.Consent.ExceptComponent>(Except.DeepCopy());
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
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(DateTimeElement, otherT.DateTimeElement)) return false;
            if( !DeepComparable.Matches(ConsentingParty, otherT.ConsentingParty)) return false;
            if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
            if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Policy, otherT.Policy)) return false;
            if( !DeepComparable.Matches(PolicyRuleElement, otherT.PolicyRuleElement)) return false;
            if( !DeepComparable.Matches(SecurityLabel, otherT.SecurityLabel)) return false;
            if( !DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.Matches(DataPeriod, otherT.DataPeriod)) return false;
            if( !DeepComparable.Matches(Data, otherT.Data)) return false;
            if( !DeepComparable.Matches(Except, otherT.Except)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Consent;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(DateTimeElement, otherT.DateTimeElement)) return false;
            if( !DeepComparable.IsExactly(ConsentingParty, otherT.ConsentingParty)) return false;
            if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
            if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Policy, otherT.Policy)) return false;
            if( !DeepComparable.IsExactly(PolicyRuleElement, otherT.PolicyRuleElement)) return false;
            if( !DeepComparable.IsExactly(SecurityLabel, otherT.SecurityLabel)) return false;
            if( !DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.IsExactly(DataPeriod, otherT.DataPeriod)) return false;
            if( !DeepComparable.IsExactly(Data, otherT.Data)) return false;
            if( !DeepComparable.IsExactly(Except, otherT.Except)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Identifier != null) yield return Identifier;
				if (StatusElement != null) yield return StatusElement;
				foreach (var elem in Category) { if (elem != null) yield return elem; }
				if (Patient != null) yield return Patient;
				if (Period != null) yield return Period;
				if (DateTimeElement != null) yield return DateTimeElement;
				foreach (var elem in ConsentingParty) { if (elem != null) yield return elem; }
				foreach (var elem in Actor) { if (elem != null) yield return elem; }
				foreach (var elem in Action) { if (elem != null) yield return elem; }
				foreach (var elem in Organization) { if (elem != null) yield return elem; }
				if (Source != null) yield return Source;
				foreach (var elem in Policy) { if (elem != null) yield return elem; }
				if (PolicyRuleElement != null) yield return PolicyRuleElement;
				foreach (var elem in SecurityLabel) { if (elem != null) yield return elem; }
				foreach (var elem in Purpose) { if (elem != null) yield return elem; }
				if (DataPeriod != null) yield return DataPeriod;
				foreach (var elem in Data) { if (elem != null) yield return elem; }
				foreach (var elem in Except) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Period != null) yield return new ElementValue("period", Period);
                if (DateTimeElement != null) yield return new ElementValue("dateTime", DateTimeElement);
                foreach (var elem in ConsentingParty) { if (elem != null) yield return new ElementValue("consentingParty", elem); }
                foreach (var elem in Actor) { if (elem != null) yield return new ElementValue("actor", elem); }
                foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                foreach (var elem in Organization) { if (elem != null) yield return new ElementValue("organization", elem); }
                if (Source != null) yield return new ElementValue("source", Source);
                foreach (var elem in Policy) { if (elem != null) yield return new ElementValue("policy", elem); }
                if (PolicyRuleElement != null) yield return new ElementValue("policyRule", PolicyRuleElement);
                foreach (var elem in SecurityLabel) { if (elem != null) yield return new ElementValue("securityLabel", elem); }
                foreach (var elem in Purpose) { if (elem != null) yield return new ElementValue("purpose", elem); }
                if (DataPeriod != null) yield return new ElementValue("dataPeriod", DataPeriod);
                foreach (var elem in Data) { if (elem != null) yield return new ElementValue("data", elem); }
                foreach (var elem in Except) { if (elem != null) yield return new ElementValue("except", elem); }
            }
        }

    }
    
}
