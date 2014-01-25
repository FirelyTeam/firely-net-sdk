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
    /// (informative) A container for slot(s) of time that may be available for booking appointments
    /// </summary>
    [FhirType("Availability", IsResource=true)]
    [DataContract]
    public partial class Availability : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// External Ids for this item
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// The type of appointments that can be booked into slots attached to this availability resource (ideally this would be an identifiable service - which is at a location, rather than the location itself) - change to CodeableConcept
        /// </summary>
        [FhirElement("type", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
        
        /// <summary>
        /// The type of resource this availability resource is providing availability information for
        /// </summary>
        [FhirElement("individual", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Individual { get; set; }
        
        /// <summary>
        /// The period of time that the slots that are attached to this availability resource cover (even if none exist)
        /// </summary>
        [FhirElement("period", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period { get; set; }
        
        /// <summary>
        /// Comments on the availability to describe any extended information. Such as custom constraints on the slot(s) that may be associated
        /// </summary>
        [FhirElement("comment", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comment
        {
            get { return CommentElement != null ? CommentElement.Value : null; }
            set
            {
                if(value == null)
                  CommentElement = null; 
                else
                  CommentElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Who authored the availability
        /// </summary>
        [FhirElement("author", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author { get; set; }
        
        /// <summary>
        /// When this availability was created, or last revised
        /// </summary>
        [FhirElement("authorDate", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AuthorDateElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AuthorDate
        {
            get { return AuthorDateElement != null ? AuthorDateElement.Value : null; }
            set
            {
                if(value == null)
                  AuthorDateElement = null; 
                else
                  AuthorDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
            }
        }
        
    }
    
}
