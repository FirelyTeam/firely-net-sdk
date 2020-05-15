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
    /// Detailed information about conditions, problems or diagnoses
    /// </summary>
    public partial interface ICondition : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// External Ids for this condition
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Subjective severity of condition
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Severity { get; set; }
    
        /// <summary>
        /// Identification of the condition, problem or diagnosis
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Anatomical location, if relevant
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> BodySite { get; set; }
    
        /// <summary>
        /// Estimated or actual date,  date-time, or age
        /// </summary>
        Hl7.Fhir.Model.Element Onset { get; set; }
    
        /// <summary>
        /// If/when in resolution/remission
        /// </summary>
        Hl7.Fhir.Model.Element Abatement { get; set; }
    
        /// <summary>
        /// Person who asserts this condition
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Asserter { get; set; }
    
        /// <summary>
        /// Supporting evidence
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IConditionEvidenceComponent> Evidence { get; }
    
    }
    
    public partial interface IConditionStageComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Simple summary (disease specific)
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Summary { get; set; }
    
        /// <summary>
        /// Formal record of assessment
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Assessment { get; set; }
    
    }
    
    public partial interface IConditionEvidenceComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Supporting information found elsewhere
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Detail { get; set; }
    
    }

}
