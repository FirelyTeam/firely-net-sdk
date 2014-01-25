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
    /// A request to perform an action
    /// </summary>
    [FhirType("Order", IsResource=true)]
    [DataContract]
    public partial class Order : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// null
        /// </summary>
        [FhirType("OrderWhenComponent")]
        [DataContract]
        public partial class OrderWhenComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Code specifies when request should be done. The code may simply be a priority code
            /// </summary>
            [FhirElement("code", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code { get; set; }
            
            /// <summary>
            /// A formal schedule
            /// </summary>
            [FhirElement("schedule", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Schedule Schedule { get; set; }
            
        }
        
        
        /// <summary>
        /// Identifiers assigned to this order by the orderer or by the receiver
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// When the order was made
        /// </summary>
        [FhirElement("date", Order=80)]
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
        /// Patient this order is about
        /// </summary>
        [FhirElement("subject", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Who initiated the order
        /// </summary>
        [FhirElement("source", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Source { get; set; }
        
        /// <summary>
        /// Who is intended to fulfill the order
        /// </summary>
        [FhirElement("target", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Target { get; set; }
        
        /// <summary>
        /// Text - why the order was made
        /// </summary>
        [FhirElement("reason", Order=120, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Reason { get; set; }
        
        /// <summary>
        /// If required by policy
        /// </summary>
        [FhirElement("authority", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Authority { get; set; }
        
        /// <summary>
        /// When order should be fulfilled
        /// </summary>
        [FhirElement("when", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Order.OrderWhenComponent When { get; set; }
        
        /// <summary>
        /// What action is being ordered
        /// </summary>
        [FhirElement("detail", Order=150)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Detail { get; set; }
        
    }
    
}
