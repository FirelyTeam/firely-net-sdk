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
    /// A digital Signature - XML DigSig, JWT, Graphical image of signature, etc.
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "Signature")]
    [DataContract]
    public partial class Signature : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.ISignature, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Signature"; } }
    
        
        /// <summary>
        /// Indication of the reason the entity signed the object(s)
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Type
        {
            get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.Coding>(); return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Type;
        
        /// <summary>
        /// When the signature was created
        /// </summary>
        [FhirElement("when", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant WhenElement
        {
            get { return _WhenElement; }
            set { _WhenElement = value; OnPropertyChanged("WhenElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _WhenElement;
        
        /// <summary>
        /// When the signature was created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? When
        {
            get { return WhenElement != null ? WhenElement.Value : null; }
            set
            {
                if (value == null)
                    WhenElement = null;
                else
                    WhenElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("When");
            }
        }
        
        /// <summary>
        /// Who signed
        /// </summary>
        [FhirElement("who", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Who
        {
            get { return _Who; }
            set { _Who = value; OnPropertyChanged("Who"); }
        }
        
        private Hl7.Fhir.Model.Element _Who;
        
        /// <summary>
        /// The party represented
        /// </summary>
        [FhirElement("onBehalfOf", InSummary=Hl7.Fhir.Model.Version.All, Order=60, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element OnBehalfOf
        {
            get { return _OnBehalfOf; }
            set { _OnBehalfOf = value; OnPropertyChanged("OnBehalfOf"); }
        }
        
        private Hl7.Fhir.Model.Element _OnBehalfOf;
        
        /// <summary>
        /// The technical format of the signature
        /// </summary>
        [FhirElement("contentType", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Code ContentTypeElement
        {
            get { return _ContentTypeElement; }
            set { _ContentTypeElement = value; OnPropertyChanged("ContentTypeElement"); }
        }
        
        private Hl7.Fhir.Model.Code _ContentTypeElement;
        
        /// <summary>
        /// The technical format of the signature
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ContentType
        {
            get { return ContentTypeElement != null ? ContentTypeElement.Value : null; }
            set
            {
                if (value == null)
                    ContentTypeElement = null;
                else
                    ContentTypeElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("ContentType");
            }
        }
        
        /// <summary>
        /// The actual signature content (XML DigSig. JWT, picture, etc.)
        /// </summary>
        [FhirElement("blob", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.Base64Binary BlobElement
        {
            get { return _BlobElement; }
            set { _BlobElement = value; OnPropertyChanged("BlobElement"); }
        }
        
        private Hl7.Fhir.Model.Base64Binary _BlobElement;
        
        /// <summary>
        /// The actual signature content (XML DigSig. JWT, picture, etc.)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public byte[] Blob
        {
            get { return BlobElement != null ? BlobElement.Value : null; }
            set
            {
                if (value == null)
                    BlobElement = null;
                else
                    BlobElement = new Hl7.Fhir.Model.Base64Binary(value);
                OnPropertyChanged("Blob");
            }
        }
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Signature;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Type != null) dest.Type = new List<Hl7.Fhir.Model.Coding>(Type.DeepCopy());
                if(WhenElement != null) dest.WhenElement = (Hl7.Fhir.Model.Instant)WhenElement.DeepCopy();
                if(Who != null) dest.Who = (Hl7.Fhir.Model.Element)Who.DeepCopy();
                if(OnBehalfOf != null) dest.OnBehalfOf = (Hl7.Fhir.Model.Element)OnBehalfOf.DeepCopy();
                if(ContentTypeElement != null) dest.ContentTypeElement = (Hl7.Fhir.Model.Code)ContentTypeElement.DeepCopy();
                if(BlobElement != null) dest.BlobElement = (Hl7.Fhir.Model.Base64Binary)BlobElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Signature());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Signature;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(WhenElement, otherT.WhenElement)) return false;
            if( !DeepComparable.Matches(Who, otherT.Who)) return false;
            if( !DeepComparable.Matches(OnBehalfOf, otherT.OnBehalfOf)) return false;
            if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
            if( !DeepComparable.Matches(BlobElement, otherT.BlobElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Signature;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(WhenElement, otherT.WhenElement)) return false;
            if( !DeepComparable.IsExactly(Who, otherT.Who)) return false;
            if( !DeepComparable.IsExactly(OnBehalfOf, otherT.OnBehalfOf)) return false;
            if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
            if( !DeepComparable.IsExactly(BlobElement, otherT.BlobElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("Signature");
            base.Serialize(sink);
            sink.BeginList("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
            foreach(var item in Type)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("when", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); WhenElement?.Serialize(sink);
            sink.Element("who", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Who?.Serialize(sink);
            sink.Element("onBehalfOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); OnBehalfOf?.Serialize(sink);
            sink.Element("contentType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ContentTypeElement?.Serialize(sink);
            sink.Element("blob", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); BlobElement?.Serialize(sink);
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
                    Type = source.GetList<Hl7.Fhir.Model.Coding>();
                    return true;
                case "when":
                    WhenElement = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "whoUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Who, "who");
                    Who = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "whoReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Who, "who");
                    Who = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "onBehalfOfUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(OnBehalfOf, "onBehalfOf");
                    OnBehalfOf = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "onBehalfOfReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(OnBehalfOf, "onBehalfOf");
                    OnBehalfOf = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "contentType":
                    ContentTypeElement = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "blob":
                    BlobElement = source.Get<Hl7.Fhir.Model.Base64Binary>();
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
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "when":
                    WhenElement = source.PopulateValue(WhenElement);
                    return true;
                case "_when":
                    WhenElement = source.Populate(WhenElement);
                    return true;
                case "whoUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Who, "who");
                    Who = source.PopulateValue(Who as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_whoUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Who, "who");
                    Who = source.Populate(Who as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "whoReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Who, "who");
                    Who = source.Populate(Who as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "onBehalfOfUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(OnBehalfOf, "onBehalfOf");
                    OnBehalfOf = source.PopulateValue(OnBehalfOf as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_onBehalfOfUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(OnBehalfOf, "onBehalfOf");
                    OnBehalfOf = source.Populate(OnBehalfOf as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "onBehalfOfReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(OnBehalfOf, "onBehalfOf");
                    OnBehalfOf = source.Populate(OnBehalfOf as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "contentType":
                    ContentTypeElement = source.PopulateValue(ContentTypeElement);
                    return true;
                case "_contentType":
                    ContentTypeElement = source.Populate(ContentTypeElement);
                    return true;
                case "blob":
                    BlobElement = source.PopulateValue(BlobElement);
                    return true;
                case "_blob":
                    BlobElement = source.Populate(BlobElement);
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
                case "type":
                    source.PopulateListItem(Type, index);
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
                foreach (var elem in Type) { if (elem != null) yield return elem; }
                if (WhenElement != null) yield return WhenElement;
                if (Who != null) yield return Who;
                if (OnBehalfOf != null) yield return OnBehalfOf;
                if (ContentTypeElement != null) yield return ContentTypeElement;
                if (BlobElement != null) yield return BlobElement;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                if (WhenElement != null) yield return new ElementValue("when", WhenElement);
                if (Who != null) yield return new ElementValue("who", Who);
                if (OnBehalfOf != null) yield return new ElementValue("onBehalfOf", OnBehalfOf);
                if (ContentTypeElement != null) yield return new ElementValue("contentType", ContentTypeElement);
                if (BlobElement != null) yield return new ElementValue("blob", BlobElement);
            }
        }
    
    }

}
