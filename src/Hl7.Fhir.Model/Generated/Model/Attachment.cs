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
// Generated on Tue, Apr 15, 2014 17:48+0200 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Content in a format defined elsewhere
    /// </summary>
    [FhirType("Attachment")]
    [DataContract]
    public partial class Attachment : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Mime type of the content, with charset etc.
        /// </summary>
        [FhirElement("contentType", InSummary=true, Order=40)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Code ContentTypeElement
        {
            get { return _ContentTypeElement; }
            set { _ContentTypeElement = value; OnPropertyChanged("ContentTypeElement"); }
        }
        private Hl7.Fhir.Model.Code _ContentTypeElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ContentType
        {
            get { return ContentTypeElement != null ? ContentTypeElement.Value : null; }
            set
            {
                if(value == null)
                  ContentTypeElement = null; 
                else
                  ContentTypeElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("ContentType");
            }
        }
        
        /// <summary>
        /// Human language of the content (BCP-47)
        /// </summary>
        [FhirElement("language", InSummary=true, Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.Code LanguageElement
        {
            get { return _LanguageElement; }
            set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
        }
        private Hl7.Fhir.Model.Code _LanguageElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Language
        {
            get { return LanguageElement != null ? LanguageElement.Value : null; }
            set
            {
                if(value == null)
                  LanguageElement = null; 
                else
                  LanguageElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Language");
            }
        }
        
        /// <summary>
        /// Data inline, base64ed
        /// </summary>
        [FhirElement("data", InSummary=true, Order=60)]
        [DataMember]
        public Hl7.Fhir.Model.Base64Binary DataElement
        {
            get { return _DataElement; }
            set { _DataElement = value; OnPropertyChanged("DataElement"); }
        }
        private Hl7.Fhir.Model.Base64Binary _DataElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public byte[] Data
        {
            get { return DataElement != null ? DataElement.Value : null; }
            set
            {
                if(value == null)
                  DataElement = null; 
                else
                  DataElement = new Hl7.Fhir.Model.Base64Binary(value);
                OnPropertyChanged("Data");
            }
        }
        
        /// <summary>
        /// Uri where the data can be found
        /// </summary>
        [FhirElement("url", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
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
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Number of bytes of content (if url provided)
        /// </summary>
        [FhirElement("size", InSummary=true, Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.Integer SizeElement
        {
            get { return _SizeElement; }
            set { _SizeElement = value; OnPropertyChanged("SizeElement"); }
        }
        private Hl7.Fhir.Model.Integer _SizeElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Size
        {
            get { return SizeElement != null ? SizeElement.Value : null; }
            set
            {
                if(value == null)
                  SizeElement = null; 
                else
                  SizeElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("Size");
            }
        }
        
        /// <summary>
        /// Hash of the data (sha-1, base64ed )
        /// </summary>
        [FhirElement("hash", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Base64Binary HashElement
        {
            get { return _HashElement; }
            set { _HashElement = value; OnPropertyChanged("HashElement"); }
        }
        private Hl7.Fhir.Model.Base64Binary _HashElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public byte[] Hash
        {
            get { return HashElement != null ? HashElement.Value : null; }
            set
            {
                if(value == null)
                  HashElement = null; 
                else
                  HashElement = new Hl7.Fhir.Model.Base64Binary(value);
                OnPropertyChanged("Hash");
            }
        }
        
        /// <summary>
        /// Label to display in place of the data
        /// </summary>
        [FhirElement("title", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if(value == null)
                  TitleElement = null; 
                else
                  TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }
        
    }
    
}
