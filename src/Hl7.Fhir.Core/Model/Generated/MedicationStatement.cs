using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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

//
// Generated on Thu, Apr 2, 2015 14:21+0200 for FHIR v0.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Administration of medication to a patient
    /// </summary>
    [FhirType("MedicationStatement", IsResource=true)]
    [DataContract]
    public partial class MedicationStatement : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicationStatement; } }
        [NotMapped]
        public override string TypeName { get { return "MedicationStatement"; } }
        
        /// <summary>
        /// A set of codes indicating the current status of a MedicationStatement
        /// </summary>
        [FhirEnumeration("MedicationStatementStatus")]
        public enum MedicationStatementStatus
        {
            /// <summary>
            /// The medication is still being taken.
            /// </summary>
            [EnumLiteral("in-progress")]
            InProgress,
            /// <summary>
            /// All actions that are implied by the statement have occurred.
            /// </summary>
            [EnumLiteral("completed")]
            Completed,
            /// <summary>
            /// The statement was entered in error and therefore nullified.
            /// </summary>
            [EnumLiteral("entered-in-error")]
            EnteredInError,
        }
        
        [FhirType("MedicationStatementDosageComponent")]
        [DataContract]
        public partial class MedicationStatementDosageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "MedicationStatementDosageComponent"; } }
            
            /// <summary>
            /// Dosage Instructions
            /// </summary>
            [FhirElement("text", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Dosage Instructions
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if(value == null)
                      TextElement = null; 
                    else
                      TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            /// <summary>
            /// When/how often was medication taken?
            /// </summary>
            [FhirElement("schedule", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Timing Schedule
            {
                get { return _Schedule; }
                set { _Schedule = value; OnPropertyChanged("Schedule"); }
            }
            
            private Hl7.Fhir.Model.Timing _Schedule;
            
            /// <summary>
            /// Take "as needed" f(or x)
            /// </summary>
            [FhirElement("asNeeded", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
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
            [FhirElement("site", InSummary=true, Order=70)]
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
            [FhirElement("route", InSummary=true, Order=80)]
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
            [FhirElement("method", InSummary=true, Order=90)]
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
            [FhirElement("quantity", InSummary=true, Order=100)]
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
            [FhirElement("rate", InSummary=true, Order=110)]
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
            [FhirElement("maxDosePerPeriod", InSummary=true, Order=120)]
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
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(Schedule != null) dest.Schedule = (Hl7.Fhir.Model.Timing)Schedule.DeepCopy();
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
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(Schedule, otherT.Schedule)) return false;
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
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(Schedule, otherT.Schedule)) return false;
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
        /// Who was/is taking medication
        /// </summary>
        [FhirElement("patient", Order=100)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        [FhirElement("informationSource", Order=110)]
        [References("Patient","Practitioner","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference InformationSource
        {
            get { return _InformationSource; }
            set { _InformationSource = value; OnPropertyChanged("InformationSource"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _InformationSource;
        
        /// <summary>
        /// When the statement was asserted?
        /// </summary>
        [FhirElement("dateAsserted", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateAssertedElement
        {
            get { return _DateAssertedElement; }
            set { _DateAssertedElement = value; OnPropertyChanged("DateAssertedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateAssertedElement;
        
        /// <summary>
        /// When the statement was asserted?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DateAsserted
        {
            get { return DateAssertedElement != null ? DateAssertedElement.Value : null; }
            set
            {
                if(value == null)
                  DateAssertedElement = null; 
                else
                  DateAssertedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("DateAsserted");
            }
        }
        
        /// <summary>
        /// in-progress | completed | entered-in-error
        /// </summary>
        [FhirElement("status", Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MedicationStatement.MedicationStatementStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.MedicationStatement.MedicationStatementStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | completed | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MedicationStatement.MedicationStatementStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.MedicationStatement.MedicationStatementStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// True if medication is/was not being taken
        /// </summary>
        [FhirElement("wasNotGiven", Order=140)]
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
        [FhirElement("reasonNotGiven", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonNotGiven
        {
            get { if(_ReasonNotGiven==null) _ReasonNotGiven = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonNotGiven; }
            set { _ReasonNotGiven = value; OnPropertyChanged("ReasonNotGiven"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonNotGiven;
        
        [FhirElement("reasonForUse", Order=160, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element ReasonForUse
        {
            get { return _ReasonForUse; }
            set { _ReasonForUse = value; OnPropertyChanged("ReasonForUse"); }
        }
        
        private Hl7.Fhir.Model.Element _ReasonForUse;
        
        /// <summary>
        /// Over what period was medication consumed?
        /// </summary>
        [FhirElement("effective", Order=170, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Effective
        {
            get { return _Effective; }
            set { _Effective = value; OnPropertyChanged("Effective"); }
        }
        
        private Hl7.Fhir.Model.Element _Effective;
        
        /// <summary>
        /// Further information about the statement
        /// </summary>
        [FhirElement("note", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NoteElement
        {
            get { return _NoteElement; }
            set { _NoteElement = value; OnPropertyChanged("NoteElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NoteElement;
        
        /// <summary>
        /// Further information about the statement
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Note
        {
            get { return NoteElement != null ? NoteElement.Value : null; }
            set
            {
                if(value == null)
                  NoteElement = null; 
                else
                  NoteElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Note");
            }
        }
        
        /// <summary>
        /// What medication was taken?
        /// </summary>
        [FhirElement("medication", Order=190)]
        [References("Medication")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Medication
        {
            get { return _Medication; }
            set { _Medication = value; OnPropertyChanged("Medication"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Medication;
        
        /// <summary>
        /// Details of how medication was taken
        /// </summary>
        [FhirElement("dosage", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicationStatement.MedicationStatementDosageComponent> Dosage
        {
            get { if(_Dosage==null) _Dosage = new List<Hl7.Fhir.Model.MedicationStatement.MedicationStatementDosageComponent>(); return _Dosage; }
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
                if(InformationSource != null) dest.InformationSource = (Hl7.Fhir.Model.ResourceReference)InformationSource.DeepCopy();
                if(DateAssertedElement != null) dest.DateAssertedElement = (Hl7.Fhir.Model.FhirDateTime)DateAssertedElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.MedicationStatement.MedicationStatementStatus>)StatusElement.DeepCopy();
                if(WasNotGivenElement != null) dest.WasNotGivenElement = (Hl7.Fhir.Model.FhirBoolean)WasNotGivenElement.DeepCopy();
                if(ReasonNotGiven != null) dest.ReasonNotGiven = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonNotGiven.DeepCopy());
                if(ReasonForUse != null) dest.ReasonForUse = (Hl7.Fhir.Model.Element)ReasonForUse.DeepCopy();
                if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                if(NoteElement != null) dest.NoteElement = (Hl7.Fhir.Model.FhirString)NoteElement.DeepCopy();
                if(Medication != null) dest.Medication = (Hl7.Fhir.Model.ResourceReference)Medication.DeepCopy();
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
            if( !DeepComparable.Matches(InformationSource, otherT.InformationSource)) return false;
            if( !DeepComparable.Matches(DateAssertedElement, otherT.DateAssertedElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(WasNotGivenElement, otherT.WasNotGivenElement)) return false;
            if( !DeepComparable.Matches(ReasonNotGiven, otherT.ReasonNotGiven)) return false;
            if( !DeepComparable.Matches(ReasonForUse, otherT.ReasonForUse)) return false;
            if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
            if( !DeepComparable.Matches(NoteElement, otherT.NoteElement)) return false;
            if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
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
            if( !DeepComparable.IsExactly(InformationSource, otherT.InformationSource)) return false;
            if( !DeepComparable.IsExactly(DateAssertedElement, otherT.DateAssertedElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(WasNotGivenElement, otherT.WasNotGivenElement)) return false;
            if( !DeepComparable.IsExactly(ReasonNotGiven, otherT.ReasonNotGiven)) return false;
            if( !DeepComparable.IsExactly(ReasonForUse, otherT.ReasonForUse)) return false;
            if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
            if( !DeepComparable.IsExactly(NoteElement, otherT.NoteElement)) return false;
            if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
            if( !DeepComparable.IsExactly(Dosage, otherT.Dosage)) return false;
            
            return true;
        }
        
    }
    
}
