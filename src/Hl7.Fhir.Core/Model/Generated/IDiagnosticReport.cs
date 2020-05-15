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
    /// A Diagnostic report - a combination of request information, atomic results, images, interpretation, as well as formatted reports
    /// </summary>
    public partial interface IDiagnosticReport : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Id for external references to this report
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Name/Code for this diagnostic report
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// The subject of the report, usually, but not always, the patient
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// Clinically Relevant time/time-period for report
        /// </summary>
        Hl7.Fhir.Model.Element Effective { get; set; }
    
        /// <summary>
        /// DateTime this version was released
        /// </summary>
        Hl7.Fhir.Model.Instant IssuedElement { get; set; }
        
        /// <summary>
        /// DateTime this version was released
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        DateTimeOffset? Issued { get; set; }
    
        /// <summary>
        /// Specimens this report is based on
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Specimen { get; set; }
    
        /// <summary>
        /// Observations - simple, or complex nested groups
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Result { get; set; }
    
        /// <summary>
        /// Reference to full details of imaging associated with the diagnostic report
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> ImagingStudy { get; set; }
    
        /// <summary>
        /// Clinical Interpretation of test results
        /// </summary>
        Hl7.Fhir.Model.FhirString ConclusionElement { get; set; }
        
        /// <summary>
        /// Clinical Interpretation of test results
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Conclusion { get; set; }
    
        /// <summary>
        /// Entire report as issued
        /// </summary>
        List<Hl7.Fhir.Model.Attachment> PresentedForm { get; set; }
    
    }

}
