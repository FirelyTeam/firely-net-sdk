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
    /// How the medication is/was taken or should be taken
    /// </summary>
    public partial interface IDosage : Hl7.Fhir.Model.IElement
    {
    
        /// <summary>
        /// The order of the dosage instructions
        /// </summary>
        Hl7.Fhir.Model.Integer SequenceElement { get; set; }
        
        /// <summary>
        /// The order of the dosage instructions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Sequence { get; set; }
    
        /// <summary>
        /// Free text dosage instructions e.g. SIG
        /// </summary>
        Hl7.Fhir.Model.FhirString TextElement { get; set; }
        
        /// <summary>
        /// Free text dosage instructions e.g. SIG
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Text { get; set; }
    
        /// <summary>
        /// Supplemental instruction or warnings to the patient - e.g. "with meals", "may cause drowsiness"
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> AdditionalInstruction { get; set; }
    
        /// <summary>
        /// Patient or consumer oriented instructions
        /// </summary>
        Hl7.Fhir.Model.FhirString PatientInstructionElement { get; set; }
        
        /// <summary>
        /// Patient or consumer oriented instructions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string PatientInstruction { get; set; }
    
        /// <summary>
        /// When medication should be administered
        /// </summary>
        Hl7.Fhir.Model.ITiming Timing { get; }
    
        /// <summary>
        /// Take "as needed" (for x)
        /// </summary>
        Hl7.Fhir.Model.Element AsNeeded { get; set; }
    
        /// <summary>
        /// Body site to administer to
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Site { get; set; }
    
        /// <summary>
        /// How drug should enter body
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Route { get; set; }
    
        /// <summary>
        /// Technique for administering medication
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Method { get; set; }
    
        /// <summary>
        /// Upper limit on medication per unit of time
        /// </summary>
        Hl7.Fhir.Model.Ratio MaxDosePerPeriod { get; set; }
    
        /// <summary>
        /// Upper limit on medication per administration
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity MaxDosePerAdministration { get; set; }
    
        /// <summary>
        /// Upper limit on medication per lifetime of the patient
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity MaxDosePerLifetime { get; set; }
    
    }

}
