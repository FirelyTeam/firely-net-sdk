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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Healthcare plan for patient
    /// </summary>
    [FhirType("CarePlan", IsResource=true)]
    [DataContract]
    public partial class CarePlan : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Indicates whether the plan is currently being acted upon, represents future intentions or is now just historical record.
        /// </summary>
        [FhirEnumeration("CarePlanStatus")]
        public enum CarePlanStatus
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
        
        /// <summary>
        /// High-level categorization of the type of activity in a care plan.
        /// </summary>
        [FhirEnumeration("CarePlanActivityCategory")]
        public enum CarePlanActivityCategory
        {
            /// <summary>
            /// Plan for the patient to consume food of a specified nature.
            /// </summary>
            [EnumLiteral("diet")]
            Diet,
            /// <summary>
            /// Plan for the patient to consume/receive a drug, vaccine or other product.
            /// </summary>
            [EnumLiteral("drug")]
            Drug,
            /// <summary>
            /// Plan to meet or communicate with the patient (in-patient, out-patient, phone call, etc.).
            /// </summary>
            [EnumLiteral("encounter")]
            Encounter,
            /// <summary>
            /// Plan to capture information about a patient (vitals, labs, diagnostic images, etc.).
            /// </summary>
            [EnumLiteral("observation")]
            Observation,
            /// <summary>
            /// Plan to modify the patient in some way (surgery, physiotherapy, education, counseling, etc.).
            /// </summary>
            [EnumLiteral("procedure")]
            Procedure,
            /// <summary>
            /// Plan to provide something to the patient (medication, medical supply, etc.).
            /// </summary>
            [EnumLiteral("supply")]
            Supply,
            /// <summary>
            /// Some other form of action.
            /// </summary>
            [EnumLiteral("other")]
            Other,
        }
        
        /// <summary>
        /// Indicates whether the goal has been met and is still being targeted
        /// </summary>
        [FhirEnumeration("CarePlanGoalStatus")]
        public enum CarePlanGoalStatus
        {
            /// <summary>
            /// The goal is being sought but has not yet been reached.  (Also applies if goal was reached in the past but there has been regression and goal is being sought again).
            /// </summary>
            [EnumLiteral("in progress")]
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
        }
        
        /// <summary>
        /// Indicates where the activity is at in its overall life cycle
        /// </summary>
        [FhirEnumeration("CarePlanActivityStatus")]
        public enum CarePlanActivityStatus
        {
            /// <summary>
            /// Activity is planned but no action has yet been taken.
            /// </summary>
            [EnumLiteral("not started")]
            NotStarted,
            /// <summary>
            /// Appointment or other booking has occurred but activity has not yet begun.
            /// </summary>
            [EnumLiteral("scheduled")]
            Scheduled,
            /// <summary>
            /// Activity has been started but is not yet complete.
            /// </summary>
            [EnumLiteral("in progress")]
            InProgress,
            /// <summary>
            /// Activity was started but has temporarily ceased with an expectation of resumption at a future time.
            /// </summary>
            [EnumLiteral("on hold")]
            OnHold,
            /// <summary>
            /// The activities have been completed (more or less) as planned.
            /// </summary>
            [EnumLiteral("completed")]
            Completed,
            /// <summary>
            /// The activities have been ended prior to completion (perhaps even before they were started).
            /// </summary>
            [EnumLiteral("cancelled")]
            Cancelled,
        }
        
        [FhirType("CarePlanGoalComponent")]
        [DataContract]
        public partial class CarePlanGoalComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// What's the desired outcome?
            /// </summary>
            [FhirElement("description", InSummary=true, Order=40)]
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
            /// in progress | achieved | sustaining | cancelled
            /// </summary>
            [FhirElement("status", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CarePlan.CarePlanGoalStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            private Code<Hl7.Fhir.Model.CarePlan.CarePlanGoalStatus> _StatusElement;
            
            /// <summary>
            /// in progress | achieved | sustaining | cancelled
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CarePlan.CarePlanGoalStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if(value == null)
                      StatusElement = null; 
                    else
                      StatusElement = new Code<Hl7.Fhir.Model.CarePlan.CarePlanGoalStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// Comments about the goal
            /// </summary>
            [FhirElement("notes", InSummary=true, Order=60)]
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
            /// Health issues this goal addresses
            /// </summary>
            [FhirElement("concern", InSummary=true, Order=70)]
            [References("Condition")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Concern
            {
                get { return _Concern; }
                set { _Concern = value; OnPropertyChanged("Concern"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _Concern;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CarePlanGoalComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.CarePlan.CarePlanGoalStatus>)StatusElement.DeepCopy();
                    if(NotesElement != null) dest.NotesElement = (Hl7.Fhir.Model.FhirString)NotesElement.DeepCopy();
                    if(Concern != null) dest.Concern = new List<Hl7.Fhir.Model.ResourceReference>(Concern.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CarePlanGoalComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CarePlanGoalComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;
                if( !DeepComparable.Matches(Concern, otherT.Concern)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CarePlanGoalComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;
                if( !DeepComparable.IsExactly(Concern, otherT.Concern)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CarePlanParticipantComponent")]
        [DataContract]
        public partial class CarePlanParticipantComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Type of involvement
            /// </summary>
            [FhirElement("role", InSummary=true, Order=40)]
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
            [FhirElement("member", InSummary=true, Order=50)]
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
                var dest = other as CarePlanParticipantComponent;
                
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
                return CopyTo(new CarePlanParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CarePlanParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Member, otherT.Member)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CarePlanParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Member, otherT.Member)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CarePlanActivityComponent")]
        [DataContract]
        public partial class CarePlanActivityComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Goals this activity relates to
            /// </summary>
            [FhirElement("goal", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.IdRef> GoalElement
            {
                get { return _GoalElement; }
                set { _GoalElement = value; OnPropertyChanged("GoalElement"); }
            }
            private List<Hl7.Fhir.Model.IdRef> _GoalElement;
            
            /// <summary>
            /// Goals this activity relates to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Goal
            {
                get { return GoalElement != null ? GoalElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      GoalElement = null; 
                    else
                      GoalElement = new List<Hl7.Fhir.Model.IdRef>(value.Select(elem=>new Hl7.Fhir.Model.IdRef(elem)));
                    OnPropertyChanged("Goal");
                }
            }
            
            /// <summary>
            /// not started | scheduled | in progress | on hold | completed | cancelled
            /// </summary>
            [FhirElement("status", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            private Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityStatus> _StatusElement;
            
            /// <summary>
            /// not started | scheduled | in progress | on hold | completed | cancelled
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CarePlan.CarePlanActivityStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if(value == null)
                      StatusElement = null; 
                    else
                      StatusElement = new Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// Do NOT do
            /// </summary>
            [FhirElement("prohibited", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ProhibitedElement
            {
                get { return _ProhibitedElement; }
                set { _ProhibitedElement = value; OnPropertyChanged("ProhibitedElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _ProhibitedElement;
            
            /// <summary>
            /// Do NOT do
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Prohibited
            {
                get { return ProhibitedElement != null ? ProhibitedElement.Value : null; }
                set
                {
                    if(value == null)
                      ProhibitedElement = null; 
                    else
                      ProhibitedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Prohibited");
                }
            }
            
            /// <summary>
            /// Appointments, orders, etc.
            /// </summary>
            [FhirElement("actionResulting", InSummary=true, Order=70)]
            [References()]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> ActionResulting
            {
                get { return _ActionResulting; }
                set { _ActionResulting = value; OnPropertyChanged("ActionResulting"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _ActionResulting;
            
            /// <summary>
            /// Comments about the activity
            /// </summary>
            [FhirElement("notes", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NotesElement
            {
                get { return _NotesElement; }
                set { _NotesElement = value; OnPropertyChanged("NotesElement"); }
            }
            private Hl7.Fhir.Model.FhirString _NotesElement;
            
            /// <summary>
            /// Comments about the activity
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
            /// Activity details defined in specific resource
            /// </summary>
            [FhirElement("detail", InSummary=true, Order=90)]
            [References("Procedure","MedicationPrescription","DiagnosticOrder","Encounter")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Detail
            {
                get { return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Detail;
            
            /// <summary>
            /// Activity details summarised here
            /// </summary>
            [FhirElement("simple", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.CarePlan.CarePlanActivitySimpleComponent Simple
            {
                get { return _Simple; }
                set { _Simple = value; OnPropertyChanged("Simple"); }
            }
            private Hl7.Fhir.Model.CarePlan.CarePlanActivitySimpleComponent _Simple;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CarePlanActivityComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(GoalElement != null) dest.GoalElement = new List<Hl7.Fhir.Model.IdRef>(GoalElement.DeepCopy());
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityStatus>)StatusElement.DeepCopy();
                    if(ProhibitedElement != null) dest.ProhibitedElement = (Hl7.Fhir.Model.FhirBoolean)ProhibitedElement.DeepCopy();
                    if(ActionResulting != null) dest.ActionResulting = new List<Hl7.Fhir.Model.ResourceReference>(ActionResulting.DeepCopy());
                    if(NotesElement != null) dest.NotesElement = (Hl7.Fhir.Model.FhirString)NotesElement.DeepCopy();
                    if(Detail != null) dest.Detail = (Hl7.Fhir.Model.ResourceReference)Detail.DeepCopy();
                    if(Simple != null) dest.Simple = (Hl7.Fhir.Model.CarePlan.CarePlanActivitySimpleComponent)Simple.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CarePlanActivityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CarePlanActivityComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(GoalElement, otherT.GoalElement)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(ProhibitedElement, otherT.ProhibitedElement)) return false;
                if( !DeepComparable.Matches(ActionResulting, otherT.ActionResulting)) return false;
                if( !DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                if( !DeepComparable.Matches(Simple, otherT.Simple)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CarePlanActivityComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(GoalElement, otherT.GoalElement)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(ProhibitedElement, otherT.ProhibitedElement)) return false;
                if( !DeepComparable.IsExactly(ActionResulting, otherT.ActionResulting)) return false;
                if( !DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                if( !DeepComparable.IsExactly(Simple, otherT.Simple)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CarePlanActivitySimpleComponent")]
        [DataContract]
        public partial class CarePlanActivitySimpleComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// diet | drug | encounter | observation | procedure | supply | other
            /// </summary>
            [FhirElement("category", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityCategory> CategoryElement
            {
                get { return _CategoryElement; }
                set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
            }
            private Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityCategory> _CategoryElement;
            
            /// <summary>
            /// diet | drug | encounter | observation | procedure | supply | other
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CarePlan.CarePlanActivityCategory? Category
            {
                get { return CategoryElement != null ? CategoryElement.Value : null; }
                set
                {
                    if(value == null)
                      CategoryElement = null; 
                    else
                      CategoryElement = new Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityCategory>(value);
                    OnPropertyChanged("Category");
                }
            }
            
            /// <summary>
            /// Detail type of activity
            /// </summary>
            [FhirElement("code", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// When activity is to occur
            /// </summary>
            [FhirElement("timing", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Schedule),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element Timing
            {
                get { return _Timing; }
                set { _Timing = value; OnPropertyChanged("Timing"); }
            }
            private Hl7.Fhir.Model.Element _Timing;
            
            /// <summary>
            /// Where it should happen
            /// </summary>
            [FhirElement("location", InSummary=true, Order=70)]
            [References("Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Location;
            
            /// <summary>
            /// Who's responsible?
            /// </summary>
            [FhirElement("performer", InSummary=true, Order=80)]
            [References("Practitioner","Organization","RelatedPerson","Patient")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Performer
            {
                get { return _Performer; }
                set { _Performer = value; OnPropertyChanged("Performer"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _Performer;
            
            /// <summary>
            /// What's administered/supplied
            /// </summary>
            [FhirElement("product", InSummary=true, Order=90)]
            [References("Medication","Substance")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Product
            {
                get { return _Product; }
                set { _Product = value; OnPropertyChanged("Product"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Product;
            
            /// <summary>
            /// How much consumed/day?
            /// </summary>
            [FhirElement("dailyAmount", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity DailyAmount
            {
                get { return _DailyAmount; }
                set { _DailyAmount = value; OnPropertyChanged("DailyAmount"); }
            }
            private Hl7.Fhir.Model.Quantity _DailyAmount;
            
            /// <summary>
            /// How much is administered/supplied/consumed
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            private Hl7.Fhir.Model.Quantity _Quantity;
            
            /// <summary>
            /// Extra info on activity occurrence
            /// </summary>
            [FhirElement("details", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DetailsElement
            {
                get { return _DetailsElement; }
                set { _DetailsElement = value; OnPropertyChanged("DetailsElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DetailsElement;
            
            /// <summary>
            /// Extra info on activity occurrence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Details
            {
                get { return DetailsElement != null ? DetailsElement.Value : null; }
                set
                {
                    if(value == null)
                      DetailsElement = null; 
                    else
                      DetailsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Details");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CarePlanActivitySimpleComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityCategory>)CategoryElement.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                    if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ResourceReference>(Performer.DeepCopy());
                    if(Product != null) dest.Product = (Hl7.Fhir.Model.ResourceReference)Product.DeepCopy();
                    if(DailyAmount != null) dest.DailyAmount = (Hl7.Fhir.Model.Quantity)DailyAmount.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(DetailsElement != null) dest.DetailsElement = (Hl7.Fhir.Model.FhirString)DetailsElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CarePlanActivitySimpleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CarePlanActivitySimpleComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
                if( !DeepComparable.Matches(Product, otherT.Product)) return false;
                if( !DeepComparable.Matches(DailyAmount, otherT.DailyAmount)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(DetailsElement, otherT.DetailsElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CarePlanActivitySimpleComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
                if( !DeepComparable.IsExactly(Product, otherT.Product)) return false;
                if( !DeepComparable.IsExactly(DailyAmount, otherT.DailyAmount)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(DetailsElement, otherT.DetailsElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External Ids for this plan
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
        /// Who care plan is for
        /// </summary>
        [FhirElement("patient", Order=80)]
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
        [FhirElement("status", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.CarePlan.CarePlanStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.CarePlan.CarePlanStatus> _StatusElement;
        
        /// <summary>
        /// planned | active | completed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.CarePlan.CarePlanStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.CarePlan.CarePlanStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Time period plan covers
        /// </summary>
        [FhirElement("period", Order=100)]
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
        [FhirElement("modified", Order=110)]
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
        [FhirElement("concern", Order=120)]
        [References("Condition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Concern
        {
            get { return _Concern; }
            set { _Concern = value; OnPropertyChanged("Concern"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Concern;
        
        /// <summary>
        /// Who's involved in plan?
        /// </summary>
        [FhirElement("participant", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CarePlan.CarePlanParticipantComponent> Participant
        {
            get { return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        private List<Hl7.Fhir.Model.CarePlan.CarePlanParticipantComponent> _Participant;
        
        /// <summary>
        /// Desired outcome of plan
        /// </summary>
        [FhirElement("goal", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CarePlan.CarePlanGoalComponent> Goal
        {
            get { return _Goal; }
            set { _Goal = value; OnPropertyChanged("Goal"); }
        }
        private List<Hl7.Fhir.Model.CarePlan.CarePlanGoalComponent> _Goal;
        
        /// <summary>
        /// Action to occur as part of plan
        /// </summary>
        [FhirElement("activity", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CarePlan.CarePlanActivityComponent> Activity
        {
            get { return _Activity; }
            set { _Activity = value; OnPropertyChanged("Activity"); }
        }
        private List<Hl7.Fhir.Model.CarePlan.CarePlanActivityComponent> _Activity;
        
        /// <summary>
        /// Comments about the plan
        /// </summary>
        [FhirElement("notes", Order=160)]
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
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as CarePlan;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.CarePlan.CarePlanStatus>)StatusElement.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(ModifiedElement != null) dest.ModifiedElement = (Hl7.Fhir.Model.FhirDateTime)ModifiedElement.DeepCopy();
                if(Concern != null) dest.Concern = new List<Hl7.Fhir.Model.ResourceReference>(Concern.DeepCopy());
                if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.CarePlan.CarePlanParticipantComponent>(Participant.DeepCopy());
                if(Goal != null) dest.Goal = new List<Hl7.Fhir.Model.CarePlan.CarePlanGoalComponent>(Goal.DeepCopy());
                if(Activity != null) dest.Activity = new List<Hl7.Fhir.Model.CarePlan.CarePlanActivityComponent>(Activity.DeepCopy());
                if(NotesElement != null) dest.NotesElement = (Hl7.Fhir.Model.FhirString)NotesElement.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new CarePlan());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as CarePlan;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(ModifiedElement, otherT.ModifiedElement)) return false;
            if( !DeepComparable.Matches(Concern, otherT.Concern)) return false;
            if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
            if( !DeepComparable.Matches(Goal, otherT.Goal)) return false;
            if( !DeepComparable.Matches(Activity, otherT.Activity)) return false;
            if( !DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as CarePlan;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(ModifiedElement, otherT.ModifiedElement)) return false;
            if( !DeepComparable.IsExactly(Concern, otherT.Concern)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            if( !DeepComparable.IsExactly(Goal, otherT.Goal)) return false;
            if( !DeepComparable.IsExactly(Activity, otherT.Activity)) return false;
            if( !DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;
            
            return true;
        }
        
    }
    
}
