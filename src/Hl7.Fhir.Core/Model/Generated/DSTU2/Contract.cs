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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model.DSTU2
{
    /// <summary>
    /// Contract
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "Contract", IsResource=true)]
    [DataContract]
    public partial class Contract : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IContract, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Contract; } }
        [NotMapped]
        public override string TypeName { get { return "Contract"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ActorComponent")]
        [DataContract]
        public partial class ActorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ActorComponent"; } }
            
            /// <summary>
            /// Contract Actor Type
            /// </summary>
            [FhirElement("entity", Order=40)]
            [CLSCompliant(false)]
            [References("Contract","Device","Group","Location","Organization","Patient","Practitioner","RelatedPerson","Substance")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Entity
            {
                get { return _Entity; }
                set { _Entity = value; OnPropertyChanged("Entity"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Entity;
            
            /// <summary>
            /// Contract  Actor Role
            /// </summary>
            [FhirElement("role", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Role
            {
                get { if(_Role==null) _Role = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Role;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ActorComponent");
                base.Serialize(sink);
                sink.Element("entity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Entity?.Serialize(sink);
                sink.BeginList("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Role)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "entity":
                        Entity = source.Populate(Entity);
                        return true;
                    case "role":
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
                    case "role":
                        source.PopulateListItem(Role, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActorComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Entity != null) dest.Entity = (Hl7.Fhir.Model.ResourceReference)Entity.DeepCopy();
                    if(Role != null) dest.Role = new List<Hl7.Fhir.Model.CodeableConcept>(Role.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ActorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActorComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Entity, otherT.Entity)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActorComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Entity, otherT.Entity)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Entity != null) yield return Entity;
                    foreach (var elem in Role) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Entity != null) yield return new ElementValue("entity", Entity);
                    foreach (var elem in Role) { if (elem != null) yield return new ElementValue("role", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ValuedItemComponent")]
        [DataContract]
        public partial class ValuedItemComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IContractValuedItemComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ValuedItemComponent"; } }
            
            [NotMapped]
            Hl7.Fhir.Model.IMoney Hl7.Fhir.Model.IContractValuedItemComponent.UnitPrice { get { return UnitPrice; } }
            
            [NotMapped]
            Hl7.Fhir.Model.IMoney Hl7.Fhir.Model.IContractValuedItemComponent.Net { get { return Net; } }
            
            /// <summary>
            /// Contract Valued Item Type
            /// </summary>
            [FhirElement("entity", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Entity
            {
                get { return _Entity; }
                set { _Entity = value; OnPropertyChanged("Entity"); }
            }
            
            private Hl7.Fhir.Model.Element _Entity;
            
            /// <summary>
            /// Contract Valued Item Identifier
            /// </summary>
            [FhirElement("identifier", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Contract Valued Item Effective Tiem
            /// </summary>
            [FhirElement("effectiveTime", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime EffectiveTimeElement
            {
                get { return _EffectiveTimeElement; }
                set { _EffectiveTimeElement = value; OnPropertyChanged("EffectiveTimeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _EffectiveTimeElement;
            
            /// <summary>
            /// Contract Valued Item Effective Tiem
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string EffectiveTime
            {
                get { return EffectiveTimeElement != null ? EffectiveTimeElement.Value : null; }
                set
                {
                    if (value == null)
                        EffectiveTimeElement = null;
                    else
                        EffectiveTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("EffectiveTime");
                }
            }
            
            /// <summary>
            /// Count of Contract Valued Items
            /// </summary>
            [FhirElement("quantity", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Contract Valued Item fee, charge, or cost
            /// </summary>
            [FhirElement("unitPrice", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.DSTU2.Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Hl7.Fhir.Model.DSTU2.Money _UnitPrice;
            
            /// <summary>
            /// Contract Valued Item Price Scaling Factor
            /// </summary>
            [FhirElement("factor", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Contract Valued Item Price Scaling Factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if (value == null)
                        FactorElement = null;
                    else
                        FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Contract Valued Item Difficulty Scaling Factor
            /// </summary>
            [FhirElement("points", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PointsElement
            {
                get { return _PointsElement; }
                set { _PointsElement = value; OnPropertyChanged("PointsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PointsElement;
            
            /// <summary>
            /// Contract Valued Item Difficulty Scaling Factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Points
            {
                get { return PointsElement != null ? PointsElement.Value : null; }
                set
                {
                    if (value == null)
                        PointsElement = null;
                    else
                        PointsElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Points");
                }
            }
            
            /// <summary>
            /// Total Contract Valued Item Value
            /// </summary>
            [FhirElement("net", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.DSTU2.Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Hl7.Fhir.Model.DSTU2.Money _Net;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ValuedItemComponent");
                base.Serialize(sink);
                sink.Element("entity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Entity?.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Identifier?.Serialize(sink);
                sink.Element("effectiveTime", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); EffectiveTimeElement?.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("unitPrice", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UnitPrice?.Serialize(sink);
                sink.Element("factor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); FactorElement?.Serialize(sink);
                sink.Element("points", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PointsElement?.Serialize(sink);
                sink.Element("net", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Net?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "entityCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Entity, "entity");
                        Entity = source.Populate(Entity as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "entityReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Entity, "entity");
                        Entity = source.Populate(Entity as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "identifier":
                        Identifier = source.Populate(Identifier);
                        return true;
                    case "effectiveTime":
                        EffectiveTimeElement = source.PopulateValue(EffectiveTimeElement);
                        return true;
                    case "_effectiveTime":
                        EffectiveTimeElement = source.Populate(EffectiveTimeElement);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Populate(UnitPrice);
                        return true;
                    case "factor":
                        FactorElement = source.PopulateValue(FactorElement);
                        return true;
                    case "_factor":
                        FactorElement = source.Populate(FactorElement);
                        return true;
                    case "points":
                        PointsElement = source.PopulateValue(PointsElement);
                        return true;
                    case "_points":
                        PointsElement = source.Populate(PointsElement);
                        return true;
                    case "net":
                        Net = source.Populate(Net);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ValuedItemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Entity != null) dest.Entity = (Hl7.Fhir.Model.Element)Entity.DeepCopy();
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(EffectiveTimeElement != null) dest.EffectiveTimeElement = (Hl7.Fhir.Model.FhirDateTime)EffectiveTimeElement.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.DSTU2.Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(PointsElement != null) dest.PointsElement = (Hl7.Fhir.Model.FhirDecimal)PointsElement.DeepCopy();
                    if(Net != null) dest.Net = (Hl7.Fhir.Model.DSTU2.Money)Net.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ValuedItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ValuedItemComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Entity, otherT.Entity)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(EffectiveTimeElement, otherT.EffectiveTimeElement)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ValuedItemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Entity, otherT.Entity)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(EffectiveTimeElement, otherT.EffectiveTimeElement)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Entity != null) yield return Entity;
                    if (Identifier != null) yield return Identifier;
                    if (EffectiveTimeElement != null) yield return EffectiveTimeElement;
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (PointsElement != null) yield return PointsElement;
                    if (Net != null) yield return Net;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Entity != null) yield return new ElementValue("entity", Entity);
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (EffectiveTimeElement != null) yield return new ElementValue("effectiveTime", EffectiveTimeElement);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (PointsElement != null) yield return new ElementValue("points", PointsElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "SignatoryComponent")]
        [DataContract]
        public partial class SignatoryComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IContractSignatoryComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SignatoryComponent"; } }
            
            /// <summary>
            /// Contract Signer Type
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Contract Signatory Party
            /// </summary>
            [FhirElement("party", Order=50)]
            [CLSCompliant(false)]
            [References("Organization","Patient","Practitioner","RelatedPerson")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Party
            {
                get { return _Party; }
                set { _Party = value; OnPropertyChanged("Party"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Party;
            
            /// <summary>
            /// Contract Documentation Signature
            /// </summary>
            [FhirElement("signature", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SignatureElement
            {
                get { return _SignatureElement; }
                set { _SignatureElement = value; OnPropertyChanged("SignatureElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SignatureElement;
            
            /// <summary>
            /// Contract Documentation Signature
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Signature
            {
                get { return SignatureElement != null ? SignatureElement.Value : null; }
                set
                {
                    if (value == null)
                        SignatureElement = null;
                    else
                        SignatureElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Signature");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SignatoryComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.Element("party", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Party?.Serialize(sink);
                sink.Element("signature", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SignatureElement?.Serialize(sink);
                sink.End();
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
                    case "party":
                        Party = source.Populate(Party);
                        return true;
                    case "signature":
                        SignatureElement = source.PopulateValue(SignatureElement);
                        return true;
                    case "_signature":
                        SignatureElement = source.Populate(SignatureElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SignatoryComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Party != null) dest.Party = (Hl7.Fhir.Model.ResourceReference)Party.DeepCopy();
                    if(SignatureElement != null) dest.SignatureElement = (Hl7.Fhir.Model.FhirString)SignatureElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SignatoryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SignatoryComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Party, otherT.Party)) return false;
                if( !DeepComparable.Matches(SignatureElement, otherT.SignatureElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SignatoryComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Party, otherT.Party)) return false;
                if( !DeepComparable.IsExactly(SignatureElement, otherT.SignatureElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Party != null) yield return Party;
                    if (SignatureElement != null) yield return SignatureElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Party != null) yield return new ElementValue("party", Party);
                    if (SignatureElement != null) yield return new ElementValue("signature", SignatureElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "TermComponent")]
        [DataContract]
        public partial class TermComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IContractTermComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TermComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IContractTermComponent> Hl7.Fhir.Model.IContractTermComponent.Group { get { return Group; } }
            
            /// <summary>
            /// Contract Term identifier
            /// </summary>
            [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Contract Term Issue Date Time
            /// </summary>
            [FhirElement("issued", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime IssuedElement
            {
                get { return _IssuedElement; }
                set { _IssuedElement = value; OnPropertyChanged("IssuedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _IssuedElement;
            
            /// <summary>
            /// Contract Term Issue Date Time
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Issued
            {
                get { return IssuedElement != null ? IssuedElement.Value : null; }
                set
                {
                    if (value == null)
                        IssuedElement = null;
                    else
                        IssuedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Issued");
                }
            }
            
            /// <summary>
            /// Contract Term Effective Time
            /// </summary>
            [FhirElement("applies", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Period Applies
            {
                get { return _Applies; }
                set { _Applies = value; OnPropertyChanged("Applies"); }
            }
            
            private Hl7.Fhir.Model.Period _Applies;
            
            /// <summary>
            /// Contract Term Type
            /// </summary>
            [FhirElement("type", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Contract Term Subtype
            /// </summary>
            [FhirElement("subType", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept SubType
            {
                get { return _SubType; }
                set { _SubType = value; OnPropertyChanged("SubType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _SubType;
            
            /// <summary>
            /// Subject of this Contract Term
            /// </summary>
            [FhirElement("subject", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Subject
            {
                get { return _Subject; }
                set { _Subject = value; OnPropertyChanged("Subject"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Subject;
            
            /// <summary>
            /// Contract Term Action
            /// </summary>
            [FhirElement("action", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Action;
            
            /// <summary>
            /// Contract Term Action Reason
            /// </summary>
            [FhirElement("actionReason", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ActionReason
            {
                get { if(_ActionReason==null) _ActionReason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ActionReason; }
                set { _ActionReason = value; OnPropertyChanged("ActionReason"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ActionReason;
            
            /// <summary>
            /// Contract Term Actor List
            /// </summary>
            [FhirElement("actor", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<TermActorComponent> Actor
            {
                get { if(_Actor==null) _Actor = new List<TermActorComponent>(); return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private List<TermActorComponent> _Actor;
            
            /// <summary>
            /// Human readable Contract term text
            /// </summary>
            [FhirElement("text", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Human readable Contract term text
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if (value == null)
                        TextElement = null;
                    else
                        TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            /// <summary>
            /// Contract Term Valued Item
            /// </summary>
            [FhirElement("valuedItem", Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<TermValuedItemComponent> ValuedItem
            {
                get { if(_ValuedItem==null) _ValuedItem = new List<TermValuedItemComponent>(); return _ValuedItem; }
                set { _ValuedItem = value; OnPropertyChanged("ValuedItem"); }
            }
            
            private List<TermValuedItemComponent> _ValuedItem;
            
            /// <summary>
            /// Nested Contract Term Group
            /// </summary>
            [FhirElement("group", Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<TermComponent> Group
            {
                get { if(_Group==null) _Group = new List<TermComponent>(); return _Group; }
                set { _Group = value; OnPropertyChanged("Group"); }
            }
            
            private List<TermComponent> _Group;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TermComponent");
                base.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
                sink.Element("issued", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IssuedElement?.Serialize(sink);
                sink.Element("applies", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Applies?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("subType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SubType?.Serialize(sink);
                sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Subject?.Serialize(sink);
                sink.BeginList("action", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Action)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("actionReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ActionReason)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Actor)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("text", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TextElement?.Serialize(sink);
                sink.BeginList("valuedItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ValuedItem)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("group", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Group)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
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
                        Identifier = source.Populate(Identifier);
                        return true;
                    case "issued":
                        IssuedElement = source.PopulateValue(IssuedElement);
                        return true;
                    case "_issued":
                        IssuedElement = source.Populate(IssuedElement);
                        return true;
                    case "applies":
                        Applies = source.Populate(Applies);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "subType":
                        SubType = source.Populate(SubType);
                        return true;
                    case "subject":
                        Subject = source.Populate(Subject);
                        return true;
                    case "action":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "actionReason":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "actor":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "text":
                        TextElement = source.PopulateValue(TextElement);
                        return true;
                    case "_text":
                        TextElement = source.Populate(TextElement);
                        return true;
                    case "valuedItem":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "group":
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
                    case "action":
                        source.PopulateListItem(Action, index);
                        return true;
                    case "actionReason":
                        source.PopulateListItem(ActionReason, index);
                        return true;
                    case "actor":
                        source.PopulateListItem(Actor, index);
                        return true;
                    case "valuedItem":
                        source.PopulateListItem(ValuedItem, index);
                        return true;
                    case "group":
                        source.PopulateListItem(Group, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TermComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.FhirDateTime)IssuedElement.DeepCopy();
                    if(Applies != null) dest.Applies = (Hl7.Fhir.Model.Period)Applies.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(SubType != null) dest.SubType = (Hl7.Fhir.Model.CodeableConcept)SubType.DeepCopy();
                    if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.CodeableConcept>(Action.DeepCopy());
                    if(ActionReason != null) dest.ActionReason = new List<Hl7.Fhir.Model.CodeableConcept>(ActionReason.DeepCopy());
                    if(Actor != null) dest.Actor = new List<TermActorComponent>(Actor.DeepCopy());
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(ValuedItem != null) dest.ValuedItem = new List<TermValuedItemComponent>(ValuedItem.DeepCopy());
                    if(Group != null) dest.Group = new List<TermComponent>(Group.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TermComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TermComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
                if( !DeepComparable.Matches(Applies, otherT.Applies)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(SubType, otherT.SubType)) return false;
                if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                if( !DeepComparable.Matches(ActionReason, otherT.ActionReason)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(ValuedItem, otherT.ValuedItem)) return false;
                if( !DeepComparable.Matches(Group, otherT.Group)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TermComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
                if( !DeepComparable.IsExactly(Applies, otherT.Applies)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(SubType, otherT.SubType)) return false;
                if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                if( !DeepComparable.IsExactly(ActionReason, otherT.ActionReason)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(ValuedItem, otherT.ValuedItem)) return false;
                if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (IssuedElement != null) yield return IssuedElement;
                    if (Applies != null) yield return Applies;
                    if (Type != null) yield return Type;
                    if (SubType != null) yield return SubType;
                    if (Subject != null) yield return Subject;
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                    foreach (var elem in ActionReason) { if (elem != null) yield return elem; }
                    foreach (var elem in Actor) { if (elem != null) yield return elem; }
                    if (TextElement != null) yield return TextElement;
                    foreach (var elem in ValuedItem) { if (elem != null) yield return elem; }
                    foreach (var elem in Group) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (IssuedElement != null) yield return new ElementValue("issued", IssuedElement);
                    if (Applies != null) yield return new ElementValue("applies", Applies);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (SubType != null) yield return new ElementValue("subType", SubType);
                    if (Subject != null) yield return new ElementValue("subject", Subject);
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                    foreach (var elem in ActionReason) { if (elem != null) yield return new ElementValue("actionReason", elem); }
                    foreach (var elem in Actor) { if (elem != null) yield return new ElementValue("actor", elem); }
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                    foreach (var elem in ValuedItem) { if (elem != null) yield return new ElementValue("valuedItem", elem); }
                    foreach (var elem in Group) { if (elem != null) yield return new ElementValue("group", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "TermActorComponent")]
        [DataContract]
        public partial class TermActorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TermActorComponent"; } }
            
            /// <summary>
            /// Contract Term Actor
            /// </summary>
            [FhirElement("entity", Order=40)]
            [CLSCompliant(false)]
            [References("Contract","Device","Group","Location","Organization","Patient","Practitioner","RelatedPerson","Substance")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Entity
            {
                get { return _Entity; }
                set { _Entity = value; OnPropertyChanged("Entity"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Entity;
            
            /// <summary>
            /// Contract Term Actor Role
            /// </summary>
            [FhirElement("role", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Role
            {
                get { if(_Role==null) _Role = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Role;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TermActorComponent");
                base.Serialize(sink);
                sink.Element("entity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Entity?.Serialize(sink);
                sink.BeginList("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Role)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "entity":
                        Entity = source.Populate(Entity);
                        return true;
                    case "role":
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
                    case "role":
                        source.PopulateListItem(Role, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TermActorComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Entity != null) dest.Entity = (Hl7.Fhir.Model.ResourceReference)Entity.DeepCopy();
                    if(Role != null) dest.Role = new List<Hl7.Fhir.Model.CodeableConcept>(Role.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TermActorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TermActorComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Entity, otherT.Entity)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TermActorComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Entity, otherT.Entity)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Entity != null) yield return Entity;
                    foreach (var elem in Role) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Entity != null) yield return new ElementValue("entity", Entity);
                    foreach (var elem in Role) { if (elem != null) yield return new ElementValue("role", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "TermValuedItemComponent")]
        [DataContract]
        public partial class TermValuedItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TermValuedItemComponent"; } }
            
            /// <summary>
            /// Contract Term Valued Item Type
            /// </summary>
            [FhirElement("entity", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Entity
            {
                get { return _Entity; }
                set { _Entity = value; OnPropertyChanged("Entity"); }
            }
            
            private Hl7.Fhir.Model.Element _Entity;
            
            /// <summary>
            /// Contract Term Valued Item Identifier
            /// </summary>
            [FhirElement("identifier", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Contract Term Valued Item Effective Tiem
            /// </summary>
            [FhirElement("effectiveTime", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime EffectiveTimeElement
            {
                get { return _EffectiveTimeElement; }
                set { _EffectiveTimeElement = value; OnPropertyChanged("EffectiveTimeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _EffectiveTimeElement;
            
            /// <summary>
            /// Contract Term Valued Item Effective Tiem
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string EffectiveTime
            {
                get { return EffectiveTimeElement != null ? EffectiveTimeElement.Value : null; }
                set
                {
                    if (value == null)
                        EffectiveTimeElement = null;
                    else
                        EffectiveTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("EffectiveTime");
                }
            }
            
            /// <summary>
            /// Contract Term Valued Item Count
            /// </summary>
            [FhirElement("quantity", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Contract Term Valued Item fee, charge, or cost
            /// </summary>
            [FhirElement("unitPrice", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.DSTU2.Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Hl7.Fhir.Model.DSTU2.Money _UnitPrice;
            
            /// <summary>
            /// Contract Term Valued Item Price Scaling Factor
            /// </summary>
            [FhirElement("factor", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Contract Term Valued Item Price Scaling Factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if (value == null)
                        FactorElement = null;
                    else
                        FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Contract Term Valued Item Difficulty Scaling Factor
            /// </summary>
            [FhirElement("points", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PointsElement
            {
                get { return _PointsElement; }
                set { _PointsElement = value; OnPropertyChanged("PointsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PointsElement;
            
            /// <summary>
            /// Contract Term Valued Item Difficulty Scaling Factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Points
            {
                get { return PointsElement != null ? PointsElement.Value : null; }
                set
                {
                    if (value == null)
                        PointsElement = null;
                    else
                        PointsElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Points");
                }
            }
            
            /// <summary>
            /// Total Contract Term Valued Item Value
            /// </summary>
            [FhirElement("net", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.DSTU2.Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Hl7.Fhir.Model.DSTU2.Money _Net;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TermValuedItemComponent");
                base.Serialize(sink);
                sink.Element("entity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Entity?.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Identifier?.Serialize(sink);
                sink.Element("effectiveTime", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); EffectiveTimeElement?.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("unitPrice", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UnitPrice?.Serialize(sink);
                sink.Element("factor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); FactorElement?.Serialize(sink);
                sink.Element("points", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PointsElement?.Serialize(sink);
                sink.Element("net", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Net?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "entityCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Entity, "entity");
                        Entity = source.Populate(Entity as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "entityReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Entity, "entity");
                        Entity = source.Populate(Entity as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "identifier":
                        Identifier = source.Populate(Identifier);
                        return true;
                    case "effectiveTime":
                        EffectiveTimeElement = source.PopulateValue(EffectiveTimeElement);
                        return true;
                    case "_effectiveTime":
                        EffectiveTimeElement = source.Populate(EffectiveTimeElement);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Populate(UnitPrice);
                        return true;
                    case "factor":
                        FactorElement = source.PopulateValue(FactorElement);
                        return true;
                    case "_factor":
                        FactorElement = source.Populate(FactorElement);
                        return true;
                    case "points":
                        PointsElement = source.PopulateValue(PointsElement);
                        return true;
                    case "_points":
                        PointsElement = source.Populate(PointsElement);
                        return true;
                    case "net":
                        Net = source.Populate(Net);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TermValuedItemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Entity != null) dest.Entity = (Hl7.Fhir.Model.Element)Entity.DeepCopy();
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(EffectiveTimeElement != null) dest.EffectiveTimeElement = (Hl7.Fhir.Model.FhirDateTime)EffectiveTimeElement.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.DSTU2.Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(PointsElement != null) dest.PointsElement = (Hl7.Fhir.Model.FhirDecimal)PointsElement.DeepCopy();
                    if(Net != null) dest.Net = (Hl7.Fhir.Model.DSTU2.Money)Net.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TermValuedItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TermValuedItemComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Entity, otherT.Entity)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(EffectiveTimeElement, otherT.EffectiveTimeElement)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TermValuedItemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Entity, otherT.Entity)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(EffectiveTimeElement, otherT.EffectiveTimeElement)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Entity != null) yield return Entity;
                    if (Identifier != null) yield return Identifier;
                    if (EffectiveTimeElement != null) yield return EffectiveTimeElement;
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (PointsElement != null) yield return PointsElement;
                    if (Net != null) yield return Net;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Entity != null) yield return new ElementValue("entity", Entity);
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (EffectiveTimeElement != null) yield return new ElementValue("effectiveTime", EffectiveTimeElement);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (PointsElement != null) yield return new ElementValue("points", PointsElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "FriendlyLanguageComponent")]
        [DataContract]
        public partial class FriendlyLanguageComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IContractFriendlyLanguageComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "FriendlyLanguageComponent"; } }
            
            /// <summary>
            /// Easily comprehended representation of this Contract
            /// </summary>
            [FhirElement("content", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Content
            {
                get { return _Content; }
                set { _Content = value; OnPropertyChanged("Content"); }
            }
            
            private Hl7.Fhir.Model.Element _Content;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("FriendlyLanguageComponent");
                base.Serialize(sink);
                sink.Element("content", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Content?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "contentAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Content, "content");
                        Content = source.Populate(Content as Hl7.Fhir.Model.Attachment);
                        return true;
                    case "contentReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Content, "content");
                        Content = source.Populate(Content as Hl7.Fhir.Model.ResourceReference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FriendlyLanguageComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Content != null) dest.Content = (Hl7.Fhir.Model.Element)Content.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new FriendlyLanguageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FriendlyLanguageComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Content, otherT.Content)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FriendlyLanguageComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Content, otherT.Content)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Content != null) yield return Content;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Content != null) yield return new ElementValue("content", Content);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "LegalLanguageComponent")]
        [DataContract]
        public partial class LegalLanguageComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IContractLegalLanguageComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "LegalLanguageComponent"; } }
            
            /// <summary>
            /// Contract Legal Text
            /// </summary>
            [FhirElement("content", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Content
            {
                get { return _Content; }
                set { _Content = value; OnPropertyChanged("Content"); }
            }
            
            private Hl7.Fhir.Model.Element _Content;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("LegalLanguageComponent");
                base.Serialize(sink);
                sink.Element("content", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Content?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "contentAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Content, "content");
                        Content = source.Populate(Content as Hl7.Fhir.Model.Attachment);
                        return true;
                    case "contentReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Content, "content");
                        Content = source.Populate(Content as Hl7.Fhir.Model.ResourceReference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LegalLanguageComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Content != null) dest.Content = (Hl7.Fhir.Model.Element)Content.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new LegalLanguageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LegalLanguageComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Content, otherT.Content)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LegalLanguageComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Content, otherT.Content)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Content != null) yield return Content;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Content != null) yield return new ElementValue("content", Content);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ComputableLanguageComponent")]
        [DataContract]
        public partial class ComputableLanguageComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IContractComputableLanguageComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ComputableLanguageComponent"; } }
            
            /// <summary>
            /// Computable Contract Rules
            /// </summary>
            [FhirElement("content", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Content
            {
                get { return _Content; }
                set { _Content = value; OnPropertyChanged("Content"); }
            }
            
            private Hl7.Fhir.Model.Element _Content;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ComputableLanguageComponent");
                base.Serialize(sink);
                sink.Element("content", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Content?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "contentAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Content, "content");
                        Content = source.Populate(Content as Hl7.Fhir.Model.Attachment);
                        return true;
                    case "contentReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Content, "content");
                        Content = source.Populate(Content as Hl7.Fhir.Model.ResourceReference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ComputableLanguageComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Content != null) dest.Content = (Hl7.Fhir.Model.Element)Content.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ComputableLanguageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ComputableLanguageComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Content, otherT.Content)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ComputableLanguageComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Content, otherT.Content)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Content != null) yield return Content;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Content != null) yield return new ElementValue("content", Content);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContractSignatoryComponent> Hl7.Fhir.Model.IContract.Signer { get { return Signer; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContractTermComponent> Hl7.Fhir.Model.IContract.Term { get { return Term; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContractFriendlyLanguageComponent> Hl7.Fhir.Model.IContract.Friendly { get { return Friendly; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContractLegalLanguageComponent> Hl7.Fhir.Model.IContract.Legal { get { return Legal; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContractComputableLanguageComponent> Hl7.Fhir.Model.IContract.Rule { get { return Rule; } }
    
        
        /// <summary>
        /// Contract identifier
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// When this Contract was issued
        /// </summary>
        [FhirElement("issued", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime IssuedElement
        {
            get { return _IssuedElement; }
            set { _IssuedElement = value; OnPropertyChanged("IssuedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _IssuedElement;
        
        /// <summary>
        /// When this Contract was issued
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Issued
        {
            get { return IssuedElement != null ? IssuedElement.Value : null; }
            set
            {
                if (value == null)
                    IssuedElement = null;
                else
                    IssuedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Issued");
            }
        }
        
        /// <summary>
        /// Effective time
        /// </summary>
        [FhirElement("applies", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period Applies
        {
            get { return _Applies; }
            set { _Applies = value; OnPropertyChanged("Applies"); }
        }
        
        private Hl7.Fhir.Model.Period _Applies;
        
        /// <summary>
        /// Subject of this Contract
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Subject
        {
            get { if(_Subject==null) _Subject = new List<Hl7.Fhir.Model.ResourceReference>(); return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Subject;
        
        /// <summary>
        /// Authority under which this Contract has standing
        /// </summary>
        [FhirElement("authority", Order=130)]
        [CLSCompliant(false)]
        [References("Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Authority
        {
            get { if(_Authority==null) _Authority = new List<Hl7.Fhir.Model.ResourceReference>(); return _Authority; }
            set { _Authority = value; OnPropertyChanged("Authority"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Authority;
        
        /// <summary>
        /// Domain in which this Contract applies
        /// </summary>
        [FhirElement("domain", Order=140)]
        [CLSCompliant(false)]
        [References("Location")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Domain
        {
            get { if(_Domain==null) _Domain = new List<Hl7.Fhir.Model.ResourceReference>(); return _Domain; }
            set { _Domain = value; OnPropertyChanged("Domain"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Domain;
        
        /// <summary>
        /// Contract Tyoe
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Contract Subtype
        /// </summary>
        [FhirElement("subType", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> SubType
        {
            get { if(_SubType==null) _SubType = new List<Hl7.Fhir.Model.CodeableConcept>(); return _SubType; }
            set { _SubType = value; OnPropertyChanged("SubType"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _SubType;
        
        /// <summary>
        /// Contract Action
        /// </summary>
        [FhirElement("action", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Action
        {
            get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Action; }
            set { _Action = value; OnPropertyChanged("Action"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Action;
        
        /// <summary>
        /// Contract Action Reason
        /// </summary>
        [FhirElement("actionReason", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ActionReason
        {
            get { if(_ActionReason==null) _ActionReason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ActionReason; }
            set { _ActionReason = value; OnPropertyChanged("ActionReason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ActionReason;
        
        /// <summary>
        /// Contract Actor
        /// </summary>
        [FhirElement("actor", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ActorComponent> Actor
        {
            get { if(_Actor==null) _Actor = new List<ActorComponent>(); return _Actor; }
            set { _Actor = value; OnPropertyChanged("Actor"); }
        }
        
        private List<ActorComponent> _Actor;
        
        /// <summary>
        /// Contract Valued Item
        /// </summary>
        [FhirElement("valuedItem", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ValuedItemComponent> ValuedItem
        {
            get { if(_ValuedItem==null) _ValuedItem = new List<ValuedItemComponent>(); return _ValuedItem; }
            set { _ValuedItem = value; OnPropertyChanged("ValuedItem"); }
        }
        
        private List<ValuedItemComponent> _ValuedItem;
        
        /// <summary>
        /// Contract Signer
        /// </summary>
        [FhirElement("signer", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SignatoryComponent> Signer
        {
            get { if(_Signer==null) _Signer = new List<SignatoryComponent>(); return _Signer; }
            set { _Signer = value; OnPropertyChanged("Signer"); }
        }
        
        private List<SignatoryComponent> _Signer;
        
        /// <summary>
        /// Contract Term List
        /// </summary>
        [FhirElement("term", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<TermComponent> Term
        {
            get { if(_Term==null) _Term = new List<TermComponent>(); return _Term; }
            set { _Term = value; OnPropertyChanged("Term"); }
        }
        
        private List<TermComponent> _Term;
        
        /// <summary>
        /// Binding Contract
        /// </summary>
        [FhirElement("binding", Order=230, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Binding
        {
            get { return _Binding; }
            set { _Binding = value; OnPropertyChanged("Binding"); }
        }
        
        private Hl7.Fhir.Model.Element _Binding;
        
        /// <summary>
        /// Contract Friendly Language
        /// </summary>
        [FhirElement("friendly", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<FriendlyLanguageComponent> Friendly
        {
            get { if(_Friendly==null) _Friendly = new List<FriendlyLanguageComponent>(); return _Friendly; }
            set { _Friendly = value; OnPropertyChanged("Friendly"); }
        }
        
        private List<FriendlyLanguageComponent> _Friendly;
        
        /// <summary>
        /// Contract Legal Language
        /// </summary>
        [FhirElement("legal", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<LegalLanguageComponent> Legal
        {
            get { if(_Legal==null) _Legal = new List<LegalLanguageComponent>(); return _Legal; }
            set { _Legal = value; OnPropertyChanged("Legal"); }
        }
        
        private List<LegalLanguageComponent> _Legal;
        
        /// <summary>
        /// Computable Contract Language
        /// </summary>
        [FhirElement("rule", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ComputableLanguageComponent> Rule
        {
            get { if(_Rule==null) _Rule = new List<ComputableLanguageComponent>(); return _Rule; }
            set { _Rule = value; OnPropertyChanged("Rule"); }
        }
        
        private List<ComputableLanguageComponent> _Rule;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Contract;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.FhirDateTime)IssuedElement.DeepCopy();
                if(Applies != null) dest.Applies = (Hl7.Fhir.Model.Period)Applies.DeepCopy();
                if(Subject != null) dest.Subject = new List<Hl7.Fhir.Model.ResourceReference>(Subject.DeepCopy());
                if(Authority != null) dest.Authority = new List<Hl7.Fhir.Model.ResourceReference>(Authority.DeepCopy());
                if(Domain != null) dest.Domain = new List<Hl7.Fhir.Model.ResourceReference>(Domain.DeepCopy());
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(SubType != null) dest.SubType = new List<Hl7.Fhir.Model.CodeableConcept>(SubType.DeepCopy());
                if(Action != null) dest.Action = new List<Hl7.Fhir.Model.CodeableConcept>(Action.DeepCopy());
                if(ActionReason != null) dest.ActionReason = new List<Hl7.Fhir.Model.CodeableConcept>(ActionReason.DeepCopy());
                if(Actor != null) dest.Actor = new List<ActorComponent>(Actor.DeepCopy());
                if(ValuedItem != null) dest.ValuedItem = new List<ValuedItemComponent>(ValuedItem.DeepCopy());
                if(Signer != null) dest.Signer = new List<SignatoryComponent>(Signer.DeepCopy());
                if(Term != null) dest.Term = new List<TermComponent>(Term.DeepCopy());
                if(Binding != null) dest.Binding = (Hl7.Fhir.Model.Element)Binding.DeepCopy();
                if(Friendly != null) dest.Friendly = new List<FriendlyLanguageComponent>(Friendly.DeepCopy());
                if(Legal != null) dest.Legal = new List<LegalLanguageComponent>(Legal.DeepCopy());
                if(Rule != null) dest.Rule = new List<ComputableLanguageComponent>(Rule.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Contract());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Contract;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.Matches(Applies, otherT.Applies)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Authority, otherT.Authority)) return false;
            if( !DeepComparable.Matches(Domain, otherT.Domain)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(SubType, otherT.SubType)) return false;
            if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            if( !DeepComparable.Matches(ActionReason, otherT.ActionReason)) return false;
            if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
            if( !DeepComparable.Matches(ValuedItem, otherT.ValuedItem)) return false;
            if( !DeepComparable.Matches(Signer, otherT.Signer)) return false;
            if( !DeepComparable.Matches(Term, otherT.Term)) return false;
            if( !DeepComparable.Matches(Binding, otherT.Binding)) return false;
            if( !DeepComparable.Matches(Friendly, otherT.Friendly)) return false;
            if( !DeepComparable.Matches(Legal, otherT.Legal)) return false;
            if( !DeepComparable.Matches(Rule, otherT.Rule)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Contract;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.IsExactly(Applies, otherT.Applies)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Authority, otherT.Authority)) return false;
            if( !DeepComparable.IsExactly(Domain, otherT.Domain)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(SubType, otherT.SubType)) return false;
            if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            if( !DeepComparable.IsExactly(ActionReason, otherT.ActionReason)) return false;
            if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
            if( !DeepComparable.IsExactly(ValuedItem, otherT.ValuedItem)) return false;
            if( !DeepComparable.IsExactly(Signer, otherT.Signer)) return false;
            if( !DeepComparable.IsExactly(Term, otherT.Term)) return false;
            if( !DeepComparable.IsExactly(Binding, otherT.Binding)) return false;
            if( !DeepComparable.IsExactly(Friendly, otherT.Friendly)) return false;
            if( !DeepComparable.IsExactly(Legal, otherT.Legal)) return false;
            if( !DeepComparable.IsExactly(Rule, otherT.Rule)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Contract");
            base.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("issued", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IssuedElement?.Serialize(sink);
            sink.Element("applies", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Applies?.Serialize(sink);
            sink.BeginList("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Subject)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("authority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Authority)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("domain", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Domain)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
            sink.BeginList("subType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in SubType)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("action", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Action)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("actionReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ActionReason)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Actor)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("valuedItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ValuedItem)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("signer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Signer)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("term", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Term)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("binding", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Binding?.Serialize(sink);
            sink.BeginList("friendly", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Friendly)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("legal", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Legal)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("rule", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Rule)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
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
                    Identifier = source.Populate(Identifier);
                    return true;
                case "issued":
                    IssuedElement = source.PopulateValue(IssuedElement);
                    return true;
                case "_issued":
                    IssuedElement = source.Populate(IssuedElement);
                    return true;
                case "applies":
                    Applies = source.Populate(Applies);
                    return true;
                case "subject":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "authority":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "domain":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "type":
                    Type = source.Populate(Type);
                    return true;
                case "subType":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "action":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "actionReason":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "actor":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "valuedItem":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "signer":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "term":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "bindingAttachment":
                    source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Binding, "binding");
                    Binding = source.Populate(Binding as Hl7.Fhir.Model.Attachment);
                    return true;
                case "bindingReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Binding, "binding");
                    Binding = source.Populate(Binding as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "friendly":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "legal":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "rule":
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
                case "subject":
                    source.PopulateListItem(Subject, index);
                    return true;
                case "authority":
                    source.PopulateListItem(Authority, index);
                    return true;
                case "domain":
                    source.PopulateListItem(Domain, index);
                    return true;
                case "subType":
                    source.PopulateListItem(SubType, index);
                    return true;
                case "action":
                    source.PopulateListItem(Action, index);
                    return true;
                case "actionReason":
                    source.PopulateListItem(ActionReason, index);
                    return true;
                case "actor":
                    source.PopulateListItem(Actor, index);
                    return true;
                case "valuedItem":
                    source.PopulateListItem(ValuedItem, index);
                    return true;
                case "signer":
                    source.PopulateListItem(Signer, index);
                    return true;
                case "term":
                    source.PopulateListItem(Term, index);
                    return true;
                case "friendly":
                    source.PopulateListItem(Friendly, index);
                    return true;
                case "legal":
                    source.PopulateListItem(Legal, index);
                    return true;
                case "rule":
                    source.PopulateListItem(Rule, index);
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
                if (Identifier != null) yield return Identifier;
                if (IssuedElement != null) yield return IssuedElement;
                if (Applies != null) yield return Applies;
                foreach (var elem in Subject) { if (elem != null) yield return elem; }
                foreach (var elem in Authority) { if (elem != null) yield return elem; }
                foreach (var elem in Domain) { if (elem != null) yield return elem; }
                if (Type != null) yield return Type;
                foreach (var elem in SubType) { if (elem != null) yield return elem; }
                foreach (var elem in Action) { if (elem != null) yield return elem; }
                foreach (var elem in ActionReason) { if (elem != null) yield return elem; }
                foreach (var elem in Actor) { if (elem != null) yield return elem; }
                foreach (var elem in ValuedItem) { if (elem != null) yield return elem; }
                foreach (var elem in Signer) { if (elem != null) yield return elem; }
                foreach (var elem in Term) { if (elem != null) yield return elem; }
                if (Binding != null) yield return Binding;
                foreach (var elem in Friendly) { if (elem != null) yield return elem; }
                foreach (var elem in Legal) { if (elem != null) yield return elem; }
                foreach (var elem in Rule) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (IssuedElement != null) yield return new ElementValue("issued", IssuedElement);
                if (Applies != null) yield return new ElementValue("applies", Applies);
                foreach (var elem in Subject) { if (elem != null) yield return new ElementValue("subject", elem); }
                foreach (var elem in Authority) { if (elem != null) yield return new ElementValue("authority", elem); }
                foreach (var elem in Domain) { if (elem != null) yield return new ElementValue("domain", elem); }
                if (Type != null) yield return new ElementValue("type", Type);
                foreach (var elem in SubType) { if (elem != null) yield return new ElementValue("subType", elem); }
                foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                foreach (var elem in ActionReason) { if (elem != null) yield return new ElementValue("actionReason", elem); }
                foreach (var elem in Actor) { if (elem != null) yield return new ElementValue("actor", elem); }
                foreach (var elem in ValuedItem) { if (elem != null) yield return new ElementValue("valuedItem", elem); }
                foreach (var elem in Signer) { if (elem != null) yield return new ElementValue("signer", elem); }
                foreach (var elem in Term) { if (elem != null) yield return new ElementValue("term", elem); }
                if (Binding != null) yield return new ElementValue("binding", Binding);
                foreach (var elem in Friendly) { if (elem != null) yield return new ElementValue("friendly", elem); }
                foreach (var elem in Legal) { if (elem != null) yield return new ElementValue("legal", elem); }
                foreach (var elem in Rule) { if (elem != null) yield return new ElementValue("rule", elem); }
            }
        }
    
    }

}
