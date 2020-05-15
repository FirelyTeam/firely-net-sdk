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
    /// The technical details of an endpoint that can be used for electronic services
    /// </summary>
    public partial interface IEndpoint : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Identifies this endpoint across multiple systems
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// active | suspended | error | off | entered-in-error | test
        /// </summary>
        Code<Hl7.Fhir.Model.EndpointStatus> StatusElement { get; set; }
        
        /// <summary>
        /// active | suspended | error | off | entered-in-error | test
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.EndpointStatus? Status { get; set; }
    
        /// <summary>
        /// Protocol/Profile/Standard to be used with this endpoint connection
        /// </summary>
        Hl7.Fhir.Model.Coding ConnectionType { get; set; }
    
        /// <summary>
        /// A name that this endpoint can be identified by
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// A name that this endpoint can be identified by
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Organization that manages this endpoint (might not be the organization that exposes the endpoint)
        /// </summary>
        Hl7.Fhir.Model.ResourceReference ManagingOrganization { get; set; }
    
        /// <summary>
        /// Contact details for source (e.g. troubleshooting)
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Contact { get; }
    
        /// <summary>
        /// Interval the endpoint is expected to be operational
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// The type of content that may be used at this endpoint (e.g. XDS Discharge summaries)
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> PayloadType { get; set; }
    
        /// <summary>
        /// Mimetype to send. If not specified, the content could be anything (including no payload, if the connectionType defined this)
        /// </summary>
        List<Hl7.Fhir.Model.Code> PayloadMimeTypeElement { get; set; }
        
        /// <summary>
        /// Mimetype to send. If not specified, the content could be anything (including no payload, if the connectionType defined this)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> PayloadMimeType { get; set; }
    
        /// <summary>
        /// Usage depends on the channel type
        /// </summary>
        List<Hl7.Fhir.Model.FhirString> HeaderElement { get; set; }
        
        /// <summary>
        /// Usage depends on the channel type
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> Header { get; set; }
    
    }

}
