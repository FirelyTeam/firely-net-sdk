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
    /// Sample for analysis
    /// </summary>
    public partial interface ISpecimen : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// External Identifier
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Identifier assigned by the lab
        /// </summary>
        Hl7.Fhir.Model.Identifier AccessionIdentifier { get; set; }
    
        /// <summary>
        /// available | unavailable | unsatisfactory | entered-in-error
        /// </summary>
        Code<Hl7.Fhir.Model.SpecimenStatus> StatusElement { get; set; }
        
        /// <summary>
        /// available | unavailable | unsatisfactory | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.SpecimenStatus? Status { get; set; }
    
        /// <summary>
        /// Kind of material that forms the specimen
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Where the specimen came from. This may be from the patient(s) or from the environment or a device
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// The time when specimen was received for processing
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime ReceivedTimeElement { get; set; }
        
        /// <summary>
        /// The time when specimen was received for processing
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ReceivedTime { get; set; }
    
        /// <summary>
        /// Specimen from which this specimen originated
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Parent { get; set; }
    
        /// <summary>
        /// Collection details
        /// </summary>
        Hl7.Fhir.Model.ISpecimenCollectionComponent Collection { get; }
    
        /// <summary>
        /// Direct container of specimen (tube/slide, etc.)
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ISpecimenContainerComponent> Container { get; }
    
    }
    
    public partial interface ISpecimenCollectionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Who collected the specimen
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Collector { get; set; }
    
        /// <summary>
        /// Collection time
        /// </summary>
        Hl7.Fhir.Model.Element Collected { get; set; }
    
        /// <summary>
        /// The quantity of specimen collected
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Quantity { get; set; }
    
        /// <summary>
        /// Technique used to perform collection
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Method { get; set; }
    
        /// <summary>
        /// Anatomical collection site
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept BodySite { get; set; }
    
    }
    
    public partial interface ISpecimenContainerComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Id for the container
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Textual description of the container
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Textual description of the container
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Kind of container directly associated with specimen
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Container volume or size
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Capacity { get; set; }
    
        /// <summary>
        /// Quantity of specimen within container
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity SpecimenQuantity { get; set; }
    
        /// <summary>
        /// Additive associated with container
        /// </summary>
        Hl7.Fhir.Model.Element Additive { get; set; }
    
    }

}
