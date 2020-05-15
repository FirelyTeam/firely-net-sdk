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
    /// A series of measurements taken by a device
    /// </summary>
    public partial interface ISampledData : Hl7.Fhir.Model.IElement
    {
    
        /// <summary>
        /// Zero value and units
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Origin { get; set; }
    
        /// <summary>
        /// Number of milliseconds between samples
        /// </summary>
        Hl7.Fhir.Model.FhirDecimal PeriodElement { get; set; }
        
        /// <summary>
        /// Number of milliseconds between samples
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        decimal? Period { get; set; }
    
        /// <summary>
        /// Multiply data by this before adding to origin
        /// </summary>
        Hl7.Fhir.Model.FhirDecimal FactorElement { get; set; }
        
        /// <summary>
        /// Multiply data by this before adding to origin
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        decimal? Factor { get; set; }
    
        /// <summary>
        /// Lower limit of detection
        /// </summary>
        Hl7.Fhir.Model.FhirDecimal LowerLimitElement { get; set; }
        
        /// <summary>
        /// Lower limit of detection
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        decimal? LowerLimit { get; set; }
    
        /// <summary>
        /// Upper limit of detection
        /// </summary>
        Hl7.Fhir.Model.FhirDecimal UpperLimitElement { get; set; }
        
        /// <summary>
        /// Upper limit of detection
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        decimal? UpperLimit { get; set; }
    
        /// <summary>
        /// Number of sample points at each time point
        /// </summary>
        Hl7.Fhir.Model.PositiveInt DimensionsElement { get; set; }
        
        /// <summary>
        /// Number of sample points at each time point
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Dimensions { get; set; }
    
        /// <summary>
        /// Decimal values with spaces, or "E" | "U" | "L"
        /// </summary>
        Hl7.Fhir.Model.FhirString DataElement { get; set; }
        
        /// <summary>
        /// Decimal values with spaces, or "E" | "U" | "L"
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Data { get; set; }
    
    }

}
