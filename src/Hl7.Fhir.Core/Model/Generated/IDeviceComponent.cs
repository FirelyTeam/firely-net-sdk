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
// Generated for FHIR v1.0.2, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// An instance of a medical-related component of a medical device
    /// </summary>
    public partial interface IDeviceComponent : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Instance id assigned by the software stack
        /// </summary>
        Hl7.Fhir.Model.Identifier Identifier { get; set; }
    
        /// <summary>
        /// What kind of component it is
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Recent system change timestamp
        /// </summary>
        Hl7.Fhir.Model.Instant LastSystemChangeElement { get; set; }
        
        /// <summary>
        /// Recent system change timestamp
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        DateTimeOffset? LastSystemChange { get; set; }
    
        /// <summary>
        /// A source device of this component
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Source { get; set; }
    
        /// <summary>
        /// Parent resource link
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Parent { get; set; }
    
        /// <summary>
        /// Component operational status
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> OperationalStatus { get; set; }
    
        /// <summary>
        /// Current supported parameter group
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept ParameterGroup { get; set; }
    
        /// <summary>
        /// Production specification of the component
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IDeviceComponentProductionSpecificationComponent> ProductionSpecification { get; }
    
        /// <summary>
        /// Language code for the human-readable text strings produced by the device
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept LanguageCode { get; set; }
    
    }
    
    public partial interface IDeviceComponentProductionSpecificationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Specification type
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept SpecType { get; set; }
    
        /// <summary>
        /// Internal component unique identification
        /// </summary>
        Hl7.Fhir.Model.Identifier ComponentId { get; set; }
    
        /// <summary>
        /// A printable string defining the component
        /// </summary>
        Hl7.Fhir.Model.FhirString ProductionSpecElement { get; set; }
        
        /// <summary>
        /// A printable string defining the component
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ProductionSpec { get; set; }
    
    }

}
