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
    /// Medical care, research study or other healthcare event causing physical injury
    /// </summary>
    public partial interface IAdverseEvent : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Business identifier for the event
        /// </summary>
        Hl7.Fhir.Model.Identifier Identifier { get; set; }
    
        /// <summary>
        /// Subject impacted by event
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// When the event occurred
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// When the event occurred
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// Location where adverse event occurred
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Location { get; set; }
    
        /// <summary>
        /// Seriousness of the event
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Seriousness { get; set; }
    
        /// <summary>
        /// resolved | recovering | ongoing | resolvedWithSequelae | fatal | unknown
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Outcome { get; set; }
    
        /// <summary>
        /// Who recorded the adverse event
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Recorder { get; set; }
    
        /// <summary>
        /// The suspected agent causing the adverse event
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IAdverseEventSuspectEntityComponent> SuspectEntity { get; }
    
        /// <summary>
        /// AdverseEvent.subjectMedicalHistory
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> SubjectMedicalHistory { get; set; }
    
        /// <summary>
        /// AdverseEvent.referenceDocument
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> ReferenceDocument { get; set; }
    
        /// <summary>
        /// AdverseEvent.study
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Study { get; set; }
    
    }
    
    public partial interface IAdverseEventSuspectEntityComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Refers to the specific entity that caused the adverse event
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Instance { get; set; }
    
    }

}
