using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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
// Generated for FHIR v1.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information about a healthcare consumer’s consent statements
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
        /// Indicates the status of the consent
        /// (url: http://hl7.org/fhir/ValueSet/consent-status)
        /// </summary>
        [FhirEnumeration("ConsentStatus")]
        public enum ConsentStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-status)
            /// </summary>
            [EnumLiteral("draft"), Description("Pending")]
            Draft,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-status)
            /// </summary>
            [EnumLiteral("proposed"), Description("Proposed")]
            Proposed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-status)
            /// </summary>
            [EnumLiteral("active"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-status)
            /// </summary>
            [EnumLiteral("rejected"), Description("Rejected")]
            Rejected,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-status)
            /// </summary>
            [EnumLiteral("inactive"), Description("Inactive")]
            Inactive,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-status)
            /// </summary>
            [EnumLiteral("entered-in-error"), Description("Entered in Error")]
            EnteredInError,
        }

        /// <summary>
        /// How an exception is statement is applied, as adding additional consent, or removing consent
        /// (url: http://hl7.org/fhir/ValueSet/consent-except-type)
        /// </summary>
        [FhirEnumeration("ConsentExceptType")]
        public enum ConsentExceptType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-except-type)
            /// </summary>
            [EnumLiteral("deny"), Description("Opt Out")]
            Deny,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-except-type)
            /// </summary>
            [EnumLiteral("permit"), Description("Opt In")]
            Permit,
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
            [EnumLiteral("instance"), Description("Instance")]
            Instance,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-data-meaning)
            /// </summary>
            [EnumLiteral("related"), Description("Related")]
            Related,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/consent-data-meaning)
            /// </summary>
            [EnumLiteral("dependents"), Description("Dependents")]
            Dependents,
        }

        [FhirType("ExceptComponent")]
        [DataContract]
        public partial class ExceptComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
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
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.Consent.ConsentExceptType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Consent Exception Effective Time
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
            public List<Hl7.Fhir.Model.Consent.ActorComponent> Actor
            {
                get { if(_Actor==null) _Actor = new List<Hl7.Fhir.Model.Consent.ActorComponent>(); return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private List<Hl7.Fhir.Model.Consent.ActorComponent> _Actor;
            
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
            /// Security Labels for the operation/context
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
            /// Data controlled by this exception
            /// </summary>
            [FhirElement("data", InSummary=true, Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Consent.DataComponent> Data
            {
                get { if(_Data==null) _Data = new List<Hl7.Fhir.Model.Consent.DataComponent>(); return _Data; }
                set { _Data = value; OnPropertyChanged("Data"); }
            }
            
            private List<Hl7.Fhir.Model.Consent.DataComponent> _Data;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ExceptComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Consent.ConsentExceptType>)TypeElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Actor != null) dest.Actor = new List<Hl7.Fhir.Model.Consent.ActorComponent>(Actor.DeepCopy());
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.CodeableConcept>(Action.DeepCopy());
                    if(SecurityLabel != null) dest.SecurityLabel = new List<Hl7.Fhir.Model.Coding>(SecurityLabel.DeepCopy());
                    if(Purpose != null) dest.Purpose = new List<Hl7.Fhir.Model.Coding>(Purpose.DeepCopy());
                    if(Class != null) dest.Class = new List<Hl7.Fhir.Model.Coding>(Class.DeepCopy());
                    if(Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                    if(Data != null) dest.Data = new List<Hl7.Fhir.Model.Consent.DataComponent>(Data.DeepCopy());
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
                if( !DeepComparable.IsExactly(Data, otherT.Data)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ActorComponent")]
        [DataContract]
        public partial class ActorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ActorComponent"; } }
            
            /// <summary>
            /// How the actor is/was involved
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
            [References("Device","Group","Organization","Patient","Practitioner","RelatedPerson")]
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
            
        }
        
        
        [FhirType("DataComponent")]
        [DataContract]
        public partial class DataComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DataComponent"; } }
            
            /// <summary>
            /// instance | related | dependents
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
            /// instance | related | dependents
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Consent.ConsentDataMeaning? Meaning
            {
                get { return MeaningElement != null ? MeaningElement.Value : null; }
                set
                {
                    if(value == null)
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
        public Code<Hl7.Fhir.Model.Consent.ConsentStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Consent.ConsentStatus> _StatusElement;
        
        /// <summary>
        /// draft | proposed | active | rejected | inactive | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Consent.ConsentStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Consent.ConsentStatus>(value);
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
        /// When this Consent was created or indexed
        /// </summary>
        [FhirElement("dateTime", InSummary=true, Order=120)]
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
                if(value == null)
                  DateTimeElement = null; 
                else
                  DateTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("DateTime");
            }
        }
        
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
        /// Who the consent applies to
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=140)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Who made the consent statement
        /// </summary>
        [FhirElement("author", InSummary=true, Order=150)]
        [References("Organization","Patient","Practitioner","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// Organization that manages the consent
        /// </summary>
        [FhirElement("organization", InSummary=true, Order=160)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Organization
        {
            get { return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Organization;
        
        /// <summary>
        /// Source from which this consent is taken
        /// </summary>
        [FhirElement("source", InSummary=true, Order=170, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private Hl7.Fhir.Model.Element _Source;
        
        /// <summary>
        /// Policy that this consents to
        /// </summary>
        [FhirElement("policy", InSummary=true, Order=180)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri PolicyElement
        {
            get { return _PolicyElement; }
            set { _PolicyElement = value; OnPropertyChanged("PolicyElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _PolicyElement;
        
        /// <summary>
        /// Policy that this consents to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Policy
        {
            get { return PolicyElement != null ? PolicyElement.Value : null; }
            set
            {
                if(value == null)
                  PolicyElement = null; 
                else
                  PolicyElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Policy");
            }
        }
        
        /// <summary>
        /// Who|what the consent is in regard to
        /// </summary>
        [FhirElement("recipient", InSummary=true, Order=190)]
        [References("Device","Group","Organization","Patient","Practitioner","RelatedPerson")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Recipient
        {
            get { if(_Recipient==null) _Recipient = new List<Hl7.Fhir.Model.ResourceReference>(); return _Recipient; }
            set { _Recipient = value; OnPropertyChanged("Recipient"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Recipient;
        
        /// <summary>
        /// Consent Exception
        /// </summary>
        [FhirElement("except", InSummary=true, Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Consent.ExceptComponent> Except
        {
            get { if(_Except==null) _Except = new List<Hl7.Fhir.Model.Consent.ExceptComponent>(); return _Except; }
            set { _Except = value; OnPropertyChanged("Except"); }
        }
        
        private List<Hl7.Fhir.Model.Consent.ExceptComponent> _Except;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Consent;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Consent.ConsentStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(DateTimeElement != null) dest.DateTimeElement = (Hl7.Fhir.Model.FhirDateTime)DateTimeElement.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                if(Source != null) dest.Source = (Hl7.Fhir.Model.Element)Source.DeepCopy();
                if(PolicyElement != null) dest.PolicyElement = (Hl7.Fhir.Model.FhirUri)PolicyElement.DeepCopy();
                if(Recipient != null) dest.Recipient = new List<Hl7.Fhir.Model.ResourceReference>(Recipient.DeepCopy());
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
            if( !DeepComparable.Matches(DateTimeElement, otherT.DateTimeElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(PolicyElement, otherT.PolicyElement)) return false;
            if( !DeepComparable.Matches(Recipient, otherT.Recipient)) return false;
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
            if( !DeepComparable.IsExactly(DateTimeElement, otherT.DateTimeElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(PolicyElement, otherT.PolicyElement)) return false;
            if( !DeepComparable.IsExactly(Recipient, otherT.Recipient)) return false;
            if( !DeepComparable.IsExactly(Except, otherT.Except)) return false;
            
            return true;
        }
        
    }
    
}
