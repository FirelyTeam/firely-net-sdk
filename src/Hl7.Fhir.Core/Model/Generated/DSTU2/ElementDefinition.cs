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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model.DSTU2
{
    /// <summary>
    /// Definition of an element in a resource or extension
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ElementDefinition")]
    [DataContract]
    public partial class ElementDefinition : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IElementDefinition, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "ElementDefinition"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "SlicingComponent")]
        [DataContract]
        public partial class SlicingComponent : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IElementDefinitionSlicingComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SlicingComponent"; } }
            
            /// <summary>
            /// Element values that used to distinguish the slices
            /// </summary>
            [FhirElement("discriminator", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> DiscriminatorElement
            {
                get { if(_DiscriminatorElement==null) _DiscriminatorElement = new List<Hl7.Fhir.Model.FhirString>(); return _DiscriminatorElement; }
                set { _DiscriminatorElement = value; OnPropertyChanged("DiscriminatorElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _DiscriminatorElement;
            
            /// <summary>
            /// Element values that used to distinguish the slices
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Discriminator
            {
                get { return DiscriminatorElement != null ? DiscriminatorElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        DiscriminatorElement = null;
                    else
                        DiscriminatorElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Discriminator");
                }
            }
            
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
                sink.Serialize(DiscriminatorElement);
                sink.End();
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("ordered", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OrderedElement?.Serialize(sink);
                sink.Element("rules", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); RulesElement?.Serialize(sink);
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
                    case "discriminator":
                        DiscriminatorElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "ordered":
                        OrderedElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "rules":
                        RulesElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.SlicingRules>>();
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
                    case "discriminator":
                    case "_discriminator":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "ordered":
                        OrderedElement = source.PopulateValue(OrderedElement);
                        return true;
                    case "_ordered":
                        OrderedElement = source.Populate(OrderedElement);
                        return true;
                    case "rules":
                        RulesElement = source.PopulateValue(RulesElement);
                        return true;
                    case "_rules":
                        RulesElement = source.Populate(RulesElement);
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
                    case "discriminator":
                        source.PopulatePrimitiveListItemValue(DiscriminatorElement, index);
                        return true;
                    case "_discriminator":
                        source.PopulatePrimitiveListItem(DiscriminatorElement, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SlicingComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DiscriminatorElement != null) dest.DiscriminatorElement = new List<Hl7.Fhir.Model.FhirString>(DiscriminatorElement.DeepCopy());
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
                if( !DeepComparable.Matches(DiscriminatorElement, otherT.DiscriminatorElement)) return false;
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
                if( !DeepComparable.IsExactly(DiscriminatorElement, otherT.DiscriminatorElement)) return false;
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
                    foreach (var elem in DiscriminatorElement) { if (elem != null) yield return elem; }
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
                    foreach (var elem in DiscriminatorElement) { if (elem != null) yield return new ElementValue("discriminator", elem); }
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (OrderedElement != null) yield return new ElementValue("ordered", OrderedElement);
                    if (RulesElement != null) yield return new ElementValue("rules", RulesElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "BaseComponent")]
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
            public Hl7.Fhir.Model.Integer MinElement
            {
                get { return _MinElement; }
                set { _MinElement = value; OnPropertyChanged("MinElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _MinElement;
            
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
                        MinElement = new Hl7.Fhir.Model.Integer(value);
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
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "path":
                        PathElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "min":
                        MinElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "max":
                        MaxElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "path":
                        PathElement = source.PopulateValue(PathElement);
                        return true;
                    case "_path":
                        PathElement = source.Populate(PathElement);
                        return true;
                    case "min":
                        MinElement = source.PopulateValue(MinElement);
                        return true;
                    case "_min":
                        MinElement = source.Populate(MinElement);
                        return true;
                    case "max":
                        MaxElement = source.PopulateValue(MaxElement);
                        return true;
                    case "_max":
                        MaxElement = source.Populate(MaxElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BaseComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(MinElement != null) dest.MinElement = (Hl7.Fhir.Model.Integer)MinElement.DeepCopy();
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "TypeRefComponent")]
        [DataContract]
        public partial class TypeRefComponent : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IElementDefinitionTypeRefComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TypeRefComponent"; } }
            
            /// <summary>
            /// Name of Data type or Resource
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType> _CodeElement;
            
            /// <summary>
            /// Name of Data type or Resource
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.FHIRDefinedType? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Profile (StructureDefinition) to apply (or IG)
            /// </summary>
            [FhirElement("profile", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> ProfileElement
            {
                get { if(_ProfileElement==null) _ProfileElement = new List<Hl7.Fhir.Model.FhirUri>(); return _ProfileElement; }
                set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirUri> _ProfileElement;
            
            /// <summary>
            /// Profile (StructureDefinition) to apply (or IG)
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
                        ProfileElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Profile");
                }
            }
            
            /// <summary>
            /// contained | referenced | bundled - how aggregated
            /// </summary>
            [FhirElement("aggregation", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TypeRefComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); CodeElement?.Serialize(sink);
                sink.BeginList("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(ProfileElement);
                sink.End();
                sink.BeginList("aggregation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(AggregationElement);
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
                    case "code":
                        CodeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType>>();
                        return true;
                    case "profile":
                        ProfileElement = source.GetList<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "aggregation":
                        AggregationElement = source.GetList<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AggregationMode>>();
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
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
                        return true;
                    case "profile":
                    case "_profile":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "aggregation":
                    case "_aggregation":
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
                    case "profile":
                        source.PopulatePrimitiveListItemValue(ProfileElement, index);
                        return true;
                    case "_profile":
                        source.PopulatePrimitiveListItem(ProfileElement, index);
                        return true;
                    case "aggregation":
                        source.PopulatePrimitiveListItemValue(AggregationElement, index);
                        return true;
                    case "_aggregation":
                        source.PopulatePrimitiveListItem(AggregationElement, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TypeRefComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType>)CodeElement.DeepCopy();
                    if(ProfileElement != null) dest.ProfileElement = new List<Hl7.Fhir.Model.FhirUri>(ProfileElement.DeepCopy());
                    if(AggregationElement != null) dest.AggregationElement = new List<Code<Hl7.Fhir.Model.AggregationMode>>(AggregationElement.DeepCopy());
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
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    foreach (var elem in ProfileElement) { if (elem != null) yield return elem; }
                    foreach (var elem in AggregationElement) { if (elem != null) yield return elem; }
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
                    foreach (var elem in AggregationElement) { if (elem != null) yield return new ElementValue("aggregation", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ConstraintComponent")]
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
            /// Why this constraint necessary or appropriate
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
            /// Why this constraint necessary or appropriate
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
            /// XPath expression of constraint
            /// </summary>
            [FhirElement("xpath", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
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
                    if (value == null)
                        XpathElement = null;
                    else
                        XpathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Xpath");
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
                sink.Element("xpath", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); XpathElement?.Serialize(sink);
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
                    case "key":
                        KeyElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "requirements":
                        RequirementsElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "severity":
                        SeverityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ConstraintSeverity>>();
                        return true;
                    case "human":
                        HumanElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "xpath":
                        XpathElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "key":
                        KeyElement = source.PopulateValue(KeyElement);
                        return true;
                    case "_key":
                        KeyElement = source.Populate(KeyElement);
                        return true;
                    case "requirements":
                        RequirementsElement = source.PopulateValue(RequirementsElement);
                        return true;
                    case "_requirements":
                        RequirementsElement = source.Populate(RequirementsElement);
                        return true;
                    case "severity":
                        SeverityElement = source.PopulateValue(SeverityElement);
                        return true;
                    case "_severity":
                        SeverityElement = source.Populate(SeverityElement);
                        return true;
                    case "human":
                        HumanElement = source.PopulateValue(HumanElement);
                        return true;
                    case "_human":
                        HumanElement = source.Populate(HumanElement);
                        return true;
                    case "xpath":
                        XpathElement = source.PopulateValue(XpathElement);
                        return true;
                    case "_xpath":
                        XpathElement = source.Populate(XpathElement);
                        return true;
                }
                return false;
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
                    if(XpathElement != null) dest.XpathElement = (Hl7.Fhir.Model.FhirString)XpathElement.DeepCopy();
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
                if( !DeepComparable.Matches(XpathElement, otherT.XpathElement)) return false;
            
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
                if( !DeepComparable.IsExactly(XpathElement, otherT.XpathElement)) return false;
            
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
                    if (XpathElement != null) yield return XpathElement;
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
                    if (XpathElement != null) yield return new ElementValue("xpath", XpathElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "BindingComponent")]
        [DataContract]
        public partial class BindingComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "BindingComponent"; } }
            
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
            [FhirElement("valueSet", InSummary=Hl7.Fhir.Model.Version.All, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element ValueSet
            {
                get { return _ValueSet; }
                set { _ValueSet = value; OnPropertyChanged("ValueSet"); }
            }
            
            private Hl7.Fhir.Model.Element _ValueSet;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("BindingComponent");
                base.Serialize(sink);
                sink.Element("strength", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StrengthElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("valueSet", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); ValueSet?.Serialize(sink);
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
                    case "strength":
                        StrengthElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.BindingStrength>>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "valueSetUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(ValueSet, "valueSet");
                        ValueSet = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "valueSetReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(ValueSet, "valueSet");
                        ValueSet = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "strength":
                        StrengthElement = source.PopulateValue(StrengthElement);
                        return true;
                    case "_strength":
                        StrengthElement = source.Populate(StrengthElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "valueSetUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(ValueSet, "valueSet");
                        ValueSet = source.PopulateValue(ValueSet as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "_valueSetUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(ValueSet, "valueSet");
                        ValueSet = source.Populate(ValueSet as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "valueSetReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(ValueSet, "valueSet");
                        ValueSet = source.Populate(ValueSet as Hl7.Fhir.Model.ResourceReference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BindingComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StrengthElement != null) dest.StrengthElement = (Code<Hl7.Fhir.Model.BindingStrength>)StrengthElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(ValueSet != null) dest.ValueSet = (Hl7.Fhir.Model.Element)ValueSet.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new BindingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BindingComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(StrengthElement, otherT.StrengthElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(ValueSet, otherT.ValueSet)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BindingComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StrengthElement, otherT.StrengthElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(ValueSet, otherT.ValueSet)) return false;
            
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
                    if (ValueSet != null) yield return ValueSet;
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
                    if (ValueSet != null) yield return new ElementValue("valueSet", ValueSet);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "MappingComponent")]
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("MappingComponent");
                base.Serialize(sink);
                sink.Element("identity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); IdentityElement?.Serialize(sink);
                sink.Element("language", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LanguageElement?.Serialize(sink);
                sink.Element("map", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); MapElement?.Serialize(sink);
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
                    case "identity":
                        IdentityElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "language":
                        LanguageElement = source.Get<Hl7.Fhir.Model.Code>();
                        return true;
                    case "map":
                        MapElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "identity":
                        IdentityElement = source.PopulateValue(IdentityElement);
                        return true;
                    case "_identity":
                        IdentityElement = source.Populate(IdentityElement);
                        return true;
                    case "language":
                        LanguageElement = source.PopulateValue(LanguageElement);
                        return true;
                    case "_language":
                        LanguageElement = source.Populate(LanguageElement);
                        return true;
                    case "map":
                        MapElement = source.PopulateValue(MapElement);
                        return true;
                    case "_map":
                        MapElement = source.Populate(MapElement);
                        return true;
                }
                return false;
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
        /// The path of the element (see the Detailed Descriptions)
        /// </summary>
        [FhirElement("path", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
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
        /// The path of the element (see the Detailed Descriptions)
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
        /// How this element is represented in instances
        /// </summary>
        [FhirElement("representation", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.DSTU2.PropertyRepresentation>> RepresentationElement
        {
            get { if(_RepresentationElement==null) _RepresentationElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.PropertyRepresentation>>(); return _RepresentationElement; }
            set { _RepresentationElement = value; OnPropertyChanged("RepresentationElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.DSTU2.PropertyRepresentation>> _RepresentationElement;
        
        /// <summary>
        /// How this element is represented in instances
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.DSTU2.PropertyRepresentation?> Representation
        {
            get { return RepresentationElement != null ? RepresentationElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    RepresentationElement = null;
                else
                    RepresentationElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.PropertyRepresentation>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.PropertyRepresentation>(elem)));
                OnPropertyChanged("Representation");
            }
        }
        
        /// <summary>
        /// Name for this particular element definition (reference target)
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
        [CLSCompliant(false)]
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
                if (value == null)
                    NameElement = null;
                else
                    NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Name for element to display with or prompt for element
        /// </summary>
        [FhirElement("label", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
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
        /// Defining code
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
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
        [FhirElement("slicing", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
        [CLSCompliant(false)]
        [DataMember]
        public SlicingComponent Slicing
        {
            get { return _Slicing; }
            set { _Slicing = value; OnPropertyChanged("Slicing"); }
        }
        
        private SlicingComponent _Slicing;
        
        /// <summary>
        /// Concise definition for xml presentation
        /// </summary>
        [FhirElement("short", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
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
        [FhirElement("definition", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
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
        [FhirElement("comments", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown CommentsElement
        {
            get { return _CommentsElement; }
            set { _CommentsElement = value; OnPropertyChanged("CommentsElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _CommentsElement;
        
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
                if (value == null)
                    CommentsElement = null;
                else
                    CommentsElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Comments");
            }
        }
        
        /// <summary>
        /// Why is this needed?
        /// </summary>
        [FhirElement("requirements", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown RequirementsElement
        {
            get { return _RequirementsElement; }
            set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _RequirementsElement;
        
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
        [FhirElement("alias", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
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
        [FhirElement("min", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
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
                if (value == null)
                    MinElement = null;
                else
                    MinElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("Min");
            }
        }
        
        /// <summary>
        /// Maximum Cardinality (a number or *)
        /// </summary>
        [FhirElement("max", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
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
        [FhirElement("base", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public BaseComponent Base
        {
            get { return _Base; }
            set { _Base = value; OnPropertyChanged("Base"); }
        }
        
        private BaseComponent _Base;
        
        /// <summary>
        /// Data type and Profile for this element
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
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
        /// To another element constraint (by element.name)
        /// </summary>
        [FhirElement("nameReference", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
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
                if (value == null)
                    NameReferenceElement = null;
                else
                    NameReferenceElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("NameReference");
            }
        }
        
        /// <summary>
        /// Specified value it missing from instance
        /// </summary>
        [FhirElement("defaultValue", InSummary=Hl7.Fhir.Model.Version.All, Order=190, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.DSTU2.SampledData),typeof(Hl7.Fhir.Model.DSTU2.Signature),typeof(Hl7.Fhir.Model.DSTU2.HumanName),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.DSTU2.ContactPoint),typeof(Hl7.Fhir.Model.DSTU2.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Meta))]
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
        [FhirElement("meaningWhenMissing", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
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
        /// Value must be exactly this
        /// </summary>
        [FhirElement("fixed", InSummary=Hl7.Fhir.Model.Version.All, Order=210, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.DSTU2.SampledData),typeof(Hl7.Fhir.Model.DSTU2.Signature),typeof(Hl7.Fhir.Model.DSTU2.HumanName),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.DSTU2.ContactPoint),typeof(Hl7.Fhir.Model.DSTU2.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Meta))]
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
        [FhirElement("pattern", InSummary=Hl7.Fhir.Model.Version.All, Order=220, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.DSTU2.SampledData),typeof(Hl7.Fhir.Model.DSTU2.Signature),typeof(Hl7.Fhir.Model.DSTU2.HumanName),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.DSTU2.ContactPoint),typeof(Hl7.Fhir.Model.DSTU2.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Meta))]
        [DataMember]
        public Hl7.Fhir.Model.Element Pattern
        {
            get { return _Pattern; }
            set { _Pattern = value; OnPropertyChanged("Pattern"); }
        }
        
        private Hl7.Fhir.Model.Element _Pattern;
        
        /// <summary>
        /// Example value: [as defined for type]
        /// </summary>
        [FhirElement("example", InSummary=Hl7.Fhir.Model.Version.All, Order=230, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.DSTU2.SampledData),typeof(Hl7.Fhir.Model.DSTU2.Signature),typeof(Hl7.Fhir.Model.DSTU2.HumanName),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.DSTU2.ContactPoint),typeof(Hl7.Fhir.Model.DSTU2.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Meta))]
        [DataMember]
        public Hl7.Fhir.Model.Element Example
        {
            get { return _Example; }
            set { _Example = value; OnPropertyChanged("Example"); }
        }
        
        private Hl7.Fhir.Model.Element _Example;
        
        /// <summary>
        /// Minimum Allowed Value (for some types)
        /// </summary>
        [FhirElement("minValue", InSummary=Hl7.Fhir.Model.Version.All, Order=240, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.DSTU2.SampledData),typeof(Hl7.Fhir.Model.DSTU2.Signature),typeof(Hl7.Fhir.Model.DSTU2.HumanName),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.DSTU2.ContactPoint),typeof(Hl7.Fhir.Model.DSTU2.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Meta))]
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
        [FhirElement("maxValue", InSummary=Hl7.Fhir.Model.Version.All, Order=250, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.DSTU2.SampledData),typeof(Hl7.Fhir.Model.DSTU2.Signature),typeof(Hl7.Fhir.Model.DSTU2.HumanName),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.DSTU2.ContactPoint),typeof(Hl7.Fhir.Model.DSTU2.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Meta))]
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
        [FhirElement("maxLength", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
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
        [FhirElement("condition", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
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
        [FhirElement("constraint", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
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
        /// If the element must supported
        /// </summary>
        [FhirElement("mustSupport", InSummary=Hl7.Fhir.Model.Version.All, Order=290)]
        [CLSCompliant(false)]
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
        [FhirElement("isModifier", InSummary=Hl7.Fhir.Model.Version.All, Order=300)]
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
        /// Include when _summary = true?
        /// </summary>
        [FhirElement("isSummary", InSummary=Hl7.Fhir.Model.Version.All, Order=310)]
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
        [FhirElement("binding", InSummary=Hl7.Fhir.Model.Version.All, Order=320)]
        [CLSCompliant(false)]
        [DataMember]
        public BindingComponent Binding
        {
            get { return _Binding; }
            set { _Binding = value; OnPropertyChanged("Binding"); }
        }
        
        private BindingComponent _Binding;
        
        /// <summary>
        /// Map element to another set of definitions
        /// </summary>
        [FhirElement("mapping", InSummary=Hl7.Fhir.Model.Version.All, Order=330)]
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
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-2",
                severity: ConstraintSeverity.Warning,
                expression: "min.empty() or max.empty() or (max = '*') or (min <= max.toInteger())",
                human: "Min <= Max",
                xpath: "not(exists(f:min)) or not(exists(f:max)) or (not(f:max/@value) and not(f:min/@value)) or (f:max/@value = '*') or (number(f:max/@value) >= f:min/@value)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-5",
                severity: ConstraintSeverity.Warning,
                expression: "nameReference.empty() or (type.empty() and defaultValue.empty() and fixed.empty() and pattern.empty() and example.empty() and minValue.empty() and maxValue.empty() and maxLength.empty() and binding.empty())",
                human: "Either a namereference or a fixed value (but not both) is permitted",
                xpath: "not(exists(f:nameReference) and exists(f:*[starts-with(local-name(.), 'value')]))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-7",
                severity: ConstraintSeverity.Warning,
                expression: "pattern.empty() or (type.count() = 1)",
                human: "Pattern may only be specified if there is one type",
                xpath: "not(exists(f:*[starts-with(local-name(.), 'pattern')])) or (count(f:type)=1)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-6",
                severity: ConstraintSeverity.Warning,
                expression: "fixed.empty() or (type.count() = 1)",
                human: "Fixed value may only be specified if there is one type",
                xpath: "not(exists(f:*[starts-with(local-name(.), 'fixed')])) or (count(f:type)=1)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-11",
                severity: ConstraintSeverity.Warning,
                expression: "binding.empty() or type.code.empty() or type.select((code = 'code') or (code = 'Coding') or (code='CodeableConcept') or (code = 'Quantity') or (code = 'Extension') or (code = 'string') or (code = 'uri')).exists()",
                human: "Binding can only be present for coded elements, string, and uri",
                xpath: "not(exists(f:binding)) or (count(f:type/f:code) = 0) or  f:type/f:code/@value=('code','Coding','CodeableConcept','Quantity','Extension', 'string', 'uri')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-8",
                severity: ConstraintSeverity.Warning,
                expression: "pattern.empty() or fixed.empty()",
                human: "Pattern and value are mutually exclusive",
                xpath: "not(exists(f:*[starts-with(local-name(.), 'pattern')])) or not(exists(f:*[starts-with(local-name(.), 'value')]))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-14",
                severity: ConstraintSeverity.Warning,
                expression: "constraint.key.isDistinct()",
                human: "Constraints must be unique by key",
                xpath: "count(f:constraint) = count(distinct-values(f:constraint/f:key/@value))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-13",
                severity: ConstraintSeverity.Warning,
                expression: "type.select(code&profile).isDistinct()",
                human: "Types must be unique by the combination of code and profile",
                xpath: "not(exists(for $type in f:type return $type/preceding-sibling::f:type[f:code/@value=$type/f:code/@value and f:profile/@value = $type/f:profile/@value]))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-15",
                severity: ConstraintSeverity.Warning,
                expression: "defaultValue.empty() or meaningWhenMissing.empty()",
                human: "default value and meaningWhenMissing are mutually exclusive",
                xpath: "not(exists(f:*[starts-with(local-name(.), 'fixed')])) or not(exists(f:meaningWhenMissing))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-1",
                severity: ConstraintSeverity.Warning,
                expression: "slicing.all(discriminator or description)",
                human: "If there are no discriminators, there must be a definition",
                xpath: "(f:discriminator) or (f:description)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-3",
                severity: ConstraintSeverity.Warning,
                expression: "max.all(empty() or ($this = '*') or (toInteger() >= 0))",
                human: "Max SHALL be a number or \"*\"",
                xpath: "@value='*' or (normalize-space(@value)!='' and normalize-space(translate(@value, '0123456789',''))='')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-4",
                severity: ConstraintSeverity.Warning,
                expression: "type.all(aggregation.empty() or (code = 'Reference'))",
                human: "Aggregation may only be specified if one of the allowed types for the element is a resource",
                xpath: "not(exists(f:aggregation)) or exists(f:code[@value = 'Reference'])"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-10",
                severity: ConstraintSeverity.Warning,
                expression: "binding.all(valueSet or description)",
                human: "provide either a reference or a description (or both)",
                xpath: "(exists(f:valueSetUri) or exists(f:valueSetReference)) or exists(f:description)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "eld-12",
                severity: ConstraintSeverity.Warning,
                expression: "binding.valueSet.all(startsWith('http:') or startsWith('https'))",
                human: "URI SHALL start with http:// or https://",
                xpath: "starts-with(string(@value), 'http:') or starts-with(string(@value), 'https:')"
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
                if(RepresentationElement != null) dest.RepresentationElement = new List<Code<Hl7.Fhir.Model.DSTU2.PropertyRepresentation>>(RepresentationElement.DeepCopy());
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                if(Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                if(Slicing != null) dest.Slicing = (SlicingComponent)Slicing.DeepCopy();
                if(ShortElement != null) dest.ShortElement = (Hl7.Fhir.Model.FhirString)ShortElement.DeepCopy();
                if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.Markdown)DefinitionElement.DeepCopy();
                if(CommentsElement != null) dest.CommentsElement = (Hl7.Fhir.Model.Markdown)CommentsElement.DeepCopy();
                if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.Markdown)RequirementsElement.DeepCopy();
                if(AliasElement != null) dest.AliasElement = new List<Hl7.Fhir.Model.FhirString>(AliasElement.DeepCopy());
                if(MinElement != null) dest.MinElement = (Hl7.Fhir.Model.Integer)MinElement.DeepCopy();
                if(MaxElement != null) dest.MaxElement = (Hl7.Fhir.Model.FhirString)MaxElement.DeepCopy();
                if(Base != null) dest.Base = (BaseComponent)Base.DeepCopy();
                if(Type != null) dest.Type = new List<TypeRefComponent>(Type.DeepCopy());
                if(NameReferenceElement != null) dest.NameReferenceElement = (Hl7.Fhir.Model.FhirString)NameReferenceElement.DeepCopy();
                if(DefaultValue != null) dest.DefaultValue = (Hl7.Fhir.Model.Element)DefaultValue.DeepCopy();
                if(MeaningWhenMissingElement != null) dest.MeaningWhenMissingElement = (Hl7.Fhir.Model.Markdown)MeaningWhenMissingElement.DeepCopy();
                if(Fixed != null) dest.Fixed = (Hl7.Fhir.Model.Element)Fixed.DeepCopy();
                if(Pattern != null) dest.Pattern = (Hl7.Fhir.Model.Element)Pattern.DeepCopy();
                if(Example != null) dest.Example = (Hl7.Fhir.Model.Element)Example.DeepCopy();
                if(MinValue != null) dest.MinValue = (Hl7.Fhir.Model.Element)MinValue.DeepCopy();
                if(MaxValue != null) dest.MaxValue = (Hl7.Fhir.Model.Element)MaxValue.DeepCopy();
                if(MaxLengthElement != null) dest.MaxLengthElement = (Hl7.Fhir.Model.Integer)MaxLengthElement.DeepCopy();
                if(ConditionElement != null) dest.ConditionElement = new List<Hl7.Fhir.Model.Id>(ConditionElement.DeepCopy());
                if(Constraint != null) dest.Constraint = new List<ConstraintComponent>(Constraint.DeepCopy());
                if(MustSupportElement != null) dest.MustSupportElement = (Hl7.Fhir.Model.FhirBoolean)MustSupportElement.DeepCopy();
                if(IsModifierElement != null) dest.IsModifierElement = (Hl7.Fhir.Model.FhirBoolean)IsModifierElement.DeepCopy();
                if(IsSummaryElement != null) dest.IsSummaryElement = (Hl7.Fhir.Model.FhirBoolean)IsSummaryElement.DeepCopy();
                if(Binding != null) dest.Binding = (BindingComponent)Binding.DeepCopy();
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
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Slicing, otherT.Slicing)) return false;
            if( !DeepComparable.Matches(ShortElement, otherT.ShortElement)) return false;
            if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.Matches(CommentsElement, otherT.CommentsElement)) return false;
            if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.Matches(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.Matches(MinElement, otherT.MinElement)) return false;
            if( !DeepComparable.Matches(MaxElement, otherT.MaxElement)) return false;
            if( !DeepComparable.Matches(Base, otherT.Base)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(NameReferenceElement, otherT.NameReferenceElement)) return false;
            if( !DeepComparable.Matches(DefaultValue, otherT.DefaultValue)) return false;
            if( !DeepComparable.Matches(MeaningWhenMissingElement, otherT.MeaningWhenMissingElement)) return false;
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
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Slicing, otherT.Slicing)) return false;
            if( !DeepComparable.IsExactly(ShortElement, otherT.ShortElement)) return false;
            if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;
            if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.IsExactly(AliasElement, otherT.AliasElement)) return false;
            if( !DeepComparable.IsExactly(MinElement, otherT.MinElement)) return false;
            if( !DeepComparable.IsExactly(MaxElement, otherT.MaxElement)) return false;
            if( !DeepComparable.IsExactly(Base, otherT.Base)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(NameReferenceElement, otherT.NameReferenceElement)) return false;
            if( !DeepComparable.IsExactly(DefaultValue, otherT.DefaultValue)) return false;
            if( !DeepComparable.IsExactly(MeaningWhenMissingElement, otherT.MeaningWhenMissingElement)) return false;
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
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
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
            sink.Element("comments", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CommentsElement?.Serialize(sink);
            sink.Element("requirements", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RequirementsElement?.Serialize(sink);
            sink.BeginList("alias", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(AliasElement);
            sink.End();
            sink.Element("min", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MinElement?.Serialize(sink);
            sink.Element("max", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MaxElement?.Serialize(sink);
            sink.Element("base", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Base?.Serialize(sink);
            sink.BeginList("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Type)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("nameReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameReferenceElement?.Serialize(sink);
            sink.Element("defaultValue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); DefaultValue?.Serialize(sink);
            sink.Element("meaningWhenMissing", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MeaningWhenMissingElement?.Serialize(sink);
            sink.Element("fixed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Fixed?.Serialize(sink);
            sink.Element("pattern", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Pattern?.Serialize(sink);
            sink.Element("example", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Example?.Serialize(sink);
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
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "path":
                    PathElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "representation":
                    RepresentationElement = source.GetList<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.PropertyRepresentation>>();
                    return true;
                case "name":
                    NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "label":
                    LabelElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "code":
                    Code = source.GetList<Hl7.Fhir.Model.Coding>();
                    return true;
                case "slicing":
                    Slicing = source.Get<SlicingComponent>();
                    return true;
                case "short":
                    ShortElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "definition":
                    DefinitionElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "comments":
                    CommentsElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "requirements":
                    RequirementsElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "alias":
                    AliasElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "min":
                    MinElement = source.Get<Hl7.Fhir.Model.Integer>();
                    return true;
                case "max":
                    MaxElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "base":
                    Base = source.Get<BaseComponent>();
                    return true;
                case "type":
                    Type = source.GetList<TypeRefComponent>();
                    return true;
                case "nameReference":
                    NameReferenceElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "defaultValueBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "defaultValueInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Integer>();
                    return true;
                case "defaultValueDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                    return true;
                case "defaultValueBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Base64Binary>();
                    return true;
                case "defaultValueInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "defaultValueString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "defaultValueUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "defaultValueDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "defaultValueDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "defaultValueTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Time>();
                    return true;
                case "defaultValueCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "defaultValueOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Oid>();
                    return true;
                case "defaultValueId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Id>();
                    return true;
                case "defaultValueUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                    return true;
                case "defaultValuePositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.PositiveInt>();
                    return true;
                case "defaultValueMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "defaultValueAnnotation":
                    source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "defaultValueAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Attachment>();
                    return true;
                case "defaultValueIdentifier":
                    source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "defaultValueCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "defaultValueCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "defaultValueQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "defaultValueRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Range>();
                    return true;
                case "defaultValuePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "defaultValueRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Ratio>();
                    return true;
                case "defaultValueSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.DSTU2.SampledData>();
                    return true;
                case "defaultValueSignature":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.DSTU2.Signature>();
                    return true;
                case "defaultValueHumanName":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.DSTU2.HumanName>();
                    return true;
                case "defaultValueAddress":
                    source.CheckDuplicates<Hl7.Fhir.Model.Address>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Address>();
                    return true;
                case "defaultValueContactPoint":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.DSTU2.ContactPoint>();
                    return true;
                case "defaultValueTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.DSTU2.Timing>();
                    return true;
                case "defaultValueReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "defaultValueMeta":
                    source.CheckDuplicates<Hl7.Fhir.Model.Meta>(DefaultValue, "defaultValue");
                    DefaultValue = source.Get<Hl7.Fhir.Model.Meta>();
                    return true;
                case "meaningWhenMissing":
                    MeaningWhenMissingElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "fixedBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "fixedInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Integer>();
                    return true;
                case "fixedDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                    return true;
                case "fixedBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Base64Binary>();
                    return true;
                case "fixedInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "fixedString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "fixedUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "fixedDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "fixedDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "fixedTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Time>();
                    return true;
                case "fixedCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "fixedOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Oid>();
                    return true;
                case "fixedId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Id>();
                    return true;
                case "fixedUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                    return true;
                case "fixedPositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.PositiveInt>();
                    return true;
                case "fixedMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "fixedAnnotation":
                    source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "fixedAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Attachment>();
                    return true;
                case "fixedIdentifier":
                    source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "fixedCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "fixedCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "fixedQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "fixedRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Range>();
                    return true;
                case "fixedPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "fixedRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Ratio>();
                    return true;
                case "fixedSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.DSTU2.SampledData>();
                    return true;
                case "fixedSignature":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.DSTU2.Signature>();
                    return true;
                case "fixedHumanName":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.DSTU2.HumanName>();
                    return true;
                case "fixedAddress":
                    source.CheckDuplicates<Hl7.Fhir.Model.Address>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Address>();
                    return true;
                case "fixedContactPoint":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.DSTU2.ContactPoint>();
                    return true;
                case "fixedTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.DSTU2.Timing>();
                    return true;
                case "fixedReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "fixedMeta":
                    source.CheckDuplicates<Hl7.Fhir.Model.Meta>(Fixed, "fixed");
                    Fixed = source.Get<Hl7.Fhir.Model.Meta>();
                    return true;
                case "patternBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "patternInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Integer>();
                    return true;
                case "patternDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                    return true;
                case "patternBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Base64Binary>();
                    return true;
                case "patternInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "patternString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "patternUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "patternDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "patternDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "patternTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Time>();
                    return true;
                case "patternCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "patternOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Oid>();
                    return true;
                case "patternId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Id>();
                    return true;
                case "patternUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                    return true;
                case "patternPositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.PositiveInt>();
                    return true;
                case "patternMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "patternAnnotation":
                    source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "patternAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Attachment>();
                    return true;
                case "patternIdentifier":
                    source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "patternCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "patternCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "patternQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "patternRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Range>();
                    return true;
                case "patternPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "patternRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Ratio>();
                    return true;
                case "patternSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.DSTU2.SampledData>();
                    return true;
                case "patternSignature":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.DSTU2.Signature>();
                    return true;
                case "patternHumanName":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.DSTU2.HumanName>();
                    return true;
                case "patternAddress":
                    source.CheckDuplicates<Hl7.Fhir.Model.Address>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Address>();
                    return true;
                case "patternContactPoint":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.DSTU2.ContactPoint>();
                    return true;
                case "patternTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.DSTU2.Timing>();
                    return true;
                case "patternReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "patternMeta":
                    source.CheckDuplicates<Hl7.Fhir.Model.Meta>(Pattern, "pattern");
                    Pattern = source.Get<Hl7.Fhir.Model.Meta>();
                    return true;
                case "exampleBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "exampleInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Integer>();
                    return true;
                case "exampleDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                    return true;
                case "exampleBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Base64Binary>();
                    return true;
                case "exampleInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "exampleString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "exampleUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "exampleDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "exampleDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "exampleTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Time>();
                    return true;
                case "exampleCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "exampleOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Oid>();
                    return true;
                case "exampleId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Id>();
                    return true;
                case "exampleUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                    return true;
                case "examplePositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.PositiveInt>();
                    return true;
                case "exampleMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "exampleAnnotation":
                    source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "exampleAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Attachment>();
                    return true;
                case "exampleIdentifier":
                    source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "exampleCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "exampleCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "exampleQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "exampleRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Range>();
                    return true;
                case "examplePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "exampleRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Ratio>();
                    return true;
                case "exampleSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.DSTU2.SampledData>();
                    return true;
                case "exampleSignature":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.DSTU2.Signature>();
                    return true;
                case "exampleHumanName":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.DSTU2.HumanName>();
                    return true;
                case "exampleAddress":
                    source.CheckDuplicates<Hl7.Fhir.Model.Address>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Address>();
                    return true;
                case "exampleContactPoint":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.DSTU2.ContactPoint>();
                    return true;
                case "exampleTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.DSTU2.Timing>();
                    return true;
                case "exampleReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "exampleMeta":
                    source.CheckDuplicates<Hl7.Fhir.Model.Meta>(Example, "example");
                    Example = source.Get<Hl7.Fhir.Model.Meta>();
                    return true;
                case "minValueBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "minValueInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Integer>();
                    return true;
                case "minValueDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                    return true;
                case "minValueBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Base64Binary>();
                    return true;
                case "minValueInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "minValueString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "minValueUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "minValueDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "minValueDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "minValueTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Time>();
                    return true;
                case "minValueCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "minValueOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Oid>();
                    return true;
                case "minValueId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Id>();
                    return true;
                case "minValueUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                    return true;
                case "minValuePositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.PositiveInt>();
                    return true;
                case "minValueMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "minValueAnnotation":
                    source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "minValueAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Attachment>();
                    return true;
                case "minValueIdentifier":
                    source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "minValueCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "minValueCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "minValueQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "minValueRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Range>();
                    return true;
                case "minValuePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "minValueRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Ratio>();
                    return true;
                case "minValueSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.DSTU2.SampledData>();
                    return true;
                case "minValueSignature":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.DSTU2.Signature>();
                    return true;
                case "minValueHumanName":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.DSTU2.HumanName>();
                    return true;
                case "minValueAddress":
                    source.CheckDuplicates<Hl7.Fhir.Model.Address>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Address>();
                    return true;
                case "minValueContactPoint":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.DSTU2.ContactPoint>();
                    return true;
                case "minValueTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.DSTU2.Timing>();
                    return true;
                case "minValueReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "minValueMeta":
                    source.CheckDuplicates<Hl7.Fhir.Model.Meta>(MinValue, "minValue");
                    MinValue = source.Get<Hl7.Fhir.Model.Meta>();
                    return true;
                case "maxValueBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "maxValueInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Integer>();
                    return true;
                case "maxValueDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                    return true;
                case "maxValueBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Base64Binary>();
                    return true;
                case "maxValueInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "maxValueString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "maxValueUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "maxValueDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "maxValueDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "maxValueTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Time>();
                    return true;
                case "maxValueCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "maxValueOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Oid>();
                    return true;
                case "maxValueId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Id>();
                    return true;
                case "maxValueUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                    return true;
                case "maxValuePositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.PositiveInt>();
                    return true;
                case "maxValueMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "maxValueAnnotation":
                    source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "maxValueAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Attachment>();
                    return true;
                case "maxValueIdentifier":
                    source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "maxValueCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "maxValueCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "maxValueQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "maxValueRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Range>();
                    return true;
                case "maxValuePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "maxValueRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Ratio>();
                    return true;
                case "maxValueSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.DSTU2.SampledData>();
                    return true;
                case "maxValueSignature":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.DSTU2.Signature>();
                    return true;
                case "maxValueHumanName":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.DSTU2.HumanName>();
                    return true;
                case "maxValueAddress":
                    source.CheckDuplicates<Hl7.Fhir.Model.Address>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Address>();
                    return true;
                case "maxValueContactPoint":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.DSTU2.ContactPoint>();
                    return true;
                case "maxValueTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.DSTU2.Timing>();
                    return true;
                case "maxValueReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "maxValueMeta":
                    source.CheckDuplicates<Hl7.Fhir.Model.Meta>(MaxValue, "maxValue");
                    MaxValue = source.Get<Hl7.Fhir.Model.Meta>();
                    return true;
                case "maxLength":
                    MaxLengthElement = source.Get<Hl7.Fhir.Model.Integer>();
                    return true;
                case "condition":
                    ConditionElement = source.GetList<Hl7.Fhir.Model.Id>();
                    return true;
                case "constraint":
                    Constraint = source.GetList<ConstraintComponent>();
                    return true;
                case "mustSupport":
                    MustSupportElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "isModifier":
                    IsModifierElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "isSummary":
                    IsSummaryElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "binding":
                    Binding = source.Get<BindingComponent>();
                    return true;
                case "mapping":
                    Mapping = source.GetList<MappingComponent>();
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
                case "path":
                    PathElement = source.PopulateValue(PathElement);
                    return true;
                case "_path":
                    PathElement = source.Populate(PathElement);
                    return true;
                case "representation":
                case "_representation":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
                    return true;
                case "label":
                    LabelElement = source.PopulateValue(LabelElement);
                    return true;
                case "_label":
                    LabelElement = source.Populate(LabelElement);
                    return true;
                case "code":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "slicing":
                    Slicing = source.Populate(Slicing);
                    return true;
                case "short":
                    ShortElement = source.PopulateValue(ShortElement);
                    return true;
                case "_short":
                    ShortElement = source.Populate(ShortElement);
                    return true;
                case "definition":
                    DefinitionElement = source.PopulateValue(DefinitionElement);
                    return true;
                case "_definition":
                    DefinitionElement = source.Populate(DefinitionElement);
                    return true;
                case "comments":
                    CommentsElement = source.PopulateValue(CommentsElement);
                    return true;
                case "_comments":
                    CommentsElement = source.Populate(CommentsElement);
                    return true;
                case "requirements":
                    RequirementsElement = source.PopulateValue(RequirementsElement);
                    return true;
                case "_requirements":
                    RequirementsElement = source.Populate(RequirementsElement);
                    return true;
                case "alias":
                case "_alias":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "min":
                    MinElement = source.PopulateValue(MinElement);
                    return true;
                case "_min":
                    MinElement = source.Populate(MinElement);
                    return true;
                case "max":
                    MaxElement = source.PopulateValue(MaxElement);
                    return true;
                case "_max":
                    MaxElement = source.Populate(MaxElement);
                    return true;
                case "base":
                    Base = source.Populate(Base);
                    return true;
                case "type":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "nameReference":
                    NameReferenceElement = source.PopulateValue(NameReferenceElement);
                    return true;
                case "_nameReference":
                    NameReferenceElement = source.Populate(NameReferenceElement);
                    return true;
                case "defaultValueBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "_defaultValueBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "defaultValueInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.Integer);
                    return true;
                case "_defaultValueInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Integer);
                    return true;
                case "defaultValueDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.FhirDecimal);
                    return true;
                case "_defaultValueDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.FhirDecimal);
                    return true;
                case "defaultValueBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.Base64Binary);
                    return true;
                case "_defaultValueBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Base64Binary);
                    return true;
                case "defaultValueInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.Instant);
                    return true;
                case "_defaultValueInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Instant);
                    return true;
                case "defaultValueString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_defaultValueString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.FhirString);
                    return true;
                case "defaultValueUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_defaultValueUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "defaultValueDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.Date);
                    return true;
                case "_defaultValueDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Date);
                    return true;
                case "defaultValueDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_defaultValueDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "defaultValueTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.Time);
                    return true;
                case "_defaultValueTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Time);
                    return true;
                case "defaultValueCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.Code);
                    return true;
                case "_defaultValueCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Code);
                    return true;
                case "defaultValueOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.Oid);
                    return true;
                case "_defaultValueOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Oid);
                    return true;
                case "defaultValueId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.Id);
                    return true;
                case "_defaultValueId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Id);
                    return true;
                case "defaultValueUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.UnsignedInt);
                    return true;
                case "_defaultValueUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.UnsignedInt);
                    return true;
                case "defaultValuePositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "_defaultValuePositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "defaultValueMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(DefaultValue, "defaultValue");
                    DefaultValue = source.PopulateValue(DefaultValue as Hl7.Fhir.Model.Markdown);
                    return true;
                case "_defaultValueMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Markdown);
                    return true;
                case "defaultValueAnnotation":
                    source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Annotation);
                    return true;
                case "defaultValueAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Attachment);
                    return true;
                case "defaultValueIdentifier":
                    source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Identifier);
                    return true;
                case "defaultValueCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "defaultValueCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Coding);
                    return true;
                case "defaultValueQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Quantity);
                    return true;
                case "defaultValueRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Range);
                    return true;
                case "defaultValuePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Period);
                    return true;
                case "defaultValueRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Ratio);
                    return true;
                case "defaultValueSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.DSTU2.SampledData);
                    return true;
                case "defaultValueSignature":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.DSTU2.Signature);
                    return true;
                case "defaultValueHumanName":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.DSTU2.HumanName);
                    return true;
                case "defaultValueAddress":
                    source.CheckDuplicates<Hl7.Fhir.Model.Address>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Address);
                    return true;
                case "defaultValueContactPoint":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.DSTU2.ContactPoint);
                    return true;
                case "defaultValueTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.DSTU2.Timing);
                    return true;
                case "defaultValueReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "defaultValueMeta":
                    source.CheckDuplicates<Hl7.Fhir.Model.Meta>(DefaultValue, "defaultValue");
                    DefaultValue = source.Populate(DefaultValue as Hl7.Fhir.Model.Meta);
                    return true;
                case "meaningWhenMissing":
                    MeaningWhenMissingElement = source.PopulateValue(MeaningWhenMissingElement);
                    return true;
                case "_meaningWhenMissing":
                    MeaningWhenMissingElement = source.Populate(MeaningWhenMissingElement);
                    return true;
                case "fixedBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "_fixedBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "fixedInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.Integer);
                    return true;
                case "_fixedInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Integer);
                    return true;
                case "fixedDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.FhirDecimal);
                    return true;
                case "_fixedDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.FhirDecimal);
                    return true;
                case "fixedBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.Base64Binary);
                    return true;
                case "_fixedBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Base64Binary);
                    return true;
                case "fixedInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.Instant);
                    return true;
                case "_fixedInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Instant);
                    return true;
                case "fixedString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_fixedString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.FhirString);
                    return true;
                case "fixedUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_fixedUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "fixedDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.Date);
                    return true;
                case "_fixedDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Date);
                    return true;
                case "fixedDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_fixedDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "fixedTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.Time);
                    return true;
                case "_fixedTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Time);
                    return true;
                case "fixedCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.Code);
                    return true;
                case "_fixedCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Code);
                    return true;
                case "fixedOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.Oid);
                    return true;
                case "_fixedOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Oid);
                    return true;
                case "fixedId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.Id);
                    return true;
                case "_fixedId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Id);
                    return true;
                case "fixedUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.UnsignedInt);
                    return true;
                case "_fixedUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.UnsignedInt);
                    return true;
                case "fixedPositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "_fixedPositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "fixedMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Fixed, "fixed");
                    Fixed = source.PopulateValue(Fixed as Hl7.Fhir.Model.Markdown);
                    return true;
                case "_fixedMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Markdown);
                    return true;
                case "fixedAnnotation":
                    source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Annotation);
                    return true;
                case "fixedAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Attachment);
                    return true;
                case "fixedIdentifier":
                    source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Identifier);
                    return true;
                case "fixedCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "fixedCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Coding);
                    return true;
                case "fixedQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Quantity);
                    return true;
                case "fixedRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Range);
                    return true;
                case "fixedPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Period);
                    return true;
                case "fixedRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Ratio);
                    return true;
                case "fixedSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.DSTU2.SampledData);
                    return true;
                case "fixedSignature":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.DSTU2.Signature);
                    return true;
                case "fixedHumanName":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.DSTU2.HumanName);
                    return true;
                case "fixedAddress":
                    source.CheckDuplicates<Hl7.Fhir.Model.Address>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Address);
                    return true;
                case "fixedContactPoint":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.DSTU2.ContactPoint);
                    return true;
                case "fixedTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.DSTU2.Timing);
                    return true;
                case "fixedReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "fixedMeta":
                    source.CheckDuplicates<Hl7.Fhir.Model.Meta>(Fixed, "fixed");
                    Fixed = source.Populate(Fixed as Hl7.Fhir.Model.Meta);
                    return true;
                case "patternBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "_patternBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "patternInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.Integer);
                    return true;
                case "_patternInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Integer);
                    return true;
                case "patternDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.FhirDecimal);
                    return true;
                case "_patternDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.FhirDecimal);
                    return true;
                case "patternBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.Base64Binary);
                    return true;
                case "_patternBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Base64Binary);
                    return true;
                case "patternInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.Instant);
                    return true;
                case "_patternInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Instant);
                    return true;
                case "patternString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_patternString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.FhirString);
                    return true;
                case "patternUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_patternUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "patternDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.Date);
                    return true;
                case "_patternDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Date);
                    return true;
                case "patternDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_patternDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "patternTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.Time);
                    return true;
                case "_patternTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Time);
                    return true;
                case "patternCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.Code);
                    return true;
                case "_patternCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Code);
                    return true;
                case "patternOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.Oid);
                    return true;
                case "_patternOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Oid);
                    return true;
                case "patternId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.Id);
                    return true;
                case "_patternId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Id);
                    return true;
                case "patternUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.UnsignedInt);
                    return true;
                case "_patternUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.UnsignedInt);
                    return true;
                case "patternPositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "_patternPositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "patternMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Pattern, "pattern");
                    Pattern = source.PopulateValue(Pattern as Hl7.Fhir.Model.Markdown);
                    return true;
                case "_patternMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Markdown);
                    return true;
                case "patternAnnotation":
                    source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Annotation);
                    return true;
                case "patternAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Attachment);
                    return true;
                case "patternIdentifier":
                    source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Identifier);
                    return true;
                case "patternCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "patternCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Coding);
                    return true;
                case "patternQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Quantity);
                    return true;
                case "patternRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Range);
                    return true;
                case "patternPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Period);
                    return true;
                case "patternRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Ratio);
                    return true;
                case "patternSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.DSTU2.SampledData);
                    return true;
                case "patternSignature":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.DSTU2.Signature);
                    return true;
                case "patternHumanName":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.DSTU2.HumanName);
                    return true;
                case "patternAddress":
                    source.CheckDuplicates<Hl7.Fhir.Model.Address>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Address);
                    return true;
                case "patternContactPoint":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.DSTU2.ContactPoint);
                    return true;
                case "patternTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.DSTU2.Timing);
                    return true;
                case "patternReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "patternMeta":
                    source.CheckDuplicates<Hl7.Fhir.Model.Meta>(Pattern, "pattern");
                    Pattern = source.Populate(Pattern as Hl7.Fhir.Model.Meta);
                    return true;
                case "exampleBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "_exampleBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "exampleInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.Integer);
                    return true;
                case "_exampleInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Integer);
                    return true;
                case "exampleDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.FhirDecimal);
                    return true;
                case "_exampleDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.FhirDecimal);
                    return true;
                case "exampleBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.Base64Binary);
                    return true;
                case "_exampleBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Base64Binary);
                    return true;
                case "exampleInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.Instant);
                    return true;
                case "_exampleInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Instant);
                    return true;
                case "exampleString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_exampleString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.FhirString);
                    return true;
                case "exampleUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_exampleUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "exampleDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.Date);
                    return true;
                case "_exampleDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Date);
                    return true;
                case "exampleDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_exampleDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "exampleTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.Time);
                    return true;
                case "_exampleTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Time);
                    return true;
                case "exampleCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.Code);
                    return true;
                case "_exampleCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Code);
                    return true;
                case "exampleOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.Oid);
                    return true;
                case "_exampleOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Oid);
                    return true;
                case "exampleId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.Id);
                    return true;
                case "_exampleId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Id);
                    return true;
                case "exampleUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.UnsignedInt);
                    return true;
                case "_exampleUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.UnsignedInt);
                    return true;
                case "examplePositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "_examplePositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "exampleMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Example, "example");
                    Example = source.PopulateValue(Example as Hl7.Fhir.Model.Markdown);
                    return true;
                case "_exampleMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Markdown);
                    return true;
                case "exampleAnnotation":
                    source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Annotation);
                    return true;
                case "exampleAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Attachment);
                    return true;
                case "exampleIdentifier":
                    source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Identifier);
                    return true;
                case "exampleCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "exampleCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Coding);
                    return true;
                case "exampleQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Quantity);
                    return true;
                case "exampleRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Range);
                    return true;
                case "examplePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Period);
                    return true;
                case "exampleRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Ratio);
                    return true;
                case "exampleSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.DSTU2.SampledData);
                    return true;
                case "exampleSignature":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.DSTU2.Signature);
                    return true;
                case "exampleHumanName":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.DSTU2.HumanName);
                    return true;
                case "exampleAddress":
                    source.CheckDuplicates<Hl7.Fhir.Model.Address>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Address);
                    return true;
                case "exampleContactPoint":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.DSTU2.ContactPoint);
                    return true;
                case "exampleTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.DSTU2.Timing);
                    return true;
                case "exampleReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "exampleMeta":
                    source.CheckDuplicates<Hl7.Fhir.Model.Meta>(Example, "example");
                    Example = source.Populate(Example as Hl7.Fhir.Model.Meta);
                    return true;
                case "minValueBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "_minValueBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "minValueInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.Integer);
                    return true;
                case "_minValueInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Integer);
                    return true;
                case "minValueDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.FhirDecimal);
                    return true;
                case "_minValueDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.FhirDecimal);
                    return true;
                case "minValueBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.Base64Binary);
                    return true;
                case "_minValueBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Base64Binary);
                    return true;
                case "minValueInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.Instant);
                    return true;
                case "_minValueInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Instant);
                    return true;
                case "minValueString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_minValueString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.FhirString);
                    return true;
                case "minValueUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_minValueUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "minValueDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.Date);
                    return true;
                case "_minValueDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Date);
                    return true;
                case "minValueDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_minValueDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "minValueTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.Time);
                    return true;
                case "_minValueTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Time);
                    return true;
                case "minValueCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.Code);
                    return true;
                case "_minValueCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Code);
                    return true;
                case "minValueOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.Oid);
                    return true;
                case "_minValueOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Oid);
                    return true;
                case "minValueId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.Id);
                    return true;
                case "_minValueId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Id);
                    return true;
                case "minValueUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.UnsignedInt);
                    return true;
                case "_minValueUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.UnsignedInt);
                    return true;
                case "minValuePositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "_minValuePositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "minValueMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(MinValue, "minValue");
                    MinValue = source.PopulateValue(MinValue as Hl7.Fhir.Model.Markdown);
                    return true;
                case "_minValueMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Markdown);
                    return true;
                case "minValueAnnotation":
                    source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Annotation);
                    return true;
                case "minValueAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Attachment);
                    return true;
                case "minValueIdentifier":
                    source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Identifier);
                    return true;
                case "minValueCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "minValueCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Coding);
                    return true;
                case "minValueQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Quantity);
                    return true;
                case "minValueRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Range);
                    return true;
                case "minValuePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Period);
                    return true;
                case "minValueRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Ratio);
                    return true;
                case "minValueSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.DSTU2.SampledData);
                    return true;
                case "minValueSignature":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.DSTU2.Signature);
                    return true;
                case "minValueHumanName":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.DSTU2.HumanName);
                    return true;
                case "minValueAddress":
                    source.CheckDuplicates<Hl7.Fhir.Model.Address>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Address);
                    return true;
                case "minValueContactPoint":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.DSTU2.ContactPoint);
                    return true;
                case "minValueTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.DSTU2.Timing);
                    return true;
                case "minValueReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "minValueMeta":
                    source.CheckDuplicates<Hl7.Fhir.Model.Meta>(MinValue, "minValue");
                    MinValue = source.Populate(MinValue as Hl7.Fhir.Model.Meta);
                    return true;
                case "maxValueBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "_maxValueBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "maxValueInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.Integer);
                    return true;
                case "_maxValueInteger":
                    source.CheckDuplicates<Hl7.Fhir.Model.Integer>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Integer);
                    return true;
                case "maxValueDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.FhirDecimal);
                    return true;
                case "_maxValueDecimal":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.FhirDecimal);
                    return true;
                case "maxValueBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.Base64Binary);
                    return true;
                case "_maxValueBase64Binary":
                    source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Base64Binary);
                    return true;
                case "maxValueInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.Instant);
                    return true;
                case "_maxValueInstant":
                    source.CheckDuplicates<Hl7.Fhir.Model.Instant>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Instant);
                    return true;
                case "maxValueString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_maxValueString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.FhirString);
                    return true;
                case "maxValueUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_maxValueUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "maxValueDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.Date);
                    return true;
                case "_maxValueDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Date);
                    return true;
                case "maxValueDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_maxValueDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "maxValueTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.Time);
                    return true;
                case "_maxValueTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Time);
                    return true;
                case "maxValueCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.Code);
                    return true;
                case "_maxValueCode":
                    source.CheckDuplicates<Hl7.Fhir.Model.Code>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Code);
                    return true;
                case "maxValueOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.Oid);
                    return true;
                case "_maxValueOid":
                    source.CheckDuplicates<Hl7.Fhir.Model.Oid>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Oid);
                    return true;
                case "maxValueId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.Id);
                    return true;
                case "_maxValueId":
                    source.CheckDuplicates<Hl7.Fhir.Model.Id>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Id);
                    return true;
                case "maxValueUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.UnsignedInt);
                    return true;
                case "_maxValueUnsignedInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.UnsignedInt);
                    return true;
                case "maxValuePositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "_maxValuePositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "maxValueMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(MaxValue, "maxValue");
                    MaxValue = source.PopulateValue(MaxValue as Hl7.Fhir.Model.Markdown);
                    return true;
                case "_maxValueMarkdown":
                    source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Markdown);
                    return true;
                case "maxValueAnnotation":
                    source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Annotation);
                    return true;
                case "maxValueAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Attachment);
                    return true;
                case "maxValueIdentifier":
                    source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Identifier);
                    return true;
                case "maxValueCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "maxValueCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Coding);
                    return true;
                case "maxValueQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Quantity);
                    return true;
                case "maxValueRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Range);
                    return true;
                case "maxValuePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Period);
                    return true;
                case "maxValueRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Ratio);
                    return true;
                case "maxValueSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.DSTU2.SampledData);
                    return true;
                case "maxValueSignature":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.DSTU2.Signature);
                    return true;
                case "maxValueHumanName":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.DSTU2.HumanName);
                    return true;
                case "maxValueAddress":
                    source.CheckDuplicates<Hl7.Fhir.Model.Address>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Address);
                    return true;
                case "maxValueContactPoint":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.DSTU2.ContactPoint);
                    return true;
                case "maxValueTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.DSTU2.Timing);
                    return true;
                case "maxValueReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "maxValueMeta":
                    source.CheckDuplicates<Hl7.Fhir.Model.Meta>(MaxValue, "maxValue");
                    MaxValue = source.Populate(MaxValue as Hl7.Fhir.Model.Meta);
                    return true;
                case "maxLength":
                    MaxLengthElement = source.PopulateValue(MaxLengthElement);
                    return true;
                case "_maxLength":
                    MaxLengthElement = source.Populate(MaxLengthElement);
                    return true;
                case "condition":
                case "_condition":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "constraint":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "mustSupport":
                    MustSupportElement = source.PopulateValue(MustSupportElement);
                    return true;
                case "_mustSupport":
                    MustSupportElement = source.Populate(MustSupportElement);
                    return true;
                case "isModifier":
                    IsModifierElement = source.PopulateValue(IsModifierElement);
                    return true;
                case "_isModifier":
                    IsModifierElement = source.Populate(IsModifierElement);
                    return true;
                case "isSummary":
                    IsSummaryElement = source.PopulateValue(IsSummaryElement);
                    return true;
                case "_isSummary":
                    IsSummaryElement = source.Populate(IsSummaryElement);
                    return true;
                case "binding":
                    Binding = source.Populate(Binding);
                    return true;
                case "mapping":
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
                case "representation":
                    source.PopulatePrimitiveListItemValue(RepresentationElement, index);
                    return true;
                case "_representation":
                    source.PopulatePrimitiveListItem(RepresentationElement, index);
                    return true;
                case "code":
                    source.PopulateListItem(Code, index);
                    return true;
                case "alias":
                    source.PopulatePrimitiveListItemValue(AliasElement, index);
                    return true;
                case "_alias":
                    source.PopulatePrimitiveListItem(AliasElement, index);
                    return true;
                case "type":
                    source.PopulateListItem(Type, index);
                    return true;
                case "condition":
                    source.PopulatePrimitiveListItemValue(ConditionElement, index);
                    return true;
                case "_condition":
                    source.PopulatePrimitiveListItem(ConditionElement, index);
                    return true;
                case "constraint":
                    source.PopulateListItem(Constraint, index);
                    return true;
                case "mapping":
                    source.PopulateListItem(Mapping, index);
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
                if (PathElement != null) yield return PathElement;
                foreach (var elem in RepresentationElement) { if (elem != null) yield return elem; }
                if (NameElement != null) yield return NameElement;
                if (LabelElement != null) yield return LabelElement;
                foreach (var elem in Code) { if (elem != null) yield return elem; }
                if (Slicing != null) yield return Slicing;
                if (ShortElement != null) yield return ShortElement;
                if (DefinitionElement != null) yield return DefinitionElement;
                if (CommentsElement != null) yield return CommentsElement;
                if (RequirementsElement != null) yield return RequirementsElement;
                foreach (var elem in AliasElement) { if (elem != null) yield return elem; }
                if (MinElement != null) yield return MinElement;
                if (MaxElement != null) yield return MaxElement;
                if (Base != null) yield return Base;
                foreach (var elem in Type) { if (elem != null) yield return elem; }
                if (NameReferenceElement != null) yield return NameReferenceElement;
                if (DefaultValue != null) yield return DefaultValue;
                if (MeaningWhenMissingElement != null) yield return MeaningWhenMissingElement;
                if (Fixed != null) yield return Fixed;
                if (Pattern != null) yield return Pattern;
                if (Example != null) yield return Example;
                if (MinValue != null) yield return MinValue;
                if (MaxValue != null) yield return MaxValue;
                if (MaxLengthElement != null) yield return MaxLengthElement;
                foreach (var elem in ConditionElement) { if (elem != null) yield return elem; }
                foreach (var elem in Constraint) { if (elem != null) yield return elem; }
                if (MustSupportElement != null) yield return MustSupportElement;
                if (IsModifierElement != null) yield return IsModifierElement;
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
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (LabelElement != null) yield return new ElementValue("label", LabelElement);
                foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                if (Slicing != null) yield return new ElementValue("slicing", Slicing);
                if (ShortElement != null) yield return new ElementValue("short", ShortElement);
                if (DefinitionElement != null) yield return new ElementValue("definition", DefinitionElement);
                if (CommentsElement != null) yield return new ElementValue("comments", CommentsElement);
                if (RequirementsElement != null) yield return new ElementValue("requirements", RequirementsElement);
                foreach (var elem in AliasElement) { if (elem != null) yield return new ElementValue("alias", elem); }
                if (MinElement != null) yield return new ElementValue("min", MinElement);
                if (MaxElement != null) yield return new ElementValue("max", MaxElement);
                if (Base != null) yield return new ElementValue("base", Base);
                foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                if (NameReferenceElement != null) yield return new ElementValue("nameReference", NameReferenceElement);
                if (DefaultValue != null) yield return new ElementValue("defaultValue", DefaultValue);
                if (MeaningWhenMissingElement != null) yield return new ElementValue("meaningWhenMissing", MeaningWhenMissingElement);
                if (Fixed != null) yield return new ElementValue("fixed", Fixed);
                if (Pattern != null) yield return new ElementValue("pattern", Pattern);
                if (Example != null) yield return new ElementValue("example", Example);
                if (MinValue != null) yield return new ElementValue("minValue", MinValue);
                if (MaxValue != null) yield return new ElementValue("maxValue", MaxValue);
                if (MaxLengthElement != null) yield return new ElementValue("maxLength", MaxLengthElement);
                foreach (var elem in ConditionElement) { if (elem != null) yield return new ElementValue("condition", elem); }
                foreach (var elem in Constraint) { if (elem != null) yield return new ElementValue("constraint", elem); }
                if (MustSupportElement != null) yield return new ElementValue("mustSupport", MustSupportElement);
                if (IsModifierElement != null) yield return new ElementValue("isModifier", IsModifierElement);
                if (IsSummaryElement != null) yield return new ElementValue("isSummary", IsSummaryElement);
                if (Binding != null) yield return new ElementValue("binding", Binding);
                foreach (var elem in Mapping) { if (elem != null) yield return new ElementValue("mapping", elem); }
            }
        }
    
    }

}
