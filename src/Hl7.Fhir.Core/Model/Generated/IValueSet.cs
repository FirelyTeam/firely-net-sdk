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
    /// A set of codes drawn from one or more code systems
    /// </summary>
    public partial interface IValueSet : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Globally unique logical identifier for  value set
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Globally unique logical identifier for  value set
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Logical identifier for this version of the value set
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Logical identifier for this version of the value set
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Informal name for this value set
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Informal name for this value set
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
        /// Indicates whether or not any change to the content logical definition may occur
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ImmutableElement { get; set; }
        
        /// <summary>
        /// Indicates whether or not any change to the content logical definition may occur
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Immutable { get; set; }
    
        /// <summary>
        /// When value set includes codes from elsewhere
        /// </summary>
        Hl7.Fhir.Model.IValueSetComposeComponent Compose { get; }
    
        /// <summary>
        /// Used when the value set is "expanded"
        /// </summary>
        Hl7.Fhir.Model.IValueSetExpansionComponent Expansion { get; }
    
    }
    
    public partial interface IValueSetComposeComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Include one or more codes from a code system
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IValueSetConceptSetComponent> Include { get; }
    
        /// <summary>
        /// Explicitly exclude codes
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IValueSetConceptSetComponent> Exclude { get; }
    
    }
    
    public partial interface IValueSetConceptSetComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The system the codes come from
        /// </summary>
        Hl7.Fhir.Model.FhirUri SystemElement { get; set; }
        
        /// <summary>
        /// The system the codes come from
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string System { get; set; }
    
        /// <summary>
        /// Specific version of the code system referred to
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Specific version of the code system referred to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// A concept defined in the system
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IValueSetConceptReferenceComponent> Concept { get; }
    
        /// <summary>
        /// Select codes/concepts by their properties (including relationships)
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IValueSetFilterComponent> Filter { get; }
    
    }
    
    public partial interface IValueSetConceptReferenceComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Code or expression from system
        /// </summary>
        Hl7.Fhir.Model.Code CodeElement { get; set; }
        
        /// <summary>
        /// Code or expression from system
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Code { get; set; }
    
        /// <summary>
        /// Test to display for this code for this value set
        /// </summary>
        Hl7.Fhir.Model.FhirString DisplayElement { get; set; }
        
        /// <summary>
        /// Test to display for this code for this value set
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Display { get; set; }
    
        /// <summary>
        /// Additional representations for this valueset
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IValueSetDesignationComponent> Designation { get; }
    
    }
    
    public partial interface IValueSetDesignationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Human language of the designation
        /// </summary>
        Hl7.Fhir.Model.Code LanguageElement { get; set; }
        
        /// <summary>
        /// Human language of the designation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Language { get; set; }
    
        /// <summary>
        /// Details how this designation would be used
        /// </summary>
        Hl7.Fhir.Model.Coding Use { get; set; }
    
        /// <summary>
        /// The text value for this designation
        /// </summary>
        Hl7.Fhir.Model.FhirString ValueElement { get; set; }
        
        /// <summary>
        /// The text value for this designation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Value { get; set; }
    
    }
    
    public partial interface IValueSetFilterComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// A property defined by the code system
        /// </summary>
        Hl7.Fhir.Model.Code PropertyElement { get; set; }
        
        /// <summary>
        /// A property defined by the code system
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Property { get; set; }
    
    }
    
    public partial interface IValueSetExpansionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Uniquely identifies this expansion
        /// </summary>
        Hl7.Fhir.Model.FhirUri IdentifierElement { get; set; }
        
        /// <summary>
        /// Uniquely identifies this expansion
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Identifier { get; set; }
    
        /// <summary>
        /// Time ValueSet expansion happened
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime TimestampElement { get; set; }
        
        /// <summary>
        /// Time ValueSet expansion happened
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Timestamp { get; set; }
    
        /// <summary>
        /// Total number of codes in the expansion
        /// </summary>
        Hl7.Fhir.Model.Integer TotalElement { get; set; }
        
        /// <summary>
        /// Total number of codes in the expansion
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Total { get; set; }
    
        /// <summary>
        /// Offset at which this resource starts
        /// </summary>
        Hl7.Fhir.Model.Integer OffsetElement { get; set; }
        
        /// <summary>
        /// Offset at which this resource starts
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Offset { get; set; }
    
        /// <summary>
        /// Parameter that controlled the expansion process
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IValueSetParameterComponent> Parameter { get; }
    
        /// <summary>
        /// Codes in the value set
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IValueSetContainsComponent> Contains { get; }
    
    }
    
    public partial interface IValueSetParameterComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Name as assigned by the server
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Name as assigned by the server
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Value of the named parameter
        /// </summary>
        Hl7.Fhir.Model.Element Value { get; set; }
    
    }
    
    public partial interface IValueSetContainsComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// System value for the code
        /// </summary>
        Hl7.Fhir.Model.FhirUri SystemElement { get; set; }
        
        /// <summary>
        /// System value for the code
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string System { get; set; }
    
        /// <summary>
        /// If user cannot select this entry
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean AbstractElement { get; set; }
        
        /// <summary>
        /// If user cannot select this entry
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Abstract { get; set; }
    
        /// <summary>
        /// Version in which this code/display is defined
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Version in which this code/display is defined
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Code - if blank, this is not a selectable code
        /// </summary>
        Hl7.Fhir.Model.Code CodeElement { get; set; }
        
        /// <summary>
        /// Code - if blank, this is not a selectable code
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Code { get; set; }
    
        /// <summary>
        /// User display for the concept
        /// </summary>
        Hl7.Fhir.Model.FhirString DisplayElement { get; set; }
        
        /// <summary>
        /// User display for the concept
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Display { get; set; }
    
        /// <summary>
        /// Codes contained under this entry
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IValueSetContainsComponent> Contains { get; }
    
    }

}
