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
    /// Technology mediated contact details (phone, fax, email, etc)
    /// </summary>
    [FhirType("Contact")]
    [DataContract]
    public partial class Contact : Hl7.Fhir.Model.Element
    {
        /// <summary>
        /// Telecommunications form for contact
        /// </summary>
        [FhirEnumeration("ContactSystem")]
        public enum ContactSystem
        {
            [EnumLiteral("phone")]
            Phone, // The value is a telephone number used for voice calls. Use of full international numbers starting with + is recommended to enable automatic dialing support but not required.
            [EnumLiteral("fax")]
            Fax, // The value is a fax machine. Use of full international numbers starting with + is recommended to enable automatic dialing support but not required.
            [EnumLiteral("email")]
            Email, // The value is an email address.
            [EnumLiteral("url")]
            Url, // The value is a url. This is intended for various personal contacts including blogs, Twitter, Facebook, etc. Do not use for email addresses.
        }
        
        /// <summary>
        /// Location, type or status of telecommunications address indicating use
        /// </summary>
        [FhirEnumeration("ContactUse")]
        public enum ContactUse
        {
            [EnumLiteral("home")]
            Home, // A communication contact at a home; attempted contacts for business purposes might intrude privacy and chances are one will contact family or other household members instead of the person one wishes to call. Typically used with urgent cases, or if no other contacts are available.
            [EnumLiteral("work")]
            Work, // An office contact. First choice for business related contacts during business hours.
            [EnumLiteral("temp")]
            Temp, // A temporary contact. The period can provide more detailed information.
            [EnumLiteral("old")]
            Old, // This contact is no longer in use (or was never correct, but retained for records).
            [EnumLiteral("mobile")]
            Mobile, // A telecommunication device that moves and stays with its owner. May have characteristics of all other use codes, suitable for urgent matters, not the first choice for routine business.
        }
        
        /// <summary>
        /// phone | fax | email | url
        /// </summary>
        [FhirElement("system", Order=40)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Contact.ContactSystem> SystemElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Contact.ContactSystem? System
        {
            get { return SystemElement != null ? SystemElement.Value : null; }
            set
            {
                if(value == null)
                  SystemElement = null; 
                else
                  SystemElement = new Code<Hl7.Fhir.Model.Contact.ContactSystem>(value);
            }
        }
        
        /// <summary>
        /// The actual contact details
        /// </summary>
        [FhirElement("value", Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ValueElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Value
        {
            get { return ValueElement != null ? ValueElement.Value : null; }
            set
            {
                if(value == null)
                  ValueElement = null; 
                else
                  ValueElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// home | work | temp | old | mobile - purpose of this address
        /// </summary>
        [FhirElement("use", Order=60)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Contact.ContactUse> UseElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Contact.ContactUse? Use
        {
            get { return UseElement != null ? UseElement.Value : null; }
            set
            {
                if(value == null)
                  UseElement = null; 
                else
                  UseElement = new Code<Hl7.Fhir.Model.Contact.ContactUse>(value);
            }
        }
        
        /// <summary>
        /// Time period when the contact was/is in use
        /// </summary>
        [FhirElement("period", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period { get; set; }
        
    }
    
}
