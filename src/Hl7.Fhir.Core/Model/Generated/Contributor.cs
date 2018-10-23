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
    /// Contributor information
    /// </summary>
    [FhirType("Contributor")]
    [DataContract]
    public partial class Contributor : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Contributor"; } }
        
        /// <summary>
        /// The type of contributor
        /// (url: http://hl7.org/fhir/ValueSet/contributor-type)
        /// </summary>
        [FhirEnumeration("ContributorType")]
        public enum ContributorType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contributor-type)
            /// </summary>
            [EnumLiteral("author", "http://hl7.org/fhir/contributor-type"), Description("Author")]
            Author,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contributor-type)
            /// </summary>
            [EnumLiteral("editor", "http://hl7.org/fhir/contributor-type"), Description("Editor")]
            Editor,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contributor-type)
            /// </summary>
            [EnumLiteral("reviewer", "http://hl7.org/fhir/contributor-type"), Description("Reviewer")]
            Reviewer,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/contributor-type)
            /// </summary>
            [EnumLiteral("endorser", "http://hl7.org/fhir/contributor-type"), Description("Endorser")]
            Endorser,
        }

        /// <summary>
        /// author | editor | reviewer | endorser
        /// </summary>
        [FhirElement("type", InSummary=true, Order=30)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Contributor.ContributorType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Contributor.ContributorType> _TypeElement;
        
        /// <summary>
        /// author | editor | reviewer | endorser
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Contributor.ContributorType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.Contributor.ContributorType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Who contributed the content
        /// </summary>
        [FhirElement("name", InSummary=true, Order=40)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Who contributed the content
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if (value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Contact details of the contributor
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=50)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactDetail> _Contact;
        

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Contributor;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Contributor.ContributorType>)TypeElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<ContactDetail>(Contact.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Contributor());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Contributor;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Contributor;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (TypeElement != null) yield return TypeElement;
                if (NameElement != null) yield return NameElement;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren 
        { 
            get 
            { 
                foreach (var item in base.NamedChildren) yield return item; 
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
 
            } 
        } 
    
    
    }
    
}
