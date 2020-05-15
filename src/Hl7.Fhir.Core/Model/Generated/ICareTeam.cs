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
    /// Planned participants in the coordination and delivery of care for a patient or group
    /// </summary>
    public partial interface ICareTeam : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// External Ids for this team
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// proposed | active | suspended | inactive | entered-in-error
        /// </summary>
        Code<Hl7.Fhir.Model.CareTeamStatus> StatusElement { get; set; }
        
        /// <summary>
        /// proposed | active | suspended | inactive | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.CareTeamStatus? Status { get; set; }
    
        /// <summary>
        /// Type of team
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Category { get; set; }
    
        /// <summary>
        /// Name of the team, such as crisis assessment team
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Name of the team, such as crisis assessment team
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Who care team is for
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// Time period team covers
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// Members of the team
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICareTeamParticipantComponent> Participant { get; }
    
        /// <summary>
        /// Why the care team exists
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> ReasonCode { get; set; }
    
        /// <summary>
        /// Why the care team exists
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> ReasonReference { get; set; }
    
        /// <summary>
        /// Organization responsible for the care team
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> ManagingOrganization { get; set; }
    
        /// <summary>
        /// Comments made about the CareTeam
        /// </summary>
        List<Hl7.Fhir.Model.Annotation> Note { get; set; }
    
    }
    
    public partial interface ICareTeamParticipantComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Who is involved
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Member { get; set; }
    
        /// <summary>
        /// Organization of the practitioner
        /// </summary>
        Hl7.Fhir.Model.ResourceReference OnBehalfOf { get; set; }
    
        /// <summary>
        /// Time period of participant
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
    }

}
