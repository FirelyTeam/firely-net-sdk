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
    /// A map from one set of concepts to one or more other concepts
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ConceptMap", IsResource=true)]
    [DataContract]
    public partial class ConceptMap : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IConceptMap, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ConceptMap; } }
        [NotMapped]
        public override string TypeName { get { return "ConceptMap"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ContactComponent")]
        [DataContract]
        public partial class ContactComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ContactComponent"; } }
            
            /// <summary>
            /// Name of a individual to contact
            /// </summary>
            [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Name of a individual to contact
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
            /// Contact details for individual or publisher
            /// </summary>
            [FhirElement("telecom", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.DSTU2.ContactPoint> Telecom
            {
                get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.DSTU2.ContactPoint>(); return _Telecom; }
                set { _Telecom = value; OnPropertyChanged("Telecom"); }
            }
            
            private List<Hl7.Fhir.Model.DSTU2.ContactPoint> _Telecom;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ContactComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
                sink.BeginList("telecom", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Telecom)
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
                    case "name":
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "telecom":
                        Telecom = source.GetList<Hl7.Fhir.Model.DSTU2.ContactPoint>();
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
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "telecom":
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
                    case "telecom":
                        source.PopulateListItem(Telecom, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContactComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.DSTU2.ContactPoint>(Telecom.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ContactComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    foreach (var elem in Telecom) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "SourceElementComponent")]
        [DataContract]
        public partial class SourceElementComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IConceptMapSourceElementComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SourceElementComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IConceptMapTargetElementComponent> Hl7.Fhir.Model.IConceptMapSourceElementComponent.Target { get { return Target; } }
            
            /// <summary>
            /// Code System (if value set crosses code systems)
            /// </summary>
            [FhirElement("codeSystem", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri CodeSystemElement
            {
                get { return _CodeSystemElement; }
                set { _CodeSystemElement = value; OnPropertyChanged("CodeSystemElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _CodeSystemElement;
            
            /// <summary>
            /// Code System (if value set crosses code systems)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CodeSystem
            {
                get { return CodeSystemElement != null ? CodeSystemElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeSystemElement = null;
                    else
                        CodeSystemElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("CodeSystem");
                }
            }
            
            /// <summary>
            /// Identifies element being mapped
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
                sink.Element("codeSystem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CodeSystemElement?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CodeElement?.Serialize(sink);
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
                    case "codeSystem":
                        CodeSystemElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "code":
                        CodeElement = source.Get<Hl7.Fhir.Model.Code>();
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
                    case "codeSystem":
                        CodeSystemElement = source.PopulateValue(CodeSystemElement);
                        return true;
                    case "_codeSystem":
                        CodeSystemElement = source.Populate(CodeSystemElement);
                        return true;
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
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
                    if(CodeSystemElement != null) dest.CodeSystemElement = (Hl7.Fhir.Model.FhirUri)CodeSystemElement.DeepCopy();
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
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
                if( !DeepComparable.Matches(CodeSystemElement, otherT.CodeSystemElement)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SourceElementComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeSystemElement, otherT.CodeSystemElement)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeSystemElement != null) yield return CodeSystemElement;
                    if (CodeElement != null) yield return CodeElement;
                    foreach (var elem in Target) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeSystemElement != null) yield return new ElementValue("codeSystem", CodeSystemElement);
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    foreach (var elem in Target) { if (elem != null) yield return new ElementValue("target", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "TargetElementComponent")]
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
            /// System of the target (if necessary)
            /// </summary>
            [FhirElement("codeSystem", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri CodeSystemElement
            {
                get { return _CodeSystemElement; }
                set { _CodeSystemElement = value; OnPropertyChanged("CodeSystemElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _CodeSystemElement;
            
            /// <summary>
            /// System of the target (if necessary)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CodeSystem
            {
                get { return CodeSystemElement != null ? CodeSystemElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeSystemElement = null;
                    else
                        CodeSystemElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("CodeSystem");
                }
            }
            
            /// <summary>
            /// Code that identifies the target element
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
            /// equivalent | equal | wider | subsumes | narrower | specializes | inexact | unmatched | disjoint
            /// </summary>
            [FhirElement("equivalence", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DSTU2.ConceptMapEquivalence> EquivalenceElement
            {
                get { return _EquivalenceElement; }
                set { _EquivalenceElement = value; OnPropertyChanged("EquivalenceElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.ConceptMapEquivalence> _EquivalenceElement;
            
            /// <summary>
            /// equivalent | equal | wider | subsumes | narrower | specializes | inexact | unmatched | disjoint
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.ConceptMapEquivalence? Equivalence
            {
                get { return EquivalenceElement != null ? EquivalenceElement.Value : null; }
                set
                {
                    if (value == null)
                        EquivalenceElement = null;
                    else
                        EquivalenceElement = new Code<Hl7.Fhir.Model.DSTU2.ConceptMapEquivalence>(value);
                    OnPropertyChanged("Equivalence");
                }
            }
            
            /// <summary>
            /// Description of status/issues in mapping
            /// </summary>
            [FhirElement("comments", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CommentsElement
            {
                get { return _CommentsElement; }
                set { _CommentsElement = value; OnPropertyChanged("CommentsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CommentsElement;
            
            /// <summary>
            /// Description of status/issues in mapping
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
                        CommentsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Comments");
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
                sink.Element("codeSystem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CodeSystemElement?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CodeElement?.Serialize(sink);
                sink.Element("equivalence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); EquivalenceElement?.Serialize(sink);
                sink.Element("comments", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CommentsElement?.Serialize(sink);
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
                    case "codeSystem":
                        CodeSystemElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "code":
                        CodeElement = source.Get<Hl7.Fhir.Model.Code>();
                        return true;
                    case "equivalence":
                        EquivalenceElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.ConceptMapEquivalence>>();
                        return true;
                    case "comments":
                        CommentsElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "codeSystem":
                        CodeSystemElement = source.PopulateValue(CodeSystemElement);
                        return true;
                    case "_codeSystem":
                        CodeSystemElement = source.Populate(CodeSystemElement);
                        return true;
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
                        return true;
                    case "equivalence":
                        EquivalenceElement = source.PopulateValue(EquivalenceElement);
                        return true;
                    case "_equivalence":
                        EquivalenceElement = source.Populate(EquivalenceElement);
                        return true;
                    case "comments":
                        CommentsElement = source.PopulateValue(CommentsElement);
                        return true;
                    case "_comments":
                        CommentsElement = source.Populate(CommentsElement);
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
                    if(CodeSystemElement != null) dest.CodeSystemElement = (Hl7.Fhir.Model.FhirUri)CodeSystemElement.DeepCopy();
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(EquivalenceElement != null) dest.EquivalenceElement = (Code<Hl7.Fhir.Model.DSTU2.ConceptMapEquivalence>)EquivalenceElement.DeepCopy();
                    if(CommentsElement != null) dest.CommentsElement = (Hl7.Fhir.Model.FhirString)CommentsElement.DeepCopy();
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
                if( !DeepComparable.Matches(CodeSystemElement, otherT.CodeSystemElement)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(EquivalenceElement, otherT.EquivalenceElement)) return false;
                if( !DeepComparable.Matches(CommentsElement, otherT.CommentsElement)) return false;
                if( !DeepComparable.Matches(DependsOn, otherT.DependsOn)) return false;
                if( !DeepComparable.Matches(Product, otherT.Product)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TargetElementComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeSystemElement, otherT.CodeSystemElement)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(EquivalenceElement, otherT.EquivalenceElement)) return false;
                if( !DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;
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
                    if (CodeSystemElement != null) yield return CodeSystemElement;
                    if (CodeElement != null) yield return CodeElement;
                    if (EquivalenceElement != null) yield return EquivalenceElement;
                    if (CommentsElement != null) yield return CommentsElement;
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
                    if (CodeSystemElement != null) yield return new ElementValue("codeSystem", CodeSystemElement);
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (EquivalenceElement != null) yield return new ElementValue("equivalence", EquivalenceElement);
                    if (CommentsElement != null) yield return new ElementValue("comments", CommentsElement);
                    foreach (var elem in DependsOn) { if (elem != null) yield return new ElementValue("dependsOn", elem); }
                    foreach (var elem in Product) { if (elem != null) yield return new ElementValue("product", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "OtherElementComponent")]
        [DataContract]
        public partial class OtherElementComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IConceptMapOtherElementComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OtherElementComponent"; } }
            
            /// <summary>
            /// Reference to element/field/ValueSet mapping depends on
            /// </summary>
            [FhirElement("element", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri ElementElement
            {
                get { return _ElementElement; }
                set { _ElementElement = value; OnPropertyChanged("ElementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _ElementElement;
            
            /// <summary>
            /// Reference to element/field/ValueSet mapping depends on
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
                        ElementElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Element");
                }
            }
            
            /// <summary>
            /// Code System (if necessary)
            /// </summary>
            [FhirElement("codeSystem", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri CodeSystemElement
            {
                get { return _CodeSystemElement; }
                set { _CodeSystemElement = value; OnPropertyChanged("CodeSystemElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _CodeSystemElement;
            
            /// <summary>
            /// Code System (if necessary)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CodeSystem
            {
                get { return CodeSystemElement != null ? CodeSystemElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeSystemElement = null;
                    else
                        CodeSystemElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("CodeSystem");
                }
            }
            
            /// <summary>
            /// Value of the referenced element
            /// </summary>
            [FhirElement("code", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CodeElement;
            
            /// <summary>
            /// Value of the referenced element
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
                        CodeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Code");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("OtherElementComponent");
                base.Serialize(sink);
                sink.Element("element", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ElementElement?.Serialize(sink);
                sink.Element("codeSystem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); CodeSystemElement?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); CodeElement?.Serialize(sink);
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
                    case "element":
                        ElementElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "codeSystem":
                        CodeSystemElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "code":
                        CodeElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "element":
                        ElementElement = source.PopulateValue(ElementElement);
                        return true;
                    case "_element":
                        ElementElement = source.Populate(ElementElement);
                        return true;
                    case "codeSystem":
                        CodeSystemElement = source.PopulateValue(CodeSystemElement);
                        return true;
                    case "_codeSystem":
                        CodeSystemElement = source.Populate(CodeSystemElement);
                        return true;
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
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
                    if(ElementElement != null) dest.ElementElement = (Hl7.Fhir.Model.FhirUri)ElementElement.DeepCopy();
                    if(CodeSystemElement != null) dest.CodeSystemElement = (Hl7.Fhir.Model.FhirUri)CodeSystemElement.DeepCopy();
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.FhirString)CodeElement.DeepCopy();
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
                if( !DeepComparable.Matches(ElementElement, otherT.ElementElement)) return false;
                if( !DeepComparable.Matches(CodeSystemElement, otherT.CodeSystemElement)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OtherElementComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ElementElement, otherT.ElementElement)) return false;
                if( !DeepComparable.IsExactly(CodeSystemElement, otherT.CodeSystemElement)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ElementElement != null) yield return ElementElement;
                    if (CodeSystemElement != null) yield return CodeSystemElement;
                    if (CodeElement != null) yield return CodeElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ElementElement != null) yield return new ElementValue("element", ElementElement);
                    if (CodeSystemElement != null) yield return new ElementValue("codeSystem", CodeSystemElement);
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Globally unique logical id for concept map
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
        /// Globally unique logical id for concept map
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
        /// Logical id for this version of the concept map
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
        /// Logical id for this version of the concept map
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
        /// Informal name for this concept map
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
        /// Informal name for this concept map
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
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
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
                if (value == null)
                    ExperimentalElement = null;
                else
                    ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
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
        /// Contact details of the publisher
        /// </summary>
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactComponent> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactComponent>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactComponent> _Contact;
        
        /// <summary>
        /// Date for given status
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date for given status
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
        /// Human language description of the concept map
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Human language description of the concept map
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
        /// Content intends to support these contexts
        /// </summary>
        [FhirElement("useContext", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _UseContext;
        
        /// <summary>
        /// Why needed
        /// </summary>
        [FhirElement("requirements", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RequirementsElement
        {
            get { return _RequirementsElement; }
            set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RequirementsElement;
        
        /// <summary>
        /// Why needed
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
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CopyrightElement
        {
            get { return _CopyrightElement; }
            set { _CopyrightElement = value; OnPropertyChanged("CopyrightElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CopyrightElement;
        
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
                    CopyrightElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Copyright");
            }
        }
        
        /// <summary>
        /// Identifies the source of the concepts which are being mapped
        /// </summary>
        [FhirElement("source", InSummary=Hl7.Fhir.Model.Version.All, Order=220, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private Hl7.Fhir.Model.Element _Source;
        
        /// <summary>
        /// Provides context to the mappings
        /// </summary>
        [FhirElement("target", InSummary=Hl7.Fhir.Model.Version.All, Order=230, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Target
        {
            get { return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        
        private Hl7.Fhir.Model.Element _Target;
        
        /// <summary>
        /// Mappings for a concept from the source set
        /// </summary>
        [FhirElement("element", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SourceElementComponent> Element
        {
            get { if(_Element==null) _Element = new List<SourceElementComponent>(); return _Element; }
            set { _Element = value; OnPropertyChanged("Element"); }
        }
        
        private List<SourceElementComponent> _Element;
    
    
        public static ElementDefinitionConstraint[] ConceptMap_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "cmd-1",
                severity: ConstraintSeverity.Warning,
                expression: "element.target.all(comments or ((equivalence != 'narrower') and (equivalence != 'inexact')))",
                human: "If the map is narrower or inexact, there SHALL be some comments",
                xpath: "exists(f:comments) or ((f:equivalence/@value != 'narrower') and (f:equivalence/@value != 'inexact'))"
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
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<ContactComponent>(Contact.DeepCopy());
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(UseContext.DeepCopy());
                if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.FhirString)CopyrightElement.DeepCopy();
                if(Source != null) dest.Source = (Hl7.Fhir.Model.Element)Source.DeepCopy();
                if(Target != null) dest.Target = (Hl7.Fhir.Model.Element)Target.DeepCopy();
                if(Element != null) dest.Element = new List<SourceElementComponent>(Element.DeepCopy());
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
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            if( !DeepComparable.Matches(Element, otherT.Element)) return false;
        
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
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            if( !DeepComparable.IsExactly(Element, otherT.Element)) return false;
        
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
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("experimental", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExperimentalElement?.Serialize(sink);
            sink.Element("publisher", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PublisherElement?.Serialize(sink);
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
            sink.BeginList("useContext", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in UseContext)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("requirements", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequirementsElement?.Serialize(sink);
            sink.Element("copyright", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CopyrightElement?.Serialize(sink);
            sink.Element("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Source?.Serialize(sink);
            sink.Element("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Target?.Serialize(sink);
            sink.BeginList("element", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Element)
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
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus>>();
                    return true;
                case "experimental":
                    ExperimentalElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "publisher":
                    PublisherElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "contact":
                    Contact = source.GetList<ContactComponent>();
                    return true;
                case "date":
                    DateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "useContext":
                    UseContext = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "requirements":
                    RequirementsElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "copyright":
                    CopyrightElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "sourceUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Source, "source");
                    Source = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "sourceReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Source, "source");
                    Source = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "targetUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Target, "target");
                    Target = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "targetReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Target, "target");
                    Target = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "element":
                    Element = source.GetList<SourceElementComponent>();
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
                case "publisher":
                    PublisherElement = source.PopulateValue(PublisherElement);
                    return true;
                case "_publisher":
                    PublisherElement = source.Populate(PublisherElement);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
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
                case "requirements":
                    RequirementsElement = source.PopulateValue(RequirementsElement);
                    return true;
                case "_requirements":
                    RequirementsElement = source.Populate(RequirementsElement);
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
                case "sourceReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Source, "source");
                    Source = source.Populate(Source as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "targetUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Target, "target");
                    Target = source.PopulateValue(Target as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_targetUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Target, "target");
                    Target = source.Populate(Target as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "targetReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Target, "target");
                    Target = source.Populate(Target as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "element":
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
                case "element":
                    source.PopulateListItem(Element, index);
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
                if (StatusElement != null) yield return StatusElement;
                if (ExperimentalElement != null) yield return ExperimentalElement;
                if (PublisherElement != null) yield return PublisherElement;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (DateElement != null) yield return DateElement;
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                if (RequirementsElement != null) yield return RequirementsElement;
                if (CopyrightElement != null) yield return CopyrightElement;
                if (Source != null) yield return Source;
                if (Target != null) yield return Target;
                foreach (var elem in Element) { if (elem != null) yield return elem; }
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
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                if (RequirementsElement != null) yield return new ElementValue("requirements", RequirementsElement);
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                if (Source != null) yield return new ElementValue("source", Source);
                if (Target != null) yield return new ElementValue("target", Target);
                foreach (var elem in Element) { if (elem != null) yield return new ElementValue("element", elem); }
            }
        }
    
    }

}
