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
    /// Declares the existence of and describes a code system or code system supplement
    /// </summary>
    public partial interface ICodeSystem : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Canonical identifier for this code system, represented as a URI (globally unique) (Coding.system)
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Canonical identifier for this code system, represented as a URI (globally unique) (Coding.system)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Business version of the code system (Coding.version)
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Business version of the code system (Coding.version)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Name for this code system (computer friendly)
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Name for this code system (computer friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Name for this code system (human friendly)
        /// </summary>
        Hl7.Fhir.Model.FhirString TitleElement { get; set; }
        
        /// <summary>
        /// Name for this code system (human friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Title { get; set; }
    
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        Code<Hl7.Fhir.Model.PublicationStatus> StatusElement { get; set; }
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.PublicationStatus? Status { get; set; }
    
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ExperimentalElement { get; set; }
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Experimental { get; set; }
    
        /// <summary>
        /// Date last changed
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// Date last changed
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
        /// Contact details for the publisher
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContactDetail> Contact { get; }
    
        /// <summary>
        /// Natural language description of the code system
        /// </summary>
        Hl7.Fhir.Model.Markdown DescriptionElement { get; set; }
        
        /// <summary>
        /// Natural language description of the code system
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        List<Hl7.Fhir.Model.UsageContext> UseContext { get; set; }
    
        /// <summary>
        /// Intended jurisdiction for code system (if applicable)
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction { get; set; }
    
        /// <summary>
        /// Why this code system is defined
        /// </summary>
        Hl7.Fhir.Model.Markdown PurposeElement { get; set; }
        
        /// <summary>
        /// Why this code system is defined
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Purpose { get; set; }
    
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        Hl7.Fhir.Model.Markdown CopyrightElement { get; set; }
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Copyright { get; set; }
    
        /// <summary>
        /// If code comparison is case sensitive
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean CaseSensitiveElement { get; set; }
        
        /// <summary>
        /// If code comparison is case sensitive
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? CaseSensitive { get; set; }
    
        /// <summary>
        /// grouped-by | is-a | part-of | classified-with
        /// </summary>
        Code<Hl7.Fhir.Model.CodeSystemHierarchyMeaning> HierarchyMeaningElement { get; set; }
        
        /// <summary>
        /// grouped-by | is-a | part-of | classified-with
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.CodeSystemHierarchyMeaning? HierarchyMeaning { get; set; }
    
        /// <summary>
        /// If code system defines a compositional grammar
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean CompositionalElement { get; set; }
        
        /// <summary>
        /// If code system defines a compositional grammar
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Compositional { get; set; }
    
        /// <summary>
        /// If definitions are not stable
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean VersionNeededElement { get; set; }
        
        /// <summary>
        /// If definitions are not stable
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? VersionNeeded { get; set; }
    
        /// <summary>
        /// Total concepts in the code system
        /// </summary>
        Hl7.Fhir.Model.UnsignedInt CountElement { get; set; }
        
        /// <summary>
        /// Total concepts in the code system
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Count { get; set; }
    
        /// <summary>
        /// Filter that can be used in a value set
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICodeSystemFilterComponent> Filter { get; }
    
        /// <summary>
        /// Additional information supplied about each concept
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICodeSystemPropertyComponent> Property { get; }
    
        /// <summary>
        /// Concepts in the code system
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICodeSystemConceptDefinitionComponent> Concept { get; }
    
    }
    
    public partial interface ICodeSystemFilterComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Code that identifies the filter
        /// </summary>
        Hl7.Fhir.Model.Code CodeElement { get; set; }
        
        /// <summary>
        /// Code that identifies the filter
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Code { get; set; }
    
        /// <summary>
        /// How or why the filter is used
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// How or why the filter is used
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// What to use for the value
        /// </summary>
        Hl7.Fhir.Model.FhirString ValueElement { get; set; }
        
        /// <summary>
        /// What to use for the value
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Value { get; set; }
    
    }
    
    public partial interface ICodeSystemPropertyComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Identifies the property on the concepts, and when referred to in operations
        /// </summary>
        Hl7.Fhir.Model.Code CodeElement { get; set; }
        
        /// <summary>
        /// Identifies the property on the concepts, and when referred to in operations
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Code { get; set; }
    
        /// <summary>
        /// Formal identifier for the property
        /// </summary>
        Hl7.Fhir.Model.FhirUri UriElement { get; set; }
        
        /// <summary>
        /// Formal identifier for the property
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Uri { get; set; }
    
        /// <summary>
        /// Why the property is defined, and/or what it conveys
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Why the property is defined, and/or what it conveys
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
    }
    
    public partial interface ICodeSystemConceptDefinitionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Code that identifies concept
        /// </summary>
        Hl7.Fhir.Model.Code CodeElement { get; set; }
        
        /// <summary>
        /// Code that identifies concept
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Code { get; set; }
    
        /// <summary>
        /// Text to display to the user
        /// </summary>
        Hl7.Fhir.Model.FhirString DisplayElement { get; set; }
        
        /// <summary>
        /// Text to display to the user
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Display { get; set; }
    
        /// <summary>
        /// Formal definition
        /// </summary>
        Hl7.Fhir.Model.FhirString DefinitionElement { get; set; }
        
        /// <summary>
        /// Formal definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Definition { get; set; }
    
        /// <summary>
        /// Additional representations for the concept
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICodeSystemDesignationComponent> Designation { get; }
    
        /// <summary>
        /// Property value for the concept
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICodeSystemConceptPropertyComponent> Property { get; }
    
        /// <summary>
        /// Child Concepts (is-a/contains/categorizes)
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICodeSystemConceptDefinitionComponent> Concept { get; }
    
    }
    
    public partial interface ICodeSystemDesignationComponent : Hl7.Fhir.Model.IBackboneElement
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
    
    public partial interface ICodeSystemConceptPropertyComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Reference to CodeSystem.property.code
        /// </summary>
        Hl7.Fhir.Model.Code CodeElement { get; set; }
        
        /// <summary>
        /// Reference to CodeSystem.property.code
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Code { get; set; }
    
        /// <summary>
        /// Value of the property for this concept
        /// </summary>
        Hl7.Fhir.Model.Element Value { get; set; }
    
    }

}
