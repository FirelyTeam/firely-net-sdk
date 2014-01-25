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
    /// An instance of a manufactured thing that is used in the provision of healthcare
    /// </summary>
    [FhirType("Device", IsResource=true)]
    [DataContract]
    public partial class Device : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// Instance id from manufacturer, owner and others
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// What kind of device this is
        /// </summary>
        [FhirElement("type", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
        
        /// <summary>
        /// Name of device manufacturer
        /// </summary>
        [FhirElement("manufacturer", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ManufacturerElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Manufacturer
        {
            get { return ManufacturerElement != null ? ManufacturerElement.Value : null; }
            set
            {
                if(value == null)
                  ManufacturerElement = null; 
                else
                  ManufacturerElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Model id assigned by the manufacturer
        /// </summary>
        [FhirElement("model", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ModelElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Model
        {
            get { return ModelElement != null ? ModelElement.Value : null; }
            set
            {
                if(value == null)
                  ModelElement = null; 
                else
                  ModelElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Version number (i.e. software)
        /// </summary>
        [FhirElement("version", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if(value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Date of expiry of this device (if applicable)
        /// </summary>
        [FhirElement("expiry", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Date ExpiryElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Expiry
        {
            get { return ExpiryElement != null ? ExpiryElement.Value : null; }
            set
            {
                if(value == null)
                  ExpiryElement = null; 
                else
                  ExpiryElement = new Hl7.Fhir.Model.Date(value);
            }
        }
        
        /// <summary>
        /// FDA Mandated Unique Device Identifier
        /// </summary>
        [FhirElement("udi", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString UdiElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Udi
        {
            get { return UdiElement != null ? UdiElement.Value : null; }
            set
            {
                if(value == null)
                  UdiElement = null; 
                else
                  UdiElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Lot number of manufacture
        /// </summary>
        [FhirElement("lotNumber", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString LotNumberElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LotNumber
        {
            get { return LotNumberElement != null ? LotNumberElement.Value : null; }
            set
            {
                if(value == null)
                  LotNumberElement = null; 
                else
                  LotNumberElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Organization responsible for device
        /// </summary>
        [FhirElement("owner", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Owner { get; set; }
        
        /// <summary>
        /// Where the resource is found
        /// </summary>
        [FhirElement("location", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location { get; set; }
        
        /// <summary>
        /// If the resource is affixed to a person
        /// </summary>
        [FhirElement("patient", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient { get; set; }
        
        /// <summary>
        /// Details for human/organization for support
        /// </summary>
        [FhirElement("contact", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Contact { get; set; }
        
        /// <summary>
        /// Network address to contact device
        /// </summary>
        [FhirElement("url", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public System.Uri Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if(value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
            }
        }
        
    }
    
}
