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
    /// Describes a set of tests
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "TestScript", IsResource=true)]
    [DataContract]
    public partial class TestScript : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ITestScript, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.TestScript; } }
        [NotMapped]
        public override string TypeName { get { return "TestScript"; } }
    
    
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "MetadataComponent")]
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "LinkComponent")]
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "CapabilityComponent")]
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
            /// Which server these requirements apply to
            /// </summary>
            [FhirElement("destination", Order=70)]
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
            [FhirElement("link", Order=80)]
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
            /// Required Conformance
            /// </summary>
            [FhirElement("conformance", Order=90)]
            [CLSCompliant(false)]
            [References("Conformance")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Conformance
            {
                get { return _Conformance; }
                set { _Conformance = value; OnPropertyChanged("Conformance"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Conformance;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CapabilityComponent");
                base.Serialize(sink);
                sink.Element("required", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequiredElement?.Serialize(sink);
                sink.Element("validated", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValidatedElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("destination", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DestinationElement?.Serialize(sink);
                sink.BeginList("link", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(LinkElement);
                sink.End();
                sink.Element("conformance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Conformance?.Serialize(sink);
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
                    case "destination":
                        DestinationElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "link":
                        LinkElement = source.GetList<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "conformance":
                        Conformance = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "conformance":
                        Conformance = source.Populate(Conformance);
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
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    if(LinkElement != null) dest.LinkElement = new List<Hl7.Fhir.Model.FhirUri>(LinkElement.DeepCopy());
                    if(Conformance != null) dest.Conformance = (Hl7.Fhir.Model.ResourceReference)Conformance.DeepCopy();
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
                if( !DeepComparable.Matches(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.Matches(LinkElement, otherT.LinkElement)) return false;
                if( !DeepComparable.Matches(Conformance, otherT.Conformance)) return false;
            
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
                if( !DeepComparable.IsExactly(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.IsExactly(LinkElement, otherT.LinkElement)) return false;
                if( !DeepComparable.IsExactly(Conformance, otherT.Conformance)) return false;
            
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
                    if (DestinationElement != null) yield return DestinationElement;
                    foreach (var elem in LinkElement) { if (elem != null) yield return elem; }
                    if (Conformance != null) yield return Conformance;
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
                    if (DestinationElement != null) yield return new ElementValue("destination", DestinationElement);
                    foreach (var elem in LinkElement) { if (elem != null) yield return new ElementValue("link", elem); }
                    if (Conformance != null) yield return new ElementValue("conformance", Conformance);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "FixtureComponent")]
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "VariableComponent")]
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
            /// HTTP header field name for source
            /// </summary>
            [FhirElement("headerField", Order=50)]
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
            /// XPath or JSONPath against the fixture body
            /// </summary>
            [FhirElement("path", Order=60)]
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
            [FhirElement("sourceId", Order=70)]
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
                sink.Element("headerField", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); HeaderFieldElement?.Serialize(sink);
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
                    case "headerField":
                        HeaderFieldElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "headerField":
                        HeaderFieldElement = source.PopulateValue(HeaderFieldElement);
                        return true;
                    case "_headerField":
                        HeaderFieldElement = source.Populate(HeaderFieldElement);
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
                    if(HeaderFieldElement != null) dest.HeaderFieldElement = (Hl7.Fhir.Model.FhirString)HeaderFieldElement.DeepCopy();
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
                if( !DeepComparable.Matches(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
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
                if( !DeepComparable.IsExactly(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
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
                    if (HeaderFieldElement != null) yield return HeaderFieldElement;
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
                    if (HeaderFieldElement != null) yield return new ElementValue("headerField", HeaderFieldElement);
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (SourceIdElement != null) yield return new ElementValue("sourceId", SourceIdElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "SetupComponent")]
        [DataContract]
        public partial class SetupComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptSetupComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SetupComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ITestScriptSetupActionComponent> Hl7.Fhir.Model.ITestScriptSetupComponent.Action { get { return Action; } }
            
            /// <summary>
            /// Capabilities  that are assumed to function correctly on the FHIR server being tested
            /// </summary>
            [FhirElement("metadata", Order=40)]
            [DataMember]
            public MetadataComponent Metadata
            {
                get { return _Metadata; }
                set { _Metadata = value; OnPropertyChanged("Metadata"); }
            }
            
            private MetadataComponent _Metadata;
            
            /// <summary>
            /// A setup operation or assert to perform
            /// </summary>
            [FhirElement("action", Order=50)]
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
                sink.Element("metadata", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Metadata?.Serialize(sink);
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
                    case "metadata":
                        Metadata = source.Get<MetadataComponent>();
                        return true;
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
                    case "metadata":
                        Metadata = source.Populate(Metadata);
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
                var dest = other as SetupComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Metadata != null) dest.Metadata = (MetadataComponent)Metadata.DeepCopy();
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
                if( !DeepComparable.Matches(Metadata, otherT.Metadata)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SetupComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Metadata, otherT.Metadata)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Metadata != null) yield return Metadata;
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Metadata != null) yield return new ElementValue("metadata", Metadata);
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "SetupActionComponent")]
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "OperationComponent")]
        [DataContract]
        public partial class OperationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITestScriptOperationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OperationComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ITestScriptRequestHeaderComponent> Hl7.Fhir.Model.ITestScriptOperationComponent.RequestHeader { get { return RequestHeader; } }
            
            /// <summary>
            /// The setup operation type that will be executed
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
            public Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType> ResourceElement
            {
                get { return _ResourceElement; }
                set { _ResourceElement = value; OnPropertyChanged("ResourceElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType> _ResourceElement;
            
            /// <summary>
            /// Resource type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.FHIRDefinedType? Resource
            {
                get { return ResourceElement != null ? ResourceElement.Value : null; }
                set
                {
                    if (value == null)
                        ResourceElement = null;
                    else
                        ResourceElement = new Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType>(value);
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
            /// xml | json
            /// </summary>
            [FhirElement("accept", Order=80)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DSTU2.ContentType> AcceptElement
            {
                get { return _AcceptElement; }
                set { _AcceptElement = value; OnPropertyChanged("AcceptElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.ContentType> _AcceptElement;
            
            /// <summary>
            /// xml | json
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.ContentType? Accept
            {
                get { return AcceptElement != null ? AcceptElement.Value : null; }
                set
                {
                    if (value == null)
                        AcceptElement = null;
                    else
                        AcceptElement = new Code<Hl7.Fhir.Model.DSTU2.ContentType>(value);
                    OnPropertyChanged("Accept");
                }
            }
            
            /// <summary>
            /// xml | json
            /// </summary>
            [FhirElement("contentType", Order=90)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DSTU2.ContentType> ContentTypeElement
            {
                get { return _ContentTypeElement; }
                set { _ContentTypeElement = value; OnPropertyChanged("ContentTypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.ContentType> _ContentTypeElement;
            
            /// <summary>
            /// xml | json
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.ContentType? ContentType
            {
                get { return ContentTypeElement != null ? ContentTypeElement.Value : null; }
                set
                {
                    if (value == null)
                        ContentTypeElement = null;
                    else
                        ContentTypeElement = new Code<Hl7.Fhir.Model.DSTU2.ContentType>(value);
                    OnPropertyChanged("ContentType");
                }
            }
            
            /// <summary>
            /// Which server to perform the operation on
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
            /// Which server to perform the operation on
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
            /// Explicitly defined path parameters
            /// </summary>
            [FhirElement("params", Order=120)]
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
            /// Each operation can have one ore more header elements
            /// </summary>
            [FhirElement("requestHeader", Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<RequestHeaderComponent> RequestHeader
            {
                get { if(_RequestHeader==null) _RequestHeader = new List<RequestHeaderComponent>(); return _RequestHeader; }
                set { _RequestHeader = value; OnPropertyChanged("RequestHeader"); }
            }
            
            private List<RequestHeaderComponent> _RequestHeader;
            
            /// <summary>
            /// Fixture Id of mapped response
            /// </summary>
            [FhirElement("responseId", Order=140)]
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
            [FhirElement("sourceId", Order=150)]
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
            [FhirElement("targetId", Order=160)]
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
            [FhirElement("url", Order=170)]
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
                sink.Element("params", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ParamsElement?.Serialize(sink);
                sink.BeginList("requestHeader", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in RequestHeader)
                {
                    item?.Serialize(sink);
                }
                sink.End();
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
                        ResourceElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType>>();
                        return true;
                    case "label":
                        LabelElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "accept":
                        AcceptElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.ContentType>>();
                        return true;
                    case "contentType":
                        ContentTypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.ContentType>>();
                        return true;
                    case "destination":
                        DestinationElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "encodeRequestUrl":
                        EncodeRequestUrlElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "params":
                        ParamsElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "requestHeader":
                        RequestHeader = source.GetList<RequestHeaderComponent>();
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
                    case "params":
                        ParamsElement = source.PopulateValue(ParamsElement);
                        return true;
                    case "_params":
                        ParamsElement = source.Populate(ParamsElement);
                        return true;
                    case "requestHeader":
                        source.SetList(this, jsonPropertyName);
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
                    if(ResourceElement != null) dest.ResourceElement = (Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType>)ResourceElement.DeepCopy();
                    if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(AcceptElement != null) dest.AcceptElement = (Code<Hl7.Fhir.Model.DSTU2.ContentType>)AcceptElement.DeepCopy();
                    if(ContentTypeElement != null) dest.ContentTypeElement = (Code<Hl7.Fhir.Model.DSTU2.ContentType>)ContentTypeElement.DeepCopy();
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    if(EncodeRequestUrlElement != null) dest.EncodeRequestUrlElement = (Hl7.Fhir.Model.FhirBoolean)EncodeRequestUrlElement.DeepCopy();
                    if(ParamsElement != null) dest.ParamsElement = (Hl7.Fhir.Model.FhirString)ParamsElement.DeepCopy();
                    if(RequestHeader != null) dest.RequestHeader = new List<RequestHeaderComponent>(RequestHeader.DeepCopy());
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
                if( !DeepComparable.Matches(ParamsElement, otherT.ParamsElement)) return false;
                if( !DeepComparable.Matches(RequestHeader, otherT.RequestHeader)) return false;
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
                if( !DeepComparable.IsExactly(ParamsElement, otherT.ParamsElement)) return false;
                if( !DeepComparable.IsExactly(RequestHeader, otherT.RequestHeader)) return false;
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
                    if (ParamsElement != null) yield return ParamsElement;
                    foreach (var elem in RequestHeader) { if (elem != null) yield return elem; }
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
                    if (ParamsElement != null) yield return new ElementValue("params", ParamsElement);
                    foreach (var elem in RequestHeader) { if (elem != null) yield return new ElementValue("requestHeader", elem); }
                    if (ResponseIdElement != null) yield return new ElementValue("responseId", ResponseIdElement);
                    if (SourceIdElement != null) yield return new ElementValue("sourceId", SourceIdElement);
                    if (TargetIdElement != null) yield return new ElementValue("targetId", TargetIdElement);
                    if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "RequestHeaderComponent")]
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "AssertComponent")]
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
            /// Id of fixture used to compare the "sourceId/path" evaluations to
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
            /// Id of fixture used to compare the "sourceId/path" evaluations to
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
            /// XPath or JSONPath expression against fixture used to compare the "sourceId/path" evaluations to
            /// </summary>
            [FhirElement("compareToSourcePath", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CompareToSourcePathElement
            {
                get { return _CompareToSourcePathElement; }
                set { _CompareToSourcePathElement = value; OnPropertyChanged("CompareToSourcePathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CompareToSourcePathElement;
            
            /// <summary>
            /// XPath or JSONPath expression against fixture used to compare the "sourceId/path" evaluations to
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
            /// xml | json
            /// </summary>
            [FhirElement("contentType", Order=90)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DSTU2.ContentType> ContentTypeElement
            {
                get { return _ContentTypeElement; }
                set { _ContentTypeElement = value; OnPropertyChanged("ContentTypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.ContentType> _ContentTypeElement;
            
            /// <summary>
            /// xml | json
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.ContentType? ContentType
            {
                get { return ContentTypeElement != null ? ContentTypeElement.Value : null; }
                set
                {
                    if (value == null)
                        ContentTypeElement = null;
                    else
                        ContentTypeElement = new Code<Hl7.Fhir.Model.DSTU2.ContentType>(value);
                    OnPropertyChanged("ContentType");
                }
            }
            
            /// <summary>
            /// HTTP header field name
            /// </summary>
            [FhirElement("headerField", Order=100)]
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
            [FhirElement("minimumId", Order=110)]
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
            [FhirElement("navigationLinks", Order=120)]
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
            /// equals | notEquals | in | notIn | greaterThan | lessThan | empty | notEmpty | contains | notContains
            /// </summary>
            [FhirElement("operator", Order=130)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DSTU2.AssertionOperatorType> OperatorElement
            {
                get { return _OperatorElement; }
                set { _OperatorElement = value; OnPropertyChanged("OperatorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.AssertionOperatorType> _OperatorElement;
            
            /// <summary>
            /// equals | notEquals | in | notIn | greaterThan | lessThan | empty | notEmpty | contains | notContains
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.AssertionOperatorType? Operator
            {
                get { return OperatorElement != null ? OperatorElement.Value : null; }
                set
                {
                    if (value == null)
                        OperatorElement = null;
                    else
                        OperatorElement = new Code<Hl7.Fhir.Model.DSTU2.AssertionOperatorType>(value);
                    OnPropertyChanged("Operator");
                }
            }
            
            /// <summary>
            /// XPath or JSONPath expression
            /// </summary>
            [FhirElement("path", Order=140)]
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
            /// Resource type
            /// </summary>
            [FhirElement("resource", Order=150)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType> ResourceElement
            {
                get { return _ResourceElement; }
                set { _ResourceElement = value; OnPropertyChanged("ResourceElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType> _ResourceElement;
            
            /// <summary>
            /// Resource type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.FHIRDefinedType? Resource
            {
                get { return ResourceElement != null ? ResourceElement.Value : null; }
                set
                {
                    if (value == null)
                        ResourceElement = null;
                    else
                        ResourceElement = new Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType>(value);
                    OnPropertyChanged("Resource");
                }
            }
            
            /// <summary>
            /// okay | created | noContent | notModified | bad | forbidden | notFound | methodNotAllowed | conflict | gone | preconditionFailed | unprocessable
            /// </summary>
            [FhirElement("response", Order=160)]
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
            [FhirElement("responseCode", Order=170)]
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
            /// Fixture Id of source expression or headerField
            /// </summary>
            [FhirElement("sourceId", Order=180)]
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
            [FhirElement("validateProfileId", Order=190)]
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
            [FhirElement("value", Order=200)]
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
            [FhirElement("warningOnly", Order=210)]
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
                sink.Element("compareToSourcePath", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CompareToSourcePathElement?.Serialize(sink);
                sink.Element("contentType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ContentTypeElement?.Serialize(sink);
                sink.Element("headerField", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); HeaderFieldElement?.Serialize(sink);
                sink.Element("minimumId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); MinimumIdElement?.Serialize(sink);
                sink.Element("navigationLinks", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NavigationLinksElement?.Serialize(sink);
                sink.Element("operator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OperatorElement?.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PathElement?.Serialize(sink);
                sink.Element("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ResourceElement?.Serialize(sink);
                sink.Element("response", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ResponseElement?.Serialize(sink);
                sink.Element("responseCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ResponseCodeElement?.Serialize(sink);
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
                    case "compareToSourcePath":
                        CompareToSourcePathElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "contentType":
                        ContentTypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.ContentType>>();
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
                        OperatorElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.AssertionOperatorType>>();
                        return true;
                    case "path":
                        PathElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "resource":
                        ResourceElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType>>();
                        return true;
                    case "response":
                        ResponseElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AssertionResponseTypes>>();
                        return true;
                    case "responseCode":
                        ResponseCodeElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    if(CompareToSourcePathElement != null) dest.CompareToSourcePathElement = (Hl7.Fhir.Model.FhirString)CompareToSourcePathElement.DeepCopy();
                    if(ContentTypeElement != null) dest.ContentTypeElement = (Code<Hl7.Fhir.Model.DSTU2.ContentType>)ContentTypeElement.DeepCopy();
                    if(HeaderFieldElement != null) dest.HeaderFieldElement = (Hl7.Fhir.Model.FhirString)HeaderFieldElement.DeepCopy();
                    if(MinimumIdElement != null) dest.MinimumIdElement = (Hl7.Fhir.Model.FhirString)MinimumIdElement.DeepCopy();
                    if(NavigationLinksElement != null) dest.NavigationLinksElement = (Hl7.Fhir.Model.FhirBoolean)NavigationLinksElement.DeepCopy();
                    if(OperatorElement != null) dest.OperatorElement = (Code<Hl7.Fhir.Model.DSTU2.AssertionOperatorType>)OperatorElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(ResourceElement != null) dest.ResourceElement = (Code<Hl7.Fhir.Model.DSTU2.FHIRDefinedType>)ResourceElement.DeepCopy();
                    if(ResponseElement != null) dest.ResponseElement = (Code<Hl7.Fhir.Model.AssertionResponseTypes>)ResponseElement.DeepCopy();
                    if(ResponseCodeElement != null) dest.ResponseCodeElement = (Hl7.Fhir.Model.FhirString)ResponseCodeElement.DeepCopy();
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
                if( !DeepComparable.Matches(CompareToSourcePathElement, otherT.CompareToSourcePathElement)) return false;
                if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.Matches(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.Matches(MinimumIdElement, otherT.MinimumIdElement)) return false;
                if( !DeepComparable.Matches(NavigationLinksElement, otherT.NavigationLinksElement)) return false;
                if( !DeepComparable.Matches(OperatorElement, otherT.OperatorElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.Matches(ResponseElement, otherT.ResponseElement)) return false;
                if( !DeepComparable.Matches(ResponseCodeElement, otherT.ResponseCodeElement)) return false;
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
                if( !DeepComparable.IsExactly(CompareToSourcePathElement, otherT.CompareToSourcePathElement)) return false;
                if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.IsExactly(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.IsExactly(MinimumIdElement, otherT.MinimumIdElement)) return false;
                if( !DeepComparable.IsExactly(NavigationLinksElement, otherT.NavigationLinksElement)) return false;
                if( !DeepComparable.IsExactly(OperatorElement, otherT.OperatorElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.IsExactly(ResponseElement, otherT.ResponseElement)) return false;
                if( !DeepComparable.IsExactly(ResponseCodeElement, otherT.ResponseCodeElement)) return false;
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
                    if (CompareToSourcePathElement != null) yield return CompareToSourcePathElement;
                    if (ContentTypeElement != null) yield return ContentTypeElement;
                    if (HeaderFieldElement != null) yield return HeaderFieldElement;
                    if (MinimumIdElement != null) yield return MinimumIdElement;
                    if (NavigationLinksElement != null) yield return NavigationLinksElement;
                    if (OperatorElement != null) yield return OperatorElement;
                    if (PathElement != null) yield return PathElement;
                    if (ResourceElement != null) yield return ResourceElement;
                    if (ResponseElement != null) yield return ResponseElement;
                    if (ResponseCodeElement != null) yield return ResponseCodeElement;
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
                    if (CompareToSourcePathElement != null) yield return new ElementValue("compareToSourcePath", CompareToSourcePathElement);
                    if (ContentTypeElement != null) yield return new ElementValue("contentType", ContentTypeElement);
                    if (HeaderFieldElement != null) yield return new ElementValue("headerField", HeaderFieldElement);
                    if (MinimumIdElement != null) yield return new ElementValue("minimumId", MinimumIdElement);
                    if (NavigationLinksElement != null) yield return new ElementValue("navigationLinks", NavigationLinksElement);
                    if (OperatorElement != null) yield return new ElementValue("operator", OperatorElement);
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (ResourceElement != null) yield return new ElementValue("resource", ResourceElement);
                    if (ResponseElement != null) yield return new ElementValue("response", ResponseElement);
                    if (ResponseCodeElement != null) yield return new ElementValue("responseCode", ResponseCodeElement);
                    if (SourceIdElement != null) yield return new ElementValue("sourceId", SourceIdElement);
                    if (ValidateProfileIdElement != null) yield return new ElementValue("validateProfileId", ValidateProfileIdElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                    if (WarningOnlyElement != null) yield return new ElementValue("warningOnly", WarningOnlyElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "TestComponent")]
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
            /// Capabilities  that are expected to function correctly on the FHIR server being tested
            /// </summary>
            [FhirElement("metadata", Order=60)]
            [DataMember]
            public MetadataComponent Metadata
            {
                get { return _Metadata; }
                set { _Metadata = value; OnPropertyChanged("Metadata"); }
            }
            
            private MetadataComponent _Metadata;
            
            /// <summary>
            /// A test operation or assert to perform
            /// </summary>
            [FhirElement("action", Order=70)]
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
                sink.Element("metadata", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Metadata?.Serialize(sink);
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
                    case "metadata":
                        Metadata = source.Get<MetadataComponent>();
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
                    case "metadata":
                        Metadata = source.Populate(Metadata);
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
                    if(Metadata != null) dest.Metadata = (MetadataComponent)Metadata.DeepCopy();
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
                if( !DeepComparable.Matches(Metadata, otherT.Metadata)) return false;
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
                if( !DeepComparable.IsExactly(Metadata, otherT.Metadata)) return false;
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
                    if (Metadata != null) yield return Metadata;
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
                    if (Metadata != null) yield return new ElementValue("metadata", Metadata);
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "TestActionComponent")]
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "TeardownComponent")]
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
            public List<TearDownActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<TearDownActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<TearDownActionComponent> _Action;
        
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
                        Action = source.GetList<TearDownActionComponent>();
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
                    if(Action != null) dest.Action = new List<TearDownActionComponent>(Action.DeepCopy());
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "TearDownActionComponent")]
        [DataContract]
        public partial class TearDownActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TearDownActionComponent"; } }
            
            /// <summary>
            /// The teardown operation to perform
            /// </summary>
            [FhirElement("operation", Order=40)]
            [DataMember]
            public OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private OperationComponent _Operation;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TearDownActionComponent");
                base.Serialize(sink);
                sink.Element("operation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Operation?.Serialize(sink);
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
                var dest = other as TearDownActionComponent;
            
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
                 return CopyTo(new TearDownActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TearDownActionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TearDownActionComponent;
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
        /// Absolute URL used to reference this TestScript
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
        /// Absolute URL used to reference this TestScript
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
        /// Logical id for this version of the TestScript
        /// </summary>
        [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Logical id for this version of the TestScript
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
        /// Informal name for this TestScript
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
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
        /// Informal name for this TestScript
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
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
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
        /// External identifier
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
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
        /// Name of the publisher (Organization or individual)
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
        /// Date for this version of the TestScript
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
        /// Date for this version of the TestScript
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
        /// Natural language description of the TestScript
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
        /// Natural language description of the TestScript
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
        /// Scope and Usage this Test Script is for
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
        /// Scope and Usage this Test Script is for
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
        /// Required capability that is assumed to function correctly on the FHIR server being tested
        /// </summary>
        [FhirElement("metadata", Order=220)]
        [DataMember]
        public MetadataComponent Metadata
        {
            get { return _Metadata; }
            set { _Metadata = value; OnPropertyChanged("Metadata"); }
        }
        
        private MetadataComponent _Metadata;
        
        /// <summary>
        /// Whether or not the tests apply to more than one FHIR server
        /// </summary>
        [FhirElement("multiserver", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean MultiserverElement
        {
            get { return _MultiserverElement; }
            set { _MultiserverElement = value; OnPropertyChanged("MultiserverElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _MultiserverElement;
        
        /// <summary>
        /// Whether or not the tests apply to more than one FHIR server
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Multiserver
        {
            get { return MultiserverElement != null ? MultiserverElement.Value : null; }
            set
            {
                if (value == null)
                    MultiserverElement = null;
                else
                    MultiserverElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Multiserver");
            }
        }
        
        /// <summary>
        /// Fixture in the test script - by reference (uri)
        /// </summary>
        [FhirElement("fixture", Order=240)]
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
        [FhirElement("profile", Order=250)]
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
        [FhirElement("variable", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<VariableComponent> Variable
        {
            get { if(_Variable==null) _Variable = new List<VariableComponent>(); return _Variable; }
            set { _Variable = value; OnPropertyChanged("Variable"); }
        }
        
        private List<VariableComponent> _Variable;
        
        /// <summary>
        /// A series of required setup operations before tests are executed
        /// </summary>
        [FhirElement("setup", Order=270)]
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
        [FhirElement("test", Order=280)]
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
        [FhirElement("teardown", Order=290)]
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
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-5",
                severity: ConstraintSeverity.Warning,
                expression: "metadata.all(capability.required or capability.validated)",
                human: "TestScript metadata capability SHALL contain required or validated or both.",
                xpath: "f:capability/f:required or f:capability/f:validated or (f:capability/f:required and f:capability/f:validated)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-4",
                severity: ConstraintSeverity.Warning,
                expression: "variable.all(headerField.empty() or path.empty())",
                human: "Variable cannot contain both headerField and path.",
                xpath: "not(f:headerField and f:path)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-6",
                severity: ConstraintSeverity.Warning,
                expression: "setup.metadata.all(capability.required or capability.validated)",
                human: "Setup metadata capability SHALL contain required or validated or both.",
                xpath: "f:capability/f:required or f:capability/f:validated or (f:capability/f:required and f:capability/f:validated)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-1",
                severity: ConstraintSeverity.Warning,
                expression: "setup.action.all(operation xor assert)",
                human: "Setup action SHALL contain either an operation or assert but not both.",
                xpath: "(f:operation or f:assert) and not(f:operation and f:assert)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-10",
                severity: ConstraintSeverity.Warning,
                expression: "setup.action.operation.all(sourceId or (targetId.count() + url.count() + params.count() = 1) or (type.code in ('conformance' |'search' | 'transaction' | 'history')))",
                human: "Setup operation SHALL contain either sourceId or targetId or params or url.",
                xpath: "f:sourceId or ((f:targetId or f:url or f:params) and (count(f:targetId) + count(f:url) + count(f:params) =1)) or (f:type/f:code/@value='conformance' or f:type/f:code/@value='search' or f:type/f:code/@value='transaction' or f:type/f:code/@value='history')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-13",
                severity: ConstraintSeverity.Warning,
                expression: "setup.action.assert.all(compareToSourceId.empty() xor compareToSourcePath)",
                human: "Setup action assert shall contain both compareToSourceId and compareToSourcePath or neither.",
                xpath: "(f:compareToSourceId and f:compareToSourcePath) or not(f:compareToSourceId or f:compareToSourcePath)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-8",
                severity: ConstraintSeverity.Warning,
                expression: "setup.action.assert.all(contentType.count() + headerField.count() + minimumId.count() + navigationLinks.count() + path.count() + resource.count() + responseCode.count() + response.count() + validateProfileId.count() <=1)",
                human: "Only a single assertion SHALL be present within setup action assert element.",
                xpath: "count(f:contentType) + count(f:headerField) + count(f:minimumId) + count(f:navigationLinks) + count(f:path) + count(f:resource) + count(f:responseCode) + count(f:response) + count(f:validateProfileId)  <=1"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-7",
                severity: ConstraintSeverity.Warning,
                expression: "test.metadata.all(capability.required or capability.validated)",
                human: "Test metadata capability SHALL contain required or validated or both.",
                xpath: "f:capability/f:required or f:capability/f:validated or (f:capability/f:required and f:capability/f:validated)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-2",
                severity: ConstraintSeverity.Warning,
                expression: "test.action.all(operation xor assert)",
                human: "Test action SHALL contain either an operation or assert but not both.",
                xpath: "(f:operation or f:assert) and not(f:operation and f:assert)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-11",
                severity: ConstraintSeverity.Warning,
                expression: "test.action.operation.all(sourceId or (targetId.count() + url.count() + params.count() = 1) or (type.code in ('conformance' | 'search' | 'transaction' | 'history')))",
                human: "Test operation SHALL contain either sourceId or targetId or params or url.",
                xpath: "f:sourceId or (f:targetId or f:url or f:params) and (count(f:targetId) + count(f:url) + count(f:params) =1) or (f:type/f:code/@value='conformance' or f:type/f:code/@value='search' or f:type/f:code/@value='transaction' or f:type/f:code/@value='history')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-14",
                severity: ConstraintSeverity.Warning,
                expression: "test.action.assert.all(compareToSourceId.empty() xor compareToSourcePath)",
                human: "Test action assert shall contain both compareToSourceId and compareToSourcePath or neither.",
                xpath: "(f:compareToSourceId and f:compareToSourcePath) or not(f:compareToSourceId or f:compareToSourcePath)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-9",
                severity: ConstraintSeverity.Warning,
                expression: "test.action.assert.all(contentType.count() + headerField.count() + minimumId.count() + navigationLinks.count() + path.count() + resource.count() + responseCode.count() + response.count() + validateProfileId.count() <=1)",
                human: "Only a single assertion SHALL be present within test action assert element.",
                xpath: "count(f:contentType) + count(f:headerField) + count(f:minimumId) + count(f:navigationLinks) + count(f:path) + count(f:resource) + count(f:responseCode) + count(f:response) + count(f:validateProfileId)  <=1"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-3",
                severity: ConstraintSeverity.Warning,
                expression: "teardown.action.all(operation)",
                human: "Teardown action SHALL contain an operation.",
                xpath: "f:operation"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-12",
                severity: ConstraintSeverity.Warning,
                expression: "teardown.action.operation.all(sourceId or (targetId.count() + url.count() + params.count() = 1) or (type.code in ('conformance' | 'search' | 'transaction' | 'history')))",
                human: "Teardown operation SHALL contain either sourceId or targetId or params or url.",
                xpath: "f:sourceId or (f:targetId or f:url or (f:params and f:resource)) and (count(f:targetId) + count(f:url) + count(f:params) =1) or (f:type/f:code/@value='conformance' or f:type/f:code/@value='search' or f:type/f:code/@value='transaction' or f:type/f:code/@value='history')"
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
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus>)StatusElement.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<ContactComponent>(Contact.DeepCopy());
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(UseContext.DeepCopy());
                if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.FhirString)CopyrightElement.DeepCopy();
                if(Metadata != null) dest.Metadata = (MetadataComponent)Metadata.DeepCopy();
                if(MultiserverElement != null) dest.MultiserverElement = (Hl7.Fhir.Model.FhirBoolean)MultiserverElement.DeepCopy();
                if(Fixture != null) dest.Fixture = new List<FixtureComponent>(Fixture.DeepCopy());
                if(Profile != null) dest.Profile = new List<Hl7.Fhir.Model.ResourceReference>(Profile.DeepCopy());
                if(Variable != null) dest.Variable = new List<VariableComponent>(Variable.DeepCopy());
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
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(Metadata, otherT.Metadata)) return false;
            if( !DeepComparable.Matches(MultiserverElement, otherT.MultiserverElement)) return false;
            if( !DeepComparable.Matches(Fixture, otherT.Fixture)) return false;
            if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
            if( !DeepComparable.Matches(Variable, otherT.Variable)) return false;
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
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(Metadata, otherT.Metadata)) return false;
            if( !DeepComparable.IsExactly(MultiserverElement, otherT.MultiserverElement)) return false;
            if( !DeepComparable.IsExactly(Fixture, otherT.Fixture)) return false;
            if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
            if( !DeepComparable.IsExactly(Variable, otherT.Variable)) return false;
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
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); NameElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
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
            sink.Element("metadata", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Metadata?.Serialize(sink);
            sink.Element("multiserver", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); MultiserverElement?.Serialize(sink);
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
                case "version":
                    VersionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "name":
                    NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus>>();
                    return true;
                case "identifier":
                    Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
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
                case "metadata":
                    Metadata = source.Get<MetadataComponent>();
                    return true;
                case "multiserver":
                    MultiserverElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
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
                case "identifier":
                    Identifier = source.Populate(Identifier);
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
                case "metadata":
                    Metadata = source.Populate(Metadata);
                    return true;
                case "multiserver":
                    MultiserverElement = source.PopulateValue(MultiserverElement);
                    return true;
                case "_multiserver":
                    MultiserverElement = source.Populate(MultiserverElement);
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
                case "fixture":
                    source.PopulateListItem(Fixture, index);
                    return true;
                case "profile":
                    source.PopulateListItem(Profile, index);
                    return true;
                case "variable":
                    source.PopulateListItem(Variable, index);
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
                if (VersionElement != null) yield return VersionElement;
                if (NameElement != null) yield return NameElement;
                if (StatusElement != null) yield return StatusElement;
                if (Identifier != null) yield return Identifier;
                if (ExperimentalElement != null) yield return ExperimentalElement;
                if (PublisherElement != null) yield return PublisherElement;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (DateElement != null) yield return DateElement;
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                if (RequirementsElement != null) yield return RequirementsElement;
                if (CopyrightElement != null) yield return CopyrightElement;
                if (Metadata != null) yield return Metadata;
                if (MultiserverElement != null) yield return MultiserverElement;
                foreach (var elem in Fixture) { if (elem != null) yield return elem; }
                foreach (var elem in Profile) { if (elem != null) yield return elem; }
                foreach (var elem in Variable) { if (elem != null) yield return elem; }
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
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                if (RequirementsElement != null) yield return new ElementValue("requirements", RequirementsElement);
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                if (Metadata != null) yield return new ElementValue("metadata", Metadata);
                if (MultiserverElement != null) yield return new ElementValue("multiserver", MultiserverElement);
                foreach (var elem in Fixture) { if (elem != null) yield return new ElementValue("fixture", elem); }
                foreach (var elem in Profile) { if (elem != null) yield return new ElementValue("profile", elem); }
                foreach (var elem in Variable) { if (elem != null) yield return new ElementValue("variable", elem); }
                if (Setup != null) yield return new ElementValue("setup", Setup);
                foreach (var elem in Test) { if (elem != null) yield return new ElementValue("test", elem); }
                if (Teardown != null) yield return new ElementValue("teardown", Teardown);
            }
        }
    
    }

}
