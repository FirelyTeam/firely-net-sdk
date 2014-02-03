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
    /// A supply -  request and provision
    /// </summary>
    [FhirType("Supply", IsResource=true)]
    [DataContract]
    public partial class Supply : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// Status of the dispense
        /// </summary>
        [FhirEnumeration("SupplyDispenseStatus")]
        public enum SupplyDispenseStatus
        {
            [EnumLiteral("in progress")]
            InProgress, // Supply has been requested, but not dispensed.
            [EnumLiteral("dispensed")]
            Dispensed, // Supply is part of a pharmacy order and has been dispensed.
            [EnumLiteral("abandoned")]
            Abandoned, // Dispensing was not completed.
        }
        
        /// <summary>
        /// Status of the supply
        /// </summary>
        [FhirEnumeration("SupplyStatus")]
        public enum SupplyStatus
        {
            [EnumLiteral("requested")]
            Requested, // Supply has been requested, but not dispensed.
            [EnumLiteral("dispensed")]
            Dispensed, // Supply is part of a pharmacy order and has been dispensed.
            [EnumLiteral("received")]
            Received, // Supply has been received by the requestor.
            [EnumLiteral("failed")]
            Failed, // The supply will not be completed because the supplier was unable or unwilling to supply the item.
            [EnumLiteral("cancelled")]
            Cancelled, // The orderer of the supply cancelled the request.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SupplyDispenseComponent")]
        [DataContract]
        public partial class SupplyDispenseComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// External identifier
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier { get; set; }
            
            /// <summary>
            /// in progress | dispensed | abandoned
            /// </summary>
            [FhirElement("status", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Supply.SupplyDispenseStatus> StatusElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Supply.SupplyDispenseStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if(value == null)
                      StatusElement = null; 
                    else
                      StatusElement = new Code<Hl7.Fhir.Model.Supply.SupplyDispenseStatus>(value);
                }
            }
            
            /// <summary>
            /// Category of dispense event
            /// </summary>
            [FhirElement("type", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
            
            /// <summary>
            /// Amount dispensed
            /// </summary>
            [FhirElement("quantity", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity { get; set; }
            
            /// <summary>
            /// Medication, Substance, or Device supplied
            /// </summary>
            [FhirElement("suppliedItem", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference SuppliedItem { get; set; }
            
            /// <summary>
            /// Dispenser
            /// </summary>
            [FhirElement("supplier", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Supplier { get; set; }
            
            /// <summary>
            /// Dispensing time
            /// </summary>
            [FhirElement("whenPrepared", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Period WhenPrepared { get; set; }
            
            /// <summary>
            /// Handover time
            /// </summary>
            [FhirElement("whenHandedOver", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Period WhenHandedOver { get; set; }
            
            /// <summary>
            /// Where the Supply was sent
            /// </summary>
            [FhirElement("destination", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Destination { get; set; }
            
            /// <summary>
            /// Who collected the Supply
            /// </summary>
            [FhirElement("receiver", Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Receiver { get; set; }
            
        }
        
        
        /// <summary>
        /// The kind of supply (central, non-stock, etc)
        /// </summary>
        [FhirElement("kind", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Kind { get; set; }
        
        /// <summary>
        /// Unique identifier
        /// </summary>
        [FhirElement("identifier", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier { get; set; }
        
        /// <summary>
        /// requested | dispensed | received | failed | cancelled
        /// </summary>
        [FhirElement("status", Order=90)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Supply.SupplyStatus> StatusElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Supply.SupplyStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Supply.SupplyStatus>(value);
            }
        }
        
        /// <summary>
        /// Medication, Substance, or Device requested to be supplied
        /// </summary>
        [FhirElement("orderedItem", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference OrderedItem { get; set; }
        
        /// <summary>
        /// Patient for whom the item is supplied
        /// </summary>
        [FhirElement("patient", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient { get; set; }
        
        /// <summary>
        /// Supply details
        /// </summary>
        [FhirElement("dispense", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Supply.SupplyDispenseComponent> Dispense { get; set; }
        
    }
    
}
