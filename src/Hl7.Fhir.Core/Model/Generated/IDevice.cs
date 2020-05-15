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
    /// An instance of a manufactured te that is used in the provision of healthcare
    /// </summary>
    public partial interface IDevice : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Instance id from manufacturer, owner, and others
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// What kind of device this is
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Lot number of manufacture
        /// </summary>
        Hl7.Fhir.Model.FhirString LotNumberElement { get; set; }
        
        /// <summary>
        /// Lot number of manufacture
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string LotNumber { get; set; }
    
        /// <summary>
        /// Name of device manufacturer
        /// </summary>
        Hl7.Fhir.Model.FhirString ManufacturerElement { get; set; }
        
        /// <summary>
        /// Name of device manufacturer
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Manufacturer { get; set; }
    
        /// <summary>
        /// Manufacture date
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime ManufactureDateElement { get; set; }
        
        /// <summary>
        /// Manufacture date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ManufactureDate { get; set; }
    
        /// <summary>
        /// If the resource is affixed to a person
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Patient { get; set; }
    
        /// <summary>
        /// Organization responsible for device
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Owner { get; set; }
    
        /// <summary>
        /// Details for human/organization for support
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Contact { get; }
    
        /// <summary>
        /// Where the resource is found
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Location { get; set; }
    
        /// <summary>
        /// Network address to contact device
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Network address to contact device
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Device notes and comments
        /// </summary>
        List<Hl7.Fhir.Model.Annotation> Note { get; set; }
    
    }

}
