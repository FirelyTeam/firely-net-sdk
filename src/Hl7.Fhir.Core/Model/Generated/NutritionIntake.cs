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
// Generated for FHIR v4.2.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Record of food or fluid being taken by a patient
    /// </summary>
    [FhirType("NutritionIntake", IsResource=true)]
    [DataContract]
    public partial class NutritionIntake : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.NutritionIntake; } }
        [NotMapped]
        public override string TypeName { get { return "NutritionIntake"; } }
        
        /// <summary>
        /// NutritionIntake Status Codes
        /// (url: http://hl7.org/fhir/ValueSet/nutrition-intake-status)
        /// </summary>
        [FhirEnumeration("NutritionIntakeStatusCodes")]
        public enum NutritionIntakeStatusCodes
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/nutrition-intake-status)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/CodeSystem/nutrition-intake-status"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/nutrition-intake-status)
            /// </summary>
            [EnumLiteral("completed", "http://hl7.org/fhir/CodeSystem/nutrition-intake-status"), Description("Completed")]
            Completed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/nutrition-intake-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/CodeSystem/nutrition-intake-status"), Description("Entered in Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/nutrition-intake-status)
            /// </summary>
            [EnumLiteral("intended", "http://hl7.org/fhir/CodeSystem/nutrition-intake-status"), Description("Intended")]
            Intended,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/nutrition-intake-status)
            /// </summary>
            [EnumLiteral("stopped", "http://hl7.org/fhir/CodeSystem/nutrition-intake-status"), Description("Stopped")]
            Stopped,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/nutrition-intake-status)
            /// </summary>
            [EnumLiteral("on-hold", "http://hl7.org/fhir/CodeSystem/nutrition-intake-status"), Description("On Hold")]
            OnHold,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/nutrition-intake-status)
            /// </summary>
            [EnumLiteral("unknown", "http://hl7.org/fhir/CodeSystem/nutrition-intake-status"), Description("Unknown")]
            Unknown,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/nutrition-intake-status)
            /// </summary>
            [EnumLiteral("not-taken", "http://hl7.org/fhir/CodeSystem/nutrition-intake-status"), Description("Not Taken")]
            NotTaken,
        }

        [FhirType("ConsumedItemComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ConsumedItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConsumedItemComponent"; } }
            
            /// <summary>
            /// The type of food or fluid product
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Code that identifies the food or fluid product that was consumed
            /// </summary>
            [FhirElement("nutritionProduct", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept NutritionProduct
            {
                get { return _NutritionProduct; }
                set { _NutritionProduct = value; OnPropertyChanged("NutritionProduct"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _NutritionProduct;
            
            /// <summary>
            /// Scheduled frequency of consumption
            /// </summary>
            [FhirElement("schedule", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Timing Schedule
            {
                get { return _Schedule; }
                set { _Schedule = value; OnPropertyChanged("Schedule"); }
            }
            
            private Hl7.Fhir.Model.Timing _Schedule;
            
            /// <summary>
            /// Quantity of the specified food
            /// </summary>
            [FhirElement("amount", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Amount;
            
            /// <summary>
            /// Rate at which enteral feeding was administered
            /// </summary>
            [FhirElement("rate", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Rate
            {
                get { return _Rate; }
                set { _Rate = value; OnPropertyChanged("Rate"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Rate;
            
            /// <summary>
            /// Flag to indicate if the food or fluid item was refused or otherwise not consumed
            /// </summary>
            [FhirElement("notConsumed", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean NotConsumedElement
            {
                get { return _NotConsumedElement; }
                set { _NotConsumedElement = value; OnPropertyChanged("NotConsumedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _NotConsumedElement;
            
            /// <summary>
            /// Flag to indicate if the food or fluid item was refused or otherwise not consumed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? NotConsumed
            {
                get { return NotConsumedElement != null ? NotConsumedElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        NotConsumedElement = null; 
                    else
                        NotConsumedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("NotConsumed");
                }
            }
            
            /// <summary>
            /// Reason food or fluid was not consumed
            /// </summary>
            [FhirElement("notConsumedReason", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept NotConsumedReason
            {
                get { return _NotConsumedReason; }
                set { _NotConsumedReason = value; OnPropertyChanged("NotConsumedReason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _NotConsumedReason;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConsumedItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(NutritionProduct != null) dest.NutritionProduct = (Hl7.Fhir.Model.CodeableConcept)NutritionProduct.DeepCopy();
                    if(Schedule != null) dest.Schedule = (Hl7.Fhir.Model.Timing)Schedule.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.SimpleQuantity)Amount.DeepCopy();
                    if(Rate != null) dest.Rate = (Hl7.Fhir.Model.SimpleQuantity)Rate.DeepCopy();
                    if(NotConsumedElement != null) dest.NotConsumedElement = (Hl7.Fhir.Model.FhirBoolean)NotConsumedElement.DeepCopy();
                    if(NotConsumedReason != null) dest.NotConsumedReason = (Hl7.Fhir.Model.CodeableConcept)NotConsumedReason.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConsumedItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConsumedItemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(NutritionProduct, otherT.NutritionProduct)) return false;
                if( !DeepComparable.Matches(Schedule, otherT.Schedule)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(Rate, otherT.Rate)) return false;
                if( !DeepComparable.Matches(NotConsumedElement, otherT.NotConsumedElement)) return false;
                if( !DeepComparable.Matches(NotConsumedReason, otherT.NotConsumedReason)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConsumedItemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(NutritionProduct, otherT.NutritionProduct)) return false;
                if( !DeepComparable.IsExactly(Schedule, otherT.Schedule)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(Rate, otherT.Rate)) return false;
                if( !DeepComparable.IsExactly(NotConsumedElement, otherT.NotConsumedElement)) return false;
                if( !DeepComparable.IsExactly(NotConsumedReason, otherT.NotConsumedReason)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (NutritionProduct != null) yield return NutritionProduct;
                    if (Schedule != null) yield return Schedule;
                    if (Amount != null) yield return Amount;
                    if (Rate != null) yield return Rate;
                    if (NotConsumedElement != null) yield return NotConsumedElement;
                    if (NotConsumedReason != null) yield return NotConsumedReason;
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (NutritionProduct != null) yield return new ElementValue("nutritionProduct", NutritionProduct);
                    if (Schedule != null) yield return new ElementValue("schedule", Schedule);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (Rate != null) yield return new ElementValue("rate", Rate);
                    if (NotConsumedElement != null) yield return new ElementValue("notConsumed", NotConsumedElement);
                    if (NotConsumedReason != null) yield return new ElementValue("notConsumedReason", NotConsumedReason);
                }
            }

            
        }
        
        
        [FhirType("IngredientLabelComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class IngredientLabelComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "IngredientLabelComponent"; } }
            
            /// <summary>
            /// Total nutrient consumed
            /// </summary>
            [FhirElement("nutrient", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Nutrient
            {
                get { return _Nutrient; }
                set { _Nutrient = value; OnPropertyChanged("Nutrient"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Nutrient;
            
            /// <summary>
            /// Total amount of nutrient consumed
            /// </summary>
            [FhirElement("amount", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Amount;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as IngredientLabelComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Nutrient != null) dest.Nutrient = (Hl7.Fhir.Model.CodeableConcept)Nutrient.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.SimpleQuantity)Amount.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new IngredientLabelComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as IngredientLabelComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Nutrient, otherT.Nutrient)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as IngredientLabelComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Nutrient, otherT.Nutrient)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Nutrient != null) yield return Nutrient;
                    if (Amount != null) yield return Amount;
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Nutrient != null) yield return new ElementValue("nutrient", Nutrient);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                }
            }

            
        }
        
        
        /// <summary>
        /// External identifier
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
        /// Fulfils plan, proposal or order
        /// </summary>
        [FhirElement("basedOn", InSummary=true, Order=100)]
        [CLSCompliant(false)]
		[References("NutritionOrder","CarePlan","ServiceRequest")]
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
        [FhirElement("partOf", InSummary=true, Order=110)]
        [CLSCompliant(false)]
		[References("NutritionIntake","Procedure","Observation")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PartOf
        {
            get { if(_PartOf==null) _PartOf = new List<Hl7.Fhir.Model.ResourceReference>(); return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PartOf;
        
        /// <summary>
        /// active | completed | entered-in-error | intended | stopped | on-hold | unknown | not-taken
        /// </summary>
        [FhirElement("status", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.NutritionIntake.NutritionIntakeStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.NutritionIntake.NutritionIntakeStatusCodes> _StatusElement;
        
        /// <summary>
        /// active | completed | entered-in-error | intended | stopped | on-hold | unknown | not-taken
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.NutritionIntake.NutritionIntakeStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.NutritionIntake.NutritionIntakeStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reason for current status
        /// </summary>
        [FhirElement("statusReason", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> StatusReason
        {
            get { if(_StatusReason==null) _StatusReason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _StatusReason; }
            set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _StatusReason;
        
        /// <summary>
        /// Type of nutrition intake setting/reporting
        /// </summary>
        [FhirElement("category", InSummary=true, Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// What food or fluid product or item was consumed
        /// </summary>
        [FhirElement("consumedItem", Order=150)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.NutritionIntake.ConsumedItemComponent> ConsumedItem
        {
            get { if(_ConsumedItem==null) _ConsumedItem = new List<Hl7.Fhir.Model.NutritionIntake.ConsumedItemComponent>(); return _ConsumedItem; }
            set { _ConsumedItem = value; OnPropertyChanged("ConsumedItem"); }
        }
        
        private List<Hl7.Fhir.Model.NutritionIntake.ConsumedItemComponent> _ConsumedItem;
        
        /// <summary>
        /// Total nutrient for the whole meal, product, serving
        /// </summary>
        [FhirElement("ingredientLabel", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.NutritionIntake.IngredientLabelComponent> IngredientLabel
        {
            get { if(_IngredientLabel==null) _IngredientLabel = new List<Hl7.Fhir.Model.NutritionIntake.IngredientLabelComponent>(); return _IngredientLabel; }
            set { _IngredientLabel = value; OnPropertyChanged("IngredientLabel"); }
        }
        
        private List<Hl7.Fhir.Model.NutritionIntake.IngredientLabelComponent> _IngredientLabel;
        
        /// <summary>
        /// Who is/was consuming the food or fluid
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=170)]
        [CLSCompliant(false)]
		[References("Patient","Group")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Encounter associated with NutritionIntake
        /// </summary>
        [FhirElement("encounter", InSummary=true, Order=180)]
        [CLSCompliant(false)]
		[References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// The date/time or interval when the food or fluid is/was consumed
        /// </summary>
        [FhirElement("effective", InSummary=true, Order=190, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Effective
        {
            get { return _Effective; }
            set { _Effective = value; OnPropertyChanged("Effective"); }
        }
        
        private Hl7.Fhir.Model.Element _Effective;
        
        /// <summary>
        /// When the consumption was asserted?
        /// </summary>
        [FhirElement("dateAsserted", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateAssertedElement
        {
            get { return _DateAssertedElement; }
            set { _DateAssertedElement = value; OnPropertyChanged("DateAssertedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateAssertedElement;
        
        /// <summary>
        /// When the consumption was asserted?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DateAsserted
        {
            get { return DateAssertedElement != null ? DateAssertedElement.Value : null; }
            set
            {
                if (value == null)
                  DateAssertedElement = null; 
                else
                  DateAssertedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("DateAsserted");
            }
        }
        
        /// <summary>
        /// Person or organization that provided the information about the consumption of this food or fluid
        /// </summary>
        [FhirElement("informationSource", Order=210)]
        [CLSCompliant(false)]
		[References("Patient","Practitioner","PractitionerRole","RelatedPerson","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference InformationSource
        {
            get { return _InformationSource; }
            set { _InformationSource = value; OnPropertyChanged("InformationSource"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _InformationSource;
        
        /// <summary>
        /// Additional supporting information
        /// </summary>
        [FhirElement("derivedFrom", Order=220)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> DerivedFrom
        {
            get { if(_DerivedFrom==null) _DerivedFrom = new List<Hl7.Fhir.Model.ResourceReference>(); return _DerivedFrom; }
            set { _DerivedFrom = value; OnPropertyChanged("DerivedFrom"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _DerivedFrom;
        
        /// <summary>
        /// Reason for why the food or fluid is /was consumed
        /// </summary>
        [FhirElement("reasonCode", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableReference> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableReference>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableReference> _ReasonCode;
        
        /// <summary>
        /// Further information about the consumption
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
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as NutritionIntake;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.NutritionIntake.NutritionIntakeStatusCodes>)StatusElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = new List<Hl7.Fhir.Model.CodeableConcept>(StatusReason.DeepCopy());
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(ConsumedItem != null) dest.ConsumedItem = new List<Hl7.Fhir.Model.NutritionIntake.ConsumedItemComponent>(ConsumedItem.DeepCopy());
                if(IngredientLabel != null) dest.IngredientLabel = new List<Hl7.Fhir.Model.NutritionIntake.IngredientLabelComponent>(IngredientLabel.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                if(DateAssertedElement != null) dest.DateAssertedElement = (Hl7.Fhir.Model.FhirDateTime)DateAssertedElement.DeepCopy();
                if(InformationSource != null) dest.InformationSource = (Hl7.Fhir.Model.ResourceReference)InformationSource.DeepCopy();
                if(DerivedFrom != null) dest.DerivedFrom = new List<Hl7.Fhir.Model.ResourceReference>(DerivedFrom.DeepCopy());
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableReference>(ReasonCode.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new NutritionIntake());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as NutritionIntake;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(ConsumedItem, otherT.ConsumedItem)) return false;
            if( !DeepComparable.Matches(IngredientLabel, otherT.IngredientLabel)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
            if( !DeepComparable.Matches(DateAssertedElement, otherT.DateAssertedElement)) return false;
            if( !DeepComparable.Matches(InformationSource, otherT.InformationSource)) return false;
            if( !DeepComparable.Matches(DerivedFrom, otherT.DerivedFrom)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as NutritionIntake;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(ConsumedItem, otherT.ConsumedItem)) return false;
            if( !DeepComparable.IsExactly(IngredientLabel, otherT.IngredientLabel)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
            if( !DeepComparable.IsExactly(DateAssertedElement, otherT.DateAssertedElement)) return false;
            if( !DeepComparable.IsExactly(InformationSource, otherT.InformationSource)) return false;
            if( !DeepComparable.IsExactly(DerivedFrom, otherT.DerivedFrom)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            
            return true;
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
				foreach (var elem in StatusReason) { if (elem != null) yield return elem; }
				foreach (var elem in Category) { if (elem != null) yield return elem; }
				foreach (var elem in ConsumedItem) { if (elem != null) yield return elem; }
				foreach (var elem in IngredientLabel) { if (elem != null) yield return elem; }
				if (Subject != null) yield return Subject;
				if (Encounter != null) yield return Encounter;
				if (Effective != null) yield return Effective;
				if (DateAssertedElement != null) yield return DateAssertedElement;
				if (InformationSource != null) yield return InformationSource;
				foreach (var elem in DerivedFrom) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        public override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                foreach (var elem in PartOf) { if (elem != null) yield return new ElementValue("partOf", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in StatusReason) { if (elem != null) yield return new ElementValue("statusReason", elem); }
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                foreach (var elem in ConsumedItem) { if (elem != null) yield return new ElementValue("consumedItem", elem); }
                foreach (var elem in IngredientLabel) { if (elem != null) yield return new ElementValue("ingredientLabel", elem); }
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (Effective != null) yield return new ElementValue("effective", Effective);
                if (DateAssertedElement != null) yield return new ElementValue("dateAsserted", DateAssertedElement);
                if (InformationSource != null) yield return new ElementValue("informationSource", InformationSource);
                foreach (var elem in DerivedFrom) { if (elem != null) yield return new ElementValue("derivedFrom", elem); }
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
            }
        }

    }
    
}
