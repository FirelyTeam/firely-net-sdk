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
// Generated for FHIR v1.0.2, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Process request
    /// </summary>
    public partial interface IProcessRequest : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Business Identifier
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// cancel | poll | reprocess | status
        /// </summary>
        Code<Hl7.Fhir.Model.ActionList> ActionElement { get; set; }
        
        /// <summary>
        /// cancel | poll | reprocess | status
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ActionList? Action { get; set; }
    
        /// <summary>
        /// Target of the request
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Target { get; set; }
    
        /// <summary>
        /// Creation date
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime CreatedElement { get; set; }
        
        /// <summary>
        /// Creation date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Created { get; set; }
    
        /// <summary>
        /// Responsible practitioner
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Provider { get; set; }
    
        /// <summary>
        /// Responsible organization
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Organization { get; set; }
    
        /// <summary>
        /// Request reference
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Request { get; set; }
    
        /// <summary>
        /// Response reference
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Response { get; set; }
    
        /// <summary>
        /// Nullify
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean NullifyElement { get; set; }
        
        /// <summary>
        /// Nullify
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Nullify { get; set; }
    
        /// <summary>
        /// Reference number/string
        /// </summary>
        Hl7.Fhir.Model.FhirString ReferenceElement { get; set; }
        
        /// <summary>
        /// Reference number/string
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Reference { get; set; }
    
        /// <summary>
        /// Items to re-adjudicate
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IProcessRequestItemsComponent> Item { get; }
    
        /// <summary>
        /// Resource type(s) to include
        /// </summary>
        List<Hl7.Fhir.Model.FhirString> IncludeElement { get; set; }
        
        /// <summary>
        /// Resource type(s) to include
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> Include { get; set; }
    
        /// <summary>
        /// Resource type(s) to exclude
        /// </summary>
        List<Hl7.Fhir.Model.FhirString> ExcludeElement { get; set; }
        
        /// <summary>
        /// Resource type(s) to exclude
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> Exclude { get; set; }
    
        /// <summary>
        /// Period
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
    }
    
    public partial interface IProcessRequestItemsComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Service instance
        /// </summary>
        Hl7.Fhir.Model.Integer SequenceLinkIdElement { get; set; }
        
        /// <summary>
        /// Service instance
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? SequenceLinkId { get; set; }
    
    }

}
