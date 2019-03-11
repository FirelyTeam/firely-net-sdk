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
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Allergy or Intolerance (generally: Risk of adverse reaction to a substance)
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
            [EnumLiteral("allergy", "http://hl7.org/fhir/allergy-intolerance-type"), Description("Allergy")]
            Allergy,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-type)
            /// </summary>
            [EnumLiteral("intolerance", "http://hl7.org/fhir/allergy-intolerance-type"), Description("Intolerance")]
            Intolerance,
        }

        /// <summary>
        /// Category of an identified substance associated with allergies or intolerances.
        /// (url: http://hl7.org/fhir/ValueSet/allergy-intolerance-category)
        /// </summary>
        [FhirEnumeration("AllergyIntoleranceCategory")]
        public enum AllergyIntoleranceCategory
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-category)
            /// </summary>
            [EnumLiteral("food", "http://hl7.org/fhir/allergy-intolerance-category"), Description("Food")]
            Food,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-category)
            /// </summary>
            [EnumLiteral("medication", "http://hl7.org/fhir/allergy-intolerance-category"), Description("Medication")]
            Medication,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-category)
            /// </summary>
            [EnumLiteral("environment", "http://hl7.org/fhir/allergy-intolerance-category"), Description("Environment")]
            Environment,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-category)
            /// </summary>
            [EnumLiteral("biologic", "http://hl7.org/fhir/allergy-intolerance-category"), Description("Biologic")]
            Biologic,
        }

        /// <summary>
        /// Estimate of the potential clinical harm, or seriousness, of a reaction to an identified substance.
        /// (url: http://hl7.org/fhir/ValueSet/allergy-intolerance-criticality)
        /// </summary>
        [FhirEnumeration("AllergyIntoleranceCriticality")]
        public enum AllergyIntoleranceCriticality
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-criticality)
            /// </summary>
            [EnumLiteral("low", "http://hl7.org/fhir/allergy-intolerance-criticality"), Description("Low Risk")]
            Low,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-criticality)
            /// </summary>
            [EnumLiteral("high", "http://hl7.org/fhir/allergy-intolerance-criticality"), Description("High Risk")]
            High,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/allergy-intolerance-criticality)
            /// </summary>
            [EnumLiteral("unable-to-assess", "http://hl7.org/fhir/allergy-intolerance-criticality"), Description("Unable to Assess Risk")]
            UnableToAssess,
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
            [EnumLiteral("mild", "http://hl7.org/fhir/reaction-event-severity"), Description("Mild")]
            Mild,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reaction-event-severity)
            /// </summary>
            [EnumLiteral("moderate", "http://hl7.org/fhir/reaction-event-severity"), Description("Moderate")]
            Moderate,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/reaction-event-severity)
            /// </summary>
            [EnumLiteral("severe", "http://hl7.org/fhir/reaction-event-severity"), Description("Severe")]
            Severe,
        }

        [FhirType("ReactionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ReactionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ReactionComponent"; } }
            
            /// <summary>
            /// Specific substance or pharmaceutical product considered to be responsible for event
            /// </summary>
            [FhirElement("substance", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Substance
            {
                get { return _Substance; }
                set { _Substance = value; OnPropertyChanged("Substance"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Substance;
            
            /// <summary>
            /// Clinical symptoms/signs associated with the Event
            /// </summary>
            [FhirElement("manifestation", Order=50)]
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
            [FhirElement("description", Order=60)]
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
                    if (value == null)
                        DescriptionElement = null; 
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Date(/time) when manifestations showed
            /// </summary>
            [FhirElement("onset", Order=70)]
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
                    if (value == null)
                        OnsetElement = null; 
                    else
                        OnsetElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Onset");
                }
            }
            
            /// <summary>
            /// mild | moderate | severe (of event as a whole)
            /// </summary>
            [FhirElement("severity", Order=80)]
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
                    if (!value.HasValue)
                        SeverityElement = null; 
                    else
                        SeverityElement = new Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceSeverity>(value);
                    OnPropertyChanged("Severity");
                }
            }
            
            /// <summary>
            /// How the subject was exposed to the substance
            /// </summary>
            [FhirElement("exposureRoute", Order=90)]
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
            [FhirElement("note", Order=100)]
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
                if( !DeepComparable.IsExactly(Manifestation, otherT.Manifestation)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(OnsetElement, otherT.OnsetElement)) return false;
                if( !DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.IsExactly(ExposureRoute, otherT.ExposureRoute)) return false;
                if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Substance != null) yield return Substance;
                    foreach (var elem in Manifestation) { if (elem != null) yield return elem; }
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (OnsetElement != null) yield return OnsetElement;
                    if (SeverityElement != null) yield return SeverityElement;
                    if (ExposureRoute != null) yield return ExposureRoute;
                    foreach (var elem in Note) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Substance != null) yield return new ElementValue("substance", Substance);
                    foreach (var elem in Manifestation) { if (elem != null) yield return new ElementValue("manifestation", elem); }
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (OnsetElement != null) yield return new ElementValue("onset", OnsetElement);
                    if (SeverityElement != null) yield return new ElementValue("severity", SeverityElement);
                    if (ExposureRoute != null) yield return new ElementValue("exposureRoute", ExposureRoute);
                    foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                }
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
        /// active | inactive | resolved
        /// </summary>
        [FhirElement("clinicalStatus", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ClinicalStatus
        {
            get { return _ClinicalStatus; }
            set { _ClinicalStatus = value; OnPropertyChanged("ClinicalStatus"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ClinicalStatus;
        
        /// <summary>
        /// unconfirmed | confirmed | refuted | entered-in-error
        /// </summary>
        [FhirElement("verificationStatus", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept VerificationStatus
        {
            get { return _VerificationStatus; }
            set { _VerificationStatus = value; OnPropertyChanged("VerificationStatus"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _VerificationStatus;
        
        /// <summary>
        /// allergy | intolerance - Underlying mechanism (if known)
        /// </summary>
        [FhirElement("type", InSummary=true, Order=120)]
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
                if (!value.HasValue)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// food | medication | environment | biologic
        /// </summary>
        [FhirElement("category", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory>> CategoryElement
        {
            get { if(_CategoryElement==null) _CategoryElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory>>(); return _CategoryElement; }
            set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory>> _CategoryElement;
        
        /// <summary>
        /// food | medication | environment | biologic
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory?> Category
        {
            get { return CategoryElement != null ? CategoryElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  CategoryElement = null; 
                else
                  CategoryElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory>(elem)));
                OnPropertyChanged("Category");
            }
        }
        
        /// <summary>
        /// low | high | unable-to-assess
        /// </summary>
        [FhirElement("criticality", InSummary=true, Order=140)]
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
                if (!value.HasValue)
                  CriticalityElement = null; 
                else
                  CriticalityElement = new Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCriticality>(value);
                OnPropertyChanged("Criticality");
            }
        }
        
        /// <summary>
        /// Code that identifies the allergy or intolerance
        /// </summary>
        [FhirElement("code", InSummary=true, Order=150)]
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
        [FhirElement("patient", InSummary=true, Order=160)]
        [CLSCompliant(false)]
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
        /// Encounter when the allergy or intolerance was asserted
        /// </summary>
        [FhirElement("encounter", Order=170)]
        [CLSCompliant(false)]
		[References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// When allergy or intolerance was identified
        /// </summary>
        [FhirElement("onset", Order=180, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Age),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Onset
        {
            get { return _Onset; }
            set { _Onset = value; OnPropertyChanged("Onset"); }
        }
        
        private Hl7.Fhir.Model.Element _Onset;
        
        /// <summary>
        /// Date first version of the resource instance was recorded
        /// </summary>
        [FhirElement("recordedDate", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime RecordedDateElement
        {
            get { return _RecordedDateElement; }
            set { _RecordedDateElement = value; OnPropertyChanged("RecordedDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _RecordedDateElement;
        
        /// <summary>
        /// Date first version of the resource instance was recorded
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RecordedDate
        {
            get { return RecordedDateElement != null ? RecordedDateElement.Value : null; }
            set
            {
                if (value == null)
                  RecordedDateElement = null; 
                else
                  RecordedDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("RecordedDate");
            }
        }
        
        /// <summary>
        /// Who recorded the sensitivity
        /// </summary>
        [FhirElement("recorder", Order=200)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole","Patient","RelatedPerson")]
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
        [FhirElement("asserter", InSummary=true, Order=210)]
        [CLSCompliant(false)]
		[References("Patient","RelatedPerson","Practitioner","PractitionerRole")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Asserter
        {
            get { return _Asserter; }
            set { _Asserter = value; OnPropertyChanged("Asserter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Asserter;
        
        /// <summary>
        /// Date(/time) of last known occurrence of a reaction
        /// </summary>
        [FhirElement("lastOccurrence", Order=220)]
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
                if (value == null)
                  LastOccurrenceElement = null; 
                else
                  LastOccurrenceElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("LastOccurrence");
            }
        }
        
        /// <summary>
        /// Additional text not captured in other fields
        /// </summary>
        [FhirElement("note", Order=230)]
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
        [FhirElement("reaction", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.AllergyIntolerance.ReactionComponent> Reaction
        {
            get { if(_Reaction==null) _Reaction = new List<Hl7.Fhir.Model.AllergyIntolerance.ReactionComponent>(); return _Reaction; }
            set { _Reaction = value; OnPropertyChanged("Reaction"); }
        }
        
        private List<Hl7.Fhir.Model.AllergyIntolerance.ReactionComponent> _Reaction;
        

        public static ElementDefinition.ConstraintComponent AllergyIntolerance_AIT_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "verificationStatus='entered-in-error' or clinicalStatus.exists()",
            Key = "ait-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "AllergyIntolerance.clinicalStatus SHALL be present if verificationStatus is not entered-in-error.",
            Xpath = "f:verificationStatus/@value='entered-in-error' or exists(f:clinicalStatus)"
        };

        public static ElementDefinition.ConstraintComponent AllergyIntolerance_AIT_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "verificationStatus!='entered-in-error' or clinicalStatus.empty()",
            Key = "ait-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "AllergyIntolerance.clinicalStatus SHALL NOT be present if verification Status is entered-in-error",
            Xpath = "f:verificationStatus/@value!='entered-in-error' or not(exists(f:clinicalStatus))"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(AllergyIntolerance_AIT_1);
            InvariantConstraints.Add(AllergyIntolerance_AIT_2);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as AllergyIntolerance;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(ClinicalStatus != null) dest.ClinicalStatus = (Hl7.Fhir.Model.CodeableConcept)ClinicalStatus.DeepCopy();
                if(VerificationStatus != null) dest.VerificationStatus = (Hl7.Fhir.Model.CodeableConcept)VerificationStatus.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceType>)TypeElement.DeepCopy();
                if(CategoryElement != null) dest.CategoryElement = new List<Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory>>(CategoryElement.DeepCopy());
                if(CriticalityElement != null) dest.CriticalityElement = (Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCriticality>)CriticalityElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Onset != null) dest.Onset = (Hl7.Fhir.Model.Element)Onset.DeepCopy();
                if(RecordedDateElement != null) dest.RecordedDateElement = (Hl7.Fhir.Model.FhirDateTime)RecordedDateElement.DeepCopy();
                if(Recorder != null) dest.Recorder = (Hl7.Fhir.Model.ResourceReference)Recorder.DeepCopy();
                if(Asserter != null) dest.Asserter = (Hl7.Fhir.Model.ResourceReference)Asserter.DeepCopy();
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
            if( !DeepComparable.Matches(ClinicalStatus, otherT.ClinicalStatus)) return false;
            if( !DeepComparable.Matches(VerificationStatus, otherT.VerificationStatus)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.Matches(CriticalityElement, otherT.CriticalityElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Onset, otherT.Onset)) return false;
            if( !DeepComparable.Matches(RecordedDateElement, otherT.RecordedDateElement)) return false;
            if( !DeepComparable.Matches(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.Matches(Asserter, otherT.Asserter)) return false;
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
            if( !DeepComparable.IsExactly(ClinicalStatus, otherT.ClinicalStatus)) return false;
            if( !DeepComparable.IsExactly(VerificationStatus, otherT.VerificationStatus)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.IsExactly(CriticalityElement, otherT.CriticalityElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Onset, otherT.Onset)) return false;
            if( !DeepComparable.IsExactly(RecordedDateElement, otherT.RecordedDateElement)) return false;
            if( !DeepComparable.IsExactly(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.IsExactly(Asserter, otherT.Asserter)) return false;
            if( !DeepComparable.IsExactly(LastOccurrenceElement, otherT.LastOccurrenceElement)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Reaction, otherT.Reaction)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (ClinicalStatus != null) yield return ClinicalStatus;
				if (VerificationStatus != null) yield return VerificationStatus;
				if (TypeElement != null) yield return TypeElement;
				foreach (var elem in CategoryElement) { if (elem != null) yield return elem; }
				if (CriticalityElement != null) yield return CriticalityElement;
				if (Code != null) yield return Code;
				if (Patient != null) yield return Patient;
				if (Encounter != null) yield return Encounter;
				if (Onset != null) yield return Onset;
				if (RecordedDateElement != null) yield return RecordedDateElement;
				if (Recorder != null) yield return Recorder;
				if (Asserter != null) yield return Asserter;
				if (LastOccurrenceElement != null) yield return LastOccurrenceElement;
				foreach (var elem in Note) { if (elem != null) yield return elem; }
				foreach (var elem in Reaction) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (ClinicalStatus != null) yield return new ElementValue("clinicalStatus", ClinicalStatus);
                if (VerificationStatus != null) yield return new ElementValue("verificationStatus", VerificationStatus);
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                foreach (var elem in CategoryElement) { if (elem != null) yield return new ElementValue("category", elem); }
                if (CriticalityElement != null) yield return new ElementValue("criticality", CriticalityElement);
                if (Code != null) yield return new ElementValue("code", Code);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (Onset != null) yield return new ElementValue("onset", Onset);
                if (RecordedDateElement != null) yield return new ElementValue("recordedDate", RecordedDateElement);
                if (Recorder != null) yield return new ElementValue("recorder", Recorder);
                if (Asserter != null) yield return new ElementValue("asserter", Asserter);
                if (LastOccurrenceElement != null) yield return new ElementValue("lastOccurrence", LastOccurrenceElement);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in Reaction) { if (elem != null) yield return new ElementValue("reaction", elem); }
            }
        }

    }
    
}
