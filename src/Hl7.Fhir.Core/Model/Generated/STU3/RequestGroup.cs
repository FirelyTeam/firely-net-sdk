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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// A group of related requests
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "RequestGroup", IsResource=true)]
    [DataContract]
    public partial class RequestGroup : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IRequestGroup, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.RequestGroup; } }
        [NotMapped]
        public override string TypeName { get { return "RequestGroup"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ActionComponent")]
        [DataContract]
        public partial class ActionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IRequestGroupActionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ActionComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IRelatedArtifact> Hl7.Fhir.Model.IRequestGroupActionComponent.Documentation { get { return Documentation; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IRequestGroupConditionComponent> Hl7.Fhir.Model.IRequestGroupActionComponent.Condition { get { return Condition; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IRequestGroupRelatedActionComponent> Hl7.Fhir.Model.IRequestGroupActionComponent.RelatedAction { get { return RelatedAction; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IRequestGroupActionComponent> Hl7.Fhir.Model.IRequestGroupActionComponent.Action { get { return Action; } }
            
            /// <summary>
            /// User-visible label for the action (e.g. 1. or A.)
            /// </summary>
            [FhirElement("label", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LabelElement
            {
                get { return _LabelElement; }
                set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LabelElement;
            
            /// <summary>
            /// User-visible label for the action (e.g. 1. or A.)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Label
            {
                get { return LabelElement != null ? LabelElement.Value : null; }
                set
                {
                    if (value == null)
                        LabelElement = null;
                    else
                        LabelElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Label");
                }
            }
            
            /// <summary>
            /// User-visible title
            /// </summary>
            [FhirElement("title", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// User-visible title
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Title
            {
                get { return TitleElement != null ? TitleElement.Value : null; }
                set
                {
                    if (value == null)
                        TitleElement = null;
                    else
                        TitleElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Title");
                }
            }
            
            /// <summary>
            /// Short description of the action
            /// </summary>
            [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Short description of the action
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null;
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Static text equivalent of the action, used if the dynamic aspects cannot be interpreted by the receiving system
            /// </summary>
            [FhirElement("textEquivalent", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextEquivalentElement
            {
                get { return _TextEquivalentElement; }
                set { _TextEquivalentElement = value; OnPropertyChanged("TextEquivalentElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextEquivalentElement;
            
            /// <summary>
            /// Static text equivalent of the action, used if the dynamic aspects cannot be interpreted by the receiving system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string TextEquivalent
            {
                get { return TextEquivalentElement != null ? TextEquivalentElement.Value : null; }
                set
                {
                    if (value == null)
                        TextEquivalentElement = null;
                    else
                        TextEquivalentElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("TextEquivalent");
                }
            }
            
            /// <summary>
            /// Code representing the meaning of the action or sub-actions
            /// </summary>
            [FhirElement("code", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Code
            {
                get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Code;
            
            /// <summary>
            /// Supporting documentation for the intended performer of the action
            /// </summary>
            [FhirElement("documentation", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.STU3.RelatedArtifact> Documentation
            {
                get { if(_Documentation==null) _Documentation = new List<Hl7.Fhir.Model.STU3.RelatedArtifact>(); return _Documentation; }
                set { _Documentation = value; OnPropertyChanged("Documentation"); }
            }
            
            private List<Hl7.Fhir.Model.STU3.RelatedArtifact> _Documentation;
            
            /// <summary>
            /// Whether or not the action is applicable
            /// </summary>
            [FhirElement("condition", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ConditionComponent> Condition
            {
                get { if(_Condition==null) _Condition = new List<ConditionComponent>(); return _Condition; }
                set { _Condition = value; OnPropertyChanged("Condition"); }
            }
            
            private List<ConditionComponent> _Condition;
            
            /// <summary>
            /// Relationship to another action
            /// </summary>
            [FhirElement("relatedAction", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<RelatedActionComponent> RelatedAction
            {
                get { if(_RelatedAction==null) _RelatedAction = new List<RelatedActionComponent>(); return _RelatedAction; }
                set { _RelatedAction = value; OnPropertyChanged("RelatedAction"); }
            }
            
            private List<RelatedActionComponent> _RelatedAction;
            
            /// <summary>
            /// When the action should take place
            /// </summary>
            [FhirElement("timing", Order=120, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.STU3.Duration),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.STU3.Timing))]
            [DataMember]
            public Hl7.Fhir.Model.Element Timing
            {
                get { return _Timing; }
                set { _Timing = value; OnPropertyChanged("Timing"); }
            }
            
            private Hl7.Fhir.Model.Element _Timing;
            
            /// <summary>
            /// Who should perform the action
            /// </summary>
            [FhirElement("participant", Order=130)]
            [CLSCompliant(false)]
            [References("Patient","Person","Practitioner","RelatedPerson")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Participant
            {
                get { if(_Participant==null) _Participant = new List<Hl7.Fhir.Model.ResourceReference>(); return _Participant; }
                set { _Participant = value; OnPropertyChanged("Participant"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Participant;
            
            /// <summary>
            /// create | update | remove | fire-event
            /// </summary>
            [FhirElement("type", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// visual-group | logical-group | sentence-group
            /// </summary>
            [FhirElement("groupingBehavior", Order=150)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ActionGroupingBehavior> GroupingBehaviorElement
            {
                get { return _GroupingBehaviorElement; }
                set { _GroupingBehaviorElement = value; OnPropertyChanged("GroupingBehaviorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ActionGroupingBehavior> _GroupingBehaviorElement;
            
            /// <summary>
            /// visual-group | logical-group | sentence-group
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ActionGroupingBehavior? GroupingBehavior
            {
                get { return GroupingBehaviorElement != null ? GroupingBehaviorElement.Value : null; }
                set
                {
                    if (value == null)
                        GroupingBehaviorElement = null;
                    else
                        GroupingBehaviorElement = new Code<Hl7.Fhir.Model.ActionGroupingBehavior>(value);
                    OnPropertyChanged("GroupingBehavior");
                }
            }
            
            /// <summary>
            /// any | all | all-or-none | exactly-one | at-most-one | one-or-more
            /// </summary>
            [FhirElement("selectionBehavior", Order=160)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ActionSelectionBehavior> SelectionBehaviorElement
            {
                get { return _SelectionBehaviorElement; }
                set { _SelectionBehaviorElement = value; OnPropertyChanged("SelectionBehaviorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ActionSelectionBehavior> _SelectionBehaviorElement;
            
            /// <summary>
            /// any | all | all-or-none | exactly-one | at-most-one | one-or-more
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ActionSelectionBehavior? SelectionBehavior
            {
                get { return SelectionBehaviorElement != null ? SelectionBehaviorElement.Value : null; }
                set
                {
                    if (value == null)
                        SelectionBehaviorElement = null;
                    else
                        SelectionBehaviorElement = new Code<Hl7.Fhir.Model.ActionSelectionBehavior>(value);
                    OnPropertyChanged("SelectionBehavior");
                }
            }
            
            /// <summary>
            /// must | could | must-unless-documented
            /// </summary>
            [FhirElement("requiredBehavior", Order=170)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ActionRequiredBehavior> RequiredBehaviorElement
            {
                get { return _RequiredBehaviorElement; }
                set { _RequiredBehaviorElement = value; OnPropertyChanged("RequiredBehaviorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ActionRequiredBehavior> _RequiredBehaviorElement;
            
            /// <summary>
            /// must | could | must-unless-documented
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ActionRequiredBehavior? RequiredBehavior
            {
                get { return RequiredBehaviorElement != null ? RequiredBehaviorElement.Value : null; }
                set
                {
                    if (value == null)
                        RequiredBehaviorElement = null;
                    else
                        RequiredBehaviorElement = new Code<Hl7.Fhir.Model.ActionRequiredBehavior>(value);
                    OnPropertyChanged("RequiredBehavior");
                }
            }
            
            /// <summary>
            /// yes | no
            /// </summary>
            [FhirElement("precheckBehavior", Order=180)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ActionPrecheckBehavior> PrecheckBehaviorElement
            {
                get { return _PrecheckBehaviorElement; }
                set { _PrecheckBehaviorElement = value; OnPropertyChanged("PrecheckBehaviorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ActionPrecheckBehavior> _PrecheckBehaviorElement;
            
            /// <summary>
            /// yes | no
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ActionPrecheckBehavior? PrecheckBehavior
            {
                get { return PrecheckBehaviorElement != null ? PrecheckBehaviorElement.Value : null; }
                set
                {
                    if (value == null)
                        PrecheckBehaviorElement = null;
                    else
                        PrecheckBehaviorElement = new Code<Hl7.Fhir.Model.ActionPrecheckBehavior>(value);
                    OnPropertyChanged("PrecheckBehavior");
                }
            }
            
            /// <summary>
            /// single | multiple
            /// </summary>
            [FhirElement("cardinalityBehavior", Order=190)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ActionCardinalityBehavior> CardinalityBehaviorElement
            {
                get { return _CardinalityBehaviorElement; }
                set { _CardinalityBehaviorElement = value; OnPropertyChanged("CardinalityBehaviorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ActionCardinalityBehavior> _CardinalityBehaviorElement;
            
            /// <summary>
            /// single | multiple
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ActionCardinalityBehavior? CardinalityBehavior
            {
                get { return CardinalityBehaviorElement != null ? CardinalityBehaviorElement.Value : null; }
                set
                {
                    if (value == null)
                        CardinalityBehaviorElement = null;
                    else
                        CardinalityBehaviorElement = new Code<Hl7.Fhir.Model.ActionCardinalityBehavior>(value);
                    OnPropertyChanged("CardinalityBehavior");
                }
            }
            
            /// <summary>
            /// The target of the action
            /// </summary>
            [FhirElement("resource", Order=200)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Resource;
            
            /// <summary>
            /// Sub action
            /// </summary>
            [FhirElement("action", Order=210)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<ActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<ActionComponent> _Action;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ActionComponent");
                base.Serialize(sink);
                sink.Element("label", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LabelElement?.Serialize(sink);
                sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TitleElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("textEquivalent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TextEquivalentElement?.Serialize(sink);
                sink.BeginList("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Code)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Documentation)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("condition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Condition)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("relatedAction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in RelatedAction)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("timing", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Timing?.Serialize(sink);
                sink.BeginList("participant", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Participant)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("groupingBehavior", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); GroupingBehaviorElement?.Serialize(sink);
                sink.Element("selectionBehavior", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SelectionBehaviorElement?.Serialize(sink);
                sink.Element("requiredBehavior", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequiredBehaviorElement?.Serialize(sink);
                sink.Element("precheckBehavior", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PrecheckBehaviorElement?.Serialize(sink);
                sink.Element("cardinalityBehavior", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CardinalityBehaviorElement?.Serialize(sink);
                sink.Element("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Resource?.Serialize(sink);
                sink.BeginList("action", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Action)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "label":
                        LabelElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "title":
                        TitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "textEquivalent":
                        TextEquivalentElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "code":
                        Code = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "documentation":
                        Documentation = source.GetList<Hl7.Fhir.Model.STU3.RelatedArtifact>();
                        return true;
                    case "condition":
                        Condition = source.GetList<ConditionComponent>();
                        return true;
                    case "relatedAction":
                        RelatedAction = source.GetList<RelatedActionComponent>();
                        return true;
                    case "timingDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Timing, "timing");
                        Timing = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                        return true;
                    case "timingPeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Timing, "timing");
                        Timing = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "timingDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Duration>(Timing, "timing");
                        Timing = source.Get<Hl7.Fhir.Model.STU3.Duration>();
                        return true;
                    case "timingRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Timing, "timing");
                        Timing = source.Get<Hl7.Fhir.Model.Range>();
                        return true;
                    case "timingTiming":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(Timing, "timing");
                        Timing = source.Get<Hl7.Fhir.Model.STU3.Timing>();
                        return true;
                    case "participant":
                        Participant = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "groupingBehavior":
                        GroupingBehaviorElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ActionGroupingBehavior>>();
                        return true;
                    case "selectionBehavior":
                        SelectionBehaviorElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ActionSelectionBehavior>>();
                        return true;
                    case "requiredBehavior":
                        RequiredBehaviorElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ActionRequiredBehavior>>();
                        return true;
                    case "precheckBehavior":
                        PrecheckBehaviorElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ActionPrecheckBehavior>>();
                        return true;
                    case "cardinalityBehavior":
                        CardinalityBehaviorElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ActionCardinalityBehavior>>();
                        return true;
                    case "resource":
                        Resource = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "action":
                        Action = source.GetList<ActionComponent>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "label":
                        LabelElement = source.PopulateValue(LabelElement);
                        return true;
                    case "_label":
                        LabelElement = source.Populate(LabelElement);
                        return true;
                    case "title":
                        TitleElement = source.PopulateValue(TitleElement);
                        return true;
                    case "_title":
                        TitleElement = source.Populate(TitleElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "textEquivalent":
                        TextEquivalentElement = source.PopulateValue(TextEquivalentElement);
                        return true;
                    case "_textEquivalent":
                        TextEquivalentElement = source.Populate(TextEquivalentElement);
                        return true;
                    case "code":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "documentation":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "condition":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "relatedAction":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "timingDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Timing, "timing");
                        Timing = source.PopulateValue(Timing as Hl7.Fhir.Model.FhirDateTime);
                        return true;
                    case "_timingDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Timing, "timing");
                        Timing = source.Populate(Timing as Hl7.Fhir.Model.FhirDateTime);
                        return true;
                    case "timingPeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Timing, "timing");
                        Timing = source.Populate(Timing as Hl7.Fhir.Model.Period);
                        return true;
                    case "timingDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Duration>(Timing, "timing");
                        Timing = source.Populate(Timing as Hl7.Fhir.Model.STU3.Duration);
                        return true;
                    case "timingRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Timing, "timing");
                        Timing = source.Populate(Timing as Hl7.Fhir.Model.Range);
                        return true;
                    case "timingTiming":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(Timing, "timing");
                        Timing = source.Populate(Timing as Hl7.Fhir.Model.STU3.Timing);
                        return true;
                    case "participant":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "groupingBehavior":
                        GroupingBehaviorElement = source.PopulateValue(GroupingBehaviorElement);
                        return true;
                    case "_groupingBehavior":
                        GroupingBehaviorElement = source.Populate(GroupingBehaviorElement);
                        return true;
                    case "selectionBehavior":
                        SelectionBehaviorElement = source.PopulateValue(SelectionBehaviorElement);
                        return true;
                    case "_selectionBehavior":
                        SelectionBehaviorElement = source.Populate(SelectionBehaviorElement);
                        return true;
                    case "requiredBehavior":
                        RequiredBehaviorElement = source.PopulateValue(RequiredBehaviorElement);
                        return true;
                    case "_requiredBehavior":
                        RequiredBehaviorElement = source.Populate(RequiredBehaviorElement);
                        return true;
                    case "precheckBehavior":
                        PrecheckBehaviorElement = source.PopulateValue(PrecheckBehaviorElement);
                        return true;
                    case "_precheckBehavior":
                        PrecheckBehaviorElement = source.Populate(PrecheckBehaviorElement);
                        return true;
                    case "cardinalityBehavior":
                        CardinalityBehaviorElement = source.PopulateValue(CardinalityBehaviorElement);
                        return true;
                    case "_cardinalityBehavior":
                        CardinalityBehaviorElement = source.Populate(CardinalityBehaviorElement);
                        return true;
                    case "resource":
                        Resource = source.Populate(Resource);
                        return true;
                    case "action":
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "code":
                        source.PopulateListItem(Code, index);
                        return true;
                    case "documentation":
                        source.PopulateListItem(Documentation, index);
                        return true;
                    case "condition":
                        source.PopulateListItem(Condition, index);
                        return true;
                    case "relatedAction":
                        source.PopulateListItem(RelatedAction, index);
                        return true;
                    case "participant":
                        source.PopulateListItem(Participant, index);
                        return true;
                    case "action":
                        source.PopulateListItem(Action, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(TextEquivalentElement != null) dest.TextEquivalentElement = (Hl7.Fhir.Model.FhirString)TextEquivalentElement.DeepCopy();
                    if(Code != null) dest.Code = new List<Hl7.Fhir.Model.CodeableConcept>(Code.DeepCopy());
                    if(Documentation != null) dest.Documentation = new List<Hl7.Fhir.Model.STU3.RelatedArtifact>(Documentation.DeepCopy());
                    if(Condition != null) dest.Condition = new List<ConditionComponent>(Condition.DeepCopy());
                    if(RelatedAction != null) dest.RelatedAction = new List<RelatedActionComponent>(RelatedAction.DeepCopy());
                    if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                    if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.ResourceReference>(Participant.DeepCopy());
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(GroupingBehaviorElement != null) dest.GroupingBehaviorElement = (Code<Hl7.Fhir.Model.ActionGroupingBehavior>)GroupingBehaviorElement.DeepCopy();
                    if(SelectionBehaviorElement != null) dest.SelectionBehaviorElement = (Code<Hl7.Fhir.Model.ActionSelectionBehavior>)SelectionBehaviorElement.DeepCopy();
                    if(RequiredBehaviorElement != null) dest.RequiredBehaviorElement = (Code<Hl7.Fhir.Model.ActionRequiredBehavior>)RequiredBehaviorElement.DeepCopy();
                    if(PrecheckBehaviorElement != null) dest.PrecheckBehaviorElement = (Code<Hl7.Fhir.Model.ActionPrecheckBehavior>)PrecheckBehaviorElement.DeepCopy();
                    if(CardinalityBehaviorElement != null) dest.CardinalityBehaviorElement = (Code<Hl7.Fhir.Model.ActionCardinalityBehavior>)CardinalityBehaviorElement.DeepCopy();
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                    if(Action != null) dest.Action = new List<ActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(TextEquivalentElement, otherT.TextEquivalentElement)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.Matches(Condition, otherT.Condition)) return false;
                if( !DeepComparable.Matches(RelatedAction, otherT.RelatedAction)) return false;
                if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
                if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(GroupingBehaviorElement, otherT.GroupingBehaviorElement)) return false;
                if( !DeepComparable.Matches(SelectionBehaviorElement, otherT.SelectionBehaviorElement)) return false;
                if( !DeepComparable.Matches(RequiredBehaviorElement, otherT.RequiredBehaviorElement)) return false;
                if( !DeepComparable.Matches(PrecheckBehaviorElement, otherT.PrecheckBehaviorElement)) return false;
                if( !DeepComparable.Matches(CardinalityBehaviorElement, otherT.CardinalityBehaviorElement)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(TextEquivalentElement, otherT.TextEquivalentElement)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.IsExactly(Condition, otherT.Condition)) return false;
                if( !DeepComparable.IsExactly(RelatedAction, otherT.RelatedAction)) return false;
                if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
                if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(GroupingBehaviorElement, otherT.GroupingBehaviorElement)) return false;
                if( !DeepComparable.IsExactly(SelectionBehaviorElement, otherT.SelectionBehaviorElement)) return false;
                if( !DeepComparable.IsExactly(RequiredBehaviorElement, otherT.RequiredBehaviorElement)) return false;
                if( !DeepComparable.IsExactly(PrecheckBehaviorElement, otherT.PrecheckBehaviorElement)) return false;
                if( !DeepComparable.IsExactly(CardinalityBehaviorElement, otherT.CardinalityBehaviorElement)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LabelElement != null) yield return LabelElement;
                    if (TitleElement != null) yield return TitleElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (TextEquivalentElement != null) yield return TextEquivalentElement;
                    foreach (var elem in Code) { if (elem != null) yield return elem; }
                    foreach (var elem in Documentation) { if (elem != null) yield return elem; }
                    foreach (var elem in Condition) { if (elem != null) yield return elem; }
                    foreach (var elem in RelatedAction) { if (elem != null) yield return elem; }
                    if (Timing != null) yield return Timing;
                    foreach (var elem in Participant) { if (elem != null) yield return elem; }
                    if (Type != null) yield return Type;
                    if (GroupingBehaviorElement != null) yield return GroupingBehaviorElement;
                    if (SelectionBehaviorElement != null) yield return SelectionBehaviorElement;
                    if (RequiredBehaviorElement != null) yield return RequiredBehaviorElement;
                    if (PrecheckBehaviorElement != null) yield return PrecheckBehaviorElement;
                    if (CardinalityBehaviorElement != null) yield return CardinalityBehaviorElement;
                    if (Resource != null) yield return Resource;
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (LabelElement != null) yield return new ElementValue("label", LabelElement);
                    if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (TextEquivalentElement != null) yield return new ElementValue("textEquivalent", TextEquivalentElement);
                    foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                    foreach (var elem in Documentation) { if (elem != null) yield return new ElementValue("documentation", elem); }
                    foreach (var elem in Condition) { if (elem != null) yield return new ElementValue("condition", elem); }
                    foreach (var elem in RelatedAction) { if (elem != null) yield return new ElementValue("relatedAction", elem); }
                    if (Timing != null) yield return new ElementValue("timing", Timing);
                    foreach (var elem in Participant) { if (elem != null) yield return new ElementValue("participant", elem); }
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (GroupingBehaviorElement != null) yield return new ElementValue("groupingBehavior", GroupingBehaviorElement);
                    if (SelectionBehaviorElement != null) yield return new ElementValue("selectionBehavior", SelectionBehaviorElement);
                    if (RequiredBehaviorElement != null) yield return new ElementValue("requiredBehavior", RequiredBehaviorElement);
                    if (PrecheckBehaviorElement != null) yield return new ElementValue("precheckBehavior", PrecheckBehaviorElement);
                    if (CardinalityBehaviorElement != null) yield return new ElementValue("cardinalityBehavior", CardinalityBehaviorElement);
                    if (Resource != null) yield return new ElementValue("resource", Resource);
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ConditionComponent")]
        [DataContract]
        public partial class ConditionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IRequestGroupConditionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ConditionComponent"; } }
            
            /// <summary>
            /// applicability | start | stop
            /// </summary>
            [FhirElement("kind", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ActionConditionKind> KindElement
            {
                get { return _KindElement; }
                set { _KindElement = value; OnPropertyChanged("KindElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ActionConditionKind> _KindElement;
            
            /// <summary>
            /// applicability | start | stop
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ActionConditionKind? Kind
            {
                get { return KindElement != null ? KindElement.Value : null; }
                set
                {
                    if (value == null)
                        KindElement = null;
                    else
                        KindElement = new Code<Hl7.Fhir.Model.ActionConditionKind>(value);
                    OnPropertyChanged("Kind");
                }
            }
            
            /// <summary>
            /// Natural language description of the condition
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Natural language description of the condition
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null;
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Language of the expression
            /// </summary>
            [FhirElement("language", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LanguageElement
            {
                get { return _LanguageElement; }
                set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LanguageElement;
            
            /// <summary>
            /// Language of the expression
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Language
            {
                get { return LanguageElement != null ? LanguageElement.Value : null; }
                set
                {
                    if (value == null)
                        LanguageElement = null;
                    else
                        LanguageElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Language");
                }
            }
            
            /// <summary>
            /// Boolean-valued expression
            /// </summary>
            [FhirElement("expression", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ExpressionElement
            {
                get { return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ExpressionElement;
            
            /// <summary>
            /// Boolean-valued expression
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Expression
            {
                get { return ExpressionElement != null ? ExpressionElement.Value : null; }
                set
                {
                    if (value == null)
                        ExpressionElement = null;
                    else
                        ExpressionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Expression");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ConditionComponent");
                base.Serialize(sink);
                sink.Element("kind", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); KindElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("language", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LanguageElement?.Serialize(sink);
                sink.Element("expression", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ExpressionElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "kind":
                        KindElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ActionConditionKind>>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "language":
                        LanguageElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "expression":
                        ExpressionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "kind":
                        KindElement = source.PopulateValue(KindElement);
                        return true;
                    case "_kind":
                        KindElement = source.Populate(KindElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "language":
                        LanguageElement = source.PopulateValue(LanguageElement);
                        return true;
                    case "_language":
                        LanguageElement = source.Populate(LanguageElement);
                        return true;
                    case "expression":
                        ExpressionElement = source.PopulateValue(ExpressionElement);
                        return true;
                    case "_expression":
                        ExpressionElement = source.Populate(ExpressionElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConditionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(KindElement != null) dest.KindElement = (Code<Hl7.Fhir.Model.ActionConditionKind>)KindElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.FhirString)LanguageElement.DeepCopy();
                    if(ExpressionElement != null) dest.ExpressionElement = (Hl7.Fhir.Model.FhirString)ExpressionElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ConditionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConditionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(KindElement, otherT.KindElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConditionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(KindElement, otherT.KindElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (KindElement != null) yield return KindElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (LanguageElement != null) yield return LanguageElement;
                    if (ExpressionElement != null) yield return ExpressionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (KindElement != null) yield return new ElementValue("kind", KindElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (LanguageElement != null) yield return new ElementValue("language", LanguageElement);
                    if (ExpressionElement != null) yield return new ElementValue("expression", ExpressionElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RelatedActionComponent")]
        [DataContract]
        public partial class RelatedActionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IRequestGroupRelatedActionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedActionComponent"; } }
            
            /// <summary>
            /// What action this is related to
            /// </summary>
            [FhirElement("actionId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id ActionIdElement
            {
                get { return _ActionIdElement; }
                set { _ActionIdElement = value; OnPropertyChanged("ActionIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ActionIdElement;
            
            /// <summary>
            /// What action this is related to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ActionId
            {
                get { return ActionIdElement != null ? ActionIdElement.Value : null; }
                set
                {
                    if (value == null)
                        ActionIdElement = null;
                    else
                        ActionIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("ActionId");
                }
            }
            
            /// <summary>
            /// before-start | before | before-end | concurrent-with-start | concurrent | concurrent-with-end | after-start | after | after-end
            /// </summary>
            [FhirElement("relationship", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ActionRelationshipType> RelationshipElement
            {
                get { return _RelationshipElement; }
                set { _RelationshipElement = value; OnPropertyChanged("RelationshipElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ActionRelationshipType> _RelationshipElement;
            
            /// <summary>
            /// before-start | before | before-end | concurrent-with-start | concurrent | concurrent-with-end | after-start | after | after-end
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ActionRelationshipType? Relationship
            {
                get { return RelationshipElement != null ? RelationshipElement.Value : null; }
                set
                {
                    if (value == null)
                        RelationshipElement = null;
                    else
                        RelationshipElement = new Code<Hl7.Fhir.Model.ActionRelationshipType>(value);
                    OnPropertyChanged("Relationship");
                }
            }
            
            /// <summary>
            /// Time offset for the relationship
            /// </summary>
            [FhirElement("offset", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.STU3.Duration),typeof(Hl7.Fhir.Model.Range))]
            [DataMember]
            public Hl7.Fhir.Model.Element Offset
            {
                get { return _Offset; }
                set { _Offset = value; OnPropertyChanged("Offset"); }
            }
            
            private Hl7.Fhir.Model.Element _Offset;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RelatedActionComponent");
                base.Serialize(sink);
                sink.Element("actionId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ActionIdElement?.Serialize(sink);
                sink.Element("relationship", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); RelationshipElement?.Serialize(sink);
                sink.Element("offset", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Offset?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "actionId":
                        ActionIdElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "relationship":
                        RelationshipElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ActionRelationshipType>>();
                        return true;
                    case "offsetDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Duration>(Offset, "offset");
                        Offset = source.Get<Hl7.Fhir.Model.STU3.Duration>();
                        return true;
                    case "offsetRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Offset, "offset");
                        Offset = source.Get<Hl7.Fhir.Model.Range>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "actionId":
                        ActionIdElement = source.PopulateValue(ActionIdElement);
                        return true;
                    case "_actionId":
                        ActionIdElement = source.Populate(ActionIdElement);
                        return true;
                    case "relationship":
                        RelationshipElement = source.PopulateValue(RelationshipElement);
                        return true;
                    case "_relationship":
                        RelationshipElement = source.Populate(RelationshipElement);
                        return true;
                    case "offsetDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Duration>(Offset, "offset");
                        Offset = source.Populate(Offset as Hl7.Fhir.Model.STU3.Duration);
                        return true;
                    case "offsetRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Offset, "offset");
                        Offset = source.Populate(Offset as Hl7.Fhir.Model.Range);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedActionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ActionIdElement != null) dest.ActionIdElement = (Hl7.Fhir.Model.Id)ActionIdElement.DeepCopy();
                    if(RelationshipElement != null) dest.RelationshipElement = (Code<Hl7.Fhir.Model.ActionRelationshipType>)RelationshipElement.DeepCopy();
                    if(Offset != null) dest.Offset = (Hl7.Fhir.Model.Element)Offset.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RelatedActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RelatedActionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ActionIdElement, otherT.ActionIdElement)) return false;
                if( !DeepComparable.Matches(RelationshipElement, otherT.RelationshipElement)) return false;
                if( !DeepComparable.Matches(Offset, otherT.Offset)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedActionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ActionIdElement, otherT.ActionIdElement)) return false;
                if( !DeepComparable.IsExactly(RelationshipElement, otherT.RelationshipElement)) return false;
                if( !DeepComparable.IsExactly(Offset, otherT.Offset)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ActionIdElement != null) yield return ActionIdElement;
                    if (RelationshipElement != null) yield return RelationshipElement;
                    if (Offset != null) yield return Offset;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ActionIdElement != null) yield return new ElementValue("actionId", ActionIdElement);
                    if (RelationshipElement != null) yield return new ElementValue("relationship", RelationshipElement);
                    if (Offset != null) yield return new ElementValue("offset", Offset);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IRequestGroupActionComponent> Hl7.Fhir.Model.IRequestGroup.Action { get { return Action; } }
    
        
        /// <summary>
        /// Business identifier
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Instantiates protocol or definition
        /// </summary>
        [FhirElement("definition", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Definition
        {
            get { if(_Definition==null) _Definition = new List<Hl7.Fhir.Model.ResourceReference>(); return _Definition; }
            set { _Definition = value; OnPropertyChanged("Definition"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Definition;
        
        /// <summary>
        /// Fulfills plan, proposal, or order
        /// </summary>
        [FhirElement("basedOn", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Request(s) replaced by this request
        /// </summary>
        [FhirElement("replaces", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Replaces
        {
            get { if(_Replaces==null) _Replaces = new List<Hl7.Fhir.Model.ResourceReference>(); return _Replaces; }
            set { _Replaces = value; OnPropertyChanged("Replaces"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Replaces;
        
        /// <summary>
        /// Composite request this is part of
        /// </summary>
        [FhirElement("groupIdentifier", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier GroupIdentifier
        {
            get { return _GroupIdentifier; }
            set { _GroupIdentifier = value; OnPropertyChanged("GroupIdentifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _GroupIdentifier;
        
        /// <summary>
        /// draft | active | suspended | cancelled | completed | entered-in-error | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.RequestStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.RequestStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | suspended | cancelled | completed | entered-in-error | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.RequestStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.STU3.RequestStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// proposal | plan | order
        /// </summary>
        [FhirElement("intent", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.RequestIntent> IntentElement
        {
            get { return _IntentElement; }
            set { _IntentElement = value; OnPropertyChanged("IntentElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.RequestIntent> _IntentElement;
        
        /// <summary>
        /// proposal | plan | order
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.RequestIntent? Intent
        {
            get { return IntentElement != null ? IntentElement.Value : null; }
            set
            {
                if (value == null)
                    IntentElement = null;
                else
                    IntentElement = new Code<Hl7.Fhir.Model.STU3.RequestIntent>(value);
                OnPropertyChanged("Intent");
            }
        }
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        [FhirElement("priority", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RequestPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RequestPriority> _PriorityElement;
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.RequestPriority? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if (value == null)
                    PriorityElement = null;
                else
                    PriorityElement = new Code<Hl7.Fhir.Model.RequestPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// Who the request group is about
        /// </summary>
        [FhirElement("subject", Order=170)]
        [CLSCompliant(false)]
        [References("Patient","Group")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Encounter or Episode for the request group
        /// </summary>
        [FhirElement("context", Order=180)]
        [CLSCompliant(false)]
        [References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// When the request group was authored
        /// </summary>
        [FhirElement("authoredOn", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AuthoredOnElement
        {
            get { return _AuthoredOnElement; }
            set { _AuthoredOnElement = value; OnPropertyChanged("AuthoredOnElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _AuthoredOnElement;
        
        /// <summary>
        /// When the request group was authored
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AuthoredOn
        {
            get { return AuthoredOnElement != null ? AuthoredOnElement.Value : null; }
            set
            {
                if (value == null)
                    AuthoredOnElement = null;
                else
                    AuthoredOnElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("AuthoredOn");
            }
        }
        
        /// <summary>
        /// Device or practitioner that authored the request group
        /// </summary>
        [FhirElement("author", Order=200)]
        [CLSCompliant(false)]
        [References("Device","Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// Reason for the request group
        /// </summary>
        [FhirElement("reason", Order=210, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private Hl7.Fhir.Model.Element _Reason;
        
        /// <summary>
        /// Additional notes about the response
        /// </summary>
        [FhirElement("note", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Proposed actions, if any
        /// </summary>
        [FhirElement("action", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ActionComponent> Action
        {
            get { if(_Action==null) _Action = new List<ActionComponent>(); return _Action; }
            set { _Action = value; OnPropertyChanged("Action"); }
        }
        
        private List<ActionComponent> _Action;
    
    
        public static ElementDefinitionConstraint[] RequestGroup_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "rqg-1",
                severity: ConstraintSeverity.Warning,
                expression: "action.all(resource.exists() != action.exists())",
                human: "Must have resource or action but not both",
                xpath: "exists(f:resource) != exists(f:action)"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(RequestGroup_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as RequestGroup;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Definition != null) dest.Definition = new List<Hl7.Fhir.Model.ResourceReference>(Definition.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(Replaces != null) dest.Replaces = new List<Hl7.Fhir.Model.ResourceReference>(Replaces.DeepCopy());
                if(GroupIdentifier != null) dest.GroupIdentifier = (Hl7.Fhir.Model.Identifier)GroupIdentifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.STU3.RequestStatus>)StatusElement.DeepCopy();
                if(IntentElement != null) dest.IntentElement = (Code<Hl7.Fhir.Model.STU3.RequestIntent>)IntentElement.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.RequestPriority>)PriorityElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(AuthoredOnElement != null) dest.AuthoredOnElement = (Hl7.Fhir.Model.FhirDateTime)AuthoredOnElement.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.Element)Reason.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Action != null) dest.Action = new List<ActionComponent>(Action.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new RequestGroup());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as RequestGroup;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(Replaces, otherT.Replaces)) return false;
            if( !DeepComparable.Matches(GroupIdentifier, otherT.GroupIdentifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(IntentElement, otherT.IntentElement)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Action, otherT.Action)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as RequestGroup;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(Replaces, otherT.Replaces)) return false;
            if( !DeepComparable.IsExactly(GroupIdentifier, otherT.GroupIdentifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(IntentElement, otherT.IntentElement)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("RequestGroup");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Definition)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("basedOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in BasedOn)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("replaces", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Replaces)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("groupIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); GroupIdentifier?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("intent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); IntentElement?.Serialize(sink);
            sink.Element("priority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PriorityElement?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Subject?.Serialize(sink);
            sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Context?.Serialize(sink);
            sink.Element("authoredOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AuthoredOnElement?.Serialize(sink);
            sink.Element("author", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Author?.Serialize(sink);
            sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Reason?.Serialize(sink);
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("action", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Action)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "definition":
                    Definition = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "basedOn":
                    BasedOn = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "replaces":
                    Replaces = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "groupIdentifier":
                    GroupIdentifier = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.RequestStatus>>();
                    return true;
                case "intent":
                    IntentElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.RequestIntent>>();
                    return true;
                case "priority":
                    PriorityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.RequestPriority>>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "context":
                    Context = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "authoredOn":
                    AuthoredOnElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "author":
                    Author = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "reasonCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Reason, "reason");
                    Reason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "reasonReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Reason, "reason");
                    Reason = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "action":
                    Action = source.GetList<ActionComponent>();
                    return true;
            }
            return false;
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "definition":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "basedOn":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "replaces":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "groupIdentifier":
                    GroupIdentifier = source.Populate(GroupIdentifier);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "intent":
                    IntentElement = source.PopulateValue(IntentElement);
                    return true;
                case "_intent":
                    IntentElement = source.Populate(IntentElement);
                    return true;
                case "priority":
                    PriorityElement = source.PopulateValue(PriorityElement);
                    return true;
                case "_priority":
                    PriorityElement = source.Populate(PriorityElement);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "context":
                    Context = source.Populate(Context);
                    return true;
                case "authoredOn":
                    AuthoredOnElement = source.PopulateValue(AuthoredOnElement);
                    return true;
                case "_authoredOn":
                    AuthoredOnElement = source.Populate(AuthoredOnElement);
                    return true;
                case "author":
                    Author = source.Populate(Author);
                    return true;
                case "reasonCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Reason, "reason");
                    Reason = source.Populate(Reason as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "reasonReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Reason, "reason");
                    Reason = source.Populate(Reason as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "action":
                    source.SetList(this, jsonPropertyName);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "definition":
                    source.PopulateListItem(Definition, index);
                    return true;
                case "basedOn":
                    source.PopulateListItem(BasedOn, index);
                    return true;
                case "replaces":
                    source.PopulateListItem(Replaces, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "action":
                    source.PopulateListItem(Action, index);
                    return true;
            }
            return false;
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                foreach (var elem in Definition) { if (elem != null) yield return elem; }
                foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
                foreach (var elem in Replaces) { if (elem != null) yield return elem; }
                if (GroupIdentifier != null) yield return GroupIdentifier;
                if (StatusElement != null) yield return StatusElement;
                if (IntentElement != null) yield return IntentElement;
                if (PriorityElement != null) yield return PriorityElement;
                if (Subject != null) yield return Subject;
                if (Context != null) yield return Context;
                if (AuthoredOnElement != null) yield return AuthoredOnElement;
                if (Author != null) yield return Author;
                if (Reason != null) yield return Reason;
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in Action) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in Definition) { if (elem != null) yield return new ElementValue("definition", elem); }
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                foreach (var elem in Replaces) { if (elem != null) yield return new ElementValue("replaces", elem); }
                if (GroupIdentifier != null) yield return new ElementValue("groupIdentifier", GroupIdentifier);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (IntentElement != null) yield return new ElementValue("intent", IntentElement);
                if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (AuthoredOnElement != null) yield return new ElementValue("authoredOn", AuthoredOnElement);
                if (Author != null) yield return new ElementValue("author", Author);
                if (Reason != null) yield return new ElementValue("reason", Reason);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
            }
        }
    
    }

}
