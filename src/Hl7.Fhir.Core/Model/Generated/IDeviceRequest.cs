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
    /// Medical device request
    /// </summary>
    public partial interface IDeviceRequest : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// External Request identifier
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// What request fulfills
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> BasedOn { get; set; }
    
        /// <summary>
        /// What request replaces
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> PriorRequest { get; set; }
    
        /// <summary>
        /// Identifier of composite request
        /// </summary>
        Hl7.Fhir.Model.Identifier GroupIdentifier { get; set; }
    
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        Code<Hl7.Fhir.Model.RequestPriority> PriorityElement { get; set; }
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.RequestPriority? Priority { get; set; }
    
        /// <summary>
        /// Device requested
        /// </summary>
        Hl7.Fhir.Model.Element Code { get; set; }
    
        /// <summary>
        /// Focus of request
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// Desired time or schedule for use
        /// </summary>
        Hl7.Fhir.Model.Element Occurrence { get; set; }
    
        /// <summary>
        /// When recorded
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime AuthoredOnElement { get; set; }
        
        /// <summary>
        /// When recorded
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string AuthoredOn { get; set; }
    
        /// <summary>
        /// Filler role
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept PerformerType { get; set; }
    
        /// <summary>
        /// Requested Filler
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Performer { get; set; }
    
        /// <summary>
        /// Coded Reason for request
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> ReasonCode { get; set; }
    
        /// <summary>
        /// Linked Reason for request
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> ReasonReference { get; set; }
    
        /// <summary>
        /// Additional clinical information
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> SupportingInfo { get; set; }
    
        /// <summary>
        /// Notes or comments
        /// </summary>
        List<Hl7.Fhir.Model.Annotation> Note { get; set; }
    
        /// <summary>
        /// Request provenance
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> RelevantHistory { get; set; }
    
    }

}
