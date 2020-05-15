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
    /// System of unique identification
    /// </summary>
    public partial interface INamingSystem : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Human-readable label
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Human-readable label
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// codesystem | identifier | root
        /// </summary>
        Code<Hl7.Fhir.Model.NamingSystemType> KindElement { get; set; }
        
        /// <summary>
        /// codesystem | identifier | root
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.NamingSystemType? Kind { get; set; }
    
        /// <summary>
        /// Publication Date(/time)
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// Publication Date(/time)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        Hl7.Fhir.Model.FhirString PublisherElement { get; set; }
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Publisher { get; set; }
    
        /// <summary>
        /// Who maintains system namespace?
        /// </summary>
        Hl7.Fhir.Model.FhirString ResponsibleElement { get; set; }
        
        /// <summary>
        /// Who maintains system namespace?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Responsible { get; set; }
    
        /// <summary>
        /// e.g. driver,  provider,  patient, bank etc.
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// How/where is it used
        /// </summary>
        Hl7.Fhir.Model.FhirString UsageElement { get; set; }
        
        /// <summary>
        /// How/where is it used
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Usage { get; set; }
    
        /// <summary>
        /// Unique identifiers used for system
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.INamingSystemUniqueIdComponent> UniqueId { get; }
    
    }
    
    public partial interface INamingSystemUniqueIdComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// oid | uuid | uri | other
        /// </summary>
        Code<Hl7.Fhir.Model.NamingSystemIdentifierType> TypeElement { get; set; }
        
        /// <summary>
        /// oid | uuid | uri | other
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.NamingSystemIdentifierType? Type { get; set; }
    
        /// <summary>
        /// The unique identifier
        /// </summary>
        Hl7.Fhir.Model.FhirString ValueElement { get; set; }
        
        /// <summary>
        /// The unique identifier
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Value { get; set; }
    
        /// <summary>
        /// Is this the id that should be used for this type
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean PreferredElement { get; set; }
        
        /// <summary>
        /// Is this the id that should be used for this type
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Preferred { get; set; }
    
        /// <summary>
        /// When is identifier valid?
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
    }

}
