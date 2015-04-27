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
        /// A code specifying the state of the dispense event.
        /// </summary>
        [FhirEnumeration("MedicationDispenseStatus")]
        public enum MedicationDispenseStatus
        {
            /// <summary>
            /// The dispense has started but has not yet completed.
            /// </summary>
            [EnumLiteral("in-progress")]
            InProgress,
            /// <summary>
            /// Actions implied by the administration have been temporarily halted, but are expected to continue later. May also be called "suspended".
            /// </summary>
            [EnumLiteral("on-hold")]
            OnHold,
            /// <summary>
            /// All actions that are implied by the dispense have occurred.
            /// </summary>
            [EnumLiteral("completed")]
            Completed,
            /// <summary>
            /// The dispense was entered in error and therefore nullified.
            /// </summary>
            [EnumLiteral("entered-in-error")]
            EnteredInError,
            /// <summary>
            /// Actions implied by the dispense have been permanently halted, before all of them occurred.
            /// </summary>
            [EnumLiteral("stopped")]
            Stopped,
        }
        
        [FhirType("MedicationDispenseSubstitutionComponent")]
        [DataContract]
        public partial class MedicationDispenseSubstitutionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "MedicationDispenseSubstitutionComponent"; } }
            
            /// <summary>
            /// Type of substitiution
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
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
            [FhirElement("reason", InSummary=true, Order=50)]
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
            [FhirElement("responsibleParty", InSummary=true, Order=60)]
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
                var dest = other as MedicationDispenseSubstitutionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
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
                return CopyTo(new MedicationDispenseSubstitutionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MedicationDispenseSubstitutionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
                if( !DeepComparable.Matches(ResponsibleParty, otherT.ResponsibleParty)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MedicationDispenseSubstitutionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
                if( !DeepComparable.IsExactly(ResponsibleParty, otherT.ResponsibleParty)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("MedicationDispenseDosageInstructionComponent")]
        [DataContract]
        public partial class MedicationDispenseDosageInstructionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "MedicationDispenseDosageInstructionComponent"; } }
            
            /// <summary>
            /// E.g. "Take with food"
            /// </summary>
            [FhirElement("additionalInstructions", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AdditionalInstructions
            {
                get { return _AdditionalInstructions; }
                set { _AdditionalInstructions = value; OnPropertyChanged("AdditionalInstructions"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _AdditionalInstructions;
            
            /// <summary>
            /// When medication should be administered
            /// </summary>
            [FhirElement("schedule", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Timing))]
            [DataMember]
            public Hl7.Fhir.Model.Element Schedule
            {
                get { return _Schedule; }
                set { _Schedule = value; OnPropertyChanged("Schedule"); }
            }
            
            private Hl7.Fhir.Model.Element _Schedule;
            
            /// <summary>
            /// Take "as needed" f(or x)
            /// </summary>
            [FhirElement("asNeeded", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [DataMember]
            public Hl7.Fhir.Model.Element AsNeeded
            {
                get { return _AsNeeded; }
                set { _AsNeeded = value; OnPropertyChanged("AsNeeded"); }
            }
            
            private Hl7.Fhir.Model.Element _AsNeeded;
            
            /// <summary>
            /// Body site to administer to
            /// </summary>
            [FhirElement("site", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Site
            {
                get { return _Site; }
                set { _Site = value; OnPropertyChanged("Site"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Site;
            
            /// <summary>
            /// How drug should enter body
            /// </summary>
            [FhirElement("route", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Route
            {
                get { return _Route; }
                set { _Route = value; OnPropertyChanged("Route"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Route;
            
            /// <summary>
            /// Technique for administering medication
            /// </summary>
            [FhirElement("method", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method
            {
                get { return _Method; }
                set { _Method = value; OnPropertyChanged("Method"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Method;
            
            /// <summary>
            /// Amount of medication per dose
            /// </summary>
            [FhirElement("dose", InSummary=true, Order=100, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Quantity))]
            [DataMember]
            public Hl7.Fhir.Model.Element Dose
            {
                get { return _Dose; }
                set { _Dose = value; OnPropertyChanged("Dose"); }
            }
            
            private Hl7.Fhir.Model.Element _Dose;
            
            /// <summary>
            /// Amount of medication per unit of time
            /// </summary>
            [FhirElement("rate", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Rate
            {
                get { return _Rate; }
                set { _Rate = value; OnPropertyChanged("Rate"); }
            }
            
            private Hl7.Fhir.Model.Ratio _Rate;
            
            /// <summary>
            /// Upper limit on medication per unit of time
            /// </summary>
            [FhirElement("maxDosePerPeriod", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio MaxDosePerPeriod
            {
                get { return _MaxDosePerPeriod; }
                set { _MaxDosePerPeriod = value; OnPropertyChanged("MaxDosePerPeriod"); }
            }
            
            private Hl7.Fhir.Model.Ratio _MaxDosePerPeriod;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MedicationDispenseDosageInstructionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AdditionalInstructions != null) dest.AdditionalInstructions = (Hl7.Fhir.Model.CodeableConcept)AdditionalInstructions.DeepCopy();
                    if(Schedule != null) dest.Schedule = (Hl7.Fhir.Model.Element)Schedule.DeepCopy();
                    if(AsNeeded != null) dest.AsNeeded = (Hl7.Fhir.Model.Element)AsNeeded.DeepCopy();
                    if(Site != null) dest.Site = (Hl7.Fhir.Model.CodeableConcept)Site.DeepCopy();
                    if(Route != null) dest.Route = (Hl7.Fhir.Model.CodeableConcept)Route.DeepCopy();
                    if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                    if(Dose != null) dest.Dose = (Hl7.Fhir.Model.Element)Dose.DeepCopy();
                    if(Rate != null) dest.Rate = (Hl7.Fhir.Model.Ratio)Rate.DeepCopy();
                    if(MaxDosePerPeriod != null) dest.MaxDosePerPeriod = (Hl7.Fhir.Model.Ratio)MaxDosePerPeriod.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MedicationDispenseDosageInstructionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MedicationDispenseDosageInstructionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AdditionalInstructions, otherT.AdditionalInstructions)) return false;
                if( !DeepComparable.Matches(Schedule, otherT.Schedule)) return false;
                if( !DeepComparable.Matches(AsNeeded, otherT.AsNeeded)) return false;
                if( !DeepComparable.Matches(Site, otherT.Site)) return false;
                if( !DeepComparable.Matches(Route, otherT.Route)) return false;
                if( !DeepComparable.Matches(Method, otherT.Method)) return false;
                if( !DeepComparable.Matches(Dose, otherT.Dose)) return false;
                if( !DeepComparable.Matches(Rate, otherT.Rate)) return false;
                if( !DeepComparable.Matches(MaxDosePerPeriod, otherT.MaxDosePerPeriod)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MedicationDispenseDosageInstructionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AdditionalInstructions, otherT.AdditionalInstructions)) return false;
                if( !DeepComparable.IsExactly(Schedule, otherT.Schedule)) return false;
                if( !DeepComparable.IsExactly(AsNeeded, otherT.AsNeeded)) return false;
                if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
                if( !DeepComparable.IsExactly(Route, otherT.Route)) return false;
                if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
                if( !DeepComparable.IsExactly(Dose, otherT.Dose)) return false;
                if( !DeepComparable.IsExactly(Rate, otherT.Rate)) return false;
                if( !DeepComparable.IsExactly(MaxDosePerPeriod, otherT.MaxDosePerPeriod)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External identifier
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// in-progress | on-hold | completed | entered-in-error | stopped
        /// </summary>
        [FhirElement("status", Order=100)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | on-hold | completed | entered-in-error | stopped
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Who the dispense is for
        /// </summary>
        [FhirElement("patient", Order=110)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Practitioner responsible for dispensing medication
        /// </summary>
        [FhirElement("dispenser", Order=120)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Dispenser
        {
            get { return _Dispenser; }
            set { _Dispenser = value; OnPropertyChanged("Dispenser"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Dispenser;
        
        /// <summary>
        /// Medication order that authorizes the dispense
        /// </summary>
        [FhirElement("authorizingPrescription", Order=130)]
        [References("MedicationPrescription")]
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
        [FhirElement("type", Order=140)]
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
        [FhirElement("quantity", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Hl7.Fhir.Model.Quantity _Quantity;
        
        /// <summary>
        /// Days Supply
        /// </summary>
        [FhirElement("daysSupply", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity DaysSupply
        {
            get { return _DaysSupply; }
            set { _DaysSupply = value; OnPropertyChanged("DaysSupply"); }
        }
        
        private Hl7.Fhir.Model.Quantity _DaysSupply;
        
        /// <summary>
        /// What medication was supplied
        /// </summary>
        [FhirElement("medication", Order=170)]
        [References("Medication")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Medication
        {
            get { return _Medication; }
            set { _Medication = value; OnPropertyChanged("Medication"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Medication;
        
        /// <summary>
        /// Dispense processing time
        /// </summary>
        [FhirElement("whenPrepared", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime WhenPreparedElement
        {
            get { return _WhenPreparedElement; }
            set { _WhenPreparedElement = value; OnPropertyChanged("WhenPreparedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _WhenPreparedElement;
        
        /// <summary>
        /// Dispense processing time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string WhenPrepared
        {
            get { return WhenPreparedElement != null ? WhenPreparedElement.Value : null; }
            set
            {
                if(value == null)
                  WhenPreparedElement = null; 
                else
                  WhenPreparedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("WhenPrepared");
            }
        }
        
        /// <summary>
        /// Handover time
        /// </summary>
        [FhirElement("whenHandedOver", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime WhenHandedOverElement
        {
            get { return _WhenHandedOverElement; }
            set { _WhenHandedOverElement = value; OnPropertyChanged("WhenHandedOverElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _WhenHandedOverElement;
        
        /// <summary>
        /// Handover time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string WhenHandedOver
        {
            get { return WhenHandedOverElement != null ? WhenHandedOverElement.Value : null; }
            set
            {
                if(value == null)
                  WhenHandedOverElement = null; 
                else
                  WhenHandedOverElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("WhenHandedOver");
            }
        }
        
        /// <summary>
        /// Where the medication was sent
        /// </summary>
        [FhirElement("destination", Order=200)]
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
        [FhirElement("receiver", Order=210)]
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
        [FhirElement("note", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NoteElement
        {
            get { return _NoteElement; }
            set { _NoteElement = value; OnPropertyChanged("NoteElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NoteElement;
        
        /// <summary>
        /// Information about the dispense
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Note
        {
            get { return NoteElement != null ? NoteElement.Value : null; }
            set
            {
                if(value == null)
                  NoteElement = null; 
                else
                  NoteElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Note");
            }
        }
        
        /// <summary>
        /// Medicine administration instructions to the patient/carer
        /// </summary>
        [FhirElement("dosageInstruction", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseDosageInstructionComponent> DosageInstruction
        {
            get { if(_DosageInstruction==null) _DosageInstruction = new List<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseDosageInstructionComponent>(); return _DosageInstruction; }
            set { _DosageInstruction = value; OnPropertyChanged("DosageInstruction"); }
        }
        
        private List<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseDosageInstructionComponent> _DosageInstruction;
        
        /// <summary>
        /// Deals with substitution of one medicine for another
        /// </summary>
        [FhirElement("substitution", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.MedicationDispense.MedicationDispenseSubstitutionComponent Substitution
        {
            get { return _Substitution; }
            set { _Substitution = value; OnPropertyChanged("Substitution"); }
        }
        
        private Hl7.Fhir.Model.MedicationDispense.MedicationDispenseSubstitutionComponent _Substitution;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicationDispense;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus>)StatusElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Dispenser != null) dest.Dispenser = (Hl7.Fhir.Model.ResourceReference)Dispenser.DeepCopy();
                if(AuthorizingPrescription != null) dest.AuthorizingPrescription = new List<Hl7.Fhir.Model.ResourceReference>(AuthorizingPrescription.DeepCopy());
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                if(DaysSupply != null) dest.DaysSupply = (Hl7.Fhir.Model.Quantity)DaysSupply.DeepCopy();
                if(Medication != null) dest.Medication = (Hl7.Fhir.Model.ResourceReference)Medication.DeepCopy();
                if(WhenPreparedElement != null) dest.WhenPreparedElement = (Hl7.Fhir.Model.FhirDateTime)WhenPreparedElement.DeepCopy();
                if(WhenHandedOverElement != null) dest.WhenHandedOverElement = (Hl7.Fhir.Model.FhirDateTime)WhenHandedOverElement.DeepCopy();
                if(Destination != null) dest.Destination = (Hl7.Fhir.Model.ResourceReference)Destination.DeepCopy();
                if(Receiver != null) dest.Receiver = new List<Hl7.Fhir.Model.ResourceReference>(Receiver.DeepCopy());
                if(NoteElement != null) dest.NoteElement = (Hl7.Fhir.Model.FhirString)NoteElement.DeepCopy();
                if(DosageInstruction != null) dest.DosageInstruction = new List<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseDosageInstructionComponent>(DosageInstruction.DeepCopy());
                if(Substitution != null) dest.Substitution = (Hl7.Fhir.Model.MedicationDispense.MedicationDispenseSubstitutionComponent)Substitution.DeepCopy();
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
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Dispenser, otherT.Dispenser)) return false;
            if( !DeepComparable.Matches(AuthorizingPrescription, otherT.AuthorizingPrescription)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(DaysSupply, otherT.DaysSupply)) return false;
            if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
            if( !DeepComparable.Matches(WhenPreparedElement, otherT.WhenPreparedElement)) return false;
            if( !DeepComparable.Matches(WhenHandedOverElement, otherT.WhenHandedOverElement)) return false;
            if( !DeepComparable.Matches(Destination, otherT.Destination)) return false;
            if( !DeepComparable.Matches(Receiver, otherT.Receiver)) return false;
            if( !DeepComparable.Matches(NoteElement, otherT.NoteElement)) return false;
            if( !DeepComparable.Matches(DosageInstruction, otherT.DosageInstruction)) return false;
            if( !DeepComparable.Matches(Substitution, otherT.Substitution)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicationDispense;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Dispenser, otherT.Dispenser)) return false;
            if( !DeepComparable.IsExactly(AuthorizingPrescription, otherT.AuthorizingPrescription)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(DaysSupply, otherT.DaysSupply)) return false;
            if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
            if( !DeepComparable.IsExactly(WhenPreparedElement, otherT.WhenPreparedElement)) return false;
            if( !DeepComparable.IsExactly(WhenHandedOverElement, otherT.WhenHandedOverElement)) return false;
            if( !DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
            if( !DeepComparable.IsExactly(Receiver, otherT.Receiver)) return false;
            if( !DeepComparable.IsExactly(NoteElement, otherT.NoteElement)) return false;
            if( !DeepComparable.IsExactly(DosageInstruction, otherT.DosageInstruction)) return false;
            if( !DeepComparable.IsExactly(Substitution, otherT.Substitution)) return false;
            
            return true;
        }
        
    }
    
}
