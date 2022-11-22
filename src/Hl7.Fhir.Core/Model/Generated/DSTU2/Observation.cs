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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model.DSTU2
{
    /// <summary>
    /// Measurements and simple assertions
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "Observation", IsResource=true)]
    [DataContract]
    public partial class Observation : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IObservation, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Observation; } }
        [NotMapped]
        public override string TypeName { get { return "Observation"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ReferenceRangeComponent")]
        [DataContract]
        public partial class ReferenceRangeComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IObservationReferenceRangeComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ReferenceRangeComponent"; } }
            
            /// <summary>
            /// Low Range, if relevant
            /// </summary>
            [FhirElement("low", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Low
            {
                get { return _Low; }
                set { _Low = value; OnPropertyChanged("Low"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Low;
            
            /// <summary>
            /// High Range, if relevant
            /// </summary>
            [FhirElement("high", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity High
            {
                get { return _High; }
                set { _High = value; OnPropertyChanged("High"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _High;
            
            /// <summary>
            /// Indicates the meaning/use of this range of this range
            /// </summary>
            [FhirElement("meaning", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Meaning
            {
                get { return _Meaning; }
                set { _Meaning = value; OnPropertyChanged("Meaning"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Meaning;
            
            /// <summary>
            /// Applicable age range, if relevant
            /// </summary>
            [FhirElement("age", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Range Age
            {
                get { return _Age; }
                set { _Age = value; OnPropertyChanged("Age"); }
            }
            
            private Hl7.Fhir.Model.Range _Age;
            
            /// <summary>
            /// Text based reference range in an observation
            /// </summary>
            [FhirElement("text", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Text based reference range in an observation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if (value == null)
                        TextElement = null;
                    else
                        TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ReferenceRangeComponent");
                base.Serialize(sink);
                sink.Element("low", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Low?.Serialize(sink);
                sink.Element("high", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); High?.Serialize(sink);
                sink.Element("meaning", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Meaning?.Serialize(sink);
                sink.Element("age", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Age?.Serialize(sink);
                sink.Element("text", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TextElement?.Serialize(sink);
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
                    case "low":
                        Low = source.Populate(Low);
                        return true;
                    case "high":
                        High = source.Populate(High);
                        return true;
                    case "meaning":
                        Meaning = source.Populate(Meaning);
                        return true;
                    case "age":
                        Age = source.Populate(Age);
                        return true;
                    case "text":
                        TextElement = source.PopulateValue(TextElement);
                        return true;
                    case "_text":
                        TextElement = source.Populate(TextElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ReferenceRangeComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Low != null) dest.Low = (Hl7.Fhir.Model.SimpleQuantity)Low.DeepCopy();
                    if(High != null) dest.High = (Hl7.Fhir.Model.SimpleQuantity)High.DeepCopy();
                    if(Meaning != null) dest.Meaning = (Hl7.Fhir.Model.CodeableConcept)Meaning.DeepCopy();
                    if(Age != null) dest.Age = (Hl7.Fhir.Model.Range)Age.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ReferenceRangeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ReferenceRangeComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Low, otherT.Low)) return false;
                if( !DeepComparable.Matches(High, otherT.High)) return false;
                if( !DeepComparable.Matches(Meaning, otherT.Meaning)) return false;
                if( !DeepComparable.Matches(Age, otherT.Age)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ReferenceRangeComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Low, otherT.Low)) return false;
                if( !DeepComparable.IsExactly(High, otherT.High)) return false;
                if( !DeepComparable.IsExactly(Meaning, otherT.Meaning)) return false;
                if( !DeepComparable.IsExactly(Age, otherT.Age)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Low != null) yield return Low;
                    if (High != null) yield return High;
                    if (Meaning != null) yield return Meaning;
                    if (Age != null) yield return Age;
                    if (TextElement != null) yield return TextElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Low != null) yield return new ElementValue("low", Low);
                    if (High != null) yield return new ElementValue("high", High);
                    if (Meaning != null) yield return new ElementValue("meaning", Meaning);
                    if (Age != null) yield return new ElementValue("age", Age);
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "RelatedComponent")]
        [DataContract]
        public partial class RelatedComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedComponent"; } }
            
            /// <summary>
            /// has-member | derived-from | sequel-to | replaces | qualified-by | interfered-by
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ObservationRelationshipType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ObservationRelationshipType> _TypeElement;
            
            /// <summary>
            /// has-member | derived-from | sequel-to | replaces | qualified-by | interfered-by
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ObservationRelationshipType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.ObservationRelationshipType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Resource that is related to this one
            /// </summary>
            [FhirElement("target", Order=50)]
            [CLSCompliant(false)]
            [References("Observation","QuestionnaireResponse")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Target;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RelatedComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TypeElement?.Serialize(sink);
                sink.Element("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Target?.Serialize(sink);
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
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "target":
                        Target = source.Populate(Target);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.ObservationRelationshipType>)TypeElement.DeepCopy();
                    if(Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RelatedComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RelatedComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (Target != null) yield return Target;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (Target != null) yield return new ElementValue("target", Target);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ComponentComponent")]
        [DataContract]
        public partial class ComponentComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IObservationComponentComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ComponentComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IObservationReferenceRangeComponent> Hl7.Fhir.Model.IObservationComponentComponent.ReferenceRange { get { return ReferenceRange; } }
            
            /// <summary>
            /// Type of component observation (code / type)
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Actual component result
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.DSTU2.SampledData),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            /// <summary>
            /// Why the component result is missing
            /// </summary>
            [FhirElement("dataAbsentReason", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DataAbsentReason
            {
                get { return _DataAbsentReason; }
                set { _DataAbsentReason = value; OnPropertyChanged("DataAbsentReason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _DataAbsentReason;
            
            /// <summary>
            /// Provides guide for interpretation of component result
            /// </summary>
            [FhirElement("referenceRange", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ReferenceRangeComponent> ReferenceRange
            {
                get { if(_ReferenceRange==null) _ReferenceRange = new List<ReferenceRangeComponent>(); return _ReferenceRange; }
                set { _ReferenceRange = value; OnPropertyChanged("ReferenceRange"); }
            }
            
            private List<ReferenceRangeComponent> _ReferenceRange;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ComponentComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Value?.Serialize(sink);
                sink.Element("dataAbsentReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DataAbsentReason?.Serialize(sink);
                sink.BeginList("referenceRange", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ReferenceRange)
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
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "valueQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Quantity);
                        return true;
                    case "valueCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "valueRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Range);
                        return true;
                    case "valueRatio":
                        source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Ratio);
                        return true;
                    case "valueSampledData":
                        source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.DSTU2.SampledData);
                        return true;
                    case "valueAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Attachment);
                        return true;
                    case "valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Time);
                        return true;
                    case "_valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Time);
                        return true;
                    case "valueDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirDateTime);
                        return true;
                    case "_valueDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirDateTime);
                        return true;
                    case "valuePeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Period);
                        return true;
                    case "dataAbsentReason":
                        DataAbsentReason = source.Populate(DataAbsentReason);
                        return true;
                    case "referenceRange":
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
                    case "referenceRange":
                        source.PopulateListItem(ReferenceRange, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ComponentComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    if(DataAbsentReason != null) dest.DataAbsentReason = (Hl7.Fhir.Model.CodeableConcept)DataAbsentReason.DeepCopy();
                    if(ReferenceRange != null) dest.ReferenceRange = new List<ReferenceRangeComponent>(ReferenceRange.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ComponentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ComponentComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(DataAbsentReason, otherT.DataAbsentReason)) return false;
                if( !DeepComparable.Matches(ReferenceRange, otherT.ReferenceRange)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ComponentComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(DataAbsentReason, otherT.DataAbsentReason)) return false;
                if( !DeepComparable.IsExactly(ReferenceRange, otherT.ReferenceRange)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (Value != null) yield return Value;
                    if (DataAbsentReason != null) yield return DataAbsentReason;
                    foreach (var elem in ReferenceRange) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Value != null) yield return new ElementValue("value", Value);
                    if (DataAbsentReason != null) yield return new ElementValue("dataAbsentReason", DataAbsentReason);
                    foreach (var elem in ReferenceRange) { if (elem != null) yield return new ElementValue("referenceRange", elem); }
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IObservationReferenceRangeComponent> Hl7.Fhir.Model.IObservation.ReferenceRange { get { return ReferenceRange; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IObservationComponentComponent> Hl7.Fhir.Model.IObservation.Component { get { return Component; } }
    
        
        /// <summary>
        /// Unique Id for this particular observation
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
        /// registered | preliminary | final | amended +
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.ObservationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.ObservationStatus> _StatusElement;
        
        /// <summary>
        /// registered | preliminary | final | amended +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.ObservationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.ObservationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Classification of  type of observation
        /// </summary>
        [FhirElement("category", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// Type of observation (code / type)
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// Who and/or what this is about
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Patient","Group","Device","Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Healthcare event during which this observation is made
        /// </summary>
        [FhirElement("encounter", Order=140)]
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
        /// Clinically relevant time/time-period for observation
        /// </summary>
        [FhirElement("effective", InSummary=Hl7.Fhir.Model.Version.All, Order=150, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Effective
        {
            get { return _Effective; }
            set { _Effective = value; OnPropertyChanged("Effective"); }
        }
        
        private Hl7.Fhir.Model.Element _Effective;
        
        /// <summary>
        /// Date/Time this was made available
        /// </summary>
        [FhirElement("issued", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Instant IssuedElement
        {
            get { return _IssuedElement; }
            set { _IssuedElement = value; OnPropertyChanged("IssuedElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _IssuedElement;
        
        /// <summary>
        /// Date/Time this was made available
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Issued
        {
            get { return IssuedElement != null ? IssuedElement.Value : null; }
            set
            {
                if (value == null)
                    IssuedElement = null;
                else
                    IssuedElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Issued");
            }
        }
        
        /// <summary>
        /// Who is responsible for the observation
        /// </summary>
        [FhirElement("performer", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [References("Practitioner","Organization","Patient","RelatedPerson")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Performer
        {
            get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Performer;
        
        /// <summary>
        /// Actual result
        /// </summary>
        [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=180, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.DSTU2.SampledData),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Value
        {
            get { return _Value; }
            set { _Value = value; OnPropertyChanged("Value"); }
        }
        
        private Hl7.Fhir.Model.Element _Value;
        
        /// <summary>
        /// Why the result is missing
        /// </summary>
        [FhirElement("dataAbsentReason", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept DataAbsentReason
        {
            get { return _DataAbsentReason; }
            set { _DataAbsentReason = value; OnPropertyChanged("DataAbsentReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _DataAbsentReason;
        
        /// <summary>
        /// High, low, normal, etc.
        /// </summary>
        [FhirElement("interpretation", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Interpretation
        {
            get { return _Interpretation; }
            set { _Interpretation = value; OnPropertyChanged("Interpretation"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Interpretation;
        
        /// <summary>
        /// Comments about result
        /// </summary>
        [FhirElement("comments", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentsElement
        {
            get { return _CommentsElement; }
            set { _CommentsElement = value; OnPropertyChanged("CommentsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CommentsElement;
        
        /// <summary>
        /// Comments about result
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comments
        {
            get { return CommentsElement != null ? CommentsElement.Value : null; }
            set
            {
                if (value == null)
                    CommentsElement = null;
                else
                    CommentsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Comments");
            }
        }
        
        /// <summary>
        /// Observed body part
        /// </summary>
        [FhirElement("bodySite", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept BodySite
        {
            get { return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _BodySite;
        
        /// <summary>
        /// How it was done
        /// </summary>
        [FhirElement("method", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Method
        {
            get { return _Method; }
            set { _Method = value; OnPropertyChanged("Method"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Method;
        
        /// <summary>
        /// Specimen used for this observation
        /// </summary>
        [FhirElement("specimen", Order=240)]
        [CLSCompliant(false)]
        [References("Specimen")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Specimen
        {
            get { return _Specimen; }
            set { _Specimen = value; OnPropertyChanged("Specimen"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Specimen;
        
        /// <summary>
        /// (Measurement) Device
        /// </summary>
        [FhirElement("device", Order=250)]
        [CLSCompliant(false)]
        [References("Device","DeviceMetric")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Device
        {
            get { return _Device; }
            set { _Device = value; OnPropertyChanged("Device"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Device;
        
        /// <summary>
        /// Provides guide for interpretation
        /// </summary>
        [FhirElement("referenceRange", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ReferenceRangeComponent> ReferenceRange
        {
            get { if(_ReferenceRange==null) _ReferenceRange = new List<ReferenceRangeComponent>(); return _ReferenceRange; }
            set { _ReferenceRange = value; OnPropertyChanged("ReferenceRange"); }
        }
        
        private List<ReferenceRangeComponent> _ReferenceRange;
        
        /// <summary>
        /// Resource related to this observation
        /// </summary>
        [FhirElement("related", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<RelatedComponent> Related
        {
            get { if(_Related==null) _Related = new List<RelatedComponent>(); return _Related; }
            set { _Related = value; OnPropertyChanged("Related"); }
        }
        
        private List<RelatedComponent> _Related;
        
        /// <summary>
        /// Component results
        /// </summary>
        [FhirElement("component", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ComponentComponent> Component
        {
            get { if(_Component==null) _Component = new List<ComponentComponent>(); return _Component; }
            set { _Component = value; OnPropertyChanged("Component"); }
        }
        
        private List<ComponentComponent> _Component;
    
    
        public static ElementDefinitionConstraint[] Observation_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "obs-6",
                severity: ConstraintSeverity.Warning,
                expression: "dataAbsentReason.empty() or value.empty()",
                human: "dataAbsentReason SHALL only be present if Observation.value[x] is not present",
                xpath: "not(exists(f:dataAbsentReason)) or (not(exists(*[starts-with(local-name(.), 'value')])))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "obs-7",
                severity: ConstraintSeverity.Warning,
                expression: "component.where(code = %context.code).empty()",
                human: "Component code SHALL not be same as observation code",
                xpath: "not(exists(f:component/f:code)) or count(for $coding in f:code/f:coding return parent::*/f:component/f:code/f:coding[f:code/@value=$coding/f:code/@value and f:system/@value=$coding/f:system/@value])=0"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "obs-3",
                severity: ConstraintSeverity.Warning,
                expression: "referenceRange.all(low or high or text)",
                human: "Must have at least a low or a high or text",
                xpath: "(exists(f:low) or exists(f:high)or exists(f:text))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Observation_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Observation;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.ObservationStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.Instant)IssuedElement.DeepCopy();
                if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ResourceReference>(Performer.DeepCopy());
                if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                if(DataAbsentReason != null) dest.DataAbsentReason = (Hl7.Fhir.Model.CodeableConcept)DataAbsentReason.DeepCopy();
                if(Interpretation != null) dest.Interpretation = (Hl7.Fhir.Model.CodeableConcept)Interpretation.DeepCopy();
                if(CommentsElement != null) dest.CommentsElement = (Hl7.Fhir.Model.FhirString)CommentsElement.DeepCopy();
                if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.CodeableConcept)BodySite.DeepCopy();
                if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                if(Specimen != null) dest.Specimen = (Hl7.Fhir.Model.ResourceReference)Specimen.DeepCopy();
                if(Device != null) dest.Device = (Hl7.Fhir.Model.ResourceReference)Device.DeepCopy();
                if(ReferenceRange != null) dest.ReferenceRange = new List<ReferenceRangeComponent>(ReferenceRange.DeepCopy());
                if(Related != null) dest.Related = new List<RelatedComponent>(Related.DeepCopy());
                if(Component != null) dest.Component = new List<ComponentComponent>(Component.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Observation());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Observation;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
            if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            if( !DeepComparable.Matches(DataAbsentReason, otherT.DataAbsentReason)) return false;
            if( !DeepComparable.Matches(Interpretation, otherT.Interpretation)) return false;
            if( !DeepComparable.Matches(CommentsElement, otherT.CommentsElement)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Method, otherT.Method)) return false;
            if( !DeepComparable.Matches(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.Matches(Device, otherT.Device)) return false;
            if( !DeepComparable.Matches(ReferenceRange, otherT.ReferenceRange)) return false;
            if( !DeepComparable.Matches(Related, otherT.Related)) return false;
            if( !DeepComparable.Matches(Component, otherT.Component)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Observation;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
            if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            if( !DeepComparable.IsExactly(DataAbsentReason, otherT.DataAbsentReason)) return false;
            if( !DeepComparable.IsExactly(Interpretation, otherT.Interpretation)) return false;
            if( !DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
            if( !DeepComparable.IsExactly(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
            if( !DeepComparable.IsExactly(ReferenceRange, otherT.ReferenceRange)) return false;
            if( !DeepComparable.IsExactly(Related, otherT.Related)) return false;
            if( !DeepComparable.IsExactly(Component, otherT.Component)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Observation");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Category?.Serialize(sink);
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Subject?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Encounter?.Serialize(sink);
            sink.Element("effective", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Effective?.Serialize(sink);
            sink.Element("issued", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IssuedElement?.Serialize(sink);
            sink.BeginList("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Performer)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Value?.Serialize(sink);
            sink.Element("dataAbsentReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DataAbsentReason?.Serialize(sink);
            sink.Element("interpretation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Interpretation?.Serialize(sink);
            sink.Element("comments", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CommentsElement?.Serialize(sink);
            sink.Element("bodySite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); BodySite?.Serialize(sink);
            sink.Element("method", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Method?.Serialize(sink);
            sink.Element("specimen", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Specimen?.Serialize(sink);
            sink.Element("device", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Device?.Serialize(sink);
            sink.BeginList("referenceRange", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ReferenceRange)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("related", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Related)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("component", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Component)
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
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "category":
                    Category = source.Populate(Category);
                    return true;
                case "code":
                    Code = source.Populate(Code);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "effectiveDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Effective, "effective");
                    Effective = source.PopulateValue(Effective as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_effectiveDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Effective, "effective");
                    Effective = source.Populate(Effective as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "effectivePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Effective, "effective");
                    Effective = source.Populate(Effective as Hl7.Fhir.Model.Period);
                    return true;
                case "issued":
                    IssuedElement = source.PopulateValue(IssuedElement);
                    return true;
                case "_issued":
                    IssuedElement = source.Populate(IssuedElement);
                    return true;
                case "performer":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "valueQuantity":
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Value, "value");
                    Value = source.Populate(Value as Hl7.Fhir.Model.Quantity);
                    return true;
                case "valueCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Value, "value");
                    Value = source.Populate(Value as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "valueString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                    Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_valueString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                    Value = source.Populate(Value as Hl7.Fhir.Model.FhirString);
                    return true;
                case "valueRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Value, "value");
                    Value = source.Populate(Value as Hl7.Fhir.Model.Range);
                    return true;
                case "valueRatio":
                    source.CheckDuplicates<Hl7.Fhir.Model.Ratio>(Value, "value");
                    Value = source.Populate(Value as Hl7.Fhir.Model.Ratio);
                    return true;
                case "valueSampledData":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.SampledData>(Value, "value");
                    Value = source.Populate(Value as Hl7.Fhir.Model.DSTU2.SampledData);
                    return true;
                case "valueAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Value, "value");
                    Value = source.Populate(Value as Hl7.Fhir.Model.Attachment);
                    return true;
                case "valueTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                    Value = source.PopulateValue(Value as Hl7.Fhir.Model.Time);
                    return true;
                case "_valueTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                    Value = source.Populate(Value as Hl7.Fhir.Model.Time);
                    return true;
                case "valueDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Value, "value");
                    Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_valueDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Value, "value");
                    Value = source.Populate(Value as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "valuePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Value, "value");
                    Value = source.Populate(Value as Hl7.Fhir.Model.Period);
                    return true;
                case "dataAbsentReason":
                    DataAbsentReason = source.Populate(DataAbsentReason);
                    return true;
                case "interpretation":
                    Interpretation = source.Populate(Interpretation);
                    return true;
                case "comments":
                    CommentsElement = source.PopulateValue(CommentsElement);
                    return true;
                case "_comments":
                    CommentsElement = source.Populate(CommentsElement);
                    return true;
                case "bodySite":
                    BodySite = source.Populate(BodySite);
                    return true;
                case "method":
                    Method = source.Populate(Method);
                    return true;
                case "specimen":
                    Specimen = source.Populate(Specimen);
                    return true;
                case "device":
                    Device = source.Populate(Device);
                    return true;
                case "referenceRange":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "related":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "component":
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
                case "performer":
                    source.PopulateListItem(Performer, index);
                    return true;
                case "referenceRange":
                    source.PopulateListItem(ReferenceRange, index);
                    return true;
                case "related":
                    source.PopulateListItem(Related, index);
                    return true;
                case "component":
                    source.PopulateListItem(Component, index);
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
                if (StatusElement != null) yield return StatusElement;
                if (Category != null) yield return Category;
                if (Code != null) yield return Code;
                if (Subject != null) yield return Subject;
                if (Encounter != null) yield return Encounter;
                if (Effective != null) yield return Effective;
                if (IssuedElement != null) yield return IssuedElement;
                foreach (var elem in Performer) { if (elem != null) yield return elem; }
                if (Value != null) yield return Value;
                if (DataAbsentReason != null) yield return DataAbsentReason;
                if (Interpretation != null) yield return Interpretation;
                if (CommentsElement != null) yield return CommentsElement;
                if (BodySite != null) yield return BodySite;
                if (Method != null) yield return Method;
                if (Specimen != null) yield return Specimen;
                if (Device != null) yield return Device;
                foreach (var elem in ReferenceRange) { if (elem != null) yield return elem; }
                foreach (var elem in Related) { if (elem != null) yield return elem; }
                foreach (var elem in Component) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Category != null) yield return new ElementValue("category", Category);
                if (Code != null) yield return new ElementValue("code", Code);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (Effective != null) yield return new ElementValue("effective", Effective);
                if (IssuedElement != null) yield return new ElementValue("issued", IssuedElement);
                foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                if (Value != null) yield return new ElementValue("value", Value);
                if (DataAbsentReason != null) yield return new ElementValue("dataAbsentReason", DataAbsentReason);
                if (Interpretation != null) yield return new ElementValue("interpretation", Interpretation);
                if (CommentsElement != null) yield return new ElementValue("comments", CommentsElement);
                if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                if (Method != null) yield return new ElementValue("method", Method);
                if (Specimen != null) yield return new ElementValue("specimen", Specimen);
                if (Device != null) yield return new ElementValue("device", Device);
                foreach (var elem in ReferenceRange) { if (elem != null) yield return new ElementValue("referenceRange", elem); }
                foreach (var elem in Related) { if (elem != null) yield return new ElementValue("related", elem); }
                foreach (var elem in Component) { if (elem != null) yield return new ElementValue("component", elem); }
            }
        }
    
    }

}
