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
    /// Sample for analysis
    /// </summary>
    [FhirType("Specimen", IsResource=true)]
    [DataContract]
    public partial class Specimen : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// Type indicating if this is a parent or child relationship
        /// </summary>
        [FhirEnumeration("HierarchicalRelationshipType")]
        public enum HierarchicalRelationshipType
        {
            [EnumLiteral("parent")]
            Parent, // The target resource is the parent of the focal specimen resource.
            [EnumLiteral("child")]
            Child, // The target resource is the child of the focal specimen resource.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SpecimenCollectionComponent")]
        [DataContract]
        public partial class SpecimenCollectionComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Who collected the specimen
            /// </summary>
            [FhirElement("collector", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Collector { get; set; }
            
            /// <summary>
            /// Collector comments
            /// </summary>
            [FhirElement("comment", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> CommentElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Comment
            {
                get { return CommentElement != null ? CommentElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      CommentElement = null; 
                    else
                      CommentElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                }
            }
            
            /// <summary>
            /// Collection time
            /// </summary>
            [FhirElement("collected", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Collected { get; set; }
            
            /// <summary>
            /// The quantity of specimen collected
            /// </summary>
            [FhirElement("quantity", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity { get; set; }
            
            /// <summary>
            /// Technique used to perform collection
            /// </summary>
            [FhirElement("method", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method { get; set; }
            
            /// <summary>
            /// Anatomical collection site
            /// </summary>
            [FhirElement("sourceSite", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept SourceSite { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SpecimenSourceComponent")]
        [DataContract]
        public partial class SpecimenSourceComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// parent | child
            /// </summary>
            [FhirElement("relationship", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Specimen.HierarchicalRelationshipType> RelationshipElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Specimen.HierarchicalRelationshipType? Relationship
            {
                get { return RelationshipElement != null ? RelationshipElement.Value : null; }
                set
                {
                    if(value == null)
                      RelationshipElement = null; 
                    else
                      RelationshipElement = new Code<Hl7.Fhir.Model.Specimen.HierarchicalRelationshipType>(value);
                }
            }
            
            /// <summary>
            /// The subject of the relationship
            /// </summary>
            [FhirElement("target", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Target { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SpecimenTreatmentComponent")]
        [DataContract]
        public partial class SpecimenTreatmentComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Textual description of procedure
            /// </summary>
            [FhirElement("description", Order=40)]
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
            /// Indicates the treatment or processing step  applied to the specimen
            /// </summary>
            [FhirElement("procedure", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Procedure { get; set; }
            
            /// <summary>
            /// Material used in the processing step
            /// </summary>
            [FhirElement("additive", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Additive { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SpecimenContainerComponent")]
        [DataContract]
        public partial class SpecimenContainerComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Id for the container
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
            
            /// <summary>
            /// Textual description of the container
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
            /// Kind of container directly associated with specimen
            /// </summary>
            [FhirElement("type", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
            
            /// <summary>
            /// Container volume or size
            /// </summary>
            [FhirElement("capacity", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Capacity { get; set; }
            
            /// <summary>
            /// Quantity of specimen within container
            /// </summary>
            [FhirElement("specimenQuantity", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity SpecimenQuantity { get; set; }
            
            /// <summary>
            /// Additive associated with container
            /// </summary>
            [FhirElement("additive", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Additive { get; set; }
            
        }
        
        
        /// <summary>
        /// External Identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// Kind of material that forms the specimen
        /// </summary>
        [FhirElement("type", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
        
        /// <summary>
        /// Parent of specimen
        /// </summary>
        [FhirElement("source", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Specimen.SpecimenSourceComponent> Source { get; set; }
        
        /// <summary>
        /// Where the specimen came from. This may be the patient(s) or from the environment or  a device
        /// </summary>
        [FhirElement("subject", Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Identifier assigned by the lab
        /// </summary>
        [FhirElement("accessionIdentifier", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier AccessionIdentifier { get; set; }
        
        /// <summary>
        /// The time when specimen was received for processing
        /// </summary>
        [FhirElement("receivedTime", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ReceivedTimeElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ReceivedTime
        {
            get { return ReceivedTimeElement != null ? ReceivedTimeElement.Value : null; }
            set
            {
                if(value == null)
                  ReceivedTimeElement = null; 
                else
                  ReceivedTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
            }
        }
        
        /// <summary>
        /// Collection details
        /// </summary>
        [FhirElement("collection", Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Specimen.SpecimenCollectionComponent Collection { get; set; }
        
        /// <summary>
        /// Treatment and processing step details
        /// </summary>
        [FhirElement("treatment", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Specimen.SpecimenTreatmentComponent> Treatment { get; set; }
        
        /// <summary>
        /// Direct container of specimen (tube/slide, etc)
        /// </summary>
        [FhirElement("container", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Specimen.SpecimenContainerComponent> Container { get; set; }
        
    }
    
}
