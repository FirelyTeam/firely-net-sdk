﻿using System;
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
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Definition of an observation
    /// </summary>
    [FhirType("ObservationDefinition", IsResource=true)]
    [DataContract]
    public partial class ObservationDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ObservationDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "ObservationDefinition"; } }
        
        /// <summary>
        /// Permitted data type for observation value.
        /// (url: http://hl7.org/fhir/ValueSet/permitted-data-type)
        /// </summary>
        [FhirEnumeration("ObservationDataType")]
        public enum ObservationDataType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/permitted-data-type)
            /// </summary>
            [EnumLiteral("Quantity", "http://hl7.org/fhir/permitted-data-type"), Description("Quantity")]
            Quantity,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/permitted-data-type)
            /// </summary>
            [EnumLiteral("CodeableConcept", "http://hl7.org/fhir/permitted-data-type"), Description("CodeableConcept")]
            CodeableConcept,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/permitted-data-type)
            /// </summary>
            [EnumLiteral("string", "http://hl7.org/fhir/permitted-data-type"), Description("string")]
            String,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/permitted-data-type)
            /// </summary>
            [EnumLiteral("boolean", "http://hl7.org/fhir/permitted-data-type"), Description("boolean")]
            Boolean,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/permitted-data-type)
            /// </summary>
            [EnumLiteral("integer", "http://hl7.org/fhir/permitted-data-type"), Description("integer")]
            Integer,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/permitted-data-type)
            /// </summary>
            [EnumLiteral("Range", "http://hl7.org/fhir/permitted-data-type"), Description("Range")]
            Range,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/permitted-data-type)
            /// </summary>
            [EnumLiteral("Ratio", "http://hl7.org/fhir/permitted-data-type"), Description("Ratio")]
            Ratio,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/permitted-data-type)
            /// </summary>
            [EnumLiteral("SampledData", "http://hl7.org/fhir/permitted-data-type"), Description("SampledData")]
            SampledData,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/permitted-data-type)
            /// </summary>
            [EnumLiteral("time", "http://hl7.org/fhir/permitted-data-type"), Description("time")]
            Time,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/permitted-data-type)
            /// </summary>
            [EnumLiteral("dateTime", "http://hl7.org/fhir/permitted-data-type"), Description("dateTime")]
            DateTime,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/permitted-data-type)
            /// </summary>
            [EnumLiteral("Period", "http://hl7.org/fhir/permitted-data-type"), Description("Period")]
            Period,
        }

        /// <summary>
        /// Codes identifying the category of observation range.
        /// (url: http://hl7.org/fhir/ValueSet/observation-range-category)
        /// </summary>
        [FhirEnumeration("ObservationRangeCategory")]
        public enum ObservationRangeCategory
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/observation-range-category)
            /// </summary>
            [EnumLiteral("reference", "http://hl7.org/fhir/observation-range-category"), Description("reference range")]
            Reference,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/observation-range-category)
            /// </summary>
            [EnumLiteral("critical", "http://hl7.org/fhir/observation-range-category"), Description("critical range")]
            Critical,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/observation-range-category)
            /// </summary>
            [EnumLiteral("absolute", "http://hl7.org/fhir/observation-range-category"), Description("absolute range")]
            Absolute,
        }

        [FhirType("QuantitativeDetailsComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class QuantitativeDetailsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
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
                    if (!value.HasValue)
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
                    if (!value.HasValue)
                        DecimalPrecisionElement = null; 
                    else
                        DecimalPrecisionElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("DecimalPrecision");
                }
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
        
        
        [FhirType("QualifiedIntervalComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class QualifiedIntervalComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "QualifiedIntervalComponent"; } }
            
            /// <summary>
            /// reference | critical | absolute
            /// </summary>
            [FhirElement("category", Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ObservationDefinition.ObservationRangeCategory> CategoryElement
            {
                get { return _CategoryElement; }
                set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ObservationDefinition.ObservationRangeCategory> _CategoryElement;
            
            /// <summary>
            /// reference | critical | absolute
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ObservationDefinition.ObservationRangeCategory? Category
            {
                get { return CategoryElement != null ? CategoryElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CategoryElement = null; 
                    else
                        CategoryElement = new Code<Hl7.Fhir.Model.ObservationDefinition.ObservationRangeCategory>(value);
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
                    if (!value.HasValue)
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QualifiedIntervalComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.ObservationDefinition.ObservationRangeCategory>)CategoryElement.DeepCopy();
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
        [FhirElement("category", InSummary=true, Order=90)]
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
        [FhirElement("code", InSummary=true, Order=100)]
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
        [FhirElement("identifier", InSummary=true, Order=110)]
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
        public List<Code<Hl7.Fhir.Model.ObservationDefinition.ObservationDataType>> PermittedDataTypeElement
        {
            get { if(_PermittedDataTypeElement==null) _PermittedDataTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ObservationDefinition.ObservationDataType>>(); return _PermittedDataTypeElement; }
            set { _PermittedDataTypeElement = value; OnPropertyChanged("PermittedDataTypeElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.ObservationDefinition.ObservationDataType>> _PermittedDataTypeElement;
        
        /// <summary>
        /// Quantity | CodeableConcept | string | boolean | integer | Range | Ratio | SampledData | time | dateTime | Period
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.ObservationDefinition.ObservationDataType?> PermittedDataType
        {
            get { return PermittedDataTypeElement != null ? PermittedDataTypeElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  PermittedDataTypeElement = null; 
                else
                  PermittedDataTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ObservationDefinition.ObservationDataType>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ObservationDefinition.ObservationDataType>(elem)));
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
                if (!value.HasValue)
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
        public Hl7.Fhir.Model.ObservationDefinition.QuantitativeDetailsComponent QuantitativeDetails
        {
            get { return _QuantitativeDetails; }
            set { _QuantitativeDetails = value; OnPropertyChanged("QuantitativeDetails"); }
        }
        
        private Hl7.Fhir.Model.ObservationDefinition.QuantitativeDetailsComponent _QuantitativeDetails;
        
        /// <summary>
        /// Qualified range for continuous and ordinal observation results
        /// </summary>
        [FhirElement("qualifiedInterval", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ObservationDefinition.QualifiedIntervalComponent> QualifiedInterval
        {
            get { if(_QualifiedInterval==null) _QualifiedInterval = new List<Hl7.Fhir.Model.ObservationDefinition.QualifiedIntervalComponent>(); return _QualifiedInterval; }
            set { _QualifiedInterval = value; OnPropertyChanged("QualifiedInterval"); }
        }
        
        private List<Hl7.Fhir.Model.ObservationDefinition.QualifiedIntervalComponent> _QualifiedInterval;
        
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
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ObservationDefinition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(PermittedDataTypeElement != null) dest.PermittedDataTypeElement = new List<Code<Hl7.Fhir.Model.ObservationDefinition.ObservationDataType>>(PermittedDataTypeElement.DeepCopy());
                if(MultipleResultsAllowedElement != null) dest.MultipleResultsAllowedElement = (Hl7.Fhir.Model.FhirBoolean)MultipleResultsAllowedElement.DeepCopy();
                if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                if(PreferredReportNameElement != null) dest.PreferredReportNameElement = (Hl7.Fhir.Model.FhirString)PreferredReportNameElement.DeepCopy();
                if(QuantitativeDetails != null) dest.QuantitativeDetails = (Hl7.Fhir.Model.ObservationDefinition.QuantitativeDetailsComponent)QuantitativeDetails.DeepCopy();
                if(QualifiedInterval != null) dest.QualifiedInterval = new List<Hl7.Fhir.Model.ObservationDefinition.QualifiedIntervalComponent>(QualifiedInterval.DeepCopy());
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
