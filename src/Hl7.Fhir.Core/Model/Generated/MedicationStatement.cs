using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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
// Generated for FHIR v1.8.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Record of medication being taken by a patient
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
        /// A coded concept indicating the current status of a MedicationStatement.
        /// (url: http://hl7.org/fhir/ValueSet/medication-statement-status)
        /// </summary>
        [FhirEnumeration("MedicationStatementStatus")]
        public enum MedicationStatementStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-statement-status)
            /// </summary>
            [EnumLiteral("active"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-statement-status)
            /// </summary>
            [EnumLiteral("completed"), Description("Completed")]
            Completed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-statement-status)
            /// </summary>
            [EnumLiteral("entered-in-error"), Description("Entered in Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-statement-status)
            /// </summary>
            [EnumLiteral("intended"), Description("Intended")]
            Intended,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-statement-status)
            /// </summary>
            [EnumLiteral("stopped"), Description("Stopped")]
            Stopped,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-statement-status)
            /// </summary>
            [EnumLiteral("on-hold"), Description("On Hold")]
            OnHold,
        }

        /// <summary>
        /// A coded concept identifying level of certainty if patient has taken or has not taken the medication
        /// (url: http://hl7.org/fhir/ValueSet/medication-statement-nottaken)
        /// </summary>
        [FhirEnumeration("MedicationStatementNotTaken")]
        public enum MedicationStatementNotTaken
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-statement-nottaken)
            /// </summary>
            [EnumLiteral("y"), Description("Yes")]
            Y,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-statement-nottaken)
            /// </summary>
            [EnumLiteral("n"), Description("No")]
            N,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-statement-nottaken)
            /// </summary>
            [EnumLiteral("unk"), Description("Unknown")]
            Unk,
        }

        /// <summary>
        /// External identifier
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
        /// active | completed | entered-in-error | intended | stopped | on-hold
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MedicationStatement.MedicationStatementStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.MedicationStatement.MedicationStatementStatus> _StatusElement;
        
        /// <summary>
        /// active | completed | entered-in-error | intended | stopped | on-hold
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MedicationStatement.MedicationStatementStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.MedicationStatement.MedicationStatementStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// What medication was taken
        /// </summary>
        [FhirElement("medication", InSummary=true, Order=110, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Medication
        {
            get { return _Medication; }
            set { _Medication = value; OnPropertyChanged("Medication"); }
        }
        
        private Hl7.Fhir.Model.Element _Medication;
        
        /// <summary>
        /// Who is/was taking  the medication
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=120)]
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
        /// Over what period was medication consumed?
        /// </summary>
        [FhirElement("effective", InSummary=true, Order=130, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Effective
        {
            get { return _Effective; }
            set { _Effective = value; OnPropertyChanged("Effective"); }
        }
        
        private Hl7.Fhir.Model.Element _Effective;
        
        /// <summary>
        /// Person or organization that provided the information about the taking of this medication
        /// </summary>
        [FhirElement("informationSource", Order=140)]
        [References("Patient","Practitioner","RelatedPerson","Organization")]
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
        [FhirElement("derivedFrom", Order=150)]
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
        /// When the statement was asserted?
        /// </summary>
        [FhirElement("dateAsserted", InSummary=true, Order=160)]
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
                if (value == null)
                  DateAssertedElement = null; 
                else
                  DateAssertedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("DateAsserted");
            }
        }
        
        /// <summary>
        /// y | n | unk
        /// </summary>
        [FhirElement("notTaken", InSummary=true, Order=170)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MedicationStatement.MedicationStatementNotTaken> NotTakenElement
        {
            get { return _NotTakenElement; }
            set { _NotTakenElement = value; OnPropertyChanged("NotTakenElement"); }
        }
        
        private Code<Hl7.Fhir.Model.MedicationStatement.MedicationStatementNotTaken> _NotTakenElement;
        
        /// <summary>
        /// y | n | unk
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MedicationStatement.MedicationStatementNotTaken? NotTaken
        {
            get { return NotTakenElement != null ? NotTakenElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  NotTakenElement = null; 
                else
                  NotTakenElement = new Code<Hl7.Fhir.Model.MedicationStatement.MedicationStatementNotTaken>(value);
                OnPropertyChanged("NotTaken");
            }
        }
        
        /// <summary>
        /// True if asserting medication was not given
        /// </summary>
        [FhirElement("reasonNotTaken", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonNotTaken
        {
            get { if(_ReasonNotTaken==null) _ReasonNotTaken = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonNotTaken; }
            set { _ReasonNotTaken = value; OnPropertyChanged("ReasonNotTaken"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonNotTaken;
        
        /// <summary>
        /// Reason for why the medication is being/was taken
        /// </summary>
        [FhirElement("reasonForUseCodeableConcept", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonForUseCodeableConcept
        {
            get { if(_ReasonForUseCodeableConcept==null) _ReasonForUseCodeableConcept = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonForUseCodeableConcept; }
            set { _ReasonForUseCodeableConcept = value; OnPropertyChanged("ReasonForUseCodeableConcept"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonForUseCodeableConcept;
        
        /// <summary>
        /// Condition or observation that supports why the medication is being/was taken
        /// </summary>
        [FhirElement("reasonForUseReference", Order=200)]
        [References("Condition","Observation")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonForUseReference
        {
            get { if(_ReasonForUseReference==null) _ReasonForUseReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonForUseReference; }
            set { _ReasonForUseReference = value; OnPropertyChanged("ReasonForUseReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonForUseReference;
        
        /// <summary>
        /// Further information about the statement
        /// </summary>
        [FhirElement("note", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Type of medication usage
        /// </summary>
        [FhirElement("category", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Code CategoryElement
        {
            get { return _CategoryElement; }
            set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
        }
        
        private Hl7.Fhir.Model.Code _CategoryElement;
        
        /// <summary>
        /// Type of medication usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Category
        {
            get { return CategoryElement != null ? CategoryElement.Value : null; }
            set
            {
                if (value == null)
                  CategoryElement = null; 
                else
                  CategoryElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Category");
            }
        }
        
        /// <summary>
        /// Details of how medication was taken
        /// </summary>
        [FhirElement("dosage", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DosageInstruction> Dosage
        {
            get { if(_Dosage==null) _Dosage = new List<DosageInstruction>(); return _Dosage; }
            set { _Dosage = value; OnPropertyChanged("Dosage"); }
        }
        
        private List<DosageInstruction> _Dosage;
        

        public static ElementDefinition.ConstraintComponent MedicationStatement_MST_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "reasonNotTaken.empty() or notTaken='y'",
            Key = "mst-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Reason not taken is only permitted if notTaken is Yes",
            Xpath = "not(exists(f:reasonNotTaken) and (f:notTaken/@value='n' or f:notTaken/@value='unk'))"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(MedicationStatement_MST_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicationStatement;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.MedicationStatement.MedicationStatementStatus>)StatusElement.DeepCopy();
                if(Medication != null) dest.Medication = (Hl7.Fhir.Model.Element)Medication.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                if(InformationSource != null) dest.InformationSource = (Hl7.Fhir.Model.ResourceReference)InformationSource.DeepCopy();
                if(DerivedFrom != null) dest.DerivedFrom = new List<Hl7.Fhir.Model.ResourceReference>(DerivedFrom.DeepCopy());
                if(DateAssertedElement != null) dest.DateAssertedElement = (Hl7.Fhir.Model.FhirDateTime)DateAssertedElement.DeepCopy();
                if(NotTakenElement != null) dest.NotTakenElement = (Code<Hl7.Fhir.Model.MedicationStatement.MedicationStatementNotTaken>)NotTakenElement.DeepCopy();
                if(ReasonNotTaken != null) dest.ReasonNotTaken = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonNotTaken.DeepCopy());
                if(ReasonForUseCodeableConcept != null) dest.ReasonForUseCodeableConcept = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonForUseCodeableConcept.DeepCopy());
                if(ReasonForUseReference != null) dest.ReasonForUseReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonForUseReference.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(CategoryElement != null) dest.CategoryElement = (Hl7.Fhir.Model.Code)CategoryElement.DeepCopy();
                if(Dosage != null) dest.Dosage = new List<DosageInstruction>(Dosage.DeepCopy());
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
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
            if( !DeepComparable.Matches(InformationSource, otherT.InformationSource)) return false;
            if( !DeepComparable.Matches(DerivedFrom, otherT.DerivedFrom)) return false;
            if( !DeepComparable.Matches(DateAssertedElement, otherT.DateAssertedElement)) return false;
            if( !DeepComparable.Matches(NotTakenElement, otherT.NotTakenElement)) return false;
            if( !DeepComparable.Matches(ReasonNotTaken, otherT.ReasonNotTaken)) return false;
            if( !DeepComparable.Matches(ReasonForUseCodeableConcept, otherT.ReasonForUseCodeableConcept)) return false;
            if( !DeepComparable.Matches(ReasonForUseReference, otherT.ReasonForUseReference)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.Matches(Dosage, otherT.Dosage)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicationStatement;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
            if( !DeepComparable.IsExactly(InformationSource, otherT.InformationSource)) return false;
            if( !DeepComparable.IsExactly(DerivedFrom, otherT.DerivedFrom)) return false;
            if( !DeepComparable.IsExactly(DateAssertedElement, otherT.DateAssertedElement)) return false;
            if( !DeepComparable.IsExactly(NotTakenElement, otherT.NotTakenElement)) return false;
            if( !DeepComparable.IsExactly(ReasonNotTaken, otherT.ReasonNotTaken)) return false;
            if( !DeepComparable.IsExactly(ReasonForUseCodeableConcept, otherT.ReasonForUseCodeableConcept)) return false;
            if( !DeepComparable.IsExactly(ReasonForUseReference, otherT.ReasonForUseReference)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.IsExactly(Dosage, otherT.Dosage)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
				// MedicationStatement elements
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (Medication != null) yield return Medication;
				if (Subject != null) yield return Subject;
				if (Effective != null) yield return Effective;
				if (InformationSource != null) yield return InformationSource;
				foreach (var elem in DerivedFrom) { if (elem != null) yield return elem; }
				if (DateAssertedElement != null) yield return DateAssertedElement;
				if (NotTakenElement != null) yield return NotTakenElement;
				foreach (var elem in ReasonNotTaken) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonForUseCodeableConcept) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonForUseReference) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
				if (CategoryElement != null) yield return CategoryElement;
				foreach (var elem in Dosage) { if (elem != null) yield return elem; }
            }
        }
    }
    
}
