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
    /// Contains a collection of resources
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Bundle", IsResource=true)]
    [DataContract]
    public partial class Bundle : Hl7.Fhir.Model.Resource, Hl7.Fhir.Model.IBundle, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Bundle; } }
        [NotMapped]
        public override string TypeName { get { return "Bundle"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "LinkComponent")]
        [DataContract]
        public partial class LinkComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IBundleLinkComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "LinkComponent"; } }
            
            /// <summary>
            /// See http://www.iana.org/assignments/link-relations/link-relations.xhtml#link-relations-1
            /// </summary>
            [FhirElement("relation", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RelationElement
            {
                get { return _RelationElement; }
                set { _RelationElement = value; OnPropertyChanged("RelationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _RelationElement;
            
            /// <summary>
            /// See http://www.iana.org/assignments/link-relations/link-relations.xhtml#link-relations-1
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Relation
            {
                get { return RelationElement != null ? RelationElement.Value : null; }
                set
                {
                    if (value == null)
                        RelationElement = null;
                    else
                        RelationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Relation");
                }
            }
            
            /// <summary>
            /// Reference details for the link
            /// </summary>
            [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
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
            /// Reference details for the link
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("LinkComponent");
                base.Serialize(sink);
                sink.Element("relation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); RelationElement?.Serialize(sink);
                sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); UrlElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "relation":
                        RelationElement = source.PopulateValue(RelationElement);
                        return true;
                    case "_relation":
                        RelationElement = source.Populate(RelationElement);
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
                var dest = other as LinkComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RelationElement != null) dest.RelationElement = (Hl7.Fhir.Model.FhirString)RelationElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
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
                if( !DeepComparable.Matches(RelationElement, otherT.RelationElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LinkComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RelationElement, otherT.RelationElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RelationElement != null) yield return RelationElement;
                    if (UrlElement != null) yield return UrlElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RelationElement != null) yield return new ElementValue("relation", RelationElement);
                    if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "EntryComponent")]
        [DataContract]
        public partial class EntryComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IBundleEntryComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "EntryComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IBundleLinkComponent> Hl7.Fhir.Model.IBundleEntryComponent.Link { get { return Link; } }
            
            [NotMapped]
            Hl7.Fhir.Model.IBundleSearchComponent Hl7.Fhir.Model.IBundleEntryComponent.Search { get { return Search; } }
            
            [NotMapped]
            Hl7.Fhir.Model.IBundleRequestComponent Hl7.Fhir.Model.IBundleEntryComponent.Request { get { return Request; } }
            
            [NotMapped]
            Hl7.Fhir.Model.IBundleResponseComponent Hl7.Fhir.Model.IBundleEntryComponent.Response { get { return Response; } }
            
            /// <summary>
            /// Links related to this entry
            /// </summary>
            [FhirElement("link", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<LinkComponent> Link
            {
                get { if(_Link==null) _Link = new List<LinkComponent>(); return _Link; }
                set { _Link = value; OnPropertyChanged("Link"); }
            }
            
            private List<LinkComponent> _Link;
            
            /// <summary>
            /// URI for resource (Absolute URL server address or URI for UUID/OID)
            /// </summary>
            [FhirElement("fullUrl", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri FullUrlElement
            {
                get { return _FullUrlElement; }
                set { _FullUrlElement = value; OnPropertyChanged("FullUrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _FullUrlElement;
            
            /// <summary>
            /// URI for resource (Absolute URL server address or URI for UUID/OID)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string FullUrl
            {
                get { return FullUrlElement != null ? FullUrlElement.Value : null; }
                set
                {
                    if (value == null)
                        FullUrlElement = null;
                    else
                        FullUrlElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("FullUrl");
                }
            }
            
            /// <summary>
            /// A resource in the bundle
            /// </summary>
            [FhirElement("resource", InSummary=Hl7.Fhir.Model.Version.All, Order=60, Choice=ChoiceType.ResourceChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Resource))]
            [DataMember]
            public Hl7.Fhir.Model.Resource Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.Resource _Resource;
            
            /// <summary>
            /// Search related information
            /// </summary>
            [FhirElement("search", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public SearchComponent Search
            {
                get { return _Search; }
                set { _Search = value; OnPropertyChanged("Search"); }
            }
            
            private SearchComponent _Search;
            
            /// <summary>
            /// Additional execution information (transaction/batch/history)
            /// </summary>
            [FhirElement("request", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public RequestComponent Request
            {
                get { return _Request; }
                set { _Request = value; OnPropertyChanged("Request"); }
            }
            
            private RequestComponent _Request;
            
            /// <summary>
            /// Results of execution (transaction/batch/history)
            /// </summary>
            [FhirElement("response", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [DataMember]
            public ResponseComponent Response
            {
                get { return _Response; }
                set { _Response = value; OnPropertyChanged("Response"); }
            }
            
            private ResponseComponent _Response;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("EntryComponent");
                base.Serialize(sink);
                sink.BeginList("link", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Link)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("fullUrl", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); FullUrlElement?.Serialize(sink);
                sink.Element("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Resource?.Serialize(sink);
                sink.Element("search", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Search?.Serialize(sink);
                sink.Element("request", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Request?.Serialize(sink);
                sink.Element("response", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Response?.Serialize(sink);
                sink.End();
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
                    case "fullUrl":
                        FullUrlElement = source.PopulateValue(FullUrlElement);
                        return true;
                    case "_fullUrl":
                        FullUrlElement = source.Populate(FullUrlElement);
                        return true;
                    case "resource":
                        Resource = source.GetResource();
                        return true;
                    case "search":
                        Search = source.Populate(Search);
                        return true;
                    case "request":
                        Request = source.Populate(Request);
                        return true;
                    case "response":
                        Response = source.Populate(Response);
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
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EntryComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Link != null) dest.Link = new List<LinkComponent>(Link.DeepCopy());
                    if(FullUrlElement != null) dest.FullUrlElement = (Hl7.Fhir.Model.FhirUri)FullUrlElement.DeepCopy();
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.Resource)Resource.DeepCopy();
                    if(Search != null) dest.Search = (SearchComponent)Search.DeepCopy();
                    if(Request != null) dest.Request = (RequestComponent)Request.DeepCopy();
                    if(Response != null) dest.Response = (ResponseComponent)Response.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new EntryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EntryComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Link, otherT.Link)) return false;
                if( !DeepComparable.Matches(FullUrlElement, otherT.FullUrlElement)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Search, otherT.Search)) return false;
                if( !DeepComparable.Matches(Request, otherT.Request)) return false;
                if( !DeepComparable.Matches(Response, otherT.Response)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EntryComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Link, otherT.Link)) return false;
                if( !DeepComparable.IsExactly(FullUrlElement, otherT.FullUrlElement)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Search, otherT.Search)) return false;
                if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
                if( !DeepComparable.IsExactly(Response, otherT.Response)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Link) { if (elem != null) yield return elem; }
                    if (FullUrlElement != null) yield return FullUrlElement;
                    if (Resource != null) yield return Resource;
                    if (Search != null) yield return Search;
                    if (Request != null) yield return Request;
                    if (Response != null) yield return Response;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Link) { if (elem != null) yield return new ElementValue("link", elem); }
                    if (FullUrlElement != null) yield return new ElementValue("fullUrl", FullUrlElement);
                    if (Resource != null) yield return new ElementValue("resource", Resource);
                    if (Search != null) yield return new ElementValue("search", Search);
                    if (Request != null) yield return new ElementValue("request", Request);
                    if (Response != null) yield return new ElementValue("response", Response);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SearchComponent")]
        [DataContract]
        public partial class SearchComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IBundleSearchComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SearchComponent"; } }
            
            /// <summary>
            /// match | include | outcome - why this is in the result set
            /// </summary>
            [FhirElement("mode", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Code<Hl7.Fhir.Model.SearchEntryMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.SearchEntryMode> _ModeElement;
            
            /// <summary>
            /// match | include | outcome - why this is in the result set
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.SearchEntryMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (value == null)
                        ModeElement = null;
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.SearchEntryMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Search ranking (between 0 and 1)
            /// </summary>
            [FhirElement("score", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ScoreElement
            {
                get { return _ScoreElement; }
                set { _ScoreElement = value; OnPropertyChanged("ScoreElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ScoreElement;
            
            /// <summary>
            /// Search ranking (between 0 and 1)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Score
            {
                get { return ScoreElement != null ? ScoreElement.Value : null; }
                set
                {
                    if (value == null)
                        ScoreElement = null;
                    else
                        ScoreElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Score");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SearchComponent");
                base.Serialize(sink);
                sink.Element("mode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ModeElement?.Serialize(sink);
                sink.Element("score", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ScoreElement?.Serialize(sink);
                sink.End();
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
                    case "score":
                        ScoreElement = source.PopulateValue(ScoreElement);
                        return true;
                    case "_score":
                        ScoreElement = source.Populate(ScoreElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SearchComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.SearchEntryMode>)ModeElement.DeepCopy();
                    if(ScoreElement != null) dest.ScoreElement = (Hl7.Fhir.Model.FhirDecimal)ScoreElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SearchComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SearchComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(ScoreElement, otherT.ScoreElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SearchComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(ScoreElement, otherT.ScoreElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ModeElement != null) yield return ModeElement;
                    if (ScoreElement != null) yield return ScoreElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                    if (ScoreElement != null) yield return new ElementValue("score", ScoreElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "RequestComponent")]
        [DataContract]
        public partial class RequestComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IBundleRequestComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RequestComponent"; } }
            
            /// <summary>
            /// GET | HEAD | POST | PUT | DELETE | PATCH
            /// </summary>
            [FhirElement("method", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.HTTPVerb> MethodElement
            {
                get { return _MethodElement; }
                set { _MethodElement = value; OnPropertyChanged("MethodElement"); }
            }
            
            private Code<Hl7.Fhir.Model.HTTPVerb> _MethodElement;
            
            /// <summary>
            /// GET | HEAD | POST | PUT | DELETE | PATCH
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.HTTPVerb? Method
            {
                get { return MethodElement != null ? MethodElement.Value : null; }
                set
                {
                    if (value == null)
                        MethodElement = null;
                    else
                        MethodElement = new Code<Hl7.Fhir.Model.HTTPVerb>(value);
                    OnPropertyChanged("Method");
                }
            }
            
            /// <summary>
            /// URL for HTTP equivalent of this entry
            /// </summary>
            [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
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
            /// URL for HTTP equivalent of this entry
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
            /// For managing cache currency
            /// </summary>
            [FhirElement("ifNoneMatch", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IfNoneMatchElement
            {
                get { return _IfNoneMatchElement; }
                set { _IfNoneMatchElement = value; OnPropertyChanged("IfNoneMatchElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IfNoneMatchElement;
            
            /// <summary>
            /// For managing cache currency
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string IfNoneMatch
            {
                get { return IfNoneMatchElement != null ? IfNoneMatchElement.Value : null; }
                set
                {
                    if (value == null)
                        IfNoneMatchElement = null;
                    else
                        IfNoneMatchElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("IfNoneMatch");
                }
            }
            
            /// <summary>
            /// For managing cache currency
            /// </summary>
            [FhirElement("ifModifiedSince", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Instant IfModifiedSinceElement
            {
                get { return _IfModifiedSinceElement; }
                set { _IfModifiedSinceElement = value; OnPropertyChanged("IfModifiedSinceElement"); }
            }
            
            private Hl7.Fhir.Model.Instant _IfModifiedSinceElement;
            
            /// <summary>
            /// For managing cache currency
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public DateTimeOffset? IfModifiedSince
            {
                get { return IfModifiedSinceElement != null ? IfModifiedSinceElement.Value : null; }
                set
                {
                    if (value == null)
                        IfModifiedSinceElement = null;
                    else
                        IfModifiedSinceElement = new Hl7.Fhir.Model.Instant(value);
                    OnPropertyChanged("IfModifiedSince");
                }
            }
            
            /// <summary>
            /// For managing update contention
            /// </summary>
            [FhirElement("ifMatch", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IfMatchElement
            {
                get { return _IfMatchElement; }
                set { _IfMatchElement = value; OnPropertyChanged("IfMatchElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IfMatchElement;
            
            /// <summary>
            /// For managing update contention
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string IfMatch
            {
                get { return IfMatchElement != null ? IfMatchElement.Value : null; }
                set
                {
                    if (value == null)
                        IfMatchElement = null;
                    else
                        IfMatchElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("IfMatch");
                }
            }
            
            /// <summary>
            /// For conditional creates
            /// </summary>
            [FhirElement("ifNoneExist", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IfNoneExistElement
            {
                get { return _IfNoneExistElement; }
                set { _IfNoneExistElement = value; OnPropertyChanged("IfNoneExistElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IfNoneExistElement;
            
            /// <summary>
            /// For conditional creates
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string IfNoneExist
            {
                get { return IfNoneExistElement != null ? IfNoneExistElement.Value : null; }
                set
                {
                    if (value == null)
                        IfNoneExistElement = null;
                    else
                        IfNoneExistElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("IfNoneExist");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RequestComponent");
                base.Serialize(sink);
                sink.Element("method", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); MethodElement?.Serialize(sink);
                sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); UrlElement?.Serialize(sink);
                sink.Element("ifNoneMatch", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IfNoneMatchElement?.Serialize(sink);
                sink.Element("ifModifiedSince", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IfModifiedSinceElement?.Serialize(sink);
                sink.Element("ifMatch", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IfMatchElement?.Serialize(sink);
                sink.Element("ifNoneExist", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IfNoneExistElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "method":
                        MethodElement = source.PopulateValue(MethodElement);
                        return true;
                    case "_method":
                        MethodElement = source.Populate(MethodElement);
                        return true;
                    case "url":
                        UrlElement = source.PopulateValue(UrlElement);
                        return true;
                    case "_url":
                        UrlElement = source.Populate(UrlElement);
                        return true;
                    case "ifNoneMatch":
                        IfNoneMatchElement = source.PopulateValue(IfNoneMatchElement);
                        return true;
                    case "_ifNoneMatch":
                        IfNoneMatchElement = source.Populate(IfNoneMatchElement);
                        return true;
                    case "ifModifiedSince":
                        IfModifiedSinceElement = source.PopulateValue(IfModifiedSinceElement);
                        return true;
                    case "_ifModifiedSince":
                        IfModifiedSinceElement = source.Populate(IfModifiedSinceElement);
                        return true;
                    case "ifMatch":
                        IfMatchElement = source.PopulateValue(IfMatchElement);
                        return true;
                    case "_ifMatch":
                        IfMatchElement = source.Populate(IfMatchElement);
                        return true;
                    case "ifNoneExist":
                        IfNoneExistElement = source.PopulateValue(IfNoneExistElement);
                        return true;
                    case "_ifNoneExist":
                        IfNoneExistElement = source.Populate(IfNoneExistElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RequestComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(MethodElement != null) dest.MethodElement = (Code<Hl7.Fhir.Model.HTTPVerb>)MethodElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    if(IfNoneMatchElement != null) dest.IfNoneMatchElement = (Hl7.Fhir.Model.FhirString)IfNoneMatchElement.DeepCopy();
                    if(IfModifiedSinceElement != null) dest.IfModifiedSinceElement = (Hl7.Fhir.Model.Instant)IfModifiedSinceElement.DeepCopy();
                    if(IfMatchElement != null) dest.IfMatchElement = (Hl7.Fhir.Model.FhirString)IfMatchElement.DeepCopy();
                    if(IfNoneExistElement != null) dest.IfNoneExistElement = (Hl7.Fhir.Model.FhirString)IfNoneExistElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RequestComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RequestComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(MethodElement, otherT.MethodElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(IfNoneMatchElement, otherT.IfNoneMatchElement)) return false;
                if( !DeepComparable.Matches(IfModifiedSinceElement, otherT.IfModifiedSinceElement)) return false;
                if( !DeepComparable.Matches(IfMatchElement, otherT.IfMatchElement)) return false;
                if( !DeepComparable.Matches(IfNoneExistElement, otherT.IfNoneExistElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RequestComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(MethodElement, otherT.MethodElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(IfNoneMatchElement, otherT.IfNoneMatchElement)) return false;
                if( !DeepComparable.IsExactly(IfModifiedSinceElement, otherT.IfModifiedSinceElement)) return false;
                if( !DeepComparable.IsExactly(IfMatchElement, otherT.IfMatchElement)) return false;
                if( !DeepComparable.IsExactly(IfNoneExistElement, otherT.IfNoneExistElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (MethodElement != null) yield return MethodElement;
                    if (UrlElement != null) yield return UrlElement;
                    if (IfNoneMatchElement != null) yield return IfNoneMatchElement;
                    if (IfModifiedSinceElement != null) yield return IfModifiedSinceElement;
                    if (IfMatchElement != null) yield return IfMatchElement;
                    if (IfNoneExistElement != null) yield return IfNoneExistElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (MethodElement != null) yield return new ElementValue("method", MethodElement);
                    if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                    if (IfNoneMatchElement != null) yield return new ElementValue("ifNoneMatch", IfNoneMatchElement);
                    if (IfModifiedSinceElement != null) yield return new ElementValue("ifModifiedSince", IfModifiedSinceElement);
                    if (IfMatchElement != null) yield return new ElementValue("ifMatch", IfMatchElement);
                    if (IfNoneExistElement != null) yield return new ElementValue("ifNoneExist", IfNoneExistElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ResponseComponent")]
        [DataContract]
        public partial class ResponseComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IBundleResponseComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ResponseComponent"; } }
            
            /// <summary>
            /// Status response code (text optional)
            /// </summary>
            [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _StatusElement;
            
            /// <summary>
            /// Status response code (text optional)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if (value == null)
                        StatusElement = null;
                    else
                        StatusElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// The location (if the operation returns a location)
            /// </summary>
            [FhirElement("location", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri LocationElement
            {
                get { return _LocationElement; }
                set { _LocationElement = value; OnPropertyChanged("LocationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _LocationElement;
            
            /// <summary>
            /// The location (if the operation returns a location)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Location
            {
                get { return LocationElement != null ? LocationElement.Value : null; }
                set
                {
                    if (value == null)
                        LocationElement = null;
                    else
                        LocationElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Location");
                }
            }
            
            /// <summary>
            /// The Etag for the resource (if relevant)
            /// </summary>
            [FhirElement("etag", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString EtagElement
            {
                get { return _EtagElement; }
                set { _EtagElement = value; OnPropertyChanged("EtagElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _EtagElement;
            
            /// <summary>
            /// The Etag for the resource (if relevant)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Etag
            {
                get { return EtagElement != null ? EtagElement.Value : null; }
                set
                {
                    if (value == null)
                        EtagElement = null;
                    else
                        EtagElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Etag");
                }
            }
            
            /// <summary>
            /// Server's date time modified
            /// </summary>
            [FhirElement("lastModified", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Instant LastModifiedElement
            {
                get { return _LastModifiedElement; }
                set { _LastModifiedElement = value; OnPropertyChanged("LastModifiedElement"); }
            }
            
            private Hl7.Fhir.Model.Instant _LastModifiedElement;
            
            /// <summary>
            /// Server's date time modified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public DateTimeOffset? LastModified
            {
                get { return LastModifiedElement != null ? LastModifiedElement.Value : null; }
                set
                {
                    if (value == null)
                        LastModifiedElement = null;
                    else
                        LastModifiedElement = new Hl7.Fhir.Model.Instant(value);
                    OnPropertyChanged("LastModified");
                }
            }
            
            /// <summary>
            /// OperationOutcome with hints and warnings (for batch/transaction)
            /// </summary>
            [FhirElement("outcome", InSummary=Hl7.Fhir.Model.Version.All, Order=80, Choice=ChoiceType.ResourceChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Resource))]
            [DataMember]
            public Hl7.Fhir.Model.Resource Outcome
            {
                get { return _Outcome; }
                set { _Outcome = value; OnPropertyChanged("Outcome"); }
            }
            
            private Hl7.Fhir.Model.Resource _Outcome;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ResponseComponent");
                base.Serialize(sink);
                sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
                sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LocationElement?.Serialize(sink);
                sink.Element("etag", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EtagElement?.Serialize(sink);
                sink.Element("lastModified", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LastModifiedElement?.Serialize(sink);
                sink.Element("outcome", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Outcome?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "status":
                        StatusElement = source.PopulateValue(StatusElement);
                        return true;
                    case "_status":
                        StatusElement = source.Populate(StatusElement);
                        return true;
                    case "location":
                        LocationElement = source.PopulateValue(LocationElement);
                        return true;
                    case "_location":
                        LocationElement = source.Populate(LocationElement);
                        return true;
                    case "etag":
                        EtagElement = source.PopulateValue(EtagElement);
                        return true;
                    case "_etag":
                        EtagElement = source.Populate(EtagElement);
                        return true;
                    case "lastModified":
                        LastModifiedElement = source.PopulateValue(LastModifiedElement);
                        return true;
                    case "_lastModified":
                        LastModifiedElement = source.Populate(LastModifiedElement);
                        return true;
                    case "outcome":
                        Outcome = source.GetResource();
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ResponseComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StatusElement != null) dest.StatusElement = (Hl7.Fhir.Model.FhirString)StatusElement.DeepCopy();
                    if(LocationElement != null) dest.LocationElement = (Hl7.Fhir.Model.FhirUri)LocationElement.DeepCopy();
                    if(EtagElement != null) dest.EtagElement = (Hl7.Fhir.Model.FhirString)EtagElement.DeepCopy();
                    if(LastModifiedElement != null) dest.LastModifiedElement = (Hl7.Fhir.Model.Instant)LastModifiedElement.DeepCopy();
                    if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.Resource)Outcome.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ResponseComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ResponseComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(LocationElement, otherT.LocationElement)) return false;
                if( !DeepComparable.Matches(EtagElement, otherT.EtagElement)) return false;
                if( !DeepComparable.Matches(LastModifiedElement, otherT.LastModifiedElement)) return false;
                if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ResponseComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(LocationElement, otherT.LocationElement)) return false;
                if( !DeepComparable.IsExactly(EtagElement, otherT.EtagElement)) return false;
                if( !DeepComparable.IsExactly(LastModifiedElement, otherT.LastModifiedElement)) return false;
                if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (StatusElement != null) yield return StatusElement;
                    if (LocationElement != null) yield return LocationElement;
                    if (EtagElement != null) yield return EtagElement;
                    if (LastModifiedElement != null) yield return LastModifiedElement;
                    if (Outcome != null) yield return Outcome;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                    if (LocationElement != null) yield return new ElementValue("location", LocationElement);
                    if (EtagElement != null) yield return new ElementValue("etag", EtagElement);
                    if (LastModifiedElement != null) yield return new ElementValue("lastModified", LastModifiedElement);
                    if (Outcome != null) yield return new ElementValue("outcome", Outcome);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IBundleLinkComponent> Hl7.Fhir.Model.IBundle.Link { get { return Link; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IBundleEntryComponent> Hl7.Fhir.Model.IBundle.Entry { get { return Entry; } }
        
        [NotMapped]
        Hl7.Fhir.Model.ISignature Hl7.Fhir.Model.IBundle.Signature { get { return Signature; } }
    
        
        /// <summary>
        /// Persistent identifier for the bundle
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// document | message | transaction | transaction-response | batch | batch-response | history | searchset | collection
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.BundleType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.BundleType> _TypeElement;
        
        /// <summary>
        /// document | message | transaction | transaction-response | batch | batch-response | history | searchset | collection
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.BundleType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Code<Hl7.Fhir.Model.BundleType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// When the bundle was assembled
        /// </summary>
        [FhirElement("timestamp", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Instant TimestampElement
        {
            get { return _TimestampElement; }
            set { _TimestampElement = value; OnPropertyChanged("TimestampElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _TimestampElement;
        
        /// <summary>
        /// When the bundle was assembled
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Timestamp
        {
            get { return TimestampElement != null ? TimestampElement.Value : null; }
            set
            {
                if (value == null)
                    TimestampElement = null;
                else
                    TimestampElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Timestamp");
            }
        }
        
        /// <summary>
        /// If search, the total number of matches
        /// </summary>
        [FhirElement("total", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt TotalElement
        {
            get { return _TotalElement; }
            set { _TotalElement = value; OnPropertyChanged("TotalElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _TotalElement;
        
        /// <summary>
        /// If search, the total number of matches
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Total
        {
            get { return TotalElement != null ? TotalElement.Value : null; }
            set
            {
                if (value == null)
                    TotalElement = null;
                else
                    TotalElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("Total");
            }
        }
        
        /// <summary>
        /// Links related to this Bundle
        /// </summary>
        [FhirElement("link", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<LinkComponent> Link
        {
            get { if(_Link==null) _Link = new List<LinkComponent>(); return _Link; }
            set { _Link = value; OnPropertyChanged("Link"); }
        }
        
        private List<LinkComponent> _Link;
        
        /// <summary>
        /// Entry in the bundle - will have a resource or information
        /// </summary>
        [FhirElement("entry", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<EntryComponent> Entry
        {
            get { if(_Entry==null) _Entry = new List<EntryComponent>(); return _Entry; }
            set { _Entry = value; OnPropertyChanged("Entry"); }
        }
        
        private List<EntryComponent> _Entry;
        
        /// <summary>
        /// Digital Signature
        /// </summary>
        [FhirElement("signature", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.R4.Signature Signature
        {
            get { return _Signature; }
            set { _Signature = value; OnPropertyChanged("Signature"); }
        }
        
        private Hl7.Fhir.Model.R4.Signature _Signature;
    
    
        public static ElementDefinitionConstraint[] Bundle_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "bdl-7",
                severity: ConstraintSeverity.Warning,
                expression: "(type = 'history') or entry.where(fullUrl.exists()).select(fullUrl&resource.meta.versionId).isDistinct()",
                human: "FullUrl must be unique in a bundle, or else entries with the same fullUrl must have different meta.versionId (except in history bundles)",
                xpath: "(f:type/@value = 'history') or (count(for $entry in f:entry[f:resource] return $entry[count(parent::f:Bundle/f:entry[f:fullUrl/@value=$entry/f:fullUrl/@value and ((not(f:resource/*/f:meta/f:versionId/@value) and not($entry/f:resource/*/f:meta/f:versionId/@value)) or f:resource/*/f:meta/f:versionId/@value=$entry/f:resource/*/f:meta/f:versionId/@value)])!=1])=0)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "bdl-9",
                severity: ConstraintSeverity.Warning,
                expression: "type = 'document' implies (identifier.system.exists() and identifier.value.exists())",
                human: "A document must have an identifier with a system and a value",
                xpath: "not(f:type/@value = 'document') or exists(f:identifier/f:system) or exists(f:identifier/f:value)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "bdl-3",
                severity: ConstraintSeverity.Warning,
                expression: "entry.all(request.exists() = (%resource.type = 'batch' or %resource.type = 'transaction' or %resource.type = 'history'))",
                human: "entry.request mandatory for batch/transaction/history, otherwise prohibited",
                xpath: "not(f:entry/f:request) or (f:type/@value = 'batch') or (f:type/@value = 'transaction') or (f:type/@value = 'history')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "bdl-4",
                severity: ConstraintSeverity.Warning,
                expression: "entry.all(response.exists() = (%resource.type = 'batch-response' or %resource.type = 'transaction-response' or %resource.type = 'history'))",
                human: "entry.response mandatory for batch-response/transaction-response/history, otherwise prohibited",
                xpath: "not(f:entry/f:response) or (f:type/@value = 'batch-response') or (f:type/@value = 'transaction-response') or (f:type/@value = 'history')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "bdl-12",
                severity: ConstraintSeverity.Warning,
                expression: "type = 'message' implies entry.first().resource.is(MessageHeader)",
                human: "A message must have a MessageHeader as the first resource",
                xpath: "not(f:type/@value='message') or f:entry[1]/f:resource/f:MessageHeader"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "bdl-1",
                severity: ConstraintSeverity.Warning,
                expression: "total.empty() or (type = 'searchset') or (type = 'history')",
                human: "total only when a search or history",
                xpath: "not(f:total) or (f:type/@value = 'searchset') or (f:type/@value = 'history')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "bdl-2",
                severity: ConstraintSeverity.Warning,
                expression: "entry.search.empty() or (type = 'searchset')",
                human: "entry.search only when a search",
                xpath: "not(f:entry/f:search) or (f:type/@value = 'searchset')"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "bdl-11",
                severity: ConstraintSeverity.Warning,
                expression: "type = 'document' implies entry.first().resource.is(Composition)",
                human: "A document must have a Composition as the first resource",
                xpath: "not(f:type/@value='document') or f:entry[1]/f:resource/f:Composition"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "bdl-10",
                severity: ConstraintSeverity.Warning,
                expression: "type = 'document' implies (timestamp.hasValue())",
                human: "A document must have a date",
                xpath: "not(f:type/@value = 'document') or exists(f:timestamp/@value)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "bdl-8",
                severity: ConstraintSeverity.Warning,
                expression: "entry.all(fullUrl.contains('/_history/').not())",
                human: "fullUrl cannot be a version specific reference",
                xpath: "not(exists(f:fullUrl[contains(string(@value), '/_history/')]))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "bdl-5",
                severity: ConstraintSeverity.Warning,
                expression: "entry.all(resource.exists() or request.exists() or response.exists())",
                human: "must be a resource unless there's a request or response",
                xpath: "exists(f:resource) or exists(f:request) or exists(f:response)"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Bundle_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Bundle;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.BundleType>)TypeElement.DeepCopy();
                if(TimestampElement != null) dest.TimestampElement = (Hl7.Fhir.Model.Instant)TimestampElement.DeepCopy();
                if(TotalElement != null) dest.TotalElement = (Hl7.Fhir.Model.UnsignedInt)TotalElement.DeepCopy();
                if(Link != null) dest.Link = new List<LinkComponent>(Link.DeepCopy());
                if(Entry != null) dest.Entry = new List<EntryComponent>(Entry.DeepCopy());
                if(Signature != null) dest.Signature = (Hl7.Fhir.Model.R4.Signature)Signature.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Bundle());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Bundle;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(TimestampElement, otherT.TimestampElement)) return false;
            if( !DeepComparable.Matches(TotalElement, otherT.TotalElement)) return false;
            if( !DeepComparable.Matches(Link, otherT.Link)) return false;
            if( !DeepComparable.Matches(Entry, otherT.Entry)) return false;
            if( !DeepComparable.Matches(Signature, otherT.Signature)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Bundle;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(TimestampElement, otherT.TimestampElement)) return false;
            if( !DeepComparable.IsExactly(TotalElement, otherT.TotalElement)) return false;
            if( !DeepComparable.IsExactly(Link, otherT.Link)) return false;
            if( !DeepComparable.IsExactly(Entry, otherT.Entry)) return false;
            if( !DeepComparable.IsExactly(Signature, otherT.Signature)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Bundle");
            base.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
            sink.Element("timestamp", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TimestampElement?.Serialize(sink);
            sink.Element("total", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TotalElement?.Serialize(sink);
            sink.BeginList("link", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Link)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("entry", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Entry)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("signature", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Signature?.Serialize(sink);
            sink.End();
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    Identifier = source.Populate(Identifier);
                    return true;
                case "type":
                    TypeElement = source.PopulateValue(TypeElement);
                    return true;
                case "_type":
                    TypeElement = source.Populate(TypeElement);
                    return true;
                case "timestamp":
                    TimestampElement = source.PopulateValue(TimestampElement);
                    return true;
                case "_timestamp":
                    TimestampElement = source.Populate(TimestampElement);
                    return true;
                case "total":
                    TotalElement = source.PopulateValue(TotalElement);
                    return true;
                case "_total":
                    TotalElement = source.Populate(TotalElement);
                    return true;
                case "link":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "entry":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "signature":
                    Signature = source.Populate(Signature);
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
                case "entry":
                    source.PopulateListItem(Entry, index);
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
                if (Identifier != null) yield return Identifier;
                if (TypeElement != null) yield return TypeElement;
                if (TimestampElement != null) yield return TimestampElement;
                if (TotalElement != null) yield return TotalElement;
                foreach (var elem in Link) { if (elem != null) yield return elem; }
                foreach (var elem in Entry) { if (elem != null) yield return elem; }
                if (Signature != null) yield return Signature;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (TimestampElement != null) yield return new ElementValue("timestamp", TimestampElement);
                if (TotalElement != null) yield return new ElementValue("total", TotalElement);
                foreach (var elem in Link) { if (elem != null) yield return new ElementValue("link", elem); }
                foreach (var elem in Entry) { if (elem != null) yield return new ElementValue("entry", elem); }
                if (Signature != null) yield return new ElementValue("signature", Signature);
            }
        }
    
    }

}
