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
    /// A set of resources composed into a single coherent clinical statement with clinical attestation
    /// </summary>
    [FhirType("Composition", IsResource=true)]
    [DataContract]
    public partial class Composition : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The workflow/clinical status of the composition
        /// </summary>
        [FhirEnumeration("CompositionStatus")]
        public enum CompositionStatus
        {
            [EnumLiteral("preliminary")]
            Preliminary, // This is a preliminary composition or document (also known as initial or interim). The content may be incomplete or unverified.
            [EnumLiteral("final")]
            Final, // The composition or document is complete and verified by an appropriate person, and no further work is planned.
            [EnumLiteral("appended")]
            Appended, // The composition or document has been modified subsequent to being released as "final", and is complete and verified by an authorized person. The modifications added new information to the composition or document, but did not revise existing content.
            [EnumLiteral("amended")]
            Amended, // The composition or document has been modified subsequent to being released as "final", and is complete and verified by an authorized person.
            [EnumLiteral("entered in error")]
            EnteredInError, // The composition or document was originally created/issued in error, and this is an amendment that marks that the entire series should not be considered as valid.
        }
        
        /// <summary>
        /// The way in which a person authenticated a composition
        /// </summary>
        [FhirEnumeration("CompositionAttestationMode")]
        public enum CompositionAttestationMode
        {
            [EnumLiteral("personal")]
            Personal, // The person authenticated the content in their personal capacity.
            [EnumLiteral("professional")]
            Professional, // The person authenticated the content in their professional capacity.
            [EnumLiteral("legal")]
            Legal, // The person authenticated the content and accepted legal responsibility for its content.
            [EnumLiteral("official")]
            Official, // The organization authenticated the content as consistent with their policies and procedures.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SectionComponent")]
        [DataContract]
        public partial class SectionComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Classification of section (recommended)
            /// </summary>
            [FhirElement("code", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code { get; set; }
            
            /// <summary>
            /// If section different to composition
            /// </summary>
            [FhirElement("subject", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
            
            /// <summary>
            /// The actual data for the section
            /// </summary>
            [FhirElement("content", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Content { get; set; }
            
            /// <summary>
            /// Nested Section
            /// </summary>
            [FhirElement("section", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Composition.SectionComponent> Section { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("CompositionEventComponent")]
        [DataContract]
        public partial class CompositionEventComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Code(s) that apply to the event being documented
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Code { get; set; }
            
            /// <summary>
            /// The period covered by the documentation
            /// </summary>
            [FhirElement("period", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period { get; set; }
            
            /// <summary>
            /// Full details for the event(s) the composition consents
            /// </summary>
            [FhirElement("detail", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Detail { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("CompositionAttesterComponent")]
        [DataContract]
        public partial class CompositionAttesterComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// personal | professional | legal | official
            /// </summary>
            [FhirElement("mode", Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.Composition.CompositionAttestationMode>> ModeElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.Composition.CompositionAttestationMode?> Mode
            {
                get { return ModeElement != null ? ModeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      ModeElement = null; 
                    else
                      ModeElement = new List<Code<Hl7.Fhir.Model.Composition.CompositionAttestationMode>>(value.Select(elem=>new Code<Hl7.Fhir.Model.Composition.CompositionAttestationMode>(elem)));
                }
            }
            
            /// <summary>
            /// When composition attested
            /// </summary>
            [FhirElement("time", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime TimeElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Time
            {
                get { return TimeElement != null ? TimeElement.Value : null; }
                set
                {
                    if(value == null)
                      TimeElement = null; 
                    else
                      TimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                }
            }
            
            /// <summary>
            /// Who attested the composition
            /// </summary>
            [FhirElement("party", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Party { get; set; }
            
        }
        
        
        /// <summary>
        /// Logical identifier of composition (version-independent)
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier { get; set; }
        
        /// <summary>
        /// Composition editing time
        /// </summary>
        [FhirElement("instant", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant InstantElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Instant
        {
            get { return InstantElement != null ? InstantElement.Value : null; }
            set
            {
                if(value == null)
                  InstantElement = null; 
                else
                  InstantElement = new Hl7.Fhir.Model.Instant(value);
            }
        }
        
        /// <summary>
        /// Kind of composition (LOINC if possible)
        /// </summary>
        [FhirElement("type", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
        
        /// <summary>
        /// Categorization of Composition
        /// </summary>
        [FhirElement("class", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Class { get; set; }
        
        /// <summary>
        /// Human Readable name/title
        /// </summary>
        [FhirElement("title", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if(value == null)
                  TitleElement = null; 
                else
                  TitleElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// preliminary | final | appended | amended | entered in error
        /// </summary>
        [FhirElement("status", Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Composition.CompositionStatus> StatusElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Composition.CompositionStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Composition.CompositionStatus>(value);
            }
        }
        
        /// <summary>
        /// As defined by affinity domain
        /// </summary>
        [FhirElement("confidentiality", Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Confidentiality { get; set; }
        
        /// <summary>
        /// Who and/or what the composition is about
        /// </summary>
        [FhirElement("subject", Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Who and/or what authored the composition
        /// </summary>
        [FhirElement("author", Order=150)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Author { get; set; }
        
        /// <summary>
        /// Attests to accuracy of composition
        /// </summary>
        [FhirElement("attester", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Composition.CompositionAttesterComponent> Attester { get; set; }
        
        /// <summary>
        /// Org which maintains the composition
        /// </summary>
        [FhirElement("custodian", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Custodian { get; set; }
        
        /// <summary>
        /// The clinical event/act/item being documented
        /// </summary>
        [FhirElement("event", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Composition.CompositionEventComponent Event { get; set; }
        
        /// <summary>
        /// Context of the conposition
        /// </summary>
        [FhirElement("encounter", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter { get; set; }
        
        /// <summary>
        /// Composition is broken into sections
        /// </summary>
        [FhirElement("section", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Composition.SectionComponent> Section { get; set; }
        
    }
    
}
