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
// Generated for FHIR v1.0.2, v4.0.1, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A postal address
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "Address")]
    [DataContract]
    public partial class Address : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Address"; } }
    
        
        /// <summary>
        /// home | work | temp | old - purpose of this address
        /// </summary>
        [FhirElement("use", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Code UseElement
        {
            get { return _UseElement; }
            set { _UseElement = value; OnPropertyChanged("UseElement"); }
        }
        
        private Hl7.Fhir.Model.Code _UseElement;
        
        /// <summary>
        /// home | work | temp | old - purpose of this address
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Use
        {
            get { return UseElement != null ? UseElement.Value : null; }
            set
            {
                if (value == null)
                    UseElement = null;
                else
                    UseElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Use");
            }
        }
        
        /// <summary>
        /// postal | physical | both
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AddressType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.AddressType> _TypeElement;
        
        /// <summary>
        /// postal | physical | both
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AddressType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Code<Hl7.Fhir.Model.AddressType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Text representation of the address
        /// </summary>
        [FhirElement("text", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TextElement
        {
            get { return _TextElement; }
            set { _TextElement = value; OnPropertyChanged("TextElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TextElement;
        
        /// <summary>
        /// Text representation of the address
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Text
        {
            get { return TextElement != null ? TextElement.Value : null; }
            set
            {
                if (value == null)
                    TextElement = null;
                else
                    TextElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Text");
            }
        }
        
        /// <summary>
        /// Street name, number, direction &amp; P.O. Box etc.
        /// </summary>
        [FhirElement("line", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> LineElement
        {
            get { if(_LineElement==null) _LineElement = new List<Hl7.Fhir.Model.FhirString>(); return _LineElement; }
            set { _LineElement = value; OnPropertyChanged("LineElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _LineElement;
        
        /// <summary>
        /// Street name, number, direction &amp; P.O. Box etc.
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Line
        {
            get { return LineElement != null ? LineElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    LineElement = null;
                else
                    LineElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Line");
            }
        }
        
        /// <summary>
        /// Name of city, town etc.
        /// </summary>
        [FhirElement("city", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CityElement
        {
            get { return _CityElement; }
            set { _CityElement = value; OnPropertyChanged("CityElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CityElement;
        
        /// <summary>
        /// Name of city, town etc.
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string City
        {
            get { return CityElement != null ? CityElement.Value : null; }
            set
            {
                if (value == null)
                    CityElement = null;
                else
                    CityElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("City");
            }
        }
        
        /// <summary>
        /// District name (aka county)
        /// </summary>
        [FhirElement("district", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DistrictElement
        {
            get { return _DistrictElement; }
            set { _DistrictElement = value; OnPropertyChanged("DistrictElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DistrictElement;
        
        /// <summary>
        /// District name (aka county)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string District
        {
            get { return DistrictElement != null ? DistrictElement.Value : null; }
            set
            {
                if (value == null)
                    DistrictElement = null;
                else
                    DistrictElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("District");
            }
        }
        
        /// <summary>
        /// Sub-unit of country (abbreviations ok)
        /// </summary>
        [FhirElement("state", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString StateElement
        {
            get { return _StateElement; }
            set { _StateElement = value; OnPropertyChanged("StateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _StateElement;
        
        /// <summary>
        /// Sub-unit of country (abbreviations ok)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string State
        {
            get { return StateElement != null ? StateElement.Value : null; }
            set
            {
                if (value == null)
                    StateElement = null;
                else
                    StateElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("State");
            }
        }
        
        /// <summary>
        /// Postal code for area
        /// </summary>
        [FhirElement("postalCode", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PostalCodeElement
        {
            get { return _PostalCodeElement; }
            set { _PostalCodeElement = value; OnPropertyChanged("PostalCodeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PostalCodeElement;
        
        /// <summary>
        /// Postal code for area
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PostalCode
        {
            get { return PostalCodeElement != null ? PostalCodeElement.Value : null; }
            set
            {
                if (value == null)
                    PostalCodeElement = null;
                else
                    PostalCodeElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("PostalCode");
            }
        }
        
        /// <summary>
        /// Country (can be ISO 3166 3 letter code)
        /// </summary>
        [FhirElement("country", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CountryElement
        {
            get { return _CountryElement; }
            set { _CountryElement = value; OnPropertyChanged("CountryElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CountryElement;
        
        /// <summary>
        /// Country (can be ISO 3166 3 letter code)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Country
        {
            get { return CountryElement != null ? CountryElement.Value : null; }
            set
            {
                if (value == null)
                    CountryElement = null;
                else
                    CountryElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Country");
            }
        }
        
        /// <summary>
        /// Time period when address was/is in use
        /// </summary>
        [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Address;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UseElement != null) dest.UseElement = (Hl7.Fhir.Model.Code)UseElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.AddressType>)TypeElement.DeepCopy();
                if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                if(LineElement != null) dest.LineElement = new List<Hl7.Fhir.Model.FhirString>(LineElement.DeepCopy());
                if(CityElement != null) dest.CityElement = (Hl7.Fhir.Model.FhirString)CityElement.DeepCopy();
                if(DistrictElement != null) dest.DistrictElement = (Hl7.Fhir.Model.FhirString)DistrictElement.DeepCopy();
                if(StateElement != null) dest.StateElement = (Hl7.Fhir.Model.FhirString)StateElement.DeepCopy();
                if(PostalCodeElement != null) dest.PostalCodeElement = (Hl7.Fhir.Model.FhirString)PostalCodeElement.DeepCopy();
                if(CountryElement != null) dest.CountryElement = (Hl7.Fhir.Model.FhirString)CountryElement.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Address());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Address;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UseElement, otherT.UseElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
            if( !DeepComparable.Matches(LineElement, otherT.LineElement)) return false;
            if( !DeepComparable.Matches(CityElement, otherT.CityElement)) return false;
            if( !DeepComparable.Matches(DistrictElement, otherT.DistrictElement)) return false;
            if( !DeepComparable.Matches(StateElement, otherT.StateElement)) return false;
            if( !DeepComparable.Matches(PostalCodeElement, otherT.PostalCodeElement)) return false;
            if( !DeepComparable.Matches(CountryElement, otherT.CountryElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Address;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UseElement, otherT.UseElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
            if( !DeepComparable.IsExactly(LineElement, otherT.LineElement)) return false;
            if( !DeepComparable.IsExactly(CityElement, otherT.CityElement)) return false;
            if( !DeepComparable.IsExactly(DistrictElement, otherT.DistrictElement)) return false;
            if( !DeepComparable.IsExactly(StateElement, otherT.StateElement)) return false;
            if( !DeepComparable.IsExactly(PostalCodeElement, otherT.PostalCodeElement)) return false;
            if( !DeepComparable.IsExactly(CountryElement, otherT.CountryElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("Address");
            base.Serialize(sink);
            sink.Element("use", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UseElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TypeElement?.Serialize(sink);
            sink.Element("text", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TextElement?.Serialize(sink);
            sink.BeginList("line", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(LineElement);
            sink.End();
            sink.Element("city", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CityElement?.Serialize(sink);
            sink.Element("district", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DistrictElement?.Serialize(sink);
            sink.Element("state", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StateElement?.Serialize(sink);
            sink.Element("postalCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PostalCodeElement?.Serialize(sink);
            sink.Element("country", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CountryElement?.Serialize(sink);
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
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
                case "use":
                    UseElement = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "type":
                    TypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AddressType>>();
                    return true;
                case "text":
                    TextElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "line":
                    LineElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "city":
                    CityElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "district":
                    DistrictElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "state":
                    StateElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "postalCode":
                    PostalCodeElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "country":
                    CountryElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "period":
                    Period = source.Get<Hl7.Fhir.Model.Period>();
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
                case "use":
                    UseElement = source.PopulateValue(UseElement);
                    return true;
                case "_use":
                    UseElement = source.Populate(UseElement);
                    return true;
                case "type":
                    TypeElement = source.PopulateValue(TypeElement);
                    return true;
                case "_type":
                    TypeElement = source.Populate(TypeElement);
                    return true;
                case "text":
                    TextElement = source.PopulateValue(TextElement);
                    return true;
                case "_text":
                    TextElement = source.Populate(TextElement);
                    return true;
                case "line":
                case "_line":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "city":
                    CityElement = source.PopulateValue(CityElement);
                    return true;
                case "_city":
                    CityElement = source.Populate(CityElement);
                    return true;
                case "district":
                    DistrictElement = source.PopulateValue(DistrictElement);
                    return true;
                case "_district":
                    DistrictElement = source.Populate(DistrictElement);
                    return true;
                case "state":
                    StateElement = source.PopulateValue(StateElement);
                    return true;
                case "_state":
                    StateElement = source.Populate(StateElement);
                    return true;
                case "postalCode":
                    PostalCodeElement = source.PopulateValue(PostalCodeElement);
                    return true;
                case "_postalCode":
                    PostalCodeElement = source.Populate(PostalCodeElement);
                    return true;
                case "country":
                    CountryElement = source.PopulateValue(CountryElement);
                    return true;
                case "_country":
                    CountryElement = source.Populate(CountryElement);
                    return true;
                case "period":
                    Period = source.Populate(Period);
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
                case "line":
                    source.PopulatePrimitiveListItemValue(LineElement, index);
                    return true;
                case "_line":
                    source.PopulatePrimitiveListItem(LineElement, index);
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
                if (UseElement != null) yield return UseElement;
                if (TypeElement != null) yield return TypeElement;
                if (TextElement != null) yield return TextElement;
                foreach (var elem in LineElement) { if (elem != null) yield return elem; }
                if (CityElement != null) yield return CityElement;
                if (DistrictElement != null) yield return DistrictElement;
                if (StateElement != null) yield return StateElement;
                if (PostalCodeElement != null) yield return PostalCodeElement;
                if (CountryElement != null) yield return CountryElement;
                if (Period != null) yield return Period;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UseElement != null) yield return new ElementValue("use", UseElement);
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (TextElement != null) yield return new ElementValue("text", TextElement);
                foreach (var elem in LineElement) { if (elem != null) yield return new ElementValue("line", elem); }
                if (CityElement != null) yield return new ElementValue("city", CityElement);
                if (DistrictElement != null) yield return new ElementValue("district", DistrictElement);
                if (StateElement != null) yield return new ElementValue("state", StateElement);
                if (PostalCodeElement != null) yield return new ElementValue("postalCode", PostalCodeElement);
                if (CountryElement != null) yield return new ElementValue("country", CountryElement);
                if (Period != null) yield return new ElementValue("period", Period);
            }
        }
    
    }

}
