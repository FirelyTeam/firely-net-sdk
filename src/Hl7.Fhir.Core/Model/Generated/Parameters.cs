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
// Generated for FHIR v1.0.2, v4.0.1, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Operation Request or Response
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "Parameters", IsResource=true)]
    [DataContract]
    public partial class Parameters : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Parameters; } }
        [NotMapped]
        public override string TypeName { get { return "Parameters"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "ParameterComponent")]
        [DataContract]
        public partial class ParameterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParameterComponent"; } }
            
            /// <summary>
            /// Name from the definition
            /// </summary>
            [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Name from the definition
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
            /// If parameter is a data type
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(Version=Version.DSTU2, Types=new[]{typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.DSTU2.SampledData),typeof(Hl7.Fhir.Model.DSTU2.Signature),typeof(Hl7.Fhir.Model.DSTU2.HumanName),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.DSTU2.ContactPoint),typeof(Hl7.Fhir.Model.DSTU2.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Meta)})]
            [AllowedTypes(Version=Version.R4, Types=new[]{typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Canonical),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Url),typeof(Hl7.Fhir.Model.Uuid),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.R4.Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.R4.ContactPoint),typeof(Hl7.Fhir.Model.R4.Count),typeof(Hl7.Fhir.Model.R4.Distance),typeof(Hl7.Fhir.Model.R4.Duration),typeof(Hl7.Fhir.Model.R4.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.R4.Money),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.R4.SampledData),typeof(Hl7.Fhir.Model.R4.Signature),typeof(Hl7.Fhir.Model.R4.Timing),typeof(Hl7.Fhir.Model.R4.ContactDetail),typeof(Hl7.Fhir.Model.R4.Contributor),typeof(Hl7.Fhir.Model.R4.DataRequirement),typeof(Hl7.Fhir.Model.Expression),typeof(Hl7.Fhir.Model.R4.ParameterDefinition),typeof(Hl7.Fhir.Model.R4.RelatedArtifact),typeof(Hl7.Fhir.Model.R4.TriggerDefinition),typeof(Hl7.Fhir.Model.UsageContext),typeof(Hl7.Fhir.Model.R4.Dosage),typeof(Hl7.Fhir.Model.Meta)})]
            [AllowedTypes(Version=Version.STU3, Types=new[]{typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.STU3.Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.STU3.ContactPoint),typeof(Hl7.Fhir.Model.STU3.Count),typeof(Hl7.Fhir.Model.STU3.Distance),typeof(Hl7.Fhir.Model.STU3.Duration),typeof(Hl7.Fhir.Model.STU3.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.STU3.Money),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.STU3.SampledData),typeof(Hl7.Fhir.Model.STU3.Signature),typeof(Hl7.Fhir.Model.STU3.Timing),typeof(Hl7.Fhir.Model.Meta)})]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            /// <summary>
            /// If parameter is a whole resource
            /// </summary>
            [FhirElement("resource", InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=60, Choice=ChoiceType.ResourceChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Resource))]
            [DataMember]
            public Hl7.Fhir.Model.Resource Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.Resource _Resource;
            
            /// <summary>
            /// Named part of a parameter (e.g. Tuple)
            /// </summary>
            [FhirElement("part", InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ParameterComponent> Part
            {
                get { if(_Part==null) _Part = new List<ParameterComponent>(); return _Part; }
                set { _Part = value; OnPropertyChanged("Part"); }
            }
            
            private List<ParameterComponent> _Part;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParameterComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, true, false); NameElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false, true); Value?.Serialize(sink);
                sink.Element("resource", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false, false); Resource?.Serialize(sink);
                sink.BeginList("part", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false);
                foreach(var item in Part)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "name":
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "valueDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                        return true;
                    case "valueBase64Binary":
                        source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Base64Binary>();
                        return true;
                    case "valueInstant":
                        source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Instant>();
                        return true;
                    case "valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "valueUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "valueDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Date>();
                        return true;
                    case "valueDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                        return true;
                    case "valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Time>();
                        return true;
                    case "valueCode":
                        source.CheckDuplicates<Hl7.Fhir.Model.Code>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Code>();
                        return true;
                    case "valueOid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Oid>();
                        return true;
                    case "valueId":
                        source.CheckDuplicates<Hl7.Fhir.Model.Id>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "valueUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "valuePositiveInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "valueMarkdown":
                        source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Markdown>();
                        return true;
                    case "valueAnnotation":
                        source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Annotation>();
                        return true;
                    case "valueAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Attachment>();
                        return true;
                    case "valueIdentifier":
                        source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "valueCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "valueCoding":
                        source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "valueQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Quantity>();
                        return true;
                    case "valueRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Range>();
                        return true;
                    case "valuePeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "valueRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Ratio>();
                        return true;
                    case "valueSampledData" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.DSTU2.SampledData>();
                        return true;
                    case "valueSignature" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.DSTU2.Signature>();
                        return true;
                    case "valueHumanName" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.DSTU2.HumanName>();
                        return true;
                    case "valueAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Address>();
                        return true;
                    case "valueContactPoint" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.DSTU2.ContactPoint>();
                        return true;
                    case "valueTiming" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.DSTU2.Timing>();
                        return true;
                    case "valueReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "valueMeta":
                        source.CheckDuplicates<Hl7.Fhir.Model.Meta>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Meta>();
                        return true;
                    case "valueCanonical" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Canonical>();
                        return true;
                    case "valueUrl" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.Url>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Url>();
                        return true;
                    case "valueUuid" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.Uuid>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Uuid>();
                        return true;
                    case "valueAge" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Age>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.Age>();
                        return true;
                    case "valueContactPoint" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.ContactPoint>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.ContactPoint>();
                        return true;
                    case "valueCount" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Count>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.Count>();
                        return true;
                    case "valueDistance" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Distance>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.Distance>();
                        return true;
                    case "valueDuration" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Duration>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.Duration>();
                        return true;
                    case "valueHumanName" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.HumanName>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.HumanName>();
                        return true;
                    case "valueMoney" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Money>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.Money>();
                        return true;
                    case "valueSampledData" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.SampledData>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.SampledData>();
                        return true;
                    case "valueSignature" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Signature>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.Signature>();
                        return true;
                    case "valueTiming" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Timing>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.Timing>();
                        return true;
                    case "valueContactDetail" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.ContactDetail>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.ContactDetail>();
                        return true;
                    case "valueContributor" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Contributor>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.Contributor>();
                        return true;
                    case "valueDataRequirement" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.DataRequirement>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.DataRequirement>();
                        return true;
                    case "valueExpression" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.Expression>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Expression>();
                        return true;
                    case "valueParameterDefinition" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.ParameterDefinition>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.ParameterDefinition>();
                        return true;
                    case "valueRelatedArtifact" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.RelatedArtifact>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.RelatedArtifact>();
                        return true;
                    case "valueTriggerDefinition" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.TriggerDefinition>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.TriggerDefinition>();
                        return true;
                    case "valueUsageContext" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.UsageContext>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.UsageContext>();
                        return true;
                    case "valueDosage" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Dosage>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.R4.Dosage>();
                        return true;
                    case "valueAge" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Age>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Age>();
                        return true;
                    case "valueContactPoint" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.ContactPoint>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.ContactPoint>();
                        return true;
                    case "valueCount" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Count>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Count>();
                        return true;
                    case "valueDistance" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Distance>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Distance>();
                        return true;
                    case "valueDuration" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Duration>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Duration>();
                        return true;
                    case "valueHumanName" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.HumanName>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.HumanName>();
                        return true;
                    case "valueMoney" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "valueSampledData" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.SampledData>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.SampledData>();
                        return true;
                    case "valueSignature" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Signature>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Signature>();
                        return true;
                    case "valueTiming" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Timing>();
                        return true;
                    case "resource":
                        Resource = source.GetResource();
                        return true;
                    case "part":
                        Part = source.GetList<ParameterComponent>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "_valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Integer);
                        return true;
                    case "_valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Integer);
                        return true;
                    case "valueDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirDecimal);
                        return true;
                    case "_valueDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirDecimal);
                        return true;
                    case "valueBase64Binary":
                        source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Base64Binary);
                        return true;
                    case "_valueBase64Binary":
                        source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Base64Binary);
                        return true;
                    case "valueInstant":
                        source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Instant);
                        return true;
                    case "_valueInstant":
                        source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Instant);
                        return true;
                    case "valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "valueUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "_valueUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "valueDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Date);
                        return true;
                    case "_valueDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Date);
                        return true;
                    case "valueDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirDateTime);
                        return true;
                    case "_valueDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirDateTime);
                        return true;
                    case "valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Time);
                        return true;
                    case "_valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Time);
                        return true;
                    case "valueCode":
                        source.CheckDuplicates<Hl7.Fhir.Model.Code>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Code);
                        return true;
                    case "_valueCode":
                        source.CheckDuplicates<Hl7.Fhir.Model.Code>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Code);
                        return true;
                    case "valueOid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Oid);
                        return true;
                    case "_valueOid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Oid);
                        return true;
                    case "valueId":
                        source.CheckDuplicates<Hl7.Fhir.Model.Id>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Id);
                        return true;
                    case "_valueId":
                        source.CheckDuplicates<Hl7.Fhir.Model.Id>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Id);
                        return true;
                    case "valueUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "_valueUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "valuePositiveInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.PositiveInt);
                        return true;
                    case "_valuePositiveInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.PositiveInt);
                        return true;
                    case "valueMarkdown":
                        source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Markdown);
                        return true;
                    case "_valueMarkdown":
                        source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Markdown);
                        return true;
                    case "valueAnnotation":
                        source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Annotation);
                        return true;
                    case "valueAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Attachment);
                        return true;
                    case "valueIdentifier":
                        source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Identifier);
                        return true;
                    case "valueCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "valueCoding":
                        source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Coding);
                        return true;
                    case "valueQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Quantity);
                        return true;
                    case "valueRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Range);
                        return true;
                    case "valuePeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Period);
                        return true;
                    case "valueRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Ratio);
                        return true;
                    case "valueSampledData" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.DSTU2.SampledData);
                        return true;
                    case "valueSignature" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Signature>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.DSTU2.Signature);
                        return true;
                    case "valueHumanName" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.HumanName>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.DSTU2.HumanName);
                        return true;
                    case "valueAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Address);
                        return true;
                    case "valueContactPoint" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.ContactPoint>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.DSTU2.ContactPoint);
                        return true;
                    case "valueTiming" when source.IsVersion(Hl7.Fhir.Model.Version.DSTU2):
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.DSTU2.Timing);
                        return true;
                    case "valueReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "valueMeta":
                        source.CheckDuplicates<Hl7.Fhir.Model.Meta>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Meta);
                        return true;
                    case "valueCanonical" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Canonical);
                        return true;
                    case "_valueCanonical" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Canonical);
                        return true;
                    case "valueUrl" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.Url>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Url);
                        return true;
                    case "_valueUrl" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.Url>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Url);
                        return true;
                    case "valueUuid" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.Uuid>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Uuid);
                        return true;
                    case "_valueUuid" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.Uuid>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Uuid);
                        return true;
                    case "valueAge" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Age>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Age);
                        return true;
                    case "valueContactPoint" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.ContactPoint>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.ContactPoint);
                        return true;
                    case "valueCount" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Count>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Count);
                        return true;
                    case "valueDistance" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Distance>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Distance);
                        return true;
                    case "valueDuration" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Duration>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Duration);
                        return true;
                    case "valueHumanName" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.HumanName>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.HumanName);
                        return true;
                    case "valueMoney" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Money>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Money);
                        return true;
                    case "valueSampledData" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.SampledData>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.SampledData);
                        return true;
                    case "valueSignature" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Signature>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Signature);
                        return true;
                    case "valueTiming" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Timing>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Timing);
                        return true;
                    case "valueContactDetail" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.ContactDetail>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.ContactDetail);
                        return true;
                    case "valueContributor" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Contributor>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Contributor);
                        return true;
                    case "valueDataRequirement" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.DataRequirement>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.DataRequirement);
                        return true;
                    case "valueExpression" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.Expression>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Expression);
                        return true;
                    case "valueParameterDefinition" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.ParameterDefinition>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.ParameterDefinition);
                        return true;
                    case "valueRelatedArtifact" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.RelatedArtifact>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.RelatedArtifact);
                        return true;
                    case "valueTriggerDefinition" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.TriggerDefinition>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.TriggerDefinition);
                        return true;
                    case "valueUsageContext" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.UsageContext>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.UsageContext);
                        return true;
                    case "valueDosage" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Dosage>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Dosage);
                        return true;
                    case "valueAge" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Age>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Age);
                        return true;
                    case "valueContactPoint" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.ContactPoint>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.ContactPoint);
                        return true;
                    case "valueCount" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Count>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Count);
                        return true;
                    case "valueDistance" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Distance>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Distance);
                        return true;
                    case "valueDuration" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Duration>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Duration);
                        return true;
                    case "valueHumanName" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.HumanName>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.HumanName);
                        return true;
                    case "valueMoney" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Money);
                        return true;
                    case "valueSampledData" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.SampledData>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.SampledData);
                        return true;
                    case "valueSignature" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Signature>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Signature);
                        return true;
                    case "valueTiming" when source.IsVersion(Hl7.Fhir.Model.Version.STU3):
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Timing);
                        return true;
                    case "resource":
                        Resource = source.GetResource();
                        return true;
                    case "part":
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "part":
                        source.PopulateListItem(Part, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParameterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.Resource)Resource.DeepCopy();
                    if(Part != null) dest.Part = new List<ParameterComponent>(Part.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ParameterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Part, otherT.Part)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Part, otherT.Part)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (Value != null) yield return Value;
                    if (Resource != null) yield return Resource;
                    foreach (var elem in Part) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (Value != null) yield return new ElementValue("value", Value);
                    if (Resource != null) yield return new ElementValue("resource", Resource);
                    foreach (var elem in Part) { if (elem != null) yield return new ElementValue("part", elem); }
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Operation Parameter
        /// </summary>
        [FhirElement("parameter", InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=50)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ParameterComponent> Parameter
        {
            get { if(_Parameter==null) _Parameter = new List<ParameterComponent>(); return _Parameter; }
            set { _Parameter = value; OnPropertyChanged("Parameter"); }
        }
        
        private List<ParameterComponent> _Parameter;
    
    
        public static ElementDefinitionConstraint[] Parameters_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "inv-1",
                severity: ConstraintSeverity.Warning,
                expression: "parameter.all((part.exists() and value.empty() and resource.empty()) or (part.empty() and (value.exists() xor resource.exists())))",
                human: "A parameter must have only one of (value, resource, part)",
                xpath: "exists(f:value) or exists(f:resource) and not(exists(f:value) and exists(f:resource))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "inv-1",
                severity: ConstraintSeverity.Warning,
                expression: "parameter.all((part.exists() and value.empty() and resource.empty()) or (part.empty() and (value.exists() xor resource.exists())))",
                human: "A parameter must have one and only one of (value, resource, part)",
                xpath: "(exists(f:resource) or exists(f:part) or exists(f:*[starts-with(local-name(.), 'value')])) and not(exists(f:*[starts-with(local-name(.), 'value')])) and exists(f:resource))) and not(exists(f:*[starts-with(local-name(.), 'value')])) and exists(f:part))) and not(exists(f:part) and exists(f:resource))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "inv-1",
                severity: ConstraintSeverity.Warning,
                expression: "parameter.all((part.exists() and value.empty() and resource.empty()) or (part.empty() and (value.exists() xor resource.exists())))",
                human: "A parameter must have only one of (value, resource, part)",
                xpath: "exists(f:value) or exists(f:resource) and not(exists(f:value) and exists(f:resource))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Parameters_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Parameters;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Parameter != null) dest.Parameter = new List<ParameterComponent>(Parameter.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Parameters());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Parameters;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Parameters;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Parameters");
            base.Serialize(sink);
            sink.BeginList("parameter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false);
            foreach(var item in Parameter)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "parameter":
                    Parameter = source.GetList<ParameterComponent>();
                    return true;
            }
            return false;
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "parameter":
                    source.SetList(this, jsonPropertyName);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "parameter":
                    source.PopulateListItem(Parameter, index);
                    return true;
            }
            return false;
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Parameter) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Parameter) { if (elem != null) yield return new ElementValue("parameter", elem); }
            }
        }
    
    }

}
