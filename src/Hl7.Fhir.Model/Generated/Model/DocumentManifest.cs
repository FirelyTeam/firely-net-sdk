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
    /// A manifest that defines a set of documents
    /// </summary>
    [FhirType("DocumentManifest", IsResource=true)]
    [DataContract]
    public partial class DocumentManifest : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// Unique Identifier for the set of documents
        /// </summary>
        [FhirElement("masterIdentifier", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier MasterIdentifier { get; set; }
        
        /// <summary>
        /// Other identifiers for the manifest
        /// </summary>
        [FhirElement("identifier", Order=80)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// The subject of the set of documents
        /// </summary>
        [FhirElement("subject", Order=90)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Subject { get; set; }
        
        /// <summary>
        /// Intended to get notified about this set of documents
        /// </summary>
        [FhirElement("recipient", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Recipient { get; set; }
        
        /// <summary>
        /// What kind of document set this is
        /// </summary>
        [FhirElement("type", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
        
        /// <summary>
        /// Who and/or what authored the document
        /// </summary>
        [FhirElement("author", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Author { get; set; }
        
        /// <summary>
        /// When this document manifest created
        /// </summary>
        [FhirElement("created", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Created
        {
            get { return CreatedElement != null ? CreatedElement.Value : null; }
            set
            {
                if(value == null)
                  CreatedElement = null; 
                else
                  CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
            }
        }
        
        /// <summary>
        /// The source system/application/software
        /// </summary>
        [FhirElement("source", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri SourceElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public System.Uri Source
        {
            get { return SourceElement != null ? SourceElement.Value : null; }
            set
            {
                if(value == null)
                  SourceElement = null; 
                else
                  SourceElement = new Hl7.Fhir.Model.FhirUri(value);
            }
        }
        
        /// <summary>
        /// current | superceded | entered in error
        /// </summary>
        [FhirElement("status", Order=150)]
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
        /// If this document manifest replaces another
        /// </summary>
        [FhirElement("supercedes", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Supercedes { get; set; }
        
        /// <summary>
        /// Human-readable description (title)
        /// </summary>
        [FhirElement("description", Order=170)]
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
        /// Sensitivity of set of documents
        /// </summary>
        [FhirElement("confidentiality", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Confidentiality { get; set; }
        
        /// <summary>
        /// Contents of this set of documents
        /// </summary>
        [FhirElement("content", Order=190)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Content { get; set; }
        
    }
    
}
