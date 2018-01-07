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
// Generated for FHIR v3.2.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A detailed description of a device, typically as part of a regulated medicinal product. It is not intended to relace the Device resource, which covers use of device instances
    /// </summary>
    [FhirType("MedicinalProductDeviceSpec", IsResource=true)]
    [DataContract]
    public partial class MedicinalProductDeviceSpec : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicinalProductDeviceSpec; } }
        [NotMapped]
        public override string TypeName { get { return "MedicinalProductDeviceSpec"; } }
        
        [FhirType("MaterialComponent")]
        [DataContract]
        public partial class MaterialComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "MaterialComponent"; } }
            
            /// <summary>
            /// The substance
            /// </summary>
            [FhirElement("substance", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Substance
            {
                get { return _Substance; }
                set { _Substance = value; OnPropertyChanged("Substance"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Substance;
            
            /// <summary>
            /// Indicates an alternative material of the device
            /// </summary>
            [FhirElement("alternate", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AlternateElement
            {
                get { return _AlternateElement; }
                set { _AlternateElement = value; OnPropertyChanged("AlternateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AlternateElement;
            
            /// <summary>
            /// Indicates an alternative material of the device
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Alternate
            {
                get { return AlternateElement != null ? AlternateElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        AlternateElement = null; 
                    else
                        AlternateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Alternate");
                }
            }
            
            /// <summary>
            /// Whether the substance is a known or suspected allergen
            /// </summary>
            [FhirElement("allergenicIndicator", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AllergenicIndicatorElement
            {
                get { return _AllergenicIndicatorElement; }
                set { _AllergenicIndicatorElement = value; OnPropertyChanged("AllergenicIndicatorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AllergenicIndicatorElement;
            
            /// <summary>
            /// Whether the substance is a known or suspected allergen
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? AllergenicIndicator
            {
                get { return AllergenicIndicatorElement != null ? AllergenicIndicatorElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        AllergenicIndicatorElement = null; 
                    else
                        AllergenicIndicatorElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("AllergenicIndicator");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MaterialComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Substance != null) dest.Substance = (Hl7.Fhir.Model.CodeableConcept)Substance.DeepCopy();
                    if(AlternateElement != null) dest.AlternateElement = (Hl7.Fhir.Model.FhirBoolean)AlternateElement.DeepCopy();
                    if(AllergenicIndicatorElement != null) dest.AllergenicIndicatorElement = (Hl7.Fhir.Model.FhirBoolean)AllergenicIndicatorElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MaterialComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MaterialComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Substance, otherT.Substance)) return false;
                if( !DeepComparable.Matches(AlternateElement, otherT.AlternateElement)) return false;
                if( !DeepComparable.Matches(AllergenicIndicatorElement, otherT.AllergenicIndicatorElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MaterialComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Substance, otherT.Substance)) return false;
                if( !DeepComparable.IsExactly(AlternateElement, otherT.AlternateElement)) return false;
                if( !DeepComparable.IsExactly(AllergenicIndicatorElement, otherT.AllergenicIndicatorElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Substance != null) yield return Substance;
                    if (AlternateElement != null) yield return AlternateElement;
                    if (AllergenicIndicatorElement != null) yield return AllergenicIndicatorElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Substance != null) yield return new ElementValue("substance", false, Substance);
                    if (AlternateElement != null) yield return new ElementValue("alternate", false, AlternateElement);
                    if (AllergenicIndicatorElement != null) yield return new ElementValue("allergenicIndicator", false, AllergenicIndicatorElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business identifier
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// The type of device
        /// </summary>
        [FhirElement("type", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Trade name of the device, where applicable
        /// </summary>
        [FhirElement("tradeName", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TradeNameElement
        {
            get { return _TradeNameElement; }
            set { _TradeNameElement = value; OnPropertyChanged("TradeNameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TradeNameElement;
        
        /// <summary>
        /// Trade name of the device, where applicable
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string TradeName
        {
            get { return TradeNameElement != null ? TradeNameElement.Value : null; }
            set
            {
                if (value == null)
                  TradeNameElement = null; 
                else
                  TradeNameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("TradeName");
            }
        }
        
        /// <summary>
        /// The quantity of the device present in the packaging of a medicinal product
        /// </summary>
        [FhirElement("quantity", InSummary=true, Order=120)]
        [DataMember]
        public Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Quantity _Quantity;
        
        /// <summary>
        /// Device listing number
        /// </summary>
        [FhirElement("listingNumber", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ListingNumberElement
        {
            get { return _ListingNumberElement; }
            set { _ListingNumberElement = value; OnPropertyChanged("ListingNumberElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ListingNumberElement;
        
        /// <summary>
        /// Device listing number
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ListingNumber
        {
            get { return ListingNumberElement != null ? ListingNumberElement.Value : null; }
            set
            {
                if (value == null)
                  ListingNumberElement = null; 
                else
                  ListingNumberElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ListingNumber");
            }
        }
        
        /// <summary>
        /// Device model or reference number
        /// </summary>
        [FhirElement("modelNumber", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ModelNumberElement
        {
            get { return _ModelNumberElement; }
            set { _ModelNumberElement = value; OnPropertyChanged("ModelNumberElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ModelNumberElement;
        
        /// <summary>
        /// Device model or reference number
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ModelNumber
        {
            get { return ModelNumberElement != null ? ModelNumberElement.Value : null; }
            set
            {
                if (value == null)
                  ModelNumberElement = null; 
                else
                  ModelNumberElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ModelNumber");
            }
        }
        
        /// <summary>
        /// Whether the device is supplied as sterile
        /// </summary>
        [FhirElement("sterilityIndicator", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept SterilityIndicator
        {
            get { return _SterilityIndicator; }
            set { _SterilityIndicator = value; OnPropertyChanged("SterilityIndicator"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _SterilityIndicator;
        
        /// <summary>
        /// Whether the device must be sterilised before use
        /// </summary>
        [FhirElement("sterilisationRequirement", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept SterilisationRequirement
        {
            get { return _SterilisationRequirement; }
            set { _SterilisationRequirement = value; OnPropertyChanged("SterilisationRequirement"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _SterilisationRequirement;
        
        /// <summary>
        /// Usage pattern including the number of times that the device may be used
        /// </summary>
        [FhirElement("usage", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Usage
        {
            get { return _Usage; }
            set { _Usage = value; OnPropertyChanged("Usage"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Usage;
        
        /// <summary>
        /// A nomenclature term for the device
        /// </summary>
        [FhirElement("nomenclature", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Nomenclature
        {
            get { if(_Nomenclature==null) _Nomenclature = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Nomenclature; }
            set { _Nomenclature = value; OnPropertyChanged("Nomenclature"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Nomenclature;
        
        /// <summary>
        /// Shelf Life and storage information
        /// </summary>
        [FhirElement("shelfLife", InSummary=true, Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ProductShelfLife> ShelfLife
        {
            get { if(_ShelfLife==null) _ShelfLife = new List<ProductShelfLife>(); return _ShelfLife; }
            set { _ShelfLife = value; OnPropertyChanged("ShelfLife"); }
        }
        
        private List<ProductShelfLife> _ShelfLife;
        
        /// <summary>
        /// Dimensions, color etc
        /// </summary>
        [FhirElement("physicalCharacteristics", InSummary=true, Order=200)]
        [DataMember]
        public ProdCharacteristic PhysicalCharacteristics
        {
            get { return _PhysicalCharacteristics; }
            set { _PhysicalCharacteristics = value; OnPropertyChanged("PhysicalCharacteristics"); }
        }
        
        private ProdCharacteristic _PhysicalCharacteristics;
        
        /// <summary>
        /// Other codeable characteristics
        /// </summary>
        [FhirElement("otherCharacteristics", InSummary=true, Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> OtherCharacteristics
        {
            get { if(_OtherCharacteristics==null) _OtherCharacteristics = new List<Hl7.Fhir.Model.CodeableConcept>(); return _OtherCharacteristics; }
            set { _OtherCharacteristics = value; OnPropertyChanged("OtherCharacteristics"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _OtherCharacteristics;
        
        /// <summary>
        /// Batch number or expiry date of a device
        /// </summary>
        [FhirElement("batchIdentifier", InSummary=true, Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> BatchIdentifier
        {
            get { if(_BatchIdentifier==null) _BatchIdentifier = new List<Hl7.Fhir.Model.Identifier>(); return _BatchIdentifier; }
            set { _BatchIdentifier = value; OnPropertyChanged("BatchIdentifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _BatchIdentifier;
        
        /// <summary>
        /// Manufacturer of this Device
        /// </summary>
        [FhirElement("manufacturer", InSummary=true, Order=230)]
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
        /// A substance used to create the material(s) of which the device is made
        /// </summary>
        [FhirElement("material", InSummary=true, Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicinalProductDeviceSpec.MaterialComponent> Material
        {
            get { if(_Material==null) _Material = new List<Hl7.Fhir.Model.MedicinalProductDeviceSpec.MaterialComponent>(); return _Material; }
            set { _Material = value; OnPropertyChanged("Material"); }
        }
        
        private List<Hl7.Fhir.Model.MedicinalProductDeviceSpec.MaterialComponent> _Material;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicinalProductDeviceSpec;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(TradeNameElement != null) dest.TradeNameElement = (Hl7.Fhir.Model.FhirString)TradeNameElement.DeepCopy();
                if(Quantity != null) dest.Quantity = (Quantity)Quantity.DeepCopy();
                if(ListingNumberElement != null) dest.ListingNumberElement = (Hl7.Fhir.Model.FhirString)ListingNumberElement.DeepCopy();
                if(ModelNumberElement != null) dest.ModelNumberElement = (Hl7.Fhir.Model.FhirString)ModelNumberElement.DeepCopy();
                if(SterilityIndicator != null) dest.SterilityIndicator = (Hl7.Fhir.Model.CodeableConcept)SterilityIndicator.DeepCopy();
                if(SterilisationRequirement != null) dest.SterilisationRequirement = (Hl7.Fhir.Model.CodeableConcept)SterilisationRequirement.DeepCopy();
                if(Usage != null) dest.Usage = (Hl7.Fhir.Model.CodeableConcept)Usage.DeepCopy();
                if(Nomenclature != null) dest.Nomenclature = new List<Hl7.Fhir.Model.CodeableConcept>(Nomenclature.DeepCopy());
                if(ShelfLife != null) dest.ShelfLife = new List<ProductShelfLife>(ShelfLife.DeepCopy());
                if(PhysicalCharacteristics != null) dest.PhysicalCharacteristics = (ProdCharacteristic)PhysicalCharacteristics.DeepCopy();
                if(OtherCharacteristics != null) dest.OtherCharacteristics = new List<Hl7.Fhir.Model.CodeableConcept>(OtherCharacteristics.DeepCopy());
                if(BatchIdentifier != null) dest.BatchIdentifier = new List<Hl7.Fhir.Model.Identifier>(BatchIdentifier.DeepCopy());
                if(Manufacturer != null) dest.Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(Manufacturer.DeepCopy());
                if(Material != null) dest.Material = new List<Hl7.Fhir.Model.MedicinalProductDeviceSpec.MaterialComponent>(Material.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new MedicinalProductDeviceSpec());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicinalProductDeviceSpec;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(TradeNameElement, otherT.TradeNameElement)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(ListingNumberElement, otherT.ListingNumberElement)) return false;
            if( !DeepComparable.Matches(ModelNumberElement, otherT.ModelNumberElement)) return false;
            if( !DeepComparable.Matches(SterilityIndicator, otherT.SterilityIndicator)) return false;
            if( !DeepComparable.Matches(SterilisationRequirement, otherT.SterilisationRequirement)) return false;
            if( !DeepComparable.Matches(Usage, otherT.Usage)) return false;
            if( !DeepComparable.Matches(Nomenclature, otherT.Nomenclature)) return false;
            if( !DeepComparable.Matches(ShelfLife, otherT.ShelfLife)) return false;
            if( !DeepComparable.Matches(PhysicalCharacteristics, otherT.PhysicalCharacteristics)) return false;
            if( !DeepComparable.Matches(OtherCharacteristics, otherT.OtherCharacteristics)) return false;
            if( !DeepComparable.Matches(BatchIdentifier, otherT.BatchIdentifier)) return false;
            if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.Matches(Material, otherT.Material)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicinalProductDeviceSpec;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(TradeNameElement, otherT.TradeNameElement)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(ListingNumberElement, otherT.ListingNumberElement)) return false;
            if( !DeepComparable.IsExactly(ModelNumberElement, otherT.ModelNumberElement)) return false;
            if( !DeepComparable.IsExactly(SterilityIndicator, otherT.SterilityIndicator)) return false;
            if( !DeepComparable.IsExactly(SterilisationRequirement, otherT.SterilisationRequirement)) return false;
            if( !DeepComparable.IsExactly(Usage, otherT.Usage)) return false;
            if( !DeepComparable.IsExactly(Nomenclature, otherT.Nomenclature)) return false;
            if( !DeepComparable.IsExactly(ShelfLife, otherT.ShelfLife)) return false;
            if( !DeepComparable.IsExactly(PhysicalCharacteristics, otherT.PhysicalCharacteristics)) return false;
            if( !DeepComparable.IsExactly(OtherCharacteristics, otherT.OtherCharacteristics)) return false;
            if( !DeepComparable.IsExactly(BatchIdentifier, otherT.BatchIdentifier)) return false;
            if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.IsExactly(Material, otherT.Material)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Identifier != null) yield return Identifier;
				if (Type != null) yield return Type;
				if (TradeNameElement != null) yield return TradeNameElement;
				if (Quantity != null) yield return Quantity;
				if (ListingNumberElement != null) yield return ListingNumberElement;
				if (ModelNumberElement != null) yield return ModelNumberElement;
				if (SterilityIndicator != null) yield return SterilityIndicator;
				if (SterilisationRequirement != null) yield return SterilisationRequirement;
				if (Usage != null) yield return Usage;
				foreach (var elem in Nomenclature) { if (elem != null) yield return elem; }
				foreach (var elem in ShelfLife) { if (elem != null) yield return elem; }
				if (PhysicalCharacteristics != null) yield return PhysicalCharacteristics;
				foreach (var elem in OtherCharacteristics) { if (elem != null) yield return elem; }
				foreach (var elem in BatchIdentifier) { if (elem != null) yield return elem; }
				foreach (var elem in Manufacturer) { if (elem != null) yield return elem; }
				foreach (var elem in Material) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", false, Identifier);
                if (Type != null) yield return new ElementValue("type", false, Type);
                if (TradeNameElement != null) yield return new ElementValue("tradeName", false, TradeNameElement);
                if (Quantity != null) yield return new ElementValue("quantity", false, Quantity);
                if (ListingNumberElement != null) yield return new ElementValue("listingNumber", false, ListingNumberElement);
                if (ModelNumberElement != null) yield return new ElementValue("modelNumber", false, ModelNumberElement);
                if (SterilityIndicator != null) yield return new ElementValue("sterilityIndicator", false, SterilityIndicator);
                if (SterilisationRequirement != null) yield return new ElementValue("sterilisationRequirement", false, SterilisationRequirement);
                if (Usage != null) yield return new ElementValue("usage", false, Usage);
                foreach (var elem in Nomenclature) { if (elem != null) yield return new ElementValue("nomenclature", true, elem); }
                foreach (var elem in ShelfLife) { if (elem != null) yield return new ElementValue("shelfLife", true, elem); }
                if (PhysicalCharacteristics != null) yield return new ElementValue("physicalCharacteristics", false, PhysicalCharacteristics);
                foreach (var elem in OtherCharacteristics) { if (elem != null) yield return new ElementValue("otherCharacteristics", true, elem); }
                foreach (var elem in BatchIdentifier) { if (elem != null) yield return new ElementValue("batchIdentifier", true, elem); }
                foreach (var elem in Manufacturer) { if (elem != null) yield return new ElementValue("manufacturer", true, elem); }
                foreach (var elem in Material) { if (elem != null) yield return new ElementValue("material", true, elem); }
            }
        }

    }
    
}
