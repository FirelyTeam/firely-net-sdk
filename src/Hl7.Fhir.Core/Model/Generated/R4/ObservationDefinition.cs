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
    /// Definition of an observation
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "ObservationDefinition", IsResource=true)]
    [DataContract]
    public partial class ObservationDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ObservationDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "ObservationDefinition"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "QuantitativeDetailsComponent")]
        [DataContract]
        public partial class QuantitativeDetailsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "QuantitativeDetailsComponent"; } }
            
            /// <summary>
            /// Customary unit for quantitative results
            /// </summary>
            [FhirElement("customaryUnit", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept CustomaryUnit
            {
                get { return _CustomaryUnit; }
                set { _CustomaryUnit = value; OnPropertyChanged("CustomaryUnit"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _CustomaryUnit;
            
            /// <summary>
            /// SI unit for quantitative results
            /// </summary>
            [FhirElement("unit", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Unit
            {
                get { return _Unit; }
                set { _Unit = value; OnPropertyChanged("Unit"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Unit;
            
            /// <summary>
            /// SI to Customary unit conversion factor
            /// </summary>
            [FhirElement("conversionFactor", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ConversionFactorElement
            {
                get { return _ConversionFactorElement; }
                set { _ConversionFactorElement = value; OnPropertyChanged("ConversionFactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ConversionFactorElement;
            
            /// <summary>
            /// SI to Customary unit conversion factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? ConversionFactor
            {
                get { return ConversionFactorElement != null ? ConversionFactorElement.Value : null; }
                set
                {
                    if (value == null)
                        ConversionFactorElement = null;
                    else
                        ConversionFactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("ConversionFactor");
                }
            }
            
            /// <summary>
            /// Decimal precision of observation quantitative results
            /// </summary>
            [FhirElement("decimalPrecision", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DecimalPrecisionElement
            {
                get { return _DecimalPrecisionElement; }
                set { _DecimalPrecisionElement = value; OnPropertyChanged("DecimalPrecisionElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _DecimalPrecisionElement;
            
            /// <summary>
            /// Decimal precision of observation quantitative results
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? DecimalPrecision
            {
                get { return DecimalPrecisionElement != null ? DecimalPrecisionElement.Value : null; }
                set
                {
                    if (value == null)
                        DecimalPrecisionElement = null;
                    else
                        DecimalPrecisionElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("DecimalPrecision");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("QuantitativeDetailsComponent");
                base.Serialize(sink);
                sink.Element("customaryUnit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CustomaryUnit?.Serialize(sink);
                sink.Element("unit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Unit?.Serialize(sink);
                sink.Element("conversionFactor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ConversionFactorElement?.Serialize(sink);
                sink.Element("decimalPrecision", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DecimalPrecisionElement?.Serialize(sink);
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
                    case "customaryUnit":
                        CustomaryUnit = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "unit":
                        Unit = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "conversionFactor":
                        ConversionFactorElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                        return true;
                    case "decimalPrecision":
                        DecimalPrecisionElement = source.Get<Hl7.Fhir.Model.Integer>();
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
                    case "customaryUnit":
                        CustomaryUnit = source.Populate(CustomaryUnit);
                        return true;
                    case "unit":
                        Unit = source.Populate(Unit);
                        return true;
                    case "conversionFactor":
                        ConversionFactorElement = source.PopulateValue(ConversionFactorElement);
                        return true;
                    case "_conversionFactor":
                        ConversionFactorElement = source.Populate(ConversionFactorElement);
                        return true;
                    case "decimalPrecision":
                        DecimalPrecisionElement = source.PopulateValue(DecimalPrecisionElement);
                        return true;
                    case "_decimalPrecision":
                        DecimalPrecisionElement = source.Populate(DecimalPrecisionElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QuantitativeDetailsComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CustomaryUnit != null) dest.CustomaryUnit = (Hl7.Fhir.Model.CodeableConcept)CustomaryUnit.DeepCopy();
                    if(Unit != null) dest.Unit = (Hl7.Fhir.Model.CodeableConcept)Unit.DeepCopy();
                    if(ConversionFactorElement != null) dest.ConversionFactorElement = (Hl7.Fhir.Model.FhirDecimal)ConversionFactorElement.DeepCopy();
                    if(DecimalPrecisionElement != null) dest.DecimalPrecisionElement = (Hl7.Fhir.Model.Integer)DecimalPrecisionElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new QuantitativeDetailsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as QuantitativeDetailsComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CustomaryUnit, otherT.CustomaryUnit)) return false;
                if( !DeepComparable.Matches(Unit, otherT.Unit)) return false;
                if( !DeepComparable.Matches(ConversionFactorElement, otherT.ConversionFactorElement)) return false;
                if( !DeepComparable.Matches(DecimalPrecisionElement, otherT.DecimalPrecisionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as QuantitativeDetailsComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CustomaryUnit, otherT.CustomaryUnit)) return false;
                if( !DeepComparable.IsExactly(Unit, otherT.Unit)) return false;
                if( !DeepComparable.IsExactly(ConversionFactorElement, otherT.ConversionFactorElement)) return false;
                if( !DeepComparable.IsExactly(DecimalPrecisionElement, otherT.DecimalPrecisionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CustomaryUnit != null) yield return CustomaryUnit;
                    if (Unit != null) yield return Unit;
                    if (ConversionFactorElement != null) yield return ConversionFactorElement;
                    if (DecimalPrecisionElement != null) yield return DecimalPrecisionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CustomaryUnit != null) yield return new ElementValue("customaryUnit", CustomaryUnit);
                    if (Unit != null) yield return new ElementValue("unit", Unit);
                    if (ConversionFactorElement != null) yield return new ElementValue("conversionFactor", ConversionFactorElement);
                    if (DecimalPrecisionElement != null) yield return new ElementValue("decimalPrecision", DecimalPrecisionElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "QualifiedIntervalComponent")]
        [DataContract]
        public partial class QualifiedIntervalComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "QualifiedIntervalComponent"; } }
            
            /// <summary>
            /// reference | critical | absolute
            /// </summary>
            [FhirElement("category", Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.ObservationRangeCategory> CategoryElement
            {
                get { return _CategoryElement; }
                set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.ObservationRangeCategory> _CategoryElement;
            
            /// <summary>
            /// reference | critical | absolute
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.ObservationRangeCategory? Category
            {
                get { return CategoryElement != null ? CategoryElement.Value : null; }
                set
                {
                    if (value == null)
                        CategoryElement = null;
                    else
                        CategoryElement = new Code<Hl7.Fhir.Model.R4.ObservationRangeCategory>(value);
                    OnPropertyChanged("Category");
                }
            }
            
            /// <summary>
            /// The interval itself, for continuous or ordinal observations
            /// </summary>
            [FhirElement("range", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Range Range
            {
                get { return _Range; }
                set { _Range = value; OnPropertyChanged("Range"); }
            }
            
            private Hl7.Fhir.Model.Range _Range;
            
            /// <summary>
            /// Range context qualifier
            /// </summary>
            [FhirElement("context", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Context
            {
                get { return _Context; }
                set { _Context = value; OnPropertyChanged("Context"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Context;
            
            /// <summary>
            /// Targetted population of the range
            /// </summary>
            [FhirElement("appliesTo", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> AppliesTo
            {
                get { if(_AppliesTo==null) _AppliesTo = new List<Hl7.Fhir.Model.CodeableConcept>(); return _AppliesTo; }
                set { _AppliesTo = value; OnPropertyChanged("AppliesTo"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _AppliesTo;
            
            /// <summary>
            /// male | female | other | unknown
            /// </summary>
            [FhirElement("gender", Order=80)]
            [DataMember]
            public Code<Hl7.Fhir.Model.AdministrativeGender> GenderElement
            {
                get { return _GenderElement; }
                set { _GenderElement = value; OnPropertyChanged("GenderElement"); }
            }
            
            private Code<Hl7.Fhir.Model.AdministrativeGender> _GenderElement;
            
            /// <summary>
            /// male | female | other | unknown
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AdministrativeGender? Gender
            {
                get { return GenderElement != null ? GenderElement.Value : null; }
                set
                {
                    if (value == null)
                        GenderElement = null;
                    else
                        GenderElement = new Code<Hl7.Fhir.Model.AdministrativeGender>(value);
                    OnPropertyChanged("Gender");
                }
            }
            
            /// <summary>
            /// Applicable age range, if relevant
            /// </summary>
            [FhirElement("age", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Range Age
            {
                get { return _Age; }
                set { _Age = value; OnPropertyChanged("Age"); }
            }
            
            private Hl7.Fhir.Model.Range _Age;
            
            /// <summary>
            /// Applicable gestational age range, if relevant
            /// </summary>
            [FhirElement("gestationalAge", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Range GestationalAge
            {
                get { return _GestationalAge; }
                set { _GestationalAge = value; OnPropertyChanged("GestationalAge"); }
            }
            
            private Hl7.Fhir.Model.Range _GestationalAge;
            
            /// <summary>
            /// Condition associated with the reference range
            /// </summary>
            [FhirElement("condition", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ConditionElement
            {
                get { return _ConditionElement; }
                set { _ConditionElement = value; OnPropertyChanged("ConditionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ConditionElement;
            
            /// <summary>
            /// Condition associated with the reference range
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Condition
            {
                get { return ConditionElement != null ? ConditionElement.Value : null; }
                set
                {
                    if (value == null)
                        ConditionElement = null;
                    else
                        ConditionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Condition");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("QualifiedIntervalComponent");
                base.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CategoryElement?.Serialize(sink);
                sink.Element("range", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Range?.Serialize(sink);
                sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Context?.Serialize(sink);
                sink.BeginList("appliesTo", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in AppliesTo)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("gender", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); GenderElement?.Serialize(sink);
                sink.Element("age", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Age?.Serialize(sink);
                sink.Element("gestationalAge", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); GestationalAge?.Serialize(sink);
                sink.Element("condition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ConditionElement?.Serialize(sink);
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
                    case "category":
                        CategoryElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.ObservationRangeCategory>>();
                        return true;
                    case "range":
                        Range = source.Get<Hl7.Fhir.Model.Range>();
                        return true;
                    case "context":
                        Context = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "appliesTo":
                        AppliesTo = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "gender":
                        GenderElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AdministrativeGender>>();
                        return true;
                    case "age":
                        Age = source.Get<Hl7.Fhir.Model.Range>();
                        return true;
                    case "gestationalAge":
                        GestationalAge = source.Get<Hl7.Fhir.Model.Range>();
                        return true;
                    case "condition":
                        ConditionElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "category":
                        CategoryElement = source.PopulateValue(CategoryElement);
                        return true;
                    case "_category":
                        CategoryElement = source.Populate(CategoryElement);
                        return true;
                    case "range":
                        Range = source.Populate(Range);
                        return true;
                    case "context":
                        Context = source.Populate(Context);
                        return true;
                    case "appliesTo":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "gender":
                        GenderElement = source.PopulateValue(GenderElement);
                        return true;
                    case "_gender":
                        GenderElement = source.Populate(GenderElement);
                        return true;
                    case "age":
                        Age = source.Populate(Age);
                        return true;
                    case "gestationalAge":
                        GestationalAge = source.Populate(GestationalAge);
                        return true;
                    case "condition":
                        ConditionElement = source.PopulateValue(ConditionElement);
                        return true;
                    case "_condition":
                        ConditionElement = source.Populate(ConditionElement);
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
                    case "appliesTo":
                        source.PopulateListItem(AppliesTo, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QualifiedIntervalComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.R4.ObservationRangeCategory>)CategoryElement.DeepCopy();
                    if(Range != null) dest.Range = (Hl7.Fhir.Model.Range)Range.DeepCopy();
                    if(Context != null) dest.Context = (Hl7.Fhir.Model.CodeableConcept)Context.DeepCopy();
                    if(AppliesTo != null) dest.AppliesTo = new List<Hl7.Fhir.Model.CodeableConcept>(AppliesTo.DeepCopy());
                    if(GenderElement != null) dest.GenderElement = (Code<Hl7.Fhir.Model.AdministrativeGender>)GenderElement.DeepCopy();
                    if(Age != null) dest.Age = (Hl7.Fhir.Model.Range)Age.DeepCopy();
                    if(GestationalAge != null) dest.GestationalAge = (Hl7.Fhir.Model.Range)GestationalAge.DeepCopy();
                    if(ConditionElement != null) dest.ConditionElement = (Hl7.Fhir.Model.FhirString)ConditionElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new QualifiedIntervalComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as QualifiedIntervalComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
                if( !DeepComparable.Matches(Range, otherT.Range)) return false;
                if( !DeepComparable.Matches(Context, otherT.Context)) return false;
                if( !DeepComparable.Matches(AppliesTo, otherT.AppliesTo)) return false;
                if( !DeepComparable.Matches(GenderElement, otherT.GenderElement)) return false;
                if( !DeepComparable.Matches(Age, otherT.Age)) return false;
                if( !DeepComparable.Matches(GestationalAge, otherT.GestationalAge)) return false;
                if( !DeepComparable.Matches(ConditionElement, otherT.ConditionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as QualifiedIntervalComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
                if( !DeepComparable.IsExactly(Range, otherT.Range)) return false;
                if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
                if( !DeepComparable.IsExactly(AppliesTo, otherT.AppliesTo)) return false;
                if( !DeepComparable.IsExactly(GenderElement, otherT.GenderElement)) return false;
                if( !DeepComparable.IsExactly(Age, otherT.Age)) return false;
                if( !DeepComparable.IsExactly(GestationalAge, otherT.GestationalAge)) return false;
                if( !DeepComparable.IsExactly(ConditionElement, otherT.ConditionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CategoryElement != null) yield return CategoryElement;
                    if (Range != null) yield return Range;
                    if (Context != null) yield return Context;
                    foreach (var elem in AppliesTo) { if (elem != null) yield return elem; }
                    if (GenderElement != null) yield return GenderElement;
                    if (Age != null) yield return Age;
                    if (GestationalAge != null) yield return GestationalAge;
                    if (ConditionElement != null) yield return ConditionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CategoryElement != null) yield return new ElementValue("category", CategoryElement);
                    if (Range != null) yield return new ElementValue("range", Range);
                    if (Context != null) yield return new ElementValue("context", Context);
                    foreach (var elem in AppliesTo) { if (elem != null) yield return new ElementValue("appliesTo", elem); }
                    if (GenderElement != null) yield return new ElementValue("gender", GenderElement);
                    if (Age != null) yield return new ElementValue("age", Age);
                    if (GestationalAge != null) yield return new ElementValue("gestationalAge", GestationalAge);
                    if (ConditionElement != null) yield return new ElementValue("condition", ConditionElement);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Category of observation
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// Type of observation (code / type)
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
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
        /// Business identifier for this ObservationDefinition instance
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
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
        /// Quantity | CodeableConcept | string | boolean | integer | Range | Ratio | SampledData | time | dateTime | Period
        /// </summary>
        [FhirElement("permittedDataType", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.R4.ObservationDataType>> PermittedDataTypeElement
        {
            get { if(_PermittedDataTypeElement==null) _PermittedDataTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.ObservationDataType>>(); return _PermittedDataTypeElement; }
            set { _PermittedDataTypeElement = value; OnPropertyChanged("PermittedDataTypeElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.R4.ObservationDataType>> _PermittedDataTypeElement;
        
        /// <summary>
        /// Quantity | CodeableConcept | string | boolean | integer | Range | Ratio | SampledData | time | dateTime | Period
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.R4.ObservationDataType?> PermittedDataType
        {
            get { return PermittedDataTypeElement != null ? PermittedDataTypeElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    PermittedDataTypeElement = null;
                else
                    PermittedDataTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.ObservationDataType>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.ObservationDataType>(elem)));
                OnPropertyChanged("PermittedDataType");
            }
        }
        
        /// <summary>
        /// Multiple results allowed
        /// </summary>
        [FhirElement("multipleResultsAllowed", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean MultipleResultsAllowedElement
        {
            get { return _MultipleResultsAllowedElement; }
            set { _MultipleResultsAllowedElement = value; OnPropertyChanged("MultipleResultsAllowedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _MultipleResultsAllowedElement;
        
        /// <summary>
        /// Multiple results allowed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? MultipleResultsAllowed
        {
            get { return MultipleResultsAllowedElement != null ? MultipleResultsAllowedElement.Value : null; }
            set
            {
                if (value == null)
                    MultipleResultsAllowedElement = null;
                else
                    MultipleResultsAllowedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("MultipleResultsAllowed");
            }
        }
        
        /// <summary>
        /// Method used to produce the observation
        /// </summary>
        [FhirElement("method", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Method
        {
            get { return _Method; }
            set { _Method = value; OnPropertyChanged("Method"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Method;
        
        /// <summary>
        /// Preferred report name
        /// </summary>
        [FhirElement("preferredReportName", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PreferredReportNameElement
        {
            get { return _PreferredReportNameElement; }
            set { _PreferredReportNameElement = value; OnPropertyChanged("PreferredReportNameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PreferredReportNameElement;
        
        /// <summary>
        /// Preferred report name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PreferredReportName
        {
            get { return PreferredReportNameElement != null ? PreferredReportNameElement.Value : null; }
            set
            {
                if (value == null)
                    PreferredReportNameElement = null;
                else
                    PreferredReportNameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("PreferredReportName");
            }
        }
        
        /// <summary>
        /// Characteristics of quantitative results
        /// </summary>
        [FhirElement("quantitativeDetails", Order=160)]
        [DataMember]
        public QuantitativeDetailsComponent QuantitativeDetails
        {
            get { return _QuantitativeDetails; }
            set { _QuantitativeDetails = value; OnPropertyChanged("QuantitativeDetails"); }
        }
        
        private QuantitativeDetailsComponent _QuantitativeDetails;
        
        /// <summary>
        /// Qualified range for continuous and ordinal observation results
        /// </summary>
        [FhirElement("qualifiedInterval", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<QualifiedIntervalComponent> QualifiedInterval
        {
            get { if(_QualifiedInterval==null) _QualifiedInterval = new List<QualifiedIntervalComponent>(); return _QualifiedInterval; }
            set { _QualifiedInterval = value; OnPropertyChanged("QualifiedInterval"); }
        }
        
        private List<QualifiedIntervalComponent> _QualifiedInterval;
        
        /// <summary>
        /// Value set of valid coded values for the observations conforming to this ObservationDefinition
        /// </summary>
        [FhirElement("validCodedValueSet", Order=180)]
        [CLSCompliant(false)]
        [References("ValueSet")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ValidCodedValueSet
        {
            get { return _ValidCodedValueSet; }
            set { _ValidCodedValueSet = value; OnPropertyChanged("ValidCodedValueSet"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ValidCodedValueSet;
        
        /// <summary>
        /// Value set of normal coded values for the observations conforming to this ObservationDefinition
        /// </summary>
        [FhirElement("normalCodedValueSet", Order=190)]
        [CLSCompliant(false)]
        [References("ValueSet")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference NormalCodedValueSet
        {
            get { return _NormalCodedValueSet; }
            set { _NormalCodedValueSet = value; OnPropertyChanged("NormalCodedValueSet"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _NormalCodedValueSet;
        
        /// <summary>
        /// Value set of abnormal coded values for the observations conforming to this ObservationDefinition
        /// </summary>
        [FhirElement("abnormalCodedValueSet", Order=200)]
        [CLSCompliant(false)]
        [References("ValueSet")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference AbnormalCodedValueSet
        {
            get { return _AbnormalCodedValueSet; }
            set { _AbnormalCodedValueSet = value; OnPropertyChanged("AbnormalCodedValueSet"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _AbnormalCodedValueSet;
        
        /// <summary>
        /// Value set of critical coded values for the observations conforming to this ObservationDefinition
        /// </summary>
        [FhirElement("criticalCodedValueSet", Order=210)]
        [CLSCompliant(false)]
        [References("ValueSet")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference CriticalCodedValueSet
        {
            get { return _CriticalCodedValueSet; }
            set { _CriticalCodedValueSet = value; OnPropertyChanged("CriticalCodedValueSet"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _CriticalCodedValueSet;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ObservationDefinition;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(PermittedDataTypeElement != null) dest.PermittedDataTypeElement = new List<Code<Hl7.Fhir.Model.R4.ObservationDataType>>(PermittedDataTypeElement.DeepCopy());
                if(MultipleResultsAllowedElement != null) dest.MultipleResultsAllowedElement = (Hl7.Fhir.Model.FhirBoolean)MultipleResultsAllowedElement.DeepCopy();
                if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                if(PreferredReportNameElement != null) dest.PreferredReportNameElement = (Hl7.Fhir.Model.FhirString)PreferredReportNameElement.DeepCopy();
                if(QuantitativeDetails != null) dest.QuantitativeDetails = (QuantitativeDetailsComponent)QuantitativeDetails.DeepCopy();
                if(QualifiedInterval != null) dest.QualifiedInterval = new List<QualifiedIntervalComponent>(QualifiedInterval.DeepCopy());
                if(ValidCodedValueSet != null) dest.ValidCodedValueSet = (Hl7.Fhir.Model.ResourceReference)ValidCodedValueSet.DeepCopy();
                if(NormalCodedValueSet != null) dest.NormalCodedValueSet = (Hl7.Fhir.Model.ResourceReference)NormalCodedValueSet.DeepCopy();
                if(AbnormalCodedValueSet != null) dest.AbnormalCodedValueSet = (Hl7.Fhir.Model.ResourceReference)AbnormalCodedValueSet.DeepCopy();
                if(CriticalCodedValueSet != null) dest.CriticalCodedValueSet = (Hl7.Fhir.Model.ResourceReference)CriticalCodedValueSet.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ObservationDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ObservationDefinition;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(PermittedDataTypeElement, otherT.PermittedDataTypeElement)) return false;
            if( !DeepComparable.Matches(MultipleResultsAllowedElement, otherT.MultipleResultsAllowedElement)) return false;
            if( !DeepComparable.Matches(Method, otherT.Method)) return false;
            if( !DeepComparable.Matches(PreferredReportNameElement, otherT.PreferredReportNameElement)) return false;
            if( !DeepComparable.Matches(QuantitativeDetails, otherT.QuantitativeDetails)) return false;
            if( !DeepComparable.Matches(QualifiedInterval, otherT.QualifiedInterval)) return false;
            if( !DeepComparable.Matches(ValidCodedValueSet, otherT.ValidCodedValueSet)) return false;
            if( !DeepComparable.Matches(NormalCodedValueSet, otherT.NormalCodedValueSet)) return false;
            if( !DeepComparable.Matches(AbnormalCodedValueSet, otherT.AbnormalCodedValueSet)) return false;
            if( !DeepComparable.Matches(CriticalCodedValueSet, otherT.CriticalCodedValueSet)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ObservationDefinition;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(PermittedDataTypeElement, otherT.PermittedDataTypeElement)) return false;
            if( !DeepComparable.IsExactly(MultipleResultsAllowedElement, otherT.MultipleResultsAllowedElement)) return false;
            if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
            if( !DeepComparable.IsExactly(PreferredReportNameElement, otherT.PreferredReportNameElement)) return false;
            if( !DeepComparable.IsExactly(QuantitativeDetails, otherT.QuantitativeDetails)) return false;
            if( !DeepComparable.IsExactly(QualifiedInterval, otherT.QualifiedInterval)) return false;
            if( !DeepComparable.IsExactly(ValidCodedValueSet, otherT.ValidCodedValueSet)) return false;
            if( !DeepComparable.IsExactly(NormalCodedValueSet, otherT.NormalCodedValueSet)) return false;
            if( !DeepComparable.IsExactly(AbnormalCodedValueSet, otherT.AbnormalCodedValueSet)) return false;
            if( !DeepComparable.IsExactly(CriticalCodedValueSet, otherT.CriticalCodedValueSet)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ObservationDefinition");
            base.Serialize(sink);
            sink.BeginList("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Category)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("permittedDataType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            sink.Serialize(PermittedDataTypeElement);
            sink.End();
            sink.Element("multipleResultsAllowed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); MultipleResultsAllowedElement?.Serialize(sink);
            sink.Element("method", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Method?.Serialize(sink);
            sink.Element("preferredReportName", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PreferredReportNameElement?.Serialize(sink);
            sink.Element("quantitativeDetails", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); QuantitativeDetails?.Serialize(sink);
            sink.BeginList("qualifiedInterval", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in QualifiedInterval)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("validCodedValueSet", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValidCodedValueSet?.Serialize(sink);
            sink.Element("normalCodedValueSet", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NormalCodedValueSet?.Serialize(sink);
            sink.Element("abnormalCodedValueSet", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AbnormalCodedValueSet?.Serialize(sink);
            sink.Element("criticalCodedValueSet", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CriticalCodedValueSet?.Serialize(sink);
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
                case "category":
                    Category = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "code":
                    Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "permittedDataType":
                    PermittedDataTypeElement = source.GetList<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.ObservationDataType>>();
                    return true;
                case "multipleResultsAllowed":
                    MultipleResultsAllowedElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "method":
                    Method = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "preferredReportName":
                    PreferredReportNameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "quantitativeDetails":
                    QuantitativeDetails = source.Get<QuantitativeDetailsComponent>();
                    return true;
                case "qualifiedInterval":
                    QualifiedInterval = source.GetList<QualifiedIntervalComponent>();
                    return true;
                case "validCodedValueSet":
                    ValidCodedValueSet = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "normalCodedValueSet":
                    NormalCodedValueSet = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "abnormalCodedValueSet":
                    AbnormalCodedValueSet = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "criticalCodedValueSet":
                    CriticalCodedValueSet = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                case "category":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "code":
                    Code = source.Populate(Code);
                    return true;
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "permittedDataType":
                case "_permittedDataType":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "multipleResultsAllowed":
                    MultipleResultsAllowedElement = source.PopulateValue(MultipleResultsAllowedElement);
                    return true;
                case "_multipleResultsAllowed":
                    MultipleResultsAllowedElement = source.Populate(MultipleResultsAllowedElement);
                    return true;
                case "method":
                    Method = source.Populate(Method);
                    return true;
                case "preferredReportName":
                    PreferredReportNameElement = source.PopulateValue(PreferredReportNameElement);
                    return true;
                case "_preferredReportName":
                    PreferredReportNameElement = source.Populate(PreferredReportNameElement);
                    return true;
                case "quantitativeDetails":
                    QuantitativeDetails = source.Populate(QuantitativeDetails);
                    return true;
                case "qualifiedInterval":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "validCodedValueSet":
                    ValidCodedValueSet = source.Populate(ValidCodedValueSet);
                    return true;
                case "normalCodedValueSet":
                    NormalCodedValueSet = source.Populate(NormalCodedValueSet);
                    return true;
                case "abnormalCodedValueSet":
                    AbnormalCodedValueSet = source.Populate(AbnormalCodedValueSet);
                    return true;
                case "criticalCodedValueSet":
                    CriticalCodedValueSet = source.Populate(CriticalCodedValueSet);
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
                case "category":
                    source.PopulateListItem(Category, index);
                    return true;
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "permittedDataType":
                    source.PopulatePrimitiveListItemValue(PermittedDataTypeElement, index);
                    return true;
                case "_permittedDataType":
                    source.PopulatePrimitiveListItem(PermittedDataTypeElement, index);
                    return true;
                case "qualifiedInterval":
                    source.PopulateListItem(QualifiedInterval, index);
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
                foreach (var elem in Category) { if (elem != null) yield return elem; }
                if (Code != null) yield return Code;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                foreach (var elem in PermittedDataTypeElement) { if (elem != null) yield return elem; }
                if (MultipleResultsAllowedElement != null) yield return MultipleResultsAllowedElement;
                if (Method != null) yield return Method;
                if (PreferredReportNameElement != null) yield return PreferredReportNameElement;
                if (QuantitativeDetails != null) yield return QuantitativeDetails;
                foreach (var elem in QualifiedInterval) { if (elem != null) yield return elem; }
                if (ValidCodedValueSet != null) yield return ValidCodedValueSet;
                if (NormalCodedValueSet != null) yield return NormalCodedValueSet;
                if (AbnormalCodedValueSet != null) yield return AbnormalCodedValueSet;
                if (CriticalCodedValueSet != null) yield return CriticalCodedValueSet;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (Code != null) yield return new ElementValue("code", Code);
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in PermittedDataTypeElement) { if (elem != null) yield return new ElementValue("permittedDataType", elem); }
                if (MultipleResultsAllowedElement != null) yield return new ElementValue("multipleResultsAllowed", MultipleResultsAllowedElement);
                if (Method != null) yield return new ElementValue("method", Method);
                if (PreferredReportNameElement != null) yield return new ElementValue("preferredReportName", PreferredReportNameElement);
                if (QuantitativeDetails != null) yield return new ElementValue("quantitativeDetails", QuantitativeDetails);
                foreach (var elem in QualifiedInterval) { if (elem != null) yield return new ElementValue("qualifiedInterval", elem); }
                if (ValidCodedValueSet != null) yield return new ElementValue("validCodedValueSet", ValidCodedValueSet);
                if (NormalCodedValueSet != null) yield return new ElementValue("normalCodedValueSet", NormalCodedValueSet);
                if (AbnormalCodedValueSet != null) yield return new ElementValue("abnormalCodedValueSet", AbnormalCodedValueSet);
                if (CriticalCodedValueSet != null) yield return new ElementValue("criticalCodedValueSet", CriticalCodedValueSet);
            }
        }
    
    }

}
