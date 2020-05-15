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
    /// A reply to an appointment request for a patient and/or practitioner(s), such as a confirmation or rejection
    /// </summary>
    public partial interface IAppointmentResponse : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// External Ids for this item
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Appointment this response relates to
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Appointment { get; set; }
    
        /// <summary>
        /// Time from appointment, or requested new start time
        /// </summary>
        Hl7.Fhir.Model.Instant StartElement { get; set; }
        
        /// <summary>
        /// Time from appointment, or requested new start time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        DateTimeOffset? Start { get; set; }
    
        /// <summary>
        /// Time from appointment, or requested new end time
        /// </summary>
        Hl7.Fhir.Model.Instant EndElement { get; set; }
        
        /// <summary>
        /// Time from appointment, or requested new end time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        DateTimeOffset? End { get; set; }
    
        /// <summary>
        /// Role of participant in the appointment
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> ParticipantType { get; set; }
    
        /// <summary>
        /// Person, Location/HealthcareService or Device
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Actor { get; set; }
    
        /// <summary>
        /// Additional comments
        /// </summary>
        Hl7.Fhir.Model.FhirString CommentElement { get; set; }
        
        /// <summary>
        /// Additional comments
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Comment { get; set; }
    
    }

}
