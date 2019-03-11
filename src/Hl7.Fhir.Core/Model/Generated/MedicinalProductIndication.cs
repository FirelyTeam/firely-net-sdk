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
    /// MedicinalProductIndication
    /// </summary>
    [FhirType("MedicinalProductIndication", IsResource=true)]
    [DataContract]
    public partial class MedicinalProductIndication : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicinalProductIndication; } }
        [NotMapped]
        public override string TypeName { get { return "MedicinalProductIndication"; } }
        
        [FhirType("OtherTherapyComponent", NamedBackboneElement=true)]
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
                    if (TherapyRelationshipType != null) yield return new ElementValue("therapyRelationshipType", TherapyRelationshipType);
                    if (Medication != null) yield return new ElementValue("medication", Medication);
                }
            }

            
        }
        
        
        /// <summary>
        /// The medication for which this is an indication
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=90)]
        [CLSCompliant(false)]
		[References("MedicinalProduct","Medication")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Subject
        {
            get { if(_Subject==null) _Subject = new List<Hl7.Fhir.Model.ResourceReference>(); return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Subject;
        
        /// <summary>
        /// The disease, symptom or procedure that is the indication for treatment
        /// </summary>
        [FhirElement("diseaseSymptomProcedure", InSummary=true, Order=100)]
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
        [FhirElement("diseaseStatus", InSummary=true, Order=110)]
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
        [FhirElement("comorbidity", InSummary=true, Order=120)]
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
        [FhirElement("intendedEffect", InSummary=true, Order=130)]
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
        [FhirElement("duration", InSummary=true, Order=140)]
        [DataMember]
        public Quantity Duration
        {
            get { return _Duration; }
            set { _Duration = value; OnPropertyChanged("Duration"); }
        }
        
        private Quantity _Duration;
        
        /// <summary>
        /// Information about the use of the medicinal product in relation to other therapies described as part of the indication
        /// </summary>
        [FhirElement("otherTherapy", InSummary=true, Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicinalProductIndication.OtherTherapyComponent> OtherTherapy
        {
            get { if(_OtherTherapy==null) _OtherTherapy = new List<Hl7.Fhir.Model.MedicinalProductIndication.OtherTherapyComponent>(); return _OtherTherapy; }
            set { _OtherTherapy = value; OnPropertyChanged("OtherTherapy"); }
        }
        
        private List<Hl7.Fhir.Model.MedicinalProductIndication.OtherTherapyComponent> _OtherTherapy;
        
        /// <summary>
        /// Describe the undesirable effects of the medicinal product
        /// </summary>
        [FhirElement("undesirableEffect", InSummary=true, Order=160)]
        [CLSCompliant(false)]
		[References("MedicinalProductUndesirableEffect")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> UndesirableEffect
        {
            get { if(_UndesirableEffect==null) _UndesirableEffect = new List<Hl7.Fhir.Model.ResourceReference>(); return _UndesirableEffect; }
            set { _UndesirableEffect = value; OnPropertyChanged("UndesirableEffect"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _UndesirableEffect;
        
        /// <summary>
        /// The population group to which this applies
        /// </summary>
        [FhirElement("population", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Population> Population
        {
            get { if(_Population==null) _Population = new List<Population>(); return _Population; }
            set { _Population = value; OnPropertyChanged("Population"); }
        }
        
        private List<Population> _Population;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicinalProductIndication;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Subject != null) dest.Subject = new List<Hl7.Fhir.Model.ResourceReference>(Subject.DeepCopy());
                if(DiseaseSymptomProcedure != null) dest.DiseaseSymptomProcedure = (Hl7.Fhir.Model.CodeableConcept)DiseaseSymptomProcedure.DeepCopy();
                if(DiseaseStatus != null) dest.DiseaseStatus = (Hl7.Fhir.Model.CodeableConcept)DiseaseStatus.DeepCopy();
                if(Comorbidity != null) dest.Comorbidity = new List<Hl7.Fhir.Model.CodeableConcept>(Comorbidity.DeepCopy());
                if(IntendedEffect != null) dest.IntendedEffect = (Hl7.Fhir.Model.CodeableConcept)IntendedEffect.DeepCopy();
                if(Duration != null) dest.Duration = (Quantity)Duration.DeepCopy();
                if(OtherTherapy != null) dest.OtherTherapy = new List<Hl7.Fhir.Model.MedicinalProductIndication.OtherTherapyComponent>(OtherTherapy.DeepCopy());
                if(UndesirableEffect != null) dest.UndesirableEffect = new List<Hl7.Fhir.Model.ResourceReference>(UndesirableEffect.DeepCopy());
                if(Population != null) dest.Population = new List<Population>(Population.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new MedicinalProductIndication());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicinalProductIndication;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(DiseaseSymptomProcedure, otherT.DiseaseSymptomProcedure)) return false;
            if( !DeepComparable.Matches(DiseaseStatus, otherT.DiseaseStatus)) return false;
            if( !DeepComparable.Matches(Comorbidity, otherT.Comorbidity)) return false;
            if( !DeepComparable.Matches(IntendedEffect, otherT.IntendedEffect)) return false;
            if( !DeepComparable.Matches(Duration, otherT.Duration)) return false;
            if( !DeepComparable.Matches(OtherTherapy, otherT.OtherTherapy)) return false;
            if( !DeepComparable.Matches(UndesirableEffect, otherT.UndesirableEffect)) return false;
            if( !DeepComparable.Matches(Population, otherT.Population)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicinalProductIndication;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(DiseaseSymptomProcedure, otherT.DiseaseSymptomProcedure)) return false;
            if( !DeepComparable.IsExactly(DiseaseStatus, otherT.DiseaseStatus)) return false;
            if( !DeepComparable.IsExactly(Comorbidity, otherT.Comorbidity)) return false;
            if( !DeepComparable.IsExactly(IntendedEffect, otherT.IntendedEffect)) return false;
            if( !DeepComparable.IsExactly(Duration, otherT.Duration)) return false;
            if( !DeepComparable.IsExactly(OtherTherapy, otherT.OtherTherapy)) return false;
            if( !DeepComparable.IsExactly(UndesirableEffect, otherT.UndesirableEffect)) return false;
            if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Subject) { if (elem != null) yield return elem; }
				if (DiseaseSymptomProcedure != null) yield return DiseaseSymptomProcedure;
				if (DiseaseStatus != null) yield return DiseaseStatus;
				foreach (var elem in Comorbidity) { if (elem != null) yield return elem; }
				if (IntendedEffect != null) yield return IntendedEffect;
				if (Duration != null) yield return Duration;
				foreach (var elem in OtherTherapy) { if (elem != null) yield return elem; }
				foreach (var elem in UndesirableEffect) { if (elem != null) yield return elem; }
				foreach (var elem in Population) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Subject) { if (elem != null) yield return new ElementValue("subject", elem); }
                if (DiseaseSymptomProcedure != null) yield return new ElementValue("diseaseSymptomProcedure", DiseaseSymptomProcedure);
                if (DiseaseStatus != null) yield return new ElementValue("diseaseStatus", DiseaseStatus);
                foreach (var elem in Comorbidity) { if (elem != null) yield return new ElementValue("comorbidity", elem); }
                if (IntendedEffect != null) yield return new ElementValue("intendedEffect", IntendedEffect);
                if (Duration != null) yield return new ElementValue("duration", Duration);
                foreach (var elem in OtherTherapy) { if (elem != null) yield return new ElementValue("otherTherapy", elem); }
                foreach (var elem in UndesirableEffect) { if (elem != null) yield return new ElementValue("undesirableEffect", elem); }
                foreach (var elem in Population) { if (elem != null) yield return new ElementValue("population", elem); }
            }
        }

    }
    
}
