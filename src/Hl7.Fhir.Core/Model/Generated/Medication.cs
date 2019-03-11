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
    /// Definition of a Medication
    /// </summary>
    [FhirType("Medication", IsResource=true)]
    [DataContract]
    public partial class Medication : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Medication; } }
        [NotMapped]
        public override string TypeName { get { return "Medication"; } }
        
        /// <summary>
        /// Medication Status Codes
        /// (url: http://hl7.org/fhir/ValueSet/medication-status)
        /// </summary>
        [FhirEnumeration("MedicationStatusCodes")]
        public enum MedicationStatusCodes
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/medication-status)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/CodeSystem/medication-status"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/medication-status)
            /// </summary>
            [EnumLiteral("inactive", "http://hl7.org/fhir/CodeSystem/medication-status"), Description("Inactive")]
            Inactive,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/medication-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/CodeSystem/medication-status"), Description("Entered in Error")]
            EnteredInError,
        }

        [FhirType("IngredientComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class IngredientComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "IngredientComponent"; } }
            
            /// <summary>
            /// The actual ingredient or content
            /// </summary>
            [FhirElement("item", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private Hl7.Fhir.Model.Element _Item;
            
            /// <summary>
            /// Active ingredient indicator
            /// </summary>
            [FhirElement("isActive", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean IsActiveElement
            {
                get { return _IsActiveElement; }
                set { _IsActiveElement = value; OnPropertyChanged("IsActiveElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _IsActiveElement;
            
            /// <summary>
            /// Active ingredient indicator
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? IsActive
            {
                get { return IsActiveElement != null ? IsActiveElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        IsActiveElement = null; 
                    else
                        IsActiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("IsActive");
                }
            }
            
            /// <summary>
            /// Quantity of ingredient present
            /// </summary>
            [FhirElement("strength", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Strength
            {
                get { return _Strength; }
                set { _Strength = value; OnPropertyChanged("Strength"); }
            }
            
            private Hl7.Fhir.Model.Ratio _Strength;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as IngredientComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Item != null) dest.Item = (Hl7.Fhir.Model.Element)Item.DeepCopy();
                    if(IsActiveElement != null) dest.IsActiveElement = (Hl7.Fhir.Model.FhirBoolean)IsActiveElement.DeepCopy();
                    if(Strength != null) dest.Strength = (Hl7.Fhir.Model.Ratio)Strength.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new IngredientComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as IngredientComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
                if( !DeepComparable.Matches(IsActiveElement, otherT.IsActiveElement)) return false;
                if( !DeepComparable.Matches(Strength, otherT.Strength)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as IngredientComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
                if( !DeepComparable.IsExactly(IsActiveElement, otherT.IsActiveElement)) return false;
                if( !DeepComparable.IsExactly(Strength, otherT.Strength)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Item != null) yield return Item;
                    if (IsActiveElement != null) yield return IsActiveElement;
                    if (Strength != null) yield return Strength;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Item != null) yield return new ElementValue("item", Item);
                    if (IsActiveElement != null) yield return new ElementValue("isActive", IsActiveElement);
                    if (Strength != null) yield return new ElementValue("strength", Strength);
                }
            }

            
        }
        
        
        [FhirType("BatchComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class BatchComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "BatchComponent"; } }
            
            /// <summary>
            /// Identifier assigned to batch
            /// </summary>
            [FhirElement("lotNumber", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LotNumberElement
            {
                get { return _LotNumberElement; }
                set { _LotNumberElement = value; OnPropertyChanged("LotNumberElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LotNumberElement;
            
            /// <summary>
            /// Identifier assigned to batch
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string LotNumber
            {
                get { return LotNumberElement != null ? LotNumberElement.Value : null; }
                set
                {
                    if (value == null)
                        LotNumberElement = null; 
                    else
                        LotNumberElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("LotNumber");
                }
            }
            
            /// <summary>
            /// When batch will expire
            /// </summary>
            [FhirElement("expirationDate", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ExpirationDateElement
            {
                get { return _ExpirationDateElement; }
                set { _ExpirationDateElement = value; OnPropertyChanged("ExpirationDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _ExpirationDateElement;
            
            /// <summary>
            /// When batch will expire
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ExpirationDate
            {
                get { return ExpirationDateElement != null ? ExpirationDateElement.Value : null; }
                set
                {
                    if (value == null)
                        ExpirationDateElement = null; 
                    else
                        ExpirationDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("ExpirationDate");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as BatchComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LotNumberElement != null) dest.LotNumberElement = (Hl7.Fhir.Model.FhirString)LotNumberElement.DeepCopy();
                    if(ExpirationDateElement != null) dest.ExpirationDateElement = (Hl7.Fhir.Model.FhirDateTime)ExpirationDateElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new BatchComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as BatchComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LotNumberElement, otherT.LotNumberElement)) return false;
                if( !DeepComparable.Matches(ExpirationDateElement, otherT.ExpirationDateElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as BatchComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LotNumberElement, otherT.LotNumberElement)) return false;
                if( !DeepComparable.IsExactly(ExpirationDateElement, otherT.ExpirationDateElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LotNumberElement != null) yield return LotNumberElement;
                    if (ExpirationDateElement != null) yield return ExpirationDateElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (LotNumberElement != null) yield return new ElementValue("lotNumber", LotNumberElement);
                    if (ExpirationDateElement != null) yield return new ElementValue("expirationDate", ExpirationDateElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business identifier for this medication
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Codes that identify this medication
        /// </summary>
        [FhirElement("code", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// active | inactive | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Medication.MedicationStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Medication.MedicationStatusCodes> _StatusElement;
        
        /// <summary>
        /// active | inactive | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Medication.MedicationStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Medication.MedicationStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Manufacturer of the item
        /// </summary>
        [FhirElement("manufacturer", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Manufacturer
        {
            get { return _Manufacturer; }
            set { _Manufacturer = value; OnPropertyChanged("Manufacturer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Manufacturer;
        
        /// <summary>
        /// powder | tablets | capsule +
        /// </summary>
        [FhirElement("form", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Form
        {
            get { return _Form; }
            set { _Form = value; OnPropertyChanged("Form"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Form;
        
        /// <summary>
        /// Amount of drug in package
        /// </summary>
        [FhirElement("amount", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Ratio Amount
        {
            get { return _Amount; }
            set { _Amount = value; OnPropertyChanged("Amount"); }
        }
        
        private Hl7.Fhir.Model.Ratio _Amount;
        
        /// <summary>
        /// Active or inactive ingredient
        /// </summary>
        [FhirElement("ingredient", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Medication.IngredientComponent> Ingredient
        {
            get { if(_Ingredient==null) _Ingredient = new List<Hl7.Fhir.Model.Medication.IngredientComponent>(); return _Ingredient; }
            set { _Ingredient = value; OnPropertyChanged("Ingredient"); }
        }
        
        private List<Hl7.Fhir.Model.Medication.IngredientComponent> _Ingredient;
        
        /// <summary>
        /// Details about packaged medications
        /// </summary>
        [FhirElement("batch", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Medication.BatchComponent Batch
        {
            get { return _Batch; }
            set { _Batch = value; OnPropertyChanged("Batch"); }
        }
        
        private Hl7.Fhir.Model.Medication.BatchComponent _Batch;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Medication;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Medication.MedicationStatusCodes>)StatusElement.DeepCopy();
                if(Manufacturer != null) dest.Manufacturer = (Hl7.Fhir.Model.ResourceReference)Manufacturer.DeepCopy();
                if(Form != null) dest.Form = (Hl7.Fhir.Model.CodeableConcept)Form.DeepCopy();
                if(Amount != null) dest.Amount = (Hl7.Fhir.Model.Ratio)Amount.DeepCopy();
                if(Ingredient != null) dest.Ingredient = new List<Hl7.Fhir.Model.Medication.IngredientComponent>(Ingredient.DeepCopy());
                if(Batch != null) dest.Batch = (Hl7.Fhir.Model.Medication.BatchComponent)Batch.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Medication());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Medication;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.Matches(Form, otherT.Form)) return false;
            if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
            if( !DeepComparable.Matches(Ingredient, otherT.Ingredient)) return false;
            if( !DeepComparable.Matches(Batch, otherT.Batch)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Medication;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.IsExactly(Form, otherT.Form)) return false;
            if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
            if( !DeepComparable.IsExactly(Ingredient, otherT.Ingredient)) return false;
            if( !DeepComparable.IsExactly(Batch, otherT.Batch)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (Code != null) yield return Code;
				if (StatusElement != null) yield return StatusElement;
				if (Manufacturer != null) yield return Manufacturer;
				if (Form != null) yield return Form;
				if (Amount != null) yield return Amount;
				foreach (var elem in Ingredient) { if (elem != null) yield return elem; }
				if (Batch != null) yield return Batch;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Code != null) yield return new ElementValue("code", Code);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Manufacturer != null) yield return new ElementValue("manufacturer", Manufacturer);
                if (Form != null) yield return new ElementValue("form", Form);
                if (Amount != null) yield return new ElementValue("amount", Amount);
                foreach (var elem in Ingredient) { if (elem != null) yield return new ElementValue("ingredient", elem); }
                if (Batch != null) yield return new ElementValue("batch", Batch);
            }
        }

    }
    
}
