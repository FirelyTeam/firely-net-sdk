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
    /// Describes a required data item
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "DataRequirement")]
    [DataContract]
    public partial class DataRequirement : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IDataRequirement, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "DataRequirement"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "CodeFilterComponent")]
        [DataContract]
        public partial class CodeFilterComponent : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IDataRequirementCodeFilterComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CodeFilterComponent"; } }
            
            /// <summary>
            /// The code-valued attribute of the filter
            /// </summary>
            [FhirElement("path", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// The code-valued attribute of the filter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if (value == null)
                        PathElement = null;
                    else
                        PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// Valueset for the filter
            /// </summary>
            [FhirElement("valueSet", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element ValueSet
            {
                get { return _ValueSet; }
                set { _ValueSet = value; OnPropertyChanged("ValueSet"); }
            }
            
            private Hl7.Fhir.Model.Element _ValueSet;
            
            /// <summary>
            /// What code is expected
            /// </summary>
            [FhirElement("valueCode", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Code> ValueCodeElement
            {
                get { if(_ValueCodeElement==null) _ValueCodeElement = new List<Hl7.Fhir.Model.Code>(); return _ValueCodeElement; }
                set { _ValueCodeElement = value; OnPropertyChanged("ValueCodeElement"); }
            }
            
            private List<Hl7.Fhir.Model.Code> _ValueCodeElement;
            
            /// <summary>
            /// What code is expected
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> ValueCode
            {
                get { return ValueCodeElement != null ? ValueCodeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ValueCodeElement = null;
                    else
                        ValueCodeElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem=>new Hl7.Fhir.Model.Code(elem)));
                    OnPropertyChanged("ValueCode");
                }
            }
            
            /// <summary>
            /// What Coding is expected
            /// </summary>
            [FhirElement("valueCoding", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> ValueCoding
            {
                get { if(_ValueCoding==null) _ValueCoding = new List<Hl7.Fhir.Model.Coding>(); return _ValueCoding; }
                set { _ValueCoding = value; OnPropertyChanged("ValueCoding"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _ValueCoding;
            
            /// <summary>
            /// What CodeableConcept is expected
            /// </summary>
            [FhirElement("valueCodeableConcept", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ValueCodeableConcept
            {
                get { if(_ValueCodeableConcept==null) _ValueCodeableConcept = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ValueCodeableConcept; }
                set { _ValueCodeableConcept = value; OnPropertyChanged("ValueCodeableConcept"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ValueCodeableConcept;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CodeFilterComponent");
                base.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); PathElement?.Serialize(sink);
                sink.Element("valueSet", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); ValueSet?.Serialize(sink);
                sink.BeginList("valueCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(ValueCodeElement);
                sink.End();
                sink.BeginList("valueCoding", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in ValueCoding)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("valueCodeableConcept", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in ValueCodeableConcept)
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
                    case "path":
                        PathElement = source.PopulateValue(PathElement);
                        return true;
                    case "_path":
                        PathElement = source.Populate(PathElement);
                        return true;
                    case "valueSetString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(ValueSet, "valueSet");
                        ValueSet = source.PopulateValue(ValueSet as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_valueSetString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(ValueSet, "valueSet");
                        ValueSet = source.Populate(ValueSet as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "valueSetReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(ValueSet, "valueSet");
                        ValueSet = source.Populate(ValueSet as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "valueCode":
                    case "_valueCode":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "valueCoding":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "valueCodeableConcept":
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
                    case "valueCode":
                        source.PopulatePrimitiveListItemValue(ValueCodeElement, index);
                        return true;
                    case "_valueCode":
                        source.PopulatePrimitiveListItem(ValueCodeElement, index);
                        return true;
                    case "valueCoding":
                        source.PopulateListItem(ValueCoding, index);
                        return true;
                    case "valueCodeableConcept":
                        source.PopulateListItem(ValueCodeableConcept, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CodeFilterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(ValueSet != null) dest.ValueSet = (Hl7.Fhir.Model.Element)ValueSet.DeepCopy();
                    if(ValueCodeElement != null) dest.ValueCodeElement = new List<Hl7.Fhir.Model.Code>(ValueCodeElement.DeepCopy());
                    if(ValueCoding != null) dest.ValueCoding = new List<Hl7.Fhir.Model.Coding>(ValueCoding.DeepCopy());
                    if(ValueCodeableConcept != null) dest.ValueCodeableConcept = new List<Hl7.Fhir.Model.CodeableConcept>(ValueCodeableConcept.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CodeFilterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CodeFilterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(ValueSet, otherT.ValueSet)) return false;
                if( !DeepComparable.Matches(ValueCodeElement, otherT.ValueCodeElement)) return false;
                if( !DeepComparable.Matches(ValueCoding, otherT.ValueCoding)) return false;
                if( !DeepComparable.Matches(ValueCodeableConcept, otherT.ValueCodeableConcept)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CodeFilterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(ValueSet, otherT.ValueSet)) return false;
                if( !DeepComparable.IsExactly(ValueCodeElement, otherT.ValueCodeElement)) return false;
                if( !DeepComparable.IsExactly(ValueCoding, otherT.ValueCoding)) return false;
                if( !DeepComparable.IsExactly(ValueCodeableConcept, otherT.ValueCodeableConcept)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (PathElement != null) yield return PathElement;
                    if (ValueSet != null) yield return ValueSet;
                    foreach (var elem in ValueCodeElement) { if (elem != null) yield return elem; }
                    foreach (var elem in ValueCoding) { if (elem != null) yield return elem; }
                    foreach (var elem in ValueCodeableConcept) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (ValueSet != null) yield return new ElementValue("valueSet", ValueSet);
                    foreach (var elem in ValueCodeElement) { if (elem != null) yield return new ElementValue("valueCode", elem); }
                    foreach (var elem in ValueCoding) { if (elem != null) yield return new ElementValue("valueCoding", elem); }
                    foreach (var elem in ValueCodeableConcept) { if (elem != null) yield return new ElementValue("valueCodeableConcept", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "DateFilterComponent")]
        [DataContract]
        public partial class DateFilterComponent : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IDataRequirementDateFilterComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DateFilterComponent"; } }
            
            /// <summary>
            /// The date-valued attribute of the filter
            /// </summary>
            [FhirElement("path", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// The date-valued attribute of the filter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if (value == null)
                        PathElement = null;
                    else
                        PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// The value of the filter, as a Period, DateTime, or Duration value
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.STU3.Duration))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DateFilterComponent");
                base.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); PathElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Value?.Serialize(sink);
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
                    case "path":
                        PathElement = source.PopulateValue(PathElement);
                        return true;
                    case "_path":
                        PathElement = source.Populate(PathElement);
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
                    case "valueDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.STU3.Duration>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.STU3.Duration);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DateFilterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DateFilterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DateFilterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DateFilterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (PathElement != null) yield return PathElement;
                    if (Value != null) yield return Value;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IDataRequirementCodeFilterComponent> Hl7.Fhir.Model.IDataRequirement.CodeFilter { get { return CodeFilter; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IDataRequirementDateFilterComponent> Hl7.Fhir.Model.IDataRequirement.DateFilter { get { return DateFilter; } }
    
        
        /// <summary>
        /// The type of the required data
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.FHIRAllTypes> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.FHIRAllTypes> _TypeElement;
        
        /// <summary>
        /// The type of the required data
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.FHIRAllTypes? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Code<Hl7.Fhir.Model.STU3.FHIRAllTypes>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// The profile of the required data
        /// </summary>
        [FhirElement("profile", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> ProfileElement
        {
            get { if(_ProfileElement==null) _ProfileElement = new List<Hl7.Fhir.Model.FhirUri>(); return _ProfileElement; }
            set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _ProfileElement;
        
        /// <summary>
        /// The profile of the required data
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Profile
        {
            get { return ProfileElement != null ? ProfileElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    ProfileElement = null;
                else
                    ProfileElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("Profile");
            }
        }
        
        /// <summary>
        /// Indicates that specific structure elements are referenced by the knowledge module
        /// </summary>
        [FhirElement("mustSupport", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> MustSupportElement
        {
            get { if(_MustSupportElement==null) _MustSupportElement = new List<Hl7.Fhir.Model.FhirString>(); return _MustSupportElement; }
            set { _MustSupportElement = value; OnPropertyChanged("MustSupportElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _MustSupportElement;
        
        /// <summary>
        /// Indicates that specific structure elements are referenced by the knowledge module
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> MustSupport
        {
            get { return MustSupportElement != null ? MustSupportElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    MustSupportElement = null;
                else
                    MustSupportElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("MustSupport");
            }
        }
        
        /// <summary>
        /// What codes are expected
        /// </summary>
        [FhirElement("codeFilter", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<CodeFilterComponent> CodeFilter
        {
            get { if(_CodeFilter==null) _CodeFilter = new List<CodeFilterComponent>(); return _CodeFilter; }
            set { _CodeFilter = value; OnPropertyChanged("CodeFilter"); }
        }
        
        private List<CodeFilterComponent> _CodeFilter;
        
        /// <summary>
        /// What dates/date ranges are expected
        /// </summary>
        [FhirElement("dateFilter", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DateFilterComponent> DateFilter
        {
            get { if(_DateFilter==null) _DateFilter = new List<DateFilterComponent>(); return _DateFilter; }
            set { _DateFilter = value; OnPropertyChanged("DateFilter"); }
        }
        
        private List<DateFilterComponent> _DateFilter;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DataRequirement;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.STU3.FHIRAllTypes>)TypeElement.DeepCopy();
                if(ProfileElement != null) dest.ProfileElement = new List<Hl7.Fhir.Model.FhirUri>(ProfileElement.DeepCopy());
                if(MustSupportElement != null) dest.MustSupportElement = new List<Hl7.Fhir.Model.FhirString>(MustSupportElement.DeepCopy());
                if(CodeFilter != null) dest.CodeFilter = new List<CodeFilterComponent>(CodeFilter.DeepCopy());
                if(DateFilter != null) dest.DateFilter = new List<DateFilterComponent>(DateFilter.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new DataRequirement());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DataRequirement;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
            if( !DeepComparable.Matches(MustSupportElement, otherT.MustSupportElement)) return false;
            if( !DeepComparable.Matches(CodeFilter, otherT.CodeFilter)) return false;
            if( !DeepComparable.Matches(DateFilter, otherT.DateFilter)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DataRequirement;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
            if( !DeepComparable.IsExactly(MustSupportElement, otherT.MustSupportElement)) return false;
            if( !DeepComparable.IsExactly(CodeFilter, otherT.CodeFilter)) return false;
            if( !DeepComparable.IsExactly(DateFilter, otherT.DateFilter)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("DataRequirement");
            base.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
            sink.BeginList("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(ProfileElement);
            sink.End();
            sink.BeginList("mustSupport", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(MustSupportElement);
            sink.End();
            sink.BeginList("codeFilter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in CodeFilter)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("dateFilter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in DateFilter)
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
                case "type":
                    TypeElement = source.PopulateValue(TypeElement);
                    return true;
                case "_type":
                    TypeElement = source.Populate(TypeElement);
                    return true;
                case "profile":
                case "_profile":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "mustSupport":
                case "_mustSupport":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "codeFilter":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "dateFilter":
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
                case "profile":
                    source.PopulatePrimitiveListItemValue(ProfileElement, index);
                    return true;
                case "_profile":
                    source.PopulatePrimitiveListItem(ProfileElement, index);
                    return true;
                case "mustSupport":
                    source.PopulatePrimitiveListItemValue(MustSupportElement, index);
                    return true;
                case "_mustSupport":
                    source.PopulatePrimitiveListItem(MustSupportElement, index);
                    return true;
                case "codeFilter":
                    source.PopulateListItem(CodeFilter, index);
                    return true;
                case "dateFilter":
                    source.PopulateListItem(DateFilter, index);
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
                if (TypeElement != null) yield return TypeElement;
                foreach (var elem in ProfileElement) { if (elem != null) yield return elem; }
                foreach (var elem in MustSupportElement) { if (elem != null) yield return elem; }
                foreach (var elem in CodeFilter) { if (elem != null) yield return elem; }
                foreach (var elem in DateFilter) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                foreach (var elem in ProfileElement) { if (elem != null) yield return new ElementValue("profile", elem); }
                foreach (var elem in MustSupportElement) { if (elem != null) yield return new ElementValue("mustSupport", elem); }
                foreach (var elem in CodeFilter) { if (elem != null) yield return new ElementValue("codeFilter", elem); }
                foreach (var elem in DateFilter) { if (elem != null) yield return new ElementValue("dateFilter", elem); }
            }
        }
    
    }

}
