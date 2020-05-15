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
    /// Information about patient's relatives, relevant for patient
    /// </summary>
    public partial interface IFamilyMemberHistory : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// External Id(s) for this record
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// partial | completed | entered-in-error | health-unknown
        /// </summary>
        Code<Hl7.Fhir.Model.FamilyHistoryStatus> StatusElement { get; set; }
        
        /// <summary>
        /// partial | completed | entered-in-error | health-unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.FamilyHistoryStatus? Status { get; set; }
    
        /// <summary>
        /// Patient history is about
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Patient { get; set; }
    
        /// <summary>
        /// When history was captured/updated
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// When history was captured/updated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// The family member described
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// The family member described
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Relationship to the subject
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Relationship { get; set; }
    
        /// <summary>
        /// (approximate) date of birth
        /// </summary>
        Hl7.Fhir.Model.Element Born { get; set; }
    
        /// <summary>
        /// (approximate) age
        /// </summary>
        Hl7.Fhir.Model.Element Age { get; set; }
    
        /// <summary>
        /// Dead? How old/when?
        /// </summary>
        Hl7.Fhir.Model.Element Deceased { get; set; }
    
        /// <summary>
        /// Condition that the related person had
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IFamilyMemberHistoryConditionComponent> Condition { get; }
    
    }
    
    public partial interface IFamilyMemberHistoryConditionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Condition suffered by relation
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// deceased | permanent disability | etc.
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Outcome { get; set; }
    
        /// <summary>
        /// When condition first manifested
        /// </summary>
        Hl7.Fhir.Model.Element Onset { get; set; }
    
    }

}
