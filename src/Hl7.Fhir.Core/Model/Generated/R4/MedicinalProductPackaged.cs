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
    /// A medicinal product in a container or package
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "MedicinalProductPackaged", IsResource=true)]
    [DataContract]
    public partial class MedicinalProductPackaged : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicinalProductPackaged; } }
        [NotMapped]
        public override string TypeName { get { return "MedicinalProductPackaged"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "BatchIdentifierComponent")]
        [DataContract]
        public partial class BatchIdentifierComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "BatchIdentifierComponent"; } }
            
            /// <summary>
            /// A number appearing on the outer packaging of a specific batch
            /// </summary>
            [FhirElement("outerPackaging", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier OuterPackaging
            {
                get { return _OuterPackaging; }
                set { _OuterPackaging = value; OnPropertyChanged("OuterPackaging"); }
            }
            
            private Hl7.Fhir.Model.Identifier _OuterPackaging;
            
            /// <summary>
            /// A number appearing on the immediate packaging (and not the outer packaging)
            /// </summary>
            [FhirElement("immediatePackaging", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier ImmediatePackaging
            {
                get { return _ImmediatePackaging; }
                set { _ImmediatePackaging = value; OnPropertyChanged("ImmediatePackaging"); }
            }
            
            private Hl7.Fhir.Model.Identifier _ImmediatePackaging;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("BatchIdentifierComponent");
                base.Serialize(sink);
                sink.Element("outerPackaging", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); OuterPackaging?.Serialize(sink);
                sink.Element("immediatePackaging", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ImmediatePackaging?.Serialize(sink);
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
                    case "outerPackaging":
                        OuterPackaging = source.Get<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "immediatePackaging":
                        ImmediatePackaging = source.Get<Hl7.Fhir.Model.Identifier>();
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
                    case "outerPackaging":
                        OuterPackaging = source.Populate(OuterPackaging);
                        return true;
                    case "immediatePackaging":
                        ImmediatePackaging = source.Populate(ImmediatePackaging);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BatchIdentifierComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(OuterPackaging != null) dest.OuterPackaging = (Hl7.Fhir.Model.Identifier)OuterPackaging.DeepCopy();
                    if(ImmediatePackaging != null) dest.ImmediatePackaging = (Hl7.Fhir.Model.Identifier)ImmediatePackaging.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new BatchIdentifierComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BatchIdentifierComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(OuterPackaging, otherT.OuterPackaging)) return false;
                if( !DeepComparable.Matches(ImmediatePackaging, otherT.ImmediatePackaging)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BatchIdentifierComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(OuterPackaging, otherT.OuterPackaging)) return false;
                if( !DeepComparable.IsExactly(ImmediatePackaging, otherT.ImmediatePackaging)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (OuterPackaging != null) yield return OuterPackaging;
                    if (ImmediatePackaging != null) yield return ImmediatePackaging;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (OuterPackaging != null) yield return new ElementValue("outerPackaging", OuterPackaging);
                    if (ImmediatePackaging != null) yield return new ElementValue("immediatePackaging", ImmediatePackaging);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "PackageItemComponent")]
        [DataContract]
        public partial class PackageItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PackageItemComponent"; } }
            
            /// <summary>
            /// Including possibly Data Carrier Identifier
            /// </summary>
            [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
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
            /// The physical type of the container of the medicine
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// The quantity of this package in the medicinal product, at the current level of packaging. The outermost is always 1
            /// </summary>
            [FhirElement("quantity", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
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
            /// Material type of the package item
            /// </summary>
            [FhirElement("material", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Material
            {
                get { if(_Material==null) _Material = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Material; }
                set { _Material = value; OnPropertyChanged("Material"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Material;
            
            /// <summary>
            /// A possible alternate material for the packaging
            /// </summary>
            [FhirElement("alternateMaterial", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> AlternateMaterial
            {
                get { if(_AlternateMaterial==null) _AlternateMaterial = new List<Hl7.Fhir.Model.CodeableConcept>(); return _AlternateMaterial; }
                set { _AlternateMaterial = value; OnPropertyChanged("AlternateMaterial"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _AlternateMaterial;
            
            /// <summary>
            /// A device accompanying a medicinal product
            /// </summary>
            [FhirElement("device", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [References("DeviceDefinition")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Device
            {
                get { if(_Device==null) _Device = new List<Hl7.Fhir.Model.ResourceReference>(); return _Device; }
                set { _Device = value; OnPropertyChanged("Device"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Device;
            
            /// <summary>
            /// The manufactured item as contained in the packaged medicinal product
            /// </summary>
            [FhirElement("manufacturedItem", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [References("MedicinalProductManufactured")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> ManufacturedItem
            {
                get { if(_ManufacturedItem==null) _ManufacturedItem = new List<Hl7.Fhir.Model.ResourceReference>(); return _ManufacturedItem; }
                set { _ManufacturedItem = value; OnPropertyChanged("ManufacturedItem"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _ManufacturedItem;
            
            /// <summary>
            /// Allows containers within containers
            /// </summary>
            [FhirElement("packageItem", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<PackageItemComponent> PackageItem
            {
                get { if(_PackageItem==null) _PackageItem = new List<PackageItemComponent>(); return _PackageItem; }
                set { _PackageItem = value; OnPropertyChanged("PackageItem"); }
            }
            
            private List<PackageItemComponent> _PackageItem;
            
            /// <summary>
            /// Dimensions, color etc.
            /// </summary>
            [FhirElement("physicalCharacteristics", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
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
            [FhirElement("otherCharacteristics", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> OtherCharacteristics
            {
                get { if(_OtherCharacteristics==null) _OtherCharacteristics = new List<Hl7.Fhir.Model.CodeableConcept>(); return _OtherCharacteristics; }
                set { _OtherCharacteristics = value; OnPropertyChanged("OtherCharacteristics"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _OtherCharacteristics;
            
            /// <summary>
            /// Shelf Life and storage information
            /// </summary>
            [FhirElement("shelfLifeStorage", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ProductShelfLife> ShelfLifeStorage
            {
                get { if(_ShelfLifeStorage==null) _ShelfLifeStorage = new List<Hl7.Fhir.Model.ProductShelfLife>(); return _ShelfLifeStorage; }
                set { _ShelfLifeStorage = value; OnPropertyChanged("ShelfLifeStorage"); }
            }
            
            private List<Hl7.Fhir.Model.ProductShelfLife> _ShelfLifeStorage;
            
            /// <summary>
            /// Manufacturer of this Package Item
            /// </summary>
            [FhirElement("manufacturer", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PackageItemComponent");
                base.Serialize(sink);
                sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Identifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Type?.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Quantity?.Serialize(sink);
                sink.BeginList("material", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Material)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("alternateMaterial", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in AlternateMaterial)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("device", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Device)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("manufacturedItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in ManufacturedItem)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("packageItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in PackageItem)
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
                sink.BeginList("shelfLifeStorage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in ShelfLifeStorage)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("manufacturer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Manufacturer)
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.Quantity>();
                        return true;
                    case "material":
                        Material = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "alternateMaterial":
                        AlternateMaterial = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "device":
                        Device = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "manufacturedItem":
                        ManufacturedItem = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "packageItem":
                        PackageItem = source.GetList<PackageItemComponent>();
                        return true;
                    case "physicalCharacteristics":
                        PhysicalCharacteristics = source.Get<Hl7.Fhir.Model.ProdCharacteristic>();
                        return true;
                    case "otherCharacteristics":
                        OtherCharacteristics = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "shelfLifeStorage":
                        ShelfLifeStorage = source.GetList<Hl7.Fhir.Model.ProductShelfLife>();
                        return true;
                    case "manufacturer":
                        Manufacturer = source.GetList<Hl7.Fhir.Model.ResourceReference>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "material":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "alternateMaterial":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "device":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "manufacturedItem":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "packageItem":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "physicalCharacteristics":
                        PhysicalCharacteristics = source.Populate(PhysicalCharacteristics);
                        return true;
                    case "otherCharacteristics":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "shelfLifeStorage":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "manufacturer":
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
                    case "material":
                        source.PopulateListItem(Material, index);
                        return true;
                    case "alternateMaterial":
                        source.PopulateListItem(AlternateMaterial, index);
                        return true;
                    case "device":
                        source.PopulateListItem(Device, index);
                        return true;
                    case "manufacturedItem":
                        source.PopulateListItem(ManufacturedItem, index);
                        return true;
                    case "packageItem":
                        source.PopulateListItem(PackageItem, index);
                        return true;
                    case "otherCharacteristics":
                        source.PopulateListItem(OtherCharacteristics, index);
                        return true;
                    case "shelfLifeStorage":
                        source.PopulateListItem(ShelfLifeStorage, index);
                        return true;
                    case "manufacturer":
                        source.PopulateListItem(Manufacturer, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PackageItemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(Material != null) dest.Material = new List<Hl7.Fhir.Model.CodeableConcept>(Material.DeepCopy());
                    if(AlternateMaterial != null) dest.AlternateMaterial = new List<Hl7.Fhir.Model.CodeableConcept>(AlternateMaterial.DeepCopy());
                    if(Device != null) dest.Device = new List<Hl7.Fhir.Model.ResourceReference>(Device.DeepCopy());
                    if(ManufacturedItem != null) dest.ManufacturedItem = new List<Hl7.Fhir.Model.ResourceReference>(ManufacturedItem.DeepCopy());
                    if(PackageItem != null) dest.PackageItem = new List<PackageItemComponent>(PackageItem.DeepCopy());
                    if(PhysicalCharacteristics != null) dest.PhysicalCharacteristics = (Hl7.Fhir.Model.ProdCharacteristic)PhysicalCharacteristics.DeepCopy();
                    if(OtherCharacteristics != null) dest.OtherCharacteristics = new List<Hl7.Fhir.Model.CodeableConcept>(OtherCharacteristics.DeepCopy());
                    if(ShelfLifeStorage != null) dest.ShelfLifeStorage = new List<Hl7.Fhir.Model.ProductShelfLife>(ShelfLifeStorage.DeepCopy());
                    if(Manufacturer != null) dest.Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(Manufacturer.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PackageItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PackageItemComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Material, otherT.Material)) return false;
                if( !DeepComparable.Matches(AlternateMaterial, otherT.AlternateMaterial)) return false;
                if( !DeepComparable.Matches(Device, otherT.Device)) return false;
                if( !DeepComparable.Matches(ManufacturedItem, otherT.ManufacturedItem)) return false;
                if( !DeepComparable.Matches(PackageItem, otherT.PackageItem)) return false;
                if( !DeepComparable.Matches(PhysicalCharacteristics, otherT.PhysicalCharacteristics)) return false;
                if( !DeepComparable.Matches(OtherCharacteristics, otherT.OtherCharacteristics)) return false;
                if( !DeepComparable.Matches(ShelfLifeStorage, otherT.ShelfLifeStorage)) return false;
                if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PackageItemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Material, otherT.Material)) return false;
                if( !DeepComparable.IsExactly(AlternateMaterial, otherT.AlternateMaterial)) return false;
                if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
                if( !DeepComparable.IsExactly(ManufacturedItem, otherT.ManufacturedItem)) return false;
                if( !DeepComparable.IsExactly(PackageItem, otherT.PackageItem)) return false;
                if( !DeepComparable.IsExactly(PhysicalCharacteristics, otherT.PhysicalCharacteristics)) return false;
                if( !DeepComparable.IsExactly(OtherCharacteristics, otherT.OtherCharacteristics)) return false;
                if( !DeepComparable.IsExactly(ShelfLifeStorage, otherT.ShelfLifeStorage)) return false;
                if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                    if (Type != null) yield return Type;
                    if (Quantity != null) yield return Quantity;
                    foreach (var elem in Material) { if (elem != null) yield return elem; }
                    foreach (var elem in AlternateMaterial) { if (elem != null) yield return elem; }
                    foreach (var elem in Device) { if (elem != null) yield return elem; }
                    foreach (var elem in ManufacturedItem) { if (elem != null) yield return elem; }
                    foreach (var elem in PackageItem) { if (elem != null) yield return elem; }
                    if (PhysicalCharacteristics != null) yield return PhysicalCharacteristics;
                    foreach (var elem in OtherCharacteristics) { if (elem != null) yield return elem; }
                    foreach (var elem in ShelfLifeStorage) { if (elem != null) yield return elem; }
                    foreach (var elem in Manufacturer) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    foreach (var elem in Material) { if (elem != null) yield return new ElementValue("material", elem); }
                    foreach (var elem in AlternateMaterial) { if (elem != null) yield return new ElementValue("alternateMaterial", elem); }
                    foreach (var elem in Device) { if (elem != null) yield return new ElementValue("device", elem); }
                    foreach (var elem in ManufacturedItem) { if (elem != null) yield return new ElementValue("manufacturedItem", elem); }
                    foreach (var elem in PackageItem) { if (elem != null) yield return new ElementValue("packageItem", elem); }
                    if (PhysicalCharacteristics != null) yield return new ElementValue("physicalCharacteristics", PhysicalCharacteristics);
                    foreach (var elem in OtherCharacteristics) { if (elem != null) yield return new ElementValue("otherCharacteristics", elem); }
                    foreach (var elem in ShelfLifeStorage) { if (elem != null) yield return new ElementValue("shelfLifeStorage", elem); }
                    foreach (var elem in Manufacturer) { if (elem != null) yield return new ElementValue("manufacturer", elem); }
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Unique identifier
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
        /// The product with this is a pack for
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [References("MedicinalProduct")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Subject
        {
            get { if(_Subject==null) _Subject = new List<Hl7.Fhir.Model.ResourceReference>(); return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Subject;
        
        /// <summary>
        /// Textual description
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Textual description
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if (value == null)
                    DescriptionElement = null;
                else
                    DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// The legal status of supply of the medicinal product as classified by the regulator
        /// </summary>
        [FhirElement("legalStatusOfSupply", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept LegalStatusOfSupply
        {
            get { return _LegalStatusOfSupply; }
            set { _LegalStatusOfSupply = value; OnPropertyChanged("LegalStatusOfSupply"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _LegalStatusOfSupply;
        
        /// <summary>
        /// Marketing information
        /// </summary>
        [FhirElement("marketingStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MarketingStatus> MarketingStatus
        {
            get { if(_MarketingStatus==null) _MarketingStatus = new List<Hl7.Fhir.Model.MarketingStatus>(); return _MarketingStatus; }
            set { _MarketingStatus = value; OnPropertyChanged("MarketingStatus"); }
        }
        
        private List<Hl7.Fhir.Model.MarketingStatus> _MarketingStatus;
        
        /// <summary>
        /// Manufacturer of this Package Item
        /// </summary>
        [FhirElement("marketingAuthorization", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("MedicinalProductAuthorization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference MarketingAuthorization
        {
            get { return _MarketingAuthorization; }
            set { _MarketingAuthorization = value; OnPropertyChanged("MarketingAuthorization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _MarketingAuthorization;
        
        /// <summary>
        /// Manufacturer of this Package Item
        /// </summary>
        [FhirElement("manufacturer", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
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
        /// Batch numbering
        /// </summary>
        [FhirElement("batchIdentifier", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<BatchIdentifierComponent> BatchIdentifier
        {
            get { if(_BatchIdentifier==null) _BatchIdentifier = new List<BatchIdentifierComponent>(); return _BatchIdentifier; }
            set { _BatchIdentifier = value; OnPropertyChanged("BatchIdentifier"); }
        }
        
        private List<BatchIdentifierComponent> _BatchIdentifier;
        
        /// <summary>
        /// A packaging item, as a contained for medicine, possibly with other packaging items within
        /// </summary>
        [FhirElement("packageItem", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<PackageItemComponent> PackageItem
        {
            get { if(_PackageItem==null) _PackageItem = new List<PackageItemComponent>(); return _PackageItem; }
            set { _PackageItem = value; OnPropertyChanged("PackageItem"); }
        }
        
        private List<PackageItemComponent> _PackageItem;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicinalProductPackaged;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Subject != null) dest.Subject = new List<Hl7.Fhir.Model.ResourceReference>(Subject.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(LegalStatusOfSupply != null) dest.LegalStatusOfSupply = (Hl7.Fhir.Model.CodeableConcept)LegalStatusOfSupply.DeepCopy();
                if(MarketingStatus != null) dest.MarketingStatus = new List<Hl7.Fhir.Model.MarketingStatus>(MarketingStatus.DeepCopy());
                if(MarketingAuthorization != null) dest.MarketingAuthorization = (Hl7.Fhir.Model.ResourceReference)MarketingAuthorization.DeepCopy();
                if(Manufacturer != null) dest.Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(Manufacturer.DeepCopy());
                if(BatchIdentifier != null) dest.BatchIdentifier = new List<BatchIdentifierComponent>(BatchIdentifier.DeepCopy());
                if(PackageItem != null) dest.PackageItem = new List<PackageItemComponent>(PackageItem.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new MedicinalProductPackaged());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicinalProductPackaged;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(LegalStatusOfSupply, otherT.LegalStatusOfSupply)) return false;
            if( !DeepComparable.Matches(MarketingStatus, otherT.MarketingStatus)) return false;
            if( !DeepComparable.Matches(MarketingAuthorization, otherT.MarketingAuthorization)) return false;
            if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.Matches(BatchIdentifier, otherT.BatchIdentifier)) return false;
            if( !DeepComparable.Matches(PackageItem, otherT.PackageItem)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicinalProductPackaged;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(LegalStatusOfSupply, otherT.LegalStatusOfSupply)) return false;
            if( !DeepComparable.IsExactly(MarketingStatus, otherT.MarketingStatus)) return false;
            if( !DeepComparable.IsExactly(MarketingAuthorization, otherT.MarketingAuthorization)) return false;
            if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.IsExactly(BatchIdentifier, otherT.BatchIdentifier)) return false;
            if( !DeepComparable.IsExactly(PackageItem, otherT.PackageItem)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MedicinalProductPackaged");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Subject)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
            sink.Element("legalStatusOfSupply", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LegalStatusOfSupply?.Serialize(sink);
            sink.BeginList("marketingStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in MarketingStatus)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("marketingAuthorization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MarketingAuthorization?.Serialize(sink);
            sink.BeginList("manufacturer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Manufacturer)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("batchIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in BatchIdentifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("packageItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
            foreach(var item in PackageItem)
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
                case "subject":
                    Subject = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "legalStatusOfSupply":
                    LegalStatusOfSupply = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "marketingStatus":
                    MarketingStatus = source.GetList<Hl7.Fhir.Model.MarketingStatus>();
                    return true;
                case "marketingAuthorization":
                    MarketingAuthorization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "manufacturer":
                    Manufacturer = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "batchIdentifier":
                    BatchIdentifier = source.GetList<BatchIdentifierComponent>();
                    return true;
                case "packageItem":
                    PackageItem = source.GetList<PackageItemComponent>();
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
                case "subject":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "legalStatusOfSupply":
                    LegalStatusOfSupply = source.Populate(LegalStatusOfSupply);
                    return true;
                case "marketingStatus":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "marketingAuthorization":
                    MarketingAuthorization = source.Populate(MarketingAuthorization);
                    return true;
                case "manufacturer":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "batchIdentifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "packageItem":
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
                case "subject":
                    source.PopulateListItem(Subject, index);
                    return true;
                case "marketingStatus":
                    source.PopulateListItem(MarketingStatus, index);
                    return true;
                case "manufacturer":
                    source.PopulateListItem(Manufacturer, index);
                    return true;
                case "batchIdentifier":
                    source.PopulateListItem(BatchIdentifier, index);
                    return true;
                case "packageItem":
                    source.PopulateListItem(PackageItem, index);
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
                foreach (var elem in Subject) { if (elem != null) yield return elem; }
                if (DescriptionElement != null) yield return DescriptionElement;
                if (LegalStatusOfSupply != null) yield return LegalStatusOfSupply;
                foreach (var elem in MarketingStatus) { if (elem != null) yield return elem; }
                if (MarketingAuthorization != null) yield return MarketingAuthorization;
                foreach (var elem in Manufacturer) { if (elem != null) yield return elem; }
                foreach (var elem in BatchIdentifier) { if (elem != null) yield return elem; }
                foreach (var elem in PackageItem) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in Subject) { if (elem != null) yield return new ElementValue("subject", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (LegalStatusOfSupply != null) yield return new ElementValue("legalStatusOfSupply", LegalStatusOfSupply);
                foreach (var elem in MarketingStatus) { if (elem != null) yield return new ElementValue("marketingStatus", elem); }
                if (MarketingAuthorization != null) yield return new ElementValue("marketingAuthorization", MarketingAuthorization);
                foreach (var elem in Manufacturer) { if (elem != null) yield return new ElementValue("manufacturer", elem); }
                foreach (var elem in BatchIdentifier) { if (elem != null) yield return new ElementValue("batchIdentifier", elem); }
                foreach (var elem in PackageItem) { if (elem != null) yield return new ElementValue("packageItem", elem); }
            }
        }
    
    }

}
