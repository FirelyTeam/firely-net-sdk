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
    /// A booking of a healthcare event among patient(s), practitioner(s), related person(s) and/or device(s) for a specific date/time. This may result in one or more Encounter(s)
    /// </summary>
    public partial interface IAppointment : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// External Ids for this item
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Used to make informed decisions if needing to re-prioritize
        /// </summary>
        Hl7.Fhir.Model.UnsignedInt PriorityElement { get; set; }
        
        /// <summary>
        /// Used to make informed decisions if needing to re-prioritize
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Priority { get; set; }
    
        /// <summary>
        /// Shown on a subject line in a meeting request, or appointment list
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Shown on a subject line in a meeting request, or appointment list
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// When appointment is to take place
        /// </summary>
        Hl7.Fhir.Model.Instant StartElement { get; set; }
        
        /// <summary>
        /// When appointment is to take place
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        DateTimeOffset? Start { get; set; }
    
        /// <summary>
        /// When appointment is to conclude
        /// </summary>
        Hl7.Fhir.Model.Instant EndElement { get; set; }
        
        /// <summary>
        /// When appointment is to conclude
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        DateTimeOffset? End { get; set; }
    
        /// <summary>
        /// Can be less than start/end (e.g. estimate)
        /// </summary>
        Hl7.Fhir.Model.PositiveInt MinutesDurationElement { get; set; }
        
        /// <summary>
        /// Can be less than start/end (e.g. estimate)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? MinutesDuration { get; set; }
    
        /// <summary>
        /// If provided, then no schedule and start/end values MUST match slot
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Slot { get; set; }
    
        /// <summary>
        /// Additional comments
        /// </summary>
        Hl7.Fhir.Model.FhirString CommentElement { get; set; }
        
        /// <summary>
        /// Additional comments
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Comment { get; set; }
    
        /// <summary>
        /// Participants involved in appointment
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IAppointmentParticipantComponent> Participant { get; }
    
    }
    
    public partial interface IAppointmentParticipantComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Role of participant in the appointment
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Type { get; set; }
    
        /// <summary>
        /// Person, Location/HealthcareService or Device
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Actor { get; set; }
    
        /// <summary>
        /// required | optional | information-only
        /// </summary>
        Code<Hl7.Fhir.Model.ParticipantRequired> RequiredElement { get; set; }
        
        /// <summary>
        /// required | optional | information-only
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ParticipantRequired? Required { get; set; }
    
        /// <summary>
        /// accepted | declined | tentative | needs-action
        /// </summary>
        Code<Hl7.Fhir.Model.ParticipationStatus> StatusElement { get; set; }
        
        /// <summary>
        /// accepted | declined | tentative | needs-action
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ParticipationStatus? Status { get; set; }
    
    }

}
