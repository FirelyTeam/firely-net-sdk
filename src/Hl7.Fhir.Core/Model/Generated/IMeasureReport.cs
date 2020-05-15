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
    /// Results of a measure evaluation
    /// </summary>
    public partial interface IMeasureReport : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// complete | pending | error
        /// </summary>
        Code<Hl7.Fhir.Model.MeasureReportStatus> StatusElement { get; set; }
        
        /// <summary>
        /// complete | pending | error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.MeasureReportStatus? Status { get; set; }
    
        /// <summary>
        /// When the report was generated
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// When the report was generated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// What period the report covers
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// Measure results for each group
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IMeasureReportGroupComponent> Group { get; }
    
    }
    
    public partial interface IMeasureReportGroupComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The populations in the group
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IMeasureReportPopulationComponent> Population { get; }
    
        /// <summary>
        /// Stratification results
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IMeasureReportStratifierComponent> Stratifier { get; }
    
    }
    
    public partial interface IMeasureReportPopulationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// initial-population | numerator | numerator-exclusion | denominator | denominator-exclusion | denominator-exception | measure-population | measure-population-exclusion | measure-observation
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Size of the population
        /// </summary>
        Hl7.Fhir.Model.Integer CountElement { get; set; }
        
        /// <summary>
        /// Size of the population
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Count { get; set; }
    
    }
    
    public partial interface IMeasureReportStratifierComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Stratum results, one for each unique value, or set of values, in the stratifier, or stratifier components
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IMeasureReportStratifierGroupComponent> Stratum { get; }
    
    }
    
    public partial interface IMeasureReportStratifierGroupComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Population results in this stratum
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IMeasureReportStratifierGroupPopulationComponent> Population { get; }
    
    }
    
    public partial interface IMeasureReportStratifierGroupPopulationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// initial-population | numerator | numerator-exclusion | denominator | denominator-exclusion | denominator-exception | measure-population | measure-population-exclusion | measure-observation
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// Size of the population
        /// </summary>
        Hl7.Fhir.Model.Integer CountElement { get; set; }
        
        /// <summary>
        /// Size of the population
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Count { get; set; }
    
    }

}
