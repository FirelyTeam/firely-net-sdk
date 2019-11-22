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
#if !NETSTANDARD1_1
    [Serializable]
#endif
    [System.Diagnostics.DebuggerDisplay(@"\{Value={Value} Url={_Url}}")]
    [FhirType(Version.All, "Extension")]
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
        [FhirElement("url", XmlSerialization =XmlRepresentation.XmlAttr, InSummary = new[] { Version.All }, Order = 30, TypeRedirect = typeof(FhirUri))]
        [CLSCompliant(false)]
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
        [FhirElement("value", InSummary =  new[] { Version.All }, Order = 40, Choice = ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(
            Version = Version.DSTU2, 
            Types = new[] 
            {
                typeof(FhirBoolean),
                typeof(Integer),
                typeof(FhirDecimal),
                typeof(Base64Binary),
                typeof(Instant),
                typeof(FhirString),
                typeof(FhirUri),
                typeof(Date),
                typeof(FhirDateTime),
                typeof(Time),
                typeof(Code),
                typeof(Oid),
                typeof(Id),
                typeof(UnsignedInt),
                typeof(PositiveInt),
                typeof(Markdown),
                typeof(Annotation),
                typeof(Attachment),
                typeof(Identifier),
                typeof(CodeableConcept),
                typeof(Coding),
                typeof(Quantity),
                typeof(Range),
                typeof(Period),
                typeof(Ratio),
                typeof(DSTU2.SampledData),
                typeof(DSTU2.Signature),
                typeof(DSTU2.HumanName),
                typeof(Address),
                typeof(DSTU2.ContactPoint),
                typeof(DSTU2.Timing),
                typeof(ResourceReference),
                typeof(Meta),
            }
        )]
        [AllowedTypes(
            Version = Version.STU3, 
            Types = new[] 
            {
                typeof(Base64Binary),
                typeof(FhirBoolean),
                typeof(Code),
                typeof(Date),
                typeof(FhirDateTime),
                typeof(FhirDecimal),
                typeof(Id),
                typeof(Instant),
                typeof(Integer),
                typeof(Markdown),
                typeof(Oid),
                typeof(PositiveInt),
                typeof(FhirString),
                typeof(Time),
                typeof(UnsignedInt),
                typeof(FhirUri),
                typeof(Address),
                typeof(STU3.Age),
                typeof(Annotation),
                typeof(Attachment),
                typeof(CodeableConcept),
                typeof(Coding),
                typeof(STU3.ContactPoint),
                typeof(STU3.Count),
                typeof(STU3.Distance),
                typeof(STU3.Duration),
                typeof(STU3.HumanName),
                typeof(Identifier),
                typeof(STU3.Money),
                typeof(Period),
                typeof(Quantity),
                typeof(Range),
                typeof(Ratio),
                typeof(ResourceReference),
                typeof(STU3.SampledData),
                typeof(STU3.Signature),
                typeof(STU3.Timing),
                typeof(Meta)
            }
        )]
        [AllowedTypes(
            Version = Version.R4,
            Types = new[]
            {
                typeof(Base64Binary),
                typeof(FhirBoolean),
                typeof(Canonical),
                typeof(Code),
                typeof(Date),
                typeof(FhirDateTime),
                typeof(FhirDecimal),
                typeof(Id),
                typeof(Instant),
                typeof(Integer),
                typeof(Markdown),
                typeof(Oid),
                typeof(PositiveInt),
                typeof(FhirString),
                typeof(Time),
                typeof(UnsignedInt),
                typeof(FhirUri),
                typeof(Url),
                typeof(Uuid),
                typeof(Address),
                typeof(R4.Age),
                typeof(Annotation),
                typeof(Attachment),
                typeof(CodeableConcept),
                typeof(Coding),
                typeof(R4.ContactPoint),
                typeof(R4.Count),
                typeof(R4.Distance),
                typeof(R4.Duration),
                typeof(R4.HumanName),
                typeof(Identifier),
                typeof(R4.Money),
                typeof(Period),
                typeof(Quantity),
                typeof(Range),
                typeof(Ratio),
                typeof(ResourceReference),
                typeof(R4.SampledData),
                typeof(R4.Signature),
                typeof(R4.Timing),
                typeof(R4.ContactDetail),
                typeof(R4.Contributor),
                typeof(R4.DataRequirement),
                typeof(Expression),
                typeof(R4.ParameterDefinition),
                typeof(R4.RelatedArtifact),
                typeof(R4.TriggerDefinition),
                typeof(UsageContext),
                typeof(R4.Dosage)
            }
        )]
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

        public override void Serialize(StreamingSerializer serializer)
        {
            serializer.BeginDataType(TypeName);
            serializer.StringValue("url", Url, summaryVersions: Version.None, isRequired: true);
            base.Serialize(serializer);
            serializer.Element("value", isChoice: true); Value?.Serialize(serializer);
            serializer.End();
        }
    }
}
