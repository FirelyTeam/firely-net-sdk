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
    /// A Map of relationships between 2 structures that can be used to transform data
    /// </summary>
    public partial interface IStructureMap : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Canonical identifier for this structure map, represented as a URI (globally unique)
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Canonical identifier for this structure map, represented as a URI (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Additional identifier for the structure map
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Business version of the structure map
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Business version of the structure map
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Name for this structure map (computer friendly)
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Name for this structure map (computer friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Name for this structure map (human friendly)
        /// </summary>
        Hl7.Fhir.Model.FhirString TitleElement { get; set; }
        
        /// <summary>
        /// Name for this structure map (human friendly)
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
        /// Contact details for the publisher
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IContactDetail> Contact { get; }
    
        /// <summary>
        /// Natural language description of the structure map
        /// </summary>
        Hl7.Fhir.Model.Markdown DescriptionElement { get; set; }
        
        /// <summary>
        /// Natural language description of the structure map
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        List<Hl7.Fhir.Model.UsageContext> UseContext { get; set; }
    
        /// <summary>
        /// Intended jurisdiction for structure map (if applicable)
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction { get; set; }
    
        /// <summary>
        /// Why this structure map is defined
        /// </summary>
        Hl7.Fhir.Model.Markdown PurposeElement { get; set; }
        
        /// <summary>
        /// Why this structure map is defined
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Purpose { get; set; }
    
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
        /// Structure Definition used by this map
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IStructureMapStructureComponent> Structure { get; }
    
        /// <summary>
        /// Named sections for reader convenience
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IStructureMapGroupComponent> Group { get; }
    
    }
    
    public partial interface IStructureMapStructureComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// source | queried | target | produced
        /// </summary>
        Code<Hl7.Fhir.Model.StructureMapModelMode> ModeElement { get; set; }
        
        /// <summary>
        /// source | queried | target | produced
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.StructureMapModelMode? Mode { get; set; }
    
        /// <summary>
        /// Name for type in this map
        /// </summary>
        Hl7.Fhir.Model.FhirString AliasElement { get; set; }
        
        /// <summary>
        /// Name for type in this map
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Alias { get; set; }
    
        /// <summary>
        /// Documentation on use of structure
        /// </summary>
        Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
        
        /// <summary>
        /// Documentation on use of structure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Documentation { get; set; }
    
    }
    
    public partial interface IStructureMapGroupComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Human-readable label
        /// </summary>
        Hl7.Fhir.Model.Id NameElement { get; set; }
        
        /// <summary>
        /// Human-readable label
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Another group that this group adds rules to
        /// </summary>
        Hl7.Fhir.Model.Id ExtendsElement { get; set; }
        
        /// <summary>
        /// Another group that this group adds rules to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Extends { get; set; }
    
        /// <summary>
        /// none | types | type-and-types
        /// </summary>
        Code<Hl7.Fhir.Model.StructureMapGroupTypeMode> TypeModeElement { get; set; }
        
        /// <summary>
        /// none | types | type-and-types
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.StructureMapGroupTypeMode? TypeMode { get; set; }
    
        /// <summary>
        /// Additional description/explanation for group
        /// </summary>
        Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
        
        /// <summary>
        /// Additional description/explanation for group
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Documentation { get; set; }
    
        /// <summary>
        /// Named instance provided when invoking the map
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IStructureMapInputComponent> Input { get; }
    
        /// <summary>
        /// Transform Rule from source to target
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IStructureMapRuleComponent> Rule { get; }
    
    }
    
    public partial interface IStructureMapInputComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Name for this instance of data
        /// </summary>
        Hl7.Fhir.Model.Id NameElement { get; set; }
        
        /// <summary>
        /// Name for this instance of data
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Type for this instance of data
        /// </summary>
        Hl7.Fhir.Model.FhirString TypeElement { get; set; }
        
        /// <summary>
        /// Type for this instance of data
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Type { get; set; }
    
        /// <summary>
        /// source | target
        /// </summary>
        Code<Hl7.Fhir.Model.StructureMapInputMode> ModeElement { get; set; }
        
        /// <summary>
        /// source | target
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.StructureMapInputMode? Mode { get; set; }
    
        /// <summary>
        /// Documentation for this instance of data
        /// </summary>
        Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
        
        /// <summary>
        /// Documentation for this instance of data
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Documentation { get; set; }
    
    }
    
    public partial interface IStructureMapRuleComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Name of the rule for internal references
        /// </summary>
        Hl7.Fhir.Model.Id NameElement { get; set; }
        
        /// <summary>
        /// Name of the rule for internal references
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Source inputs to the mapping
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IStructureMapSourceComponent> Source { get; }
    
        /// <summary>
        /// Content to create because of this mapping rule
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IStructureMapTargetComponent> Target { get; }
    
        /// <summary>
        /// Rules contained in this rule
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IStructureMapRuleComponent> Rule { get; }
    
        /// <summary>
        /// Which other rules to apply in the context of this rule
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IStructureMapDependentComponent> Dependent { get; }
    
        /// <summary>
        /// Documentation for this instance of data
        /// </summary>
        Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
        
        /// <summary>
        /// Documentation for this instance of data
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Documentation { get; set; }
    
    }
    
    public partial interface IStructureMapSourceComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Type or variable this rule applies to
        /// </summary>
        Hl7.Fhir.Model.Id ContextElement { get; set; }
        
        /// <summary>
        /// Type or variable this rule applies to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Context { get; set; }
    
        /// <summary>
        /// Specified minimum cardinality
        /// </summary>
        Hl7.Fhir.Model.Integer MinElement { get; set; }
        
        /// <summary>
        /// Specified minimum cardinality
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Min { get; set; }
    
        /// <summary>
        /// Specified maximum cardinality (number or *)
        /// </summary>
        Hl7.Fhir.Model.FhirString MaxElement { get; set; }
        
        /// <summary>
        /// Specified maximum cardinality (number or *)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Max { get; set; }
    
        /// <summary>
        /// Rule only applies if source has this type
        /// </summary>
        Hl7.Fhir.Model.FhirString TypeElement { get; set; }
        
        /// <summary>
        /// Rule only applies if source has this type
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Type { get; set; }
    
        /// <summary>
        /// Default value if no value exists
        /// </summary>
        Hl7.Fhir.Model.Element DefaultValue { get; set; }
    
        /// <summary>
        /// Optional field for this source
        /// </summary>
        Hl7.Fhir.Model.FhirString ElementElement { get; set; }
        
        /// <summary>
        /// Optional field for this source
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Element { get; set; }
    
        /// <summary>
        /// first | not_first | last | not_last | only_one
        /// </summary>
        Code<Hl7.Fhir.Model.StructureMapSourceListMode> ListModeElement { get; set; }
        
        /// <summary>
        /// first | not_first | last | not_last | only_one
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.StructureMapSourceListMode? ListMode { get; set; }
    
        /// <summary>
        /// Named context for field, if a field is specified
        /// </summary>
        Hl7.Fhir.Model.Id VariableElement { get; set; }
        
        /// <summary>
        /// Named context for field, if a field is specified
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Variable { get; set; }
    
        /// <summary>
        /// FHIRPath expression  - must be true or the rule does not apply
        /// </summary>
        Hl7.Fhir.Model.FhirString ConditionElement { get; set; }
        
        /// <summary>
        /// FHIRPath expression  - must be true or the rule does not apply
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Condition { get; set; }
    
        /// <summary>
        /// FHIRPath expression  - must be true or the mapping engine throws an error instead of completing
        /// </summary>
        Hl7.Fhir.Model.FhirString CheckElement { get; set; }
        
        /// <summary>
        /// FHIRPath expression  - must be true or the mapping engine throws an error instead of completing
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Check { get; set; }
    
    }
    
    public partial interface IStructureMapTargetComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Type or variable this rule applies to
        /// </summary>
        Hl7.Fhir.Model.Id ContextElement { get; set; }
        
        /// <summary>
        /// Type or variable this rule applies to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Context { get; set; }
    
        /// <summary>
        /// type | variable
        /// </summary>
        Code<Hl7.Fhir.Model.StructureMapContextType> ContextTypeElement { get; set; }
        
        /// <summary>
        /// type | variable
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.StructureMapContextType? ContextType { get; set; }
    
        /// <summary>
        /// Field to create in the context
        /// </summary>
        Hl7.Fhir.Model.FhirString ElementElement { get; set; }
        
        /// <summary>
        /// Field to create in the context
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Element { get; set; }
    
        /// <summary>
        /// Named context for field, if desired, and a field is specified
        /// </summary>
        Hl7.Fhir.Model.Id VariableElement { get; set; }
        
        /// <summary>
        /// Named context for field, if desired, and a field is specified
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Variable { get; set; }
    
        /// <summary>
        /// first | share | last | collate
        /// </summary>
        List<Code<Hl7.Fhir.Model.StructureMapTargetListMode>> ListModeElement { get; set; }
        
        /// <summary>
        /// first | share | last | collate
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<Hl7.Fhir.Model.StructureMapTargetListMode?> ListMode { get; set; }
    
        /// <summary>
        /// Internal rule reference for shared list items
        /// </summary>
        Hl7.Fhir.Model.Id ListRuleIdElement { get; set; }
        
        /// <summary>
        /// Internal rule reference for shared list items
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ListRuleId { get; set; }
    
        /// <summary>
        /// create | copy +
        /// </summary>
        Code<Hl7.Fhir.Model.StructureMapTransform> TransformElement { get; set; }
        
        /// <summary>
        /// create | copy +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.StructureMapTransform? Transform { get; set; }
    
        /// <summary>
        /// Parameters to the transform
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IStructureMapParameterComponent> Parameter { get; }
    
    }
    
    public partial interface IStructureMapParameterComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Parameter value - variable or literal
        /// </summary>
        Hl7.Fhir.Model.Element Value { get; set; }
    
    }
    
    public partial interface IStructureMapDependentComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Name of a rule or group to apply
        /// </summary>
        Hl7.Fhir.Model.Id NameElement { get; set; }
        
        /// <summary>
        /// Name of a rule or group to apply
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Variable to pass to the rule or group
        /// </summary>
        List<Hl7.Fhir.Model.FhirString> VariableElement { get; set; }
        
        /// <summary>
        /// Variable to pass to the rule or group
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> Variable { get; set; }
    
    }

}
