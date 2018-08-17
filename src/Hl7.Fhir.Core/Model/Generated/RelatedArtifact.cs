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
    /// Related artifacts for a knowledge resource
    /// </summary>
    [FhirType("RelatedArtifact")]
    [DataContract]
    public partial class RelatedArtifact : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "RelatedArtifact"; } }
        
        /// <summary>
        /// The type of relationship to the related artifact
        /// (url: http://hl7.org/fhir/ValueSet/related-artifact-type)
        /// </summary>
        [FhirEnumeration("RelatedArtifactType")]
        public enum RelatedArtifactType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-artifact-type)
            /// </summary>
            [EnumLiteral("documentation", "http://hl7.org/fhir/related-artifact-type"), Description("Documentation")]
            Documentation,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-artifact-type)
            /// </summary>
            [EnumLiteral("justification", "http://hl7.org/fhir/related-artifact-type"), Description("Justification")]
            Justification,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-artifact-type)
            /// </summary>
            [EnumLiteral("citation", "http://hl7.org/fhir/related-artifact-type"), Description("Citation")]
            Citation,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-artifact-type)
            /// </summary>
            [EnumLiteral("predecessor", "http://hl7.org/fhir/related-artifact-type"), Description("Predecessor")]
            Predecessor,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-artifact-type)
            /// </summary>
            [EnumLiteral("successor", "http://hl7.org/fhir/related-artifact-type"), Description("Successor")]
            Successor,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-artifact-type)
            /// </summary>
            [EnumLiteral("derived-from", "http://hl7.org/fhir/related-artifact-type"), Description("Derived From")]
            DerivedFrom,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-artifact-type)
            /// </summary>
            [EnumLiteral("depends-on", "http://hl7.org/fhir/related-artifact-type"), Description("Depends On")]
            DependsOn,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/related-artifact-type)
            /// </summary>
            [EnumLiteral("composed-of", "http://hl7.org/fhir/related-artifact-type"), Description("Composed Of")]
            ComposedOf,
        }

        /// <summary>
        /// documentation | justification | citation | predecessor | successor | derived-from | depends-on | composed-of
        /// </summary>
        [FhirElement("type", InSummary=true, Order=30)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RelatedArtifact.RelatedArtifactType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RelatedArtifact.RelatedArtifactType> _TypeElement;
        
        /// <summary>
        /// documentation | justification | citation | predecessor | successor | derived-from | depends-on | composed-of
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.RelatedArtifact.RelatedArtifactType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.RelatedArtifact.RelatedArtifactType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Brief description of the related artifact
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
        [FhirElement("citation", InSummary=true, Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CitationElement
        {
            get { return _CitationElement; }
            set { _CitationElement = value; OnPropertyChanged("CitationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CitationElement;
        
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
                  CitationElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Citation");
            }
        }
        
        /// <summary>
        /// Where the artifact can be accessed
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
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// What document is being referenced
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
        /// What resource is being referenced
        /// </summary>
        [FhirElement("resource", InSummary=true, Order=80)]
        [CLSCompliant(false)]
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
            var dest = other as RelatedArtifact;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.RelatedArtifact.RelatedArtifactType>)TypeElement.DeepCopy();
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
            return CopyTo(new RelatedArtifact());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as RelatedArtifact;
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
            var otherT = other as RelatedArtifact;
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

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (TypeElement != null) yield return TypeElement;
                if (DisplayElement != null) yield return DisplayElement;
                if (CitationElement != null) yield return CitationElement;
                if (UrlElement != null) yield return UrlElement;
                if (Document != null) yield return Document;
                if (Resource != null) yield return Resource;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren 
        { 
            get 
            { 
                foreach (var item in base.NamedChildren) yield return item; 
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                if (CitationElement != null) yield return new ElementValue("citation", CitationElement);
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                if (Document != null) yield return new ElementValue("document", Document);
                if (Resource != null) yield return new ElementValue("resource", Resource);
 
            } 
        } 
    
    
    }
    
}
