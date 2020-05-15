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
    /// A quality measure definition
    /// </summary>
    public partial interface IMeasure : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Canonical identifier for this measure, represented as a URI (globally unique)
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Canonical identifier for this measure, represented as a URI (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Additional identifier for the measure
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Business version of the measure
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Business version of the measure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Name for this measure (computer friendly)
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Name for this measure (computer friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Name for this measure (human friendly)
        /// </summary>
        Hl7.Fhir.Model.FhirString TitleElement { get; set; }
        
        /// <summary>
        /// Name for this measure (human friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Title { get; set; }
    
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        Code<Hl7.Fhir.Model.PublicationStatus> StatusElement { get; set; }
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.PublicationStatus? Status { get; set; }
    
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean ExperimentalElement { get; set; }
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Experimental { get; set; }
    
        /// <summary>
        /// Date last changed
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
        /// <summary>
        /// Date last changed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Date { get; set; }
    
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        Hl7.Fhir.Model.FhirString PublisherElement { get; set; }
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Publisher { get; set; }
    
        /// <summary>
        /// Natural language description of the measure
        /// </summary>
        Hl7.Fhir.Model.Markdown DescriptionElement { get; set; }
        
        /// <summary>
        /// Natural language description of the measure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Why this measure is defined
        /// </summary>
        Hl7.Fhir.Model.Markdown PurposeElement { get; set; }
        
        /// <summary>
        /// Why this measure is defined
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Purpose { get; set; }
    
        /// <summary>
        /// Describes the clinical usage of the measure
        /// </summary>
        Hl7.Fhir.Model.FhirString UsageElement { get; set; }
        
        /// <summary>
        /// Describes the clinical usage of the measure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Usage { get; set; }
    
        /// <summary>
        /// When the measure was approved by publisher
        /// </summary>
        Hl7.Fhir.Model.Date ApprovalDateElement { get; set; }
        
        /// <summary>
        /// When the measure was approved by publisher
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ApprovalDate { get; set; }
    
        /// <summary>
        /// When the measure was last reviewed
        /// </summary>
        Hl7.Fhir.Model.Date LastReviewDateElement { get; set; }
        
        /// <summary>
        /// When the measure was last reviewed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string LastReviewDate { get; set; }
    
        /// <summary>
        /// When the measure is expected to be used
        /// </summary>
        Hl7.Fhir.Model.Period EffectivePeriod { get; set; }
    
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        List<Hl7.Fhir.Model.UsageContext> UseContext { get; set; }
    
        /// <summary>
        /// Intended jurisdiction for measure (if applicable)
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction { get; set; }
    
        /// <summary>
        /// The category of the measure, such as Education, Treatment, Assessment, etc.
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Topic { get; set; }
    
        /// <summary>
        /// Contact details for the publisher
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContactDetail> Contact { get; }
    
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        Hl7.Fhir.Model.Markdown CopyrightElement { get; set; }
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Copyright { get; set; }
    
        /// <summary>
        /// Additional documentation, citations, etc.
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IRelatedArtifact> RelatedArtifact { get; }
    
        /// <summary>
        /// Disclaimer for use of the measure or its referenced content
        /// </summary>
        Hl7.Fhir.Model.Markdown DisclaimerElement { get; set; }
        
        /// <summary>
        /// Disclaimer for use of the measure or its referenced content
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Disclaimer { get; set; }
    
        /// <summary>
        /// proportion | ratio | continuous-variable | cohort
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Scoring { get; set; }
    
        /// <summary>
        /// opportunity | all-or-nothing | linear | weighted
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept CompositeScoring { get; set; }
    
        /// <summary>
        /// process | outcome | structure | patient-reported-outcome | composite
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Type { get; set; }
    
        /// <summary>
        /// How risk adjustment is applied for this measure
        /// </summary>
        Hl7.Fhir.Model.FhirString RiskAdjustmentElement { get; set; }
        
        /// <summary>
        /// How risk adjustment is applied for this measure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string RiskAdjustment { get; set; }
    
        /// <summary>
        /// How is rate aggregation performed for this measure
        /// </summary>
        Hl7.Fhir.Model.FhirString RateAggregationElement { get; set; }
        
        /// <summary>
        /// How is rate aggregation performed for this measure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string RateAggregation { get; set; }
    
        /// <summary>
        /// Detailed description of why the measure exists
        /// </summary>
        Hl7.Fhir.Model.Markdown RationaleElement { get; set; }
        
        /// <summary>
        /// Detailed description of why the measure exists
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Rationale { get; set; }
    
        /// <summary>
        /// Summary of clinical guidelines
        /// </summary>
        Hl7.Fhir.Model.Markdown ClinicalRecommendationStatementElement { get; set; }
        
        /// <summary>
        /// Summary of clinical guidelines
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ClinicalRecommendationStatement { get; set; }
    
        /// <summary>
        /// Defined terms used in the measure documentation
        /// </summary>
        List<Hl7.Fhir.Model.Markdown> DefinitionElement { get; set; }
        
        /// <summary>
        /// Defined terms used in the measure documentation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> Definition { get; set; }
    
        /// <summary>
        /// Additional guidance for implementers
        /// </summary>
        Hl7.Fhir.Model.Markdown GuidanceElement { get; set; }
        
        /// <summary>
        /// Additional guidance for implementers
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Guidance { get; set; }
    
        /// <summary>
        /// Population criteria group
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IMeasureGroupComponent> Group { get; }
    
        /// <summary>
        /// What other data should be reported with the measure
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IMeasureSupplementalDataComponent> SupplementalData { get; }
    
    }
    
    public partial interface IMeasureGroupComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Summary description
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Summary description
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Population criteria
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IMeasurePopulationComponent> Population { get; }
    
        /// <summary>
        /// Stratifier criteria for the measure
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IMeasureStratifierComponent> Stratifier { get; }
    
    }
    
    public partial interface IMeasurePopulationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// initial-population | numerator | numerator-exclusion | denominator | denominator-exclusion | denominator-exception | measure-population | measure-population-exclusion | measure-observation
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Code { get; set; }
    
        /// <summary>
        /// The human readable description of this population criteria
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// The human readable description of this population criteria
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
    }
    
    public partial interface IMeasureStratifierComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
    }
    
    public partial interface IMeasureSupplementalDataComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// supplemental-data | risk-adjustment-factor
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Usage { get; set; }
    
    }

}
