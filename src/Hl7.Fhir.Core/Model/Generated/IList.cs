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
    /// Information summarized from a list of other resources
    /// </summary>
    public partial interface IList : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Business identifier
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// current | retired | entered-in-error
        /// </summary>
        Code<Hl7.Fhir.Model.ListStatus> StatusElement { get; set; }
        
        /// <summary>
        /// current | retired | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ListStatus? Status { get; set; }
    
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
        /// Descriptive name for the list
        /// </summary>
        Hl7.Fhir.Model.FhirString TitleElement { get; set; }
        
        /// <summary>
        /// Descriptive name for the list
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Title { get; set; }
    
        /// <summary>
        /// What the purpose of this list is
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// If all resources have the same subject
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// Context in which list created
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Encounter { get; set; }
    
        /// <summary>
        /// When the list was prepared
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// When the list was prepared
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// Who and/or what defined the list contents (aka Author)
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Source { get; set; }
    
        /// <summary>
        /// What order the list has
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept OrderedBy { get; set; }
    
        /// <summary>
        /// Entries in the list
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IListEntryComponent> Entry { get; }
    
        /// <summary>
        /// Why list is empty
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept EmptyReason { get; set; }
    
    }
    
    public partial interface IListEntryComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Status/Workflow information about this item
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Flag { get; set; }
    
        /// <summary>
        /// If this item is actually marked as deleted
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean DeletedElement { get; set; }
        
        /// <summary>
        /// If this item is actually marked as deleted
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Deleted { get; set; }
    
        /// <summary>
        /// When item added to list
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// When item added to list
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// Actual entry
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Item { get; set; }
    
    }

}
