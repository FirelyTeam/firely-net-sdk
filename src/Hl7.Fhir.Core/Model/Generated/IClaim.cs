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
    /// Claim, Pre-determination or Pre-authorization
    /// </summary>
    public partial interface IClaim : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Claim number
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// The subject of the Products and Services
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Patient { get; set; }
    
        /// <summary>
        /// Creation date
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime CreatedElement { get; set; }
        
        /// <summary>
        /// Creation date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Created { get; set; }
    
        /// <summary>
        /// Author
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Enterer { get; set; }
    
        /// <summary>
        /// Responsible provider
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Provider { get; set; }
    
        /// <summary>
        /// Prescription
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Prescription { get; set; }
    
        /// <summary>
        /// Original Prescription
        /// </summary>
        Hl7.Fhir.Model.ResourceReference OriginalPrescription { get; set; }
    
        /// <summary>
        /// Payee
        /// </summary>
        Hl7.Fhir.Model.IClaimPayeeComponent Payee { get; }
    
        /// <summary>
        /// Treatment Referral
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Referral { get; set; }
    
        /// <summary>
        /// Servicing Facility
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Facility { get; set; }
    
        /// <summary>
        /// Diagnosis
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IClaimDiagnosisComponent> Diagnosis { get; }
    
    }
    
    public partial interface IClaimPayeeComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
    }
    
    public partial interface IClaimDiagnosisComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Sequence of diagnosis
        /// </summary>
        Hl7.Fhir.Model.PositiveInt SequenceElement { get; set; }
        
        /// <summary>
        /// Sequence of diagnosis
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Sequence { get; set; }
    
    }
    
    public partial interface IClaimDetailComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Service instance
        /// </summary>
        Hl7.Fhir.Model.PositiveInt SequenceElement { get; set; }
        
        /// <summary>
        /// Service instance
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Sequence { get; set; }
    
        /// <summary>
        /// Count of Products or Services
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Quantity { get; set; }
    
        /// <summary>
        /// Fee, charge or cost per point
        /// </summary>
        Hl7.Fhir.Model.IMoney UnitPrice { get; }
    
        /// <summary>
        /// Price scaling factor
        /// </summary>
        Hl7.Fhir.Model.FhirDecimal FactorElement { get; set; }
        
        /// <summary>
        /// Price scaling factor
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        decimal? Factor { get; set; }
    
        /// <summary>
        /// Total additional item cost
        /// </summary>
        Hl7.Fhir.Model.IMoney Net { get; }
    
        /// <summary>
        /// Additional items
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IClaimSubDetailComponent> SubDetail { get; }
    
    }
    
    public partial interface IClaimSubDetailComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Service instance
        /// </summary>
        Hl7.Fhir.Model.PositiveInt SequenceElement { get; set; }
        
        /// <summary>
        /// Service instance
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Sequence { get; set; }
    
        /// <summary>
        /// Count of Products or Services
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Quantity { get; set; }
    
        /// <summary>
        /// Fee, charge or cost per point
        /// </summary>
        Hl7.Fhir.Model.IMoney UnitPrice { get; }
    
        /// <summary>
        /// Price scaling factor
        /// </summary>
        Hl7.Fhir.Model.FhirDecimal FactorElement { get; set; }
        
        /// <summary>
        /// Price scaling factor
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        decimal? Factor { get; set; }
    
        /// <summary>
        /// Net additional item cost
        /// </summary>
        Hl7.Fhir.Model.IMoney Net { get; }
    
    }

}
