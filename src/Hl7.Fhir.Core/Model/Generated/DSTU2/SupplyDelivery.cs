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
    /// Delivery of Supply
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "SupplyDelivery", IsResource=true)]
    [DataContract]
    public partial class SupplyDelivery : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ISupplyDelivery, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SupplyDelivery; } }
        [NotMapped]
        public override string TypeName { get { return "SupplyDelivery"; } }
    
        
        /// <summary>
        /// External identifier
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
        /// in-progress | completed | abandoned
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.SupplyDeliveryStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.SupplyDeliveryStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | completed | abandoned
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.SupplyDeliveryStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.SupplyDeliveryStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Patient for whom the item is supplied
        /// </summary>
        [FhirElement("patient", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Category of dispense event
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
        /// Amount dispensed
        /// </summary>
        [FhirElement("quantity", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.SimpleQuantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Hl7.Fhir.Model.SimpleQuantity _Quantity;
        
        /// <summary>
        /// Medication, Substance, or Device supplied
        /// </summary>
        [FhirElement("suppliedItem", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
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
        [FhirElement("supplier", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
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
        [FhirElement("whenPrepared", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
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
        [FhirElement("time", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime TimeElement
        {
            get { return _TimeElement; }
            set { _TimeElement = value; OnPropertyChanged("TimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _TimeElement;
        
        /// <summary>
        /// Handover time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Time
        {
            get { return TimeElement != null ? TimeElement.Value : null; }
            set
            {
                if (value == null)
                    TimeElement = null;
                else
                    TimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Time");
            }
        }
        
        /// <summary>
        /// Where the Supply was sent
        /// </summary>
        [FhirElement("destination", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
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
        [FhirElement("receiver", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [References("Practitioner")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Receiver
        {
            get { if(_Receiver==null) _Receiver = new List<Hl7.Fhir.Model.ResourceReference>(); return _Receiver; }
            set { _Receiver = value; OnPropertyChanged("Receiver"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Receiver;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SupplyDelivery;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.SupplyDeliveryStatus>)StatusElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                if(SuppliedItem != null) dest.SuppliedItem = (Hl7.Fhir.Model.ResourceReference)SuppliedItem.DeepCopy();
                if(Supplier != null) dest.Supplier = (Hl7.Fhir.Model.ResourceReference)Supplier.DeepCopy();
                if(WhenPrepared != null) dest.WhenPrepared = (Hl7.Fhir.Model.Period)WhenPrepared.DeepCopy();
                if(TimeElement != null) dest.TimeElement = (Hl7.Fhir.Model.FhirDateTime)TimeElement.DeepCopy();
                if(Destination != null) dest.Destination = (Hl7.Fhir.Model.ResourceReference)Destination.DeepCopy();
                if(Receiver != null) dest.Receiver = new List<Hl7.Fhir.Model.ResourceReference>(Receiver.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new SupplyDelivery());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SupplyDelivery;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(SuppliedItem, otherT.SuppliedItem)) return false;
            if( !DeepComparable.Matches(Supplier, otherT.Supplier)) return false;
            if( !DeepComparable.Matches(WhenPrepared, otherT.WhenPrepared)) return false;
            if( !DeepComparable.Matches(TimeElement, otherT.TimeElement)) return false;
            if( !DeepComparable.Matches(Destination, otherT.Destination)) return false;
            if( !DeepComparable.Matches(Receiver, otherT.Receiver)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SupplyDelivery;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(SuppliedItem, otherT.SuppliedItem)) return false;
            if( !DeepComparable.IsExactly(Supplier, otherT.Supplier)) return false;
            if( !DeepComparable.IsExactly(WhenPrepared, otherT.WhenPrepared)) return false;
            if( !DeepComparable.IsExactly(TimeElement, otherT.TimeElement)) return false;
            if( !DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
            if( !DeepComparable.IsExactly(Receiver, otherT.Receiver)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("SupplyDelivery");
            base.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Patient?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
            sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Quantity?.Serialize(sink);
            sink.Element("suppliedItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SuppliedItem?.Serialize(sink);
            sink.Element("supplier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Supplier?.Serialize(sink);
            sink.Element("whenPrepared", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); WhenPrepared?.Serialize(sink);
            sink.Element("time", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TimeElement?.Serialize(sink);
            sink.Element("destination", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Destination?.Serialize(sink);
            sink.BeginList("receiver", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Receiver)
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
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "type":
                    Type = source.Populate(Type);
                    return true;
                case "quantity":
                    Quantity = source.Populate(Quantity);
                    return true;
                case "suppliedItem":
                    SuppliedItem = source.Populate(SuppliedItem);
                    return true;
                case "supplier":
                    Supplier = source.Populate(Supplier);
                    return true;
                case "whenPrepared":
                    WhenPrepared = source.Populate(WhenPrepared);
                    return true;
                case "time":
                    TimeElement = source.PopulateValue(TimeElement);
                    return true;
                case "_time":
                    TimeElement = source.Populate(TimeElement);
                    return true;
                case "destination":
                    Destination = source.Populate(Destination);
                    return true;
                case "receiver":
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
                case "receiver":
                    source.PopulateListItem(Receiver, index);
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
                if (Patient != null) yield return Patient;
                if (Type != null) yield return Type;
                if (Quantity != null) yield return Quantity;
                if (SuppliedItem != null) yield return SuppliedItem;
                if (Supplier != null) yield return Supplier;
                if (WhenPrepared != null) yield return WhenPrepared;
                if (TimeElement != null) yield return TimeElement;
                if (Destination != null) yield return Destination;
                foreach (var elem in Receiver) { if (elem != null) yield return elem; }
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
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Type != null) yield return new ElementValue("type", Type);
                if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                if (SuppliedItem != null) yield return new ElementValue("suppliedItem", SuppliedItem);
                if (Supplier != null) yield return new ElementValue("supplier", Supplier);
                if (WhenPrepared != null) yield return new ElementValue("whenPrepared", WhenPrepared);
                if (TimeElement != null) yield return new ElementValue("time", TimeElement);
                if (Destination != null) yield return new ElementValue("destination", Destination);
                foreach (var elem in Receiver) { if (elem != null) yield return new ElementValue("receiver", elem); }
            }
        }
    
    }

}
