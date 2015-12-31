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
// Generated for FHIR v1.2.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// The definition of an order set
    /// </summary>
    [FhirType("OrderSet", IsResource=true)]
    [DataContract]
    public partial class OrderSet : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.OrderSet; } }
        [NotMapped]
        public override string TypeName { get { return "OrderSet"; } }
        
        /// <summary>
        /// The type of participant for an action in the orderset
        /// (url: http://hl7.org/fhir/ValueSet/order-set-participant)
        /// </summary>
        [FhirEnumeration("OrderSetParticipantType")]
        public enum OrderSetParticipantType
        {
            /// <summary>
            /// The participant is the patient under evaluation
            /// (system: http://hl7.org/fhir/order-set-participant)
            /// </summary>
            [EnumLiteral("patient"), Description("Patient")]
            Patient,
            /// <summary>
            /// The participant is a person
            /// (system: http://hl7.org/fhir/order-set-participant)
            /// </summary>
            [EnumLiteral("person"), Description("Person")]
            Person,
            /// <summary>
            /// The participant is a practitioner involved in the patient's care
            /// (system: http://hl7.org/fhir/order-set-participant)
            /// </summary>
            [EnumLiteral("practitioner"), Description("Practitioner")]
            Practitioner,
            /// <summary>
            /// The participant is a person related to the patient
            /// (system: http://hl7.org/fhir/order-set-participant)
            /// </summary>
            [EnumLiteral("related-person"), Description("Related Person")]
            RelatedPerson,
        }

        /// <summary>
        /// Defines organization behavior of a group
        /// (url: http://hl7.org/fhir/ValueSet/grouping-behavior)
        /// </summary>
        [FhirEnumeration("OrderSetItemGroupingBehavior")]
        public enum OrderSetItemGroupingBehavior
        {
            /// <summary>
            /// Any group marked with this behavior should be displayed as a visual group to the end user
            /// (system: http://hl7.org/fhir/grouping-behavior)
            /// </summary>
            [EnumLiteral("visual-group"), Description("Visual Group")]
            VisualGroup,
            /// <summary>
            /// A group with this behavior logically groups its sub-elements, and may be shown as a visual group to the end user, but it is not required to do so
            /// (system: http://hl7.org/fhir/grouping-behavior)
            /// </summary>
            [EnumLiteral("logical-group"), Description("Logical Group")]
            LogicalGroup,
            /// <summary>
            /// A group of related alternative items is a sentence group if the target referenced by the item is the same in all the items, and each item simply constitutes a different variation on how to specify the details for the target. For example, two items that could be in a SentenceGroup are "aspirin, 500 mg, 2 times per day" and "aspirin, 300 mg, 3 times per day". In both cases, aspirin is the target referenced by the item, and the two items represent two different options for how aspirin might be ordered for the patient. Note that a SentenceGroup would almost always have an associated selection behavior of "AtMostOne", unless it's a required item, in which case, it would be "ExactlyOne"
            /// (system: http://hl7.org/fhir/grouping-behavior)
            /// </summary>
            [EnumLiteral("sentence-group"), Description("Sentence Group")]
            SentenceGroup,
        }

        /// <summary>
        /// Defines selection behavior of a group
        /// (url: http://hl7.org/fhir/ValueSet/selection-behavior)
        /// </summary>
        [FhirEnumeration("OrderSetItemSelectionBehavior")]
        public enum OrderSetItemSelectionBehavior
        {
            /// <summary>
            /// Any number of the items in the group may be chosen, from zero to all
            /// (system: http://hl7.org/fhir/selection-behavior)
            /// </summary>
            [EnumLiteral("any"), Description("Any")]
            Any,
            /// <summary>
            /// All the items in the group must be selected as a single unit
            /// (system: http://hl7.org/fhir/selection-behavior)
            /// </summary>
            [EnumLiteral("all"), Description("All")]
            All,
            /// <summary>
            /// All the items in the group are meant to be chosen as a single unit: either all must be selected by the end user, or none may be selected
            /// (system: http://hl7.org/fhir/selection-behavior)
            /// </summary>
            [EnumLiteral("all-or-none"), Description("All Or None")]
            AllOrNone,
            /// <summary>
            /// The end user must choose one and only one of the selectable items in the group. The user may not choose none of the items in the group
            /// (system: http://hl7.org/fhir/selection-behavior)
            /// </summary>
            [EnumLiteral("exactly-one"), Description("Exactly One")]
            ExactlyOne,
            /// <summary>
            /// The end user may choose zero or at most one of the items in the group
            /// (system: http://hl7.org/fhir/selection-behavior)
            /// </summary>
            [EnumLiteral("at-most-one"), Description("At Most One")]
            AtMostOne,
            /// <summary>
            /// The end user must choose a minimum of one, and as many additional as desired
            /// (system: http://hl7.org/fhir/selection-behavior)
            /// </summary>
            [EnumLiteral("one-or-more"), Description("One Or More")]
            OneOrMore,
        }

        /// <summary>
        /// Defines requiredness behavior for selecting an action or an action group
        /// (url: http://hl7.org/fhir/ValueSet/required-behavior)
        /// </summary>
        [FhirEnumeration("OrderSetItemRequiredBehavior")]
        public enum OrderSetItemRequiredBehavior
        {
            /// <summary>
            /// An item with this behavior must be included in the items processed by the end user; the end user may not choose not to include this item
            /// (system: http://hl7.org/fhir/required-behavior)
            /// </summary>
            [EnumLiteral("must"), Description("Must")]
            Must,
            /// <summary>
            /// An item with this behavior may be included in the set of items processed by the end user
            /// (system: http://hl7.org/fhir/required-behavior)
            /// </summary>
            [EnumLiteral("could"), Description("Could")]
            Could,
            /// <summary>
            /// An item with this behavior must be included in the set of items processed by the end user, unless the end user provides documentation as to why the item was not included
            /// (system: http://hl7.org/fhir/required-behavior)
            /// </summary>
            [EnumLiteral("must-unless-documented"), Description("Must Unless Documented")]
            MustUnlessDocumented,
        }

        /// <summary>
        /// Defines selection frequency behavior for an action or group
        /// (url: http://hl7.org/fhir/ValueSet/precheck-behavior)
        /// </summary>
        [FhirEnumeration("OrderSetItemPrecheckBehavior")]
        public enum OrderSetItemPrecheckBehavior
        {
            /// <summary>
            /// An item with this behavior is one of the most frequent items that is, or should be, included by an end user, for the particular context in which the item occurs. The system displaying the item to the end user should consider "pre-checking" such an item as a convenience for the user
            /// (system: http://hl7.org/fhir/precheck-behavior)
            /// </summary>
            [EnumLiteral("yes"), Description("Yes")]
            Yes,
            /// <summary>
            /// An item with this behavior is one of the less frequent items included by the end user, for the particular context in which the item occurs. The system displaying the items to the end user would typically not "pre-check" such an item
            /// (system: http://hl7.org/fhir/precheck-behavior)
            /// </summary>
            [EnumLiteral("no"), Description("No")]
            No,
        }

        /// <summary>
        /// Defines behavior for an action or a group for how many times that item may be repeated
        /// (url: http://hl7.org/fhir/ValueSet/cardinality-behavior)
        /// </summary>
        [FhirEnumeration("OrderSetItemCardinalityBehavior")]
        public enum OrderSetItemCardinalityBehavior
        {
            /// <summary>
            /// The item may only be selected one time
            /// (system: http://hl7.org/fhir/cardinality-behavior)
            /// </summary>
            [EnumLiteral("single"), Description("Single")]
            Single,
            /// <summary>
            /// The item may be selected multiple times
            /// (system: http://hl7.org/fhir/cardinality-behavior)
            /// </summary>
            [EnumLiteral("multiple"), Description("Multiple")]
            Multiple,
        }

        [FhirType("ItemComponent")]
        [DataContract]
        public partial class ItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ItemComponent"; } }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("number", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NumberElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Number
            {
                get { return NumberElement != null ? NumberElement.Value : null; }
                set
                {
                    if(value == null)
                      NumberElement = null; 
                    else
                      NumberElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// 
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
            /// 
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
            /// 
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
            /// 
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
            /// 
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
            /// 
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
            /// 
            /// </summary>
            [FhirElement("supportingEvidence", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Attachment> SupportingEvidence
            {
                get { if(_SupportingEvidence==null) _SupportingEvidence = new List<Hl7.Fhir.Model.Attachment>(); return _SupportingEvidence; }
                set { _SupportingEvidence = value; OnPropertyChanged("SupportingEvidence"); }
            }
            
            private List<Hl7.Fhir.Model.Attachment> _SupportingEvidence;
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("documentation", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Attachment> Documentation
            {
                get { if(_Documentation==null) _Documentation = new List<Hl7.Fhir.Model.Attachment>(); return _Documentation; }
                set { _Documentation = value; OnPropertyChanged("Documentation"); }
            }
            
            private List<Hl7.Fhir.Model.Attachment> _Documentation;
            
            /// <summary>
            /// patient | person | practitioner | related-person
            /// </summary>
            [FhirElement("participantType", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.OrderSet.OrderSetParticipantType>> ParticipantTypeElement
            {
                get { if(_ParticipantTypeElement==null) _ParticipantTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.OrderSet.OrderSetParticipantType>>(); return _ParticipantTypeElement; }
                set { _ParticipantTypeElement = value; OnPropertyChanged("ParticipantTypeElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.OrderSet.OrderSetParticipantType>> _ParticipantTypeElement;
            
            /// <summary>
            /// patient | person | practitioner | related-person
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.OrderSet.OrderSetParticipantType?> ParticipantType
            {
                get { return ParticipantTypeElement != null ? ParticipantTypeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      ParticipantTypeElement = null; 
                    else
                      ParticipantTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.OrderSet.OrderSetParticipantType>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.OrderSet.OrderSetParticipantType>(elem)));
                    OnPropertyChanged("ParticipantType");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("concept", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Concept
            {
                get { if(_Concept==null) _Concept = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Concept; }
                set { _Concept = value; OnPropertyChanged("Concept"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Concept;
            
            /// <summary>
            /// create | update | remove | fire-event
            /// </summary>
            [FhirElement("type", Order=130)]
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
            /// visual-group | logical-group | sentence-group
            /// </summary>
            [FhirElement("groupingBehavior", Order=140)]
            [DataMember]
            public Code<Hl7.Fhir.Model.OrderSet.OrderSetItemGroupingBehavior> GroupingBehaviorElement
            {
                get { return _GroupingBehaviorElement; }
                set { _GroupingBehaviorElement = value; OnPropertyChanged("GroupingBehaviorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.OrderSet.OrderSetItemGroupingBehavior> _GroupingBehaviorElement;
            
            /// <summary>
            /// visual-group | logical-group | sentence-group
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.OrderSet.OrderSetItemGroupingBehavior? GroupingBehavior
            {
                get { return GroupingBehaviorElement != null ? GroupingBehaviorElement.Value : null; }
                set
                {
                    if(value == null)
                      GroupingBehaviorElement = null; 
                    else
                      GroupingBehaviorElement = new Code<Hl7.Fhir.Model.OrderSet.OrderSetItemGroupingBehavior>(value);
                    OnPropertyChanged("GroupingBehavior");
                }
            }
            
            /// <summary>
            /// any | all | all-or-none | exactly-one | at-most-one | one-or-more
            /// </summary>
            [FhirElement("selectionBehavior", Order=150)]
            [DataMember]
            public Code<Hl7.Fhir.Model.OrderSet.OrderSetItemSelectionBehavior> SelectionBehaviorElement
            {
                get { return _SelectionBehaviorElement; }
                set { _SelectionBehaviorElement = value; OnPropertyChanged("SelectionBehaviorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.OrderSet.OrderSetItemSelectionBehavior> _SelectionBehaviorElement;
            
            /// <summary>
            /// any | all | all-or-none | exactly-one | at-most-one | one-or-more
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.OrderSet.OrderSetItemSelectionBehavior? SelectionBehavior
            {
                get { return SelectionBehaviorElement != null ? SelectionBehaviorElement.Value : null; }
                set
                {
                    if(value == null)
                      SelectionBehaviorElement = null; 
                    else
                      SelectionBehaviorElement = new Code<Hl7.Fhir.Model.OrderSet.OrderSetItemSelectionBehavior>(value);
                    OnPropertyChanged("SelectionBehavior");
                }
            }
            
            /// <summary>
            /// must | could | must-unless-documented
            /// </summary>
            [FhirElement("requiredBehavior", Order=160)]
            [DataMember]
            public Code<Hl7.Fhir.Model.OrderSet.OrderSetItemRequiredBehavior> RequiredBehaviorElement
            {
                get { return _RequiredBehaviorElement; }
                set { _RequiredBehaviorElement = value; OnPropertyChanged("RequiredBehaviorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.OrderSet.OrderSetItemRequiredBehavior> _RequiredBehaviorElement;
            
            /// <summary>
            /// must | could | must-unless-documented
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.OrderSet.OrderSetItemRequiredBehavior? RequiredBehavior
            {
                get { return RequiredBehaviorElement != null ? RequiredBehaviorElement.Value : null; }
                set
                {
                    if(value == null)
                      RequiredBehaviorElement = null; 
                    else
                      RequiredBehaviorElement = new Code<Hl7.Fhir.Model.OrderSet.OrderSetItemRequiredBehavior>(value);
                    OnPropertyChanged("RequiredBehavior");
                }
            }
            
            /// <summary>
            /// yes | no
            /// </summary>
            [FhirElement("precheckBehavior", Order=170)]
            [DataMember]
            public Code<Hl7.Fhir.Model.OrderSet.OrderSetItemPrecheckBehavior> PrecheckBehaviorElement
            {
                get { return _PrecheckBehaviorElement; }
                set { _PrecheckBehaviorElement = value; OnPropertyChanged("PrecheckBehaviorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.OrderSet.OrderSetItemPrecheckBehavior> _PrecheckBehaviorElement;
            
            /// <summary>
            /// yes | no
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.OrderSet.OrderSetItemPrecheckBehavior? PrecheckBehavior
            {
                get { return PrecheckBehaviorElement != null ? PrecheckBehaviorElement.Value : null; }
                set
                {
                    if(value == null)
                      PrecheckBehaviorElement = null; 
                    else
                      PrecheckBehaviorElement = new Code<Hl7.Fhir.Model.OrderSet.OrderSetItemPrecheckBehavior>(value);
                    OnPropertyChanged("PrecheckBehavior");
                }
            }
            
            /// <summary>
            /// single | multiple
            /// </summary>
            [FhirElement("cardinalityBehavior", Order=180)]
            [DataMember]
            public Code<Hl7.Fhir.Model.OrderSet.OrderSetItemCardinalityBehavior> CardinalityBehaviorElement
            {
                get { return _CardinalityBehaviorElement; }
                set { _CardinalityBehaviorElement = value; OnPropertyChanged("CardinalityBehaviorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.OrderSet.OrderSetItemCardinalityBehavior> _CardinalityBehaviorElement;
            
            /// <summary>
            /// single | multiple
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.OrderSet.OrderSetItemCardinalityBehavior? CardinalityBehavior
            {
                get { return CardinalityBehaviorElement != null ? CardinalityBehaviorElement.Value : null; }
                set
                {
                    if(value == null)
                      CardinalityBehaviorElement = null; 
                    else
                      CardinalityBehaviorElement = new Code<Hl7.Fhir.Model.OrderSet.OrderSetItemCardinalityBehavior>(value);
                    OnPropertyChanged("CardinalityBehavior");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("resource", Order=190)]
            [References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Resource;
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("customization", Order=200)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.OrderSet.CustomizationComponent> Customization
            {
                get { if(_Customization==null) _Customization = new List<Hl7.Fhir.Model.OrderSet.CustomizationComponent>(); return _Customization; }
                set { _Customization = value; OnPropertyChanged("Customization"); }
            }
            
            private List<Hl7.Fhir.Model.OrderSet.CustomizationComponent> _Customization;
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("items", Order=210)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.OrderSet.ItemComponent> Items
            {
                get { if(_Items==null) _Items = new List<Hl7.Fhir.Model.OrderSet.ItemComponent>(); return _Items; }
                set { _Items = value; OnPropertyChanged("Items"); }
            }
            
            private List<Hl7.Fhir.Model.OrderSet.ItemComponent> _Items;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.FhirString)NumberElement.DeepCopy();
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(TextEquivalentElement != null) dest.TextEquivalentElement = (Hl7.Fhir.Model.FhirString)TextEquivalentElement.DeepCopy();
                    if(SupportingEvidence != null) dest.SupportingEvidence = new List<Hl7.Fhir.Model.Attachment>(SupportingEvidence.DeepCopy());
                    if(Documentation != null) dest.Documentation = new List<Hl7.Fhir.Model.Attachment>(Documentation.DeepCopy());
                    if(ParticipantTypeElement != null) dest.ParticipantTypeElement = new List<Code<Hl7.Fhir.Model.OrderSet.OrderSetParticipantType>>(ParticipantTypeElement.DeepCopy());
                    if(Concept != null) dest.Concept = new List<Hl7.Fhir.Model.CodeableConcept>(Concept.DeepCopy());
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                    if(GroupingBehaviorElement != null) dest.GroupingBehaviorElement = (Code<Hl7.Fhir.Model.OrderSet.OrderSetItemGroupingBehavior>)GroupingBehaviorElement.DeepCopy();
                    if(SelectionBehaviorElement != null) dest.SelectionBehaviorElement = (Code<Hl7.Fhir.Model.OrderSet.OrderSetItemSelectionBehavior>)SelectionBehaviorElement.DeepCopy();
                    if(RequiredBehaviorElement != null) dest.RequiredBehaviorElement = (Code<Hl7.Fhir.Model.OrderSet.OrderSetItemRequiredBehavior>)RequiredBehaviorElement.DeepCopy();
                    if(PrecheckBehaviorElement != null) dest.PrecheckBehaviorElement = (Code<Hl7.Fhir.Model.OrderSet.OrderSetItemPrecheckBehavior>)PrecheckBehaviorElement.DeepCopy();
                    if(CardinalityBehaviorElement != null) dest.CardinalityBehaviorElement = (Code<Hl7.Fhir.Model.OrderSet.OrderSetItemCardinalityBehavior>)CardinalityBehaviorElement.DeepCopy();
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                    if(Customization != null) dest.Customization = new List<Hl7.Fhir.Model.OrderSet.CustomizationComponent>(Customization.DeepCopy());
                    if(Items != null) dest.Items = new List<Hl7.Fhir.Model.OrderSet.ItemComponent>(Items.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(TextEquivalentElement, otherT.TextEquivalentElement)) return false;
                if( !DeepComparable.Matches(SupportingEvidence, otherT.SupportingEvidence)) return false;
                if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.Matches(ParticipantTypeElement, otherT.ParticipantTypeElement)) return false;
                if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(GroupingBehaviorElement, otherT.GroupingBehaviorElement)) return false;
                if( !DeepComparable.Matches(SelectionBehaviorElement, otherT.SelectionBehaviorElement)) return false;
                if( !DeepComparable.Matches(RequiredBehaviorElement, otherT.RequiredBehaviorElement)) return false;
                if( !DeepComparable.Matches(PrecheckBehaviorElement, otherT.PrecheckBehaviorElement)) return false;
                if( !DeepComparable.Matches(CardinalityBehaviorElement, otherT.CardinalityBehaviorElement)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Customization, otherT.Customization)) return false;
                if( !DeepComparable.Matches(Items, otherT.Items)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(TextEquivalentElement, otherT.TextEquivalentElement)) return false;
                if( !DeepComparable.IsExactly(SupportingEvidence, otherT.SupportingEvidence)) return false;
                if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.IsExactly(ParticipantTypeElement, otherT.ParticipantTypeElement)) return false;
                if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(GroupingBehaviorElement, otherT.GroupingBehaviorElement)) return false;
                if( !DeepComparable.IsExactly(SelectionBehaviorElement, otherT.SelectionBehaviorElement)) return false;
                if( !DeepComparable.IsExactly(RequiredBehaviorElement, otherT.RequiredBehaviorElement)) return false;
                if( !DeepComparable.IsExactly(PrecheckBehaviorElement, otherT.PrecheckBehaviorElement)) return false;
                if( !DeepComparable.IsExactly(CardinalityBehaviorElement, otherT.CardinalityBehaviorElement)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Customization, otherT.Customization)) return false;
                if( !DeepComparable.IsExactly(Items, otherT.Items)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CustomizationComponent")]
        [DataContract]
        public partial class CustomizationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CustomizationComponent"; } }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("path", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// 
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
            /// 
            /// </summary>
            [FhirElement("expression", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ExpressionElement
            {
                get { return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ExpressionElement;
            
            /// <summary>
            /// 
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
        /// Logical identifier
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// The version of the module, if any
        /// </summary>
        [FhirElement("version", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// The version of the module, if any
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if(value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// The metadata for the orderset
        /// </summary>
        [FhirElement("moduleMetadata", Order=110)]
        [References("ModuleMetadata")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ModuleMetadata
        {
            get { return _ModuleMetadata; }
            set { _ModuleMetadata = value; OnPropertyChanged("ModuleMetadata"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ModuleMetadata;
        
        /// <summary>
        /// Logic used by the orderset
        /// </summary>
        [FhirElement("library", Order=120)]
        [References("Library")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Library
        {
            get { if(_Library==null) _Library = new List<Hl7.Fhir.Model.ResourceReference>(); return _Library; }
            set { _Library = value; OnPropertyChanged("Library"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Library;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("item", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.OrderSet.ItemComponent> Item
        {
            get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.OrderSet.ItemComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        
        private List<Hl7.Fhir.Model.OrderSet.ItemComponent> _Item;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as OrderSet;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(ModuleMetadata != null) dest.ModuleMetadata = (Hl7.Fhir.Model.ResourceReference)ModuleMetadata.DeepCopy();
                if(Library != null) dest.Library = new List<Hl7.Fhir.Model.ResourceReference>(Library.DeepCopy());
                if(Item != null) dest.Item = new List<Hl7.Fhir.Model.OrderSet.ItemComponent>(Item.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new OrderSet());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as OrderSet;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.Matches(Library, otherT.Library)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as OrderSet;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.IsExactly(Library, otherT.Library)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            
            return true;
        }
        
    }
    
}
