using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;

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

#pragma warning disable 1591 // suppress XML summary warnings 

//
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Describes the intended objective(s) for a patient, group or organization
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
        /// (url: http://hl7.org/fhir/ValueSet/goal-status)
        /// </summary>
        [FhirEnumeration("GoalStatus")]
        public enum GoalStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("proposed", "http://hl7.org/fhir/goal-status"), Description("Proposed")]
            Proposed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("accepted", "http://hl7.org/fhir/goal-status"), Description("Accepted")]
            Accepted,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("planned", "http://hl7.org/fhir/goal-status"), Description("Planned")]
            Planned,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("in-progress", "http://hl7.org/fhir/goal-status"), Description("In Progress")]
            InProgress,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("on-target", "http://hl7.org/fhir/goal-status"), Description("On Target")]
            OnTarget,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("ahead-of-target", "http://hl7.org/fhir/goal-status"), Description("Ahead of Target")]
            AheadOfTarget,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("behind-target", "http://hl7.org/fhir/goal-status"), Description("Behind Target")]
            BehindTarget,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("sustaining", "http://hl7.org/fhir/goal-status"), Description("Sustaining")]
            Sustaining,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("achieved", "http://hl7.org/fhir/goal-status"), Description("Achieved")]
            Achieved,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("on-hold", "http://hl7.org/fhir/goal-status"), Description("On Hold")]
            OnHold,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("cancelled", "http://hl7.org/fhir/goal-status"), Description("Cancelled")]
            Cancelled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/goal-status"), Description("Entered In Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/goal-status)
            /// </summary>
            [EnumLiteral("rejected", "http://hl7.org/fhir/goal-status"), Description("Rejected")]
            Rejected,
        }

        [FhirType("TargetComponent")]
        [DataContract]
        public partial class TargetComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "TargetComponent"; } }
            
            /// <summary>
            /// The parameter whose value is being tracked
            /// </summary>
            [FhirElement("measure", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Measure
            {
                get { return _Measure; }
                set { _Measure = value; OnPropertyChanged("Measure"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Measure;
            
            /// <summary>
            /// The target value to be achieved
            /// </summary>
            [FhirElement("detail", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [DataMember]
            public Hl7.Fhir.Model.Element Detail
            {
                get { return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private Hl7.Fhir.Model.Element _Detail;
            
            /// <summary>
            /// Reach goal on or before
            /// </summary>
            [FhirElement("due", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Duration))]
            [DataMember]
            public Hl7.Fhir.Model.Element Due
            {
                get { return _Due; }
                set { _Due = value; OnPropertyChanged("Due"); }
            }
            
            private Hl7.Fhir.Model.Element _Due;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TargetComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Measure != null) dest.Measure = (Hl7.Fhir.Model.CodeableConcept)Measure.DeepCopy();
                    if(Detail != null) dest.Detail = (Hl7.Fhir.Model.Element)Detail.DeepCopy();
                    if(Due != null) dest.Due = (Hl7.Fhir.Model.Element)Due.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TargetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TargetComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Measure, otherT.Measure)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                if( !DeepComparable.Matches(Due, otherT.Due)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TargetComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Measure, otherT.Measure)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                if( !DeepComparable.IsExactly(Due, otherT.Due)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Measure != null) yield return Measure;
                    if (Detail != null) yield return Detail;
                    if (Due != null) yield return Due;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Measure != null) yield return new ElementValue("measure", Measure);
                    if (Detail != null) yield return new ElementValue("detail", Detail);
                    if (Due != null) yield return new ElementValue("due", Due);
                }
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
        /// proposed | accepted | planned | in-progress | on-target | ahead-of-target | behind-target | sustaining | achieved | on-hold | cancelled | entered-in-error | rejected
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Goal.GoalStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Goal.GoalStatus> _StatusElement;
        
        /// <summary>
        /// proposed | accepted | planned | in-progress | on-target | ahead-of-target | behind-target | sustaining | achieved | on-hold | cancelled | entered-in-error | rejected
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Goal.GoalStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Goal.GoalStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// E.g. Treatment, dietary, behavioral, etc.
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
        /// high-priority | medium-priority | low-priority
        /// </summary>
        [FhirElement("priority", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Priority
        {
            get { return _Priority; }
            set { _Priority = value; OnPropertyChanged("Priority"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Priority;
        
        /// <summary>
        /// Code or text describing goal
        /// </summary>
        [FhirElement("description", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Description;
        
        /// <summary>
        /// Who this goal is intended for
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References("Patient","Group","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// When goal pursuit begins
        /// </summary>
        [FhirElement("start", InSummary=true, Order=150, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.CodeableConcept))]
        [DataMember]
        public Hl7.Fhir.Model.Element Start
        {
            get { return _Start; }
            set { _Start = value; OnPropertyChanged("Start"); }
        }
        
        private Hl7.Fhir.Model.Element _Start;
        
        /// <summary>
        /// Target outcome for the goal
        /// </summary>
        [FhirElement("target", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Goal.TargetComponent Target
        {
            get { return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        
        private Hl7.Fhir.Model.Goal.TargetComponent _Target;
        
        /// <summary>
        /// When goal status took effect
        /// </summary>
        [FhirElement("statusDate", InSummary=true, Order=170)]
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
                if (value == null)
                  StatusDateElement = null; 
                else
                  StatusDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("StatusDate");
            }
        }
        
        /// <summary>
        /// Reason for current status
        /// </summary>
        [FhirElement("statusReason", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString StatusReasonElement
        {
            get { return _StatusReasonElement; }
            set { _StatusReasonElement = value; OnPropertyChanged("StatusReasonElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _StatusReasonElement;
        
        /// <summary>
        /// Reason for current status
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string StatusReason
        {
            get { return StatusReasonElement != null ? StatusReasonElement.Value : null; }
            set
            {
                if (value == null)
                  StatusReasonElement = null; 
                else
                  StatusReasonElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("StatusReason");
            }
        }
        
        /// <summary>
        /// Who's responsible for creating Goal?
        /// </summary>
        [FhirElement("expressedBy", InSummary=true, Order=190)]
        [CLSCompliant(false)]
		[References("Patient","Practitioner","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ExpressedBy
        {
            get { return _ExpressedBy; }
            set { _ExpressedBy = value; OnPropertyChanged("ExpressedBy"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ExpressedBy;
        
        /// <summary>
        /// Issues addressed by this goal
        /// </summary>
        [FhirElement("addresses", Order=200)]
        [CLSCompliant(false)]
		[References("Condition","Observation","MedicationStatement","NutritionOrder","ProcedureRequest","RiskAssessment")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Addresses
        {
            get { if(_Addresses==null) _Addresses = new List<Hl7.Fhir.Model.ResourceReference>(); return _Addresses; }
            set { _Addresses = value; OnPropertyChanged("Addresses"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Addresses;
        
        /// <summary>
        /// Comments about the goal
        /// </summary>
        [FhirElement("note", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// What result was achieved regarding the goal?
        /// </summary>
        [FhirElement("outcomeCode", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> OutcomeCode
        {
            get { if(_OutcomeCode==null) _OutcomeCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _OutcomeCode; }
            set { _OutcomeCode = value; OnPropertyChanged("OutcomeCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _OutcomeCode;
        
        /// <summary>
        /// Observation that resulted from goal
        /// </summary>
        [FhirElement("outcomeReference", Order=230)]
        [CLSCompliant(false)]
		[References("Observation")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> OutcomeReference
        {
            get { if(_OutcomeReference==null) _OutcomeReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _OutcomeReference; }
            set { _OutcomeReference = value; OnPropertyChanged("OutcomeReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _OutcomeReference;
        

        public static ElementDefinition.ConstraintComponent Goal_GOL_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "target.all((detail.exists() and measure.exists()) or detail.exists().not())",
            Key = "gol-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Goal.target.measure is required if Goal.target.detail is populated",
            Xpath = "(exists(f:*[starts-with(local-name(.), 'detail')]) and exists(f:measure)) or not(exists(f:*[starts-with(local-name(.), 'detail')]))"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(Goal_GOL_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Goal;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Goal.GoalStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.CodeableConcept)Priority.DeepCopy();
                if(Description != null) dest.Description = (Hl7.Fhir.Model.CodeableConcept)Description.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Start != null) dest.Start = (Hl7.Fhir.Model.Element)Start.DeepCopy();
                if(Target != null) dest.Target = (Hl7.Fhir.Model.Goal.TargetComponent)Target.DeepCopy();
                if(StatusDateElement != null) dest.StatusDateElement = (Hl7.Fhir.Model.Date)StatusDateElement.DeepCopy();
                if(StatusReasonElement != null) dest.StatusReasonElement = (Hl7.Fhir.Model.FhirString)StatusReasonElement.DeepCopy();
                if(ExpressedBy != null) dest.ExpressedBy = (Hl7.Fhir.Model.ResourceReference)ExpressedBy.DeepCopy();
                if(Addresses != null) dest.Addresses = new List<Hl7.Fhir.Model.ResourceReference>(Addresses.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(OutcomeCode != null) dest.OutcomeCode = new List<Hl7.Fhir.Model.CodeableConcept>(OutcomeCode.DeepCopy());
                if(OutcomeReference != null) dest.OutcomeReference = new List<Hl7.Fhir.Model.ResourceReference>(OutcomeReference.DeepCopy());
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
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
            if( !DeepComparable.Matches(Description, otherT.Description)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Start, otherT.Start)) return false;
            if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            if( !DeepComparable.Matches(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.Matches(StatusReasonElement, otherT.StatusReasonElement)) return false;
            if( !DeepComparable.Matches(ExpressedBy, otherT.ExpressedBy)) return false;
            if( !DeepComparable.Matches(Addresses, otherT.Addresses)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(OutcomeCode, otherT.OutcomeCode)) return false;
            if( !DeepComparable.Matches(OutcomeReference, otherT.OutcomeReference)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Goal;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
            if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Start, otherT.Start)) return false;
            if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            if( !DeepComparable.IsExactly(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.IsExactly(StatusReasonElement, otherT.StatusReasonElement)) return false;
            if( !DeepComparable.IsExactly(ExpressedBy, otherT.ExpressedBy)) return false;
            if( !DeepComparable.IsExactly(Addresses, otherT.Addresses)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(OutcomeCode, otherT.OutcomeCode)) return false;
            if( !DeepComparable.IsExactly(OutcomeReference, otherT.OutcomeReference)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				foreach (var elem in Category) { if (elem != null) yield return elem; }
				if (Priority != null) yield return Priority;
				if (Description != null) yield return Description;
				if (Subject != null) yield return Subject;
				if (Start != null) yield return Start;
				if (Target != null) yield return Target;
				if (StatusDateElement != null) yield return StatusDateElement;
				if (StatusReasonElement != null) yield return StatusReasonElement;
				if (ExpressedBy != null) yield return ExpressedBy;
				foreach (var elem in Addresses) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
				foreach (var elem in OutcomeCode) { if (elem != null) yield return elem; }
				foreach (var elem in OutcomeReference) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (Priority != null) yield return new ElementValue("priority", Priority);
                if (Description != null) yield return new ElementValue("description", Description);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Start != null) yield return new ElementValue("start", Start);
                if (Target != null) yield return new ElementValue("target", Target);
                if (StatusDateElement != null) yield return new ElementValue("statusDate", StatusDateElement);
                if (StatusReasonElement != null) yield return new ElementValue("statusReason", StatusReasonElement);
                if (ExpressedBy != null) yield return new ElementValue("expressedBy", ExpressedBy);
                foreach (var elem in Addresses) { if (elem != null) yield return new ElementValue("addresses", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in OutcomeCode) { if (elem != null) yield return new ElementValue("outcomeCode", elem); }
                foreach (var elem in OutcomeReference) { if (elem != null) yield return new ElementValue("outcomeReference", elem); }
            }
        }

    }
    
}
