using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A postal address
    /// </summary>
    [FhirType("Address")]
    [DataContract]
    public partial class Address : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// The use of an address
        /// </summary>
        [FhirEnumeration("AddressUse")]
        public enum AddressUse
        {
            /// <summary>
            /// A communication address at a home.
            /// </summary>
            [EnumLiteral("home")]
            Home,
            /// <summary>
            /// An office address. First choice for business related contacts during business hours.
            /// </summary>
            [EnumLiteral("work")]
            Work,
            /// <summary>
            /// A temporary address. The period can provide more detailed information.
            /// </summary>
            [EnumLiteral("temp")]
            Temp,
            /// <summary>
            /// This address is no longer in use (or was never correct, but retained for records).
            /// </summary>
            [EnumLiteral("old")]
            Old,
        }
        
        /// <summary>
        /// home | work | temp | old - purpose of this address
        /// </summary>
        [FhirElement("use", InSummary=true, Order=40)]
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
                if(value == null)
                  UseElement = null; 
                else
                  UseElement = new Code<Hl7.Fhir.Model.Address.AddressUse>(value);
                OnPropertyChanged("Use");
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
                if(value == null)
                  TextElement = null; 
                else
                  TextElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Text");
            }
        }
        
        /// <summary>
        /// Street name, number, direction &amp; P.O. Box etc
        /// </summary>
        [FhirElement("line", InSummary=true, Order=60)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> LineElement
        {
            get { return _LineElement; }
            set { _LineElement = value; OnPropertyChanged("LineElement"); }
        }
        private List<Hl7.Fhir.Model.FhirString> _LineElement;
        
        /// <summary>
        /// Street name, number, direction &amp; P.O. Box etc
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Line
        {
            get { return LineElement != null ? LineElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
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
                if(value == null)
                  CityElement = null; 
                else
                  CityElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("City");
            }
        }
        
        /// <summary>
        /// Sub-unit of country (abreviations ok)
        /// </summary>
        [FhirElement("state", InSummary=true, Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString StateElement
        {
            get { return _StateElement; }
            set { _StateElement = value; OnPropertyChanged("StateElement"); }
        }
        private Hl7.Fhir.Model.FhirString _StateElement;
        
        /// <summary>
        /// Sub-unit of country (abreviations ok)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string State
        {
            get { return StateElement != null ? StateElement.Value : null; }
            set
            {
                if(value == null)
                  StateElement = null; 
                else
                  StateElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("State");
            }
        }
        
        /// <summary>
        /// Postal code for area
        /// </summary>
        [FhirElement("zip", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ZipElement
        {
            get { return _ZipElement; }
            set { _ZipElement = value; OnPropertyChanged("ZipElement"); }
        }
        private Hl7.Fhir.Model.FhirString _ZipElement;
        
        /// <summary>
        /// Postal code for area
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Zip
        {
            get { return ZipElement != null ? ZipElement.Value : null; }
            set
            {
                if(value == null)
                  ZipElement = null; 
                else
                  ZipElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Zip");
            }
        }
        
        /// <summary>
        /// Country (can be ISO 3166 3 letter code)
        /// </summary>
        [FhirElement("country", InSummary=true, Order=100)]
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
                if(value == null)
                  CountryElement = null; 
                else
                  CountryElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Country");
            }
        }
        
        /// <summary>
        /// Time period when address was/is in use
        /// </summary>
        [FhirElement("period", InSummary=true, Order=110)]
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
                if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                if(LineElement != null) dest.LineElement = new List<Hl7.Fhir.Model.FhirString>(LineElement.DeepCopy());
                if(CityElement != null) dest.CityElement = (Hl7.Fhir.Model.FhirString)CityElement.DeepCopy();
                if(StateElement != null) dest.StateElement = (Hl7.Fhir.Model.FhirString)StateElement.DeepCopy();
                if(ZipElement != null) dest.ZipElement = (Hl7.Fhir.Model.FhirString)ZipElement.DeepCopy();
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
            if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
            if( !DeepComparable.Matches(LineElement, otherT.LineElement)) return false;
            if( !DeepComparable.Matches(CityElement, otherT.CityElement)) return false;
            if( !DeepComparable.Matches(StateElement, otherT.StateElement)) return false;
            if( !DeepComparable.Matches(ZipElement, otherT.ZipElement)) return false;
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
            if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
            if( !DeepComparable.IsExactly(LineElement, otherT.LineElement)) return false;
            if( !DeepComparable.IsExactly(CityElement, otherT.CityElement)) return false;
            if( !DeepComparable.IsExactly(StateElement, otherT.StateElement)) return false;
            if( !DeepComparable.IsExactly(ZipElement, otherT.ZipElement)) return false;
            if( !DeepComparable.IsExactly(CountryElement, otherT.CountryElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            
            return true;
        }
        
    }
    
}
