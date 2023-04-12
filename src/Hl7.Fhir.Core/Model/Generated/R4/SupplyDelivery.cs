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
    /// Delivery of bulk Supplies
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "SupplyDelivery", IsResource=true)]
    [DataContract]
    public partial class SupplyDelivery : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ISupplyDelivery, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SupplyDelivery; } }
        [NotMapped]
        public override string TypeName { get { return "SupplyDelivery"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SuppliedItemComponent")]
        [DataContract]
        public partial class SuppliedItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SuppliedItemComponent"; } }
            
            /// <summary>
            /// Amount dispensed
            /// </summary>
            [FhirElement("quantity", Order=40)]
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
            [FhirElement("item", Order=50, Choice=ChoiceType.DatatypeChoice)]
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
                sink.BeginDataType("SuppliedItemComponent");
                base.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("item", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Item?.Serialize(sink);
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
                        Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
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
                var dest = other as SuppliedItemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(Item != null) dest.Item = (Hl7.Fhir.Model.Element)Item.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SuppliedItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SuppliedItemComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SuppliedItemComponent;
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
    
        
        /// <summary>
        /// External identifier
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Fulfills plan, proposal or order
        /// </summary>
        [FhirElement("basedOn", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [References("SupplyRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Part of referenced event
        /// </summary>
        [FhirElement("partOf", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [References("SupplyDelivery","Contract")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PartOf
        {
            get { if(_PartOf==null) _PartOf = new List<Hl7.Fhir.Model.ResourceReference>(); return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PartOf;
        
        /// <summary>
        /// in-progress | completed | abandoned | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.SupplyDeliveryStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.SupplyDeliveryStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | completed | abandoned | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.SupplyDeliveryStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.R4.SupplyDeliveryStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Patient for whom the item is supplied
        /// </summary>
        [FhirElement("patient", Order=130)]
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
        [FhirElement("type", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// The item that is delivered or supplied
        /// </summary>
        [FhirElement("suppliedItem", Order=150)]
        [DataMember]
        public SuppliedItemComponent SuppliedItem
        {
            get { return _SuppliedItem; }
            set { _SuppliedItem = value; OnPropertyChanged("SuppliedItem"); }
        }
        
        private SuppliedItemComponent _SuppliedItem;
        
        /// <summary>
        /// When event occurred
        /// </summary>
        [FhirElement("occurrence", InSummary=Hl7.Fhir.Model.Version.All, Order=160, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.R4.Timing))]
        [DataMember]
        public Hl7.Fhir.Model.Element Occurrence
        {
            get { return _Occurrence; }
            set { _Occurrence = value; OnPropertyChanged("Occurrence"); }
        }
        
        private Hl7.Fhir.Model.Element _Occurrence;
        
        /// <summary>
        /// Dispenser
        /// </summary>
        [FhirElement("supplier", Order=170)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Supplier
        {
            get { return _Supplier; }
            set { _Supplier = value; OnPropertyChanged("Supplier"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Supplier;
        
        /// <summary>
        /// Where the Supply was sent
        /// </summary>
        [FhirElement("destination", Order=180)]
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
        [FhirElement("receiver", Order=190)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole")]
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
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.R4.SupplyDeliveryStatus>)StatusElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(SuppliedItem != null) dest.SuppliedItem = (SuppliedItemComponent)SuppliedItem.DeepCopy();
                if(Occurrence != null) dest.Occurrence = (Hl7.Fhir.Model.Element)Occurrence.DeepCopy();
                if(Supplier != null) dest.Supplier = (Hl7.Fhir.Model.ResourceReference)Supplier.DeepCopy();
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
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(SuppliedItem, otherT.SuppliedItem)) return false;
            if( !DeepComparable.Matches(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.Matches(Supplier, otherT.Supplier)) return false;
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
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(SuppliedItem, otherT.SuppliedItem)) return false;
            if( !DeepComparable.IsExactly(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.IsExactly(Supplier, otherT.Supplier)) return false;
            if( !DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
            if( !DeepComparable.IsExactly(Receiver, otherT.Receiver)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("SupplyDelivery");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("basedOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in BasedOn)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("partOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in PartOf)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Patient?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
            sink.Element("suppliedItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SuppliedItem?.Serialize(sink);
            sink.Element("occurrence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Occurrence?.Serialize(sink);
            sink.Element("supplier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Supplier?.Serialize(sink);
            sink.Element("destination", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Destination?.Serialize(sink);
            sink.BeginList("receiver", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Receiver)
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
                case "basedOn":
                    BasedOn = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "partOf":
                    PartOf = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.SupplyDeliveryStatus>>();
                    return true;
                case "patient":
                    Patient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "type":
                    Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "suppliedItem":
                    SuppliedItem = source.Get<SuppliedItemComponent>();
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
                    source.CheckDuplicates<Hl7.Fhir.Model.R4.Timing>(Occurrence, "occurrence");
                    Occurrence = source.Get<Hl7.Fhir.Model.R4.Timing>();
                    return true;
                case "supplier":
                    Supplier = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "destination":
                    Destination = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "receiver":
                    Receiver = source.GetList<Hl7.Fhir.Model.ResourceReference>();
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
                case "basedOn":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "partOf":
                    source.SetList(this, jsonPropertyName);
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
                case "suppliedItem":
                    SuppliedItem = source.Populate(SuppliedItem);
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
                    source.CheckDuplicates<Hl7.Fhir.Model.R4.Timing>(Occurrence, "occurrence");
                    Occurrence = source.Populate(Occurrence as Hl7.Fhir.Model.R4.Timing);
                    return true;
                case "supplier":
                    Supplier = source.Populate(Supplier);
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
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "basedOn":
                    source.PopulateListItem(BasedOn, index);
                    return true;
                case "partOf":
                    source.PopulateListItem(PartOf, index);
                    return true;
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
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
                foreach (var elem in PartOf) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                if (Patient != null) yield return Patient;
                if (Type != null) yield return Type;
                if (SuppliedItem != null) yield return SuppliedItem;
                if (Occurrence != null) yield return Occurrence;
                if (Supplier != null) yield return Supplier;
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
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                foreach (var elem in PartOf) { if (elem != null) yield return new ElementValue("partOf", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Type != null) yield return new ElementValue("type", Type);
                if (SuppliedItem != null) yield return new ElementValue("suppliedItem", SuppliedItem);
                if (Occurrence != null) yield return new ElementValue("occurrence", Occurrence);
                if (Supplier != null) yield return new ElementValue("supplier", Supplier);
                if (Destination != null) yield return new ElementValue("destination", Destination);
                foreach (var elem in Receiver) { if (elem != null) yield return new ElementValue("receiver", elem); }
            }
        }
    
    }

}
