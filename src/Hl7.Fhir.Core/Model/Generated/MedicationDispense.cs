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
    /// Dispensing a medication to a named patient
    /// </summary>
    [FhirType("MedicationDispense", IsResource=true)]
    [DataContract]
    public partial class MedicationDispense : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicationDispense; } }
        [NotMapped]
        public override string TypeName { get { return "MedicationDispense"; } }
        
        /// <summary>
        /// A coded concept specifying the state of the dispense event.
        /// (url: http://hl7.org/fhir/ValueSet/medication-dispense-status)
        /// </summary>
        [FhirEnumeration("MedicationDispenseStatus")]
        public enum MedicationDispenseStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-dispense-status)
            /// </summary>
            [EnumLiteral("preparation", "http://hl7.org/fhir/medication-dispense-status"), Description("Preparation")]
            Preparation,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-dispense-status)
            /// </summary>
            [EnumLiteral("in-progress", "http://hl7.org/fhir/medication-dispense-status"), Description("In Progress")]
            InProgress,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-dispense-status)
            /// </summary>
            [EnumLiteral("on-hold", "http://hl7.org/fhir/medication-dispense-status"), Description("On Hold")]
            OnHold,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-dispense-status)
            /// </summary>
            [EnumLiteral("completed", "http://hl7.org/fhir/medication-dispense-status"), Description("Completed")]
            Completed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-dispense-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/medication-dispense-status"), Description("Entered in-Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-dispense-status)
            /// </summary>
            [EnumLiteral("stopped", "http://hl7.org/fhir/medication-dispense-status"), Description("Stopped")]
            Stopped,
        }

        [FhirType("PerformerComponent")]
        [DataContract]
        public partial class PerformerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "PerformerComponent"; } }
            
            /// <summary>
            /// Individual who was performing
            /// </summary>
            [FhirElement("actor", Order=40)]
            [CLSCompliant(false)]
			[References("Practitioner","Organization","Patient","Device","RelatedPerson")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Actor
            {
                get { return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Actor;
            
            /// <summary>
            /// Organization organization was acting for
            /// </summary>
            [FhirElement("onBehalfOf", Order=50)]
            [CLSCompliant(false)]
			[References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference OnBehalfOf
            {
                get { return _OnBehalfOf; }
                set { _OnBehalfOf = value; OnPropertyChanged("OnBehalfOf"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _OnBehalfOf;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PerformerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    if(OnBehalfOf != null) dest.OnBehalfOf = (Hl7.Fhir.Model.ResourceReference)OnBehalfOf.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PerformerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                if( !DeepComparable.Matches(OnBehalfOf, otherT.OnBehalfOf)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                if( !DeepComparable.IsExactly(OnBehalfOf, otherT.OnBehalfOf)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Actor != null) yield return Actor;
                    if (OnBehalfOf != null) yield return OnBehalfOf;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Actor != null) yield return new ElementValue("actor", Actor);
                    if (OnBehalfOf != null) yield return new ElementValue("onBehalfOf", OnBehalfOf);
                }
            }

            
        }
        
        
        [FhirType("SubstitutionComponent")]
        [DataContract]
        public partial class SubstitutionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "SubstitutionComponent"; } }
            
            /// <summary>
            /// Whether a substitution was or was not performed on the dispense
            /// </summary>
            [FhirElement("wasSubstituted", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean WasSubstitutedElement
            {
                get { return _WasSubstitutedElement; }
                set { _WasSubstitutedElement = value; OnPropertyChanged("WasSubstitutedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _WasSubstitutedElement;
            
            /// <summary>
            /// Whether a substitution was or was not performed on the dispense
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? WasSubstituted
            {
                get { return WasSubstitutedElement != null ? WasSubstitutedElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        WasSubstitutedElement = null; 
                    else
                        WasSubstitutedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("WasSubstituted");
                }
            }
            
            /// <summary>
            /// Code signifying whether a different drug was dispensed from what was prescribed
            /// </summary>
            [FhirElement("type", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Why was substitution made
            /// </summary>
            [FhirElement("reason", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Reason
            {
                get { if(_Reason==null) _Reason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Reason;
            
            /// <summary>
            /// Who is responsible for the substitution
            /// </summary>
            [FhirElement("responsibleParty", Order=70)]
            [CLSCompliant(false)]
			[References("Practitioner")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> ResponsibleParty
            {
                get { if(_ResponsibleParty==null) _ResponsibleParty = new List<Hl7.Fhir.Model.ResourceReference>(); return _ResponsibleParty; }
                set { _ResponsibleParty = value; OnPropertyChanged("ResponsibleParty"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _ResponsibleParty;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubstitutionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(WasSubstitutedElement != null) dest.WasSubstitutedElement = (Hl7.Fhir.Model.FhirBoolean)WasSubstitutedElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Reason != null) dest.Reason = new List<Hl7.Fhir.Model.CodeableConcept>(Reason.DeepCopy());
                    if(ResponsibleParty != null) dest.ResponsibleParty = new List<Hl7.Fhir.Model.ResourceReference>(ResponsibleParty.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SubstitutionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubstitutionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(WasSubstitutedElement, otherT.WasSubstitutedElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
                if( !DeepComparable.Matches(ResponsibleParty, otherT.ResponsibleParty)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubstitutionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(WasSubstitutedElement, otherT.WasSubstitutedElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
                if( !DeepComparable.IsExactly(ResponsibleParty, otherT.ResponsibleParty)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (WasSubstitutedElement != null) yield return WasSubstitutedElement;
                    if (Type != null) yield return Type;
                    foreach (var elem in Reason) { if (elem != null) yield return elem; }
                    foreach (var elem in ResponsibleParty) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (WasSubstitutedElement != null) yield return new ElementValue("wasSubstituted", WasSubstitutedElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in Reason) { if (elem != null) yield return new ElementValue("reason", elem); }
                    foreach (var elem in ResponsibleParty) { if (elem != null) yield return new ElementValue("responsibleParty", elem); }
                }
            }

            
        }
        
        
        /// <summary>
        /// External identifier
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Event that dispense is part of
        /// </summary>
        [FhirElement("partOf", Order=100)]
        [CLSCompliant(false)]
		[References("Procedure")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PartOf
        {
            get { if(_PartOf==null) _PartOf = new List<Hl7.Fhir.Model.ResourceReference>(); return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PartOf;
        
        /// <summary>
        /// preparation | in-progress | on-hold | completed | entered-in-error | stopped
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus> _StatusElement;
        
        /// <summary>
        /// preparation | in-progress | on-hold | completed | entered-in-error | stopped
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Type of medication dispense
        /// </summary>
        [FhirElement("category", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// What medication was supplied
        /// </summary>
        [FhirElement("medication", InSummary=true, Order=130, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Medication
        {
            get { return _Medication; }
            set { _Medication = value; OnPropertyChanged("Medication"); }
        }
        
        private Hl7.Fhir.Model.Element _Medication;
        
        /// <summary>
        /// Who the dispense is for
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References("Patient","Group")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Encounter / Episode associated with event
        /// </summary>
        [FhirElement("context", Order=150)]
        [CLSCompliant(false)]
		[References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// Information that supports the dispensing of the medication
        /// </summary>
        [FhirElement("supportingInformation", Order=160)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SupportingInformation
        {
            get { if(_SupportingInformation==null) _SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(); return _SupportingInformation; }
            set { _SupportingInformation = value; OnPropertyChanged("SupportingInformation"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SupportingInformation;
        
        /// <summary>
        /// Who performed event
        /// </summary>
        [FhirElement("performer", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicationDispense.PerformerComponent> Performer
        {
            get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.MedicationDispense.PerformerComponent>(); return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private List<Hl7.Fhir.Model.MedicationDispense.PerformerComponent> _Performer;
        
        /// <summary>
        /// Medication order that authorizes the dispense
        /// </summary>
        [FhirElement("authorizingPrescription", Order=180)]
        [CLSCompliant(false)]
		[References("MedicationRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> AuthorizingPrescription
        {
            get { if(_AuthorizingPrescription==null) _AuthorizingPrescription = new List<Hl7.Fhir.Model.ResourceReference>(); return _AuthorizingPrescription; }
            set { _AuthorizingPrescription = value; OnPropertyChanged("AuthorizingPrescription"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _AuthorizingPrescription;
        
        /// <summary>
        /// Trial fill, partial fill, emergency fill, etc.
        /// </summary>
        [FhirElement("type", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Amount dispensed
        /// </summary>
        [FhirElement("quantity", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.SimpleQuantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Hl7.Fhir.Model.SimpleQuantity _Quantity;
        
        /// <summary>
        /// Amount of medication expressed as a timing amount
        /// </summary>
        [FhirElement("daysSupply", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.SimpleQuantity DaysSupply
        {
            get { return _DaysSupply; }
            set { _DaysSupply = value; OnPropertyChanged("DaysSupply"); }
        }
        
        private Hl7.Fhir.Model.SimpleQuantity _DaysSupply;
        
        /// <summary>
        /// When product was packaged and reviewed
        /// </summary>
        [FhirElement("whenPrepared", InSummary=true, Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime WhenPreparedElement
        {
            get { return _WhenPreparedElement; }
            set { _WhenPreparedElement = value; OnPropertyChanged("WhenPreparedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _WhenPreparedElement;
        
        /// <summary>
        /// When product was packaged and reviewed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string WhenPrepared
        {
            get { return WhenPreparedElement != null ? WhenPreparedElement.Value : null; }
            set
            {
                if (value == null)
                  WhenPreparedElement = null; 
                else
                  WhenPreparedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("WhenPrepared");
            }
        }
        
        /// <summary>
        /// When product was given out
        /// </summary>
        [FhirElement("whenHandedOver", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime WhenHandedOverElement
        {
            get { return _WhenHandedOverElement; }
            set { _WhenHandedOverElement = value; OnPropertyChanged("WhenHandedOverElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _WhenHandedOverElement;
        
        /// <summary>
        /// When product was given out
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string WhenHandedOver
        {
            get { return WhenHandedOverElement != null ? WhenHandedOverElement.Value : null; }
            set
            {
                if (value == null)
                  WhenHandedOverElement = null; 
                else
                  WhenHandedOverElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("WhenHandedOver");
            }
        }
        
        /// <summary>
        /// Where the medication was sent
        /// </summary>
        [FhirElement("destination", Order=240)]
        [CLSCompliant(false)]
		[References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Destination
        {
            get { return _Destination; }
            set { _Destination = value; OnPropertyChanged("Destination"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Destination;
        
        /// <summary>
        /// Who collected the medication
        /// </summary>
        [FhirElement("receiver", Order=250)]
        [CLSCompliant(false)]
		[References("Patient","Practitioner")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Receiver
        {
            get { if(_Receiver==null) _Receiver = new List<Hl7.Fhir.Model.ResourceReference>(); return _Receiver; }
            set { _Receiver = value; OnPropertyChanged("Receiver"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Receiver;
        
        /// <summary>
        /// Information about the dispense
        /// </summary>
        [FhirElement("note", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// How the medication is to be used by the patient or administered by the caregiver
        /// </summary>
        [FhirElement("dosageInstruction", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Dosage> DosageInstruction
        {
            get { if(_DosageInstruction==null) _DosageInstruction = new List<Dosage>(); return _DosageInstruction; }
            set { _DosageInstruction = value; OnPropertyChanged("DosageInstruction"); }
        }
        
        private List<Dosage> _DosageInstruction;
        
        /// <summary>
        /// Whether a substitution was performed on the dispense
        /// </summary>
        [FhirElement("substitution", Order=280)]
        [DataMember]
        public Hl7.Fhir.Model.MedicationDispense.SubstitutionComponent Substitution
        {
            get { return _Substitution; }
            set { _Substitution = value; OnPropertyChanged("Substitution"); }
        }
        
        private Hl7.Fhir.Model.MedicationDispense.SubstitutionComponent _Substitution;
        
        /// <summary>
        /// Clinical issue with action
        /// </summary>
        [FhirElement("detectedIssue", Order=290)]
        [CLSCompliant(false)]
		[References("DetectedIssue")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> DetectedIssue
        {
            get { if(_DetectedIssue==null) _DetectedIssue = new List<Hl7.Fhir.Model.ResourceReference>(); return _DetectedIssue; }
            set { _DetectedIssue = value; OnPropertyChanged("DetectedIssue"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _DetectedIssue;
        
        /// <summary>
        /// Whether the dispense was or was not performed
        /// </summary>
        [FhirElement("notDone", Order=300)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean NotDoneElement
        {
            get { return _NotDoneElement; }
            set { _NotDoneElement = value; OnPropertyChanged("NotDoneElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _NotDoneElement;
        
        /// <summary>
        /// Whether the dispense was or was not performed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? NotDone
        {
            get { return NotDoneElement != null ? NotDoneElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  NotDoneElement = null; 
                else
                  NotDoneElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("NotDone");
            }
        }
        
        /// <summary>
        /// Why a dispense was not performed
        /// </summary>
        [FhirElement("notDoneReason", Order=310, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element NotDoneReason
        {
            get { return _NotDoneReason; }
            set { _NotDoneReason = value; OnPropertyChanged("NotDoneReason"); }
        }
        
        private Hl7.Fhir.Model.Element _NotDoneReason;
        
        /// <summary>
        /// A list of releveant lifecycle events
        /// </summary>
        [FhirElement("eventHistory", Order=320)]
        [CLSCompliant(false)]
		[References("Provenance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> EventHistory
        {
            get { if(_EventHistory==null) _EventHistory = new List<Hl7.Fhir.Model.ResourceReference>(); return _EventHistory; }
            set { _EventHistory = value; OnPropertyChanged("EventHistory"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _EventHistory;
        

        public static ElementDefinition.ConstraintComponent MedicationDispense_MDD_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "whenHandedOver.empty() or whenPrepared.empty() or whenHandedOver >= whenPrepared",
            Key = "mdd-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "whenHandedOver cannot be before whenPrepared",
            Xpath = "not(exists(f:whenHandedOver/@value)) or not(exists(f:whenPrepared/@value)) or ( f:whenHandedOver/@value >= f:whenPrepared/@value)"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(MedicationDispense_MDD_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicationDispense;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(Medication != null) dest.Medication = (Hl7.Fhir.Model.Element)Medication.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(SupportingInformation != null) dest.SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(SupportingInformation.DeepCopy());
                if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.MedicationDispense.PerformerComponent>(Performer.DeepCopy());
                if(AuthorizingPrescription != null) dest.AuthorizingPrescription = new List<Hl7.Fhir.Model.ResourceReference>(AuthorizingPrescription.DeepCopy());
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                if(DaysSupply != null) dest.DaysSupply = (Hl7.Fhir.Model.SimpleQuantity)DaysSupply.DeepCopy();
                if(WhenPreparedElement != null) dest.WhenPreparedElement = (Hl7.Fhir.Model.FhirDateTime)WhenPreparedElement.DeepCopy();
                if(WhenHandedOverElement != null) dest.WhenHandedOverElement = (Hl7.Fhir.Model.FhirDateTime)WhenHandedOverElement.DeepCopy();
                if(Destination != null) dest.Destination = (Hl7.Fhir.Model.ResourceReference)Destination.DeepCopy();
                if(Receiver != null) dest.Receiver = new List<Hl7.Fhir.Model.ResourceReference>(Receiver.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(DosageInstruction != null) dest.DosageInstruction = new List<Dosage>(DosageInstruction.DeepCopy());
                if(Substitution != null) dest.Substitution = (Hl7.Fhir.Model.MedicationDispense.SubstitutionComponent)Substitution.DeepCopy();
                if(DetectedIssue != null) dest.DetectedIssue = new List<Hl7.Fhir.Model.ResourceReference>(DetectedIssue.DeepCopy());
                if(NotDoneElement != null) dest.NotDoneElement = (Hl7.Fhir.Model.FhirBoolean)NotDoneElement.DeepCopy();
                if(NotDoneReason != null) dest.NotDoneReason = (Hl7.Fhir.Model.Element)NotDoneReason.DeepCopy();
                if(EventHistory != null) dest.EventHistory = new List<Hl7.Fhir.Model.ResourceReference>(EventHistory.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new MedicationDispense());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicationDispense;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(AuthorizingPrescription, otherT.AuthorizingPrescription)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(DaysSupply, otherT.DaysSupply)) return false;
            if( !DeepComparable.Matches(WhenPreparedElement, otherT.WhenPreparedElement)) return false;
            if( !DeepComparable.Matches(WhenHandedOverElement, otherT.WhenHandedOverElement)) return false;
            if( !DeepComparable.Matches(Destination, otherT.Destination)) return false;
            if( !DeepComparable.Matches(Receiver, otherT.Receiver)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(DosageInstruction, otherT.DosageInstruction)) return false;
            if( !DeepComparable.Matches(Substitution, otherT.Substitution)) return false;
            if( !DeepComparable.Matches(DetectedIssue, otherT.DetectedIssue)) return false;
            if( !DeepComparable.Matches(NotDoneElement, otherT.NotDoneElement)) return false;
            if( !DeepComparable.Matches(NotDoneReason, otherT.NotDoneReason)) return false;
            if( !DeepComparable.Matches(EventHistory, otherT.EventHistory)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicationDispense;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(AuthorizingPrescription, otherT.AuthorizingPrescription)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(DaysSupply, otherT.DaysSupply)) return false;
            if( !DeepComparable.IsExactly(WhenPreparedElement, otherT.WhenPreparedElement)) return false;
            if( !DeepComparable.IsExactly(WhenHandedOverElement, otherT.WhenHandedOverElement)) return false;
            if( !DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
            if( !DeepComparable.IsExactly(Receiver, otherT.Receiver)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(DosageInstruction, otherT.DosageInstruction)) return false;
            if( !DeepComparable.IsExactly(Substitution, otherT.Substitution)) return false;
            if( !DeepComparable.IsExactly(DetectedIssue, otherT.DetectedIssue)) return false;
            if( !DeepComparable.IsExactly(NotDoneElement, otherT.NotDoneElement)) return false;
            if( !DeepComparable.IsExactly(NotDoneReason, otherT.NotDoneReason)) return false;
            if( !DeepComparable.IsExactly(EventHistory, otherT.EventHistory)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				foreach (var elem in PartOf) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (Category != null) yield return Category;
				if (Medication != null) yield return Medication;
				if (Subject != null) yield return Subject;
				if (Context != null) yield return Context;
				foreach (var elem in SupportingInformation) { if (elem != null) yield return elem; }
				foreach (var elem in Performer) { if (elem != null) yield return elem; }
				foreach (var elem in AuthorizingPrescription) { if (elem != null) yield return elem; }
				if (Type != null) yield return Type;
				if (Quantity != null) yield return Quantity;
				if (DaysSupply != null) yield return DaysSupply;
				if (WhenPreparedElement != null) yield return WhenPreparedElement;
				if (WhenHandedOverElement != null) yield return WhenHandedOverElement;
				if (Destination != null) yield return Destination;
				foreach (var elem in Receiver) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
				foreach (var elem in DosageInstruction) { if (elem != null) yield return elem; }
				if (Substitution != null) yield return Substitution;
				foreach (var elem in DetectedIssue) { if (elem != null) yield return elem; }
				if (NotDoneElement != null) yield return NotDoneElement;
				if (NotDoneReason != null) yield return NotDoneReason;
				foreach (var elem in EventHistory) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in PartOf) { if (elem != null) yield return new ElementValue("partOf", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Category != null) yield return new ElementValue("category", Category);
                if (Medication != null) yield return new ElementValue("medication", Medication);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                foreach (var elem in SupportingInformation) { if (elem != null) yield return new ElementValue("supportingInformation", elem); }
                foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                foreach (var elem in AuthorizingPrescription) { if (elem != null) yield return new ElementValue("authorizingPrescription", elem); }
                if (Type != null) yield return new ElementValue("type", Type);
                if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                if (DaysSupply != null) yield return new ElementValue("daysSupply", DaysSupply);
                if (WhenPreparedElement != null) yield return new ElementValue("whenPrepared", WhenPreparedElement);
                if (WhenHandedOverElement != null) yield return new ElementValue("whenHandedOver", WhenHandedOverElement);
                if (Destination != null) yield return new ElementValue("destination", Destination);
                foreach (var elem in Receiver) { if (elem != null) yield return new ElementValue("receiver", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in DosageInstruction) { if (elem != null) yield return new ElementValue("dosageInstruction", elem); }
                if (Substitution != null) yield return new ElementValue("substitution", Substitution);
                foreach (var elem in DetectedIssue) { if (elem != null) yield return new ElementValue("detectedIssue", elem); }
                if (NotDoneElement != null) yield return new ElementValue("notDone", NotDoneElement);
                if (NotDoneReason != null) yield return new ElementValue("notDoneReason", NotDoneReason);
                foreach (var elem in EventHistory) { if (elem != null) yield return new ElementValue("eventHistory", elem); }
            }
        }

    }
    
}
