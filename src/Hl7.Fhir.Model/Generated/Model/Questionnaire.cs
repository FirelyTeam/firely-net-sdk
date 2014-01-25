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
    /// A structured set of questions and their answers
    /// </summary>
    [FhirType("Questionnaire", IsResource=true)]
    [DataContract]
    public partial class Questionnaire : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// Lifecycle status of the questionnaire
        /// </summary>
        [FhirEnumeration("QuestionnaireStatus")]
        public enum QuestionnaireStatus
        {
            [EnumLiteral("draft")]
            Draft, // This Questionnaire is used as a template but the template is not ready for use or publication.
            [EnumLiteral("published")]
            Published, // This Questionnaire is used as a template, is published and ready for use.
            [EnumLiteral("retired")]
            Retired, // This Questionnaire is used as a template but should no longer be used for new Questionnaires.
            [EnumLiteral("in progress")]
            InProgress, // This Questionnaire has been filled out with answers, but changes or additions are still expected to be made to it.
            [EnumLiteral("completed")]
            Completed, // This Questionnaire has been filled out with answers, and the current content is regarded as definitive.
            [EnumLiteral("amended")]
            Amended, // This Questionnaire has been filled out with answers, then marked as complete, yet changes or additions have been made to it afterwards.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("QuestionComponent")]
        [DataContract]
        public partial class QuestionComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Code or name of the question
            /// </summary>
            [FhirElement("name", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Name { get; set; }
            
            /// <summary>
            /// Text of the question as it is shown to the user
            /// </summary>
            [FhirElement("text", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if(value == null)
                      TextElement = null; 
                    else
                      TextElement = new Hl7.Fhir.Model.FhirString(value);
                }
            }
            
            /// <summary>
            /// Single-valued answer to the question
            /// </summary>
            [FhirElement("answer", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Instant))]
            [DataMember]
            public Hl7.Fhir.Model.Element Answer { get; set; }
            
            /// <summary>
            /// Selected options
            /// </summary>
            [FhirElement("choice", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Choice { get; set; }
            
            /// <summary>
            /// Valueset containing the possible options
            /// </summary>
            [FhirElement("options", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Options { get; set; }
            
            /// <summary>
            /// Structured answer
            /// </summary>
            [FhirElement("data", Order=90, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Element))]
            [DataMember]
            public Hl7.Fhir.Model.Element Data { get; set; }
            
            /// <summary>
            /// Remarks about the answer given
            /// </summary>
            [FhirElement("remarks", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RemarksElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Remarks
            {
                get { return RemarksElement != null ? RemarksElement.Value : null; }
                set
                {
                    if(value == null)
                      RemarksElement = null; 
                    else
                      RemarksElement = new Hl7.Fhir.Model.FhirString(value);
                }
            }
            
            /// <summary>
            /// Nested questionnaire group
            /// </summary>
            [FhirElement("group", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Questionnaire.GroupComponent> Group { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("GroupComponent")]
        [DataContract]
        public partial class GroupComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Code or name of the section on a questionnaire
            /// </summary>
            [FhirElement("name", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Name { get; set; }
            
            /// <summary>
            /// Text that is displayed above the contents of the group
            /// </summary>
            [FhirElement("header", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HeaderElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Header
            {
                get { return HeaderElement != null ? HeaderElement.Value : null; }
                set
                {
                    if(value == null)
                      HeaderElement = null; 
                    else
                      HeaderElement = new Hl7.Fhir.Model.FhirString(value);
                }
            }
            
            /// <summary>
            /// Additional text for the group
            /// </summary>
            [FhirElement("text", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if(value == null)
                      TextElement = null; 
                    else
                      TextElement = new Hl7.Fhir.Model.FhirString(value);
                }
            }
            
            /// <summary>
            /// Whether the contents of this group have a meaningful order
            /// </summary>
            [FhirElement("ordered", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean OrderedElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Ordered
            {
                get { return OrderedElement != null ? OrderedElement.Value : null; }
                set
                {
                    if(value == null)
                      OrderedElement = null; 
                    else
                      OrderedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                }
            }
            
            /// <summary>
            /// The subject this group's answers are about
            /// </summary>
            [FhirElement("subject", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
            
            /// <summary>
            /// Nested questionnaire group
            /// </summary>
            [FhirElement("group", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Questionnaire.GroupComponent> Group { get; set; }
            
            /// <summary>
            /// Questions in this group
            /// </summary>
            [FhirElement("question", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Questionnaire.QuestionComponent> Question { get; set; }
            
        }
        
        
        /// <summary>
        /// draft | published | retired | in progress | completed | amended
        /// </summary>
        [FhirElement("status", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireStatus> StatusElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Questionnaire.QuestionnaireStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireStatus>(value);
            }
        }
        
        /// <summary>
        /// Date this version was authored
        /// </summary>
        [FhirElement("authored", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AuthoredElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Authored
        {
            get { return AuthoredElement != null ? AuthoredElement.Value : null; }
            set
            {
                if(value == null)
                  AuthoredElement = null; 
                else
                  AuthoredElement = new Hl7.Fhir.Model.FhirDateTime(value);
            }
        }
        
        /// <summary>
        /// The subject of the questions
        /// </summary>
        [FhirElement("subject", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Person who received and recorded the answers
        /// </summary>
        [FhirElement("author", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author { get; set; }
        
        /// <summary>
        /// The person who answered the questions
        /// </summary>
        [FhirElement("source", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Source { get; set; }
        
        /// <summary>
        /// Name/code for a predefined list of questions
        /// </summary>
        [FhirElement("name", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Name { get; set; }
        
        /// <summary>
        /// External Ids for this questionnaire
        /// </summary>
        [FhirElement("identifier", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// Primary encounter during which the answers were collected
        /// </summary>
        [FhirElement("encounter", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter { get; set; }
        
        /// <summary>
        /// Grouped questions
        /// </summary>
        [FhirElement("group", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Questionnaire.GroupComponent Group { get; set; }
        
    }
    
}
