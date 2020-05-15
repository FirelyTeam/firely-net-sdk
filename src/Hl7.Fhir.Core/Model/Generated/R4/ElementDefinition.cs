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
    /// Definition of an element in a resource or extension
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "ElementDefinition")]
    [DataContract]
    public partial class ElementDefinition : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IElementDefinition, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "ElementDefinition"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SlicingComponent")]
        [DataContract]
        public partial class SlicingComponent : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IElementDefinitionSlicingComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SlicingComponent"; } }
            
            /// <summary>
            /// Element values that are used to distinguish the slices
            /// </summary>
            [FhirElement("discriminator", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<DiscriminatorComponent> Discriminator
            {
                get { if(_Discriminator==null) _Discriminator = new List<DiscriminatorComponent>(); return _Discriminator; }
                set { _Discriminator = value; OnPropertyChanged("Discriminator"); }
            }
            
            private List<DiscriminatorComponent> _Discriminator;
            
            /// <summary>
            /// Text description of how slicing works (or not)
            /// </summary>
            [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Text description of how slicing works (or not)
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
            /// If elements must be in same order as slices
            /// </summary>
            [FhirElement("ordered", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
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
                    if (value == null)
                        OrderedElement = null;
                    else
                        OrderedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Ordered");
                }
            }
            
            /// <summary>
            /// closed | open | openAtEnd
            /// </summary>
            [FhirElement("rules", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.SlicingRules> RulesElement
            {
                get { return _RulesElement; }
                set { _RulesElement = value; OnPropertyChanged("RulesElement"); }
            }
            
            private Code<Hl7.Fhir.Model.SlicingRules> _RulesElement;
            
            /// <summary>
            /// closed | open | openAtEnd
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.SlicingRules? Rules
            {
                get { return RulesElement != null ? RulesElement.Value : null; }
                set
                {
                    if (value == null)
                        RulesElement = null;
                    else
                        RulesElement = new Code<Hl7.Fhir.Model.SlicingRules>(value);
                    OnPropertyChanged("Rules");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SlicingComponent");
                base.Serialize(sink);
                sink.BeginList("discriminator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Discriminator)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("ordered", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OrderedElement?.Serialize(sink);
                sink.Element("rules", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); RulesElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SlicingComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Discriminator != null) dest.Discriminator = new List<DiscriminatorComponent>(Discriminator.DeepCopy());
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(OrderedElement != null) dest.OrderedElement = (Hl7.Fhir.Model.FhirBoolean)OrderedElement.DeepCopy();
                    if(RulesElement != null) dest.RulesElement = (Code<Hl7.Fhir.Model.SlicingRules>)RulesElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SlicingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SlicingComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Discriminator, otherT.Discriminator)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(OrderedElement, otherT.OrderedElement)) return false;
                if( !DeepComparable.Matches(RulesElement, otherT.RulesElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SlicingComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Discriminator, otherT.Discriminator)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(OrderedElement, otherT.OrderedElement)) return false;
                if( !DeepComparable.IsExactly(RulesElement, otherT.RulesElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Discriminator) { if (elem != null) yield return elem; }
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (OrderedElement != null) yield return OrderedElement;
                    if (RulesElement != null) yield return RulesElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Discriminator) { if (elem != null) yield return new ElementValue("discriminator", elem); }
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (OrderedElement != null) yield return new ElementValue("ordered", OrderedElement);
                    if (RulesElement != null) yield return new ElementValue("rules", RulesElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "DiscriminatorComponent")]
        [DataContract]
        public partial class DiscriminatorComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DiscriminatorComponent"; } }
            
            /// <summary>
            /// value | exists | pattern | type | profile
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DiscriminatorType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DiscriminatorType> _TypeElement;
            
            /// <summary>
            /// value | exists | pattern | type | profile
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DiscriminatorType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.DiscriminatorType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Path to element value
            /// </summary>
            [FhirElement("path", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// Path to element value
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if (value == null)
                        PathElement = null;
                    else
                        PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DiscriminatorComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); PathElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DiscriminatorComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.DiscriminatorType>)TypeElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DiscriminatorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DiscriminatorComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DiscriminatorComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (PathElement != null) yield return PathElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "BaseComponent")]
        [DataContract]
        public partial class BaseComponent : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IElementDefinitionBaseComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "BaseComponent"; } }
            
            /// <summary>
            /// Path that identifies the base element
            /// </summary>
            [FhirElement("path", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// Path that identifies the base element
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if (value == null)
                        PathElement = null;
                    else
                        PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// Min cardinality of the base element
            /// </summary>
            [FhirElement("min", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt MinElement
            {
                get { return _MinElement; }
                set { _MinElement = value; OnPropertyChanged("MinElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _MinElement;
            
            /// <summary>
            /// Min cardinality of the base element
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
                        MinElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("Min");
                }
            }
            
            /// <summary>
            /// Max cardinality of the base element
            /// </summary>
            [FhirElement("max", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MaxElement
            {
                get { return _MaxElement; }
                set { _MaxElement = value; OnPropertyChanged("MaxElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MaxElement;
            
            /// <summary>
            /// Max cardinality of the base element
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("BaseComponent");
                base.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); PathElement?.Serialize(sink);
                sink.Element("min", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); MinElement?.Serialize(sink);
                sink.Element("max", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); MaxElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BaseComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(MinElement != null) dest.MinElement = (Hl7.Fhir.Model.UnsignedInt)MinElement.DeepCopy();
                    if(MaxElement != null) dest.MaxElement = (Hl7.Fhir.Model.FhirString)MaxElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new BaseComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BaseComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(MinElement, otherT.MinElement)) return false;
                if( !DeepComparable.Matches(MaxElement, otherT.MaxElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BaseComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(MinElement, otherT.MinElement)) return false;
                if( !DeepComparable.IsExactly(MaxElement, otherT.MaxElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (PathElement != null) yield return PathElement;
                    if (MinElement != null) yield return MinElement;
                    if (MaxElement != null) yield return MaxElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (MinElement != null) yield return new ElementValue("min", MinElement);
                    if (MaxElement != null) yield return new ElementValue("max", MaxElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "TypeRefComponent")]
        [DataContract]
        public partial class TypeRefComponent : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IElementDefinitionTypeRefComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TypeRefComponent"; } }
            
            /// <summary>
            /// Data type or Resource (reference to definition)
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _CodeElement;
            
            /// <summary>
            /// Data type or Resource (reference to definition)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Profiles (StructureDefinition or IG) - one must apply
            /// </summary>
            [FhirElement("profile", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Canonical> ProfileElement
            {
                get { if(_ProfileElement==null) _ProfileElement = new List<Hl7.Fhir.Model.Canonical>(); return _ProfileElement; }
                set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
            }
            
            private List<Hl7.Fhir.Model.Canonical> _ProfileElement;
            
            /// <summary>
            /// Profiles (StructureDefinition or IG) - one must apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Profile
            {
                get { return ProfileElement != null ? ProfileElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ProfileElement = null;
                    else
                        ProfileElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                    OnPropertyChanged("Profile");
                }
            }
            
            /// <summary>
            /// Profile (StructureDefinition or IG) on the Reference/canonical target - one must apply
            /// </summary>
            [FhirElement("targetProfile", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Canonical> TargetProfileElement
            {
                get { if(_TargetProfileElement==null) _TargetProfileElement = new List<Hl7.Fhir.Model.Canonical>(); return _TargetProfileElement; }
                set { _TargetProfileElement = value; OnPropertyChanged("TargetProfileElement"); }
            }
            
            private List<Hl7.Fhir.Model.Canonical> _TargetProfileElement;
            
            /// <summary>
            /// Profile (StructureDefinition or IG) on the Reference/canonical target - one must apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> TargetProfile
            {
                get { return TargetProfileElement != null ? TargetProfileElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        TargetProfileElement = null;
                    else
                        TargetProfileElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                    OnPropertyChanged("TargetProfile");
                }
            }
            
            /// <summary>
            /// contained | referenced | bundled - how aggregated
            /// </summary>
            [FhirElement("aggregation", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.AggregationMode>> AggregationElement
            {
                get { if(_AggregationElement==null) _AggregationElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AggregationMode>>(); return _AggregationElement; }
                set { _AggregationElement = value; OnPropertyChanged("AggregationElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.AggregationMode>> _AggregationElement;
            
            /// <summary>
            /// contained | referenced | bundled - how aggregated
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.AggregationMode?> Aggregation
            {
                get { return AggregationElement != null ? AggregationElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        AggregationElement = null;
                    else
                        AggregationElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AggregationMode>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AggregationMode>(elem)));
                    OnPropertyChanged("Aggregation");
                }
            }
            
            /// <summary>
            /// either | independent | specific
            /// </summary>
            [FhirElement("versioning", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ReferenceVersionRules> VersioningElement
            {
                get { return _VersioningElement; }
                set { _VersioningElement = value; OnPropertyChanged("VersioningElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ReferenceVersionRules> _VersioningElement;
            
            /// <summary>
            /// either | independent | specific
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ReferenceVersionRules? Versioning
            {
                get { return VersioningElement != null ? VersioningElement.Value : null; }
                set
                {
                    if (value == null)
                        VersioningElement = null;
                    else
                        VersioningElement = new Code<Hl7.Fhir.Model.ReferenceVersionRules>(value);
                    OnPropertyChanged("Versioning");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TypeRefComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); CodeElement?.Serialize(sink);
                sink.BeginList("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(ProfileElement);
                sink.End();
                sink.BeginList("targetProfile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(TargetProfileElement);
                sink.End();
                sink.BeginList("aggregation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(AggregationElement);
                sink.End();
                sink.Element("versioning", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersioningElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TypeRefComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.FhirUri)CodeElement.DeepCopy();
                    if(ProfileElement != null) dest.ProfileElement = new List<Hl7.Fhir.Model.Canonical>(ProfileElement.DeepCopy());
                    if(TargetProfileElement != null) dest.TargetProfileElement = new List<Hl7.Fhir.Model.Canonical>(TargetProfileElement.DeepCopy());
                    if(AggregationElement != null) dest.AggregationElement = new List<Code<Hl7.Fhir.Model.AggregationMode>>(AggregationElement.DeepCopy());
                    if(VersioningElement != null) dest.VersioningElement = (Code<Hl7.Fhir.Model.ReferenceVersionRules>)VersioningElement.DeepCopy();
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
                if( !DeepComparable.Matches(TargetProfileElement, otherT.TargetProfileElement)) return false;
                if( !DeepComparable.Matches(AggregationElement, otherT.AggregationElement)) return false;
                if( !DeepComparable.Matches(VersioningElement, otherT.VersioningElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TypeRefComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
                if( !DeepComparable.IsExactly(TargetProfileElement, otherT.TargetProfileElement)) return false;
                if( !DeepComparable.IsExactly(AggregationElement, otherT.AggregationElement)) return false;
                if( !DeepComparable.IsExactly(VersioningElement, otherT.VersioningElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    foreach (var elem in ProfileElement) { if (elem != null) yield return elem; }
                    foreach (var elem in TargetProfileElement) { if (elem != null) yield return elem; }
                    foreach (var elem in AggregationElement) { if (elem != null) yield return elem; }
                    if (VersioningElement != null) yield return VersioningElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    foreach (var elem in ProfileElement) { if (elem != null) yield return new ElementValue("profile", elem); }
                    foreach (var elem in TargetProfileElement) { if (elem != null) yield return new ElementValue("targetProfile", elem); }
                    foreach (var elem in AggregationElement) { if (elem != null) yield return new ElementValue("aggregation", elem); }
                    if (VersioningElement != null) yield return new ElementValue("versioning", VersioningElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ExampleComponent")]
        [DataContract]
        public partial class ExampleComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ExampleComponent"; } }
            
            /// <summary>
            /// Describes the purpose of this example
            /// </summary>
            [FhirElement("label", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LabelElement
            {
                get { return _LabelElement; }
                set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LabelElement;
            
            /// <summary>
            /// Describes the purpose of this example
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
            /// Value of Example (one of allowed types)
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Canonical),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Url),typeof(Hl7.Fhir.Model.Uuid),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.R4.Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.R4.ContactPoint),typeof(Hl7.Fhir.Model.R4.Count),typeof(Hl7.Fhir.Model.R4.Distance),typeof(Hl7.Fhir.Model.R4.Duration),typeof(Hl7.Fhir.Model.R4.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.R4.Money),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.R4.SampledData),typeof(Hl7.Fhir.Model.R4.Signature),typeof(Hl7.Fhir.Model.R4.Timing),typeof(Hl7.Fhir.Model.R4.ContactDetail),typeof(Hl7.Fhir.Model.R4.Contributor),typeof(Hl7.Fhir.Model.R4.DataRequirement),typeof(Hl7.Fhir.Model.Expression),typeof(Hl7.Fhir.Model.R4.ParameterDefinition),typeof(Hl7.Fhir.Model.R4.RelatedArtifact),typeof(Hl7.Fhir.Model.R4.TriggerDefinition),typeof(Hl7.Fhir.Model.UsageContext),typeof(Hl7.Fhir.Model.R4.Dosage),typeof(Hl7.Fhir.Model.Meta))]
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
                sink.BeginDataType("ExampleComponent");
                base.Serialize(sink);
                sink.Element("label", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); LabelElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Value?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ExampleComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ExampleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ExampleComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ExampleComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LabelElement != null) yield return LabelElement;
                    if (Value != null) yield return Value;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (LabelElement != null) yield return new ElementValue("label", LabelElement);
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ConstraintComponent")]
        [DataContract]
        public partial class ConstraintComponent : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IElementDefinitionConstraintComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ConstraintComponent"; } }
            
            /// <summary>
            /// Target of 'condition' reference above
            /// </summary>
            [FhirElement("key", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
                    if (value == null)
                        KeyElement = null;
                    else
                        KeyElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Key");
                }
            }
            
            /// <summary>
            /// Why this constraint is necessary or appropriate
            /// </summary>
            [FhirElement("requirements", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RequirementsElement
            {
                get { return _RequirementsElement; }
                set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _RequirementsElement;
            
            /// <summary>
            /// Why this constraint is necessary or appropriate
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Requirements
            {
                get { return RequirementsElement != null ? RequirementsElement.Value : null; }
                set
                {
                    if (value == null)
                        RequirementsElement = null;
                    else
                        RequirementsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Requirements");
                }
            }
            
            /// <summary>
            /// error | warning
            /// </summary>
            [FhirElement("severity", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ConstraintSeverity> SeverityElement
            {
                get { return _SeverityElement; }
                set { _SeverityElement = value; OnPropertyChanged("SeverityElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ConstraintSeverity> _SeverityElement;
            
            /// <summary>
            /// error | warning
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ConstraintSeverity? Severity
            {
                get { return SeverityElement != null ? SeverityElement.Value : null; }
                set
                {
                    if (value == null)
                        SeverityElement = null;
                    else
                        SeverityElement = new Code<Hl7.Fhir.Model.ConstraintSeverity>(value);
                    OnPropertyChanged("Severity");
                }
            }
            
            /// <summary>
            /// Human description of constraint
            /// </summary>
            [FhirElement("human", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
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
                    if (value == null)
                        HumanElement = null;
                    else
                        HumanElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Human");
                }
            }
            
            /// <summary>
            /// FHIRPath expression of constraint
            /// </summary>
            [FhirElement("expression", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ExpressionElement
            {
                get { return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ExpressionElement;
            
            /// <summary>
            /// FHIRPath expression of constraint
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
            
            /// <summary>
            /// XPath expression of constraint
            /// </summary>
            [FhirElement("xpath", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
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
                    if (value == null)
                        XpathElement = null;
                    else
                        XpathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Xpath");
                }
            }
            
            /// <summary>
            /// Reference to original source of constraint
            /// </summary>
            [FhirElement("source", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical SourceElement
            {
                get { return _SourceElement; }
                set { _SourceElement = value; OnPropertyChanged("SourceElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _SourceElement;
            
            /// <summary>
            /// Reference to original source of constraint
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Source
            {
                get { return SourceElement != null ? SourceElement.Value : null; }
                set
                {
                    if (value == null)
                        SourceElement = null;
                    else
                        SourceElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("Source");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ConstraintComponent");
                base.Serialize(sink);
                sink.Element("key", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); KeyElement?.Serialize(sink);
                sink.Element("requirements", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RequirementsElement?.Serialize(sink);
                sink.Element("severity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); SeverityElement?.Serialize(sink);
                sink.Element("human", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); HumanElement?.Serialize(sink);
                sink.Element("expression", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExpressionElement?.Serialize(sink);
                sink.Element("xpath", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); XpathElement?.Serialize(sink);
                sink.Element("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SourceElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConstraintComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(KeyElement != null) dest.KeyElement = (Hl7.Fhir.Model.Id)KeyElement.DeepCopy();
                    if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
                    if(SeverityElement != null) dest.SeverityElement = (Code<Hl7.Fhir.Model.ConstraintSeverity>)SeverityElement.DeepCopy();
                    if(HumanElement != null) dest.HumanElement = (Hl7.Fhir.Model.FhirString)HumanElement.DeepCopy();
                    if(ExpressionElement != null) dest.ExpressionElement = (Hl7.Fhir.Model.FhirString)ExpressionElement.DeepCopy();
                    if(XpathElement != null) dest.XpathElement = (Hl7.Fhir.Model.FhirString)XpathElement.DeepCopy();
                    if(SourceElement != null) dest.SourceElement = (Hl7.Fhir.Model.Canonical)SourceElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ConstraintComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConstraintComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(KeyElement, otherT.KeyElement)) return false;
                if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
                if( !DeepComparable.Matches(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.Matches(HumanElement, otherT.HumanElement)) return false;
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
                if( !DeepComparable.Matches(XpathElement, otherT.XpathElement)) return false;
                if( !DeepComparable.Matches(SourceElement, otherT.SourceElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConstraintComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(KeyElement, otherT.KeyElement)) return false;
                if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
                if( !DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.IsExactly(HumanElement, otherT.HumanElement)) return false;
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
                if( !DeepComparable.IsExactly(XpathElement, otherT.XpathElement)) return false;
                if( !DeepComparable.IsExactly(SourceElement, otherT.SourceElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (KeyElement != null) yield return KeyElement;
                    if (RequirementsElement != null) yield return RequirementsElement;
                    if (SeverityElement != null) yield return SeverityElement;
                    if (HumanElement != null) yield return HumanElement;
                    if (ExpressionElement != null) yield return ExpressionElement;
                    if (XpathElement != null) yield return XpathElement;
                    if (SourceElement != null) yield return SourceElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (KeyElement != null) yield return new ElementValue("key", KeyElement);
                    if (RequirementsElement != null) yield return new ElementValue("requirements", RequirementsElement);
                    if (SeverityElement != null) yield return new ElementValue("severity", SeverityElement);
                    if (HumanElement != null) yield return new ElementValue("human", HumanElement);
                    if (ExpressionElement != null) yield return new ElementValue("expression", ExpressionElement);
                    if (XpathElement != null) yield return new ElementValue("xpath", XpathElement);
                    if (SourceElement != null) yield return new ElementValue("source", SourceElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ElementDefinitionBindingComponent")]
        [DataContract]
        public partial class ElementDefinitionBindingComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ElementDefinitionBindingComponent"; } }
            
            /// <summary>
            /// required | extensible | preferred | example
            /// </summary>
            [FhirElement("strength", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.BindingStrength> StrengthElement
            {
                get { return _StrengthElement; }
                set { _StrengthElement = value; OnPropertyChanged("StrengthElement"); }
            }
            
            private Code<Hl7.Fhir.Model.BindingStrength> _StrengthElement;
            
            /// <summary>
            /// required | extensible | preferred | example
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.BindingStrength? Strength
            {
                get { return StrengthElement != null ? StrengthElement.Value : null; }
                set
                {
                    if (value == null)
                        StrengthElement = null;
                    else
                        StrengthElement = new Code<Hl7.Fhir.Model.BindingStrength>(value);
                    OnPropertyChanged("Strength");
                }
            }
            
            /// <summary>
            /// Human explanation of the value set
            /// </summary>
            [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
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
                    if (value == null)
                        DescriptionElement = null;
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Source of value set
            /// </summary>
            [FhirElement("valueSet", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical ValueSetElement
            {
                get { return _ValueSetElement; }
                set { _ValueSetElement = value; OnPropertyChanged("ValueSetElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _ValueSetElement;
            
            /// <summary>
            /// Source of value set
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ValueSet
            {
                get { return ValueSetElement != null ? ValueSetElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueSetElement = null;
                    else
                        ValueSetElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("ValueSet");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ElementDefinitionBindingComponent");
                base.Serialize(sink);
                sink.Element("strength", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StrengthElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("valueSet", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ValueSetElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ElementDefinitionBindingComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StrengthElement != null) dest.StrengthElement = (Code<Hl7.Fhir.Model.BindingStrength>)StrengthElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(ValueSetElement != null) dest.ValueSetElement = (Hl7.Fhir.Model.Canonical)ValueSetElement.DeepCopy();
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
                if( !DeepComparable.Matches(StrengthElement, otherT.StrengthElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(ValueSetElement, otherT.ValueSetElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ElementDefinitionBindingComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StrengthElement, otherT.StrengthElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(ValueSetElement, otherT.ValueSetElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (StrengthElement != null) yield return StrengthElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (ValueSetElement != null) yield return ValueSetElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (StrengthElement != null) yield return new ElementValue("strength", StrengthElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (ValueSetElement != null) yield return new ElementValue("valueSet", ValueSetElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "MappingComponent")]
        [DataContract]
        public partial class MappingComponent : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IElementDefinitionMappingComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "MappingComponent"; } }
            
            /// <summary>
            /// Reference to mapping declaration
            /// </summary>
            [FhirElement("identity", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
                    if (value == null)
                        IdentityElement = null;
                    else
                        IdentityElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Identity");
                }
            }
            
            /// <summary>
            /// Computable language of mapping
            /// </summary>
            [FhirElement("language", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Code LanguageElement
            {
                get { return _LanguageElement; }
                set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
            }
            
            private Hl7.Fhir.Model.Code _LanguageElement;
            
            /// <summary>
            /// Computable language of mapping
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
                        LanguageElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Language");
                }
            }
            
            /// <summary>
            /// Details of the mapping
            /// </summary>
            [FhirElement("map", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
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
                    if (value == null)
                        MapElement = null;
                    else
                        MapElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Map");
                }
            }
            
            /// <summary>
            /// Comments about the mapping or its use
            /// </summary>
            [FhirElement("comment", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentElement
            {
                get { return _CommentElement; }
                set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CommentElement;
            
            /// <summary>
            /// Comments about the mapping or its use
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Comment
            {
                get { return CommentElement != null ? CommentElement.Value : null; }
                set
                {
                    if (value == null)
                        CommentElement = null;
                    else
                        CommentElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Comment");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("MappingComponent");
                base.Serialize(sink);
                sink.Element("identity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); IdentityElement?.Serialize(sink);
                sink.Element("language", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LanguageElement?.Serialize(sink);
                sink.Element("map", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); MapElement?.Serialize(sink);
                sink.Element("comment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CommentElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MappingComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IdentityElement != null) dest.IdentityElement = (Hl7.Fhir.Model.Id)IdentityElement.DeepCopy();
                    if(LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
                    if(MapElement != null) dest.MapElement = (Hl7.Fhir.Model.FhirString)MapElement.DeepCopy();
                    if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new MappingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MappingComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IdentityElement, otherT.IdentityElement)) return false;
                if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.Matches(MapElement, otherT.MapElement)) return false;
                if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MappingComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IdentityElement, otherT.IdentityElement)) return false;
                if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.IsExactly(MapElement, otherT.MapElement)) return false;
                if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (IdentityElement != null) yield return IdentityElement;
                    if (LanguageElement != null) yield return LanguageElement;
                    if (MapElement != null) yield return MapElement;
                    if (CommentElement != null) yield return CommentElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (IdentityElement != null) yield return new ElementValue("identity", IdentityElement);
                    if (LanguageElement != null) yield return new ElementValue("language", LanguageElement);
                    if (MapElement != null) yield return new ElementValue("map", MapElement);
                    if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                }
            }
        
        
        }
        
        [NotMapped]
        Hl7.Fhir.Model.IElementDefinitionSlicingComponent Hl7.Fhir.Model.IElementDefinition.Slicing { get { return Slicing; } }
        
        [NotMapped]
        Hl7.Fhir.Model.IElementDefinitionBaseComponent Hl7.Fhir.Model.IElementDefinition.Base { get { return Base; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IElementDefinitionTypeRefComponent> Hl7.Fhir.Model.IElementDefinition.Type { get { return Type; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IElementDefinitionConstraintComponent> Hl7.Fhir.Model.IElementDefinition.Constraint { get { return Constraint; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IElementDefinitionMappingComponent> Hl7.Fhir.Model.IElementDefinition.Mapping { get { return Mapping; } }
    
        
        /// <summary>
        /// Path of the element in the hierarchy of elements
        /// </summary>
        [FhirElement("path", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PathElement
        {
            get { return _PathElement; }
            set { _PathElement = value; OnPropertyChanged("PathElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PathElement;
        
        /// <summary>
        /// Path of the element in the hierarchy of elements
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Path
        {
            get { return PathElement != null ? PathElement.Value : null; }
            set
            {
                if (value == null)
                    PathElement = null;
                else
                    PathElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Path");
            }
        }
        
        /// <summary>
        /// xmlAttr | xmlText | typeAttr | cdaText | xhtml
        /// </summary>
        [FhirElement("representation", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.R4.PropertyRepresentation>> RepresentationElement
        {
            get { if(_RepresentationElement==null) _RepresentationElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.PropertyRepresentation>>(); return _RepresentationElement; }
            set { _RepresentationElement = value; OnPropertyChanged("RepresentationElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.R4.PropertyRepresentation>> _RepresentationElement;
        
        /// <summary>
        /// xmlAttr | xmlText | typeAttr | cdaText | xhtml
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.R4.PropertyRepresentation?> Representation
        {
            get { return RepresentationElement != null ? RepresentationElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    RepresentationElement = null;
                else
                    RepresentationElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.PropertyRepresentation>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.PropertyRepresentation>(elem)));
                OnPropertyChanged("Representation");
            }
        }
        
        /// <summary>
        /// Name for this particular element (in a set of slices)
        /// </summary>
        [FhirElement("sliceName", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SliceNameElement
        {
            get { return _SliceNameElement; }
            set { _SliceNameElement = value; OnPropertyChanged("SliceNameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SliceNameElement;
        
        /// <summary>
        /// Name for this particular element (in a set of slices)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string SliceName
        {
            get { return SliceNameElement != null ? SliceNameElement.Value : null; }
            set
            {
                if (value == null)
                    SliceNameElement = null;
                else
                    SliceNameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("SliceName");
            }
        }
        
        /// <summary>
        /// If this slice definition constrains an inherited slice definition (or not)
        /// </summary>
        [FhirElement("sliceIsConstraining", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean SliceIsConstrainingElement
        {
            get { return _SliceIsConstrainingElement; }
            set { _SliceIsConstrainingElement = value; OnPropertyChanged("SliceIsConstrainingElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _SliceIsConstrainingElement;
        
        /// <summary>
        /// If this slice definition constrains an inherited slice definition (or not)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? SliceIsConstraining
        {
            get { return SliceIsConstrainingElement != null ? SliceIsConstrainingElement.Value : null; }
            set
            {
                if (value == null)
                    SliceIsConstrainingElement = null;
                else
                    SliceIsConstrainingElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("SliceIsConstraining");
            }
        }
        
        /// <summary>
        /// Name for element to display with or prompt for element
        /// </summary>
        [FhirElement("label", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString LabelElement
        {
            get { return _LabelElement; }
            set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _LabelElement;
        
        /// <summary>
        /// Name for element to display with or prompt for element
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
        /// Corresponding codes in terminologies
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Code
        {
            get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.Coding>(); return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Code;
        
        /// <summary>
        /// This element is sliced - slices follow
        /// </summary>
        [FhirElement("slicing", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public SlicingComponent Slicing
        {
            get { return _Slicing; }
            set { _Slicing = value; OnPropertyChanged("Slicing"); }
        }
        
        private SlicingComponent _Slicing;
        
        /// <summary>
        /// Concise definition for space-constrained presentation
        /// </summary>
        [FhirElement("short", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ShortElement
        {
            get { return _ShortElement; }
            set { _ShortElement = value; OnPropertyChanged("ShortElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ShortElement;
        
        /// <summary>
        /// Concise definition for space-constrained presentation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Short
        {
            get { return ShortElement != null ? ShortElement.Value : null; }
            set
            {
                if (value == null)
                    ShortElement = null;
                else
                    ShortElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Short");
            }
        }
        
        /// <summary>
        /// Full formal definition as narrative text
        /// </summary>
        [FhirElement("definition", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown DefinitionElement
        {
            get { return _DefinitionElement; }
            set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _DefinitionElement;
        
        /// <summary>
        /// Full formal definition as narrative text
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Definition
        {
            get { return DefinitionElement != null ? DefinitionElement.Value : null; }
            set
            {
                if (value == null)
                    DefinitionElement = null;
                else
                    DefinitionElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Definition");
            }
        }
        
        /// <summary>
        /// Comments about the use of this element
        /// </summary>
        [FhirElement("comment", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown CommentElement
        {
            get { return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _CommentElement;
        
        /// <summary>
        /// Comments about the use of this element
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comment
        {
            get { return CommentElement != null ? CommentElement.Value : null; }
            set
            {
                if (value == null)
                    CommentElement = null;
                else
                    CommentElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Comment");
            }
        }
        
        /// <summary>
        /// Why this resource has been created
        /// </summary>
        [FhirElement("requirements", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown RequirementsElement
        {
            get { return _RequirementsElement; }
            set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _RequirementsElement;
        
        /// <summary>
        /// Why this resource has been created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Requirements
        {
            get { return RequirementsElement != null ? RequirementsElement.Value : null; }
            set
            {
                if (value == null)
                    RequirementsElement = null;
                else
                    RequirementsElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Requirements");
            }
        }
        
        /// <summary>
        /// Other names
        /// </summary>
        [FhirElement("alias", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> AliasElement
        {
            get { if(_AliasElement==null) _AliasElement = new List<Hl7.Fhir.Model.FhirString>(); return _AliasElement; }
            set { _AliasElement = value; OnPropertyChanged("AliasElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _AliasElement;
        
        /// <summary>
        /// Other names
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Alias
        {
            get { return AliasElement != null ? AliasElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    AliasElement = null;
                else
                    AliasElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Alias");
            }
        }
        
        /// <summary>
        /// Minimum Cardinality
        /// </summary>
        [FhirElement("min", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt MinElement
        {
            get { return _MinElement; }
            set { _MinElement = value; OnPropertyChanged("MinElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _MinElement;
        
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
                if (value == null)
                    MinElement = null;
                else
                    MinElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("Min");
            }
        }
        
        /// <summary>
        /// Maximum Cardinality (a number or *)
        /// </summary>
        [FhirElement("max", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
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
                if (value == null)
                    MaxElement = null;
                else
                    MaxElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Max");
            }
        }
        
        /// <summary>
        /// Base definition information for tools
        /// </summary>
        [FhirElement("base", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [DataMember]
        public BaseComponent Base
        {
            get { return _Base; }
            set { _Base = value; OnPropertyChanged("Base"); }
        }
        
        private BaseComponent _Base;
        
        /// <summary>
        /// Reference to definition of content for the element
        /// </summary>
        [FhirElement("contentReference", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri ContentReferenceElement
        {
            get { return _ContentReferenceElement; }
            set { _ContentReferenceElement = value; OnPropertyChanged("ContentReferenceElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _ContentReferenceElement;
        
        /// <summary>
        /// Reference to definition of content for the element
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ContentReference
        {
            get { return ContentReferenceElement != null ? ContentReferenceElement.Value : null; }
            set
            {
                if (value == null)
                    ContentReferenceElement = null;
                else
                    ContentReferenceElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("ContentReference");
            }
        }
        
        /// <summary>
        /// Data type and Profile for this element
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<TypeRefComponent> Type
        {
            get { if(_Type==null) _Type = new List<TypeRefComponent>(); return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private List<TypeRefComponent> _Type;
        
        /// <summary>
        /// Specified value if missing from instance
        /// </summary>
        [FhirElement("defaultValue", InSummary=Hl7.Fhir.Model.Version.All, Order=260, Choice=ChoiceType.DatatypeChoice)]
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
        /// Implicit meaning when this element is missing
        /// </summary>
        [FhirElement("meaningWhenMissing", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown MeaningWhenMissingElement
        {
            get { return _MeaningWhenMissingElement; }
            set { _MeaningWhenMissingElement = value; OnPropertyChanged("MeaningWhenMissingElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _MeaningWhenMissingElement;
        
        /// <summary>
        /// Implicit meaning when this element is missing
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string MeaningWhenMissing
        {
            get { return MeaningWhenMissingElement != null ? MeaningWhenMissingElement.Value : null; }
            set
            {
                if (value == null)
                    MeaningWhenMissingElement = null;
                else
                    MeaningWhenMissingElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("MeaningWhenMissing");
            }
        }
        
        /// <summary>
        /// What the order of the elements means
        /// </summary>
        [FhirElement("orderMeaning", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString OrderMeaningElement
        {
            get { return _OrderMeaningElement; }
            set { _OrderMeaningElement = value; OnPropertyChanged("OrderMeaningElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _OrderMeaningElement;
        
        /// <summary>
        /// What the order of the elements means
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string OrderMeaning
        {
            get { return OrderMeaningElement != null ? OrderMeaningElement.Value : null; }
            set
            {
                if (value == null)
                    OrderMeaningElement = null;
                else
                    OrderMeaningElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("OrderMeaning");
            }
        }
        
        /// <summary>
        /// Value must be exactly this
        /// </summary>
        [FhirElement("fixed", InSummary=Hl7.Fhir.Model.Version.All, Order=290, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Canonical),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Url),typeof(Hl7.Fhir.Model.Uuid),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.R4.Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.R4.ContactPoint),typeof(Hl7.Fhir.Model.R4.Count),typeof(Hl7.Fhir.Model.R4.Distance),typeof(Hl7.Fhir.Model.R4.Duration),typeof(Hl7.Fhir.Model.R4.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.R4.Money),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.R4.SampledData),typeof(Hl7.Fhir.Model.R4.Signature),typeof(Hl7.Fhir.Model.R4.Timing),typeof(Hl7.Fhir.Model.R4.ContactDetail),typeof(Hl7.Fhir.Model.R4.Contributor),typeof(Hl7.Fhir.Model.R4.DataRequirement),typeof(Hl7.Fhir.Model.Expression),typeof(Hl7.Fhir.Model.R4.ParameterDefinition),typeof(Hl7.Fhir.Model.R4.RelatedArtifact),typeof(Hl7.Fhir.Model.R4.TriggerDefinition),typeof(Hl7.Fhir.Model.UsageContext),typeof(Hl7.Fhir.Model.R4.Dosage),typeof(Hl7.Fhir.Model.Meta))]
        [DataMember]
        public Hl7.Fhir.Model.Element Fixed
        {
            get { return _Fixed; }
            set { _Fixed = value; OnPropertyChanged("Fixed"); }
        }
        
        private Hl7.Fhir.Model.Element _Fixed;
        
        /// <summary>
        /// Value must have at least these property values
        /// </summary>
        [FhirElement("pattern", InSummary=Hl7.Fhir.Model.Version.All, Order=300, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Canonical),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Url),typeof(Hl7.Fhir.Model.Uuid),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.R4.Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.R4.ContactPoint),typeof(Hl7.Fhir.Model.R4.Count),typeof(Hl7.Fhir.Model.R4.Distance),typeof(Hl7.Fhir.Model.R4.Duration),typeof(Hl7.Fhir.Model.R4.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.R4.Money),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.R4.SampledData),typeof(Hl7.Fhir.Model.R4.Signature),typeof(Hl7.Fhir.Model.R4.Timing),typeof(Hl7.Fhir.Model.R4.ContactDetail),typeof(Hl7.Fhir.Model.R4.Contributor),typeof(Hl7.Fhir.Model.R4.DataRequirement),typeof(Hl7.Fhir.Model.Expression),typeof(Hl7.Fhir.Model.R4.ParameterDefinition),typeof(Hl7.Fhir.Model.R4.RelatedArtifact),typeof(Hl7.Fhir.Model.R4.TriggerDefinition),typeof(Hl7.Fhir.Model.UsageContext),typeof(Hl7.Fhir.Model.R4.Dosage),typeof(Hl7.Fhir.Model.Meta))]
        [DataMember]
        public Hl7.Fhir.Model.Element Pattern
        {
            get { return _Pattern; }
            set { _Pattern = value; OnPropertyChanged("Pattern"); }
        }
        
        private Hl7.Fhir.Model.Element _Pattern;
        
        /// <summary>
        /// Example value (as defined for type)
        /// </summary>
        [FhirElement("example", InSummary=Hl7.Fhir.Model.Version.All, Order=310)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ExampleComponent> Example
        {
            get { if(_Example==null) _Example = new List<ExampleComponent>(); return _Example; }
            set { _Example = value; OnPropertyChanged("Example"); }
        }
        
        private List<ExampleComponent> _Example;
        
        /// <summary>
        /// Minimum Allowed Value (for some types)
        /// </summary>
        [FhirElement("minValue", InSummary=Hl7.Fhir.Model.Version.All, Order=320, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.Quantity))]
        [DataMember]
        public Hl7.Fhir.Model.Element MinValue
        {
            get { return _MinValue; }
            set { _MinValue = value; OnPropertyChanged("MinValue"); }
        }
        
        private Hl7.Fhir.Model.Element _MinValue;
        
        /// <summary>
        /// Maximum Allowed Value (for some types)
        /// </summary>
        [FhirElement("maxValue", InSummary=Hl7.Fhir.Model.Version.All, Order=330, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.Quantity))]
        [DataMember]
        public Hl7.Fhir.Model.Element MaxValue
        {
            get { return _MaxValue; }
            set { _MaxValue = value; OnPropertyChanged("MaxValue"); }
        }
        
        private Hl7.Fhir.Model.Element _MaxValue;
        
        /// <summary>
        /// Max length for strings
        /// </summary>
        [FhirElement("maxLength", InSummary=Hl7.Fhir.Model.Version.All, Order=340)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Integer MaxLengthElement
        {
            get { return _MaxLengthElement; }
            set { _MaxLengthElement = value; OnPropertyChanged("MaxLengthElement"); }
        }
        
        private Hl7.Fhir.Model.Integer _MaxLengthElement;
        
        /// <summary>
        /// Max length for strings
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? MaxLength
        {
            get { return MaxLengthElement != null ? MaxLengthElement.Value : null; }
            set
            {
                if (value == null)
                    MaxLengthElement = null;
                else
                    MaxLengthElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("MaxLength");
            }
        }
        
        /// <summary>
        /// Reference to invariant about presence
        /// </summary>
        [FhirElement("condition", InSummary=Hl7.Fhir.Model.Version.All, Order=350)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Id> ConditionElement
        {
            get { if(_ConditionElement==null) _ConditionElement = new List<Hl7.Fhir.Model.Id>(); return _ConditionElement; }
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
                if (value == null)
                    ConditionElement = null;
                else
                    ConditionElement = new List<Hl7.Fhir.Model.Id>(value.Select(elem=>new Hl7.Fhir.Model.Id(elem)));
                OnPropertyChanged("Condition");
            }
        }
        
        /// <summary>
        /// Condition that must evaluate to true
        /// </summary>
        [FhirElement("constraint", InSummary=Hl7.Fhir.Model.Version.All, Order=360)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ConstraintComponent> Constraint
        {
            get { if(_Constraint==null) _Constraint = new List<ConstraintComponent>(); return _Constraint; }
            set { _Constraint = value; OnPropertyChanged("Constraint"); }
        }
        
        private List<ConstraintComponent> _Constraint;
        
        /// <summary>
        /// If the element must be supported
        /// </summary>
        [FhirElement("mustSupport", InSummary=Hl7.Fhir.Model.Version.All, Order=370)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean MustSupportElement
        {
            get { return _MustSupportElement; }
            set { _MustSupportElement = value; OnPropertyChanged("MustSupportElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _MustSupportElement;
        
        /// <summary>
        /// If the element must be supported
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? MustSupport
        {
            get { return MustSupportElement != null ? MustSupportElement.Value : null; }
            set
            {
                if (value == null)
                    MustSupportElement = null;
                else
                    MustSupportElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("MustSupport");
            }
        }
        
        /// <summary>
        /// If this modifies the meaning of other elements
        /// </summary>
        [FhirElement("isModifier", InSummary=Hl7.Fhir.Model.Version.All, Order=380)]
        [CLSCompliant(false)]
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
                if (value == null)
                    IsModifierElement = null;
                else
                    IsModifierElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("IsModifier");
            }
        }
        
        /// <summary>
        /// Reason that this element is marked as a modifier
        /// </summary>
        [FhirElement("isModifierReason", InSummary=Hl7.Fhir.Model.Version.All, Order=390)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString IsModifierReasonElement
        {
            get { return _IsModifierReasonElement; }
            set { _IsModifierReasonElement = value; OnPropertyChanged("IsModifierReasonElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _IsModifierReasonElement;
        
        /// <summary>
        /// Reason that this element is marked as a modifier
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string IsModifierReason
        {
            get { return IsModifierReasonElement != null ? IsModifierReasonElement.Value : null; }
            set
            {
                if (value == null)
                    IsModifierReasonElement = null;
                else
                    IsModifierReasonElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("IsModifierReason");
            }
        }
        
        /// <summary>
        /// Include when _summary = true?
        /// </summary>
        [FhirElement("isSummary", InSummary=Hl7.Fhir.Model.Version.All, Order=400)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean IsSummaryElement
        {
            get { return _IsSummaryElement; }
            set { _IsSummaryElement = value; OnPropertyChanged("IsSummaryElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _IsSummaryElement;
        
        /// <summary>
        /// Include when _summary = true?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? IsSummary
        {
            get { return IsSummaryElement != null ? IsSummaryElement.Value : null; }
            set
            {
                if (value == null)
                    IsSummaryElement = null;
                else
                    IsSummaryElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("IsSummary");
            }
        }
        
        /// <summary>
        /// ValueSet details if this is coded
        /// </summary>
        [FhirElement("binding", InSummary=Hl7.Fhir.Model.Version.All, Order=410)]
        [CLSCompliant(false)]
        [DataMember]
        public ElementDefinitionBindingComponent Binding
        {
            get { return _Binding; }
            set { _Binding = value; OnPropertyChanged("Binding"); }
        }
        
        private ElementDefinitionBindingComponent _Binding;
        
        /// <summary>
        /// Map element to another set of definitions
        /// </summary>
        [FhirElement("mapping", InSummary=Hl7.Fhir.Model.Version.All, Order=420)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<MappingComponent> Mapping
        {
            get { if(_Mapping==null) _Mapping = new List<MappingComponent>(); return _Mapping; }
            set { _Mapping = value; OnPropertyChanged("Mapping"); }
        }
        
        private List<MappingComponent> _Mapping;
    
    
        public static ElementDefinitionConstraint[] ElementDefinition_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-19",
                severity: ConstraintSeverity.Warning,
                expression: "path.matches('[^\\\\s\\\\.,:;\\\\\\'\"\\\\/|?!@#$%&*()\\\\[\\\\]{}]{1,64}(\\\\.[^\\\\s\\\\.,:;\\\\\\'\"\\\\/|?!@#$%&*()\\\\[\\\\]{}]{1,64}(\\\\[x\\\\])?(\\\\:[^\\\\s\\\\.]+)?)*')",
                human: "Element names cannot include some special characters",
                xpath: "matches(path/@value, '[^\\s\\.,:;\\'&quot;\\/|?!@#$%&amp;*()\\[\\]{}]{1,64}(\\.[^\\s\\.,:;\\'&quot;\\/|?!@#$%&amp;*()\\[\\]{}]{1,64}(\\[x\\])?(\\:[^\\s\\.]+)?)*')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-2",
                severity: ConstraintSeverity.Warning,
                expression: "min.empty() or max.empty() or (max = '*') or iif(max != '*', min <= max.toInteger())",
                human: "Min <= Max",
                xpath: "not(exists(f:min)) or not(exists(f:max)) or (not(f:max/@value) and not(f:min/@value)) or (f:max/@value = '*') or (number(f:max/@value) >= f:min/@value)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-5",
                severity: ConstraintSeverity.Warning,
                expression: "contentReference.empty() or (type.empty() and defaultValue.empty() and fixed.empty() and pattern.empty() and example.empty() and minValue.empty() and maxValue.empty() and maxLength.empty() and binding.empty())",
                human: "if the element definition has a contentReference, it cannot have type, defaultValue, fixed, pattern, example, minValue, maxValue, maxLength, or binding",
                xpath: "not(exists(f:contentReference) and (exists(f:type) or exists(f:*[starts-with(local-name(.), 'value')]) or exists(f:*[starts-with(local-name(.), 'defaultValue')])  or exists(f:*[starts-with(local-name(.), 'fixed')]) or exists(f:*[starts-with(local-name(.), 'pattern')]) or exists(f:*[starts-with(local-name(.), 'example')]) or exists(f:*[starts-with(local-name(.), 'f:minValue')]) or exists(f:*[starts-with(local-name(.), 'f:maxValue')]) or exists(f:maxLength) or exists(f:binding)))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-7",
                severity: ConstraintSeverity.Warning,
                expression: "pattern.empty() or (type.count() <= 1)",
                human: "Pattern may only be specified if there is one type",
                xpath: "not(exists(f:*[starts-with(local-name(.), 'pattern')])) or (count(f:type)<=1)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-6",
                severity: ConstraintSeverity.Warning,
                expression: "fixed.empty() or (type.count()  <= 1)",
                human: "Fixed value may only be specified if there is one type",
                xpath: "not(exists(f:*[starts-with(local-name(.), 'fixed')])) or (count(f:type)<=1)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-11",
                severity: ConstraintSeverity.Warning,
                expression: "binding.empty() or type.code.empty() or type.select((code = 'code') or (code = 'Coding') or (code='CodeableConcept') or (code = 'Quantity') or (code = 'string') or (code = 'uri')).exists()",
                human: "Binding can only be present for coded elements, string, and uri",
                xpath: "not(exists(f:binding)) or (count(f:type/f:code) = 0) or  f:type/f:code/@value=('code','Coding','CodeableConcept','Quantity','string', 'uri')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-22",
                severity: ConstraintSeverity.Warning,
                expression: "sliceIsConstraining.exists() implies sliceName.exists()",
                human: "sliceIsConstraining can only appear if slicename is present",
                xpath: "exists(f:sliceName) or not(exists(f:sliceIsConstraining))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-8",
                severity: ConstraintSeverity.Warning,
                expression: "pattern.empty() or fixed.empty()",
                human: "Pattern and fixed are mutually exclusive",
                xpath: "not(exists(f:*[starts-with(local-name(.), 'pattern')])) or not(exists(f:*[starts-with(local-name(.), 'fixed')]))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-14",
                severity: ConstraintSeverity.Warning,
                expression: "constraint.select(key).isDistinct()",
                human: "Constraints must be unique by key",
                xpath: "count(f:constraint) = count(distinct-values(f:constraint/f:key/@value))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-13",
                severity: ConstraintSeverity.Warning,
                expression: "type.select(code).isDistinct()",
                human: "Types must be unique by code",
                xpath: "not(exists(for $type in f:type return $type/preceding-sibling::f:type[f:code/@value=$type/f:code/@value]))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-16",
                severity: ConstraintSeverity.Warning,
                expression: "sliceName.empty() or sliceName.matches('^[a-zA-Z0-9\\\\/\\\\-_\\\\[\\\\]\\\\@]+$')",
                human: "sliceName must be composed of proper tokens separated by \"/\"",
                xpath: "not(exists(f:sliceName/@value)) or matches(f:sliceName/@value, '^[a-zA-Z0-9\\/\\-_\\[\\]\\@]+$')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-15",
                severity: ConstraintSeverity.Warning,
                expression: "defaultValue.empty() or meaningWhenMissing.empty()",
                human: "default value and meaningWhenMissing are mutually exclusive",
                xpath: "not(exists(f:*[starts-with(local-name(.), 'fixed')])) or not(exists(f:meaningWhenMissing))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-18",
                severity: ConstraintSeverity.Warning,
                expression: "(isModifier.exists() and isModifier) implies isModifierReason.exists()",
                human: "Must have a modifier reason if isModifier = true",
                xpath: "not(f:isModifier/@value = 'true') or exists(f:isModifierReason)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-20",
                severity: ConstraintSeverity.Warning,
                expression: "path.matches('[A-Za-z][A-Za-z0-9]*(\\\\.[a-z][A-Za-z0-9]*(\\\\[x])?)*')",
                human: "Element names should be simple alphanumerics with a max of 64 characters, or code generation tools may be broken",
                xpath: "matches(path/@value, '[A-Za-z][A-Za-z0-9]*(\\.[a-z][A-Za-z0-9]*(\\[x])?)*')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-1",
                severity: ConstraintSeverity.Warning,
                expression: "slicing.all(discriminator.exists() or description.exists())",
                human: "If there are no discriminators, there must be a definition",
                xpath: "(f:discriminator) or (f:description)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-3",
                severity: ConstraintSeverity.Warning,
                expression: "max.all(empty() or ($this = '*') or (toInteger() >= 0))",
                human: "Max SHALL be a number or \"*\"",
                xpath: "@value='*' or (normalize-space(@value)!='' and normalize-space(translate(@value, '0123456789',''))='')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-4",
                severity: ConstraintSeverity.Warning,
                expression: "type.all(aggregation.empty() or (code = 'Reference') or (code = 'canonical'))",
                human: "Aggregation may only be specified if one of the allowed types for the element is a reference",
                xpath: "not(exists(f:aggregation)) or exists(f:code[@value = 'Reference']) or exists(f:code[@value = 'canonical'])"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-17",
                severity: ConstraintSeverity.Warning,
                expression: "type.all((code='Reference' or code = 'canonical') or targetProfile.empty())",
                human: "targetProfile is only allowed if the type is Reference or canonical",
                xpath: "not(exists(f:targetProfile)) or (f:code/@value = 'Reference')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-21",
                severity: ConstraintSeverity.Warning,
                expression: "constraint.all(expression.exists())",
                human: "Constraints should have an expression or else validators will not be able to enforce them",
                xpath: "exists(f:expression/@value)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "eld-12",
                severity: ConstraintSeverity.Warning,
                expression: "binding.all(valueSet.exists() implies (valueSet.startsWith('http:') or valueSet.startsWith('https') or valueSet.startsWith('urn:')))",
                human: "ValueSet SHALL start with http:// or https:// or urn:",
                xpath: "(starts-with(string(f:valueSet/@value), 'http:') or starts-with(string(f:valueSet/@value), 'https:') or starts-with(string(f:valueSet/@value), 'urn:'))"
            ),
        };
    
        // TODO: Add code to enforce the above constraints
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ElementDefinition;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                if(RepresentationElement != null) dest.RepresentationElement = new List<Code<Hl7.Fhir.Model.R4.PropertyRepresentation>>(RepresentationElement.DeepCopy());
                if(SliceNameElement != null) dest.SliceNameElement = (Hl7.Fhir.Model.FhirString)SliceNameElement.DeepCopy();
                if(SliceIsConstrainingElement != null) dest.SliceIsConstrainingElement = (Hl7.Fhir.Model.FhirBoolean)SliceIsConstrainingElement.DeepCopy();
                if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                if(Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                if(Slicing != null) dest.Slicing = (SlicingComponent)Slicing.DeepCopy();
                if(ShortElement != null) dest.ShortElement = (Hl7.Fhir.Model.FhirString)ShortElement.DeepCopy();
                if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.Markdown)DefinitionElement.DeepCopy();
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.Markdown)CommentElement.DeepCopy();
                if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.Markdown)RequirementsElement.DeepCopy();
                if(AliasElement != null) dest.AliasElement = new List<Hl7.Fhir.Model.FhirString>(AliasElement.DeepCopy());
                if(MinElement != null) dest.MinElement = (Hl7.Fhir.Model.UnsignedInt)MinElement.DeepCopy();
                if(MaxElement != null) dest.MaxElement = (Hl7.Fhir.Model.FhirString)MaxElement.DeepCopy();
                if(Base != null) dest.Base = (BaseComponent)Base.DeepCopy();
                if(ContentReferenceElement != null) dest.ContentReferenceElement = (Hl7.Fhir.Model.FhirUri)ContentReferenceElement.DeepCopy();
                if(Type != null) dest.Type = new List<TypeRefComponent>(Type.DeepCopy());
                if(DefaultValue != null) dest.DefaultValue = (Hl7.Fhir.Model.Element)DefaultValue.DeepCopy();
                if(MeaningWhenMissingElement != null) dest.MeaningWhenMissingElement = (Hl7.Fhir.Model.Markdown)MeaningWhenMissingElement.DeepCopy();
                if(OrderMeaningElement != null) dest.OrderMeaningElement = (Hl7.Fhir.Model.FhirString)OrderMeaningElement.DeepCopy();
                if(Fixed != null) dest.Fixed = (Hl7.Fhir.Model.Element)Fixed.DeepCopy();
                if(Pattern != null) dest.Pattern = (Hl7.Fhir.Model.Element)Pattern.DeepCopy();
                if(Example != null) dest.Example = new List<ExampleComponent>(Example.DeepCopy());
                if(MinValue != null) dest.MinValue = (Hl7.Fhir.Model.Element)MinValue.DeepCopy();
                if(MaxValue != null) dest.MaxValue = (Hl7.Fhir.Model.Element)MaxValue.DeepCopy();
                if(MaxLengthElement != null) dest.MaxLengthElement = (Hl7.Fhir.Model.Integer)MaxLengthElement.DeepCopy();
                if(ConditionElement != null) dest.ConditionElement = new List<Hl7.Fhir.Model.Id>(ConditionElement.DeepCopy());
                if(Constraint != null) dest.Constraint = new List<ConstraintComponent>(Constraint.DeepCopy());
                if(MustSupportElement != null) dest.MustSupportElement = (Hl7.Fhir.Model.FhirBoolean)MustSupportElement.DeepCopy();
                if(IsModifierElement != null) dest.IsModifierElement = (Hl7.Fhir.Model.FhirBoolean)IsModifierElement.DeepCopy();
                if(IsModifierReasonElement != null) dest.IsModifierReasonElement = (Hl7.Fhir.Model.FhirString)IsModifierReasonElement.DeepCopy();
                if(IsSummaryElement != null) dest.IsSummaryElement = (Hl7.Fhir.Model.FhirBoolean)IsSummaryElement.DeepCopy();
                if(Binding != null) dest.Binding = (ElementDefinitionBindingComponent)Binding.DeepCopy();
                if(Mapping != null) dest.Mapping = new List<MappingComponent>(Mapping.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ElementDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ElementDefinition;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
            if( !DeepComparable.Matches(RepresentationElement, otherT.RepresentationElement)) return false;
            if( !DeepComparable.Matches(SliceNameElement, otherT.SliceNameElement)) return false;
            if( !DeepComparable.Matches(SliceIsConstrainingElement, otherT.SliceIsConstrainingElement)) return false;
            if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Slicing, otherT.Slicing)) return false;
            if( !DeepComparable.Matches(ShortElement, otherT.ShortElement)) return false;
            if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.Matches(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.Matches(MinElement, otherT.MinElement)) return false;
            if( !DeepComparable.Matches(MaxElement, otherT.MaxElement)) return false;
            if( !DeepComparable.Matches(Base, otherT.Base)) return false;
            if( !DeepComparable.Matches(ContentReferenceElement, otherT.ContentReferenceElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(DefaultValue, otherT.DefaultValue)) return false;
            if( !DeepComparable.Matches(MeaningWhenMissingElement, otherT.MeaningWhenMissingElement)) return false;
            if( !DeepComparable.Matches(OrderMeaningElement, otherT.OrderMeaningElement)) return false;
            if( !DeepComparable.Matches(Fixed, otherT.Fixed)) return false;
            if( !DeepComparable.Matches(Pattern, otherT.Pattern)) return false;
            if( !DeepComparable.Matches(Example, otherT.Example)) return false;
            if( !DeepComparable.Matches(MinValue, otherT.MinValue)) return false;
            if( !DeepComparable.Matches(MaxValue, otherT.MaxValue)) return false;
            if( !DeepComparable.Matches(MaxLengthElement, otherT.MaxLengthElement)) return false;
            if( !DeepComparable.Matches(ConditionElement, otherT.ConditionElement)) return false;
            if( !DeepComparable.Matches(Constraint, otherT.Constraint)) return false;
            if( !DeepComparable.Matches(MustSupportElement, otherT.MustSupportElement)) return false;
            if( !DeepComparable.Matches(IsModifierElement, otherT.IsModifierElement)) return false;
            if( !DeepComparable.Matches(IsModifierReasonElement, otherT.IsModifierReasonElement)) return false;
            if( !DeepComparable.Matches(IsSummaryElement, otherT.IsSummaryElement)) return false;
            if( !DeepComparable.Matches(Binding, otherT.Binding)) return false;
            if( !DeepComparable.Matches(Mapping, otherT.Mapping)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ElementDefinition;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
            if( !DeepComparable.IsExactly(RepresentationElement, otherT.RepresentationElement)) return false;
            if( !DeepComparable.IsExactly(SliceNameElement, otherT.SliceNameElement)) return false;
            if( !DeepComparable.IsExactly(SliceIsConstrainingElement, otherT.SliceIsConstrainingElement)) return false;
            if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Slicing, otherT.Slicing)) return false;
            if( !DeepComparable.IsExactly(ShortElement, otherT.ShortElement)) return false;
            if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.IsExactly(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.IsExactly(MinElement, otherT.MinElement)) return false;
            if( !DeepComparable.IsExactly(MaxElement, otherT.MaxElement)) return false;
            if( !DeepComparable.IsExactly(Base, otherT.Base)) return false;
            if( !DeepComparable.IsExactly(ContentReferenceElement, otherT.ContentReferenceElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(DefaultValue, otherT.DefaultValue)) return false;
            if( !DeepComparable.IsExactly(MeaningWhenMissingElement, otherT.MeaningWhenMissingElement)) return false;
            if( !DeepComparable.IsExactly(OrderMeaningElement, otherT.OrderMeaningElement)) return false;
            if( !DeepComparable.IsExactly(Fixed, otherT.Fixed)) return false;
            if( !DeepComparable.IsExactly(Pattern, otherT.Pattern)) return false;
            if( !DeepComparable.IsExactly(Example, otherT.Example)) return false;
            if( !DeepComparable.IsExactly(MinValue, otherT.MinValue)) return false;
            if( !DeepComparable.IsExactly(MaxValue, otherT.MaxValue)) return false;
            if( !DeepComparable.IsExactly(MaxLengthElement, otherT.MaxLengthElement)) return false;
            if( !DeepComparable.IsExactly(ConditionElement, otherT.ConditionElement)) return false;
            if( !DeepComparable.IsExactly(Constraint, otherT.Constraint)) return false;
            if( !DeepComparable.IsExactly(MustSupportElement, otherT.MustSupportElement)) return false;
            if( !DeepComparable.IsExactly(IsModifierElement, otherT.IsModifierElement)) return false;
            if( !DeepComparable.IsExactly(IsModifierReasonElement, otherT.IsModifierReasonElement)) return false;
            if( !DeepComparable.IsExactly(IsSummaryElement, otherT.IsSummaryElement)) return false;
            if( !DeepComparable.IsExactly(Binding, otherT.Binding)) return false;
            if( !DeepComparable.IsExactly(Mapping, otherT.Mapping)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("ElementDefinition");
            base.Serialize(sink);
            sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); PathElement?.Serialize(sink);
            sink.BeginList("representation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(RepresentationElement);
            sink.End();
            sink.Element("sliceName", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SliceNameElement?.Serialize(sink);
            sink.Element("sliceIsConstraining", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SliceIsConstrainingElement?.Serialize(sink);
            sink.Element("label", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LabelElement?.Serialize(sink);
            sink.BeginList("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Code)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("slicing", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Slicing?.Serialize(sink);
            sink.Element("short", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ShortElement?.Serialize(sink);
            sink.Element("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DefinitionElement?.Serialize(sink);
            sink.Element("comment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CommentElement?.Serialize(sink);
            sink.Element("requirements", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RequirementsElement?.Serialize(sink);
            sink.BeginList("alias", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(AliasElement);
            sink.End();
            sink.Element("min", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MinElement?.Serialize(sink);
            sink.Element("max", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MaxElement?.Serialize(sink);
            sink.Element("base", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Base?.Serialize(sink);
            sink.Element("contentReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ContentReferenceElement?.Serialize(sink);
            sink.BeginList("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Type)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("defaultValue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); DefaultValue?.Serialize(sink);
            sink.Element("meaningWhenMissing", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MeaningWhenMissingElement?.Serialize(sink);
            sink.Element("orderMeaning", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OrderMeaningElement?.Serialize(sink);
            sink.Element("fixed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Fixed?.Serialize(sink);
            sink.Element("pattern", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Pattern?.Serialize(sink);
            sink.BeginList("example", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Example)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("minValue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); MinValue?.Serialize(sink);
            sink.Element("maxValue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); MaxValue?.Serialize(sink);
            sink.Element("maxLength", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MaxLengthElement?.Serialize(sink);
            sink.BeginList("condition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(ConditionElement);
            sink.End();
            sink.BeginList("constraint", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Constraint)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("mustSupport", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MustSupportElement?.Serialize(sink);
            sink.Element("isModifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IsModifierElement?.Serialize(sink);
            sink.Element("isModifierReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IsModifierReasonElement?.Serialize(sink);
            sink.Element("isSummary", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IsSummaryElement?.Serialize(sink);
            sink.Element("binding", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Binding?.Serialize(sink);
            sink.BeginList("mapping", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Mapping)
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
                if (PathElement != null) yield return PathElement;
                foreach (var elem in RepresentationElement) { if (elem != null) yield return elem; }
                if (SliceNameElement != null) yield return SliceNameElement;
                if (SliceIsConstrainingElement != null) yield return SliceIsConstrainingElement;
                if (LabelElement != null) yield return LabelElement;
                foreach (var elem in Code) { if (elem != null) yield return elem; }
                if (Slicing != null) yield return Slicing;
                if (ShortElement != null) yield return ShortElement;
                if (DefinitionElement != null) yield return DefinitionElement;
                if (CommentElement != null) yield return CommentElement;
                if (RequirementsElement != null) yield return RequirementsElement;
                foreach (var elem in AliasElement) { if (elem != null) yield return elem; }
                if (MinElement != null) yield return MinElement;
                if (MaxElement != null) yield return MaxElement;
                if (Base != null) yield return Base;
                if (ContentReferenceElement != null) yield return ContentReferenceElement;
                foreach (var elem in Type) { if (elem != null) yield return elem; }
                if (DefaultValue != null) yield return DefaultValue;
                if (MeaningWhenMissingElement != null) yield return MeaningWhenMissingElement;
                if (OrderMeaningElement != null) yield return OrderMeaningElement;
                if (Fixed != null) yield return Fixed;
                if (Pattern != null) yield return Pattern;
                foreach (var elem in Example) { if (elem != null) yield return elem; }
                if (MinValue != null) yield return MinValue;
                if (MaxValue != null) yield return MaxValue;
                if (MaxLengthElement != null) yield return MaxLengthElement;
                foreach (var elem in ConditionElement) { if (elem != null) yield return elem; }
                foreach (var elem in Constraint) { if (elem != null) yield return elem; }
                if (MustSupportElement != null) yield return MustSupportElement;
                if (IsModifierElement != null) yield return IsModifierElement;
                if (IsModifierReasonElement != null) yield return IsModifierReasonElement;
                if (IsSummaryElement != null) yield return IsSummaryElement;
                if (Binding != null) yield return Binding;
                foreach (var elem in Mapping) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (PathElement != null) yield return new ElementValue("path", PathElement);
                foreach (var elem in RepresentationElement) { if (elem != null) yield return new ElementValue("representation", elem); }
                if (SliceNameElement != null) yield return new ElementValue("sliceName", SliceNameElement);
                if (SliceIsConstrainingElement != null) yield return new ElementValue("sliceIsConstraining", SliceIsConstrainingElement);
                if (LabelElement != null) yield return new ElementValue("label", LabelElement);
                foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                if (Slicing != null) yield return new ElementValue("slicing", Slicing);
                if (ShortElement != null) yield return new ElementValue("short", ShortElement);
                if (DefinitionElement != null) yield return new ElementValue("definition", DefinitionElement);
                if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                if (RequirementsElement != null) yield return new ElementValue("requirements", RequirementsElement);
                foreach (var elem in AliasElement) { if (elem != null) yield return new ElementValue("alias", elem); }
                if (MinElement != null) yield return new ElementValue("min", MinElement);
                if (MaxElement != null) yield return new ElementValue("max", MaxElement);
                if (Base != null) yield return new ElementValue("base", Base);
                if (ContentReferenceElement != null) yield return new ElementValue("contentReference", ContentReferenceElement);
                foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                if (DefaultValue != null) yield return new ElementValue("defaultValue", DefaultValue);
                if (MeaningWhenMissingElement != null) yield return new ElementValue("meaningWhenMissing", MeaningWhenMissingElement);
                if (OrderMeaningElement != null) yield return new ElementValue("orderMeaning", OrderMeaningElement);
                if (Fixed != null) yield return new ElementValue("fixed", Fixed);
                if (Pattern != null) yield return new ElementValue("pattern", Pattern);
                foreach (var elem in Example) { if (elem != null) yield return new ElementValue("example", elem); }
                if (MinValue != null) yield return new ElementValue("minValue", MinValue);
                if (MaxValue != null) yield return new ElementValue("maxValue", MaxValue);
                if (MaxLengthElement != null) yield return new ElementValue("maxLength", MaxLengthElement);
                foreach (var elem in ConditionElement) { if (elem != null) yield return new ElementValue("condition", elem); }
                foreach (var elem in Constraint) { if (elem != null) yield return new ElementValue("constraint", elem); }
                if (MustSupportElement != null) yield return new ElementValue("mustSupport", MustSupportElement);
                if (IsModifierElement != null) yield return new ElementValue("isModifier", IsModifierElement);
                if (IsModifierReasonElement != null) yield return new ElementValue("isModifierReason", IsModifierReasonElement);
                if (IsSummaryElement != null) yield return new ElementValue("isSummary", IsSummaryElement);
                if (Binding != null) yield return new ElementValue("binding", Binding);
                foreach (var elem in Mapping) { if (elem != null) yield return new ElementValue("mapping", elem); }
            }
        }
    
    }

}
