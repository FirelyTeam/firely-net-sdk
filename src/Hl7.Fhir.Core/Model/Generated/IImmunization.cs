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
    /// Immunization event information
    /// </summary>
    public partial interface IImmunization : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Business identifier
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Vaccine product administered
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept VaccineCode { get; set; }
    
        /// <summary>
        /// Who was immunized
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Patient { get; set; }
    
        /// <summary>
        /// Encounter administered as part of
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Encounter { get; set; }
    
        /// <summary>
        /// Where vaccination occurred
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Location { get; set; }
    
        /// <summary>
        /// Vaccine manufacturer
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Manufacturer { get; set; }
    
        /// <summary>
        /// Vaccine lot number
        /// </summary>
        Hl7.Fhir.Model.FhirString LotNumberElement { get; set; }
        
        /// <summary>
        /// Vaccine lot number
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string LotNumber { get; set; }
    
        /// <summary>
        /// Vaccine expiration date
        /// </summary>
        Hl7.Fhir.Model.Date ExpirationDateElement { get; set; }
        
        /// <summary>
        /// Vaccine expiration date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ExpirationDate { get; set; }
    
        /// <summary>
        /// Body site vaccine  was administered
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Site { get; set; }
    
        /// <summary>
        /// How vaccine entered body
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Route { get; set; }
    
        /// <summary>
        /// Amount of vaccine administered
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity DoseQuantity { get; set; }
    
        /// <summary>
        /// Vaccination notes
        /// </summary>
        List<Hl7.Fhir.Model.Annotation> Note { get; set; }
    
        /// <summary>
        /// Details of a reaction that follows immunization
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IImmunizationReactionComponent> Reaction { get; }
    
    }
    
    public partial interface IImmunizationReactionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// When reaction started
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// When reaction started
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// Additional information on reaction
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Detail { get; set; }
    
        /// <summary>
        /// Indicates self-reported reaction
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ReportedElement { get; set; }
        
        /// <summary>
        /// Indicates self-reported reaction
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Reported { get; set; }
    
    }

}
