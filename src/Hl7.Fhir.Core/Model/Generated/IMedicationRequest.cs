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
    /// Ordering of medication for patient or group
    /// </summary>
    public partial interface IMedicationRequest : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// External ids for this request
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// What request fulfills
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> BasedOn { get; set; }
    
        /// <summary>
        /// Composite request this is part of
        /// </summary>
        Hl7.Fhir.Model.Identifier GroupIdentifier { get; set; }
    
        /// <summary>
        /// Medication to be taken
        /// </summary>
        Hl7.Fhir.Model.Element Medication { get; set; }
    
        /// <summary>
        /// Who or group medication request is for
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// Information to support ordering of the medication
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> SupportingInformation { get; set; }
    
        /// <summary>
        /// When request was initially authored
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime AuthoredOnElement { get; set; }
        
        /// <summary>
        /// When request was initially authored
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string AuthoredOn { get; set; }
    
        /// <summary>
        /// Person who entered the request
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Recorder { get; set; }
    
        /// <summary>
        /// Reason or indication for ordering or not ordering the medication
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> ReasonCode { get; set; }
    
        /// <summary>
        /// Condition or observation that supports why the prescription is being written
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> ReasonReference { get; set; }
    
        /// <summary>
        /// Information about the prescription
        /// </summary>
        List<Hl7.Fhir.Model.Annotation> Note { get; set; }
    
        /// <summary>
        /// How the medication should be taken
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IDosage> DosageInstruction { get; }
    
        /// <summary>
        /// Medication supply authorization
        /// </summary>
        Hl7.Fhir.Model.IMedicationRequestDispenseRequestComponent DispenseRequest { get; }
    
        /// <summary>
        /// Any restrictions on medication substitution
        /// </summary>
        Hl7.Fhir.Model.IMedicationRequestSubstitutionComponent Substitution { get; }
    
        /// <summary>
        /// An order/prescription that is being replaced
        /// </summary>
        Hl7.Fhir.Model.ResourceReference PriorPrescription { get; set; }
    
        /// <summary>
        /// Clinical Issue with action
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> DetectedIssue { get; set; }
    
        /// <summary>
        /// A list of events of interest in the lifecycle
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> EventHistory { get; set; }
    
    }
    
    public partial interface IMedicationRequestDispenseRequestComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Time period supply is authorized for
        /// </summary>
        Hl7.Fhir.Model.Period ValidityPeriod { get; set; }
    
        /// <summary>
        /// Amount of medication to supply per dispense
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Quantity { get; set; }
    
        /// <summary>
        /// Number of days supply per dispense
        /// </summary>
        Hl7.Fhir.Model.IDuration ExpectedSupplyDuration { get; }
    
        /// <summary>
        /// Intended dispenser
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Performer { get; set; }
    
    }
    
    public partial interface IMedicationRequestSubstitutionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Why should (not) substitution be made
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Reason { get; set; }
    
    }

}
