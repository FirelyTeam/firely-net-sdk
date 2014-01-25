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
// Generated on Fri, Jan 24, 2014 09:44-0600 for FHIR v0.12
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A postal address
    /// </summary>
    [FhirType("Address")]
    [DataContract]
    public partial class Address : Hl7.Fhir.Model.Element
    {
        /// <summary>
        /// The use of an address
        /// </summary>
        [FhirEnumeration("AddressUse")]
        public enum AddressUse
        {
            [EnumLiteral("home")]
            Home, // A communication address at a home.
            [EnumLiteral("work")]
            Work, // An office address. First choice for business related contacts during business hours.
            [EnumLiteral("temp")]
            Temp, // A temporary address. The period can provide more detailed information.
            [EnumLiteral("old")]
            Old, // This address is no longer in use (or was never correct, but retained for records).
        }
        
        /// <summary>
        /// home | work | temp | old - purpose of this address
        /// </summary>
        [FhirElement("use", Order=40)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Address.AddressUse> UseElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Text representation of the address
        /// </summary>
        [FhirElement("text", Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TextElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Street name, number, direction & P.O. Box etc
        /// </summary>
        [FhirElement("line", Order=60)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> LineElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Name of city, town etc.
        /// </summary>
        [FhirElement("city", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CityElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Sub-unit of country (abreviations ok)
        /// </summary>
        [FhirElement("state", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString StateElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Postal code for area
        /// </summary>
        [FhirElement("zip", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ZipElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Country (can be ISO 3166 3 letter code)
        /// </summary>
        [FhirElement("country", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CountryElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Time period when address was/is in use
        /// </summary>
        [FhirElement("period", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period { get; set; }
        
    }
    
}
