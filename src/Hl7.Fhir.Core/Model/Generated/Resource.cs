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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    [DataContract]
    public abstract partial class Resource : System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Additional Content defined by implementations
        /// </summary>
        [FhirElement("extension", Order=10)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Extension> Extension
        {
            get { return _Extension; }
            set { _Extension = value; OnPropertyChanged("Extension"); }
        }
        private List<Hl7.Fhir.Model.Extension> _Extension;
        
        /// <summary>
        /// Extensions that cannot be ignored
        /// </summary>
        [FhirElement("modifierExtension", Order=20)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Extension> ModifierExtension
        {
            get { return _ModifierExtension; }
            set { _ModifierExtension = value; OnPropertyChanged("ModifierExtension"); }
        }
        private List<Hl7.Fhir.Model.Extension> _ModifierExtension;
        
        /// <summary>
        /// Language of the resource content
        /// </summary>
        [FhirElement("language", Order=30)]
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
        
        /// <summary>
        /// Text summary of the resource, for human interpretation
        /// </summary>
        [FhirElement("text", Order=40)]
        [DataMember]
        public Hl7.Fhir.Model.Narrative Text
        {
            get { return _Text; }
            set { _Text = value; OnPropertyChanged("Text"); }
        }
        private Hl7.Fhir.Model.Narrative _Text;
        
        /// <summary>
        /// Contained, inline Resources
        /// </summary>
        [FhirElement("contained", Order=50, Choice=ChoiceType.ResourceChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Resource))]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Resource> Contained
        {
            get { return _Contained; }
            set { _Contained = value; OnPropertyChanged("Contained"); }
        }
        private List<Hl7.Fhir.Model.Resource> _Contained;
        
        /// <summary>
        /// Local id for element
        /// </summary>
        [FhirElement("id", XmlSerialization=XmlSerializationHint.Attribute, InSummary=true, Order=60)]
        [IdPattern]
        [DataMember]
        public string Id
        {
            get { return _Id; }
            set { _Id = value; OnPropertyChanged("Id"); }
        }
        private string _Id;
        
        public virtual IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Resource;
            
            if (dest != null)
            {
                if(Extension != null) dest.Extension = new List<Hl7.Fhir.Model.Extension>(Extension.DeepCopy());
                if(ModifierExtension != null) dest.ModifierExtension = new List<Hl7.Fhir.Model.Extension>(ModifierExtension.DeepCopy());
                if(LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
                if(Text != null) dest.Text = (Hl7.Fhir.Model.Narrative)Text.DeepCopy();
                if(Contained != null) dest.Contained = new List<Hl7.Fhir.Model.Resource>(Contained.DeepCopy());
                if(Id != null) dest.Id = Id;
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public virtual bool Matches(IDeepComparable other)
        {
            var otherT = other as Resource;
            if(otherT == null) return false;
            
            if( !DeepComparable.Matches(Extension, otherT.Extension)) return false;
            if( !DeepComparable.Matches(ModifierExtension, otherT.ModifierExtension)) return false;
            if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
            if( !DeepComparable.Matches(Text, otherT.Text)) return false;
            if( !DeepComparable.Matches(Contained, otherT.Contained)) return false;
            if( Id != otherT.Id ) return false;
            
            return true;
        }
        
        public virtual bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Resource;
            if(otherT == null) return false;
            
            if( !DeepComparable.IsExactly(Extension, otherT.Extension)) return false;
            if( !DeepComparable.IsExactly(ModifierExtension, otherT.ModifierExtension)) return false;
            if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
            if( !DeepComparable.IsExactly(Text, otherT.Text)) return false;
            if( !DeepComparable.IsExactly(Contained, otherT.Contained)) return false;
            if( Id != otherT.Id ) return false;
            
            return true;
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            	PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(property));
        }
    }
    
}
