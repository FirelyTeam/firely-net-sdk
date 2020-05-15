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
    /// A resource that describes a message that is exchanged between systems
    /// </summary>
    public partial interface IMessageHeader : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Message Destination Application(s)
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IMessageHeaderMessageDestinationComponent> Destination { get; }
    
        /// <summary>
        /// The source of the data entry
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Enterer { get; set; }
    
        /// <summary>
        /// The source of the decision
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Author { get; set; }
    
        /// <summary>
        /// Message Source Application
        /// </summary>
        Hl7.Fhir.Model.IMessageHeaderMessageSourceComponent Source { get; }
    
        /// <summary>
        /// Final responsibility for event
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Responsible { get; set; }
    
        /// <summary>
        /// Cause of event
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Reason { get; set; }
    
        /// <summary>
        /// If this is a reply to prior message
        /// </summary>
        Hl7.Fhir.Model.IMessageHeaderResponseComponent Response { get; }
    
    }
    
    public partial interface IMessageHeaderMessageDestinationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Name of system
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Name of system
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Particular delivery destination within the destination
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Target { get; set; }
    
    }
    
    public partial interface IMessageHeaderMessageSourceComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Name of system
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Name of system
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Name of software running the system
        /// </summary>
        Hl7.Fhir.Model.FhirString SoftwareElement { get; set; }
        
        /// <summary>
        /// Name of software running the system
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Software { get; set; }
    
        /// <summary>
        /// Version of software running
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Version of software running
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Human contact for problems
        /// </summary>
        Hl7.Fhir.Model.IContactPoint Contact { get; }
    
    }
    
    public partial interface IMessageHeaderResponseComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Id of original message
        /// </summary>
        Hl7.Fhir.Model.Id IdentifierElement { get; set; }
        
        /// <summary>
        /// Id of original message
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Identifier { get; set; }
    
        /// <summary>
        /// ok | transient-error | fatal-error
        /// </summary>
        Code<Hl7.Fhir.Model.ResponseType> CodeElement { get; set; }
        
        /// <summary>
        /// ok | transient-error | fatal-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ResponseType? Code { get; set; }
    
        /// <summary>
        /// Specific list of hints/warnings/errors
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Details { get; set; }
    
    }

}
