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
    /// Drug, food, environmental and others
    /// </summary>
    [FhirType("AllergyIntolerance", IsResource=true)]
    [DataContract]
    public partial class AllergyIntolerance : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The status of the adverse sensitivity
        /// </summary>
        [FhirEnumeration("SensitivityStatus")]
        public enum SensitivityStatus
        {
            [EnumLiteral("suspected")]
            Suspected, // A suspected sensitivity to a substance.
            [EnumLiteral("confirmed")]
            Confirmed, // The sensitivity has been confirmed and is active.
            [EnumLiteral("refuted")]
            Refuted, // The sensitivity has been shown to never have existed.
            [EnumLiteral("resolved")]
            Resolved, // The sensitivity used to exist but no longer does.
        }
        
        /// <summary>
        /// The criticality of an adverse sensitivity
        /// </summary>
        [FhirEnumeration("Criticality")]
        public enum Criticality
        {
            [EnumLiteral("fatal")]
            Fatal, // Likely to result in death if re-exposed.
            [EnumLiteral("high")]
            High, // Likely to result in reactions that will need to be treated if re-exposed.
            [EnumLiteral("medium")]
            Medium, // Likely to result in reactions that will inconvenience the subject.
            [EnumLiteral("low")]
            Low, // Not likely to result in any inconveniences for the subject.
        }
        
        /// <summary>
        /// The type of an adverse sensitivity
        /// </summary>
        [FhirEnumeration("SensitivityType")]
        public enum SensitivityType
        {
            [EnumLiteral("allergy")]
            Allergy, // Allergic Reaction.
            [EnumLiteral("intolerance")]
            Intolerance, // Non-Allergic Reaction.
            [EnumLiteral("unknown")]
            Unknown, // Unknown type.
        }
        
        /// <summary>
        /// External Ids for this item
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// fatal | high | medium | low
        /// </summary>
        [FhirElement("criticality", Order=80)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AllergyIntolerance.Criticality> Criticality_Element { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AllergyIntolerance.Criticality? Criticality_
        {
            get { return Criticality_Element != null ? Criticality_Element.Value : null; }
            set
            {
                if(value == null)
                  Criticality_Element = null; 
                else
                  Criticality_Element = new Code<Hl7.Fhir.Model.AllergyIntolerance.Criticality>(value);
            }
        }
        
        /// <summary>
        /// allergy | intolerance | unknown
        /// </summary>
        [FhirElement("sensitivityType", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AllergyIntolerance.SensitivityType> SensitivityType_Element { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AllergyIntolerance.SensitivityType? SensitivityType_
        {
            get { return SensitivityType_Element != null ? SensitivityType_Element.Value : null; }
            set
            {
                if(value == null)
                  SensitivityType_Element = null; 
                else
                  SensitivityType_Element = new Code<Hl7.Fhir.Model.AllergyIntolerance.SensitivityType>(value);
            }
        }
        
        /// <summary>
        /// When recorded
        /// </summary>
        [FhirElement("recordedDate", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime RecordedDateElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// suspected | confirmed | refuted | resolved
        /// </summary>
        [FhirElement("status", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AllergyIntolerance.SensitivityStatus> StatusElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AllergyIntolerance.SensitivityStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.AllergyIntolerance.SensitivityStatus>(value);
            }
        }
        
        /// <summary>
        /// Who the sensitivity is for
        /// </summary>
        [FhirElement("subject", Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Who recorded the sensitivity
        /// </summary>
        [FhirElement("recorder", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recorder { get; set; }
        
        /// <summary>
        /// The substance that causes the sensitivity
        /// </summary>
        [FhirElement("substance", Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Substance { get; set; }
        
        /// <summary>
        /// Reactions associated with the sensitivity
        /// </summary>
        [FhirElement("reaction", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Reaction { get; set; }
        
        /// <summary>
        /// Observations that confirm or refute
        /// </summary>
        [FhirElement("sensitivityTest", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SensitivityTest { get; set; }
        
    }
    
}
