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
    /// Dispensing a medication to a named patient
    /// </summary>
    [FhirType("MedicationDispense", IsResource=true)]
    [DataContract]
    public partial class MedicationDispense : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// A code specifying the state of the dispense event.
        /// </summary>
        [FhirEnumeration("MedicationDispenseStatus")]
        public enum MedicationDispenseStatus
        {
            [EnumLiteral("in progress")]
            InProgress, // The dispense has started but has not yet completed.
            [EnumLiteral("on hold")]
            OnHold, // Actions implied by the administration have been temporarily halted, but are expected to continue later. May also be called "suspended".
            [EnumLiteral("completed")]
            Completed, // All actions that are implied by the dispense have occurred.
            [EnumLiteral("entered in error")]
            EnteredInError, // The dispense was entered in error and therefore nullified.
            [EnumLiteral("stopped")]
            Stopped, // Actions implied by the dispense have been permanently halted, before all of them occurred.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationDispenseDispenseDosageComponent")]
        [DataContract]
        public partial class MedicationDispenseDispenseDosageComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// E.g. "Take with food"
            /// </summary>
            [FhirElement("additionalInstructions", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AdditionalInstructions { get; set; }
            
            /// <summary>
            /// When medication should be administered
            /// </summary>
            [FhirElement("timing", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Schedule))]
            [DataMember]
            public Hl7.Fhir.Model.Element Timing { get; set; }
            
            /// <summary>
            /// Take "as needed" f(or x)
            /// </summary>
            [FhirElement("asNeeded", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [DataMember]
            public Hl7.Fhir.Model.Element AsNeeded { get; set; }
            
            /// <summary>
            /// Body site to administer to
            /// </summary>
            [FhirElement("site", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Site { get; set; }
            
            /// <summary>
            /// How drug should enter body
            /// </summary>
            [FhirElement("route", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Route { get; set; }
            
            /// <summary>
            /// Technique for administering medication
            /// </summary>
            [FhirElement("method", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method { get; set; }
            
            /// <summary>
            /// Amount of medication per dose
            /// </summary>
            [FhirElement("quantity", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity { get; set; }
            
            /// <summary>
            /// Amount of medication per unit of time
            /// </summary>
            [FhirElement("rate", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Rate { get; set; }
            
            /// <summary>
            /// Upper limit on medication per unit of time
            /// </summary>
            [FhirElement("maxDosePerPeriod", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio MaxDosePerPeriod { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationDispenseSubstitutionComponent")]
        [DataContract]
        public partial class MedicationDispenseSubstitutionComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Type of substitiution
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
            
            /// <summary>
            /// Why was substitution made
            /// </summary>
            [FhirElement("reason", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Reason { get; set; }
            
            /// <summary>
            /// Who is responsible for the substitution
            /// </summary>
            [FhirElement("responsibleParty", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> ResponsibleParty { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationDispenseDispenseComponent")]
        [DataContract]
        public partial class MedicationDispenseDispenseComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// External identifier for individual item
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier { get; set; }
            
            /// <summary>
            /// in progress | on hold | completed | entered in error | stopped
            /// </summary>
            [FhirElement("status", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus> StatusElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Trial fill, partial fill, emergency fill, etc.
            /// </summary>
            [FhirElement("type", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
            
            /// <summary>
            /// Amount dispensed
            /// </summary>
            [FhirElement("quantity", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity { get; set; }
            
            /// <summary>
            /// What medication was supplied
            /// </summary>
            [FhirElement("medication", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Medication { get; set; }
            
            /// <summary>
            /// Dispense processing time
            /// </summary>
            [FhirElement("whenPrepared", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime WhenPreparedElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Handover time
            /// </summary>
            [FhirElement("whenHandedOver", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime WhenHandedOverElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Where the medication was sent
            /// </summary>
            [FhirElement("destination", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Destination { get; set; }
            
            /// <summary>
            /// Who collected the medication
            /// </summary>
            [FhirElement("receiver", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Receiver { get; set; }
            
            /// <summary>
            /// Medicine administration instructions to the patient/carer
            /// </summary>
            [FhirElement("dosage", Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseDispenseDosageComponent> Dosage { get; set; }
            
        }
        
        
        /// <summary>
        /// External identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier { get; set; }
        
        /// <summary>
        /// in progress | on hold | completed | entered in error | stopped
        /// </summary>
        [FhirElement("status", Order=80)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus> StatusElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Who the dispense is for
        /// </summary>
        [FhirElement("patient", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient { get; set; }
        
        /// <summary>
        /// Practitioner responsible for dispensing medication
        /// </summary>
        [FhirElement("dispenser", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Dispenser { get; set; }
        
        /// <summary>
        /// Medication order that authorizes the dispense
        /// </summary>
        [FhirElement("authorizingPrescription", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> AuthorizingPrescription { get; set; }
        
        /// <summary>
        /// Details for individual dispensed medicationdetails
        /// </summary>
        [FhirElement("dispense", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseDispenseComponent> Dispense { get; set; }
        
        /// <summary>
        /// Deals with substitution of one medicine for another
        /// </summary>
        [FhirElement("substitution", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.MedicationDispense.MedicationDispenseSubstitutionComponent Substitution { get; set; }
        
    }
    
}
