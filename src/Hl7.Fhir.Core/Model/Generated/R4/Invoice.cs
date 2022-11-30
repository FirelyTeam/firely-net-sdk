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
    /// Invoice containing ChargeItems from an Account
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Invoice", IsResource=true)]
    [DataContract]
    public partial class Invoice : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Invoice; } }
        [NotMapped]
        public override string TypeName { get { return "Invoice"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ParticipantComponent")]
        [DataContract]
        public partial class ParticipantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParticipantComponent"; } }
            
            /// <summary>
            /// Type of involvement in creation of this Invoice
            /// </summary>
            [FhirElement("role", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            /// <summary>
            /// Individual who was involved
            /// </summary>
            [FhirElement("actor", Order=50)]
            [CLSCompliant(false)]
            [References("Practitioner","Organization","Patient","PractitionerRole","Device","RelatedPerson")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Actor
            {
                get { return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Actor;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParticipantComponent");
                base.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Role?.Serialize(sink);
                sink.Element("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Actor?.Serialize(sink);
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
                    case "role":
                        Role = source.Populate(Role);
                        return true;
                    case "actor":
                        Actor = source.Populate(Actor);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParticipantComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Role != null) yield return Role;
                    if (Actor != null) yield return Actor;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Role != null) yield return new ElementValue("role", Role);
                    if (Actor != null) yield return new ElementValue("actor", Actor);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "LineItemComponent")]
        [DataContract]
        public partial class LineItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "LineItemComponent"; } }
            
            /// <summary>
            /// Sequence number of line item
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Sequence number of line item
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceElement = null;
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Reference to ChargeItem containing details of this line item or an inline billing code
            /// </summary>
            [FhirElement("chargeItem", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element ChargeItem
            {
                get { return _ChargeItem; }
                set { _ChargeItem = value; OnPropertyChanged("ChargeItem"); }
            }
            
            private Hl7.Fhir.Model.Element _ChargeItem;
            
            /// <summary>
            /// Components of total line item price
            /// </summary>
            [FhirElement("priceComponent", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<PriceComponentComponent> PriceComponent
            {
                get { if(_PriceComponent==null) _PriceComponent = new List<PriceComponentComponent>(); return _PriceComponent; }
                set { _PriceComponent = value; OnPropertyChanged("PriceComponent"); }
            }
            
            private List<PriceComponentComponent> _PriceComponent;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("LineItemComponent");
                base.Serialize(sink);
                sink.Element("sequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SequenceElement?.Serialize(sink);
                sink.Element("chargeItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); ChargeItem?.Serialize(sink);
                sink.BeginList("priceComponent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in PriceComponent)
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
                    case "sequence":
                        SequenceElement = source.PopulateValue(SequenceElement);
                        return true;
                    case "_sequence":
                        SequenceElement = source.Populate(SequenceElement);
                        return true;
                    case "chargeItemReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(ChargeItem, "chargeItem");
                        ChargeItem = source.Populate(ChargeItem as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "chargeItemCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(ChargeItem, "chargeItem");
                        ChargeItem = source.Populate(ChargeItem as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "priceComponent":
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
                    case "priceComponent":
                        source.PopulateListItem(PriceComponent, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LineItemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(ChargeItem != null) dest.ChargeItem = (Hl7.Fhir.Model.Element)ChargeItem.DeepCopy();
                    if(PriceComponent != null) dest.PriceComponent = new List<PriceComponentComponent>(PriceComponent.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new LineItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LineItemComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(ChargeItem, otherT.ChargeItem)) return false;
                if( !DeepComparable.Matches(PriceComponent, otherT.PriceComponent)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LineItemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(ChargeItem, otherT.ChargeItem)) return false;
                if( !DeepComparable.IsExactly(PriceComponent, otherT.PriceComponent)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (ChargeItem != null) yield return ChargeItem;
                    foreach (var elem in PriceComponent) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (ChargeItem != null) yield return new ElementValue("chargeItem", ChargeItem);
                    foreach (var elem in PriceComponent) { if (elem != null) yield return new ElementValue("priceComponent", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "PriceComponentComponent")]
        [DataContract]
        public partial class PriceComponentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PriceComponentComponent"; } }
            
            /// <summary>
            /// base | surcharge | deduction | discount | tax | informational
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.InvoicePriceComponentType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.InvoicePriceComponentType> _TypeElement;
            
            /// <summary>
            /// base | surcharge | deduction | discount | tax | informational
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.InvoicePriceComponentType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.R4.InvoicePriceComponentType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Code identifying the specific component
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Factor used for calculating this component
            /// </summary>
            [FhirElement("factor", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Factor used for calculating this component
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
            /// Monetary amount associated with this component
            /// </summary>
            [FhirElement("amount", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.R4.Money _Amount;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PriceComponentComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); TypeElement?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Code?.Serialize(sink);
                sink.Element("factor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); FactorElement?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Amount?.Serialize(sink);
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
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "factor":
                        FactorElement = source.PopulateValue(FactorElement);
                        return true;
                    case "_factor":
                        FactorElement = source.Populate(FactorElement);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PriceComponentComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.R4.InvoicePriceComponentType>)TypeElement.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.R4.Money)Amount.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PriceComponentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PriceComponentComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PriceComponentComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (Code != null) yield return Code;
                    if (FactorElement != null) yield return FactorElement;
                    if (Amount != null) yield return Amount;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Business Identifier for item
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
        /// draft | issued | balanced | cancelled | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.InvoiceStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.InvoiceStatus> _StatusElement;
        
        /// <summary>
        /// draft | issued | balanced | cancelled | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.InvoiceStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.R4.InvoiceStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reason for cancellation of this Invoice
        /// </summary>
        [FhirElement("cancelledReason", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CancelledReasonElement
        {
            get { return _CancelledReasonElement; }
            set { _CancelledReasonElement = value; OnPropertyChanged("CancelledReasonElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CancelledReasonElement;
        
        /// <summary>
        /// Reason for cancellation of this Invoice
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string CancelledReason
        {
            get { return CancelledReasonElement != null ? CancelledReasonElement.Value : null; }
            set
            {
                if (value == null)
                    CancelledReasonElement = null;
                else
                    CancelledReasonElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("CancelledReason");
            }
        }
        
        /// <summary>
        /// Type of Invoice
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Recipient(s) of goods and services
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Patient","Group")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Recipient of this invoice
        /// </summary>
        [FhirElement("recipient", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Organization","Patient","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recipient
        {
            get { return _Recipient; }
            set { _Recipient = value; OnPropertyChanged("Recipient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Recipient;
        
        /// <summary>
        /// Invoice date / posting date
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Invoice date / posting date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if (value == null)
                    DateElement = null;
                else
                    DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Participant in creation of this Invoice
        /// </summary>
        [FhirElement("participant", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<ParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<ParticipantComponent> _Participant;
        
        /// <summary>
        /// Issuing Organization of Invoice
        /// </summary>
        [FhirElement("issuer", Order=170)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Issuer
        {
            get { return _Issuer; }
            set { _Issuer = value; OnPropertyChanged("Issuer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Issuer;
        
        /// <summary>
        /// Account that is being balanced
        /// </summary>
        [FhirElement("account", Order=180)]
        [CLSCompliant(false)]
        [References("Account")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Account
        {
            get { return _Account; }
            set { _Account = value; OnPropertyChanged("Account"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Account;
        
        /// <summary>
        /// Line items of this Invoice
        /// </summary>
        [FhirElement("lineItem", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<LineItemComponent> LineItem
        {
            get { if(_LineItem==null) _LineItem = new List<LineItemComponent>(); return _LineItem; }
            set { _LineItem = value; OnPropertyChanged("LineItem"); }
        }
        
        private List<LineItemComponent> _LineItem;
        
        /// <summary>
        /// Components of Invoice total
        /// </summary>
        [FhirElement("totalPriceComponent", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<PriceComponentComponent> TotalPriceComponent
        {
            get { if(_TotalPriceComponent==null) _TotalPriceComponent = new List<PriceComponentComponent>(); return _TotalPriceComponent; }
            set { _TotalPriceComponent = value; OnPropertyChanged("TotalPriceComponent"); }
        }
        
        private List<PriceComponentComponent> _TotalPriceComponent;
        
        /// <summary>
        /// Net total of this Invoice
        /// </summary>
        [FhirElement("totalNet", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.R4.Money TotalNet
        {
            get { return _TotalNet; }
            set { _TotalNet = value; OnPropertyChanged("TotalNet"); }
        }
        
        private Hl7.Fhir.Model.R4.Money _TotalNet;
        
        /// <summary>
        /// Gross total of this Invoice
        /// </summary>
        [FhirElement("totalGross", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.R4.Money TotalGross
        {
            get { return _TotalGross; }
            set { _TotalGross = value; OnPropertyChanged("TotalGross"); }
        }
        
        private Hl7.Fhir.Model.R4.Money _TotalGross;
        
        /// <summary>
        /// Payment details
        /// </summary>
        [FhirElement("paymentTerms", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown PaymentTermsElement
        {
            get { return _PaymentTermsElement; }
            set { _PaymentTermsElement = value; OnPropertyChanged("PaymentTermsElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _PaymentTermsElement;
        
        /// <summary>
        /// Payment details
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PaymentTerms
        {
            get { return PaymentTermsElement != null ? PaymentTermsElement.Value : null; }
            set
            {
                if (value == null)
                    PaymentTermsElement = null;
                else
                    PaymentTermsElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("PaymentTerms");
            }
        }
        
        /// <summary>
        /// Comments made about the invoice
        /// </summary>
        [FhirElement("note", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Invoice;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.R4.InvoiceStatus>)StatusElement.DeepCopy();
                if(CancelledReasonElement != null) dest.CancelledReasonElement = (Hl7.Fhir.Model.FhirString)CancelledReasonElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Recipient != null) dest.Recipient = (Hl7.Fhir.Model.ResourceReference)Recipient.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Participant != null) dest.Participant = new List<ParticipantComponent>(Participant.DeepCopy());
                if(Issuer != null) dest.Issuer = (Hl7.Fhir.Model.ResourceReference)Issuer.DeepCopy();
                if(Account != null) dest.Account = (Hl7.Fhir.Model.ResourceReference)Account.DeepCopy();
                if(LineItem != null) dest.LineItem = new List<LineItemComponent>(LineItem.DeepCopy());
                if(TotalPriceComponent != null) dest.TotalPriceComponent = new List<PriceComponentComponent>(TotalPriceComponent.DeepCopy());
                if(TotalNet != null) dest.TotalNet = (Hl7.Fhir.Model.R4.Money)TotalNet.DeepCopy();
                if(TotalGross != null) dest.TotalGross = (Hl7.Fhir.Model.R4.Money)TotalGross.DeepCopy();
                if(PaymentTermsElement != null) dest.PaymentTermsElement = (Hl7.Fhir.Model.Markdown)PaymentTermsElement.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Invoice());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Invoice;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(CancelledReasonElement, otherT.CancelledReasonElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Recipient, otherT.Recipient)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
            if( !DeepComparable.Matches(Issuer, otherT.Issuer)) return false;
            if( !DeepComparable.Matches(Account, otherT.Account)) return false;
            if( !DeepComparable.Matches(LineItem, otherT.LineItem)) return false;
            if( !DeepComparable.Matches(TotalPriceComponent, otherT.TotalPriceComponent)) return false;
            if( !DeepComparable.Matches(TotalNet, otherT.TotalNet)) return false;
            if( !DeepComparable.Matches(TotalGross, otherT.TotalGross)) return false;
            if( !DeepComparable.Matches(PaymentTermsElement, otherT.PaymentTermsElement)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Invoice;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(CancelledReasonElement, otherT.CancelledReasonElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Recipient, otherT.Recipient)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            if( !DeepComparable.IsExactly(Issuer, otherT.Issuer)) return false;
            if( !DeepComparable.IsExactly(Account, otherT.Account)) return false;
            if( !DeepComparable.IsExactly(LineItem, otherT.LineItem)) return false;
            if( !DeepComparable.IsExactly(TotalPriceComponent, otherT.TotalPriceComponent)) return false;
            if( !DeepComparable.IsExactly(TotalNet, otherT.TotalNet)) return false;
            if( !DeepComparable.IsExactly(TotalGross, otherT.TotalGross)) return false;
            if( !DeepComparable.IsExactly(PaymentTermsElement, otherT.PaymentTermsElement)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Invoice");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("cancelledReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CancelledReasonElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Subject?.Serialize(sink);
            sink.Element("recipient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Recipient?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.BeginList("participant", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Participant)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("issuer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Issuer?.Serialize(sink);
            sink.Element("account", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Account?.Serialize(sink);
            sink.BeginList("lineItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in LineItem)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("totalPriceComponent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in TotalPriceComponent)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("totalNet", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TotalNet?.Serialize(sink);
            sink.Element("totalGross", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TotalGross?.Serialize(sink);
            sink.Element("paymentTerms", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PaymentTermsElement?.Serialize(sink);
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
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
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "cancelledReason":
                    CancelledReasonElement = source.PopulateValue(CancelledReasonElement);
                    return true;
                case "_cancelledReason":
                    CancelledReasonElement = source.Populate(CancelledReasonElement);
                    return true;
                case "type":
                    Type = source.Populate(Type);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "recipient":
                    Recipient = source.Populate(Recipient);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "participant":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "issuer":
                    Issuer = source.Populate(Issuer);
                    return true;
                case "account":
                    Account = source.Populate(Account);
                    return true;
                case "lineItem":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "totalPriceComponent":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "totalNet":
                    TotalNet = source.Populate(TotalNet);
                    return true;
                case "totalGross":
                    TotalGross = source.Populate(TotalGross);
                    return true;
                case "paymentTerms":
                    PaymentTermsElement = source.PopulateValue(PaymentTermsElement);
                    return true;
                case "_paymentTerms":
                    PaymentTermsElement = source.Populate(PaymentTermsElement);
                    return true;
                case "note":
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
                case "participant":
                    source.PopulateListItem(Participant, index);
                    return true;
                case "lineItem":
                    source.PopulateListItem(LineItem, index);
                    return true;
                case "totalPriceComponent":
                    source.PopulateListItem(TotalPriceComponent, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
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
                if (StatusElement != null) yield return StatusElement;
                if (CancelledReasonElement != null) yield return CancelledReasonElement;
                if (Type != null) yield return Type;
                if (Subject != null) yield return Subject;
                if (Recipient != null) yield return Recipient;
                if (DateElement != null) yield return DateElement;
                foreach (var elem in Participant) { if (elem != null) yield return elem; }
                if (Issuer != null) yield return Issuer;
                if (Account != null) yield return Account;
                foreach (var elem in LineItem) { if (elem != null) yield return elem; }
                foreach (var elem in TotalPriceComponent) { if (elem != null) yield return elem; }
                if (TotalNet != null) yield return TotalNet;
                if (TotalGross != null) yield return TotalGross;
                if (PaymentTermsElement != null) yield return PaymentTermsElement;
                foreach (var elem in Note) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (CancelledReasonElement != null) yield return new ElementValue("cancelledReason", CancelledReasonElement);
                if (Type != null) yield return new ElementValue("type", Type);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Recipient != null) yield return new ElementValue("recipient", Recipient);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                foreach (var elem in Participant) { if (elem != null) yield return new ElementValue("participant", elem); }
                if (Issuer != null) yield return new ElementValue("issuer", Issuer);
                if (Account != null) yield return new ElementValue("account", Account);
                foreach (var elem in LineItem) { if (elem != null) yield return new ElementValue("lineItem", elem); }
                foreach (var elem in TotalPriceComponent) { if (elem != null) yield return new ElementValue("totalPriceComponent", elem); }
                if (TotalNet != null) yield return new ElementValue("totalNet", TotalNet);
                if (TotalGross != null) yield return new ElementValue("totalGross", TotalGross);
                if (PaymentTermsElement != null) yield return new ElementValue("paymentTerms", PaymentTermsElement);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
            }
        }
    
    }

}
