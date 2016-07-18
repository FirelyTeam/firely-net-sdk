using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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
// Generated for FHIR v1.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Related resources for the module
    /// </summary>
    [FhirType("RelatedResource")]
    [DataContract]
    public partial class RelatedResource : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "RelatedResource"; } }
        
        /// <summary>
        /// The type of related resource for the module
        /// (url: http://hl7.org/fhir/ValueSet/related-resource-type)
        /// </summary>
        [FhirEnumeration("RelatedResourceType")]
        public enum RelatedResourceType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-resource-type)
            /// </summary>
            [EnumLiteral("documentation"), Description("Documentation")]
            Documentation,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-resource-type)
            /// </summary>
            [EnumLiteral("justification"), Description("Justification")]
            Justification,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-resource-type)
            /// </summary>
            [EnumLiteral("citation"), Description("Citation")]
            Citation,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-resource-type)
            /// </summary>
            [EnumLiteral("predecessor"), Description("Predecessor")]
            Predecessor,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-resource-type)
            /// </summary>
            [EnumLiteral("successor"), Description("Successor")]
            Successor,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-resource-type)
            /// </summary>
            [EnumLiteral("derived-from"), Description("Derived From")]
            DerivedFrom,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-resource-type)
            /// </summary>
            [EnumLiteral("depends-on"), Description("Depends On")]
            DependsOn,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-resource-type)
            /// </summary>
            [EnumLiteral("composed-of"), Description("Composed Of")]
            ComposedOf,
        }

        /// <summary>
        /// documentation | justification | citation | predecessor | successor | derived-from | depends-on | composed-of
        /// </summary>
        [FhirElement("type", InSummary=true, Order=30)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RelatedResource.RelatedResourceType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RelatedResource.RelatedResourceType> _TypeElement;
        
        /// <summary>
        /// documentation | justification | citation | predecessor | successor | derived-from | depends-on | composed-of
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.RelatedResource.RelatedResourceType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if(value == null)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.RelatedResource.RelatedResourceType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Brief description of the related resource
        /// </summary>
        [FhirElement("display", InSummary=true, Order=40)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DisplayElement
        {
            get { return _DisplayElement; }
            set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DisplayElement;
        
        /// <summary>
        /// Brief description of the related resource
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Display
        {
            get { return DisplayElement != null ? DisplayElement.Value : null; }
            set
            {
                if(value == null)
                  DisplayElement = null; 
                else
                  DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Display");
            }
        }
        
        /// <summary>
        /// Bibliographic citation for the resource
        /// </summary>
        [FhirElement("citation", InSummary=true, Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CitationElement
        {
            get { return _CitationElement; }
            set { _CitationElement = value; OnPropertyChanged("CitationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CitationElement;
        
        /// <summary>
        /// Bibliographic citation for the resource
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Citation
        {
            get { return CitationElement != null ? CitationElement.Value : null; }
            set
            {
                if(value == null)
                  CitationElement = null; 
                else
                  CitationElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Citation");
            }
        }
        
        /// <summary>
        /// Url for the related resource
        /// </summary>
        [FhirElement("url", InSummary=true, Order=60)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Url for the related resource
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
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
        /// The related document
        /// </summary>
        [FhirElement("document", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Document
        {
            get { return _Document; }
            set { _Document = value; OnPropertyChanged("Document"); }
        }
        
        private Hl7.Fhir.Model.Attachment _Document;
        
        /// <summary>
        /// The related resource
        /// </summary>
        [FhirElement("resource", InSummary=true, Order=80)]
        [References()]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Resource
        {
            get { return _Resource; }
            set { _Resource = value; OnPropertyChanged("Resource"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Resource;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as RelatedResource;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.RelatedResource.RelatedResourceType>)TypeElement.DeepCopy();
                if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                if(CitationElement != null) dest.CitationElement = (Hl7.Fhir.Model.FhirString)CitationElement.DeepCopy();
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Document != null) dest.Document = (Hl7.Fhir.Model.Attachment)Document.DeepCopy();
                if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new RelatedResource());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as RelatedResource;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
            if( !DeepComparable.Matches(CitationElement, otherT.CitationElement)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Document, otherT.Document)) return false;
            if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as RelatedResource;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
            if( !DeepComparable.IsExactly(CitationElement, otherT.CitationElement)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Document, otherT.Document)) return false;
            if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
            
            return true;
        }
        
    }
    
}
