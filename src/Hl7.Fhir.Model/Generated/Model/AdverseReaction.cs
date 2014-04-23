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
// Generated on Thu, Apr 17, 2014 11:39+0200 for FHIR v0.80
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
            [EnumLiteral("severe")]
            Severe, // Severe complications arose due to the reaction.
            [EnumLiteral("serious")]
            Serious, // Serious inconvenience to the subject.
            [EnumLiteral("moderate")]
            Moderate, // Moderate inconvenience to the subject.
            [EnumLiteral("minor")]
            Minor, // Minor inconvenience to the subject.
        }
        
        /// <summary>
        /// The type of exposure that resulted in an adverse reaction
        /// </summary>
        [FhirEnumeration("ExposureType")]
        public enum ExposureType
        {
            [EnumLiteral("drugadmin")]
            Drugadmin, // Drug Administration.
            [EnumLiteral("immuniz")]
            Immuniz, // Immunization.
            [EnumLiteral("coincidental")]
            Coincidental, // In the same area as the substance.
        }
        
        /// <summary>
        /// How likely is it that the given exposure caused a reaction
        /// </summary>
        [FhirEnumeration("CausalityExpectation")]
        public enum CausalityExpectation
        {
            [EnumLiteral("likely")]
            Likely, // Likely that this specific exposure caused the reaction.
            [EnumLiteral("unlikely")]
            Unlikely, // Unlikely that this specific exposure caused the reaction - the exposure is being linked to for information purposes.
            [EnumLiteral("confirmed")]
            Confirmed, // It has been confirmed that this exposure was one of the causes of the reaction.
            [EnumLiteral("unknown")]
            Unknown, // It is unknown whether this exposure had anything to do with the reaction.
        }
        
        /// <summary>
        /// null
        /// </summary>
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
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
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
        
    }
    
}
