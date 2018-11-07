using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
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

//
// Generated, and then post-processed by hand for FHIR v1.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Base Resource
    /// </summary>
    [DataContract]
    [FhirType("Resource", IsResource = true)]
    public abstract partial class Resource : Base
    {
        [NotMapped]
        public virtual ResourceType ResourceType { get { return ResourceType.Resource; } }
        [NotMapped]
        public override string TypeName { get { return "Resource"; } }
        
        /// <summary>
        /// Codes indicating the type of validation to perform
        /// </summary>
        [FhirEnumeration("ResourceValidationMode")]
        public enum ResourceValidationMode
        {
            /// <summary>
            /// The server checks the content, and then checks that the content would be acceptable as a create (e.g. that the content would not violate any uniqueness constraints).
            /// </summary>
            [EnumLiteral("create", "http://hl7.org/fhir/resource-validation-mode")]
            Create,
            /// <summary>
            /// The server checks the content, and then checks that it would accept it as an update against the nominated specific resource (e.g. that there are no changes to immutable fields the server does not allow to change, and checking version integrity if appropriate).
            /// </summary>
            [EnumLiteral("update", "http://hl7.org/fhir/resource-validation-mode")]
            Update,
            /// <summary>
            /// The server ignores the content, and checks that the nominated resource is allowed to be deleted (e.g. checking referential integrity rules).
            /// </summary>
            [EnumLiteral("delete", "http://hl7.org/fhir/resource-validation-mode")]
            Delete,
        }
        
        /// <summary>
        /// Logical id of this artifact
        /// </summary>
        [FhirElement("id", InSummary=true, Order=10)]
        [DataMember]
        public Hl7.Fhir.Model.Id IdElement
        {
            get { return _IdElement; }
            set { _IdElement = value; OnPropertyChanged("IdElement"); }
        }
        
        private Hl7.Fhir.Model.Id _IdElement;
        
        /// <summary>
        /// Logical id of this artifact
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Id
        {
            get { return IdElement != null ? IdElement.Value : null; }
            set
            {
                if(value == null)
                  IdElement = null; 
                else
                  IdElement = new Hl7.Fhir.Model.Id(value);
                OnPropertyChanged("Id");
            }
        }
        
        /// <summary>
        /// Metadata about the resource
        /// </summary>
        [FhirElement("meta", InSummary=true, Order=20)]
        [DataMember]
        public Hl7.Fhir.Model.Meta Meta
        {
            get { return _Meta; }
            set { _Meta = value; OnPropertyChanged("Meta"); }
        }
        
        private Hl7.Fhir.Model.Meta _Meta;
        
        /// <summary>
        /// A set of rules under which this content was created
        /// </summary>
        [FhirElement("implicitRules", InSummary=true, Order=30)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri ImplicitRulesElement
        {
            get { return _ImplicitRulesElement; }
            set { _ImplicitRulesElement = value; OnPropertyChanged("ImplicitRulesElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _ImplicitRulesElement;
        
        /// <summary>
        /// A set of rules under which this content was created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ImplicitRules
        {
            get { return ImplicitRulesElement != null ? ImplicitRulesElement.Value : null; }
            set
            {
                if(value == null)
                  ImplicitRulesElement = null; 
                else
                  ImplicitRulesElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("ImplicitRules");
            }
        }
        
        /// <summary>
        /// Language of the resource content
        /// </summary>
        [FhirElement("language", Order=40)]
        [DataMember]
        public Hl7.Fhir.Model.Code LanguageElement
        {
            get { return _LanguageElement; }
            set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
        }
        
        private Hl7.Fhir.Model.Code _LanguageElement;
        
        /// <summary>
        /// Language of the resource content
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Resource;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(IdElement != null) dest.IdElement = (Hl7.Fhir.Model.Id)IdElement.DeepCopy();
                if(Meta != null) dest.Meta = (Hl7.Fhir.Model.Meta)Meta.DeepCopy();
                if(ImplicitRulesElement != null) dest.ImplicitRulesElement = (Hl7.Fhir.Model.FhirUri)ImplicitRulesElement.DeepCopy();
                if(LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Resource;
            if(otherT == null) return false;
            
            if( !DeepComparable.Matches(IdElement, otherT.IdElement)) return false;
            if( !DeepComparable.Matches(Meta, otherT.Meta)) return false;
            if( !DeepComparable.Matches(ImplicitRulesElement, otherT.ImplicitRulesElement)) return false;
            if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Resource;
            if(otherT == null) return false;
            
            if( !DeepComparable.IsExactly(IdElement, otherT.IdElement)) return false;
            if( !DeepComparable.IsExactly(Meta, otherT.Meta)) return false;
            if( !DeepComparable.IsExactly(ImplicitRulesElement, otherT.ImplicitRulesElement)) return false;
            if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (IdElement != null) yield return IdElement;
                if (Meta != null) yield return Meta;
                if (ImplicitRulesElement != null) yield return ImplicitRulesElement;
                if (LanguageElement != null) yield return LanguageElement;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (IdElement != null) yield return new ElementValue("id",IdElement);
                if (Meta != null) yield return new ElementValue("meta",Meta);
                if (ImplicitRulesElement != null) yield return new ElementValue("implicitRules",ImplicitRulesElement);
                if (LanguageElement != null) yield return new ElementValue("language",LanguageElement);
            }
        }
    }

}
