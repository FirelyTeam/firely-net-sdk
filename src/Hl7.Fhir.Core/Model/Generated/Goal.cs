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
    /// Describes the intended objective(s) of patient care
    /// </summary>
    [FhirType("Goal", IsResource=true)]
    [DataContract]
    public partial class Goal : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Goal; } }
        [NotMapped]
        public override string TypeName { get { return "Goal"; } }
        
        /// <summary>
        /// Indicates whether the goal has been met and is still being targeted
        /// </summary>
        [FhirEnumeration("GoalStatus")]
        public enum GoalStatus
        {
            /// <summary>
            /// A goal is proposed for this patient.
            /// </summary>
            [EnumLiteral("proposed")]
            Proposed,
            /// <summary>
            /// A goal is planned for this patient.
            /// </summary>
            [EnumLiteral("planned")]
            Planned,
            /// <summary>
            /// The goal is being sought but has not yet been reached.  (Also applies if goal was reached in the past but there has been regression and goal is being sought again).
            /// </summary>
            [EnumLiteral("in-progress")]
            InProgress,
            /// <summary>
            /// The goal has been met and no further action is needed.
            /// </summary>
            [EnumLiteral("achieved")]
            Achieved,
            /// <summary>
            /// The goal has been met, but ongoing activity is needed to sustain the goal objective.
            /// </summary>
            [EnumLiteral("sustaining")]
            Sustaining,
            /// <summary>
            /// The goal is no longer being sought.
            /// </summary>
            [EnumLiteral("cancelled")]
            Cancelled,
            /// <summary>
            /// A proposed goal was accepted.
            /// </summary>
            [EnumLiteral("accepted")]
            Accepted,
            /// <summary>
            /// A proposed goal was rejected.
            /// </summary>
            [EnumLiteral("rejected")]
            Rejected,
        }
        
        [FhirType("GoalOutcomeComponent")]
        [DataContract]
        public partial class GoalOutcomeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "GoalOutcomeComponent"; } }
            
            /// <summary>
            /// Code or observation that resulted from gual
            /// </summary>
            [FhirElement("result", InSummary=true, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Result
            {
                get { return _Result; }
                set { _Result = value; OnPropertyChanged("Result"); }
            }
            
            private Hl7.Fhir.Model.Element _Result;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GoalOutcomeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Result != null) dest.Result = (Hl7.Fhir.Model.Element)Result.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new GoalOutcomeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GoalOutcomeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Result, otherT.Result)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GoalOutcomeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Result, otherT.Result)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External Ids for this goal
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
        /// The patient for whom this goal is intended for
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=100)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Reach goal on or before
        /// </summary>
        [FhirElement("targetDate", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Date TargetDateElement
        {
            get { return _TargetDateElement; }
            set { _TargetDateElement = value; OnPropertyChanged("TargetDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _TargetDateElement;
        
        /// <summary>
        /// Reach goal on or before
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string TargetDate
        {
            get { return TargetDateElement != null ? TargetDateElement.Value : null; }
            set
            {
                if(value == null)
                  TargetDateElement = null; 
                else
                  TargetDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("TargetDate");
            }
        }
        
        /// <summary>
        /// What's the desired outcome?
        /// </summary>
        [FhirElement("description", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// What's the desired outcome?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if(value == null)
                  DescriptionElement = null; 
                else
                  DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// proposed | planned | in-progress | achieved | sustaining | cancelled | accepted | rejected
        /// </summary>
        [FhirElement("status", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Goal.GoalStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Goal.GoalStatus> _StatusElement;
        
        /// <summary>
        /// proposed | planned | in-progress | achieved | sustaining | cancelled | accepted | rejected
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Goal.GoalStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Goal.GoalStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// When goal status took effect
        /// </summary>
        [FhirElement("statusDate", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Date StatusDateElement
        {
            get { return _StatusDateElement; }
            set { _StatusDateElement = value; OnPropertyChanged("StatusDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _StatusDateElement;
        
        /// <summary>
        /// When goal status took effect
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string StatusDate
        {
            get { return StatusDateElement != null ? StatusDateElement.Value : null; }
            set
            {
                if(value == null)
                  StatusDateElement = null; 
                else
                  StatusDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("StatusDate");
            }
        }
        
        /// <summary>
        /// Who's responsible for creating Goal?
        /// </summary>
        [FhirElement("author", InSummary=true, Order=150)]
        [References("Patient","Practitioner","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// high | medium |low
        /// </summary>
        [FhirElement("priority", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Priority
        {
            get { return _Priority; }
            set { _Priority = value; OnPropertyChanged("Priority"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Priority;
        
        /// <summary>
        /// Health issues this goal addresses
        /// </summary>
        [FhirElement("concern", Order=170)]
        [References("Condition","Observation","MedicationStatement","NutritionOrder","ProcedureRequest","RiskAssessment")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Concern
        {
            get { if(_Concern==null) _Concern = new List<Hl7.Fhir.Model.ResourceReference>(); return _Concern; }
            set { _Concern = value; OnPropertyChanged("Concern"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Concern;
        
        /// <summary>
        /// Comments about the goal
        /// </summary>
        [FhirElement("notes", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NotesElement
        {
            get { return _NotesElement; }
            set { _NotesElement = value; OnPropertyChanged("NotesElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NotesElement;
        
        /// <summary>
        /// Comments about the goal
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
        /// What was end result of goal?
        /// </summary>
        [FhirElement("outcome", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Goal.GoalOutcomeComponent> Outcome
        {
            get { if(_Outcome==null) _Outcome = new List<Hl7.Fhir.Model.Goal.GoalOutcomeComponent>(); return _Outcome; }
            set { _Outcome = value; OnPropertyChanged("Outcome"); }
        }
        
        private List<Hl7.Fhir.Model.Goal.GoalOutcomeComponent> _Outcome;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Goal;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(TargetDateElement != null) dest.TargetDateElement = (Hl7.Fhir.Model.Date)TargetDateElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Goal.GoalStatus>)StatusElement.DeepCopy();
                if(StatusDateElement != null) dest.StatusDateElement = (Hl7.Fhir.Model.Date)StatusDateElement.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.CodeableConcept)Priority.DeepCopy();
                if(Concern != null) dest.Concern = new List<Hl7.Fhir.Model.ResourceReference>(Concern.DeepCopy());
                if(NotesElement != null) dest.NotesElement = (Hl7.Fhir.Model.FhirString)NotesElement.DeepCopy();
                if(Outcome != null) dest.Outcome = new List<Hl7.Fhir.Model.Goal.GoalOutcomeComponent>(Outcome.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Goal());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Goal;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(TargetDateElement, otherT.TargetDateElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
            if( !DeepComparable.Matches(Concern, otherT.Concern)) return false;
            if( !DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Goal;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(TargetDateElement, otherT.TargetDateElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
            if( !DeepComparable.IsExactly(Concern, otherT.Concern)) return false;
            if( !DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            
            return true;
        }
        
    }
    
}
