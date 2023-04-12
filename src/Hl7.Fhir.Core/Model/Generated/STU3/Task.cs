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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// A task to be performed
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "Task", IsResource=true)]
    [DataContract]
    public partial class Task : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ITask, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Task; } }
        [NotMapped]
        public override string TypeName { get { return "Task"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RequesterComponent")]
        [DataContract]
        public partial class RequesterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RequesterComponent"; } }
            
            /// <summary>
            /// Individual asking for task
            /// </summary>
            [FhirElement("agent", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [References("Device","Organization","Patient","Practitioner","RelatedPerson")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Agent
            {
                get { return _Agent; }
                set { _Agent = value; OnPropertyChanged("Agent"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Agent;
            
            /// <summary>
            /// Organization individual is acting for
            /// </summary>
            [FhirElement("onBehalfOf", Order=50)]
            [CLSCompliant(false)]
            [References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference OnBehalfOf
            {
                get { return _OnBehalfOf; }
                set { _OnBehalfOf = value; OnPropertyChanged("OnBehalfOf"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _OnBehalfOf;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RequesterComponent");
                base.Serialize(sink);
                sink.Element("agent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Agent?.Serialize(sink);
                sink.Element("onBehalfOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OnBehalfOf?.Serialize(sink);
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
                    case "agent":
                        Agent = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "onBehalfOf":
                        OnBehalfOf = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "agent":
                        Agent = source.Populate(Agent);
                        return true;
                    case "onBehalfOf":
                        OnBehalfOf = source.Populate(OnBehalfOf);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RequesterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Agent != null) dest.Agent = (Hl7.Fhir.Model.ResourceReference)Agent.DeepCopy();
                    if(OnBehalfOf != null) dest.OnBehalfOf = (Hl7.Fhir.Model.ResourceReference)OnBehalfOf.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RequesterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RequesterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Agent, otherT.Agent)) return false;
                if( !DeepComparable.Matches(OnBehalfOf, otherT.OnBehalfOf)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RequesterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Agent, otherT.Agent)) return false;
                if( !DeepComparable.IsExactly(OnBehalfOf, otherT.OnBehalfOf)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Agent != null) yield return Agent;
                    if (OnBehalfOf != null) yield return OnBehalfOf;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Agent != null) yield return new ElementValue("agent", Agent);
                    if (OnBehalfOf != null) yield return new ElementValue("onBehalfOf", OnBehalfOf);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RestrictionComponent")]
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
            [References("Patient","Practitioner","RelatedPerson","Group","Organization")]
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
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "repetitions":
                        RepetitionsElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "period":
                        Period = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "recipient":
                        Recipient = source.GetList<Hl7.Fhir.Model.ResourceReference>();
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ParameterComponent")]
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
            [AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.STU3.Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.STU3.ContactPoint),typeof(Hl7.Fhir.Model.STU3.Count),typeof(Hl7.Fhir.Model.STU3.Distance),typeof(Hl7.Fhir.Model.STU3.Duration),typeof(Hl7.Fhir.Model.STU3.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.STU3.Money),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.STU3.SampledData),typeof(Hl7.Fhir.Model.STU3.Signature),typeof(Hl7.Fhir.Model.STU3.Timing),typeof(Hl7.Fhir.Model.Meta))]
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
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "valueBase64Binary":
                        source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Base64Binary>();
                        return true;
                    case "valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "valueCode":
                        source.CheckDuplicates<Hl7.Fhir.Model.Code>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Code>();
                        return true;
                    case "valueDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Date>();
                        return true;
                    case "valueDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                        return true;
                    case "valueDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                        return true;
                    case "valueId":
                        source.CheckDuplicates<Hl7.Fhir.Model.Id>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "valueInstant":
                        source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Instant>();
                        return true;
                    case "valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "valueMarkdown":
                        source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Markdown>();
                        return true;
                    case "valueOid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Oid>();
                        return true;
                    case "valuePositiveInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Time>();
                        return true;
                    case "valueUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "valueUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "valueAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Address>();
                        return true;
                    case "valueAge":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Age>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Age>();
                        return true;
                    case "valueAnnotation":
                        source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Annotation>();
                        return true;
                    case "valueAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Attachment>();
                        return true;
                    case "valueCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "valueCoding":
                        source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "valueContactPoint":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.ContactPoint>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.ContactPoint>();
                        return true;
                    case "valueCount":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Count>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Count>();
                        return true;
                    case "valueDistance":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Distance>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Distance>();
                        return true;
                    case "valueDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Duration>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Duration>();
                        return true;
                    case "valueHumanName":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.HumanName>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.HumanName>();
                        return true;
                    case "valueIdentifier":
                        source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "valueMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "valuePeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "valueQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Quantity>();
                        return true;
                    case "valueRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Range>();
                        return true;
                    case "valueRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Ratio>();
                        return true;
                    case "valueReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "valueSampledData":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.SampledData>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.SampledData>();
                        return true;
                    case "valueSignature":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Signature>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Signature>();
                        return true;
                    case "valueTiming":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Timing>();
                        return true;
                    case "valueMeta":
                        source.CheckDuplicates<Hl7.Fhir.Model.Meta>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Meta>();
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
                    case "valueAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Address);
                        return true;
                    case "valueAge":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Age>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Age);
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
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.ContactPoint>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.ContactPoint);
                        return true;
                    case "valueCount":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Count>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Count);
                        return true;
                    case "valueDistance":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Distance>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Distance);
                        return true;
                    case "valueDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Duration>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Duration);
                        return true;
                    case "valueHumanName":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.HumanName>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.HumanName);
                        return true;
                    case "valueIdentifier":
                        source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Identifier);
                        return true;
                    case "valueMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Money);
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
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.SampledData>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.SampledData);
                        return true;
                    case "valueSignature":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Signature>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Signature);
                        return true;
                    case "valueTiming":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Timing);
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "OutputComponent")]
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
            [AllowedTypes(typeof(Hl7.Fhir.Model.Base64Binary),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Id),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Markdown),typeof(Hl7.Fhir.Model.Oid),typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.UnsignedInt),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.STU3.Age),typeof(Hl7.Fhir.Model.Annotation),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.STU3.ContactPoint),typeof(Hl7.Fhir.Model.STU3.Count),typeof(Hl7.Fhir.Model.STU3.Distance),typeof(Hl7.Fhir.Model.STU3.Duration),typeof(Hl7.Fhir.Model.STU3.HumanName),typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.STU3.Money),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.STU3.SampledData),typeof(Hl7.Fhir.Model.STU3.Signature),typeof(Hl7.Fhir.Model.STU3.Timing),typeof(Hl7.Fhir.Model.Meta))]
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
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "valueBase64Binary":
                        source.CheckDuplicates<Hl7.Fhir.Model.Base64Binary>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Base64Binary>();
                        return true;
                    case "valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "valueCode":
                        source.CheckDuplicates<Hl7.Fhir.Model.Code>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Code>();
                        return true;
                    case "valueDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Date>();
                        return true;
                    case "valueDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                        return true;
                    case "valueDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                        return true;
                    case "valueId":
                        source.CheckDuplicates<Hl7.Fhir.Model.Id>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Id>();
                        return true;
                    case "valueInstant":
                        source.CheckDuplicates<Hl7.Fhir.Model.Instant>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Instant>();
                        return true;
                    case "valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "valueMarkdown":
                        source.CheckDuplicates<Hl7.Fhir.Model.Markdown>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Markdown>();
                        return true;
                    case "valueOid":
                        source.CheckDuplicates<Hl7.Fhir.Model.Oid>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Oid>();
                        return true;
                    case "valuePositiveInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Time>();
                        return true;
                    case "valueUnsignedInt":
                        source.CheckDuplicates<Hl7.Fhir.Model.UnsignedInt>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "valueUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "valueAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Address>();
                        return true;
                    case "valueAge":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Age>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Age>();
                        return true;
                    case "valueAnnotation":
                        source.CheckDuplicates<Hl7.Fhir.Model.Annotation>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Annotation>();
                        return true;
                    case "valueAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Attachment>();
                        return true;
                    case "valueCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "valueCoding":
                        source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "valueContactPoint":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.ContactPoint>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.ContactPoint>();
                        return true;
                    case "valueCount":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Count>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Count>();
                        return true;
                    case "valueDistance":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Distance>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Distance>();
                        return true;
                    case "valueDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Duration>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Duration>();
                        return true;
                    case "valueHumanName":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.HumanName>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.HumanName>();
                        return true;
                    case "valueIdentifier":
                        source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "valueMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Money>();
                        return true;
                    case "valuePeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "valueQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Quantity>();
                        return true;
                    case "valueRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Range>();
                        return true;
                    case "valueRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Ratio>();
                        return true;
                    case "valueReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "valueSampledData":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.SampledData>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.SampledData>();
                        return true;
                    case "valueSignature":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Signature>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Signature>();
                        return true;
                    case "valueTiming":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.STU3.Timing>();
                        return true;
                    case "valueMeta":
                        source.CheckDuplicates<Hl7.Fhir.Model.Meta>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Meta>();
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
                    case "valueAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Address);
                        return true;
                    case "valueAge":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Age>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Age);
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
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.ContactPoint>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.ContactPoint);
                        return true;
                    case "valueCount":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Count>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Count);
                        return true;
                    case "valueDistance":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Distance>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Distance);
                        return true;
                    case "valueDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Duration>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Duration);
                        return true;
                    case "valueHumanName":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.HumanName>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.HumanName);
                        return true;
                    case "valueIdentifier":
                        source.CheckDuplicates<Hl7.Fhir.Model.Identifier>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Identifier);
                        return true;
                    case "valueMoney":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Money>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Money);
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
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.SampledData>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.SampledData);
                        return true;
                    case "valueSignature":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Signature>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Signature);
                        return true;
                    case "valueTiming":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Timing);
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
        [FhirElement("definition", InSummary=Hl7.Fhir.Model.Version.All, Order=100, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Definition
        {
            get { return _Definition; }
            set { _Definition = value; OnPropertyChanged("Definition"); }
        }
        
        private Hl7.Fhir.Model.Element _Definition;
        
        /// <summary>
        /// Request fulfilled by this task
        /// </summary>
        [FhirElement("basedOn", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
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
        [FhirElement("groupIdentifier", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
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
        [FhirElement("partOf", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
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
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
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
        [FhirElement("statusReason", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
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
        [FhirElement("businessStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept BusinessStatus
        {
            get { return _BusinessStatus; }
            set { _BusinessStatus = value; OnPropertyChanged("BusinessStatus"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _BusinessStatus;
        
        /// <summary>
        /// proposal | plan | order +
        /// </summary>
        [FhirElement("intent", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.RequestIntent> IntentElement
        {
            get { return _IntentElement; }
            set { _IntentElement = value; OnPropertyChanged("IntentElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.RequestIntent> _IntentElement;
        
        /// <summary>
        /// proposal | plan | order +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.RequestIntent? Intent
        {
            get { return IntentElement != null ? IntentElement.Value : null; }
            set
            {
                if (value == null)
                    IntentElement = null;
                else
                    IntentElement = new Code<Hl7.Fhir.Model.STU3.RequestIntent>(value);
                OnPropertyChanged("Intent");
            }
        }
        
        /// <summary>
        /// normal | urgent | asap | stat
        /// </summary>
        [FhirElement("priority", Order=180)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RequestPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RequestPriority> _PriorityElement;
        
        /// <summary>
        /// normal | urgent | asap | stat
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
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
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
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
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
        [FhirElement("focus", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
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
        [FhirElement("for", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
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
        [FhirElement("context", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// Start and end time of execution
        /// </summary>
        [FhirElement("executionPeriod", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
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
        [FhirElement("authoredOn", Order=250)]
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
        [FhirElement("lastModified", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
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
        [FhirElement("requester", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [DataMember]
        public RequesterComponent Requester
        {
            get { return _Requester; }
            set { _Requester = value; OnPropertyChanged("Requester"); }
        }
        
        private RequesterComponent _Requester;
        
        /// <summary>
        /// requester | dispatcher | scheduler | performer | monitor | manager | acquirer | reviewer
        /// </summary>
        [FhirElement("performerType", Order=280)]
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
        [FhirElement("owner", InSummary=Hl7.Fhir.Model.Version.All, Order=290)]
        [CLSCompliant(false)]
        [References("Device","Organization","Patient","Practitioner","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Owner
        {
            get { return _Owner; }
            set { _Owner = value; OnPropertyChanged("Owner"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Owner;
        
        /// <summary>
        /// Why task is needed
        /// </summary>
        [FhirElement("reason", Order=300)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Reason;
        
        /// <summary>
        /// Comments made about the task
        /// </summary>
        [FhirElement("note", Order=310)]
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
        [FhirElement("relevantHistory", Order=320)]
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
        [FhirElement("restriction", Order=330)]
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
        [FhirElement("input", Order=340)]
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
        [FhirElement("output", Order=350)]
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
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
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
                if(Definition != null) dest.Definition = (Hl7.Fhir.Model.Element)Definition.DeepCopy();
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(GroupIdentifier != null) dest.GroupIdentifier = (Hl7.Fhir.Model.Identifier)GroupIdentifier.DeepCopy();
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.TaskStatus>)StatusElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = (Hl7.Fhir.Model.CodeableConcept)StatusReason.DeepCopy();
                if(BusinessStatus != null) dest.BusinessStatus = (Hl7.Fhir.Model.CodeableConcept)BusinessStatus.DeepCopy();
                if(IntentElement != null) dest.IntentElement = (Code<Hl7.Fhir.Model.STU3.RequestIntent>)IntentElement.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.RequestPriority>)PriorityElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Focus != null) dest.Focus = (Hl7.Fhir.Model.ResourceReference)Focus.DeepCopy();
                if(For != null) dest.For = (Hl7.Fhir.Model.ResourceReference)For.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(ExecutionPeriod != null) dest.ExecutionPeriod = (Hl7.Fhir.Model.Period)ExecutionPeriod.DeepCopy();
                if(AuthoredOnElement != null) dest.AuthoredOnElement = (Hl7.Fhir.Model.FhirDateTime)AuthoredOnElement.DeepCopy();
                if(LastModifiedElement != null) dest.LastModifiedElement = (Hl7.Fhir.Model.FhirDateTime)LastModifiedElement.DeepCopy();
                if(Requester != null) dest.Requester = (RequesterComponent)Requester.DeepCopy();
                if(PerformerType != null) dest.PerformerType = new List<Hl7.Fhir.Model.CodeableConcept>(PerformerType.DeepCopy());
                if(Owner != null) dest.Owner = (Hl7.Fhir.Model.ResourceReference)Owner.DeepCopy();
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
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
            if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
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
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(ExecutionPeriod, otherT.ExecutionPeriod)) return false;
            if( !DeepComparable.Matches(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.Matches(LastModifiedElement, otherT.LastModifiedElement)) return false;
            if( !DeepComparable.Matches(Requester, otherT.Requester)) return false;
            if( !DeepComparable.Matches(PerformerType, otherT.PerformerType)) return false;
            if( !DeepComparable.Matches(Owner, otherT.Owner)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
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
            if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
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
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(ExecutionPeriod, otherT.ExecutionPeriod)) return false;
            if( !DeepComparable.IsExactly(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.IsExactly(LastModifiedElement, otherT.LastModifiedElement)) return false;
            if( !DeepComparable.IsExactly(Requester, otherT.Requester)) return false;
            if( !DeepComparable.IsExactly(PerformerType, otherT.PerformerType)) return false;
            if( !DeepComparable.IsExactly(Owner, otherT.Owner)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
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
            sink.Element("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Definition?.Serialize(sink);
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
            sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Context?.Serialize(sink);
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
            sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Reason?.Serialize(sink);
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
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "definitionUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Definition, "definition");
                    Definition = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "definitionReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Definition, "definition");
                    Definition = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "basedOn":
                    BasedOn = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "groupIdentifier":
                    GroupIdentifier = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "partOf":
                    PartOf = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.TaskStatus>>();
                    return true;
                case "statusReason":
                    StatusReason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "businessStatus":
                    BusinessStatus = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "intent":
                    IntentElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.RequestIntent>>();
                    return true;
                case "priority":
                    PriorityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.RequestPriority>>();
                    return true;
                case "code":
                    Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "focus":
                    Focus = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "for":
                    For = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "context":
                    Context = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "executionPeriod":
                    ExecutionPeriod = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "authoredOn":
                    AuthoredOnElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "lastModified":
                    LastModifiedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "requester":
                    Requester = source.Get<RequesterComponent>();
                    return true;
                case "performerType":
                    PerformerType = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "owner":
                    Owner = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "reason":
                    Reason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "relevantHistory":
                    RelevantHistory = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "restriction":
                    Restriction = source.Get<RestrictionComponent>();
                    return true;
                case "input":
                    Input = source.GetList<ParameterComponent>();
                    return true;
                case "output":
                    Output = source.GetList<OutputComponent>();
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
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "definitionUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Definition, "definition");
                    Definition = source.PopulateValue(Definition as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_definitionUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Definition, "definition");
                    Definition = source.Populate(Definition as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "definitionReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Definition, "definition");
                    Definition = source.Populate(Definition as Hl7.Fhir.Model.ResourceReference);
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
                case "context":
                    Context = source.Populate(Context);
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
                case "reason":
                    Reason = source.Populate(Reason);
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
                if (Definition != null) yield return Definition;
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
                if (Context != null) yield return Context;
                if (ExecutionPeriod != null) yield return ExecutionPeriod;
                if (AuthoredOnElement != null) yield return AuthoredOnElement;
                if (LastModifiedElement != null) yield return LastModifiedElement;
                if (Requester != null) yield return Requester;
                foreach (var elem in PerformerType) { if (elem != null) yield return elem; }
                if (Owner != null) yield return Owner;
                if (Reason != null) yield return Reason;
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
                if (Definition != null) yield return new ElementValue("definition", Definition);
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
                if (Context != null) yield return new ElementValue("context", Context);
                if (ExecutionPeriod != null) yield return new ElementValue("executionPeriod", ExecutionPeriod);
                if (AuthoredOnElement != null) yield return new ElementValue("authoredOn", AuthoredOnElement);
                if (LastModifiedElement != null) yield return new ElementValue("lastModified", LastModifiedElement);
                if (Requester != null) yield return new ElementValue("requester", Requester);
                foreach (var elem in PerformerType) { if (elem != null) yield return new ElementValue("performerType", elem); }
                if (Owner != null) yield return new ElementValue("owner", Owner);
                if (Reason != null) yield return new ElementValue("reason", Reason);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in RelevantHistory) { if (elem != null) yield return new ElementValue("relevantHistory", elem); }
                if (Restriction != null) yield return new ElementValue("restriction", Restriction);
                foreach (var elem in Input) { if (elem != null) yield return new ElementValue("input", elem); }
                foreach (var elem in Output) { if (elem != null) yield return new ElementValue("output", elem); }
            }
        }
    
    }

}
