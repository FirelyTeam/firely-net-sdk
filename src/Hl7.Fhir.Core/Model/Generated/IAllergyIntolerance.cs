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
    /// Allergy or Intolerance (generally: Risk Of Adverse reaction to a substance)
    /// </summary>
    public partial interface IAllergyIntolerance : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// External ids for this item
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// allergy | intolerance - Underlying mechanism (if known)
        /// </summary>
        Code<Hl7.Fhir.Model.AllergyIntoleranceType> TypeElement { get; set; }
        
        /// <summary>
        /// allergy | intolerance - Underlying mechanism (if known)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.AllergyIntoleranceType? Type { get; set; }
    
        /// <summary>
        /// Who the sensitivity is for
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Patient { get; set; }
    
        /// <summary>
        /// Who recorded the sensitivity
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Recorder { get; set; }
    
        /// <summary>
        /// Adverse Reaction Events linked to exposure to substance
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IAllergyIntoleranceReactionComponent> Reaction { get; }
    
    }
    
    public partial interface IAllergyIntoleranceReactionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Specific substance considered to be responsible for event
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Substance { get; set; }
    
        /// <summary>
        /// Clinical symptoms/signs associated with the Event
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Manifestation { get; set; }
    
        /// <summary>
        /// Description of the event as a whole
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Description of the event as a whole
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Date(/time) when manifestations showed
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime OnsetElement { get; set; }
        
        /// <summary>
        /// Date(/time) when manifestations showed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Onset { get; set; }
    
        /// <summary>
        /// mild | moderate | severe (of event as a whole)
        /// </summary>
        Code<Hl7.Fhir.Model.AllergyIntoleranceSeverity> SeverityElement { get; set; }
        
        /// <summary>
        /// mild | moderate | severe (of event as a whole)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.AllergyIntoleranceSeverity? Severity { get; set; }
    
        /// <summary>
        /// How the subject was exposed to the substance
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept ExposureRoute { get; set; }
    
    }

}
