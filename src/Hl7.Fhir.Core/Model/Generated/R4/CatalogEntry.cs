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
    /// An entry in a catalog
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "CatalogEntry", IsResource=true)]
    [DataContract]
    public partial class CatalogEntry : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.CatalogEntry; } }
        [NotMapped]
        public override string TypeName { get { return "CatalogEntry"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "RelatedEntryComponent")]
        [DataContract]
        public partial class RelatedEntryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedEntryComponent"; } }
            
            /// <summary>
            /// triggers | is-replaced-by
            /// </summary>
            [FhirElement("relationtype", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.CatalogEntryRelationType> RelationtypeElement
            {
                get { return _RelationtypeElement; }
                set { _RelationtypeElement = value; OnPropertyChanged("RelationtypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.CatalogEntryRelationType> _RelationtypeElement;
            
            /// <summary>
            /// triggers | is-replaced-by
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.CatalogEntryRelationType? Relationtype
            {
                get { return RelationtypeElement != null ? RelationtypeElement.Value : null; }
                set
                {
                    if (value == null)
                        RelationtypeElement = null;
                    else
                        RelationtypeElement = new Code<Hl7.Fhir.Model.R4.CatalogEntryRelationType>(value);
                    OnPropertyChanged("Relationtype");
                }
            }
            
            /// <summary>
            /// The reference to the related item
            /// </summary>
            [FhirElement("item", Order=50)]
            [CLSCompliant(false)]
            [References("CatalogEntry")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Item;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RelatedEntryComponent");
                base.Serialize(sink);
                sink.Element("relationtype", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); RelationtypeElement?.Serialize(sink);
                sink.Element("item", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Item?.Serialize(sink);
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
                    case "relationtype":
                        RelationtypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.CatalogEntryRelationType>>();
                        return true;
                    case "item":
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
                    case "relationtype":
                        RelationtypeElement = source.PopulateValue(RelationtypeElement);
                        return true;
                    case "_relationtype":
                        RelationtypeElement = source.Populate(RelationtypeElement);
                        return true;
                    case "item":
                        Item = source.Populate(Item);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedEntryComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RelationtypeElement != null) dest.RelationtypeElement = (Code<Hl7.Fhir.Model.R4.CatalogEntryRelationType>)RelationtypeElement.DeepCopy();
                    if(Item != null) dest.Item = (Hl7.Fhir.Model.ResourceReference)Item.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RelatedEntryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RelatedEntryComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RelationtypeElement, otherT.RelationtypeElement)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedEntryComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RelationtypeElement, otherT.RelationtypeElement)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (RelationtypeElement != null) yield return RelationtypeElement;
                    if (Item != null) yield return Item;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (RelationtypeElement != null) yield return new ElementValue("relationtype", RelationtypeElement);
                    if (Item != null) yield return new ElementValue("item", Item);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Unique identifier of the catalog item
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
        /// The type of item - medication, device, service, protocol or other
        /// </summary>
        [FhirElement("type", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Whether the entry represents an orderable item
        /// </summary>
        [FhirElement("orderable", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean OrderableElement
        {
            get { return _OrderableElement; }
            set { _OrderableElement = value; OnPropertyChanged("OrderableElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _OrderableElement;
        
        /// <summary>
        /// Whether the entry represents an orderable item
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Orderable
        {
            get { return OrderableElement != null ? OrderableElement.Value : null; }
            set
            {
                if (value == null)
                    OrderableElement = null;
                else
                    OrderableElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Orderable");
            }
        }
        
        /// <summary>
        /// The item that is being defined
        /// </summary>
        [FhirElement("referencedItem", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("Medication","Device","Organization","Practitioner","PractitionerRole","HealthcareService","ActivityDefinition","PlanDefinition","SpecimenDefinition","ObservationDefinition","Binary")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ReferencedItem
        {
            get { return _ReferencedItem; }
            set { _ReferencedItem = value; OnPropertyChanged("ReferencedItem"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ReferencedItem;
        
        /// <summary>
        /// Any additional identifier(s) for the catalog item, in the same granularity or concept
        /// </summary>
        [FhirElement("additionalIdentifier", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> AdditionalIdentifier
        {
            get { if(_AdditionalIdentifier==null) _AdditionalIdentifier = new List<Hl7.Fhir.Model.Identifier>(); return _AdditionalIdentifier; }
            set { _AdditionalIdentifier = value; OnPropertyChanged("AdditionalIdentifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _AdditionalIdentifier;
        
        /// <summary>
        /// Classification (category or class) of the item entry
        /// </summary>
        [FhirElement("classification", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Classification
        {
            get { if(_Classification==null) _Classification = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Classification; }
            set { _Classification = value; OnPropertyChanged("Classification"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Classification;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", Order=150)]
        [DataMember]
        public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.PublicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// The time period in which this catalog entry is expected to be active
        /// </summary>
        [FhirElement("validityPeriod", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Period ValidityPeriod
        {
            get { return _ValidityPeriod; }
            set { _ValidityPeriod = value; OnPropertyChanged("ValidityPeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _ValidityPeriod;
        
        /// <summary>
        /// The date until which this catalog entry is expected to be active
        /// </summary>
        [FhirElement("validTo", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ValidToElement
        {
            get { return _ValidToElement; }
            set { _ValidToElement = value; OnPropertyChanged("ValidToElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _ValidToElement;
        
        /// <summary>
        /// The date until which this catalog entry is expected to be active
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ValidTo
        {
            get { return ValidToElement != null ? ValidToElement.Value : null; }
            set
            {
                if (value == null)
                    ValidToElement = null;
                else
                    ValidToElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("ValidTo");
            }
        }
        
        /// <summary>
        /// When was this catalog last updated
        /// </summary>
        [FhirElement("lastUpdated", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime LastUpdatedElement
        {
            get { return _LastUpdatedElement; }
            set { _LastUpdatedElement = value; OnPropertyChanged("LastUpdatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _LastUpdatedElement;
        
        /// <summary>
        /// When was this catalog last updated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LastUpdated
        {
            get { return LastUpdatedElement != null ? LastUpdatedElement.Value : null; }
            set
            {
                if (value == null)
                    LastUpdatedElement = null;
                else
                    LastUpdatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("LastUpdated");
            }
        }
        
        /// <summary>
        /// Additional characteristics of the catalog entry
        /// </summary>
        [FhirElement("additionalCharacteristic", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> AdditionalCharacteristic
        {
            get { if(_AdditionalCharacteristic==null) _AdditionalCharacteristic = new List<Hl7.Fhir.Model.CodeableConcept>(); return _AdditionalCharacteristic; }
            set { _AdditionalCharacteristic = value; OnPropertyChanged("AdditionalCharacteristic"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _AdditionalCharacteristic;
        
        /// <summary>
        /// Additional classification of the catalog entry
        /// </summary>
        [FhirElement("additionalClassification", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> AdditionalClassification
        {
            get { if(_AdditionalClassification==null) _AdditionalClassification = new List<Hl7.Fhir.Model.CodeableConcept>(); return _AdditionalClassification; }
            set { _AdditionalClassification = value; OnPropertyChanged("AdditionalClassification"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _AdditionalClassification;
        
        /// <summary>
        /// An item that this catalog entry is related to
        /// </summary>
        [FhirElement("relatedEntry", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<RelatedEntryComponent> RelatedEntry
        {
            get { if(_RelatedEntry==null) _RelatedEntry = new List<RelatedEntryComponent>(); return _RelatedEntry; }
            set { _RelatedEntry = value; OnPropertyChanged("RelatedEntry"); }
        }
        
        private List<RelatedEntryComponent> _RelatedEntry;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as CatalogEntry;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(OrderableElement != null) dest.OrderableElement = (Hl7.Fhir.Model.FhirBoolean)OrderableElement.DeepCopy();
                if(ReferencedItem != null) dest.ReferencedItem = (Hl7.Fhir.Model.ResourceReference)ReferencedItem.DeepCopy();
                if(AdditionalIdentifier != null) dest.AdditionalIdentifier = new List<Hl7.Fhir.Model.Identifier>(AdditionalIdentifier.DeepCopy());
                if(Classification != null) dest.Classification = new List<Hl7.Fhir.Model.CodeableConcept>(Classification.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ValidityPeriod != null) dest.ValidityPeriod = (Hl7.Fhir.Model.Period)ValidityPeriod.DeepCopy();
                if(ValidToElement != null) dest.ValidToElement = (Hl7.Fhir.Model.FhirDateTime)ValidToElement.DeepCopy();
                if(LastUpdatedElement != null) dest.LastUpdatedElement = (Hl7.Fhir.Model.FhirDateTime)LastUpdatedElement.DeepCopy();
                if(AdditionalCharacteristic != null) dest.AdditionalCharacteristic = new List<Hl7.Fhir.Model.CodeableConcept>(AdditionalCharacteristic.DeepCopy());
                if(AdditionalClassification != null) dest.AdditionalClassification = new List<Hl7.Fhir.Model.CodeableConcept>(AdditionalClassification.DeepCopy());
                if(RelatedEntry != null) dest.RelatedEntry = new List<RelatedEntryComponent>(RelatedEntry.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new CatalogEntry());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as CatalogEntry;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(OrderableElement, otherT.OrderableElement)) return false;
            if( !DeepComparable.Matches(ReferencedItem, otherT.ReferencedItem)) return false;
            if( !DeepComparable.Matches(AdditionalIdentifier, otherT.AdditionalIdentifier)) return false;
            if( !DeepComparable.Matches(Classification, otherT.Classification)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ValidityPeriod, otherT.ValidityPeriod)) return false;
            if( !DeepComparable.Matches(ValidToElement, otherT.ValidToElement)) return false;
            if( !DeepComparable.Matches(LastUpdatedElement, otherT.LastUpdatedElement)) return false;
            if( !DeepComparable.Matches(AdditionalCharacteristic, otherT.AdditionalCharacteristic)) return false;
            if( !DeepComparable.Matches(AdditionalClassification, otherT.AdditionalClassification)) return false;
            if( !DeepComparable.Matches(RelatedEntry, otherT.RelatedEntry)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as CatalogEntry;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(OrderableElement, otherT.OrderableElement)) return false;
            if( !DeepComparable.IsExactly(ReferencedItem, otherT.ReferencedItem)) return false;
            if( !DeepComparable.IsExactly(AdditionalIdentifier, otherT.AdditionalIdentifier)) return false;
            if( !DeepComparable.IsExactly(Classification, otherT.Classification)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ValidityPeriod, otherT.ValidityPeriod)) return false;
            if( !DeepComparable.IsExactly(ValidToElement, otherT.ValidToElement)) return false;
            if( !DeepComparable.IsExactly(LastUpdatedElement, otherT.LastUpdatedElement)) return false;
            if( !DeepComparable.IsExactly(AdditionalCharacteristic, otherT.AdditionalCharacteristic)) return false;
            if( !DeepComparable.IsExactly(AdditionalClassification, otherT.AdditionalClassification)) return false;
            if( !DeepComparable.IsExactly(RelatedEntry, otherT.RelatedEntry)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("CatalogEntry");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
            sink.Element("orderable", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); OrderableElement?.Serialize(sink);
            sink.Element("referencedItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ReferencedItem?.Serialize(sink);
            sink.BeginList("additionalIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in AdditionalIdentifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("classification", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Classification)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); StatusElement?.Serialize(sink);
            sink.Element("validityPeriod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValidityPeriod?.Serialize(sink);
            sink.Element("validTo", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValidToElement?.Serialize(sink);
            sink.Element("lastUpdated", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LastUpdatedElement?.Serialize(sink);
            sink.BeginList("additionalCharacteristic", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in AdditionalCharacteristic)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("additionalClassification", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in AdditionalClassification)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("relatedEntry", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in RelatedEntry)
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
                case "orderable":
                    OrderableElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "referencedItem":
                    ReferencedItem = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "additionalIdentifier":
                    AdditionalIdentifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "classification":
                    Classification = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.PublicationStatus>>();
                    return true;
                case "validityPeriod":
                    ValidityPeriod = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "validTo":
                    ValidToElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "lastUpdated":
                    LastUpdatedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "additionalCharacteristic":
                    AdditionalCharacteristic = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "additionalClassification":
                    AdditionalClassification = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "relatedEntry":
                    RelatedEntry = source.GetList<RelatedEntryComponent>();
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
                case "orderable":
                    OrderableElement = source.PopulateValue(OrderableElement);
                    return true;
                case "_orderable":
                    OrderableElement = source.Populate(OrderableElement);
                    return true;
                case "referencedItem":
                    ReferencedItem = source.Populate(ReferencedItem);
                    return true;
                case "additionalIdentifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "classification":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "validityPeriod":
                    ValidityPeriod = source.Populate(ValidityPeriod);
                    return true;
                case "validTo":
                    ValidToElement = source.PopulateValue(ValidToElement);
                    return true;
                case "_validTo":
                    ValidToElement = source.Populate(ValidToElement);
                    return true;
                case "lastUpdated":
                    LastUpdatedElement = source.PopulateValue(LastUpdatedElement);
                    return true;
                case "_lastUpdated":
                    LastUpdatedElement = source.Populate(LastUpdatedElement);
                    return true;
                case "additionalCharacteristic":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "additionalClassification":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "relatedEntry":
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
                case "additionalIdentifier":
                    source.PopulateListItem(AdditionalIdentifier, index);
                    return true;
                case "classification":
                    source.PopulateListItem(Classification, index);
                    return true;
                case "additionalCharacteristic":
                    source.PopulateListItem(AdditionalCharacteristic, index);
                    return true;
                case "additionalClassification":
                    source.PopulateListItem(AdditionalClassification, index);
                    return true;
                case "relatedEntry":
                    source.PopulateListItem(RelatedEntry, index);
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
                if (Type != null) yield return Type;
                if (OrderableElement != null) yield return OrderableElement;
                if (ReferencedItem != null) yield return ReferencedItem;
                foreach (var elem in AdditionalIdentifier) { if (elem != null) yield return elem; }
                foreach (var elem in Classification) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                if (ValidityPeriod != null) yield return ValidityPeriod;
                if (ValidToElement != null) yield return ValidToElement;
                if (LastUpdatedElement != null) yield return LastUpdatedElement;
                foreach (var elem in AdditionalCharacteristic) { if (elem != null) yield return elem; }
                foreach (var elem in AdditionalClassification) { if (elem != null) yield return elem; }
                foreach (var elem in RelatedEntry) { if (elem != null) yield return elem; }
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
                if (OrderableElement != null) yield return new ElementValue("orderable", OrderableElement);
                if (ReferencedItem != null) yield return new ElementValue("referencedItem", ReferencedItem);
                foreach (var elem in AdditionalIdentifier) { if (elem != null) yield return new ElementValue("additionalIdentifier", elem); }
                foreach (var elem in Classification) { if (elem != null) yield return new ElementValue("classification", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ValidityPeriod != null) yield return new ElementValue("validityPeriod", ValidityPeriod);
                if (ValidToElement != null) yield return new ElementValue("validTo", ValidToElement);
                if (LastUpdatedElement != null) yield return new ElementValue("lastUpdated", LastUpdatedElement);
                foreach (var elem in AdditionalCharacteristic) { if (elem != null) yield return new ElementValue("additionalCharacteristic", elem); }
                foreach (var elem in AdditionalClassification) { if (elem != null) yield return new ElementValue("additionalClassification", elem); }
                foreach (var elem in RelatedEntry) { if (elem != null) yield return new ElementValue("relatedEntry", elem); }
            }
        }
    
    }

}
