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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// Request for a medication, substance or device
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "SupplyRequest", IsResource=true)]
    [DataContract]
    public partial class SupplyRequest : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ISupplyRequest, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SupplyRequest; } }
        [NotMapped]
        public override string TypeName { get { return "SupplyRequest"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "OrderedItemComponent")]
        [DataContract]
        public partial class OrderedItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OrderedItemComponent"; } }
            
            /// <summary>
            /// The requested amount of the item indicated
            /// </summary>
            [FhirElement("quantity", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
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
            /// Medication, Substance, or Device requested to be supplied
            /// </summary>
            [FhirElement("item", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private Hl7.Fhir.Model.Element _Item;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("OrderedItemComponent");
                base.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Quantity?.Serialize(sink);
                sink.Element("item", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Item?.Serialize(sink);
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
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.Quantity>();
                        return true;
                    case "itemCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Item, "item");
                        Item = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "itemReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Item, "item");
                        Item = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "itemCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Item, "item");
                        Item = source.Populate(Item as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "itemReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Item, "item");
                        Item = source.Populate(Item as Hl7.Fhir.Model.ResourceReference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OrderedItemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(Item != null) dest.Item = (Hl7.Fhir.Model.Element)Item.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new OrderedItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OrderedItemComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OrderedItemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Quantity != null) yield return Quantity;
                    if (Item != null) yield return Item;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (Item != null) yield return new ElementValue("item", Item);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RequesterComponent")]
        [DataContract]
        public partial class RequesterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RequesterComponent"; } }
            
            /// <summary>
            /// Individual making the request
            /// </summary>
            [FhirElement("agent", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [References("Practitioner","Organization","Patient","RelatedPerson","Device")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Agent
            {
                get { return _Agent; }
                set { _Agent = value; OnPropertyChanged("Agent"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Agent;
            
            /// <summary>
            /// Organization agent is acting for
            /// </summary>
            [FhirElement("onBehalfOf", Order=50)]
            [CLSCompliant(false)]
            [References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference OnBehalfOf
            {
                get { return _OnBehalfOf; }
                set { _OnBehalfOf = value; OnPropertyChanged("OnBehalfOf"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _OnBehalfOf;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RequesterComponent");
                base.Serialize(sink);
                sink.Element("agent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Agent?.Serialize(sink);
                sink.Element("onBehalfOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OnBehalfOf?.Serialize(sink);
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
                    case "agent":
                        Agent = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "onBehalfOf":
                        OnBehalfOf = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "agent":
                        Agent = source.Populate(Agent);
                        return true;
                    case "onBehalfOf":
                        OnBehalfOf = source.Populate(OnBehalfOf);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RequesterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Agent != null) dest.Agent = (Hl7.Fhir.Model.ResourceReference)Agent.DeepCopy();
                    if(OnBehalfOf != null) dest.OnBehalfOf = (Hl7.Fhir.Model.ResourceReference)OnBehalfOf.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RequesterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RequesterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Agent, otherT.Agent)) return false;
                if( !DeepComparable.Matches(OnBehalfOf, otherT.OnBehalfOf)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RequesterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Agent, otherT.Agent)) return false;
                if( !DeepComparable.IsExactly(OnBehalfOf, otherT.OnBehalfOf)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Agent != null) yield return Agent;
                    if (OnBehalfOf != null) yield return OnBehalfOf;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Agent != null) yield return new ElementValue("agent", Agent);
                    if (OnBehalfOf != null) yield return new ElementValue("onBehalfOf", OnBehalfOf);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Unique identifier
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
        /// draft | active | suspended +
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.SupplyRequestStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.SupplyRequestStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | suspended +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.SupplyRequestStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.STU3.SupplyRequestStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// The kind of supply (central, non-stock, etc.)
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        [FhirElement("priority", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RequestPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RequestPriority> _PriorityElement;
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.RequestPriority? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if (value == null)
                    PriorityElement = null;
                else
                    PriorityElement = new Code<Hl7.Fhir.Model.RequestPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// The item being requested
        /// </summary>
        [FhirElement("orderedItem", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public OrderedItemComponent OrderedItem
        {
            get { return _OrderedItem; }
            set { _OrderedItem = value; OnPropertyChanged("OrderedItem"); }
        }
        
        private OrderedItemComponent _OrderedItem;
        
        /// <summary>
        /// When the request should be fulfilled
        /// </summary>
        [FhirElement("occurrence", InSummary=Hl7.Fhir.Model.Version.All, Order=140, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.STU3.Timing))]
        [DataMember]
        public Hl7.Fhir.Model.Element Occurrence
        {
            get { return _Occurrence; }
            set { _Occurrence = value; OnPropertyChanged("Occurrence"); }
        }
        
        private Hl7.Fhir.Model.Element _Occurrence;
        
        /// <summary>
        /// When the request was made
        /// </summary>
        [FhirElement("authoredOn", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AuthoredOnElement
        {
            get { return _AuthoredOnElement; }
            set { _AuthoredOnElement = value; OnPropertyChanged("AuthoredOnElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _AuthoredOnElement;
        
        /// <summary>
        /// When the request was made
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AuthoredOn
        {
            get { return AuthoredOnElement != null ? AuthoredOnElement.Value : null; }
            set
            {
                if (value == null)
                    AuthoredOnElement = null;
                else
                    AuthoredOnElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("AuthoredOn");
            }
        }
        
        /// <summary>
        /// Who/what is requesting service
        /// </summary>
        [FhirElement("requester", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public RequesterComponent Requester
        {
            get { return _Requester; }
            set { _Requester = value; OnPropertyChanged("Requester"); }
        }
        
        private RequesterComponent _Requester;
        
        /// <summary>
        /// Who is intended to fulfill the request
        /// </summary>
        [FhirElement("supplier", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [References("Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Supplier
        {
            get { if(_Supplier==null) _Supplier = new List<Hl7.Fhir.Model.ResourceReference>(); return _Supplier; }
            set { _Supplier = value; OnPropertyChanged("Supplier"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Supplier;
        
        /// <summary>
        /// Why the supply item was requested
        /// </summary>
        [FhirElement("reason", Order=180, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private Hl7.Fhir.Model.Element _Reason;
        
        /// <summary>
        /// The origin of the supply
        /// </summary>
        [FhirElement("deliverFrom", Order=190)]
        [CLSCompliant(false)]
        [References("Organization","Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference DeliverFrom
        {
            get { return _DeliverFrom; }
            set { _DeliverFrom = value; OnPropertyChanged("DeliverFrom"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _DeliverFrom;
        
        /// <summary>
        /// The destination of the supply
        /// </summary>
        [FhirElement("deliverTo", Order=200)]
        [CLSCompliant(false)]
        [References("Organization","Location","Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference DeliverTo
        {
            get { return _DeliverTo; }
            set { _DeliverTo = value; OnPropertyChanged("DeliverTo"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _DeliverTo;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SupplyRequest;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.STU3.SupplyRequestStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.RequestPriority>)PriorityElement.DeepCopy();
                if(OrderedItem != null) dest.OrderedItem = (OrderedItemComponent)OrderedItem.DeepCopy();
                if(Occurrence != null) dest.Occurrence = (Hl7.Fhir.Model.Element)Occurrence.DeepCopy();
                if(AuthoredOnElement != null) dest.AuthoredOnElement = (Hl7.Fhir.Model.FhirDateTime)AuthoredOnElement.DeepCopy();
                if(Requester != null) dest.Requester = (RequesterComponent)Requester.DeepCopy();
                if(Supplier != null) dest.Supplier = new List<Hl7.Fhir.Model.ResourceReference>(Supplier.DeepCopy());
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.Element)Reason.DeepCopy();
                if(DeliverFrom != null) dest.DeliverFrom = (Hl7.Fhir.Model.ResourceReference)DeliverFrom.DeepCopy();
                if(DeliverTo != null) dest.DeliverTo = (Hl7.Fhir.Model.ResourceReference)DeliverTo.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new SupplyRequest());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SupplyRequest;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.Matches(OrderedItem, otherT.OrderedItem)) return false;
            if( !DeepComparable.Matches(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.Matches(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.Matches(Requester, otherT.Requester)) return false;
            if( !DeepComparable.Matches(Supplier, otherT.Supplier)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(DeliverFrom, otherT.DeliverFrom)) return false;
            if( !DeepComparable.Matches(DeliverTo, otherT.DeliverTo)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SupplyRequest;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.IsExactly(OrderedItem, otherT.OrderedItem)) return false;
            if( !DeepComparable.IsExactly(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.IsExactly(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.IsExactly(Requester, otherT.Requester)) return false;
            if( !DeepComparable.IsExactly(Supplier, otherT.Supplier)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(DeliverFrom, otherT.DeliverFrom)) return false;
            if( !DeepComparable.IsExactly(DeliverTo, otherT.DeliverTo)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("SupplyRequest");
            base.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Category?.Serialize(sink);
            sink.Element("priority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PriorityElement?.Serialize(sink);
            sink.Element("orderedItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OrderedItem?.Serialize(sink);
            sink.Element("occurrence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Occurrence?.Serialize(sink);
            sink.Element("authoredOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AuthoredOnElement?.Serialize(sink);
            sink.Element("requester", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Requester?.Serialize(sink);
            sink.BeginList("supplier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Supplier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Reason?.Serialize(sink);
            sink.Element("deliverFrom", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DeliverFrom?.Serialize(sink);
            sink.Element("deliverTo", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DeliverTo?.Serialize(sink);
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
                    Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.SupplyRequestStatus>>();
                    return true;
                case "category":
                    Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "priority":
                    PriorityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.RequestPriority>>();
                    return true;
                case "orderedItem":
                    OrderedItem = source.Get<OrderedItemComponent>();
                    return true;
                case "occurrenceDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Occurrence, "occurrence");
                    Occurrence = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "occurrencePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Occurrence, "occurrence");
                    Occurrence = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "occurrenceTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(Occurrence, "occurrence");
                    Occurrence = source.Get<Hl7.Fhir.Model.STU3.Timing>();
                    return true;
                case "authoredOn":
                    AuthoredOnElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "requester":
                    Requester = source.Get<RequesterComponent>();
                    return true;
                case "supplier":
                    Supplier = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "reasonCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Reason, "reason");
                    Reason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "reasonReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Reason, "reason");
                    Reason = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "deliverFrom":
                    DeliverFrom = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "deliverTo":
                    DeliverTo = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    Identifier = source.Populate(Identifier);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "category":
                    Category = source.Populate(Category);
                    return true;
                case "priority":
                    PriorityElement = source.PopulateValue(PriorityElement);
                    return true;
                case "_priority":
                    PriorityElement = source.Populate(PriorityElement);
                    return true;
                case "orderedItem":
                    OrderedItem = source.Populate(OrderedItem);
                    return true;
                case "occurrenceDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Occurrence, "occurrence");
                    Occurrence = source.PopulateValue(Occurrence as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_occurrenceDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Occurrence, "occurrence");
                    Occurrence = source.Populate(Occurrence as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "occurrencePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Occurrence, "occurrence");
                    Occurrence = source.Populate(Occurrence as Hl7.Fhir.Model.Period);
                    return true;
                case "occurrenceTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(Occurrence, "occurrence");
                    Occurrence = source.Populate(Occurrence as Hl7.Fhir.Model.STU3.Timing);
                    return true;
                case "authoredOn":
                    AuthoredOnElement = source.PopulateValue(AuthoredOnElement);
                    return true;
                case "_authoredOn":
                    AuthoredOnElement = source.Populate(AuthoredOnElement);
                    return true;
                case "requester":
                    Requester = source.Populate(Requester);
                    return true;
                case "supplier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Reason, "reason");
                    Reason = source.Populate(Reason as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "reasonReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Reason, "reason");
                    Reason = source.Populate(Reason as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "deliverFrom":
                    DeliverFrom = source.Populate(DeliverFrom);
                    return true;
                case "deliverTo":
                    DeliverTo = source.Populate(DeliverTo);
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
                case "supplier":
                    source.PopulateListItem(Supplier, index);
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
                if (StatusElement != null) yield return StatusElement;
                if (Category != null) yield return Category;
                if (PriorityElement != null) yield return PriorityElement;
                if (OrderedItem != null) yield return OrderedItem;
                if (Occurrence != null) yield return Occurrence;
                if (AuthoredOnElement != null) yield return AuthoredOnElement;
                if (Requester != null) yield return Requester;
                foreach (var elem in Supplier) { if (elem != null) yield return elem; }
                if (Reason != null) yield return Reason;
                if (DeliverFrom != null) yield return DeliverFrom;
                if (DeliverTo != null) yield return DeliverTo;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Category != null) yield return new ElementValue("category", Category);
                if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
                if (OrderedItem != null) yield return new ElementValue("orderedItem", OrderedItem);
                if (Occurrence != null) yield return new ElementValue("occurrence", Occurrence);
                if (AuthoredOnElement != null) yield return new ElementValue("authoredOn", AuthoredOnElement);
                if (Requester != null) yield return new ElementValue("requester", Requester);
                foreach (var elem in Supplier) { if (elem != null) yield return new ElementValue("supplier", elem); }
                if (Reason != null) yield return new ElementValue("reason", Reason);
                if (DeliverFrom != null) yield return new ElementValue("deliverFrom", DeliverFrom);
                if (DeliverTo != null) yield return new ElementValue("deliverTo", DeliverTo);
            }
        }
    
    }

}
