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
// Generated on Mon, Feb 3, 2014 11:56+0100 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Administration of medication to a patient
    /// </summary>
    [FhirType("MedicationAdministration", IsResource=true)]
    [DataContract]
    public partial class MedicationAdministration : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// A set of codes indicating the current status of a MedicationAdministration
        /// </summary>
        [FhirEnumeration("MedicationAdministrationStatus")]
        public enum MedicationAdministrationStatus
        {
            [EnumLiteral("in progress")]
            InProgress, // The administration has started but has not yet completed.
            [EnumLiteral("on hold")]
            OnHold, // Actions implied by the administration have been temporarily halted, but are expected to continue later. May also be called "suspended".
            [EnumLiteral("completed")]
            Completed, // All actions that are implied by the administration have occurred.
            [EnumLiteral("entered in error")]
            EnteredInError, // The administration was entered in error and therefore nullified.
            [EnumLiteral("stopped")]
            Stopped, // Actions implied by the administration have been permanently halted, before all of them occurred.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationAdministrationDosageComponent")]
        [DataContract]
        public partial class MedicationAdministrationDosageComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// When dose(s) were given
            /// </summary>
            [FhirElement("timing", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Timing { get; set; }
            
            /// <summary>
            /// Take "as needed" f(or x)
            /// </summary>
            [FhirElement("asNeeded", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [DataMember]
            public Hl7.Fhir.Model.Element AsNeeded { get; set; }
            
            /// <summary>
            /// Body site administered to
            /// </summary>
            [FhirElement("site", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Site { get; set; }
            
            /// <summary>
            /// Path of substance into body
            /// </summary>
            [FhirElement("route", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Route { get; set; }
            
            /// <summary>
            /// How drug was administered
            /// </summary>
            [FhirElement("method", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method { get; set; }
            
            /// <summary>
            /// Amount administered in one dose
            /// </summary>
            [FhirElement("quantity", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity { get; set; }
            
            /// <summary>
            /// Dose quantity per unit of time
            /// </summary>
            [FhirElement("rate", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Rate { get; set; }
            
            /// <summary>
            /// Total dose that was consumed per unit of time
            /// </summary>
            [FhirElement("maxDosePerPeriod", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio MaxDosePerPeriod { get; set; }
            
        }
        
        
        /// <summary>
        /// External identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// in progress | on hold | completed | entered in error | stopped
        /// </summary>
        [FhirElement("status", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MedicationAdministration.MedicationAdministrationStatus> StatusElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MedicationAdministration.MedicationAdministrationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.MedicationAdministration.MedicationAdministrationStatus>(value);
            }
        }
        
        /// <summary>
        /// Who received medication?
        /// </summary>
        [FhirElement("patient", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient { get; set; }
        
        /// <summary>
        /// Who administered substance?
        /// </summary>
        [FhirElement("practitioner", Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Practitioner { get; set; }
        
        /// <summary>
        /// Encounter administered as part of
        /// </summary>
        [FhirElement("encounter", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter { get; set; }
        
        /// <summary>
        /// Order administration performed against
        /// </summary>
        [FhirElement("prescription", Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Prescription { get; set; }
        
        /// <summary>
        /// True if medication not administered
        /// </summary>
        [FhirElement("wasNotGiven", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean WasNotGivenElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? WasNotGiven
        {
            get { return WasNotGivenElement != null ? WasNotGivenElement.Value : null; }
            set
            {
                if(value == null)
                  WasNotGivenElement = null; 
                else
                  WasNotGivenElement = new Hl7.Fhir.Model.FhirBoolean(value);
            }
        }
        
        /// <summary>
        /// Reason administration not performed
        /// </summary>
        [FhirElement("reasonNotGiven", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonNotGiven { get; set; }
        
        /// <summary>
        /// Start and end time of administration
        /// </summary>
        [FhirElement("whenGiven", Order=150)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Period WhenGiven { get; set; }
        
        /// <summary>
        /// What was administered?
        /// </summary>
        [FhirElement("medication", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Medication { get; set; }
        
        /// <summary>
        /// Device used to administer
        /// </summary>
        [FhirElement("device", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Device { get; set; }
        
        /// <summary>
        /// Medicine administration instructions to the patient/carer
        /// </summary>
        [FhirElement("dosage", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicationAdministration.MedicationAdministrationDosageComponent> Dosage { get; set; }
        
    }
    
}
