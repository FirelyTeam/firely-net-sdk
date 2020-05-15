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
    /// Measurements and simple assertions
    /// </summary>
    public partial interface IObservation : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Unique Id for this particular observation
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Type of observation (code / type)
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Who and/or what this is about
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// Clinically relevant time/time-period for observation
        /// </summary>
        Hl7.Fhir.Model.Element Effective { get; set; }
    
        /// <summary>
        /// Date/Time this was made available
        /// </summary>
        Hl7.Fhir.Model.Instant IssuedElement { get; set; }
        
        /// <summary>
        /// Date/Time this was made available
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        DateTimeOffset? Issued { get; set; }
    
        /// <summary>
        /// Who is responsible for the observation
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Performer { get; set; }
    
        /// <summary>
        /// Actual result
        /// </summary>
        Hl7.Fhir.Model.Element Value { get; set; }
    
        /// <summary>
        /// Why the result is missing
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept DataAbsentReason { get; set; }
    
        /// <summary>
        /// Observed body part
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept BodySite { get; set; }
    
        /// <summary>
        /// How it was done
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Method { get; set; }
    
        /// <summary>
        /// Specimen used for this observation
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Specimen { get; set; }
    
        /// <summary>
        /// (Measurement) Device
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Device { get; set; }
    
        /// <summary>
        /// Provides guide for interpretation
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IObservationReferenceRangeComponent> ReferenceRange { get; }
    
        /// <summary>
        /// Component results
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IObservationComponentComponent> Component { get; }
    
    }
    
    public partial interface IObservationReferenceRangeComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Low Range, if relevant
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Low { get; set; }
    
        /// <summary>
        /// High Range, if relevant
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity High { get; set; }
    
        /// <summary>
        /// Applicable age range, if relevant
        /// </summary>
        Hl7.Fhir.Model.Range Age { get; set; }
    
        /// <summary>
        /// Text based reference range in an observation
        /// </summary>
        Hl7.Fhir.Model.FhirString TextElement { get; set; }
        
        /// <summary>
        /// Text based reference range in an observation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Text { get; set; }
    
    }
    
    public partial interface IObservationComponentComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Type of component observation (code / type)
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Actual component result
        /// </summary>
        Hl7.Fhir.Model.Element Value { get; set; }
    
        /// <summary>
        /// Why the component result is missing
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept DataAbsentReason { get; set; }
    
        /// <summary>
        /// Provides guide for interpretation of component result
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IObservationReferenceRangeComponent> ReferenceRange { get; }
    
    }

}
