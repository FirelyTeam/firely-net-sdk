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
    /// Definition of an operation or a named query
    /// </summary>
    public partial interface IOperationDefinition : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Logical URL to reference this operation definition
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Logical URL to reference this operation definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Logical id for this version of the operation definition
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Logical id for this version of the operation definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Informal name for this operation
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Informal name for this operation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// operation | query
        /// </summary>
        Code<Hl7.Fhir.Model.OperationKind> KindElement { get; set; }
        
        /// <summary>
        /// operation | query
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.OperationKind? Kind { get; set; }
    
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ExperimentalElement { get; set; }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Experimental { get; set; }
    
        /// <summary>
        /// Date for this version of the operation definition
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// Date for this version of the operation definition
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
        /// Name used to invoke the operation
        /// </summary>
        Hl7.Fhir.Model.Code CodeElement { get; set; }
        
        /// <summary>
        /// Name used to invoke the operation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Code { get; set; }
    
        /// <summary>
        /// Invoke at the system level?
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean SystemElement { get; set; }
        
        /// <summary>
        /// Invoke at the system level?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? System { get; set; }
    
        /// <summary>
        /// Invoke on an instance?
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean InstanceElement { get; set; }
        
        /// <summary>
        /// Invoke on an instance?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Instance { get; set; }
    
        /// <summary>
        /// Parameters for the operation/query
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IOperationDefinitionParameterComponent> Parameter { get; }
    
    }
    
    public partial interface IOperationDefinitionParameterComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Name in Parameters.parameter.name or in URL
        /// </summary>
        Hl7.Fhir.Model.Code NameElement { get; set; }
        
        /// <summary>
        /// Name in Parameters.parameter.name or in URL
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// in | out
        /// </summary>
        Code<Hl7.Fhir.Model.OperationParameterUse> UseElement { get; set; }
        
        /// <summary>
        /// in | out
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.OperationParameterUse? Use { get; set; }
    
        /// <summary>
        /// Minimum Cardinality
        /// </summary>
        Hl7.Fhir.Model.Integer MinElement { get; set; }
        
        /// <summary>
        /// Minimum Cardinality
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Min { get; set; }
    
        /// <summary>
        /// Maximum Cardinality (a number or *)
        /// </summary>
        Hl7.Fhir.Model.FhirString MaxElement { get; set; }
        
        /// <summary>
        /// Maximum Cardinality (a number or *)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Max { get; set; }
    
        /// <summary>
        /// Description of meaning/use
        /// </summary>
        Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
        
        /// <summary>
        /// Description of meaning/use
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Documentation { get; set; }
    
        /// <summary>
        /// ValueSet details if this is coded
        /// </summary>
        Hl7.Fhir.Model.IOperationDefinitionBindingComponent Binding { get; }
    
        /// <summary>
        /// Parts of a Tuple Parameter
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IOperationDefinitionParameterComponent> Part { get; }
    
    }
    
    public partial interface IOperationDefinitionBindingComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// required | extensible | preferred | example
        /// </summary>
        Code<Hl7.Fhir.Model.BindingStrength> StrengthElement { get; set; }
        
        /// <summary>
        /// required | extensible | preferred | example
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.BindingStrength? Strength { get; set; }
    
    }

}
