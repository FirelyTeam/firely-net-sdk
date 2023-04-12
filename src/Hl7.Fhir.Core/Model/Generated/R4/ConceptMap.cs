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
    /// A map from one set of concepts to one or more other concepts
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "ConceptMap", IsResource=true)]
    [DataContract]
    public partial class ConceptMap : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IConceptMap, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ConceptMap; } }
        [NotMapped]
        public override string TypeName { get { return "ConceptMap"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "GroupComponent")]
        [DataContract]
        public partial class GroupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "GroupComponent"; } }
            
            /// <summary>
            /// Source system where concepts to be mapped are defined
            /// </summary>
            [FhirElement("source", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SourceElement
            {
                get { return _SourceElement; }
                set { _SourceElement = value; OnPropertyChanged("SourceElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _SourceElement;
            
            /// <summary>
            /// Source system where concepts to be mapped are defined
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
                        SourceElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Source");
                }
            }
            
            /// <summary>
            /// Specific version of the  code system
            /// </summary>
            [FhirElement("sourceVersion", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SourceVersionElement
            {
                get { return _SourceVersionElement; }
                set { _SourceVersionElement = value; OnPropertyChanged("SourceVersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SourceVersionElement;
            
            /// <summary>
            /// Specific version of the  code system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SourceVersion
            {
                get { return SourceVersionElement != null ? SourceVersionElement.Value : null; }
                set
                {
                    if (value == null)
                        SourceVersionElement = null;
                    else
                        SourceVersionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("SourceVersion");
                }
            }
            
            /// <summary>
            /// Target system that the concepts are to be mapped to
            /// </summary>
            [FhirElement("target", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri TargetElement
            {
                get { return _TargetElement; }
                set { _TargetElement = value; OnPropertyChanged("TargetElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _TargetElement;
            
            /// <summary>
            /// Target system that the concepts are to be mapped to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Target
            {
                get { return TargetElement != null ? TargetElement.Value : null; }
                set
                {
                    if (value == null)
                        TargetElement = null;
                    else
                        TargetElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Target");
                }
            }
            
            /// <summary>
            /// Specific version of the  code system
            /// </summary>
            [FhirElement("targetVersion", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TargetVersionElement
            {
                get { return _TargetVersionElement; }
                set { _TargetVersionElement = value; OnPropertyChanged("TargetVersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TargetVersionElement;
            
            /// <summary>
            /// Specific version of the  code system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string TargetVersion
            {
                get { return TargetVersionElement != null ? TargetVersionElement.Value : null; }
                set
                {
                    if (value == null)
                        TargetVersionElement = null;
                    else
                        TargetVersionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("TargetVersion");
                }
            }
            
            /// <summary>
            /// Mappings for a concept from the source set
            /// </summary>
            [FhirElement("element", Order=80)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<SourceElementComponent> Element
            {
                get { if(_Element==null) _Element = new List<SourceElementComponent>(); return _Element; }
                set { _Element = value; OnPropertyChanged("Element"); }
            }
            
            private List<SourceElementComponent> _Element;
            
            /// <summary>
            /// What to do when there is no mapping for the source concept
            /// </summary>
            [FhirElement("unmapped", Order=90)]
            [DataMember]
            public UnmappedComponent Unmapped
            {
                get { return _Unmapped; }
                set { _Unmapped = value; OnPropertyChanged("Unmapped"); }
            }
            
            private UnmappedComponent _Unmapped;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("GroupComponent");
                base.Serialize(sink);
                sink.Element("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SourceElement?.Serialize(sink);
                sink.Element("sourceVersion", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SourceVersionElement?.Serialize(sink);
                sink.Element("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TargetElement?.Serialize(sink);
                sink.Element("targetVersion", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TargetVersionElement?.Serialize(sink);
                sink.BeginList("element", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Element)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("unmapped", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Unmapped?.Serialize(sink);
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
                    case "source":
                        SourceElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "sourceVersion":
                        SourceVersionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "target":
                        TargetElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "targetVersion":
                        TargetVersionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "element":
                        Element = source.GetList<SourceElementComponent>();
                        return true;
                    case "unmapped":
                        Unmapped = source.Get<UnmappedComponent>();
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
                    case "source":
                        SourceElement = source.PopulateValue(SourceElement);
                        return true;
                    case "_source":
                        SourceElement = source.Populate(SourceElement);
                        return true;
                    case "sourceVersion":
                        SourceVersionElement = source.PopulateValue(SourceVersionElement);
                        return true;
                    case "_sourceVersion":
                        SourceVersionElement = source.Populate(SourceVersionElement);
                        return true;
                    case "target":
                        TargetElement = source.PopulateValue(TargetElement);
                        return true;
                    case "_target":
                        TargetElement = source.Populate(TargetElement);
                        return true;
                    case "targetVersion":
                        TargetVersionElement = source.PopulateValue(TargetVersionElement);
                        return true;
                    case "_targetVersion":
                        TargetVersionElement = source.Populate(TargetVersionElement);
                        return true;
                    case "element":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "unmapped":
                        Unmapped = source.Populate(Unmapped);
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
                    case "element":
                        source.PopulateListItem(Element, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SourceElement != null) dest.SourceElement = (Hl7.Fhir.Model.FhirUri)SourceElement.DeepCopy();
                    if(SourceVersionElement != null) dest.SourceVersionElement = (Hl7.Fhir.Model.FhirString)SourceVersionElement.DeepCopy();
                    if(TargetElement != null) dest.TargetElement = (Hl7.Fhir.Model.FhirUri)TargetElement.DeepCopy();
                    if(TargetVersionElement != null) dest.TargetVersionElement = (Hl7.Fhir.Model.FhirString)TargetVersionElement.DeepCopy();
                    if(Element != null) dest.Element = new List<SourceElementComponent>(Element.DeepCopy());
                    if(Unmapped != null) dest.Unmapped = (UnmappedComponent)Unmapped.DeepCopy();
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
                if( !DeepComparable.Matches(SourceElement, otherT.SourceElement)) return false;
                if( !DeepComparable.Matches(SourceVersionElement, otherT.SourceVersionElement)) return false;
                if( !DeepComparable.Matches(TargetElement, otherT.TargetElement)) return false;
                if( !DeepComparable.Matches(TargetVersionElement, otherT.TargetVersionElement)) return false;
                if( !DeepComparable.Matches(Element, otherT.Element)) return false;
                if( !DeepComparable.Matches(Unmapped, otherT.Unmapped)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SourceElement, otherT.SourceElement)) return false;
                if( !DeepComparable.IsExactly(SourceVersionElement, otherT.SourceVersionElement)) return false;
                if( !DeepComparable.IsExactly(TargetElement, otherT.TargetElement)) return false;
                if( !DeepComparable.IsExactly(TargetVersionElement, otherT.TargetVersionElement)) return false;
                if( !DeepComparable.IsExactly(Element, otherT.Element)) return false;
                if( !DeepComparable.IsExactly(Unmapped, otherT.Unmapped)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SourceElement != null) yield return SourceElement;
                    if (SourceVersionElement != null) yield return SourceVersionElement;
                    if (TargetElement != null) yield return TargetElement;
                    if (TargetVersionElement != null) yield return TargetVersionElement;
                    foreach (var elem in Element) { if (elem != null) yield return elem; }
                    if (Unmapped != null) yield return Unmapped;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SourceElement != null) yield return new ElementValue("source", SourceElement);
                    if (SourceVersionElement != null) yield return new ElementValue("sourceVersion", SourceVersionElement);
                    if (TargetElement != null) yield return new ElementValue("target", TargetElement);
                    if (TargetVersionElement != null) yield return new ElementValue("targetVersion", TargetVersionElement);
                    foreach (var elem in Element) { if (elem != null) yield return new ElementValue("element", elem); }
                    if (Unmapped != null) yield return new ElementValue("unmapped", Unmapped);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SourceElementComponent")]
        [DataContract]
        public partial class SourceElementComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IConceptMapSourceElementComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SourceElementComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IConceptMapTargetElementComponent> Hl7.Fhir.Model.IConceptMapSourceElementComponent.Target { get { return Target; } }
            
            /// <summary>
            /// Identifies element being mapped
            /// </summary>
            [FhirElement("code", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Identifies element being mapped
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
                        CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Display for the code
            /// </summary>
            [FhirElement("display", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            /// <summary>
            /// Display for the code
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Display
            {
                get { return DisplayElement != null ? DisplayElement.Value : null; }
                set
                {
                    if (value == null)
                        DisplayElement = null;
                    else
                        DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Display");
                }
            }
            
            /// <summary>
            /// Concept in target system for element
            /// </summary>
            [FhirElement("target", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<TargetElementComponent> Target
            {
                get { if(_Target==null) _Target = new List<TargetElementComponent>(); return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            
            private List<TargetElementComponent> _Target;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SourceElementComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CodeElement?.Serialize(sink);
                sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DisplayElement?.Serialize(sink);
                sink.BeginList("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Target)
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
                    case "code":
                        CodeElement = source.Get<Hl7.Fhir.Model.Code>();
                        return true;
                    case "display":
                        DisplayElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "target":
                        Target = source.GetList<TargetElementComponent>();
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
                    case "display":
                        DisplayElement = source.PopulateValue(DisplayElement);
                        return true;
                    case "_display":
                        DisplayElement = source.Populate(DisplayElement);
                        return true;
                    case "target":
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
                    case "target":
                        source.PopulateListItem(Target, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SourceElementComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    if(Target != null) dest.Target = new List<TargetElementComponent>(Target.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SourceElementComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SourceElementComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SourceElementComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (DisplayElement != null) yield return DisplayElement;
                    foreach (var elem in Target) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                    foreach (var elem in Target) { if (elem != null) yield return new ElementValue("target", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "TargetElementComponent")]
        [DataContract]
        public partial class TargetElementComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IConceptMapTargetElementComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TargetElementComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IConceptMapOtherElementComponent> Hl7.Fhir.Model.IConceptMapTargetElementComponent.DependsOn { get { return DependsOn; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IConceptMapOtherElementComponent> Hl7.Fhir.Model.IConceptMapTargetElementComponent.Product { get { return Product; } }
            
            /// <summary>
            /// Code that identifies the target element
            /// </summary>
            [FhirElement("code", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Code that identifies the target element
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
                        CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Display for the code
            /// </summary>
            [FhirElement("display", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            /// <summary>
            /// Display for the code
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Display
            {
                get { return DisplayElement != null ? DisplayElement.Value : null; }
                set
                {
                    if (value == null)
                        DisplayElement = null;
                    else
                        DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Display");
                }
            }
            
            /// <summary>
            /// relatedto | equivalent | equal | wider | subsumes | narrower | specializes | inexact | unmatched | disjoint
            /// </summary>
            [FhirElement("equivalence", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.ConceptMapEquivalence> EquivalenceElement
            {
                get { return _EquivalenceElement; }
                set { _EquivalenceElement = value; OnPropertyChanged("EquivalenceElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.ConceptMapEquivalence> _EquivalenceElement;
            
            /// <summary>
            /// relatedto | equivalent | equal | wider | subsumes | narrower | specializes | inexact | unmatched | disjoint
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.ConceptMapEquivalence? Equivalence
            {
                get { return EquivalenceElement != null ? EquivalenceElement.Value : null; }
                set
                {
                    if (value == null)
                        EquivalenceElement = null;
                    else
                        EquivalenceElement = new Code<Hl7.Fhir.Model.R4.ConceptMapEquivalence>(value);
                    OnPropertyChanged("Equivalence");
                }
            }
            
            /// <summary>
            /// Description of status/issues in mapping
            /// </summary>
            [FhirElement("comment", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentElement
            {
                get { return _CommentElement; }
                set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CommentElement;
            
            /// <summary>
            /// Description of status/issues in mapping
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
            
            /// <summary>
            /// Other elements required for this mapping (from context)
            /// </summary>
            [FhirElement("dependsOn", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<OtherElementComponent> DependsOn
            {
                get { if(_DependsOn==null) _DependsOn = new List<OtherElementComponent>(); return _DependsOn; }
                set { _DependsOn = value; OnPropertyChanged("DependsOn"); }
            }
            
            private List<OtherElementComponent> _DependsOn;
            
            /// <summary>
            /// Other concepts that this mapping also produces
            /// </summary>
            [FhirElement("product", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<OtherElementComponent> Product
            {
                get { if(_Product==null) _Product = new List<OtherElementComponent>(); return _Product; }
                set { _Product = value; OnPropertyChanged("Product"); }
            }
            
            private List<OtherElementComponent> _Product;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TargetElementComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CodeElement?.Serialize(sink);
                sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DisplayElement?.Serialize(sink);
                sink.Element("equivalence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); EquivalenceElement?.Serialize(sink);
                sink.Element("comment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CommentElement?.Serialize(sink);
                sink.BeginList("dependsOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in DependsOn)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("product", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Product)
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
                    case "code":
                        CodeElement = source.Get<Hl7.Fhir.Model.Code>();
                        return true;
                    case "display":
                        DisplayElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "equivalence":
                        EquivalenceElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.ConceptMapEquivalence>>();
                        return true;
                    case "comment":
                        CommentElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "dependsOn":
                        DependsOn = source.GetList<OtherElementComponent>();
                        return true;
                    case "product":
                        Product = source.GetList<OtherElementComponent>();
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
                    case "display":
                        DisplayElement = source.PopulateValue(DisplayElement);
                        return true;
                    case "_display":
                        DisplayElement = source.Populate(DisplayElement);
                        return true;
                    case "equivalence":
                        EquivalenceElement = source.PopulateValue(EquivalenceElement);
                        return true;
                    case "_equivalence":
                        EquivalenceElement = source.Populate(EquivalenceElement);
                        return true;
                    case "comment":
                        CommentElement = source.PopulateValue(CommentElement);
                        return true;
                    case "_comment":
                        CommentElement = source.Populate(CommentElement);
                        return true;
                    case "dependsOn":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "product":
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
                    case "dependsOn":
                        source.PopulateListItem(DependsOn, index);
                        return true;
                    case "product":
                        source.PopulateListItem(Product, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TargetElementComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    if(EquivalenceElement != null) dest.EquivalenceElement = (Code<Hl7.Fhir.Model.R4.ConceptMapEquivalence>)EquivalenceElement.DeepCopy();
                    if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                    if(DependsOn != null) dest.DependsOn = new List<OtherElementComponent>(DependsOn.DeepCopy());
                    if(Product != null) dest.Product = new List<OtherElementComponent>(Product.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TargetElementComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TargetElementComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.Matches(EquivalenceElement, otherT.EquivalenceElement)) return false;
                if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
                if( !DeepComparable.Matches(DependsOn, otherT.DependsOn)) return false;
                if( !DeepComparable.Matches(Product, otherT.Product)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TargetElementComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.IsExactly(EquivalenceElement, otherT.EquivalenceElement)) return false;
                if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
                if( !DeepComparable.IsExactly(DependsOn, otherT.DependsOn)) return false;
                if( !DeepComparable.IsExactly(Product, otherT.Product)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (DisplayElement != null) yield return DisplayElement;
                    if (EquivalenceElement != null) yield return EquivalenceElement;
                    if (CommentElement != null) yield return CommentElement;
                    foreach (var elem in DependsOn) { if (elem != null) yield return elem; }
                    foreach (var elem in Product) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                    if (EquivalenceElement != null) yield return new ElementValue("equivalence", EquivalenceElement);
                    if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                    foreach (var elem in DependsOn) { if (elem != null) yield return new ElementValue("dependsOn", elem); }
                    foreach (var elem in Product) { if (elem != null) yield return new ElementValue("product", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "OtherElementComponent")]
        [DataContract]
        public partial class OtherElementComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IConceptMapOtherElementComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OtherElementComponent"; } }
            
            /// <summary>
            /// Reference to property mapping depends on
            /// </summary>
            [FhirElement("property", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri PropertyElement
            {
                get { return _PropertyElement; }
                set { _PropertyElement = value; OnPropertyChanged("PropertyElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _PropertyElement;
            
            /// <summary>
            /// Reference to property mapping depends on
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Property
            {
                get { return PropertyElement != null ? PropertyElement.Value : null; }
                set
                {
                    if (value == null)
                        PropertyElement = null;
                    else
                        PropertyElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Property");
                }
            }
            
            /// <summary>
            /// Code System (if necessary)
            /// </summary>
            [FhirElement("system", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical SystemElement
            {
                get { return _SystemElement; }
                set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _SystemElement;
            
            /// <summary>
            /// Code System (if necessary)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string System
            {
                get { return SystemElement != null ? SystemElement.Value : null; }
                set
                {
                    if (value == null)
                        SystemElement = null;
                    else
                        SystemElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("System");
                }
            }
            
            /// <summary>
            /// Value of the referenced element
            /// </summary>
            [FhirElement("value", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Value of the referenced element
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null;
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            /// <summary>
            /// Display for the code (if value is a code)
            /// </summary>
            [FhirElement("display", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            /// <summary>
            /// Display for the code (if value is a code)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Display
            {
                get { return DisplayElement != null ? DisplayElement.Value : null; }
                set
                {
                    if (value == null)
                        DisplayElement = null;
                    else
                        DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Display");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("OtherElementComponent");
                base.Serialize(sink);
                sink.Element("property", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); PropertyElement?.Serialize(sink);
                sink.Element("system", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SystemElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ValueElement?.Serialize(sink);
                sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DisplayElement?.Serialize(sink);
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
                    case "property":
                        PropertyElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "system":
                        SystemElement = source.Get<Hl7.Fhir.Model.Canonical>();
                        return true;
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "display":
                        DisplayElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "property":
                        PropertyElement = source.PopulateValue(PropertyElement);
                        return true;
                    case "_property":
                        PropertyElement = source.Populate(PropertyElement);
                        return true;
                    case "system":
                        SystemElement = source.PopulateValue(SystemElement);
                        return true;
                    case "_system":
                        SystemElement = source.Populate(SystemElement);
                        return true;
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                    case "display":
                        DisplayElement = source.PopulateValue(DisplayElement);
                        return true;
                    case "_display":
                        DisplayElement = source.Populate(DisplayElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OtherElementComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PropertyElement != null) dest.PropertyElement = (Hl7.Fhir.Model.FhirUri)PropertyElement.DeepCopy();
                    if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.Canonical)SystemElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new OtherElementComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OtherElementComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PropertyElement, otherT.PropertyElement)) return false;
                if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OtherElementComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PropertyElement, otherT.PropertyElement)) return false;
                if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (PropertyElement != null) yield return PropertyElement;
                    if (SystemElement != null) yield return SystemElement;
                    if (ValueElement != null) yield return ValueElement;
                    if (DisplayElement != null) yield return DisplayElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (PropertyElement != null) yield return new ElementValue("property", PropertyElement);
                    if (SystemElement != null) yield return new ElementValue("system", SystemElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                    if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "UnmappedComponent")]
        [DataContract]
        public partial class UnmappedComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "UnmappedComponent"; } }
            
            /// <summary>
            /// provided | fixed | other-map
            /// </summary>
            [FhirElement("mode", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ConceptMapGroupUnmappedMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ConceptMapGroupUnmappedMode> _ModeElement;
            
            /// <summary>
            /// provided | fixed | other-map
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ConceptMapGroupUnmappedMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (value == null)
                        ModeElement = null;
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.ConceptMapGroupUnmappedMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Fixed code when mode = fixed
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Fixed code when mode = fixed
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
                        CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Display for the code
            /// </summary>
            [FhirElement("display", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            /// <summary>
            /// Display for the code
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Display
            {
                get { return DisplayElement != null ? DisplayElement.Value : null; }
                set
                {
                    if (value == null)
                        DisplayElement = null;
                    else
                        DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Display");
                }
            }
            
            /// <summary>
            /// canonical reference to an additional ConceptMap to use for mapping if the source concept is unmapped
            /// </summary>
            [FhirElement("url", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _UrlElement;
            
            /// <summary>
            /// canonical reference to an additional ConceptMap to use for mapping if the source concept is unmapped
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("UnmappedComponent");
                base.Serialize(sink);
                sink.Element("mode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ModeElement?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CodeElement?.Serialize(sink);
                sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DisplayElement?.Serialize(sink);
                sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UrlElement?.Serialize(sink);
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
                    case "mode":
                        ModeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ConceptMapGroupUnmappedMode>>();
                        return true;
                    case "code":
                        CodeElement = source.Get<Hl7.Fhir.Model.Code>();
                        return true;
                    case "display":
                        DisplayElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "url":
                        UrlElement = source.Get<Hl7.Fhir.Model.Canonical>();
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
                    case "mode":
                        ModeElement = source.PopulateValue(ModeElement);
                        return true;
                    case "_mode":
                        ModeElement = source.Populate(ModeElement);
                        return true;
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
                        return true;
                    case "display":
                        DisplayElement = source.PopulateValue(DisplayElement);
                        return true;
                    case "_display":
                        DisplayElement = source.Populate(DisplayElement);
                        return true;
                    case "url":
                        UrlElement = source.PopulateValue(UrlElement);
                        return true;
                    case "_url":
                        UrlElement = source.Populate(UrlElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as UnmappedComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.ConceptMapGroupUnmappedMode>)ModeElement.DeepCopy();
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.Canonical)UrlElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new UnmappedComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as UnmappedComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as UnmappedComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ModeElement != null) yield return ModeElement;
                    if (CodeElement != null) yield return CodeElement;
                    if (DisplayElement != null) yield return DisplayElement;
                    if (UrlElement != null) yield return UrlElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                    if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Canonical identifier for this concept map, represented as a URI (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Canonical identifier for this concept map, represented as a URI (globally unique)
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
        /// Additional identifier for the concept map
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Business version of the concept map
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
        /// Business version of the concept map
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
        /// Name for this concept map (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name for this concept map (computer friendly)
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
        /// Name for this concept map (human friendly)
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
        /// Name for this concept map (human friendly)
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
        /// Natural language description of the concept map
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
        /// Natural language description of the concept map
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
        /// Intended jurisdiction for concept map (if applicable)
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
        /// Why this concept map is defined
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
        /// Why this concept map is defined
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
        /// The source value set that contains the concepts that are being mapped
        /// </summary>
        [FhirElement("source", InSummary=Hl7.Fhir.Model.Version.All, Order=240, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Canonical))]
        [DataMember]
        public Hl7.Fhir.Model.Element Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private Hl7.Fhir.Model.Element _Source;
        
        /// <summary>
        /// The target value set which provides context for the mappings
        /// </summary>
        [FhirElement("target", InSummary=Hl7.Fhir.Model.Version.All, Order=250, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Canonical))]
        [DataMember]
        public Hl7.Fhir.Model.Element Target
        {
            get { return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        
        private Hl7.Fhir.Model.Element _Target;
        
        /// <summary>
        /// Same source and target systems
        /// </summary>
        [FhirElement("group", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<GroupComponent> Group
        {
            get { if(_Group==null) _Group = new List<GroupComponent>(); return _Group; }
            set { _Group = value; OnPropertyChanged("Group"); }
        }
        
        private List<GroupComponent> _Group;
    
    
        public static ElementDefinitionConstraint[] ConceptMap_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "cmd-0",
                severity: ConstraintSeverity.Warning,
                expression: "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
                human: "Name should be usable as an identifier for the module by machine processing applications such as code generation",
                xpath: "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "cmd-1",
                severity: ConstraintSeverity.Warning,
                expression: "group.element.target.all(comment.exists() or equivalence.empty() or ((equivalence != 'narrower') and (equivalence != 'inexact')))",
                human: "If the map is narrower or inexact, there SHALL be some comments",
                xpath: "exists(f:comment) or not(exists(f:equivalence)) or ((f:equivalence/@value != 'narrower') and (f:equivalence/@value != 'inexact'))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "cmd-3",
                severity: ConstraintSeverity.Warning,
                expression: "group.unmapped.all((mode = 'other-map') implies url.exists())",
                human: "If the mode is 'other-map', a url must be provided",
                xpath: "(f:mode/@value != 'other-map') or exists(f:url)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "cmd-2",
                severity: ConstraintSeverity.Warning,
                expression: "group.unmapped.all((mode = 'fixed') implies code.exists())",
                human: "If the mode is 'fixed', a code must be provided",
                xpath: "(f:mode/@value != 'fixed') or exists(f:code)"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(ConceptMap_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ConceptMap;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
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
                if(Source != null) dest.Source = (Hl7.Fhir.Model.Element)Source.DeepCopy();
                if(Target != null) dest.Target = (Hl7.Fhir.Model.Element)Target.DeepCopy();
                if(Group != null) dest.Group = new List<GroupComponent>(Group.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ConceptMap());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ConceptMap;
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
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            if( !DeepComparable.Matches(Group, otherT.Group)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ConceptMap;
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
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ConceptMap");
            base.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
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
            sink.Element("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Source?.Serialize(sink);
            sink.Element("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Target?.Serialize(sink);
            sink.BeginList("group", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Group)
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
                case "url":
                    UrlElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "identifier":
                    Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "version":
                    VersionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "name":
                    NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "title":
                    TitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.PublicationStatus>>();
                    return true;
                case "experimental":
                    ExperimentalElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "date":
                    DateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "publisher":
                    PublisherElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "contact":
                    Contact = source.GetList<Hl7.Fhir.Model.R4.ContactDetail>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "useContext":
                    UseContext = source.GetList<Hl7.Fhir.Model.UsageContext>();
                    return true;
                case "jurisdiction":
                    Jurisdiction = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "purpose":
                    PurposeElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "copyright":
                    CopyrightElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "sourceUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Source, "source");
                    Source = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "sourceCanonical":
                    source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Source, "source");
                    Source = source.Get<Hl7.Fhir.Model.Canonical>();
                    return true;
                case "targetUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Target, "target");
                    Target = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "targetCanonical":
                    source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Target, "target");
                    Target = source.Get<Hl7.Fhir.Model.Canonical>();
                    return true;
                case "group":
                    Group = source.GetList<GroupComponent>();
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
                case "url":
                    UrlElement = source.PopulateValue(UrlElement);
                    return true;
                case "_url":
                    UrlElement = source.Populate(UrlElement);
                    return true;
                case "identifier":
                    Identifier = source.Populate(Identifier);
                    return true;
                case "version":
                    VersionElement = source.PopulateValue(VersionElement);
                    return true;
                case "_version":
                    VersionElement = source.Populate(VersionElement);
                    return true;
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
                    return true;
                case "title":
                    TitleElement = source.PopulateValue(TitleElement);
                    return true;
                case "_title":
                    TitleElement = source.Populate(TitleElement);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "experimental":
                    ExperimentalElement = source.PopulateValue(ExperimentalElement);
                    return true;
                case "_experimental":
                    ExperimentalElement = source.Populate(ExperimentalElement);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "publisher":
                    PublisherElement = source.PopulateValue(PublisherElement);
                    return true;
                case "_publisher":
                    PublisherElement = source.Populate(PublisherElement);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "useContext":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "jurisdiction":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "purpose":
                    PurposeElement = source.PopulateValue(PurposeElement);
                    return true;
                case "_purpose":
                    PurposeElement = source.Populate(PurposeElement);
                    return true;
                case "copyright":
                    CopyrightElement = source.PopulateValue(CopyrightElement);
                    return true;
                case "_copyright":
                    CopyrightElement = source.Populate(CopyrightElement);
                    return true;
                case "sourceUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Source, "source");
                    Source = source.PopulateValue(Source as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_sourceUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Source, "source");
                    Source = source.Populate(Source as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "sourceCanonical":
                    source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Source, "source");
                    Source = source.PopulateValue(Source as Hl7.Fhir.Model.Canonical);
                    return true;
                case "_sourceCanonical":
                    source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Source, "source");
                    Source = source.Populate(Source as Hl7.Fhir.Model.Canonical);
                    return true;
                case "targetUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Target, "target");
                    Target = source.PopulateValue(Target as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_targetUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Target, "target");
                    Target = source.Populate(Target as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "targetCanonical":
                    source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Target, "target");
                    Target = source.PopulateValue(Target as Hl7.Fhir.Model.Canonical);
                    return true;
                case "_targetCanonical":
                    source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Target, "target");
                    Target = source.Populate(Target as Hl7.Fhir.Model.Canonical);
                    return true;
                case "group":
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
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "useContext":
                    source.PopulateListItem(UseContext, index);
                    return true;
                case "jurisdiction":
                    source.PopulateListItem(Jurisdiction, index);
                    return true;
                case "group":
                    source.PopulateListItem(Group, index);
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
                if (UrlElement != null) yield return UrlElement;
                if (Identifier != null) yield return Identifier;
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
                if (Source != null) yield return Source;
                if (Target != null) yield return Target;
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
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
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
                if (Source != null) yield return new ElementValue("source", Source);
                if (Target != null) yield return new ElementValue("target", Target);
                foreach (var elem in Group) { if (elem != null) yield return new ElementValue("group", elem); }
            }
        }
    
    }

}
