using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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

//
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Administration of medication to a patient
    /// </summary>
    [FhirType("MedicationStatement", IsResource=true)]
    [DataContract]
    public partial class MedicationStatement : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        [FhirType("MedicationStatementDosageComponent")]
        [DataContract]
        public partial class MedicationStatementDosageComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// When/how often was medication taken?
            /// </summary>
            [FhirElement("timing", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Schedule Timing
            {
                get { return _Timing; }
                set { _Timing = value; OnPropertyChanged("Timing"); }
            }
            private Hl7.Fhir.Model.Schedule _Timing;
            
            /// <summary>
            /// Take "as needed" f(or x)
            /// </summary>
            [FhirElement("asNeeded", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [DataMember]
            public Hl7.Fhir.Model.Element AsNeeded
            {
                get { return _AsNeeded; }
                set { _AsNeeded = value; OnPropertyChanged("AsNeeded"); }
            }
            private Hl7.Fhir.Model.Element _AsNeeded;
            
            /// <summary>
            /// Where on body was medication administered?
            /// </summary>
            [FhirElement("site", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Site
            {
                get { return _Site; }
                set { _Site = value; OnPropertyChanged("Site"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Site;
            
            /// <summary>
            /// How did the medication enter the body?
            /// </summary>
            [FhirElement("route", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Route
            {
                get { return _Route; }
                set { _Route = value; OnPropertyChanged("Route"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Route;
            
            /// <summary>
            /// Technique used to administer medication
            /// </summary>
            [FhirElement("method", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method
            {
                get { return _Method; }
                set { _Method = value; OnPropertyChanged("Method"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Method;
            
            /// <summary>
            /// Amount administered in one dose
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            private Hl7.Fhir.Model.Quantity _Quantity;
            
            /// <summary>
            /// Dose quantity per unit of time
            /// </summary>
            [FhirElement("rate", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Rate
            {
                get { return _Rate; }
                set { _Rate = value; OnPropertyChanged("Rate"); }
            }
            private Hl7.Fhir.Model.Ratio _Rate;
            
            /// <summary>
            /// Maximum dose that was consumed per unit of time
            /// </summary>
            [FhirElement("maxDosePerPeriod", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio MaxDosePerPeriod
            {
                get { return _MaxDosePerPeriod; }
                set { _MaxDosePerPeriod = value; OnPropertyChanged("MaxDosePerPeriod"); }
            }
            private Hl7.Fhir.Model.Ratio _MaxDosePerPeriod;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MedicationStatementDosageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Schedule)Timing.DeepCopy();
                    if(AsNeeded != null) dest.AsNeeded = (Hl7.Fhir.Model.Element)AsNeeded.DeepCopy();
                    if(Site != null) dest.Site = (Hl7.Fhir.Model.CodeableConcept)Site.DeepCopy();
                    if(Route != null) dest.Route = (Hl7.Fhir.Model.CodeableConcept)Route.DeepCopy();
                    if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                    if(Rate != null) dest.Rate = (Hl7.Fhir.Model.Ratio)Rate.DeepCopy();
                    if(MaxDosePerPeriod != null) dest.MaxDosePerPeriod = (Hl7.Fhir.Model.Ratio)MaxDosePerPeriod.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MedicationStatementDosageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MedicationStatementDosageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
                if( !DeepComparable.Matches(AsNeeded, otherT.AsNeeded)) return false;
                if( !DeepComparable.Matches(Site, otherT.Site)) return false;
                if( !DeepComparable.Matches(Route, otherT.Route)) return false;
                if( !DeepComparable.Matches(Method, otherT.Method)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Rate, otherT.Rate)) return false;
                if( !DeepComparable.Matches(MaxDosePerPeriod, otherT.MaxDosePerPeriod)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MedicationStatementDosageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
                if( !DeepComparable.IsExactly(AsNeeded, otherT.AsNeeded)) return false;
                if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
                if( !DeepComparable.IsExactly(Route, otherT.Route)) return false;
                if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Rate, otherT.Rate)) return false;
                if( !DeepComparable.IsExactly(MaxDosePerPeriod, otherT.MaxDosePerPeriod)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External Identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Who was/is taking medication
        /// </summary>
        [FhirElement("patient", Order=80)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// True if medication is/was not being taken
        /// </summary>
        [FhirElement("wasNotGiven", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean WasNotGivenElement
        {
            get { return _WasNotGivenElement; }
            set { _WasNotGivenElement = value; OnPropertyChanged("WasNotGivenElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _WasNotGivenElement;
        
        /// <summary>
        /// True if medication is/was not being taken
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? WasNotGiven
        {
            get { return WasNotGivenElement != null ? WasNotGivenElement.Value : null; }
            set
            {
                if(value == null)
                  WasNotGivenElement = null; 
                else
                  WasNotGivenElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("WasNotGiven");
            }
        }
        
        /// <summary>
        /// True if asserting medication was not given
        /// </summary>
        [FhirElement("reasonNotGiven", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonNotGiven
        {
            get { return _ReasonNotGiven; }
            set { _ReasonNotGiven = value; OnPropertyChanged("ReasonNotGiven"); }
        }
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonNotGiven;
        
        /// <summary>
        /// Over what period was medication consumed?
        /// </summary>
        [FhirElement("whenGiven", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Period WhenGiven
        {
            get { return _WhenGiven; }
            set { _WhenGiven = value; OnPropertyChanged("WhenGiven"); }
        }
        private Hl7.Fhir.Model.Period _WhenGiven;
        
        /// <summary>
        /// What medication was taken?
        /// </summary>
        [FhirElement("medication", Order=120)]
        [References("Medication")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Medication
        {
            get { return _Medication; }
            set { _Medication = value; OnPropertyChanged("Medication"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Medication;
        
        /// <summary>
        /// E.g. infusion pump
        /// </summary>
        [FhirElement("device", Order=130)]
        [References("Device")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Device
        {
            get { return _Device; }
            set { _Device = value; OnPropertyChanged("Device"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Device;
        
        /// <summary>
        /// Details of how medication was taken
        /// </summary>
        [FhirElement("dosage", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicationStatement.MedicationStatementDosageComponent> Dosage
        {
            get { return _Dosage; }
            set { _Dosage = value; OnPropertyChanged("Dosage"); }
        }
        private List<Hl7.Fhir.Model.MedicationStatement.MedicationStatementDosageComponent> _Dosage;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicationStatement;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(WasNotGivenElement != null) dest.WasNotGivenElement = (Hl7.Fhir.Model.FhirBoolean)WasNotGivenElement.DeepCopy();
                if(ReasonNotGiven != null) dest.ReasonNotGiven = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonNotGiven.DeepCopy());
                if(WhenGiven != null) dest.WhenGiven = (Hl7.Fhir.Model.Period)WhenGiven.DeepCopy();
                if(Medication != null) dest.Medication = (Hl7.Fhir.Model.ResourceReference)Medication.DeepCopy();
                if(Device != null) dest.Device = new List<Hl7.Fhir.Model.ResourceReference>(Device.DeepCopy());
                if(Dosage != null) dest.Dosage = new List<Hl7.Fhir.Model.MedicationStatement.MedicationStatementDosageComponent>(Dosage.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new MedicationStatement());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicationStatement;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(WasNotGivenElement, otherT.WasNotGivenElement)) return false;
            if( !DeepComparable.Matches(ReasonNotGiven, otherT.ReasonNotGiven)) return false;
            if( !DeepComparable.Matches(WhenGiven, otherT.WhenGiven)) return false;
            if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
            if( !DeepComparable.Matches(Device, otherT.Device)) return false;
            if( !DeepComparable.Matches(Dosage, otherT.Dosage)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicationStatement;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(WasNotGivenElement, otherT.WasNotGivenElement)) return false;
            if( !DeepComparable.IsExactly(ReasonNotGiven, otherT.ReasonNotGiven)) return false;
            if( !DeepComparable.IsExactly(WhenGiven, otherT.WhenGiven)) return false;
            if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
            if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
            if( !DeepComparable.IsExactly(Dosage, otherT.Dosage)) return false;
            
            return true;
        }
        
    }
    
}
