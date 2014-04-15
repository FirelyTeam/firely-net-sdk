using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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
// Generated on Tue, Apr 15, 2014 17:48+0200 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Resource Profile
    /// </summary>
    [FhirType("Profile", IsResource=true)]
    [DataContract]
    public partial class Profile : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Binding conformance for applications
        /// </summary>
        [FhirEnumeration("BindingConformance")]
        public enum BindingConformance
        {
            [EnumLiteral("required")]
            Required, // Only codes in the specified set are allowed.  If the binding is extensible, other codes may be used for concepts not covered by the bound set of codes.
            [EnumLiteral("preferred")]
            Preferred, // For greater interoperability, implementers are strongly encouraged to use the bound set of codes, however alternate codes may be used in derived profiles and implementations if necessary without being considered non-conformant.
            [EnumLiteral("example")]
            Example, // The codes in the set are an example to illustrate the meaning of the field. There is no particular preference for its use nor any assertion that the provided values are sufficient to meet implementation needs.
        }
        
        /// <summary>
        /// SHALL applications comply with this constraint?
        /// </summary>
        [FhirEnumeration("ConstraintSeverity")]
        public enum ConstraintSeverity
        {
            [EnumLiteral("error")]
            Error, // If the constraint is violated, the resource is not conformant.
            [EnumLiteral("warning")]
            Warning, // If the constraint is violated, the resource is conformant, but it is not necessarily following best practice.
        }
        
        /// <summary>
        /// The lifecycle status of a Resource Profile
        /// </summary>
        [FhirEnumeration("ResourceProfileStatus")]
        public enum ResourceProfileStatus
        {
            [EnumLiteral("draft")]
            Draft, // This profile is still under development.
            [EnumLiteral("active")]
            Active, // This profile is ready for normal use.
            [EnumLiteral("retired")]
            Retired, // This profile has been deprecated, withdrawn or superseded and should no longer be used.
        }
        
        /// <summary>
        /// How a property is represented on the wire
        /// </summary>
        [FhirEnumeration("PropertyRepresentation")]
        public enum PropertyRepresentation
        {
            [EnumLiteral("xmlAttr")]
            XmlAttr, // In XML, this property is represented as an attribute not an element.
        }
        
        /// <summary>
        /// How resource references can be aggregated
        /// </summary>
        [FhirEnumeration("AggregationMode")]
        public enum AggregationMode
        {
            [EnumLiteral("contained")]
            Contained, // The reference is a local reference to a contained resource.
            [EnumLiteral("referenced")]
            Referenced, // The reference to to a resource that has to be resolved externally to the resource that includes the reference.
            [EnumLiteral("bundled")]
            Bundled, // The resource the reference points to will be found in the same bundle as the resource that includes the reference.
        }
        
        /// <summary>
        /// How an extension context is interpreted
        /// </summary>
        [FhirEnumeration("ExtensionContext")]
        public enum ExtensionContext
        {
            [EnumLiteral("resource")]
            Resource, // The context is all elements matching a particular resource element path.
            [EnumLiteral("datatype")]
            Datatype, // The context is all nodes matching a particular data type element path (root or repeating element) or all elements referencing a particular primitive data type (expressed as the datatype name).
            [EnumLiteral("mapping")]
            Mapping, // The context is all nodes whose mapping to a specified reference model corresponds to a particular mapping structure.  The context identifies the mapping target. The mapping should clearly identify where such an extension could be used.
            [EnumLiteral("extension")]
            Extension, // The context is a particular extension from a particular profile.  Expressed as uri#name, where uri identifies the profile and #name identifies the extension code.
        }
        
        /// <summary>
        /// How slices are interpreted when evaluating an instance
        /// </summary>
        [FhirEnumeration("SlicingRules")]
        public enum SlicingRules
        {
            [EnumLiteral("closed")]
            Closed, // No additional content is allowed other than that described by the slices in this profile.
            [EnumLiteral("open")]
            Open, // Additional content is allowed anywhere in the list.
            [EnumLiteral("openAtEnd")]
            OpenAtEnd, // Additional content is allowed, but only at the end of the list.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ElementDefinitionComponent")]
        [DataContract]
        public partial class ElementDefinitionComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Concise definition for xml presentation
            /// </summary>
            [FhirElement("short", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ShortElement
            {
                get { return _ShortElement; }
                set { _ShortElement = value; OnPropertyChanged("ShortElement"); }
            }
            private Hl7.Fhir.Model.FhirString _ShortElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Short
            {
                get { return ShortElement != null ? ShortElement.Value : null; }
                set
                {
                    if(value == null)
                      ShortElement = null; 
                    else
                      ShortElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Short");
                }
            }
            
            /// <summary>
            /// Full formal definition in human language
            /// </summary>
            [FhirElement("formal", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString FormalElement
            {
                get { return _FormalElement; }
                set { _FormalElement = value; OnPropertyChanged("FormalElement"); }
            }
            private Hl7.Fhir.Model.FhirString _FormalElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Formal
            {
                get { return FormalElement != null ? FormalElement.Value : null; }
                set
                {
                    if(value == null)
                      FormalElement = null; 
                    else
                      FormalElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Formal");
                }
            }
            
            /// <summary>
            /// Comments about the use of this element
            /// </summary>
            [FhirElement("comments", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentsElement
            {
                get { return _CommentsElement; }
                set { _CommentsElement = value; OnPropertyChanged("CommentsElement"); }
            }
            private Hl7.Fhir.Model.FhirString _CommentsElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Comments
            {
                get { return CommentsElement != null ? CommentsElement.Value : null; }
                set
                {
                    if(value == null)
                      CommentsElement = null; 
                    else
                      CommentsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Comments");
                }
            }
            
            /// <summary>
            /// Why is this needed?
            /// </summary>
            [FhirElement("requirements", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RequirementsElement
            {
                get { return _RequirementsElement; }
                set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
            }
            private Hl7.Fhir.Model.FhirString _RequirementsElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Requirements
            {
                get { return RequirementsElement != null ? RequirementsElement.Value : null; }
                set
                {
                    if(value == null)
                      RequirementsElement = null; 
                    else
                      RequirementsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Requirements");
                }
            }
            
            /// <summary>
            /// Other names
            /// </summary>
            [FhirElement("synonym", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> SynonymElement
            {
                get { return _SynonymElement; }
                set { _SynonymElement = value; OnPropertyChanged("SynonymElement"); }
            }
            private List<Hl7.Fhir.Model.FhirString> _SynonymElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Synonym
            {
                get { return SynonymElement != null ? SynonymElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      SynonymElement = null; 
                    else
                      SynonymElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Synonym");
                }
            }
            
            /// <summary>
            /// Minimum Cardinality
            /// </summary>
            [FhirElement("min", InSummary=true, Order=90)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer MinElement
            {
                get { return _MinElement; }
                set { _MinElement = value; OnPropertyChanged("MinElement"); }
            }
            private Hl7.Fhir.Model.Integer _MinElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Min
            {
                get { return MinElement != null ? MinElement.Value : null; }
                set
                {
                    if(value == null)
                      MinElement = null; 
                    else
                      MinElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Min");
                }
            }
            
            /// <summary>
            /// Maximum Cardinality (a number or *)
            /// </summary>
            [FhirElement("max", InSummary=true, Order=100)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MaxElement
            {
                get { return _MaxElement; }
                set { _MaxElement = value; OnPropertyChanged("MaxElement"); }
            }
            private Hl7.Fhir.Model.FhirString _MaxElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Max
            {
                get { return MaxElement != null ? MaxElement.Value : null; }
                set
                {
                    if(value == null)
                      MaxElement = null; 
                    else
                      MaxElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Max");
                }
            }
            
            /// <summary>
            /// Data type and Profile for this element
            /// </summary>
            [FhirElement("type", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Profile.TypeRefComponent> Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private List<Hl7.Fhir.Model.Profile.TypeRefComponent> _Type;
            
            /// <summary>
            /// To another element constraint (by element.name)
            /// </summary>
            [FhirElement("nameReference", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameReferenceElement
            {
                get { return _NameReferenceElement; }
                set { _NameReferenceElement = value; OnPropertyChanged("NameReferenceElement"); }
            }
            private Hl7.Fhir.Model.FhirString _NameReferenceElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string NameReference
            {
                get { return NameReferenceElement != null ? NameReferenceElement.Value : null; }
                set
                {
                    if(value == null)
                      NameReferenceElement = null; 
                    else
                      NameReferenceElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("NameReference");
                }
            }
            
            /// <summary>
            /// Fixed value: [as defined for a primitive type]
            /// </summary>
            [FhirElement("value", InSummary=true, Order=130, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Element))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            private Hl7.Fhir.Model.Element _Value;
            
            /// <summary>
            /// Example value: [as defined for type]
            /// </summary>
            [FhirElement("example", InSummary=true, Order=140, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Element))]
            [DataMember]
            public Hl7.Fhir.Model.Element Example
            {
                get { return _Example; }
                set { _Example = value; OnPropertyChanged("Example"); }
            }
            private Hl7.Fhir.Model.Element _Example;
            
            /// <summary>
            /// Length for strings
            /// </summary>
            [FhirElement("maxLength", InSummary=true, Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.Integer MaxLengthElement
            {
                get { return _MaxLengthElement; }
                set { _MaxLengthElement = value; OnPropertyChanged("MaxLengthElement"); }
            }
            private Hl7.Fhir.Model.Integer _MaxLengthElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? MaxLength
            {
                get { return MaxLengthElement != null ? MaxLengthElement.Value : null; }
                set
                {
                    if(value == null)
                      MaxLengthElement = null; 
                    else
                      MaxLengthElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("MaxLength");
                }
            }
            
            /// <summary>
            /// Reference to invariant about presence
            /// </summary>
            [FhirElement("condition", InSummary=true, Order=160)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Id> ConditionElement
            {
                get { return _ConditionElement; }
                set { _ConditionElement = value; OnPropertyChanged("ConditionElement"); }
            }
            private List<Hl7.Fhir.Model.Id> _ConditionElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Condition
            {
                get { return ConditionElement != null ? ConditionElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      ConditionElement = null; 
                    else
                      ConditionElement = new List<Hl7.Fhir.Model.Id>(value.Select(elem=>new Hl7.Fhir.Model.Id(elem)));
                    OnPropertyChanged("Condition");
                }
            }
            
            /// <summary>
            /// Condition that must evaluate to true
            /// </summary>
            [FhirElement("constraint", InSummary=true, Order=170)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Profile.ElementDefinitionConstraintComponent> Constraint
            {
                get { return _Constraint; }
                set { _Constraint = value; OnPropertyChanged("Constraint"); }
            }
            private List<Hl7.Fhir.Model.Profile.ElementDefinitionConstraintComponent> _Constraint;
            
            /// <summary>
            /// If the element must supported
            /// </summary>
            [FhirElement("mustSupport", InSummary=true, Order=180)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean MustSupportElement
            {
                get { return _MustSupportElement; }
                set { _MustSupportElement = value; OnPropertyChanged("MustSupportElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _MustSupportElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? MustSupport
            {
                get { return MustSupportElement != null ? MustSupportElement.Value : null; }
                set
                {
                    if(value == null)
                      MustSupportElement = null; 
                    else
                      MustSupportElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("MustSupport");
                }
            }
            
            /// <summary>
            /// If this modifies the meaning of other elements
            /// </summary>
            [FhirElement("isModifier", InSummary=true, Order=190)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean IsModifierElement
            {
                get { return _IsModifierElement; }
                set { _IsModifierElement = value; OnPropertyChanged("IsModifierElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _IsModifierElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? IsModifier
            {
                get { return IsModifierElement != null ? IsModifierElement.Value : null; }
                set
                {
                    if(value == null)
                      IsModifierElement = null; 
                    else
                      IsModifierElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("IsModifier");
                }
            }
            
            /// <summary>
            /// ValueSet details if this is coded
            /// </summary>
            [FhirElement("binding", InSummary=true, Order=200)]
            [DataMember]
            public Hl7.Fhir.Model.Profile.ElementDefinitionBindingComponent Binding
            {
                get { return _Binding; }
                set { _Binding = value; OnPropertyChanged("Binding"); }
            }
            private Hl7.Fhir.Model.Profile.ElementDefinitionBindingComponent _Binding;
            
            /// <summary>
            /// Map element to another set of definitions
            /// </summary>
            [FhirElement("mapping", InSummary=true, Order=210)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Profile.ElementDefinitionMappingComponent> Mapping
            {
                get { return _Mapping; }
                set { _Mapping = value; OnPropertyChanged("Mapping"); }
            }
            private List<Hl7.Fhir.Model.Profile.ElementDefinitionMappingComponent> _Mapping;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ElementSlicingComponent")]
        [DataContract]
        public partial class ElementSlicingComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Element that used to distinguish the slices
            /// </summary>
            [FhirElement("discriminator", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id DiscriminatorElement
            {
                get { return _DiscriminatorElement; }
                set { _DiscriminatorElement = value; OnPropertyChanged("DiscriminatorElement"); }
            }
            private Hl7.Fhir.Model.Id _DiscriminatorElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Discriminator
            {
                get { return DiscriminatorElement != null ? DiscriminatorElement.Value : null; }
                set
                {
                    if(value == null)
                      DiscriminatorElement = null; 
                    else
                      DiscriminatorElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Discriminator");
                }
            }
            
            /// <summary>
            /// If elements must be in same order as slices
            /// </summary>
            [FhirElement("ordered", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean OrderedElement
            {
                get { return _OrderedElement; }
                set { _OrderedElement = value; OnPropertyChanged("OrderedElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _OrderedElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Ordered
            {
                get { return OrderedElement != null ? OrderedElement.Value : null; }
                set
                {
                    if(value == null)
                      OrderedElement = null; 
                    else
                      OrderedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Ordered");
                }
            }
            
            /// <summary>
            /// closed | open | openAtEnd
            /// </summary>
            [FhirElement("rules", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Profile.SlicingRules> RulesElement
            {
                get { return _RulesElement; }
                set { _RulesElement = value; OnPropertyChanged("RulesElement"); }
            }
            private Code<Hl7.Fhir.Model.Profile.SlicingRules> _RulesElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Profile.SlicingRules? Rules
            {
                get { return RulesElement != null ? RulesElement.Value : null; }
                set
                {
                    if(value == null)
                      RulesElement = null; 
                    else
                      RulesElement = new Code<Hl7.Fhir.Model.Profile.SlicingRules>(value);
                    OnPropertyChanged("Rules");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProfileStructureComponent")]
        [DataContract]
        public partial class ProfileStructureComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// The Resource or Data Type being described
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Hl7.Fhir.Model.Code _TypeElement;
            
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
            /// Name for this particular structure (reference target)
            /// </summary>
            [FhirElement("name", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// This definition is published (i.e. for validation)
            /// </summary>
            [FhirElement("publish", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean PublishElement
            {
                get { return _PublishElement; }
                set { _PublishElement = value; OnPropertyChanged("PublishElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _PublishElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Publish
            {
                get { return PublishElement != null ? PublishElement.Value : null; }
                set
                {
                    if(value == null)
                      PublishElement = null; 
                    else
                      PublishElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Publish");
                }
            }
            
            /// <summary>
            /// Human summary: why describe this resource?
            /// </summary>
            [FhirElement("purpose", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PurposeElement
            {
                get { return _PurposeElement; }
                set { _PurposeElement = value; OnPropertyChanged("PurposeElement"); }
            }
            private Hl7.Fhir.Model.FhirString _PurposeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Purpose
            {
                get { return PurposeElement != null ? PurposeElement.Value : null; }
                set
                {
                    if(value == null)
                      PurposeElement = null; 
                    else
                      PurposeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Purpose");
                }
            }
            
            /// <summary>
            /// Definition of elements in the resource (if no profile)
            /// </summary>
            [FhirElement("element", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Profile.ElementComponent> Element
            {
                get { return _Element; }
                set { _Element = value; OnPropertyChanged("Element"); }
            }
            private List<Hl7.Fhir.Model.Profile.ElementComponent> _Element;
            
            /// <summary>
            /// Search params defined
            /// </summary>
            [FhirElement("searchParam", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Profile.ProfileStructureSearchParamComponent> SearchParam
            {
                get { return _SearchParam; }
                set { _SearchParam = value; OnPropertyChanged("SearchParam"); }
            }
            private List<Hl7.Fhir.Model.Profile.ProfileStructureSearchParamComponent> _SearchParam;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProfileStructureSearchParamComponent")]
        [DataContract]
        public partial class ProfileStructureSearchParamComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Name of search parameter
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// number | date | string | token | reference | composite | quantity
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Hl7.Fhir.Model.Code _TypeElement;
            
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
            /// Contents and meaning of search parameter
            /// </summary>
            [FhirElement("documentation", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if(value == null)
                      DocumentationElement = null; 
                    else
                      DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
            /// <summary>
            /// XPath that extracts the parameter set
            /// </summary>
            [FhirElement("xpath", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString XpathElement
            {
                get { return _XpathElement; }
                set { _XpathElement = value; OnPropertyChanged("XpathElement"); }
            }
            private Hl7.Fhir.Model.FhirString _XpathElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Xpath
            {
                get { return XpathElement != null ? XpathElement.Value : null; }
                set
                {
                    if(value == null)
                      XpathElement = null; 
                    else
                      XpathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Xpath");
                }
            }
            
            /// <summary>
            /// Types of resource (if a resource reference)
            /// </summary>
            [FhirElement("target", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Code> TargetElement
            {
                get { return _TargetElement; }
                set { _TargetElement = value; OnPropertyChanged("TargetElement"); }
            }
            private List<Hl7.Fhir.Model.Code> _TargetElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Target
            {
                get { return TargetElement != null ? TargetElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      TargetElement = null; 
                    else
                      TargetElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem=>new Hl7.Fhir.Model.Code(elem)));
                    OnPropertyChanged("Target");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProfileQueryComponent")]
        [DataContract]
        public partial class ProfileQueryComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Special named queries (_query=)
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Describes the named query
            /// </summary>
            [FhirElement("documentation", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if(value == null)
                      DocumentationElement = null; 
                    else
                      DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
            /// <summary>
            /// Parameter for the named query
            /// </summary>
            [FhirElement("parameter", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Profile.ProfileStructureSearchParamComponent> Parameter
            {
                get { return _Parameter; }
                set { _Parameter = value; OnPropertyChanged("Parameter"); }
            }
            private List<Hl7.Fhir.Model.Profile.ProfileStructureSearchParamComponent> _Parameter;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("TypeRefComponent")]
        [DataContract]
        public partial class TypeRefComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Name of Data type or Resource
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            private Hl7.Fhir.Model.Code _CodeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Profile.structure to apply
            /// </summary>
            [FhirElement("profile", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri ProfileElement
            {
                get { return _ProfileElement; }
                set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _ProfileElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri Profile
            {
                get { return ProfileElement != null ? ProfileElement.Value : null; }
                set
                {
                    if(value == null)
                      ProfileElement = null; 
                    else
                      ProfileElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Profile");
                }
            }
            
            /// <summary>
            /// contained | referenced | bundled - how aggregated
            /// </summary>
            [FhirElement("aggregation", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.Profile.AggregationMode>> AggregationElement
            {
                get { return _AggregationElement; }
                set { _AggregationElement = value; OnPropertyChanged("AggregationElement"); }
            }
            private List<Code<Hl7.Fhir.Model.Profile.AggregationMode>> _AggregationElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.Profile.AggregationMode?> Aggregation
            {
                get { return AggregationElement != null ? AggregationElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      AggregationElement = null; 
                    else
                      AggregationElement = new List<Code<Hl7.Fhir.Model.Profile.AggregationMode>>(value.Select(elem=>new Code<Hl7.Fhir.Model.Profile.AggregationMode>(elem)));
                    OnPropertyChanged("Aggregation");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProfileMappingComponent")]
        [DataContract]
        public partial class ProfileMappingComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Internal id when this mapping is used
            /// </summary>
            [FhirElement("identity", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id IdentityElement
            {
                get { return _IdentityElement; }
                set { _IdentityElement = value; OnPropertyChanged("IdentityElement"); }
            }
            private Hl7.Fhir.Model.Id _IdentityElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Identity
            {
                get { return IdentityElement != null ? IdentityElement.Value : null; }
                set
                {
                    if(value == null)
                      IdentityElement = null; 
                    else
                      IdentityElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Identity");
                }
            }
            
            /// <summary>
            /// Identifies what this mapping refers to
            /// </summary>
            [FhirElement("uri", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UriElement
            {
                get { return _UriElement; }
                set { _UriElement = value; OnPropertyChanged("UriElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _UriElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri Uri
            {
                get { return UriElement != null ? UriElement.Value : null; }
                set
                {
                    if(value == null)
                      UriElement = null; 
                    else
                      UriElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Uri");
                }
            }
            
            /// <summary>
            /// Names what this mapping refers to
            /// </summary>
            [FhirElement("name", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Versions, Issues, Scope limitations etc
            /// </summary>
            [FhirElement("comments", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentsElement
            {
                get { return _CommentsElement; }
                set { _CommentsElement = value; OnPropertyChanged("CommentsElement"); }
            }
            private Hl7.Fhir.Model.FhirString _CommentsElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Comments
            {
                get { return CommentsElement != null ? CommentsElement.Value : null; }
                set
                {
                    if(value == null)
                      CommentsElement = null; 
                    else
                      CommentsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Comments");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ElementDefinitionMappingComponent")]
        [DataContract]
        public partial class ElementDefinitionMappingComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Reference to mapping declaration
            /// </summary>
            [FhirElement("identity", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id IdentityElement
            {
                get { return _IdentityElement; }
                set { _IdentityElement = value; OnPropertyChanged("IdentityElement"); }
            }
            private Hl7.Fhir.Model.Id _IdentityElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Identity
            {
                get { return IdentityElement != null ? IdentityElement.Value : null; }
                set
                {
                    if(value == null)
                      IdentityElement = null; 
                    else
                      IdentityElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Identity");
                }
            }
            
            /// <summary>
            /// Details of the mapping
            /// </summary>
            [FhirElement("map", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MapElement
            {
                get { return _MapElement; }
                set { _MapElement = value; OnPropertyChanged("MapElement"); }
            }
            private Hl7.Fhir.Model.FhirString _MapElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Map
            {
                get { return MapElement != null ? MapElement.Value : null; }
                set
                {
                    if(value == null)
                      MapElement = null; 
                    else
                      MapElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Map");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ElementDefinitionBindingComponent")]
        [DataContract]
        public partial class ElementDefinitionBindingComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Descriptive Name
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Can additional codes be used?
            /// </summary>
            [FhirElement("isExtensible", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean IsExtensibleElement
            {
                get { return _IsExtensibleElement; }
                set { _IsExtensibleElement = value; OnPropertyChanged("IsExtensibleElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _IsExtensibleElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? IsExtensible
            {
                get { return IsExtensibleElement != null ? IsExtensibleElement.Value : null; }
                set
                {
                    if(value == null)
                      IsExtensibleElement = null; 
                    else
                      IsExtensibleElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("IsExtensible");
                }
            }
            
            /// <summary>
            /// required | preferred | example
            /// </summary>
            [FhirElement("conformance", InSummary=true, Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Profile.BindingConformance> ConformanceElement
            {
                get { return _ConformanceElement; }
                set { _ConformanceElement = value; OnPropertyChanged("ConformanceElement"); }
            }
            private Code<Hl7.Fhir.Model.Profile.BindingConformance> _ConformanceElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Profile.BindingConformance? Conformance
            {
                get { return ConformanceElement != null ? ConformanceElement.Value : null; }
                set
                {
                    if(value == null)
                      ConformanceElement = null; 
                    else
                      ConformanceElement = new Code<Hl7.Fhir.Model.Profile.BindingConformance>(value);
                    OnPropertyChanged("Conformance");
                }
            }
            
            /// <summary>
            /// Human explanation of the value set
            /// </summary>
            [FhirElement("description", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
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
            /// Source of value set
            /// </summary>
            [FhirElement("reference", InSummary=true, Order=80, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            private Hl7.Fhir.Model.Element _Reference;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ElementDefinitionConstraintComponent")]
        [DataContract]
        public partial class ElementDefinitionConstraintComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Target of 'condition' reference above
            /// </summary>
            [FhirElement("key", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id KeyElement
            {
                get { return _KeyElement; }
                set { _KeyElement = value; OnPropertyChanged("KeyElement"); }
            }
            private Hl7.Fhir.Model.Id _KeyElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Key
            {
                get { return KeyElement != null ? KeyElement.Value : null; }
                set
                {
                    if(value == null)
                      KeyElement = null; 
                    else
                      KeyElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Key");
                }
            }
            
            /// <summary>
            /// Short human label
            /// </summary>
            [FhirElement("name", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// error | warning
            /// </summary>
            [FhirElement("severity", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Profile.ConstraintSeverity> SeverityElement
            {
                get { return _SeverityElement; }
                set { _SeverityElement = value; OnPropertyChanged("SeverityElement"); }
            }
            private Code<Hl7.Fhir.Model.Profile.ConstraintSeverity> _SeverityElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Profile.ConstraintSeverity? Severity
            {
                get { return SeverityElement != null ? SeverityElement.Value : null; }
                set
                {
                    if(value == null)
                      SeverityElement = null; 
                    else
                      SeverityElement = new Code<Hl7.Fhir.Model.Profile.ConstraintSeverity>(value);
                    OnPropertyChanged("Severity");
                }
            }
            
            /// <summary>
            /// Human description of constraint
            /// </summary>
            [FhirElement("human", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HumanElement
            {
                get { return _HumanElement; }
                set { _HumanElement = value; OnPropertyChanged("HumanElement"); }
            }
            private Hl7.Fhir.Model.FhirString _HumanElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Human
            {
                get { return HumanElement != null ? HumanElement.Value : null; }
                set
                {
                    if(value == null)
                      HumanElement = null; 
                    else
                      HumanElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Human");
                }
            }
            
            /// <summary>
            /// XPath expression of constraint
            /// </summary>
            [FhirElement("xpath", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString XpathElement
            {
                get { return _XpathElement; }
                set { _XpathElement = value; OnPropertyChanged("XpathElement"); }
            }
            private Hl7.Fhir.Model.FhirString _XpathElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Xpath
            {
                get { return XpathElement != null ? XpathElement.Value : null; }
                set
                {
                    if(value == null)
                      XpathElement = null; 
                    else
                      XpathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Xpath");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ElementComponent")]
        [DataContract]
        public partial class ElementComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// The path of the element (see the formal definitions)
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
            /// How this element is represented in instances
            /// </summary>
            [FhirElement("representation", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.Profile.PropertyRepresentation>> RepresentationElement
            {
                get { return _RepresentationElement; }
                set { _RepresentationElement = value; OnPropertyChanged("RepresentationElement"); }
            }
            private List<Code<Hl7.Fhir.Model.Profile.PropertyRepresentation>> _RepresentationElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.Profile.PropertyRepresentation?> Representation
            {
                get { return RepresentationElement != null ? RepresentationElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      RepresentationElement = null; 
                    else
                      RepresentationElement = new List<Code<Hl7.Fhir.Model.Profile.PropertyRepresentation>>(value.Select(elem=>new Code<Hl7.Fhir.Model.Profile.PropertyRepresentation>(elem)));
                    OnPropertyChanged("Representation");
                }
            }
            
            /// <summary>
            /// Name for this particular element definition (reference target)
            /// </summary>
            [FhirElement("name", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// This element is sliced - slices follow
            /// </summary>
            [FhirElement("slicing", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Profile.ElementSlicingComponent Slicing
            {
                get { return _Slicing; }
                set { _Slicing = value; OnPropertyChanged("Slicing"); }
            }
            private Hl7.Fhir.Model.Profile.ElementSlicingComponent _Slicing;
            
            /// <summary>
            /// More specific definition of the element
            /// </summary>
            [FhirElement("definition", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Profile.ElementDefinitionComponent Definition
            {
                get { return _Definition; }
                set { _Definition = value; OnPropertyChanged("Definition"); }
            }
            private Hl7.Fhir.Model.Profile.ElementDefinitionComponent _Definition;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProfileExtensionDefnComponent")]
        [DataContract]
        public partial class ProfileExtensionDefnComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Identifies the extension in this profile
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            private Hl7.Fhir.Model.Code _CodeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Use this name when displaying the value
            /// </summary>
            [FhirElement("display", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Display
            {
                get { return DisplayElement != null ? DisplayElement.Value : null; }
                set
                {
                    if(value == null)
                      DisplayElement = null; 
                    else
                      DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Display");
                }
            }
            
            /// <summary>
            /// resource | datatype | mapping | extension
            /// </summary>
            [FhirElement("contextType", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Profile.ExtensionContext> ContextTypeElement
            {
                get { return _ContextTypeElement; }
                set { _ContextTypeElement = value; OnPropertyChanged("ContextTypeElement"); }
            }
            private Code<Hl7.Fhir.Model.Profile.ExtensionContext> _ContextTypeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Profile.ExtensionContext? ContextType
            {
                get { return ContextTypeElement != null ? ContextTypeElement.Value : null; }
                set
                {
                    if(value == null)
                      ContextTypeElement = null; 
                    else
                      ContextTypeElement = new Code<Hl7.Fhir.Model.Profile.ExtensionContext>(value);
                    OnPropertyChanged("ContextType");
                }
            }
            
            /// <summary>
            /// Where the extension can be used in instances
            /// </summary>
            [FhirElement("context", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ContextElement
            {
                get { return _ContextElement; }
                set { _ContextElement = value; OnPropertyChanged("ContextElement"); }
            }
            private List<Hl7.Fhir.Model.FhirString> _ContextElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Context
            {
                get { return ContextElement != null ? ContextElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      ContextElement = null; 
                    else
                      ContextElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Context");
                }
            }
            
            /// <summary>
            /// Definition of the extension and its content
            /// </summary>
            [FhirElement("definition", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Profile.ElementDefinitionComponent Definition
            {
                get { return _Definition; }
                set { _Definition = value; OnPropertyChanged("Definition"); }
            }
            private Hl7.Fhir.Model.Profile.ElementDefinitionComponent _Definition;
            
        }
        
        
        /// <summary>
        /// Logical id to reference this profile
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString IdentifierElement
        {
            get { return _IdentifierElement; }
            set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
        }
        private Hl7.Fhir.Model.FhirString _IdentifierElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Identifier
        {
            get { return IdentifierElement != null ? IdentifierElement.Value : null; }
            set
            {
                if(value == null)
                  IdentifierElement = null; 
                else
                  IdentifierElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Identifier");
            }
        }
        
        /// <summary>
        /// Logical id for this version of the profile
        /// </summary>
        [FhirElement("version", InSummary=true, Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
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
        /// Informal name for this profile
        /// </summary>
        [FhirElement("name", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if(value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if(value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact information of the publisher
        /// </summary>
        [FhirElement("telecom", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Telecom
        {
            get { return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }
        private List<Hl7.Fhir.Model.Contact> _Telecom;
        
        /// <summary>
        /// Natural language description of the profile
        /// </summary>
        [FhirElement("description", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
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
        /// Assist with indexing and finding
        /// </summary>
        [FhirElement("code", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        private List<Hl7.Fhir.Model.Coding> _Code;
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", InSummary=true, Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Profile.ResourceProfileStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.Profile.ResourceProfileStatus> _StatusElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Profile.ResourceProfileStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Profile.ResourceProfileStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if(value == null)
                  ExperimentalElement = null; 
                else
                  ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Date for this version of the profile
        /// </summary>
        [FhirElement("date", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if(value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Scope and Usage this profile is for
        /// </summary>
        [FhirElement("requirements", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RequirementsElement
        {
            get { return _RequirementsElement; }
            set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
        }
        private Hl7.Fhir.Model.FhirString _RequirementsElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Requirements
        {
            get { return RequirementsElement != null ? RequirementsElement.Value : null; }
            set
            {
                if(value == null)
                  RequirementsElement = null; 
                else
                  RequirementsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Requirements");
            }
        }
        
        /// <summary>
        /// FHIR Version this profile targets
        /// </summary>
        [FhirElement("fhirVersion", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Id FhirVersionElement
        {
            get { return _FhirVersionElement; }
            set { _FhirVersionElement = value; OnPropertyChanged("FhirVersionElement"); }
        }
        private Hl7.Fhir.Model.Id _FhirVersionElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string FhirVersion
        {
            get { return FhirVersionElement != null ? FhirVersionElement.Value : null; }
            set
            {
                if(value == null)
                  FhirVersionElement = null; 
                else
                  FhirVersionElement = new Hl7.Fhir.Model.Id(value);
                OnPropertyChanged("FhirVersion");
            }
        }
        
        /// <summary>
        /// External specification that the content is mapped to
        /// </summary>
        [FhirElement("mapping", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Profile.ProfileMappingComponent> Mapping
        {
            get { return _Mapping; }
            set { _Mapping = value; OnPropertyChanged("Mapping"); }
        }
        private List<Hl7.Fhir.Model.Profile.ProfileMappingComponent> _Mapping;
        
        /// <summary>
        /// A constraint on a resource or a data type
        /// </summary>
        [FhirElement("structure", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Profile.ProfileStructureComponent> Structure
        {
            get { return _Structure; }
            set { _Structure = value; OnPropertyChanged("Structure"); }
        }
        private List<Hl7.Fhir.Model.Profile.ProfileStructureComponent> _Structure;
        
        /// <summary>
        /// Definition of an extension
        /// </summary>
        [FhirElement("extensionDefn", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Profile.ProfileExtensionDefnComponent> ExtensionDefn
        {
            get { return _ExtensionDefn; }
            set { _ExtensionDefn = value; OnPropertyChanged("ExtensionDefn"); }
        }
        private List<Hl7.Fhir.Model.Profile.ProfileExtensionDefnComponent> _ExtensionDefn;
        
        /// <summary>
        /// Definition of a named query
        /// </summary>
        [FhirElement("query", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Profile.ProfileQueryComponent> Query
        {
            get { return _Query; }
            set { _Query = value; OnPropertyChanged("Query"); }
        }
        private List<Hl7.Fhir.Model.Profile.ProfileQueryComponent> _Query;
        
    }
    
}
