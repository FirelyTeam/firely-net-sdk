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
    /// A manifest that defines a set of documents
    /// </summary>
    public partial interface IDocumentManifest : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Unique Identifier for the set of documents
        /// </summary>
        Hl7.Fhir.Model.Identifier MasterIdentifier { get; set; }
    
        /// <summary>
        /// Other identifiers for the manifest
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// current | superseded | entered-in-error
        /// </summary>
        Code<Hl7.Fhir.Model.DocumentReferenceStatus> StatusElement { get; set; }
        
        /// <summary>
        /// current | superseded | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.DocumentReferenceStatus? Status { get; set; }
    
        /// <summary>
        /// Kind of document set
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// The subject of the set of documents
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// When this document manifest created
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime CreatedElement { get; set; }
        
        /// <summary>
        /// When this document manifest created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Created { get; set; }
    
        /// <summary>
        /// Who and/or what authored the manifest
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Author { get; set; }
    
        /// <summary>
        /// Intended to get notified about this set of documents
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Recipient { get; set; }
    
        /// <summary>
        /// The source system/application/software
        /// </summary>
        Hl7.Fhir.Model.FhirUri SourceElement { get; set; }
        
        /// <summary>
        /// The source system/application/software
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Source { get; set; }
    
        /// <summary>
        /// Human-readable description (title)
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Human-readable description (title)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Related things
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IDocumentManifestRelatedComponent> Related { get; }
    
    }
    
    public partial interface IDocumentManifestRelatedComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Identifiers of things that are related
        /// </summary>
        Hl7.Fhir.Model.Identifier Identifier { get; set; }
    
        /// <summary>
        /// Related Resource
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Ref { get; set; }
    
    }

}
