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
    /// Roles/organizations the practitioner is associated with
    /// </summary>
    public partial interface IPractitionerRole : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Business Identifiers that are specific to a role/location
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Whether this practitioner role record is in active use
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ActiveElement { get; set; }
        
        /// <summary>
        /// Whether this practitioner role record is in active use
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Active { get; set; }
    
        /// <summary>
        /// The period during which the practitioner is authorized to perform in these role(s)
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// Practitioner that is able to provide the defined services for the organization
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Practitioner { get; set; }
    
        /// <summary>
        /// Organization where the roles are available
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Organization { get; set; }
    
        /// <summary>
        /// Roles which this practitioner may perform
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Code { get; set; }
    
        /// <summary>
        /// Specific specialty of the practitioner
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Specialty { get; set; }
    
        /// <summary>
        /// The location(s) at which this practitioner provides care
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Location { get; set; }
    
        /// <summary>
        /// The list of healthcare services that this worker provides for this role's Organization/Location(s)
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> HealthcareService { get; set; }
    
        /// <summary>
        /// Contact details that are specific to the role/location/service
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Telecom { get; }
    
        /// <summary>
        /// Times the Service Site is available
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPractitionerRoleAvailableTimeComponent> AvailableTime { get; }
    
        /// <summary>
        /// Not available during this time due to provided reason
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPractitionerRoleNotAvailableComponent> NotAvailable { get; }
    
        /// <summary>
        /// Description of availability exceptions
        /// </summary>
        Hl7.Fhir.Model.FhirString AvailabilityExceptionsElement { get; set; }
        
        /// <summary>
        /// Description of availability exceptions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string AvailabilityExceptions { get; set; }
    
        /// <summary>
        /// Technical endpoints providing access to services operated for the practitioner with this role
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Endpoint { get; set; }
    
    }
    
    public partial interface IPractitionerRoleAvailableTimeComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// mon | tue | wed | thu | fri | sat | sun
        /// </summary>
        List<Code<Hl7.Fhir.Model.DaysOfWeek>> DaysOfWeekElement { get; set; }
        
        /// <summary>
        /// mon | tue | wed | thu | fri | sat | sun
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<Hl7.Fhir.Model.DaysOfWeek?> DaysOfWeek { get; set; }
    
        /// <summary>
        /// Always available? e.g. 24 hour service
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean AllDayElement { get; set; }
        
        /// <summary>
        /// Always available? e.g. 24 hour service
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? AllDay { get; set; }
    
        /// <summary>
        /// Opening time of day (ignored if allDay = true)
        /// </summary>
        Hl7.Fhir.Model.Time AvailableStartTimeElement { get; set; }
        
        /// <summary>
        /// Opening time of day (ignored if allDay = true)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string AvailableStartTime { get; set; }
    
        /// <summary>
        /// Closing time of day (ignored if allDay = true)
        /// </summary>
        Hl7.Fhir.Model.Time AvailableEndTimeElement { get; set; }
        
        /// <summary>
        /// Closing time of day (ignored if allDay = true)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string AvailableEndTime { get; set; }
    
    }
    
    public partial interface IPractitionerRoleNotAvailableComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Reason presented to the user explaining why time not available
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Reason presented to the user explaining why time not available
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Service not available from this date
        /// </summary>
        Hl7.Fhir.Model.Period During { get; set; }
    
    }

}
