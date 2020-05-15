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
// Generated for FHIR v1.0.2, v4.0.1, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Dispensing a medication to a named patient
    /// </summary>
    public partial interface IMedicationDispense : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// What medication was supplied
        /// </summary>
        Hl7.Fhir.Model.Element Medication { get; set; }
    
        /// <summary>
        /// Medication order that authorizes the dispense
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> AuthorizingPrescription { get; set; }
    
        /// <summary>
        /// Trial fill, partial fill, emergency fill, etc.
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Amount dispensed
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Quantity { get; set; }
    
        /// <summary>
        /// Days Supply
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity DaysSupply { get; set; }
    
        /// <summary>
        /// Dispense processing time
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime WhenPreparedElement { get; set; }
        
        /// <summary>
        /// Dispense processing time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string WhenPrepared { get; set; }
    
        /// <summary>
        /// When product was given out
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime WhenHandedOverElement { get; set; }
        
        /// <summary>
        /// When product was given out
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string WhenHandedOver { get; set; }
    
        /// <summary>
        /// Where the medication was sent
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Destination { get; set; }
    
        /// <summary>
        /// Who collected the medication
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Receiver { get; set; }
    
        /// <summary>
        /// Deals with substitution of one medicine for another
        /// </summary>
        Hl7.Fhir.Model.IMedicationDispenseSubstitutionComponent Substitution { get; }
    
    }
    
    public partial interface IMedicationDispenseSubstitutionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Type of substitution
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Why was substitution made
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Reason { get; set; }
    
        /// <summary>
        /// Who is responsible for the substitution
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> ResponsibleParty { get; set; }
    
    }

}
