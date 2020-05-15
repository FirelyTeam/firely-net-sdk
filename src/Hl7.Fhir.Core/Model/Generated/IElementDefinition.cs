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
    /// Definition of an element in a resource or extension
    /// </summary>
    public partial interface IElementDefinition : Hl7.Fhir.Model.IElement
    {
    
        /// <summary>
        /// The path of the element (see the Detailed Descriptions)
        /// </summary>
        Hl7.Fhir.Model.FhirString PathElement { get; set; }
        
        /// <summary>
        /// The path of the element (see the Detailed Descriptions)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Path { get; set; }
    
        /// <summary>
        /// Name for element to display with or prompt for element
        /// </summary>
        Hl7.Fhir.Model.FhirString LabelElement { get; set; }
        
        /// <summary>
        /// Name for element to display with or prompt for element
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Label { get; set; }
    
        /// <summary>
        /// Defining code
        /// </summary>
        List<Hl7.Fhir.Model.Coding> Code { get; set; }
    
        /// <summary>
        /// This element is sliced - slices follow
        /// </summary>
        Hl7.Fhir.Model.IElementDefinitionSlicingComponent Slicing { get; }
    
        /// <summary>
        /// Concise definition for xml presentation
        /// </summary>
        Hl7.Fhir.Model.FhirString ShortElement { get; set; }
        
        /// <summary>
        /// Concise definition for xml presentation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Short { get; set; }
    
        /// <summary>
        /// Full formal definition as narrative text
        /// </summary>
        Hl7.Fhir.Model.Markdown DefinitionElement { get; set; }
        
        /// <summary>
        /// Full formal definition as narrative text
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Definition { get; set; }
    
        /// <summary>
        /// Why is this needed?
        /// </summary>
        Hl7.Fhir.Model.Markdown RequirementsElement { get; set; }
        
        /// <summary>
        /// Why is this needed?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Requirements { get; set; }
    
        /// <summary>
        /// Other names
        /// </summary>
        List<Hl7.Fhir.Model.FhirString> AliasElement { get; set; }
        
        /// <summary>
        /// Other names
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> Alias { get; set; }
    
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
        /// Base definition information for tools
        /// </summary>
        Hl7.Fhir.Model.IElementDefinitionBaseComponent Base { get; }
    
        /// <summary>
        /// Data type and Profile for this element
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IElementDefinitionTypeRefComponent> Type { get; }
    
        /// <summary>
        /// Specified value it missing from instance
        /// </summary>
        Hl7.Fhir.Model.Element DefaultValue { get; set; }
    
        /// <summary>
        /// Implicit meaning when this element is missing
        /// </summary>
        Hl7.Fhir.Model.Markdown MeaningWhenMissingElement { get; set; }
        
        /// <summary>
        /// Implicit meaning when this element is missing
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string MeaningWhenMissing { get; set; }
    
        /// <summary>
        /// Value must be exactly this
        /// </summary>
        Hl7.Fhir.Model.Element Fixed { get; set; }
    
        /// <summary>
        /// Value must have at least these property values
        /// </summary>
        Hl7.Fhir.Model.Element Pattern { get; set; }
    
        /// <summary>
        /// Minimum Allowed Value (for some types)
        /// </summary>
        Hl7.Fhir.Model.Element MinValue { get; set; }
    
        /// <summary>
        /// Maximum Allowed Value (for some types)
        /// </summary>
        Hl7.Fhir.Model.Element MaxValue { get; set; }
    
        /// <summary>
        /// Max length for strings
        /// </summary>
        Hl7.Fhir.Model.Integer MaxLengthElement { get; set; }
        
        /// <summary>
        /// Max length for strings
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? MaxLength { get; set; }
    
        /// <summary>
        /// Reference to invariant about presence
        /// </summary>
        List<Hl7.Fhir.Model.Id> ConditionElement { get; set; }
        
        /// <summary>
        /// Reference to invariant about presence
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> Condition { get; set; }
    
        /// <summary>
        /// Condition that must evaluate to true
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IElementDefinitionConstraintComponent> Constraint { get; }
    
        /// <summary>
        /// If the element must supported
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean MustSupportElement { get; set; }
        
        /// <summary>
        /// If the element must supported
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? MustSupport { get; set; }
    
        /// <summary>
        /// If this modifies the meaning of other elements
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean IsModifierElement { get; set; }
        
        /// <summary>
        /// If this modifies the meaning of other elements
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? IsModifier { get; set; }
    
        /// <summary>
        /// Include when _summary = true?
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean IsSummaryElement { get; set; }
        
        /// <summary>
        /// Include when _summary = true?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? IsSummary { get; set; }
    
        /// <summary>
        /// Map element to another set of definitions
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IElementDefinitionMappingComponent> Mapping { get; }
    
    }
    
    public partial interface IElementDefinitionSlicingComponent : Hl7.Fhir.Model.IElement
    {
    
        /// <summary>
        /// Text description of how slicing works (or not)
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Text description of how slicing works (or not)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// If elements must be in same order as slices
        /// </summary>
        Hl7.Fhir.Model.FhirBoolean OrderedElement { get; set; }
        
        /// <summary>
        /// If elements must be in same order as slices
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        bool? Ordered { get; set; }
    
        /// <summary>
        /// closed | open | openAtEnd
        /// </summary>
        Code<Hl7.Fhir.Model.SlicingRules> RulesElement { get; set; }
        
        /// <summary>
        /// closed | open | openAtEnd
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.SlicingRules? Rules { get; set; }
    
    }
    
    public partial interface IElementDefinitionBaseComponent : Hl7.Fhir.Model.IElement
    {
    
        /// <summary>
        /// Path that identifies the base element
        /// </summary>
        Hl7.Fhir.Model.FhirString PathElement { get; set; }
        
        /// <summary>
        /// Path that identifies the base element
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Path { get; set; }
    
        /// <summary>
        /// Max cardinality of the base element
        /// </summary>
        Hl7.Fhir.Model.FhirString MaxElement { get; set; }
        
        /// <summary>
        /// Max cardinality of the base element
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Max { get; set; }
    
    }
    
    public partial interface IElementDefinitionTypeRefComponent : Hl7.Fhir.Model.IElement
    {
    
        /// <summary>
        /// contained | referenced | bundled - how aggregated
        /// </summary>
        List<Code<Hl7.Fhir.Model.AggregationMode>> AggregationElement { get; set; }
        
        /// <summary>
        /// contained | referenced | bundled - how aggregated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<Hl7.Fhir.Model.AggregationMode?> Aggregation { get; set; }
    
    }
    
    public partial interface IElementDefinitionConstraintComponent : Hl7.Fhir.Model.IElement
    {
    
        /// <summary>
        /// Target of 'condition' reference above
        /// </summary>
        Hl7.Fhir.Model.Id KeyElement { get; set; }
        
        /// <summary>
        /// Target of 'condition' reference above
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Key { get; set; }
    
        /// <summary>
        /// Why this constraint necessary or appropriate
        /// </summary>
        Hl7.Fhir.Model.FhirString RequirementsElement { get; set; }
        
        /// <summary>
        /// Why this constraint necessary or appropriate
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Requirements { get; set; }
    
        /// <summary>
        /// error | warning
        /// </summary>
        Code<Hl7.Fhir.Model.ConstraintSeverity> SeverityElement { get; set; }
        
        /// <summary>
        /// error | warning
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ConstraintSeverity? Severity { get; set; }
    
        /// <summary>
        /// Human description of constraint
        /// </summary>
        Hl7.Fhir.Model.FhirString HumanElement { get; set; }
        
        /// <summary>
        /// Human description of constraint
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Human { get; set; }
    
        /// <summary>
        /// XPath expression of constraint
        /// </summary>
        Hl7.Fhir.Model.FhirString XpathElement { get; set; }
        
        /// <summary>
        /// XPath expression of constraint
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Xpath { get; set; }
    
    }
    
    public partial interface IElementDefinitionMappingComponent : Hl7.Fhir.Model.IElement
    {
    
        /// <summary>
        /// Reference to mapping declaration
        /// </summary>
        Hl7.Fhir.Model.Id IdentityElement { get; set; }
        
        /// <summary>
        /// Reference to mapping declaration
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Identity { get; set; }
    
        /// <summary>
        /// Computable language of mapping
        /// </summary>
        Hl7.Fhir.Model.Code LanguageElement { get; set; }
        
        /// <summary>
        /// Computable language of mapping
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Language { get; set; }
    
        /// <summary>
        /// Details of the mapping
        /// </summary>
        Hl7.Fhir.Model.FhirString MapElement { get; set; }
        
        /// <summary>
        /// Details of the mapping
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Map { get; set; }
    
    }

}
