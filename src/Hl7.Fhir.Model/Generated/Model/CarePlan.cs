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
    /// Healthcare plan for patient
    /// </summary>
    [FhirType("CarePlan", IsResource=true)]
    [DataContract]
    public partial class CarePlan : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// Indicates whether the plan is currently being acted upon, represents future intentions or is now just historical record.
        /// </summary>
        [FhirEnumeration("CarePlanStatus")]
        public enum CarePlanStatus
        {
            [EnumLiteral("planned")]
            Planned, // The plan is in development or awaiting use but is not yet intended to be acted upon.
            [EnumLiteral("active")]
            Active, // The plan is intended to be followed and used as part of patient care.
            [EnumLiteral("completed")]
            Completed, // The plan is no longer in use and is not expected to be followed or used in patient care.
        }
        
        /// <summary>
        /// High-level categorization of the type of activity in a care plan.
        /// </summary>
        [FhirEnumeration("CarePlanActivityCategory")]
        public enum CarePlanActivityCategory
        {
            [EnumLiteral("diet")]
            Diet, // Plan for the patient to consume food of a specified nature.
            [EnumLiteral("drug")]
            Drug, // Plan for the patient to consume/receive a drug, vaccine or other product.
            [EnumLiteral("encounter")]
            Encounter, // Plan to meet or communicate with the patient (in-patient, out-patient, phone call, etc.).
            [EnumLiteral("observation")]
            Observation, // Plan to capture information about a patient (vitals, labs, diagnostic images, etc.).
            [EnumLiteral("procedure")]
            Procedure, // Plan to modify the patient in some way (surgery, physiotherapy, education, counseling, etc.).
            [EnumLiteral("supply")]
            Supply, // Plan to provide something to the patient (medication, medical supply, etc.).
            [EnumLiteral("other")]
            Other, // Some other form of action.
        }
        
        /// <summary>
        /// Indicates whether the goal has been met and is still being targeted
        /// </summary>
        [FhirEnumeration("CarePlanGoalStatus")]
        public enum CarePlanGoalStatus
        {
            [EnumLiteral("in progress")]
            InProgress, // The goal is being sought but has not yet been reached.  (Also applies if goal was reached in the past but there has been regression and goal is being sought again).
            [EnumLiteral("achieved")]
            Achieved, // The goal has been met and no further action is needed.
            [EnumLiteral("sustaining")]
            Sustaining, // The goal has been met, but ongoing activity is needed to sustain the goal objective.
            [EnumLiteral("cancelled")]
            Cancelled, // The goal is no longer being sought.
        }
        
        /// <summary>
        /// Indicates where the activity is at in its overall life cycle
        /// </summary>
        [FhirEnumeration("CarePlanActivityStatus")]
        public enum CarePlanActivityStatus
        {
            [EnumLiteral("not started")]
            NotStarted, // Activity is planned but no action has yet been taken.
            [EnumLiteral("scheduled")]
            Scheduled, // Appointment or other booking has occurred but activity has not yet begun.
            [EnumLiteral("in progress")]
            InProgress, // Activity has been started but is not yet complete.
            [EnumLiteral("on hold")]
            OnHold, // Activity was started but has temporarily ceased with an expectation of resumption at a future time.
            [EnumLiteral("completed")]
            Completed, // The activities have been completed (more or less) as planned.
            [EnumLiteral("cancelled")]
            Cancelled, // The activities have been ended prior to completion (perhaps even before they were started).
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("CarePlanGoalComponent")]
        [DataContract]
        public partial class CarePlanGoalComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// What's the desired outcome?
            /// </summary>
            [FhirElement("description", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// in progress | achieved | sustaining | cancelled
            /// </summary>
            [FhirElement("status", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CarePlan.CarePlanGoalStatus> StatusElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Comments about the goal
            /// </summary>
            [FhirElement("notes", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NotesElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Health issues this goal addresses
            /// </summary>
            [FhirElement("concern", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Concern { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("CarePlanParticipantComponent")]
        [DataContract]
        public partial class CarePlanParticipantComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Type of involvement
            /// </summary>
            [FhirElement("role", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role { get; set; }
            
            /// <summary>
            /// Who is involved
            /// </summary>
            [FhirElement("member", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Member { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("CarePlanActivityComponent")]
        [DataContract]
        public partial class CarePlanActivityComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Goals this activity relates to
            /// </summary>
            [FhirElement("goal", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.IdRef> GoalElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// not started | scheduled | in progress | on hold | completed | cancelled
            /// </summary>
            [FhirElement("status", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityStatus> StatusElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Do NOT do
            /// </summary>
            [FhirElement("prohibited", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ProhibitedElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Appointments, orders, etc.
            /// </summary>
            [FhirElement("actionResulting", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> ActionResulting { get; set; }
            
            /// <summary>
            /// Comments about the activity
            /// </summary>
            [FhirElement("notes", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NotesElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Activity details defined in specific resource
            /// </summary>
            [FhirElement("detail", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Detail { get; set; }
            
            /// <summary>
            /// Activity details summarised here
            /// </summary>
            [FhirElement("simple", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.CarePlan.CarePlanActivitySimpleComponent Simple { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("CarePlanActivitySimpleComponent")]
        [DataContract]
        public partial class CarePlanActivitySimpleComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// diet | drug | encounter | observation | procedure | supply | other
            /// </summary>
            [FhirElement("category", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityCategory> CategoryElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Detail type of activity
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code { get; set; }
            
            /// <summary>
            /// When activity is to occur
            /// </summary>
            [FhirElement("timing", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Schedule),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element Timing { get; set; }
            
            /// <summary>
            /// Where it should happen
            /// </summary>
            [FhirElement("location", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Location { get; set; }
            
            /// <summary>
            /// Who's responsible?
            /// </summary>
            [FhirElement("performer", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Performer { get; set; }
            
            /// <summary>
            /// What's administered/supplied
            /// </summary>
            [FhirElement("product", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Product { get; set; }
            
            /// <summary>
            /// How much consumed/day?
            /// </summary>
            [FhirElement("dailyAmount", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity DailyAmount { get; set; }
            
            /// <summary>
            /// How much is administered/supplied/consumed
            /// </summary>
            [FhirElement("quantity", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity { get; set; }
            
            /// <summary>
            /// Extra info on activity occurrence
            /// </summary>
            [FhirElement("details", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DetailsElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// External Ids for this plan
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// Who care plan is for
        /// </summary>
        [FhirElement("patient", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient { get; set; }
        
        /// <summary>
        /// planned | active | completed
        /// </summary>
        [FhirElement("status", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.CarePlan.CarePlanStatus> StatusElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Time period plan covers
        /// </summary>
        [FhirElement("period", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period { get; set; }
        
        /// <summary>
        /// When last updated
        /// </summary>
        [FhirElement("modified", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ModifiedElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Health issues this plan addresses
        /// </summary>
        [FhirElement("concern", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Concern { get; set; }
        
        /// <summary>
        /// Who's involved in plan?
        /// </summary>
        [FhirElement("participant", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CarePlan.CarePlanParticipantComponent> Participant { get; set; }
        
        /// <summary>
        /// Desired outcome of plan
        /// </summary>
        [FhirElement("goal", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CarePlan.CarePlanGoalComponent> Goal { get; set; }
        
        /// <summary>
        /// Action to occur as part of plan
        /// </summary>
        [FhirElement("activity", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CarePlan.CarePlanActivityComponent> Activity { get; set; }
        
        /// <summary>
        /// Comments about the plan
        /// </summary>
        [FhirElement("notes", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NotesElement { get; set; }
        
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
            }
        }
        
    }
    
}
