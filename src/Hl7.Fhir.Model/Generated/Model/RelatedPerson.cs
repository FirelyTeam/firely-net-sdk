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
    /// An person that is related to a patient, but who is not a direct target of care
    /// </summary>
    [FhirType("RelatedPerson", IsResource=true)]
    [DataContract]
    public partial class RelatedPerson : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// A Human identifier for this person
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// The patient this person is related to
        /// </summary>
        [FhirElement("patient", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient { get; set; }
        
        /// <summary>
        /// The nature of the relationship
        /// </summary>
        [FhirElement("relationship", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Relationship { get; set; }
        
        /// <summary>
        /// A name associated with the person
        /// </summary>
        [FhirElement("name", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.HumanName Name { get; set; }
        
        /// <summary>
        /// A contact detail for the person
        /// </summary>
        [FhirElement("telecom", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }
        
        /// <summary>
        /// Gender for administrative purposes
        /// </summary>
        [FhirElement("gender", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Gender { get; set; }
        
        /// <summary>
        /// Address where the related person can be contacted or visited
        /// </summary>
        [FhirElement("address", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Address Address { get; set; }
        
        /// <summary>
        /// Image of the person
        /// </summary>
        [FhirElement("photo", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> Photo { get; set; }
        
    }
    
}
