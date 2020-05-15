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
    /// The formal response to a guidance request
    /// </summary>
    public partial interface IGuidanceResponse : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// success | data-requested | data-required | in-progress | failure | entered-in-error
        /// </summary>
        Code<Hl7.Fhir.Model.GuidanceResponseStatus> StatusElement { get; set; }
        
        /// <summary>
        /// success | data-requested | data-required | in-progress | failure | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.GuidanceResponseStatus? Status { get; set; }
    
        /// <summary>
        /// Patient the request was performed for
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// When the guidance response was processed
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime OccurrenceDateTimeElement { get; set; }
        
        /// <summary>
        /// When the guidance response was processed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string OccurrenceDateTime { get; set; }
    
        /// <summary>
        /// Device returning the guidance
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Performer { get; set; }
    
        /// <summary>
        /// Additional notes about the response
        /// </summary>
        List<Hl7.Fhir.Model.Annotation> Note { get; set; }
    
        /// <summary>
        /// Messages resulting from the evaluation of the artifact or artifacts
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> EvaluationMessage { get; set; }
    
        /// <summary>
        /// The output parameters of the evaluation, if any
        /// </summary>
        Hl7.Fhir.Model.ResourceReference OutputParameters { get; set; }
    
        /// <summary>
        /// Proposed actions, if any
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Result { get; set; }
    
        /// <summary>
        /// Additional required data
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IDataRequirement> DataRequirement { get; }
    
    }

}
