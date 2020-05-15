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
    /// A person with a  formal responsibility in the provisioning of healthcare or related services
    /// </summary>
    public partial interface IPractitioner : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// A identifier for the person as this agent
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Whether this practitioner's record is in active use
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ActiveElement { get; set; }
        
        /// <summary>
        /// Whether this practitioner's record is in active use
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Active { get; set; }
    
        /// <summary>
        /// A contact detail for the practitioner
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Telecom { get; }
    
        /// <summary>
        /// Where practitioner can be found/visited
        /// </summary>
        List<Hl7.Fhir.Model.Address> Address { get; set; }
    
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
        /// The date  on which the practitioner was born
        /// </summary>
        Hl7.Fhir.Model.Date BirthDateElement { get; set; }
        
        /// <summary>
        /// The date  on which the practitioner was born
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string BirthDate { get; set; }
    
        /// <summary>
        /// Image of the person
        /// </summary>
        List<Hl7.Fhir.Model.Attachment> Photo { get; set; }
    
        /// <summary>
        /// Qualifications obtained by training and certification
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPractitionerQualificationComponent> Qualification { get; }
    
        /// <summary>
        /// A language the practitioner is able to use in patient communication
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Communication { get; set; }
    
    }
    
    public partial interface IPractitionerQualificationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// An identifier for this qualification for the practitioner
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Coded representation of the qualification
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Period during which the qualification is valid
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// Organization that regulates and issues the qualification
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Issuer { get; set; }
    
    }

}
