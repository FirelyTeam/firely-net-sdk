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
    /// A statement of relationships from one set of concepts to one or more other concept systems
    /// </summary>
    [FhirType("ConceptMap", IsResource=true)]
    [DataContract]
    public partial class ConceptMap : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The degree of equivalence between concepts
        /// </summary>
        [FhirEnumeration("ConceptMapEquivalence")]
        public enum ConceptMapEquivalence
        {
            [EnumLiteral("equal")]
            Equal, // The definitions of the concepts are exactly the same (i.e. only grammatical differences) and structural implications of meaning are identifical or irrelevant (i.e. intensionally identical).
            [EnumLiteral("equivalent")]
            Equivalent, // The definitions of the concepts mean the same thing (including when structural implications of meaning are considered) (i.e. extensionally identical).
            [EnumLiteral("wider")]
            Wider, // The target mapping is wider in meaning than the source concept.
            [EnumLiteral("subsumes")]
            Subsumes, // The target mapping subsumes the meaning of the source concept (e.g. the source is-a target).
            [EnumLiteral("narrower")]
            Narrower, // The target mapping is narrower in meaning that the source concept. The sense in which the mapping is narrower SHALL be described in the comments in this case, and applications should be careful when atempting to use these mappings operationally.
            [EnumLiteral("specialises")]
            Specialises, // The target mapping specialises the meaning of the source concept (e.g. the target is-a source).
            [EnumLiteral("inexact")]
            Inexact, // The target mapping overlaps with the source concept, but both source and target cover additional meaning. The sense in which the mapping is narrower SHALL be described in the comments in this case, and applications should be careful when atempting to use these mappings operationally.
            [EnumLiteral("unmatched")]
            Unmatched, // There is no match for this concept in the destination concept system.
            [EnumLiteral("disjoint")]
            Disjoint, // This is an explicit assertion that there is no mapping between the source and target concept.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConceptMapConceptMapComponent")]
        [DataContract]
        public partial class ConceptMapConceptMapComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// System of the target
            /// </summary>
            [FhirElement("system", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri System
            {
                get { return SystemElement != null ? SystemElement.Value : null; }
                set
                {
                    if(value == null)
                      SystemElement = null; 
                    else
                      SystemElement = new Hl7.Fhir.Model.FhirUri(value);
                }
            }
            
            /// <summary>
            /// Code that identifies the target concept
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Hl7.Fhir.Model.Code(value);
                }
            }
            
            /// <summary>
            /// equal | equivalent | wider | subsumes | narrower | specialises | inexact | unmatched | disjoint
            /// </summary>
            [FhirElement("equivalence", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ConceptMap.ConceptMapEquivalence> EquivalenceElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ConceptMap.ConceptMapEquivalence? Equivalence
            {
                get { return EquivalenceElement != null ? EquivalenceElement.Value : null; }
                set
                {
                    if(value == null)
                      EquivalenceElement = null; 
                    else
                      EquivalenceElement = new Code<Hl7.Fhir.Model.ConceptMap.ConceptMapEquivalence>(value);
                }
            }
            
            /// <summary>
            /// Description of status/issues in mapping
            /// </summary>
            [FhirElement("comments", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentsElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Comments
            {
                get { return CommentsElement != null ? CommentsElement.Value : null; }
                set
                {
                    if(value == null)
                      CommentsElement = null; 
                    else
                      CommentsElement = new Hl7.Fhir.Model.FhirString(value);
                }
            }
            
            /// <summary>
            /// Other concepts that this mapping also produces
            /// </summary>
            [FhirElement("product", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ConceptMap.OtherConceptComponent> Product { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("OtherConceptComponent")]
        [DataContract]
        public partial class OtherConceptComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Reference to element/field/valueset provides the context
            /// </summary>
            [FhirElement("concept", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri ConceptElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri Concept
            {
                get { return ConceptElement != null ? ConceptElement.Value : null; }
                set
                {
                    if(value == null)
                      ConceptElement = null; 
                    else
                      ConceptElement = new Hl7.Fhir.Model.FhirUri(value);
                }
            }
            
            /// <summary>
            /// System for a concept in the referenced concept
            /// </summary>
            [FhirElement("system", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri System
            {
                get { return SystemElement != null ? SystemElement.Value : null; }
                set
                {
                    if(value == null)
                      SystemElement = null; 
                    else
                      SystemElement = new Hl7.Fhir.Model.FhirUri(value);
                }
            }
            
            /// <summary>
            /// Code for a concept in the referenced concept
            /// </summary>
            [FhirElement("code", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Hl7.Fhir.Model.Code(value);
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConceptMapConceptComponent")]
        [DataContract]
        public partial class ConceptMapConceptComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// System that defines the concept being mapped
            /// </summary>
            [FhirElement("system", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri System
            {
                get { return SystemElement != null ? SystemElement.Value : null; }
                set
                {
                    if(value == null)
                      SystemElement = null; 
                    else
                      SystemElement = new Hl7.Fhir.Model.FhirUri(value);
                }
            }
            
            /// <summary>
            /// Identifies concept being mapped
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Hl7.Fhir.Model.Code(value);
                }
            }
            
            /// <summary>
            /// Other concepts required for this mapping (from context)
            /// </summary>
            [FhirElement("dependsOn", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ConceptMap.OtherConceptComponent> DependsOn { get; set; }
            
            /// <summary>
            /// A concept from the target value set that this concept maps to
            /// </summary>
            [FhirElement("map", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ConceptMap.ConceptMapConceptMapComponent> Map { get; set; }
            
        }
        
        
        /// <summary>
        /// Logical id to reference this concept map
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString IdentifierElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Identifier
        {
            get { return IdentifierElement != null ? IdentifierElement.Value : null; }
            set
            {
                if(value == null)
                  IdentifierElement = null; 
                else
                  IdentifierElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Logical id for this version of the concept map
        /// </summary>
        [FhirElement("version", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if(value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Informal name for this concept map
        /// </summary>
        [FhirElement("name", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if(value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        [FhirElement("publisher", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if(value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Contact information of the publisher
        /// </summary>
        [FhirElement("telecom", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }
        
        /// <summary>
        /// Human language description of the concept map
        /// </summary>
        [FhirElement("description", Order=120)]
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
        /// About the concept map or its content
        /// </summary>
        [FhirElement("copyright", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CopyrightElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Copyright
        {
            get { return CopyrightElement != null ? CopyrightElement.Value : null; }
            set
            {
                if(value == null)
                  CopyrightElement = null; 
                else
                  CopyrightElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Code StatusElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Hl7.Fhir.Model.Code(value);
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if(value == null)
                  ExperimentalElement = null; 
                else
                  ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
            }
        }
        
        /// <summary>
        /// Date for given status
        /// </summary>
        [FhirElement("date", Order=160)]
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
        /// Identifies the source value set which is being mapped
        /// </summary>
        [FhirElement("source", Order=170)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Source { get; set; }
        
        /// <summary>
        /// Provides context to the mappings
        /// </summary>
        [FhirElement("target", Order=180)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Target { get; set; }
        
        /// <summary>
        /// Mappings for a concept from the source valueset
        /// </summary>
        [FhirElement("concept", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ConceptMap.ConceptMapConceptComponent> Concept { get; set; }
        
    }
    
}
