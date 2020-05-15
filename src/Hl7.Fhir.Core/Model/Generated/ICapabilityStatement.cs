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
    /// A statement of system capabilities
    /// </summary>
    public partial interface ICapabilityStatement : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Canonical identifier for this capability statement, represented as a URI (globally unique)
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Canonical identifier for this capability statement, represented as a URI (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Business version of the capability statement
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Business version of the capability statement
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Name for this capability statement (computer friendly)
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Name for this capability statement (computer friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Name for this capability statement (human friendly)
        /// </summary>
        Hl7.Fhir.Model.FhirString TitleElement { get; set; }
        
        /// <summary>
        /// Name for this capability statement (human friendly)
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
        /// Natural language description of the capability statement
        /// </summary>
        Hl7.Fhir.Model.Markdown DescriptionElement { get; set; }
        
        /// <summary>
        /// Natural language description of the capability statement
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        List<Hl7.Fhir.Model.UsageContext> UseContext { get; set; }
    
        /// <summary>
        /// Intended jurisdiction for capability statement (if applicable)
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction { get; set; }
    
        /// <summary>
        /// Why this capability statement is defined
        /// </summary>
        Hl7.Fhir.Model.Markdown PurposeElement { get; set; }
        
        /// <summary>
        /// Why this capability statement is defined
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
        /// instance | capability | requirements
        /// </summary>
        Code<Hl7.Fhir.Model.CapabilityStatementKind> KindElement { get; set; }
        
        /// <summary>
        /// instance | capability | requirements
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.CapabilityStatementKind? Kind { get; set; }
    
        /// <summary>
        /// Software that is covered by this capability statement
        /// </summary>
        Hl7.Fhir.Model.ICapabilityStatementSoftwareComponent Software { get; }
    
        /// <summary>
        /// If this describes a specific instance
        /// </summary>
        Hl7.Fhir.Model.ICapabilityStatementImplementationComponent Implementation { get; }
    
        /// <summary>
        /// formats supported (xml | json | ttl | mime type)
        /// </summary>
        List<Hl7.Fhir.Model.Code> FormatElement { get; set; }
        
        /// <summary>
        /// formats supported (xml | json | ttl | mime type)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> Format { get; set; }
    
        /// <summary>
        /// Patch formats supported
        /// </summary>
        List<Hl7.Fhir.Model.Code> PatchFormatElement { get; set; }
        
        /// <summary>
        /// Patch formats supported
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> PatchFormat { get; set; }
    
        /// <summary>
        /// If the endpoint is a RESTful one
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementRestComponent> Rest { get; }
    
        /// <summary>
        /// If messaging is supported
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementMessagingComponent> Messaging { get; }
    
        /// <summary>
        /// Document definition
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementDocumentComponent> Document { get; }
    
    }
    
    public partial interface ICapabilityStatementSoftwareComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// A name the software is known by
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// A name the software is known by
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Version covered by this statement
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Version covered by this statement
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Date this version was released
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime ReleaseDateElement { get; set; }
        
        /// <summary>
        /// Date this version was released
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ReleaseDate { get; set; }
    
    }
    
    public partial interface ICapabilityStatementImplementationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Describes this specific instance
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Describes this specific instance
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
    }
    
    public partial interface ICapabilityStatementRestComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// client | server
        /// </summary>
        Code<Hl7.Fhir.Model.RestfulCapabilityMode> ModeElement { get; set; }
        
        /// <summary>
        /// client | server
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.RestfulCapabilityMode? Mode { get; set; }
    
        /// <summary>
        /// Information about security of implementation
        /// </summary>
        Hl7.Fhir.Model.ICapabilityStatementSecurityComponent Security { get; }
    
        /// <summary>
        /// Resource served on the REST interface
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementResourceComponent> Resource { get; }
    
        /// <summary>
        /// What operations are supported?
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementSystemInteractionComponent> Interaction { get; }
    
        /// <summary>
        /// Search parameters for searching all resources
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementSearchParamComponent> SearchParam { get; }
    
        /// <summary>
        /// Definition of a system level operation
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementOperationComponent> Operation { get; }
    
    }
    
    public partial interface ICapabilityStatementSecurityComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Adds CORS Headers (http://enable-cors.org/)
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean CorsElement { get; set; }
        
        /// <summary>
        /// Adds CORS Headers (http://enable-cors.org/)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Cors { get; set; }
    
        /// <summary>
        /// OAuth | SMART-on-FHIR | NTLM | Basic | Kerberos | Certificates
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Service { get; set; }
    
    }
    
    public partial interface ICapabilityStatementResourceComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// A resource type that is supported
        /// </summary>
        Code<Hl7.Fhir.Model.ResourceType> TypeElement { get; set; }
        
        /// <summary>
        /// A resource type that is supported
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ResourceType? Type { get; set; }
    
        /// <summary>
        /// Additional information about the use of the resource type
        /// </summary>
        Hl7.Fhir.Model.Markdown DocumentationElement { get; set; }
        
        /// <summary>
        /// Additional information about the use of the resource type
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Documentation { get; set; }
    
        /// <summary>
        /// What operations are supported?
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementResourceInteractionComponent> Interaction { get; }
    
        /// <summary>
        /// no-version | versioned | versioned-update
        /// </summary>
        Code<Hl7.Fhir.Model.ResourceVersionPolicy> VersioningElement { get; set; }
        
        /// <summary>
        /// no-version | versioned | versioned-update
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ResourceVersionPolicy? Versioning { get; set; }
    
        /// <summary>
        /// Whether vRead can return past versions
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ReadHistoryElement { get; set; }
        
        /// <summary>
        /// Whether vRead can return past versions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? ReadHistory { get; set; }
    
        /// <summary>
        /// If update can commit to a new identity
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean UpdateCreateElement { get; set; }
        
        /// <summary>
        /// If update can commit to a new identity
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? UpdateCreate { get; set; }
    
        /// <summary>
        /// If allows/uses conditional create
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ConditionalCreateElement { get; set; }
        
        /// <summary>
        /// If allows/uses conditional create
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? ConditionalCreate { get; set; }
    
        /// <summary>
        /// not-supported | modified-since | not-match | full-support
        /// </summary>
        Code<Hl7.Fhir.Model.ConditionalReadStatus> ConditionalReadElement { get; set; }
        
        /// <summary>
        /// not-supported | modified-since | not-match | full-support
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ConditionalReadStatus? ConditionalRead { get; set; }
    
        /// <summary>
        /// If allows/uses conditional update
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ConditionalUpdateElement { get; set; }
        
        /// <summary>
        /// If allows/uses conditional update
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? ConditionalUpdate { get; set; }
    
        /// <summary>
        /// not-supported | single | multiple - how conditional delete is supported
        /// </summary>
        Code<Hl7.Fhir.Model.ConditionalDeleteStatus> ConditionalDeleteElement { get; set; }
        
        /// <summary>
        /// not-supported | single | multiple - how conditional delete is supported
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ConditionalDeleteStatus? ConditionalDelete { get; set; }
    
        /// <summary>
        /// literal | logical | resolves | enforced | local
        /// </summary>
        List<Code<Hl7.Fhir.Model.ReferenceHandlingPolicy>> ReferencePolicyElement { get; set; }
        
        /// <summary>
        /// literal | logical | resolves | enforced | local
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<Hl7.Fhir.Model.ReferenceHandlingPolicy?> ReferencePolicy { get; set; }
    
        /// <summary>
        /// _include values supported by the server
        /// </summary>
        List<Hl7.Fhir.Model.FhirString> SearchIncludeElement { get; set; }
        
        /// <summary>
        /// _include values supported by the server
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> SearchInclude { get; set; }
    
        /// <summary>
        /// _revinclude values supported by the server
        /// </summary>
        List<Hl7.Fhir.Model.FhirString> SearchRevIncludeElement { get; set; }
        
        /// <summary>
        /// _revinclude values supported by the server
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> SearchRevInclude { get; set; }
    
        /// <summary>
        /// Search parameters supported by implementation
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementSearchParamComponent> SearchParam { get; }
    
    }
    
    public partial interface ICapabilityStatementResourceInteractionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
    }
    
    public partial interface ICapabilityStatementSearchParamComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Name of search parameter
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Name of search parameter
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// number | date | string | token | reference | composite | quantity | uri | special
        /// </summary>
        Code<Hl7.Fhir.Model.SearchParamType> TypeElement { get; set; }
        
        /// <summary>
        /// number | date | string | token | reference | composite | quantity | uri | special
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.SearchParamType? Type { get; set; }
    
    }
    
    public partial interface ICapabilityStatementSystemInteractionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
    }
    
    public partial interface ICapabilityStatementOperationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Name by which the operation/query is invoked
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Name by which the operation/query is invoked
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
    }
    
    public partial interface ICapabilityStatementMessagingComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Where messages should be sent
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementEndpointComponent> Endpoint { get; }
    
        /// <summary>
        /// Reliable Message Cache Length (min)
        /// </summary>
        Hl7.Fhir.Model.UnsignedInt ReliableCacheElement { get; set; }
        
        /// <summary>
        /// Reliable Message Cache Length (min)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? ReliableCache { get; set; }
    
        /// <summary>
        /// Messages supported by this system
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICapabilityStatementSupportedMessageComponent> SupportedMessage { get; }
    
    }
    
    public partial interface ICapabilityStatementEndpointComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// http | ftp | mllp +
        /// </summary>
        Hl7.Fhir.Model.Coding Protocol { get; set; }
    
    }
    
    public partial interface ICapabilityStatementSupportedMessageComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// sender | receiver
        /// </summary>
        Code<Hl7.Fhir.Model.EventCapabilityMode> ModeElement { get; set; }
        
        /// <summary>
        /// sender | receiver
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.EventCapabilityMode? Mode { get; set; }
    
    }
    
    public partial interface ICapabilityStatementDocumentComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// producer | consumer
        /// </summary>
        Code<Hl7.Fhir.Model.DocumentMode> ModeElement { get; set; }
        
        /// <summary>
        /// producer | consumer
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.DocumentMode? Mode { get; set; }
    
    }

}
