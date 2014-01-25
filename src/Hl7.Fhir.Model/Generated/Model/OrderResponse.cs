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
    /// A response to an order
    /// </summary>
    [FhirType("OrderResponse", IsResource=true)]
    [DataContract]
    public partial class OrderResponse : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The status of the response to an order
        /// </summary>
        [FhirEnumeration("OrderOutcomeStatus")]
        public enum OrderOutcomeStatus
        {
            [EnumLiteral("pending")]
            Pending, // The order is known, but no processing has occurred at this time.
            [EnumLiteral("review")]
            Review, // The order is undergoing initial processing to determine whether it will be accepted (usually this involves human review).
            [EnumLiteral("rejected")]
            Rejected, // The order was rejected because of a workflow/business logic reason.
            [EnumLiteral("error")]
            Error, // The order was unable to be processed because of a technical error (i.e. unexpected error).
            [EnumLiteral("accepted")]
            Accepted, // The order has been accepted, and work is in progress.
            [EnumLiteral("cancelled")]
            Cancelled, // Processing the order was halted at the initiators request.
            [EnumLiteral("replaced")]
            Replaced, // The order has been cancelled and replaced by another.
            [EnumLiteral("aborted")]
            Aborted, // Processing the order was stopped because of some workflow/business logic reason.
            [EnumLiteral("complete")]
            Complete, // The order has been completed.
        }
        
        /// <summary>
        /// Identifiers assigned to this order by the orderer or by the receiver
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// The order that this is a response to
        /// </summary>
        [FhirElement("request", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Request { get; set; }
        
        /// <summary>
        /// When the response was made
        /// </summary>
        [FhirElement("date", Order=90)]
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
        /// Who made the response
        /// </summary>
        [FhirElement("who", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Who { get; set; }
        
        /// <summary>
        /// If required by policy
        /// </summary>
        [FhirElement("authority", Order=110, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Authority { get; set; }
        
        /// <summary>
        /// pending | review | rejected | error | accepted | cancelled | replaced | aborted | complete
        /// </summary>
        [FhirElement("code", Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.OrderResponse.OrderOutcomeStatus> CodeElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.OrderResponse.OrderOutcomeStatus? Code
        {
            get { return CodeElement != null ? CodeElement.Value : null; }
            set
            {
                if(value == null)
                  CodeElement = null; 
                else
                  CodeElement = new Code<Hl7.Fhir.Model.OrderResponse.OrderOutcomeStatus>(value);
            }
        }
        
        /// <summary>
        /// Additional description of the response
        /// </summary>
        [FhirElement("description", Order=130)]
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
        /// Details of the outcome of performing the order
        /// </summary>
        [FhirElement("fulfillment", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Fulfillment { get; set; }
        
    }
    
}
