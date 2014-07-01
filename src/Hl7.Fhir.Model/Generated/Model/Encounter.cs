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
// Generated on Tue, Jul 1, 2014 16:22+0200 for FHIR v0.0.81
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// An interaction during which services are provided to the patient
    /// </summary>
    [FhirType("Encounter", IsResource=true)]
    [DataContract]
    public partial class Encounter : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Classification of the encounter
        /// </summary>
        [FhirEnumeration("EncounterClass")]
        public enum EncounterClass
        {
            /// <summary>
            /// An encounter during which the patient is hospitalized and stays overnight.
            /// </summary>
            [EnumLiteral("inpatient")]
            Inpatient,
            /// <summary>
            /// An encounter during which the patient is not hospitalized overnight.
            /// </summary>
            [EnumLiteral("outpatient")]
            Outpatient,
            /// <summary>
            /// An encounter where the patient visits the practitioner in his/her office, e.g. a G.P. visit.
            /// </summary>
            [EnumLiteral("ambulatory")]
            Ambulatory,
            /// <summary>
            /// An encounter where the patient needs urgent care.
            /// </summary>
            [EnumLiteral("emergency")]
            Emergency,
            /// <summary>
            /// An encounter where the practitioner visits the patient at his/her home.
            /// </summary>
            [EnumLiteral("home")]
            Home,
            /// <summary>
            /// An encounter taking place outside the regular environment for giving care.
            /// </summary>
            [EnumLiteral("field")]
            Field,
            /// <summary>
            /// An encounter where the patient needs more prolonged treatment or investigations than outpatients, but who do not need to stay in the hospital overnight.
            /// </summary>
            [EnumLiteral("daytime")]
            Daytime,
            /// <summary>
            /// An encounter that takes place where the patient and practitioner do not physically meet but use electronic means for contact.
            /// </summary>
            [EnumLiteral("virtual")]
            Virtual,
        }
        
        /// <summary>
        /// Current state of the encounter
        /// </summary>
        [FhirEnumeration("EncounterState")]
        public enum EncounterState
        {
            /// <summary>
            /// The Encounter has not yet started.
            /// </summary>
            [EnumLiteral("planned")]
            Planned,
            /// <summary>
            /// The Encounter has begun and the patient is present / the practitioner and the patient are meeting.
            /// </summary>
            [EnumLiteral("in progress")]
            InProgress,
            /// <summary>
            /// The Encounter has begun, but the patient is temporarily on leave.
            /// </summary>
            [EnumLiteral("onleave")]
            Onleave,
            /// <summary>
            /// The Encounter has ended.
            /// </summary>
            [EnumLiteral("finished")]
            Finished,
            /// <summary>
            /// The Encounter has ended before it has begun.
            /// </summary>
            [EnumLiteral("cancelled")]
            Cancelled,
        }
        
        [FhirType("EncounterHospitalizationComponent")]
        [DataContract]
        public partial class EncounterHospitalizationComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Pre-admission identifier
            /// </summary>
            [FhirElement("preAdmissionIdentifier", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier PreAdmissionIdentifier
            {
                get { return _PreAdmissionIdentifier; }
                set { _PreAdmissionIdentifier = value; OnPropertyChanged("PreAdmissionIdentifier"); }
            }
            private Hl7.Fhir.Model.Identifier _PreAdmissionIdentifier;
            
            /// <summary>
            /// The location from which the patient came before admission
            /// </summary>
            [FhirElement("origin", InSummary=true, Order=50)]
            [References("Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Origin
            {
                get { return _Origin; }
                set { _Origin = value; OnPropertyChanged("Origin"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Origin;
            
            /// <summary>
            /// From where patient was admitted (physician referral, transfer)
            /// </summary>
            [FhirElement("admitSource", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AdmitSource
            {
                get { return _AdmitSource; }
                set { _AdmitSource = value; OnPropertyChanged("AdmitSource"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _AdmitSource;
            
            /// <summary>
            /// Period during which the patient was admitted
            /// </summary>
            [FhirElement("period", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            private Hl7.Fhir.Model.Period _Period;
            
            /// <summary>
            /// Where the patient stays during this encounter
            /// </summary>
            [FhirElement("accomodation", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Encounter.EncounterHospitalizationAccomodationComponent> Accomodation
            {
                get { return _Accomodation; }
                set { _Accomodation = value; OnPropertyChanged("Accomodation"); }
            }
            private List<Hl7.Fhir.Model.Encounter.EncounterHospitalizationAccomodationComponent> _Accomodation;
            
            /// <summary>
            /// Dietary restrictions for the patient
            /// </summary>
            [FhirElement("diet", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Diet
            {
                get { return _Diet; }
                set { _Diet = value; OnPropertyChanged("Diet"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Diet;
            
            /// <summary>
            /// Special courtesies (VIP, board member)
            /// </summary>
            [FhirElement("specialCourtesy", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> SpecialCourtesy
            {
                get { return _SpecialCourtesy; }
                set { _SpecialCourtesy = value; OnPropertyChanged("SpecialCourtesy"); }
            }
            private List<Hl7.Fhir.Model.CodeableConcept> _SpecialCourtesy;
            
            /// <summary>
            /// Wheelchair, translator, stretcher, etc
            /// </summary>
            [FhirElement("specialArrangement", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> SpecialArrangement
            {
                get { return _SpecialArrangement; }
                set { _SpecialArrangement = value; OnPropertyChanged("SpecialArrangement"); }
            }
            private List<Hl7.Fhir.Model.CodeableConcept> _SpecialArrangement;
            
            /// <summary>
            /// Location to which the patient is discharged
            /// </summary>
            [FhirElement("destination", InSummary=true, Order=120)]
            [References("Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Destination
            {
                get { return _Destination; }
                set { _Destination = value; OnPropertyChanged("Destination"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Destination;
            
            /// <summary>
            /// Category or kind of location after discharge
            /// </summary>
            [FhirElement("dischargeDisposition", InSummary=true, Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DischargeDisposition
            {
                get { return _DischargeDisposition; }
                set { _DischargeDisposition = value; OnPropertyChanged("DischargeDisposition"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _DischargeDisposition;
            
            /// <summary>
            /// The final diagnosis given a patient before release from the hospital after all testing, surgery, and workup are complete
            /// </summary>
            [FhirElement("dischargeDiagnosis", InSummary=true, Order=140)]
            [References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference DischargeDiagnosis
            {
                get { return _DischargeDiagnosis; }
                set { _DischargeDiagnosis = value; OnPropertyChanged("DischargeDiagnosis"); }
            }
            private Hl7.Fhir.Model.ResourceReference _DischargeDiagnosis;
            
            /// <summary>
            /// Is this hospitalization a readmission?
            /// </summary>
            [FhirElement("reAdmission", InSummary=true, Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ReAdmissionElement
            {
                get { return _ReAdmissionElement; }
                set { _ReAdmissionElement = value; OnPropertyChanged("ReAdmissionElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _ReAdmissionElement;
            
            /// <summary>
            /// Is this hospitalization a readmission?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                    OnPropertyChanged("ReAdmission");
                }
            }
            
        }
        
        
        [FhirType("EncounterHospitalizationAccomodationComponent")]
        [DataContract]
        public partial class EncounterHospitalizationAccomodationComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// The bed that is assigned to the patient
            /// </summary>
            [FhirElement("bed", InSummary=true, Order=40)]
            [References("Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Bed
            {
                get { return _Bed; }
                set { _Bed = value; OnPropertyChanged("Bed"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Bed;
            
            /// <summary>
            /// Period during which the patient was assigned the bed
            /// </summary>
            [FhirElement("period", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            private Hl7.Fhir.Model.Period _Period;
            
        }
        
        
        [FhirType("EncounterLocationComponent")]
        [DataContract]
        public partial class EncounterLocationComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Location the encounter takes place
            /// </summary>
            [FhirElement("location", InSummary=true, Order=40)]
            [References("Location")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Location;
            
            /// <summary>
            /// Time period during which the patient was present at the location
            /// </summary>
            [FhirElement("period", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            private Hl7.Fhir.Model.Period _Period;
            
        }
        
        
        [FhirType("EncounterParticipantComponent")]
        [DataContract]
        public partial class EncounterParticipantComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Role of participant in encounter
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private List<Hl7.Fhir.Model.CodeableConcept> _Type;
            
            /// <summary>
            /// Persons involved in the encounter other than the patient
            /// </summary>
            [FhirElement("individual", InSummary=true, Order=50)]
            [References("Practitioner","RelatedPerson")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Individual
            {
                get { return _Individual; }
                set { _Individual = value; OnPropertyChanged("Individual"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Individual;
            
        }
        
        
        /// <summary>
        /// Identifier(s) by which this encounter is known
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// planned | in progress | onleave | finished | cancelled
        /// </summary>
        [FhirElement("status", InSummary=true, Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Encounter.EncounterState> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.Encounter.EncounterState> _StatusElement;
        
        /// <summary>
        /// planned | in progress | onleave | finished | cancelled
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// inpatient | outpatient | ambulatory | emergency +
        /// </summary>
        [FhirElement("class", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Encounter.EncounterClass> ClassElement
        {
            get { return _ClassElement; }
            set { _ClassElement = value; OnPropertyChanged("ClassElement"); }
        }
        private Code<Hl7.Fhir.Model.Encounter.EncounterClass> _ClassElement;
        
        /// <summary>
        /// inpatient | outpatient | ambulatory | emergency +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                OnPropertyChanged("Class");
            }
        }
        
        /// <summary>
        /// Specific type of encounter
        /// </summary>
        [FhirElement("type", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        private List<Hl7.Fhir.Model.CodeableConcept> _Type;
        
        /// <summary>
        /// The patient present at the encounter
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=110)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// List of participants involved in the encounter
        /// </summary>
        [FhirElement("participant", InSummary=true, Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Encounter.EncounterParticipantComponent> Participant
        {
            get { return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        private List<Hl7.Fhir.Model.Encounter.EncounterParticipantComponent> _Participant;
        
        /// <summary>
        /// The start and end time of the encounter
        /// </summary>
        [FhirElement("period", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Quantity of time the encounter lasted
        /// </summary>
        [FhirElement("length", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Duration Length
        {
            get { return _Length; }
            set { _Length = value; OnPropertyChanged("Length"); }
        }
        private Hl7.Fhir.Model.Duration _Length;
        
        /// <summary>
        /// Reason the encounter takes place (code)
        /// </summary>
        [FhirElement("reason", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Reason;
        
        /// <summary>
        /// Reason the encounter takes place (resource)
        /// </summary>
        [FhirElement("indication", Order=160)]
        [References()]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Indication
        {
            get { return _Indication; }
            set { _Indication = value; OnPropertyChanged("Indication"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Indication;
        
        /// <summary>
        /// Indicates the urgency of the encounter
        /// </summary>
        [FhirElement("priority", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Priority
        {
            get { return _Priority; }
            set { _Priority = value; OnPropertyChanged("Priority"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Priority;
        
        /// <summary>
        /// Details about an admission to a clinic
        /// </summary>
        [FhirElement("hospitalization", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Encounter.EncounterHospitalizationComponent Hospitalization
        {
            get { return _Hospitalization; }
            set { _Hospitalization = value; OnPropertyChanged("Hospitalization"); }
        }
        private Hl7.Fhir.Model.Encounter.EncounterHospitalizationComponent _Hospitalization;
        
        /// <summary>
        /// List of locations the patient has been at
        /// </summary>
        [FhirElement("location", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Encounter.EncounterLocationComponent> Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        private List<Hl7.Fhir.Model.Encounter.EncounterLocationComponent> _Location;
        
        /// <summary>
        /// Department or team providing care
        /// </summary>
        [FhirElement("serviceProvider", Order=200)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ServiceProvider
        {
            get { return _ServiceProvider; }
            set { _ServiceProvider = value; OnPropertyChanged("ServiceProvider"); }
        }
        private Hl7.Fhir.Model.ResourceReference _ServiceProvider;
        
        /// <summary>
        /// Another Encounter this encounter is part of
        /// </summary>
        [FhirElement("partOf", Order=210)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PartOf
        {
            get { return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        private Hl7.Fhir.Model.ResourceReference _PartOf;
        
    }
    
}
