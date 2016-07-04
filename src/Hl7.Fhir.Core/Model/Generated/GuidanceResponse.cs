using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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

//
// Generated for FHIR v1.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// The formal response to a guidance request
    /// </summary>
    [FhirType("GuidanceResponse", IsResource=true)]
    [DataContract]
    public partial class GuidanceResponse : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.GuidanceResponse; } }
        [NotMapped]
        public override string TypeName { get { return "GuidanceResponse"; } }
        
        /// <summary>
        /// The status of a guidance response
        /// (url: http://hl7.org/fhir/ValueSet/guidance-response-status)
        /// </summary>
        [FhirEnumeration("GuidanceResponseStatus")]
        public enum GuidanceResponseStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("success"), Description("Success")]
            Success,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("data-requested"), Description("Data Requested")]
            DataRequested,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("data-required"), Description("Data Required")]
            DataRequired,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("in-progress"), Description("In Progress")]
            InProgress,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("failure"), Description("Failure")]
            Failure,
        }

        /// <summary>
        /// The type of action to be performed
        /// (url: http://hl7.org/fhir/ValueSet/action-type)
        /// </summary>
        [FhirEnumeration("ActionType")]
        public enum ActionType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/action-type)
            /// </summary>
            [EnumLiteral("create"), Description("Create")]
            Create,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/action-type)
            /// </summary>
            [EnumLiteral("update"), Description("Update")]
            Update,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/action-type)
            /// </summary>
            [EnumLiteral("remove"), Description("Remove")]
            Remove,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/action-type)
            /// </summary>
            [EnumLiteral("fire-event"), Description("Fire Event")]
            FireEvent,
        }

        [FhirType("ActionComponent")]
        [DataContract]
        public partial class ActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ActionComponent"; } }
            
            /// <summary>
            /// Unique identifier
            /// </summary>
            [FhirElement("actionIdentifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier ActionIdentifier
            {
                get { return _ActionIdentifier; }
                set { _ActionIdentifier = value; OnPropertyChanged("ActionIdentifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _ActionIdentifier;
            
            /// <summary>
            /// User-visible label for the action (e.g. 1. or A.)
            /// </summary>
            [FhirElement("label", Order=50)]
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
                    if(value == null)
                      LabelElement = null; 
                    else
                      LabelElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Label");
                }
            }
            
            /// <summary>
            /// User-visible title
            /// </summary>
            [FhirElement("title", Order=60)]
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
                    if(value == null)
                      TitleElement = null; 
                    else
                      TitleElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Title");
                }
            }
            
            /// <summary>
            /// Short description of the action
            /// </summary>
            [FhirElement("description", Order=70)]
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
                    if(value == null)
                      DescriptionElement = null; 
                    else
                      DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Static text equivalent of the action, used if the dynamic aspects cannot be interpreted by the receiving system
            /// </summary>
            [FhirElement("textEquivalent", Order=80)]
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
                    if(value == null)
                      TextEquivalentElement = null; 
                    else
                      TextEquivalentElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("TextEquivalent");
                }
            }
            
            /// <summary>
            /// The meaning of the action or its sub-actions
            /// </summary>
            [FhirElement("concept", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Concept
            {
                get { if(_Concept==null) _Concept = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Concept; }
                set { _Concept = value; OnPropertyChanged("Concept"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Concept;
            
            /// <summary>
            /// Evidence that supports taking the action
            /// </summary>
            [FhirElement("supportingEvidence", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Attachment> SupportingEvidence
            {
                get { if(_SupportingEvidence==null) _SupportingEvidence = new List<Hl7.Fhir.Model.Attachment>(); return _SupportingEvidence; }
                set { _SupportingEvidence = value; OnPropertyChanged("SupportingEvidence"); }
            }
            
            private List<Hl7.Fhir.Model.Attachment> _SupportingEvidence;
            
            /// <summary>
            /// Relationship to another action
            /// </summary>
            [FhirElement("relatedAction", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.GuidanceResponse.RelatedActionComponent RelatedAction
            {
                get { return _RelatedAction; }
                set { _RelatedAction = value; OnPropertyChanged("RelatedAction"); }
            }
            
            private Hl7.Fhir.Model.GuidanceResponse.RelatedActionComponent _RelatedAction;
            
            /// <summary>
            /// Supporting documentation for the intended performer of the action
            /// </summary>
            [FhirElement("documentation", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Attachment> Documentation
            {
                get { if(_Documentation==null) _Documentation = new List<Hl7.Fhir.Model.Attachment>(); return _Documentation; }
                set { _Documentation = value; OnPropertyChanged("Documentation"); }
            }
            
            private List<Hl7.Fhir.Model.Attachment> _Documentation;
            
            /// <summary>
            /// Participant
            /// </summary>
            [FhirElement("participant", Order=130)]
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
            public Code<Hl7.Fhir.Model.GuidanceResponse.ActionType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.GuidanceResponse.ActionType> _TypeElement;
            
            /// <summary>
            /// create | update | remove | fire-event
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.GuidanceResponse.ActionType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.GuidanceResponse.ActionType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Defines behaviors such as selection and grouping
            /// </summary>
            [FhirElement("behavior", Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.GuidanceResponse.BehaviorComponent> Behavior
            {
                get { if(_Behavior==null) _Behavior = new List<Hl7.Fhir.Model.GuidanceResponse.BehaviorComponent>(); return _Behavior; }
                set { _Behavior = value; OnPropertyChanged("Behavior"); }
            }
            
            private List<Hl7.Fhir.Model.GuidanceResponse.BehaviorComponent> _Behavior;
            
            /// <summary>
            /// The target of the action
            /// </summary>
            [FhirElement("resource", Order=160)]
            [References()]
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
            [FhirElement("action", Order=170)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent> _Action;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ActionIdentifier != null) dest.ActionIdentifier = (Hl7.Fhir.Model.Identifier)ActionIdentifier.DeepCopy();
                    if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(TextEquivalentElement != null) dest.TextEquivalentElement = (Hl7.Fhir.Model.FhirString)TextEquivalentElement.DeepCopy();
                    if(Concept != null) dest.Concept = new List<Hl7.Fhir.Model.CodeableConcept>(Concept.DeepCopy());
                    if(SupportingEvidence != null) dest.SupportingEvidence = new List<Hl7.Fhir.Model.Attachment>(SupportingEvidence.DeepCopy());
                    if(RelatedAction != null) dest.RelatedAction = (Hl7.Fhir.Model.GuidanceResponse.RelatedActionComponent)RelatedAction.DeepCopy();
                    if(Documentation != null) dest.Documentation = new List<Hl7.Fhir.Model.Attachment>(Documentation.DeepCopy());
                    if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.ResourceReference>(Participant.DeepCopy());
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.GuidanceResponse.ActionType>)TypeElement.DeepCopy();
                    if(Behavior != null) dest.Behavior = new List<Hl7.Fhir.Model.GuidanceResponse.BehaviorComponent>(Behavior.DeepCopy());
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent>(Action.DeepCopy());
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
                if( !DeepComparable.Matches(ActionIdentifier, otherT.ActionIdentifier)) return false;
                if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(TextEquivalentElement, otherT.TextEquivalentElement)) return false;
                if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
                if( !DeepComparable.Matches(SupportingEvidence, otherT.SupportingEvidence)) return false;
                if( !DeepComparable.Matches(RelatedAction, otherT.RelatedAction)) return false;
                if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Behavior, otherT.Behavior)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ActionIdentifier, otherT.ActionIdentifier)) return false;
                if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(TextEquivalentElement, otherT.TextEquivalentElement)) return false;
                if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
                if( !DeepComparable.IsExactly(SupportingEvidence, otherT.SupportingEvidence)) return false;
                if( !DeepComparable.IsExactly(RelatedAction, otherT.RelatedAction)) return false;
                if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Behavior, otherT.Behavior)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("RelatedActionComponent")]
        [DataContract]
        public partial class RelatedActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedActionComponent"; } }
            
            /// <summary>
            /// Identifier of the related action
            /// </summary>
            [FhirElement("actionIdentifier", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier ActionIdentifier
            {
                get { return _ActionIdentifier; }
                set { _ActionIdentifier = value; OnPropertyChanged("ActionIdentifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _ActionIdentifier;
            
            /// <summary>
            /// before | after
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
            /// before | after
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ActionRelationshipType? Relationship
            {
                get { return RelationshipElement != null ? RelationshipElement.Value : null; }
                set
                {
                    if(value == null)
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
            [AllowedTypes(typeof(Duration),typeof(Hl7.Fhir.Model.Range))]
            [DataMember]
            public Hl7.Fhir.Model.Element Offset
            {
                get { return _Offset; }
                set { _Offset = value; OnPropertyChanged("Offset"); }
            }
            
            private Hl7.Fhir.Model.Element _Offset;
            
            /// <summary>
            /// start | end
            /// </summary>
            [FhirElement("anchor", Order=70)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ActionRelationshipAnchor> AnchorElement
            {
                get { return _AnchorElement; }
                set { _AnchorElement = value; OnPropertyChanged("AnchorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ActionRelationshipAnchor> _AnchorElement;
            
            /// <summary>
            /// start | end
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ActionRelationshipAnchor? Anchor
            {
                get { return AnchorElement != null ? AnchorElement.Value : null; }
                set
                {
                    if(value == null)
                      AnchorElement = null; 
                    else
                      AnchorElement = new Code<Hl7.Fhir.Model.ActionRelationshipAnchor>(value);
                    OnPropertyChanged("Anchor");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ActionIdentifier != null) dest.ActionIdentifier = (Hl7.Fhir.Model.Identifier)ActionIdentifier.DeepCopy();
                    if(RelationshipElement != null) dest.RelationshipElement = (Code<Hl7.Fhir.Model.ActionRelationshipType>)RelationshipElement.DeepCopy();
                    if(Offset != null) dest.Offset = (Hl7.Fhir.Model.Element)Offset.DeepCopy();
                    if(AnchorElement != null) dest.AnchorElement = (Code<Hl7.Fhir.Model.ActionRelationshipAnchor>)AnchorElement.DeepCopy();
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
                if( !DeepComparable.Matches(ActionIdentifier, otherT.ActionIdentifier)) return false;
                if( !DeepComparable.Matches(RelationshipElement, otherT.RelationshipElement)) return false;
                if( !DeepComparable.Matches(Offset, otherT.Offset)) return false;
                if( !DeepComparable.Matches(AnchorElement, otherT.AnchorElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ActionIdentifier, otherT.ActionIdentifier)) return false;
                if( !DeepComparable.IsExactly(RelationshipElement, otherT.RelationshipElement)) return false;
                if( !DeepComparable.IsExactly(Offset, otherT.Offset)) return false;
                if( !DeepComparable.IsExactly(AnchorElement, otherT.AnchorElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("BehaviorComponent")]
        [DataContract]
        public partial class BehaviorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BehaviorComponent"; } }
            
            /// <summary>
            /// The type of behavior (grouping, precheck, selection, cardinality, etc)
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Specific behavior (e.g. required, at-most-one, single, etc)
            /// </summary>
            [FhirElement("value", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Coding _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BehaviorComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Coding)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new BehaviorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BehaviorComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BehaviorComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// The id of the request associated with this response, if any
        /// </summary>
        [FhirElement("requestId", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RequestIdElement
        {
            get { return _RequestIdElement; }
            set { _RequestIdElement = value; OnPropertyChanged("RequestIdElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RequestIdElement;
        
        /// <summary>
        /// The id of the request associated with this response, if any
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RequestId
        {
            get { return RequestIdElement != null ? RequestIdElement.Value : null; }
            set
            {
                if(value == null)
                  RequestIdElement = null; 
                else
                  RequestIdElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("RequestId");
            }
        }
        
        /// <summary>
        /// A reference to a knowledge module
        /// </summary>
        [FhirElement("module", InSummary=true, Order=100)]
        [References("DecisionSupportServiceModule","DecisionSupportRule")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Module
        {
            get { return _Module; }
            set { _Module = value; OnPropertyChanged("Module"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Module;
        
        /// <summary>
        /// success | data-requested | data-required | in-progress | failure
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus> _StatusElement;
        
        /// <summary>
        /// success | data-requested | data-required | in-progress | failure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Messages resulting from the evaluation of the artifact or artifacts
        /// </summary>
        [FhirElement("evaluationMessage", Order=120)]
        [References("OperationOutcome")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> EvaluationMessage
        {
            get { if(_EvaluationMessage==null) _EvaluationMessage = new List<Hl7.Fhir.Model.ResourceReference>(); return _EvaluationMessage; }
            set { _EvaluationMessage = value; OnPropertyChanged("EvaluationMessage"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _EvaluationMessage;
        
        /// <summary>
        /// The output parameters of the evaluation, if any
        /// </summary>
        [FhirElement("outputParameters", Order=130)]
        [References("Parameters")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference OutputParameters
        {
            get { return _OutputParameters; }
            set { _OutputParameters = value; OnPropertyChanged("OutputParameters"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _OutputParameters;
        
        /// <summary>
        /// Proposed actions, if any
        /// </summary>
        [FhirElement("action", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent> Action
        {
            get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent>(); return _Action; }
            set { _Action = value; OnPropertyChanged("Action"); }
        }
        
        private List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent> _Action;
        
        /// <summary>
        /// Additional required data
        /// </summary>
        [FhirElement("dataRequirement", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DataRequirement> DataRequirement
        {
            get { if(_DataRequirement==null) _DataRequirement = new List<DataRequirement>(); return _DataRequirement; }
            set { _DataRequirement = value; OnPropertyChanged("DataRequirement"); }
        }
        
        private List<DataRequirement> _DataRequirement;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as GuidanceResponse;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(RequestIdElement != null) dest.RequestIdElement = (Hl7.Fhir.Model.FhirString)RequestIdElement.DeepCopy();
                if(Module != null) dest.Module = (Hl7.Fhir.Model.ResourceReference)Module.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus>)StatusElement.DeepCopy();
                if(EvaluationMessage != null) dest.EvaluationMessage = new List<Hl7.Fhir.Model.ResourceReference>(EvaluationMessage.DeepCopy());
                if(OutputParameters != null) dest.OutputParameters = (Hl7.Fhir.Model.ResourceReference)OutputParameters.DeepCopy();
                if(Action != null) dest.Action = new List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent>(Action.DeepCopy());
                if(DataRequirement != null) dest.DataRequirement = new List<DataRequirement>(DataRequirement.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new GuidanceResponse());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as GuidanceResponse;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(RequestIdElement, otherT.RequestIdElement)) return false;
            if( !DeepComparable.Matches(Module, otherT.Module)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(EvaluationMessage, otherT.EvaluationMessage)) return false;
            if( !DeepComparable.Matches(OutputParameters, otherT.OutputParameters)) return false;
            if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            if( !DeepComparable.Matches(DataRequirement, otherT.DataRequirement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as GuidanceResponse;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(RequestIdElement, otherT.RequestIdElement)) return false;
            if( !DeepComparable.IsExactly(Module, otherT.Module)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(EvaluationMessage, otherT.EvaluationMessage)) return false;
            if( !DeepComparable.IsExactly(OutputParameters, otherT.OutputParameters)) return false;
            if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            if( !DeepComparable.IsExactly(DataRequirement, otherT.DataRequirement)) return false;
            
            return true;
        }
        
    }
    
}
