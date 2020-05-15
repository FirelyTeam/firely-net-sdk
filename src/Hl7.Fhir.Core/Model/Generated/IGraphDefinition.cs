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
    /// Definition of a graph of resources
    /// </summary>
    public partial interface IGraphDefinition : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Canonical identifier for this graph definition, represented as a URI (globally unique)
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Canonical identifier for this graph definition, represented as a URI (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Business version of the graph definition
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Business version of the graph definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Name for this graph definition (computer friendly)
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Name for this graph definition (computer friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
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
        /// Natural language description of the graph definition
        /// </summary>
        Hl7.Fhir.Model.Markdown DescriptionElement { get; set; }
        
        /// <summary>
        /// Natural language description of the graph definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        List<Hl7.Fhir.Model.UsageContext> UseContext { get; set; }
    
        /// <summary>
        /// Intended jurisdiction for graph definition (if applicable)
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction { get; set; }
    
        /// <summary>
        /// Why this graph definition is defined
        /// </summary>
        Hl7.Fhir.Model.Markdown PurposeElement { get; set; }
        
        /// <summary>
        /// Why this graph definition is defined
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Purpose { get; set; }
    
        /// <summary>
        /// Type of resource at which the graph starts
        /// </summary>
        Code<Hl7.Fhir.Model.ResourceType> StartElement { get; set; }
        
        /// <summary>
        /// Type of resource at which the graph starts
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ResourceType? Start { get; set; }
    
        /// <summary>
        /// Links this graph makes rules about
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IGraphDefinitionLinkComponent> Link { get; }
    
    }
    
    public partial interface IGraphDefinitionLinkComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Path in the resource that contains the link
        /// </summary>
        Hl7.Fhir.Model.FhirString PathElement { get; set; }
        
        /// <summary>
        /// Path in the resource that contains the link
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Path { get; set; }
    
        /// <summary>
        /// Which slice (if profiled)
        /// </summary>
        Hl7.Fhir.Model.FhirString SliceNameElement { get; set; }
        
        /// <summary>
        /// Which slice (if profiled)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string SliceName { get; set; }
    
        /// <summary>
        /// Minimum occurrences for this link
        /// </summary>
        Hl7.Fhir.Model.Integer MinElement { get; set; }
        
        /// <summary>
        /// Minimum occurrences for this link
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Min { get; set; }
    
        /// <summary>
        /// Maximum occurrences for this link
        /// </summary>
        Hl7.Fhir.Model.FhirString MaxElement { get; set; }
        
        /// <summary>
        /// Maximum occurrences for this link
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Max { get; set; }
    
        /// <summary>
        /// Why this link is specified
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Why this link is specified
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Potential target for the link
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IGraphDefinitionTargetComponent> Target { get; }
    
    }
    
    public partial interface IGraphDefinitionTargetComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Type of resource this link refers to
        /// </summary>
        Code<Hl7.Fhir.Model.ResourceType> TypeElement { get; set; }
        
        /// <summary>
        /// Type of resource this link refers to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ResourceType? Type { get; set; }
    
        /// <summary>
        /// Compartment Consistency Rules
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IGraphDefinitionCompartmentComponent> Compartment { get; }
    
        /// <summary>
        /// Additional links from target resource
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IGraphDefinitionLinkComponent> Link { get; }
    
    }
    
    public partial interface IGraphDefinitionCompartmentComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Patient | Encounter | RelatedPerson | Practitioner | Device
        /// </summary>
        Code<Hl7.Fhir.Model.CompartmentType> CodeElement { get; set; }
        
        /// <summary>
        /// Patient | Encounter | RelatedPerson | Practitioner | Device
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.CompartmentType? Code { get; set; }
    
        /// <summary>
        /// identical | matching | different | custom
        /// </summary>
        Code<Hl7.Fhir.Model.GraphCompartmentRule> RuleElement { get; set; }
        
        /// <summary>
        /// identical | matching | different | custom
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.GraphCompartmentRule? Rule { get; set; }
    
        /// <summary>
        /// Custom rule, as a FHIRPath expression
        /// </summary>
        Hl7.Fhir.Model.FhirString ExpressionElement { get; set; }
        
        /// <summary>
        /// Custom rule, as a FHIRPath expression
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Expression { get; set; }
    
        /// <summary>
        /// Documentation for FHIRPath expression
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Documentation for FHIRPath expression
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
    }

}
