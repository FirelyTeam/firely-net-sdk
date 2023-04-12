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
    /// A Signature - XML DigSig, JWS, Graphical image of signature, etc.
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Signature")]
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
        [FhirElement("who", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole","RelatedPerson","Patient","Device","Organization")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Who
        {
            get { return _Who; }
            set { _Who = value; OnPropertyChanged("Who"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Who;
        
        /// <summary>
        /// The party represented
        /// </summary>
        [FhirElement("onBehalfOf", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole","RelatedPerson","Patient","Device","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference OnBehalfOf
        {
            get { return _OnBehalfOf; }
            set { _OnBehalfOf = value; OnPropertyChanged("OnBehalfOf"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _OnBehalfOf;
        
        /// <summary>
        /// The technical format of the signed resources
        /// </summary>
        [FhirElement("targetFormat", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.Code TargetFormatElement
        {
            get { return _TargetFormatElement; }
            set { _TargetFormatElement = value; OnPropertyChanged("TargetFormatElement"); }
        }
        
        private Hl7.Fhir.Model.Code _TargetFormatElement;
        
        /// <summary>
        /// The technical format of the signed resources
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string TargetFormat
        {
            get { return TargetFormatElement != null ? TargetFormatElement.Value : null; }
            set
            {
                if (value == null)
                    TargetFormatElement = null;
                else
                    TargetFormatElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("TargetFormat");
            }
        }
        
        /// <summary>
        /// The technical format of the signature
        /// </summary>
        [FhirElement("sigFormat", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.Code SigFormatElement
        {
            get { return _SigFormatElement; }
            set { _SigFormatElement = value; OnPropertyChanged("SigFormatElement"); }
        }
        
        private Hl7.Fhir.Model.Code _SigFormatElement;
        
        /// <summary>
        /// The technical format of the signature
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string SigFormat
        {
            get { return SigFormatElement != null ? SigFormatElement.Value : null; }
            set
            {
                if (value == null)
                    SigFormatElement = null;
                else
                    SigFormatElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("SigFormat");
            }
        }
        
        /// <summary>
        /// The actual signature content (XML DigSig. JWS, picture, etc.)
        /// </summary>
        [FhirElement("data", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Base64Binary DataElement
        {
            get { return _DataElement; }
            set { _DataElement = value; OnPropertyChanged("DataElement"); }
        }
        
        private Hl7.Fhir.Model.Base64Binary _DataElement;
        
        /// <summary>
        /// The actual signature content (XML DigSig. JWS, picture, etc.)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public byte[] Data
        {
            get { return DataElement != null ? DataElement.Value : null; }
            set
            {
                if (value == null)
                    DataElement = null;
                else
                    DataElement = new Hl7.Fhir.Model.Base64Binary(value);
                OnPropertyChanged("Data");
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
                if(Who != null) dest.Who = (Hl7.Fhir.Model.ResourceReference)Who.DeepCopy();
                if(OnBehalfOf != null) dest.OnBehalfOf = (Hl7.Fhir.Model.ResourceReference)OnBehalfOf.DeepCopy();
                if(TargetFormatElement != null) dest.TargetFormatElement = (Hl7.Fhir.Model.Code)TargetFormatElement.DeepCopy();
                if(SigFormatElement != null) dest.SigFormatElement = (Hl7.Fhir.Model.Code)SigFormatElement.DeepCopy();
                if(DataElement != null) dest.DataElement = (Hl7.Fhir.Model.Base64Binary)DataElement.DeepCopy();
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
            if( !DeepComparable.Matches(TargetFormatElement, otherT.TargetFormatElement)) return false;
            if( !DeepComparable.Matches(SigFormatElement, otherT.SigFormatElement)) return false;
            if( !DeepComparable.Matches(DataElement, otherT.DataElement)) return false;
        
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
            if( !DeepComparable.IsExactly(TargetFormatElement, otherT.TargetFormatElement)) return false;
            if( !DeepComparable.IsExactly(SigFormatElement, otherT.SigFormatElement)) return false;
            if( !DeepComparable.IsExactly(DataElement, otherT.DataElement)) return false;
        
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
            sink.Element("who", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Who?.Serialize(sink);
            sink.Element("onBehalfOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OnBehalfOf?.Serialize(sink);
            sink.Element("targetFormat", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TargetFormatElement?.Serialize(sink);
            sink.Element("sigFormat", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SigFormatElement?.Serialize(sink);
            sink.Element("data", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DataElement?.Serialize(sink);
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
                case "who":
                    Who = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "onBehalfOf":
                    OnBehalfOf = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "targetFormat":
                    TargetFormatElement = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "sigFormat":
                    SigFormatElement = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "data":
                    DataElement = source.Get<Hl7.Fhir.Model.Base64Binary>();
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
                case "who":
                    Who = source.Populate(Who);
                    return true;
                case "onBehalfOf":
                    OnBehalfOf = source.Populate(OnBehalfOf);
                    return true;
                case "targetFormat":
                    TargetFormatElement = source.PopulateValue(TargetFormatElement);
                    return true;
                case "_targetFormat":
                    TargetFormatElement = source.Populate(TargetFormatElement);
                    return true;
                case "sigFormat":
                    SigFormatElement = source.PopulateValue(SigFormatElement);
                    return true;
                case "_sigFormat":
                    SigFormatElement = source.Populate(SigFormatElement);
                    return true;
                case "data":
                    DataElement = source.PopulateValue(DataElement);
                    return true;
                case "_data":
                    DataElement = source.Populate(DataElement);
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
                if (TargetFormatElement != null) yield return TargetFormatElement;
                if (SigFormatElement != null) yield return SigFormatElement;
                if (DataElement != null) yield return DataElement;
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
                if (TargetFormatElement != null) yield return new ElementValue("targetFormat", TargetFormatElement);
                if (SigFormatElement != null) yield return new ElementValue("sigFormat", SigFormatElement);
                if (DataElement != null) yield return new ElementValue("data", DataElement);
            }
        }
    
    }

}
