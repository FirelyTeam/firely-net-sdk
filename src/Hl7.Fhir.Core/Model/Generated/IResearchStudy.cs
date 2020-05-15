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
    /// Investigation to increase healthcare-related patient-independent knowledge
    /// </summary>
    public partial interface IResearchStudy : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Business Identifier for study
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Name for this study
        /// </summary>
        Hl7.Fhir.Model.FhirString TitleElement { get; set; }
        
        /// <summary>
        /// Name for this study
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Title { get; set; }
    
        /// <summary>
        /// Steps followed in executing study
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Protocol { get; set; }
    
        /// <summary>
        /// Part of larger study
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> PartOf { get; set; }
    
        /// <summary>
        /// Classifications for the study
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Category { get; set; }
    
        /// <summary>
        /// Drugs, devices, etc. under study
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Focus { get; set; }
    
        /// <summary>
        /// Contact details for the study
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContactDetail> Contact { get; }
    
        /// <summary>
        /// References and dependencies
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IRelatedArtifact> RelatedArtifact { get; }
    
        /// <summary>
        /// Used to search for the study
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Keyword { get; set; }
    
        /// <summary>
        /// What this is study doing
        /// </summary>
        Hl7.Fhir.Model.Markdown DescriptionElement { get; set; }
        
        /// <summary>
        /// What this is study doing
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Inclusion &amp; exclusion criteria
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Enrollment { get; set; }
    
        /// <summary>
        /// When the study began and ended
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// Organization that initiates and is legally responsible for the study
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Sponsor { get; set; }
    
        /// <summary>
        /// Researcher who oversees multiple aspects of the study
        /// </summary>
        Hl7.Fhir.Model.ResourceReference PrincipalInvestigator { get; set; }
    
        /// <summary>
        /// Facility where study activities are conducted
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Site { get; set; }
    
        /// <summary>
        /// accrual-goal-met | closed-due-to-toxicity | closed-due-to-lack-of-study-progress | temporarily-closed-per-study-design
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept ReasonStopped { get; set; }
    
        /// <summary>
        /// Comments made about the study
        /// </summary>
        List<Hl7.Fhir.Model.Annotation> Note { get; set; }
    
        /// <summary>
        /// Defined path through the study for a subject
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IResearchStudyArmComponent> Arm { get; }
    
    }
    
    public partial interface IResearchStudyArmComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Label for study arm
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Label for study arm
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Short explanation of study path
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Short explanation of study path
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
    }

}
