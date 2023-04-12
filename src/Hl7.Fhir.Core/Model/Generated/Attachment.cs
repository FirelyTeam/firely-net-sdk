﻿using System;
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
    /// Content in a format defined elsewhere
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "Attachment")]
    [DataContract]
    public partial class Attachment : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Attachment"; } }
    
        
        /// <summary>
        /// Mime type of the content, with charset etc.
        /// </summary>
        [FhirElement("contentType", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
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
        /// Human language of the content (BCP-47)
        /// </summary>
        [FhirElement("language", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
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
        [NotMapped]
        [IgnoreDataMemberAttribute]
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
        [FhirElement("data", InSummary=Hl7.Fhir.Model.Version.DSTU2, Order=50)]
        [CLSCompliant(false)]
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
        
        /// <summary>
        /// Uri where the data can be found
        /// </summary>
        [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Url UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.Url _UrlElement;
        
        /// <summary>
        /// Uri where the data can be found
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if (value == null)
                    UrlElement = null;
                else
                    UrlElement = new Hl7.Fhir.Model.Url(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Number of bytes of content (if url provided)
        /// </summary>
        [FhirElement("size", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
        [CLSCompliant(false)]
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
        [NotMapped]
        [IgnoreDataMemberAttribute]
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
        [FhirElement("hash", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
        [CLSCompliant(false)]
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
        [NotMapped]
        [IgnoreDataMemberAttribute]
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
        [FhirElement("title", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
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
        [NotMapped]
        [IgnoreDataMemberAttribute]
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
        [FhirElement("creation", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
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
        [NotMapped]
        [IgnoreDataMemberAttribute]
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
    
    
        public static ElementDefinitionConstraint[] Attachment_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "att-1",
                severity: ConstraintSeverity.Warning,
                expression: "data.empty() or contentType",
                human: "It the Attachment has data, it SHALL have a contentType",
                xpath: "not(exists(f:data)) or exists(f:contentType)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "att-1",
                severity: ConstraintSeverity.Warning,
                expression: "data.empty() or contentType.exists()",
                human: "If the Attachment has data, it SHALL have a contentType",
                xpath: "not(exists(f:data)) or exists(f:contentType)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "att-1",
                severity: ConstraintSeverity.Warning,
                expression: "data.empty() or contentType.exists()",
                human: "It the Attachment has data, it SHALL have a contentType",
                xpath: "not(exists(f:data)) or exists(f:contentType)"
            ),
        };
    
        // TODO: Add code to enforce the above constraints
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Attachment;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(ContentTypeElement != null) dest.ContentTypeElement = (Hl7.Fhir.Model.Code)ContentTypeElement.DeepCopy();
                if(LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
                if(DataElement != null) dest.DataElement = (Hl7.Fhir.Model.Base64Binary)DataElement.DeepCopy();
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.Url)UrlElement.DeepCopy();
                if(SizeElement != null) dest.SizeElement = (Hl7.Fhir.Model.UnsignedInt)SizeElement.DeepCopy();
                if(HashElement != null) dest.HashElement = (Hl7.Fhir.Model.Base64Binary)HashElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(CreationElement != null) dest.CreationElement = (Hl7.Fhir.Model.FhirDateTime)CreationElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Attachment());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Attachment;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
            if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
            if( !DeepComparable.Matches(DataElement, otherT.DataElement)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(SizeElement, otherT.SizeElement)) return false;
            if( !DeepComparable.Matches(HashElement, otherT.HashElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(CreationElement, otherT.CreationElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Attachment;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
            if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
            if( !DeepComparable.IsExactly(DataElement, otherT.DataElement)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(SizeElement, otherT.SizeElement)) return false;
            if( !DeepComparable.IsExactly(HashElement, otherT.HashElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(CreationElement, otherT.CreationElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("Attachment");
            base.Serialize(sink);
            sink.Element("contentType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ContentTypeElement?.Serialize(sink);
            sink.Element("language", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LanguageElement?.Serialize(sink);
            sink.Element("data", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.DSTU2, false, false); DataElement?.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
            sink.Element("size", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SizeElement?.Serialize(sink);
            sink.Element("hash", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); HashElement?.Serialize(sink);
            sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TitleElement?.Serialize(sink);
            sink.Element("creation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CreationElement?.Serialize(sink);
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
                case "contentType":
                    ContentTypeElement = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "language":
                    LanguageElement = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "data":
                    DataElement = source.Get<Hl7.Fhir.Model.Base64Binary>();
                    return true;
                case "url":
                    UrlElement = source.Get<Hl7.Fhir.Model.Url>();
                    return true;
                case "size":
                    SizeElement = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                    return true;
                case "hash":
                    HashElement = source.Get<Hl7.Fhir.Model.Base64Binary>();
                    return true;
                case "title":
                    TitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "creation":
                    CreationElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
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
                case "contentType":
                    ContentTypeElement = source.PopulateValue(ContentTypeElement);
                    return true;
                case "_contentType":
                    ContentTypeElement = source.Populate(ContentTypeElement);
                    return true;
                case "language":
                    LanguageElement = source.PopulateValue(LanguageElement);
                    return true;
                case "_language":
                    LanguageElement = source.Populate(LanguageElement);
                    return true;
                case "data":
                    DataElement = source.PopulateValue(DataElement);
                    return true;
                case "_data":
                    DataElement = source.Populate(DataElement);
                    return true;
                case "url":
                    UrlElement = source.PopulateValue(UrlElement);
                    return true;
                case "_url":
                    UrlElement = source.Populate(UrlElement);
                    return true;
                case "size":
                    SizeElement = source.PopulateValue(SizeElement);
                    return true;
                case "_size":
                    SizeElement = source.Populate(SizeElement);
                    return true;
                case "hash":
                    HashElement = source.PopulateValue(HashElement);
                    return true;
                case "_hash":
                    HashElement = source.Populate(HashElement);
                    return true;
                case "title":
                    TitleElement = source.PopulateValue(TitleElement);
                    return true;
                case "_title":
                    TitleElement = source.Populate(TitleElement);
                    return true;
                case "creation":
                    CreationElement = source.PopulateValue(CreationElement);
                    return true;
                case "_creation":
                    CreationElement = source.Populate(CreationElement);
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
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
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
    
    }

}
