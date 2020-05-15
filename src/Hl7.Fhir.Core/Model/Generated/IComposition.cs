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
    /// A set of resources composed into a single coherent clinical statement with clinical attestation
    /// </summary>
    public partial interface IComposition : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Logical identifier of composition (version-independent)
        /// </summary>
        Hl7.Fhir.Model.Identifier Identifier { get; set; }
    
        /// <summary>
        /// preliminary | final | amended | entered-in-error
        /// </summary>
        Code<Hl7.Fhir.Model.CompositionStatus> StatusElement { get; set; }
        
        /// <summary>
        /// preliminary | final | amended | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.CompositionStatus? Status { get; set; }
    
        /// <summary>
        /// Kind of composition (LOINC if possible)
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Who and/or what the composition is about
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// Context of the Composition
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Encounter { get; set; }
    
        /// <summary>
        /// Composition editing time
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// Composition editing time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// Who and/or what authored the composition
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Author { get; set; }
    
        /// <summary>
        /// Human Readable name/title
        /// </summary>
        Hl7.Fhir.Model.FhirString TitleElement { get; set; }
        
        /// <summary>
        /// Human Readable name/title
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Title { get; set; }
    
        /// <summary>
        /// Attests to accuracy of composition
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICompositionAttesterComponent> Attester { get; }
    
        /// <summary>
        /// Organization which maintains the composition
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Custodian { get; set; }
    
        /// <summary>
        /// The clinical service(s) being documented
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICompositionEventComponent> Event { get; }
    
        /// <summary>
        /// Composition is broken into sections
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICompositionSectionComponent> Section { get; }
    
    }
    
    public partial interface ICompositionAttesterComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// When composition attested
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime TimeElement { get; set; }
        
        /// <summary>
        /// When composition attested
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Time { get; set; }
    
        /// <summary>
        /// Who attested the composition
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Party { get; set; }
    
    }
    
    public partial interface ICompositionEventComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Code(s) that apply to the event being documented
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Code { get; set; }
    
        /// <summary>
        /// The period covered by the documentation
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// The event(s) being documented
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Detail { get; set; }
    
    }
    
    public partial interface ICompositionSectionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Label for section (e.g. for ToC)
        /// </summary>
        Hl7.Fhir.Model.FhirString TitleElement { get; set; }
        
        /// <summary>
        /// Label for section (e.g. for ToC)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Title { get; set; }
    
        /// <summary>
        /// Classification of section (recommended)
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Text summary of the section, for human interpretation
        /// </summary>
        Hl7.Fhir.Model.Narrative Text { get; set; }
    
        /// <summary>
        /// working | snapshot | changes
        /// </summary>
        Code<Hl7.Fhir.Model.ListMode> ModeElement { get; set; }
        
        /// <summary>
        /// working | snapshot | changes
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ListMode? Mode { get; set; }
    
        /// <summary>
        /// Order of section entries
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept OrderedBy { get; set; }
    
        /// <summary>
        /// A reference to data that supports this section
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Entry { get; set; }
    
        /// <summary>
        /// Why the section is empty
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept EmptyReason { get; set; }
    
        /// <summary>
        /// Nested Section
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICompositionSectionComponent> Section { get; }
    
    }

}
