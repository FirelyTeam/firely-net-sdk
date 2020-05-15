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
    /// A map from one set of concepts to one or more other concepts
    /// </summary>
    public partial interface IConceptMap : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Globally unique logical id for concept map
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Globally unique logical id for concept map
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Additional identifier for the concept map
        /// </summary>
        Hl7.Fhir.Model.Identifier Identifier { get; set; }
    
        /// <summary>
        /// Logical id for this version of the concept map
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Logical id for this version of the concept map
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Informal name for this concept map
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Informal name for this concept map
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
        /// Date for given status
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// Date for given status
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        Hl7.Fhir.Model.FhirString PublisherElement { get; set; }
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Publisher { get; set; }
    
        /// <summary>
        /// Identifies the source of the concepts which are being mapped
        /// </summary>
        Hl7.Fhir.Model.Element Source { get; set; }
    
        /// <summary>
        /// Provides context to the mappings
        /// </summary>
        Hl7.Fhir.Model.Element Target { get; set; }
    
    }
    
    public partial interface IConceptMapSourceElementComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Identifies element being mapped
        /// </summary>
        Hl7.Fhir.Model.Code CodeElement { get; set; }
        
        /// <summary>
        /// Identifies element being mapped
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Code { get; set; }
    
        /// <summary>
        /// Concept in target system for element
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IConceptMapTargetElementComponent> Target { get; }
    
    }
    
    public partial interface IConceptMapTargetElementComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Code that identifies the target element
        /// </summary>
        Hl7.Fhir.Model.Code CodeElement { get; set; }
        
        /// <summary>
        /// Code that identifies the target element
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Code { get; set; }
    
        /// <summary>
        /// Other elements required for this mapping (from context)
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IConceptMapOtherElementComponent> DependsOn { get; }
    
        /// <summary>
        /// Other concepts that this mapping also produces
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IConceptMapOtherElementComponent> Product { get; }
    
    }
    
    public partial interface IConceptMapOtherElementComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
    }

}
