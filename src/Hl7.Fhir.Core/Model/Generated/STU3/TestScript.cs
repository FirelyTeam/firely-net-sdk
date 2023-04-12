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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// Describes a set of tests
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "TestScript", IsResource=true)]
    [DataContract]
    public partial class TestScript : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ITestScript, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.TestScript; } }
        [NotMapped]
        public override string TypeName { get { return "TestScript"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "OriginComponent")]
        [DataContract]
        public partial class OriginComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OriginComponent"; } }
            
            /// <summary>
            /// The index of the abstract origin server starting at 1
            /// </summary>
            [FhirElement("index", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer IndexElement
            {
                get { return _IndexElement; }
                set { _IndexElement = value; OnPropertyChanged("IndexElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _IndexElement;
            
            /// <summary>
            /// The index of the abstract origin server starting at 1
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Index
            {
                get { return IndexElement != null ? IndexElement.Value : null; }
                set
                {
                    if (value == null)
                        IndexElement = null;
                    else
                        IndexElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Index");
                }
            }
            
            /// <summary>
            /// FHIR-Client | FHIR-SDC-FormFiller
            /// </summary>
            [FhirElement("profile", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Profile
            {
                get { return _Profile; }
                set { _Profile = value; OnPropertyChanged("Profile"); }
            }
            
            private Hl7.Fhir.Model.Coding _Profile;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("OriginComponent");
                base.Serialize(sink);
                sink.Element("index", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); IndexElement?.Serialize(sink);
                sink.Element("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Profile?.Serialize(sink);
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
                    case "index":
                        IndexElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "profile":
                        Profile = source.Get<Hl7.Fhir.Model.Coding>();
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
                    case "index":
                        IndexElement = source.PopulateValue(IndexElement);
                        return true;
                    case "_index":
                        IndexElement = source.Populate(IndexElement);
                        return true;
                    case "profile":
                        Profile = source.Populate(Profile);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OriginComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IndexElement != null) dest.IndexElement = (Hl7.Fhir.Model.Integer)IndexElement.DeepCopy();
                    if(Profile != null) dest.Profile = (Hl7.Fhir.Model.Coding)Profile.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new OriginComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OriginComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IndexElement, otherT.IndexElement)) return false;
                if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OriginComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IndexElement, otherT.IndexElement)) return false;
                if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (IndexElement != null) yield return IndexElement;
                    if (Profile != null) yield return Profile;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (IndexElement != null) yield return new ElementValue("index", IndexElement);
                    if (Profile != null) yield return new ElementValue("profile", Profile);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "DestinationComponent")]
        [DataContract]
        public partial class DestinationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DestinationComponent"; } }
            
            /// <summary>
            /// The index of the abstract destination server starting at 1
            /// </summary>
            [FhirElement("index", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer IndexElement
            {
                get { return _IndexElement; }
                set { _IndexElement = value; OnPropertyChanged("IndexElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _IndexElement;
            
            /// <summary>
            /// The index of the abstract destination server starting at 1
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Index
            {
                get { return IndexElement != null ? IndexElement.Value : null; }
                set
                {
                    if (value == null)
                        IndexElement = null;
                    else
                        IndexElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Index");
                }
            }
            
            /// <summary>
            /// FHIR-Server | FHIR-SDC-FormManager | FHIR-SDC-FormReceiver | FHIR-SDC-FormProcessor
            /// </summary>
            [FhirElement("profile", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Profile
            {
                get { return _Profile; }
                set { _Profile = value; OnPropertyChanged("Profile"); }
            }
            
            private Hl7.Fhir.Model.Coding _Profile;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DestinationComponent");
                base.Serialize(sink);
                sink.Element("index", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); IndexElement?.Serialize(sink);
                sink.Element("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Profile?.Serialize(sink);
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
                    case "index":
                        IndexElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "profile":
                        Profile = source.Get<Hl7.Fhir.Model.Coding>();
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
                    case "index":
                        IndexElement = source.PopulateValue(IndexElement);
                        return true;
                    case "_index":
                        IndexElement = source.Populate(IndexElement);
                        return true;
                    case "profile":
                        Profile = source.Populate(Profile);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DestinationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IndexElement != null) dest.IndexElement = (Hl7.Fhir.Model.Integer)IndexElement.DeepCopy();
                    if(Profile != null) dest.Profile = (Hl7.Fhir.Model.Coding)Profile.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DestinationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DestinationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IndexElement, otherT.IndexElement)) return false;
                if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DestinationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IndexElement, otherT.IndexElement)) return false;
                if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (IndexElement != null) yield return IndexElement;
                    if (Profile != null) yield return Profile;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (IndexElement != null) yield return new ElementValue("index", IndexElement);
                    if (Profile != null) yield return new ElementValue("profile", Profile);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "MetadataComponent")]
        [DataContract]
        public partial class MetadataComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptMetadataComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "MetadataComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ITestScriptLinkComponent> Hl7.Fhir.Model.ITestScriptMetadataComponent.Link { get { return Link; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ITestScriptCapabilityComponent> Hl7.Fhir.Model.ITestScriptMetadataComponent.Capability { get { return Capability; } }
            
            /// <summary>
            /// Links to the FHIR specification
            /// </summary>
            [FhirElement("link", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<LinkComponent> Link
            {
                get { if(_Link==null) _Link = new List<LinkComponent>(); return _Link; }
                set { _Link = value; OnPropertyChanged("Link"); }
            }
            
            private List<LinkComponent> _Link;
            
            /// <summary>
            /// Capabilities  that are assumed to function correctly on the FHIR server being tested
            /// </summary>
            [FhirElement("capability", Order=50)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<CapabilityComponent> Capability
            {
                get { if(_Capability==null) _Capability = new List<CapabilityComponent>(); return _Capability; }
                set { _Capability = value; OnPropertyChanged("Capability"); }
            }
            
            private List<CapabilityComponent> _Capability;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("MetadataComponent");
                base.Serialize(sink);
                sink.BeginList("link", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Link)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("capability", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Capability)
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
                    case "link":
                        Link = source.GetList<LinkComponent>();
                        return true;
                    case "capability":
                        Capability = source.GetList<CapabilityComponent>();
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
                    case "link":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "capability":
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
                    case "link":
                        source.PopulateListItem(Link, index);
                        return true;
                    case "capability":
                        source.PopulateListItem(Capability, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MetadataComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Link != null) dest.Link = new List<LinkComponent>(Link.DeepCopy());
                    if(Capability != null) dest.Capability = new List<CapabilityComponent>(Capability.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new MetadataComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MetadataComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Link, otherT.Link)) return false;
                if( !DeepComparable.Matches(Capability, otherT.Capability)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MetadataComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Link, otherT.Link)) return false;
                if( !DeepComparable.IsExactly(Capability, otherT.Capability)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Link) { if (elem != null) yield return elem; }
                    foreach (var elem in Capability) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Link) { if (elem != null) yield return new ElementValue("link", elem); }
                    foreach (var elem in Capability) { if (elem != null) yield return new ElementValue("capability", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "LinkComponent")]
        [DataContract]
        public partial class LinkComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptLinkComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "LinkComponent"; } }
            
            /// <summary>
            /// URL to the specification
            /// </summary>
            [FhirElement("url", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// URL to the specification
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
            /// Short description
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Short description
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("LinkComponent");
                base.Serialize(sink);
                sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); UrlElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
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
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LinkComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new LinkComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LinkComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LinkComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (UrlElement != null) yield return UrlElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "CapabilityComponent")]
        [DataContract]
        public partial class CapabilityComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptCapabilityComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CapabilityComponent"; } }
            
            /// <summary>
            /// Are the capabilities required?
            /// </summary>
            [FhirElement("required", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean RequiredElement
            {
                get { return _RequiredElement; }
                set { _RequiredElement = value; OnPropertyChanged("RequiredElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _RequiredElement;
            
            /// <summary>
            /// Are the capabilities required?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Required
            {
                get { return RequiredElement != null ? RequiredElement.Value : null; }
                set
                {
                    if (value == null)
                        RequiredElement = null;
                    else
                        RequiredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Required");
                }
            }
            
            /// <summary>
            /// Are the capabilities validated?
            /// </summary>
            [FhirElement("validated", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ValidatedElement
            {
                get { return _ValidatedElement; }
                set { _ValidatedElement = value; OnPropertyChanged("ValidatedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ValidatedElement;
            
            /// <summary>
            /// Are the capabilities validated?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Validated
            {
                get { return ValidatedElement != null ? ValidatedElement.Value : null; }
                set
                {
                    if (value == null)
                        ValidatedElement = null;
                    else
                        ValidatedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Validated");
                }
            }
            
            /// <summary>
            /// The expected capabilities of the server
            /// </summary>
            [FhirElement("description", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// The expected capabilities of the server
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
            /// Which origin server these requirements apply to
            /// </summary>
            [FhirElement("origin", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Integer> OriginElement
            {
                get { if(_OriginElement==null) _OriginElement = new List<Hl7.Fhir.Model.Integer>(); return _OriginElement; }
                set { _OriginElement = value; OnPropertyChanged("OriginElement"); }
            }
            
            private List<Hl7.Fhir.Model.Integer> _OriginElement;
            
            /// <summary>
            /// Which origin server these requirements apply to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> Origin
            {
                get { return OriginElement != null ? OriginElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        OriginElement = null;
                    else
                        OriginElement = new List<Hl7.Fhir.Model.Integer>(value.Select(elem=>new Hl7.Fhir.Model.Integer(elem)));
                    OnPropertyChanged("Origin");
                }
            }
            
            /// <summary>
            /// Which server these requirements apply to
            /// </summary>
            [FhirElement("destination", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DestinationElement
            {
                get { return _DestinationElement; }
                set { _DestinationElement = value; OnPropertyChanged("DestinationElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _DestinationElement;
            
            /// <summary>
            /// Which server these requirements apply to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Destination
            {
                get { return DestinationElement != null ? DestinationElement.Value : null; }
                set
                {
                    if (value == null)
                        DestinationElement = null;
                    else
                        DestinationElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Destination");
                }
            }
            
            /// <summary>
            /// Links to the FHIR specification
            /// </summary>
            [FhirElement("link", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> LinkElement
            {
                get { if(_LinkElement==null) _LinkElement = new List<Hl7.Fhir.Model.FhirUri>(); return _LinkElement; }
                set { _LinkElement = value; OnPropertyChanged("LinkElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirUri> _LinkElement;
            
            /// <summary>
            /// Links to the FHIR specification
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Link
            {
                get { return LinkElement != null ? LinkElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        LinkElement = null;
                    else
                        LinkElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Link");
                }
            }
            
            /// <summary>
            /// Required Capability Statement
            /// </summary>
            [FhirElement("capabilities", Order=100)]
            [CLSCompliant(false)]
            [References("CapabilityStatement")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Capabilities
            {
                get { return _Capabilities; }
                set { _Capabilities = value; OnPropertyChanged("Capabilities"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Capabilities;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CapabilityComponent");
                base.Serialize(sink);
                sink.Element("required", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequiredElement?.Serialize(sink);
                sink.Element("validated", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValidatedElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.BeginList("origin", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(OriginElement);
                sink.End();
                sink.Element("destination", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DestinationElement?.Serialize(sink);
                sink.BeginList("link", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(LinkElement);
                sink.End();
                sink.Element("capabilities", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Capabilities?.Serialize(sink);
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
                    case "required":
                        RequiredElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "validated":
                        ValidatedElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "origin":
                        OriginElement = source.GetList<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "destination":
                        DestinationElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "link":
                        LinkElement = source.GetList<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "capabilities":
                        Capabilities = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "required":
                        RequiredElement = source.PopulateValue(RequiredElement);
                        return true;
                    case "_required":
                        RequiredElement = source.Populate(RequiredElement);
                        return true;
                    case "validated":
                        ValidatedElement = source.PopulateValue(ValidatedElement);
                        return true;
                    case "_validated":
                        ValidatedElement = source.Populate(ValidatedElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "origin":
                    case "_origin":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "destination":
                        DestinationElement = source.PopulateValue(DestinationElement);
                        return true;
                    case "_destination":
                        DestinationElement = source.Populate(DestinationElement);
                        return true;
                    case "link":
                    case "_link":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "capabilities":
                        Capabilities = source.Populate(Capabilities);
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
                    case "origin":
                        source.PopulatePrimitiveListItemValue(OriginElement, index);
                        return true;
                    case "_origin":
                        source.PopulatePrimitiveListItem(OriginElement, index);
                        return true;
                    case "link":
                        source.PopulatePrimitiveListItemValue(LinkElement, index);
                        return true;
                    case "_link":
                        source.PopulatePrimitiveListItem(LinkElement, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CapabilityComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RequiredElement != null) dest.RequiredElement = (Hl7.Fhir.Model.FhirBoolean)RequiredElement.DeepCopy();
                    if(ValidatedElement != null) dest.ValidatedElement = (Hl7.Fhir.Model.FhirBoolean)ValidatedElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(OriginElement != null) dest.OriginElement = new List<Hl7.Fhir.Model.Integer>(OriginElement.DeepCopy());
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    if(LinkElement != null) dest.LinkElement = new List<Hl7.Fhir.Model.FhirUri>(LinkElement.DeepCopy());
                    if(Capabilities != null) dest.Capabilities = (Hl7.Fhir.Model.ResourceReference)Capabilities.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CapabilityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CapabilityComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.Matches(ValidatedElement, otherT.ValidatedElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(OriginElement, otherT.OriginElement)) return false;
                if( !DeepComparable.Matches(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.Matches(LinkElement, otherT.LinkElement)) return false;
                if( !DeepComparable.Matches(Capabilities, otherT.Capabilities)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CapabilityComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.IsExactly(ValidatedElement, otherT.ValidatedElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(OriginElement, otherT.OriginElement)) return false;
                if( !DeepComparable.IsExactly(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.IsExactly(LinkElement, otherT.LinkElement)) return false;
                if( !DeepComparable.IsExactly(Capabilities, otherT.Capabilities)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RequiredElement != null) yield return RequiredElement;
                    if (ValidatedElement != null) yield return ValidatedElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in OriginElement) { if (elem != null) yield return elem; }
                    if (DestinationElement != null) yield return DestinationElement;
                    foreach (var elem in LinkElement) { if (elem != null) yield return elem; }
                    if (Capabilities != null) yield return Capabilities;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RequiredElement != null) yield return new ElementValue("required", RequiredElement);
                    if (ValidatedElement != null) yield return new ElementValue("validated", ValidatedElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in OriginElement) { if (elem != null) yield return new ElementValue("origin", elem); }
                    if (DestinationElement != null) yield return new ElementValue("destination", DestinationElement);
                    foreach (var elem in LinkElement) { if (elem != null) yield return new ElementValue("link", elem); }
                    if (Capabilities != null) yield return new ElementValue("capabilities", Capabilities);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "FixtureComponent")]
        [DataContract]
        public partial class FixtureComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptFixtureComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "FixtureComponent"; } }
            
            /// <summary>
            /// Whether or not to implicitly create the fixture during setup
            /// </summary>
            [FhirElement("autocreate", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AutocreateElement
            {
                get { return _AutocreateElement; }
                set { _AutocreateElement = value; OnPropertyChanged("AutocreateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AutocreateElement;
            
            /// <summary>
            /// Whether or not to implicitly create the fixture during setup
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Autocreate
            {
                get { return AutocreateElement != null ? AutocreateElement.Value : null; }
                set
                {
                    if (value == null)
                        AutocreateElement = null;
                    else
                        AutocreateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Autocreate");
                }
            }
            
            /// <summary>
            /// Whether or not to implicitly delete the fixture during teardown
            /// </summary>
            [FhirElement("autodelete", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AutodeleteElement
            {
                get { return _AutodeleteElement; }
                set { _AutodeleteElement = value; OnPropertyChanged("AutodeleteElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AutodeleteElement;
            
            /// <summary>
            /// Whether or not to implicitly delete the fixture during teardown
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Autodelete
            {
                get { return AutodeleteElement != null ? AutodeleteElement.Value : null; }
                set
                {
                    if (value == null)
                        AutodeleteElement = null;
                    else
                        AutodeleteElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Autodelete");
                }
            }
            
            /// <summary>
            /// Reference of the resource
            /// </summary>
            [FhirElement("resource", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Resource;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("FixtureComponent");
                base.Serialize(sink);
                sink.Element("autocreate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AutocreateElement?.Serialize(sink);
                sink.Element("autodelete", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AutodeleteElement?.Serialize(sink);
                sink.Element("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Resource?.Serialize(sink);
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
                    case "autocreate":
                        AutocreateElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "autodelete":
                        AutodeleteElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "resource":
                        Resource = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "autocreate":
                        AutocreateElement = source.PopulateValue(AutocreateElement);
                        return true;
                    case "_autocreate":
                        AutocreateElement = source.Populate(AutocreateElement);
                        return true;
                    case "autodelete":
                        AutodeleteElement = source.PopulateValue(AutodeleteElement);
                        return true;
                    case "_autodelete":
                        AutodeleteElement = source.Populate(AutodeleteElement);
                        return true;
                    case "resource":
                        Resource = source.Populate(Resource);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FixtureComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AutocreateElement != null) dest.AutocreateElement = (Hl7.Fhir.Model.FhirBoolean)AutocreateElement.DeepCopy();
                    if(AutodeleteElement != null) dest.AutodeleteElement = (Hl7.Fhir.Model.FhirBoolean)AutodeleteElement.DeepCopy();
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new FixtureComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FixtureComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AutocreateElement, otherT.AutocreateElement)) return false;
                if( !DeepComparable.Matches(AutodeleteElement, otherT.AutodeleteElement)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FixtureComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AutocreateElement, otherT.AutocreateElement)) return false;
                if( !DeepComparable.IsExactly(AutodeleteElement, otherT.AutodeleteElement)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (AutocreateElement != null) yield return AutocreateElement;
                    if (AutodeleteElement != null) yield return AutodeleteElement;
                    if (Resource != null) yield return Resource;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (AutocreateElement != null) yield return new ElementValue("autocreate", AutocreateElement);
                    if (AutodeleteElement != null) yield return new ElementValue("autodelete", AutodeleteElement);
                    if (Resource != null) yield return new ElementValue("resource", Resource);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "VariableComponent")]
        [DataContract]
        public partial class VariableComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptVariableComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "VariableComponent"; } }
            
            /// <summary>
            /// Descriptive name for this variable
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Descriptive name for this variable
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
            /// Default, hard-coded, or user-defined value for this variable
            /// </summary>
            [FhirElement("defaultValue", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DefaultValueElement
            {
                get { return _DefaultValueElement; }
                set { _DefaultValueElement = value; OnPropertyChanged("DefaultValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DefaultValueElement;
            
            /// <summary>
            /// Default, hard-coded, or user-defined value for this variable
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string DefaultValue
            {
                get { return DefaultValueElement != null ? DefaultValueElement.Value : null; }
                set
                {
                    if (value == null)
                        DefaultValueElement = null;
                    else
                        DefaultValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("DefaultValue");
                }
            }
            
            /// <summary>
            /// Natural language description of the variable
            /// </summary>
            [FhirElement("description", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Natural language description of the variable
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
            /// The fluentpath expression against the fixture body
            /// </summary>
            [FhirElement("expression", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ExpressionElement
            {
                get { return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ExpressionElement;
            
            /// <summary>
            /// The fluentpath expression against the fixture body
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
            /// HTTP header field name for source
            /// </summary>
            [FhirElement("headerField", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HeaderFieldElement
            {
                get { return _HeaderFieldElement; }
                set { _HeaderFieldElement = value; OnPropertyChanged("HeaderFieldElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _HeaderFieldElement;
            
            /// <summary>
            /// HTTP header field name for source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string HeaderField
            {
                get { return HeaderFieldElement != null ? HeaderFieldElement.Value : null; }
                set
                {
                    if (value == null)
                        HeaderFieldElement = null;
                    else
                        HeaderFieldElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("HeaderField");
                }
            }
            
            /// <summary>
            /// Hint help text for default value to enter
            /// </summary>
            [FhirElement("hint", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HintElement
            {
                get { return _HintElement; }
                set { _HintElement = value; OnPropertyChanged("HintElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _HintElement;
            
            /// <summary>
            /// Hint help text for default value to enter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Hint
            {
                get { return HintElement != null ? HintElement.Value : null; }
                set
                {
                    if (value == null)
                        HintElement = null;
                    else
                        HintElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Hint");
                }
            }
            
            /// <summary>
            /// XPath or JSONPath against the fixture body
            /// </summary>
            [FhirElement("path", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// XPath or JSONPath against the fixture body
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
            /// Fixture Id of source expression or headerField within this variable
            /// </summary>
            [FhirElement("sourceId", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Id SourceIdElement
            {
                get { return _SourceIdElement; }
                set { _SourceIdElement = value; OnPropertyChanged("SourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _SourceIdElement;
            
            /// <summary>
            /// Fixture Id of source expression or headerField within this variable
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SourceId
            {
                get { return SourceIdElement != null ? SourceIdElement.Value : null; }
                set
                {
                    if (value == null)
                        SourceIdElement = null;
                    else
                        SourceIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("SourceId");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("VariableComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("defaultValue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DefaultValueElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("expression", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ExpressionElement?.Serialize(sink);
                sink.Element("headerField", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); HeaderFieldElement?.Serialize(sink);
                sink.Element("hint", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); HintElement?.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PathElement?.Serialize(sink);
                sink.Element("sourceId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SourceIdElement?.Serialize(sink);
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
                    case "defaultValue":
                        DefaultValueElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "expression":
                        ExpressionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "headerField":
                        HeaderFieldElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "hint":
                        HintElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "path":
                        PathElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "sourceId":
                        SourceIdElement = source.Get<Hl7.Fhir.Model.Id>();
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
                    case "defaultValue":
                        DefaultValueElement = source.PopulateValue(DefaultValueElement);
                        return true;
                    case "_defaultValue":
                        DefaultValueElement = source.Populate(DefaultValueElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "expression":
                        ExpressionElement = source.PopulateValue(ExpressionElement);
                        return true;
                    case "_expression":
                        ExpressionElement = source.Populate(ExpressionElement);
                        return true;
                    case "headerField":
                        HeaderFieldElement = source.PopulateValue(HeaderFieldElement);
                        return true;
                    case "_headerField":
                        HeaderFieldElement = source.Populate(HeaderFieldElement);
                        return true;
                    case "hint":
                        HintElement = source.PopulateValue(HintElement);
                        return true;
                    case "_hint":
                        HintElement = source.Populate(HintElement);
                        return true;
                    case "path":
                        PathElement = source.PopulateValue(PathElement);
                        return true;
                    case "_path":
                        PathElement = source.Populate(PathElement);
                        return true;
                    case "sourceId":
                        SourceIdElement = source.PopulateValue(SourceIdElement);
                        return true;
                    case "_sourceId":
                        SourceIdElement = source.Populate(SourceIdElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as VariableComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DefaultValueElement != null) dest.DefaultValueElement = (Hl7.Fhir.Model.FhirString)DefaultValueElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(ExpressionElement != null) dest.ExpressionElement = (Hl7.Fhir.Model.FhirString)ExpressionElement.DeepCopy();
                    if(HeaderFieldElement != null) dest.HeaderFieldElement = (Hl7.Fhir.Model.FhirString)HeaderFieldElement.DeepCopy();
                    if(HintElement != null) dest.HintElement = (Hl7.Fhir.Model.FhirString)HintElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(SourceIdElement != null) dest.SourceIdElement = (Hl7.Fhir.Model.Id)SourceIdElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new VariableComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as VariableComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DefaultValueElement, otherT.DefaultValueElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
                if( !DeepComparable.Matches(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.Matches(HintElement, otherT.HintElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(SourceIdElement, otherT.SourceIdElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as VariableComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DefaultValueElement, otherT.DefaultValueElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
                if( !DeepComparable.IsExactly(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.IsExactly(HintElement, otherT.HintElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(SourceIdElement, otherT.SourceIdElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (DefaultValueElement != null) yield return DefaultValueElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (ExpressionElement != null) yield return ExpressionElement;
                    if (HeaderFieldElement != null) yield return HeaderFieldElement;
                    if (HintElement != null) yield return HintElement;
                    if (PathElement != null) yield return PathElement;
                    if (SourceIdElement != null) yield return SourceIdElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DefaultValueElement != null) yield return new ElementValue("defaultValue", DefaultValueElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (ExpressionElement != null) yield return new ElementValue("expression", ExpressionElement);
                    if (HeaderFieldElement != null) yield return new ElementValue("headerField", HeaderFieldElement);
                    if (HintElement != null) yield return new ElementValue("hint", HintElement);
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (SourceIdElement != null) yield return new ElementValue("sourceId", SourceIdElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RuleComponent")]
        [DataContract]
        public partial class RuleComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RuleComponent"; } }
            
            /// <summary>
            /// Assert rule resource reference
            /// </summary>
            [FhirElement("resource", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Resource;
            
            /// <summary>
            /// Rule parameter template
            /// </summary>
            [FhirElement("param", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<RuleParamComponent> Param
            {
                get { if(_Param==null) _Param = new List<RuleParamComponent>(); return _Param; }
                set { _Param = value; OnPropertyChanged("Param"); }
            }
            
            private List<RuleParamComponent> _Param;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RuleComponent");
                base.Serialize(sink);
                sink.Element("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Resource?.Serialize(sink);
                sink.BeginList("param", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Param)
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
                    case "resource":
                        Resource = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "param":
                        Param = source.GetList<RuleParamComponent>();
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
                    case "resource":
                        Resource = source.Populate(Resource);
                        return true;
                    case "param":
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
                    case "param":
                        source.PopulateListItem(Param, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RuleComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                    if(Param != null) dest.Param = new List<RuleParamComponent>(Param.DeepCopy());
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
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Param, otherT.Param)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RuleComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Param, otherT.Param)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Resource != null) yield return Resource;
                    foreach (var elem in Param) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Resource != null) yield return new ElementValue("resource", Resource);
                    foreach (var elem in Param) { if (elem != null) yield return new ElementValue("param", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RuleParamComponent")]
        [DataContract]
        public partial class RuleParamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RuleParamComponent"; } }
            
            /// <summary>
            /// Parameter name matching external assert rule parameter
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Parameter name matching external assert rule parameter
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
            /// Parameter value defined either explicitly or dynamically
            /// </summary>
            [FhirElement("value", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Parameter value defined either explicitly or dynamically
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RuleParamComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValueElement?.Serialize(sink);
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
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RuleParamComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RuleParamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RuleParamComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RuleParamComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RulesetComponent")]
        [DataContract]
        public partial class RulesetComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RulesetComponent"; } }
            
            /// <summary>
            /// Assert ruleset resource reference
            /// </summary>
            [FhirElement("resource", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Resource;
            
            /// <summary>
            /// The referenced rule within the ruleset
            /// </summary>
            [FhirElement("rule", Order=50)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<RulesetRuleComponent> Rule
            {
                get { if(_Rule==null) _Rule = new List<RulesetRuleComponent>(); return _Rule; }
                set { _Rule = value; OnPropertyChanged("Rule"); }
            }
            
            private List<RulesetRuleComponent> _Rule;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RulesetComponent");
                base.Serialize(sink);
                sink.Element("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Resource?.Serialize(sink);
                sink.BeginList("rule", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Rule)
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
                    case "resource":
                        Resource = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "rule":
                        Rule = source.GetList<RulesetRuleComponent>();
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
                    case "resource":
                        Resource = source.Populate(Resource);
                        return true;
                    case "rule":
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
                    case "rule":
                        source.PopulateListItem(Rule, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RulesetComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                    if(Rule != null) dest.Rule = new List<RulesetRuleComponent>(Rule.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RulesetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RulesetComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RulesetComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Resource != null) yield return Resource;
                    foreach (var elem in Rule) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Resource != null) yield return new ElementValue("resource", Resource);
                    foreach (var elem in Rule) { if (elem != null) yield return new ElementValue("rule", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RulesetRuleComponent")]
        [DataContract]
        public partial class RulesetRuleComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RulesetRuleComponent"; } }
            
            /// <summary>
            /// Id of referenced rule within the ruleset
            /// </summary>
            [FhirElement("ruleId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id RuleIdElement
            {
                get { return _RuleIdElement; }
                set { _RuleIdElement = value; OnPropertyChanged("RuleIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _RuleIdElement;
            
            /// <summary>
            /// Id of referenced rule within the ruleset
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RuleId
            {
                get { return RuleIdElement != null ? RuleIdElement.Value : null; }
                set
                {
                    if (value == null)
                        RuleIdElement = null;
                    else
                        RuleIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("RuleId");
                }
            }
            
            /// <summary>
            /// Ruleset rule parameter template
            /// </summary>
            [FhirElement("param", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<RulesetRuleParamComponent> Param
            {
                get { if(_Param==null) _Param = new List<RulesetRuleParamComponent>(); return _Param; }
                set { _Param = value; OnPropertyChanged("Param"); }
            }
            
            private List<RulesetRuleParamComponent> _Param;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RulesetRuleComponent");
                base.Serialize(sink);
                sink.Element("ruleId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); RuleIdElement?.Serialize(sink);
                sink.BeginList("param", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Param)
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
                    case "ruleId":
                        RuleIdElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "param":
                        Param = source.GetList<RulesetRuleParamComponent>();
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
                    case "ruleId":
                        RuleIdElement = source.PopulateValue(RuleIdElement);
                        return true;
                    case "_ruleId":
                        RuleIdElement = source.Populate(RuleIdElement);
                        return true;
                    case "param":
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
                    case "param":
                        source.PopulateListItem(Param, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RulesetRuleComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RuleIdElement != null) dest.RuleIdElement = (Hl7.Fhir.Model.Id)RuleIdElement.DeepCopy();
                    if(Param != null) dest.Param = new List<RulesetRuleParamComponent>(Param.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RulesetRuleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RulesetRuleComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RuleIdElement, otherT.RuleIdElement)) return false;
                if( !DeepComparable.Matches(Param, otherT.Param)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RulesetRuleComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RuleIdElement, otherT.RuleIdElement)) return false;
                if( !DeepComparable.IsExactly(Param, otherT.Param)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RuleIdElement != null) yield return RuleIdElement;
                    foreach (var elem in Param) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RuleIdElement != null) yield return new ElementValue("ruleId", RuleIdElement);
                    foreach (var elem in Param) { if (elem != null) yield return new ElementValue("param", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RulesetRuleParamComponent")]
        [DataContract]
        public partial class RulesetRuleParamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RulesetRuleParamComponent"; } }
            
            /// <summary>
            /// Parameter name matching external assert ruleset rule parameter
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Parameter name matching external assert ruleset rule parameter
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
            /// Parameter value defined either explicitly or dynamically
            /// </summary>
            [FhirElement("value", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Parameter value defined either explicitly or dynamically
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RulesetRuleParamComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValueElement?.Serialize(sink);
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
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RulesetRuleParamComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RulesetRuleParamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RulesetRuleParamComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RulesetRuleParamComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SetupComponent")]
        [DataContract]
        public partial class SetupComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptSetupComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SetupComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ITestScriptSetupActionComponent> Hl7.Fhir.Model.ITestScriptSetupComponent.Action { get { return Action; } }
            
            /// <summary>
            /// A setup operation or assert to perform
            /// </summary>
            [FhirElement("action", Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<SetupActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<SetupActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<SetupActionComponent> _Action;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SetupComponent");
                base.Serialize(sink);
                sink.BeginList("action", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Action)
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
                    case "action":
                        Action = source.GetList<SetupActionComponent>();
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
                    case "action":
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
                    case "action":
                        source.PopulateListItem(Action, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SetupComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Action != null) dest.Action = new List<SetupActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SetupComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SetupComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SetupComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SetupActionComponent")]
        [DataContract]
        public partial class SetupActionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptSetupActionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SetupActionComponent"; } }
            
            [NotMapped]
            Hl7.Fhir.Model.ITestScriptOperationComponent Hl7.Fhir.Model.ITestScriptSetupActionComponent.Operation { get { return Operation; } }
            
            [NotMapped]
            Hl7.Fhir.Model.ITestScriptAssertComponent Hl7.Fhir.Model.ITestScriptSetupActionComponent.Assert { get { return Assert; } }
            
            /// <summary>
            /// The setup operation to perform
            /// </summary>
            [FhirElement("operation", Order=40)]
            [DataMember]
            public OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private OperationComponent _Operation;
            
            /// <summary>
            /// The assertion to perform
            /// </summary>
            [FhirElement("assert", Order=50)]
            [DataMember]
            public AssertComponent Assert
            {
                get { return _Assert; }
                set { _Assert = value; OnPropertyChanged("Assert"); }
            }
            
            private AssertComponent _Assert;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SetupActionComponent");
                base.Serialize(sink);
                sink.Element("operation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Operation?.Serialize(sink);
                sink.Element("assert", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Assert?.Serialize(sink);
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
                    case "operation":
                        Operation = source.Get<OperationComponent>();
                        return true;
                    case "assert":
                        Assert = source.Get<AssertComponent>();
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
                    case "operation":
                        Operation = source.Populate(Operation);
                        return true;
                    case "assert":
                        Assert = source.Populate(Assert);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SetupActionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (OperationComponent)Operation.DeepCopy();
                    if(Assert != null) dest.Assert = (AssertComponent)Assert.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SetupActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SetupActionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                if( !DeepComparable.Matches(Assert, otherT.Assert)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SetupActionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                if( !DeepComparable.IsExactly(Assert, otherT.Assert)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Operation != null) yield return Operation;
                    if (Assert != null) yield return Assert;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Operation != null) yield return new ElementValue("operation", Operation);
                    if (Assert != null) yield return new ElementValue("assert", Assert);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "OperationComponent")]
        [DataContract]
        public partial class OperationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptOperationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OperationComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ITestScriptRequestHeaderComponent> Hl7.Fhir.Model.ITestScriptOperationComponent.RequestHeader { get { return RequestHeader; } }
            
            /// <summary>
            /// The operation code type that will be executed
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Resource type
            /// </summary>
            [FhirElement("resource", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.FHIRDefinedType> ResourceElement
            {
                get { return _ResourceElement; }
                set { _ResourceElement = value; OnPropertyChanged("ResourceElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.FHIRDefinedType> _ResourceElement;
            
            /// <summary>
            /// Resource type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.FHIRDefinedType? Resource
            {
                get { return ResourceElement != null ? ResourceElement.Value : null; }
                set
                {
                    if (value == null)
                        ResourceElement = null;
                    else
                        ResourceElement = new Code<Hl7.Fhir.Model.STU3.FHIRDefinedType>(value);
                    OnPropertyChanged("Resource");
                }
            }
            
            /// <summary>
            /// Tracking/logging operation label
            /// </summary>
            [FhirElement("label", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LabelElement
            {
                get { return _LabelElement; }
                set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LabelElement;
            
            /// <summary>
            /// Tracking/logging operation label
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
            /// Tracking/reporting operation description
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
            /// Tracking/reporting operation description
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
            /// xml | json | ttl | none
            /// </summary>
            [FhirElement("accept", Order=80)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.ContentType> AcceptElement
            {
                get { return _AcceptElement; }
                set { _AcceptElement = value; OnPropertyChanged("AcceptElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.ContentType> _AcceptElement;
            
            /// <summary>
            /// xml | json | ttl | none
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.ContentType? Accept
            {
                get { return AcceptElement != null ? AcceptElement.Value : null; }
                set
                {
                    if (value == null)
                        AcceptElement = null;
                    else
                        AcceptElement = new Code<Hl7.Fhir.Model.STU3.ContentType>(value);
                    OnPropertyChanged("Accept");
                }
            }
            
            /// <summary>
            /// xml | json | ttl | none
            /// </summary>
            [FhirElement("contentType", Order=90)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.ContentType> ContentTypeElement
            {
                get { return _ContentTypeElement; }
                set { _ContentTypeElement = value; OnPropertyChanged("ContentTypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.ContentType> _ContentTypeElement;
            
            /// <summary>
            /// xml | json | ttl | none
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.ContentType? ContentType
            {
                get { return ContentTypeElement != null ? ContentTypeElement.Value : null; }
                set
                {
                    if (value == null)
                        ContentTypeElement = null;
                    else
                        ContentTypeElement = new Code<Hl7.Fhir.Model.STU3.ContentType>(value);
                    OnPropertyChanged("ContentType");
                }
            }
            
            /// <summary>
            /// Server responding to the request
            /// </summary>
            [FhirElement("destination", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DestinationElement
            {
                get { return _DestinationElement; }
                set { _DestinationElement = value; OnPropertyChanged("DestinationElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _DestinationElement;
            
            /// <summary>
            /// Server responding to the request
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Destination
            {
                get { return DestinationElement != null ? DestinationElement.Value : null; }
                set
                {
                    if (value == null)
                        DestinationElement = null;
                    else
                        DestinationElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Destination");
                }
            }
            
            /// <summary>
            /// Whether or not to send the request url in encoded format
            /// </summary>
            [FhirElement("encodeRequestUrl", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean EncodeRequestUrlElement
            {
                get { return _EncodeRequestUrlElement; }
                set { _EncodeRequestUrlElement = value; OnPropertyChanged("EncodeRequestUrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _EncodeRequestUrlElement;
            
            /// <summary>
            /// Whether or not to send the request url in encoded format
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? EncodeRequestUrl
            {
                get { return EncodeRequestUrlElement != null ? EncodeRequestUrlElement.Value : null; }
                set
                {
                    if (value == null)
                        EncodeRequestUrlElement = null;
                    else
                        EncodeRequestUrlElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("EncodeRequestUrl");
                }
            }
            
            /// <summary>
            /// Server initiating the request
            /// </summary>
            [FhirElement("origin", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Integer OriginElement
            {
                get { return _OriginElement; }
                set { _OriginElement = value; OnPropertyChanged("OriginElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _OriginElement;
            
            /// <summary>
            /// Server initiating the request
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Origin
            {
                get { return OriginElement != null ? OriginElement.Value : null; }
                set
                {
                    if (value == null)
                        OriginElement = null;
                    else
                        OriginElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Origin");
                }
            }
            
            /// <summary>
            /// Explicitly defined path parameters
            /// </summary>
            [FhirElement("params", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ParamsElement
            {
                get { return _ParamsElement; }
                set { _ParamsElement = value; OnPropertyChanged("ParamsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ParamsElement;
            
            /// <summary>
            /// Explicitly defined path parameters
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Params
            {
                get { return ParamsElement != null ? ParamsElement.Value : null; }
                set
                {
                    if (value == null)
                        ParamsElement = null;
                    else
                        ParamsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Params");
                }
            }
            
            /// <summary>
            /// Each operation can have one or more header elements
            /// </summary>
            [FhirElement("requestHeader", Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<RequestHeaderComponent> RequestHeader
            {
                get { if(_RequestHeader==null) _RequestHeader = new List<RequestHeaderComponent>(); return _RequestHeader; }
                set { _RequestHeader = value; OnPropertyChanged("RequestHeader"); }
            }
            
            private List<RequestHeaderComponent> _RequestHeader;
            
            /// <summary>
            /// Fixture Id of mapped request
            /// </summary>
            [FhirElement("requestId", Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.Id RequestIdElement
            {
                get { return _RequestIdElement; }
                set { _RequestIdElement = value; OnPropertyChanged("RequestIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _RequestIdElement;
            
            /// <summary>
            /// Fixture Id of mapped request
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RequestId
            {
                get { return RequestIdElement != null ? RequestIdElement.Value : null; }
                set
                {
                    if (value == null)
                        RequestIdElement = null;
                    else
                        RequestIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("RequestId");
                }
            }
            
            /// <summary>
            /// Fixture Id of mapped response
            /// </summary>
            [FhirElement("responseId", Order=160)]
            [DataMember]
            public Hl7.Fhir.Model.Id ResponseIdElement
            {
                get { return _ResponseIdElement; }
                set { _ResponseIdElement = value; OnPropertyChanged("ResponseIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ResponseIdElement;
            
            /// <summary>
            /// Fixture Id of mapped response
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ResponseId
            {
                get { return ResponseIdElement != null ? ResponseIdElement.Value : null; }
                set
                {
                    if (value == null)
                        ResponseIdElement = null;
                    else
                        ResponseIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("ResponseId");
                }
            }
            
            /// <summary>
            /// Fixture Id of body for PUT and POST requests
            /// </summary>
            [FhirElement("sourceId", Order=170)]
            [DataMember]
            public Hl7.Fhir.Model.Id SourceIdElement
            {
                get { return _SourceIdElement; }
                set { _SourceIdElement = value; OnPropertyChanged("SourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _SourceIdElement;
            
            /// <summary>
            /// Fixture Id of body for PUT and POST requests
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SourceId
            {
                get { return SourceIdElement != null ? SourceIdElement.Value : null; }
                set
                {
                    if (value == null)
                        SourceIdElement = null;
                    else
                        SourceIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("SourceId");
                }
            }
            
            /// <summary>
            /// Id of fixture used for extracting the [id],  [type], and [vid] for GET requests
            /// </summary>
            [FhirElement("targetId", Order=180)]
            [DataMember]
            public Hl7.Fhir.Model.Id TargetIdElement
            {
                get { return _TargetIdElement; }
                set { _TargetIdElement = value; OnPropertyChanged("TargetIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _TargetIdElement;
            
            /// <summary>
            /// Id of fixture used for extracting the [id],  [type], and [vid] for GET requests
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string TargetId
            {
                get { return TargetIdElement != null ? TargetIdElement.Value : null; }
                set
                {
                    if (value == null)
                        TargetIdElement = null;
                    else
                        TargetIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("TargetId");
                }
            }
            
            /// <summary>
            /// Request URL
            /// </summary>
            [FhirElement("url", Order=190)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _UrlElement;
            
            /// <summary>
            /// Request URL
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
                        UrlElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Url");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("OperationComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ResourceElement?.Serialize(sink);
                sink.Element("label", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LabelElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("accept", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AcceptElement?.Serialize(sink);
                sink.Element("contentType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ContentTypeElement?.Serialize(sink);
                sink.Element("destination", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DestinationElement?.Serialize(sink);
                sink.Element("encodeRequestUrl", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); EncodeRequestUrlElement?.Serialize(sink);
                sink.Element("origin", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OriginElement?.Serialize(sink);
                sink.Element("params", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ParamsElement?.Serialize(sink);
                sink.BeginList("requestHeader", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in RequestHeader)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("requestId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequestIdElement?.Serialize(sink);
                sink.Element("responseId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ResponseIdElement?.Serialize(sink);
                sink.Element("sourceId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SourceIdElement?.Serialize(sink);
                sink.Element("targetId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TargetIdElement?.Serialize(sink);
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "resource":
                        ResourceElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.FHIRDefinedType>>();
                        return true;
                    case "label":
                        LabelElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "accept":
                        AcceptElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.ContentType>>();
                        return true;
                    case "contentType":
                        ContentTypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.ContentType>>();
                        return true;
                    case "destination":
                        DestinationElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "encodeRequestUrl":
                        EncodeRequestUrlElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "origin":
                        OriginElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "params":
                        ParamsElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "requestHeader":
                        RequestHeader = source.GetList<RequestHeaderComponent>();
                        return true;
                    case "requestId":
                        RequestIdElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "responseId":
                        ResponseIdElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "sourceId":
                        SourceIdElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "targetId":
                        TargetIdElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "url":
                        UrlElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "resource":
                        ResourceElement = source.PopulateValue(ResourceElement);
                        return true;
                    case "_resource":
                        ResourceElement = source.Populate(ResourceElement);
                        return true;
                    case "label":
                        LabelElement = source.PopulateValue(LabelElement);
                        return true;
                    case "_label":
                        LabelElement = source.Populate(LabelElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "accept":
                        AcceptElement = source.PopulateValue(AcceptElement);
                        return true;
                    case "_accept":
                        AcceptElement = source.Populate(AcceptElement);
                        return true;
                    case "contentType":
                        ContentTypeElement = source.PopulateValue(ContentTypeElement);
                        return true;
                    case "_contentType":
                        ContentTypeElement = source.Populate(ContentTypeElement);
                        return true;
                    case "destination":
                        DestinationElement = source.PopulateValue(DestinationElement);
                        return true;
                    case "_destination":
                        DestinationElement = source.Populate(DestinationElement);
                        return true;
                    case "encodeRequestUrl":
                        EncodeRequestUrlElement = source.PopulateValue(EncodeRequestUrlElement);
                        return true;
                    case "_encodeRequestUrl":
                        EncodeRequestUrlElement = source.Populate(EncodeRequestUrlElement);
                        return true;
                    case "origin":
                        OriginElement = source.PopulateValue(OriginElement);
                        return true;
                    case "_origin":
                        OriginElement = source.Populate(OriginElement);
                        return true;
                    case "params":
                        ParamsElement = source.PopulateValue(ParamsElement);
                        return true;
                    case "_params":
                        ParamsElement = source.Populate(ParamsElement);
                        return true;
                    case "requestHeader":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "requestId":
                        RequestIdElement = source.PopulateValue(RequestIdElement);
                        return true;
                    case "_requestId":
                        RequestIdElement = source.Populate(RequestIdElement);
                        return true;
                    case "responseId":
                        ResponseIdElement = source.PopulateValue(ResponseIdElement);
                        return true;
                    case "_responseId":
                        ResponseIdElement = source.Populate(ResponseIdElement);
                        return true;
                    case "sourceId":
                        SourceIdElement = source.PopulateValue(SourceIdElement);
                        return true;
                    case "_sourceId":
                        SourceIdElement = source.Populate(SourceIdElement);
                        return true;
                    case "targetId":
                        TargetIdElement = source.PopulateValue(TargetIdElement);
                        return true;
                    case "_targetId":
                        TargetIdElement = source.Populate(TargetIdElement);
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
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "requestHeader":
                        source.PopulateListItem(RequestHeader, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OperationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(ResourceElement != null) dest.ResourceElement = (Code<Hl7.Fhir.Model.STU3.FHIRDefinedType>)ResourceElement.DeepCopy();
                    if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(AcceptElement != null) dest.AcceptElement = (Code<Hl7.Fhir.Model.STU3.ContentType>)AcceptElement.DeepCopy();
                    if(ContentTypeElement != null) dest.ContentTypeElement = (Code<Hl7.Fhir.Model.STU3.ContentType>)ContentTypeElement.DeepCopy();
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    if(EncodeRequestUrlElement != null) dest.EncodeRequestUrlElement = (Hl7.Fhir.Model.FhirBoolean)EncodeRequestUrlElement.DeepCopy();
                    if(OriginElement != null) dest.OriginElement = (Hl7.Fhir.Model.Integer)OriginElement.DeepCopy();
                    if(ParamsElement != null) dest.ParamsElement = (Hl7.Fhir.Model.FhirString)ParamsElement.DeepCopy();
                    if(RequestHeader != null) dest.RequestHeader = new List<RequestHeaderComponent>(RequestHeader.DeepCopy());
                    if(RequestIdElement != null) dest.RequestIdElement = (Hl7.Fhir.Model.Id)RequestIdElement.DeepCopy();
                    if(ResponseIdElement != null) dest.ResponseIdElement = (Hl7.Fhir.Model.Id)ResponseIdElement.DeepCopy();
                    if(SourceIdElement != null) dest.SourceIdElement = (Hl7.Fhir.Model.Id)SourceIdElement.DeepCopy();
                    if(TargetIdElement != null) dest.TargetIdElement = (Hl7.Fhir.Model.Id)TargetIdElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirString)UrlElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new OperationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OperationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(AcceptElement, otherT.AcceptElement)) return false;
                if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.Matches(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.Matches(EncodeRequestUrlElement, otherT.EncodeRequestUrlElement)) return false;
                if( !DeepComparable.Matches(OriginElement, otherT.OriginElement)) return false;
                if( !DeepComparable.Matches(ParamsElement, otherT.ParamsElement)) return false;
                if( !DeepComparable.Matches(RequestHeader, otherT.RequestHeader)) return false;
                if( !DeepComparable.Matches(RequestIdElement, otherT.RequestIdElement)) return false;
                if( !DeepComparable.Matches(ResponseIdElement, otherT.ResponseIdElement)) return false;
                if( !DeepComparable.Matches(SourceIdElement, otherT.SourceIdElement)) return false;
                if( !DeepComparable.Matches(TargetIdElement, otherT.TargetIdElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OperationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(AcceptElement, otherT.AcceptElement)) return false;
                if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.IsExactly(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.IsExactly(EncodeRequestUrlElement, otherT.EncodeRequestUrlElement)) return false;
                if( !DeepComparable.IsExactly(OriginElement, otherT.OriginElement)) return false;
                if( !DeepComparable.IsExactly(ParamsElement, otherT.ParamsElement)) return false;
                if( !DeepComparable.IsExactly(RequestHeader, otherT.RequestHeader)) return false;
                if( !DeepComparable.IsExactly(RequestIdElement, otherT.RequestIdElement)) return false;
                if( !DeepComparable.IsExactly(ResponseIdElement, otherT.ResponseIdElement)) return false;
                if( !DeepComparable.IsExactly(SourceIdElement, otherT.SourceIdElement)) return false;
                if( !DeepComparable.IsExactly(TargetIdElement, otherT.TargetIdElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (ResourceElement != null) yield return ResourceElement;
                    if (LabelElement != null) yield return LabelElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (AcceptElement != null) yield return AcceptElement;
                    if (ContentTypeElement != null) yield return ContentTypeElement;
                    if (DestinationElement != null) yield return DestinationElement;
                    if (EncodeRequestUrlElement != null) yield return EncodeRequestUrlElement;
                    if (OriginElement != null) yield return OriginElement;
                    if (ParamsElement != null) yield return ParamsElement;
                    foreach (var elem in RequestHeader) { if (elem != null) yield return elem; }
                    if (RequestIdElement != null) yield return RequestIdElement;
                    if (ResponseIdElement != null) yield return ResponseIdElement;
                    if (SourceIdElement != null) yield return SourceIdElement;
                    if (TargetIdElement != null) yield return TargetIdElement;
                    if (UrlElement != null) yield return UrlElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (ResourceElement != null) yield return new ElementValue("resource", ResourceElement);
                    if (LabelElement != null) yield return new ElementValue("label", LabelElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (AcceptElement != null) yield return new ElementValue("accept", AcceptElement);
                    if (ContentTypeElement != null) yield return new ElementValue("contentType", ContentTypeElement);
                    if (DestinationElement != null) yield return new ElementValue("destination", DestinationElement);
                    if (EncodeRequestUrlElement != null) yield return new ElementValue("encodeRequestUrl", EncodeRequestUrlElement);
                    if (OriginElement != null) yield return new ElementValue("origin", OriginElement);
                    if (ParamsElement != null) yield return new ElementValue("params", ParamsElement);
                    foreach (var elem in RequestHeader) { if (elem != null) yield return new ElementValue("requestHeader", elem); }
                    if (RequestIdElement != null) yield return new ElementValue("requestId", RequestIdElement);
                    if (ResponseIdElement != null) yield return new ElementValue("responseId", ResponseIdElement);
                    if (SourceIdElement != null) yield return new ElementValue("sourceId", SourceIdElement);
                    if (TargetIdElement != null) yield return new ElementValue("targetId", TargetIdElement);
                    if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RequestHeaderComponent")]
        [DataContract]
        public partial class RequestHeaderComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptRequestHeaderComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RequestHeaderComponent"; } }
            
            /// <summary>
            /// HTTP header field name
            /// </summary>
            [FhirElement("field", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString FieldElement
            {
                get { return _FieldElement; }
                set { _FieldElement = value; OnPropertyChanged("FieldElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _FieldElement;
            
            /// <summary>
            /// HTTP header field name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Field
            {
                get { return FieldElement != null ? FieldElement.Value : null; }
                set
                {
                    if (value == null)
                        FieldElement = null;
                    else
                        FieldElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Field");
                }
            }
            
            /// <summary>
            /// HTTP headerfield value
            /// </summary>
            [FhirElement("value", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// HTTP headerfield value
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RequestHeaderComponent");
                base.Serialize(sink);
                sink.Element("field", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); FieldElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ValueElement?.Serialize(sink);
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
                    case "field":
                        FieldElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "field":
                        FieldElement = source.PopulateValue(FieldElement);
                        return true;
                    case "_field":
                        FieldElement = source.Populate(FieldElement);
                        return true;
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RequestHeaderComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(FieldElement != null) dest.FieldElement = (Hl7.Fhir.Model.FhirString)FieldElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RequestHeaderComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RequestHeaderComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(FieldElement, otherT.FieldElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RequestHeaderComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(FieldElement, otherT.FieldElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (FieldElement != null) yield return FieldElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (FieldElement != null) yield return new ElementValue("field", FieldElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "AssertComponent")]
        [DataContract]
        public partial class AssertComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptAssertComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AssertComponent"; } }
            
            /// <summary>
            /// Tracking/logging assertion label
            /// </summary>
            [FhirElement("label", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LabelElement
            {
                get { return _LabelElement; }
                set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LabelElement;
            
            /// <summary>
            /// Tracking/logging assertion label
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
            /// Tracking/reporting assertion description
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Tracking/reporting assertion description
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
            /// response | request
            /// </summary>
            [FhirElement("direction", Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.AssertionDirectionType> DirectionElement
            {
                get { return _DirectionElement; }
                set { _DirectionElement = value; OnPropertyChanged("DirectionElement"); }
            }
            
            private Code<Hl7.Fhir.Model.AssertionDirectionType> _DirectionElement;
            
            /// <summary>
            /// response | request
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AssertionDirectionType? Direction
            {
                get { return DirectionElement != null ? DirectionElement.Value : null; }
                set
                {
                    if (value == null)
                        DirectionElement = null;
                    else
                        DirectionElement = new Code<Hl7.Fhir.Model.AssertionDirectionType>(value);
                    OnPropertyChanged("Direction");
                }
            }
            
            /// <summary>
            /// Id of the source fixture to be evaluated
            /// </summary>
            [FhirElement("compareToSourceId", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CompareToSourceIdElement
            {
                get { return _CompareToSourceIdElement; }
                set { _CompareToSourceIdElement = value; OnPropertyChanged("CompareToSourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CompareToSourceIdElement;
            
            /// <summary>
            /// Id of the source fixture to be evaluated
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CompareToSourceId
            {
                get { return CompareToSourceIdElement != null ? CompareToSourceIdElement.Value : null; }
                set
                {
                    if (value == null)
                        CompareToSourceIdElement = null;
                    else
                        CompareToSourceIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("CompareToSourceId");
                }
            }
            
            /// <summary>
            /// The fluentpath expression to evaluate against the source fixture
            /// </summary>
            [FhirElement("compareToSourceExpression", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CompareToSourceExpressionElement
            {
                get { return _CompareToSourceExpressionElement; }
                set { _CompareToSourceExpressionElement = value; OnPropertyChanged("CompareToSourceExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CompareToSourceExpressionElement;
            
            /// <summary>
            /// The fluentpath expression to evaluate against the source fixture
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CompareToSourceExpression
            {
                get { return CompareToSourceExpressionElement != null ? CompareToSourceExpressionElement.Value : null; }
                set
                {
                    if (value == null)
                        CompareToSourceExpressionElement = null;
                    else
                        CompareToSourceExpressionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("CompareToSourceExpression");
                }
            }
            
            /// <summary>
            /// XPath or JSONPath expression to evaluate against the source fixture
            /// </summary>
            [FhirElement("compareToSourcePath", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CompareToSourcePathElement
            {
                get { return _CompareToSourcePathElement; }
                set { _CompareToSourcePathElement = value; OnPropertyChanged("CompareToSourcePathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CompareToSourcePathElement;
            
            /// <summary>
            /// XPath or JSONPath expression to evaluate against the source fixture
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CompareToSourcePath
            {
                get { return CompareToSourcePathElement != null ? CompareToSourcePathElement.Value : null; }
                set
                {
                    if (value == null)
                        CompareToSourcePathElement = null;
                    else
                        CompareToSourcePathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("CompareToSourcePath");
                }
            }
            
            /// <summary>
            /// xml | json | ttl | none
            /// </summary>
            [FhirElement("contentType", Order=100)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.ContentType> ContentTypeElement
            {
                get { return _ContentTypeElement; }
                set { _ContentTypeElement = value; OnPropertyChanged("ContentTypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.ContentType> _ContentTypeElement;
            
            /// <summary>
            /// xml | json | ttl | none
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.ContentType? ContentType
            {
                get { return ContentTypeElement != null ? ContentTypeElement.Value : null; }
                set
                {
                    if (value == null)
                        ContentTypeElement = null;
                    else
                        ContentTypeElement = new Code<Hl7.Fhir.Model.STU3.ContentType>(value);
                    OnPropertyChanged("ContentType");
                }
            }
            
            /// <summary>
            /// The fluentpath expression to be evaluated
            /// </summary>
            [FhirElement("expression", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ExpressionElement
            {
                get { return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ExpressionElement;
            
            /// <summary>
            /// The fluentpath expression to be evaluated
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
            /// HTTP header field name
            /// </summary>
            [FhirElement("headerField", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HeaderFieldElement
            {
                get { return _HeaderFieldElement; }
                set { _HeaderFieldElement = value; OnPropertyChanged("HeaderFieldElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _HeaderFieldElement;
            
            /// <summary>
            /// HTTP header field name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string HeaderField
            {
                get { return HeaderFieldElement != null ? HeaderFieldElement.Value : null; }
                set
                {
                    if (value == null)
                        HeaderFieldElement = null;
                    else
                        HeaderFieldElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("HeaderField");
                }
            }
            
            /// <summary>
            /// Fixture Id of minimum content resource
            /// </summary>
            [FhirElement("minimumId", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MinimumIdElement
            {
                get { return _MinimumIdElement; }
                set { _MinimumIdElement = value; OnPropertyChanged("MinimumIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MinimumIdElement;
            
            /// <summary>
            /// Fixture Id of minimum content resource
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string MinimumId
            {
                get { return MinimumIdElement != null ? MinimumIdElement.Value : null; }
                set
                {
                    if (value == null)
                        MinimumIdElement = null;
                    else
                        MinimumIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("MinimumId");
                }
            }
            
            /// <summary>
            /// Perform validation on navigation links?
            /// </summary>
            [FhirElement("navigationLinks", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean NavigationLinksElement
            {
                get { return _NavigationLinksElement; }
                set { _NavigationLinksElement = value; OnPropertyChanged("NavigationLinksElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _NavigationLinksElement;
            
            /// <summary>
            /// Perform validation on navigation links?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? NavigationLinks
            {
                get { return NavigationLinksElement != null ? NavigationLinksElement.Value : null; }
                set
                {
                    if (value == null)
                        NavigationLinksElement = null;
                    else
                        NavigationLinksElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("NavigationLinks");
                }
            }
            
            /// <summary>
            /// equals | notEquals | in | notIn | greaterThan | lessThan | empty | notEmpty | contains | notContains | eval
            /// </summary>
            [FhirElement("operator", Order=150)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.AssertionOperatorType> OperatorElement
            {
                get { return _OperatorElement; }
                set { _OperatorElement = value; OnPropertyChanged("OperatorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.AssertionOperatorType> _OperatorElement;
            
            /// <summary>
            /// equals | notEquals | in | notIn | greaterThan | lessThan | empty | notEmpty | contains | notContains | eval
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.AssertionOperatorType? Operator
            {
                get { return OperatorElement != null ? OperatorElement.Value : null; }
                set
                {
                    if (value == null)
                        OperatorElement = null;
                    else
                        OperatorElement = new Code<Hl7.Fhir.Model.STU3.AssertionOperatorType>(value);
                    OnPropertyChanged("Operator");
                }
            }
            
            /// <summary>
            /// XPath or JSONPath expression
            /// </summary>
            [FhirElement("path", Order=160)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// XPath or JSONPath expression
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
            /// delete | get | options | patch | post | put
            /// </summary>
            [FhirElement("requestMethod", Order=170)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.TestScriptRequestMethodCode> RequestMethodElement
            {
                get { return _RequestMethodElement; }
                set { _RequestMethodElement = value; OnPropertyChanged("RequestMethodElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.TestScriptRequestMethodCode> _RequestMethodElement;
            
            /// <summary>
            /// delete | get | options | patch | post | put
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.TestScriptRequestMethodCode? RequestMethod
            {
                get { return RequestMethodElement != null ? RequestMethodElement.Value : null; }
                set
                {
                    if (value == null)
                        RequestMethodElement = null;
                    else
                        RequestMethodElement = new Code<Hl7.Fhir.Model.STU3.TestScriptRequestMethodCode>(value);
                    OnPropertyChanged("RequestMethod");
                }
            }
            
            /// <summary>
            /// Request URL comparison value
            /// </summary>
            [FhirElement("requestURL", Order=180)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RequestURLElement
            {
                get { return _RequestURLElement; }
                set { _RequestURLElement = value; OnPropertyChanged("RequestURLElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _RequestURLElement;
            
            /// <summary>
            /// Request URL comparison value
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RequestURL
            {
                get { return RequestURLElement != null ? RequestURLElement.Value : null; }
                set
                {
                    if (value == null)
                        RequestURLElement = null;
                    else
                        RequestURLElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("RequestURL");
                }
            }
            
            /// <summary>
            /// Resource type
            /// </summary>
            [FhirElement("resource", Order=190)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.FHIRDefinedType> ResourceElement
            {
                get { return _ResourceElement; }
                set { _ResourceElement = value; OnPropertyChanged("ResourceElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.FHIRDefinedType> _ResourceElement;
            
            /// <summary>
            /// Resource type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.FHIRDefinedType? Resource
            {
                get { return ResourceElement != null ? ResourceElement.Value : null; }
                set
                {
                    if (value == null)
                        ResourceElement = null;
                    else
                        ResourceElement = new Code<Hl7.Fhir.Model.STU3.FHIRDefinedType>(value);
                    OnPropertyChanged("Resource");
                }
            }
            
            /// <summary>
            /// okay | created | noContent | notModified | bad | forbidden | notFound | methodNotAllowed | conflict | gone | preconditionFailed | unprocessable
            /// </summary>
            [FhirElement("response", Order=200)]
            [DataMember]
            public Code<Hl7.Fhir.Model.AssertionResponseTypes> ResponseElement
            {
                get { return _ResponseElement; }
                set { _ResponseElement = value; OnPropertyChanged("ResponseElement"); }
            }
            
            private Code<Hl7.Fhir.Model.AssertionResponseTypes> _ResponseElement;
            
            /// <summary>
            /// okay | created | noContent | notModified | bad | forbidden | notFound | methodNotAllowed | conflict | gone | preconditionFailed | unprocessable
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AssertionResponseTypes? Response
            {
                get { return ResponseElement != null ? ResponseElement.Value : null; }
                set
                {
                    if (value == null)
                        ResponseElement = null;
                    else
                        ResponseElement = new Code<Hl7.Fhir.Model.AssertionResponseTypes>(value);
                    OnPropertyChanged("Response");
                }
            }
            
            /// <summary>
            /// HTTP response code to test
            /// </summary>
            [FhirElement("responseCode", Order=210)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ResponseCodeElement
            {
                get { return _ResponseCodeElement; }
                set { _ResponseCodeElement = value; OnPropertyChanged("ResponseCodeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ResponseCodeElement;
            
            /// <summary>
            /// HTTP response code to test
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ResponseCode
            {
                get { return ResponseCodeElement != null ? ResponseCodeElement.Value : null; }
                set
                {
                    if (value == null)
                        ResponseCodeElement = null;
                    else
                        ResponseCodeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ResponseCode");
                }
            }
            
            /// <summary>
            /// The reference to a TestScript.rule
            /// </summary>
            [FhirElement("rule", Order=220)]
            [DataMember]
            public ActionAssertRuleComponent Rule
            {
                get { return _Rule; }
                set { _Rule = value; OnPropertyChanged("Rule"); }
            }
            
            private ActionAssertRuleComponent _Rule;
            
            /// <summary>
            /// The reference to a TestScript.ruleset
            /// </summary>
            [FhirElement("ruleset", Order=230)]
            [DataMember]
            public ActionAssertRulesetComponent Ruleset
            {
                get { return _Ruleset; }
                set { _Ruleset = value; OnPropertyChanged("Ruleset"); }
            }
            
            private ActionAssertRulesetComponent _Ruleset;
            
            /// <summary>
            /// Fixture Id of source expression or headerField
            /// </summary>
            [FhirElement("sourceId", Order=240)]
            [DataMember]
            public Hl7.Fhir.Model.Id SourceIdElement
            {
                get { return _SourceIdElement; }
                set { _SourceIdElement = value; OnPropertyChanged("SourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _SourceIdElement;
            
            /// <summary>
            /// Fixture Id of source expression or headerField
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SourceId
            {
                get { return SourceIdElement != null ? SourceIdElement.Value : null; }
                set
                {
                    if (value == null)
                        SourceIdElement = null;
                    else
                        SourceIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("SourceId");
                }
            }
            
            /// <summary>
            /// Profile Id of validation profile reference
            /// </summary>
            [FhirElement("validateProfileId", Order=250)]
            [DataMember]
            public Hl7.Fhir.Model.Id ValidateProfileIdElement
            {
                get { return _ValidateProfileIdElement; }
                set { _ValidateProfileIdElement = value; OnPropertyChanged("ValidateProfileIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ValidateProfileIdElement;
            
            /// <summary>
            /// Profile Id of validation profile reference
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ValidateProfileId
            {
                get { return ValidateProfileIdElement != null ? ValidateProfileIdElement.Value : null; }
                set
                {
                    if (value == null)
                        ValidateProfileIdElement = null;
                    else
                        ValidateProfileIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("ValidateProfileId");
                }
            }
            
            /// <summary>
            /// The value to compare to
            /// </summary>
            [FhirElement("value", Order=260)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// The value to compare to
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
            /// Will this assert produce a warning only on error?
            /// </summary>
            [FhirElement("warningOnly", Order=270)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean WarningOnlyElement
            {
                get { return _WarningOnlyElement; }
                set { _WarningOnlyElement = value; OnPropertyChanged("WarningOnlyElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _WarningOnlyElement;
            
            /// <summary>
            /// Will this assert produce a warning only on error?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? WarningOnly
            {
                get { return WarningOnlyElement != null ? WarningOnlyElement.Value : null; }
                set
                {
                    if (value == null)
                        WarningOnlyElement = null;
                    else
                        WarningOnlyElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("WarningOnly");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AssertComponent");
                base.Serialize(sink);
                sink.Element("label", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LabelElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("direction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DirectionElement?.Serialize(sink);
                sink.Element("compareToSourceId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CompareToSourceIdElement?.Serialize(sink);
                sink.Element("compareToSourceExpression", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CompareToSourceExpressionElement?.Serialize(sink);
                sink.Element("compareToSourcePath", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CompareToSourcePathElement?.Serialize(sink);
                sink.Element("contentType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ContentTypeElement?.Serialize(sink);
                sink.Element("expression", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ExpressionElement?.Serialize(sink);
                sink.Element("headerField", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); HeaderFieldElement?.Serialize(sink);
                sink.Element("minimumId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); MinimumIdElement?.Serialize(sink);
                sink.Element("navigationLinks", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NavigationLinksElement?.Serialize(sink);
                sink.Element("operator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OperatorElement?.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PathElement?.Serialize(sink);
                sink.Element("requestMethod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequestMethodElement?.Serialize(sink);
                sink.Element("requestURL", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequestURLElement?.Serialize(sink);
                sink.Element("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ResourceElement?.Serialize(sink);
                sink.Element("response", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ResponseElement?.Serialize(sink);
                sink.Element("responseCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ResponseCodeElement?.Serialize(sink);
                sink.Element("rule", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Rule?.Serialize(sink);
                sink.Element("ruleset", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Ruleset?.Serialize(sink);
                sink.Element("sourceId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SourceIdElement?.Serialize(sink);
                sink.Element("validateProfileId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValidateProfileIdElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValueElement?.Serialize(sink);
                sink.Element("warningOnly", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); WarningOnlyElement?.Serialize(sink);
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
                    case "label":
                        LabelElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "direction":
                        DirectionElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AssertionDirectionType>>();
                        return true;
                    case "compareToSourceId":
                        CompareToSourceIdElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "compareToSourceExpression":
                        CompareToSourceExpressionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "compareToSourcePath":
                        CompareToSourcePathElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "contentType":
                        ContentTypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.ContentType>>();
                        return true;
                    case "expression":
                        ExpressionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "headerField":
                        HeaderFieldElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "minimumId":
                        MinimumIdElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "navigationLinks":
                        NavigationLinksElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "operator":
                        OperatorElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.AssertionOperatorType>>();
                        return true;
                    case "path":
                        PathElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "requestMethod":
                        RequestMethodElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.TestScriptRequestMethodCode>>();
                        return true;
                    case "requestURL":
                        RequestURLElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "resource":
                        ResourceElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.FHIRDefinedType>>();
                        return true;
                    case "response":
                        ResponseElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AssertionResponseTypes>>();
                        return true;
                    case "responseCode":
                        ResponseCodeElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "rule":
                        Rule = source.Get<ActionAssertRuleComponent>();
                        return true;
                    case "ruleset":
                        Ruleset = source.Get<ActionAssertRulesetComponent>();
                        return true;
                    case "sourceId":
                        SourceIdElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "validateProfileId":
                        ValidateProfileIdElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "warningOnly":
                        WarningOnlyElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
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
                    case "label":
                        LabelElement = source.PopulateValue(LabelElement);
                        return true;
                    case "_label":
                        LabelElement = source.Populate(LabelElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "direction":
                        DirectionElement = source.PopulateValue(DirectionElement);
                        return true;
                    case "_direction":
                        DirectionElement = source.Populate(DirectionElement);
                        return true;
                    case "compareToSourceId":
                        CompareToSourceIdElement = source.PopulateValue(CompareToSourceIdElement);
                        return true;
                    case "_compareToSourceId":
                        CompareToSourceIdElement = source.Populate(CompareToSourceIdElement);
                        return true;
                    case "compareToSourceExpression":
                        CompareToSourceExpressionElement = source.PopulateValue(CompareToSourceExpressionElement);
                        return true;
                    case "_compareToSourceExpression":
                        CompareToSourceExpressionElement = source.Populate(CompareToSourceExpressionElement);
                        return true;
                    case "compareToSourcePath":
                        CompareToSourcePathElement = source.PopulateValue(CompareToSourcePathElement);
                        return true;
                    case "_compareToSourcePath":
                        CompareToSourcePathElement = source.Populate(CompareToSourcePathElement);
                        return true;
                    case "contentType":
                        ContentTypeElement = source.PopulateValue(ContentTypeElement);
                        return true;
                    case "_contentType":
                        ContentTypeElement = source.Populate(ContentTypeElement);
                        return true;
                    case "expression":
                        ExpressionElement = source.PopulateValue(ExpressionElement);
                        return true;
                    case "_expression":
                        ExpressionElement = source.Populate(ExpressionElement);
                        return true;
                    case "headerField":
                        HeaderFieldElement = source.PopulateValue(HeaderFieldElement);
                        return true;
                    case "_headerField":
                        HeaderFieldElement = source.Populate(HeaderFieldElement);
                        return true;
                    case "minimumId":
                        MinimumIdElement = source.PopulateValue(MinimumIdElement);
                        return true;
                    case "_minimumId":
                        MinimumIdElement = source.Populate(MinimumIdElement);
                        return true;
                    case "navigationLinks":
                        NavigationLinksElement = source.PopulateValue(NavigationLinksElement);
                        return true;
                    case "_navigationLinks":
                        NavigationLinksElement = source.Populate(NavigationLinksElement);
                        return true;
                    case "operator":
                        OperatorElement = source.PopulateValue(OperatorElement);
                        return true;
                    case "_operator":
                        OperatorElement = source.Populate(OperatorElement);
                        return true;
                    case "path":
                        PathElement = source.PopulateValue(PathElement);
                        return true;
                    case "_path":
                        PathElement = source.Populate(PathElement);
                        return true;
                    case "requestMethod":
                        RequestMethodElement = source.PopulateValue(RequestMethodElement);
                        return true;
                    case "_requestMethod":
                        RequestMethodElement = source.Populate(RequestMethodElement);
                        return true;
                    case "requestURL":
                        RequestURLElement = source.PopulateValue(RequestURLElement);
                        return true;
                    case "_requestURL":
                        RequestURLElement = source.Populate(RequestURLElement);
                        return true;
                    case "resource":
                        ResourceElement = source.PopulateValue(ResourceElement);
                        return true;
                    case "_resource":
                        ResourceElement = source.Populate(ResourceElement);
                        return true;
                    case "response":
                        ResponseElement = source.PopulateValue(ResponseElement);
                        return true;
                    case "_response":
                        ResponseElement = source.Populate(ResponseElement);
                        return true;
                    case "responseCode":
                        ResponseCodeElement = source.PopulateValue(ResponseCodeElement);
                        return true;
                    case "_responseCode":
                        ResponseCodeElement = source.Populate(ResponseCodeElement);
                        return true;
                    case "rule":
                        Rule = source.Populate(Rule);
                        return true;
                    case "ruleset":
                        Ruleset = source.Populate(Ruleset);
                        return true;
                    case "sourceId":
                        SourceIdElement = source.PopulateValue(SourceIdElement);
                        return true;
                    case "_sourceId":
                        SourceIdElement = source.Populate(SourceIdElement);
                        return true;
                    case "validateProfileId":
                        ValidateProfileIdElement = source.PopulateValue(ValidateProfileIdElement);
                        return true;
                    case "_validateProfileId":
                        ValidateProfileIdElement = source.Populate(ValidateProfileIdElement);
                        return true;
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                    case "warningOnly":
                        WarningOnlyElement = source.PopulateValue(WarningOnlyElement);
                        return true;
                    case "_warningOnly":
                        WarningOnlyElement = source.Populate(WarningOnlyElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AssertComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(DirectionElement != null) dest.DirectionElement = (Code<Hl7.Fhir.Model.AssertionDirectionType>)DirectionElement.DeepCopy();
                    if(CompareToSourceIdElement != null) dest.CompareToSourceIdElement = (Hl7.Fhir.Model.FhirString)CompareToSourceIdElement.DeepCopy();
                    if(CompareToSourceExpressionElement != null) dest.CompareToSourceExpressionElement = (Hl7.Fhir.Model.FhirString)CompareToSourceExpressionElement.DeepCopy();
                    if(CompareToSourcePathElement != null) dest.CompareToSourcePathElement = (Hl7.Fhir.Model.FhirString)CompareToSourcePathElement.DeepCopy();
                    if(ContentTypeElement != null) dest.ContentTypeElement = (Code<Hl7.Fhir.Model.STU3.ContentType>)ContentTypeElement.DeepCopy();
                    if(ExpressionElement != null) dest.ExpressionElement = (Hl7.Fhir.Model.FhirString)ExpressionElement.DeepCopy();
                    if(HeaderFieldElement != null) dest.HeaderFieldElement = (Hl7.Fhir.Model.FhirString)HeaderFieldElement.DeepCopy();
                    if(MinimumIdElement != null) dest.MinimumIdElement = (Hl7.Fhir.Model.FhirString)MinimumIdElement.DeepCopy();
                    if(NavigationLinksElement != null) dest.NavigationLinksElement = (Hl7.Fhir.Model.FhirBoolean)NavigationLinksElement.DeepCopy();
                    if(OperatorElement != null) dest.OperatorElement = (Code<Hl7.Fhir.Model.STU3.AssertionOperatorType>)OperatorElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(RequestMethodElement != null) dest.RequestMethodElement = (Code<Hl7.Fhir.Model.STU3.TestScriptRequestMethodCode>)RequestMethodElement.DeepCopy();
                    if(RequestURLElement != null) dest.RequestURLElement = (Hl7.Fhir.Model.FhirString)RequestURLElement.DeepCopy();
                    if(ResourceElement != null) dest.ResourceElement = (Code<Hl7.Fhir.Model.STU3.FHIRDefinedType>)ResourceElement.DeepCopy();
                    if(ResponseElement != null) dest.ResponseElement = (Code<Hl7.Fhir.Model.AssertionResponseTypes>)ResponseElement.DeepCopy();
                    if(ResponseCodeElement != null) dest.ResponseCodeElement = (Hl7.Fhir.Model.FhirString)ResponseCodeElement.DeepCopy();
                    if(Rule != null) dest.Rule = (ActionAssertRuleComponent)Rule.DeepCopy();
                    if(Ruleset != null) dest.Ruleset = (ActionAssertRulesetComponent)Ruleset.DeepCopy();
                    if(SourceIdElement != null) dest.SourceIdElement = (Hl7.Fhir.Model.Id)SourceIdElement.DeepCopy();
                    if(ValidateProfileIdElement != null) dest.ValidateProfileIdElement = (Hl7.Fhir.Model.Id)ValidateProfileIdElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    if(WarningOnlyElement != null) dest.WarningOnlyElement = (Hl7.Fhir.Model.FhirBoolean)WarningOnlyElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AssertComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AssertComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(DirectionElement, otherT.DirectionElement)) return false;
                if( !DeepComparable.Matches(CompareToSourceIdElement, otherT.CompareToSourceIdElement)) return false;
                if( !DeepComparable.Matches(CompareToSourceExpressionElement, otherT.CompareToSourceExpressionElement)) return false;
                if( !DeepComparable.Matches(CompareToSourcePathElement, otherT.CompareToSourcePathElement)) return false;
                if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
                if( !DeepComparable.Matches(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.Matches(MinimumIdElement, otherT.MinimumIdElement)) return false;
                if( !DeepComparable.Matches(NavigationLinksElement, otherT.NavigationLinksElement)) return false;
                if( !DeepComparable.Matches(OperatorElement, otherT.OperatorElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(RequestMethodElement, otherT.RequestMethodElement)) return false;
                if( !DeepComparable.Matches(RequestURLElement, otherT.RequestURLElement)) return false;
                if( !DeepComparable.Matches(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.Matches(ResponseElement, otherT.ResponseElement)) return false;
                if( !DeepComparable.Matches(ResponseCodeElement, otherT.ResponseCodeElement)) return false;
                if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
                if( !DeepComparable.Matches(Ruleset, otherT.Ruleset)) return false;
                if( !DeepComparable.Matches(SourceIdElement, otherT.SourceIdElement)) return false;
                if( !DeepComparable.Matches(ValidateProfileIdElement, otherT.ValidateProfileIdElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.Matches(WarningOnlyElement, otherT.WarningOnlyElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AssertComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(DirectionElement, otherT.DirectionElement)) return false;
                if( !DeepComparable.IsExactly(CompareToSourceIdElement, otherT.CompareToSourceIdElement)) return false;
                if( !DeepComparable.IsExactly(CompareToSourceExpressionElement, otherT.CompareToSourceExpressionElement)) return false;
                if( !DeepComparable.IsExactly(CompareToSourcePathElement, otherT.CompareToSourcePathElement)) return false;
                if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
                if( !DeepComparable.IsExactly(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.IsExactly(MinimumIdElement, otherT.MinimumIdElement)) return false;
                if( !DeepComparable.IsExactly(NavigationLinksElement, otherT.NavigationLinksElement)) return false;
                if( !DeepComparable.IsExactly(OperatorElement, otherT.OperatorElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(RequestMethodElement, otherT.RequestMethodElement)) return false;
                if( !DeepComparable.IsExactly(RequestURLElement, otherT.RequestURLElement)) return false;
                if( !DeepComparable.IsExactly(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.IsExactly(ResponseElement, otherT.ResponseElement)) return false;
                if( !DeepComparable.IsExactly(ResponseCodeElement, otherT.ResponseCodeElement)) return false;
                if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
                if( !DeepComparable.IsExactly(Ruleset, otherT.Ruleset)) return false;
                if( !DeepComparable.IsExactly(SourceIdElement, otherT.SourceIdElement)) return false;
                if( !DeepComparable.IsExactly(ValidateProfileIdElement, otherT.ValidateProfileIdElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.IsExactly(WarningOnlyElement, otherT.WarningOnlyElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LabelElement != null) yield return LabelElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (DirectionElement != null) yield return DirectionElement;
                    if (CompareToSourceIdElement != null) yield return CompareToSourceIdElement;
                    if (CompareToSourceExpressionElement != null) yield return CompareToSourceExpressionElement;
                    if (CompareToSourcePathElement != null) yield return CompareToSourcePathElement;
                    if (ContentTypeElement != null) yield return ContentTypeElement;
                    if (ExpressionElement != null) yield return ExpressionElement;
                    if (HeaderFieldElement != null) yield return HeaderFieldElement;
                    if (MinimumIdElement != null) yield return MinimumIdElement;
                    if (NavigationLinksElement != null) yield return NavigationLinksElement;
                    if (OperatorElement != null) yield return OperatorElement;
                    if (PathElement != null) yield return PathElement;
                    if (RequestMethodElement != null) yield return RequestMethodElement;
                    if (RequestURLElement != null) yield return RequestURLElement;
                    if (ResourceElement != null) yield return ResourceElement;
                    if (ResponseElement != null) yield return ResponseElement;
                    if (ResponseCodeElement != null) yield return ResponseCodeElement;
                    if (Rule != null) yield return Rule;
                    if (Ruleset != null) yield return Ruleset;
                    if (SourceIdElement != null) yield return SourceIdElement;
                    if (ValidateProfileIdElement != null) yield return ValidateProfileIdElement;
                    if (ValueElement != null) yield return ValueElement;
                    if (WarningOnlyElement != null) yield return WarningOnlyElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (LabelElement != null) yield return new ElementValue("label", LabelElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (DirectionElement != null) yield return new ElementValue("direction", DirectionElement);
                    if (CompareToSourceIdElement != null) yield return new ElementValue("compareToSourceId", CompareToSourceIdElement);
                    if (CompareToSourceExpressionElement != null) yield return new ElementValue("compareToSourceExpression", CompareToSourceExpressionElement);
                    if (CompareToSourcePathElement != null) yield return new ElementValue("compareToSourcePath", CompareToSourcePathElement);
                    if (ContentTypeElement != null) yield return new ElementValue("contentType", ContentTypeElement);
                    if (ExpressionElement != null) yield return new ElementValue("expression", ExpressionElement);
                    if (HeaderFieldElement != null) yield return new ElementValue("headerField", HeaderFieldElement);
                    if (MinimumIdElement != null) yield return new ElementValue("minimumId", MinimumIdElement);
                    if (NavigationLinksElement != null) yield return new ElementValue("navigationLinks", NavigationLinksElement);
                    if (OperatorElement != null) yield return new ElementValue("operator", OperatorElement);
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (RequestMethodElement != null) yield return new ElementValue("requestMethod", RequestMethodElement);
                    if (RequestURLElement != null) yield return new ElementValue("requestURL", RequestURLElement);
                    if (ResourceElement != null) yield return new ElementValue("resource", ResourceElement);
                    if (ResponseElement != null) yield return new ElementValue("response", ResponseElement);
                    if (ResponseCodeElement != null) yield return new ElementValue("responseCode", ResponseCodeElement);
                    if (Rule != null) yield return new ElementValue("rule", Rule);
                    if (Ruleset != null) yield return new ElementValue("ruleset", Ruleset);
                    if (SourceIdElement != null) yield return new ElementValue("sourceId", SourceIdElement);
                    if (ValidateProfileIdElement != null) yield return new ElementValue("validateProfileId", ValidateProfileIdElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                    if (WarningOnlyElement != null) yield return new ElementValue("warningOnly", WarningOnlyElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ActionAssertRuleComponent")]
        [DataContract]
        public partial class ActionAssertRuleComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ActionAssertRuleComponent"; } }
            
            /// <summary>
            /// Id of the TestScript.rule
            /// </summary>
            [FhirElement("ruleId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id RuleIdElement
            {
                get { return _RuleIdElement; }
                set { _RuleIdElement = value; OnPropertyChanged("RuleIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _RuleIdElement;
            
            /// <summary>
            /// Id of the TestScript.rule
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RuleId
            {
                get { return RuleIdElement != null ? RuleIdElement.Value : null; }
                set
                {
                    if (value == null)
                        RuleIdElement = null;
                    else
                        RuleIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("RuleId");
                }
            }
            
            /// <summary>
            /// Rule parameter template
            /// </summary>
            [FhirElement("param", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ActionAssertRuleParamComponent> Param
            {
                get { if(_Param==null) _Param = new List<ActionAssertRuleParamComponent>(); return _Param; }
                set { _Param = value; OnPropertyChanged("Param"); }
            }
            
            private List<ActionAssertRuleParamComponent> _Param;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ActionAssertRuleComponent");
                base.Serialize(sink);
                sink.Element("ruleId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); RuleIdElement?.Serialize(sink);
                sink.BeginList("param", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Param)
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
                    case "ruleId":
                        RuleIdElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "param":
                        Param = source.GetList<ActionAssertRuleParamComponent>();
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
                    case "ruleId":
                        RuleIdElement = source.PopulateValue(RuleIdElement);
                        return true;
                    case "_ruleId":
                        RuleIdElement = source.Populate(RuleIdElement);
                        return true;
                    case "param":
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
                    case "param":
                        source.PopulateListItem(Param, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionAssertRuleComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RuleIdElement != null) dest.RuleIdElement = (Hl7.Fhir.Model.Id)RuleIdElement.DeepCopy();
                    if(Param != null) dest.Param = new List<ActionAssertRuleParamComponent>(Param.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ActionAssertRuleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActionAssertRuleComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RuleIdElement, otherT.RuleIdElement)) return false;
                if( !DeepComparable.Matches(Param, otherT.Param)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionAssertRuleComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RuleIdElement, otherT.RuleIdElement)) return false;
                if( !DeepComparable.IsExactly(Param, otherT.Param)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RuleIdElement != null) yield return RuleIdElement;
                    foreach (var elem in Param) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RuleIdElement != null) yield return new ElementValue("ruleId", RuleIdElement);
                    foreach (var elem in Param) { if (elem != null) yield return new ElementValue("param", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ActionAssertRuleParamComponent")]
        [DataContract]
        public partial class ActionAssertRuleParamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ActionAssertRuleParamComponent"; } }
            
            /// <summary>
            /// Parameter name matching external assert rule parameter
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Parameter name matching external assert rule parameter
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
            /// Parameter value defined either explicitly or dynamically
            /// </summary>
            [FhirElement("value", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Parameter value defined either explicitly or dynamically
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ActionAssertRuleParamComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ValueElement?.Serialize(sink);
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
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionAssertRuleParamComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ActionAssertRuleParamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActionAssertRuleParamComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionAssertRuleParamComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ActionAssertRulesetComponent")]
        [DataContract]
        public partial class ActionAssertRulesetComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ActionAssertRulesetComponent"; } }
            
            /// <summary>
            /// Id of the TestScript.ruleset
            /// </summary>
            [FhirElement("rulesetId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id RulesetIdElement
            {
                get { return _RulesetIdElement; }
                set { _RulesetIdElement = value; OnPropertyChanged("RulesetIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _RulesetIdElement;
            
            /// <summary>
            /// Id of the TestScript.ruleset
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RulesetId
            {
                get { return RulesetIdElement != null ? RulesetIdElement.Value : null; }
                set
                {
                    if (value == null)
                        RulesetIdElement = null;
                    else
                        RulesetIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("RulesetId");
                }
            }
            
            /// <summary>
            /// The referenced rule within the ruleset
            /// </summary>
            [FhirElement("rule", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ActionAssertRulesetRuleComponent> Rule
            {
                get { if(_Rule==null) _Rule = new List<ActionAssertRulesetRuleComponent>(); return _Rule; }
                set { _Rule = value; OnPropertyChanged("Rule"); }
            }
            
            private List<ActionAssertRulesetRuleComponent> _Rule;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ActionAssertRulesetComponent");
                base.Serialize(sink);
                sink.Element("rulesetId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); RulesetIdElement?.Serialize(sink);
                sink.BeginList("rule", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Rule)
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
                    case "rulesetId":
                        RulesetIdElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "rule":
                        Rule = source.GetList<ActionAssertRulesetRuleComponent>();
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
                    case "rulesetId":
                        RulesetIdElement = source.PopulateValue(RulesetIdElement);
                        return true;
                    case "_rulesetId":
                        RulesetIdElement = source.Populate(RulesetIdElement);
                        return true;
                    case "rule":
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
                    case "rule":
                        source.PopulateListItem(Rule, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionAssertRulesetComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RulesetIdElement != null) dest.RulesetIdElement = (Hl7.Fhir.Model.Id)RulesetIdElement.DeepCopy();
                    if(Rule != null) dest.Rule = new List<ActionAssertRulesetRuleComponent>(Rule.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ActionAssertRulesetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActionAssertRulesetComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RulesetIdElement, otherT.RulesetIdElement)) return false;
                if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionAssertRulesetComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RulesetIdElement, otherT.RulesetIdElement)) return false;
                if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RulesetIdElement != null) yield return RulesetIdElement;
                    foreach (var elem in Rule) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RulesetIdElement != null) yield return new ElementValue("rulesetId", RulesetIdElement);
                    foreach (var elem in Rule) { if (elem != null) yield return new ElementValue("rule", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ActionAssertRulesetRuleComponent")]
        [DataContract]
        public partial class ActionAssertRulesetRuleComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ActionAssertRulesetRuleComponent"; } }
            
            /// <summary>
            /// Id of referenced rule within the ruleset
            /// </summary>
            [FhirElement("ruleId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id RuleIdElement
            {
                get { return _RuleIdElement; }
                set { _RuleIdElement = value; OnPropertyChanged("RuleIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _RuleIdElement;
            
            /// <summary>
            /// Id of referenced rule within the ruleset
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RuleId
            {
                get { return RuleIdElement != null ? RuleIdElement.Value : null; }
                set
                {
                    if (value == null)
                        RuleIdElement = null;
                    else
                        RuleIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("RuleId");
                }
            }
            
            /// <summary>
            /// Rule parameter template
            /// </summary>
            [FhirElement("param", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ParamComponent> Param
            {
                get { if(_Param==null) _Param = new List<ParamComponent>(); return _Param; }
                set { _Param = value; OnPropertyChanged("Param"); }
            }
            
            private List<ParamComponent> _Param;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ActionAssertRulesetRuleComponent");
                base.Serialize(sink);
                sink.Element("ruleId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); RuleIdElement?.Serialize(sink);
                sink.BeginList("param", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Param)
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
                    case "ruleId":
                        RuleIdElement = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "param":
                        Param = source.GetList<ParamComponent>();
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
                    case "ruleId":
                        RuleIdElement = source.PopulateValue(RuleIdElement);
                        return true;
                    case "_ruleId":
                        RuleIdElement = source.Populate(RuleIdElement);
                        return true;
                    case "param":
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
                    case "param":
                        source.PopulateListItem(Param, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionAssertRulesetRuleComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RuleIdElement != null) dest.RuleIdElement = (Hl7.Fhir.Model.Id)RuleIdElement.DeepCopy();
                    if(Param != null) dest.Param = new List<ParamComponent>(Param.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ActionAssertRulesetRuleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActionAssertRulesetRuleComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RuleIdElement, otherT.RuleIdElement)) return false;
                if( !DeepComparable.Matches(Param, otherT.Param)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionAssertRulesetRuleComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RuleIdElement, otherT.RuleIdElement)) return false;
                if( !DeepComparable.IsExactly(Param, otherT.Param)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RuleIdElement != null) yield return RuleIdElement;
                    foreach (var elem in Param) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RuleIdElement != null) yield return new ElementValue("ruleId", RuleIdElement);
                    foreach (var elem in Param) { if (elem != null) yield return new ElementValue("param", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ParamComponent")]
        [DataContract]
        public partial class ParamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParamComponent"; } }
            
            /// <summary>
            /// Parameter name matching external assert ruleset rule parameter
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Parameter name matching external assert ruleset rule parameter
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
            /// Parameter value defined either explicitly or dynamically
            /// </summary>
            [FhirElement("value", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Parameter value defined either explicitly or dynamically
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParamComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ValueElement?.Serialize(sink);
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
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParamComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ParamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParamComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParamComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "TestComponent")]
        [DataContract]
        public partial class TestComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptTestComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TestComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ITestScriptTestActionComponent> Hl7.Fhir.Model.ITestScriptTestComponent.Action { get { return Action; } }
            
            /// <summary>
            /// Tracking/logging name of this test
            /// </summary>
            [FhirElement("name", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Tracking/logging name of this test
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
            /// Tracking/reporting short description of the test
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Tracking/reporting short description of the test
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
            /// A test operation or assert to perform
            /// </summary>
            [FhirElement("action", Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<TestActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<TestActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<TestActionComponent> _Action;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TestComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NameElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.BeginList("action", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Action)
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
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "action":
                        Action = source.GetList<TestActionComponent>();
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
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "action":
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
                    case "action":
                        source.PopulateListItem(Action, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Action != null) dest.Action = new List<TestActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TestComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "TestActionComponent")]
        [DataContract]
        public partial class TestActionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptTestActionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TestActionComponent"; } }
            
            [NotMapped]
            Hl7.Fhir.Model.ITestScriptOperationComponent Hl7.Fhir.Model.ITestScriptTestActionComponent.Operation { get { return Operation; } }
            
            [NotMapped]
            Hl7.Fhir.Model.ITestScriptAssertComponent Hl7.Fhir.Model.ITestScriptTestActionComponent.Assert { get { return Assert; } }
            
            /// <summary>
            /// The setup operation to perform
            /// </summary>
            [FhirElement("operation", Order=40)]
            [DataMember]
            public OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private OperationComponent _Operation;
            
            /// <summary>
            /// The setup assertion to perform
            /// </summary>
            [FhirElement("assert", Order=50)]
            [DataMember]
            public AssertComponent Assert
            {
                get { return _Assert; }
                set { _Assert = value; OnPropertyChanged("Assert"); }
            }
            
            private AssertComponent _Assert;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TestActionComponent");
                base.Serialize(sink);
                sink.Element("operation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Operation?.Serialize(sink);
                sink.Element("assert", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Assert?.Serialize(sink);
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
                    case "operation":
                        Operation = source.Get<OperationComponent>();
                        return true;
                    case "assert":
                        Assert = source.Get<AssertComponent>();
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
                    case "operation":
                        Operation = source.Populate(Operation);
                        return true;
                    case "assert":
                        Assert = source.Populate(Assert);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestActionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (OperationComponent)Operation.DeepCopy();
                    if(Assert != null) dest.Assert = (AssertComponent)Assert.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TestActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestActionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                if( !DeepComparable.Matches(Assert, otherT.Assert)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestActionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                if( !DeepComparable.IsExactly(Assert, otherT.Assert)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Operation != null) yield return Operation;
                    if (Assert != null) yield return Assert;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Operation != null) yield return new ElementValue("operation", Operation);
                    if (Assert != null) yield return new ElementValue("assert", Assert);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "TeardownComponent")]
        [DataContract]
        public partial class TeardownComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptTeardownComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TeardownComponent"; } }
            
            /// <summary>
            /// One or more teardown operations to perform
            /// </summary>
            [FhirElement("action", Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<TeardownActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<TeardownActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<TeardownActionComponent> _Action;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TeardownComponent");
                base.Serialize(sink);
                sink.BeginList("action", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Action)
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
                    case "action":
                        Action = source.GetList<TeardownActionComponent>();
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
                    case "action":
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
                    case "action":
                        source.PopulateListItem(Action, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TeardownComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Action != null) dest.Action = new List<TeardownActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TeardownComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TeardownComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TeardownComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "TeardownActionComponent")]
        [DataContract]
        public partial class TeardownActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TeardownActionComponent"; } }
            
            /// <summary>
            /// The teardown operation to perform
            /// </summary>
            [FhirElement("operation", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private OperationComponent _Operation;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TeardownActionComponent");
                base.Serialize(sink);
                sink.Element("operation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Operation?.Serialize(sink);
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
                    case "operation":
                        Operation = source.Get<OperationComponent>();
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
                    case "operation":
                        Operation = source.Populate(Operation);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TeardownActionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (OperationComponent)Operation.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TeardownActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TeardownActionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TeardownActionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Operation != null) yield return Operation;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Operation != null) yield return new ElementValue("operation", Operation);
                }
            }
        
        
        }
        
        [NotMapped]
        Hl7.Fhir.Model.ITestScriptMetadataComponent Hl7.Fhir.Model.ITestScript.Metadata { get { return Metadata; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ITestScriptFixtureComponent> Hl7.Fhir.Model.ITestScript.Fixture { get { return Fixture; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ITestScriptVariableComponent> Hl7.Fhir.Model.ITestScript.Variable { get { return Variable; } }
        
        [NotMapped]
        Hl7.Fhir.Model.ITestScriptSetupComponent Hl7.Fhir.Model.ITestScript.Setup { get { return Setup; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ITestScriptTestComponent> Hl7.Fhir.Model.ITestScript.Test { get { return Test; } }
        
        [NotMapped]
        Hl7.Fhir.Model.ITestScriptTeardownComponent Hl7.Fhir.Model.ITestScript.Teardown { get { return Teardown; } }
    
        
        /// <summary>
        /// Logical URI to reference this test script (globally unique)
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
        /// Logical URI to reference this test script (globally unique)
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
        /// Additional identifier for the test script
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
        /// Business version of the test script
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
        /// Business version of the test script
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
        /// Name for this test script (computer friendly)
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
        /// Name for this test script (computer friendly)
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
        /// Name for this test script (human friendly)
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
        /// Name for this test script (human friendly)
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
        /// Date this was last changed
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
        /// Date this was last changed
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
        public List<Hl7.Fhir.Model.STU3.ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the test script
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
        /// Natural language description of the test script
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
        /// Context the content is intended to support
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
        /// Intended jurisdiction for test script (if applicable)
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
        /// Why this test script is defined
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
        /// Why this test script is defined
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
        /// An abstract server representing a client or sender in a message exchange
        /// </summary>
        [FhirElement("origin", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<OriginComponent> Origin
        {
            get { if(_Origin==null) _Origin = new List<OriginComponent>(); return _Origin; }
            set { _Origin = value; OnPropertyChanged("Origin"); }
        }
        
        private List<OriginComponent> _Origin;
        
        /// <summary>
        /// An abstract server representing a destination or receiver in a message exchange
        /// </summary>
        [FhirElement("destination", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DestinationComponent> Destination
        {
            get { if(_Destination==null) _Destination = new List<DestinationComponent>(); return _Destination; }
            set { _Destination = value; OnPropertyChanged("Destination"); }
        }
        
        private List<DestinationComponent> _Destination;
        
        /// <summary>
        /// Required capability that is assumed to function correctly on the FHIR server being tested
        /// </summary>
        [FhirElement("metadata", Order=260)]
        [DataMember]
        public MetadataComponent Metadata
        {
            get { return _Metadata; }
            set { _Metadata = value; OnPropertyChanged("Metadata"); }
        }
        
        private MetadataComponent _Metadata;
        
        /// <summary>
        /// Fixture in the test script - by reference (uri)
        /// </summary>
        [FhirElement("fixture", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<FixtureComponent> Fixture
        {
            get { if(_Fixture==null) _Fixture = new List<FixtureComponent>(); return _Fixture; }
            set { _Fixture = value; OnPropertyChanged("Fixture"); }
        }
        
        private List<FixtureComponent> _Fixture;
        
        /// <summary>
        /// Reference of the validation profile
        /// </summary>
        [FhirElement("profile", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Profile
        {
            get { if(_Profile==null) _Profile = new List<Hl7.Fhir.Model.ResourceReference>(); return _Profile; }
            set { _Profile = value; OnPropertyChanged("Profile"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Profile;
        
        /// <summary>
        /// Placeholder for evaluated elements
        /// </summary>
        [FhirElement("variable", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<VariableComponent> Variable
        {
            get { if(_Variable==null) _Variable = new List<VariableComponent>(); return _Variable; }
            set { _Variable = value; OnPropertyChanged("Variable"); }
        }
        
        private List<VariableComponent> _Variable;
        
        /// <summary>
        /// Assert rule used within the test script
        /// </summary>
        [FhirElement("rule", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<RuleComponent> Rule
        {
            get { if(_Rule==null) _Rule = new List<RuleComponent>(); return _Rule; }
            set { _Rule = value; OnPropertyChanged("Rule"); }
        }
        
        private List<RuleComponent> _Rule;
        
        /// <summary>
        /// Assert ruleset used within the test script
        /// </summary>
        [FhirElement("ruleset", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<RulesetComponent> Ruleset
        {
            get { if(_Ruleset==null) _Ruleset = new List<RulesetComponent>(); return _Ruleset; }
            set { _Ruleset = value; OnPropertyChanged("Ruleset"); }
        }
        
        private List<RulesetComponent> _Ruleset;
        
        /// <summary>
        /// A series of required setup operations before tests are executed
        /// </summary>
        [FhirElement("setup", Order=320)]
        [DataMember]
        public SetupComponent Setup
        {
            get { return _Setup; }
            set { _Setup = value; OnPropertyChanged("Setup"); }
        }
        
        private SetupComponent _Setup;
        
        /// <summary>
        /// A test in this script
        /// </summary>
        [FhirElement("test", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<TestComponent> Test
        {
            get { if(_Test==null) _Test = new List<TestComponent>(); return _Test; }
            set { _Test = value; OnPropertyChanged("Test"); }
        }
        
        private List<TestComponent> _Test;
        
        /// <summary>
        /// A series of required clean up steps
        /// </summary>
        [FhirElement("teardown", Order=340)]
        [DataMember]
        public TeardownComponent Teardown
        {
            get { return _Teardown; }
            set { _Teardown = value; OnPropertyChanged("Teardown"); }
        }
        
        private TeardownComponent _Teardown;
    
    
        public static ElementDefinitionConstraint[] TestScript_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-4",
                severity: ConstraintSeverity.Warning,
                expression: "metadata.all(capability.required.exists() or capability.validated.exists())",
                human: "TestScript metadata capability SHALL contain required or validated or both.",
                xpath: "f:capability/f:required or f:capability/f:validated or (f:capability/f:required and f:capability/f:validated)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-3",
                severity: ConstraintSeverity.Warning,
                expression: "variable.all(expression.empty() or headerField.empty() or path.empty())",
                human: "Variable can only contain one of expression, headerField or path.",
                xpath: "not(f:expression and f:headerField and f:path)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-1",
                severity: ConstraintSeverity.Warning,
                expression: "setup.action.all(operation.exists() xor assert.exists())",
                human: "Setup action SHALL contain either an operation or assert but not both.",
                xpath: "(f:operation or f:assert) and not(f:operation and f:assert)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-7",
                severity: ConstraintSeverity.Warning,
                expression: "setup.action.operation.all(sourceId.exists() or (targetId.count() + url.count() + params.count() = 1) or (type.code in ('capabilities' |'search' | 'transaction' | 'history')))",
                human: "Setup operation SHALL contain either sourceId or targetId or params or url.",
                xpath: "f:sourceId or ((f:targetId or f:url or f:params) and (count(f:targetId) + count(f:url) + count(f:params) =1)) or (f:type/f:code/@value='capabilities' or f:type/f:code/@value='search' or f:type/f:code/@value='transaction' or f:type/f:code/@value='history')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-5",
                severity: ConstraintSeverity.Warning,
                expression: "setup.action.assert.all(contentType.count() + expression.count() + headerField.count() + minimumId.count() + navigationLinks.count() + path.count() + requestMethod.count() + resource.count() + responseCode.count() + response.count() + rule.count() + ruleset.count() + validateProfileId.count() <=1)",
                human: "Only a single assertion SHALL be present within setup action assert element.",
                xpath: "count(f:contentType) + count(f:expression) + count(f:headerField) + count(f:minimumId) + count(f:navigationLinks) + count(f:path) + count(f:requestMethod) + count(f:resource) + count(f:responseCode) + count(f:response) + count(f:rule) + count(f:ruleset) + count(f:validateProfileId)  <=1"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-10",
                severity: ConstraintSeverity.Warning,
                expression: "setup.action.assert.all(compareToSourceId.empty() xor (compareToSourceExpression.exists() or compareToSourcePath.exists()))",
                human: "Setup action assert SHALL contain either compareToSourceId and compareToSourceExpression, compareToSourceId and compareToSourcePath or neither.",
                xpath: "(f:compareToSourceId and f:compareToSourceExpression) or (f:compareToSourceId and f:compareToSourcePath) or not(f:compareToSourceId or f:compareToSourceExpression or f:compareToSourcePath)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-12",
                severity: ConstraintSeverity.Warning,
                expression: "setup.action.assert.all((response.empty() and responseCode.empty() and direction = 'request') or direction.empty() or direction = 'response')",
                human: "Setup action assert response and responseCode SHALL be empty when direction equals request",
                xpath: "((count(f:response) + count(f:responseCode)) = 0 and (f:direction/@value='request')) or (count(f:direction) = 0) or (f:direction/@value='response')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-2",
                severity: ConstraintSeverity.Warning,
                expression: "test.action.all(operation.exists() xor assert.exists())",
                human: "Test action SHALL contain either an operation or assert but not both.",
                xpath: "(f:operation or f:assert) and not(f:operation and f:assert)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-8",
                severity: ConstraintSeverity.Warning,
                expression: "test.action.operation.all(sourceId.exists() or (targetId.count() + url.count() + params.count() = 1) or (type.code in ('capabilities' | 'search' | 'transaction' | 'history')))",
                human: "Test operation SHALL contain either sourceId or targetId or params or url.",
                xpath: "f:sourceId or (f:targetId or f:url or f:params) and (count(f:targetId) + count(f:url) + count(f:params) =1) or (f:type/f:code/@value='capabilities' or f:type/f:code/@value='search' or f:type/f:code/@value='transaction' or f:type/f:code/@value='history')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-6",
                severity: ConstraintSeverity.Warning,
                expression: "test.action.assert.all(contentType.count() + expression.count() + headerField.count() + minimumId.count() + navigationLinks.count() + path.count() + requestMethod.count() + resource.count() + responseCode.count() + response.count() + rule.count() + ruleset.count() + validateProfileId.count() <=1)",
                human: "Only a single assertion SHALL be present within test action assert element.",
                xpath: "count(f:contentType) + count(f:expression) + count(f:headerField) + count(f:minimumId) + count(f:navigationLinks) + count(f:path) + count(f:requestMethod) + count(f:resource) + count(f:responseCode) + count(f:response) + count(f:rule) + count(f:ruleset) + count(f:validateProfileId)  <=1"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-11",
                severity: ConstraintSeverity.Warning,
                expression: "test.action.assert.all(compareToSourceId.empty() xor (compareToSourceExpression.exists() or compareToSourcePath.exists()))",
                human: "Test action assert SHALL contain either compareToSourceId and compareToSourceExpression, compareToSourceId and compareToSourcePath or neither.",
                xpath: "(f:compareToSourceId and f:compareToSourceExpression) or (f:compareToSourceId and f:compareToSourcePath) or not(f:compareToSourceId or f:compareToSourceExpression or f:compareToSourcePath)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-13",
                severity: ConstraintSeverity.Warning,
                expression: "test.action.assert.all((response.empty() and responseCode.empty() and direction = 'request') or direction.empty() or direction = 'response')",
                human: "Test action assert response and response and responseCode SHALL be empty when direction equals request",
                xpath: "((count(f:response) + count(f:responseCode)) = 0 and (f:direction/@value='request')) or (count(f:direction) = 0) or (f:direction/@value='response')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-9",
                severity: ConstraintSeverity.Warning,
                expression: "teardown.action.operation.all(sourceId.exists() or (targetId.count() + url.count() + params.count() = 1) or (type.code in ('capabilities' | 'search' | 'transaction' | 'history')))",
                human: "Teardown operation SHALL contain either sourceId or targetId or params or url.",
                xpath: "f:sourceId or (f:targetId or f:url or (f:params and f:resource)) and (count(f:targetId) + count(f:url) + count(f:params) =1) or (f:type/f:code/@value='capabilities' or f:type/f:code/@value='search' or f:type/f:code/@value='transaction' or f:type/f:code/@value='history')"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(TestScript_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TestScript;
        
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
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(Contact.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.Markdown)PurposeElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.Markdown)CopyrightElement.DeepCopy();
                if(Origin != null) dest.Origin = new List<OriginComponent>(Origin.DeepCopy());
                if(Destination != null) dest.Destination = new List<DestinationComponent>(Destination.DeepCopy());
                if(Metadata != null) dest.Metadata = (MetadataComponent)Metadata.DeepCopy();
                if(Fixture != null) dest.Fixture = new List<FixtureComponent>(Fixture.DeepCopy());
                if(Profile != null) dest.Profile = new List<Hl7.Fhir.Model.ResourceReference>(Profile.DeepCopy());
                if(Variable != null) dest.Variable = new List<VariableComponent>(Variable.DeepCopy());
                if(Rule != null) dest.Rule = new List<RuleComponent>(Rule.DeepCopy());
                if(Ruleset != null) dest.Ruleset = new List<RulesetComponent>(Ruleset.DeepCopy());
                if(Setup != null) dest.Setup = (SetupComponent)Setup.DeepCopy();
                if(Test != null) dest.Test = new List<TestComponent>(Test.DeepCopy());
                if(Teardown != null) dest.Teardown = (TeardownComponent)Teardown.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new TestScript());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as TestScript;
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
            if( !DeepComparable.Matches(Origin, otherT.Origin)) return false;
            if( !DeepComparable.Matches(Destination, otherT.Destination)) return false;
            if( !DeepComparable.Matches(Metadata, otherT.Metadata)) return false;
            if( !DeepComparable.Matches(Fixture, otherT.Fixture)) return false;
            if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
            if( !DeepComparable.Matches(Variable, otherT.Variable)) return false;
            if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
            if( !DeepComparable.Matches(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.Matches(Setup, otherT.Setup)) return false;
            if( !DeepComparable.Matches(Test, otherT.Test)) return false;
            if( !DeepComparable.Matches(Teardown, otherT.Teardown)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as TestScript;
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
            if( !DeepComparable.IsExactly(Origin, otherT.Origin)) return false;
            if( !DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
            if( !DeepComparable.IsExactly(Metadata, otherT.Metadata)) return false;
            if( !DeepComparable.IsExactly(Fixture, otherT.Fixture)) return false;
            if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
            if( !DeepComparable.IsExactly(Variable, otherT.Variable)) return false;
            if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
            if( !DeepComparable.IsExactly(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.IsExactly(Setup, otherT.Setup)) return false;
            if( !DeepComparable.IsExactly(Test, otherT.Test)) return false;
            if( !DeepComparable.IsExactly(Teardown, otherT.Teardown)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("TestScript");
            base.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); UrlElement?.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
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
            sink.BeginList("origin", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Origin)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("destination", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Destination)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("metadata", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Metadata?.Serialize(sink);
            sink.BeginList("fixture", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Fixture)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Profile)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("variable", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Variable)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("rule", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Rule)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("ruleset", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Ruleset)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("setup", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Setup?.Serialize(sink);
            sink.BeginList("test", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Test)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("teardown", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Teardown?.Serialize(sink);
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
                    Contact = source.GetList<Hl7.Fhir.Model.STU3.ContactDetail>();
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
                case "origin":
                    Origin = source.GetList<OriginComponent>();
                    return true;
                case "destination":
                    Destination = source.GetList<DestinationComponent>();
                    return true;
                case "metadata":
                    Metadata = source.Get<MetadataComponent>();
                    return true;
                case "fixture":
                    Fixture = source.GetList<FixtureComponent>();
                    return true;
                case "profile":
                    Profile = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "variable":
                    Variable = source.GetList<VariableComponent>();
                    return true;
                case "rule":
                    Rule = source.GetList<RuleComponent>();
                    return true;
                case "ruleset":
                    Ruleset = source.GetList<RulesetComponent>();
                    return true;
                case "setup":
                    Setup = source.Get<SetupComponent>();
                    return true;
                case "test":
                    Test = source.GetList<TestComponent>();
                    return true;
                case "teardown":
                    Teardown = source.Get<TeardownComponent>();
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
                case "origin":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "destination":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "metadata":
                    Metadata = source.Populate(Metadata);
                    return true;
                case "fixture":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "profile":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "variable":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "rule":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "ruleset":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "setup":
                    Setup = source.Populate(Setup);
                    return true;
                case "test":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "teardown":
                    Teardown = source.Populate(Teardown);
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
                case "origin":
                    source.PopulateListItem(Origin, index);
                    return true;
                case "destination":
                    source.PopulateListItem(Destination, index);
                    return true;
                case "fixture":
                    source.PopulateListItem(Fixture, index);
                    return true;
                case "profile":
                    source.PopulateListItem(Profile, index);
                    return true;
                case "variable":
                    source.PopulateListItem(Variable, index);
                    return true;
                case "rule":
                    source.PopulateListItem(Rule, index);
                    return true;
                case "ruleset":
                    source.PopulateListItem(Ruleset, index);
                    return true;
                case "test":
                    source.PopulateListItem(Test, index);
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
                foreach (var elem in Origin) { if (elem != null) yield return elem; }
                foreach (var elem in Destination) { if (elem != null) yield return elem; }
                if (Metadata != null) yield return Metadata;
                foreach (var elem in Fixture) { if (elem != null) yield return elem; }
                foreach (var elem in Profile) { if (elem != null) yield return elem; }
                foreach (var elem in Variable) { if (elem != null) yield return elem; }
                foreach (var elem in Rule) { if (elem != null) yield return elem; }
                foreach (var elem in Ruleset) { if (elem != null) yield return elem; }
                if (Setup != null) yield return Setup;
                foreach (var elem in Test) { if (elem != null) yield return elem; }
                if (Teardown != null) yield return Teardown;
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
                foreach (var elem in Origin) { if (elem != null) yield return new ElementValue("origin", elem); }
                foreach (var elem in Destination) { if (elem != null) yield return new ElementValue("destination", elem); }
                if (Metadata != null) yield return new ElementValue("metadata", Metadata);
                foreach (var elem in Fixture) { if (elem != null) yield return new ElementValue("fixture", elem); }
                foreach (var elem in Profile) { if (elem != null) yield return new ElementValue("profile", elem); }
                foreach (var elem in Variable) { if (elem != null) yield return new ElementValue("variable", elem); }
                foreach (var elem in Rule) { if (elem != null) yield return new ElementValue("rule", elem); }
                foreach (var elem in Ruleset) { if (elem != null) yield return new ElementValue("ruleset", elem); }
                if (Setup != null) yield return new ElementValue("setup", Setup);
                foreach (var elem in Test) { if (elem != null) yield return new ElementValue("test", elem); }
                if (Teardown != null) yield return new ElementValue("teardown", Teardown);
            }
        }
    
    }

}
