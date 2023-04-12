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
    /// Group of multiple entities
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Group", IsResource=true)]
    [DataContract]
    public partial class Group : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IGroup, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Group; } }
        [NotMapped]
        public override string TypeName { get { return "Group"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "CharacteristicComponent")]
        [DataContract]
        public partial class CharacteristicComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IGroupCharacteristicComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CharacteristicComponent"; } }
            
            /// <summary>
            /// Kind of characteristic
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Value held by characteristic
            /// </summary>
            [FhirElement("value", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            /// <summary>
            /// Group includes or excludes
            /// </summary>
            [FhirElement("exclude", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ExcludeElement
            {
                get { return _ExcludeElement; }
                set { _ExcludeElement = value; OnPropertyChanged("ExcludeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ExcludeElement;
            
            /// <summary>
            /// Group includes or excludes
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Exclude
            {
                get { return ExcludeElement != null ? ExcludeElement.Value : null; }
                set
                {
                    if (value == null)
                        ExcludeElement = null;
                    else
                        ExcludeElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Exclude");
                }
            }
            
            /// <summary>
            /// Period over which characteristic is tested
            /// </summary>
            [FhirElement("period", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CharacteristicComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Code?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Value?.Serialize(sink);
                sink.Element("exclude", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ExcludeElement?.Serialize(sink);
                sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Period?.Serialize(sink);
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
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "valueCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "valueQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Quantity>();
                        return true;
                    case "valueRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.Range>();
                        return true;
                    case "valueReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Value, "value");
                        Value = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "exclude":
                        ExcludeElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "period":
                        Period = source.Get<Hl7.Fhir.Model.Period>();
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
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "valueCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "_valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "valueQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Quantity);
                        return true;
                    case "valueRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Range);
                        return true;
                    case "valueReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "exclude":
                        ExcludeElement = source.PopulateValue(ExcludeElement);
                        return true;
                    case "_exclude":
                        ExcludeElement = source.Populate(ExcludeElement);
                        return true;
                    case "period":
                        Period = source.Populate(Period);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CharacteristicComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    if(ExcludeElement != null) dest.ExcludeElement = (Hl7.Fhir.Model.FhirBoolean)ExcludeElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CharacteristicComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CharacteristicComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(ExcludeElement, otherT.ExcludeElement)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CharacteristicComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(ExcludeElement, otherT.ExcludeElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            
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
                    if (ExcludeElement != null) yield return ExcludeElement;
                    if (Period != null) yield return Period;
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
                    if (ExcludeElement != null) yield return new ElementValue("exclude", ExcludeElement);
                    if (Period != null) yield return new ElementValue("period", Period);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "MemberComponent")]
        [DataContract]
        public partial class MemberComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IGroupMemberComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "MemberComponent"; } }
            
            /// <summary>
            /// Reference to the group member
            /// </summary>
            [FhirElement("entity", Order=40)]
            [CLSCompliant(false)]
            [References("Patient","Practitioner","PractitionerRole","Device","Medication","Substance","Group")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Entity
            {
                get { return _Entity; }
                set { _Entity = value; OnPropertyChanged("Entity"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Entity;
            
            /// <summary>
            /// Period member belonged to the group
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
            /// If member is no longer in group
            /// </summary>
            [FhirElement("inactive", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean InactiveElement
            {
                get { return _InactiveElement; }
                set { _InactiveElement = value; OnPropertyChanged("InactiveElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _InactiveElement;
            
            /// <summary>
            /// If member is no longer in group
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Inactive
            {
                get { return InactiveElement != null ? InactiveElement.Value : null; }
                set
                {
                    if (value == null)
                        InactiveElement = null;
                    else
                        InactiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Inactive");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("MemberComponent");
                base.Serialize(sink);
                sink.Element("entity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Entity?.Serialize(sink);
                sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Period?.Serialize(sink);
                sink.Element("inactive", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); InactiveElement?.Serialize(sink);
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
                    case "entity":
                        Entity = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "period":
                        Period = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "inactive":
                        InactiveElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
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
                    case "entity":
                        Entity = source.Populate(Entity);
                        return true;
                    case "period":
                        Period = source.Populate(Period);
                        return true;
                    case "inactive":
                        InactiveElement = source.PopulateValue(InactiveElement);
                        return true;
                    case "_inactive":
                        InactiveElement = source.Populate(InactiveElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MemberComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Entity != null) dest.Entity = (Hl7.Fhir.Model.ResourceReference)Entity.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(InactiveElement != null) dest.InactiveElement = (Hl7.Fhir.Model.FhirBoolean)InactiveElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new MemberComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MemberComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Entity, otherT.Entity)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                if( !DeepComparable.Matches(InactiveElement, otherT.InactiveElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MemberComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Entity, otherT.Entity)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                if( !DeepComparable.IsExactly(InactiveElement, otherT.InactiveElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Entity != null) yield return Entity;
                    if (Period != null) yield return Period;
                    if (InactiveElement != null) yield return InactiveElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Entity != null) yield return new ElementValue("entity", Entity);
                    if (Period != null) yield return new ElementValue("period", Period);
                    if (InactiveElement != null) yield return new ElementValue("inactive", InactiveElement);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IGroupCharacteristicComponent> Hl7.Fhir.Model.IGroup.Characteristic { get { return Characteristic; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IGroupMemberComponent> Hl7.Fhir.Model.IGroup.Member { get { return Member; } }
    
        
        /// <summary>
        /// Unique id
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Whether this group's record is in active use
        /// </summary>
        [FhirElement("active", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ActiveElement
        {
            get { return _ActiveElement; }
            set { _ActiveElement = value; OnPropertyChanged("ActiveElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ActiveElement;
        
        /// <summary>
        /// Whether this group's record is in active use
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Active
        {
            get { return ActiveElement != null ? ActiveElement.Value : null; }
            set
            {
                if (value == null)
                    ActiveElement = null;
                else
                    ActiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Active");
            }
        }
        
        /// <summary>
        /// person | animal | practitioner | device | medication | substance
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.GroupType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.GroupType> _TypeElement;
        
        /// <summary>
        /// person | animal | practitioner | device | medication | substance
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.GroupType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Code<Hl7.Fhir.Model.GroupType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Descriptive or actual
        /// </summary>
        [FhirElement("actual", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ActualElement
        {
            get { return _ActualElement; }
            set { _ActualElement = value; OnPropertyChanged("ActualElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ActualElement;
        
        /// <summary>
        /// Descriptive or actual
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Actual
        {
            get { return ActualElement != null ? ActualElement.Value : null; }
            set
            {
                if (value == null)
                    ActualElement = null;
                else
                    ActualElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Actual");
            }
        }
        
        /// <summary>
        /// Kind of Group members
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// Label for Group
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Label for Group
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
        /// Number of members
        /// </summary>
        [FhirElement("quantity", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt QuantityElement
        {
            get { return _QuantityElement; }
            set { _QuantityElement = value; OnPropertyChanged("QuantityElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _QuantityElement;
        
        /// <summary>
        /// Number of members
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Quantity
        {
            get { return QuantityElement != null ? QuantityElement.Value : null; }
            set
            {
                if (value == null)
                    QuantityElement = null;
                else
                    QuantityElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("Quantity");
            }
        }
        
        /// <summary>
        /// Entity that is the custodian of the Group's definition
        /// </summary>
        [FhirElement("managingEntity", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [References("Organization","RelatedPerson","Practitioner","PractitionerRole")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ManagingEntity
        {
            get { return _ManagingEntity; }
            set { _ManagingEntity = value; OnPropertyChanged("ManagingEntity"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ManagingEntity;
        
        /// <summary>
        /// Include / Exclude group members by Trait
        /// </summary>
        [FhirElement("characteristic", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<CharacteristicComponent> Characteristic
        {
            get { if(_Characteristic==null) _Characteristic = new List<CharacteristicComponent>(); return _Characteristic; }
            set { _Characteristic = value; OnPropertyChanged("Characteristic"); }
        }
        
        private List<CharacteristicComponent> _Characteristic;
        
        /// <summary>
        /// Who or what is in group
        /// </summary>
        [FhirElement("member", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<MemberComponent> Member
        {
            get { if(_Member==null) _Member = new List<MemberComponent>(); return _Member; }
            set { _Member = value; OnPropertyChanged("Member"); }
        }
        
        private List<MemberComponent> _Member;
    
    
        public static ElementDefinitionConstraint[] Group_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "grp-1",
                severity: ConstraintSeverity.Warning,
                expression: "member.empty() or (actual = true)",
                human: "Can only have members if group is \"actual\"",
                xpath: "f:actual/@value=true() or not(exists(f:member))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Group_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Group;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(ActiveElement != null) dest.ActiveElement = (Hl7.Fhir.Model.FhirBoolean)ActiveElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.GroupType>)TypeElement.DeepCopy();
                if(ActualElement != null) dest.ActualElement = (Hl7.Fhir.Model.FhirBoolean)ActualElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(QuantityElement != null) dest.QuantityElement = (Hl7.Fhir.Model.UnsignedInt)QuantityElement.DeepCopy();
                if(ManagingEntity != null) dest.ManagingEntity = (Hl7.Fhir.Model.ResourceReference)ManagingEntity.DeepCopy();
                if(Characteristic != null) dest.Characteristic = new List<CharacteristicComponent>(Characteristic.DeepCopy());
                if(Member != null) dest.Member = new List<MemberComponent>(Member.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Group());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Group;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(ActiveElement, otherT.ActiveElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(ActualElement, otherT.ActualElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(QuantityElement, otherT.QuantityElement)) return false;
            if( !DeepComparable.Matches(ManagingEntity, otherT.ManagingEntity)) return false;
            if( !DeepComparable.Matches(Characteristic, otherT.Characteristic)) return false;
            if( !DeepComparable.Matches(Member, otherT.Member)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Group;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(ActiveElement, otherT.ActiveElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(ActualElement, otherT.ActualElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(QuantityElement, otherT.QuantityElement)) return false;
            if( !DeepComparable.IsExactly(ManagingEntity, otherT.ManagingEntity)) return false;
            if( !DeepComparable.IsExactly(Characteristic, otherT.Characteristic)) return false;
            if( !DeepComparable.IsExactly(Member, otherT.Member)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Group");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("active", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ActiveElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
            sink.Element("actual", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ActualElement?.Serialize(sink);
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Code?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); QuantityElement?.Serialize(sink);
            sink.Element("managingEntity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ManagingEntity?.Serialize(sink);
            sink.BeginList("characteristic", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Characteristic)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("member", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Member)
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
                case "active":
                    ActiveElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "type":
                    TypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.GroupType>>();
                    return true;
                case "actual":
                    ActualElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "code":
                    Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "name":
                    NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "quantity":
                    QuantityElement = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                    return true;
                case "managingEntity":
                    ManagingEntity = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "characteristic":
                    Characteristic = source.GetList<CharacteristicComponent>();
                    return true;
                case "member":
                    Member = source.GetList<MemberComponent>();
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
                case "active":
                    ActiveElement = source.PopulateValue(ActiveElement);
                    return true;
                case "_active":
                    ActiveElement = source.Populate(ActiveElement);
                    return true;
                case "type":
                    TypeElement = source.PopulateValue(TypeElement);
                    return true;
                case "_type":
                    TypeElement = source.Populate(TypeElement);
                    return true;
                case "actual":
                    ActualElement = source.PopulateValue(ActualElement);
                    return true;
                case "_actual":
                    ActualElement = source.Populate(ActualElement);
                    return true;
                case "code":
                    Code = source.Populate(Code);
                    return true;
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
                    return true;
                case "quantity":
                    QuantityElement = source.PopulateValue(QuantityElement);
                    return true;
                case "_quantity":
                    QuantityElement = source.Populate(QuantityElement);
                    return true;
                case "managingEntity":
                    ManagingEntity = source.Populate(ManagingEntity);
                    return true;
                case "characteristic":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "member":
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
                case "characteristic":
                    source.PopulateListItem(Characteristic, index);
                    return true;
                case "member":
                    source.PopulateListItem(Member, index);
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
                if (ActiveElement != null) yield return ActiveElement;
                if (TypeElement != null) yield return TypeElement;
                if (ActualElement != null) yield return ActualElement;
                if (Code != null) yield return Code;
                if (NameElement != null) yield return NameElement;
                if (QuantityElement != null) yield return QuantityElement;
                if (ManagingEntity != null) yield return ManagingEntity;
                foreach (var elem in Characteristic) { if (elem != null) yield return elem; }
                foreach (var elem in Member) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (ActiveElement != null) yield return new ElementValue("active", ActiveElement);
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (ActualElement != null) yield return new ElementValue("actual", ActualElement);
                if (Code != null) yield return new ElementValue("code", Code);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (QuantityElement != null) yield return new ElementValue("quantity", QuantityElement);
                if (ManagingEntity != null) yield return new ElementValue("managingEntity", ManagingEntity);
                foreach (var elem in Characteristic) { if (elem != null) yield return new ElementValue("characteristic", elem); }
                foreach (var elem in Member) { if (elem != null) yield return new ElementValue("member", elem); }
            }
        }
    
    }

}
