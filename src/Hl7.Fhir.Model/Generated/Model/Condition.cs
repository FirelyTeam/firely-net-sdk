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
    /// Detailed information about conditions, problems or diagnoses
    /// </summary>
    [FhirType("Condition", IsResource=true)]
    [DataContract]
    public partial class Condition : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The clinical status of the Condition or diagnosis
        /// </summary>
        [FhirEnumeration("ConditionStatus")]
        public enum ConditionStatus
        {
            [EnumLiteral("provisional")]
            Provisional, // This is a tentative diagnosis - still a candidate that is under consideration.
            [EnumLiteral("working")]
            Working, // The patient is being treated on the basis that this is the condition, but it is still not confirmed.
            [EnumLiteral("confirmed")]
            Confirmed, // There is sufficient diagnostic and/or clinical evidence to treat this as a confirmed condition.
            [EnumLiteral("refuted")]
            Refuted, // This condition has been ruled out by diagnostic and clinical evidence.
        }
        
        /// <summary>
        /// The type of relationship between a condition and its related item
        /// </summary>
        [FhirEnumeration("ConditionRelationshipType")]
        public enum ConditionRelationshipType
        {
            [EnumLiteral("due-to")]
            DueTo, // this condition follows the identified condition/procedure/substance and is a consequence of it.
            [EnumLiteral("following")]
            Following, // this condition follows the identified condition/procedure/substance, but it is not known whether they are causually linked.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConditionRelatedItemComponent")]
        [DataContract]
        public partial class ConditionRelatedItemComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// due-to | following
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Condition.ConditionRelationshipType> TypeElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Condition.ConditionRelationshipType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.Condition.ConditionRelationshipType>(value);
                }
            }
            
            /// <summary>
            /// Relationship target by means of a predefined code
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code { get; set; }
            
            /// <summary>
            /// Relationship target resource
            /// </summary>
            [FhirElement("target", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConditionEvidenceComponent")]
        [DataContract]
        public partial class ConditionEvidenceComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Manifestation/symptom
            /// </summary>
            [FhirElement("code", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code { get; set; }
            
            /// <summary>
            /// Supporting information found elsewhere
            /// </summary>
            [FhirElement("detail", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Detail { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConditionStageComponent")]
        [DataContract]
        public partial class ConditionStageComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Simple summary (disease specific)
            /// </summary>
            [FhirElement("summary", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Summary { get; set; }
            
            /// <summary>
            /// Formal record of assessment
            /// </summary>
            [FhirElement("assessment", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Assessment { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConditionLocationComponent")]
        [DataContract]
        public partial class ConditionLocationComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Location - may include laterality
            /// </summary>
            [FhirElement("code", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code { get; set; }
            
            /// <summary>
            /// Precise location details
            /// </summary>
            [FhirElement("detail", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DetailElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Detail
            {
                get { return DetailElement != null ? DetailElement.Value : null; }
                set
                {
                    if(value == null)
                      DetailElement = null; 
                    else
                      DetailElement = new Hl7.Fhir.Model.FhirString(value);
                }
            }
            
        }
        
        
        /// <summary>
        /// External Ids for this condition
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// Who has the condition?
        /// </summary>
        [FhirElement("subject", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Encounter when condition first asserted
        /// </summary>
        [FhirElement("encounter", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter { get; set; }
        
        /// <summary>
        /// Person who asserts this condition
        /// </summary>
        [FhirElement("asserter", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Asserter { get; set; }
        
        /// <summary>
        /// When first detected/suspected/entered
        /// </summary>
        [FhirElement("dateAsserted", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Date DateAssertedElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DateAsserted
        {
            get { return DateAssertedElement != null ? DateAssertedElement.Value : null; }
            set
            {
                if(value == null)
                  DateAssertedElement = null; 
                else
                  DateAssertedElement = new Hl7.Fhir.Model.Date(value);
            }
        }
        
        /// <summary>
        /// Identification of the condition, problem or diagnosis
        /// </summary>
        [FhirElement("code", Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code { get; set; }
        
        /// <summary>
        /// E.g. complaint | symptom | finding | diagnosis
        /// </summary>
        [FhirElement("category", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category { get; set; }
        
        /// <summary>
        /// provisional | working | confirmed | refuted
        /// </summary>
        [FhirElement("status", Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Condition.ConditionStatus> StatusElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Condition.ConditionStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Condition.ConditionStatus>(value);
            }
        }
        
        /// <summary>
        /// Degree of confidence
        /// </summary>
        [FhirElement("certainty", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Certainty { get; set; }
        
        /// <summary>
        /// Subjective severity of condition
        /// </summary>
        [FhirElement("severity", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Severity { get; set; }
        
        /// <summary>
        /// Estimated or actual date, or age
        /// </summary>
        [FhirElement("onset", Order=170, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Age))]
        [DataMember]
        public Hl7.Fhir.Model.Element Onset { get; set; }
        
        /// <summary>
        /// If/when in resolution/remission
        /// </summary>
        [FhirElement("abatement", Order=180, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Age),typeof(Hl7.Fhir.Model.FhirBoolean))]
        [DataMember]
        public Hl7.Fhir.Model.Element Abatement { get; set; }
        
        /// <summary>
        /// Stage/grade, usually assessed formally
        /// </summary>
        [FhirElement("stage", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Condition.ConditionStageComponent Stage { get; set; }
        
        /// <summary>
        /// Supporting evidence
        /// </summary>
        [FhirElement("evidence", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Condition.ConditionEvidenceComponent> Evidence { get; set; }
        
        /// <summary>
        /// Anatomical location, if relevant
        /// </summary>
        [FhirElement("location", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Condition.ConditionLocationComponent> Location { get; set; }
        
        /// <summary>
        /// Causes or precedents for this Condition
        /// </summary>
        [FhirElement("relatedItem", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Condition.ConditionRelatedItemComponent> RelatedItem { get; set; }
        
        /// <summary>
        /// Additional information about the Condition
        /// </summary>
        [FhirElement("notes", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NotesElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Notes
        {
            get { return NotesElement != null ? NotesElement.Value : null; }
            set
            {
                if(value == null)
                  NotesElement = null; 
                else
                  NotesElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
    }
    
}
