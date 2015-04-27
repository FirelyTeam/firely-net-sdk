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
    /// An interaction during which services are provided to the patient
    /// </summary>
    [FhirType("Encounter", IsResource=true)]
    [DataContract]
    public partial class Encounter : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Encounter; } }
        [NotMapped]
        public override string TypeName { get { return "Encounter"; } }
        
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
            /// <summary>
            /// Any other encounter type that is not described by one of the other values. Where this is used it is expected that an implementer will include an extension value to define what the actual other type is.
            /// </summary>
            [EnumLiteral("other")]
            Other,
        }
        
        /// <summary>
        /// The status of the location
        /// </summary>
        [FhirEnumeration("EncounterLocationStatus")]
        public enum EncounterLocationStatus
        {
            /// <summary>
            /// The patient is planned to be moved to this location at some point in the future.
            /// </summary>
            [EnumLiteral("planned")]
            Planned,
            /// <summary>
            /// The patient is currently at this location, or was between the period specified.
            /// </summary>
            [EnumLiteral("present")]
            Present,
            /// <summary>
            /// This location is held empty for this patient.
            /// </summary>
            [EnumLiteral("reserved")]
            Reserved,
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
            /// The Patient is present for the encounter, however is not currently meeting with a practitioner.
            /// </summary>
            [EnumLiteral("arrived")]
            Arrived,
            /// <summary>
            /// The Encounter has begun and the patient is present / the practitioner and the patient are meeting.
            /// </summary>
            [EnumLiteral("in-progress")]
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
        public partial class EncounterHospitalizationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "EncounterHospitalizationComponent"; } }
            
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
            /// Diet preferences reported by the patient
            /// </summary>
            [FhirElement("dietPreference", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DietPreference
            {
                get { return _DietPreference; }
                set { _DietPreference = value; OnPropertyChanged("DietPreference"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _DietPreference;
            
            /// <summary>
            /// Special courtesies (VIP, board member)
            /// </summary>
            [FhirElement("specialCourtesy", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> SpecialCourtesy
            {
                get { if(_SpecialCourtesy==null) _SpecialCourtesy = new List<Hl7.Fhir.Model.CodeableConcept>(); return _SpecialCourtesy; }
                set { _SpecialCourtesy = value; OnPropertyChanged("SpecialCourtesy"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _SpecialCourtesy;
            
            /// <summary>
            /// Wheelchair, translator, stretcher, etc
            /// </summary>
            [FhirElement("specialArrangement", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> SpecialArrangement
            {
                get { if(_SpecialArrangement==null) _SpecialArrangement = new List<Hl7.Fhir.Model.CodeableConcept>(); return _SpecialArrangement; }
                set { _SpecialArrangement = value; OnPropertyChanged("SpecialArrangement"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _SpecialArrangement;
            
            /// <summary>
            /// Location to which the patient is discharged
            /// </summary>
            [FhirElement("destination", InSummary=true, Order=100)]
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
            [FhirElement("dischargeDisposition", InSummary=true, Order=110)]
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
            [FhirElement("dischargeDiagnosis", InSummary=true, Order=120)]
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
            [FhirElement("reAdmission", InSummary=true, Order=130)]
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EncounterHospitalizationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PreAdmissionIdentifier != null) dest.PreAdmissionIdentifier = (Hl7.Fhir.Model.Identifier)PreAdmissionIdentifier.DeepCopy();
                    if(Origin != null) dest.Origin = (Hl7.Fhir.Model.ResourceReference)Origin.DeepCopy();
                    if(AdmitSource != null) dest.AdmitSource = (Hl7.Fhir.Model.CodeableConcept)AdmitSource.DeepCopy();
                    if(DietPreference != null) dest.DietPreference = (Hl7.Fhir.Model.CodeableConcept)DietPreference.DeepCopy();
                    if(SpecialCourtesy != null) dest.SpecialCourtesy = new List<Hl7.Fhir.Model.CodeableConcept>(SpecialCourtesy.DeepCopy());
                    if(SpecialArrangement != null) dest.SpecialArrangement = new List<Hl7.Fhir.Model.CodeableConcept>(SpecialArrangement.DeepCopy());
                    if(Destination != null) dest.Destination = (Hl7.Fhir.Model.ResourceReference)Destination.DeepCopy();
                    if(DischargeDisposition != null) dest.DischargeDisposition = (Hl7.Fhir.Model.CodeableConcept)DischargeDisposition.DeepCopy();
                    if(DischargeDiagnosis != null) dest.DischargeDiagnosis = (Hl7.Fhir.Model.ResourceReference)DischargeDiagnosis.DeepCopy();
                    if(ReAdmissionElement != null) dest.ReAdmissionElement = (Hl7.Fhir.Model.FhirBoolean)ReAdmissionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new EncounterHospitalizationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EncounterHospitalizationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PreAdmissionIdentifier, otherT.PreAdmissionIdentifier)) return false;
                if( !DeepComparable.Matches(Origin, otherT.Origin)) return false;
                if( !DeepComparable.Matches(AdmitSource, otherT.AdmitSource)) return false;
                if( !DeepComparable.Matches(DietPreference, otherT.DietPreference)) return false;
                if( !DeepComparable.Matches(SpecialCourtesy, otherT.SpecialCourtesy)) return false;
                if( !DeepComparable.Matches(SpecialArrangement, otherT.SpecialArrangement)) return false;
                if( !DeepComparable.Matches(Destination, otherT.Destination)) return false;
                if( !DeepComparable.Matches(DischargeDisposition, otherT.DischargeDisposition)) return false;
                if( !DeepComparable.Matches(DischargeDiagnosis, otherT.DischargeDiagnosis)) return false;
                if( !DeepComparable.Matches(ReAdmissionElement, otherT.ReAdmissionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EncounterHospitalizationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PreAdmissionIdentifier, otherT.PreAdmissionIdentifier)) return false;
                if( !DeepComparable.IsExactly(Origin, otherT.Origin)) return false;
                if( !DeepComparable.IsExactly(AdmitSource, otherT.AdmitSource)) return false;
                if( !DeepComparable.IsExactly(DietPreference, otherT.DietPreference)) return false;
                if( !DeepComparable.IsExactly(SpecialCourtesy, otherT.SpecialCourtesy)) return false;
                if( !DeepComparable.IsExactly(SpecialArrangement, otherT.SpecialArrangement)) return false;
                if( !DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
                if( !DeepComparable.IsExactly(DischargeDisposition, otherT.DischargeDisposition)) return false;
                if( !DeepComparable.IsExactly(DischargeDiagnosis, otherT.DischargeDiagnosis)) return false;
                if( !DeepComparable.IsExactly(ReAdmissionElement, otherT.ReAdmissionElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("EncounterStatusHistoryComponent")]
        [DataContract]
        public partial class EncounterStatusHistoryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "EncounterStatusHistoryComponent"; } }
            
            /// <summary>
            /// planned | arrived | in-progress | onleave | finished | cancelled
            /// </summary>
            [FhirElement("status", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Encounter.EncounterState> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Encounter.EncounterState> _StatusElement;
            
            /// <summary>
            /// planned | arrived | in-progress | onleave | finished | cancelled
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
            /// The time that the episode was in the specified status
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EncounterStatusHistoryComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Encounter.EncounterState>)StatusElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new EncounterStatusHistoryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EncounterStatusHistoryComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EncounterStatusHistoryComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("EncounterLocationComponent")]
        [DataContract]
        public partial class EncounterLocationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "EncounterLocationComponent"; } }
            
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
            /// planned | present | reserved
            /// </summary>
            [FhirElement("status", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Encounter.EncounterLocationStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Encounter.EncounterLocationStatus> _StatusElement;
            
            /// <summary>
            /// planned | present | reserved
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Encounter.EncounterLocationStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if(value == null)
                      StatusElement = null; 
                    else
                      StatusElement = new Code<Hl7.Fhir.Model.Encounter.EncounterLocationStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// Time period during which the patient was present at the location
            /// </summary>
            [FhirElement("period", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EncounterLocationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Encounter.EncounterLocationStatus>)StatusElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new EncounterLocationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EncounterLocationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EncounterLocationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("EncounterParticipantComponent")]
        [DataContract]
        public partial class EncounterParticipantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "EncounterParticipantComponent"; } }
            
            /// <summary>
            /// Role of participant in encounter
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Type;
            
            /// <summary>
            /// Period of time during the encounter participant was present
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
            /// Persons involved in the encounter other than the patient
            /// </summary>
            [FhirElement("individual", InSummary=true, Order=60)]
            [References("Practitioner","RelatedPerson")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Individual
            {
                get { return _Individual; }
                set { _Individual = value; OnPropertyChanged("Individual"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Individual;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EncounterParticipantComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Individual != null) dest.Individual = (Hl7.Fhir.Model.ResourceReference)Individual.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new EncounterParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EncounterParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                if( !DeepComparable.Matches(Individual, otherT.Individual)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EncounterParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                if( !DeepComparable.IsExactly(Individual, otherT.Individual)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Identifier(s) by which this encounter is known
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// planned | arrived | in-progress | onleave | finished | cancelled
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Encounter.EncounterState> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Encounter.EncounterState> _StatusElement;
        
        /// <summary>
        /// planned | arrived | in-progress | onleave | finished | cancelled
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
        /// List of Encounter statuses
        /// </summary>
        [FhirElement("statusHistory", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Encounter.EncounterStatusHistoryComponent> StatusHistory
        {
            get { if(_StatusHistory==null) _StatusHistory = new List<Hl7.Fhir.Model.Encounter.EncounterStatusHistoryComponent>(); return _StatusHistory; }
            set { _StatusHistory = value; OnPropertyChanged("StatusHistory"); }
        }
        
        private List<Hl7.Fhir.Model.Encounter.EncounterStatusHistoryComponent> _StatusHistory;
        
        /// <summary>
        /// inpatient | outpatient | ambulatory | emergency +
        /// </summary>
        [FhirElement("class", InSummary=true, Order=120)]
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
        [FhirElement("type", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Type
        {
            get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Type;
        
        /// <summary>
        /// The patient present at the encounter
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
        /// An episode of care that this encounter should be recorded against
        /// </summary>
        [FhirElement("episodeOfCare", InSummary=true, Order=150)]
        [References("EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference EpisodeOfCare
        {
            get { return _EpisodeOfCare; }
            set { _EpisodeOfCare = value; OnPropertyChanged("EpisodeOfCare"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _EpisodeOfCare;
        
        /// <summary>
        /// Incoming Referral Request
        /// </summary>
        [FhirElement("incomingReferralRequest", Order=160)]
        [References("ReferralRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> IncomingReferralRequest
        {
            get { if(_IncomingReferralRequest==null) _IncomingReferralRequest = new List<Hl7.Fhir.Model.ResourceReference>(); return _IncomingReferralRequest; }
            set { _IncomingReferralRequest = value; OnPropertyChanged("IncomingReferralRequest"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _IncomingReferralRequest;
        
        /// <summary>
        /// List of participants involved in the encounter
        /// </summary>
        [FhirElement("participant", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Encounter.EncounterParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<Hl7.Fhir.Model.Encounter.EncounterParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<Hl7.Fhir.Model.Encounter.EncounterParticipantComponent> _Participant;
        
        /// <summary>
        /// The appointment that scheduled this encounter
        /// </summary>
        [FhirElement("fulfills", InSummary=true, Order=180)]
        [References("Appointment")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Fulfills
        {
            get { return _Fulfills; }
            set { _Fulfills = value; OnPropertyChanged("Fulfills"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Fulfills;
        
        /// <summary>
        /// The start and end time of the encounter
        /// </summary>
        [FhirElement("period", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Quantity of time the encounter lasted (less time absent)
        /// </summary>
        [FhirElement("length", Order=200)]
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
        [FhirElement("reason", InSummary=true, Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Reason
        {
            get { if(_Reason==null) _Reason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Reason;
        
        /// <summary>
        /// Reason the encounter takes place (resource)
        /// </summary>
        [FhirElement("indication", Order=220)]
        [References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Indication
        {
            get { if(_Indication==null) _Indication = new List<Hl7.Fhir.Model.ResourceReference>(); return _Indication; }
            set { _Indication = value; OnPropertyChanged("Indication"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Indication;
        
        /// <summary>
        /// Indicates the urgency of the encounter
        /// </summary>
        [FhirElement("priority", Order=230)]
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
        [FhirElement("hospitalization", Order=240)]
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
        [FhirElement("location", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Encounter.EncounterLocationComponent> Location
        {
            get { if(_Location==null) _Location = new List<Hl7.Fhir.Model.Encounter.EncounterLocationComponent>(); return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        
        private List<Hl7.Fhir.Model.Encounter.EncounterLocationComponent> _Location;
        
        /// <summary>
        /// The custodian organization of this Encounter record
        /// </summary>
        [FhirElement("serviceProvider", Order=260)]
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
        [FhirElement("partOf", Order=270)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PartOf
        {
            get { return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _PartOf;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Encounter;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Encounter.EncounterState>)StatusElement.DeepCopy();
                if(StatusHistory != null) dest.StatusHistory = new List<Hl7.Fhir.Model.Encounter.EncounterStatusHistoryComponent>(StatusHistory.DeepCopy());
                if(ClassElement != null) dest.ClassElement = (Code<Hl7.Fhir.Model.Encounter.EncounterClass>)ClassElement.DeepCopy();
                if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(EpisodeOfCare != null) dest.EpisodeOfCare = (Hl7.Fhir.Model.ResourceReference)EpisodeOfCare.DeepCopy();
                if(IncomingReferralRequest != null) dest.IncomingReferralRequest = new List<Hl7.Fhir.Model.ResourceReference>(IncomingReferralRequest.DeepCopy());
                if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.Encounter.EncounterParticipantComponent>(Participant.DeepCopy());
                if(Fulfills != null) dest.Fulfills = (Hl7.Fhir.Model.ResourceReference)Fulfills.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Length != null) dest.Length = (Hl7.Fhir.Model.Duration)Length.DeepCopy();
                if(Reason != null) dest.Reason = new List<Hl7.Fhir.Model.CodeableConcept>(Reason.DeepCopy());
                if(Indication != null) dest.Indication = new List<Hl7.Fhir.Model.ResourceReference>(Indication.DeepCopy());
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.CodeableConcept)Priority.DeepCopy();
                if(Hospitalization != null) dest.Hospitalization = (Hl7.Fhir.Model.Encounter.EncounterHospitalizationComponent)Hospitalization.DeepCopy();
                if(Location != null) dest.Location = new List<Hl7.Fhir.Model.Encounter.EncounterLocationComponent>(Location.DeepCopy());
                if(ServiceProvider != null) dest.ServiceProvider = (Hl7.Fhir.Model.ResourceReference)ServiceProvider.DeepCopy();
                if(PartOf != null) dest.PartOf = (Hl7.Fhir.Model.ResourceReference)PartOf.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Encounter());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Encounter;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusHistory, otherT.StatusHistory)) return false;
            if( !DeepComparable.Matches(ClassElement, otherT.ClassElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(EpisodeOfCare, otherT.EpisodeOfCare)) return false;
            if( !DeepComparable.Matches(IncomingReferralRequest, otherT.IncomingReferralRequest)) return false;
            if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
            if( !DeepComparable.Matches(Fulfills, otherT.Fulfills)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Length, otherT.Length)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Indication, otherT.Indication)) return false;
            if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
            if( !DeepComparable.Matches(Hospitalization, otherT.Hospitalization)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(ServiceProvider, otherT.ServiceProvider)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Encounter;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusHistory, otherT.StatusHistory)) return false;
            if( !DeepComparable.IsExactly(ClassElement, otherT.ClassElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(EpisodeOfCare, otherT.EpisodeOfCare)) return false;
            if( !DeepComparable.IsExactly(IncomingReferralRequest, otherT.IncomingReferralRequest)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            if( !DeepComparable.IsExactly(Fulfills, otherT.Fulfills)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Length, otherT.Length)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Indication, otherT.Indication)) return false;
            if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
            if( !DeepComparable.IsExactly(Hospitalization, otherT.Hospitalization)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(ServiceProvider, otherT.ServiceProvider)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            
            return true;
        }
        
    }
    
}
