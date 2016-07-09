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
// Generated for FHIR v1.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// The definition of an action to be performed
    /// </summary>
    [FhirType("ActionDefinition")]
    [DataContract]
    public partial class ActionDefinition : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "ActionDefinition"; } }
        
        /// <summary>
        /// The type of participant for the action
        /// (url: http://hl7.org/fhir/ValueSet/action-participant-type)
        /// </summary>
        [FhirEnumeration("ParticipantType")]
        public enum ParticipantType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/action-participant-type)
            /// </summary>
            [EnumLiteral("patient"), Description("Patient")]
            Patient,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/action-participant-type)
            /// </summary>
            [EnumLiteral("practitioner"), Description("Practitioner")]
            Practitioner,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/action-participant-type)
            /// </summary>
            [EnumLiteral("related-person"), Description("Related Person")]
            RelatedPerson,
        }

        [FhirType("RelatedActionComponent")]
        [DataContract]
        public partial class RelatedActionComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedActionComponent"; } }
            
            /// <summary>
            /// Identifier of the related action
            /// </summary>
            [FhirElement("actionIdentifier", InSummary=true, Order=40)]
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
            [FhirElement("relationship", InSummary=true, Order=50)]
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
            [FhirElement("offset", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
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
            [FhirElement("anchor", InSummary=true, Order=70)]
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
        public partial class BehaviorComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BehaviorComponent"; } }
            
            /// <summary>
            /// The type of behavior (grouping, precheck, selection, cardinality, etc)
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
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
            [FhirElement("value", InSummary=true, Order=50)]
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
        
        
        [FhirType("CustomizationComponent")]
        [DataContract]
        public partial class CustomizationComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CustomizationComponent"; } }
            
            /// <summary>
            /// The path to the element to be set dynamically
            /// </summary>
            [FhirElement("path", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// The path to the element to be set dynamically
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if(value == null)
                      PathElement = null; 
                    else
                      PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// An expression that provides the dynamic value for the customization
            /// </summary>
            [FhirElement("expression", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ExpressionElement
            {
                get { return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ExpressionElement;
            
            /// <summary>
            /// An expression that provides the dynamic value for the customization
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Expression
            {
                get { return ExpressionElement != null ? ExpressionElement.Value : null; }
                set
                {
                    if(value == null)
                      ExpressionElement = null; 
                    else
                      ExpressionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Expression");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CustomizationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(ExpressionElement != null) dest.ExpressionElement = (Hl7.Fhir.Model.FhirString)ExpressionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CustomizationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CustomizationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CustomizationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Unique identifier
        /// </summary>
        [FhirElement("actionIdentifier", InSummary=true, Order=30)]
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
        [FhirElement("label", InSummary=true, Order=40)]
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
        [FhirElement("title", InSummary=true, Order=50)]
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
        [FhirElement("description", InSummary=true, Order=60)]
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
        [FhirElement("textEquivalent", InSummary=true, Order=70)]
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
        [FhirElement("concept", InSummary=true, Order=80)]
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
        [FhirElement("supportingEvidence", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> SupportingEvidence
        {
            get { if(_SupportingEvidence==null) _SupportingEvidence = new List<Hl7.Fhir.Model.Attachment>(); return _SupportingEvidence; }
            set { _SupportingEvidence = value; OnPropertyChanged("SupportingEvidence"); }
        }
        
        private List<Hl7.Fhir.Model.Attachment> _SupportingEvidence;
        
        /// <summary>
        /// Supporting documentation for the intended performer of the action
        /// </summary>
        [FhirElement("documentation", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Attachment> Documentation
        {
            get { if(_Documentation==null) _Documentation = new List<Hl7.Fhir.Model.Attachment>(); return _Documentation; }
            set { _Documentation = value; OnPropertyChanged("Documentation"); }
        }
        
        private List<Hl7.Fhir.Model.Attachment> _Documentation;
        
        /// <summary>
        /// Relationship to another action
        /// </summary>
        [FhirElement("relatedAction", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.ActionDefinition.RelatedActionComponent RelatedAction
        {
            get { return _RelatedAction; }
            set { _RelatedAction = value; OnPropertyChanged("RelatedAction"); }
        }
        
        private Hl7.Fhir.Model.ActionDefinition.RelatedActionComponent _RelatedAction;
        
        /// <summary>
        /// patient | practitioner | related-person
        /// </summary>
        [FhirElement("participantType", InSummary=true, Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.ActionDefinition.ParticipantType>> ParticipantType_Element
        {
            get { if(_ParticipantType_Element==null) _ParticipantType_Element = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ActionDefinition.ParticipantType>>(); return _ParticipantType_Element; }
            set { _ParticipantType_Element = value; OnPropertyChanged("ParticipantType_Element"); }
        }
        
        private List<Code<Hl7.Fhir.Model.ActionDefinition.ParticipantType>> _ParticipantType_Element;
        
        /// <summary>
        /// patient | practitioner | related-person
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.ActionDefinition.ParticipantType?> ParticipantType_
        {
            get { return ParticipantType_Element != null ? ParticipantType_Element.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  ParticipantType_Element = null; 
                else
                  ParticipantType_Element = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ActionDefinition.ParticipantType>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ActionDefinition.ParticipantType>(elem)));
                OnPropertyChanged("ParticipantType_");
            }
        }
        
        /// <summary>
        /// create | update | remove | fire-event
        /// </summary>
        [FhirElement("type", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Code TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Hl7.Fhir.Model.Code _TypeElement;
        
        /// <summary>
        /// create | update | remove | fire-event
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if(value == null)
                  TypeElement = null; 
                else
                  TypeElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Defines behaviors such as selection and grouping
        /// </summary>
        [FhirElement("behavior", InSummary=true, Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ActionDefinition.BehaviorComponent> Behavior
        {
            get { if(_Behavior==null) _Behavior = new List<Hl7.Fhir.Model.ActionDefinition.BehaviorComponent>(); return _Behavior; }
            set { _Behavior = value; OnPropertyChanged("Behavior"); }
        }
        
        private List<Hl7.Fhir.Model.ActionDefinition.BehaviorComponent> _Behavior;
        
        /// <summary>
        /// Static portion of the action definition
        /// </summary>
        [FhirElement("resource", InSummary=true, Order=150)]
        [References()]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Resource
        {
            get { return _Resource; }
            set { _Resource = value; OnPropertyChanged("Resource"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Resource;
        
        /// <summary>
        /// Dynamic aspects of the definition
        /// </summary>
        [FhirElement("customization", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ActionDefinition.CustomizationComponent> Customization
        {
            get { if(_Customization==null) _Customization = new List<Hl7.Fhir.Model.ActionDefinition.CustomizationComponent>(); return _Customization; }
            set { _Customization = value; OnPropertyChanged("Customization"); }
        }
        
        private List<Hl7.Fhir.Model.ActionDefinition.CustomizationComponent> _Customization;
        
        /// <summary>
        /// A sub-action
        /// </summary>
        [FhirElement("action", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ActionDefinition> Action
        {
            get { if(_Action==null) _Action = new List<ActionDefinition>(); return _Action; }
            set { _Action = value; OnPropertyChanged("Action"); }
        }
        
        private List<ActionDefinition> _Action;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ActionDefinition;
            
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
                if(Documentation != null) dest.Documentation = new List<Hl7.Fhir.Model.Attachment>(Documentation.DeepCopy());
                if(RelatedAction != null) dest.RelatedAction = (Hl7.Fhir.Model.ActionDefinition.RelatedActionComponent)RelatedAction.DeepCopy();
                if(ParticipantType_Element != null) dest.ParticipantType_Element = new List<Code<Hl7.Fhir.Model.ActionDefinition.ParticipantType>>(ParticipantType_Element.DeepCopy());
                if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                if(Behavior != null) dest.Behavior = new List<Hl7.Fhir.Model.ActionDefinition.BehaviorComponent>(Behavior.DeepCopy());
                if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                if(Customization != null) dest.Customization = new List<Hl7.Fhir.Model.ActionDefinition.CustomizationComponent>(Customization.DeepCopy());
                if(Action != null) dest.Action = new List<ActionDefinition>(Action.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ActionDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ActionDefinition;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(ActionIdentifier, otherT.ActionIdentifier)) return false;
            if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(TextEquivalentElement, otherT.TextEquivalentElement)) return false;
            if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
            if( !DeepComparable.Matches(SupportingEvidence, otherT.SupportingEvidence)) return false;
            if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
            if( !DeepComparable.Matches(RelatedAction, otherT.RelatedAction)) return false;
            if( !DeepComparable.Matches(ParticipantType_Element, otherT.ParticipantType_Element)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(Behavior, otherT.Behavior)) return false;
            if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
            if( !DeepComparable.Matches(Customization, otherT.Customization)) return false;
            if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ActionDefinition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(ActionIdentifier, otherT.ActionIdentifier)) return false;
            if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(TextEquivalentElement, otherT.TextEquivalentElement)) return false;
            if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
            if( !DeepComparable.IsExactly(SupportingEvidence, otherT.SupportingEvidence)) return false;
            if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
            if( !DeepComparable.IsExactly(RelatedAction, otherT.RelatedAction)) return false;
            if( !DeepComparable.IsExactly(ParticipantType_Element, otherT.ParticipantType_Element)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(Behavior, otherT.Behavior)) return false;
            if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
            if( !DeepComparable.IsExactly(Customization, otherT.Customization)) return false;
            if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            
            return true;
        }
        
    }
    
}
