using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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
// Generated for FHIR v4.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A product used for nutritional purposes
    /// </summary>
    [FhirType("NutritionProduct", IsResource = true)]
    [DataContract]
    public partial class NutritionProduct : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.NutritionProduct; } }
        [NotMapped]
        public override string TypeName { get { return "NutritionProduct"; } }

        /// <summary>
        /// Codes identifying the lifecycle stage of a product.
        /// (url: http://hl7.org/fhir/ValueSet/nutritionproduct-status)
        /// </summary>
        [FhirEnumeration("NutritionProductStatus")]
        public enum NutritionProductStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/nutritionproduct-status)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/nutritionproduct-status"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/nutritionproduct-status)
            /// </summary>
            [EnumLiteral("inactive", "http://hl7.org/fhir/nutritionproduct-status"), Description("Inactive")]
            Inactive,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/nutritionproduct-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/nutritionproduct-status"), Description("Entered in Error")]
            EnteredInError,
        }

        [FhirType("NutrientComponent", NamedBackboneElement = true)]
        [DataContract]
        public partial class NutrientComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "NutrientComponent"; } }

            /// <summary>
            /// The (relevant) nutrients in the product
            /// </summary>
            [FhirElement("item", Order = 40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableReference Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }

            private Hl7.Fhir.Model.CodeableReference _Item;

            /// <summary>
            /// The amount of nutrient expressed in one or more units: X per pack / per serving / per dose
            /// </summary>
            [FhirElement("amount", Order = 50)]
            [Cardinality(Min = 0, Max = -1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Ratio> Amount
            {
                get { if (_Amount == null) _Amount = new List<Hl7.Fhir.Model.Ratio>(); return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }

            private List<Hl7.Fhir.Model.Ratio> _Amount;

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NutrientComponent;

                if (dest != null)
                {
                    base.CopyTo(dest);
                    if (Item != null) dest.Item = (Hl7.Fhir.Model.CodeableReference)Item.DeepCopy();
                    if (Amount != null) dest.Amount = new List<Hl7.Fhir.Model.Ratio>(Amount.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new NutrientComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NutrientComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(Item, otherT.Item)) return false;
                if (!DeepComparable.Matches(Amount, otherT.Amount)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NutrientComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(Item, otherT.Item)) return false;
                if (!DeepComparable.IsExactly(Amount, otherT.Amount)) return false;

                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Item != null) yield return Item;
                    foreach (var elem in Amount) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Item != null) yield return new ElementValue("item", Item);
                    foreach (var elem in Amount) { if (elem != null) yield return new ElementValue("amount", elem); }
                }
            }


        }


        [FhirType("IngredientComponent", NamedBackboneElement = true)]
        [DataContract]
        public partial class IngredientComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "IngredientComponent"; } }

            /// <summary>
            /// The ingredient contained in the product
            /// </summary>
            [FhirElement("item", InSummary = true, Order = 40)]
            [Cardinality(Min = 1, Max = 1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableReference Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }

            private Hl7.Fhir.Model.CodeableReference _Item;

            /// <summary>
            /// The amount of ingredient that is in the product
            /// </summary>
            [FhirElement("amount", InSummary = true, Order = 50)]
            [Cardinality(Min = 0, Max = -1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Ratio> Amount
            {
                get { if (_Amount == null) _Amount = new List<Hl7.Fhir.Model.Ratio>(); return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }

            private List<Hl7.Fhir.Model.Ratio> _Amount;

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as IngredientComponent;

                if (dest != null)
                {
                    base.CopyTo(dest);
                    if (Item != null) dest.Item = (Hl7.Fhir.Model.CodeableReference)Item.DeepCopy();
                    if (Amount != null) dest.Amount = new List<Hl7.Fhir.Model.Ratio>(Amount.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new IngredientComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as IngredientComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(Item, otherT.Item)) return false;
                if (!DeepComparable.Matches(Amount, otherT.Amount)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as IngredientComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(Item, otherT.Item)) return false;
                if (!DeepComparable.IsExactly(Amount, otherT.Amount)) return false;

                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Item != null) yield return Item;
                    foreach (var elem in Amount) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Item != null) yield return new ElementValue("item", Item);
                    foreach (var elem in Amount) { if (elem != null) yield return new ElementValue("amount", elem); }
                }
            }


        }


        [FhirType("ProductCharacteristicComponent", NamedBackboneElement = true)]
        [DataContract]
        public partial class ProductCharacteristicComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ProductCharacteristicComponent"; } }

            /// <summary>
            /// Code specifying the type of characteristic
            /// </summary>
            [FhirElement("type", Order = 40)]
            [Cardinality(Min = 1, Max = 1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }

            private Hl7.Fhir.Model.CodeableConcept _Type;

            /// <summary>
            /// The value of the characteristic
            /// </summary>
            [FhirElement("value", Order = 50, Choice = ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept), typeof(Hl7.Fhir.Model.FhirString), typeof(Hl7.Fhir.Model.Quantity), typeof(Hl7.Fhir.Model.Base64Binary), typeof(Hl7.Fhir.Model.Attachment), typeof(Hl7.Fhir.Model.FhirBoolean))]
            [Cardinality(Min = 1, Max = 1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }

            private Hl7.Fhir.Model.Element _Value;

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProductCharacteristicComponent;

                if (dest != null)
                {
                    base.CopyTo(dest);
                    if (Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if (Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProductCharacteristicComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProductCharacteristicComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(Type, otherT.Type)) return false;
                if (!DeepComparable.Matches(Value, otherT.Value)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProductCharacteristicComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if (!DeepComparable.IsExactly(Value, otherT.Value)) return false;

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
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }


        }


        [FhirType("InstanceComponent", NamedBackboneElement = true)]
        [DataContract]
        public partial class InstanceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "InstanceComponent"; } }

            /// <summary>
            /// The amount of items or instances
            /// </summary>
            [FhirElement("quantity", Order = 40)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }

            private Hl7.Fhir.Model.SimpleQuantity _Quantity;

            /// <summary>
            /// The identifier for the physical instance, typically a serial number
            /// </summary>
            [FhirElement("identifier", Order = 50)]
            [Cardinality(Min = 0, Max = -1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Identifier> Identifier
            {
                get { if (_Identifier == null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }

            private List<Hl7.Fhir.Model.Identifier> _Identifier;

            /// <summary>
            /// The identification of the batch or lot of the product
            /// </summary>
            [FhirElement("lotNumber", Order = 60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LotNumberElement
            {
                get { return _LotNumberElement; }
                set { _LotNumberElement = value; OnPropertyChanged("LotNumberElement"); }
            }

            private Hl7.Fhir.Model.FhirString _LotNumberElement;

            /// <summary>
            /// The identification of the batch or lot of the product
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string LotNumber
            {
                get { return LotNumberElement != null ? LotNumberElement.Value : null; }
                set
                {
                    if (value == null)
                        LotNumberElement = null;
                    else
                        LotNumberElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("LotNumber");
                }
            }

            /// <summary>
            /// The expiry date or date and time for the product
            /// </summary>
            [FhirElement("expiry", Order = 70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ExpiryElement
            {
                get { return _ExpiryElement; }
                set { _ExpiryElement = value; OnPropertyChanged("ExpiryElement"); }
            }

            private Hl7.Fhir.Model.FhirDateTime _ExpiryElement;

            /// <summary>
            /// The expiry date or date and time for the product
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Expiry
            {
                get { return ExpiryElement != null ? ExpiryElement.Value : null; }
                set
                {
                    if (value == null)
                        ExpiryElement = null;
                    else
                        ExpiryElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Expiry");
                }
            }

            /// <summary>
            /// The date until which the product is expected to be good for consumption
            /// </summary>
            [FhirElement("useBy", Order = 80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime UseByElement
            {
                get { return _UseByElement; }
                set { _UseByElement = value; OnPropertyChanged("UseByElement"); }
            }

            private Hl7.Fhir.Model.FhirDateTime _UseByElement;

            /// <summary>
            /// The date until which the product is expected to be good for consumption
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string UseBy
            {
                get { return UseByElement != null ? UseByElement.Value : null; }
                set
                {
                    if (value == null)
                        UseByElement = null;
                    else
                        UseByElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("UseBy");
                }
            }

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InstanceComponent;

                if (dest != null)
                {
                    base.CopyTo(dest);
                    if (Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                    if (LotNumberElement != null) dest.LotNumberElement = (Hl7.Fhir.Model.FhirString)LotNumberElement.DeepCopy();
                    if (ExpiryElement != null) dest.ExpiryElement = (Hl7.Fhir.Model.FhirDateTime)ExpiryElement.DeepCopy();
                    if (UseByElement != null) dest.UseByElement = (Hl7.Fhir.Model.FhirDateTime)UseByElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new InstanceComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InstanceComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if (!DeepComparable.Matches(LotNumberElement, otherT.LotNumberElement)) return false;
                if (!DeepComparable.Matches(ExpiryElement, otherT.ExpiryElement)) return false;
                if (!DeepComparable.Matches(UseByElement, otherT.UseByElement)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InstanceComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if (!DeepComparable.IsExactly(LotNumberElement, otherT.LotNumberElement)) return false;
                if (!DeepComparable.IsExactly(ExpiryElement, otherT.ExpiryElement)) return false;
                if (!DeepComparable.IsExactly(UseByElement, otherT.UseByElement)) return false;

                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Quantity != null) yield return Quantity;
                    foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                    if (LotNumberElement != null) yield return LotNumberElement;
                    if (ExpiryElement != null) yield return ExpiryElement;
                    if (UseByElement != null) yield return UseByElement;
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                    if (LotNumberElement != null) yield return new ElementValue("lotNumber", LotNumberElement);
                    if (ExpiryElement != null) yield return new ElementValue("expiry", ExpiryElement);
                    if (UseByElement != null) yield return new ElementValue("useBy", UseByElement);
                }
            }


        }


        /// <summary>
        /// active | inactive | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary = true, Order = 90)]
        [Cardinality(Min = 1, Max = 1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.NutritionProduct.NutritionProductStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }

        private Code<Hl7.Fhir.Model.NutritionProduct.NutritionProductStatus> _StatusElement;

        /// <summary>
        /// active | inactive | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.NutritionProduct.NutritionProductStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.NutritionProduct.NutritionProductStatus>(value);
                OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// A category or class of the nutrition product (halal, kosher, gluten free, vegan, etc)
        /// </summary>
        [FhirElement("category", InSummary = true, Order = 100)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if (_Category == null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }

        private List<Hl7.Fhir.Model.CodeableConcept> _Category;

        /// <summary>
        /// A code designating a specific type of nutritional product
        /// </summary>
        [FhirElement("code", InSummary = true, Order = 110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }

        private Hl7.Fhir.Model.CodeableConcept _Code;

        /// <summary>
        /// Manufacturer, representative or officially responsible for the product
        /// </summary>
        [FhirElement("manufacturer", InSummary = true, Order = 120)]
        [CLSCompliant(false)]
        [References("Organization")]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Manufacturer
        {
            get { if (_Manufacturer == null) _Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Manufacturer; }
            set { _Manufacturer = value; OnPropertyChanged("Manufacturer"); }
        }

        private List<Hl7.Fhir.Model.ResourceReference> _Manufacturer;

        /// <summary>
        /// The product's nutritional information expressed by the nutrients
        /// </summary>
        [FhirElement("nutrient", InSummary = true, Order = 130)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.NutritionProduct.NutrientComponent> Nutrient
        {
            get { if (_Nutrient == null) _Nutrient = new List<Hl7.Fhir.Model.NutritionProduct.NutrientComponent>(); return _Nutrient; }
            set { _Nutrient = value; OnPropertyChanged("Nutrient"); }
        }

        private List<Hl7.Fhir.Model.NutritionProduct.NutrientComponent> _Nutrient;

        /// <summary>
        /// Ingredients contained in this product
        /// </summary>
        [FhirElement("ingredient", Order = 140)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.NutritionProduct.IngredientComponent> Ingredient
        {
            get { if (_Ingredient == null) _Ingredient = new List<Hl7.Fhir.Model.NutritionProduct.IngredientComponent>(); return _Ingredient; }
            set { _Ingredient = value; OnPropertyChanged("Ingredient"); }
        }

        private List<Hl7.Fhir.Model.NutritionProduct.IngredientComponent> _Ingredient;

        /// <summary>
        /// Allergens that are known to be a part of this nutrition product
        /// </summary>
        [FhirElement("knownAllergen", Order = 150)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableReference> KnownAllergen
        {
            get { if (_KnownAllergen == null) _KnownAllergen = new List<Hl7.Fhir.Model.CodeableReference>(); return _KnownAllergen; }
            set { _KnownAllergen = value; OnPropertyChanged("KnownAllergen"); }
        }

        private List<Hl7.Fhir.Model.CodeableReference> _KnownAllergen;

        /// <summary>
        /// Specifies descriptive properties of the nutrition product
        /// </summary>
        [FhirElement("productCharacteristic", Order = 160)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.NutritionProduct.ProductCharacteristicComponent> ProductCharacteristic
        {
            get { if (_ProductCharacteristic == null) _ProductCharacteristic = new List<Hl7.Fhir.Model.NutritionProduct.ProductCharacteristicComponent>(); return _ProductCharacteristic; }
            set { _ProductCharacteristic = value; OnPropertyChanged("ProductCharacteristic"); }
        }

        private List<Hl7.Fhir.Model.NutritionProduct.ProductCharacteristicComponent> _ProductCharacteristic;

        /// <summary>
        /// One or several physical instances or occurrences of the nutrition product
        /// </summary>
        [FhirElement("instance", Order = 170)]
        [DataMember]
        public Hl7.Fhir.Model.NutritionProduct.InstanceComponent Instance
        {
            get { return _Instance; }
            set { _Instance = value; OnPropertyChanged("Instance"); }
        }

        private Hl7.Fhir.Model.NutritionProduct.InstanceComponent _Instance;

        /// <summary>
        /// Comments made about the product
        /// </summary>
        [FhirElement("note", Order = 180)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if (_Note == null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }

        private List<Hl7.Fhir.Model.Annotation> _Note;


        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as NutritionProduct;

            if (dest != null)
            {
                base.CopyTo(dest);
                if (StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.NutritionProduct.NutritionProductStatus>)StatusElement.DeepCopy();
                if (Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if (Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if (Manufacturer != null) dest.Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(Manufacturer.DeepCopy());
                if (Nutrient != null) dest.Nutrient = new List<Hl7.Fhir.Model.NutritionProduct.NutrientComponent>(Nutrient.DeepCopy());
                if (Ingredient != null) dest.Ingredient = new List<Hl7.Fhir.Model.NutritionProduct.IngredientComponent>(Ingredient.DeepCopy());
                if (KnownAllergen != null) dest.KnownAllergen = new List<Hl7.Fhir.Model.CodeableReference>(KnownAllergen.DeepCopy());
                if (ProductCharacteristic != null) dest.ProductCharacteristic = new List<Hl7.Fhir.Model.NutritionProduct.ProductCharacteristicComponent>(ProductCharacteristic.DeepCopy());
                if (Instance != null) dest.Instance = (Hl7.Fhir.Model.NutritionProduct.InstanceComponent)Instance.DeepCopy();
                if (Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new NutritionProduct());
        }

        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as NutritionProduct;
            if (otherT == null) return false;

            if (!base.Matches(otherT)) return false;
            if (!DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if (!DeepComparable.Matches(Category, otherT.Category)) return false;
            if (!DeepComparable.Matches(Code, otherT.Code)) return false;
            if (!DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
            if (!DeepComparable.Matches(Nutrient, otherT.Nutrient)) return false;
            if (!DeepComparable.Matches(Ingredient, otherT.Ingredient)) return false;
            if (!DeepComparable.Matches(KnownAllergen, otherT.KnownAllergen)) return false;
            if (!DeepComparable.Matches(ProductCharacteristic, otherT.ProductCharacteristic)) return false;
            if (!DeepComparable.Matches(Instance, otherT.Instance)) return false;
            if (!DeepComparable.Matches(Note, otherT.Note)) return false;

            return true;
        }

        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as NutritionProduct;
            if (otherT == null) return false;

            if (!base.IsExactly(otherT)) return false;
            if (!DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if (!DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if (!DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
            if (!DeepComparable.IsExactly(Nutrient, otherT.Nutrient)) return false;
            if (!DeepComparable.IsExactly(Ingredient, otherT.Ingredient)) return false;
            if (!DeepComparable.IsExactly(KnownAllergen, otherT.KnownAllergen)) return false;
            if (!DeepComparable.IsExactly(ProductCharacteristic, otherT.ProductCharacteristic)) return false;
            if (!DeepComparable.IsExactly(Instance, otherT.Instance)) return false;
            if (!DeepComparable.IsExactly(Note, otherT.Note)) return false;

            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (StatusElement != null) yield return StatusElement;
                foreach (var elem in Category) { if (elem != null) yield return elem; }
                if (Code != null) yield return Code;
                foreach (var elem in Manufacturer) { if (elem != null) yield return elem; }
                foreach (var elem in Nutrient) { if (elem != null) yield return elem; }
                foreach (var elem in Ingredient) { if (elem != null) yield return elem; }
                foreach (var elem in KnownAllergen) { if (elem != null) yield return elem; }
                foreach (var elem in ProductCharacteristic) { if (elem != null) yield return elem; }
                if (Instance != null) yield return Instance;
                foreach (var elem in Note) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        public override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (Code != null) yield return new ElementValue("code", Code);
                foreach (var elem in Manufacturer) { if (elem != null) yield return new ElementValue("manufacturer", elem); }
                foreach (var elem in Nutrient) { if (elem != null) yield return new ElementValue("nutrient", elem); }
                foreach (var elem in Ingredient) { if (elem != null) yield return new ElementValue("ingredient", elem); }
                foreach (var elem in KnownAllergen) { if (elem != null) yield return new ElementValue("knownAllergen", elem); }
                foreach (var elem in ProductCharacteristic) { if (elem != null) yield return new ElementValue("productCharacteristic", elem); }
                if (Instance != null) yield return new ElementValue("instance", Instance);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
            }
        }

    }

}
