using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;
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
// Generated, but posts-processed by hand for FHIR v1.0.2
//
namespace Hl7.Fhir.Model
{
#if NET45
    [Serializable]
#endif
    [System.Diagnostics.DebuggerDisplay(@"\{Value={Value} Url={_Url}}")]
    [FhirType("Extension")]
    [DataContract]
    public partial class Extension : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        public Extension()
        {
        }

        public Extension(string url, Element value)
        {
            this.Url = url;
            this.Value = value;
        }

        [NotMapped]
        public override string TypeName { get { return "Extension"; } }

        /// <summary>
        /// identifies the meaning of the extension
        /// </summary>
        [FhirElement("url", XmlSerialization = XmlRepresentation.XmlAttr, InSummary = true, Order = 30, TypeRedirect = typeof(FhirUri))]
        [Cardinality(Min = 1, Max = 1)]
        [UriPattern]
        [DataMember]
        public string Url
        {
            get { return _Url; }
            set { _Url = value; OnPropertyChanged("Url"); }
        }

        private string _Url;

        /// <summary>
        /// Value of extension
        /// </summary>
        [FhirElement("value", InSummary = true, Order = 40, Choice = ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary), typeof(Hl7.Fhir.Model.FhirBoolean), typeof(Hl7.Fhir.Model.Code), typeof(Hl7.Fhir.Model.Date), typeof(Hl7.Fhir.Model.FhirDateTime), typeof(Hl7.Fhir.Model.FhirDecimal), typeof(Hl7.Fhir.Model.Id), typeof(Hl7.Fhir.Model.Instant), typeof(Hl7.Fhir.Model.Integer), typeof(Hl7.Fhir.Model.Markdown), typeof(Hl7.Fhir.Model.Oid), typeof(Hl7.Fhir.Model.PositiveInt), typeof(Hl7.Fhir.Model.FhirString), typeof(Hl7.Fhir.Model.Time), typeof(Hl7.Fhir.Model.UnsignedInt), typeof(Hl7.Fhir.Model.FhirUri), typeof(Hl7.Fhir.Model.Address), typeof(Age), typeof(Hl7.Fhir.Model.Annotation), typeof(Hl7.Fhir.Model.Attachment), typeof(Hl7.Fhir.Model.CodeableConcept), typeof(Hl7.Fhir.Model.Coding), typeof(Hl7.Fhir.Model.ContactPoint), typeof(Count), typeof(Distance), typeof(Duration), typeof(Hl7.Fhir.Model.HumanName), typeof(Hl7.Fhir.Model.Identifier), typeof(Money), typeof(Hl7.Fhir.Model.Period), typeof(Quantity), typeof(Hl7.Fhir.Model.Range), typeof(Hl7.Fhir.Model.Ratio), typeof(Hl7.Fhir.Model.ResourceReference), typeof(Hl7.Fhir.Model.SampledData), typeof(Hl7.Fhir.Model.Signature), typeof(Hl7.Fhir.Model.Timing), typeof(Hl7.Fhir.Model.Meta))]
        [DataMember]
        public Hl7.Fhir.Model.Element Value
        {
            get { return _Value; }
            set { _Value = value; OnPropertyChanged("Value"); }
        }

        private Hl7.Fhir.Model.Element _Value;

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Extension;

            if (dest != null)
            {
                base.CopyTo(dest);
                if (Url != null) dest.Url = Url;
                if (Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Extension());
        }

        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Extension;
            if (otherT == null) return false;

            if (!base.Matches(otherT)) return false;
            if (Url != otherT.Url) return false;
            if (!DeepComparable.Matches(Value, otherT.Value)) return false;

            return true;
        }

        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Extension;
            if (otherT == null) return false;

            if (!base.IsExactly(otherT)) return false;
            if (Url != otherT.Url) return false;
            if (!DeepComparable.IsExactly(Value, otherT.Value)) return false;

            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (Value != null) yield return Value;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                // Extension elements 
                foreach (var item in base.NamedChildren) yield return item;
                if(Url != null) yield return new ElementValue("url",Url);
                if (Value != null) yield return new ElementValue ("value",Value);
            }
        } 
    }
}
