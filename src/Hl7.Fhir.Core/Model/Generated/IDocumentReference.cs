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
    /// A reference to a document
    /// </summary>
    public partial interface IDocumentReference : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Master Version Specific Identifier
        /// </summary>
        Hl7.Fhir.Model.Identifier MasterIdentifier { get; set; }
    
        /// <summary>
        /// Other identifiers for the document
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
        /// Kind of document (LOINC if possible)
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Who/what is the subject of the document
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// Who and/or what authored the document
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Author { get; set; }
    
        /// <summary>
        /// Who/what authenticated the document
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Authenticator { get; set; }
    
        /// <summary>
        /// Organization which maintains the document
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Custodian { get; set; }
    
        /// <summary>
        /// Relationships to other documents
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IDocumentReferenceRelatesToComponent> RelatesTo { get; }
    
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
        /// Document security-tags
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> SecurityLabel { get; set; }
    
        /// <summary>
        /// Document referenced
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IDocumentReferenceContentComponent> Content { get; }
    
        /// <summary>
        /// Clinical context of document
        /// </summary>
        Hl7.Fhir.Model.IDocumentReferenceContextComponent Context { get; }
    
    }
    
    public partial interface IDocumentReferenceRelatesToComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// replaces | transforms | signs | appends
        /// </summary>
        Code<Hl7.Fhir.Model.DocumentRelationshipType> CodeElement { get; set; }
        
        /// <summary>
        /// replaces | transforms | signs | appends
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.DocumentRelationshipType? Code { get; set; }
    
        /// <summary>
        /// Target of the relationship
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Target { get; set; }
    
    }
    
    public partial interface IDocumentReferenceContentComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Where to access the document
        /// </summary>
        Hl7.Fhir.Model.Attachment Attachment { get; set; }
    
    }
    
    public partial interface IDocumentReferenceContextComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Main Clinical Acts Documented
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Event { get; set; }
    
        /// <summary>
        /// Time of service that is being documented
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// Kind of facility where patient was seen
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept FacilityType { get; set; }
    
        /// <summary>
        /// Additional details about where the content was created (e.g. clinical specialty)
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept PracticeSetting { get; set; }
    
        /// <summary>
        /// Patient demographics from source
        /// </summary>
        Hl7.Fhir.Model.ResourceReference SourcePatientInfo { get; set; }
    
    }

}
