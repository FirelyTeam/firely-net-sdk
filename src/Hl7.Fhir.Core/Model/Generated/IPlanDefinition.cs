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
    /// The definition of a plan for a series of actions, independent of any specific patient or context
    /// </summary>
    public partial interface IPlanDefinition : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Canonical identifier for this plan definition, represented as a URI (globally unique)
        /// </summary>
        Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        /// <summary>
        /// Canonical identifier for this plan definition, represented as a URI (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Url { get; set; }
    
        /// <summary>
        /// Additional identifier for the plan definition
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Business version of the plan definition
        /// </summary>
        Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        /// <summary>
        /// Business version of the plan definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Version { get; set; }
    
        /// <summary>
        /// Name for this plan definition (computer friendly)
        /// </summary>
        Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        /// <summary>
        /// Name for this plan definition (computer friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Name { get; set; }
    
        /// <summary>
        /// Name for this plan definition (human friendly)
        /// </summary>
        Hl7.Fhir.Model.FhirString TitleElement { get; set; }
        
        /// <summary>
        /// Name for this plan definition (human friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Title { get; set; }
    
        /// <summary>
        /// order-set | clinical-protocol | eca-rule | workflow-definition
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
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
        /// Natural language description of the plan definition
        /// </summary>
        Hl7.Fhir.Model.Markdown DescriptionElement { get; set; }
        
        /// <summary>
        /// Natural language description of the plan definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Why this plan definition is defined
        /// </summary>
        Hl7.Fhir.Model.Markdown PurposeElement { get; set; }
        
        /// <summary>
        /// Why this plan definition is defined
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Purpose { get; set; }
    
        /// <summary>
        /// Describes the clinical usage of the plan
        /// </summary>
        Hl7.Fhir.Model.FhirString UsageElement { get; set; }
        
        /// <summary>
        /// Describes the clinical usage of the plan
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Usage { get; set; }
    
        /// <summary>
        /// When the plan definition was approved by publisher
        /// </summary>
        Hl7.Fhir.Model.Date ApprovalDateElement { get; set; }
        
        /// <summary>
        /// When the plan definition was approved by publisher
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ApprovalDate { get; set; }
    
        /// <summary>
        /// When the plan definition was last reviewed
        /// </summary>
        Hl7.Fhir.Model.Date LastReviewDateElement { get; set; }
        
        /// <summary>
        /// When the plan definition was last reviewed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string LastReviewDate { get; set; }
    
        /// <summary>
        /// When the plan definition is expected to be used
        /// </summary>
        Hl7.Fhir.Model.Period EffectivePeriod { get; set; }
    
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        List<Hl7.Fhir.Model.UsageContext> UseContext { get; set; }
    
        /// <summary>
        /// Intended jurisdiction for plan definition (if applicable)
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction { get; set; }
    
        /// <summary>
        /// E.g. Education, Treatment, Assessment
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
        /// Additional documentation, citations
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IRelatedArtifact> RelatedArtifact { get; }
    
        /// <summary>
        /// What the plan is trying to accomplish
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPlanDefinitionGoalComponent> Goal { get; }
    
        /// <summary>
        /// Action defined by the plan
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPlanDefinitionActionComponent> Action { get; }
    
    }
    
    public partial interface IPlanDefinitionGoalComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// E.g. Treatment, dietary, behavioral
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Category { get; set; }
    
        /// <summary>
        /// Code or text describing the goal
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Description { get; set; }
    
        /// <summary>
        /// high-priority | medium-priority | low-priority
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Priority { get; set; }
    
        /// <summary>
        /// When goal pursuit begins
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Start { get; set; }
    
        /// <summary>
        /// What does the goal address
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Addresses { get; set; }
    
        /// <summary>
        /// Supporting documentation for the goal
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IRelatedArtifact> Documentation { get; }
    
        /// <summary>
        /// Target outcome for the goal
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPlanDefinitionTargetComponent> Target { get; }
    
    }
    
    public partial interface IPlanDefinitionTargetComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The parameter whose value is to be tracked
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Measure { get; set; }
    
        /// <summary>
        /// The target value to be achieved
        /// </summary>
        Hl7.Fhir.Model.Element Detail { get; set; }
    
        /// <summary>
        /// Reach goal within
        /// </summary>
        Hl7.Fhir.Model.IDuration Due { get; }
    
    }
    
    public partial interface IPlanDefinitionActionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// User-visible title
        /// </summary>
        Hl7.Fhir.Model.FhirString TitleElement { get; set; }
        
        /// <summary>
        /// User-visible title
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Title { get; set; }
    
        /// <summary>
        /// Brief description of the action
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Brief description of the action
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Static text equivalent of the action, used if the dynamic aspects cannot be interpreted by the receiving system
        /// </summary>
        Hl7.Fhir.Model.FhirString TextEquivalentElement { get; set; }
        
        /// <summary>
        /// Static text equivalent of the action, used if the dynamic aspects cannot be interpreted by the receiving system
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string TextEquivalent { get; set; }
    
        /// <summary>
        /// Code representing the meaning of the action or sub-actions
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Code { get; set; }
    
        /// <summary>
        /// Why the action should be performed
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Reason { get; set; }
    
        /// <summary>
        /// Supporting documentation for the intended performer of the action
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IRelatedArtifact> Documentation { get; }
    
        /// <summary>
        /// What goals this action supports
        /// </summary>
        List<Hl7.Fhir.Model.Id> GoalIdElement { get; set; }
        
        /// <summary>
        /// What goals this action supports
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        IEnumerable<string> GoalId { get; set; }
    
        /// <summary>
        /// Whether or not the action is applicable
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPlanDefinitionConditionComponent> Condition { get; }
    
        /// <summary>
        /// Input data requirements
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IDataRequirement> Input { get; }
    
        /// <summary>
        /// Output data definition
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IDataRequirement> Output { get; }
    
        /// <summary>
        /// Relationship to another action
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPlanDefinitionRelatedActionComponent> RelatedAction { get; }
    
        /// <summary>
        /// When the action should take place
        /// </summary>
        Hl7.Fhir.Model.Element Timing { get; set; }
    
        /// <summary>
        /// Who should participate in the action
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPlanDefinitionParticipantComponent> Participant { get; }
    
        /// <summary>
        /// visual-group | logical-group | sentence-group
        /// </summary>
        Code<Hl7.Fhir.Model.ActionGroupingBehavior> GroupingBehaviorElement { get; set; }
        
        /// <summary>
        /// visual-group | logical-group | sentence-group
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ActionGroupingBehavior? GroupingBehavior { get; set; }
    
        /// <summary>
        /// any | all | all-or-none | exactly-one | at-most-one | one-or-more
        /// </summary>
        Code<Hl7.Fhir.Model.ActionSelectionBehavior> SelectionBehaviorElement { get; set; }
        
        /// <summary>
        /// any | all | all-or-none | exactly-one | at-most-one | one-or-more
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ActionSelectionBehavior? SelectionBehavior { get; set; }
    
        /// <summary>
        /// must | could | must-unless-documented
        /// </summary>
        Code<Hl7.Fhir.Model.ActionRequiredBehavior> RequiredBehaviorElement { get; set; }
        
        /// <summary>
        /// must | could | must-unless-documented
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ActionRequiredBehavior? RequiredBehavior { get; set; }
    
        /// <summary>
        /// yes | no
        /// </summary>
        Code<Hl7.Fhir.Model.ActionPrecheckBehavior> PrecheckBehaviorElement { get; set; }
        
        /// <summary>
        /// yes | no
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ActionPrecheckBehavior? PrecheckBehavior { get; set; }
    
        /// <summary>
        /// single | multiple
        /// </summary>
        Code<Hl7.Fhir.Model.ActionCardinalityBehavior> CardinalityBehaviorElement { get; set; }
        
        /// <summary>
        /// single | multiple
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ActionCardinalityBehavior? CardinalityBehavior { get; set; }
    
        /// <summary>
        /// Dynamic aspects of the definition
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPlanDefinitionDynamicValueComponent> DynamicValue { get; }
    
        /// <summary>
        /// A sub-action
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IPlanDefinitionActionComponent> Action { get; }
    
    }
    
    public partial interface IPlanDefinitionConditionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// applicability | start | stop
        /// </summary>
        Code<Hl7.Fhir.Model.ActionConditionKind> KindElement { get; set; }
        
        /// <summary>
        /// applicability | start | stop
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ActionConditionKind? Kind { get; set; }
    
    }
    
    public partial interface IPlanDefinitionRelatedActionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// What action is this related to
        /// </summary>
        Hl7.Fhir.Model.Id ActionIdElement { get; set; }
        
        /// <summary>
        /// What action is this related to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ActionId { get; set; }
    
        /// <summary>
        /// before-start | before | before-end | concurrent-with-start | concurrent | concurrent-with-end | after-start | after | after-end
        /// </summary>
        Code<Hl7.Fhir.Model.ActionRelationshipType> RelationshipElement { get; set; }
        
        /// <summary>
        /// before-start | before | before-end | concurrent-with-start | concurrent | concurrent-with-end | after-start | after | after-end
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ActionRelationshipType? Relationship { get; set; }
    
        /// <summary>
        /// Time offset for the relationship
        /// </summary>
        Hl7.Fhir.Model.Element Offset { get; set; }
    
    }
    
    public partial interface IPlanDefinitionParticipantComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// E.g. Nurse, Surgeon, Parent
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Role { get; set; }
    
    }
    
    public partial interface IPlanDefinitionDynamicValueComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The path to the element to be set dynamically
        /// </summary>
        Hl7.Fhir.Model.FhirString PathElement { get; set; }
        
        /// <summary>
        /// The path to the element to be set dynamically
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Path { get; set; }
    
    }

}
