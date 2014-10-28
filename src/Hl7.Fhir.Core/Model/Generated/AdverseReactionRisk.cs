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
// Generated on Tue, Oct 28, 2014 16:11+0100 for FHIR v0.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Adverse reaction risk
    /// </summary>
    [FhirType("AdverseReactionRisk", IsResource=true)]
    [DataContract]
    public partial class AdverseReactionRisk : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Statement about the degree of clinical certainty that a Specific Substance was the cause of the Manifestation in an reaction event
        /// </summary>
        [FhirEnumeration("AdverseReactionRiskCertainty")]
        public enum AdverseReactionRiskCertainty
        {
            /// <summary>
            /// There is a low level of clinical certainty that the reaction was caused by the identified Substance.
            /// </summary>
            [EnumLiteral("unlikely")]
            Unlikely,
            /// <summary>
            /// There is a high level of clinical certainty that the reaction was caused by the identified Substance.
            /// </summary>
            [EnumLiteral("likely")]
            Likely,
            /// <summary>
            /// There is a very high level of clinical certainty that the reaction was due to the identified Substance, which may include clinical evidence by testing or rechallenge.
            /// </summary>
            [EnumLiteral("confirmed")]
            Confirmed,
        }
        
        /// <summary>
        /// Assertion about certainty associated with a propensity, or potential risk, of a reaction to the identified Substance
        /// </summary>
        [FhirEnumeration("AdverseReactionRiskStatus")]
        public enum AdverseReactionRiskStatus
        {
            /// <summary>
            /// A low level of certainty about the propensity for a reaction to the identified Substance.
            /// </summary>
            [EnumLiteral("unconfirmed")]
            Unconfirmed,
            /// <summary>
            /// A high level of certainty about the propensity for a reaction to the identified Substance, which may include clinical evidence by testing or rechallenge.
            /// </summary>
            [EnumLiteral("confirmed")]
            Confirmed,
            /// <summary>
            /// A reaction to the identified Substance has been clinically reassessed by testing or rechallenge and considered to be resolved.
            /// </summary>
            [EnumLiteral("resolved")]
            Resolved,
            /// <summary>
            /// A propensity for a reaction to the identified Substance has been disproven with a high level of clinical certainty, which may include testing or rechallenge, and is refuted.
            /// </summary>
            [EnumLiteral("refuted")]
            Refuted,
        }
        
        /// <summary>
        /// Category of an identified Substance
        /// </summary>
        [FhirEnumeration("AdverseReactionRiskCategory")]
        public enum AdverseReactionRiskCategory
        {
            /// <summary>
            /// Any substance consumed to provide nutritional support for the body.
            /// </summary>
            [EnumLiteral("food")]
            Food,
            /// <summary>
            /// Substances administered to achieve a physiological effect.
            /// </summary>
            [EnumLiteral("medication")]
            Medication,
            /// <summary>
            /// Substances that are encountered in the environment.
            /// </summary>
            [EnumLiteral("environment")]
            Environment,
        }
        
        /// <summary>
        /// Estimate of the potential clinical harm, or seriousness, of a reaction to an identified Substance
        /// </summary>
        [FhirEnumeration("AdverseReactionRiskCriticality")]
        public enum AdverseReactionRiskCriticality
        {
            /// <summary>
            /// The potential clinical impact of a future reaction is estimated as low risk. Future exposure to the Substance is considered a relative contra-indication.
            /// </summary>
            [EnumLiteral("low")]
            Low,
            /// <summary>
            /// The potential clinical impact of a future reaction is estimated as high risk. Future exposure to the Substance may be considered an absolute contra-indication.
            /// </summary>
            [EnumLiteral("high")]
            High,
        }
        
        /// <summary>
        /// Clinical assessment of the severity of a reaction event as a whole, potentially considering multiple different manifestations
        /// </summary>
        [FhirEnumeration("AdverseReactionRiskSeverity")]
        public enum AdverseReactionRiskSeverity
        {
            /// <summary>
            /// Causes mild physiological effects.
            /// </summary>
            [EnumLiteral("mild")]
            Mild,
            /// <summary>
            /// Causes moderate physiological effects.
            /// </summary>
            [EnumLiteral("moderate")]
            Moderate,
            /// <summary>
            /// Causes severe physiological effects.
            /// </summary>
            [EnumLiteral("severe")]
            Severe,
        }
        
        /// <summary>
        /// Identification of the underlying physiological mechanism for a Reaction Risk
        /// </summary>
        [FhirEnumeration("AdverseReactionRiskType")]
        public enum AdverseReactionRiskType
        {
            /// <summary>
            /// Immune mediated reaction, including allergic reactions and hypersensitivities.
            /// </summary>
            [EnumLiteral("immune")]
            Immune,
            /// <summary>
            /// A non-immune mediated reaction, which can include pseudoallergic reactions, side effects, intolerances, drug toxicities (eg to Gentamicin), drug-drug interactions, food-drug interactions, and drug-disease interactions.
            /// </summary>
            [EnumLiteral("non-immune")]
            NonImmune,
        }
        
        [FhirType("AdverseReactionRiskEventComponent")]
        [DataContract]
        public partial class AdverseReactionRiskEventComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Specific substance considered to be responsible for event
            /// </summary>
            [FhirElement("substance", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Substance
            {
                get { return _Substance; }
                set { _Substance = value; OnPropertyChanged("Substance"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Substance;
            
            /// <summary>
            /// unlikely | likely | confirmed - clinical certainty about the specific substance
            /// </summary>
            [FhirElement("certainty", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCertainty> CertaintyElement
            {
                get { return _CertaintyElement; }
                set { _CertaintyElement = value; OnPropertyChanged("CertaintyElement"); }
            }
            private Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCertainty> _CertaintyElement;
            
            /// <summary>
            /// unlikely | likely | confirmed - clinical certainty about the specific substance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCertainty? Certainty
            {
                get { return CertaintyElement != null ? CertaintyElement.Value : null; }
                set
                {
                    if(value == null)
                      CertaintyElement = null; 
                    else
                      CertaintyElement = new Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCertainty>(value);
                    OnPropertyChanged("Certainty");
                }
            }
            
            /// <summary>
            /// Clinical symptoms/signs associated with the Event
            /// </summary>
            [FhirElement("manifestation", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Manifestation
            {
                get { return _Manifestation; }
                set { _Manifestation = value; OnPropertyChanged("Manifestation"); }
            }
            private List<Hl7.Fhir.Model.CodeableConcept> _Manifestation;
            
            /// <summary>
            /// Description of the event as a whole
            /// </summary>
            [FhirElement("description", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Description of the event as a whole
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
            /// Date(/time) when manifestations showed
            /// </summary>
            [FhirElement("onset", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime OnsetElement
            {
                get { return _OnsetElement; }
                set { _OnsetElement = value; OnPropertyChanged("OnsetElement"); }
            }
            private Hl7.Fhir.Model.FhirDateTime _OnsetElement;
            
            /// <summary>
            /// Date(/time) when manifestations showed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Onset
            {
                get { return OnsetElement != null ? OnsetElement.Value : null; }
                set
                {
                    if(value == null)
                      OnsetElement = null; 
                    else
                      OnsetElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Onset");
                }
            }
            
            /// <summary>
            /// How long Manifestations persisted
            /// </summary>
            [FhirElement("duration", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Duration Duration
            {
                get { return _Duration; }
                set { _Duration = value; OnPropertyChanged("Duration"); }
            }
            private Hl7.Fhir.Model.Duration _Duration;
            
            /// <summary>
            /// mild | moderate | severe (of event as a whole)
            /// </summary>
            [FhirElement("severity", InSummary=true, Order=100)]
            [DataMember]
            public Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskSeverity> SeverityElement
            {
                get { return _SeverityElement; }
                set { _SeverityElement = value; OnPropertyChanged("SeverityElement"); }
            }
            private Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskSeverity> _SeverityElement;
            
            /// <summary>
            /// mild | moderate | severe (of event as a whole)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskSeverity? Severity
            {
                get { return SeverityElement != null ? SeverityElement.Value : null; }
                set
                {
                    if(value == null)
                      SeverityElement = null; 
                    else
                      SeverityElement = new Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskSeverity>(value);
                    OnPropertyChanged("Severity");
                }
            }
            
            /// <summary>
            /// How the subject was exposed to the substance
            /// </summary>
            [FhirElement("exposureRoute", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ExposureRoute
            {
                get { return _ExposureRoute; }
                set { _ExposureRoute = value; OnPropertyChanged("ExposureRoute"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _ExposureRoute;
            
            /// <summary>
            /// Text about event not captured in other fields
            /// </summary>
            [FhirElement("comment", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentElement
            {
                get { return _CommentElement; }
                set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
            }
            private Hl7.Fhir.Model.FhirString _CommentElement;
            
            /// <summary>
            /// Text about event not captured in other fields
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Comment
            {
                get { return CommentElement != null ? CommentElement.Value : null; }
                set
                {
                    if(value == null)
                      CommentElement = null; 
                    else
                      CommentElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Comment");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AdverseReactionRiskEventComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Substance != null) dest.Substance = (Hl7.Fhir.Model.CodeableConcept)Substance.DeepCopy();
                    if(CertaintyElement != null) dest.CertaintyElement = (Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCertainty>)CertaintyElement.DeepCopy();
                    if(Manifestation != null) dest.Manifestation = new List<Hl7.Fhir.Model.CodeableConcept>(Manifestation.DeepCopy());
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(OnsetElement != null) dest.OnsetElement = (Hl7.Fhir.Model.FhirDateTime)OnsetElement.DeepCopy();
                    if(Duration != null) dest.Duration = (Hl7.Fhir.Model.Duration)Duration.DeepCopy();
                    if(SeverityElement != null) dest.SeverityElement = (Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskSeverity>)SeverityElement.DeepCopy();
                    if(ExposureRoute != null) dest.ExposureRoute = (Hl7.Fhir.Model.CodeableConcept)ExposureRoute.DeepCopy();
                    if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AdverseReactionRiskEventComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AdverseReactionRiskEventComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Substance, otherT.Substance)) return false;
                if( !DeepComparable.Matches(CertaintyElement, otherT.CertaintyElement)) return false;
                if( !DeepComparable.Matches(Manifestation, otherT.Manifestation)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(OnsetElement, otherT.OnsetElement)) return false;
                if( !DeepComparable.Matches(Duration, otherT.Duration)) return false;
                if( !DeepComparable.Matches(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.Matches(ExposureRoute, otherT.ExposureRoute)) return false;
                if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AdverseReactionRiskEventComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Substance, otherT.Substance)) return false;
                if( !DeepComparable.IsExactly(CertaintyElement, otherT.CertaintyElement)) return false;
                if( !DeepComparable.IsExactly(Manifestation, otherT.Manifestation)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(OnsetElement, otherT.OnsetElement)) return false;
                if( !DeepComparable.IsExactly(Duration, otherT.Duration)) return false;
                if( !DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.IsExactly(ExposureRoute, otherT.ExposureRoute)) return false;
                if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External Ids for this item
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=60)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// When recorded
        /// </summary>
        [FhirElement("recordedDate", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime RecordedDateElement
        {
            get { return _RecordedDateElement; }
            set { _RecordedDateElement = value; OnPropertyChanged("RecordedDateElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _RecordedDateElement;
        
        /// <summary>
        /// When recorded
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RecordedDate
        {
            get { return RecordedDateElement != null ? RecordedDateElement.Value : null; }
            set
            {
                if(value == null)
                  RecordedDateElement = null; 
                else
                  RecordedDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("RecordedDate");
            }
        }
        
        /// <summary>
        /// Who recorded the sensitivity
        /// </summary>
        [FhirElement("recorder", InSummary=true, Order=80)]
        [References("Practitioner","Patient")]
        [DataMember]
        public Hl7.Fhir.Model.Reference Recorder
        {
            get { return _Recorder; }
            set { _Recorder = value; OnPropertyChanged("Recorder"); }
        }
        private Hl7.Fhir.Model.Reference _Recorder;
        
        /// <summary>
        /// Who the sensitivity is for
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=90)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Reference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        private Hl7.Fhir.Model.Reference _Subject;
        
        /// <summary>
        /// Ssubstance, (or class) considered to be responsible for risk
        /// </summary>
        [FhirElement("substance", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Substance
        {
            get { return _Substance; }
            set { _Substance = value; OnPropertyChanged("Substance"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Substance;
        
        /// <summary>
        /// unconfirmed | confirmed | resolved | refuted
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskStatus> _StatusElement;
        
        /// <summary>
        /// unconfirmed | confirmed | resolved | refuted
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// low | high - Estimated potential clinical harm
        /// </summary>
        [FhirElement("criticality", InSummary=true, Order=120)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCriticality> CriticalityElement
        {
            get { return _CriticalityElement; }
            set { _CriticalityElement = value; OnPropertyChanged("CriticalityElement"); }
        }
        private Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCriticality> _CriticalityElement;
        
        /// <summary>
        /// low | high - Estimated potential clinical harm
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCriticality? Criticality
        {
            get { return CriticalityElement != null ? CriticalityElement.Value : null; }
            set
            {
                if(value == null)
                  CriticalityElement = null; 
                else
                  CriticalityElement = new Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCriticality>(value);
                OnPropertyChanged("Criticality");
            }
        }
        
        /// <summary>
        /// immune | non-immune - Underlying mechanism (if known)
        /// </summary>
        [FhirElement("type", InSummary=true, Order=130)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        private Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskType> _TypeElement;
        
        /// <summary>
        /// immune | non-immune - Underlying mechanism (if known)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if(value == null)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// food | medication | environment - Category of Substance
        /// </summary>
        [FhirElement("category", InSummary=true, Order=140)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCategory> CategoryElement
        {
            get { return _CategoryElement; }
            set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
        }
        private Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCategory> _CategoryElement;
        
        /// <summary>
        /// food | medication | environment - Category of Substance
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCategory? Category
        {
            get { return CategoryElement != null ? CategoryElement.Value : null; }
            set
            {
                if(value == null)
                  CategoryElement = null; 
                else
                  CategoryElement = new Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCategory>(value);
                OnPropertyChanged("Category");
            }
        }
        
        /// <summary>
        /// Date(/time) of last known occurence of a reaction
        /// </summary>
        [FhirElement("lastOccurence", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime LastOccurenceElement
        {
            get { return _LastOccurenceElement; }
            set { _LastOccurenceElement = value; OnPropertyChanged("LastOccurenceElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _LastOccurenceElement;
        
        /// <summary>
        /// Date(/time) of last known occurence of a reaction
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LastOccurence
        {
            get { return LastOccurenceElement != null ? LastOccurenceElement.Value : null; }
            set
            {
                if(value == null)
                  LastOccurenceElement = null; 
                else
                  LastOccurenceElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("LastOccurence");
            }
        }
        
        /// <summary>
        /// Additional text not captured in other fields
        /// </summary>
        [FhirElement("comment", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentElement
        {
            get { return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        private Hl7.Fhir.Model.FhirString _CommentElement;
        
        /// <summary>
        /// Additional text not captured in other fields
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comment
        {
            get { return CommentElement != null ? CommentElement.Value : null; }
            set
            {
                if(value == null)
                  CommentElement = null; 
                else
                  CommentElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Comment");
            }
        }
        
        /// <summary>
        /// Adverse Reaction Events linked to exposure to substance
        /// </summary>
        [FhirElement("event", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskEventComponent> Event
        {
            get { return _Event; }
            set { _Event = value; OnPropertyChanged("Event"); }
        }
        private List<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskEventComponent> _Event;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as AdverseReactionRisk;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(RecordedDateElement != null) dest.RecordedDateElement = (Hl7.Fhir.Model.FhirDateTime)RecordedDateElement.DeepCopy();
                if(Recorder != null) dest.Recorder = (Hl7.Fhir.Model.Reference)Recorder.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.Reference)Subject.DeepCopy();
                if(Substance != null) dest.Substance = (Hl7.Fhir.Model.CodeableConcept)Substance.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskStatus>)StatusElement.DeepCopy();
                if(CriticalityElement != null) dest.CriticalityElement = (Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCriticality>)CriticalityElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskType>)TypeElement.DeepCopy();
                if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskCategory>)CategoryElement.DeepCopy();
                if(LastOccurenceElement != null) dest.LastOccurenceElement = (Hl7.Fhir.Model.FhirDateTime)LastOccurenceElement.DeepCopy();
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                if(Event != null) dest.Event = new List<Hl7.Fhir.Model.AdverseReactionRisk.AdverseReactionRiskEventComponent>(Event.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new AdverseReactionRisk());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as AdverseReactionRisk;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(RecordedDateElement, otherT.RecordedDateElement)) return false;
            if( !DeepComparable.Matches(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Substance, otherT.Substance)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(CriticalityElement, otherT.CriticalityElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.Matches(LastOccurenceElement, otherT.LastOccurenceElement)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.Matches(Event, otherT.Event)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as AdverseReactionRisk;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(RecordedDateElement, otherT.RecordedDateElement)) return false;
            if( !DeepComparable.IsExactly(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Substance, otherT.Substance)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(CriticalityElement, otherT.CriticalityElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.IsExactly(LastOccurenceElement, otherT.LastOccurenceElement)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
            
            return true;
        }
        
    }
    
}
