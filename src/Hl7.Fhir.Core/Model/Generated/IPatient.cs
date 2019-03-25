﻿using System;
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
// Generated for FHIR v1.0.2, v4.0.0, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information about an individual or animal receiving health care services
    /// </summary>
    public partial interface IPatient : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// An identifier for this patient
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Whether this patient's record is in active use
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ActiveElement { get; set; }
        
        /// <summary>
        /// Whether this patient's record is in active use
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Active { get; set; }
    
        /// <summary>
        /// A name associated with the patient
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IHumanName> Name { get; }
    
        /// <summary>
        /// A contact detail for the individual
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Telecom { get; }
    
        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        Code<Hl7.Fhir.Model.AdministrativeGender> GenderElement { get; set; }
        
        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.AdministrativeGender? Gender { get; set; }
    
        /// <summary>
        /// The date of birth for the individual
        /// </summary>
        Hl7.Fhir.Model.Date BirthDateElement { get; set; }
        
        /// <summary>
        /// The date of birth for the individual
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string BirthDate { get; set; }
    
        /// <summary>
        /// Indicates if the individual is deceased or not
        /// </summary>
        Hl7.Fhir.Model.Element Deceased { get; set; }
    
        /// <summary>
        /// Addresses for the individual
        /// </summary>
        List<Hl7.Fhir.Model.Address> Address { get; set; }
    
        /// <summary>
        /// Marital (civil) status of a patient
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept MaritalStatus { get; set; }
    
        /// <summary>
        /// Whether patient is part of a multiple birth
        /// </summary>
        Hl7.Fhir.Model.Element MultipleBirth { get; set; }
    
        /// <summary>
        /// Image of the patient
        /// </summary>
        List<Hl7.Fhir.Model.Attachment> Photo { get; set; }
    
        /// <summary>
        /// A contact party (e.g. guardian, partner, friend) for the patient
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPatientContactComponent> Contact { get; }
    
        /// <summary>
        /// A list of Languages which may be used to communicate with the patient about his or her health
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPatientCommunicationComponent> Communication { get; }
    
        /// <summary>
        /// Organization that is the custodian of the patient record
        /// </summary>
        Hl7.Fhir.Model.ResourceReference ManagingOrganization { get; set; }
    
        /// <summary>
        /// Link to another patient resource that concerns the same actual person
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPatientLinkComponent> Link { get; }
    
    }
    
    public partial interface IPatientContactComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The kind of relationship
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Relationship { get; set; }
    
        /// <summary>
        /// A name associated with the contact person
        /// </summary>
        Hl7.Fhir.Model.IHumanName Name { get; }
    
        /// <summary>
        /// A contact detail for the person
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Telecom { get; }
    
        /// <summary>
        /// Address for the contact person
        /// </summary>
        Hl7.Fhir.Model.Address Address { get; set; }
    
        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        Code<Hl7.Fhir.Model.AdministrativeGender> GenderElement { get; set; }
        
        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.AdministrativeGender? Gender { get; set; }
    
        /// <summary>
        /// Organization that is associated with the contact
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Organization { get; set; }
    
        /// <summary>
        /// The period during which this contact person or organization is valid to be contacted relating to this patient
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
    }
    
    public partial interface IPatientCommunicationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The language which can be used to communicate with the patient about his or her health
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Language { get; set; }
    
        /// <summary>
        /// Language preference indicator
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean PreferredElement { get; set; }
        
        /// <summary>
        /// Language preference indicator
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Preferred { get; set; }
    
    }
    
    public partial interface IPatientLinkComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The other patient resource that the link refers to
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Other { get; set; }
    
    }

}
