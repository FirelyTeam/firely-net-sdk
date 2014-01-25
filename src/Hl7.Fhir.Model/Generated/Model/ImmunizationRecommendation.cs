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
// Generated on Fri, Jan 24, 2014 09:44-0600 for FHIR v0.12
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Immunization profile
    /// </summary>
    [FhirType("ImmunizationRecommendation", IsResource=true)]
    [DataContract]
    public partial class ImmunizationRecommendation : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ImmunizationRecommendationRecommendationDateCriterionComponent")]
        [DataContract]
        public partial class ImmunizationRecommendationRecommendationDateCriterionComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Type of date
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code { get; set; }
            
            /// <summary>
            /// Recommended date
            /// </summary>
            [FhirElement("value", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ValueElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ImmunizationRecommendationRecommendationProtocolComponent")]
        [DataContract]
        public partial class ImmunizationRecommendationRecommendationProtocolComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Number of dose within sequence
            /// </summary>
            [FhirElement("doseSequence", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DoseSequenceElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Protocol details
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Who is responsible for protocol
            /// </summary>
            [FhirElement("authority", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Authority { get; set; }
            
            /// <summary>
            /// Name of vaccination series
            /// </summary>
            [FhirElement("series", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SeriesElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ImmunizationRecommendationRecommendationComponent")]
        [DataContract]
        public partial class ImmunizationRecommendationRecommendationComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Date recommendation created
            /// </summary>
            [FhirElement("date", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Vaccine recommendation applies to
            /// </summary>
            [FhirElement("vaccineType", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept VaccineType { get; set; }
            
            /// <summary>
            /// Recommended dose number
            /// </summary>
            [FhirElement("doseNumber", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DoseNumberElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Vaccine administration status
            /// </summary>
            [FhirElement("forecastStatus", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ForecastStatus { get; set; }
            
            /// <summary>
            /// Dates governing proposed immunization
            /// </summary>
            [FhirElement("dateCriterion", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationDateCriterionComponent> DateCriterion { get; set; }
            
            /// <summary>
            /// Protocol used by recommendation
            /// </summary>
            [FhirElement("protocol", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationProtocolComponent Protocol { get; set; }
            
            /// <summary>
            /// Past immunizations supporting recommendation
            /// </summary>
            [FhirElement("supportingImmunization", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> SupportingImmunization { get; set; }
            
            /// <summary>
            /// Patient observations supporting recommendation
            /// </summary>
            [FhirElement("supportingPatientInformation", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> SupportingPatientInformation { get; set; }
            
        }
        
        
        /// <summary>
        /// Business identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// Who this profile is for
        /// </summary>
        [FhirElement("subject", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Vaccine administration recommendations
        /// </summary>
        [FhirElement("recommendation", Order=90)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ImmunizationRecommendation.ImmunizationRecommendationRecommendationComponent> Recommendation { get; set; }
        
    }
    
}
