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
    /// Contract
    /// </summary>
    public partial interface IContract : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// When this Contract was issued
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime IssuedElement { get; set; }
        
        /// <summary>
        /// When this Contract was issued
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Issued { get; set; }
    
        /// <summary>
        /// Effective time
        /// </summary>
        Hl7.Fhir.Model.Period Applies { get; set; }
    
        /// <summary>
        /// Subject of this Contract
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Subject { get; set; }
    
        /// <summary>
        /// Authority under which this Contract has standing
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Authority { get; set; }
    
        /// <summary>
        /// Domain in which this Contract applies
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Domain { get; set; }
    
        /// <summary>
        /// Contract Tyoe
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Contract Subtype
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> SubType { get; set; }
    
        /// <summary>
        /// Contract Signer
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContractSignatoryComponent> Signer { get; }
    
        /// <summary>
        /// Contract Term List
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContractTermComponent> Term { get; }
    
        /// <summary>
        /// Contract Friendly Language
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContractFriendlyLanguageComponent> Friendly { get; }
    
        /// <summary>
        /// Contract Legal Language
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContractLegalLanguageComponent> Legal { get; }
    
        /// <summary>
        /// Computable Contract Language
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContractComputableLanguageComponent> Rule { get; }
    
    }
    
    public partial interface IContractSignatoryComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Contract Signer Type
        /// </summary>
        Hl7.Fhir.Model.Coding Type { get; set; }
    
        /// <summary>
        /// Contract Signatory Party
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Party { get; set; }
    
    }
    
    public partial interface IContractValuedItemComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Contract Valued Item Type
        /// </summary>
        Hl7.Fhir.Model.Element Entity { get; set; }
    
        /// <summary>
        /// Contract Valued Item Identifier
        /// </summary>
        Hl7.Fhir.Model.Identifier Identifier { get; set; }
    
        /// <summary>
        /// Contract Valued Item Effective Tiem
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime EffectiveTimeElement { get; set; }
        
        /// <summary>
        /// Contract Valued Item Effective Tiem
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string EffectiveTime { get; set; }
    
        /// <summary>
        /// Count of Contract Valued Items
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Quantity { get; set; }
    
        /// <summary>
        /// Contract Valued Item fee, charge, or cost
        /// </summary>
        Hl7.Fhir.Model.IMoney UnitPrice { get; }
    
        /// <summary>
        /// Contract Valued Item Price Scaling Factor
        /// </summary>
        Hl7.Fhir.Model.FhirDecimal FactorElement { get; set; }
        
        /// <summary>
        /// Contract Valued Item Price Scaling Factor
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        decimal? Factor { get; set; }
    
        /// <summary>
        /// Contract Valued Item Difficulty Scaling Factor
        /// </summary>
        Hl7.Fhir.Model.FhirDecimal PointsElement { get; set; }
        
        /// <summary>
        /// Contract Valued Item Difficulty Scaling Factor
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        decimal? Points { get; set; }
    
        /// <summary>
        /// Total Contract Valued Item Value
        /// </summary>
        Hl7.Fhir.Model.IMoney Net { get; }
    
    }
    
    public partial interface IContractTermComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Contract Term identifier
        /// </summary>
        Hl7.Fhir.Model.Identifier Identifier { get; set; }
    
        /// <summary>
        /// Contract Term Issue Date Time
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime IssuedElement { get; set; }
        
        /// <summary>
        /// Contract Term Issue Date Time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Issued { get; set; }
    
        /// <summary>
        /// Contract Term Effective Time
        /// </summary>
        Hl7.Fhir.Model.Period Applies { get; set; }
    
        /// <summary>
        /// Contract Term Type
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Contract Term Subtype
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept SubType { get; set; }
    
        /// <summary>
        /// Human readable Contract term text
        /// </summary>
        Hl7.Fhir.Model.FhirString TextElement { get; set; }
        
        /// <summary>
        /// Human readable Contract term text
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Text { get; set; }
    
        /// <summary>
        /// Nested Contract Term Group
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContractTermComponent> Group { get; }
    
    }
    
    public partial interface IContractFriendlyLanguageComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Easily comprehended representation of this Contract
        /// </summary>
        Hl7.Fhir.Model.Element Content { get; set; }
    
    }
    
    public partial interface IContractLegalLanguageComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Contract Legal Text
        /// </summary>
        Hl7.Fhir.Model.Element Content { get; set; }
    
    }
    
    public partial interface IContractComputableLanguageComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Computable Contract Rules
        /// </summary>
        Hl7.Fhir.Model.Element Content { get; set; }
    
    }

}
