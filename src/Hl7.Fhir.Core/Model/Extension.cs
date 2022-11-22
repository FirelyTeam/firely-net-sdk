using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
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
        [FhirElement("url", XmlSerialization =XmlRepresentation.XmlAttr, InSummary = Version.All, Order = 30, TypeRedirect = typeof(FhirUri))]
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
        [FhirElement("value", InSummary = Version.All, Order = 40, Choice = ChoiceType.DatatypeChoice)]
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

        internal override void Serialize(SerializerSink sink)
        {
            sink.BeginDataType(TypeName);
            sink.StringValue("url", Url, summaryVersions: Version.None, isRequired: true);
            base.Serialize(sink);
            sink.Element("value", isChoice: true); Value?.Serialize(sink);
            sink.End();
        }

        internal override bool SetElementFromJson(string jsonPropertyName, ref JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "url":
                    Url = source.GetUrl();
                    return true;
                case "valueBoolean":
                    source.CheckDuplicates<FhirBoolean>(Value, "value");
                    Value = source.PopulateValue(Value as FhirBoolean);
                    return true;
                case "_valueBoolean":
                    source.CheckDuplicates<FhirBoolean>(Value, "value");
                    Value = source.Populate(Value as FhirBoolean);
                    return true;
                case "valueInteger":
                    source.CheckDuplicates<Integer>(Value, "value");
                    Value = source.PopulateValue(Value as Integer);
                    return true;
                case "_valueInteger":
                    source.CheckDuplicates<Integer>(Value, "value");
                    Value = source.Populate(Value as Integer);
                    return true;
                case "valueDecimal":
                    source.CheckDuplicates<FhirDecimal>(Value, "value");
                    Value = source.PopulateValue(Value as FhirDecimal);
                    return true;
                case "_valueDecimal":
                    source.CheckDuplicates<FhirDecimal>(Value, "value");
                    Value = source.Populate(Value as FhirDecimal);
                    return true;
                case "valueBase64Binary":
                    source.CheckDuplicates<Base64Binary>(Value, "value");
                    Value = source.PopulateValue(Value as Base64Binary);
                    return true;
                case "_valueBase64Binary":
                    source.CheckDuplicates<Base64Binary>(Value, "value");
                    Value = source.Populate(Value as Base64Binary);
                    return true;
                case "valueInstant":
                    source.CheckDuplicates<Instant>(Value, "value");
                    Value = source.PopulateValue(Value as Instant);
                    return true;
                case "_valueInstant":
                    source.CheckDuplicates<Instant>(Value, "value");
                    Value = source.Populate(Value as Instant);
                    return true;
                case "valueString":
                    source.CheckDuplicates<FhirString>(Value, "value");
                    Value = source.PopulateValue(Value as FhirString);
                    return true;
                case "_valueString":
                    source.CheckDuplicates<FhirString>(Value, "value");
                    Value = source.Populate(Value as FhirString);
                    return true;
                case "valueUri":
                    source.CheckDuplicates<FhirUri>(Value, "value");
                    Value = source.PopulateValue(Value as FhirUri);
                    return true;
                case "_valueUri":
                    source.CheckDuplicates<FhirUri>(Value, "value");
                    Value = source.Populate(Value as FhirUri);
                    return true;
                case "valueDate":
                    source.CheckDuplicates<Date>(Value, "value");
                    Value = source.PopulateValue(Value as Date);
                    return true;
                case "_valueDate":
                    source.CheckDuplicates<Date>(Value, "value");
                    Value = source.Populate(Value as Date);
                    return true;
                case "valueDateTime":
                    source.CheckDuplicates<FhirDateTime>(Value, "value");
                    Value = source.PopulateValue(Value as FhirDateTime);
                    return true;
                case "_valueDateTime":
                    source.CheckDuplicates<FhirDateTime>(Value, "value");
                    Value = source.Populate(Value as FhirDateTime);
                    return true;
                case "valueTime":
                    source.CheckDuplicates<Time>(Value, "value");
                    Value = source.PopulateValue(Value as Time);
                    return true;
                case "_valueTime":
                    source.CheckDuplicates<Time>(Value, "value");
                    Value = source.Populate(Value as Time);
                    return true;
                case "valueCode":
                    source.CheckDuplicates<Code>(Value, "value");
                    Value = source.PopulateValue(Value as Code);
                    return true;
                case "_valueCode":
                    source.CheckDuplicates<Code>(Value, "value");
                    Value = source.Populate(Value as Code);
                    return true;
                case "valueOid":
                    source.CheckDuplicates<Oid>(Value, "value");
                    Value = source.PopulateValue(Value as Oid);
                    return true;
                case "_valueOid":
                    source.CheckDuplicates<Oid>(Value, "value");
                    Value = source.Populate(Value as Oid);
                    return true;
                case "valueId":
                    source.CheckDuplicates<Id>(Value, "value");
                    Value = source.PopulateValue(Value as Id);
                    return true;
                case "_valueId":
                    source.CheckDuplicates<Id>(Value, "value");
                    Value = source.Populate(Value as Id);
                    return true;
                case "valueUnsignedInt":
                    source.CheckDuplicates<UnsignedInt>(Value, "value");
                    Value = source.PopulateValue(Value as UnsignedInt);
                    return true;
                case "_valueUnsignedInt":
                    source.CheckDuplicates<UnsignedInt>(Value, "value");
                    Value = source.Populate(Value as UnsignedInt);
                    return true;
                case "valuePositiveInt":
                    source.CheckDuplicates<PositiveInt>(Value, "value");
                    Value = source.PopulateValue(Value as PositiveInt);
                    return true;
                case "_valuePositiveInt":
                    source.CheckDuplicates<PositiveInt>(Value, "value");
                    Value = source.Populate(Value as PositiveInt);
                    return true;
                case "valueMarkdown":
                    source.CheckDuplicates<Markdown>(Value, "value");
                    Value = source.PopulateValue(Value as Markdown);
                    return true;
                case "_valueMarkdown":
                    source.CheckDuplicates<Markdown>(Value, "value");
                    Value = source.Populate(Value as Markdown);
                    return true;
                case "valueAnnotation":
                    source.CheckDuplicates<Annotation>(Value, "value");
                    Value = source.Populate(Value as Annotation);
                    return true;
                case "valueAttachment":
                    source.CheckDuplicates<Attachment>(Value, "value");
                    Value = source.Populate(Value as Attachment);
                    return true;
                case "valueIdentifier":
                    source.CheckDuplicates<Identifier>(Value, "value");
                    Value = source.Populate(Value as Identifier);
                    return true;
                case "valueCodeableConcept":
                    source.CheckDuplicates<CodeableConcept>(Value, "value");
                    Value = source.Populate(Value as CodeableConcept);
                    return true;
                case "valueCoding":
                    source.CheckDuplicates<Coding>(Value, "value");
                    Value = source.Populate(Value as Coding);
                    return true;
                case "valueQuantity":
                    source.CheckDuplicates<Quantity>(Value, "value");
                    Value = source.Populate(Value as Quantity);
                    return true;
                case "valueRange":
                    source.CheckDuplicates<Range>(Value, "value");
                    Value = source.Populate(Value as Range);
                    return true;
                case "valuePeriod":
                    source.CheckDuplicates<Period>(Value, "value");
                    Value = source.Populate(Value as Period);
                    return true;
                case "valueRatio":
                    source.CheckDuplicates<Ratio>(Value, "value");
                    Value = source.Populate(Value as Ratio);
                    return true;
                case "valueSampledData" when source.IsVersion(Version.DSTU2):
                    source.CheckDuplicates<DSTU2.SampledData>(Value, "value");
                    Value = source.Populate(Value as DSTU2.SampledData);
                    return true;
                case "valueSignature" when source.IsVersion(Version.DSTU2):
                    source.CheckDuplicates<DSTU2.Signature>(Value, "value");
                    Value = source.Populate(Value as DSTU2.Signature);
                    return true;
                case "valueHumanName" when source.IsVersion(Version.DSTU2):
                    source.CheckDuplicates<DSTU2.HumanName>(Value, "value");
                    Value = source.Populate(Value as DSTU2.HumanName);
                    return true;
                case "valueAddress":
                    source.CheckDuplicates<Address>(Value, "value");
                    Value = source.Populate(Value as Address);
                    return true;
                case "valueContactPoint" when source.IsVersion(Version.DSTU2):
                    source.CheckDuplicates<DSTU2.ContactPoint>(Value, "value");
                    Value = source.Populate(Value as DSTU2.ContactPoint);
                    return true;
                case "valueTiming" when source.IsVersion(Version.DSTU2):
                    source.CheckDuplicates<DSTU2.Timing>(Value, "value");
                    Value = source.Populate(Value as DSTU2.Timing);
                    return true;
                case "valueReference":
                    source.CheckDuplicates<ResourceReference>(Value, "value");
                    Value = source.Populate(Value as ResourceReference);
                    return true;
                case "valueMeta":
                    source.CheckDuplicates<Meta>(Value, "value");
                    Value = source.Populate(Value as Meta);
                    return true;
                case "valueCanonical" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<Canonical>(Value, "value");
                    Value = source.PopulateValue(Value as Canonical);
                    return true;
                case "_valueCanonical" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<Canonical>(Value, "value");
                    Value = source.Populate(Value as Canonical);
                    return true;
                case "valueUrl" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<Url>(Value, "value");
                    Value = source.PopulateValue(Value as Url);
                    return true;
                case "_valueUrl" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<Url>(Value, "value");
                    Value = source.Populate(Value as Url);
                   return true;
                case "valueUuid" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<Uuid>(Value, "value");
                    Value = source.PopulateValue(Value as Uuid);
                    return true;
                case "_valueUuid" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<Uuid>(Value, "value");
                    Value = source.Populate(Value as Uuid);
                    return true;
                case "valueAge" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.Age>(Value, "value");
                    Value = source.Populate(Value as R4.Age);
                    return true;
                case "valueContactPoint" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.ContactPoint>(Value, "value");
                    Value = source.Populate(Value as R4.ContactPoint);
                    return true;
                case "valueCount" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.Count>(Value, "value");
                    Value = source.Populate(Value as R4.Count);
                    return true;
                case "valueDistance" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.Distance>(Value, "value");
                    Value = source.Populate(Value as R4.Distance);
                    return true;
                case "valueDuration" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.Duration>(Value, "value");
                    Value = source.Populate(Value as R4.Duration);
                    return true;
                case "valueHumanName" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.HumanName>(Value, "value");
                    Value = source.Populate(Value as R4.HumanName);
                    return true;
                case "valueMoney" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.Money>(Value, "value");
                    Value = source.Populate(Value as R4.Money);
                    return true;
                case "valueSampledData" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.SampledData>(Value, "value");
                    Value = source.Populate(Value as R4.SampledData);
                    return true;
                case "valueSignature" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.Signature>(Value, "value");
                    Value = source.Populate(Value as R4.Signature);
                    return true;
                case "valueTiming" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.Timing>(Value, "value");
                    Value = source.Populate(Value as R4.Timing);
                    return true;
                case "valueContactDetail" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.ContactDetail>(Value, "value");
                    Value = source.Populate(Value as R4.ContactDetail);
                    return true;
                case "valueContributor" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.Contributor>(Value, "value");
                    Value = source.Populate(Value as R4.Contributor);
                    return true;
                case "valueDataRequirement" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.DataRequirement>(Value, "value");
                    Value = source.Populate(Value as R4.DataRequirement);
                    return true;
                case "valueExpression" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<Expression>(Value, "value");
                    Value = source.Populate(Value as Expression);
                    return true;
                case "valueParameterDefinition" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.ParameterDefinition>(Value, "value");
                    Value = source.Populate(Value as R4.ParameterDefinition);
                    return true;
                case "valueRelatedArtifact" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.RelatedArtifact>(Value, "value");
                    Value = source.Populate(Value as R4.RelatedArtifact);
                    return true;
                case "valueTriggerDefinition" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.TriggerDefinition>(Value, "value");
                    Value = source.Populate(Value as R4.TriggerDefinition);
                    return true;
                case "valueUsageContext" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<UsageContext>(Value, "value");
                    Value = source.Populate(Value as UsageContext);
                    return true;
                case "valueDosage" when source.IsVersion(Version.R4):
                    source.CheckDuplicates<R4.Dosage>(Value, "value");
                    Value = source.Populate(Value as R4.Dosage);
                    return true;
                case "valueAge" when source.IsVersion(Version.STU3):
                    source.CheckDuplicates<STU3.Age>(Value, "value");
                    Value = source.Populate(Value as STU3.Age);
                    return true;
                case "valueContactPoint" when source.IsVersion(Version.STU3):
                    source.CheckDuplicates<STU3.ContactPoint>(Value, "value");
                    Value = source.Populate(Value as STU3.ContactPoint);
                    return true;
                case "valueCount" when source.IsVersion(Version.STU3):
                    source.CheckDuplicates<STU3.Count>(Value, "value");
                    Value = source.Populate(Value as STU3.Count);
                    return true;
                case "valueDistance" when source.IsVersion(Version.STU3):
                    source.CheckDuplicates<STU3.Distance>(Value, "value");
                    Value = source.Populate(Value as STU3.Distance);
                    return true;
                case "valueDuration" when source.IsVersion(Version.STU3):
                    source.CheckDuplicates<STU3.Duration>(Value, "value");
                    Value = source.Populate(Value as STU3.Duration);
                    return true;
                case "valueHumanName" when source.IsVersion(Version.STU3):
                    source.CheckDuplicates<STU3.HumanName>(Value, "value");
                    Value = source.Populate(Value as STU3.HumanName);
                    return true;
                case "valueMoney" when source.IsVersion(Version.STU3):
                    source.CheckDuplicates<STU3.Money>(Value, "value");
                    Value = source.Populate(Value as STU3.Money);
                    return true;
                case "valueSampledData" when source.IsVersion(Version.STU3):
                    source.CheckDuplicates<STU3.SampledData>(Value, "value");
                    Value = source.Populate(Value as STU3.SampledData);
                    return true;
                case "valueSignature" when source.IsVersion(Version.STU3):
                    source.CheckDuplicates<STU3.Signature>(Value, "value");
                    Value = source.Populate(Value as STU3.Signature);
                    return true;
                case "valueTiming" when source.IsVersion(Version.STU3):
                    source.CheckDuplicates<STU3.Timing>(Value, "value");
                    Value = source.Populate(Value as STU3.Timing);
                    return true;
            }
            return false;
        }
    }
}
