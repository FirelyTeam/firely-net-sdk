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
// Generated for FHIR v1.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Allergy or Intolerance (generally: Risk Of Adverse reaction to a substance)
    /// </summary>
    [FhirType("AllergyIntolerance", IsResource=true)]
    [DataContract]
    public partial class AllergyIntolerance : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.AllergyIntolerance; } }
        [NotMapped]
        public override string TypeName { get { return "AllergyIntolerance"; } }
        
        /// <summary>
        /// Assertion about certainty associated with a propensity, or potential risk, of a reaction to the identified Substance.
        /// (url: http://hl7.org/fhir/ValueSet/allergy-intolerance-status)
        /// </summary>
        [FhirEnumeration("AllergyIntoleranceStatus")]
        public enum AllergyIntoleranceStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-status)
            /// </summary>
            [EnumLiteral("active"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-status)
            /// </summary>
            [EnumLiteral("active-confirmed"), Description("Active Confirmed")]
            ActiveConfirmed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-status)
            /// </summary>
            [EnumLiteral("inactive"), Description("Inactive")]
            Inactive,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-status)
            /// </summary>
            [EnumLiteral("resolved"), Description("Resolved")]
            Resolved,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-status)
            /// </summary>
            [EnumLiteral("refuted"), Description("Refuted")]
            Refuted,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-status)
            /// </summary>
            [EnumLiteral("entered-in-error"), Description("Entered In Error")]
            EnteredInError,
        }

        /// <summary>
        /// Identification of the underlying physiological mechanism for a Reaction Risk.
        /// (url: http://hl7.org/fhir/ValueSet/allergy-intolerance-type)
        /// </summary>
        [FhirEnumeration("AllergyIntoleranceType")]
        public enum AllergyIntoleranceType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-type)
            /// </summary>
            [EnumLiteral("allergy"), Description("Allergy")]
            Allergy,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-type)
            /// </summary>
            [EnumLiteral("intolerance"), Description("Intolerance")]
            Intolerance,
        }

        /// <summary>
        /// Category of an identified Substance.
        /// (url: http://hl7.org/fhir/ValueSet/allergy-intolerance-category)
        /// </summary>
        [FhirEnumeration("AllergyIntoleranceCategory")]
        public enum AllergyIntoleranceCategory
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-category)
            /// </summary>
            [EnumLiteral("food"), Description("Food")]
            Food,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-category)
            /// </summary>
            [EnumLiteral("medication"), Description("Medication")]
            Medication,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-category)
            /// </summary>
            [EnumLiteral("environment"), Description("Environment")]
            Environment,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-category)
            /// </summary>
            [EnumLiteral("other"), Description("Other")]
            Other,
        }

        /// <summary>
        /// Estimate of the potential clinical harm, or seriousness, of a reaction to an identified Substance.
        /// (url: http://hl7.org/fhir/ValueSet/allergy-intolerance-criticality)
        /// </summary>
        [FhirEnumeration("AllergyIntoleranceCriticality")]
        public enum AllergyIntoleranceCriticality
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-criticality)
            /// </summary>
            [EnumLiteral("low"), Description("Low Risk")]
            Low,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-criticality)
            /// </summary>
            [EnumLiteral("high"), Description("High Risk")]
            High,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-criticality)
            /// </summary>
            [EnumLiteral("unable-to-assess"), Description("Unable to Assess Risk")]
            UnableToAssess,
        }

        /// <summary>
        /// Statement about the degree of clinical certainty that a Specific Substance was the cause of the Manifestation in an reaction event.
        /// (url: http://hl7.org/fhir/ValueSet/reaction-event-certainty)
        /// </summary>
        [FhirEnumeration("AllergyIntoleranceCertainty")]
        public enum AllergyIntoleranceCertainty
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reaction-event-certainty)
            /// </summary>
            [EnumLiteral("unlikely"), Description("Unlikely")]
            Unlikely,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reaction-event-certainty)
            /// </summary>
            [EnumLiteral("likely"), Description("Likely")]
            Likely,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reaction-event-certainty)
            /// </summary>
            [EnumLiteral("confirmed"), Description("Confirmed")]
            Confirmed,
        }

        /// <summary>
        /// Clinical assessment of the severity of a reaction event as a whole, potentially considering multiple different manifestations.
        /// (url: http://hl7.org/fhir/ValueSet/reaction-event-severity)
        /// </summary>
        [FhirEnumeration("AllergyIntoleranceSeverity")]
        public enum AllergyIntoleranceSeverity
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reaction-event-severity)
            /// </summary>
            [EnumLiteral("mild"), Description("Mild")]
            Mild,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reaction-event-severity)
            /// </summary>
            [EnumLiteral("moderate"), Description("Moderate")]
            Moderate,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reaction-event-severity)
            /// </summary>
            [EnumLiteral("severe"), Description("Severe")]
            Severe,
        }

        [FhirType("ReactionComponent")]
        [DataContract]
        public partial class ReactionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ReactionComponent"; } }
            
            /// <summary>
            /// Specific substance or pharmaceutical product considered to be responsible for event
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
            public Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCertainty> CertaintyElement
            {
                get { return _CertaintyElement; }
                set { _CertaintyElement = value; OnPropertyChanged("CertaintyElement"); }
            }
            
            private Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCertainty> _CertaintyElement;
            
            /// <summary>
            /// unlikely | likely | confirmed - clinical certainty about the specific substance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCertainty? Certainty
            {
                get { return CertaintyElement != null ? CertaintyElement.Value : null; }
                set
                {
                    if(value == null)
                      CertaintyElement = null; 
                    else
                      CertaintyElement = new Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCertainty>(value);
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
                get { if(_Manifestation==null) _Manifestation = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Manifestation; }
                set { _Manifestation = value; OnPropertyChanged("Manifestation"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Manifestation;
            
            /// <summary>
            /// Description of the event as a whole
            /// </summary>
            [FhirElement("description", Order=70)]
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
            /// mild | moderate | severe (of event as a whole)
            /// </summary>
            [FhirElement("severity", InSummary=true, Order=90)]
            [DataMember]
            public Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceSeverity> SeverityElement
            {
                get { return _SeverityElement; }
                set { _SeverityElement = value; OnPropertyChanged("SeverityElement"); }
            }
            
            private Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceSeverity> _SeverityElement;
            
            /// <summary>
            /// mild | moderate | severe (of event as a whole)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceSeverity? Severity
            {
                get { return SeverityElement != null ? SeverityElement.Value : null; }
                set
                {
                    if(value == null)
                      SeverityElement = null; 
                    else
                      SeverityElement = new Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceSeverity>(value);
                    OnPropertyChanged("Severity");
                }
            }
            
            /// <summary>
            /// How the subject was exposed to the substance
            /// </summary>
            [FhirElement("exposureRoute", InSummary=true, Order=100)]
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
            [FhirElement("note", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Annotation> Note
            {
                get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
                set { _Note = value; OnPropertyChanged("Note"); }
            }
            
            private List<Hl7.Fhir.Model.Annotation> _Note;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ReactionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Substance != null) dest.Substance = (Hl7.Fhir.Model.CodeableConcept)Substance.DeepCopy();
                    if(CertaintyElement != null) dest.CertaintyElement = (Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCertainty>)CertaintyElement.DeepCopy();
                    if(Manifestation != null) dest.Manifestation = new List<Hl7.Fhir.Model.CodeableConcept>(Manifestation.DeepCopy());
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(OnsetElement != null) dest.OnsetElement = (Hl7.Fhir.Model.FhirDateTime)OnsetElement.DeepCopy();
                    if(SeverityElement != null) dest.SeverityElement = (Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceSeverity>)SeverityElement.DeepCopy();
                    if(ExposureRoute != null) dest.ExposureRoute = (Hl7.Fhir.Model.CodeableConcept)ExposureRoute.DeepCopy();
                    if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ReactionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ReactionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Substance, otherT.Substance)) return false;
                if( !DeepComparable.Matches(CertaintyElement, otherT.CertaintyElement)) return false;
                if( !DeepComparable.Matches(Manifestation, otherT.Manifestation)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(OnsetElement, otherT.OnsetElement)) return false;
                if( !DeepComparable.Matches(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.Matches(ExposureRoute, otherT.ExposureRoute)) return false;
                if( !DeepComparable.Matches(Note, otherT.Note)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ReactionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Substance, otherT.Substance)) return false;
                if( !DeepComparable.IsExactly(CertaintyElement, otherT.CertaintyElement)) return false;
                if( !DeepComparable.IsExactly(Manifestation, otherT.Manifestation)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(OnsetElement, otherT.OnsetElement)) return false;
                if( !DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.IsExactly(ExposureRoute, otherT.ExposureRoute)) return false;
                if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External ids for this item
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
        /// active | active-confirmed | inactive | resolved | refuted | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceStatus> _StatusElement;
        
        /// <summary>
        /// active | active-confirmed | inactive | resolved | refuted | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// allergy | intolerance - Underlying mechanism (if known)
        /// </summary>
        [FhirElement("type", InSummary=true, Order=110)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceType> _TypeElement;
        
        /// <summary>
        /// allergy | intolerance - Underlying mechanism (if known)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if(value == null)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// food | medication | environment | other - Category of Substance
        /// </summary>
        [FhirElement("category", InSummary=true, Order=120)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory> CategoryElement
        {
            get { return _CategoryElement; }
            set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
        }
        
        private Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory> _CategoryElement;
        
        /// <summary>
        /// food | medication | environment | other - Category of Substance
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory? Category
        {
            get { return CategoryElement != null ? CategoryElement.Value : null; }
            set
            {
                if(value == null)
                  CategoryElement = null; 
                else
                  CategoryElement = new Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory>(value);
                OnPropertyChanged("Category");
            }
        }
        
        /// <summary>
        /// low | high | unable-to-assess
        /// </summary>
        [FhirElement("criticality", InSummary=true, Order=130)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCriticality> CriticalityElement
        {
            get { return _CriticalityElement; }
            set { _CriticalityElement = value; OnPropertyChanged("CriticalityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCriticality> _CriticalityElement;
        
        /// <summary>
        /// low | high | unable-to-assess
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCriticality? Criticality
        {
            get { return CriticalityElement != null ? CriticalityElement.Value : null; }
            set
            {
                if(value == null)
                  CriticalityElement = null; 
                else
                  CriticalityElement = new Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCriticality>(value);
                OnPropertyChanged("Criticality");
            }
        }
        
        /// <summary>
        /// Allergy or intolerance code
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
        /// Who the sensitivity is for
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=150)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Date record was believed accurate
        /// </summary>
        [FhirElement("attestedDate", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AttestedDateElement
        {
            get { return _AttestedDateElement; }
            set { _AttestedDateElement = value; OnPropertyChanged("AttestedDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _AttestedDateElement;
        
        /// <summary>
        /// Date record was believed accurate
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AttestedDate
        {
            get { return AttestedDateElement != null ? AttestedDateElement.Value : null; }
            set
            {
                if(value == null)
                  AttestedDateElement = null; 
                else
                  AttestedDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("AttestedDate");
            }
        }
        
        /// <summary>
        /// Who recorded the sensitivity
        /// </summary>
        [FhirElement("recorder", InSummary=true, Order=170)]
        [References("Practitioner","Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recorder
        {
            get { return _Recorder; }
            set { _Recorder = value; OnPropertyChanged("Recorder"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Recorder;
        
        /// <summary>
        /// Source of the information about the allergy
        /// </summary>
        [FhirElement("reporter", InSummary=true, Order=180)]
        [References("Patient","RelatedPerson","Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Reporter
        {
            get { return _Reporter; }
            set { _Reporter = value; OnPropertyChanged("Reporter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Reporter;
        
        /// <summary>
        /// Date(/time) when manifestations showed
        /// </summary>
        [FhirElement("onset", InSummary=true, Order=190)]
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
        /// Date(/time) of last known occurrence of a reaction
        /// </summary>
        [FhirElement("lastOccurrence", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime LastOccurrenceElement
        {
            get { return _LastOccurrenceElement; }
            set { _LastOccurrenceElement = value; OnPropertyChanged("LastOccurrenceElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _LastOccurrenceElement;
        
        /// <summary>
        /// Date(/time) of last known occurrence of a reaction
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LastOccurrence
        {
            get { return LastOccurrenceElement != null ? LastOccurrenceElement.Value : null; }
            set
            {
                if(value == null)
                  LastOccurrenceElement = null; 
                else
                  LastOccurrenceElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("LastOccurrence");
            }
        }
        
        /// <summary>
        /// Additional text not captured in other fields
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
        /// Adverse Reaction Events linked to exposure to substance
        /// </summary>
        [FhirElement("reaction", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.AllergyIntolerance.ReactionComponent> Reaction
        {
            get { if(_Reaction==null) _Reaction = new List<Hl7.Fhir.Model.AllergyIntolerance.ReactionComponent>(); return _Reaction; }
            set { _Reaction = value; OnPropertyChanged("Reaction"); }
        }
        
        private List<Hl7.Fhir.Model.AllergyIntolerance.ReactionComponent> _Reaction;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as AllergyIntolerance;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceStatus>)StatusElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceType>)TypeElement.DeepCopy();
                if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory>)CategoryElement.DeepCopy();
                if(CriticalityElement != null) dest.CriticalityElement = (Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCriticality>)CriticalityElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(AttestedDateElement != null) dest.AttestedDateElement = (Hl7.Fhir.Model.FhirDateTime)AttestedDateElement.DeepCopy();
                if(Recorder != null) dest.Recorder = (Hl7.Fhir.Model.ResourceReference)Recorder.DeepCopy();
                if(Reporter != null) dest.Reporter = (Hl7.Fhir.Model.ResourceReference)Reporter.DeepCopy();
                if(OnsetElement != null) dest.OnsetElement = (Hl7.Fhir.Model.FhirDateTime)OnsetElement.DeepCopy();
                if(LastOccurrenceElement != null) dest.LastOccurrenceElement = (Hl7.Fhir.Model.FhirDateTime)LastOccurrenceElement.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Reaction != null) dest.Reaction = new List<Hl7.Fhir.Model.AllergyIntolerance.ReactionComponent>(Reaction.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new AllergyIntolerance());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as AllergyIntolerance;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.Matches(CriticalityElement, otherT.CriticalityElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(AttestedDateElement, otherT.AttestedDateElement)) return false;
            if( !DeepComparable.Matches(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.Matches(Reporter, otherT.Reporter)) return false;
            if( !DeepComparable.Matches(OnsetElement, otherT.OnsetElement)) return false;
            if( !DeepComparable.Matches(LastOccurrenceElement, otherT.LastOccurrenceElement)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Reaction, otherT.Reaction)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as AllergyIntolerance;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.IsExactly(CriticalityElement, otherT.CriticalityElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(AttestedDateElement, otherT.AttestedDateElement)) return false;
            if( !DeepComparable.IsExactly(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.IsExactly(Reporter, otherT.Reporter)) return false;
            if( !DeepComparable.IsExactly(OnsetElement, otherT.OnsetElement)) return false;
            if( !DeepComparable.IsExactly(LastOccurrenceElement, otherT.LastOccurrenceElement)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Reaction, otherT.Reaction)) return false;
            
            return true;
        }
        
    }
    
}
