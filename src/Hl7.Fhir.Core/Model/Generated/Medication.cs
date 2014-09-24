using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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

//
// Generated on Tue, Jul 15, 2014 15:45+0200 for FHIR v0.0.81
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Definition of a Medication
    /// </summary>
    [FhirType("Medication", IsResource=true)]
    [DataContract]
    public partial class Medication : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Whether the medication is a product or a package
        /// </summary>
        [FhirEnumeration("MedicationKind")]
        public enum MedicationKind
        {
            /// <summary>
            /// The medication is a product.
            /// </summary>
            [EnumLiteral("product")]
            Product,
            /// <summary>
            /// The medication is a package - a contained group of one of more products.
            /// </summary>
            [EnumLiteral("package")]
            Package,
        }
        
        [FhirType("MedicationPackageContentComponent")]
        [DataContract]
        public partial class MedicationPackageContentComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// A product in the package
            /// </summary>
            [FhirElement("item", InSummary=true, Order=40)]
            [References("Medication")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Item;
            
            /// <summary>
            /// How many are in the package?
            /// </summary>
            [FhirElement("amount", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            private Hl7.Fhir.Model.Quantity _Amount;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MedicationPackageContentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Item != null) dest.Item = (Hl7.Fhir.Model.ResourceReference)Item.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.Quantity)Amount.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MedicationPackageContentComponent());
            }
            
        }
        
        
        [FhirType("MedicationPackageComponent")]
        [DataContract]
        public partial class MedicationPackageComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// E.g. box, vial, blister-pack
            /// </summary>
            [FhirElement("container", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Container
            {
                get { return _Container; }
                set { _Container = value; OnPropertyChanged("Container"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Container;
            
            /// <summary>
            /// What is  in the package?
            /// </summary>
            [FhirElement("content", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Medication.MedicationPackageContentComponent> Content
            {
                get { return _Content; }
                set { _Content = value; OnPropertyChanged("Content"); }
            }
            private List<Hl7.Fhir.Model.Medication.MedicationPackageContentComponent> _Content;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MedicationPackageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Container != null) dest.Container = (Hl7.Fhir.Model.CodeableConcept)Container.DeepCopy();
                    if(Content != null) dest.Content = new List<Hl7.Fhir.Model.Medication.MedicationPackageContentComponent>(Content.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MedicationPackageComponent());
            }
            
        }
        
        
        [FhirType("MedicationProductIngredientComponent")]
        [DataContract]
        public partial class MedicationProductIngredientComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// The product contained
            /// </summary>
            [FhirElement("item", InSummary=true, Order=40)]
            [References("Substance","Medication")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Item;
            
            /// <summary>
            /// How much ingredient in product
            /// </summary>
            [FhirElement("amount", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            private Hl7.Fhir.Model.Ratio _Amount;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MedicationProductIngredientComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Item != null) dest.Item = (Hl7.Fhir.Model.ResourceReference)Item.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.Ratio)Amount.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MedicationProductIngredientComponent());
            }
            
        }
        
        
        [FhirType("MedicationProductComponent")]
        [DataContract]
        public partial class MedicationProductComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// powder | tablets | carton +
            /// </summary>
            [FhirElement("form", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Form
            {
                get { return _Form; }
                set { _Form = value; OnPropertyChanged("Form"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Form;
            
            /// <summary>
            /// Active or inactive ingredient
            /// </summary>
            [FhirElement("ingredient", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Medication.MedicationProductIngredientComponent> Ingredient
            {
                get { return _Ingredient; }
                set { _Ingredient = value; OnPropertyChanged("Ingredient"); }
            }
            private List<Hl7.Fhir.Model.Medication.MedicationProductIngredientComponent> _Ingredient;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MedicationProductComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Form != null) dest.Form = (Hl7.Fhir.Model.CodeableConcept)Form.DeepCopy();
                    if(Ingredient != null) dest.Ingredient = new List<Hl7.Fhir.Model.Medication.MedicationProductIngredientComponent>(Ingredient.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MedicationProductComponent());
            }
            
        }
        
        
        /// <summary>
        /// Common / Commercial name
        /// </summary>
        [FhirElement("name", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Common / Commercial name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if(value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Codes that identify this medication
        /// </summary>
        [FhirElement("code", InSummary=true, Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// True if a brand
        /// </summary>
        [FhirElement("isBrand", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean IsBrandElement
        {
            get { return _IsBrandElement; }
            set { _IsBrandElement = value; OnPropertyChanged("IsBrandElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _IsBrandElement;
        
        /// <summary>
        /// True if a brand
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? IsBrand
        {
            get { return IsBrandElement != null ? IsBrandElement.Value : null; }
            set
            {
                if(value == null)
                  IsBrandElement = null; 
                else
                  IsBrandElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("IsBrand");
            }
        }
        
        /// <summary>
        /// Manufacturer of the item
        /// </summary>
        [FhirElement("manufacturer", InSummary=true, Order=100)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Manufacturer
        {
            get { return _Manufacturer; }
            set { _Manufacturer = value; OnPropertyChanged("Manufacturer"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Manufacturer;
        
        /// <summary>
        /// product | package
        /// </summary>
        [FhirElement("kind", InSummary=true, Order=110)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Medication.MedicationKind> KindElement
        {
            get { return _KindElement; }
            set { _KindElement = value; OnPropertyChanged("KindElement"); }
        }
        private Code<Hl7.Fhir.Model.Medication.MedicationKind> _KindElement;
        
        /// <summary>
        /// product | package
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Medication.MedicationKind? Kind
        {
            get { return KindElement != null ? KindElement.Value : null; }
            set
            {
                if(value == null)
                  KindElement = null; 
                else
                  KindElement = new Code<Hl7.Fhir.Model.Medication.MedicationKind>(value);
                OnPropertyChanged("Kind");
            }
        }
        
        /// <summary>
        /// Administrable medication details
        /// </summary>
        [FhirElement("product", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Medication.MedicationProductComponent Product
        {
            get { return _Product; }
            set { _Product = value; OnPropertyChanged("Product"); }
        }
        private Hl7.Fhir.Model.Medication.MedicationProductComponent _Product;
        
        /// <summary>
        /// Details about packaged medications
        /// </summary>
        [FhirElement("package", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Medication.MedicationPackageComponent Package
        {
            get { return _Package; }
            set { _Package = value; OnPropertyChanged("Package"); }
        }
        private Hl7.Fhir.Model.Medication.MedicationPackageComponent _Package;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Medication;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(IsBrandElement != null) dest.IsBrandElement = (Hl7.Fhir.Model.FhirBoolean)IsBrandElement.DeepCopy();
                if(Manufacturer != null) dest.Manufacturer = (Hl7.Fhir.Model.ResourceReference)Manufacturer.DeepCopy();
                if(KindElement != null) dest.KindElement = (Code<Hl7.Fhir.Model.Medication.MedicationKind>)KindElement.DeepCopy();
                if(Product != null) dest.Product = (Hl7.Fhir.Model.Medication.MedicationProductComponent)Product.DeepCopy();
                if(Package != null) dest.Package = (Hl7.Fhir.Model.Medication.MedicationPackageComponent)Package.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Medication());
        }
        
    }
    
}
