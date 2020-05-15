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
    /// Contains a collection of resources
    /// </summary>
    public partial interface IBundle : Hl7.Fhir.Model.IResource
    {
    
        /// <summary>
        /// document | message | transaction | transaction-response | batch | batch-response | history | searchset | collection
        /// </summary>
        Code<Hl7.Fhir.Model.BundleType> TypeElement { get; set; }
        
        /// <summary>
        /// document | message | transaction | transaction-response | batch | batch-response | history | searchset | collection
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.BundleType? Type { get; set; }
    
        /// <summary>
        /// If search, the total number of matches
        /// </summary>
        Hl7.Fhir.Model.UnsignedInt TotalElement { get; set; }
        
        /// <summary>
        /// If search, the total number of matches
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Total { get; set; }
    
        /// <summary>
        /// Links related to this Bundle
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IBundleLinkComponent> Link { get; }
    
        /// <summary>
        /// Entry in the bundle - will have a resource, or information
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IBundleEntryComponent> Entry { get; }
    
        /// <summary>
        /// Digital Signature
        /// </summary>
        Hl7.Fhir.Model.ISignature Signature { get; }
    
    }
    
    public partial interface IBundleLinkComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// http://www.iana.org/assignments/link-relations/link-relations.xhtml
        /// </summary>
        Hl7.Fhir.Model.FhirString RelationElement { get; set; }
        
        /// <summary>
        /// http://www.iana.org/assignments/link-relations/link-relations.xhtml
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Relation { get; set; }
    
        /// <summary>
        /// Reference details for the link
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Reference details for the link
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
    }
    
    public partial interface IBundleEntryComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Links related to this entry
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IBundleLinkComponent> Link { get; }
    
        /// <summary>
        /// Absolute URL for resource (server address, or UUID/OID)
        /// </summary>
        Hl7.Fhir.Model.FhirUri FullUrlElement { get; set; }
        
        /// <summary>
        /// Absolute URL for resource (server address, or UUID/OID)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string FullUrl { get; set; }
    
        /// <summary>
        /// A resource in the bundle
        /// </summary>
        Hl7.Fhir.Model.Resource Resource { get; set; }
    
        /// <summary>
        /// Search related information
        /// </summary>
        Hl7.Fhir.Model.IBundleSearchComponent Search { get; }
    
        /// <summary>
        /// Transaction Related Information
        /// </summary>
        Hl7.Fhir.Model.IBundleRequestComponent Request { get; }
    
        /// <summary>
        /// Transaction Related Information
        /// </summary>
        Hl7.Fhir.Model.IBundleResponseComponent Response { get; }
    
    }
    
    public partial interface IBundleSearchComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// match | include | outcome - why this is in the result set
        /// </summary>
        Code<Hl7.Fhir.Model.SearchEntryMode> ModeElement { get; set; }
        
        /// <summary>
        /// match | include | outcome - why this is in the result set
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.SearchEntryMode? Mode { get; set; }
    
        /// <summary>
        /// Search ranking (between 0 and 1)
        /// </summary>
        Hl7.Fhir.Model.FhirDecimal ScoreElement { get; set; }
        
        /// <summary>
        /// Search ranking (between 0 and 1)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        decimal? Score { get; set; }
    
    }
    
    public partial interface IBundleRequestComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// GET | POST | PUT | DELETE
        /// </summary>
        Code<Hl7.Fhir.Model.HTTPVerb> MethodElement { get; set; }
        
        /// <summary>
        /// GET | POST | PUT | DELETE
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.HTTPVerb? Method { get; set; }
    
        /// <summary>
        /// URL for HTTP equivalent of this entry
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// URL for HTTP equivalent of this entry
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// For managing cache currency
        /// </summary>
        Hl7.Fhir.Model.FhirString IfNoneMatchElement { get; set; }
        
        /// <summary>
        /// For managing cache currency
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string IfNoneMatch { get; set; }
    
        /// <summary>
        /// For managing update contention
        /// </summary>
        Hl7.Fhir.Model.Instant IfModifiedSinceElement { get; set; }
        
        /// <summary>
        /// For managing update contention
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        DateTimeOffset? IfModifiedSince { get; set; }
    
        /// <summary>
        /// For managing update contention
        /// </summary>
        Hl7.Fhir.Model.FhirString IfMatchElement { get; set; }
        
        /// <summary>
        /// For managing update contention
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string IfMatch { get; set; }
    
        /// <summary>
        /// For conditional creates
        /// </summary>
        Hl7.Fhir.Model.FhirString IfNoneExistElement { get; set; }
        
        /// <summary>
        /// For conditional creates
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string IfNoneExist { get; set; }
    
    }
    
    public partial interface IBundleResponseComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Status return code for entry
        /// </summary>
        Hl7.Fhir.Model.FhirString StatusElement { get; set; }
        
        /// <summary>
        /// Status return code for entry
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Status { get; set; }
    
        /// <summary>
        /// The location, if the operation returns a location
        /// </summary>
        Hl7.Fhir.Model.FhirUri LocationElement { get; set; }
        
        /// <summary>
        /// The location, if the operation returns a location
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Location { get; set; }
    
        /// <summary>
        /// The etag for the resource (if relevant)
        /// </summary>
        Hl7.Fhir.Model.FhirString EtagElement { get; set; }
        
        /// <summary>
        /// The etag for the resource (if relevant)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Etag { get; set; }
    
        /// <summary>
        /// Server's date time modified
        /// </summary>
        Hl7.Fhir.Model.Instant LastModifiedElement { get; set; }
        
        /// <summary>
        /// Server's date time modified
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        DateTimeOffset? LastModified { get; set; }
    
    }

}
