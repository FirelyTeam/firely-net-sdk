// Originally generated from hl7.fhir.r3.core version: 3.0.2

using Hl7.Fhir.Introspection;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Content in a format defined elsewhere
    /// </summary>
    [Serializable]
    [DataContract]
    [FhirType("Attachment", "http://hl7.org/fhir/StructureDefinition/Attachment")]
    public partial class TestAttachment : Hl7.Fhir.Model.DataType
    {
        /// <summary>
        /// FHIR Type Name
        /// </summary>
        public override string TypeName { get { return "Attachment"; } }

        /// <summary>
        /// Mime type of the content, with charset etc.
        /// </summary>
        [FhirElement("contentType", InSummary = true, Order = 30)]
        [DataMember]
        public Hl7.Fhir.Model.Code ContentTypeElement
        {
            get { return _ContentTypeElement; }
            set { _ContentTypeElement = value; OnPropertyChanged("ContentTypeElement"); }
        }

        private Hl7.Fhir.Model.Code _ContentTypeElement;

        /// <summary>
        /// Mime type of the content, with charset etc.
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
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
        /// Human language of the content (BCP-47)
        /// </summary>
        [FhirElement("language", InSummary = true, Order = 40)]
        [DataMember]
        public Hl7.Fhir.Model.Code LanguageElement
        {
            get { return _LanguageElement; }
            set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
        }

        private Hl7.Fhir.Model.Code _LanguageElement;

        /// <summary>
        /// Human language of the content (BCP-47)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
        public string Language
        {
            get { return LanguageElement != null ? LanguageElement.Value : null; }
            set
            {
                if (value == null)
                    LanguageElement = null;
                else
                    LanguageElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Language");
            }
        }

        /// <summary>
        /// Data inline, base64ed
        /// </summary>
        [FhirElement("data", Order = 50)]
        [DataMember]
        public Hl7.Fhir.Model.Base64Binary DataElement
        {
            get { return _DataElement; }
            set { _DataElement = value; OnPropertyChanged("DataElement"); }
        }

        private Hl7.Fhir.Model.Base64Binary _DataElement;

        /// <summary>
        /// Data inline, base64ed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
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

        /// <summary>
        /// Uri where the data can be found
        /// </summary>
        [FhirElement("url", InSummary = true, Order = 60)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }

        private Hl7.Fhir.Model.FhirUri _UrlElement;

        /// <summary>
        /// Uri where the data can be found
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
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
        /// Number of bytes of content (if url provided)
        /// </summary>
        [FhirElement("size", InSummary = true, Order = 70)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt SizeElement
        {
            get { return _SizeElement; }
            set { _SizeElement = value; OnPropertyChanged("SizeElement"); }
        }

        private Hl7.Fhir.Model.UnsignedInt _SizeElement;

        /// <summary>
        /// Number of bytes of content (if url provided)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
        public int? Size
        {
            get { return SizeElement != null ? SizeElement.Value : null; }
            set
            {
                if (value == null)
                    SizeElement = null;
                else
                    SizeElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("Size");
            }
        }

        /// <summary>
        /// Hash of the data (sha-1, base64ed)
        /// </summary>
        [FhirElement("hash", InSummary = true, Order = 80)]
        [DataMember]
        public Hl7.Fhir.Model.Base64Binary HashElement
        {
            get { return _HashElement; }
            set { _HashElement = value; OnPropertyChanged("HashElement"); }
        }

        private Hl7.Fhir.Model.Base64Binary _HashElement;

        /// <summary>
        /// Hash of the data (sha-1, base64ed)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
        public byte[] Hash
        {
            get { return HashElement != null ? HashElement.Value : null; }
            set
            {
                if (value == null)
                    HashElement = null;
                else
                    HashElement = new Hl7.Fhir.Model.Base64Binary(value);
                OnPropertyChanged("Hash");
            }
        }

        /// <summary>
        /// Label to display in place of the data
        /// </summary>
        [FhirElement("title", InSummary = true, Order = 90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }

        private Hl7.Fhir.Model.FhirString _TitleElement;

        /// <summary>
        /// Label to display in place of the data
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if (value == null)
                    TitleElement = null;
                else
                    TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Date attachment was first created
        /// </summary>
        [FhirElement("creation", InSummary = true, Order = 100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreationElement
        {
            get { return _CreationElement; }
            set { _CreationElement = value; OnPropertyChanged("CreationElement"); }
        }

        private Hl7.Fhir.Model.FhirDateTime _CreationElement;

        /// <summary>
        /// Date attachment was first created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
        public string Creation
        {
            get { return CreationElement != null ? CreationElement.Value : null; }
            set
            {
                if (value == null)
                    CreationElement = null;
                else
                    CreationElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Creation");
            }
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TestAttachment;

            if (dest == null)
            {
                throw new ArgumentException("Can only copy to an object of the same type", "other");
            }

            base.CopyTo(dest);
            if (ContentTypeElement != null) dest.ContentTypeElement = (Hl7.Fhir.Model.Code)ContentTypeElement.DeepCopy();
            if (LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
            if (DataElement != null) dest.DataElement = (Hl7.Fhir.Model.Base64Binary)DataElement.DeepCopy();
            if (UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
            if (SizeElement != null) dest.SizeElement = (Hl7.Fhir.Model.UnsignedInt)SizeElement.DeepCopy();
            if (HashElement != null) dest.HashElement = (Hl7.Fhir.Model.Base64Binary)HashElement.DeepCopy();
            if (TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
            if (CreationElement != null) dest.CreationElement = (Hl7.Fhir.Model.FhirDateTime)CreationElement.DeepCopy();
            return dest;
        }

        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new TestAttachment());
        }

        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as TestAttachment;
            if (otherT == null) return false;

            if (!base.Matches(otherT)) return false;
            if (!DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
            if (!DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
            if (!DeepComparable.Matches(DataElement, otherT.DataElement)) return false;
            if (!DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if (!DeepComparable.Matches(SizeElement, otherT.SizeElement)) return false;
            if (!DeepComparable.Matches(HashElement, otherT.HashElement)) return false;
            if (!DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if (!DeepComparable.Matches(CreationElement, otherT.CreationElement)) return false;

            return true;
        }

        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as TestAttachment;
            if (otherT == null) return false;

            if (!base.IsExactly(otherT)) return false;
            if (!DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
            if (!DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
            if (!DeepComparable.IsExactly(DataElement, otherT.DataElement)) return false;
            if (!DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if (!DeepComparable.IsExactly(SizeElement, otherT.SizeElement)) return false;
            if (!DeepComparable.IsExactly(HashElement, otherT.HashElement)) return false;
            if (!DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if (!DeepComparable.IsExactly(CreationElement, otherT.CreationElement)) return false;

            return true;
        }

        [IgnoreDataMember]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (ContentTypeElement != null) yield return ContentTypeElement;
                if (LanguageElement != null) yield return LanguageElement;
                if (DataElement != null) yield return DataElement;
                if (UrlElement != null) yield return UrlElement;
                if (SizeElement != null) yield return SizeElement;
                if (HashElement != null) yield return HashElement;
                if (TitleElement != null) yield return TitleElement;
                if (CreationElement != null) yield return CreationElement;
            }
        }

        [IgnoreDataMember]
        public override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (ContentTypeElement != null) yield return new ElementValue("contentType", ContentTypeElement);
                if (LanguageElement != null) yield return new ElementValue("language", LanguageElement);
                if (DataElement != null) yield return new ElementValue("data", DataElement);
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                if (SizeElement != null) yield return new ElementValue("size", SizeElement);
                if (HashElement != null) yield return new ElementValue("hash", HashElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (CreationElement != null) yield return new ElementValue("creation", CreationElement);
            }
        }

        protected override bool TryGetValue(string key, out object value)
        {
            switch (key)
            {
                case "contentType":
                    value = ContentTypeElement;
                    return ContentTypeElement is not null;
                case "language":
                    value = LanguageElement;
                    return LanguageElement is not null;
                case "data":
                    value = DataElement;
                    return DataElement is not null;
                case "url":
                    value = UrlElement;
                    return UrlElement is not null;
                case "size":
                    value = SizeElement;
                    return SizeElement is not null;
                case "hash":
                    value = HashElement;
                    return HashElement is not null;
                case "title":
                    value = TitleElement;
                    return TitleElement is not null;
                case "creation":
                    value = CreationElement;
                    return CreationElement is not null;
                default:
                    return base.TryGetValue(key, out value);
            };

        }

        protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
        {
            foreach (var kvp in base.GetElementPairs()) yield return kvp;
            if (ContentTypeElement is not null) yield return new KeyValuePair<string, object>("contentType", ContentTypeElement);
            if (LanguageElement is not null) yield return new KeyValuePair<string, object>("language", LanguageElement);
            if (DataElement is not null) yield return new KeyValuePair<string, object>("data", DataElement);
            if (UrlElement is not null) yield return new KeyValuePair<string, object>("url", UrlElement);
            if (SizeElement is not null) yield return new KeyValuePair<string, object>("size", SizeElement);
            if (HashElement is not null) yield return new KeyValuePair<string, object>("hash", HashElement);
            if (TitleElement is not null) yield return new KeyValuePair<string, object>("title", TitleElement);
            if (CreationElement is not null) yield return new KeyValuePair<string, object>("creation", CreationElement);
        }

    }

}

// end of file
