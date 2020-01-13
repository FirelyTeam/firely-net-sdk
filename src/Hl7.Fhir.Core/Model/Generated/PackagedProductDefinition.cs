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
    /// A medicinal product in a container or package
    /// </summary>
    [FhirType("PackagedProductDefinition", IsResource=true)]
    [DataContract]
    public partial class PackagedProductDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.PackagedProductDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "PackagedProductDefinition"; } }
        
        [FhirType("BatchIdentifierComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class BatchIdentifierComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BatchIdentifierComponent"; } }
            
            /// <summary>
            /// A number appearing on the outer packaging of a specific batch
            /// </summary>
            [FhirElement("outerPackaging", InSummary=true, Order=40)]
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
            [FhirElement("immediatePackaging", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier ImmediatePackaging
            {
                get { return _ImmediatePackaging; }
                set { _ImmediatePackaging = value; OnPropertyChanged("ImmediatePackaging"); }
            }
            
            private Hl7.Fhir.Model.Identifier _ImmediatePackaging;
            
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
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (OuterPackaging != null) yield return new ElementValue("outerPackaging", OuterPackaging);
                    if (ImmediatePackaging != null) yield return new ElementValue("immediatePackaging", ImmediatePackaging);
                }
            }

            
        }
        
        
        [FhirType("PackageComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PackageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PackageComponent"; } }
            
            /// <summary>
            /// Including possibly Data Carrier Identifier
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
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
            [FhirElement("type", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// The quantity of this package in the medicinal product, at the current level of packaging. If specified, the outermost level is always 1
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=60)]
            [DataMember]
            public Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Quantity _Quantity;
            
            /// <summary>
            /// Material type of the package item
            /// </summary>
            [FhirElement("material", InSummary=true, Order=70)]
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
            [FhirElement("alternateMaterial", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> AlternateMaterial
            {
                get { if(_AlternateMaterial==null) _AlternateMaterial = new List<Hl7.Fhir.Model.CodeableConcept>(); return _AlternateMaterial; }
                set { _AlternateMaterial = value; OnPropertyChanged("AlternateMaterial"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _AlternateMaterial;
            
            /// <summary>
            /// Shelf Life and storage information
            /// </summary>
            [FhirElement("shelfLifeStorage", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ProductShelfLife> ShelfLifeStorage
            {
                get { if(_ShelfLifeStorage==null) _ShelfLifeStorage = new List<ProductShelfLife>(); return _ShelfLifeStorage; }
                set { _ShelfLifeStorage = value; OnPropertyChanged("ShelfLifeStorage"); }
            }
            
            private List<ProductShelfLife> _ShelfLifeStorage;
            
            /// <summary>
            /// Manufacturer of this Package Item
            /// </summary>
            [FhirElement("manufacturer", InSummary=true, Order=100)]
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
            /// General characteristics of this item
            /// </summary>
            [FhirElement("characteristic", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PackagedProductDefinition.CharacteristicComponent> Characteristic
            {
                get { if(_Characteristic==null) _Characteristic = new List<Hl7.Fhir.Model.PackagedProductDefinition.CharacteristicComponent>(); return _Characteristic; }
                set { _Characteristic = value; OnPropertyChanged("Characteristic"); }
            }
            
            private List<Hl7.Fhir.Model.PackagedProductDefinition.CharacteristicComponent> _Characteristic;
            
            /// <summary>
            /// The item(s) within the packaging
            /// </summary>
            [FhirElement("containedItem", InSummary=true, Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PackagedProductDefinition.ContainedItemComponent> ContainedItem
            {
                get { if(_ContainedItem==null) _ContainedItem = new List<Hl7.Fhir.Model.PackagedProductDefinition.ContainedItemComponent>(); return _ContainedItem; }
                set { _ContainedItem = value; OnPropertyChanged("ContainedItem"); }
            }
            
            private List<Hl7.Fhir.Model.PackagedProductDefinition.ContainedItemComponent> _ContainedItem;
            
            /// <summary>
            /// Allows containers within containers
            /// </summary>
            [FhirElement("package", InSummary=true, Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PackagedProductDefinition.PackageComponent> Package
            {
                get { if(_Package==null) _Package = new List<Hl7.Fhir.Model.PackagedProductDefinition.PackageComponent>(); return _Package; }
                set { _Package = value; OnPropertyChanged("Package"); }
            }
            
            private List<Hl7.Fhir.Model.PackagedProductDefinition.PackageComponent> _Package;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PackageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Quantity)Quantity.DeepCopy();
                    if(Material != null) dest.Material = new List<Hl7.Fhir.Model.CodeableConcept>(Material.DeepCopy());
                    if(AlternateMaterial != null) dest.AlternateMaterial = new List<Hl7.Fhir.Model.CodeableConcept>(AlternateMaterial.DeepCopy());
                    if(ShelfLifeStorage != null) dest.ShelfLifeStorage = new List<ProductShelfLife>(ShelfLifeStorage.DeepCopy());
                    if(Manufacturer != null) dest.Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(Manufacturer.DeepCopy());
                    if(Characteristic != null) dest.Characteristic = new List<Hl7.Fhir.Model.PackagedProductDefinition.CharacteristicComponent>(Characteristic.DeepCopy());
                    if(ContainedItem != null) dest.ContainedItem = new List<Hl7.Fhir.Model.PackagedProductDefinition.ContainedItemComponent>(ContainedItem.DeepCopy());
                    if(Package != null) dest.Package = new List<Hl7.Fhir.Model.PackagedProductDefinition.PackageComponent>(Package.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PackageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PackageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Material, otherT.Material)) return false;
                if( !DeepComparable.Matches(AlternateMaterial, otherT.AlternateMaterial)) return false;
                if( !DeepComparable.Matches(ShelfLifeStorage, otherT.ShelfLifeStorage)) return false;
                if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
                if( !DeepComparable.Matches(Characteristic, otherT.Characteristic)) return false;
                if( !DeepComparable.Matches(ContainedItem, otherT.ContainedItem)) return false;
                if( !DeepComparable.Matches(Package, otherT.Package)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PackageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Material, otherT.Material)) return false;
                if( !DeepComparable.IsExactly(AlternateMaterial, otherT.AlternateMaterial)) return false;
                if( !DeepComparable.IsExactly(ShelfLifeStorage, otherT.ShelfLifeStorage)) return false;
                if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
                if( !DeepComparable.IsExactly(Characteristic, otherT.Characteristic)) return false;
                if( !DeepComparable.IsExactly(ContainedItem, otherT.ContainedItem)) return false;
                if( !DeepComparable.IsExactly(Package, otherT.Package)) return false;
                
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
                    foreach (var elem in ShelfLifeStorage) { if (elem != null) yield return elem; }
                    foreach (var elem in Manufacturer) { if (elem != null) yield return elem; }
                    foreach (var elem in Characteristic) { if (elem != null) yield return elem; }
                    foreach (var elem in ContainedItem) { if (elem != null) yield return elem; }
                    foreach (var elem in Package) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    foreach (var elem in Material) { if (elem != null) yield return new ElementValue("material", elem); }
                    foreach (var elem in AlternateMaterial) { if (elem != null) yield return new ElementValue("alternateMaterial", elem); }
                    foreach (var elem in ShelfLifeStorage) { if (elem != null) yield return new ElementValue("shelfLifeStorage", elem); }
                    foreach (var elem in Manufacturer) { if (elem != null) yield return new ElementValue("manufacturer", elem); }
                    foreach (var elem in Characteristic) { if (elem != null) yield return new ElementValue("characteristic", elem); }
                    foreach (var elem in ContainedItem) { if (elem != null) yield return new ElementValue("containedItem", elem); }
                    foreach (var elem in Package) { if (elem != null) yield return new ElementValue("package", elem); }
                }
            }

            
        }
        
        
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
        
        
        [FhirType("ContainedItemComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ContainedItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContainedItemComponent"; } }
            
            /// <summary>
            /// The manufactured item or device as contained in the packaged medicinal product
            /// </summary>
            [FhirElement("item", InSummary=true, Order=40)]
            [CLSCompliant(false)]
			[References("ManufacturedItemDefinition","DeviceDefinition")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Item
            {
                get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.ResourceReference>(); return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Item;
            
            /// <summary>
            /// The number of this type of item within this packaging
            /// </summary>
            [FhirElement("amount", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Quantity),typeof(Hl7.Fhir.Model.Integer))]
            [DataMember]
            public Hl7.Fhir.Model.Element Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.Element _Amount;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContainedItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Item != null) dest.Item = new List<Hl7.Fhir.Model.ResourceReference>(Item.DeepCopy());
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.Element)Amount.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContainedItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContainedItemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContainedItemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Item) { if (elem != null) yield return elem; }
                    if (Amount != null) yield return Amount;
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                    if (Amount != null) yield return new ElementValue("amount", Amount);
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
        /// The product that this is a pack for
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=100)]
        [CLSCompliant(false)]
		[References("MedicinalProductDefinition")]
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
        [FhirElement("description", InSummary=true, Order=110)]
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
        [FhirElement("legalStatusOfSupply", InSummary=true, Order=120)]
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
        [FhirElement("marketingStatus", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<MarketingStatus> MarketingStatus
        {
            get { if(_MarketingStatus==null) _MarketingStatus = new List<MarketingStatus>(); return _MarketingStatus; }
            set { _MarketingStatus = value; OnPropertyChanged("MarketingStatus"); }
        }
        
        private List<MarketingStatus> _MarketingStatus;
        
        /// <summary>
        /// States whether a drug product is supplied with another item such as a diluent or adjuvant
        /// </summary>
        [FhirElement("copackagedIndicator", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean CopackagedIndicatorElement
        {
            get { return _CopackagedIndicatorElement; }
            set { _CopackagedIndicatorElement = value; OnPropertyChanged("CopackagedIndicatorElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _CopackagedIndicatorElement;
        
        /// <summary>
        /// States whether a drug product is supplied with another item such as a diluent or adjuvant
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? CopackagedIndicator
        {
            get { return CopackagedIndicatorElement != null ? CopackagedIndicatorElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  CopackagedIndicatorElement = null; 
                else
                  CopackagedIndicatorElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("CopackagedIndicator");
            }
        }
        
        /// <summary>
        /// Manufacturer of this Package Item
        /// </summary>
        [FhirElement("marketingAuthorization", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("RegulatedAuthorization")]
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
        [FhirElement("manufacturer", InSummary=true, Order=160)]
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
        [FhirElement("batchIdentifier", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.PackagedProductDefinition.BatchIdentifierComponent> BatchIdentifier
        {
            get { if(_BatchIdentifier==null) _BatchIdentifier = new List<Hl7.Fhir.Model.PackagedProductDefinition.BatchIdentifierComponent>(); return _BatchIdentifier; }
            set { _BatchIdentifier = value; OnPropertyChanged("BatchIdentifier"); }
        }
        
        private List<Hl7.Fhir.Model.PackagedProductDefinition.BatchIdentifierComponent> _BatchIdentifier;
        
        /// <summary>
        /// A packaging item, as a contained for medicine, possibly with other packaging items within
        /// </summary>
        [FhirElement("package", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.PackagedProductDefinition.PackageComponent> Package
        {
            get { if(_Package==null) _Package = new List<Hl7.Fhir.Model.PackagedProductDefinition.PackageComponent>(); return _Package; }
            set { _Package = value; OnPropertyChanged("Package"); }
        }
        
        private List<Hl7.Fhir.Model.PackagedProductDefinition.PackageComponent> _Package;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as PackagedProductDefinition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Subject != null) dest.Subject = new List<Hl7.Fhir.Model.ResourceReference>(Subject.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(LegalStatusOfSupply != null) dest.LegalStatusOfSupply = (Hl7.Fhir.Model.CodeableConcept)LegalStatusOfSupply.DeepCopy();
                if(MarketingStatus != null) dest.MarketingStatus = new List<MarketingStatus>(MarketingStatus.DeepCopy());
                if(CopackagedIndicatorElement != null) dest.CopackagedIndicatorElement = (Hl7.Fhir.Model.FhirBoolean)CopackagedIndicatorElement.DeepCopy();
                if(MarketingAuthorization != null) dest.MarketingAuthorization = (Hl7.Fhir.Model.ResourceReference)MarketingAuthorization.DeepCopy();
                if(Manufacturer != null) dest.Manufacturer = new List<Hl7.Fhir.Model.ResourceReference>(Manufacturer.DeepCopy());
                if(BatchIdentifier != null) dest.BatchIdentifier = new List<Hl7.Fhir.Model.PackagedProductDefinition.BatchIdentifierComponent>(BatchIdentifier.DeepCopy());
                if(Package != null) dest.Package = new List<Hl7.Fhir.Model.PackagedProductDefinition.PackageComponent>(Package.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new PackagedProductDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as PackagedProductDefinition;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(LegalStatusOfSupply, otherT.LegalStatusOfSupply)) return false;
            if( !DeepComparable.Matches(MarketingStatus, otherT.MarketingStatus)) return false;
            if( !DeepComparable.Matches(CopackagedIndicatorElement, otherT.CopackagedIndicatorElement)) return false;
            if( !DeepComparable.Matches(MarketingAuthorization, otherT.MarketingAuthorization)) return false;
            if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.Matches(BatchIdentifier, otherT.BatchIdentifier)) return false;
            if( !DeepComparable.Matches(Package, otherT.Package)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as PackagedProductDefinition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(LegalStatusOfSupply, otherT.LegalStatusOfSupply)) return false;
            if( !DeepComparable.IsExactly(MarketingStatus, otherT.MarketingStatus)) return false;
            if( !DeepComparable.IsExactly(CopackagedIndicatorElement, otherT.CopackagedIndicatorElement)) return false;
            if( !DeepComparable.IsExactly(MarketingAuthorization, otherT.MarketingAuthorization)) return false;
            if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.IsExactly(BatchIdentifier, otherT.BatchIdentifier)) return false;
            if( !DeepComparable.IsExactly(Package, otherT.Package)) return false;
            
            return true;
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
				if (CopackagedIndicatorElement != null) yield return CopackagedIndicatorElement;
				if (MarketingAuthorization != null) yield return MarketingAuthorization;
				foreach (var elem in Manufacturer) { if (elem != null) yield return elem; }
				foreach (var elem in BatchIdentifier) { if (elem != null) yield return elem; }
				foreach (var elem in Package) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        public override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in Subject) { if (elem != null) yield return new ElementValue("subject", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (LegalStatusOfSupply != null) yield return new ElementValue("legalStatusOfSupply", LegalStatusOfSupply);
                foreach (var elem in MarketingStatus) { if (elem != null) yield return new ElementValue("marketingStatus", elem); }
                if (CopackagedIndicatorElement != null) yield return new ElementValue("copackagedIndicator", CopackagedIndicatorElement);
                if (MarketingAuthorization != null) yield return new ElementValue("marketingAuthorization", MarketingAuthorization);
                foreach (var elem in Manufacturer) { if (elem != null) yield return new ElementValue("manufacturer", elem); }
                foreach (var elem in BatchIdentifier) { if (elem != null) yield return new ElementValue("batchIdentifier", elem); }
                foreach (var elem in Package) { if (elem != null) yield return new ElementValue("package", elem); }
            }
        }

    }
    
}
