using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;

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

//
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// An address expressed using postal conventions (as opposed to GPS or other location definition formats)
    /// </summary>
    [FhirType("Address")]
    [DataContract]
    public partial class Address : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Address"; } }
        
        /// <summary>
        /// The use of an address
        /// (url: http://hl7.org/fhir/ValueSet/address-use)
        /// </summary>
        [FhirEnumeration("AddressUse")]
        public enum AddressUse
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/address-use)
            /// </summary>
            [EnumLiteral("home", "http://hl7.org/fhir/address-use"), Description("Home")]
            Home,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/address-use)
            /// </summary>
            [EnumLiteral("work", "http://hl7.org/fhir/address-use"), Description("Work")]
            Work,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/address-use)
            /// </summary>
            [EnumLiteral("temp", "http://hl7.org/fhir/address-use"), Description("Temporary")]
            Temp,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/address-use)
            /// </summary>
            [EnumLiteral("old", "http://hl7.org/fhir/address-use"), Description("Old / Incorrect")]
            Old,
        }

        /// <summary>
        /// The type of an address (physical / postal)
        /// (url: http://hl7.org/fhir/ValueSet/address-type)
        /// </summary>
        [FhirEnumeration("AddressType")]
        public enum AddressType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/address-type)
            /// </summary>
            [EnumLiteral("postal", "http://hl7.org/fhir/address-type"), Description("Postal")]
            Postal,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/address-type)
            /// </summary>
            [EnumLiteral("physical", "http://hl7.org/fhir/address-type"), Description("Physical")]
            Physical,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/address-type)
            /// </summary>
            [EnumLiteral("both", "http://hl7.org/fhir/address-type"), Description("Postal & Physical")]
            Both,
        }

        /// <summary>
        /// home | work | temp | old - purpose of this address
        /// </summary>
        [FhirElement("use", InSummary=true, Order=30)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Address.AddressUse> UseElement
        {
            get { return _UseElement; }
            set { _UseElement = value; OnPropertyChanged("UseElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Address.AddressUse> _UseElement;
        
        /// <summary>
        /// home | work | temp | old - purpose of this address
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Address.AddressUse? Use
        {
            get { return UseElement != null ? UseElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  UseElement = null; 
                else
                  UseElement = new Code<Hl7.Fhir.Model.Address.AddressUse>(value);
                OnPropertyChanged("Use");
            }
        }
        
        /// <summary>
        /// postal | physical | both
        /// </summary>
        [FhirElement("type", InSummary=true, Order=40)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Address.AddressType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Address.AddressType> _TypeElement;
        
        /// <summary>
        /// postal | physical | both
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Address.AddressType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.Address.AddressType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Text representation of the address
        /// </summary>
        [FhirElement("text", InSummary=true, Order=50)]
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
        [FhirElement("line", InSummary=true, Order=60)]
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
        [FhirElement("city", InSummary=true, Order=70)]
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
        [FhirElement("district", InSummary=true, Order=80)]
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
        [FhirElement("state", InSummary=true, Order=90)]
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
        [FhirElement("postalCode", InSummary=true, Order=100)]
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
        /// Country (e.g. can be ISO 3166 2 or 3 letter code)
        /// </summary>
        [FhirElement("country", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CountryElement
        {
            get { return _CountryElement; }
            set { _CountryElement = value; OnPropertyChanged("CountryElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CountryElement;
        
        /// <summary>
        /// Country (e.g. can be ISO 3166 2 or 3 letter code)
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
        [FhirElement("period", InSummary=true, Order=120)]
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
                if(UseElement != null) dest.UseElement = (Code<Hl7.Fhir.Model.Address.AddressUse>)UseElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Address.AddressType>)TypeElement.DeepCopy();
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
