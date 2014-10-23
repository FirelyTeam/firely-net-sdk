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
    /// Specific reactions to a substance
    /// </summary>
    [FhirType("AdverseReaction", IsResource=true)]
    [DataContract]
    public partial class AdverseReaction : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// The severity of an adverse reaction.
        /// </summary>
        [FhirEnumeration("ReactionSeverity")]
        public enum ReactionSeverity
        {
            /// <summary>
            /// Severe complications arose due to the reaction.
            /// </summary>
            [EnumLiteral("severe")]
            Severe,
            /// <summary>
            /// Serious inconvenience to the subject.
            /// </summary>
            [EnumLiteral("serious")]
            Serious,
            /// <summary>
            /// Moderate inconvenience to the subject.
            /// </summary>
            [EnumLiteral("moderate")]
            Moderate,
            /// <summary>
            /// Minor inconvenience to the subject.
            /// </summary>
            [EnumLiteral("minor")]
            Minor,
        }
        
        /// <summary>
        /// The type of exposure that resulted in an adverse reaction
        /// </summary>
        [FhirEnumeration("ExposureType")]
        public enum ExposureType
        {
            /// <summary>
            /// Drug Administration.
            /// </summary>
            [EnumLiteral("drugadmin")]
            Drugadmin,
            /// <summary>
            /// Immunization.
            /// </summary>
            [EnumLiteral("immuniz")]
            Immuniz,
            /// <summary>
            /// In the same area as the substance.
            /// </summary>
            [EnumLiteral("coincidental")]
            Coincidental,
        }
        
        /// <summary>
        /// How likely is it that the given exposure caused a reaction
        /// </summary>
        [FhirEnumeration("CausalityExpectation")]
        public enum CausalityExpectation
        {
            /// <summary>
            /// Likely that this specific exposure caused the reaction.
            /// </summary>
            [EnumLiteral("likely")]
            Likely,
            /// <summary>
            /// Unlikely that this specific exposure caused the reaction - the exposure is being linked to for information purposes.
            /// </summary>
            [EnumLiteral("unlikely")]
            Unlikely,
            /// <summary>
            /// It has been confirmed that this exposure was one of the causes of the reaction.
            /// </summary>
            [EnumLiteral("confirmed")]
            Confirmed,
            /// <summary>
            /// It is unknown whether this exposure had anything to do with the reaction.
            /// </summary>
            [EnumLiteral("unknown")]
            Unknown,
        }
        
        [FhirType("AdverseReactionSymptomComponent")]
        [DataContract]
        public partial class AdverseReactionSymptomComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// E.g. Rash, vomiting
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// severe | serious | moderate | minor
            /// </summary>
            [FhirElement("severity", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.AdverseReaction.ReactionSeverity> SeverityElement
            {
                get { return _SeverityElement; }
                set { _SeverityElement = value; OnPropertyChanged("SeverityElement"); }
            }
            private Code<Hl7.Fhir.Model.AdverseReaction.ReactionSeverity> _SeverityElement;
            
            /// <summary>
            /// severe | serious | moderate | minor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AdverseReaction.ReactionSeverity? Severity
            {
                get { return SeverityElement != null ? SeverityElement.Value : null; }
                set
                {
                    if(value == null)
                      SeverityElement = null; 
                    else
                      SeverityElement = new Code<Hl7.Fhir.Model.AdverseReaction.ReactionSeverity>(value);
                    OnPropertyChanged("Severity");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AdverseReactionSymptomComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(SeverityElement != null) dest.SeverityElement = (Code<Hl7.Fhir.Model.AdverseReaction.ReactionSeverity>)SeverityElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AdverseReactionSymptomComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AdverseReactionSymptomComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(SeverityElement, otherT.SeverityElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AdverseReactionSymptomComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("AdverseReactionExposureComponent")]
        [DataContract]
        public partial class AdverseReactionExposureComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// When the exposure occurred
            /// </summary>
            [FhirElement("date", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            private Hl7.Fhir.Model.FhirDateTime _DateElement;
            
            /// <summary>
            /// When the exposure occurred
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Date
            {
                get { return DateElement != null ? DateElement.Value : null; }
                set
                {
                    if(value == null)
                      DateElement = null; 
                    else
                      DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Date");
                }
            }
            
            /// <summary>
            /// drugadmin | immuniz | coincidental
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.AdverseReaction.ExposureType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Code<Hl7.Fhir.Model.AdverseReaction.ExposureType> _TypeElement;
            
            /// <summary>
            /// drugadmin | immuniz | coincidental
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AdverseReaction.ExposureType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.AdverseReaction.ExposureType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// likely | unlikely | confirmed | unknown
            /// </summary>
            [FhirElement("causalityExpectation", InSummary=true, Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.AdverseReaction.CausalityExpectation> CausalityExpectationElement
            {
                get { return _CausalityExpectationElement; }
                set { _CausalityExpectationElement = value; OnPropertyChanged("CausalityExpectationElement"); }
            }
            private Code<Hl7.Fhir.Model.AdverseReaction.CausalityExpectation> _CausalityExpectationElement;
            
            /// <summary>
            /// likely | unlikely | confirmed | unknown
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AdverseReaction.CausalityExpectation? CausalityExpectation
            {
                get { return CausalityExpectationElement != null ? CausalityExpectationElement.Value : null; }
                set
                {
                    if(value == null)
                      CausalityExpectationElement = null; 
                    else
                      CausalityExpectationElement = new Code<Hl7.Fhir.Model.AdverseReaction.CausalityExpectation>(value);
                    OnPropertyChanged("CausalityExpectation");
                }
            }
            
            /// <summary>
            /// Presumed causative substance
            /// </summary>
            [FhirElement("substance", InSummary=true, Order=70)]
            [References("Substance")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Substance
            {
                get { return _Substance; }
                set { _Substance = value; OnPropertyChanged("Substance"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Substance;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AdverseReactionExposureComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.AdverseReaction.ExposureType>)TypeElement.DeepCopy();
                    if(CausalityExpectationElement != null) dest.CausalityExpectationElement = (Code<Hl7.Fhir.Model.AdverseReaction.CausalityExpectation>)CausalityExpectationElement.DeepCopy();
                    if(Substance != null) dest.Substance = (Hl7.Fhir.Model.ResourceReference)Substance.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AdverseReactionExposureComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AdverseReactionExposureComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(CausalityExpectationElement, otherT.CausalityExpectationElement)) return false;
                if( !DeepComparable.Matches(Substance, otherT.Substance)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AdverseReactionExposureComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(CausalityExpectationElement, otherT.CausalityExpectationElement)) return false;
                if( !DeepComparable.IsExactly(Substance, otherT.Substance)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External Ids for this adverse reaction
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
        /// When the reaction occurred
        /// </summary>
        [FhirElement("date", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// When the reaction occurred
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if(value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Who had the reaction
        /// </summary>
        [FhirElement("subject", Order=90)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Indicates lack of reaction
        /// </summary>
        [FhirElement("didNotOccurFlag", Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean DidNotOccurFlagElement
        {
            get { return _DidNotOccurFlagElement; }
            set { _DidNotOccurFlagElement = value; OnPropertyChanged("DidNotOccurFlagElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _DidNotOccurFlagElement;
        
        /// <summary>
        /// Indicates lack of reaction
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? DidNotOccurFlag
        {
            get { return DidNotOccurFlagElement != null ? DidNotOccurFlagElement.Value : null; }
            set
            {
                if(value == null)
                  DidNotOccurFlagElement = null; 
                else
                  DidNotOccurFlagElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("DidNotOccurFlag");
            }
        }
        
        /// <summary>
        /// Who recorded the reaction
        /// </summary>
        [FhirElement("recorder", Order=110)]
        [References("Practitioner","Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recorder
        {
            get { return _Recorder; }
            set { _Recorder = value; OnPropertyChanged("Recorder"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Recorder;
        
        /// <summary>
        /// What was reaction?
        /// </summary>
        [FhirElement("symptom", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.AdverseReaction.AdverseReactionSymptomComponent> Symptom
        {
            get { return _Symptom; }
            set { _Symptom = value; OnPropertyChanged("Symptom"); }
        }
        private List<Hl7.Fhir.Model.AdverseReaction.AdverseReactionSymptomComponent> _Symptom;
        
        /// <summary>
        /// Suspected substance
        /// </summary>
        [FhirElement("exposure", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.AdverseReaction.AdverseReactionExposureComponent> Exposure
        {
            get { return _Exposure; }
            set { _Exposure = value; OnPropertyChanged("Exposure"); }
        }
        private List<Hl7.Fhir.Model.AdverseReaction.AdverseReactionExposureComponent> _Exposure;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as AdverseReaction;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(DidNotOccurFlagElement != null) dest.DidNotOccurFlagElement = (Hl7.Fhir.Model.FhirBoolean)DidNotOccurFlagElement.DeepCopy();
                if(Recorder != null) dest.Recorder = (Hl7.Fhir.Model.ResourceReference)Recorder.DeepCopy();
                if(Symptom != null) dest.Symptom = new List<Hl7.Fhir.Model.AdverseReaction.AdverseReactionSymptomComponent>(Symptom.DeepCopy());
                if(Exposure != null) dest.Exposure = new List<Hl7.Fhir.Model.AdverseReaction.AdverseReactionExposureComponent>(Exposure.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new AdverseReaction());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as AdverseReaction;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(DidNotOccurFlagElement, otherT.DidNotOccurFlagElement)) return false;
            if( !DeepComparable.Matches(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.Matches(Symptom, otherT.Symptom)) return false;
            if( !DeepComparable.Matches(Exposure, otherT.Exposure)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as AdverseReaction;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(DidNotOccurFlagElement, otherT.DidNotOccurFlagElement)) return false;
            if( !DeepComparable.IsExactly(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.IsExactly(Symptom, otherT.Symptom)) return false;
            if( !DeepComparable.IsExactly(Exposure, otherT.Exposure)) return false;
            
            return true;
        }
        
    }
    
}
