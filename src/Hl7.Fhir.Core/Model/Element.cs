using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Serialization;
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
// Generated on Tue, Sep 22, 2015 20:02+1000 for FHIR v1.0.1 and then post-processed by hand
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Base for all elements
    /// </summary>
#if NET45
    [Serializable]
#endif
    [DataContract]
    [FhirType("Element", IsResource=false)]
    public abstract partial class Element : Base, IExtendable
    {
        [NotMapped]
        public override string TypeName { get { return "Element"; } }
        
        /// <summary>
        /// xml:id (or equivalent in JSON)
        /// </summary>
        [FhirElement("id", XmlSerialization=XmlRepresentation.XmlAttr, InSummary=true, Order=10, TypeRedirect = typeof(FhirString))]
        [DataMember]
        public string ElementId
        {
            get { return _ElementId; }
            set { _ElementId = value; OnPropertyChanged("ElementId"); }
        }
        
        private string _ElementId;
        
        /// <summary>
        /// Additional Content defined by implementations
        /// </summary>
        [FhirElement("extension", InSummary=false, Order=20)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Extension> Extension
        {
            get { if(_Extension==null) _Extension = new List<Hl7.Fhir.Model.Extension>(); return _Extension; }
            set { _Extension = value; OnPropertyChanged("Extension"); }
        }
        
        private List<Hl7.Fhir.Model.Extension> _Extension;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            if (other is Element dest)
            {
                base.CopyTo(dest);
                if (ElementId != null) dest.ElementId = ElementId;
                if (Extension != null) dest.Extension = new List<Hl7.Fhir.Model.Extension>(Extension.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Element;
            if(otherT == null) return false;
            
            if( ElementId != otherT.ElementId ) return false;
            if( !DeepComparable.Matches(Extension, otherT.Extension)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Element;
            if(otherT == null) return false;
            
            if( ElementId != otherT.ElementId ) return false;
            if( !DeepComparable.IsExactly(Extension, otherT.Extension)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var p in Extension) { if (p != null) yield return p; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (ElementId != null)  yield return new ElementValue("id", ElementId);
                foreach (var p in Extension) { if (p != null) yield return new ElementValue("extension",p); }
            }
        }

    }

}
