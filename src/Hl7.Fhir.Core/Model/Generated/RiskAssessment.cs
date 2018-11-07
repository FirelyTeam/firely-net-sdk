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
    /// Potential outcomes for a subject with likelihood
    /// </summary>
    [FhirType("RiskAssessment", IsResource=true)]
    [DataContract]
    public partial class RiskAssessment : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.RiskAssessment; } }
        [NotMapped]
        public override string TypeName { get { return "RiskAssessment"; } }
        
        [FhirType("PredictionComponent")]
        [DataContract]
        public partial class PredictionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "PredictionComponent"; } }
            
            /// <summary>
            /// Possible outcome for the subject
            /// </summary>
            [FhirElement("outcome", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Outcome
            {
                get { return _Outcome; }
                set { _Outcome = value; OnPropertyChanged("Outcome"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Outcome;
            
            /// <summary>
            /// Likelihood of specified outcome
            /// </summary>
            [FhirElement("probability", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Range))]
            [DataMember]
            public Hl7.Fhir.Model.Element Probability
            {
                get { return _Probability; }
                set { _Probability = value; OnPropertyChanged("Probability"); }
            }
            
            private Hl7.Fhir.Model.Element _Probability;
            
            /// <summary>
            /// Likelihood of specified outcome as a qualitative value
            /// </summary>
            [FhirElement("qualitativeRisk", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept QualitativeRisk
            {
                get { return _QualitativeRisk; }
                set { _QualitativeRisk = value; OnPropertyChanged("QualitativeRisk"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _QualitativeRisk;
            
            /// <summary>
            /// Relative likelihood
            /// </summary>
            [FhirElement("relativeRisk", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal RelativeRiskElement
            {
                get { return _RelativeRiskElement; }
                set { _RelativeRiskElement = value; OnPropertyChanged("RelativeRiskElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _RelativeRiskElement;
            
            /// <summary>
            /// Relative likelihood
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? RelativeRisk
            {
                get { return RelativeRiskElement != null ? RelativeRiskElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        RelativeRiskElement = null; 
                    else
                        RelativeRiskElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("RelativeRisk");
                }
            }
            
            /// <summary>
            /// Timeframe or age range
            /// </summary>
            [FhirElement("when", Order=80, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Range))]
            [DataMember]
            public Hl7.Fhir.Model.Element When
            {
                get { return _When; }
                set { _When = value; OnPropertyChanged("When"); }
            }
            
            private Hl7.Fhir.Model.Element _When;
            
            /// <summary>
            /// Explanation of prediction
            /// </summary>
            [FhirElement("rationale", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RationaleElement
            {
                get { return _RationaleElement; }
                set { _RationaleElement = value; OnPropertyChanged("RationaleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _RationaleElement;
            
            /// <summary>
            /// Explanation of prediction
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Rationale
            {
                get { return RationaleElement != null ? RationaleElement.Value : null; }
                set
                {
                    if (value == null)
                        RationaleElement = null; 
                    else
                        RationaleElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Rationale");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PredictionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                    if(Probability != null) dest.Probability = (Hl7.Fhir.Model.Element)Probability.DeepCopy();
                    if(QualitativeRisk != null) dest.QualitativeRisk = (Hl7.Fhir.Model.CodeableConcept)QualitativeRisk.DeepCopy();
                    if(RelativeRiskElement != null) dest.RelativeRiskElement = (Hl7.Fhir.Model.FhirDecimal)RelativeRiskElement.DeepCopy();
                    if(When != null) dest.When = (Hl7.Fhir.Model.Element)When.DeepCopy();
                    if(RationaleElement != null) dest.RationaleElement = (Hl7.Fhir.Model.FhirString)RationaleElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PredictionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PredictionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
                if( !DeepComparable.Matches(Probability, otherT.Probability)) return false;
                if( !DeepComparable.Matches(QualitativeRisk, otherT.QualitativeRisk)) return false;
                if( !DeepComparable.Matches(RelativeRiskElement, otherT.RelativeRiskElement)) return false;
                if( !DeepComparable.Matches(When, otherT.When)) return false;
                if( !DeepComparable.Matches(RationaleElement, otherT.RationaleElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PredictionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
                if( !DeepComparable.IsExactly(Probability, otherT.Probability)) return false;
                if( !DeepComparable.IsExactly(QualitativeRisk, otherT.QualitativeRisk)) return false;
                if( !DeepComparable.IsExactly(RelativeRiskElement, otherT.RelativeRiskElement)) return false;
                if( !DeepComparable.IsExactly(When, otherT.When)) return false;
                if( !DeepComparable.IsExactly(RationaleElement, otherT.RationaleElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Outcome != null) yield return Outcome;
                    if (Probability != null) yield return Probability;
                    if (QualitativeRisk != null) yield return QualitativeRisk;
                    if (RelativeRiskElement != null) yield return RelativeRiskElement;
                    if (When != null) yield return When;
                    if (RationaleElement != null) yield return RationaleElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Outcome != null) yield return new ElementValue("outcome", Outcome);
                    if (Probability != null) yield return new ElementValue("probability", Probability);
                    if (QualitativeRisk != null) yield return new ElementValue("qualitativeRisk", QualitativeRisk);
                    if (RelativeRiskElement != null) yield return new ElementValue("relativeRisk", RelativeRiskElement);
                    if (When != null) yield return new ElementValue("when", When);
                    if (RationaleElement != null) yield return new ElementValue("rationale", RationaleElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Unique identifier for the assessment
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Request fulfilled by this assessment
        /// </summary>
        [FhirElement("basedOn", Order=100)]
        [CLSCompliant(false)]
		[References()]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference BasedOn
        {
            get { return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _BasedOn;
        
        /// <summary>
        /// Part of this occurrence
        /// </summary>
        [FhirElement("parent", Order=110)]
        [CLSCompliant(false)]
		[References()]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Parent
        {
            get { return _Parent; }
            set { _Parent = value; OnPropertyChanged("Parent"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Parent;
        
        /// <summary>
        /// registered | preliminary | final | amended +
        /// </summary>
        [FhirElement("status", Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ObservationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ObservationStatus> _StatusElement;
        
        /// <summary>
        /// registered | preliminary | final | amended +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ObservationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ObservationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Evaluation mechanism
        /// </summary>
        [FhirElement("method", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Method
        {
            get { return _Method; }
            set { _Method = value; OnPropertyChanged("Method"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Method;
        
        /// <summary>
        /// Type of assessment
        /// </summary>
        [FhirElement("code", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// Who/what does assessment apply to?
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Patient","Group")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Where was assessment performed?
        /// </summary>
        [FhirElement("context", InSummary=true, Order=160)]
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
        /// When was assessment made?
        /// </summary>
        [FhirElement("occurrence", InSummary=true, Order=170, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Occurrence
        {
            get { return _Occurrence; }
            set { _Occurrence = value; OnPropertyChanged("Occurrence"); }
        }
        
        private Hl7.Fhir.Model.Element _Occurrence;
        
        /// <summary>
        /// Condition assessed
        /// </summary>
        [FhirElement("condition", InSummary=true, Order=180)]
        [CLSCompliant(false)]
		[References("Condition")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Condition
        {
            get { return _Condition; }
            set { _Condition = value; OnPropertyChanged("Condition"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Condition;
        
        /// <summary>
        /// Who did assessment?
        /// </summary>
        [FhirElement("performer", InSummary=true, Order=190)]
        [CLSCompliant(false)]
		[References("Practitioner","Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Performer
        {
            get { return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Performer;
        
        /// <summary>
        /// Why the assessment was necessary?
        /// </summary>
        [FhirElement("reason", Order=200, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private Hl7.Fhir.Model.Element _Reason;
        
        /// <summary>
        /// Information used in assessment
        /// </summary>
        [FhirElement("basis", Order=210)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Basis
        {
            get { if(_Basis==null) _Basis = new List<Hl7.Fhir.Model.ResourceReference>(); return _Basis; }
            set { _Basis = value; OnPropertyChanged("Basis"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Basis;
        
        /// <summary>
        /// Outcome predicted
        /// </summary>
        [FhirElement("prediction", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.RiskAssessment.PredictionComponent> Prediction
        {
            get { if(_Prediction==null) _Prediction = new List<Hl7.Fhir.Model.RiskAssessment.PredictionComponent>(); return _Prediction; }
            set { _Prediction = value; OnPropertyChanged("Prediction"); }
        }
        
        private List<Hl7.Fhir.Model.RiskAssessment.PredictionComponent> _Prediction;
        
        /// <summary>
        /// How to reduce risk
        /// </summary>
        [FhirElement("mitigation", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString MitigationElement
        {
            get { return _MitigationElement; }
            set { _MitigationElement = value; OnPropertyChanged("MitigationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _MitigationElement;
        
        /// <summary>
        /// How to reduce risk
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Mitigation
        {
            get { return MitigationElement != null ? MitigationElement.Value : null; }
            set
            {
                if (value == null)
                  MitigationElement = null; 
                else
                  MitigationElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Mitigation");
            }
        }
        
        /// <summary>
        /// Comments on the risk assessment
        /// </summary>
        [FhirElement("comment", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentElement
        {
            get { return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CommentElement;
        
        /// <summary>
        /// Comments on the risk assessment
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comment
        {
            get { return CommentElement != null ? CommentElement.Value : null; }
            set
            {
                if (value == null)
                  CommentElement = null; 
                else
                  CommentElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Comment");
            }
        }
        

        public static ElementDefinition.ConstraintComponent RiskAssessment_RAS_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "prediction.all(probability is decimal implies probability.as(decimal) <= 100)",
            Key = "ras-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Must be <= 100",
            Xpath = "not(f:probabilityDecimal) or f:probabilityDecimal/@value <= 100"
        };

        public static ElementDefinition.ConstraintComponent RiskAssessment_RAS_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "prediction.probability.all((low.empty() or ((low.code = '%') and (low.system = %ucum))) and (high.empty() or ((high.code = '%') and (high.system = %ucum))))",
            Key = "ras-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "low and high must be percentages, if present",
            Xpath = "(not(f:low) or f:low[f:code/@value='%' and f:system/@value='http://unitsofmeasure.org']) and (not(f:high) or f:high[f:code/@value='%' and f:system/@value='http://unitsofmeasure.org'])"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(RiskAssessment_RAS_2);
            InvariantConstraints.Add(RiskAssessment_RAS_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as RiskAssessment;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(BasedOn != null) dest.BasedOn = (Hl7.Fhir.Model.ResourceReference)BasedOn.DeepCopy();
                if(Parent != null) dest.Parent = (Hl7.Fhir.Model.ResourceReference)Parent.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ObservationStatus>)StatusElement.DeepCopy();
                if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(Occurrence != null) dest.Occurrence = (Hl7.Fhir.Model.Element)Occurrence.DeepCopy();
                if(Condition != null) dest.Condition = (Hl7.Fhir.Model.ResourceReference)Condition.DeepCopy();
                if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.Element)Reason.DeepCopy();
                if(Basis != null) dest.Basis = new List<Hl7.Fhir.Model.ResourceReference>(Basis.DeepCopy());
                if(Prediction != null) dest.Prediction = new List<Hl7.Fhir.Model.RiskAssessment.PredictionComponent>(Prediction.DeepCopy());
                if(MitigationElement != null) dest.MitigationElement = (Hl7.Fhir.Model.FhirString)MitigationElement.DeepCopy();
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new RiskAssessment());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as RiskAssessment;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(Parent, otherT.Parent)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Method, otherT.Method)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.Matches(Condition, otherT.Condition)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Basis, otherT.Basis)) return false;
            if( !DeepComparable.Matches(Prediction, otherT.Prediction)) return false;
            if( !DeepComparable.Matches(MitigationElement, otherT.MitigationElement)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as RiskAssessment;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(Parent, otherT.Parent)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.IsExactly(Condition, otherT.Condition)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Basis, otherT.Basis)) return false;
            if( !DeepComparable.IsExactly(Prediction, otherT.Prediction)) return false;
            if( !DeepComparable.IsExactly(MitigationElement, otherT.MitigationElement)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Identifier != null) yield return Identifier;
				if (BasedOn != null) yield return BasedOn;
				if (Parent != null) yield return Parent;
				if (StatusElement != null) yield return StatusElement;
				if (Method != null) yield return Method;
				if (Code != null) yield return Code;
				if (Subject != null) yield return Subject;
				if (Context != null) yield return Context;
				if (Occurrence != null) yield return Occurrence;
				if (Condition != null) yield return Condition;
				if (Performer != null) yield return Performer;
				if (Reason != null) yield return Reason;
				foreach (var elem in Basis) { if (elem != null) yield return elem; }
				foreach (var elem in Prediction) { if (elem != null) yield return elem; }
				if (MitigationElement != null) yield return MitigationElement;
				if (CommentElement != null) yield return CommentElement;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (BasedOn != null) yield return new ElementValue("basedOn", BasedOn);
                if (Parent != null) yield return new ElementValue("parent", Parent);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Method != null) yield return new ElementValue("method", Method);
                if (Code != null) yield return new ElementValue("code", Code);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (Occurrence != null) yield return new ElementValue("occurrence", Occurrence);
                if (Condition != null) yield return new ElementValue("condition", Condition);
                if (Performer != null) yield return new ElementValue("performer", Performer);
                if (Reason != null) yield return new ElementValue("reason", Reason);
                foreach (var elem in Basis) { if (elem != null) yield return new ElementValue("basis", elem); }
                foreach (var elem in Prediction) { if (elem != null) yield return new ElementValue("prediction", elem); }
                if (MitigationElement != null) yield return new ElementValue("mitigation", MitigationElement);
                if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
            }
        }

    }
    
}
