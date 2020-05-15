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
    /// Group of multiple entities
    /// </summary>
    public partial interface IGroup : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Unique id
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// person | animal | practitioner | device | medication | substance
        /// </summary>
        Code<Hl7.Fhir.Model.GroupType> TypeElement { get; set; }
        
        /// <summary>
        /// person | animal | practitioner | device | medication | substance
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.GroupType? Type { get; set; }
    
        /// <summary>
        /// Descriptive or actual
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ActualElement { get; set; }
        
        /// <summary>
        /// Descriptive or actual
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Actual { get; set; }
    
        /// <summary>
        /// Kind of Group members
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Label for Group
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Label for Group
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Number of members
        /// </summary>
        Hl7.Fhir.Model.UnsignedInt QuantityElement { get; set; }
        
        /// <summary>
        /// Number of members
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Quantity { get; set; }
    
        /// <summary>
        /// Trait of group members
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IGroupCharacteristicComponent> Characteristic { get; }
    
        /// <summary>
        /// Who or what is in group
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IGroupMemberComponent> Member { get; }
    
    }
    
    public partial interface IGroupCharacteristicComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Kind of characteristic
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Value held by characteristic
        /// </summary>
        Hl7.Fhir.Model.Element Value { get; set; }
    
        /// <summary>
        /// Group includes or excludes
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ExcludeElement { get; set; }
        
        /// <summary>
        /// Group includes or excludes
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Exclude { get; set; }
    
        /// <summary>
        /// Period over which characteristic is tested
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
    }
    
    public partial interface IGroupMemberComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Reference to the group member
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Entity { get; set; }
    
        /// <summary>
        /// Period member belonged to the group
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// If member is no longer in group
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean InactiveElement { get; set; }
        
        /// <summary>
        /// If member is no longer in group
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Inactive { get; set; }
    
    }

}
