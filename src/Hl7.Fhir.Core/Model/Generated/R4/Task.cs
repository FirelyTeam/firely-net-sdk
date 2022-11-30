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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// A task to be performed
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Task", IsResource=true)]
    [DataContract]
    public partial class Task : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ITask, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Task; } }
        [NotMapped]
        public override string TypeName { get { return "Task"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "RestrictionComponent")]
        [DataContract]
        public partial class RestrictionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITaskRestrictionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RestrictionComponent"; } }
            
            /// <summary>
            /// How many times to repeat
            /// </summary>
            [FhirElement("repetitions", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt RepetitionsElement
            {
                get { return _RepetitionsElement; }
                set { _RepetitionsElement = value; OnPropertyChanged("RepetitionsElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _RepetitionsElement;
            
            /// <summary>
            /// How many times to repeat
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Repetitions
            {
                get { return RepetitionsElement != null ? RepetitionsElement.Value : null; }
                set
                {
                    if (value == null)
                        RepetitionsElement = null;
                    else
                        RepetitionsElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Repetitions");
                }
            }
            
            /// <summary>
            /// When fulfillment sought
            /// </summary>
            [FhirElement("period", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
            
            /// <summary>
            /// For whom is fulfillment sought?
            /// </summary>
            [FhirElement("recipient", Order=60)]
            [CLSCompliant(false)]
            [References("Patient","Practitioner","PractitionerRole","RelatedPerson","Group","Organization")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Recipient
            {
                get { if(_Recipient==null) _Recipient = new List<Hl7.Fhir.Model.ResourceReference>(); return _Recipient; }
                set { _Recipient = value; OnPropertyChanged("Recipient"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Recipient;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RestrictionComponent");
                base.Serialize(sink);
                sink.Element("repetitions", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RepetitionsElement?.Serialize(sink);
                sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Period?.Serialize(sink);
                sink.BeginList("recipient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Recipient)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "repetitions":
                        RepetitionsElement = source.PopulateValue(RepetitionsElement);
                        return true;
                    case "_repetitions":
                        RepetitionsElement = source.Populate(RepetitionsElement);
                        return true;
                    case "period":
                        Period = source.Populate(Period);
                        return true;
                    case "recipient":
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
                    case "recipient":
                        source.PopulateListItem(Recipient, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RestrictionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RepetitionsElement != null) dest.RepetitionsElement = (Hl7.Fhir.Model.PositiveInt)RepetitionsElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Recipient != null) dest.Recipient = new List<Hl7.Fhir.Model.ResourceReference>(Recipient.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RestrictionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RestrictionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RepetitionsElement, otherT.RepetitionsElement)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                if( !DeepComparable.Matches(Recipient, otherT.Recipient)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RestrictionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RepetitionsElement, otherT.RepetitionsElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                if( !DeepComparable.IsExactly(Recipient, otherT.Recipient)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RepetitionsElement != null) yield return RepetitionsElement;
                    if (Period != null) yield return Period;
                    foreach (var elem in Recipient) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RepetitionsElement != null) yield return new ElementValue("repetitions", RepetitionsElement);
                    if (Period != null) yield return new ElementValue("period", Period);
                    foreach (var elem in Recipient) { if (elem != null) yield return new ElementValue("recipient", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ParameterComponent")]
        [DataContract]
        public partial class ParameterComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITaskParameterComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParameterComponent"; } }
            
            /// <summary>
            /// Label for the input
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Content to use in performing the task
            /// </summary>
            [FhirElement("value", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Canonical),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Url),typeof(Hl7.Fhir.Model.Uuid),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.R4.Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.R4.ContactPoint),typeof(Hl7.Fhir.Model.R4.Count),typeof(Hl7.Fhir.Model.R4.Distance),typeof(Hl7.Fhir.Model.R4.Duration),typeof(Hl7.Fhir.Model.R4.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.R4.Money),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.R4.SampledData),typeof(Hl7.Fhir.Model.R4.Signature),typeof(Hl7.Fhir.Model.R4.Timing),typeof(Hl7.Fhir.Model.R4.ContactDetail),typeof(Hl7.Fhir.Model.R4.Contributor),typeof(Hl7.Fhir.Model.R4.DataRequirement),typeof(Hl7.Fhir.Model.Expression),typeof(Hl7.Fhir.Model.R4.ParameterDefinition),typeof(Hl7.Fhir.Model.R4.RelatedArtifact),typeof(Hl7.Fhir.Model.R4.TriggerDefinition),typeof(Hl7.Fhir.Model.UsageContext),typeof(Hl7.Fhir.Model.R4.Dosage),typeof(Hl7.Fhir.Model.Meta))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParameterComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Value?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "valueBase64Binary":
                        source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Base64Binary);
                        return true;
                    case "_valueBase64Binary":
                        source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Base64Binary);
                        return true;
                    case "valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "_valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "valueCanonical":
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Canonical);
                        return true;
                    case "_valueCanonical":
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Canonical);
                        return true;
                    case "valueCode":
                        source.CheckDuplicates<Hl7.Fhir.Model.Code>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Code);
                        return true;
                    case "_valueCode":
                        source.CheckDuplicates<Hl7.Fhir.Model.Code>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Code);
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
                    case "valueDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirDecimal);
                        return true;
                    case "_valueDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirDecimal);
                        return true;
                    case "valueId":
                        source.CheckDuplicates<Hl7.Fhir.Model.Id>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Id);
                        return true;
                    case "_valueId":
                        source.CheckDuplicates<Hl7.Fhir.Model.Id>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Id);
                        return true;
                    case "valueInstant":
                        source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Instant);
                        return true;
                    case "_valueInstant":
                        source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Instant);
                        return true;
                    case "valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Integer);
                        return true;
                    case "_valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Integer);
                        return true;
                    case "valueMarkdown":
                        source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Markdown);
                        return true;
                    case "_valueMarkdown":
                        source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Markdown);
                        return true;
                    case "valueOid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Oid);
                        return true;
                    case "_valueOid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Oid);
                        return true;
                    case "valuePositiveInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.PositiveInt);
                        return true;
                    case "_valuePositiveInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.PositiveInt);
                        return true;
                    case "valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Time);
                        return true;
                    case "_valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Time);
                        return true;
                    case "valueUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "_valueUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "valueUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "_valueUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "valueUrl":
                        source.CheckDuplicates<Hl7.Fhir.Model.Url>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Url);
                        return true;
                    case "_valueUrl":
                        source.CheckDuplicates<Hl7.Fhir.Model.Url>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Url);
                        return true;
                    case "valueUuid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Uuid>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Uuid);
                        return true;
                    case "_valueUuid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Uuid>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Uuid);
                        return true;
                    case "valueAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Address);
                        return true;
                    case "valueAge":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Age>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Age);
                        return true;
                    case "valueAnnotation":
                        source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Annotation);
                        return true;
                    case "valueAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Attachment);
                        return true;
                    case "valueCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "valueCoding":
                        source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Coding);
                        return true;
                    case "valueContactPoint":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.ContactPoint>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.ContactPoint);
                        return true;
                    case "valueCount":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Count>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Count);
                        return true;
                    case "valueDistance":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Distance>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Distance);
                        return true;
                    case "valueDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Duration>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Duration);
                        return true;
                    case "valueHumanName":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.HumanName>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.HumanName);
                        return true;
                    case "valueIdentifier":
                        source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Identifier);
                        return true;
                    case "valueMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Money>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Money);
                        return true;
                    case "valuePeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Period);
                        return true;
                    case "valueQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Quantity);
                        return true;
                    case "valueRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Range);
                        return true;
                    case "valueRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Ratio);
                        return true;
                    case "valueReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "valueSampledData":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.SampledData>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.SampledData);
                        return true;
                    case "valueSignature":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Signature>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Signature);
                        return true;
                    case "valueTiming":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Timing>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Timing);
                        return true;
                    case "valueContactDetail":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.ContactDetail>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.ContactDetail);
                        return true;
                    case "valueContributor":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Contributor>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Contributor);
                        return true;
                    case "valueDataRequirement":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.DataRequirement>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.DataRequirement);
                        return true;
                    case "valueExpression":
                        source.CheckDuplicates<Hl7.Fhir.Model.Expression>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Expression);
                        return true;
                    case "valueParameterDefinition":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.ParameterDefinition>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.ParameterDefinition);
                        return true;
                    case "valueRelatedArtifact":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.RelatedArtifact>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.RelatedArtifact);
                        return true;
                    case "valueTriggerDefinition":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.TriggerDefinition>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.TriggerDefinition);
                        return true;
                    case "valueUsageContext":
                        source.CheckDuplicates<Hl7.Fhir.Model.UsageContext>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.UsageContext);
                        return true;
                    case "valueDosage":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Dosage>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Dosage);
                        return true;
                    case "valueMeta":
                        source.CheckDuplicates<Hl7.Fhir.Model.Meta>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Meta);
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
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
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
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Value != null) yield return Value;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "OutputComponent")]
        [DataContract]
        public partial class OutputComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ITaskOutputComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OutputComponent"; } }
            
            /// <summary>
            /// Label for output
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Result of output
            /// </summary>
            [FhirElement("value", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Canonical),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Url),typeof(Hl7.Fhir.Model.Uuid),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.R4.Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.R4.ContactPoint),typeof(Hl7.Fhir.Model.R4.Count),typeof(Hl7.Fhir.Model.R4.Distance),typeof(Hl7.Fhir.Model.R4.Duration),typeof(Hl7.Fhir.Model.R4.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.R4.Money),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.R4.SampledData),typeof(Hl7.Fhir.Model.R4.Signature),typeof(Hl7.Fhir.Model.R4.Timing),typeof(Hl7.Fhir.Model.R4.ContactDetail),typeof(Hl7.Fhir.Model.R4.Contributor),typeof(Hl7.Fhir.Model.R4.DataRequirement),typeof(Hl7.Fhir.Model.Expression),typeof(Hl7.Fhir.Model.R4.ParameterDefinition),typeof(Hl7.Fhir.Model.R4.RelatedArtifact),typeof(Hl7.Fhir.Model.R4.TriggerDefinition),typeof(Hl7.Fhir.Model.UsageContext),typeof(Hl7.Fhir.Model.R4.Dosage),typeof(Hl7.Fhir.Model.Meta))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("OutputComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Value?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "valueBase64Binary":
                        source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Base64Binary);
                        return true;
                    case "_valueBase64Binary":
                        source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Base64Binary);
                        return true;
                    case "valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "_valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "valueCanonical":
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Canonical);
                        return true;
                    case "_valueCanonical":
                        source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Canonical);
                        return true;
                    case "valueCode":
                        source.CheckDuplicates<Hl7.Fhir.Model.Code>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Code);
                        return true;
                    case "_valueCode":
                        source.CheckDuplicates<Hl7.Fhir.Model.Code>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Code);
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
                    case "valueDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirDecimal);
                        return true;
                    case "_valueDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirDecimal);
                        return true;
                    case "valueId":
                        source.CheckDuplicates<Hl7.Fhir.Model.Id>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Id);
                        return true;
                    case "_valueId":
                        source.CheckDuplicates<Hl7.Fhir.Model.Id>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Id);
                        return true;
                    case "valueInstant":
                        source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Instant);
                        return true;
                    case "_valueInstant":
                        source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Instant);
                        return true;
                    case "valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Integer);
                        return true;
                    case "_valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Integer);
                        return true;
                    case "valueMarkdown":
                        source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Markdown);
                        return true;
                    case "_valueMarkdown":
                        source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Markdown);
                        return true;
                    case "valueOid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Oid);
                        return true;
                    case "_valueOid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Oid);
                        return true;
                    case "valuePositiveInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.PositiveInt);
                        return true;
                    case "_valuePositiveInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.PositiveInt);
                        return true;
                    case "valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Time);
                        return true;
                    case "_valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Time);
                        return true;
                    case "valueUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "_valueUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.UnsignedInt);
                        return true;
                    case "valueUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "_valueUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "valueUrl":
                        source.CheckDuplicates<Hl7.Fhir.Model.Url>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Url);
                        return true;
                    case "_valueUrl":
                        source.CheckDuplicates<Hl7.Fhir.Model.Url>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Url);
                        return true;
                    case "valueUuid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Uuid>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Uuid);
                        return true;
                    case "_valueUuid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Uuid>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Uuid);
                        return true;
                    case "valueAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Address);
                        return true;
                    case "valueAge":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Age>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Age);
                        return true;
                    case "valueAnnotation":
                        source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Annotation);
                        return true;
                    case "valueAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Attachment);
                        return true;
                    case "valueCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "valueCoding":
                        source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Coding);
                        return true;
                    case "valueContactPoint":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.ContactPoint>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.ContactPoint);
                        return true;
                    case "valueCount":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Count>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Count);
                        return true;
                    case "valueDistance":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Distance>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Distance);
                        return true;
                    case "valueDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Duration>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Duration);
                        return true;
                    case "valueHumanName":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.HumanName>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.HumanName);
                        return true;
                    case "valueIdentifier":
                        source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Identifier);
                        return true;
                    case "valueMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Money>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Money);
                        return true;
                    case "valuePeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Period);
                        return true;
                    case "valueQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Quantity);
                        return true;
                    case "valueRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Range);
                        return true;
                    case "valueRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Ratio);
                        return true;
                    case "valueReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "valueSampledData":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.SampledData>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.SampledData);
                        return true;
                    case "valueSignature":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Signature>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Signature);
                        return true;
                    case "valueTiming":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Timing>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Timing);
                        return true;
                    case "valueContactDetail":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.ContactDetail>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.ContactDetail);
                        return true;
                    case "valueContributor":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Contributor>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Contributor);
                        return true;
                    case "valueDataRequirement":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.DataRequirement>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.DataRequirement);
                        return true;
                    case "valueExpression":
                        source.CheckDuplicates<Hl7.Fhir.Model.Expression>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Expression);
                        return true;
                    case "valueParameterDefinition":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.ParameterDefinition>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.ParameterDefinition);
                        return true;
                    case "valueRelatedArtifact":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.RelatedArtifact>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.RelatedArtifact);
                        return true;
                    case "valueTriggerDefinition":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.TriggerDefinition>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.TriggerDefinition);
                        return true;
                    case "valueUsageContext":
                        source.CheckDuplicates<Hl7.Fhir.Model.UsageContext>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.UsageContext);
                        return true;
                    case "valueDosage":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Dosage>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Dosage);
                        return true;
                    case "valueMeta":
                        source.CheckDuplicates<Hl7.Fhir.Model.Meta>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Meta);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OutputComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new OutputComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OutputComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OutputComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Value != null) yield return Value;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }
        
        
        }
        
        [NotMapped]
        Hl7.Fhir.Model.ITaskRestrictionComponent Hl7.Fhir.Model.ITask.Restriction { get { return Restriction; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ITaskParameterComponent> Hl7.Fhir.Model.ITask.Input { get { return Input; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ITaskOutputComponent> Hl7.Fhir.Model.ITask.Output { get { return Output; } }
    
        
        /// <summary>
        /// Task Instance Identifier
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Formal definition of task
        /// </summary>
        [FhirElement("instantiatesCanonical", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Canonical InstantiatesCanonicalElement
        {
            get { return _InstantiatesCanonicalElement; }
            set { _InstantiatesCanonicalElement = value; OnPropertyChanged("InstantiatesCanonicalElement"); }
        }
        
        private Hl7.Fhir.Model.Canonical _InstantiatesCanonicalElement;
        
        /// <summary>
        /// Formal definition of task
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string InstantiatesCanonical
        {
            get { return InstantiatesCanonicalElement != null ? InstantiatesCanonicalElement.Value : null; }
            set
            {
                if (value == null)
                    InstantiatesCanonicalElement = null;
                else
                    InstantiatesCanonicalElement = new Hl7.Fhir.Model.Canonical(value);
                OnPropertyChanged("InstantiatesCanonical");
            }
        }
        
        /// <summary>
        /// Formal definition of task
        /// </summary>
        [FhirElement("instantiatesUri", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri InstantiatesUriElement
        {
            get { return _InstantiatesUriElement; }
            set { _InstantiatesUriElement = value; OnPropertyChanged("InstantiatesUriElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _InstantiatesUriElement;
        
        /// <summary>
        /// Formal definition of task
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string InstantiatesUri
        {
            get { return InstantiatesUriElement != null ? InstantiatesUriElement.Value : null; }
            set
            {
                if (value == null)
                    InstantiatesUriElement = null;
                else
                    InstantiatesUriElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("InstantiatesUri");
            }
        }
        
        /// <summary>
        /// Request fulfilled by this task
        /// </summary>
        [FhirElement("basedOn", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Requisition or grouper id
        /// </summary>
        [FhirElement("groupIdentifier", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier GroupIdentifier
        {
            get { return _GroupIdentifier; }
            set { _GroupIdentifier = value; OnPropertyChanged("GroupIdentifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _GroupIdentifier;
        
        /// <summary>
        /// Composite task
        /// </summary>
        [FhirElement("partOf", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Task")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PartOf
        {
            get { if(_PartOf==null) _PartOf = new List<Hl7.Fhir.Model.ResourceReference>(); return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PartOf;
        
        /// <summary>
        /// draft | requested | received | accepted | +
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.TaskStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.TaskStatus> _StatusElement;
        
        /// <summary>
        /// draft | requested | received | accepted | +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.TaskStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.TaskStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reason for current status
        /// </summary>
        [FhirElement("statusReason", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept StatusReason
        {
            get { return _StatusReason; }
            set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _StatusReason;
        
        /// <summary>
        /// E.g. "Specimen collected", "IV prepped"
        /// </summary>
        [FhirElement("businessStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept BusinessStatus
        {
            get { return _BusinessStatus; }
            set { _BusinessStatus = value; OnPropertyChanged("BusinessStatus"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _BusinessStatus;
        
        /// <summary>
        /// unknown | proposal | plan | order | original-order | reflex-order | filler-order | instance-order | option
        /// </summary>
        [FhirElement("intent", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.TaskIntent> IntentElement
        {
            get { return _IntentElement; }
            set { _IntentElement = value; OnPropertyChanged("IntentElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.TaskIntent> _IntentElement;
        
        /// <summary>
        /// unknown | proposal | plan | order | original-order | reflex-order | filler-order | instance-order | option
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.TaskIntent? Intent
        {
            get { return IntentElement != null ? IntentElement.Value : null; }
            set
            {
                if (value == null)
                    IntentElement = null;
                else
                    IntentElement = new Code<Hl7.Fhir.Model.R4.TaskIntent>(value);
                OnPropertyChanged("Intent");
            }
        }
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        [FhirElement("priority", Order=190)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RequestPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RequestPriority> _PriorityElement;
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.RequestPriority? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if (value == null)
                    PriorityElement = null;
                else
                    PriorityElement = new Code<Hl7.Fhir.Model.RequestPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// Task Type
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// Human-readable explanation of task
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Human-readable explanation of task
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if (value == null)
                    DescriptionElement = null;
                else
                    DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// What task is acting on
        /// </summary>
        [FhirElement("focus", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Focus
        {
            get { return _Focus; }
            set { _Focus = value; OnPropertyChanged("Focus"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Focus;
        
        /// <summary>
        /// Beneficiary of the Task
        /// </summary>
        [FhirElement("for", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference For
        {
            get { return _For; }
            set { _For = value; OnPropertyChanged("For"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _For;
        
        /// <summary>
        /// Healthcare event during which this task originated
        /// </summary>
        [FhirElement("encounter", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Start and end time of execution
        /// </summary>
        [FhirElement("executionPeriod", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period ExecutionPeriod
        {
            get { return _ExecutionPeriod; }
            set { _ExecutionPeriod = value; OnPropertyChanged("ExecutionPeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _ExecutionPeriod;
        
        /// <summary>
        /// Task Creation Date
        /// </summary>
        [FhirElement("authoredOn", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AuthoredOnElement
        {
            get { return _AuthoredOnElement; }
            set { _AuthoredOnElement = value; OnPropertyChanged("AuthoredOnElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _AuthoredOnElement;
        
        /// <summary>
        /// Task Creation Date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AuthoredOn
        {
            get { return AuthoredOnElement != null ? AuthoredOnElement.Value : null; }
            set
            {
                if (value == null)
                    AuthoredOnElement = null;
                else
                    AuthoredOnElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("AuthoredOn");
            }
        }
        
        /// <summary>
        /// Task Last Modified Date
        /// </summary>
        [FhirElement("lastModified", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime LastModifiedElement
        {
            get { return _LastModifiedElement; }
            set { _LastModifiedElement = value; OnPropertyChanged("LastModifiedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _LastModifiedElement;
        
        /// <summary>
        /// Task Last Modified Date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LastModified
        {
            get { return LastModifiedElement != null ? LastModifiedElement.Value : null; }
            set
            {
                if (value == null)
                    LastModifiedElement = null;
                else
                    LastModifiedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("LastModified");
            }
        }
        
        /// <summary>
        /// Who is asking for task to be done
        /// </summary>
        [FhirElement("requester", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [References("Device","Organization","Patient","Practitioner","PractitionerRole","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Requester
        {
            get { return _Requester; }
            set { _Requester = value; OnPropertyChanged("Requester"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Requester;
        
        /// <summary>
        /// Requested performer
        /// </summary>
        [FhirElement("performerType", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> PerformerType
        {
            get { if(_PerformerType==null) _PerformerType = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PerformerType; }
            set { _PerformerType = value; OnPropertyChanged("PerformerType"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _PerformerType;
        
        /// <summary>
        /// Responsible individual
        /// </summary>
        [FhirElement("owner", InSummary=Hl7.Fhir.Model.Version.All, Order=300)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole","Organization","CareTeam","HealthcareService","Patient","Device","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Owner
        {
            get { return _Owner; }
            set { _Owner = value; OnPropertyChanged("Owner"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Owner;
        
        /// <summary>
        /// Where task occurs
        /// </summary>
        [FhirElement("location", InSummary=Hl7.Fhir.Model.Version.All, Order=310)]
        [CLSCompliant(false)]
        [References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Location;
        
        /// <summary>
        /// Why task is needed
        /// </summary>
        [FhirElement("reasonCode", Order=320)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ReasonCode
        {
            get { return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ReasonCode;
        
        /// <summary>
        /// Why task is needed
        /// </summary>
        [FhirElement("reasonReference", Order=330)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ReasonReference
        {
            get { return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ReasonReference;
        
        /// <summary>
        /// Associated insurance coverage
        /// </summary>
        [FhirElement("insurance", Order=340)]
        [CLSCompliant(false)]
        [References("Coverage","ClaimResponse")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Insurance
        {
            get { if(_Insurance==null) _Insurance = new List<Hl7.Fhir.Model.ResourceReference>(); return _Insurance; }
            set { _Insurance = value; OnPropertyChanged("Insurance"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Insurance;
        
        /// <summary>
        /// Comments made about the task
        /// </summary>
        [FhirElement("note", Order=350)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Key events in history of the Task
        /// </summary>
        [FhirElement("relevantHistory", Order=360)]
        [CLSCompliant(false)]
        [References("Provenance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> RelevantHistory
        {
            get { if(_RelevantHistory==null) _RelevantHistory = new List<Hl7.Fhir.Model.ResourceReference>(); return _RelevantHistory; }
            set { _RelevantHistory = value; OnPropertyChanged("RelevantHistory"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _RelevantHistory;
        
        /// <summary>
        /// Constraints on fulfillment tasks
        /// </summary>
        [FhirElement("restriction", Order=370)]
        [DataMember]
        public RestrictionComponent Restriction
        {
            get { return _Restriction; }
            set { _Restriction = value; OnPropertyChanged("Restriction"); }
        }
        
        private RestrictionComponent _Restriction;
        
        /// <summary>
        /// Information used to perform task
        /// </summary>
        [FhirElement("input", Order=380)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ParameterComponent> Input
        {
            get { if(_Input==null) _Input = new List<ParameterComponent>(); return _Input; }
            set { _Input = value; OnPropertyChanged("Input"); }
        }
        
        private List<ParameterComponent> _Input;
        
        /// <summary>
        /// Information produced as part of task
        /// </summary>
        [FhirElement("output", Order=390)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<OutputComponent> Output
        {
            get { if(_Output==null) _Output = new List<OutputComponent>(); return _Output; }
            set { _Output = value; OnPropertyChanged("Output"); }
        }
        
        private List<OutputComponent> _Output;
    
    
        public static ElementDefinitionConstraint[] Task_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "inv-1",
                severity: ConstraintSeverity.Warning,
                expression: "lastModified.exists().not() or authoredOn.exists().not() or lastModified >= authoredOn",
                human: "Last modified date must be greater than or equal to authored-on date.",
                xpath: "not(exists(f:lastModified/@value)) or not(exists(f:authoredOn/@value)) or f:lastModified/@value >= f:authoredOn/@value"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Task_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Task;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(InstantiatesCanonicalElement != null) dest.InstantiatesCanonicalElement = (Hl7.Fhir.Model.Canonical)InstantiatesCanonicalElement.DeepCopy();
                if(InstantiatesUriElement != null) dest.InstantiatesUriElement = (Hl7.Fhir.Model.FhirUri)InstantiatesUriElement.DeepCopy();
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(GroupIdentifier != null) dest.GroupIdentifier = (Hl7.Fhir.Model.Identifier)GroupIdentifier.DeepCopy();
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.TaskStatus>)StatusElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = (Hl7.Fhir.Model.CodeableConcept)StatusReason.DeepCopy();
                if(BusinessStatus != null) dest.BusinessStatus = (Hl7.Fhir.Model.CodeableConcept)BusinessStatus.DeepCopy();
                if(IntentElement != null) dest.IntentElement = (Code<Hl7.Fhir.Model.R4.TaskIntent>)IntentElement.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.RequestPriority>)PriorityElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Focus != null) dest.Focus = (Hl7.Fhir.Model.ResourceReference)Focus.DeepCopy();
                if(For != null) dest.For = (Hl7.Fhir.Model.ResourceReference)For.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(ExecutionPeriod != null) dest.ExecutionPeriod = (Hl7.Fhir.Model.Period)ExecutionPeriod.DeepCopy();
                if(AuthoredOnElement != null) dest.AuthoredOnElement = (Hl7.Fhir.Model.FhirDateTime)AuthoredOnElement.DeepCopy();
                if(LastModifiedElement != null) dest.LastModifiedElement = (Hl7.Fhir.Model.FhirDateTime)LastModifiedElement.DeepCopy();
                if(Requester != null) dest.Requester = (Hl7.Fhir.Model.ResourceReference)Requester.DeepCopy();
                if(PerformerType != null) dest.PerformerType = new List<Hl7.Fhir.Model.CodeableConcept>(PerformerType.DeepCopy());
                if(Owner != null) dest.Owner = (Hl7.Fhir.Model.ResourceReference)Owner.DeepCopy();
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = (Hl7.Fhir.Model.CodeableConcept)ReasonCode.DeepCopy();
                if(ReasonReference != null) dest.ReasonReference = (Hl7.Fhir.Model.ResourceReference)ReasonReference.DeepCopy();
                if(Insurance != null) dest.Insurance = new List<Hl7.Fhir.Model.ResourceReference>(Insurance.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(RelevantHistory != null) dest.RelevantHistory = new List<Hl7.Fhir.Model.ResourceReference>(RelevantHistory.DeepCopy());
                if(Restriction != null) dest.Restriction = (RestrictionComponent)Restriction.DeepCopy();
                if(Input != null) dest.Input = new List<ParameterComponent>(Input.DeepCopy());
                if(Output != null) dest.Output = new List<OutputComponent>(Output.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Task());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Task;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(InstantiatesCanonicalElement, otherT.InstantiatesCanonicalElement)) return false;
            if( !DeepComparable.Matches(InstantiatesUriElement, otherT.InstantiatesUriElement)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(GroupIdentifier, otherT.GroupIdentifier)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.Matches(BusinessStatus, otherT.BusinessStatus)) return false;
            if( !DeepComparable.Matches(IntentElement, otherT.IntentElement)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Focus, otherT.Focus)) return false;
            if( !DeepComparable.Matches(For, otherT.For)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(ExecutionPeriod, otherT.ExecutionPeriod)) return false;
            if( !DeepComparable.Matches(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.Matches(LastModifiedElement, otherT.LastModifiedElement)) return false;
            if( !DeepComparable.Matches(Requester, otherT.Requester)) return false;
            if( !DeepComparable.Matches(PerformerType, otherT.PerformerType)) return false;
            if( !DeepComparable.Matches(Owner, otherT.Owner)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(RelevantHistory, otherT.RelevantHistory)) return false;
            if( !DeepComparable.Matches(Restriction, otherT.Restriction)) return false;
            if( !DeepComparable.Matches(Input, otherT.Input)) return false;
            if( !DeepComparable.Matches(Output, otherT.Output)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Task;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(InstantiatesCanonicalElement, otherT.InstantiatesCanonicalElement)) return false;
            if( !DeepComparable.IsExactly(InstantiatesUriElement, otherT.InstantiatesUriElement)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(GroupIdentifier, otherT.GroupIdentifier)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.IsExactly(BusinessStatus, otherT.BusinessStatus)) return false;
            if( !DeepComparable.IsExactly(IntentElement, otherT.IntentElement)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Focus, otherT.Focus)) return false;
            if( !DeepComparable.IsExactly(For, otherT.For)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(ExecutionPeriod, otherT.ExecutionPeriod)) return false;
            if( !DeepComparable.IsExactly(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.IsExactly(LastModifiedElement, otherT.LastModifiedElement)) return false;
            if( !DeepComparable.IsExactly(Requester, otherT.Requester)) return false;
            if( !DeepComparable.IsExactly(PerformerType, otherT.PerformerType)) return false;
            if( !DeepComparable.IsExactly(Owner, otherT.Owner)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(RelevantHistory, otherT.RelevantHistory)) return false;
            if( !DeepComparable.IsExactly(Restriction, otherT.Restriction)) return false;
            if( !DeepComparable.IsExactly(Input, otherT.Input)) return false;
            if( !DeepComparable.IsExactly(Output, otherT.Output)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Task");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("instantiatesCanonical", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); InstantiatesCanonicalElement?.Serialize(sink);
            sink.Element("instantiatesUri", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); InstantiatesUriElement?.Serialize(sink);
            sink.BeginList("basedOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in BasedOn)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("groupIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); GroupIdentifier?.Serialize(sink);
            sink.BeginList("partOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in PartOf)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("statusReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusReason?.Serialize(sink);
            sink.Element("businessStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); BusinessStatus?.Serialize(sink);
            sink.Element("intent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); IntentElement?.Serialize(sink);
            sink.Element("priority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PriorityElement?.Serialize(sink);
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Code?.Serialize(sink);
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
            sink.Element("focus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Focus?.Serialize(sink);
            sink.Element("for", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); For?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Encounter?.Serialize(sink);
            sink.Element("executionPeriod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExecutionPeriod?.Serialize(sink);
            sink.Element("authoredOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AuthoredOnElement?.Serialize(sink);
            sink.Element("lastModified", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LastModifiedElement?.Serialize(sink);
            sink.Element("requester", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Requester?.Serialize(sink);
            sink.BeginList("performerType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in PerformerType)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("owner", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Owner?.Serialize(sink);
            sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Location?.Serialize(sink);
            sink.Element("reasonCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ReasonCode?.Serialize(sink);
            sink.Element("reasonReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ReasonReference?.Serialize(sink);
            sink.BeginList("insurance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Insurance)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("relevantHistory", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in RelevantHistory)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("restriction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Restriction?.Serialize(sink);
            sink.BeginList("input", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Input)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("output", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Output)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "instantiatesCanonical":
                    InstantiatesCanonicalElement = source.PopulateValue(InstantiatesCanonicalElement);
                    return true;
                case "_instantiatesCanonical":
                    InstantiatesCanonicalElement = source.Populate(InstantiatesCanonicalElement);
                    return true;
                case "instantiatesUri":
                    InstantiatesUriElement = source.PopulateValue(InstantiatesUriElement);
                    return true;
                case "_instantiatesUri":
                    InstantiatesUriElement = source.Populate(InstantiatesUriElement);
                    return true;
                case "basedOn":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "groupIdentifier":
                    GroupIdentifier = source.Populate(GroupIdentifier);
                    return true;
                case "partOf":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "statusReason":
                    StatusReason = source.Populate(StatusReason);
                    return true;
                case "businessStatus":
                    BusinessStatus = source.Populate(BusinessStatus);
                    return true;
                case "intent":
                    IntentElement = source.PopulateValue(IntentElement);
                    return true;
                case "_intent":
                    IntentElement = source.Populate(IntentElement);
                    return true;
                case "priority":
                    PriorityElement = source.PopulateValue(PriorityElement);
                    return true;
                case "_priority":
                    PriorityElement = source.Populate(PriorityElement);
                    return true;
                case "code":
                    Code = source.Populate(Code);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "focus":
                    Focus = source.Populate(Focus);
                    return true;
                case "for":
                    For = source.Populate(For);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "executionPeriod":
                    ExecutionPeriod = source.Populate(ExecutionPeriod);
                    return true;
                case "authoredOn":
                    AuthoredOnElement = source.PopulateValue(AuthoredOnElement);
                    return true;
                case "_authoredOn":
                    AuthoredOnElement = source.Populate(AuthoredOnElement);
                    return true;
                case "lastModified":
                    LastModifiedElement = source.PopulateValue(LastModifiedElement);
                    return true;
                case "_lastModified":
                    LastModifiedElement = source.Populate(LastModifiedElement);
                    return true;
                case "requester":
                    Requester = source.Populate(Requester);
                    return true;
                case "performerType":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "owner":
                    Owner = source.Populate(Owner);
                    return true;
                case "location":
                    Location = source.Populate(Location);
                    return true;
                case "reasonCode":
                    ReasonCode = source.Populate(ReasonCode);
                    return true;
                case "reasonReference":
                    ReasonReference = source.Populate(ReasonReference);
                    return true;
                case "insurance":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "relevantHistory":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "restriction":
                    Restriction = source.Populate(Restriction);
                    return true;
                case "input":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "output":
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
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "basedOn":
                    source.PopulateListItem(BasedOn, index);
                    return true;
                case "partOf":
                    source.PopulateListItem(PartOf, index);
                    return true;
                case "performerType":
                    source.PopulateListItem(PerformerType, index);
                    return true;
                case "insurance":
                    source.PopulateListItem(Insurance, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "relevantHistory":
                    source.PopulateListItem(RelevantHistory, index);
                    return true;
                case "input":
                    source.PopulateListItem(Input, index);
                    return true;
                case "output":
                    source.PopulateListItem(Output, index);
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
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (InstantiatesCanonicalElement != null) yield return InstantiatesCanonicalElement;
                if (InstantiatesUriElement != null) yield return InstantiatesUriElement;
                foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
                if (GroupIdentifier != null) yield return GroupIdentifier;
                foreach (var elem in PartOf) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                if (StatusReason != null) yield return StatusReason;
                if (BusinessStatus != null) yield return BusinessStatus;
                if (IntentElement != null) yield return IntentElement;
                if (PriorityElement != null) yield return PriorityElement;
                if (Code != null) yield return Code;
                if (DescriptionElement != null) yield return DescriptionElement;
                if (Focus != null) yield return Focus;
                if (For != null) yield return For;
                if (Encounter != null) yield return Encounter;
                if (ExecutionPeriod != null) yield return ExecutionPeriod;
                if (AuthoredOnElement != null) yield return AuthoredOnElement;
                if (LastModifiedElement != null) yield return LastModifiedElement;
                if (Requester != null) yield return Requester;
                foreach (var elem in PerformerType) { if (elem != null) yield return elem; }
                if (Owner != null) yield return Owner;
                if (Location != null) yield return Location;
                if (ReasonCode != null) yield return ReasonCode;
                if (ReasonReference != null) yield return ReasonReference;
                foreach (var elem in Insurance) { if (elem != null) yield return elem; }
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in RelevantHistory) { if (elem != null) yield return elem; }
                if (Restriction != null) yield return Restriction;
                foreach (var elem in Input) { if (elem != null) yield return elem; }
                foreach (var elem in Output) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (InstantiatesCanonicalElement != null) yield return new ElementValue("instantiatesCanonical", InstantiatesCanonicalElement);
                if (InstantiatesUriElement != null) yield return new ElementValue("instantiatesUri", InstantiatesUriElement);
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                if (GroupIdentifier != null) yield return new ElementValue("groupIdentifier", GroupIdentifier);
                foreach (var elem in PartOf) { if (elem != null) yield return new ElementValue("partOf", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (StatusReason != null) yield return new ElementValue("statusReason", StatusReason);
                if (BusinessStatus != null) yield return new ElementValue("businessStatus", BusinessStatus);
                if (IntentElement != null) yield return new ElementValue("intent", IntentElement);
                if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
                if (Code != null) yield return new ElementValue("code", Code);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (Focus != null) yield return new ElementValue("focus", Focus);
                if (For != null) yield return new ElementValue("for", For);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (ExecutionPeriod != null) yield return new ElementValue("executionPeriod", ExecutionPeriod);
                if (AuthoredOnElement != null) yield return new ElementValue("authoredOn", AuthoredOnElement);
                if (LastModifiedElement != null) yield return new ElementValue("lastModified", LastModifiedElement);
                if (Requester != null) yield return new ElementValue("requester", Requester);
                foreach (var elem in PerformerType) { if (elem != null) yield return new ElementValue("performerType", elem); }
                if (Owner != null) yield return new ElementValue("owner", Owner);
                if (Location != null) yield return new ElementValue("location", Location);
                if (ReasonCode != null) yield return new ElementValue("reasonCode", ReasonCode);
                if (ReasonReference != null) yield return new ElementValue("reasonReference", ReasonReference);
                foreach (var elem in Insurance) { if (elem != null) yield return new ElementValue("insurance", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in RelevantHistory) { if (elem != null) yield return new ElementValue("relevantHistory", elem); }
                if (Restriction != null) yield return new ElementValue("restriction", Restriction);
                foreach (var elem in Input) { if (elem != null) yield return new ElementValue("input", elem); }
                foreach (var elem in Output) { if (elem != null) yield return new ElementValue("output", elem); }
            }
        }
    
    }

}
