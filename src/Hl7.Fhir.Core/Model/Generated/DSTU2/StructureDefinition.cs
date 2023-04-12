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
    /// Structural Definition
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "StructureDefinition", IsResource=true)]
    [DataContract]
    public partial class StructureDefinition : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IStructureDefinition, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.StructureDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "StructureDefinition"; } }
    
    
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "MappingComponent")]
        [DataContract]
        public partial class MappingComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IStructureDefinitionMappingComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "MappingComponent"; } }
            
            /// <summary>
            /// Internal id when this mapping is used
            /// </summary>
            [FhirElement("identity", Order=40)]
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
                    if (value == null)
                        IdentityElement = null;
                    else
                        IdentityElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Identity");
                }
            }
            
            /// <summary>
            /// Identifies what this mapping refers to
            /// </summary>
            [FhirElement("uri", Order=50)]
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
                    if (value == null)
                        UriElement = null;
                    else
                        UriElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Uri");
                }
            }
            
            /// <summary>
            /// Names what this mapping refers to
            /// </summary>
            [FhirElement("name", Order=60)]
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
                    if (value == null)
                        NameElement = null;
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Versions, Issues, Scope limitations etc.
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
            /// Versions, Issues, Scope limitations etc.
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("MappingComponent");
                base.Serialize(sink);
                sink.Element("identity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); IdentityElement?.Serialize(sink);
                sink.Element("uri", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UriElement?.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NameElement?.Serialize(sink);
                sink.Element("comments", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CommentsElement?.Serialize(sink);
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
                    case "uri":
                        UriElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "name":
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "comments":
                        CommentsElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "uri":
                        UriElement = source.PopulateValue(UriElement);
                        return true;
                    case "_uri":
                        UriElement = source.Populate(UriElement);
                        return true;
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "comments":
                        CommentsElement = source.PopulateValue(CommentsElement);
                        return true;
                    case "_comments":
                        CommentsElement = source.Populate(CommentsElement);
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
                 return CopyTo(new MappingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MappingComponent;
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
                var otherT = other as MappingComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IdentityElement, otherT.IdentityElement)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (IdentityElement != null) yield return IdentityElement;
                    if (UriElement != null) yield return UriElement;
                    if (NameElement != null) yield return NameElement;
                    if (CommentsElement != null) yield return CommentsElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (IdentityElement != null) yield return new ElementValue("identity", IdentityElement);
                    if (UriElement != null) yield return new ElementValue("uri", UriElement);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (CommentsElement != null) yield return new ElementValue("comments", CommentsElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "SnapshotComponent")]
        [DataContract]
        public partial class SnapshotComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IStructureDefinitionSnapshotComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SnapshotComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IElementDefinition> Hl7.Fhir.Model.IStructureDefinitionSnapshotComponent.Element { get { return Element; } }
            
            /// <summary>
            /// Definition of elements in the resource (if no StructureDefinition)
            /// </summary>
            [FhirElement("element", Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.DSTU2.ElementDefinition> Element
            {
                get { if(_Element==null) _Element = new List<Hl7.Fhir.Model.DSTU2.ElementDefinition>(); return _Element; }
                set { _Element = value; OnPropertyChanged("Element"); }
            }
            
            private List<Hl7.Fhir.Model.DSTU2.ElementDefinition> _Element;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SnapshotComponent");
                base.Serialize(sink);
                sink.BeginList("element", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
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
                    case "element":
                        Element = source.GetList<Hl7.Fhir.Model.DSTU2.ElementDefinition>();
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
                    case "element":
                        source.PopulateListItem(Element, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SnapshotComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Element != null) dest.Element = new List<Hl7.Fhir.Model.DSTU2.ElementDefinition>(Element.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SnapshotComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SnapshotComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Element, otherT.Element)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SnapshotComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Element, otherT.Element)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Element) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Element) { if (elem != null) yield return new ElementValue("element", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "DifferentialComponent")]
        [DataContract]
        public partial class DifferentialComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IStructureDefinitionDifferentialComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DifferentialComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IElementDefinition> Hl7.Fhir.Model.IStructureDefinitionDifferentialComponent.Element { get { return Element; } }
            
            /// <summary>
            /// Definition of elements in the resource (if no StructureDefinition)
            /// </summary>
            [FhirElement("element", Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.DSTU2.ElementDefinition> Element
            {
                get { if(_Element==null) _Element = new List<Hl7.Fhir.Model.DSTU2.ElementDefinition>(); return _Element; }
                set { _Element = value; OnPropertyChanged("Element"); }
            }
            
            private List<Hl7.Fhir.Model.DSTU2.ElementDefinition> _Element;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DifferentialComponent");
                base.Serialize(sink);
                sink.BeginList("element", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
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
                    case "element":
                        Element = source.GetList<Hl7.Fhir.Model.DSTU2.ElementDefinition>();
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
                    case "element":
                        source.PopulateListItem(Element, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DifferentialComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Element != null) dest.Element = new List<Hl7.Fhir.Model.DSTU2.ElementDefinition>(Element.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DifferentialComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DifferentialComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Element, otherT.Element)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DifferentialComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Element, otherT.Element)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Element) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Element) { if (elem != null) yield return new ElementValue("element", elem); }
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IStructureDefinitionMappingComponent> Hl7.Fhir.Model.IStructureDefinition.Mapping { get { return Mapping; } }
        
        [NotMapped]
        Hl7.Fhir.Model.IStructureDefinitionSnapshotComponent Hl7.Fhir.Model.IStructureDefinition.Snapshot { get { return Snapshot; } }
        
        [NotMapped]
        Hl7.Fhir.Model.IStructureDefinitionDifferentialComponent Hl7.Fhir.Model.IStructureDefinition.Differential { get { return Differential; } }
    
        
        /// <summary>
        /// Absolute URL used to reference this StructureDefinition
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
        /// Absolute URL used to reference this StructureDefinition
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
        /// Other identifiers for the StructureDefinition
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
        /// Logical id for this version of the StructureDefinition
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
        /// Logical id for this version of the StructureDefinition
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
        /// Informal name for this StructureDefinition
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
        /// Informal name for this StructureDefinition
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
        /// Use this name when displaying the value
        /// </summary>
        [FhirElement("display", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
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
                if (value == null)
                    DisplayElement = null;
                else
                    DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Display");
            }
        }
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
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
        /// Name of the publisher (Organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
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
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
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
        /// Date for this version of the StructureDefinition
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date for this version of the StructureDefinition
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
        /// Natural language description of the StructureDefinition
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Natural language description of the StructureDefinition
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
        [FhirElement("useContext", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
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
        /// Scope and Usage this structure definition is for
        /// </summary>
        [FhirElement("requirements", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RequirementsElement
        {
            get { return _RequirementsElement; }
            set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RequirementsElement;
        
        /// <summary>
        /// Scope and Usage this structure definition is for
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
        [FhirElement("copyright", Order=220)]
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
        /// Assist with indexing and finding
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
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
        /// FHIR Version this StructureDefinition targets
        /// </summary>
        [FhirElement("fhirVersion", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Id FhirVersionElement
        {
            get { return _FhirVersionElement; }
            set { _FhirVersionElement = value; OnPropertyChanged("FhirVersionElement"); }
        }
        
        private Hl7.Fhir.Model.Id _FhirVersionElement;
        
        /// <summary>
        /// FHIR Version this StructureDefinition targets
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string FhirVersion
        {
            get { return FhirVersionElement != null ? FhirVersionElement.Value : null; }
            set
            {
                if (value == null)
                    FhirVersionElement = null;
                else
                    FhirVersionElement = new Hl7.Fhir.Model.Id(value);
                OnPropertyChanged("FhirVersion");
            }
        }
        
        /// <summary>
        /// External specification that the content is mapped to
        /// </summary>
        [FhirElement("mapping", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<MappingComponent> Mapping
        {
            get { if(_Mapping==null) _Mapping = new List<MappingComponent>(); return _Mapping; }
            set { _Mapping = value; OnPropertyChanged("Mapping"); }
        }
        
        private List<MappingComponent> _Mapping;
        
        /// <summary>
        /// datatype | resource | logical
        /// </summary>
        [FhirElement("kind", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.StructureDefinitionKind> KindElement
        {
            get { return _KindElement; }
            set { _KindElement = value; OnPropertyChanged("KindElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.StructureDefinitionKind> _KindElement;
        
        /// <summary>
        /// datatype | resource | logical
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.StructureDefinitionKind? Kind
        {
            get { return KindElement != null ? KindElement.Value : null; }
            set
            {
                if (value == null)
                    KindElement = null;
                else
                    KindElement = new Code<Hl7.Fhir.Model.DSTU2.StructureDefinitionKind>(value);
                OnPropertyChanged("Kind");
            }
        }
        
        /// <summary>
        /// Any datatype or resource, including abstract ones
        /// </summary>
        [FhirElement("constrainedType", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType> ConstrainedTypeElement
        {
            get { return _ConstrainedTypeElement; }
            set { _ConstrainedTypeElement = value; OnPropertyChanged("ConstrainedTypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType> _ConstrainedTypeElement;
        
        /// <summary>
        /// Any datatype or resource, including abstract ones
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.FHIRDefinedType? ConstrainedType
        {
            get { return ConstrainedTypeElement != null ? ConstrainedTypeElement.Value : null; }
            set
            {
                if (value == null)
                    ConstrainedTypeElement = null;
                else
                    ConstrainedTypeElement = new Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType>(value);
                OnPropertyChanged("ConstrainedType");
            }
        }
        
        /// <summary>
        /// Whether the structure is abstract
        /// </summary>
        [FhirElement("abstract", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean AbstractElement
        {
            get { return _AbstractElement; }
            set { _AbstractElement = value; OnPropertyChanged("AbstractElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _AbstractElement;
        
        /// <summary>
        /// Whether the structure is abstract
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Abstract
        {
            get { return AbstractElement != null ? AbstractElement.Value : null; }
            set
            {
                if (value == null)
                    AbstractElement = null;
                else
                    AbstractElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Abstract");
            }
        }
        
        /// <summary>
        /// resource | datatype | mapping | extension
        /// </summary>
        [FhirElement("contextType", InSummary=Hl7.Fhir.Model.Version.All, Order=290)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.ExtensionContext> ContextTypeElement
        {
            get { return _ContextTypeElement; }
            set { _ContextTypeElement = value; OnPropertyChanged("ContextTypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.ExtensionContext> _ContextTypeElement;
        
        /// <summary>
        /// resource | datatype | mapping | extension
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.ExtensionContext? ContextType
        {
            get { return ContextTypeElement != null ? ContextTypeElement.Value : null; }
            set
            {
                if (value == null)
                    ContextTypeElement = null;
                else
                    ContextTypeElement = new Code<Hl7.Fhir.Model.DSTU2.ExtensionContext>(value);
                OnPropertyChanged("ContextType");
            }
        }
        
        /// <summary>
        /// Where the extension can be used in instances
        /// </summary>
        [FhirElement("context", InSummary=Hl7.Fhir.Model.Version.All, Order=300)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> ContextElement
        {
            get { if(_ContextElement==null) _ContextElement = new List<Hl7.Fhir.Model.FhirString>(); return _ContextElement; }
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
                if (value == null)
                    ContextElement = null;
                else
                    ContextElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Context");
            }
        }
        
        /// <summary>
        /// Structure that this set of constraints applies to
        /// </summary>
        [FhirElement("base", InSummary=Hl7.Fhir.Model.Version.All, Order=310)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri BaseElement
        {
            get { return _BaseElement; }
            set { _BaseElement = value; OnPropertyChanged("BaseElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _BaseElement;
        
        /// <summary>
        /// Structure that this set of constraints applies to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Base
        {
            get { return BaseElement != null ? BaseElement.Value : null; }
            set
            {
                if (value == null)
                    BaseElement = null;
                else
                    BaseElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Base");
            }
        }
        
        /// <summary>
        /// Snapshot view of the structure
        /// </summary>
        [FhirElement("snapshot", Order=320)]
        [DataMember]
        public SnapshotComponent Snapshot
        {
            get { return _Snapshot; }
            set { _Snapshot = value; OnPropertyChanged("Snapshot"); }
        }
        
        private SnapshotComponent _Snapshot;
        
        /// <summary>
        /// Differential view of the structure
        /// </summary>
        [FhirElement("differential", Order=330)]
        [DataMember]
        public DifferentialComponent Differential
        {
            get { return _Differential; }
            set { _Differential = value; OnPropertyChanged("Differential"); }
        }
        
        private DifferentialComponent _Differential;
    
    
        public static ElementDefinitionConstraint[] StructureDefinition_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "sdf-11",
                severity: ConstraintSeverity.Warning,
                expression: "constrainedType.empty() or snapshot.empty() or snapshot.element.first().path = constrainedType",
                human: "If there's a constrained type, its content must match the path name in the first element of a snapshot",
                xpath: "not(exists(f:constrainedType)) or not(exists(f:snapshot)) or (f:constrainedType/@value = f:snapshot/f:element[1]/f:path/@value)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "sdf-5",
                severity: ConstraintSeverity.Warning,
                expression: "constrainedType != 'Extension' or (context and contextType)",
                human: "If the structure defines an extension then the structure must have context information",
                xpath: "not(f:constrainedType/@value = 'extension') or (exists(f:context) and exists(f:contextType))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "sdf-12",
                severity: ConstraintSeverity.Warning,
                expression: "snapshot.element.base implies base",
                human: "element.base cannot appear if there is no base on the structure definition",
                xpath: "f:base or not(exists(f:snapshot/f:element/f:base) or exists(f:differential/f:element/f:base))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "sdf-4",
                severity: ConstraintSeverity.Warning,
                expression: "base or abstract = 'true'",
                human: "A structure must have a base unless abstract = true",
                xpath: "(f:abstract/@value =true()) or exists(f:base)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "sdf-7",
                severity: ConstraintSeverity.Warning,
                expression: "constrainedType or url = 'http://hl7.org/fhir/StructureDefinition/'+id",
                human: "If the structure describes a base Resource or Type, the URL has to start with \"http://hl7.org/fhir/StructureDefinition/\" and the tail must match the id",
                xpath: "exists(f:constrainedType) or f:url/@value=concat('http://hl7.org/fhir/StructureDefinition/', f:id/@value)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "sdf-6",
                severity: ConstraintSeverity.Warning,
                expression: "snapshot or differential",
                human: "A structure must have either a differential, or a snapshot (or both)",
                xpath: "exists(f:snapshot) or exists(f:differential)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "sdf-1",
                severity: ConstraintSeverity.Warning,
                expression: "constrainedType or snapshot.element.select(path).isDistinct()",
                human: "Element paths must be unique unless the structure is a constraint",
                xpath: "exists(f:constrainedType) or (count(f:snapshot/f:element) = count(distinct-values(f:snapshot/f:element/f:path/@value)))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "sdf-8",
                severity: ConstraintSeverity.Warning,
                expression: "snapshot.element.tail().all(path.startsWith(%context.snapshot.element.first().path+'.')) and differential.element.tail().all(path.startsWith(%context.differential.element.first().path+'.'))",
                human: "In any snapshot or differential, all the elements except the first have to have a path that starts with the path of the first + \".\"",
                xpath: "string-join(for $elementName in f:*[self::f:snapshot or self::f:differential]/f:element[position()>1]/f:path/@value return if (starts-with($elementName, concat($elementName/ancestor::f:element/parent::f:*/f:element[1]/f:path/@value, '.'))) then '' else $elementName,'')=''"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "sdf-9",
                severity: ConstraintSeverity.Warning,
                expression: "children().element.first().label.empty() and children().element.first().code.empty() and children().element.first().requirements.empty()",
                human: "In any snapshot or differential, no label, code or requirements on the an element without a \".\" in the path (e.g. the first element)",
                xpath: "not(exists(f:snapshot/f:element[not(contains(f:path/@value, '.')) and (f:label or f:code or f:requirements)])) and not(exists(f:differential/f:element[not(contains(f:path/@value, '.')) and (f:label or f:code or f:requirements)]))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "sdf-10",
                severity: ConstraintSeverity.Warning,
                expression: "base or abstract = 'true' or constrainedType",
                human: "If the structure is not abstract, or there's a constrained type, then there SHALL be a base",
                xpath: "(f:abstract/@value=true()) or not(exists(f:constrainedType)) or exists(f:base)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "sdf-2",
                severity: ConstraintSeverity.Warning,
                expression: "mapping.all(name or uri)",
                human: "Must have at a name or a uri (or both)",
                xpath: "exists(f:uri) or exists(f:name)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "sdf-3",
                severity: ConstraintSeverity.Warning,
                expression: "snapshot.all(element.all(definition and min and max))",
                human: "Each element definition in a snapshot must have a formal definition and cardinalities",
                xpath: "count(f:element) = count(f:element[exists(f:definition) and exists(f:min) and exists(f:max)])"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(StructureDefinition_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as StructureDefinition;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<ContactComponent>(Contact.DeepCopy());
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(UseContext.DeepCopy());
                if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.FhirString)CopyrightElement.DeepCopy();
                if(Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                if(FhirVersionElement != null) dest.FhirVersionElement = (Hl7.Fhir.Model.Id)FhirVersionElement.DeepCopy();
                if(Mapping != null) dest.Mapping = new List<MappingComponent>(Mapping.DeepCopy());
                if(KindElement != null) dest.KindElement = (Code<Hl7.Fhir.Model.DSTU2.StructureDefinitionKind>)KindElement.DeepCopy();
                if(ConstrainedTypeElement != null) dest.ConstrainedTypeElement = (Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType>)ConstrainedTypeElement.DeepCopy();
                if(AbstractElement != null) dest.AbstractElement = (Hl7.Fhir.Model.FhirBoolean)AbstractElement.DeepCopy();
                if(ContextTypeElement != null) dest.ContextTypeElement = (Code<Hl7.Fhir.Model.DSTU2.ExtensionContext>)ContextTypeElement.DeepCopy();
                if(ContextElement != null) dest.ContextElement = new List<Hl7.Fhir.Model.FhirString>(ContextElement.DeepCopy());
                if(BaseElement != null) dest.BaseElement = (Hl7.Fhir.Model.FhirUri)BaseElement.DeepCopy();
                if(Snapshot != null) dest.Snapshot = (SnapshotComponent)Snapshot.DeepCopy();
                if(Differential != null) dest.Differential = (DifferentialComponent)Differential.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new StructureDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as StructureDefinition;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.Matches(Mapping, otherT.Mapping)) return false;
            if( !DeepComparable.Matches(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.Matches(ConstrainedTypeElement, otherT.ConstrainedTypeElement)) return false;
            if( !DeepComparable.Matches(AbstractElement, otherT.AbstractElement)) return false;
            if( !DeepComparable.Matches(ContextTypeElement, otherT.ContextTypeElement)) return false;
            if( !DeepComparable.Matches(ContextElement, otherT.ContextElement)) return false;
            if( !DeepComparable.Matches(BaseElement, otherT.BaseElement)) return false;
            if( !DeepComparable.Matches(Snapshot, otherT.Snapshot)) return false;
            if( !DeepComparable.Matches(Differential, otherT.Differential)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as StructureDefinition;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.IsExactly(Mapping, otherT.Mapping)) return false;
            if( !DeepComparable.IsExactly(KindElement, otherT.KindElement)) return false;
            if( !DeepComparable.IsExactly(ConstrainedTypeElement, otherT.ConstrainedTypeElement)) return false;
            if( !DeepComparable.IsExactly(AbstractElement, otherT.AbstractElement)) return false;
            if( !DeepComparable.IsExactly(ContextTypeElement, otherT.ContextTypeElement)) return false;
            if( !DeepComparable.IsExactly(ContextElement, otherT.ContextElement)) return false;
            if( !DeepComparable.IsExactly(BaseElement, otherT.BaseElement)) return false;
            if( !DeepComparable.IsExactly(Snapshot, otherT.Snapshot)) return false;
            if( !DeepComparable.IsExactly(Differential, otherT.Differential)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("StructureDefinition");
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
            sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DisplayElement?.Serialize(sink);
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
            sink.BeginList("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Code)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("fhirVersion", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); FhirVersionElement?.Serialize(sink);
            sink.BeginList("mapping", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Mapping)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("kind", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); KindElement?.Serialize(sink);
            sink.Element("constrainedType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ConstrainedTypeElement?.Serialize(sink);
            sink.Element("abstract", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); AbstractElement?.Serialize(sink);
            sink.Element("contextType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ContextTypeElement?.Serialize(sink);
            sink.BeginList("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(ContextElement);
            sink.End();
            sink.Element("base", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); BaseElement?.Serialize(sink);
            sink.Element("snapshot", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Snapshot?.Serialize(sink);
            sink.Element("differential", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Differential?.Serialize(sink);
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
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "version":
                    VersionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "name":
                    NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "display":
                    DisplayElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                case "code":
                    Code = source.GetList<Hl7.Fhir.Model.Coding>();
                    return true;
                case "fhirVersion":
                    FhirVersionElement = source.Get<Hl7.Fhir.Model.Id>();
                    return true;
                case "mapping":
                    Mapping = source.GetList<MappingComponent>();
                    return true;
                case "kind":
                    KindElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.StructureDefinitionKind>>();
                    return true;
                case "constrainedType":
                    ConstrainedTypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType>>();
                    return true;
                case "abstract":
                    AbstractElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "contextType":
                    ContextTypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.ExtensionContext>>();
                    return true;
                case "context":
                    ContextElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "base":
                    BaseElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "snapshot":
                    Snapshot = source.Get<SnapshotComponent>();
                    return true;
                case "differential":
                    Differential = source.Get<DifferentialComponent>();
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
                    source.SetList(this, jsonPropertyName);
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
                case "display":
                    DisplayElement = source.PopulateValue(DisplayElement);
                    return true;
                case "_display":
                    DisplayElement = source.Populate(DisplayElement);
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
                case "code":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "fhirVersion":
                    FhirVersionElement = source.PopulateValue(FhirVersionElement);
                    return true;
                case "_fhirVersion":
                    FhirVersionElement = source.Populate(FhirVersionElement);
                    return true;
                case "mapping":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "kind":
                    KindElement = source.PopulateValue(KindElement);
                    return true;
                case "_kind":
                    KindElement = source.Populate(KindElement);
                    return true;
                case "constrainedType":
                    ConstrainedTypeElement = source.PopulateValue(ConstrainedTypeElement);
                    return true;
                case "_constrainedType":
                    ConstrainedTypeElement = source.Populate(ConstrainedTypeElement);
                    return true;
                case "abstract":
                    AbstractElement = source.PopulateValue(AbstractElement);
                    return true;
                case "_abstract":
                    AbstractElement = source.Populate(AbstractElement);
                    return true;
                case "contextType":
                    ContextTypeElement = source.PopulateValue(ContextTypeElement);
                    return true;
                case "_contextType":
                    ContextTypeElement = source.Populate(ContextTypeElement);
                    return true;
                case "context":
                case "_context":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "base":
                    BaseElement = source.PopulateValue(BaseElement);
                    return true;
                case "_base":
                    BaseElement = source.Populate(BaseElement);
                    return true;
                case "snapshot":
                    Snapshot = source.Populate(Snapshot);
                    return true;
                case "differential":
                    Differential = source.Populate(Differential);
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
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "useContext":
                    source.PopulateListItem(UseContext, index);
                    return true;
                case "code":
                    source.PopulateListItem(Code, index);
                    return true;
                case "mapping":
                    source.PopulateListItem(Mapping, index);
                    return true;
                case "context":
                    source.PopulatePrimitiveListItemValue(ContextElement, index);
                    return true;
                case "_context":
                    source.PopulatePrimitiveListItem(ContextElement, index);
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
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (VersionElement != null) yield return VersionElement;
                if (NameElement != null) yield return NameElement;
                if (DisplayElement != null) yield return DisplayElement;
                if (StatusElement != null) yield return StatusElement;
                if (ExperimentalElement != null) yield return ExperimentalElement;
                if (PublisherElement != null) yield return PublisherElement;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (DateElement != null) yield return DateElement;
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                if (RequirementsElement != null) yield return RequirementsElement;
                if (CopyrightElement != null) yield return CopyrightElement;
                foreach (var elem in Code) { if (elem != null) yield return elem; }
                if (FhirVersionElement != null) yield return FhirVersionElement;
                foreach (var elem in Mapping) { if (elem != null) yield return elem; }
                if (KindElement != null) yield return KindElement;
                if (ConstrainedTypeElement != null) yield return ConstrainedTypeElement;
                if (AbstractElement != null) yield return AbstractElement;
                if (ContextTypeElement != null) yield return ContextTypeElement;
                foreach (var elem in ContextElement) { if (elem != null) yield return elem; }
                if (BaseElement != null) yield return BaseElement;
                if (Snapshot != null) yield return Snapshot;
                if (Differential != null) yield return Differential;
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
                if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                if (RequirementsElement != null) yield return new ElementValue("requirements", RequirementsElement);
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                if (FhirVersionElement != null) yield return new ElementValue("fhirVersion", FhirVersionElement);
                foreach (var elem in Mapping) { if (elem != null) yield return new ElementValue("mapping", elem); }
                if (KindElement != null) yield return new ElementValue("kind", KindElement);
                if (ConstrainedTypeElement != null) yield return new ElementValue("constrainedType", ConstrainedTypeElement);
                if (AbstractElement != null) yield return new ElementValue("abstract", AbstractElement);
                if (ContextTypeElement != null) yield return new ElementValue("contextType", ContextTypeElement);
                foreach (var elem in ContextElement) { if (elem != null) yield return new ElementValue("context", elem); }
                if (BaseElement != null) yield return new ElementValue("base", BaseElement);
                if (Snapshot != null) yield return new ElementValue("snapshot", Snapshot);
                if (Differential != null) yield return new ElementValue("differential", Differential);
            }
        }
    
    }

}
