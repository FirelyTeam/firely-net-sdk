using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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

//
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A supply -  request and provision
    /// </summary>
    [FhirType("Supply", IsResource=true)]
    [DataContract]
    public partial class Supply : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Status of the dispense
        /// </summary>
        [FhirEnumeration("SupplyDispenseStatus")]
        public enum SupplyDispenseStatus
        {
            /// <summary>
            /// Supply has been requested, but not dispensed.
            /// </summary>
            [EnumLiteral("in progress")]
            InProgress,
            /// <summary>
            /// Supply is part of a pharmacy order and has been dispensed.
            /// </summary>
            [EnumLiteral("dispensed")]
            Dispensed,
            /// <summary>
            /// Dispensing was not completed.
            /// </summary>
            [EnumLiteral("abandoned")]
            Abandoned,
        }
        
        /// <summary>
        /// Status of the supply
        /// </summary>
        [FhirEnumeration("SupplyStatus")]
        public enum SupplyStatus
        {
            /// <summary>
            /// Supply has been requested, but not dispensed.
            /// </summary>
            [EnumLiteral("requested")]
            Requested,
            /// <summary>
            /// Supply is part of a pharmacy order and has been dispensed.
            /// </summary>
            [EnumLiteral("dispensed")]
            Dispensed,
            /// <summary>
            /// Supply has been received by the requestor.
            /// </summary>
            [EnumLiteral("received")]
            Received,
            /// <summary>
            /// The supply will not be completed because the supplier was unable or unwilling to supply the item.
            /// </summary>
            [EnumLiteral("failed")]
            Failed,
            /// <summary>
            /// The orderer of the supply cancelled the request.
            /// </summary>
            [EnumLiteral("cancelled")]
            Cancelled,
        }
        
        [FhirType("SupplyDispenseComponent")]
        [DataContract]
        public partial class SupplyDispenseComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// External identifier
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// in progress | dispensed | abandoned
            /// </summary>
            [FhirElement("status", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Supply.SupplyDispenseStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            private Code<Hl7.Fhir.Model.Supply.SupplyDispenseStatus> _StatusElement;
            
            /// <summary>
            /// in progress | dispensed | abandoned
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Supply.SupplyDispenseStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if(value == null)
                      StatusElement = null; 
                    else
                      StatusElement = new Code<Hl7.Fhir.Model.Supply.SupplyDispenseStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// Category of dispense event
            /// </summary>
            [FhirElement("type", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Amount dispensed
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            private Hl7.Fhir.Model.Quantity _Quantity;
            
            /// <summary>
            /// Medication, Substance, or Device supplied
            /// </summary>
            [FhirElement("suppliedItem", InSummary=true, Order=80)]
            [References("Medication","Substance","Device")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference SuppliedItem
            {
                get { return _SuppliedItem; }
                set { _SuppliedItem = value; OnPropertyChanged("SuppliedItem"); }
            }
            private Hl7.Fhir.Model.ResourceReference _SuppliedItem;
            
            /// <summary>
            /// Dispenser
            /// </summary>
            [FhirElement("supplier", InSummary=true, Order=90)]
            [References("Practitioner")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Supplier
            {
                get { return _Supplier; }
                set { _Supplier = value; OnPropertyChanged("Supplier"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Supplier;
            
            /// <summary>
            /// Dispensing time
            /// </summary>
            [FhirElement("whenPrepared", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Period WhenPrepared
            {
                get { return _WhenPrepared; }
                set { _WhenPrepared = value; OnPropertyChanged("WhenPrepared"); }
            }
            private Hl7.Fhir.Model.Period _WhenPrepared;
            
            /// <summary>
            /// Handover time
            /// </summary>
            [FhirElement("whenHandedOver", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Period WhenHandedOver
            {
                get { return _WhenHandedOver; }
                set { _WhenHandedOver = value; OnPropertyChanged("WhenHandedOver"); }
            }
            private Hl7.Fhir.Model.Period _WhenHandedOver;
            
            /// <summary>
            /// Where the Supply was sent
            /// </summary>
            [FhirElement("destination", InSummary=true, Order=120)]
            [References("Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Destination
            {
                get { return _Destination; }
                set { _Destination = value; OnPropertyChanged("Destination"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Destination;
            
            /// <summary>
            /// Who collected the Supply
            /// </summary>
            [FhirElement("receiver", InSummary=true, Order=130)]
            [References("Practitioner")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Receiver
            {
                get { return _Receiver; }
                set { _Receiver = value; OnPropertyChanged("Receiver"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _Receiver;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SupplyDispenseComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Supply.SupplyDispenseStatus>)StatusElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(SuppliedItem != null) dest.SuppliedItem = (Hl7.Fhir.Model.ResourceReference)SuppliedItem.DeepCopy();
                    if(Supplier != null) dest.Supplier = (Hl7.Fhir.Model.ResourceReference)Supplier.DeepCopy();
                    if(WhenPrepared != null) dest.WhenPrepared = (Hl7.Fhir.Model.Period)WhenPrepared.DeepCopy();
                    if(WhenHandedOver != null) dest.WhenHandedOver = (Hl7.Fhir.Model.Period)WhenHandedOver.DeepCopy();
                    if(Destination != null) dest.Destination = (Hl7.Fhir.Model.ResourceReference)Destination.DeepCopy();
                    if(Receiver != null) dest.Receiver = new List<Hl7.Fhir.Model.ResourceReference>(Receiver.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SupplyDispenseComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SupplyDispenseComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(SuppliedItem, otherT.SuppliedItem)) return false;
                if( !DeepComparable.Matches(Supplier, otherT.Supplier)) return false;
                if( !DeepComparable.Matches(WhenPrepared, otherT.WhenPrepared)) return false;
                if( !DeepComparable.Matches(WhenHandedOver, otherT.WhenHandedOver)) return false;
                if( !DeepComparable.Matches(Destination, otherT.Destination)) return false;
                if( !DeepComparable.Matches(Receiver, otherT.Receiver)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SupplyDispenseComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(SuppliedItem, otherT.SuppliedItem)) return false;
                if( !DeepComparable.IsExactly(Supplier, otherT.Supplier)) return false;
                if( !DeepComparable.IsExactly(WhenPrepared, otherT.WhenPrepared)) return false;
                if( !DeepComparable.IsExactly(WhenHandedOver, otherT.WhenHandedOver)) return false;
                if( !DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
                if( !DeepComparable.IsExactly(Receiver, otherT.Receiver)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// The kind of supply (central, non-stock, etc)
        /// </summary>
        [FhirElement("kind", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Kind
        {
            get { return _Kind; }
            set { _Kind = value; OnPropertyChanged("Kind"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Kind;
        
        /// <summary>
        /// Unique identifier
        /// </summary>
        [FhirElement("identifier", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// requested | dispensed | received | failed | cancelled
        /// </summary>
        [FhirElement("status", Order=90)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Supply.SupplyStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.Supply.SupplyStatus> _StatusElement;
        
        /// <summary>
        /// requested | dispensed | received | failed | cancelled
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Supply.SupplyStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Supply.SupplyStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Medication, Substance, or Device requested to be supplied
        /// </summary>
        [FhirElement("orderedItem", Order=100)]
        [References("Medication","Substance","Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference OrderedItem
        {
            get { return _OrderedItem; }
            set { _OrderedItem = value; OnPropertyChanged("OrderedItem"); }
        }
        private Hl7.Fhir.Model.ResourceReference _OrderedItem;
        
        /// <summary>
        /// Patient for whom the item is supplied
        /// </summary>
        [FhirElement("patient", Order=110)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Supply details
        /// </summary>
        [FhirElement("dispense", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Supply.SupplyDispenseComponent> Dispense
        {
            get { return _Dispense; }
            set { _Dispense = value; OnPropertyChanged("Dispense"); }
        }
        private List<Hl7.Fhir.Model.Supply.SupplyDispenseComponent> _Dispense;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Supply;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Kind != null) dest.Kind = (Hl7.Fhir.Model.CodeableConcept)Kind.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Supply.SupplyStatus>)StatusElement.DeepCopy();
                if(OrderedItem != null) dest.OrderedItem = (Hl7.Fhir.Model.ResourceReference)OrderedItem.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Dispense != null) dest.Dispense = new List<Hl7.Fhir.Model.Supply.SupplyDispenseComponent>(Dispense.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Supply());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Supply;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Kind, otherT.Kind)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(OrderedItem, otherT.OrderedItem)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Dispense, otherT.Dispense)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Supply;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Kind, otherT.Kind)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(OrderedItem, otherT.OrderedItem)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Dispense, otherT.Dispense)) return false;
            
            return true;
        }
        
    }
    
}
