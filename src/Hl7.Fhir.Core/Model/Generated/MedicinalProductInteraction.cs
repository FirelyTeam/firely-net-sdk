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
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// MedicinalProductInteraction
    /// </summary>
    [FhirType("MedicinalProductInteraction", IsResource=true)]
    [DataContract]
    public partial class MedicinalProductInteraction : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicinalProductInteraction; } }
        [NotMapped]
        public override string TypeName { get { return "MedicinalProductInteraction"; } }
        
        [FhirType("InteractantComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class InteractantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "InteractantComponent"; } }
            
            /// <summary>
            /// The specific medication, food or laboratory test that interacts
            /// </summary>
            [FhirElement("item", InSummary=true, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private Hl7.Fhir.Model.Element _Item;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InteractantComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Item != null) dest.Item = (Hl7.Fhir.Model.Element)Item.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new InteractantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InteractantComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InteractantComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Item != null) yield return Item;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Item != null) yield return new ElementValue("item", Item);
                }
            }

            
        }
        
        
        /// <summary>
        /// The medication for which this is a described interaction
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=90)]
        [CLSCompliant(false)]
		[References("MedicinalProduct","Medication","Substance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Subject
        {
            get { if(_Subject==null) _Subject = new List<Hl7.Fhir.Model.ResourceReference>(); return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Subject;
        
        /// <summary>
        /// The interaction described
        /// </summary>
        [FhirElement("description", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// The interaction described
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if (value == null)
                  DescriptionElement = null; 
                else
                  DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// The specific medication, food or laboratory test that interacts
        /// </summary>
        [FhirElement("interactant", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicinalProductInteraction.InteractantComponent> Interactant
        {
            get { if(_Interactant==null) _Interactant = new List<Hl7.Fhir.Model.MedicinalProductInteraction.InteractantComponent>(); return _Interactant; }
            set { _Interactant = value; OnPropertyChanged("Interactant"); }
        }
        
        private List<Hl7.Fhir.Model.MedicinalProductInteraction.InteractantComponent> _Interactant;
        
        /// <summary>
        /// The type of the interaction e.g. drug-drug interaction, drug-food interaction, drug-lab test interaction
        /// </summary>
        [FhirElement("type", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// The effect of the interaction, for example "reduced gastric absorption of primary medication"
        /// </summary>
        [FhirElement("effect", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Effect
        {
            get { return _Effect; }
            set { _Effect = value; OnPropertyChanged("Effect"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Effect;
        
        /// <summary>
        /// The incidence of the interaction, e.g. theoretical, observed
        /// </summary>
        [FhirElement("incidence", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Incidence
        {
            get { return _Incidence; }
            set { _Incidence = value; OnPropertyChanged("Incidence"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Incidence;
        
        /// <summary>
        /// Actions for managing the interaction
        /// </summary>
        [FhirElement("management", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Management
        {
            get { return _Management; }
            set { _Management = value; OnPropertyChanged("Management"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Management;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicinalProductInteraction;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Subject != null) dest.Subject = new List<Hl7.Fhir.Model.ResourceReference>(Subject.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Interactant != null) dest.Interactant = new List<Hl7.Fhir.Model.MedicinalProductInteraction.InteractantComponent>(Interactant.DeepCopy());
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Effect != null) dest.Effect = (Hl7.Fhir.Model.CodeableConcept)Effect.DeepCopy();
                if(Incidence != null) dest.Incidence = (Hl7.Fhir.Model.CodeableConcept)Incidence.DeepCopy();
                if(Management != null) dest.Management = (Hl7.Fhir.Model.CodeableConcept)Management.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new MedicinalProductInteraction());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicinalProductInteraction;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Interactant, otherT.Interactant)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Effect, otherT.Effect)) return false;
            if( !DeepComparable.Matches(Incidence, otherT.Incidence)) return false;
            if( !DeepComparable.Matches(Management, otherT.Management)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicinalProductInteraction;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Interactant, otherT.Interactant)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Effect, otherT.Effect)) return false;
            if( !DeepComparable.IsExactly(Incidence, otherT.Incidence)) return false;
            if( !DeepComparable.IsExactly(Management, otherT.Management)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Subject) { if (elem != null) yield return elem; }
				if (DescriptionElement != null) yield return DescriptionElement;
				foreach (var elem in Interactant) { if (elem != null) yield return elem; }
				if (Type != null) yield return Type;
				if (Effect != null) yield return Effect;
				if (Incidence != null) yield return Incidence;
				if (Management != null) yield return Management;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Subject) { if (elem != null) yield return new ElementValue("subject", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in Interactant) { if (elem != null) yield return new ElementValue("interactant", elem); }
                if (Type != null) yield return new ElementValue("type", Type);
                if (Effect != null) yield return new ElementValue("effect", Effect);
                if (Incidence != null) yield return new ElementValue("incidence", Incidence);
                if (Management != null) yield return new ElementValue("management", Management);
            }
        }

    }
    
}
