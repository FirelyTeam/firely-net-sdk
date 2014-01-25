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
// Generated on Fri, Jan 24, 2014 09:44-0600 for FHIR v0.12
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Resource Profile
    /// </summary>
    [FhirType("Profile", IsResource=true)]
    [DataContract]
    public partial class Profile : Hl7.Fhir.Model.Resource
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
        public partial class ElementDefinitionComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Concise definition for xml presentation
            /// </summary>
            [FhirElement("short", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ShortElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Full formal definition in human language
            /// </summary>
            [FhirElement("formal", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString FormalElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Comments about the use of this element
            /// </summary>
            [FhirElement("comments", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentsElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Why is this needed?
            /// </summary>
            [FhirElement("requirements", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RequirementsElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Other names
            /// </summary>
            [FhirElement("synonym", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> SynonymElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Minimum Cardinality
            /// </summary>
            [FhirElement("min", Order=90)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer MinElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Maximum Cardinality (a number or *)
            /// </summary>
            [FhirElement("max", Order=100)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MaxElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Data type and Profile for this element
            /// </summary>
            [FhirElement("type", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Profile.TypeRefComponent> Type { get; set; }
            
            /// <summary>
            /// To another element constraint (by element.name)
            /// </summary>
            [FhirElement("nameReference", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameReferenceElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Fixed value: [as defined for a primitive type]
            /// </summary>
            [FhirElement("value", Order=130, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Element))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value { get; set; }
            
            /// <summary>
            /// Example value: [as defined for type]
            /// </summary>
            [FhirElement("example", Order=140, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Element))]
            [DataMember]
            public Hl7.Fhir.Model.Element Example { get; set; }
            
            /// <summary>
            /// Length for strings
            /// </summary>
            [FhirElement("maxLength", Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.Integer MaxLengthElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Reference to invariant about presence
            /// </summary>
            [FhirElement("condition", Order=160)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Id> ConditionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Condition that must evaluate to true
            /// </summary>
            [FhirElement("constraint", Order=170)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Profile.ElementDefinitionConstraintComponent> Constraint { get; set; }
            
            /// <summary>
            /// If the element must supported
            /// </summary>
            [FhirElement("mustSupport", Order=180)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean MustSupportElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// If this modifies the meaning of other elements
            /// </summary>
            [FhirElement("isModifier", Order=190)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean IsModifierElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// ValueSet details if this is coded
            /// </summary>
            [FhirElement("binding", Order=200)]
            [DataMember]
            public Hl7.Fhir.Model.Profile.ElementDefinitionBindingComponent Binding { get; set; }
            
            /// <summary>
            /// Map element to another set of definitions
            /// </summary>
            [FhirElement("mapping", Order=210)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Profile.ElementDefinitionMappingComponent> Mapping { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ElementSlicingComponent")]
        [DataContract]
        public partial class ElementSlicingComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Element that used to distinguish the slices
            /// </summary>
            [FhirElement("discriminator", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id DiscriminatorElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// If elements must be in same order as slices
            /// </summary>
            [FhirElement("ordered", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean OrderedElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// closed | open | openAtEnd
            /// </summary>
            [FhirElement("rules", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Profile.SlicingRules> RulesElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProfileStructureComponent")]
        [DataContract]
        public partial class ProfileStructureComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// The Resource or Data Type being described
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code TypeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Name for this particular structure (reference target)
            /// </summary>
            [FhirElement("name", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// This definition is published (i.e. for validation)
            /// </summary>
            [FhirElement("publish", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean PublishElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Human summary: why describe this resource?
            /// </summary>
            [FhirElement("purpose", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PurposeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Definition of elements in the resource (if no profile)
            /// </summary>
            [FhirElement("element", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Profile.ElementComponent> Element { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("TypeRefComponent")]
        [DataContract]
        public partial class TypeRefComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Name of Data type or Resource
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Profile.structure to apply
            /// </summary>
            [FhirElement("profile", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri ProfileElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// contained | referenced | bundled - how aggregated
            /// </summary>
            [FhirElement("aggregation", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.Profile.AggregationMode>> AggregationElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProfileMappingComponent")]
        [DataContract]
        public partial class ProfileMappingComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Internal id when this mapping is used
            /// </summary>
            [FhirElement("identity", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id IdentityElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Identifies what this mapping refers to
            /// </summary>
            [FhirElement("uri", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UriElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Names what this mapping refers to
            /// </summary>
            [FhirElement("name", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Versions, Issues, Scope limitations etc
            /// </summary>
            [FhirElement("comments", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentsElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ElementDefinitionMappingComponent")]
        [DataContract]
        public partial class ElementDefinitionMappingComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Reference to mapping declaration
            /// </summary>
            [FhirElement("identity", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id IdentityElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Details of the mapping
            /// </summary>
            [FhirElement("map", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MapElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ElementDefinitionBindingComponent")]
        [DataContract]
        public partial class ElementDefinitionBindingComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Descriptive Name
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Can additional codes be used?
            /// </summary>
            [FhirElement("isExtensible", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean IsExtensibleElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// required | preferred | example
            /// </summary>
            [FhirElement("conformance", Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Profile.BindingConformance> ConformanceElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Human explanation of the value set
            /// </summary>
            [FhirElement("description", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Source of value set
            /// </summary>
            [FhirElement("reference", Order=80, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Reference { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ElementDefinitionConstraintComponent")]
        [DataContract]
        public partial class ElementDefinitionConstraintComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Target of 'condition' reference above
            /// </summary>
            [FhirElement("key", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id KeyElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Short human label
            /// </summary>
            [FhirElement("name", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// error | warning
            /// </summary>
            [FhirElement("severity", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Profile.ConstraintSeverity> SeverityElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Human description of constraint
            /// </summary>
            [FhirElement("human", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HumanElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// XPath expression of constraint
            /// </summary>
            [FhirElement("xpath", Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString XpathElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ElementComponent")]
        [DataContract]
        public partial class ElementComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// The path of the element (see the formal definitions)
            /// </summary>
            [FhirElement("path", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// How this element is represented in instances
            /// </summary>
            [FhirElement("representation", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.Profile.PropertyRepresentation>> RepresentationElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Name for this particular element definition (reference target)
            /// </summary>
            [FhirElement("name", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// This element is sliced - slices follow
            /// </summary>
            [FhirElement("slicing", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Profile.ElementSlicingComponent Slicing { get; set; }
            
            /// <summary>
            /// More specific definition of the element
            /// </summary>
            [FhirElement("definition", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Profile.ElementDefinitionComponent Definition { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProfileExtensionDefnComponent")]
        [DataContract]
        public partial class ProfileExtensionDefnComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Identifies the extension in this profile
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Use this name when displaying the value
            /// </summary>
            [FhirElement("display", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// resource | datatype | mapping | extension
            /// </summary>
            [FhirElement("contextType", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Profile.ExtensionContext> ContextTypeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Where the extension can be used in instances
            /// </summary>
            [FhirElement("context", Order=70)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ContextElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Definition of the extension and its content
            /// </summary>
            [FhirElement("definition", Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Profile.ElementDefinitionComponent Definition { get; set; }
            
        }
        
        
        /// <summary>
        /// Logical id to reference this profile
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString IdentifierElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Logical id for this version of the profile
        /// </summary>
        [FhirElement("version", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Informal name for this profile
        /// </summary>
        [FhirElement("name", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        [FhirElement("publisher", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Contact information of the publisher
        /// </summary>
        [FhirElement("telecom", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }
        
        /// <summary>
        /// Natural language description of the profile
        /// </summary>
        [FhirElement("description", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Assist with indexing and finding
        /// </summary>
        [FhirElement("code", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Code { get; set; }
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Profile.ResourceProfileStatus> StatusElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Date for this version of the profile
        /// </summary>
        [FhirElement("date", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Scope and Usage this profile is for
        /// </summary>
        [FhirElement("requirements", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RequirementsElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// FHIR Version this profile targets
        /// </summary>
        [FhirElement("fhirVersion", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Id FhirVersionElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// External specification that the content is mapped to
        /// </summary>
        [FhirElement("mapping", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Profile.ProfileMappingComponent> Mapping { get; set; }
        
        /// <summary>
        /// A constraint on a resource or a data type
        /// </summary>
        [FhirElement("structure", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Profile.ProfileStructureComponent> Structure { get; set; }
        
        /// <summary>
        /// Definition of an extension
        /// </summary>
        [FhirElement("extensionDefn", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Profile.ProfileExtensionDefnComponent> ExtensionDefn { get; set; }
        
    }
    
}
