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
// Generated for FHIR v3.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// An entry in a catalog
    /// </summary>
    [FhirType("EntryDefinition", IsResource=true)]
    [DataContract]
    public partial class EntryDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.EntryDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "EntryDefinition"; } }
        
        [FhirType("RelatedEntryComponent")]
        [DataContract]
        public partial class RelatedEntryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedEntryComponent"; } }
            
            /// <summary>
            /// The type of relation to the related item
            /// </summary>
            [FhirElement("relationtype", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Relationtype
            {
                get { return _Relationtype; }
                set { _Relationtype = value; OnPropertyChanged("Relationtype"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Relationtype;
            
            /// <summary>
            /// The reference to the related item
            /// </summary>
            [FhirElement("item", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("EntryDefinition")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Item;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedEntryComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Relationtype != null) dest.Relationtype = (Hl7.Fhir.Model.CodeableConcept)Relationtype.DeepCopy();
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
                if( !DeepComparable.Matches(Relationtype, otherT.Relationtype)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedEntryComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Relationtype, otherT.Relationtype)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Relationtype != null) yield return Relationtype;
                    if (Item != null) yield return Item;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Relationtype != null) yield return new ElementValue("relationtype", false, Relationtype);
                    if (Item != null) yield return new ElementValue("item", false, Item);
                }
            }

            
        }
        
        
        /// <summary>
        /// The type of item - medication, device, service, protocol or other
        /// </summary>
        [FhirElement("type", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Whether the entry represents an orderable item, or other
        /// </summary>
        [FhirElement("purpose", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; OnPropertyChanged("Purpose"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Purpose;
        
        /// <summary>
        /// The item itself
        /// </summary>
        [FhirElement("referencedItem", InSummary=true, Order=110)]
        [CLSCompliant(false)]
		[References("Medication","Device","Organization","Practitioner","HealthcareService","ActivityDefinition","PlanDefinition","SpecimenDefinition","ObservationDefinition","Binary")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ReferencedItem
        {
            get { return _ReferencedItem; }
            set { _ReferencedItem = value; OnPropertyChanged("ReferencedItem"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ReferencedItem;
        
        /// <summary>
        /// Unique identifier of the catalog item
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Any additional identifier(s) for the catalog item, in the same granularity or concept
        /// </summary>
        [FhirElement("additionalIdentifier", InSummary=true, Order=130)]
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
        [FhirElement("classification", InSummary=true, Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Classification
        {
            get { if(_Classification==null) _Classification = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Classification; }
            set { _Classification = value; OnPropertyChanged("Classification"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Classification;
        
        /// <summary>
        /// The status of the item, e.g. active, approved, deleted…
        /// </summary>
        [FhirElement("status", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Status
        {
            get { return _Status; }
            set { _Status = value; OnPropertyChanged("Status"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Status;
        
        /// <summary>
        /// The time period in which this catalog entry is expected to be active
        /// </summary>
        [FhirElement("validityPeriod", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Period ValidityPeriod
        {
            get { return _ValidityPeriod; }
            set { _ValidityPeriod = value; OnPropertyChanged("ValidityPeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _ValidityPeriod;
        
        /// <summary>
        /// When was this catalog last updated
        /// </summary>
        [FhirElement("lastUpdated", InSummary=true, Order=170)]
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
        [FhirElement("additionalCharacteristic", InSummary=true, Order=180)]
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
        [FhirElement("additionalClassification", InSummary=true, Order=190)]
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
        [FhirElement("relatedEntry", InSummary=true, Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.EntryDefinition.RelatedEntryComponent> RelatedEntry
        {
            get { if(_RelatedEntry==null) _RelatedEntry = new List<Hl7.Fhir.Model.EntryDefinition.RelatedEntryComponent>(); return _RelatedEntry; }
            set { _RelatedEntry = value; OnPropertyChanged("RelatedEntry"); }
        }
        
        private List<Hl7.Fhir.Model.EntryDefinition.RelatedEntryComponent> _RelatedEntry;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as EntryDefinition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Purpose != null) dest.Purpose = (Hl7.Fhir.Model.CodeableConcept)Purpose.DeepCopy();
                if(ReferencedItem != null) dest.ReferencedItem = (Hl7.Fhir.Model.ResourceReference)ReferencedItem.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(AdditionalIdentifier != null) dest.AdditionalIdentifier = new List<Hl7.Fhir.Model.Identifier>(AdditionalIdentifier.DeepCopy());
                if(Classification != null) dest.Classification = new List<Hl7.Fhir.Model.CodeableConcept>(Classification.DeepCopy());
                if(Status != null) dest.Status = (Hl7.Fhir.Model.CodeableConcept)Status.DeepCopy();
                if(ValidityPeriod != null) dest.ValidityPeriod = (Hl7.Fhir.Model.Period)ValidityPeriod.DeepCopy();
                if(LastUpdatedElement != null) dest.LastUpdatedElement = (Hl7.Fhir.Model.FhirDateTime)LastUpdatedElement.DeepCopy();
                if(AdditionalCharacteristic != null) dest.AdditionalCharacteristic = new List<Hl7.Fhir.Model.CodeableConcept>(AdditionalCharacteristic.DeepCopy());
                if(AdditionalClassification != null) dest.AdditionalClassification = new List<Hl7.Fhir.Model.CodeableConcept>(AdditionalClassification.DeepCopy());
                if(RelatedEntry != null) dest.RelatedEntry = new List<Hl7.Fhir.Model.EntryDefinition.RelatedEntryComponent>(RelatedEntry.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new EntryDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as EntryDefinition;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.Matches(ReferencedItem, otherT.ReferencedItem)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(AdditionalIdentifier, otherT.AdditionalIdentifier)) return false;
            if( !DeepComparable.Matches(Classification, otherT.Classification)) return false;
            if( !DeepComparable.Matches(Status, otherT.Status)) return false;
            if( !DeepComparable.Matches(ValidityPeriod, otherT.ValidityPeriod)) return false;
            if( !DeepComparable.Matches(LastUpdatedElement, otherT.LastUpdatedElement)) return false;
            if( !DeepComparable.Matches(AdditionalCharacteristic, otherT.AdditionalCharacteristic)) return false;
            if( !DeepComparable.Matches(AdditionalClassification, otherT.AdditionalClassification)) return false;
            if( !DeepComparable.Matches(RelatedEntry, otherT.RelatedEntry)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as EntryDefinition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
            if( !DeepComparable.IsExactly(ReferencedItem, otherT.ReferencedItem)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(AdditionalIdentifier, otherT.AdditionalIdentifier)) return false;
            if( !DeepComparable.IsExactly(Classification, otherT.Classification)) return false;
            if( !DeepComparable.IsExactly(Status, otherT.Status)) return false;
            if( !DeepComparable.IsExactly(ValidityPeriod, otherT.ValidityPeriod)) return false;
            if( !DeepComparable.IsExactly(LastUpdatedElement, otherT.LastUpdatedElement)) return false;
            if( !DeepComparable.IsExactly(AdditionalCharacteristic, otherT.AdditionalCharacteristic)) return false;
            if( !DeepComparable.IsExactly(AdditionalClassification, otherT.AdditionalClassification)) return false;
            if( !DeepComparable.IsExactly(RelatedEntry, otherT.RelatedEntry)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Type != null) yield return Type;
				if (Purpose != null) yield return Purpose;
				if (ReferencedItem != null) yield return ReferencedItem;
				if (Identifier != null) yield return Identifier;
				foreach (var elem in AdditionalIdentifier) { if (elem != null) yield return elem; }
				foreach (var elem in Classification) { if (elem != null) yield return elem; }
				if (Status != null) yield return Status;
				if (ValidityPeriod != null) yield return ValidityPeriod;
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
                if (Type != null) yield return new ElementValue("type", false, Type);
                if (Purpose != null) yield return new ElementValue("purpose", false, Purpose);
                if (ReferencedItem != null) yield return new ElementValue("referencedItem", false, ReferencedItem);
                if (Identifier != null) yield return new ElementValue("identifier", false, Identifier);
                foreach (var elem in AdditionalIdentifier) { if (elem != null) yield return new ElementValue("additionalIdentifier", true, elem); }
                foreach (var elem in Classification) { if (elem != null) yield return new ElementValue("classification", true, elem); }
                if (Status != null) yield return new ElementValue("status", false, Status);
                if (ValidityPeriod != null) yield return new ElementValue("validityPeriod", false, ValidityPeriod);
                if (LastUpdatedElement != null) yield return new ElementValue("lastUpdated", false, LastUpdatedElement);
                foreach (var elem in AdditionalCharacteristic) { if (elem != null) yield return new ElementValue("additionalCharacteristic", true, elem); }
                foreach (var elem in AdditionalClassification) { if (elem != null) yield return new ElementValue("additionalClassification", true, elem); }
                foreach (var elem in RelatedEntry) { if (elem != null) yield return new ElementValue("relatedEntry", true, elem); }
            }
        }

    }
    
}
