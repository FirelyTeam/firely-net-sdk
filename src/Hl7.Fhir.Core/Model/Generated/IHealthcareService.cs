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
    /// The details of a healthcare service available at a location
    /// </summary>
    public partial interface IHealthcareService : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// External identifiers for this item
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Organization that provides this service
        /// </summary>
        Hl7.Fhir.Model.ResourceReference ProvidedBy { get; set; }
    
        /// <summary>
        /// Additional description and/or any specific issues not covered elsewhere
        /// </summary>
        Hl7.Fhir.Model.FhirString CommentElement { get; set; }
        
        /// <summary>
        /// Additional description and/or any specific issues not covered elsewhere
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Comment { get; set; }
    
        /// <summary>
        /// Facilitates quick identification of the service
        /// </summary>
        Hl7.Fhir.Model.Attachment Photo { get; set; }
    
        /// <summary>
        /// Contacts related to the healthcare service
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Telecom { get; }
    
        /// <summary>
        /// Location(s) service is inteded for/available to
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> CoverageArea { get; set; }
    
        /// <summary>
        /// Conditions under which service is available/offered
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> ServiceProvisionCode { get; set; }
    
        /// <summary>
        /// Collection of characteristics (attributes)
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Characteristic { get; set; }
    
        /// <summary>
        /// Ways that the service accepts referrals
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> ReferralMethod { get; set; }
    
        /// <summary>
        /// If an appointment is required for access to this service
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean AppointmentRequiredElement { get; set; }
        
        /// <summary>
        /// If an appointment is required for access to this service
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? AppointmentRequired { get; set; }
    
        /// <summary>
        /// Times the Service Site is available
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IHealthcareServiceAvailableTimeComponent> AvailableTime { get; }
    
        /// <summary>
        /// Not available during this time due to provided reason
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IHealthcareServiceNotAvailableComponent> NotAvailable { get; }
    
        /// <summary>
        /// Description of availability exceptions
        /// </summary>
        Hl7.Fhir.Model.FhirString AvailabilityExceptionsElement { get; set; }
        
        /// <summary>
        /// Description of availability exceptions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string AvailabilityExceptions { get; set; }
    
    }
    
    public partial interface IHealthcareServiceAvailableTimeComponent : Hl7.Fhir.Model.IBackboneElement
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
    
    public partial interface IHealthcareServiceNotAvailableComponent : Hl7.Fhir.Model.IBackboneElement
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
        /// Service not availablefrom this date
        /// </summary>
        Hl7.Fhir.Model.Period During { get; set; }
    
    }

}
