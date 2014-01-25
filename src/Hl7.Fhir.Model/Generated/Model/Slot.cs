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
    /// (informative) A slot of time that may be available for booking appointments
    /// </summary>
    [FhirType("Slot", IsResource=true)]
    [DataContract]
    public partial class Slot : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The free/busy status of an appointment
        /// </summary>
        [FhirEnumeration("SlotStatus")]
        public enum SlotStatus
        {
            [EnumLiteral("BUSY")]
            BUSY, // Indicates that the time interval is busy because one  or more events have been scheduled for that interval.
            [EnumLiteral("FREE")]
            FREE, // Indicates that the time interval is free for scheduling.
            [EnumLiteral("BUSY-UNAVAILABLE")]
            BUSYUNAVAILABLE, // Indicates that the time interval is busy and that the interval can not be scheduled.
            [EnumLiteral("BUSY-TENTATIVE")]
            BUSYTENTATIVE, // Indicates that the time interval is busy because one or more events have been tentatively scheduled for that interval.
        }
        
        /// <summary>
        /// External Ids for this item
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// The type of appointments that can be booked into this slot (ideally this would be an identifiable service - which is at a location, rather than the location itself). If provided then this overrides the value provided on the availability resource
        /// </summary>
        [FhirElement("type", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
        
        /// <summary>
        /// The availability resource that this slot defines an interval of status information
        /// </summary>
        [FhirElement("availability", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Availability { get; set; }
        
        /// <summary>
        /// BUSY | FREE | BUSY-UNAVAILABLE | BUSY-TENTATIVE
        /// </summary>
        [FhirElement("freeBusyType", Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Slot.SlotStatus> FreeBusyTypeElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Slot.SlotStatus? FreeBusyType
        {
            get { return FreeBusyTypeElement != null ? FreeBusyTypeElement.Value : null; }
            set
            {
                if(value == null)
                  FreeBusyTypeElement = null; 
                else
                  FreeBusyTypeElement = new Code<Hl7.Fhir.Model.Slot.SlotStatus>(value);
            }
        }
        
        /// <summary>
        /// Date/Time that the slot is to begin
        /// </summary>
        [FhirElement("start", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant StartElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Start
        {
            get { return StartElement != null ? StartElement.Value : null; }
            set
            {
                if(value == null)
                  StartElement = null; 
                else
                  StartElement = new Hl7.Fhir.Model.Instant(value);
            }
        }
        
        /// <summary>
        /// Date/Time that the slot is to conclude
        /// </summary>
        [FhirElement("end", Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant EndElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? End
        {
            get { return EndElement != null ? EndElement.Value : null; }
            set
            {
                if(value == null)
                  EndElement = null; 
                else
                  EndElement = new Hl7.Fhir.Model.Instant(value);
            }
        }
        
        /// <summary>
        /// Comments on the slot to describe any extended information. Such as custom constraints on the slot
        /// </summary>
        [FhirElement("comment", Order=130)]
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
        /// Who authored the slot
        /// </summary>
        [FhirElement("author", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author { get; set; }
        
        /// <summary>
        /// When this slot was created, or last revised
        /// </summary>
        [FhirElement("authorDate", Order=150)]
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
