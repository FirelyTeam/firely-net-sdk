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
// Generated on Mon, Feb 3, 2014 11:56+0100 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Immunization event information
    /// </summary>
    [FhirType("Immunization", IsResource=true)]
    [DataContract]
    public partial class Immunization : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ImmunizationVaccinationProtocolComponent")]
        [DataContract]
        public partial class ImmunizationVaccinationProtocolComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// What dose number within series?
            /// </summary>
            [FhirElement("doseSequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
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
            /// Details of vaccine protocol
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
            /// Name of vaccine series
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
            
            /// <summary>
            /// Recommended number of doses for immunity
            /// </summary>
            [FhirElement("seriesDoses", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Integer SeriesDosesElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SeriesDoses
            {
                get { return SeriesDosesElement != null ? SeriesDosesElement.Value : null; }
                set
                {
                    if(value == null)
                      SeriesDosesElement = null; 
                    else
                      SeriesDosesElement = new Hl7.Fhir.Model.Integer(value);
                }
            }
            
            /// <summary>
            /// Disease immunized against
            /// </summary>
            [FhirElement("doseTarget", Order=90)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DoseTarget { get; set; }
            
            /// <summary>
            /// Does dose count towards immunity?
            /// </summary>
            [FhirElement("doseStatus", Order=100)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DoseStatus { get; set; }
            
            /// <summary>
            /// Why does does count/not count?
            /// </summary>
            [FhirElement("doseStatusReason", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DoseStatusReason { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ImmunizationExplanationComponent")]
        [DataContract]
        public partial class ImmunizationExplanationComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Why immunization occurred
            /// </summary>
            [FhirElement("reason", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Reason { get; set; }
            
            /// <summary>
            /// Why immunization did not occur
            /// </summary>
            [FhirElement("refusalReason", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> RefusalReason { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ImmunizationReactionComponent")]
        [DataContract]
        public partial class ImmunizationReactionComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// When did reaction start?
            /// </summary>
            [FhirElement("date", Order=40)]
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
            /// Additional information on reaction
            /// </summary>
            [FhirElement("detail", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Detail { get; set; }
            
            /// <summary>
            /// Was reaction self-reported?
            /// </summary>
            [FhirElement("reported", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ReportedElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Reported
            {
                get { return ReportedElement != null ? ReportedElement.Value : null; }
                set
                {
                    if(value == null)
                      ReportedElement = null; 
                    else
                      ReportedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                }
            }
            
        }
        
        
        /// <summary>
        /// Business identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// Vaccination administration date
        /// </summary>
        [FhirElement("date", Order=80)]
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
        /// Vaccine product administered
        /// </summary>
        [FhirElement("vaccineType", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept VaccineType { get; set; }
        
        /// <summary>
        /// Who was immunized?
        /// </summary>
        [FhirElement("subject", Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Was immunization refused?
        /// </summary>
        [FhirElement("refusedIndicator", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean RefusedIndicatorElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? RefusedIndicator
        {
            get { return RefusedIndicatorElement != null ? RefusedIndicatorElement.Value : null; }
            set
            {
                if(value == null)
                  RefusedIndicatorElement = null; 
                else
                  RefusedIndicatorElement = new Hl7.Fhir.Model.FhirBoolean(value);
            }
        }
        
        /// <summary>
        /// Is this a self-reported record?
        /// </summary>
        [FhirElement("reported", Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ReportedElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Reported
        {
            get { return ReportedElement != null ? ReportedElement.Value : null; }
            set
            {
                if(value == null)
                  ReportedElement = null; 
                else
                  ReportedElement = new Hl7.Fhir.Model.FhirBoolean(value);
            }
        }
        
        /// <summary>
        /// Who administered vaccine?
        /// </summary>
        [FhirElement("performer", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Performer { get; set; }
        
        /// <summary>
        /// Who ordered vaccination?
        /// </summary>
        [FhirElement("requester", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Requester { get; set; }
        
        /// <summary>
        /// Vaccine manufacturer
        /// </summary>
        [FhirElement("manufacturer", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Manufacturer { get; set; }
        
        /// <summary>
        /// Where did vaccination occur?
        /// </summary>
        [FhirElement("location", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location { get; set; }
        
        /// <summary>
        /// Vaccine lot number
        /// </summary>
        [FhirElement("lotNumber", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString LotNumberElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LotNumber
        {
            get { return LotNumberElement != null ? LotNumberElement.Value : null; }
            set
            {
                if(value == null)
                  LotNumberElement = null; 
                else
                  LotNumberElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Vaccine expiration date
        /// </summary>
        [FhirElement("expirationDate", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Date ExpirationDateElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ExpirationDate
        {
            get { return ExpirationDateElement != null ? ExpirationDateElement.Value : null; }
            set
            {
                if(value == null)
                  ExpirationDateElement = null; 
                else
                  ExpirationDateElement = new Hl7.Fhir.Model.Date(value);
            }
        }
        
        /// <summary>
        /// Body site vaccine  was administered
        /// </summary>
        [FhirElement("site", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Site { get; set; }
        
        /// <summary>
        /// How vaccine entered body
        /// </summary>
        [FhirElement("route", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Route { get; set; }
        
        /// <summary>
        /// Amount of vaccine administered
        /// </summary>
        [FhirElement("doseQuantity", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity DoseQuantity { get; set; }
        
        /// <summary>
        /// Administration / refusal reasons
        /// </summary>
        [FhirElement("explanation", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Immunization.ImmunizationExplanationComponent Explanation { get; set; }
        
        /// <summary>
        /// Details of a reaction that follows immunization
        /// </summary>
        [FhirElement("reaction", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Immunization.ImmunizationReactionComponent> Reaction { get; set; }
        
        /// <summary>
        /// What protocol was followed
        /// </summary>
        [FhirElement("vaccinationProtocol", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Immunization.ImmunizationVaccinationProtocolComponent> VaccinationProtocol { get; set; }
        
    }
    
}
