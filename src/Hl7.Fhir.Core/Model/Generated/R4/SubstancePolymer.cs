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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// Todo
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "SubstancePolymer", IsResource=true)]
    [DataContract]
    public partial class SubstancePolymer : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SubstancePolymer; } }
        [NotMapped]
        public override string TypeName { get { return "SubstancePolymer"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "MonomerSetComponent")]
        [DataContract]
        public partial class MonomerSetComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "MonomerSetComponent"; } }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("ratioType", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept RatioType
            {
                get { return _RatioType; }
                set { _RatioType = value; OnPropertyChanged("RatioType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _RatioType;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("startingMaterial", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<StartingMaterialComponent> StartingMaterial
            {
                get { if(_StartingMaterial==null) _StartingMaterial = new List<StartingMaterialComponent>(); return _StartingMaterial; }
                set { _StartingMaterial = value; OnPropertyChanged("StartingMaterial"); }
            }
            
            private List<StartingMaterialComponent> _StartingMaterial;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("MonomerSetComponent");
                base.Serialize(sink);
                sink.Element("ratioType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RatioType?.Serialize(sink);
                sink.BeginList("startingMaterial", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in StartingMaterial)
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
                    case "ratioType":
                        RatioType = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "startingMaterial":
                        StartingMaterial = source.GetList<StartingMaterialComponent>();
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
                    case "ratioType":
                        RatioType = source.Populate(RatioType);
                        return true;
                    case "startingMaterial":
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
                    case "startingMaterial":
                        source.PopulateListItem(StartingMaterial, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MonomerSetComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RatioType != null) dest.RatioType = (Hl7.Fhir.Model.CodeableConcept)RatioType.DeepCopy();
                    if(StartingMaterial != null) dest.StartingMaterial = new List<StartingMaterialComponent>(StartingMaterial.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new MonomerSetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MonomerSetComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RatioType, otherT.RatioType)) return false;
                if( !DeepComparable.Matches(StartingMaterial, otherT.StartingMaterial)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MonomerSetComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RatioType, otherT.RatioType)) return false;
                if( !DeepComparable.IsExactly(StartingMaterial, otherT.StartingMaterial)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RatioType != null) yield return RatioType;
                    foreach (var elem in StartingMaterial) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RatioType != null) yield return new ElementValue("ratioType", RatioType);
                    foreach (var elem in StartingMaterial) { if (elem != null) yield return new ElementValue("startingMaterial", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "StartingMaterialComponent")]
        [DataContract]
        public partial class StartingMaterialComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "StartingMaterialComponent"; } }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("material", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Material
            {
                get { return _Material; }
                set { _Material = value; OnPropertyChanged("Material"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Material;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("isDefining", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean IsDefiningElement
            {
                get { return _IsDefiningElement; }
                set { _IsDefiningElement = value; OnPropertyChanged("IsDefiningElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _IsDefiningElement;
            
            /// <summary>
            /// Todo
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? IsDefining
            {
                get { return IsDefiningElement != null ? IsDefiningElement.Value : null; }
                set
                {
                    if (value == null)
                        IsDefiningElement = null;
                    else
                        IsDefiningElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("IsDefining");
                }
            }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("amount", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.SubstanceAmount Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.SubstanceAmount _Amount;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("StartingMaterialComponent");
                base.Serialize(sink);
                sink.Element("material", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Material?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
                sink.Element("isDefining", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IsDefiningElement?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Amount?.Serialize(sink);
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
                    case "material":
                        Material = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "isDefining":
                        IsDefiningElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.SubstanceAmount>();
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
                    case "material":
                        Material = source.Populate(Material);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "isDefining":
                        IsDefiningElement = source.PopulateValue(IsDefiningElement);
                        return true;
                    case "_isDefining":
                        IsDefiningElement = source.Populate(IsDefiningElement);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StartingMaterialComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Material != null) dest.Material = (Hl7.Fhir.Model.CodeableConcept)Material.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(IsDefiningElement != null) dest.IsDefiningElement = (Hl7.Fhir.Model.FhirBoolean)IsDefiningElement.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.SubstanceAmount)Amount.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new StartingMaterialComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StartingMaterialComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Material, otherT.Material)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(IsDefiningElement, otherT.IsDefiningElement)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StartingMaterialComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Material, otherT.Material)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(IsDefiningElement, otherT.IsDefiningElement)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Material != null) yield return Material;
                    if (Type != null) yield return Type;
                    if (IsDefiningElement != null) yield return IsDefiningElement;
                    if (Amount != null) yield return Amount;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Material != null) yield return new ElementValue("material", Material);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (IsDefiningElement != null) yield return new ElementValue("isDefining", IsDefiningElement);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "RepeatComponent")]
        [DataContract]
        public partial class RepeatComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RepeatComponent"; } }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("numberOfUnits", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumberOfUnitsElement
            {
                get { return _NumberOfUnitsElement; }
                set { _NumberOfUnitsElement = value; OnPropertyChanged("NumberOfUnitsElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _NumberOfUnitsElement;
            
            /// <summary>
            /// Todo
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? NumberOfUnits
            {
                get { return NumberOfUnitsElement != null ? NumberOfUnitsElement.Value : null; }
                set
                {
                    if (value == null)
                        NumberOfUnitsElement = null;
                    else
                        NumberOfUnitsElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("NumberOfUnits");
                }
            }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("averageMolecularFormula", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString AverageMolecularFormulaElement
            {
                get { return _AverageMolecularFormulaElement; }
                set { _AverageMolecularFormulaElement = value; OnPropertyChanged("AverageMolecularFormulaElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _AverageMolecularFormulaElement;
            
            /// <summary>
            /// Todo
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string AverageMolecularFormula
            {
                get { return AverageMolecularFormulaElement != null ? AverageMolecularFormulaElement.Value : null; }
                set
                {
                    if (value == null)
                        AverageMolecularFormulaElement = null;
                    else
                        AverageMolecularFormulaElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("AverageMolecularFormula");
                }
            }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("repeatUnitAmountType", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept RepeatUnitAmountType
            {
                get { return _RepeatUnitAmountType; }
                set { _RepeatUnitAmountType = value; OnPropertyChanged("RepeatUnitAmountType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _RepeatUnitAmountType;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("repeatUnit", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<RepeatUnitComponent> RepeatUnit
            {
                get { if(_RepeatUnit==null) _RepeatUnit = new List<RepeatUnitComponent>(); return _RepeatUnit; }
                set { _RepeatUnit = value; OnPropertyChanged("RepeatUnit"); }
            }
            
            private List<RepeatUnitComponent> _RepeatUnit;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RepeatComponent");
                base.Serialize(sink);
                sink.Element("numberOfUnits", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NumberOfUnitsElement?.Serialize(sink);
                sink.Element("averageMolecularFormula", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AverageMolecularFormulaElement?.Serialize(sink);
                sink.Element("repeatUnitAmountType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RepeatUnitAmountType?.Serialize(sink);
                sink.BeginList("repeatUnit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in RepeatUnit)
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
                    case "numberOfUnits":
                        NumberOfUnitsElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "averageMolecularFormula":
                        AverageMolecularFormulaElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "repeatUnitAmountType":
                        RepeatUnitAmountType = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "repeatUnit":
                        RepeatUnit = source.GetList<RepeatUnitComponent>();
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
                    case "numberOfUnits":
                        NumberOfUnitsElement = source.PopulateValue(NumberOfUnitsElement);
                        return true;
                    case "_numberOfUnits":
                        NumberOfUnitsElement = source.Populate(NumberOfUnitsElement);
                        return true;
                    case "averageMolecularFormula":
                        AverageMolecularFormulaElement = source.PopulateValue(AverageMolecularFormulaElement);
                        return true;
                    case "_averageMolecularFormula":
                        AverageMolecularFormulaElement = source.Populate(AverageMolecularFormulaElement);
                        return true;
                    case "repeatUnitAmountType":
                        RepeatUnitAmountType = source.Populate(RepeatUnitAmountType);
                        return true;
                    case "repeatUnit":
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
                    case "repeatUnit":
                        source.PopulateListItem(RepeatUnit, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RepeatComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NumberOfUnitsElement != null) dest.NumberOfUnitsElement = (Hl7.Fhir.Model.Integer)NumberOfUnitsElement.DeepCopy();
                    if(AverageMolecularFormulaElement != null) dest.AverageMolecularFormulaElement = (Hl7.Fhir.Model.FhirString)AverageMolecularFormulaElement.DeepCopy();
                    if(RepeatUnitAmountType != null) dest.RepeatUnitAmountType = (Hl7.Fhir.Model.CodeableConcept)RepeatUnitAmountType.DeepCopy();
                    if(RepeatUnit != null) dest.RepeatUnit = new List<RepeatUnitComponent>(RepeatUnit.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RepeatComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RepeatComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NumberOfUnitsElement, otherT.NumberOfUnitsElement)) return false;
                if( !DeepComparable.Matches(AverageMolecularFormulaElement, otherT.AverageMolecularFormulaElement)) return false;
                if( !DeepComparable.Matches(RepeatUnitAmountType, otherT.RepeatUnitAmountType)) return false;
                if( !DeepComparable.Matches(RepeatUnit, otherT.RepeatUnit)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RepeatComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NumberOfUnitsElement, otherT.NumberOfUnitsElement)) return false;
                if( !DeepComparable.IsExactly(AverageMolecularFormulaElement, otherT.AverageMolecularFormulaElement)) return false;
                if( !DeepComparable.IsExactly(RepeatUnitAmountType, otherT.RepeatUnitAmountType)) return false;
                if( !DeepComparable.IsExactly(RepeatUnit, otherT.RepeatUnit)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NumberOfUnitsElement != null) yield return NumberOfUnitsElement;
                    if (AverageMolecularFormulaElement != null) yield return AverageMolecularFormulaElement;
                    if (RepeatUnitAmountType != null) yield return RepeatUnitAmountType;
                    foreach (var elem in RepeatUnit) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NumberOfUnitsElement != null) yield return new ElementValue("numberOfUnits", NumberOfUnitsElement);
                    if (AverageMolecularFormulaElement != null) yield return new ElementValue("averageMolecularFormula", AverageMolecularFormulaElement);
                    if (RepeatUnitAmountType != null) yield return new ElementValue("repeatUnitAmountType", RepeatUnitAmountType);
                    foreach (var elem in RepeatUnit) { if (elem != null) yield return new ElementValue("repeatUnit", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "RepeatUnitComponent")]
        [DataContract]
        public partial class RepeatUnitComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RepeatUnitComponent"; } }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("orientationOfPolymerisation", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept OrientationOfPolymerisation
            {
                get { return _OrientationOfPolymerisation; }
                set { _OrientationOfPolymerisation = value; OnPropertyChanged("OrientationOfPolymerisation"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _OrientationOfPolymerisation;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("repeatUnit", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RepeatUnitElement
            {
                get { return _RepeatUnitElement; }
                set { _RepeatUnitElement = value; OnPropertyChanged("RepeatUnitElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _RepeatUnitElement;
            
            /// <summary>
            /// Todo
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string RepeatUnit
            {
                get { return RepeatUnitElement != null ? RepeatUnitElement.Value : null; }
                set
                {
                    if (value == null)
                        RepeatUnitElement = null;
                    else
                        RepeatUnitElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("RepeatUnit");
                }
            }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("amount", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.SubstanceAmount Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.SubstanceAmount _Amount;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("degreeOfPolymerisation", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<DegreeOfPolymerisationComponent> DegreeOfPolymerisation
            {
                get { if(_DegreeOfPolymerisation==null) _DegreeOfPolymerisation = new List<DegreeOfPolymerisationComponent>(); return _DegreeOfPolymerisation; }
                set { _DegreeOfPolymerisation = value; OnPropertyChanged("DegreeOfPolymerisation"); }
            }
            
            private List<DegreeOfPolymerisationComponent> _DegreeOfPolymerisation;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("structuralRepresentation", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<StructuralRepresentationComponent> StructuralRepresentation
            {
                get { if(_StructuralRepresentation==null) _StructuralRepresentation = new List<StructuralRepresentationComponent>(); return _StructuralRepresentation; }
                set { _StructuralRepresentation = value; OnPropertyChanged("StructuralRepresentation"); }
            }
            
            private List<StructuralRepresentationComponent> _StructuralRepresentation;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RepeatUnitComponent");
                base.Serialize(sink);
                sink.Element("orientationOfPolymerisation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OrientationOfPolymerisation?.Serialize(sink);
                sink.Element("repeatUnit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RepeatUnitElement?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Amount?.Serialize(sink);
                sink.BeginList("degreeOfPolymerisation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in DegreeOfPolymerisation)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("structuralRepresentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in StructuralRepresentation)
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
                    case "orientationOfPolymerisation":
                        OrientationOfPolymerisation = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "repeatUnit":
                        RepeatUnitElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.SubstanceAmount>();
                        return true;
                    case "degreeOfPolymerisation":
                        DegreeOfPolymerisation = source.GetList<DegreeOfPolymerisationComponent>();
                        return true;
                    case "structuralRepresentation":
                        StructuralRepresentation = source.GetList<StructuralRepresentationComponent>();
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
                    case "orientationOfPolymerisation":
                        OrientationOfPolymerisation = source.Populate(OrientationOfPolymerisation);
                        return true;
                    case "repeatUnit":
                        RepeatUnitElement = source.PopulateValue(RepeatUnitElement);
                        return true;
                    case "_repeatUnit":
                        RepeatUnitElement = source.Populate(RepeatUnitElement);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                    case "degreeOfPolymerisation":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "structuralRepresentation":
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
                    case "degreeOfPolymerisation":
                        source.PopulateListItem(DegreeOfPolymerisation, index);
                        return true;
                    case "structuralRepresentation":
                        source.PopulateListItem(StructuralRepresentation, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RepeatUnitComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(OrientationOfPolymerisation != null) dest.OrientationOfPolymerisation = (Hl7.Fhir.Model.CodeableConcept)OrientationOfPolymerisation.DeepCopy();
                    if(RepeatUnitElement != null) dest.RepeatUnitElement = (Hl7.Fhir.Model.FhirString)RepeatUnitElement.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.SubstanceAmount)Amount.DeepCopy();
                    if(DegreeOfPolymerisation != null) dest.DegreeOfPolymerisation = new List<DegreeOfPolymerisationComponent>(DegreeOfPolymerisation.DeepCopy());
                    if(StructuralRepresentation != null) dest.StructuralRepresentation = new List<StructuralRepresentationComponent>(StructuralRepresentation.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RepeatUnitComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RepeatUnitComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(OrientationOfPolymerisation, otherT.OrientationOfPolymerisation)) return false;
                if( !DeepComparable.Matches(RepeatUnitElement, otherT.RepeatUnitElement)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(DegreeOfPolymerisation, otherT.DegreeOfPolymerisation)) return false;
                if( !DeepComparable.Matches(StructuralRepresentation, otherT.StructuralRepresentation)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RepeatUnitComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(OrientationOfPolymerisation, otherT.OrientationOfPolymerisation)) return false;
                if( !DeepComparable.IsExactly(RepeatUnitElement, otherT.RepeatUnitElement)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(DegreeOfPolymerisation, otherT.DegreeOfPolymerisation)) return false;
                if( !DeepComparable.IsExactly(StructuralRepresentation, otherT.StructuralRepresentation)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (OrientationOfPolymerisation != null) yield return OrientationOfPolymerisation;
                    if (RepeatUnitElement != null) yield return RepeatUnitElement;
                    if (Amount != null) yield return Amount;
                    foreach (var elem in DegreeOfPolymerisation) { if (elem != null) yield return elem; }
                    foreach (var elem in StructuralRepresentation) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (OrientationOfPolymerisation != null) yield return new ElementValue("orientationOfPolymerisation", OrientationOfPolymerisation);
                    if (RepeatUnitElement != null) yield return new ElementValue("repeatUnit", RepeatUnitElement);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    foreach (var elem in DegreeOfPolymerisation) { if (elem != null) yield return new ElementValue("degreeOfPolymerisation", elem); }
                    foreach (var elem in StructuralRepresentation) { if (elem != null) yield return new ElementValue("structuralRepresentation", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "DegreeOfPolymerisationComponent")]
        [DataContract]
        public partial class DegreeOfPolymerisationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DegreeOfPolymerisationComponent"; } }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("degree", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Degree
            {
                get { return _Degree; }
                set { _Degree = value; OnPropertyChanged("Degree"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Degree;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("amount", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.SubstanceAmount Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.SubstanceAmount _Amount;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DegreeOfPolymerisationComponent");
                base.Serialize(sink);
                sink.Element("degree", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Degree?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Amount?.Serialize(sink);
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
                    case "degree":
                        Degree = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.SubstanceAmount>();
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
                    case "degree":
                        Degree = source.Populate(Degree);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DegreeOfPolymerisationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Degree != null) dest.Degree = (Hl7.Fhir.Model.CodeableConcept)Degree.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.SubstanceAmount)Amount.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DegreeOfPolymerisationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DegreeOfPolymerisationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Degree, otherT.Degree)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DegreeOfPolymerisationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Degree, otherT.Degree)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Degree != null) yield return Degree;
                    if (Amount != null) yield return Amount;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Degree != null) yield return new ElementValue("degree", Degree);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "StructuralRepresentationComponent")]
        [DataContract]
        public partial class StructuralRepresentationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "StructuralRepresentationComponent"; } }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("representation", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RepresentationElement
            {
                get { return _RepresentationElement; }
                set { _RepresentationElement = value; OnPropertyChanged("RepresentationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _RepresentationElement;
            
            /// <summary>
            /// Todo
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Representation
            {
                get { return RepresentationElement != null ? RepresentationElement.Value : null; }
                set
                {
                    if (value == null)
                        RepresentationElement = null;
                    else
                        RepresentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Representation");
                }
            }
            
            /// <summary>
            /// Todo
            /// </summary>
            [FhirElement("attachment", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Attachment Attachment
            {
                get { return _Attachment; }
                set { _Attachment = value; OnPropertyChanged("Attachment"); }
            }
            
            private Hl7.Fhir.Model.Attachment _Attachment;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("StructuralRepresentationComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
                sink.Element("representation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RepresentationElement?.Serialize(sink);
                sink.Element("attachment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Attachment?.Serialize(sink);
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "representation":
                        RepresentationElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "attachment":
                        Attachment = source.Get<Hl7.Fhir.Model.Attachment>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "representation":
                        RepresentationElement = source.PopulateValue(RepresentationElement);
                        return true;
                    case "_representation":
                        RepresentationElement = source.Populate(RepresentationElement);
                        return true;
                    case "attachment":
                        Attachment = source.Populate(Attachment);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StructuralRepresentationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(RepresentationElement != null) dest.RepresentationElement = (Hl7.Fhir.Model.FhirString)RepresentationElement.DeepCopy();
                    if(Attachment != null) dest.Attachment = (Hl7.Fhir.Model.Attachment)Attachment.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new StructuralRepresentationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StructuralRepresentationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(RepresentationElement, otherT.RepresentationElement)) return false;
                if( !DeepComparable.Matches(Attachment, otherT.Attachment)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StructuralRepresentationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(RepresentationElement, otherT.RepresentationElement)) return false;
                if( !DeepComparable.IsExactly(Attachment, otherT.Attachment)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (RepresentationElement != null) yield return RepresentationElement;
                    if (Attachment != null) yield return Attachment;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (RepresentationElement != null) yield return new ElementValue("representation", RepresentationElement);
                    if (Attachment != null) yield return new ElementValue("attachment", Attachment);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Todo
        /// </summary>
        [FhirElement("class", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Class
        {
            get { return _Class; }
            set { _Class = value; OnPropertyChanged("Class"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Class;
        
        /// <summary>
        /// Todo
        /// </summary>
        [FhirElement("geometry", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Geometry
        {
            get { return _Geometry; }
            set { _Geometry = value; OnPropertyChanged("Geometry"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Geometry;
        
        /// <summary>
        /// Todo
        /// </summary>
        [FhirElement("copolymerConnectivity", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> CopolymerConnectivity
        {
            get { if(_CopolymerConnectivity==null) _CopolymerConnectivity = new List<Hl7.Fhir.Model.CodeableConcept>(); return _CopolymerConnectivity; }
            set { _CopolymerConnectivity = value; OnPropertyChanged("CopolymerConnectivity"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _CopolymerConnectivity;
        
        /// <summary>
        /// Todo
        /// </summary>
        [FhirElement("modification", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> ModificationElement
        {
            get { if(_ModificationElement==null) _ModificationElement = new List<Hl7.Fhir.Model.FhirString>(); return _ModificationElement; }
            set { _ModificationElement = value; OnPropertyChanged("ModificationElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _ModificationElement;
        
        /// <summary>
        /// Todo
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Modification
        {
            get { return ModificationElement != null ? ModificationElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    ModificationElement = null;
                else
                    ModificationElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Modification");
            }
        }
        
        /// <summary>
        /// Todo
        /// </summary>
        [FhirElement("monomerSet", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<MonomerSetComponent> MonomerSet
        {
            get { if(_MonomerSet==null) _MonomerSet = new List<MonomerSetComponent>(); return _MonomerSet; }
            set { _MonomerSet = value; OnPropertyChanged("MonomerSet"); }
        }
        
        private List<MonomerSetComponent> _MonomerSet;
        
        /// <summary>
        /// Todo
        /// </summary>
        [FhirElement("repeat", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<RepeatComponent> Repeat
        {
            get { if(_Repeat==null) _Repeat = new List<RepeatComponent>(); return _Repeat; }
            set { _Repeat = value; OnPropertyChanged("Repeat"); }
        }
        
        private List<RepeatComponent> _Repeat;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SubstancePolymer;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Class != null) dest.Class = (Hl7.Fhir.Model.CodeableConcept)Class.DeepCopy();
                if(Geometry != null) dest.Geometry = (Hl7.Fhir.Model.CodeableConcept)Geometry.DeepCopy();
                if(CopolymerConnectivity != null) dest.CopolymerConnectivity = new List<Hl7.Fhir.Model.CodeableConcept>(CopolymerConnectivity.DeepCopy());
                if(ModificationElement != null) dest.ModificationElement = new List<Hl7.Fhir.Model.FhirString>(ModificationElement.DeepCopy());
                if(MonomerSet != null) dest.MonomerSet = new List<MonomerSetComponent>(MonomerSet.DeepCopy());
                if(Repeat != null) dest.Repeat = new List<RepeatComponent>(Repeat.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new SubstancePolymer());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SubstancePolymer;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Class, otherT.Class)) return false;
            if( !DeepComparable.Matches(Geometry, otherT.Geometry)) return false;
            if( !DeepComparable.Matches(CopolymerConnectivity, otherT.CopolymerConnectivity)) return false;
            if( !DeepComparable.Matches(ModificationElement, otherT.ModificationElement)) return false;
            if( !DeepComparable.Matches(MonomerSet, otherT.MonomerSet)) return false;
            if( !DeepComparable.Matches(Repeat, otherT.Repeat)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SubstancePolymer;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Class, otherT.Class)) return false;
            if( !DeepComparable.IsExactly(Geometry, otherT.Geometry)) return false;
            if( !DeepComparable.IsExactly(CopolymerConnectivity, otherT.CopolymerConnectivity)) return false;
            if( !DeepComparable.IsExactly(ModificationElement, otherT.ModificationElement)) return false;
            if( !DeepComparable.IsExactly(MonomerSet, otherT.MonomerSet)) return false;
            if( !DeepComparable.IsExactly(Repeat, otherT.Repeat)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("SubstancePolymer");
            base.Serialize(sink);
            sink.Element("class", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Class?.Serialize(sink);
            sink.Element("geometry", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Geometry?.Serialize(sink);
            sink.BeginList("copolymerConnectivity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in CopolymerConnectivity)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("modification", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(ModificationElement);
            sink.End();
            sink.BeginList("monomerSet", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in MonomerSet)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("repeat", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Repeat)
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
                case "class":
                    Class = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "geometry":
                    Geometry = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "copolymerConnectivity":
                    CopolymerConnectivity = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "modification":
                    ModificationElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "monomerSet":
                    MonomerSet = source.GetList<MonomerSetComponent>();
                    return true;
                case "repeat":
                    Repeat = source.GetList<RepeatComponent>();
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
                case "class":
                    Class = source.Populate(Class);
                    return true;
                case "geometry":
                    Geometry = source.Populate(Geometry);
                    return true;
                case "copolymerConnectivity":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "modification":
                case "_modification":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "monomerSet":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "repeat":
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
                case "copolymerConnectivity":
                    source.PopulateListItem(CopolymerConnectivity, index);
                    return true;
                case "modification":
                    source.PopulatePrimitiveListItemValue(ModificationElement, index);
                    return true;
                case "_modification":
                    source.PopulatePrimitiveListItem(ModificationElement, index);
                    return true;
                case "monomerSet":
                    source.PopulateListItem(MonomerSet, index);
                    return true;
                case "repeat":
                    source.PopulateListItem(Repeat, index);
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
                if (Class != null) yield return Class;
                if (Geometry != null) yield return Geometry;
                foreach (var elem in CopolymerConnectivity) { if (elem != null) yield return elem; }
                foreach (var elem in ModificationElement) { if (elem != null) yield return elem; }
                foreach (var elem in MonomerSet) { if (elem != null) yield return elem; }
                foreach (var elem in Repeat) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Class != null) yield return new ElementValue("class", Class);
                if (Geometry != null) yield return new ElementValue("geometry", Geometry);
                foreach (var elem in CopolymerConnectivity) { if (elem != null) yield return new ElementValue("copolymerConnectivity", elem); }
                foreach (var elem in ModificationElement) { if (elem != null) yield return new ElementValue("modification", elem); }
                foreach (var elem in MonomerSet) { if (elem != null) yield return new ElementValue("monomerSet", elem); }
                foreach (var elem in Repeat) { if (elem != null) yield return new ElementValue("repeat", elem); }
            }
        }
    
    }

}
