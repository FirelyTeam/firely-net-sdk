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
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Request for a medication, substance or device
    /// </summary>
    [FhirType("SupplyRequest", IsResource=true)]
    [DataContract]
    public partial class SupplyRequest : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SupplyRequest; } }
        [NotMapped]
        public override string TypeName { get { return "SupplyRequest"; } }
        
        /// <summary>
        /// Status of the supply request
        /// (url: http://hl7.org/fhir/ValueSet/supplyrequest-status)
        /// </summary>
        [FhirEnumeration("SupplyRequestStatus")]
        public enum SupplyRequestStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/supplyrequest-status)
            /// </summary>
            [EnumLiteral("draft", "http://hl7.org/fhir/supplyrequest-status"), Description("Draft")]
            Draft,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/supplyrequest-status)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/supplyrequest-status"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/supplyrequest-status)
            /// </summary>
            [EnumLiteral("suspended", "http://hl7.org/fhir/supplyrequest-status"), Description("Suspended")]
            Suspended,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/supplyrequest-status)
            /// </summary>
            [EnumLiteral("cancelled", "http://hl7.org/fhir/supplyrequest-status"), Description("Cancelled")]
            Cancelled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/supplyrequest-status)
            /// </summary>
            [EnumLiteral("completed", "http://hl7.org/fhir/supplyrequest-status"), Description("Completed")]
            Completed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/supplyrequest-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/supplyrequest-status"), Description("Entered in Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/supplyrequest-status)
            /// </summary>
            [EnumLiteral("unknown", "http://hl7.org/fhir/supplyrequest-status"), Description("Unknown")]
            Unknown,
        }

        [FhirType("OrderedItemComponent")]
        [DataContract]
        public partial class OrderedItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "OrderedItemComponent"; } }
            
            /// <summary>
            /// The requested amount of the item indicated
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Quantity _Quantity;
            
            /// <summary>
            /// Medication, Substance, or Device requested to be supplied
            /// </summary>
            [FhirElement("item", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private Hl7.Fhir.Model.Element _Item;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OrderedItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Quantity != null) dest.Quantity = (Quantity)Quantity.DeepCopy();
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
        
        
        [FhirType("RequesterComponent")]
        [DataContract]
        public partial class RequesterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "RequesterComponent"; } }
            
            /// <summary>
            /// Individual making the request
            /// </summary>
            [FhirElement("agent", InSummary=true, Order=40)]
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
        [FhirElement("identifier", InSummary=true, Order=90)]
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
        [FhirElement("status", InSummary=true, Order=100)]
        [DataMember]
        public Code<Hl7.Fhir.Model.SupplyRequest.SupplyRequestStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.SupplyRequest.SupplyRequestStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | suspended +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.SupplyRequest.SupplyRequestStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.SupplyRequest.SupplyRequestStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// The kind of supply (central, non-stock, etc.)
        /// </summary>
        [FhirElement("category", InSummary=true, Order=110)]
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
        [FhirElement("priority", InSummary=true, Order=120)]
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
                if (!value.HasValue)
                  PriorityElement = null; 
                else
                  PriorityElement = new Code<Hl7.Fhir.Model.RequestPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// The item being requested
        /// </summary>
        [FhirElement("orderedItem", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.SupplyRequest.OrderedItemComponent OrderedItem
        {
            get { return _OrderedItem; }
            set { _OrderedItem = value; OnPropertyChanged("OrderedItem"); }
        }
        
        private Hl7.Fhir.Model.SupplyRequest.OrderedItemComponent _OrderedItem;
        
        /// <summary>
        /// When the request should be fulfilled
        /// </summary>
        [FhirElement("occurrence", InSummary=true, Order=140, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Timing))]
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
        [FhirElement("authoredOn", InSummary=true, Order=150)]
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
        [FhirElement("requester", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.SupplyRequest.RequesterComponent Requester
        {
            get { return _Requester; }
            set { _Requester = value; OnPropertyChanged("Requester"); }
        }
        
        private Hl7.Fhir.Model.SupplyRequest.RequesterComponent _Requester;
        
        /// <summary>
        /// Who is intended to fulfill the request
        /// </summary>
        [FhirElement("supplier", InSummary=true, Order=170)]
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
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SupplyRequest;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.SupplyRequest.SupplyRequestStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.RequestPriority>)PriorityElement.DeepCopy();
                if(OrderedItem != null) dest.OrderedItem = (Hl7.Fhir.Model.SupplyRequest.OrderedItemComponent)OrderedItem.DeepCopy();
                if(Occurrence != null) dest.Occurrence = (Hl7.Fhir.Model.Element)Occurrence.DeepCopy();
                if(AuthoredOnElement != null) dest.AuthoredOnElement = (Hl7.Fhir.Model.FhirDateTime)AuthoredOnElement.DeepCopy();
                if(Requester != null) dest.Requester = (Hl7.Fhir.Model.SupplyRequest.RequesterComponent)Requester.DeepCopy();
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
