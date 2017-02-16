using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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
// Generated for FHIR v1.8.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Healthcare plan for patient or group
    /// </summary>
    [FhirType("CarePlan", IsResource=true)]
    [DataContract]
    public partial class CarePlan : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.CarePlan; } }
        [NotMapped]
        public override string TypeName { get { return "CarePlan"; } }
        
        /// <summary>
        /// Indicates whether the plan is currently being acted upon, represents future intentions or is now a historical record.
        /// (url: http://hl7.org/fhir/ValueSet/care-plan-status)
        /// </summary>
        [FhirEnumeration("CarePlanStatus")]
        public enum CarePlanStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-status)
            /// </summary>
            [EnumLiteral("proposed"), Description("Proposed")]
            Proposed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-status)
            /// </summary>
            [EnumLiteral("draft"), Description("Pending")]
            Draft,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-status)
            /// </summary>
            [EnumLiteral("active"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-status)
            /// </summary>
            [EnumLiteral("suspended"), Description("Suspended")]
            Suspended,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-status)
            /// </summary>
            [EnumLiteral("completed"), Description("Completed")]
            Completed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-status)
            /// </summary>
            [EnumLiteral("entered-in-error"), Description("Entered In Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-status)
            /// </summary>
            [EnumLiteral("cancelled"), Description("Cancelled")]
            Cancelled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-status)
            /// </summary>
            [EnumLiteral("unknown"), Description("Unknown")]
            Unknown,
        }

        /// <summary>
        /// Codes identifying the types of relationships between two plans.
        /// (url: http://hl7.org/fhir/ValueSet/care-plan-relationship)
        /// </summary>
        [FhirEnumeration("CarePlanRelationship")]
        public enum CarePlanRelationship
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-relationship)
            /// </summary>
            [EnumLiteral("includes"), Description("Includes")]
            Includes,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-relationship)
            /// </summary>
            [EnumLiteral("replaces"), Description("Replaces")]
            Replaces,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-relationship)
            /// </summary>
            [EnumLiteral("fulfills"), Description("Fulfills")]
            Fulfills,
        }

        /// <summary>
        /// Indicates where the activity is at in its overall life cycle.
        /// (url: http://hl7.org/fhir/ValueSet/care-plan-activity-status)
        /// </summary>
        [FhirEnumeration("CarePlanActivityStatus")]
        public enum CarePlanActivityStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-activity-status)
            /// </summary>
            [EnumLiteral("not-started"), Description("Not Started")]
            NotStarted,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-activity-status)
            /// </summary>
            [EnumLiteral("scheduled"), Description("Scheduled")]
            Scheduled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-activity-status)
            /// </summary>
            [EnumLiteral("in-progress"), Description("In Progress")]
            InProgress,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-activity-status)
            /// </summary>
            [EnumLiteral("on-hold"), Description("On Hold")]
            OnHold,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-activity-status)
            /// </summary>
            [EnumLiteral("completed"), Description("Completed")]
            Completed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-activity-status)
            /// </summary>
            [EnumLiteral("cancelled"), Description("Cancelled")]
            Cancelled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/care-plan-activity-status)
            /// </summary>
            [EnumLiteral("unknown"), Description("Unknown")]
            Unknown,
        }

        [FhirType("RelatedPlanComponent")]
        [DataContract]
        public partial class RelatedPlanComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedPlanComponent"; } }
            
            /// <summary>
            /// includes | replaces | fulfills
            /// </summary>
            [FhirElement("code", Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CarePlan.CarePlanRelationship> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.CarePlan.CarePlanRelationship> _CodeElement;
            
            /// <summary>
            /// includes | replaces | fulfills
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CarePlan.CarePlanRelationship? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CodeElement = null; 
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.CarePlan.CarePlanRelationship>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Plan relationship exists with
            /// </summary>
            [FhirElement("plan", Order=50)]
            [CLSCompliant(false)]
			[References("CarePlan")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Plan
            {
                get { return _Plan; }
                set { _Plan = value; OnPropertyChanged("Plan"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Plan;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedPlanComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.CarePlan.CarePlanRelationship>)CodeElement.DeepCopy();
                    if(Plan != null) dest.Plan = (Hl7.Fhir.Model.ResourceReference)Plan.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RelatedPlanComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RelatedPlanComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Plan, otherT.Plan)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedPlanComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Plan, otherT.Plan)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // RelatedPlanComponent elements
                    if (CodeElement != null) yield return CodeElement;
                    if (Plan != null) yield return Plan;
                }
            }
            
        }
        
        
        [FhirType("ActivityComponent")]
        [DataContract]
        public partial class ActivityComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ActivityComponent"; } }
            
            /// <summary>
            /// Appointments, orders, etc.
            /// </summary>
            [FhirElement("actionResulting", Order=40)]
            [CLSCompliant(false)]
			[References()]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> ActionResulting
            {
                get { if(_ActionResulting==null) _ActionResulting = new List<Hl7.Fhir.Model.ResourceReference>(); return _ActionResulting; }
                set { _ActionResulting = value; OnPropertyChanged("ActionResulting"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _ActionResulting;
            
            /// <summary>
            /// Results of the activity
            /// </summary>
            [FhirElement("outcome", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Outcome
            {
                get { return _Outcome; }
                set { _Outcome = value; OnPropertyChanged("Outcome"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Outcome;
            
            /// <summary>
            /// Comments about the activity status/progress
            /// </summary>
            [FhirElement("progress", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Annotation> Progress
            {
                get { if(_Progress==null) _Progress = new List<Hl7.Fhir.Model.Annotation>(); return _Progress; }
                set { _Progress = value; OnPropertyChanged("Progress"); }
            }
            
            private List<Hl7.Fhir.Model.Annotation> _Progress;
            
            /// <summary>
            /// Activity details defined in specific resource
            /// </summary>
            [FhirElement("reference", Order=70)]
            [CLSCompliant(false)]
			[References("Appointment","CommunicationRequest","DeviceUseRequest","DiagnosticRequest","MedicationRequest","NutritionRequest","ProcedureRequest","ProcessRequest","ReferralRequest","VisionPrescription")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Reference;
            
            /// <summary>
            /// In-line definition of activity
            /// </summary>
            [FhirElement("detail", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CarePlan.DetailComponent Detail
            {
                get { return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private Hl7.Fhir.Model.CarePlan.DetailComponent _Detail;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActivityComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ActionResulting != null) dest.ActionResulting = new List<Hl7.Fhir.Model.ResourceReference>(ActionResulting.DeepCopy());
                    if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                    if(Progress != null) dest.Progress = new List<Hl7.Fhir.Model.Annotation>(Progress.DeepCopy());
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    if(Detail != null) dest.Detail = (Hl7.Fhir.Model.CarePlan.DetailComponent)Detail.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ActivityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActivityComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ActionResulting, otherT.ActionResulting)) return false;
                if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
                if( !DeepComparable.Matches(Progress, otherT.Progress)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActivityComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ActionResulting, otherT.ActionResulting)) return false;
                if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
                if( !DeepComparable.IsExactly(Progress, otherT.Progress)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // ActivityComponent elements
                    foreach (var elem in ActionResulting) { if (elem != null) yield return elem; }
                    if (Outcome != null) yield return Outcome;
                    foreach (var elem in Progress) { if (elem != null) yield return elem; }
                    if (Reference != null) yield return Reference;
                    if (Detail != null) yield return Detail;
                }
            }
            
        }
        
        
        [FhirType("DetailComponent")]
        [DataContract]
        public partial class DetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DetailComponent"; } }
            
            /// <summary>
            /// diet | drug | encounter | observation | procedure | supply | other
            /// </summary>
            [FhirElement("category", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Protocol or definition
            /// </summary>
            [FhirElement("definition", Order=50)]
            [CLSCompliant(false)]
			[References("PlanDefinition","Questionnaire")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Definition
            {
                get { return _Definition; }
                set { _Definition = value; OnPropertyChanged("Definition"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Definition;
            
            /// <summary>
            /// Detail type of activity
            /// </summary>
            [FhirElement("code", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Why activity should be done or why activity was prohibited
            /// </summary>
            [FhirElement("reasonCode", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
            {
                get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
                set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
            
            /// <summary>
            /// Condition triggering need for activity
            /// </summary>
            [FhirElement("reasonReference", Order=80)]
            [CLSCompliant(false)]
			[References("Condition")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
            {
                get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
                set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
            
            /// <summary>
            /// Goals this activity relates to
            /// </summary>
            [FhirElement("goal", Order=90)]
            [CLSCompliant(false)]
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
            /// not-started | scheduled | in-progress | on-hold | completed | cancelled | unknown
            /// </summary>
            [FhirElement("status", Order=100)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            
            private Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityStatus> _StatusElement;
            
            /// <summary>
            /// not-started | scheduled | in-progress | on-hold | completed | cancelled | unknown
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.CarePlan.CarePlanActivityStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        StatusElement = null; 
                    else
                        StatusElement = new Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// Reason for current status
            /// </summary>
            [FhirElement("statusReason", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept StatusReason
            {
                get { return _StatusReason; }
                set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _StatusReason;
            
            /// <summary>
            /// Do NOT do
            /// </summary>
            [FhirElement("prohibited", Order=120)]
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
                    if (!value.HasValue)
                        ProhibitedElement = null; 
                    else
                        ProhibitedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Prohibited");
                }
            }
            
            /// <summary>
            /// When activity is to occur
            /// </summary>
            [FhirElement("scheduled", Order=130, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Timing),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element Scheduled
            {
                get { return _Scheduled; }
                set { _Scheduled = value; OnPropertyChanged("Scheduled"); }
            }
            
            private Hl7.Fhir.Model.Element _Scheduled;
            
            /// <summary>
            /// Where it should happen
            /// </summary>
            [FhirElement("location", Order=140)]
            [CLSCompliant(false)]
			[References("Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Location;
            
            /// <summary>
            /// Who will be responsible?
            /// </summary>
            [FhirElement("performer", Order=150)]
            [CLSCompliant(false)]
			[References("Practitioner","Organization","RelatedPerson","Patient")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Performer
            {
                get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Performer; }
                set { _Performer = value; OnPropertyChanged("Performer"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Performer;
            
            /// <summary>
            /// What is to be administered/supplied
            /// </summary>
            [FhirElement("product", Order=160, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Product
            {
                get { return _Product; }
                set { _Product = value; OnPropertyChanged("Product"); }
            }
            
            private Hl7.Fhir.Model.Element _Product;
            
            /// <summary>
            /// How to consume/day?
            /// </summary>
            [FhirElement("dailyAmount", Order=170)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity DailyAmount
            {
                get { return _DailyAmount; }
                set { _DailyAmount = value; OnPropertyChanged("DailyAmount"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _DailyAmount;
            
            /// <summary>
            /// How much to administer/supply/consume
            /// </summary>
            [FhirElement("quantity", Order=180)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Extra info describing activity to perform
            /// </summary>
            [FhirElement("description", Order=190)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Extra info describing activity to perform
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null; 
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Definition != null) dest.Definition = (Hl7.Fhir.Model.ResourceReference)Definition.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                    if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                    if(Goal != null) dest.Goal = new List<Hl7.Fhir.Model.ResourceReference>(Goal.DeepCopy());
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.CarePlan.CarePlanActivityStatus>)StatusElement.DeepCopy();
                    if(StatusReason != null) dest.StatusReason = (Hl7.Fhir.Model.CodeableConcept)StatusReason.DeepCopy();
                    if(ProhibitedElement != null) dest.ProhibitedElement = (Hl7.Fhir.Model.FhirBoolean)ProhibitedElement.DeepCopy();
                    if(Scheduled != null) dest.Scheduled = (Hl7.Fhir.Model.Element)Scheduled.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                    if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ResourceReference>(Performer.DeepCopy());
                    if(Product != null) dest.Product = (Hl7.Fhir.Model.Element)Product.DeepCopy();
                    if(DailyAmount != null) dest.DailyAmount = (Hl7.Fhir.Model.SimpleQuantity)DailyAmount.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
                if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
                if( !DeepComparable.Matches(Goal, otherT.Goal)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
                if( !DeepComparable.Matches(ProhibitedElement, otherT.ProhibitedElement)) return false;
                if( !DeepComparable.Matches(Scheduled, otherT.Scheduled)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
                if( !DeepComparable.Matches(Product, otherT.Product)) return false;
                if( !DeepComparable.Matches(DailyAmount, otherT.DailyAmount)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
                if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
                if( !DeepComparable.IsExactly(Goal, otherT.Goal)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
                if( !DeepComparable.IsExactly(ProhibitedElement, otherT.ProhibitedElement)) return false;
                if( !DeepComparable.IsExactly(Scheduled, otherT.Scheduled)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
                if( !DeepComparable.IsExactly(Product, otherT.Product)) return false;
                if( !DeepComparable.IsExactly(DailyAmount, otherT.DailyAmount)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // DetailComponent elements
                    if (Category != null) yield return Category;
                    if (Definition != null) yield return Definition;
                    if (Code != null) yield return Code;
                    foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
                    foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
                    foreach (var elem in Goal) { if (elem != null) yield return elem; }
                    if (StatusElement != null) yield return StatusElement;
                    if (StatusReason != null) yield return StatusReason;
                    if (ProhibitedElement != null) yield return ProhibitedElement;
                    if (Scheduled != null) yield return Scheduled;
                    if (Location != null) yield return Location;
                    foreach (var elem in Performer) { if (elem != null) yield return elem; }
                    if (Product != null) yield return Product;
                    if (DailyAmount != null) yield return DailyAmount;
                    if (Quantity != null) yield return Quantity;
                    if (DescriptionElement != null) yield return DescriptionElement;
                }
            }
            
        }
        
        
        /// <summary>
        /// External Ids for this plan
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
        /// proposed | draft | active | suspended | completed | entered-in-error | cancelled | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.CarePlan.CarePlanStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.CarePlan.CarePlanStatus> _StatusElement;
        
        /// <summary>
        /// proposed | draft | active | suspended | completed | entered-in-error | cancelled | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.CarePlan.CarePlanStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.CarePlan.CarePlanStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Type of plan
        /// </summary>
        [FhirElement("category", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// Summary of nature of plan
        /// </summary>
        [FhirElement("description", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Summary of nature of plan
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if (value == null)
                  DescriptionElement = null; 
                else
                  DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// Who care plan is for
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Patient","Group")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Created in context of
        /// </summary>
        [FhirElement("context", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// Time period plan covers
        /// </summary>
        [FhirElement("period", InSummary=true, Order=150)]
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
        [FhirElement("modified", InSummary=true, Order=160)]
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
                if (value == null)
                  ModifiedElement = null; 
                else
                  ModifiedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Modified");
            }
        }
        
        /// <summary>
        /// Who is responsible for contents of the plan
        /// </summary>
        [FhirElement("author", InSummary=true, Order=170)]
        [CLSCompliant(false)]
		[References("Patient","Practitioner","RelatedPerson","Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Author
        {
            get { if(_Author==null) _Author = new List<Hl7.Fhir.Model.ResourceReference>(); return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Author;
        
        /// <summary>
        /// Who's involved in plan?
        /// </summary>
        [FhirElement("careTeam", Order=180)]
        [CLSCompliant(false)]
		[References("CareTeam")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> CareTeam
        {
            get { if(_CareTeam==null) _CareTeam = new List<Hl7.Fhir.Model.ResourceReference>(); return _CareTeam; }
            set { _CareTeam = value; OnPropertyChanged("CareTeam"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _CareTeam;
        
        /// <summary>
        /// Health issues this plan addresses
        /// </summary>
        [FhirElement("addresses", InSummary=true, Order=190)]
        [CLSCompliant(false)]
		[References("Condition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Addresses
        {
            get { if(_Addresses==null) _Addresses = new List<Hl7.Fhir.Model.ResourceReference>(); return _Addresses; }
            set { _Addresses = value; OnPropertyChanged("Addresses"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Addresses;
        
        /// <summary>
        /// Information considered as part of plan
        /// </summary>
        [FhirElement("support", Order=200)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Support
        {
            get { if(_Support==null) _Support = new List<Hl7.Fhir.Model.ResourceReference>(); return _Support; }
            set { _Support = value; OnPropertyChanged("Support"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Support;
        
        /// <summary>
        /// Protocol or definition
        /// </summary>
        [FhirElement("definition", Order=210)]
        [CLSCompliant(false)]
		[References("PlanDefinition","Questionnaire")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Definition
        {
            get { return _Definition; }
            set { _Definition = value; OnPropertyChanged("Definition"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Definition;
        
        /// <summary>
        /// Plans related to this one
        /// </summary>
        [FhirElement("relatedPlan", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CarePlan.RelatedPlanComponent> RelatedPlan
        {
            get { if(_RelatedPlan==null) _RelatedPlan = new List<Hl7.Fhir.Model.CarePlan.RelatedPlanComponent>(); return _RelatedPlan; }
            set { _RelatedPlan = value; OnPropertyChanged("RelatedPlan"); }
        }
        
        private List<Hl7.Fhir.Model.CarePlan.RelatedPlanComponent> _RelatedPlan;
        
        /// <summary>
        /// Desired outcome of plan
        /// </summary>
        [FhirElement("goal", Order=230)]
        [CLSCompliant(false)]
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
        /// Action to occur as part of plan
        /// </summary>
        [FhirElement("activity", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CarePlan.ActivityComponent> Activity
        {
            get { if(_Activity==null) _Activity = new List<Hl7.Fhir.Model.CarePlan.ActivityComponent>(); return _Activity; }
            set { _Activity = value; OnPropertyChanged("Activity"); }
        }
        
        private List<Hl7.Fhir.Model.CarePlan.ActivityComponent> _Activity;
        
        /// <summary>
        /// Comments about the plan
        /// </summary>
        [FhirElement("note", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.Annotation Note
        {
            get { return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private Hl7.Fhir.Model.Annotation _Note;
        

        public static ElementDefinition.ConstraintComponent CarePlan_CTM_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "activity.all(detail.empty() or reference.empty())",
            Key = "ctm-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Provide a reference or detail, not both",
            Xpath = "not(exists(f:detail)) or not(exists(f:reference))"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(CarePlan_CTM_3);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as CarePlan;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.CarePlan.CarePlanStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(ModifiedElement != null) dest.ModifiedElement = (Hl7.Fhir.Model.FhirDateTime)ModifiedElement.DeepCopy();
                if(Author != null) dest.Author = new List<Hl7.Fhir.Model.ResourceReference>(Author.DeepCopy());
                if(CareTeam != null) dest.CareTeam = new List<Hl7.Fhir.Model.ResourceReference>(CareTeam.DeepCopy());
                if(Addresses != null) dest.Addresses = new List<Hl7.Fhir.Model.ResourceReference>(Addresses.DeepCopy());
                if(Support != null) dest.Support = new List<Hl7.Fhir.Model.ResourceReference>(Support.DeepCopy());
                if(Definition != null) dest.Definition = (Hl7.Fhir.Model.ResourceReference)Definition.DeepCopy();
                if(RelatedPlan != null) dest.RelatedPlan = new List<Hl7.Fhir.Model.CarePlan.RelatedPlanComponent>(RelatedPlan.DeepCopy());
                if(Goal != null) dest.Goal = new List<Hl7.Fhir.Model.ResourceReference>(Goal.DeepCopy());
                if(Activity != null) dest.Activity = new List<Hl7.Fhir.Model.CarePlan.ActivityComponent>(Activity.DeepCopy());
                if(Note != null) dest.Note = (Hl7.Fhir.Model.Annotation)Note.DeepCopy();
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
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(ModifiedElement, otherT.ModifiedElement)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(CareTeam, otherT.CareTeam)) return false;
            if( !DeepComparable.Matches(Addresses, otherT.Addresses)) return false;
            if( !DeepComparable.Matches(Support, otherT.Support)) return false;
            if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            if( !DeepComparable.Matches(RelatedPlan, otherT.RelatedPlan)) return false;
            if( !DeepComparable.Matches(Goal, otherT.Goal)) return false;
            if( !DeepComparable.Matches(Activity, otherT.Activity)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as CarePlan;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(ModifiedElement, otherT.ModifiedElement)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(CareTeam, otherT.CareTeam)) return false;
            if( !DeepComparable.IsExactly(Addresses, otherT.Addresses)) return false;
            if( !DeepComparable.IsExactly(Support, otherT.Support)) return false;
            if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            if( !DeepComparable.IsExactly(RelatedPlan, otherT.RelatedPlan)) return false;
            if( !DeepComparable.IsExactly(Goal, otherT.Goal)) return false;
            if( !DeepComparable.IsExactly(Activity, otherT.Activity)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
				// CarePlan elements
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				foreach (var elem in Category) { if (elem != null) yield return elem; }
				if (DescriptionElement != null) yield return DescriptionElement;
				if (Subject != null) yield return Subject;
				if (Context != null) yield return Context;
				if (Period != null) yield return Period;
				if (ModifiedElement != null) yield return ModifiedElement;
				foreach (var elem in Author) { if (elem != null) yield return elem; }
				foreach (var elem in CareTeam) { if (elem != null) yield return elem; }
				foreach (var elem in Addresses) { if (elem != null) yield return elem; }
				foreach (var elem in Support) { if (elem != null) yield return elem; }
				if (Definition != null) yield return Definition;
				foreach (var elem in RelatedPlan) { if (elem != null) yield return elem; }
				foreach (var elem in Goal) { if (elem != null) yield return elem; }
				foreach (var elem in Activity) { if (elem != null) yield return elem; }
				if (Note != null) yield return Note;
            }
        }
    }
    
}
