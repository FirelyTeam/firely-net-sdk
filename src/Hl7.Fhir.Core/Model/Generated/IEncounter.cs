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
    /// An interaction during which services are provided to the patient
    /// </summary>
    public partial interface IEncounter : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Identifier(s) by which this encounter is known
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// List of past encounter statuses
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IEncounterStatusHistoryComponent> StatusHistory { get; }
    
        /// <summary>
        /// Specific type of encounter
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Type { get; set; }
    
        /// <summary>
        /// Indicates the urgency of the encounter
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Priority { get; set; }
    
        /// <summary>
        /// Episode(s) of care that this encounter should be recorded against
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> EpisodeOfCare { get; set; }
    
        /// <summary>
        /// List of participants involved in the encounter
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IEncounterParticipantComponent> Participant { get; }
    
        /// <summary>
        /// The start and end time of the encounter
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// Quantity of time the encounter lasted (less time absent)
        /// </summary>
        Hl7.Fhir.Model.IDuration Length { get; }
    
        /// <summary>
        /// Details about the admission to a healthcare service
        /// </summary>
        Hl7.Fhir.Model.IEncounterHospitalizationComponent Hospitalization { get; }
    
        /// <summary>
        /// List of locations where the patient has been
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IEncounterLocationComponent> Location { get; }
    
        /// <summary>
        /// The custodian organization of this Encounter record
        /// </summary>
        Hl7.Fhir.Model.ResourceReference ServiceProvider { get; set; }
    
        /// <summary>
        /// Another Encounter this encounter is part of
        /// </summary>
        Hl7.Fhir.Model.ResourceReference PartOf { get; set; }
    
    }
    
    public partial interface IEncounterStatusHistoryComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The time that the episode was in the specified status
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
    }
    
    public partial interface IEncounterParticipantComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Role of participant in encounter
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Type { get; set; }
    
        /// <summary>
        /// Period of time during the encounter participant was present
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// Persons involved in the encounter other than the patient
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Individual { get; set; }
    
    }
    
    public partial interface IEncounterHospitalizationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Pre-admission identifier
        /// </summary>
        Hl7.Fhir.Model.Identifier PreAdmissionIdentifier { get; set; }
    
        /// <summary>
        /// The location from which the patient came before admission
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Origin { get; set; }
    
        /// <summary>
        /// From where patient was admitted (physician referral, transfer)
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept AdmitSource { get; set; }
    
        /// <summary>
        /// The type of hospital re-admission that has occurred (if any). If the value is absent, then this is not identified as a readmission
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept ReAdmission { get; set; }
    
        /// <summary>
        /// Diet preferences reported by the patient
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> DietPreference { get; set; }
    
        /// <summary>
        /// Special courtesies (VIP, board member)
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> SpecialCourtesy { get; set; }
    
        /// <summary>
        /// Wheelchair, translator, stretcher, etc.
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> SpecialArrangement { get; set; }
    
        /// <summary>
        /// Location to which the patient is discharged
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Destination { get; set; }
    
        /// <summary>
        /// Category or kind of location after discharge
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept DischargeDisposition { get; set; }
    
    }
    
    public partial interface IEncounterLocationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Location the encounter takes place
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Location { get; set; }
    
        /// <summary>
        /// planned | active | reserved | completed
        /// </summary>
        Code<Hl7.Fhir.Model.EncounterLocationStatus> StatusElement { get; set; }
        
        /// <summary>
        /// planned | active | reserved | completed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.EncounterLocationStatus? Status { get; set; }
    
        /// <summary>
        /// Time period during which the patient was present at the location
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
    }

}
