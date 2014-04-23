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
    /// Prescription of medication to for patient
    /// </summary>
    [FhirType("MedicationPrescription", IsResource=true)]
    [DataContract]
    public partial class MedicationPrescription : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// A code specifying the state of the prescribing event. Describes the lifecycle of the prescription.
        /// </summary>
        [FhirEnumeration("MedicationPrescriptionStatus")]
        public enum MedicationPrescriptionStatus
        {
            [EnumLiteral("active")]
            Active, // The prescription is 'actionable', but not all actions that are implied by it have occurred yet.
            [EnumLiteral("on hold")]
            OnHold, // Actions implied by the prescription have been temporarily halted, but are expected to continue later.  May also be called "suspended".
            [EnumLiteral("completed")]
            Completed, // All actions that are implied by the prescription have occurred (this will rarely be made explicit).
            [EnumLiteral("entered in error")]
            EnteredInError, // The prescription was entered in error and therefore nullified.
            [EnumLiteral("stopped")]
            Stopped, // Actions implied by the prescription have been permanently halted, before all of them occurred.
            [EnumLiteral("superceded")]
            Superceded, // The prescription was replaced by a newer one, which encompasses all the information in the previous one.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationPrescriptionDosageInstructionComponent")]
        [DataContract]
        public partial class MedicationPrescriptionDosageInstructionComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Dosage instructions expressed as text
            /// </summary>
            [FhirElement("text", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if(value == null)
                      TextElement = null; 
                    else
                      TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            /// <summary>
            /// Supplemental instructions - e.g. "with meals"
            /// </summary>
            [FhirElement("additionalInstructions", InSummary=true, Order=50)]
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
            [FhirElement("timing", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Schedule))]
            [DataMember]
            public Hl7.Fhir.Model.Element Timing
            {
                get { return _Timing; }
                set { _Timing = value; OnPropertyChanged("Timing"); }
            }
            private Hl7.Fhir.Model.Element _Timing;
            
            /// <summary>
            /// Take "as needed" f(or x)
            /// </summary>
            [FhirElement("asNeeded", InSummary=true, Order=70, Choice=ChoiceType.DatatypeChoice)]
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
            [FhirElement("site", InSummary=true, Order=80)]
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
            [FhirElement("route", InSummary=true, Order=90)]
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
            [FhirElement("method", InSummary=true, Order=100)]
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
            [FhirElement("doseQuantity", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity DoseQuantity
            {
                get { return _DoseQuantity; }
                set { _DoseQuantity = value; OnPropertyChanged("DoseQuantity"); }
            }
            private Hl7.Fhir.Model.Quantity _DoseQuantity;
            
            /// <summary>
            /// Amount of medication per unit of time
            /// </summary>
            [FhirElement("rate", InSummary=true, Order=120)]
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
            [FhirElement("maxDosePerPeriod", InSummary=true, Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio MaxDosePerPeriod
            {
                get { return _MaxDosePerPeriod; }
                set { _MaxDosePerPeriod = value; OnPropertyChanged("MaxDosePerPeriod"); }
            }
            private Hl7.Fhir.Model.Ratio _MaxDosePerPeriod;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationPrescriptionSubstitutionComponent")]
        [DataContract]
        public partial class MedicationPrescriptionSubstitutionComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// generic | formulary +
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
            /// Why should substitution (not) be made
            /// </summary>
            [FhirElement("reason", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Reason;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationPrescriptionDispenseComponent")]
        [DataContract]
        public partial class MedicationPrescriptionDispenseComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Product to be supplied
            /// </summary>
            [FhirElement("medication", InSummary=true, Order=40)]
            [References("Medication")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Medication
            {
                get { return _Medication; }
                set { _Medication = value; OnPropertyChanged("Medication"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Medication;
            
            /// <summary>
            /// Time period supply is authorized for
            /// </summary>
            [FhirElement("validityPeriod", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Period ValidityPeriod
            {
                get { return _ValidityPeriod; }
                set { _ValidityPeriod = value; OnPropertyChanged("ValidityPeriod"); }
            }
            private Hl7.Fhir.Model.Period _ValidityPeriod;
            
            /// <summary>
            /// # of refills authorized
            /// </summary>
            [FhirElement("numberOfRepeatsAllowed", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumberOfRepeatsAllowedElement
            {
                get { return _NumberOfRepeatsAllowedElement; }
                set { _NumberOfRepeatsAllowedElement = value; OnPropertyChanged("NumberOfRepeatsAllowedElement"); }
            }
            private Hl7.Fhir.Model.Integer _NumberOfRepeatsAllowedElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? NumberOfRepeatsAllowed
            {
                get { return NumberOfRepeatsAllowedElement != null ? NumberOfRepeatsAllowedElement.Value : null; }
                set
                {
                    if(value == null)
                      NumberOfRepeatsAllowedElement = null; 
                    else
                      NumberOfRepeatsAllowedElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("NumberOfRepeatsAllowed");
                }
            }
            
            /// <summary>
            /// Amount of medication to supply per dispense
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            private Hl7.Fhir.Model.Quantity _Quantity;
            
            /// <summary>
            /// Days supply per dispense
            /// </summary>
            [FhirElement("expectedSupplyDuration", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Duration ExpectedSupplyDuration
            {
                get { return _ExpectedSupplyDuration; }
                set { _ExpectedSupplyDuration = value; OnPropertyChanged("ExpectedSupplyDuration"); }
            }
            private Hl7.Fhir.Model.Duration _ExpectedSupplyDuration;
            
        }
        
        
        /// <summary>
        /// External identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// When prescription was authorized
        /// </summary>
        [FhirElement("dateWritten", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateWrittenElement
        {
            get { return _DateWrittenElement; }
            set { _DateWrittenElement = value; OnPropertyChanged("DateWrittenElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _DateWrittenElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DateWritten
        {
            get { return DateWrittenElement != null ? DateWrittenElement.Value : null; }
            set
            {
                if(value == null)
                  DateWrittenElement = null; 
                else
                  DateWrittenElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("DateWritten");
            }
        }
        
        /// <summary>
        /// active | on hold | completed | entered in error | stopped | superceded
        /// </summary>
        [FhirElement("status", Order=90)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionStatus> _StatusElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Who prescription is for
        /// </summary>
        [FhirElement("patient", Order=100)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Who ordered the medication(s)
        /// </summary>
        [FhirElement("prescriber", Order=110)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Prescriber
        {
            get { return _Prescriber; }
            set { _Prescriber = value; OnPropertyChanged("Prescriber"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Prescriber;
        
        /// <summary>
        /// Created during encounter / admission / stay
        /// </summary>
        [FhirElement("encounter", Order=120)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Reason or indication for writing the prescription
        /// </summary>
        [FhirElement("reason", Order=130, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        private Hl7.Fhir.Model.Element _Reason;
        
        /// <summary>
        /// Medication to be taken
        /// </summary>
        [FhirElement("medication", Order=140)]
        [References("Medication")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Medication
        {
            get { return _Medication; }
            set { _Medication = value; OnPropertyChanged("Medication"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Medication;
        
        /// <summary>
        /// How medication should be taken
        /// </summary>
        [FhirElement("dosageInstruction", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionDosageInstructionComponent> DosageInstruction
        {
            get { return _DosageInstruction; }
            set { _DosageInstruction = value; OnPropertyChanged("DosageInstruction"); }
        }
        private List<Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionDosageInstructionComponent> _DosageInstruction;
        
        /// <summary>
        /// Medication supply authorization
        /// </summary>
        [FhirElement("dispense", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionDispenseComponent Dispense
        {
            get { return _Dispense; }
            set { _Dispense = value; OnPropertyChanged("Dispense"); }
        }
        private Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionDispenseComponent _Dispense;
        
        /// <summary>
        /// Any restrictions on medication substitution?
        /// </summary>
        [FhirElement("substitution", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionSubstitutionComponent Substitution
        {
            get { return _Substitution; }
            set { _Substitution = value; OnPropertyChanged("Substitution"); }
        }
        private Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionSubstitutionComponent _Substitution;
        
    }
    
}
