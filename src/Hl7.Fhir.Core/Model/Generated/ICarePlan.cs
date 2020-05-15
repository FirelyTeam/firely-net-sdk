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
    /// Healthcare plan for patient or group
    /// </summary>
    public partial interface ICarePlan : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// External Ids for this plan
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Type of plan
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Category { get; set; }
    
        /// <summary>
        /// Summary of nature of plan
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Summary of nature of plan
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Who care plan is for
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// Time period plan covers
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// Health issues this plan addresses
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Addresses { get; set; }
    
        /// <summary>
        /// Desired outcome of plan
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Goal { get; set; }
    
        /// <summary>
        /// Action to occur as part of plan
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ICarePlanActivityComponent> Activity { get; }
    
    }
    
    public partial interface ICarePlanActivityComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Comments about the activity status/progress
        /// </summary>
        List<Hl7.Fhir.Model.Annotation> Progress { get; set; }
    
        /// <summary>
        /// Activity details defined in specific resource
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Reference { get; set; }
    
        /// <summary>
        /// In-line definition of activity
        /// </summary>
        Hl7.Fhir.Model.ICarePlanDetailComponent Detail { get; }
    
    }
    
    public partial interface ICarePlanDetailComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Detail type of activity
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Why activity should be done
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> ReasonCode { get; set; }
    
        /// <summary>
        /// Condition triggering need for activity
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> ReasonReference { get; set; }
    
        /// <summary>
        /// Goals this activity relates to
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Goal { get; set; }
    
        /// <summary>
        /// When activity is to occur
        /// </summary>
        Hl7.Fhir.Model.Element Scheduled { get; set; }
    
        /// <summary>
        /// Where it should happen
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Location { get; set; }
    
        /// <summary>
        /// Who will be responsible?
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Performer { get; set; }
    
        /// <summary>
        /// What is to be administered/supplied
        /// </summary>
        Hl7.Fhir.Model.Element Product { get; set; }
    
        /// <summary>
        /// How to consume/day?
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity DailyAmount { get; set; }
    
        /// <summary>
        /// How much to administer/supply/consume
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Quantity { get; set; }
    
        /// <summary>
        /// Extra info describing activity to perform
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Extra info describing activity to perform
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
    }

}
