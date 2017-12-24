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
// Generated for FHIR v3.1.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// MedicinalProductClinicals
    /// </summary>
    [FhirType("MedicinalProductClinicals", IsResource=true)]
    [DataContract]
    public partial class MedicinalProductClinicals : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicinalProductClinicals; } }
        [NotMapped]
        public override string TypeName { get { return "MedicinalProductClinicals"; } }
        
        [FhirType("UndesirableEffectsComponent")]
        [DataContract]
        public partial class UndesirableEffectsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "UndesirableEffectsComponent"; } }
            
            /// <summary>
            /// The symptom, condition or undesirable effect
            /// </summary>
            [FhirElement("symptomConditionEffect", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept SymptomConditionEffect
            {
                get { return _SymptomConditionEffect; }
                set { _SymptomConditionEffect = value; OnPropertyChanged("SymptomConditionEffect"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _SymptomConditionEffect;
            
            /// <summary>
            /// Classification of the effect
            /// </summary>
            [FhirElement("classification", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Classification
            {
                get { return _Classification; }
                set { _Classification = value; OnPropertyChanged("Classification"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Classification;
            
            /// <summary>
            /// The frequency of occurrence of the effect
            /// </summary>
            [FhirElement("frequencyOfOccurrence", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept FrequencyOfOccurrence
            {
                get { return _FrequencyOfOccurrence; }
                set { _FrequencyOfOccurrence = value; OnPropertyChanged("FrequencyOfOccurrence"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _FrequencyOfOccurrence;
            
            /// <summary>
            /// The population group to which this applies
            /// </summary>
            [FhirElement("population", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MedicinalProductClinicals.PopulationComponent> Population
            {
                get { if(_Population==null) _Population = new List<Hl7.Fhir.Model.MedicinalProductClinicals.PopulationComponent>(); return _Population; }
                set { _Population = value; OnPropertyChanged("Population"); }
            }
            
            private List<Hl7.Fhir.Model.MedicinalProductClinicals.PopulationComponent> _Population;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as UndesirableEffectsComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SymptomConditionEffect != null) dest.SymptomConditionEffect = (Hl7.Fhir.Model.CodeableConcept)SymptomConditionEffect.DeepCopy();
                    if(Classification != null) dest.Classification = (Hl7.Fhir.Model.CodeableConcept)Classification.DeepCopy();
                    if(FrequencyOfOccurrence != null) dest.FrequencyOfOccurrence = (Hl7.Fhir.Model.CodeableConcept)FrequencyOfOccurrence.DeepCopy();
                    if(Population != null) dest.Population = new List<Hl7.Fhir.Model.MedicinalProductClinicals.PopulationComponent>(Population.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new UndesirableEffectsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as UndesirableEffectsComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SymptomConditionEffect, otherT.SymptomConditionEffect)) return false;
                if( !DeepComparable.Matches(Classification, otherT.Classification)) return false;
                if( !DeepComparable.Matches(FrequencyOfOccurrence, otherT.FrequencyOfOccurrence)) return false;
                if( !DeepComparable.Matches(Population, otherT.Population)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as UndesirableEffectsComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SymptomConditionEffect, otherT.SymptomConditionEffect)) return false;
                if( !DeepComparable.IsExactly(Classification, otherT.Classification)) return false;
                if( !DeepComparable.IsExactly(FrequencyOfOccurrence, otherT.FrequencyOfOccurrence)) return false;
                if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SymptomConditionEffect != null) yield return SymptomConditionEffect;
                    if (Classification != null) yield return Classification;
                    if (FrequencyOfOccurrence != null) yield return FrequencyOfOccurrence;
                    foreach (var elem in Population) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SymptomConditionEffect != null) yield return new ElementValue("symptomConditionEffect", false, SymptomConditionEffect);
                    if (Classification != null) yield return new ElementValue("classification", false, Classification);
                    if (FrequencyOfOccurrence != null) yield return new ElementValue("frequencyOfOccurrence", false, FrequencyOfOccurrence);
                    foreach (var elem in Population) { if (elem != null) yield return new ElementValue("population", true, elem); }
                }
            }

            
        }
        
        
        [FhirType("PopulationComponent")]
        [DataContract]
        public partial class PopulationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PopulationComponent"; } }
            
            /// <summary>
            /// The age of the specific population
            /// </summary>
            [FhirElement("age", InSummary=true, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [DataMember]
            public Hl7.Fhir.Model.Element Age
            {
                get { return _Age; }
                set { _Age = value; OnPropertyChanged("Age"); }
            }
            
            private Hl7.Fhir.Model.Element _Age;
            
            /// <summary>
            /// The gender of the specific population
            /// </summary>
            [FhirElement("gender", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Gender
            {
                get { return _Gender; }
                set { _Gender = value; OnPropertyChanged("Gender"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Gender;
            
            /// <summary>
            /// Race of the specific population
            /// </summary>
            [FhirElement("race", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Race
            {
                get { return _Race; }
                set { _Race = value; OnPropertyChanged("Race"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Race;
            
            /// <summary>
            /// The existing physiological conditions of the specific population to which this applies
            /// </summary>
            [FhirElement("physiologicalCondition", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept PhysiologicalCondition
            {
                get { return _PhysiologicalCondition; }
                set { _PhysiologicalCondition = value; OnPropertyChanged("PhysiologicalCondition"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _PhysiologicalCondition;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PopulationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Age != null) dest.Age = (Hl7.Fhir.Model.Element)Age.DeepCopy();
                    if(Gender != null) dest.Gender = (Hl7.Fhir.Model.CodeableConcept)Gender.DeepCopy();
                    if(Race != null) dest.Race = (Hl7.Fhir.Model.CodeableConcept)Race.DeepCopy();
                    if(PhysiologicalCondition != null) dest.PhysiologicalCondition = (Hl7.Fhir.Model.CodeableConcept)PhysiologicalCondition.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PopulationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PopulationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Age, otherT.Age)) return false;
                if( !DeepComparable.Matches(Gender, otherT.Gender)) return false;
                if( !DeepComparable.Matches(Race, otherT.Race)) return false;
                if( !DeepComparable.Matches(PhysiologicalCondition, otherT.PhysiologicalCondition)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PopulationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Age, otherT.Age)) return false;
                if( !DeepComparable.IsExactly(Gender, otherT.Gender)) return false;
                if( !DeepComparable.IsExactly(Race, otherT.Race)) return false;
                if( !DeepComparable.IsExactly(PhysiologicalCondition, otherT.PhysiologicalCondition)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Age != null) yield return Age;
                    if (Gender != null) yield return Gender;
                    if (Race != null) yield return Race;
                    if (PhysiologicalCondition != null) yield return PhysiologicalCondition;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Age != null) yield return new ElementValue("age", false, Age);
                    if (Gender != null) yield return new ElementValue("gender", false, Gender);
                    if (Race != null) yield return new ElementValue("race", false, Race);
                    if (PhysiologicalCondition != null) yield return new ElementValue("physiologicalCondition", false, PhysiologicalCondition);
                }
            }

            
        }
        
        
        [FhirType("TherapeuticIndicationComponent")]
        [DataContract]
        public partial class TherapeuticIndicationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TherapeuticIndicationComponent"; } }
            
            /// <summary>
            /// The disease, symptom or procedure that is the indication for treatment
            /// </summary>
            [FhirElement("diseaseSymptomProcedure", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DiseaseSymptomProcedure
            {
                get { return _DiseaseSymptomProcedure; }
                set { _DiseaseSymptomProcedure = value; OnPropertyChanged("DiseaseSymptomProcedure"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _DiseaseSymptomProcedure;
            
            /// <summary>
            /// The status of the disease or symptom for which the indication applies
            /// </summary>
            [FhirElement("diseaseStatus", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DiseaseStatus
            {
                get { return _DiseaseStatus; }
                set { _DiseaseStatus = value; OnPropertyChanged("DiseaseStatus"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _DiseaseStatus;
            
            /// <summary>
            /// Comorbidity (concurrent condition) or co-infection as part of the indication
            /// </summary>
            [FhirElement("comorbidity", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Comorbidity
            {
                get { if(_Comorbidity==null) _Comorbidity = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Comorbidity; }
                set { _Comorbidity = value; OnPropertyChanged("Comorbidity"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Comorbidity;
            
            /// <summary>
            /// The intended effect, aim or strategy to be achieved by the indication
            /// </summary>
            [FhirElement("intendedEffect", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept IntendedEffect
            {
                get { return _IntendedEffect; }
                set { _IntendedEffect = value; OnPropertyChanged("IntendedEffect"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _IntendedEffect;
            
            /// <summary>
            /// Timing or duration information as part of the indication
            /// </summary>
            [FhirElement("duration", InSummary=true, Order=80)]
            [DataMember]
            public Quantity Duration
            {
                get { return _Duration; }
                set { _Duration = value; OnPropertyChanged("Duration"); }
            }
            
            private Quantity _Duration;
            
            /// <summary>
            /// Information about the use of the medicinal product in relation to other therapies as part of the indication
            /// </summary>
            [FhirElement("undesirableEffects", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MedicinalProductClinicals.UndesirableEffectsComponent> UndesirableEffects
            {
                get { if(_UndesirableEffects==null) _UndesirableEffects = new List<Hl7.Fhir.Model.MedicinalProductClinicals.UndesirableEffectsComponent>(); return _UndesirableEffects; }
                set { _UndesirableEffects = value; OnPropertyChanged("UndesirableEffects"); }
            }
            
            private List<Hl7.Fhir.Model.MedicinalProductClinicals.UndesirableEffectsComponent> _UndesirableEffects;
            
            /// <summary>
            /// Information about the use of the medicinal product in relation to other therapies described as part of the indication
            /// </summary>
            [FhirElement("otherTherapy", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MedicinalProductClinicals.OtherTherapyComponent> OtherTherapy
            {
                get { if(_OtherTherapy==null) _OtherTherapy = new List<Hl7.Fhir.Model.MedicinalProductClinicals.OtherTherapyComponent>(); return _OtherTherapy; }
                set { _OtherTherapy = value; OnPropertyChanged("OtherTherapy"); }
            }
            
            private List<Hl7.Fhir.Model.MedicinalProductClinicals.OtherTherapyComponent> _OtherTherapy;
            
            /// <summary>
            /// The population group to which this applies
            /// </summary>
            [FhirElement("population", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MedicinalProductClinicals.PopulationComponent> Population
            {
                get { if(_Population==null) _Population = new List<Hl7.Fhir.Model.MedicinalProductClinicals.PopulationComponent>(); return _Population; }
                set { _Population = value; OnPropertyChanged("Population"); }
            }
            
            private List<Hl7.Fhir.Model.MedicinalProductClinicals.PopulationComponent> _Population;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TherapeuticIndicationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DiseaseSymptomProcedure != null) dest.DiseaseSymptomProcedure = (Hl7.Fhir.Model.CodeableConcept)DiseaseSymptomProcedure.DeepCopy();
                    if(DiseaseStatus != null) dest.DiseaseStatus = (Hl7.Fhir.Model.CodeableConcept)DiseaseStatus.DeepCopy();
                    if(Comorbidity != null) dest.Comorbidity = new List<Hl7.Fhir.Model.CodeableConcept>(Comorbidity.DeepCopy());
                    if(IntendedEffect != null) dest.IntendedEffect = (Hl7.Fhir.Model.CodeableConcept)IntendedEffect.DeepCopy();
                    if(Duration != null) dest.Duration = (Quantity)Duration.DeepCopy();
                    if(UndesirableEffects != null) dest.UndesirableEffects = new List<Hl7.Fhir.Model.MedicinalProductClinicals.UndesirableEffectsComponent>(UndesirableEffects.DeepCopy());
                    if(OtherTherapy != null) dest.OtherTherapy = new List<Hl7.Fhir.Model.MedicinalProductClinicals.OtherTherapyComponent>(OtherTherapy.DeepCopy());
                    if(Population != null) dest.Population = new List<Hl7.Fhir.Model.MedicinalProductClinicals.PopulationComponent>(Population.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TherapeuticIndicationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TherapeuticIndicationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DiseaseSymptomProcedure, otherT.DiseaseSymptomProcedure)) return false;
                if( !DeepComparable.Matches(DiseaseStatus, otherT.DiseaseStatus)) return false;
                if( !DeepComparable.Matches(Comorbidity, otherT.Comorbidity)) return false;
                if( !DeepComparable.Matches(IntendedEffect, otherT.IntendedEffect)) return false;
                if( !DeepComparable.Matches(Duration, otherT.Duration)) return false;
                if( !DeepComparable.Matches(UndesirableEffects, otherT.UndesirableEffects)) return false;
                if( !DeepComparable.Matches(OtherTherapy, otherT.OtherTherapy)) return false;
                if( !DeepComparable.Matches(Population, otherT.Population)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TherapeuticIndicationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DiseaseSymptomProcedure, otherT.DiseaseSymptomProcedure)) return false;
                if( !DeepComparable.IsExactly(DiseaseStatus, otherT.DiseaseStatus)) return false;
                if( !DeepComparable.IsExactly(Comorbidity, otherT.Comorbidity)) return false;
                if( !DeepComparable.IsExactly(IntendedEffect, otherT.IntendedEffect)) return false;
                if( !DeepComparable.IsExactly(Duration, otherT.Duration)) return false;
                if( !DeepComparable.IsExactly(UndesirableEffects, otherT.UndesirableEffects)) return false;
                if( !DeepComparable.IsExactly(OtherTherapy, otherT.OtherTherapy)) return false;
                if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DiseaseSymptomProcedure != null) yield return DiseaseSymptomProcedure;
                    if (DiseaseStatus != null) yield return DiseaseStatus;
                    foreach (var elem in Comorbidity) { if (elem != null) yield return elem; }
                    if (IntendedEffect != null) yield return IntendedEffect;
                    if (Duration != null) yield return Duration;
                    foreach (var elem in UndesirableEffects) { if (elem != null) yield return elem; }
                    foreach (var elem in OtherTherapy) { if (elem != null) yield return elem; }
                    foreach (var elem in Population) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DiseaseSymptomProcedure != null) yield return new ElementValue("diseaseSymptomProcedure", false, DiseaseSymptomProcedure);
                    if (DiseaseStatus != null) yield return new ElementValue("diseaseStatus", false, DiseaseStatus);
                    foreach (var elem in Comorbidity) { if (elem != null) yield return new ElementValue("comorbidity", true, elem); }
                    if (IntendedEffect != null) yield return new ElementValue("intendedEffect", false, IntendedEffect);
                    if (Duration != null) yield return new ElementValue("duration", false, Duration);
                    foreach (var elem in UndesirableEffects) { if (elem != null) yield return new ElementValue("undesirableEffects", true, elem); }
                    foreach (var elem in OtherTherapy) { if (elem != null) yield return new ElementValue("otherTherapy", true, elem); }
                    foreach (var elem in Population) { if (elem != null) yield return new ElementValue("population", true, elem); }
                }
            }

            
        }
        
        
        [FhirType("OtherTherapyComponent")]
        [DataContract]
        public partial class OtherTherapyComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "OtherTherapyComponent"; } }
            
            /// <summary>
            /// The type of relationship between the medicinal product indication or contraindication and another therapy
            /// </summary>
            [FhirElement("therapyRelationshipType", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept TherapyRelationshipType
            {
                get { return _TherapyRelationshipType; }
                set { _TherapyRelationshipType = value; OnPropertyChanged("TherapyRelationshipType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _TherapyRelationshipType;
            
            /// <summary>
            /// Reference to a specific medication (active substance, medicinal product or class of products) as part of an indication or contraindication
            /// </summary>
            [FhirElement("medication", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Medication
            {
                get { return _Medication; }
                set { _Medication = value; OnPropertyChanged("Medication"); }
            }
            
            private Hl7.Fhir.Model.Element _Medication;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OtherTherapyComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TherapyRelationshipType != null) dest.TherapyRelationshipType = (Hl7.Fhir.Model.CodeableConcept)TherapyRelationshipType.DeepCopy();
                    if(Medication != null) dest.Medication = (Hl7.Fhir.Model.Element)Medication.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new OtherTherapyComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OtherTherapyComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TherapyRelationshipType, otherT.TherapyRelationshipType)) return false;
                if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OtherTherapyComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TherapyRelationshipType, otherT.TherapyRelationshipType)) return false;
                if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TherapyRelationshipType != null) yield return TherapyRelationshipType;
                    if (Medication != null) yield return Medication;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TherapyRelationshipType != null) yield return new ElementValue("therapyRelationshipType", false, TherapyRelationshipType);
                    if (Medication != null) yield return new ElementValue("medication", false, Medication);
                }
            }

            
        }
        
        
        [FhirType("ContraindicationComponent")]
        [DataContract]
        public partial class ContraindicationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContraindicationComponent"; } }
            
            /// <summary>
            /// The disease, symptom or procedure for the contraindication
            /// </summary>
            [FhirElement("disease", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Disease
            {
                get { return _Disease; }
                set { _Disease = value; OnPropertyChanged("Disease"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Disease;
            
            /// <summary>
            /// The status of the disease or symptom for the contraindication
            /// </summary>
            [FhirElement("diseaseStatus", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DiseaseStatus
            {
                get { return _DiseaseStatus; }
                set { _DiseaseStatus = value; OnPropertyChanged("DiseaseStatus"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _DiseaseStatus;
            
            /// <summary>
            /// A comorbidity (concurrent condition) or coinfection
            /// </summary>
            [FhirElement("comorbidity", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Comorbidity
            {
                get { if(_Comorbidity==null) _Comorbidity = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Comorbidity; }
                set { _Comorbidity = value; OnPropertyChanged("Comorbidity"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Comorbidity;
            
            /// <summary>
            /// Information about the use of the medicinal product in relation to other therapies as part of the indication
            /// </summary>
            [FhirElement("therapeuticIndication", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MedicinalProductClinicals.TherapeuticIndicationComponent> TherapeuticIndication
            {
                get { if(_TherapeuticIndication==null) _TherapeuticIndication = new List<Hl7.Fhir.Model.MedicinalProductClinicals.TherapeuticIndicationComponent>(); return _TherapeuticIndication; }
                set { _TherapeuticIndication = value; OnPropertyChanged("TherapeuticIndication"); }
            }
            
            private List<Hl7.Fhir.Model.MedicinalProductClinicals.TherapeuticIndicationComponent> _TherapeuticIndication;
            
            /// <summary>
            /// Information about the use of the medicinal product in relation to other therapies described as part of the contraindication
            /// </summary>
            [FhirElement("otherTherapy", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MedicinalProductClinicals.OtherTherapyComponent> OtherTherapy
            {
                get { if(_OtherTherapy==null) _OtherTherapy = new List<Hl7.Fhir.Model.MedicinalProductClinicals.OtherTherapyComponent>(); return _OtherTherapy; }
                set { _OtherTherapy = value; OnPropertyChanged("OtherTherapy"); }
            }
            
            private List<Hl7.Fhir.Model.MedicinalProductClinicals.OtherTherapyComponent> _OtherTherapy;
            
            /// <summary>
            /// The population group to which this applies
            /// </summary>
            [FhirElement("population", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MedicinalProductClinicals.PopulationComponent> Population
            {
                get { if(_Population==null) _Population = new List<Hl7.Fhir.Model.MedicinalProductClinicals.PopulationComponent>(); return _Population; }
                set { _Population = value; OnPropertyChanged("Population"); }
            }
            
            private List<Hl7.Fhir.Model.MedicinalProductClinicals.PopulationComponent> _Population;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContraindicationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Disease != null) dest.Disease = (Hl7.Fhir.Model.CodeableConcept)Disease.DeepCopy();
                    if(DiseaseStatus != null) dest.DiseaseStatus = (Hl7.Fhir.Model.CodeableConcept)DiseaseStatus.DeepCopy();
                    if(Comorbidity != null) dest.Comorbidity = new List<Hl7.Fhir.Model.CodeableConcept>(Comorbidity.DeepCopy());
                    if(TherapeuticIndication != null) dest.TherapeuticIndication = new List<Hl7.Fhir.Model.MedicinalProductClinicals.TherapeuticIndicationComponent>(TherapeuticIndication.DeepCopy());
                    if(OtherTherapy != null) dest.OtherTherapy = new List<Hl7.Fhir.Model.MedicinalProductClinicals.OtherTherapyComponent>(OtherTherapy.DeepCopy());
                    if(Population != null) dest.Population = new List<Hl7.Fhir.Model.MedicinalProductClinicals.PopulationComponent>(Population.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContraindicationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContraindicationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Disease, otherT.Disease)) return false;
                if( !DeepComparable.Matches(DiseaseStatus, otherT.DiseaseStatus)) return false;
                if( !DeepComparable.Matches(Comorbidity, otherT.Comorbidity)) return false;
                if( !DeepComparable.Matches(TherapeuticIndication, otherT.TherapeuticIndication)) return false;
                if( !DeepComparable.Matches(OtherTherapy, otherT.OtherTherapy)) return false;
                if( !DeepComparable.Matches(Population, otherT.Population)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContraindicationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Disease, otherT.Disease)) return false;
                if( !DeepComparable.IsExactly(DiseaseStatus, otherT.DiseaseStatus)) return false;
                if( !DeepComparable.IsExactly(Comorbidity, otherT.Comorbidity)) return false;
                if( !DeepComparable.IsExactly(TherapeuticIndication, otherT.TherapeuticIndication)) return false;
                if( !DeepComparable.IsExactly(OtherTherapy, otherT.OtherTherapy)) return false;
                if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Disease != null) yield return Disease;
                    if (DiseaseStatus != null) yield return DiseaseStatus;
                    foreach (var elem in Comorbidity) { if (elem != null) yield return elem; }
                    foreach (var elem in TherapeuticIndication) { if (elem != null) yield return elem; }
                    foreach (var elem in OtherTherapy) { if (elem != null) yield return elem; }
                    foreach (var elem in Population) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Disease != null) yield return new ElementValue("disease", false, Disease);
                    if (DiseaseStatus != null) yield return new ElementValue("diseaseStatus", false, DiseaseStatus);
                    foreach (var elem in Comorbidity) { if (elem != null) yield return new ElementValue("comorbidity", true, elem); }
                    foreach (var elem in TherapeuticIndication) { if (elem != null) yield return new ElementValue("therapeuticIndication", true, elem); }
                    foreach (var elem in OtherTherapy) { if (elem != null) yield return new ElementValue("otherTherapy", true, elem); }
                    foreach (var elem in Population) { if (elem != null) yield return new ElementValue("population", true, elem); }
                }
            }

            
        }
        
        
        [FhirType("InteractionsComponent")]
        [DataContract]
        public partial class InteractionsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "InteractionsComponent"; } }
            
            /// <summary>
            /// The specific medication, food or laboratory test that interacts
            /// </summary>
            [FhirElement("interactant", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Interactant
            {
                get { if(_Interactant==null) _Interactant = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Interactant; }
                set { _Interactant = value; OnPropertyChanged("Interactant"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Interactant;
            
            /// <summary>
            /// The type of the interaction
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// The effect of the interaction
            /// </summary>
            [FhirElement("effect", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Effect
            {
                get { return _Effect; }
                set { _Effect = value; OnPropertyChanged("Effect"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Effect;
            
            /// <summary>
            /// The incidence of the interaction
            /// </summary>
            [FhirElement("incidence", InSummary=true, Order=70)]
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
            [FhirElement("management", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Management
            {
                get { return _Management; }
                set { _Management = value; OnPropertyChanged("Management"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Management;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InteractionsComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Interactant != null) dest.Interactant = new List<Hl7.Fhir.Model.CodeableConcept>(Interactant.DeepCopy());
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
                return CopyTo(new InteractionsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InteractionsComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Interactant, otherT.Interactant)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Effect, otherT.Effect)) return false;
                if( !DeepComparable.Matches(Incidence, otherT.Incidence)) return false;
                if( !DeepComparable.Matches(Management, otherT.Management)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InteractionsComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
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
                    foreach (var elem in Interactant) { if (elem != null) yield return new ElementValue("interactant", true, elem); }
                    if (Type != null) yield return new ElementValue("type", false, Type);
                    if (Effect != null) yield return new ElementValue("effect", false, Effect);
                    if (Incidence != null) yield return new ElementValue("incidence", false, Incidence);
                    if (Management != null) yield return new ElementValue("management", false, Management);
                }
            }

            
        }
        
        
        /// <summary>
        /// Describe the undesirable effects of the medicinal product
        /// </summary>
        [FhirElement("undesirableEffects", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicinalProductClinicals.UndesirableEffectsComponent> UndesirableEffects
        {
            get { if(_UndesirableEffects==null) _UndesirableEffects = new List<Hl7.Fhir.Model.MedicinalProductClinicals.UndesirableEffectsComponent>(); return _UndesirableEffects; }
            set { _UndesirableEffects = value; OnPropertyChanged("UndesirableEffects"); }
        }
        
        private List<Hl7.Fhir.Model.MedicinalProductClinicals.UndesirableEffectsComponent> _UndesirableEffects;
        
        /// <summary>
        /// Indication for the Medicinal Product
        /// </summary>
        [FhirElement("therapeuticIndication", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicinalProductClinicals.TherapeuticIndicationComponent> TherapeuticIndication
        {
            get { if(_TherapeuticIndication==null) _TherapeuticIndication = new List<Hl7.Fhir.Model.MedicinalProductClinicals.TherapeuticIndicationComponent>(); return _TherapeuticIndication; }
            set { _TherapeuticIndication = value; OnPropertyChanged("TherapeuticIndication"); }
        }
        
        private List<Hl7.Fhir.Model.MedicinalProductClinicals.TherapeuticIndicationComponent> _TherapeuticIndication;
        
        /// <summary>
        /// Contraindication for the medicinal product
        /// </summary>
        [FhirElement("contraindication", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicinalProductClinicals.ContraindicationComponent> Contraindication
        {
            get { if(_Contraindication==null) _Contraindication = new List<Hl7.Fhir.Model.MedicinalProductClinicals.ContraindicationComponent>(); return _Contraindication; }
            set { _Contraindication = value; OnPropertyChanged("Contraindication"); }
        }
        
        private List<Hl7.Fhir.Model.MedicinalProductClinicals.ContraindicationComponent> _Contraindication;
        
        /// <summary>
        /// The interactions of the medicinal product with other medicinal products, or other forms of interactions
        /// </summary>
        [FhirElement("interactions", InSummary=true, Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicinalProductClinicals.InteractionsComponent> Interactions
        {
            get { if(_Interactions==null) _Interactions = new List<Hl7.Fhir.Model.MedicinalProductClinicals.InteractionsComponent>(); return _Interactions; }
            set { _Interactions = value; OnPropertyChanged("Interactions"); }
        }
        
        private List<Hl7.Fhir.Model.MedicinalProductClinicals.InteractionsComponent> _Interactions;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicinalProductClinicals;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UndesirableEffects != null) dest.UndesirableEffects = new List<Hl7.Fhir.Model.MedicinalProductClinicals.UndesirableEffectsComponent>(UndesirableEffects.DeepCopy());
                if(TherapeuticIndication != null) dest.TherapeuticIndication = new List<Hl7.Fhir.Model.MedicinalProductClinicals.TherapeuticIndicationComponent>(TherapeuticIndication.DeepCopy());
                if(Contraindication != null) dest.Contraindication = new List<Hl7.Fhir.Model.MedicinalProductClinicals.ContraindicationComponent>(Contraindication.DeepCopy());
                if(Interactions != null) dest.Interactions = new List<Hl7.Fhir.Model.MedicinalProductClinicals.InteractionsComponent>(Interactions.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new MedicinalProductClinicals());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicinalProductClinicals;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UndesirableEffects, otherT.UndesirableEffects)) return false;
            if( !DeepComparable.Matches(TherapeuticIndication, otherT.TherapeuticIndication)) return false;
            if( !DeepComparable.Matches(Contraindication, otherT.Contraindication)) return false;
            if( !DeepComparable.Matches(Interactions, otherT.Interactions)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicinalProductClinicals;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UndesirableEffects, otherT.UndesirableEffects)) return false;
            if( !DeepComparable.IsExactly(TherapeuticIndication, otherT.TherapeuticIndication)) return false;
            if( !DeepComparable.IsExactly(Contraindication, otherT.Contraindication)) return false;
            if( !DeepComparable.IsExactly(Interactions, otherT.Interactions)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in UndesirableEffects) { if (elem != null) yield return elem; }
				foreach (var elem in TherapeuticIndication) { if (elem != null) yield return elem; }
				foreach (var elem in Contraindication) { if (elem != null) yield return elem; }
				foreach (var elem in Interactions) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in UndesirableEffects) { if (elem != null) yield return new ElementValue("undesirableEffects", true, elem); }
                foreach (var elem in TherapeuticIndication) { if (elem != null) yield return new ElementValue("therapeuticIndication", true, elem); }
                foreach (var elem in Contraindication) { if (elem != null) yield return new ElementValue("contraindication", true, elem); }
                foreach (var elem in Interactions) { if (elem != null) yield return new ElementValue("interactions", true, elem); }
            }
        }

    }
    
}
