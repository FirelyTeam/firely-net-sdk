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
// Generated on Fri, Jan 24, 2014 09:44-0600 for FHIR v0.12
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Prescription of medication to for patient
    /// </summary>
    [FhirType("MedicationPrescription", IsResource=true)]
    [DataContract]
    public partial class MedicationPrescription : Hl7.Fhir.Model.Resource
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
        public partial class MedicationPrescriptionDosageInstructionComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Dosage instructions expressed as text
            /// </summary>
            [FhirElement("text", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Supplemental instructions - e.g. "with meals"
            /// </summary>
            [FhirElement("additionalInstructions", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AdditionalInstructions { get; set; }
            
            /// <summary>
            /// When medication should be administered
            /// </summary>
            [FhirElement("timing", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Schedule))]
            [DataMember]
            public Hl7.Fhir.Model.Element Timing { get; set; }
            
            /// <summary>
            /// Take "as needed" f(or x)
            /// </summary>
            [FhirElement("asNeeded", Order=70, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [DataMember]
            public Hl7.Fhir.Model.Element AsNeeded { get; set; }
            
            /// <summary>
            /// Body site to administer to
            /// </summary>
            [FhirElement("site", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Site { get; set; }
            
            /// <summary>
            /// How drug should enter body
            /// </summary>
            [FhirElement("route", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Route { get; set; }
            
            /// <summary>
            /// Technique for administering medication
            /// </summary>
            [FhirElement("method", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method { get; set; }
            
            /// <summary>
            /// Amount of medication per dose
            /// </summary>
            [FhirElement("doseQuantity", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity DoseQuantity { get; set; }
            
            /// <summary>
            /// Amount of medication per unit of time
            /// </summary>
            [FhirElement("rate", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Rate { get; set; }
            
            /// <summary>
            /// Upper limit on medication per unit of time
            /// </summary>
            [FhirElement("maxDosePerPeriod", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio MaxDosePerPeriod { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationPrescriptionSubstitutionComponent")]
        [DataContract]
        public partial class MedicationPrescriptionSubstitutionComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// generic | formulary +
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
            
            /// <summary>
            /// Why should substitution (not) be made
            /// </summary>
            [FhirElement("reason", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Reason { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationPrescriptionDispenseComponent")]
        [DataContract]
        public partial class MedicationPrescriptionDispenseComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Product to be supplied
            /// </summary>
            [FhirElement("medication", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Medication { get; set; }
            
            /// <summary>
            /// Time period supply is authorized for
            /// </summary>
            [FhirElement("validityPeriod", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Period ValidityPeriod { get; set; }
            
            /// <summary>
            /// # of refills authorized
            /// </summary>
            [FhirElement("numberOfRepeatsAllowed", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumberOfRepeatsAllowedElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Amount of medication to supply per dispense
            /// </summary>
            [FhirElement("quantity", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity { get; set; }
            
            /// <summary>
            /// Days supply per dispense
            /// </summary>
            [FhirElement("expectedSupplyDuration", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Duration ExpectedSupplyDuration { get; set; }
            
        }
        
        
        /// <summary>
        /// External identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// When prescription was authorized
        /// </summary>
        [FhirElement("dateWritten", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateWrittenElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// active | on hold | completed | entered in error | stopped | superceded
        /// </summary>
        [FhirElement("status", Order=90)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionStatus> StatusElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Who prescription is for
        /// </summary>
        [FhirElement("patient", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient { get; set; }
        
        /// <summary>
        /// Who ordered the medication(s)
        /// </summary>
        [FhirElement("prescriber", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Prescriber { get; set; }
        
        /// <summary>
        /// Created during encounter / admission / stay
        /// </summary>
        [FhirElement("encounter", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter { get; set; }
        
        /// <summary>
        /// Reason or indication for writing the prescription
        /// </summary>
        [FhirElement("reason", Order=130, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Reason { get; set; }
        
        /// <summary>
        /// Medication to be taken
        /// </summary>
        [FhirElement("medication", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Medication { get; set; }
        
        /// <summary>
        /// How medication should be taken
        /// </summary>
        [FhirElement("dosageInstruction", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionDosageInstructionComponent> DosageInstruction { get; set; }
        
        /// <summary>
        /// Medication supply authorization
        /// </summary>
        [FhirElement("dispense", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionDispenseComponent Dispense { get; set; }
        
        /// <summary>
        /// Any restrictions on medication substitution?
        /// </summary>
        [FhirElement("substitution", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.MedicationPrescription.MedicationPrescriptionSubstitutionComponent Substitution { get; set; }
        
    }
    
}
