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
// Generated for FHIR v3.5.0
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
        
        [FhirType("QuantitativeDetailsComponent")]
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
            public Hl7.Fhir.Model.Coding CustomaryUnit
            {
                get { return _CustomaryUnit; }
                set { _CustomaryUnit = value; OnPropertyChanged("CustomaryUnit"); }
            }
            
            private Hl7.Fhir.Model.Coding _CustomaryUnit;
            
            /// <summary>
            /// SI unit for quantitative results
            /// </summary>
            [FhirElement("unit", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Unit
            {
                get { return _Unit; }
                set { _Unit = value; OnPropertyChanged("Unit"); }
            }
            
            private Hl7.Fhir.Model.Coding _Unit;
            
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
                    if(CustomaryUnit != null) dest.CustomaryUnit = (Hl7.Fhir.Model.Coding)CustomaryUnit.DeepCopy();
                    if(Unit != null) dest.Unit = (Hl7.Fhir.Model.Coding)Unit.DeepCopy();
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
                    if (CustomaryUnit != null) yield return new ElementValue("customaryUnit", false, CustomaryUnit);
                    if (Unit != null) yield return new ElementValue("unit", false, Unit);
                    if (ConversionFactorElement != null) yield return new ElementValue("conversionFactor", false, ConversionFactorElement);
                    if (DecimalPrecisionElement != null) yield return new ElementValue("decimalPrecision", false, DecimalPrecisionElement);
                }
            }

            
        }
        
        
        [FhirType("QualifiedIntervalComponent")]
        [DataContract]
        public partial class QualifiedIntervalComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "QualifiedIntervalComponent"; } }
            
            /// <summary>
            /// The category or type of interval
            /// </summary>
            [FhirElement("category", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Low bound of reference range, if relevant
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
            /// Reference range qualifier
            /// </summary>
            [FhirElement("type", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Reference range population
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
            /// Applicable age range, if relevant
            /// </summary>
            [FhirElement("age", Order=80)]
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
            [FhirElement("gestationalAge", Order=90)]
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
            [FhirElement("condition", Order=100)]
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
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Range != null) dest.Range = (Hl7.Fhir.Model.Range)Range.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(AppliesTo != null) dest.AppliesTo = new List<Hl7.Fhir.Model.CodeableConcept>(AppliesTo.DeepCopy());
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
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Range, otherT.Range)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(AppliesTo, otherT.AppliesTo)) return false;
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
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Range, otherT.Range)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(AppliesTo, otherT.AppliesTo)) return false;
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
                    if (Category != null) yield return Category;
                    if (Range != null) yield return Range;
                    if (Type != null) yield return Type;
                    foreach (var elem in AppliesTo) { if (elem != null) yield return elem; }
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
                    if (Category != null) yield return new ElementValue("category", false, Category);
                    if (Range != null) yield return new ElementValue("range", false, Range);
                    if (Type != null) yield return new ElementValue("type", false, Type);
                    foreach (var elem in AppliesTo) { if (elem != null) yield return new ElementValue("appliesTo", true, elem); }
                    if (Age != null) yield return new ElementValue("age", false, Age);
                    if (GestationalAge != null) yield return new ElementValue("gestationalAge", false, GestationalAge);
                    if (ConditionElement != null) yield return new ElementValue("condition", false, ConditionElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Category of observation
        /// </summary>
        [FhirElement("category", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.Coding _Category;
        
        /// <summary>
        /// Type of observation (code / type)
        /// </summary>
        [FhirElement("code", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.Coding _Code;
        
        /// <summary>
        /// Permitted data type for observation value
        /// </summary>
        [FhirElement("permittedDataType", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> PermittedDataType
        {
            get { if(_PermittedDataType==null) _PermittedDataType = new List<Hl7.Fhir.Model.Coding>(); return _PermittedDataType; }
            set { _PermittedDataType = value; OnPropertyChanged("PermittedDataType"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _PermittedDataType;
        
        /// <summary>
        /// Multiple results allowed
        /// </summary>
        [FhirElement("multipleResultsAllowed", Order=120)]
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
        /// The method or technique used to perform the observation
        /// </summary>
        [FhirElement("method", Order=130)]
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
        [FhirElement("preferredReportName", Order=140)]
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
        [FhirElement("quantitativeDetails", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.ObservationDefinition.QuantitativeDetailsComponent QuantitativeDetails
        {
            get { return _QuantitativeDetails; }
            set { _QuantitativeDetails = value; OnPropertyChanged("QuantitativeDetails"); }
        }
        
        private Hl7.Fhir.Model.ObservationDefinition.QuantitativeDetailsComponent _QuantitativeDetails;
        
        /// <summary>
        /// Reference range for observation result
        /// </summary>
        [FhirElement("qualifiedInterval", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ObservationDefinition.QualifiedIntervalComponent> QualifiedInterval
        {
            get { if(_QualifiedInterval==null) _QualifiedInterval = new List<Hl7.Fhir.Model.ObservationDefinition.QualifiedIntervalComponent>(); return _QualifiedInterval; }
            set { _QualifiedInterval = value; OnPropertyChanged("QualifiedInterval"); }
        }
        
        private List<Hl7.Fhir.Model.ObservationDefinition.QualifiedIntervalComponent> _QualifiedInterval;
        
        /// <summary>
        /// Value set of valid coded values for the observation
        /// </summary>
        [FhirElement("validCodedValueSet", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri ValidCodedValueSetElement
        {
            get { return _ValidCodedValueSetElement; }
            set { _ValidCodedValueSetElement = value; OnPropertyChanged("ValidCodedValueSetElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _ValidCodedValueSetElement;
        
        /// <summary>
        /// Value set of valid coded values for the observation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ValidCodedValueSet
        {
            get { return ValidCodedValueSetElement != null ? ValidCodedValueSetElement.Value : null; }
            set
            {
                if (value == null)
                  ValidCodedValueSetElement = null; 
                else
                  ValidCodedValueSetElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("ValidCodedValueSet");
            }
        }
        
        /// <summary>
        /// Value set of normal coded values for the observation
        /// </summary>
        [FhirElement("normalCodedValueSet", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri NormalCodedValueSetElement
        {
            get { return _NormalCodedValueSetElement; }
            set { _NormalCodedValueSetElement = value; OnPropertyChanged("NormalCodedValueSetElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _NormalCodedValueSetElement;
        
        /// <summary>
        /// Value set of normal coded values for the observation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string NormalCodedValueSet
        {
            get { return NormalCodedValueSetElement != null ? NormalCodedValueSetElement.Value : null; }
            set
            {
                if (value == null)
                  NormalCodedValueSetElement = null; 
                else
                  NormalCodedValueSetElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("NormalCodedValueSet");
            }
        }
        
        /// <summary>
        /// Value set of abnormal coded values for the observation
        /// </summary>
        [FhirElement("abnormalCodedValueSet", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri AbnormalCodedValueSetElement
        {
            get { return _AbnormalCodedValueSetElement; }
            set { _AbnormalCodedValueSetElement = value; OnPropertyChanged("AbnormalCodedValueSetElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _AbnormalCodedValueSetElement;
        
        /// <summary>
        /// Value set of abnormal coded values for the observation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AbnormalCodedValueSet
        {
            get { return AbnormalCodedValueSetElement != null ? AbnormalCodedValueSetElement.Value : null; }
            set
            {
                if (value == null)
                  AbnormalCodedValueSetElement = null; 
                else
                  AbnormalCodedValueSetElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("AbnormalCodedValueSet");
            }
        }
        
        /// <summary>
        /// Value set of critical coded values for the observation
        /// </summary>
        [FhirElement("criticalCodedValueSet", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri CriticalCodedValueSetElement
        {
            get { return _CriticalCodedValueSetElement; }
            set { _CriticalCodedValueSetElement = value; OnPropertyChanged("CriticalCodedValueSetElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _CriticalCodedValueSetElement;
        
        /// <summary>
        /// Value set of critical coded values for the observation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string CriticalCodedValueSet
        {
            get { return CriticalCodedValueSetElement != null ? CriticalCodedValueSetElement.Value : null; }
            set
            {
                if (value == null)
                  CriticalCodedValueSetElement = null; 
                else
                  CriticalCodedValueSetElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("CriticalCodedValueSet");
            }
        }
        

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
                if(Category != null) dest.Category = (Hl7.Fhir.Model.Coding)Category.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.Coding)Code.DeepCopy();
                if(PermittedDataType != null) dest.PermittedDataType = new List<Hl7.Fhir.Model.Coding>(PermittedDataType.DeepCopy());
                if(MultipleResultsAllowedElement != null) dest.MultipleResultsAllowedElement = (Hl7.Fhir.Model.FhirBoolean)MultipleResultsAllowedElement.DeepCopy();
                if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                if(PreferredReportNameElement != null) dest.PreferredReportNameElement = (Hl7.Fhir.Model.FhirString)PreferredReportNameElement.DeepCopy();
                if(QuantitativeDetails != null) dest.QuantitativeDetails = (Hl7.Fhir.Model.ObservationDefinition.QuantitativeDetailsComponent)QuantitativeDetails.DeepCopy();
                if(QualifiedInterval != null) dest.QualifiedInterval = new List<Hl7.Fhir.Model.ObservationDefinition.QualifiedIntervalComponent>(QualifiedInterval.DeepCopy());
                if(ValidCodedValueSetElement != null) dest.ValidCodedValueSetElement = (Hl7.Fhir.Model.FhirUri)ValidCodedValueSetElement.DeepCopy();
                if(NormalCodedValueSetElement != null) dest.NormalCodedValueSetElement = (Hl7.Fhir.Model.FhirUri)NormalCodedValueSetElement.DeepCopy();
                if(AbnormalCodedValueSetElement != null) dest.AbnormalCodedValueSetElement = (Hl7.Fhir.Model.FhirUri)AbnormalCodedValueSetElement.DeepCopy();
                if(CriticalCodedValueSetElement != null) dest.CriticalCodedValueSetElement = (Hl7.Fhir.Model.FhirUri)CriticalCodedValueSetElement.DeepCopy();
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
            if( !DeepComparable.Matches(PermittedDataType, otherT.PermittedDataType)) return false;
            if( !DeepComparable.Matches(MultipleResultsAllowedElement, otherT.MultipleResultsAllowedElement)) return false;
            if( !DeepComparable.Matches(Method, otherT.Method)) return false;
            if( !DeepComparable.Matches(PreferredReportNameElement, otherT.PreferredReportNameElement)) return false;
            if( !DeepComparable.Matches(QuantitativeDetails, otherT.QuantitativeDetails)) return false;
            if( !DeepComparable.Matches(QualifiedInterval, otherT.QualifiedInterval)) return false;
            if( !DeepComparable.Matches(ValidCodedValueSetElement, otherT.ValidCodedValueSetElement)) return false;
            if( !DeepComparable.Matches(NormalCodedValueSetElement, otherT.NormalCodedValueSetElement)) return false;
            if( !DeepComparable.Matches(AbnormalCodedValueSetElement, otherT.AbnormalCodedValueSetElement)) return false;
            if( !DeepComparable.Matches(CriticalCodedValueSetElement, otherT.CriticalCodedValueSetElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ObservationDefinition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(PermittedDataType, otherT.PermittedDataType)) return false;
            if( !DeepComparable.IsExactly(MultipleResultsAllowedElement, otherT.MultipleResultsAllowedElement)) return false;
            if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
            if( !DeepComparable.IsExactly(PreferredReportNameElement, otherT.PreferredReportNameElement)) return false;
            if( !DeepComparable.IsExactly(QuantitativeDetails, otherT.QuantitativeDetails)) return false;
            if( !DeepComparable.IsExactly(QualifiedInterval, otherT.QualifiedInterval)) return false;
            if( !DeepComparable.IsExactly(ValidCodedValueSetElement, otherT.ValidCodedValueSetElement)) return false;
            if( !DeepComparable.IsExactly(NormalCodedValueSetElement, otherT.NormalCodedValueSetElement)) return false;
            if( !DeepComparable.IsExactly(AbnormalCodedValueSetElement, otherT.AbnormalCodedValueSetElement)) return false;
            if( !DeepComparable.IsExactly(CriticalCodedValueSetElement, otherT.CriticalCodedValueSetElement)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Category != null) yield return Category;
				if (Code != null) yield return Code;
				foreach (var elem in PermittedDataType) { if (elem != null) yield return elem; }
				if (MultipleResultsAllowedElement != null) yield return MultipleResultsAllowedElement;
				if (Method != null) yield return Method;
				if (PreferredReportNameElement != null) yield return PreferredReportNameElement;
				if (QuantitativeDetails != null) yield return QuantitativeDetails;
				foreach (var elem in QualifiedInterval) { if (elem != null) yield return elem; }
				if (ValidCodedValueSetElement != null) yield return ValidCodedValueSetElement;
				if (NormalCodedValueSetElement != null) yield return NormalCodedValueSetElement;
				if (AbnormalCodedValueSetElement != null) yield return AbnormalCodedValueSetElement;
				if (CriticalCodedValueSetElement != null) yield return CriticalCodedValueSetElement;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Category != null) yield return new ElementValue("category", false, Category);
                if (Code != null) yield return new ElementValue("code", false, Code);
                foreach (var elem in PermittedDataType) { if (elem != null) yield return new ElementValue("permittedDataType", true, elem); }
                if (MultipleResultsAllowedElement != null) yield return new ElementValue("multipleResultsAllowed", false, MultipleResultsAllowedElement);
                if (Method != null) yield return new ElementValue("method", false, Method);
                if (PreferredReportNameElement != null) yield return new ElementValue("preferredReportName", false, PreferredReportNameElement);
                if (QuantitativeDetails != null) yield return new ElementValue("quantitativeDetails", false, QuantitativeDetails);
                foreach (var elem in QualifiedInterval) { if (elem != null) yield return new ElementValue("qualifiedInterval", true, elem); }
                if (ValidCodedValueSetElement != null) yield return new ElementValue("validCodedValueSet", false, ValidCodedValueSetElement);
                if (NormalCodedValueSetElement != null) yield return new ElementValue("normalCodedValueSet", false, NormalCodedValueSetElement);
                if (AbnormalCodedValueSetElement != null) yield return new ElementValue("abnormalCodedValueSet", false, AbnormalCodedValueSetElement);
                if (CriticalCodedValueSetElement != null) yield return new ElementValue("criticalCodedValueSet", false, CriticalCodedValueSetElement);
            }
        }

    }
    
}
