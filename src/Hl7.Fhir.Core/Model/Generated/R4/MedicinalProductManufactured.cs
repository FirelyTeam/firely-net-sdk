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
    /// The manufactured item as contained in the packaged medicinal product
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "MedicinalProductManufactured", IsResource=true)]
    [DataContract]
    public partial class MedicinalProductManufactured : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicinalProductManufactured; } }
        [NotMapped]
        public override string TypeName { get { return "MedicinalProductManufactured"; } }
    
        
        /// <summary>
        /// Dose form as manufactured and before any transformation into the pharmaceutical product
        /// </summary>
        [FhirElement("manufacturedDoseForm", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
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
        [FhirElement("unitOfPresentation", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept UnitOfPresentation
        {
            get { return _UnitOfPresentation; }
            set { _UnitOfPresentation = value; OnPropertyChanged("UnitOfPresentation"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _UnitOfPresentation;
        
        /// <summary>
        /// The quantity or "count number" of the manufactured item
        /// </summary>
        [FhirElement("quantity", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Hl7.Fhir.Model.Quantity _Quantity;
        
        /// <summary>
        /// Manufacturer of the item (Note that this should be named "manufacturer" but it currently causes technical issues)
        /// </summary>
        [FhirElement("manufacturer", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
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
        /// Ingredient
        /// </summary>
        [FhirElement("ingredient", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("MedicinalProductIngredient")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Ingredient
        {
            get { if(_Ingredient==null) _Ingredient = new List<Hl7.Fhir.Model.ResourceReference>(); return _Ingredient; }
            set { _Ingredient = value; OnPropertyChanged("Ingredient"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Ingredient;
        
        /// <summary>
        /// Dimensions, color etc.
        /// </summary>
        [FhirElement("physicalCharacteristics", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.ProdCharacteristic PhysicalCharacteristics
        {
            get { return _PhysicalCharacteristics; }
            set { _PhysicalCharacteristics = value; OnPropertyChanged("PhysicalCharacteristics"); }
        }
        
        private Hl7.Fhir.Model.ProdCharacteristic _PhysicalCharacteristics;
        
        /// <summary>
        /// Other codeable characteristics
        /// </summary>
        [FhirElement("otherCharacteristics", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> OtherCharacteristics
        {
            get { if(_OtherCharacteristics==null) _OtherCharacteristics = new List<Hl7.Fhir.Model.CodeableConcept>(); return _OtherCharacteristics; }
            set { _OtherCharacteristics = value; OnPropertyChanged("OtherCharacteristics"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _OtherCharacteristics;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicinalProductManufactured;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(ManufacturedDoseForm != null) dest.ManufacturedDoseForm = (Hl7.Fhir.Model.CodeableConcept)ManufacturedDoseForm.DeepCopy();
                if(UnitOfPresentation != null) dest.UnitOfPresentation = (Hl7.Fhir.Model.CodeableConcept)UnitOfPresentation.DeepCopy();
                if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                if(Manufacturer != null) dest.Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(Manufacturer.DeepCopy());
                if(Ingredient != null) dest.Ingredient = new List<Hl7.Fhir.Model.ResourceReference>(Ingredient.DeepCopy());
                if(PhysicalCharacteristics != null) dest.PhysicalCharacteristics = (Hl7.Fhir.Model.ProdCharacteristic)PhysicalCharacteristics.DeepCopy();
                if(OtherCharacteristics != null) dest.OtherCharacteristics = new List<Hl7.Fhir.Model.CodeableConcept>(OtherCharacteristics.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new MedicinalProductManufactured());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicinalProductManufactured;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(ManufacturedDoseForm, otherT.ManufacturedDoseForm)) return false;
            if( !DeepComparable.Matches(UnitOfPresentation, otherT.UnitOfPresentation)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.Matches(Ingredient, otherT.Ingredient)) return false;
            if( !DeepComparable.Matches(PhysicalCharacteristics, otherT.PhysicalCharacteristics)) return false;
            if( !DeepComparable.Matches(OtherCharacteristics, otherT.OtherCharacteristics)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicinalProductManufactured;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(ManufacturedDoseForm, otherT.ManufacturedDoseForm)) return false;
            if( !DeepComparable.IsExactly(UnitOfPresentation, otherT.UnitOfPresentation)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.IsExactly(Ingredient, otherT.Ingredient)) return false;
            if( !DeepComparable.IsExactly(PhysicalCharacteristics, otherT.PhysicalCharacteristics)) return false;
            if( !DeepComparable.IsExactly(OtherCharacteristics, otherT.OtherCharacteristics)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MedicinalProductManufactured");
            base.Serialize(sink);
            sink.Element("manufacturedDoseForm", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ManufacturedDoseForm?.Serialize(sink);
            sink.Element("unitOfPresentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UnitOfPresentation?.Serialize(sink);
            sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Quantity?.Serialize(sink);
            sink.BeginList("manufacturer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Manufacturer)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("ingredient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Ingredient)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("physicalCharacteristics", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PhysicalCharacteristics?.Serialize(sink);
            sink.BeginList("otherCharacteristics", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in OtherCharacteristics)
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
                case "manufacturedDoseForm":
                    ManufacturedDoseForm = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "unitOfPresentation":
                    UnitOfPresentation = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "quantity":
                    Quantity = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "manufacturer":
                    Manufacturer = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "ingredient":
                    Ingredient = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "physicalCharacteristics":
                    PhysicalCharacteristics = source.Get<Hl7.Fhir.Model.ProdCharacteristic>();
                    return true;
                case "otherCharacteristics":
                    OtherCharacteristics = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
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
                case "manufacturedDoseForm":
                    ManufacturedDoseForm = source.Populate(ManufacturedDoseForm);
                    return true;
                case "unitOfPresentation":
                    UnitOfPresentation = source.Populate(UnitOfPresentation);
                    return true;
                case "quantity":
                    Quantity = source.Populate(Quantity);
                    return true;
                case "manufacturer":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "ingredient":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "physicalCharacteristics":
                    PhysicalCharacteristics = source.Populate(PhysicalCharacteristics);
                    return true;
                case "otherCharacteristics":
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
                case "manufacturer":
                    source.PopulateListItem(Manufacturer, index);
                    return true;
                case "ingredient":
                    source.PopulateListItem(Ingredient, index);
                    return true;
                case "otherCharacteristics":
                    source.PopulateListItem(OtherCharacteristics, index);
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
                if (ManufacturedDoseForm != null) yield return ManufacturedDoseForm;
                if (UnitOfPresentation != null) yield return UnitOfPresentation;
                if (Quantity != null) yield return Quantity;
                foreach (var elem in Manufacturer) { if (elem != null) yield return elem; }
                foreach (var elem in Ingredient) { if (elem != null) yield return elem; }
                if (PhysicalCharacteristics != null) yield return PhysicalCharacteristics;
                foreach (var elem in OtherCharacteristics) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (ManufacturedDoseForm != null) yield return new ElementValue("manufacturedDoseForm", ManufacturedDoseForm);
                if (UnitOfPresentation != null) yield return new ElementValue("unitOfPresentation", UnitOfPresentation);
                if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                foreach (var elem in Manufacturer) { if (elem != null) yield return new ElementValue("manufacturer", elem); }
                foreach (var elem in Ingredient) { if (elem != null) yield return new ElementValue("ingredient", elem); }
                if (PhysicalCharacteristics != null) yield return new ElementValue("physicalCharacteristics", PhysicalCharacteristics);
                foreach (var elem in OtherCharacteristics) { if (elem != null) yield return new ElementValue("otherCharacteristics", elem); }
            }
        }
    
    }

}
