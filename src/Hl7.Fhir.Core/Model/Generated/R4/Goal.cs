﻿using System;
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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// Describes the intended objective(s) for a patient, group or organization
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Goal", IsResource=true)]
    [DataContract]
    public partial class Goal : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IGoal, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Goal; } }
        [NotMapped]
        public override string TypeName { get { return "Goal"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "TargetComponent")]
        [DataContract]
        public partial class TargetComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TargetComponent"; } }
            
            /// <summary>
            /// The parameter whose value is being tracked
            /// </summary>
            [FhirElement("measure", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
            [FhirElement("detail", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Ratio))]
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
            [FhirElement("due", InSummary=Hl7.Fhir.Model.Version.All, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.R4.Duration))]
            [DataMember]
            public Hl7.Fhir.Model.Element Due
            {
                get { return _Due; }
                set { _Due = value; OnPropertyChanged("Due"); }
            }
            
            private Hl7.Fhir.Model.Element _Due;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TargetComponent");
                base.Serialize(sink);
                sink.Element("measure", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Measure?.Serialize(sink);
                sink.Element("detail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Detail?.Serialize(sink);
                sink.Element("due", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Due?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "measure":
                        Measure = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "detailQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Detail, "detail");
                        Detail = source.Get<Hl7.Fhir.Model.Quantity>();
                        return true;
                    case "detailRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Detail, "detail");
                        Detail = source.Get<Hl7.Fhir.Model.Range>();
                        return true;
                    case "detailCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Detail, "detail");
                        Detail = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "detailString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Detail, "detail");
                        Detail = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "detailBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Detail, "detail");
                        Detail = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "detailInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Detail, "detail");
                        Detail = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "detailRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Detail, "detail");
                        Detail = source.Get<Hl7.Fhir.Model.Ratio>();
                        return true;
                    case "dueDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Due, "due");
                        Due = source.Get<Hl7.Fhir.Model.Date>();
                        return true;
                    case "dueDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Duration>(Due, "due");
                        Due = source.Get<Hl7.Fhir.Model.R4.Duration>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "measure":
                        Measure = source.Populate(Measure);
                        return true;
                    case "detailQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Detail, "detail");
                        Detail = source.Populate(Detail as Hl7.Fhir.Model.Quantity);
                        return true;
                    case "detailRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Detail, "detail");
                        Detail = source.Populate(Detail as Hl7.Fhir.Model.Range);
                        return true;
                    case "detailCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Detail, "detail");
                        Detail = source.Populate(Detail as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "detailString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Detail, "detail");
                        Detail = source.PopulateValue(Detail as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_detailString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Detail, "detail");
                        Detail = source.Populate(Detail as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "detailBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Detail, "detail");
                        Detail = source.PopulateValue(Detail as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "_detailBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Detail, "detail");
                        Detail = source.Populate(Detail as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "detailInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Detail, "detail");
                        Detail = source.PopulateValue(Detail as Hl7.Fhir.Model.Integer);
                        return true;
                    case "_detailInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Detail, "detail");
                        Detail = source.Populate(Detail as Hl7.Fhir.Model.Integer);
                        return true;
                    case "detailRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Detail, "detail");
                        Detail = source.Populate(Detail as Hl7.Fhir.Model.Ratio);
                        return true;
                    case "dueDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Due, "due");
                        Due = source.PopulateValue(Due as Hl7.Fhir.Model.Date);
                        return true;
                    case "_dueDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Due, "due");
                        Due = source.Populate(Due as Hl7.Fhir.Model.Date);
                        return true;
                    case "dueDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Duration>(Due, "due");
                        Due = source.Populate(Due as Hl7.Fhir.Model.R4.Duration);
                        return true;
                }
                return false;
            }
        
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
        /// proposed | planned | accepted | active | on-hold | completed | cancelled | entered-in-error | rejected
        /// </summary>
        [FhirElement("lifecycleStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.GoalLifecycleStatus> LifecycleStatusElement
        {
            get { return _LifecycleStatusElement; }
            set { _LifecycleStatusElement = value; OnPropertyChanged("LifecycleStatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.GoalLifecycleStatus> _LifecycleStatusElement;
        
        /// <summary>
        /// proposed | planned | accepted | active | on-hold | completed | cancelled | entered-in-error | rejected
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.GoalLifecycleStatus? LifecycleStatus
        {
            get { return LifecycleStatusElement != null ? LifecycleStatusElement.Value : null; }
            set
            {
                if (value == null)
                    LifecycleStatusElement = null;
                else
                    LifecycleStatusElement = new Code<Hl7.Fhir.Model.R4.GoalLifecycleStatus>(value);
                OnPropertyChanged("LifecycleStatus");
            }
        }
        
        /// <summary>
        /// in-progress | improving | worsening | no-change | achieved | sustaining | not-achieved | no-progress | not-attainable
        /// </summary>
        [FhirElement("achievementStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept AchievementStatus
        {
            get { return _AchievementStatus; }
            set { _AchievementStatus = value; OnPropertyChanged("AchievementStatus"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _AchievementStatus;
        
        /// <summary>
        /// E.g. Treatment, dietary, behavioral, etc.
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
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
        [FhirElement("priority", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
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
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
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
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [References("Patient","Group","Organization")]
        [Cardinality(Min=1,Max=1)]
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
        [FhirElement("start", InSummary=Hl7.Fhir.Model.Version.All, Order=160, Choice=ChoiceType.DatatypeChoice)]
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
        [FhirElement("target", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<TargetComponent> Target
        {
            get { if(_Target==null) _Target = new List<TargetComponent>(); return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        
        private List<TargetComponent> _Target;
        
        /// <summary>
        /// When goal status took effect
        /// </summary>
        [FhirElement("statusDate", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
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
        [FhirElement("statusReason", Order=190)]
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
        [FhirElement("expressedBy", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [References("Patient","Practitioner","PractitionerRole","RelatedPerson")]
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
        [FhirElement("addresses", Order=210)]
        [CLSCompliant(false)]
        [References("Condition","Observation","MedicationStatement","NutritionOrder","ServiceRequest","RiskAssessment")]
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
        [FhirElement("note", Order=220)]
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
        [FhirElement("outcomeCode", Order=230)]
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
        [FhirElement("outcomeReference", Order=240)]
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
    
    
        public static ElementDefinitionConstraint[] Goal_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "gol-1",
                severity: ConstraintSeverity.Warning,
                expression: "target.all((detail.exists() and measure.exists()) or detail.exists().not())",
                human: "Goal.target.measure is required if Goal.target.detail is populated",
                xpath: "(exists(f:*[starts-with(local-name(.), 'detail')]) and exists(f:measure)) or not(exists(f:*[starts-with(local-name(.), 'detail')]))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Goal_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Goal;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(LifecycleStatusElement != null) dest.LifecycleStatusElement = (Code<Hl7.Fhir.Model.R4.GoalLifecycleStatus>)LifecycleStatusElement.DeepCopy();
                if(AchievementStatus != null) dest.AchievementStatus = (Hl7.Fhir.Model.CodeableConcept)AchievementStatus.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.CodeableConcept)Priority.DeepCopy();
                if(Description != null) dest.Description = (Hl7.Fhir.Model.CodeableConcept)Description.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Start != null) dest.Start = (Hl7.Fhir.Model.Element)Start.DeepCopy();
                if(Target != null) dest.Target = new List<TargetComponent>(Target.DeepCopy());
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
            if( !DeepComparable.Matches(LifecycleStatusElement, otherT.LifecycleStatusElement)) return false;
            if( !DeepComparable.Matches(AchievementStatus, otherT.AchievementStatus)) return false;
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
            if( !DeepComparable.IsExactly(LifecycleStatusElement, otherT.LifecycleStatusElement)) return false;
            if( !DeepComparable.IsExactly(AchievementStatus, otherT.AchievementStatus)) return false;
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
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Goal");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("lifecycleStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); LifecycleStatusElement?.Serialize(sink);
            sink.Element("achievementStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AchievementStatus?.Serialize(sink);
            sink.BeginList("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Category)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("priority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Priority?.Serialize(sink);
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Description?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("start", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Start?.Serialize(sink);
            sink.BeginList("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Target)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("statusDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusDateElement?.Serialize(sink);
            sink.Element("statusReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); StatusReasonElement?.Serialize(sink);
            sink.Element("expressedBy", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExpressedBy?.Serialize(sink);
            sink.BeginList("addresses", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Addresses)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("outcomeCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in OutcomeCode)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("outcomeReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in OutcomeReference)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "lifecycleStatus":
                    LifecycleStatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.GoalLifecycleStatus>>();
                    return true;
                case "achievementStatus":
                    AchievementStatus = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "category":
                    Category = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "priority":
                    Priority = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "description":
                    Description = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "startDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Start, "start");
                    Start = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "startCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Start, "start");
                    Start = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "target":
                    Target = source.GetList<TargetComponent>();
                    return true;
                case "statusDate":
                    StatusDateElement = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "statusReason":
                    StatusReasonElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "expressedBy":
                    ExpressedBy = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "addresses":
                    Addresses = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "outcomeCode":
                    OutcomeCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "outcomeReference":
                    OutcomeReference = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
            }
            return false;
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "lifecycleStatus":
                    LifecycleStatusElement = source.PopulateValue(LifecycleStatusElement);
                    return true;
                case "_lifecycleStatus":
                    LifecycleStatusElement = source.Populate(LifecycleStatusElement);
                    return true;
                case "achievementStatus":
                    AchievementStatus = source.Populate(AchievementStatus);
                    return true;
                case "category":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "priority":
                    Priority = source.Populate(Priority);
                    return true;
                case "description":
                    Description = source.Populate(Description);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "startDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Start, "start");
                    Start = source.PopulateValue(Start as Hl7.Fhir.Model.Date);
                    return true;
                case "_startDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Start, "start");
                    Start = source.Populate(Start as Hl7.Fhir.Model.Date);
                    return true;
                case "startCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Start, "start");
                    Start = source.Populate(Start as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "target":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "statusDate":
                    StatusDateElement = source.PopulateValue(StatusDateElement);
                    return true;
                case "_statusDate":
                    StatusDateElement = source.Populate(StatusDateElement);
                    return true;
                case "statusReason":
                    StatusReasonElement = source.PopulateValue(StatusReasonElement);
                    return true;
                case "_statusReason":
                    StatusReasonElement = source.Populate(StatusReasonElement);
                    return true;
                case "expressedBy":
                    ExpressedBy = source.Populate(ExpressedBy);
                    return true;
                case "addresses":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "outcomeCode":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "outcomeReference":
                    source.SetList(this, jsonPropertyName);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "category":
                    source.PopulateListItem(Category, index);
                    return true;
                case "target":
                    source.PopulateListItem(Target, index);
                    return true;
                case "addresses":
                    source.PopulateListItem(Addresses, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "outcomeCode":
                    source.PopulateListItem(OutcomeCode, index);
                    return true;
                case "outcomeReference":
                    source.PopulateListItem(OutcomeReference, index);
                    return true;
            }
            return false;
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (LifecycleStatusElement != null) yield return LifecycleStatusElement;
                if (AchievementStatus != null) yield return AchievementStatus;
                foreach (var elem in Category) { if (elem != null) yield return elem; }
                if (Priority != null) yield return Priority;
                if (Description != null) yield return Description;
                if (Subject != null) yield return Subject;
                if (Start != null) yield return Start;
                foreach (var elem in Target) { if (elem != null) yield return elem; }
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
                if (LifecycleStatusElement != null) yield return new ElementValue("lifecycleStatus", LifecycleStatusElement);
                if (AchievementStatus != null) yield return new ElementValue("achievementStatus", AchievementStatus);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (Priority != null) yield return new ElementValue("priority", Priority);
                if (Description != null) yield return new ElementValue("description", Description);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Start != null) yield return new ElementValue("start", Start);
                foreach (var elem in Target) { if (elem != null) yield return new ElementValue("target", elem); }
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
