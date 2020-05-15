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
    /// Search Parameter for a resource
    /// </summary>
    public partial interface ISearchParameter : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Absolute URL used to reference this search parameter
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Absolute URL used to reference this search parameter
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Informal name for this search parameter
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Informal name for this search parameter
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ExperimentalElement { get; set; }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Experimental { get; set; }
    
        /// <summary>
        /// Publication Date(/time)
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// Publication Date(/time)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        Hl7.Fhir.Model.FhirString PublisherElement { get; set; }
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Publisher { get; set; }
    
        /// <summary>
        /// Code used in URL
        /// </summary>
        Hl7.Fhir.Model.Code CodeElement { get; set; }
        
        /// <summary>
        /// Code used in URL
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Code { get; set; }
    
        /// <summary>
        /// number | date | string | token | reference | composite | quantity | uri
        /// </summary>
        Code<Hl7.Fhir.Model.SearchParamType> TypeElement { get; set; }
        
        /// <summary>
        /// number | date | string | token | reference | composite | quantity | uri
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.SearchParamType? Type { get; set; }
    
        /// <summary>
        /// XPath that extracts the values
        /// </summary>
        Hl7.Fhir.Model.FhirString XpathElement { get; set; }
        
        /// <summary>
        /// XPath that extracts the values
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Xpath { get; set; }
    
        /// <summary>
        /// normal | phonetic | nearby | distance | other
        /// </summary>
        Code<Hl7.Fhir.Model.XPathUsageType> XpathUsageElement { get; set; }
        
        /// <summary>
        /// normal | phonetic | nearby | distance | other
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.XPathUsageType? XpathUsage { get; set; }
    
        /// <summary>
        /// Types of resource (if a resource reference)
        /// </summary>
        List<Code<Hl7.Fhir.Model.ResourceType>> TargetElement { get; set; }
        
        /// <summary>
        /// Types of resource (if a resource reference)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<Hl7.Fhir.Model.ResourceType?> Target { get; set; }
    
    }

}
