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
    /// Name of a human - parts and usage
    /// </summary>
    public partial interface IHumanName : Hl7.Fhir.Model.IElement
    {
    
        /// <summary>
        /// usual | official | temp | nickname | anonymous | old | maiden
        /// </summary>
        Code<Hl7.Fhir.Model.NameUse> UseElement { get; set; }
        
        /// <summary>
        /// usual | official | temp | nickname | anonymous | old | maiden
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.NameUse? Use { get; set; }
    
        /// <summary>
        /// Text representation of the full name
        /// </summary>
        Hl7.Fhir.Model.FhirString TextElement { get; set; }
        
        /// <summary>
        /// Text representation of the full name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Text { get; set; }
    
        /// <summary>
        /// Given names (not always 'first'). Includes middle names
        /// </summary>
        List<Hl7.Fhir.Model.FhirString> GivenElement { get; set; }
        
        /// <summary>
        /// Given names (not always 'first'). Includes middle names
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> Given { get; set; }
    
        /// <summary>
        /// Parts that come before the name
        /// </summary>
        List<Hl7.Fhir.Model.FhirString> PrefixElement { get; set; }
        
        /// <summary>
        /// Parts that come before the name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> Prefix { get; set; }
    
        /// <summary>
        /// Parts that come after the name
        /// </summary>
        List<Hl7.Fhir.Model.FhirString> SuffixElement { get; set; }
        
        /// <summary>
        /// Parts that come after the name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> Suffix { get; set; }
    
        /// <summary>
        /// Time period when name was/is in use
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
    }

}
