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
    /// A task to be performed
    /// </summary>
    public partial interface ITask : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Task Instance Identifier
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Request fulfilled by this task
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> BasedOn { get; set; }
    
        /// <summary>
        /// Requisition or grouper id
        /// </summary>
        Hl7.Fhir.Model.Identifier GroupIdentifier { get; set; }
    
        /// <summary>
        /// Composite task
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> PartOf { get; set; }
    
        /// <summary>
        /// draft | requested | received | accepted | +
        /// </summary>
        Code<Hl7.Fhir.Model.TaskStatus> StatusElement { get; set; }
        
        /// <summary>
        /// draft | requested | received | accepted | +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.TaskStatus? Status { get; set; }
    
        /// <summary>
        /// Reason for current status
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept StatusReason { get; set; }
    
        /// <summary>
        /// E.g. "Specimen collected", "IV prepped"
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept BusinessStatus { get; set; }
    
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        Code<Hl7.Fhir.Model.RequestPriority> PriorityElement { get; set; }
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.RequestPriority? Priority { get; set; }
    
        /// <summary>
        /// Task Type
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Human-readable explanation of task
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Human-readable explanation of task
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// What task is acting on
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Focus { get; set; }
    
        /// <summary>
        /// Beneficiary of the Task
        /// </summary>
        Hl7.Fhir.Model.ResourceReference For { get; set; }
    
        /// <summary>
        /// Start and end time of execution
        /// </summary>
        Hl7.Fhir.Model.Period ExecutionPeriod { get; set; }
    
        /// <summary>
        /// Task Creation Date
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime AuthoredOnElement { get; set; }
        
        /// <summary>
        /// Task Creation Date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string AuthoredOn { get; set; }
    
        /// <summary>
        /// Task Last Modified Date
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime LastModifiedElement { get; set; }
        
        /// <summary>
        /// Task Last Modified Date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string LastModified { get; set; }
    
        /// <summary>
        /// Requested performer
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> PerformerType { get; set; }
    
        /// <summary>
        /// Responsible individual
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Owner { get; set; }
    
        /// <summary>
        /// Comments made about the task
        /// </summary>
        List<Hl7.Fhir.Model.Annotation> Note { get; set; }
    
        /// <summary>
        /// Key events in history of the Task
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> RelevantHistory { get; set; }
    
        /// <summary>
        /// Constraints on fulfillment tasks
        /// </summary>
        Hl7.Fhir.Model.ITaskRestrictionComponent Restriction { get; }
    
        /// <summary>
        /// Information used to perform task
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ITaskParameterComponent> Input { get; }
    
        /// <summary>
        /// Information produced as part of task
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ITaskOutputComponent> Output { get; }
    
    }
    
    public partial interface ITaskRestrictionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// How many times to repeat
        /// </summary>
        Hl7.Fhir.Model.PositiveInt RepetitionsElement { get; set; }
        
        /// <summary>
        /// How many times to repeat
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Repetitions { get; set; }
    
        /// <summary>
        /// When fulfillment sought
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// For whom is fulfillment sought?
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Recipient { get; set; }
    
    }
    
    public partial interface ITaskParameterComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Label for the input
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Content to use in performing the task
        /// </summary>
        Hl7.Fhir.Model.Element Value { get; set; }
    
    }
    
    public partial interface ITaskOutputComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Label for output
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Result of output
        /// </summary>
        Hl7.Fhir.Model.Element Value { get; set; }
    
    }

}
