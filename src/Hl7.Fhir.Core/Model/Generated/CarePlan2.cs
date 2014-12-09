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
// Generated on Tue, Dec 9, 2014 15:49+0100 for FHIR v0.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Healthcare plan for patient
    /// </summary>
    [FhirType("CarePlan2", IsResource=true)]
    [DataContract]
    public partial class CarePlan2 : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.CarePlan2; } }
        [NotMapped]
        public override string TypeName { get { return "CarePlan2"; } }
        
        /// <summary>
        /// Indicates whether the plan is currently being acted upon, represents future intentions or is now just historical record.
        /// </summary>
        [FhirEnumeration("CarePlan2Status")]
        public enum CarePlan2Status
        {
            /// <summary>
            /// The plan is in development or awaiting use but is not yet intended to be acted upon.
            /// </summary>
            [EnumLiteral("planned")]
            Planned,
            /// <summary>
            /// The plan is intended to be followed and used as part of patient care.
            /// </summary>
            [EnumLiteral("active")]
            Active,
            /// <summary>
            /// The plan is no longer in use and is not expected to be followed or used in patient care.
            /// </summary>
            [EnumLiteral("completed")]
            Completed,
        }
        
        [FhirType("CarePlan2ParticipantComponent")]
        [DataContract]
        public partial class CarePlan2ParticipantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CarePlan2ParticipantComponent"; } }
            
            /// <summary>
            /// Type of involvement
            /// </summary>
            [FhirElement("role", InSummary=true, Order=20)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            /// <summary>
            /// Who is involved
            /// </summary>
            [FhirElement("member", InSummary=true, Order=30)]
            [References("Practitioner","RelatedPerson","Patient","Organization")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Member
            {
                get { return _Member; }
                set { _Member = value; OnPropertyChanged("Member"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Member;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CarePlan2ParticipantComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    if(Member != null) dest.Member = (Hl7.Fhir.Model.ResourceReference)Member.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CarePlan2ParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CarePlan2ParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Member, otherT.Member)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CarePlan2ParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Member, otherT.Member)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External Ids for this plan
        /// </summary>
        [FhirElement("identifier", Order=50)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Who care plan is for
        /// </summary>
        [FhirElement("patient", Order=60)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// planned | active | completed
        /// </summary>
        [FhirElement("status", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.CarePlan2.CarePlan2Status> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.CarePlan2.CarePlan2Status> _StatusElement;
        
        /// <summary>
        /// planned | active | completed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.CarePlan2.CarePlan2Status? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.CarePlan2.CarePlan2Status>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Time period plan covers
        /// </summary>
        [FhirElement("period", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// When last updated
        /// </summary>
        [FhirElement("modified", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ModifiedElement
        {
            get { return _ModifiedElement; }
            set { _ModifiedElement = value; OnPropertyChanged("ModifiedElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _ModifiedElement;
        
        /// <summary>
        /// When last updated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Modified
        {
            get { return ModifiedElement != null ? ModifiedElement.Value : null; }
            set
            {
                if(value == null)
                  ModifiedElement = null; 
                else
                  ModifiedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Modified");
            }
        }
        
        /// <summary>
        /// Health issues this plan addresses
        /// </summary>
        [FhirElement("concern", Order=100)]
        [References("Condition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Concern
        {
            get { if(_Concern==null) _Concern = new List<Hl7.Fhir.Model.ResourceReference>(); return _Concern; }
            set { _Concern = value; OnPropertyChanged("Concern"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Concern;
        
        /// <summary>
        /// Who's involved in plan?
        /// </summary>
        [FhirElement("participant", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CarePlan2.CarePlan2ParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<Hl7.Fhir.Model.CarePlan2.CarePlan2ParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        private List<Hl7.Fhir.Model.CarePlan2.CarePlan2ParticipantComponent> _Participant;
        
        /// <summary>
        /// Comments about the plan
        /// </summary>
        [FhirElement("notes", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NotesElement
        {
            get { return _NotesElement; }
            set { _NotesElement = value; OnPropertyChanged("NotesElement"); }
        }
        private Hl7.Fhir.Model.FhirString _NotesElement;
        
        /// <summary>
        /// Comments about the plan
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Notes
        {
            get { return NotesElement != null ? NotesElement.Value : null; }
            set
            {
                if(value == null)
                  NotesElement = null; 
                else
                  NotesElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Notes");
            }
        }
        
        /// <summary>
        /// CarePlan Goal
        /// </summary>
        [FhirElement("goal", Order=130)]
        [References("Goal")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Goal
        {
            get { if(_Goal==null) _Goal = new List<Hl7.Fhir.Model.ResourceReference>(); return _Goal; }
            set { _Goal = value; OnPropertyChanged("Goal"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Goal;
        
        /// <summary>
        /// CarePlan Activity
        /// </summary>
        [FhirElement("activity", Order=140)]
        [References("CareActivity")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Activity
        {
            get { if(_Activity==null) _Activity = new List<Hl7.Fhir.Model.ResourceReference>(); return _Activity; }
            set { _Activity = value; OnPropertyChanged("Activity"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Activity;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as CarePlan2;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.CarePlan2.CarePlan2Status>)StatusElement.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(ModifiedElement != null) dest.ModifiedElement = (Hl7.Fhir.Model.FhirDateTime)ModifiedElement.DeepCopy();
                if(Concern != null) dest.Concern = new List<Hl7.Fhir.Model.ResourceReference>(Concern.DeepCopy());
                if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.CarePlan2.CarePlan2ParticipantComponent>(Participant.DeepCopy());
                if(NotesElement != null) dest.NotesElement = (Hl7.Fhir.Model.FhirString)NotesElement.DeepCopy();
                if(Goal != null) dest.Goal = new List<Hl7.Fhir.Model.ResourceReference>(Goal.DeepCopy());
                if(Activity != null) dest.Activity = new List<Hl7.Fhir.Model.ResourceReference>(Activity.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new CarePlan2());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as CarePlan2;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(ModifiedElement, otherT.ModifiedElement)) return false;
            if( !DeepComparable.Matches(Concern, otherT.Concern)) return false;
            if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
            if( !DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.Matches(Goal, otherT.Goal)) return false;
            if( !DeepComparable.Matches(Activity, otherT.Activity)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as CarePlan2;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(ModifiedElement, otherT.ModifiedElement)) return false;
            if( !DeepComparable.IsExactly(Concern, otherT.Concern)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            if( !DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.IsExactly(Goal, otherT.Goal)) return false;
            if( !DeepComparable.IsExactly(Activity, otherT.Activity)) return false;
            
            return true;
        }
        
    }
    
}
