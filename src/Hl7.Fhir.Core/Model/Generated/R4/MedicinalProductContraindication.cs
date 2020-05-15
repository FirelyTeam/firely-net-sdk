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
    /// MedicinalProductContraindication
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "MedicinalProductContraindication", IsResource=true)]
    [DataContract]
    public partial class MedicinalProductContraindication : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicinalProductContraindication; } }
        [NotMapped]
        public override string TypeName { get { return "MedicinalProductContraindication"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "OtherTherapyComponent")]
        [DataContract]
        public partial class OtherTherapyComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OtherTherapyComponent"; } }
            
            /// <summary>
            /// The type of relationship between the medicinal product indication or contraindication and another therapy
            /// </summary>
            [FhirElement("therapyRelationshipType", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
            [FhirElement("medication", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("OtherTherapyComponent");
                base.Serialize(sink);
                sink.Element("therapyRelationshipType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TherapyRelationshipType?.Serialize(sink);
                sink.Element("medication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Medication?.Serialize(sink);
                sink.End();
            }
        
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
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
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
        /// The disease, symptom or procedure for the contraindication
        /// </summary>
        [FhirElement("disease", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
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
        [FhirElement("diseaseStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
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
        [FhirElement("comorbidity", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
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
        [FhirElement("therapeuticIndication", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("MedicinalProductIndication")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> TherapeuticIndication
        {
            get { if(_TherapeuticIndication==null) _TherapeuticIndication = new List<Hl7.Fhir.Model.ResourceReference>(); return _TherapeuticIndication; }
            set { _TherapeuticIndication = value; OnPropertyChanged("TherapeuticIndication"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _TherapeuticIndication;
        
        /// <summary>
        /// Information about the use of the medicinal product in relation to other therapies described as part of the indication
        /// </summary>
        [FhirElement("otherTherapy", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<OtherTherapyComponent> OtherTherapy
        {
            get { if(_OtherTherapy==null) _OtherTherapy = new List<OtherTherapyComponent>(); return _OtherTherapy; }
            set { _OtherTherapy = value; OnPropertyChanged("OtherTherapy"); }
        }
        
        private List<OtherTherapyComponent> _OtherTherapy;
        
        /// <summary>
        /// The population group to which this applies
        /// </summary>
        [FhirElement("population", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Population> Population
        {
            get { if(_Population==null) _Population = new List<Hl7.Fhir.Model.Population>(); return _Population; }
            set { _Population = value; OnPropertyChanged("Population"); }
        }
        
        private List<Hl7.Fhir.Model.Population> _Population;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicinalProductContraindication;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Subject != null) dest.Subject = new List<Hl7.Fhir.Model.ResourceReference>(Subject.DeepCopy());
                if(Disease != null) dest.Disease = (Hl7.Fhir.Model.CodeableConcept)Disease.DeepCopy();
                if(DiseaseStatus != null) dest.DiseaseStatus = (Hl7.Fhir.Model.CodeableConcept)DiseaseStatus.DeepCopy();
                if(Comorbidity != null) dest.Comorbidity = new List<Hl7.Fhir.Model.CodeableConcept>(Comorbidity.DeepCopy());
                if(TherapeuticIndication != null) dest.TherapeuticIndication = new List<Hl7.Fhir.Model.ResourceReference>(TherapeuticIndication.DeepCopy());
                if(OtherTherapy != null) dest.OtherTherapy = new List<OtherTherapyComponent>(OtherTherapy.DeepCopy());
                if(Population != null) dest.Population = new List<Hl7.Fhir.Model.Population>(Population.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new MedicinalProductContraindication());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicinalProductContraindication;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
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
            var otherT = other as MedicinalProductContraindication;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Disease, otherT.Disease)) return false;
            if( !DeepComparable.IsExactly(DiseaseStatus, otherT.DiseaseStatus)) return false;
            if( !DeepComparable.IsExactly(Comorbidity, otherT.Comorbidity)) return false;
            if( !DeepComparable.IsExactly(TherapeuticIndication, otherT.TherapeuticIndication)) return false;
            if( !DeepComparable.IsExactly(OtherTherapy, otherT.OtherTherapy)) return false;
            if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MedicinalProductContraindication");
            base.Serialize(sink);
            sink.BeginList("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Subject)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("disease", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Disease?.Serialize(sink);
            sink.Element("diseaseStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DiseaseStatus?.Serialize(sink);
            sink.BeginList("comorbidity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Comorbidity)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("therapeuticIndication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in TherapeuticIndication)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("otherTherapy", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in OtherTherapy)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("population", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Population)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Subject) { if (elem != null) yield return elem; }
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
                foreach (var elem in Subject) { if (elem != null) yield return new ElementValue("subject", elem); }
                if (Disease != null) yield return new ElementValue("disease", Disease);
                if (DiseaseStatus != null) yield return new ElementValue("diseaseStatus", DiseaseStatus);
                foreach (var elem in Comorbidity) { if (elem != null) yield return new ElementValue("comorbidity", elem); }
                foreach (var elem in TherapeuticIndication) { if (elem != null) yield return new ElementValue("therapeuticIndication", elem); }
                foreach (var elem in OtherTherapy) { if (elem != null) yield return new ElementValue("otherTherapy", elem); }
                foreach (var elem in Population) { if (elem != null) yield return new ElementValue("population", elem); }
            }
        }
    
    }

}
