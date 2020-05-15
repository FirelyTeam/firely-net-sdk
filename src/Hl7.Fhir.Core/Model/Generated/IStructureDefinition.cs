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
    /// Structural Definition
    /// </summary>
    public partial interface IStructureDefinition : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Absolute URL used to reference this StructureDefinition
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Absolute URL used to reference this StructureDefinition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Other identifiers for the StructureDefinition
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Logical id for this version of the StructureDefinition
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Logical id for this version of the StructureDefinition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Informal name for this StructureDefinition
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Informal name for this StructureDefinition
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
        /// Date for this version of the StructureDefinition
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// Date for this version of the StructureDefinition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        Hl7.Fhir.Model.FhirString PublisherElement { get; set; }
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Publisher { get; set; }
    
        /// <summary>
        /// External specification that the content is mapped to
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IStructureDefinitionMappingComponent> Mapping { get; }
    
        /// <summary>
        /// Whether the structure is abstract
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean AbstractElement { get; set; }
        
        /// <summary>
        /// Whether the structure is abstract
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Abstract { get; set; }
    
        /// <summary>
        /// Snapshot view of the structure
        /// </summary>
        Hl7.Fhir.Model.IStructureDefinitionSnapshotComponent Snapshot { get; }
    
        /// <summary>
        /// Differential view of the structure
        /// </summary>
        Hl7.Fhir.Model.IStructureDefinitionDifferentialComponent Differential { get; }
    
    }
    
    public partial interface IStructureDefinitionMappingComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Internal id when this mapping is used
        /// </summary>
        Hl7.Fhir.Model.Id IdentityElement { get; set; }
        
        /// <summary>
        /// Internal id when this mapping is used
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Identity { get; set; }
    
        /// <summary>
        /// Identifies what this mapping refers to
        /// </summary>
        Hl7.Fhir.Model.FhirUri UriElement { get; set; }
        
        /// <summary>
        /// Identifies what this mapping refers to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Uri { get; set; }
    
        /// <summary>
        /// Names what this mapping refers to
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Names what this mapping refers to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
    }
    
    public partial interface IStructureDefinitionSnapshotComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Definition of elements in the resource (if no StructureDefinition)
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IElementDefinition> Element { get; }
    
    }
    
    public partial interface IStructureDefinitionDifferentialComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Definition of elements in the resource (if no StructureDefinition)
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IElementDefinition> Element { get; }
    
    }

}
