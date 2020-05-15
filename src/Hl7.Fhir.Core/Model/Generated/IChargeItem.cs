using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;

/*
    Copyright (c) 2011+, HL7, Inc.
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
#pragma warning disable 1591 // suppress XML summary warnings

//
// Generated for FHIR v4.0.1, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Item containing charge code(s) associated with the provision of healthcare provider products
    /// </summary>
    public partial interface IChargeItem : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// planned | billable | not-billable | aborted | billed | entered-in-error | unknown
        /// </summary>
        Code<Hl7.Fhir.Model.ChargeItemStatus> StatusElement { get; set; }
        
        /// <summary>
        /// planned | billable | not-billable | aborted | billed | entered-in-error | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ChargeItemStatus? Status { get; set; }
    
        /// <summary>
        /// Part of referenced ChargeItem
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> PartOf { get; set; }
    
        /// <summary>
        /// A code that identifies the charge, like a billing code
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Individual service was done for/to
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// Encounter / Episode associated with event
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Context { get; set; }
    
        /// <summary>
        /// When the charged service was applied
        /// </summary>
        Hl7.Fhir.Model.Element Occurrence { get; set; }
    
        /// <summary>
        /// Organization providing the charged service
        /// </summary>
        Hl7.Fhir.Model.ResourceReference PerformingOrganization { get; set; }
    
        /// <summary>
        /// Organization requesting the charged service
        /// </summary>
        Hl7.Fhir.Model.ResourceReference RequestingOrganization { get; set; }
    
        /// <summary>
        /// Quantity of which the charge item has been serviced
        /// </summary>
        Hl7.Fhir.Model.Quantity Quantity { get; set; }
    
        /// <summary>
        /// Anatomical location, if relevant
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Bodysite { get; set; }
    
        /// <summary>
        /// Factor overriding the associated rules
        /// </summary>
        Hl7.Fhir.Model.FhirDecimal FactorOverrideElement { get; set; }
        
        /// <summary>
        /// Factor overriding the associated rules
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        decimal? FactorOverride { get; set; }
    
        /// <summary>
        /// Price overriding the associated rules
        /// </summary>
        Hl7.Fhir.Model.IMoney PriceOverride { get; }
    
        /// <summary>
        /// Reason for overriding the list price/factor
        /// </summary>
        Hl7.Fhir.Model.FhirString OverrideReasonElement { get; set; }
        
        /// <summary>
        /// Reason for overriding the list price/factor
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string OverrideReason { get; set; }
    
        /// <summary>
        /// Individual who was entering
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Enterer { get; set; }
    
        /// <summary>
        /// Date the charge item was entered
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime EnteredDateElement { get; set; }
        
        /// <summary>
        /// Date the charge item was entered
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string EnteredDate { get; set; }
    
        /// <summary>
        /// Why was the charged  service rendered?
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Reason { get; set; }
    
        /// <summary>
        /// Which rendered service is being charged?
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Service { get; set; }
    
        /// <summary>
        /// Account to place this charge
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Account { get; set; }
    
        /// <summary>
        /// Comments made about the ChargeItem
        /// </summary>
        List<Hl7.Fhir.Model.Annotation> Note { get; set; }
    
        /// <summary>
        /// Further information supporting this charge
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> SupportingInformation { get; set; }
    
    }

}
