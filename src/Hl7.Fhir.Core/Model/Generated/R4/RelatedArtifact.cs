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
    /// Related artifacts for a knowledge resource
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "RelatedArtifact")]
    [DataContract]
    public partial class RelatedArtifact : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IRelatedArtifact, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "RelatedArtifact"; } }
    
        
        /// <summary>
        /// documentation | justification | citation | predecessor | successor | derived-from | depends-on | composed-of
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RelatedArtifactType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RelatedArtifactType> _TypeElement;
        
        /// <summary>
        /// documentation | justification | citation | predecessor | successor | derived-from | depends-on | composed-of
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.RelatedArtifactType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Code<Hl7.Fhir.Model.RelatedArtifactType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Short label
        /// </summary>
        [FhirElement("label", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString LabelElement
        {
            get { return _LabelElement; }
            set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _LabelElement;
        
        /// <summary>
        /// Short label
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Label
        {
            get { return LabelElement != null ? LabelElement.Value : null; }
            set
            {
                if (value == null)
                    LabelElement = null;
                else
                    LabelElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Label");
            }
        }
        
        /// <summary>
        /// Brief description of the related artifact
        /// </summary>
        [FhirElement("display", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DisplayElement
        {
            get { return _DisplayElement; }
            set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DisplayElement;
        
        /// <summary>
        /// Brief description of the related artifact
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Display
        {
            get { return DisplayElement != null ? DisplayElement.Value : null; }
            set
            {
                if (value == null)
                    DisplayElement = null;
                else
                    DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Display");
            }
        }
        
        /// <summary>
        /// Bibliographic citation for the artifact
        /// </summary>
        [FhirElement("citation", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown CitationElement
        {
            get { return _CitationElement; }
            set { _CitationElement = value; OnPropertyChanged("CitationElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _CitationElement;
        
        /// <summary>
        /// Bibliographic citation for the artifact
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Citation
        {
            get { return CitationElement != null ? CitationElement.Value : null; }
            set
            {
                if (value == null)
                    CitationElement = null;
                else
                    CitationElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Citation");
            }
        }
        
        /// <summary>
        /// Where the artifact can be accessed
        /// </summary>
        [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Url UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.Url _UrlElement;
        
        /// <summary>
        /// Where the artifact can be accessed
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
        /// What document is being referenced
        /// </summary>
        [FhirElement("document", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Document
        {
            get { return _Document; }
            set { _Document = value; OnPropertyChanged("Document"); }
        }
        
        private Hl7.Fhir.Model.Attachment _Document;
        
        /// <summary>
        /// What resource is being referenced
        /// </summary>
        [FhirElement("resource", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Canonical ResourceElement
        {
            get { return _ResourceElement; }
            set { _ResourceElement = value; OnPropertyChanged("ResourceElement"); }
        }
        
        private Hl7.Fhir.Model.Canonical _ResourceElement;
        
        /// <summary>
        /// What resource is being referenced
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Resource
        {
            get { return ResourceElement != null ? ResourceElement.Value : null; }
            set
            {
                if (value == null)
                    ResourceElement = null;
                else
                    ResourceElement = new Hl7.Fhir.Model.Canonical(value);
                OnPropertyChanged("Resource");
            }
        }
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as RelatedArtifact;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.RelatedArtifactType>)TypeElement.DeepCopy();
                if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                if(CitationElement != null) dest.CitationElement = (Hl7.Fhir.Model.Markdown)CitationElement.DeepCopy();
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.Url)UrlElement.DeepCopy();
                if(Document != null) dest.Document = (Hl7.Fhir.Model.Attachment)Document.DeepCopy();
                if(ResourceElement != null) dest.ResourceElement = (Hl7.Fhir.Model.Canonical)ResourceElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new RelatedArtifact());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as RelatedArtifact;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
            if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
            if( !DeepComparable.Matches(CitationElement, otherT.CitationElement)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Document, otherT.Document)) return false;
            if( !DeepComparable.Matches(ResourceElement, otherT.ResourceElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as RelatedArtifact;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
            if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
            if( !DeepComparable.IsExactly(CitationElement, otherT.CitationElement)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Document, otherT.Document)) return false;
            if( !DeepComparable.IsExactly(ResourceElement, otherT.ResourceElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("RelatedArtifact");
            base.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
            sink.Element("label", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LabelElement?.Serialize(sink);
            sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DisplayElement?.Serialize(sink);
            sink.Element("citation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CitationElement?.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
            sink.Element("document", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Document?.Serialize(sink);
            sink.Element("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ResourceElement?.Serialize(sink);
            sink.End();
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (TypeElement != null) yield return TypeElement;
                if (LabelElement != null) yield return LabelElement;
                if (DisplayElement != null) yield return DisplayElement;
                if (CitationElement != null) yield return CitationElement;
                if (UrlElement != null) yield return UrlElement;
                if (Document != null) yield return Document;
                if (ResourceElement != null) yield return ResourceElement;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (LabelElement != null) yield return new ElementValue("label", LabelElement);
                if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                if (CitationElement != null) yield return new ElementValue("citation", CitationElement);
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                if (Document != null) yield return new ElementValue("document", Document);
                if (ResourceElement != null) yield return new ElementValue("resource", ResourceElement);
            }
        }
    
    }

}
