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
// Generated for FHIR v4.2.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// The definition and characteristics of a medicinal manufactured item, such as a tablet or capsule, as contained in a packaged medicinal product
    /// </summary>
    [FhirType("ManufacturedItemDefinition", IsResource=true)]
    [DataContract]
    public partial class ManufacturedItemDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ManufacturedItemDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "ManufacturedItemDefinition"; } }
        
        [FhirType("CharacteristicComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class CharacteristicComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CharacteristicComponent"; } }
            
            /// <summary>
            /// A code expressing the type of characteristic
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// A value for the characteristic
            /// </summary>
            [FhirElement("value", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Coding),typeof(Quantity),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Attachment))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CharacteristicComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
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
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CharacteristicComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
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
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }

            
        }
        
        
        /// <summary>
        /// Unique identifier
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Dose form as manufactured and before any transformation into the pharmaceutical product
        /// </summary>
        [FhirElement("manufacturedDoseForm", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ManufacturedDoseForm
        {
            get { return _ManufacturedDoseForm; }
            set { _ManufacturedDoseForm = value; OnPropertyChanged("ManufacturedDoseForm"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ManufacturedDoseForm;
        
        /// <summary>
        /// The “real world” units in which the quantity of the manufactured item is described
        /// </summary>
        [FhirElement("unitOfPresentation", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept UnitOfPresentation
        {
            get { return _UnitOfPresentation; }
            set { _UnitOfPresentation = value; OnPropertyChanged("UnitOfPresentation"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _UnitOfPresentation;
        
        /// <summary>
        /// Manufacturer of the item (Note that this should be named "manufacturer" but it currently causes technical issues)
        /// </summary>
        [FhirElement("manufacturer", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References("Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Manufacturer
        {
            get { if(_Manufacturer==null) _Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Manufacturer; }
            set { _Manufacturer = value; OnPropertyChanged("Manufacturer"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Manufacturer;
        
        /// <summary>
        /// The ingredients that make up this manufactured item
        /// </summary>
        [FhirElement("ingredient", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Ingredient")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Ingredient
        {
            get { if(_Ingredient==null) _Ingredient = new List<Hl7.Fhir.Model.ResourceReference>(); return _Ingredient; }
            set { _Ingredient = value; OnPropertyChanged("Ingredient"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Ingredient;
        
        /// <summary>
        /// General characteristics of this item
        /// </summary>
        [FhirElement("characteristic", InSummary=true, Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ManufacturedItemDefinition.CharacteristicComponent> Characteristic
        {
            get { if(_Characteristic==null) _Characteristic = new List<Hl7.Fhir.Model.ManufacturedItemDefinition.CharacteristicComponent>(); return _Characteristic; }
            set { _Characteristic = value; OnPropertyChanged("Characteristic"); }
        }
        
        private List<Hl7.Fhir.Model.ManufacturedItemDefinition.CharacteristicComponent> _Characteristic;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ManufacturedItemDefinition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(ManufacturedDoseForm != null) dest.ManufacturedDoseForm = (Hl7.Fhir.Model.CodeableConcept)ManufacturedDoseForm.DeepCopy();
                if(UnitOfPresentation != null) dest.UnitOfPresentation = (Hl7.Fhir.Model.CodeableConcept)UnitOfPresentation.DeepCopy();
                if(Manufacturer != null) dest.Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(Manufacturer.DeepCopy());
                if(Ingredient != null) dest.Ingredient = new List<Hl7.Fhir.Model.ResourceReference>(Ingredient.DeepCopy());
                if(Characteristic != null) dest.Characteristic = new List<Hl7.Fhir.Model.ManufacturedItemDefinition.CharacteristicComponent>(Characteristic.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ManufacturedItemDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ManufacturedItemDefinition;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(ManufacturedDoseForm, otherT.ManufacturedDoseForm)) return false;
            if( !DeepComparable.Matches(UnitOfPresentation, otherT.UnitOfPresentation)) return false;
            if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.Matches(Ingredient, otherT.Ingredient)) return false;
            if( !DeepComparable.Matches(Characteristic, otherT.Characteristic)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ManufacturedItemDefinition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(ManufacturedDoseForm, otherT.ManufacturedDoseForm)) return false;
            if( !DeepComparable.IsExactly(UnitOfPresentation, otherT.UnitOfPresentation)) return false;
            if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.IsExactly(Ingredient, otherT.Ingredient)) return false;
            if( !DeepComparable.IsExactly(Characteristic, otherT.Characteristic)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (ManufacturedDoseForm != null) yield return ManufacturedDoseForm;
				if (UnitOfPresentation != null) yield return UnitOfPresentation;
				foreach (var elem in Manufacturer) { if (elem != null) yield return elem; }
				foreach (var elem in Ingredient) { if (elem != null) yield return elem; }
				foreach (var elem in Characteristic) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        public override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (ManufacturedDoseForm != null) yield return new ElementValue("manufacturedDoseForm", ManufacturedDoseForm);
                if (UnitOfPresentation != null) yield return new ElementValue("unitOfPresentation", UnitOfPresentation);
                foreach (var elem in Manufacturer) { if (elem != null) yield return new ElementValue("manufacturer", elem); }
                foreach (var elem in Ingredient) { if (elem != null) yield return new ElementValue("ingredient", elem); }
                foreach (var elem in Characteristic) { if (elem != null) yield return new ElementValue("characteristic", elem); }
            }
        }

    }
    
}
