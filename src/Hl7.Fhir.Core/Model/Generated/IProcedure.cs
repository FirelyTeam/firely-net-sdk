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
    /// An action that is being or was performed on a patient
    /// </summary>
    public partial interface IProcedure : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// External Identifiers for this procedure
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Classification of the procedure
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Category { get; set; }
    
        /// <summary>
        /// Identification of the procedure
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Who the procedure was performed on
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// Date/Period the procedure was performed
        /// </summary>
        Hl7.Fhir.Model.Element Performed { get; set; }
    
        /// <summary>
        /// The people who performed the procedure
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IProcedurePerformerComponent> Performer { get; }
    
        /// <summary>
        /// Where the procedure happened
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Location { get; set; }
    
        /// <summary>
        /// Target body sites
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> BodySite { get; set; }
    
        /// <summary>
        /// The result of procedure
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Outcome { get; set; }
    
        /// <summary>
        /// Any report resulting from the procedure
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Report { get; set; }
    
        /// <summary>
        /// Complication following the procedure
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Complication { get; set; }
    
        /// <summary>
        /// Instructions for follow up
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> FollowUp { get; set; }
    
        /// <summary>
        /// Device changed in procedure
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IProcedureFocalDeviceComponent> FocalDevice { get; }
    
    }
    
    public partial interface IProcedurePerformerComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The reference to the practitioner
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Actor { get; set; }
    
    }
    
    public partial interface IProcedureFocalDeviceComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Kind of change to device
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Action { get; set; }
    
        /// <summary>
        /// Device that was changed
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Manipulated { get; set; }
    
    }

}
