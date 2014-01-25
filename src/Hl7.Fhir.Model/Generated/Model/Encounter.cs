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
    /// An interaction during which services are provided to the patient
    /// </summary>
    [FhirType("Encounter", IsResource=true)]
    [DataContract]
    public partial class Encounter : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// Classification of the encounter
        /// </summary>
        [FhirEnumeration("EncounterClass")]
        public enum EncounterClass
        {
            [EnumLiteral("inpatient")]
            Inpatient, // An encounter during which the patient is hospitalized and stays overnight.
            [EnumLiteral("outpatient")]
            Outpatient, // An encounter during which the patient is not hospitalized overnight.
            [EnumLiteral("ambulatory")]
            Ambulatory, // An encounter where the patient visits the practitioner in his/her office, e.g. a G.P. visit.
            [EnumLiteral("emergency")]
            Emergency, // An encounter where the patient needs urgent care.
            [EnumLiteral("home")]
            Home, // An encounter where the practitioner visits the patient at his/her home.
            [EnumLiteral("field")]
            Field, // An encounter taking place outside the regular environment for giving care.
            [EnumLiteral("daytime")]
            Daytime, // An encounter where the patient needs more prolonged treatment or investigations than outpatients, but who do not need to stay in the hospital overnight.
            [EnumLiteral("virtual")]
            Virtual, // An encounter that takes place where the patient and practitioner do not physically meet but use electronic means for contact.
        }
        
        /// <summary>
        /// Current state of the encounter
        /// </summary>
        [FhirEnumeration("EncounterState")]
        public enum EncounterState
        {
            [EnumLiteral("planned")]
            Planned, // The Encounter has not yet started.
            [EnumLiteral("in progress")]
            InProgress, // The Encounter has begun and the patient is present / the practitioner and the patient are meeting.
            [EnumLiteral("onleave")]
            Onleave, // The Encounter has begun, but the patient is temporarily on leave.
            [EnumLiteral("finished")]
            Finished, // The Encounter has ended.
            [EnumLiteral("cancelled")]
            Cancelled, // The Encounter has ended before it has begun.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("EncounterHospitalizationComponent")]
        [DataContract]
        public partial class EncounterHospitalizationComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Pre-admission identifier
            /// </summary>
            [FhirElement("preAdmissionIdentifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier PreAdmissionIdentifier { get; set; }
            
            /// <summary>
            /// The location from which the patient came before admission
            /// </summary>
            [FhirElement("origin", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Origin { get; set; }
            
            /// <summary>
            /// From where patient was admitted (physician referral, transfer)
            /// </summary>
            [FhirElement("admitSource", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AdmitSource { get; set; }
            
            /// <summary>
            /// Period during which the patient was admitted
            /// </summary>
            [FhirElement("period", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period { get; set; }
            
            /// <summary>
            /// Where the patient stays during this encounter
            /// </summary>
            [FhirElement("accomodation", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Encounter.EncounterHospitalizationAccomodationComponent> Accomodation { get; set; }
            
            /// <summary>
            /// Dietary restrictions for the patient
            /// </summary>
            [FhirElement("diet", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Diet { get; set; }
            
            /// <summary>
            /// Special courtesies (VIP, board member)
            /// </summary>
            [FhirElement("specialCourtesy", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> SpecialCourtesy { get; set; }
            
            /// <summary>
            /// Wheelchair, translator, stretcher, etc
            /// </summary>
            [FhirElement("specialArrangement", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> SpecialArrangement { get; set; }
            
            /// <summary>
            /// Location to which the patient is discharged
            /// </summary>
            [FhirElement("destination", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Destination { get; set; }
            
            /// <summary>
            /// Category or kind of location after discharge
            /// </summary>
            [FhirElement("dischargeDisposition", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DischargeDisposition { get; set; }
            
            /// <summary>
            /// The final diagnosis given a patient before release from the hospital after all testing, surgery, and workup are complete
            /// </summary>
            [FhirElement("dischargeDiagnosis", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference DischargeDiagnosis { get; set; }
            
            /// <summary>
            /// Is this hospitalization a readmission?
            /// </summary>
            [FhirElement("reAdmission", Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ReAdmissionElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? ReAdmission
            {
                get { return ReAdmissionElement != null ? ReAdmissionElement.Value : null; }
                set
                {
                    if(value == null)
                      ReAdmissionElement = null; 
                    else
                      ReAdmissionElement = new Hl7.Fhir.Model.FhirBoolean(value);
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("EncounterHospitalizationAccomodationComponent")]
        [DataContract]
        public partial class EncounterHospitalizationAccomodationComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// The bed that is assigned to the patient
            /// </summary>
            [FhirElement("bed", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Bed { get; set; }
            
            /// <summary>
            /// Period during which the patient was assigned the bed
            /// </summary>
            [FhirElement("period", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("EncounterLocationComponent")]
        [DataContract]
        public partial class EncounterLocationComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Location the encounter takes place
            /// </summary>
            [FhirElement("location", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Location { get; set; }
            
            /// <summary>
            /// Time period during which the patient was present at the location
            /// </summary>
            [FhirElement("period", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("EncounterParticipantComponent")]
        [DataContract]
        public partial class EncounterParticipantComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Role of participant in encounter
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Type { get; set; }
            
            /// <summary>
            /// Persons involved in the encounter other than the patient
            /// </summary>
            [FhirElement("individual", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Individual { get; set; }
            
        }
        
        
        /// <summary>
        /// Identifier(s) by which this encounter is known
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// planned | in progress | onleave | finished | cancelled
        /// </summary>
        [FhirElement("status", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Encounter.EncounterState> StatusElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Encounter.EncounterState? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Encounter.EncounterState>(value);
            }
        }
        
        /// <summary>
        /// inpatient | outpatient | ambulatory | emergency +
        /// </summary>
        [FhirElement("class", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Encounter.EncounterClass> ClassElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Encounter.EncounterClass? Class
        {
            get { return ClassElement != null ? ClassElement.Value : null; }
            set
            {
                if(value == null)
                  ClassElement = null; 
                else
                  ClassElement = new Code<Hl7.Fhir.Model.Encounter.EncounterClass>(value);
            }
        }
        
        /// <summary>
        /// Specific type of encounter
        /// </summary>
        [FhirElement("type", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Type { get; set; }
        
        /// <summary>
        /// The patient present at the encounter
        /// </summary>
        [FhirElement("subject", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// List of participants involved in the encounter
        /// </summary>
        [FhirElement("participant", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Encounter.EncounterParticipantComponent> Participant { get; set; }
        
        /// <summary>
        /// The start and end time of the encounter
        /// </summary>
        [FhirElement("period", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period { get; set; }
        
        /// <summary>
        /// Quantity of time the encounter lasted
        /// </summary>
        [FhirElement("length", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Duration Length { get; set; }
        
        /// <summary>
        /// Reason the encounter takes place (code)
        /// </summary>
        [FhirElement("reason", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Reason { get; set; }
        
        /// <summary>
        /// Reason the encounter takes place (resource)
        /// </summary>
        [FhirElement("indication", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Indication { get; set; }
        
        /// <summary>
        /// Indicates the urgency of the encounter
        /// </summary>
        [FhirElement("priority", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Priority { get; set; }
        
        /// <summary>
        /// Details about an admission to a clinic
        /// </summary>
        [FhirElement("hospitalization", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Encounter.EncounterHospitalizationComponent Hospitalization { get; set; }
        
        /// <summary>
        /// List of locations the patient has been at
        /// </summary>
        [FhirElement("location", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Encounter.EncounterLocationComponent> Location { get; set; }
        
        /// <summary>
        /// Department or team providing care
        /// </summary>
        [FhirElement("serviceProvider", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ServiceProvider { get; set; }
        
        /// <summary>
        /// Another Encounter this encounter is part of
        /// </summary>
        [FhirElement("partOf", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PartOf { get; set; }
        
    }
    
}
