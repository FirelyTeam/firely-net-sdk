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
    /// Immunization profile
    /// </summary>
    [FhirType("ImmunizationRecommendation", IsResource=true)]
    [DataContract]
    public partial class ImmunizationRecommendation : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        [FhirType("ImmunizationRecommendationRecommendationDateCriterionComponent")]
        [DataContract]
        public partial class ImmunizationRecommendationRecommendationDateCriterionComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Type of date
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
            /// Recommended date
            /// </summary>
            [FhirElement("value", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            private Hl7.Fhir.Model.FhirDateTime _ValueElement;
            
            /// <summary>
            /// Recommended date
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if(value == null)
                      ValueElement = null; 
                    else
                      ValueElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Value");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ImmunizationRecommendationRecommendationDateCriterionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDateTime)ValueElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ImmunizationRecommendationRecommendationDateCriterionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ImmunizationRecommendationRecommendationDateCriterionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ImmunizationRecommendationRecommendationDateCriterionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ImmunizationRecommendationRecommendationProtocolComponent")]
        [DataContract]
        public partial class ImmunizationRecommendationRecommendationProtocolComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Number of dose within sequence
            /// </summary>
            [FhirElement("doseSequence", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DoseSequenceElement
            {
                get { return _DoseSequenceElement; }
                set { _DoseSequenceElement = value; OnPropertyChanged("DoseSequenceElement"); }
            }
            private Hl7.Fhir.Model.Integer _DoseSequenceElement;
            
            /// <summary>
            /// Number of dose within sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? DoseSequence
            {
                get { return DoseSequenceElement != null ? DoseSequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      DoseSequenceElement = null; 
                    else
                      DoseSequenceElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("DoseSequence");
                }
            }
            
            /// <summary>
            /// Protocol details
            /// </summary>
            [FhirElement("description", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Protocol details
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
            /// Who is responsible for protocol
            /// </summary>
            [FhirElement("authority", InSummary=true, Order=60)]
            [References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Authority
            {
                get { return _Authority; }
                set { _Authority = value; OnPropertyChanged("Authority"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Authority;
            
            /// <summary>
            /// Name of vaccination series
            /// </summary>
            [FhirElement("series", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SeriesElement
            {
                get { return _SeriesElement; }
                set { _SeriesElement = value; OnPropertyChanged("SeriesElement"); }
            }
            private Hl7.Fhir.Model.FhirString _SeriesElement;
            
            /// <summary>
            /// Name of vaccination series
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Series
            {
                get { return SeriesElement != null ? SeriesElement.Value : null; }
                set
                {
                    if(value == null)
                      SeriesElement = null; 
                    else
                      SeriesElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Series");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ImmunizationRecommendationRecommendationProtocolComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DoseSequenceElement != null) dest.DoseSequenceElement = (Hl7.Fhir.Model.Integer)DoseSequenceElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Authority != null) dest.Authority = (Hl7.Fhir.Model.ResourceReference)Authority.DeepCopy();
                    if(SeriesElement != null) dest.SeriesElement = (Hl7.Fhir.Model.FhirString)SeriesElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ImmunizationRecommendationRecommendationProtocolComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ImmunizationRecommendationRecommendationProtocolComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DoseSequenceElement, otherT.DoseSequenceElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Authority, otherT.Authority)) return false;
                if( !DeepComparable.Matches(SeriesElement, otherT.SeriesElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ImmunizationRecommendationRecommendationProtocolComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DoseSequenceElement, otherT.DoseSequenceElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Authority, otherT.Authority)) return false;
                if( !DeepComparable.IsExactly(SeriesElement, otherT.SeriesElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ImmunizationRecommendationRecommendationComponent")]
        [DataContract]
        public partial class ImmunizationRecommendationRecommendationComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Date recommendation created
            /// </summary>
            [FhirElement("date", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            private Hl7.Fhir.Model.FhirDateTime _DateElement;
            
            /// <summary>
            /// Date recommendation created
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
            /// Vaccine recommendation applies to
            /// </summary>
            [FhirElement("vaccineType", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept VaccineType
            {
                get { return _VaccineType; }
                set { _VaccineType = value; OnPropertyChanged("VaccineType"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _VaccineType;
            
            /// <summary>
            /// Recommended dose number
            /// </summary>
            [FhirElement("doseNumber", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DoseNumberElement
            {
                get { return _DoseNumberElement; }
                set { _DoseNumberElement = value; OnPropertyChanged("DoseNumberElement"); }
            }
            private Hl7.Fhir.Model.Integer _DoseNumberElement;
            
            /// <summary>
            /// Recommended dose number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? DoseNumber
            {
                get { return DoseNumberElement != null ? DoseNumberElement.Value : null; }
                set
                {
                    if(value == null)
                      DoseNumberElement = null; 
                    else
                      DoseNumberElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("DoseNumber");
                }
            }
            
            /// <summary>
            /// Vaccine administration status
            /// </summary>
            [FhirElement("forecastStatus", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ForecastStatus
            {
                get { return _ForecastStatus; }
                set { _ForecastStatus = value; OnPropertyChanged("ForecastStatus"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _ForecastStatus;
            
            /// <summary>
            /// Dates governing proposed immunization
            /// </summary>
            [FhirElement("dateCriterion", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationDateCriterionComponent> DateCriterion
            {
                get { return _DateCriterion; }
                set { _DateCriterion = value; OnPropertyChanged("DateCriterion"); }
            }
            private List<Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationDateCriterionComponent> _DateCriterion;
            
            /// <summary>
            /// Protocol used by recommendation
            /// </summary>
            [FhirElement("protocol", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationProtocolComponent Protocol
            {
                get { return _Protocol; }
                set { _Protocol = value; OnPropertyChanged("Protocol"); }
            }
            private Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationProtocolComponent _Protocol;
            
            /// <summary>
            /// Past immunizations supporting recommendation
            /// </summary>
            [FhirElement("supportingImmunization", InSummary=true, Order=100)]
            [References("Immunization")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> SupportingImmunization
            {
                get { return _SupportingImmunization; }
                set { _SupportingImmunization = value; OnPropertyChanged("SupportingImmunization"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _SupportingImmunization;
            
            /// <summary>
            /// Patient observations supporting recommendation
            /// </summary>
            [FhirElement("supportingPatientInformation", InSummary=true, Order=110)]
            [References("Observation","AdverseReaction","AllergyIntolerance")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> SupportingPatientInformation
            {
                get { return _SupportingPatientInformation; }
                set { _SupportingPatientInformation = value; OnPropertyChanged("SupportingPatientInformation"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _SupportingPatientInformation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ImmunizationRecommendationRecommendationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                    if(VaccineType != null) dest.VaccineType = (Hl7.Fhir.Model.CodeableConcept)VaccineType.DeepCopy();
                    if(DoseNumberElement != null) dest.DoseNumberElement = (Hl7.Fhir.Model.Integer)DoseNumberElement.DeepCopy();
                    if(ForecastStatus != null) dest.ForecastStatus = (Hl7.Fhir.Model.CodeableConcept)ForecastStatus.DeepCopy();
                    if(DateCriterion != null) dest.DateCriterion = new List<Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationDateCriterionComponent>(DateCriterion.DeepCopy());
                    if(Protocol != null) dest.Protocol = (Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationProtocolComponent)Protocol.DeepCopy();
                    if(SupportingImmunization != null) dest.SupportingImmunization = new List<Hl7.Fhir.Model.ResourceReference>(SupportingImmunization.DeepCopy());
                    if(SupportingPatientInformation != null) dest.SupportingPatientInformation = new List<Hl7.Fhir.Model.ResourceReference>(SupportingPatientInformation.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ImmunizationRecommendationRecommendationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ImmunizationRecommendationRecommendationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(VaccineType, otherT.VaccineType)) return false;
                if( !DeepComparable.Matches(DoseNumberElement, otherT.DoseNumberElement)) return false;
                if( !DeepComparable.Matches(ForecastStatus, otherT.ForecastStatus)) return false;
                if( !DeepComparable.Matches(DateCriterion, otherT.DateCriterion)) return false;
                if( !DeepComparable.Matches(Protocol, otherT.Protocol)) return false;
                if( !DeepComparable.Matches(SupportingImmunization, otherT.SupportingImmunization)) return false;
                if( !DeepComparable.Matches(SupportingPatientInformation, otherT.SupportingPatientInformation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ImmunizationRecommendationRecommendationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(VaccineType, otherT.VaccineType)) return false;
                if( !DeepComparable.IsExactly(DoseNumberElement, otherT.DoseNumberElement)) return false;
                if( !DeepComparable.IsExactly(ForecastStatus, otherT.ForecastStatus)) return false;
                if( !DeepComparable.IsExactly(DateCriterion, otherT.DateCriterion)) return false;
                if( !DeepComparable.IsExactly(Protocol, otherT.Protocol)) return false;
                if( !DeepComparable.IsExactly(SupportingImmunization, otherT.SupportingImmunization)) return false;
                if( !DeepComparable.IsExactly(SupportingPatientInformation, otherT.SupportingPatientInformation)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Business identifier
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
        /// Who this profile is for
        /// </summary>
        [FhirElement("subject", Order=80)]
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
        /// Vaccine administration recommendations
        /// </summary>
        [FhirElement("recommendation", Order=90)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationComponent> Recommendation
        {
            get { return _Recommendation; }
            set { _Recommendation = value; OnPropertyChanged("Recommendation"); }
        }
        private List<Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationComponent> _Recommendation;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ImmunizationRecommendation;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Recommendation != null) dest.Recommendation = new List<Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationComponent>(Recommendation.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ImmunizationRecommendation());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ImmunizationRecommendation;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Recommendation, otherT.Recommendation)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ImmunizationRecommendation;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Recommendation, otherT.Recommendation)) return false;
            
            return true;
        }
        
    }
    
}
