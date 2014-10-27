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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
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
            /// <summary>
            /// Only codes in the specified set are allowed.  If the binding is extensible, other codes may be used for concepts not covered by the bound set of codes.
            /// </summary>
            [EnumLiteral("required")]
            Required,
            /// <summary>
            /// For greater interoperability, implementers are strongly encouraged to use the bound set of codes, however alternate codes may be used in derived profiles and implementations if necessary without being considered non-conformant.
            /// </summary>
            [EnumLiteral("preferred")]
            Preferred,
            /// <summary>
            /// The codes in the set are an example to illustrate the meaning of the field. There is no particular preference for its use nor any assertion that the provided values are sufficient to meet implementation needs.
            /// </summary>
            [EnumLiteral("example")]
            Example,
        }
        
        /// <summary>
        /// SHALL applications comply with this constraint?
        /// </summary>
        [FhirEnumeration("ConstraintSeverity")]
        public enum ConstraintSeverity
        {
            /// <summary>
            /// If the constraint is violated, the resource is not conformant.
            /// </summary>
            [EnumLiteral("error")]
            Error,
            /// <summary>
            /// If the constraint is violated, the resource is conformant, but it is not necessarily following best practice.
            /// </summary>
            [EnumLiteral("warning")]
            Warning,
        }
        
        /// <summary>
        /// The lifecycle status of a Resource Profile
        /// </summary>
        [FhirEnumeration("ResourceProfileStatus")]
        public enum ResourceProfileStatus
        {
            /// <summary>
            /// This profile is still under development.
            /// </summary>
            [EnumLiteral("draft")]
            Draft,
            /// <summary>
            /// This profile is ready for normal use.
            /// </summary>
            [EnumLiteral("active")]
            Active,
            /// <summary>
            /// This profile has been deprecated, withdrawn or superseded and should no longer be used.
            /// </summary>
            [EnumLiteral("retired")]
            Retired,
        }
        
        /// <summary>
        /// How a property is represented on the wire
        /// </summary>
        [FhirEnumeration("PropertyRepresentation")]
        public enum PropertyRepresentation
        {
            /// <summary>
            /// In XML, this property is represented as an attribute not an element.
            /// </summary>
            [EnumLiteral("xmlAttr")]
            XmlAttr,
        }
        
        /// <summary>
        /// How resource references can be aggregated
        /// </summary>
        [FhirEnumeration("AggregationMode")]
        public enum AggregationMode
        {
            /// <summary>
            /// The reference is a local reference to a contained resource.
            /// </summary>
            [EnumLiteral("contained")]
            Contained,
            /// <summary>
            /// The reference to to a resource that has to be resolved externally to the resource that includes the reference.
            /// </summary>
            [EnumLiteral("referenced")]
            Referenced,
            /// <summary>
            /// The resource the reference points to will be found in the same bundle as the resource that includes the reference.
            /// </summary>
            [EnumLiteral("bundled")]
            Bundled,
        }
        
        /// <summary>
        /// How an extension context is interpreted
        /// </summary>
        [FhirEnumeration("ExtensionContext")]
        public enum ExtensionContext
        {
            /// <summary>
            /// The context is all elements matching a particular resource element path.
            /// </summary>
            [EnumLiteral("resource")]
            Resource,
            /// <summary>
            /// The context is all nodes matching a particular data type element path (root or repeating element) or all elements referencing a particular primitive data type (expressed as the datatype name).
            /// </summary>
            [EnumLiteral("datatype")]
            Datatype,
            /// <summary>
            /// The context is all nodes whose mapping to a specified reference model corresponds to a particular mapping structure.  The context identifies the mapping target. The mapping should clearly identify where such an extension could be used.
            /// </summary>
            [EnumLiteral("mapping")]
            Mapping,
            /// <summary>
            /// The context is a particular extension from a particular profile.  Expressed as uri#name, where uri identifies the profile and #name identifies the extension code.
            /// </summary>
            [EnumLiteral("extension")]
            Extension,
        }
        
        /// <summary>
        /// How slices are interpreted when evaluating an instance
        /// </summary>
        [FhirEnumeration("SlicingRules")]
        public enum SlicingRules
        {
            /// <summary>
            /// No additional content is allowed other than that described by the slices in this profile.
            /// </summary>
            [EnumLiteral("closed")]
            Closed,
            /// <summary>
            /// Additional content is allowed anywhere in the list.
            /// </summary>
            [EnumLiteral("open")]
            Open,
            /// <summary>
            /// Additional content is allowed, but only at the end of the list.
            /// </summary>
            [EnumLiteral("openAtEnd")]
            OpenAtEnd,
        }
        
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
            
            /// <summary>
            /// Concise definition for xml presentation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Full formal definition in human language
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Comments about the use of this element
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Why is this needed?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Other names
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Minimum Cardinality
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Maximum Cardinality (a number or *)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// To another element constraint (by element.name)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Length for strings
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Reference to invariant about presence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// If the element must supported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// If this modifies the meaning of other elements
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ElementDefinitionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ShortElement != null) dest.ShortElement = (Hl7.Fhir.Model.FhirString)ShortElement.DeepCopy();
                    if(FormalElement != null) dest.FormalElement = (Hl7.Fhir.Model.FhirString)FormalElement.DeepCopy();
                    if(CommentsElement != null) dest.CommentsElement = (Hl7.Fhir.Model.FhirString)CommentsElement.DeepCopy();
                    if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
                    if(SynonymElement != null) dest.SynonymElement = new List<Hl7.Fhir.Model.FhirString>(SynonymElement.DeepCopy());
                    if(MinElement != null) dest.MinElement = (Hl7.Fhir.Model.Integer)MinElement.DeepCopy();
                    if(MaxElement != null) dest.MaxElement = (Hl7.Fhir.Model.FhirString)MaxElement.DeepCopy();
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.Profile.TypeRefComponent>(Type.DeepCopy());
                    if(NameReferenceElement != null) dest.NameReferenceElement = (Hl7.Fhir.Model.FhirString)NameReferenceElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    if(Example != null) dest.Example = (Hl7.Fhir.Model.Element)Example.DeepCopy();
                    if(MaxLengthElement != null) dest.MaxLengthElement = (Hl7.Fhir.Model.Integer)MaxLengthElement.DeepCopy();
                    if(ConditionElement != null) dest.ConditionElement = new List<Hl7.Fhir.Model.Id>(ConditionElement.DeepCopy());
                    if(Constraint != null) dest.Constraint = new List<Hl7.Fhir.Model.Profile.ElementDefinitionConstraintComponent>(Constraint.DeepCopy());
                    if(MustSupportElement != null) dest.MustSupportElement = (Hl7.Fhir.Model.FhirBoolean)MustSupportElement.DeepCopy();
                    if(IsModifierElement != null) dest.IsModifierElement = (Hl7.Fhir.Model.FhirBoolean)IsModifierElement.DeepCopy();
                    if(Binding != null) dest.Binding = (Hl7.Fhir.Model.Profile.ElementDefinitionBindingComponent)Binding.DeepCopy();
                    if(Mapping != null) dest.Mapping = new List<Hl7.Fhir.Model.Profile.ElementDefinitionMappingComponent>(Mapping.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ElementDefinitionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ElementDefinitionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ShortElement, otherT.ShortElement)) return false;
                if( !DeepComparable.Matches(FormalElement, otherT.FormalElement)) return false;
                if( !DeepComparable.Matches(CommentsElement, otherT.CommentsElement)) return false;
                if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
                if( !DeepComparable.Matches(SynonymElement, otherT.SynonymElement)) return false;
                if( !DeepComparable.Matches(MinElement, otherT.MinElement)) return false;
                if( !DeepComparable.Matches(MaxElement, otherT.MaxElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(NameReferenceElement, otherT.NameReferenceElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(Example, otherT.Example)) return false;
                if( !DeepComparable.Matches(MaxLengthElement, otherT.MaxLengthElement)) return false;
                if( !DeepComparable.Matches(ConditionElement, otherT.ConditionElement)) return false;
                if( !DeepComparable.Matches(Constraint, otherT.Constraint)) return false;
                if( !DeepComparable.Matches(MustSupportElement, otherT.MustSupportElement)) return false;
                if( !DeepComparable.Matches(IsModifierElement, otherT.IsModifierElement)) return false;
                if( !DeepComparable.Matches(Binding, otherT.Binding)) return false;
                if( !DeepComparable.Matches(Mapping, otherT.Mapping)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ElementDefinitionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ShortElement, otherT.ShortElement)) return false;
                if( !DeepComparable.IsExactly(FormalElement, otherT.FormalElement)) return false;
                if( !DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;
                if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
                if( !DeepComparable.IsExactly(SynonymElement, otherT.SynonymElement)) return false;
                if( !DeepComparable.IsExactly(MinElement, otherT.MinElement)) return false;
                if( !DeepComparable.IsExactly(MaxElement, otherT.MaxElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(NameReferenceElement, otherT.NameReferenceElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(Example, otherT.Example)) return false;
                if( !DeepComparable.IsExactly(MaxLengthElement, otherT.MaxLengthElement)) return false;
                if( !DeepComparable.IsExactly(ConditionElement, otherT.ConditionElement)) return false;
                if( !DeepComparable.IsExactly(Constraint, otherT.Constraint)) return false;
                if( !DeepComparable.IsExactly(MustSupportElement, otherT.MustSupportElement)) return false;
                if( !DeepComparable.IsExactly(IsModifierElement, otherT.IsModifierElement)) return false;
                if( !DeepComparable.IsExactly(Binding, otherT.Binding)) return false;
                if( !DeepComparable.IsExactly(Mapping, otherT.Mapping)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Element that used to distinguish the slices
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// If elements must be in same order as slices
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// closed | open | openAtEnd
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ElementSlicingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DiscriminatorElement != null) dest.DiscriminatorElement = (Hl7.Fhir.Model.Id)DiscriminatorElement.DeepCopy();
                    if(OrderedElement != null) dest.OrderedElement = (Hl7.Fhir.Model.FhirBoolean)OrderedElement.DeepCopy();
                    if(RulesElement != null) dest.RulesElement = (Code<Hl7.Fhir.Model.Profile.SlicingRules>)RulesElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ElementSlicingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ElementSlicingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DiscriminatorElement, otherT.DiscriminatorElement)) return false;
                if( !DeepComparable.Matches(OrderedElement, otherT.OrderedElement)) return false;
                if( !DeepComparable.Matches(RulesElement, otherT.RulesElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ElementSlicingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DiscriminatorElement, otherT.DiscriminatorElement)) return false;
                if( !DeepComparable.IsExactly(OrderedElement, otherT.OrderedElement)) return false;
                if( !DeepComparable.IsExactly(RulesElement, otherT.RulesElement)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// The Resource or Data Type being described
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
            
            /// <summary>
            /// Name for this particular structure (reference target)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// This definition is published (i.e. for validation)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Human summary: why describe this resource?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProfileStructureComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(PublishElement != null) dest.PublishElement = (Hl7.Fhir.Model.FhirBoolean)PublishElement.DeepCopy();
                    if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.FhirString)PurposeElement.DeepCopy();
                    if(Element != null) dest.Element = new List<Hl7.Fhir.Model.Profile.ElementComponent>(Element.DeepCopy());
                    if(SearchParam != null) dest.SearchParam = new List<Hl7.Fhir.Model.Profile.ProfileStructureSearchParamComponent>(SearchParam.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProfileStructureComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProfileStructureComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(PublishElement, otherT.PublishElement)) return false;
                if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
                if( !DeepComparable.Matches(Element, otherT.Element)) return false;
                if( !DeepComparable.Matches(SearchParam, otherT.SearchParam)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProfileStructureComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(PublishElement, otherT.PublishElement)) return false;
                if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
                if( !DeepComparable.IsExactly(Element, otherT.Element)) return false;
                if( !DeepComparable.IsExactly(SearchParam, otherT.SearchParam)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Name of search parameter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// number | date | string | token | reference | composite | quantity
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
            
            /// <summary>
            /// Contents and meaning of search parameter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// XPath that extracts the parameter set
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Types of resource (if a resource reference)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProfileStructureSearchParamComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    if(XpathElement != null) dest.XpathElement = (Hl7.Fhir.Model.FhirString)XpathElement.DeepCopy();
                    if(TargetElement != null) dest.TargetElement = new List<Hl7.Fhir.Model.Code>(TargetElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProfileStructureSearchParamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProfileStructureSearchParamComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.Matches(XpathElement, otherT.XpathElement)) return false;
                if( !DeepComparable.Matches(TargetElement, otherT.TargetElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProfileStructureSearchParamComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.IsExactly(XpathElement, otherT.XpathElement)) return false;
                if( !DeepComparable.IsExactly(TargetElement, otherT.TargetElement)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Special named queries (_query=)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Describes the named query
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProfileQueryComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    if(Parameter != null) dest.Parameter = new List<Hl7.Fhir.Model.Profile.ProfileStructureSearchParamComponent>(Parameter.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProfileQueryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProfileQueryComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProfileQueryComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Name of Data type or Resource
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Profile.structure to apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Profile
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
            
            /// <summary>
            /// contained | referenced | bundled - how aggregated
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TypeRefComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(ProfileElement != null) dest.ProfileElement = (Hl7.Fhir.Model.FhirUri)ProfileElement.DeepCopy();
                    if(AggregationElement != null) dest.AggregationElement = new List<Code<Hl7.Fhir.Model.Profile.AggregationMode>>(AggregationElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TypeRefComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TypeRefComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
                if( !DeepComparable.Matches(AggregationElement, otherT.AggregationElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TypeRefComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
                if( !DeepComparable.IsExactly(AggregationElement, otherT.AggregationElement)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Internal id when this mapping is used
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Identifies what this mapping refers to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uri
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
            
            /// <summary>
            /// Names what this mapping refers to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Versions, Issues, Scope limitations etc
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProfileMappingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IdentityElement != null) dest.IdentityElement = (Hl7.Fhir.Model.Id)IdentityElement.DeepCopy();
                    if(UriElement != null) dest.UriElement = (Hl7.Fhir.Model.FhirUri)UriElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(CommentsElement != null) dest.CommentsElement = (Hl7.Fhir.Model.FhirString)CommentsElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProfileMappingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProfileMappingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IdentityElement, otherT.IdentityElement)) return false;
                if( !DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(CommentsElement, otherT.CommentsElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProfileMappingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IdentityElement, otherT.IdentityElement)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Reference to mapping declaration
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Details of the mapping
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ElementDefinitionMappingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IdentityElement != null) dest.IdentityElement = (Hl7.Fhir.Model.Id)IdentityElement.DeepCopy();
                    if(MapElement != null) dest.MapElement = (Hl7.Fhir.Model.FhirString)MapElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ElementDefinitionMappingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ElementDefinitionMappingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IdentityElement, otherT.IdentityElement)) return false;
                if( !DeepComparable.Matches(MapElement, otherT.MapElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ElementDefinitionMappingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IdentityElement, otherT.IdentityElement)) return false;
                if( !DeepComparable.IsExactly(MapElement, otherT.MapElement)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Descriptive Name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Can additional codes be used?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// required | preferred | example
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Human explanation of the value set
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ElementDefinitionBindingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(IsExtensibleElement != null) dest.IsExtensibleElement = (Hl7.Fhir.Model.FhirBoolean)IsExtensibleElement.DeepCopy();
                    if(ConformanceElement != null) dest.ConformanceElement = (Code<Hl7.Fhir.Model.Profile.BindingConformance>)ConformanceElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.Element)Reference.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ElementDefinitionBindingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ElementDefinitionBindingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(IsExtensibleElement, otherT.IsExtensibleElement)) return false;
                if( !DeepComparable.Matches(ConformanceElement, otherT.ConformanceElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ElementDefinitionBindingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(IsExtensibleElement, otherT.IsExtensibleElement)) return false;
                if( !DeepComparable.IsExactly(ConformanceElement, otherT.ConformanceElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Target of 'condition' reference above
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Short human label
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// error | warning
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Human description of constraint
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// XPath expression of constraint
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ElementDefinitionConstraintComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(KeyElement != null) dest.KeyElement = (Hl7.Fhir.Model.Id)KeyElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(SeverityElement != null) dest.SeverityElement = (Code<Hl7.Fhir.Model.Profile.ConstraintSeverity>)SeverityElement.DeepCopy();
                    if(HumanElement != null) dest.HumanElement = (Hl7.Fhir.Model.FhirString)HumanElement.DeepCopy();
                    if(XpathElement != null) dest.XpathElement = (Hl7.Fhir.Model.FhirString)XpathElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ElementDefinitionConstraintComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ElementDefinitionConstraintComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(KeyElement, otherT.KeyElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.Matches(HumanElement, otherT.HumanElement)) return false;
                if( !DeepComparable.Matches(XpathElement, otherT.XpathElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ElementDefinitionConstraintComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(KeyElement, otherT.KeyElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.IsExactly(HumanElement, otherT.HumanElement)) return false;
                if( !DeepComparable.IsExactly(XpathElement, otherT.XpathElement)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// The path of the element (see the formal definitions)
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
            
            /// <summary>
            /// How this element is represented in instances
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Name for this particular element definition (reference target)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ElementComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(RepresentationElement != null) dest.RepresentationElement = new List<Code<Hl7.Fhir.Model.Profile.PropertyRepresentation>>(RepresentationElement.DeepCopy());
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Slicing != null) dest.Slicing = (Hl7.Fhir.Model.Profile.ElementSlicingComponent)Slicing.DeepCopy();
                    if(Definition != null) dest.Definition = (Hl7.Fhir.Model.Profile.ElementDefinitionComponent)Definition.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ElementComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ElementComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(RepresentationElement, otherT.RepresentationElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Slicing, otherT.Slicing)) return false;
                if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ElementComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(RepresentationElement, otherT.RepresentationElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Slicing, otherT.Slicing)) return false;
                if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Identifies the extension in this profile
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Use this name when displaying the value
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// resource | datatype | mapping | extension
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Where the extension can be used in instances
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProfileExtensionDefnComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    if(ContextTypeElement != null) dest.ContextTypeElement = (Code<Hl7.Fhir.Model.Profile.ExtensionContext>)ContextTypeElement.DeepCopy();
                    if(ContextElement != null) dest.ContextElement = new List<Hl7.Fhir.Model.FhirString>(ContextElement.DeepCopy());
                    if(Definition != null) dest.Definition = (Hl7.Fhir.Model.Profile.ElementDefinitionComponent)Definition.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProfileExtensionDefnComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProfileExtensionDefnComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.Matches(ContextTypeElement, otherT.ContextTypeElement)) return false;
                if( !DeepComparable.Matches(ContextElement, otherT.ContextElement)) return false;
                if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProfileExtensionDefnComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.IsExactly(ContextTypeElement, otherT.ContextTypeElement)) return false;
                if( !DeepComparable.IsExactly(ContextElement, otherT.ContextElement)) return false;
                if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
                
                return true;
            }
            
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
        
        /// <summary>
        /// Logical id to reference this profile
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        
        /// <summary>
        /// Logical id for this version of the profile
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
        
        /// <summary>
        /// Informal name for this profile
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        
        /// <summary>
        /// Natural language description of the profile
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
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        
        /// <summary>
        /// Date for this version of the profile
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        
        /// <summary>
        /// Scope and Usage this profile is for
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        
        /// <summary>
        /// FHIR Version this profile targets
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Profile;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirString)IdentifierElement.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.Contact>(Telecom.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Profile.ResourceProfileStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
                if(FhirVersionElement != null) dest.FhirVersionElement = (Hl7.Fhir.Model.Id)FhirVersionElement.DeepCopy();
                if(Mapping != null) dest.Mapping = new List<Hl7.Fhir.Model.Profile.ProfileMappingComponent>(Mapping.DeepCopy());
                if(Structure != null) dest.Structure = new List<Hl7.Fhir.Model.Profile.ProfileStructureComponent>(Structure.DeepCopy());
                if(ExtensionDefn != null) dest.ExtensionDefn = new List<Hl7.Fhir.Model.Profile.ProfileExtensionDefnComponent>(ExtensionDefn.DeepCopy());
                if(Query != null) dest.Query = new List<Hl7.Fhir.Model.Profile.ProfileQueryComponent>(Query.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Profile());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Profile;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.Matches(Mapping, otherT.Mapping)) return false;
            if( !DeepComparable.Matches(Structure, otherT.Structure)) return false;
            if( !DeepComparable.Matches(ExtensionDefn, otherT.ExtensionDefn)) return false;
            if( !DeepComparable.Matches(Query, otherT.Query)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Profile;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.IsExactly(Mapping, otherT.Mapping)) return false;
            if( !DeepComparable.IsExactly(Structure, otherT.Structure)) return false;
            if( !DeepComparable.IsExactly(ExtensionDefn, otherT.ExtensionDefn)) return false;
            if( !DeepComparable.IsExactly(Query, otherT.Query)) return false;
            
            return true;
        }
        
    }
    
}
