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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// A Map of relationships between 2 structures that can be used to transform data
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "StructureMap", IsResource=true)]
    [DataContract]
    public partial class StructureMap : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IStructureMap, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.StructureMap; } }
        [NotMapped]
        public override string TypeName { get { return "StructureMap"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "StructureComponent")]
        [DataContract]
        public partial class StructureComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IStructureMapStructureComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "StructureComponent"; } }
            
            /// <summary>
            /// Canonical reference to structure definition
            /// </summary>
            [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _UrlElement;
            
            /// <summary>
            /// Canonical reference to structure definition
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Url
            {
                get { return UrlElement != null ? UrlElement.Value : null; }
                set
                {
                    if (value == null)
                        UrlElement = null;
                    else
                        UrlElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("Url");
                }
            }
            
            /// <summary>
            /// source | queried | target | produced
            /// </summary>
            [FhirElement("mode", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureMapModelMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureMapModelMode> _ModeElement;
            
            /// <summary>
            /// source | queried | target | produced
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureMapModelMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (value == null)
                        ModeElement = null;
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.StructureMapModelMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Name for type in this map
            /// </summary>
            [FhirElement("alias", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString AliasElement
            {
                get { return _AliasElement; }
                set { _AliasElement = value; OnPropertyChanged("AliasElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _AliasElement;
            
            /// <summary>
            /// Name for type in this map
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Alias
            {
                get { return AliasElement != null ? AliasElement.Value : null; }
                set
                {
                    if (value == null)
                        AliasElement = null;
                    else
                        AliasElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Alias");
                }
            }
            
            /// <summary>
            /// Documentation on use of structure
            /// </summary>
            [FhirElement("documentation", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            /// <summary>
            /// Documentation on use of structure
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if (value == null)
                        DocumentationElement = null;
                    else
                        DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("StructureComponent");
                base.Serialize(sink);
                sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); UrlElement?.Serialize(sink);
                sink.Element("mode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ModeElement?.Serialize(sink);
                sink.Element("alias", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AliasElement?.Serialize(sink);
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DocumentationElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StructureComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.Canonical)UrlElement.DeepCopy();
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.StructureMapModelMode>)ModeElement.DeepCopy();
                    if(AliasElement != null) dest.AliasElement = (Hl7.Fhir.Model.FhirString)AliasElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new StructureComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StructureComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(AliasElement, otherT.AliasElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StructureComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(AliasElement, otherT.AliasElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (UrlElement != null) yield return UrlElement;
                    if (ModeElement != null) yield return ModeElement;
                    if (AliasElement != null) yield return AliasElement;
                    if (DocumentationElement != null) yield return DocumentationElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                    if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                    if (AliasElement != null) yield return new ElementValue("alias", AliasElement);
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "GroupComponent")]
        [DataContract]
        public partial class GroupComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IStructureMapGroupComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "GroupComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IStructureMapInputComponent> Hl7.Fhir.Model.IStructureMapGroupComponent.Input { get { return Input; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IStructureMapRuleComponent> Hl7.Fhir.Model.IStructureMapGroupComponent.Rule { get { return Rule; } }
            
            /// <summary>
            /// Human-readable label
            /// </summary>
            [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.Id _NameElement;
            
            /// <summary>
            /// Human-readable label
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null;
                    else
                        NameElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Another group that this group adds rules to
            /// </summary>
            [FhirElement("extends", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Id ExtendsElement
            {
                get { return _ExtendsElement; }
                set { _ExtendsElement = value; OnPropertyChanged("ExtendsElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ExtendsElement;
            
            /// <summary>
            /// Another group that this group adds rules to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Extends
            {
                get { return ExtendsElement != null ? ExtendsElement.Value : null; }
                set
                {
                    if (value == null)
                        ExtendsElement = null;
                    else
                        ExtendsElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Extends");
                }
            }
            
            /// <summary>
            /// none | types | type-and-types
            /// </summary>
            [FhirElement("typeMode", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureMapGroupTypeMode> TypeModeElement
            {
                get { return _TypeModeElement; }
                set { _TypeModeElement = value; OnPropertyChanged("TypeModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureMapGroupTypeMode> _TypeModeElement;
            
            /// <summary>
            /// none | types | type-and-types
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureMapGroupTypeMode? TypeMode
            {
                get { return TypeModeElement != null ? TypeModeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeModeElement = null;
                    else
                        TypeModeElement = new Code<Hl7.Fhir.Model.StructureMapGroupTypeMode>(value);
                    OnPropertyChanged("TypeMode");
                }
            }
            
            /// <summary>
            /// Additional description/explanation for group
            /// </summary>
            [FhirElement("documentation", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            /// <summary>
            /// Additional description/explanation for group
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if (value == null)
                        DocumentationElement = null;
                    else
                        DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
            /// <summary>
            /// Named instance provided when invoking the map
            /// </summary>
            [FhirElement("input", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<InputComponent> Input
            {
                get { if(_Input==null) _Input = new List<InputComponent>(); return _Input; }
                set { _Input = value; OnPropertyChanged("Input"); }
            }
            
            private List<InputComponent> _Input;
            
            /// <summary>
            /// Transform Rule from source to target
            /// </summary>
            [FhirElement("rule", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<RuleComponent> Rule
            {
                get { if(_Rule==null) _Rule = new List<RuleComponent>(); return _Rule; }
                set { _Rule = value; OnPropertyChanged("Rule"); }
            }
            
            private List<RuleComponent> _Rule;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("GroupComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); NameElement?.Serialize(sink);
                sink.Element("extends", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExtendsElement?.Serialize(sink);
                sink.Element("typeMode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeModeElement?.Serialize(sink);
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DocumentationElement?.Serialize(sink);
                sink.BeginList("input", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
                foreach(var item in Input)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("rule", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
                foreach(var item in Rule)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.Id)NameElement.DeepCopy();
                    if(ExtendsElement != null) dest.ExtendsElement = (Hl7.Fhir.Model.Id)ExtendsElement.DeepCopy();
                    if(TypeModeElement != null) dest.TypeModeElement = (Code<Hl7.Fhir.Model.StructureMapGroupTypeMode>)TypeModeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    if(Input != null) dest.Input = new List<InputComponent>(Input.DeepCopy());
                    if(Rule != null) dest.Rule = new List<RuleComponent>(Rule.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new GroupComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(ExtendsElement, otherT.ExtendsElement)) return false;
                if( !DeepComparable.Matches(TypeModeElement, otherT.TypeModeElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.Matches(Input, otherT.Input)) return false;
                if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(ExtendsElement, otherT.ExtendsElement)) return false;
                if( !DeepComparable.IsExactly(TypeModeElement, otherT.TypeModeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.IsExactly(Input, otherT.Input)) return false;
                if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (ExtendsElement != null) yield return ExtendsElement;
                    if (TypeModeElement != null) yield return TypeModeElement;
                    if (DocumentationElement != null) yield return DocumentationElement;
                    foreach (var elem in Input) { if (elem != null) yield return elem; }
                    foreach (var elem in Rule) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (ExtendsElement != null) yield return new ElementValue("extends", ExtendsElement);
                    if (TypeModeElement != null) yield return new ElementValue("typeMode", TypeModeElement);
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                    foreach (var elem in Input) { if (elem != null) yield return new ElementValue("input", elem); }
                    foreach (var elem in Rule) { if (elem != null) yield return new ElementValue("rule", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "InputComponent")]
        [DataContract]
        public partial class InputComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IStructureMapInputComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "InputComponent"; } }
            
            /// <summary>
            /// Name for this instance of data
            /// </summary>
            [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.Id _NameElement;
            
            /// <summary>
            /// Name for this instance of data
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null;
                    else
                        NameElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Type for this instance of data
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TypeElement;
            
            /// <summary>
            /// Type for this instance of data
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// source | target
            /// </summary>
            [FhirElement("mode", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureMapInputMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureMapInputMode> _ModeElement;
            
            /// <summary>
            /// source | target
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureMapInputMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (value == null)
                        ModeElement = null;
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.StructureMapInputMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Documentation for this instance of data
            /// </summary>
            [FhirElement("documentation", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            /// <summary>
            /// Documentation for this instance of data
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if (value == null)
                        DocumentationElement = null;
                    else
                        DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("InputComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); NameElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TypeElement?.Serialize(sink);
                sink.Element("mode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ModeElement?.Serialize(sink);
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DocumentationElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InputComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.Id)NameElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.FhirString)TypeElement.DeepCopy();
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.StructureMapInputMode>)ModeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new InputComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InputComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InputComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (TypeElement != null) yield return TypeElement;
                    if (ModeElement != null) yield return ModeElement;
                    if (DocumentationElement != null) yield return DocumentationElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "RuleComponent")]
        [DataContract]
        public partial class RuleComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IStructureMapRuleComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RuleComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IStructureMapSourceComponent> Hl7.Fhir.Model.IStructureMapRuleComponent.Source { get { return Source; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IStructureMapTargetComponent> Hl7.Fhir.Model.IStructureMapRuleComponent.Target { get { return Target; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IStructureMapRuleComponent> Hl7.Fhir.Model.IStructureMapRuleComponent.Rule { get { return Rule; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IStructureMapDependentComponent> Hl7.Fhir.Model.IStructureMapRuleComponent.Dependent { get { return Dependent; } }
            
            /// <summary>
            /// Name of the rule for internal references
            /// </summary>
            [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.Id _NameElement;
            
            /// <summary>
            /// Name of the rule for internal references
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null;
                    else
                        NameElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Source inputs to the mapping
            /// </summary>
            [FhirElement("source", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<SourceComponent> Source
            {
                get { if(_Source==null) _Source = new List<SourceComponent>(); return _Source; }
                set { _Source = value; OnPropertyChanged("Source"); }
            }
            
            private List<SourceComponent> _Source;
            
            /// <summary>
            /// Content to create because of this mapping rule
            /// </summary>
            [FhirElement("target", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<TargetComponent> Target
            {
                get { if(_Target==null) _Target = new List<TargetComponent>(); return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            
            private List<TargetComponent> _Target;
            
            /// <summary>
            /// Rules contained in this rule
            /// </summary>
            [FhirElement("rule", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<RuleComponent> Rule
            {
                get { if(_Rule==null) _Rule = new List<RuleComponent>(); return _Rule; }
                set { _Rule = value; OnPropertyChanged("Rule"); }
            }
            
            private List<RuleComponent> _Rule;
            
            /// <summary>
            /// Which other rules to apply in the context of this rule
            /// </summary>
            [FhirElement("dependent", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<DependentComponent> Dependent
            {
                get { if(_Dependent==null) _Dependent = new List<DependentComponent>(); return _Dependent; }
                set { _Dependent = value; OnPropertyChanged("Dependent"); }
            }
            
            private List<DependentComponent> _Dependent;
            
            /// <summary>
            /// Documentation for this instance of data
            /// </summary>
            [FhirElement("documentation", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            /// <summary>
            /// Documentation for this instance of data
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if (value == null)
                        DocumentationElement = null;
                    else
                        DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RuleComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); NameElement?.Serialize(sink);
                sink.BeginList("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
                foreach(var item in Source)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Target)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("rule", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Rule)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("dependent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Dependent)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DocumentationElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RuleComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.Id)NameElement.DeepCopy();
                    if(Source != null) dest.Source = new List<SourceComponent>(Source.DeepCopy());
                    if(Target != null) dest.Target = new List<TargetComponent>(Target.DeepCopy());
                    if(Rule != null) dest.Rule = new List<RuleComponent>(Rule.DeepCopy());
                    if(Dependent != null) dest.Dependent = new List<DependentComponent>(Dependent.DeepCopy());
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RuleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RuleComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Source, otherT.Source)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
                if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
                if( !DeepComparable.Matches(Dependent, otherT.Dependent)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RuleComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
                if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
                if( !DeepComparable.IsExactly(Dependent, otherT.Dependent)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    foreach (var elem in Source) { if (elem != null) yield return elem; }
                    foreach (var elem in Target) { if (elem != null) yield return elem; }
                    foreach (var elem in Rule) { if (elem != null) yield return elem; }
                    foreach (var elem in Dependent) { if (elem != null) yield return elem; }
                    if (DocumentationElement != null) yield return DocumentationElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    foreach (var elem in Source) { if (elem != null) yield return new ElementValue("source", elem); }
                    foreach (var elem in Target) { if (elem != null) yield return new ElementValue("target", elem); }
                    foreach (var elem in Rule) { if (elem != null) yield return new ElementValue("rule", elem); }
                    foreach (var elem in Dependent) { if (elem != null) yield return new ElementValue("dependent", elem); }
                    if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SourceComponent")]
        [DataContract]
        public partial class SourceComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IStructureMapSourceComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SourceComponent"; } }
            
            /// <summary>
            /// Type or variable this rule applies to
            /// </summary>
            [FhirElement("context", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id ContextElement
            {
                get { return _ContextElement; }
                set { _ContextElement = value; OnPropertyChanged("ContextElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ContextElement;
            
            /// <summary>
            /// Type or variable this rule applies to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Context
            {
                get { return ContextElement != null ? ContextElement.Value : null; }
                set
                {
                    if (value == null)
                        ContextElement = null;
                    else
                        ContextElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Context");
                }
            }
            
            /// <summary>
            /// Specified minimum cardinality
            /// </summary>
            [FhirElement("min", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Integer MinElement
            {
                get { return _MinElement; }
                set { _MinElement = value; OnPropertyChanged("MinElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _MinElement;
            
            /// <summary>
            /// Specified minimum cardinality
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Min
            {
                get { return MinElement != null ? MinElement.Value : null; }
                set
                {
                    if (value == null)
                        MinElement = null;
                    else
                        MinElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Min");
                }
            }
            
            /// <summary>
            /// Specified maximum cardinality (number or *)
            /// </summary>
            [FhirElement("max", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MaxElement
            {
                get { return _MaxElement; }
                set { _MaxElement = value; OnPropertyChanged("MaxElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MaxElement;
            
            /// <summary>
            /// Specified maximum cardinality (number or *)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Max
            {
                get { return MaxElement != null ? MaxElement.Value : null; }
                set
                {
                    if (value == null)
                        MaxElement = null;
                    else
                        MaxElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Max");
                }
            }
            
            /// <summary>
            /// Rule only applies if source has this type
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TypeElement;
            
            /// <summary>
            /// Rule only applies if source has this type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Default value if no value exists
            /// </summary>
            [FhirElement("defaultValue", InSummary=Hl7.Fhir.Model.Version.All, Order=80, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Canonical),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Url),typeof(Hl7.Fhir.Model.Uuid),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.R4.Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.R4.ContactPoint),typeof(Hl7.Fhir.Model.R4.Count),typeof(Hl7.Fhir.Model.R4.Distance),typeof(Hl7.Fhir.Model.R4.Duration),typeof(Hl7.Fhir.Model.R4.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.R4.Money),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.R4.SampledData),typeof(Hl7.Fhir.Model.R4.Signature),typeof(Hl7.Fhir.Model.R4.Timing),typeof(Hl7.Fhir.Model.R4.ContactDetail),typeof(Hl7.Fhir.Model.R4.Contributor),typeof(Hl7.Fhir.Model.R4.DataRequirement),typeof(Hl7.Fhir.Model.Expression),typeof(Hl7.Fhir.Model.R4.ParameterDefinition),typeof(Hl7.Fhir.Model.R4.RelatedArtifact),typeof(Hl7.Fhir.Model.R4.TriggerDefinition),typeof(Hl7.Fhir.Model.UsageContext),typeof(Hl7.Fhir.Model.R4.Dosage),typeof(Hl7.Fhir.Model.Meta))]
            [DataMember]
            public Hl7.Fhir.Model.Element DefaultValue
            {
                get { return _DefaultValue; }
                set { _DefaultValue = value; OnPropertyChanged("DefaultValue"); }
            }
            
            private Hl7.Fhir.Model.Element _DefaultValue;
            
            /// <summary>
            /// Optional field for this source
            /// </summary>
            [FhirElement("element", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ElementElement
            {
                get { return _ElementElement; }
                set { _ElementElement = value; OnPropertyChanged("ElementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ElementElement;
            
            /// <summary>
            /// Optional field for this source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Element
            {
                get { return ElementElement != null ? ElementElement.Value : null; }
                set
                {
                    if (value == null)
                        ElementElement = null;
                    else
                        ElementElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Element");
                }
            }
            
            /// <summary>
            /// first | not_first | last | not_last | only_one
            /// </summary>
            [FhirElement("listMode", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureMapSourceListMode> ListModeElement
            {
                get { return _ListModeElement; }
                set { _ListModeElement = value; OnPropertyChanged("ListModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureMapSourceListMode> _ListModeElement;
            
            /// <summary>
            /// first | not_first | last | not_last | only_one
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureMapSourceListMode? ListMode
            {
                get { return ListModeElement != null ? ListModeElement.Value : null; }
                set
                {
                    if (value == null)
                        ListModeElement = null;
                    else
                        ListModeElement = new Code<Hl7.Fhir.Model.StructureMapSourceListMode>(value);
                    OnPropertyChanged("ListMode");
                }
            }
            
            /// <summary>
            /// Named context for field, if a field is specified
            /// </summary>
            [FhirElement("variable", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Id VariableElement
            {
                get { return _VariableElement; }
                set { _VariableElement = value; OnPropertyChanged("VariableElement"); }
            }
            
            private Hl7.Fhir.Model.Id _VariableElement;
            
            /// <summary>
            /// Named context for field, if a field is specified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Variable
            {
                get { return VariableElement != null ? VariableElement.Value : null; }
                set
                {
                    if (value == null)
                        VariableElement = null;
                    else
                        VariableElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Variable");
                }
            }
            
            /// <summary>
            /// FHIRPath expression  - must be true or the rule does not apply
            /// </summary>
            [FhirElement("condition", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ConditionElement
            {
                get { return _ConditionElement; }
                set { _ConditionElement = value; OnPropertyChanged("ConditionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ConditionElement;
            
            /// <summary>
            /// FHIRPath expression  - must be true or the rule does not apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Condition
            {
                get { return ConditionElement != null ? ConditionElement.Value : null; }
                set
                {
                    if (value == null)
                        ConditionElement = null;
                    else
                        ConditionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Condition");
                }
            }
            
            /// <summary>
            /// FHIRPath expression  - must be true or the mapping engine throws an error instead of completing
            /// </summary>
            [FhirElement("check", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CheckElement
            {
                get { return _CheckElement; }
                set { _CheckElement = value; OnPropertyChanged("CheckElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CheckElement;
            
            /// <summary>
            /// FHIRPath expression  - must be true or the mapping engine throws an error instead of completing
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Check
            {
                get { return CheckElement != null ? CheckElement.Value : null; }
                set
                {
                    if (value == null)
                        CheckElement = null;
                    else
                        CheckElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Check");
                }
            }
            
            /// <summary>
            /// Message to put in log if source exists (FHIRPath)
            /// </summary>
            [FhirElement("logMessage", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LogMessageElement
            {
                get { return _LogMessageElement; }
                set { _LogMessageElement = value; OnPropertyChanged("LogMessageElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LogMessageElement;
            
            /// <summary>
            /// Message to put in log if source exists (FHIRPath)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string LogMessage
            {
                get { return LogMessageElement != null ? LogMessageElement.Value : null; }
                set
                {
                    if (value == null)
                        LogMessageElement = null;
                    else
                        LogMessageElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("LogMessage");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SourceComponent");
                base.Serialize(sink);
                sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ContextElement?.Serialize(sink);
                sink.Element("min", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MinElement?.Serialize(sink);
                sink.Element("max", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MaxElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TypeElement?.Serialize(sink);
                sink.Element("defaultValue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); DefaultValue?.Serialize(sink);
                sink.Element("element", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ElementElement?.Serialize(sink);
                sink.Element("listMode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ListModeElement?.Serialize(sink);
                sink.Element("variable", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VariableElement?.Serialize(sink);
                sink.Element("condition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ConditionElement?.Serialize(sink);
                sink.Element("check", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CheckElement?.Serialize(sink);
                sink.Element("logMessage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LogMessageElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SourceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ContextElement != null) dest.ContextElement = (Hl7.Fhir.Model.Id)ContextElement.DeepCopy();
                    if(MinElement != null) dest.MinElement = (Hl7.Fhir.Model.Integer)MinElement.DeepCopy();
                    if(MaxElement != null) dest.MaxElement = (Hl7.Fhir.Model.FhirString)MaxElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.FhirString)TypeElement.DeepCopy();
                    if(DefaultValue != null) dest.DefaultValue = (Hl7.Fhir.Model.Element)DefaultValue.DeepCopy();
                    if(ElementElement != null) dest.ElementElement = (Hl7.Fhir.Model.FhirString)ElementElement.DeepCopy();
                    if(ListModeElement != null) dest.ListModeElement = (Code<Hl7.Fhir.Model.StructureMapSourceListMode>)ListModeElement.DeepCopy();
                    if(VariableElement != null) dest.VariableElement = (Hl7.Fhir.Model.Id)VariableElement.DeepCopy();
                    if(ConditionElement != null) dest.ConditionElement = (Hl7.Fhir.Model.FhirString)ConditionElement.DeepCopy();
                    if(CheckElement != null) dest.CheckElement = (Hl7.Fhir.Model.FhirString)CheckElement.DeepCopy();
                    if(LogMessageElement != null) dest.LogMessageElement = (Hl7.Fhir.Model.FhirString)LogMessageElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SourceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ContextElement, otherT.ContextElement)) return false;
                if( !DeepComparable.Matches(MinElement, otherT.MinElement)) return false;
                if( !DeepComparable.Matches(MaxElement, otherT.MaxElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(DefaultValue, otherT.DefaultValue)) return false;
                if( !DeepComparable.Matches(ElementElement, otherT.ElementElement)) return false;
                if( !DeepComparable.Matches(ListModeElement, otherT.ListModeElement)) return false;
                if( !DeepComparable.Matches(VariableElement, otherT.VariableElement)) return false;
                if( !DeepComparable.Matches(ConditionElement, otherT.ConditionElement)) return false;
                if( !DeepComparable.Matches(CheckElement, otherT.CheckElement)) return false;
                if( !DeepComparable.Matches(LogMessageElement, otherT.LogMessageElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SourceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ContextElement, otherT.ContextElement)) return false;
                if( !DeepComparable.IsExactly(MinElement, otherT.MinElement)) return false;
                if( !DeepComparable.IsExactly(MaxElement, otherT.MaxElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(DefaultValue, otherT.DefaultValue)) return false;
                if( !DeepComparable.IsExactly(ElementElement, otherT.ElementElement)) return false;
                if( !DeepComparable.IsExactly(ListModeElement, otherT.ListModeElement)) return false;
                if( !DeepComparable.IsExactly(VariableElement, otherT.VariableElement)) return false;
                if( !DeepComparable.IsExactly(ConditionElement, otherT.ConditionElement)) return false;
                if( !DeepComparable.IsExactly(CheckElement, otherT.CheckElement)) return false;
                if( !DeepComparable.IsExactly(LogMessageElement, otherT.LogMessageElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ContextElement != null) yield return ContextElement;
                    if (MinElement != null) yield return MinElement;
                    if (MaxElement != null) yield return MaxElement;
                    if (TypeElement != null) yield return TypeElement;
                    if (DefaultValue != null) yield return DefaultValue;
                    if (ElementElement != null) yield return ElementElement;
                    if (ListModeElement != null) yield return ListModeElement;
                    if (VariableElement != null) yield return VariableElement;
                    if (ConditionElement != null) yield return ConditionElement;
                    if (CheckElement != null) yield return CheckElement;
                    if (LogMessageElement != null) yield return LogMessageElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ContextElement != null) yield return new ElementValue("context", ContextElement);
                    if (MinElement != null) yield return new ElementValue("min", MinElement);
                    if (MaxElement != null) yield return new ElementValue("max", MaxElement);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (DefaultValue != null) yield return new ElementValue("defaultValue", DefaultValue);
                    if (ElementElement != null) yield return new ElementValue("element", ElementElement);
                    if (ListModeElement != null) yield return new ElementValue("listMode", ListModeElement);
                    if (VariableElement != null) yield return new ElementValue("variable", VariableElement);
                    if (ConditionElement != null) yield return new ElementValue("condition", ConditionElement);
                    if (CheckElement != null) yield return new ElementValue("check", CheckElement);
                    if (LogMessageElement != null) yield return new ElementValue("logMessage", LogMessageElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "TargetComponent")]
        [DataContract]
        public partial class TargetComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IStructureMapTargetComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TargetComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IStructureMapParameterComponent> Hl7.Fhir.Model.IStructureMapTargetComponent.Parameter { get { return Parameter; } }
            
            /// <summary>
            /// Type or variable this rule applies to
            /// </summary>
            [FhirElement("context", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Id ContextElement
            {
                get { return _ContextElement; }
                set { _ContextElement = value; OnPropertyChanged("ContextElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ContextElement;
            
            /// <summary>
            /// Type or variable this rule applies to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Context
            {
                get { return ContextElement != null ? ContextElement.Value : null; }
                set
                {
                    if (value == null)
                        ContextElement = null;
                    else
                        ContextElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Context");
                }
            }
            
            /// <summary>
            /// type | variable
            /// </summary>
            [FhirElement("contextType", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureMapContextType> ContextTypeElement
            {
                get { return _ContextTypeElement; }
                set { _ContextTypeElement = value; OnPropertyChanged("ContextTypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureMapContextType> _ContextTypeElement;
            
            /// <summary>
            /// type | variable
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureMapContextType? ContextType
            {
                get { return ContextTypeElement != null ? ContextTypeElement.Value : null; }
                set
                {
                    if (value == null)
                        ContextTypeElement = null;
                    else
                        ContextTypeElement = new Code<Hl7.Fhir.Model.StructureMapContextType>(value);
                    OnPropertyChanged("ContextType");
                }
            }
            
            /// <summary>
            /// Field to create in the context
            /// </summary>
            [FhirElement("element", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ElementElement
            {
                get { return _ElementElement; }
                set { _ElementElement = value; OnPropertyChanged("ElementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ElementElement;
            
            /// <summary>
            /// Field to create in the context
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Element
            {
                get { return ElementElement != null ? ElementElement.Value : null; }
                set
                {
                    if (value == null)
                        ElementElement = null;
                    else
                        ElementElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Element");
                }
            }
            
            /// <summary>
            /// Named context for field, if desired, and a field is specified
            /// </summary>
            [FhirElement("variable", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Id VariableElement
            {
                get { return _VariableElement; }
                set { _VariableElement = value; OnPropertyChanged("VariableElement"); }
            }
            
            private Hl7.Fhir.Model.Id _VariableElement;
            
            /// <summary>
            /// Named context for field, if desired, and a field is specified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Variable
            {
                get { return VariableElement != null ? VariableElement.Value : null; }
                set
                {
                    if (value == null)
                        VariableElement = null;
                    else
                        VariableElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Variable");
                }
            }
            
            /// <summary>
            /// first | share | last | collate
            /// </summary>
            [FhirElement("listMode", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.StructureMapTargetListMode>> ListModeElement
            {
                get { if(_ListModeElement==null) _ListModeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.StructureMapTargetListMode>>(); return _ListModeElement; }
                set { _ListModeElement = value; OnPropertyChanged("ListModeElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.StructureMapTargetListMode>> _ListModeElement;
            
            /// <summary>
            /// first | share | last | collate
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.StructureMapTargetListMode?> ListMode
            {
                get { return ListModeElement != null ? ListModeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ListModeElement = null;
                    else
                        ListModeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.StructureMapTargetListMode>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.StructureMapTargetListMode>(elem)));
                    OnPropertyChanged("ListMode");
                }
            }
            
            /// <summary>
            /// Internal rule reference for shared list items
            /// </summary>
            [FhirElement("listRuleId", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Id ListRuleIdElement
            {
                get { return _ListRuleIdElement; }
                set { _ListRuleIdElement = value; OnPropertyChanged("ListRuleIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ListRuleIdElement;
            
            /// <summary>
            /// Internal rule reference for shared list items
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ListRuleId
            {
                get { return ListRuleIdElement != null ? ListRuleIdElement.Value : null; }
                set
                {
                    if (value == null)
                        ListRuleIdElement = null;
                    else
                        ListRuleIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("ListRuleId");
                }
            }
            
            /// <summary>
            /// create | copy +
            /// </summary>
            [FhirElement("transform", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [DataMember]
            public Code<Hl7.Fhir.Model.StructureMapTransform> TransformElement
            {
                get { return _TransformElement; }
                set { _TransformElement = value; OnPropertyChanged("TransformElement"); }
            }
            
            private Code<Hl7.Fhir.Model.StructureMapTransform> _TransformElement;
            
            /// <summary>
            /// create | copy +
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.StructureMapTransform? Transform
            {
                get { return TransformElement != null ? TransformElement.Value : null; }
                set
                {
                    if (value == null)
                        TransformElement = null;
                    else
                        TransformElement = new Code<Hl7.Fhir.Model.StructureMapTransform>(value);
                    OnPropertyChanged("Transform");
                }
            }
            
            /// <summary>
            /// Parameters to the transform
            /// </summary>
            [FhirElement("parameter", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ParameterComponent> Parameter
            {
                get { if(_Parameter==null) _Parameter = new List<ParameterComponent>(); return _Parameter; }
                set { _Parameter = value; OnPropertyChanged("Parameter"); }
            }
            
            private List<ParameterComponent> _Parameter;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TargetComponent");
                base.Serialize(sink);
                sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ContextElement?.Serialize(sink);
                sink.Element("contextType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ContextTypeElement?.Serialize(sink);
                sink.Element("element", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ElementElement?.Serialize(sink);
                sink.Element("variable", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VariableElement?.Serialize(sink);
                sink.BeginList("listMode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(ListModeElement);
                sink.End();
                sink.Element("listRuleId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ListRuleIdElement?.Serialize(sink);
                sink.Element("transform", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TransformElement?.Serialize(sink);
                sink.BeginList("parameter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Parameter)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TargetComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ContextElement != null) dest.ContextElement = (Hl7.Fhir.Model.Id)ContextElement.DeepCopy();
                    if(ContextTypeElement != null) dest.ContextTypeElement = (Code<Hl7.Fhir.Model.StructureMapContextType>)ContextTypeElement.DeepCopy();
                    if(ElementElement != null) dest.ElementElement = (Hl7.Fhir.Model.FhirString)ElementElement.DeepCopy();
                    if(VariableElement != null) dest.VariableElement = (Hl7.Fhir.Model.Id)VariableElement.DeepCopy();
                    if(ListModeElement != null) dest.ListModeElement = new List<Code<Hl7.Fhir.Model.StructureMapTargetListMode>>(ListModeElement.DeepCopy());
                    if(ListRuleIdElement != null) dest.ListRuleIdElement = (Hl7.Fhir.Model.Id)ListRuleIdElement.DeepCopy();
                    if(TransformElement != null) dest.TransformElement = (Code<Hl7.Fhir.Model.StructureMapTransform>)TransformElement.DeepCopy();
                    if(Parameter != null) dest.Parameter = new List<ParameterComponent>(Parameter.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TargetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TargetComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ContextElement, otherT.ContextElement)) return false;
                if( !DeepComparable.Matches(ContextTypeElement, otherT.ContextTypeElement)) return false;
                if( !DeepComparable.Matches(ElementElement, otherT.ElementElement)) return false;
                if( !DeepComparable.Matches(VariableElement, otherT.VariableElement)) return false;
                if( !DeepComparable.Matches(ListModeElement, otherT.ListModeElement)) return false;
                if( !DeepComparable.Matches(ListRuleIdElement, otherT.ListRuleIdElement)) return false;
                if( !DeepComparable.Matches(TransformElement, otherT.TransformElement)) return false;
                if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TargetComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ContextElement, otherT.ContextElement)) return false;
                if( !DeepComparable.IsExactly(ContextTypeElement, otherT.ContextTypeElement)) return false;
                if( !DeepComparable.IsExactly(ElementElement, otherT.ElementElement)) return false;
                if( !DeepComparable.IsExactly(VariableElement, otherT.VariableElement)) return false;
                if( !DeepComparable.IsExactly(ListModeElement, otherT.ListModeElement)) return false;
                if( !DeepComparable.IsExactly(ListRuleIdElement, otherT.ListRuleIdElement)) return false;
                if( !DeepComparable.IsExactly(TransformElement, otherT.TransformElement)) return false;
                if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ContextElement != null) yield return ContextElement;
                    if (ContextTypeElement != null) yield return ContextTypeElement;
                    if (ElementElement != null) yield return ElementElement;
                    if (VariableElement != null) yield return VariableElement;
                    foreach (var elem in ListModeElement) { if (elem != null) yield return elem; }
                    if (ListRuleIdElement != null) yield return ListRuleIdElement;
                    if (TransformElement != null) yield return TransformElement;
                    foreach (var elem in Parameter) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ContextElement != null) yield return new ElementValue("context", ContextElement);
                    if (ContextTypeElement != null) yield return new ElementValue("contextType", ContextTypeElement);
                    if (ElementElement != null) yield return new ElementValue("element", ElementElement);
                    if (VariableElement != null) yield return new ElementValue("variable", VariableElement);
                    foreach (var elem in ListModeElement) { if (elem != null) yield return new ElementValue("listMode", elem); }
                    if (ListRuleIdElement != null) yield return new ElementValue("listRuleId", ListRuleIdElement);
                    if (TransformElement != null) yield return new ElementValue("transform", TransformElement);
                    foreach (var elem in Parameter) { if (elem != null) yield return new ElementValue("parameter", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ParameterComponent")]
        [DataContract]
        public partial class ParameterComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IStructureMapParameterComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParameterComponent"; } }
            
            /// <summary>
            /// Parameter value - variable or literal
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParameterComponent");
                base.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Value?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParameterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ParameterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Value != null) yield return Value;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "DependentComponent")]
        [DataContract]
        public partial class DependentComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IStructureMapDependentComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DependentComponent"; } }
            
            /// <summary>
            /// Name of a rule or group to apply
            /// </summary>
            [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.Id _NameElement;
            
            /// <summary>
            /// Name of a rule or group to apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if (value == null)
                        NameElement = null;
                    else
                        NameElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Variable to pass to the rule or group
            /// </summary>
            [FhirElement("variable", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> VariableElement
            {
                get { if(_VariableElement==null) _VariableElement = new List<Hl7.Fhir.Model.FhirString>(); return _VariableElement; }
                set { _VariableElement = value; OnPropertyChanged("VariableElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _VariableElement;
            
            /// <summary>
            /// Variable to pass to the rule or group
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Variable
            {
                get { return VariableElement != null ? VariableElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        VariableElement = null;
                    else
                        VariableElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Variable");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DependentComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); NameElement?.Serialize(sink);
                sink.BeginList("variable", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
                sink.Serialize(VariableElement);
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DependentComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.Id)NameElement.DeepCopy();
                    if(VariableElement != null) dest.VariableElement = new List<Hl7.Fhir.Model.FhirString>(VariableElement.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DependentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DependentComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(VariableElement, otherT.VariableElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DependentComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(VariableElement, otherT.VariableElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    foreach (var elem in VariableElement) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    foreach (var elem in VariableElement) { if (elem != null) yield return new ElementValue("variable", elem); }
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactDetail> Hl7.Fhir.Model.IStructureMap.Contact { get { return Contact; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IStructureMapStructureComponent> Hl7.Fhir.Model.IStructureMap.Structure { get { return Structure; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IStructureMapGroupComponent> Hl7.Fhir.Model.IStructureMap.Group { get { return Group; } }
    
        
        /// <summary>
        /// Canonical identifier for this structure map, represented as a URI (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Canonical identifier for this structure map, represented as a URI (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if (value == null)
                    UrlElement = null;
                else
                    UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Additional identifier for the structure map
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
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
        /// Business version of the structure map
        /// </summary>
        [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business version of the structure map
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if (value == null)
                    VersionElement = null;
                else
                    VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Name for this structure map (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name for this structure map (computer friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if (value == null)
                    NameElement = null;
                else
                    NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Name for this structure map (human friendly)
        /// </summary>
        [FhirElement("title", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name for this structure map (human friendly)
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
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.PublicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if (value == null)
                    ExperimentalElement = null;
                else
                    ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Date last changed
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date last changed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if (value == null)
                    DateElement = null;
                else
                    DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if (value == null)
                    PublisherElement = null;
                else
                    PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact details for the publisher
        /// </summary>
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.R4.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.R4.ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the structure map
        /// </summary>
        [FhirElement("description", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _DescriptionElement;
        
        /// <summary>
        /// Natural language description of the structure map
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
                    DescriptionElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<Hl7.Fhir.Model.UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for structure map (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// Why this structure map is defined
        /// </summary>
        [FhirElement("purpose", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown PurposeElement
        {
            get { return _PurposeElement; }
            set { _PurposeElement = value; OnPropertyChanged("PurposeElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _PurposeElement;
        
        /// <summary>
        /// Why this structure map is defined
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Purpose
        {
            get { return PurposeElement != null ? PurposeElement.Value : null; }
            set
            {
                if (value == null)
                    PurposeElement = null;
                else
                    PurposeElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Purpose");
            }
        }
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown CopyrightElement
        {
            get { return _CopyrightElement; }
            set { _CopyrightElement = value; OnPropertyChanged("CopyrightElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _CopyrightElement;
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Copyright
        {
            get { return CopyrightElement != null ? CopyrightElement.Value : null; }
            set
            {
                if (value == null)
                    CopyrightElement = null;
                else
                    CopyrightElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Copyright");
            }
        }
        
        /// <summary>
        /// Structure Definition used by this map
        /// </summary>
        [FhirElement("structure", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<StructureComponent> Structure
        {
            get { if(_Structure==null) _Structure = new List<StructureComponent>(); return _Structure; }
            set { _Structure = value; OnPropertyChanged("Structure"); }
        }
        
        private List<StructureComponent> _Structure;
        
        /// <summary>
        /// Other maps used by this map (canonical URLs)
        /// </summary>
        [FhirElement("import", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> ImportElement
        {
            get { if(_ImportElement==null) _ImportElement = new List<Hl7.Fhir.Model.Canonical>(); return _ImportElement; }
            set { _ImportElement = value; OnPropertyChanged("ImportElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _ImportElement;
        
        /// <summary>
        /// Other maps used by this map (canonical URLs)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Import
        {
            get { return ImportElement != null ? ImportElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    ImportElement = null;
                else
                    ImportElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("Import");
            }
        }
        
        /// <summary>
        /// Named sections for reader convenience
        /// </summary>
        [FhirElement("group", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<GroupComponent> Group
        {
            get { if(_Group==null) _Group = new List<GroupComponent>(); return _Group; }
            set { _Group = value; OnPropertyChanged("Group"); }
        }
        
        private List<GroupComponent> _Group;
    
    
        public static ElementDefinitionConstraint[] StructureMap_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "smp-0",
                severity: ConstraintSeverity.Warning,
                expression: "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
                human: "Name should be usable as an identifier for the module by machine processing applications such as code generation",
                xpath: "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "smp-2",
                severity: ConstraintSeverity.Warning,
                expression: "group.rule.target.all(context.exists() implies contextType.exists())",
                human: "Must have a contextType if you have a context",
                xpath: "not(f:context) or (f:contextType)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "smp-1",
                severity: ConstraintSeverity.Warning,
                expression: "group.rule.target.all(element.exists() implies context.exists())",
                human: "Can only have an element if you have a context",
                xpath: "not(f:element) or (f:context)"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(StructureMap_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as StructureMap;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.R4.ContactDetail>(Contact.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.Markdown)PurposeElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.Markdown)CopyrightElement.DeepCopy();
                if(Structure != null) dest.Structure = new List<StructureComponent>(Structure.DeepCopy());
                if(ImportElement != null) dest.ImportElement = new List<Hl7.Fhir.Model.Canonical>(ImportElement.DeepCopy());
                if(Group != null) dest.Group = new List<GroupComponent>(Group.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new StructureMap());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as StructureMap;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(Structure, otherT.Structure)) return false;
            if( !DeepComparable.Matches(ImportElement, otherT.ImportElement)) return false;
            if( !DeepComparable.Matches(Group, otherT.Group)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as StructureMap;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(Structure, otherT.Structure)) return false;
            if( !DeepComparable.IsExactly(ImportElement, otherT.ImportElement)) return false;
            if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("StructureMap");
            base.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); UrlElement?.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); NameElement?.Serialize(sink);
            sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TitleElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("experimental", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExperimentalElement?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.Element("publisher", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PublisherElement?.Serialize(sink);
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
            sink.BeginList("useContext", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in UseContext)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("jurisdiction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Jurisdiction)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("purpose", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PurposeElement?.Serialize(sink);
            sink.Element("copyright", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CopyrightElement?.Serialize(sink);
            sink.BeginList("structure", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Structure)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("import", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(ImportElement);
            sink.End();
            sink.BeginList("group", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
            foreach(var item in Group)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (UrlElement != null) yield return UrlElement;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (VersionElement != null) yield return VersionElement;
                if (NameElement != null) yield return NameElement;
                if (TitleElement != null) yield return TitleElement;
                if (StatusElement != null) yield return StatusElement;
                if (ExperimentalElement != null) yield return ExperimentalElement;
                if (DateElement != null) yield return DateElement;
                if (PublisherElement != null) yield return PublisherElement;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                if (PurposeElement != null) yield return PurposeElement;
                if (CopyrightElement != null) yield return CopyrightElement;
                foreach (var elem in Structure) { if (elem != null) yield return elem; }
                foreach (var elem in ImportElement) { if (elem != null) yield return elem; }
                foreach (var elem in Group) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (PurposeElement != null) yield return new ElementValue("purpose", PurposeElement);
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                foreach (var elem in Structure) { if (elem != null) yield return new ElementValue("structure", elem); }
                foreach (var elem in ImportElement) { if (elem != null) yield return new ElementValue("import", elem); }
                foreach (var elem in Group) { if (elem != null) yield return new ElementValue("group", elem); }
            }
        }
    
    }

}
