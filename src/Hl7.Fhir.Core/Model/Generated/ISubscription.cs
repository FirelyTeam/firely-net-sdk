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
    /// A server push subscription criteria
    /// </summary>
    public partial interface ISubscription : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// requested | active | error | off
        /// </summary>
        Code<Hl7.Fhir.Model.SubscriptionStatus> StatusElement { get; set; }
        
        /// <summary>
        /// requested | active | error | off
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.SubscriptionStatus? Status { get; set; }
    
        /// <summary>
        /// Contact details for source (e.g. troubleshooting)
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Contact { get; }
    
        /// <summary>
        /// When to automatically delete the subscription
        /// </summary>
        Hl7.Fhir.Model.Instant EndElement { get; set; }
        
        /// <summary>
        /// When to automatically delete the subscription
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        DateTimeOffset? End { get; set; }
    
        /// <summary>
        /// Description of why this subscription was created
        /// </summary>
        Hl7.Fhir.Model.FhirString ReasonElement { get; set; }
        
        /// <summary>
        /// Description of why this subscription was created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Reason { get; set; }
    
        /// <summary>
        /// Rule for server push criteria
        /// </summary>
        Hl7.Fhir.Model.FhirString CriteriaElement { get; set; }
        
        /// <summary>
        /// Rule for server push criteria
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Criteria { get; set; }
    
        /// <summary>
        /// Latest error note
        /// </summary>
        Hl7.Fhir.Model.FhirString ErrorElement { get; set; }
        
        /// <summary>
        /// Latest error note
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Error { get; set; }
    
        /// <summary>
        /// The channel on which to report matches to the criteria
        /// </summary>
        Hl7.Fhir.Model.ISubscriptionChannelComponent Channel { get; }
    
    }
    
    public partial interface ISubscriptionChannelComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// rest-hook | websocket | email | sms | message
        /// </summary>
        Code<Hl7.Fhir.Model.SubscriptionChannelType> TypeElement { get; set; }
        
        /// <summary>
        /// rest-hook | websocket | email | sms | message
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.SubscriptionChannelType? Type { get; set; }
    
    }

}
