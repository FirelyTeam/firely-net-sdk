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
    /// A group of related requests
    /// </summary>
    public partial interface IRequestGroup : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Business identifier
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Fulfills plan, proposal, or order
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> BasedOn { get; set; }
    
        /// <summary>
        /// Request(s) replaced by this request
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Replaces { get; set; }
    
        /// <summary>
        /// Composite request this is part of
        /// </summary>
        Hl7.Fhir.Model.Identifier GroupIdentifier { get; set; }
    
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        Code<Hl7.Fhir.Model.RequestPriority> PriorityElement { get; set; }
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.RequestPriority? Priority { get; set; }
    
        /// <summary>
        /// Who the request group is about
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// When the request group was authored
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime AuthoredOnElement { get; set; }
        
        /// <summary>
        /// When the request group was authored
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string AuthoredOn { get; set; }
    
        /// <summary>
        /// Device or practitioner that authored the request group
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Author { get; set; }
    
        /// <summary>
        /// Additional notes about the response
        /// </summary>
        List<Hl7.Fhir.Model.Annotation> Note { get; set; }
    
        /// <summary>
        /// Proposed actions, if any
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IRequestGroupActionComponent> Action { get; }
    
    }
    
    public partial interface IRequestGroupActionComponent : Hl7.Fhir.Model.IBackboneElement
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
        /// Short description of the action
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Short description of the action
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
        /// Supporting documentation for the intended performer of the action
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IRelatedArtifact> Documentation { get; }
    
        /// <summary>
        /// Whether or not the action is applicable
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IRequestGroupConditionComponent> Condition { get; }
    
        /// <summary>
        /// Relationship to another action
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IRequestGroupRelatedActionComponent> RelatedAction { get; }
    
        /// <summary>
        /// When the action should take place
        /// </summary>
        Hl7.Fhir.Model.Element Timing { get; set; }
    
        /// <summary>
        /// Who should perform the action
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Participant { get; set; }
    
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
        /// The target of the action
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Resource { get; set; }
    
        /// <summary>
        /// Sub action
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IRequestGroupActionComponent> Action { get; }
    
    }
    
    public partial interface IRequestGroupConditionComponent : Hl7.Fhir.Model.IBackboneElement
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
    
    public partial interface IRequestGroupRelatedActionComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// What action this is related to
        /// </summary>
        Hl7.Fhir.Model.Id ActionIdElement { get; set; }
        
        /// <summary>
        /// What action this is related to
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

}
