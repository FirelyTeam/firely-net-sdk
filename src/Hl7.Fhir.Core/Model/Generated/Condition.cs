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
    /// Detailed information about conditions, problems or diagnoses
    /// </summary>
    [FhirType("Condition", IsResource=true)]
    [DataContract]
    public partial class Condition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Condition; } }
        [NotMapped]
        public override string TypeName { get { return "Condition"; } }
        
        [FhirType("StageComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class StageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StageComponent"; } }
            
            /// <summary>
            /// Simple summary (disease specific)
            /// </summary>
            [FhirElement("summary", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Summary
            {
                get { return _Summary; }
                set { _Summary = value; OnPropertyChanged("Summary"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Summary;
            
            /// <summary>
            /// Formal record of assessment
            /// </summary>
            [FhirElement("assessment", Order=50)]
            [CLSCompliant(false)]
			[References("ClinicalImpression","DiagnosticReport","Observation")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Assessment
            {
                get { if(_Assessment==null) _Assessment = new List<Hl7.Fhir.Model.ResourceReference>(); return _Assessment; }
                set { _Assessment = value; OnPropertyChanged("Assessment"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Assessment;
            
            /// <summary>
            /// Kind of staging
            /// </summary>
            [FhirElement("type", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Summary != null) dest.Summary = (Hl7.Fhir.Model.CodeableConcept)Summary.DeepCopy();
                    if(Assessment != null) dest.Assessment = new List<Hl7.Fhir.Model.ResourceReference>(Assessment.DeepCopy());
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new StageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Summary, otherT.Summary)) return false;
                if( !DeepComparable.Matches(Assessment, otherT.Assessment)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Summary, otherT.Summary)) return false;
                if( !DeepComparable.IsExactly(Assessment, otherT.Assessment)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Summary != null) yield return Summary;
                    foreach (var elem in Assessment) { if (elem != null) yield return elem; }
                    if (Type != null) yield return Type;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Summary != null) yield return new ElementValue("summary", Summary);
                    foreach (var elem in Assessment) { if (elem != null) yield return new ElementValue("assessment", elem); }
                    if (Type != null) yield return new ElementValue("type", Type);
                }
            }

            
        }
        
        
        [FhirType("EvidenceComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class EvidenceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "EvidenceComponent"; } }
            
            /// <summary>
            /// Manifestation/symptom
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Code
            {
                get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Code;
            
            /// <summary>
            /// Supporting information found elsewhere
            /// </summary>
            [FhirElement("detail", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References()]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Detail
            {
                get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.ResourceReference>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Detail;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EvidenceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = new List<Hl7.Fhir.Model.CodeableConcept>(Code.DeepCopy());
                    if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.ResourceReference>(Detail.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new EvidenceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EvidenceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EvidenceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Code) { if (elem != null) yield return elem; }
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }

            
        }
        
        
        /// <summary>
        /// External Ids for this condition
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
        /// active | recurrence | relapse | inactive | remission | resolved
        /// </summary>
        [FhirElement("clinicalStatus", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ClinicalStatus
        {
            get { return _ClinicalStatus; }
            set { _ClinicalStatus = value; OnPropertyChanged("ClinicalStatus"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ClinicalStatus;
        
        /// <summary>
        /// unconfirmed | provisional | differential | confirmed | refuted | entered-in-error
        /// </summary>
        [FhirElement("verificationStatus", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept VerificationStatus
        {
            get { return _VerificationStatus; }
            set { _VerificationStatus = value; OnPropertyChanged("VerificationStatus"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _VerificationStatus;
        
        /// <summary>
        /// problem-list-item | encounter-diagnosis
        /// </summary>
        [FhirElement("category", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// Subjective severity of condition
        /// </summary>
        [FhirElement("severity", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Severity
        {
            get { return _Severity; }
            set { _Severity = value; OnPropertyChanged("Severity"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Severity;
        
        /// <summary>
        /// Identification of the condition, problem or diagnosis
        /// </summary>
        [FhirElement("code", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// Anatomical location, if relevant
        /// </summary>
        [FhirElement("bodySite", InSummary=true, Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> BodySite
        {
            get { if(_BodySite==null) _BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(); return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _BodySite;
        
        /// <summary>
        /// Who has the condition?
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=160)]
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
        /// Encounter created as part of
        /// </summary>
        [FhirElement("encounter", InSummary=true, Order=170)]
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
        /// Estimated or actual date,  date-time, or age
        /// </summary>
        [FhirElement("onset", InSummary=true, Order=180, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Age),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Onset
        {
            get { return _Onset; }
            set { _Onset = value; OnPropertyChanged("Onset"); }
        }
        
        private Hl7.Fhir.Model.Element _Onset;
        
        /// <summary>
        /// When in resolution/remission
        /// </summary>
        [FhirElement("abatement", Order=190, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Age),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Abatement
        {
            get { return _Abatement; }
            set { _Abatement = value; OnPropertyChanged("Abatement"); }
        }
        
        private Hl7.Fhir.Model.Element _Abatement;
        
        /// <summary>
        /// Date record was first recorded
        /// </summary>
        [FhirElement("recordedDate", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime RecordedDateElement
        {
            get { return _RecordedDateElement; }
            set { _RecordedDateElement = value; OnPropertyChanged("RecordedDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _RecordedDateElement;
        
        /// <summary>
        /// Date record was first recorded
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RecordedDate
        {
            get { return RecordedDateElement != null ? RecordedDateElement.Value : null; }
            set
            {
                if (value == null)
                  RecordedDateElement = null; 
                else
                  RecordedDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("RecordedDate");
            }
        }
        
        /// <summary>
        /// Who recorded the condition
        /// </summary>
        [FhirElement("recorder", InSummary=true, Order=210)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole","Patient","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recorder
        {
            get { return _Recorder; }
            set { _Recorder = value; OnPropertyChanged("Recorder"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Recorder;
        
        /// <summary>
        /// Person who asserts this condition
        /// </summary>
        [FhirElement("asserter", InSummary=true, Order=220)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole","Patient","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Asserter
        {
            get { return _Asserter; }
            set { _Asserter = value; OnPropertyChanged("Asserter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Asserter;
        
        /// <summary>
        /// Stage/grade, usually assessed formally
        /// </summary>
        [FhirElement("stage", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Condition.StageComponent> Stage
        {
            get { if(_Stage==null) _Stage = new List<Hl7.Fhir.Model.Condition.StageComponent>(); return _Stage; }
            set { _Stage = value; OnPropertyChanged("Stage"); }
        }
        
        private List<Hl7.Fhir.Model.Condition.StageComponent> _Stage;
        
        /// <summary>
        /// Supporting evidence
        /// </summary>
        [FhirElement("evidence", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Condition.EvidenceComponent> Evidence
        {
            get { if(_Evidence==null) _Evidence = new List<Hl7.Fhir.Model.Condition.EvidenceComponent>(); return _Evidence; }
            set { _Evidence = value; OnPropertyChanged("Evidence"); }
        }
        
        private List<Hl7.Fhir.Model.Condition.EvidenceComponent> _Evidence;
        
        /// <summary>
        /// Additional information about the Condition
        /// </summary>
        [FhirElement("note", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        

        public static ElementDefinition.ConstraintComponent Condition_CON_5 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "verificationStatus.coding.where(system='http://terminology.hl7.org/CodeSystem/condition-ver-status' and code='entered-in-error').empty() or clinicalStatus.empty()",
            Key = "con-5",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Condition.clinicalStatus SHALL NOT be present if verification Status is entered-in-error",
            Xpath = "not(exists(f:verificationStatus/f:coding[f:system/@value='http://terminology.hl7.org/CodeSystem/condition-ver-status' and f:code/@value='entered-in-error'])) or not(exists(f:clinicalStatus))"
        };

        public static ElementDefinition.ConstraintComponent Condition_CON_4 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "abatement.empty() or clinicalStatus.coding.where(system='http://terminology.hl7.org/CodeSystem/condition-clinical' and (code='resolved' or code='remission' or code='inactive')).exists()",
            Key = "con-4",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If condition is abated, then clinicalStatus must be either inactive, resolved, or remission",
            Xpath = "not(exists(*[starts-with(local-name(.), 'abatement')])) or exists(f:clinicalStatus/f:coding[f:system/@value='http://terminology.hl7.org/CodeSystem/condition-clinical' and f:code/@value=('resolved', 'remission', 'inactive')])"
        };

        public static ElementDefinition.ConstraintComponent Condition_CON_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "clinicalStatus.exists() or verificationStatus='entered-in-error' or category.select($this='problem-list-item').empty()",
            Key = "con-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Condition.clinicalStatus SHALL be present if verificationStatus is not entered-in-error and category is problem-list-item",
            Xpath = "exists(f:clinicalStatus) or f:verificationStatus/@value='entered-in-error' or not(exists(category[@value='problem-list-item']))"
        };

        public static ElementDefinition.ConstraintComponent Condition_CON_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "stage.all(summary.exists() or assessment.exists())",
            Key = "con-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Stage SHALL have summary or assessment",
            Xpath = "exists(f:summary) or exists(f:assessment)"
        };

        public static ElementDefinition.ConstraintComponent Condition_CON_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "evidence.all(code.exists() or detail.exists())",
            Key = "con-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "evidence SHALL have code or details",
            Xpath = "exists(f:code) or exists(f:detail)"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(Condition_CON_5);
            InvariantConstraints.Add(Condition_CON_4);
            InvariantConstraints.Add(Condition_CON_3);
            InvariantConstraints.Add(Condition_CON_1);
            InvariantConstraints.Add(Condition_CON_2);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Condition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(ClinicalStatus != null) dest.ClinicalStatus = (Hl7.Fhir.Model.CodeableConcept)ClinicalStatus.DeepCopy();
                if(VerificationStatus != null) dest.VerificationStatus = (Hl7.Fhir.Model.CodeableConcept)VerificationStatus.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Severity != null) dest.Severity = (Hl7.Fhir.Model.CodeableConcept)Severity.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(BodySite.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Onset != null) dest.Onset = (Hl7.Fhir.Model.Element)Onset.DeepCopy();
                if(Abatement != null) dest.Abatement = (Hl7.Fhir.Model.Element)Abatement.DeepCopy();
                if(RecordedDateElement != null) dest.RecordedDateElement = (Hl7.Fhir.Model.FhirDateTime)RecordedDateElement.DeepCopy();
                if(Recorder != null) dest.Recorder = (Hl7.Fhir.Model.ResourceReference)Recorder.DeepCopy();
                if(Asserter != null) dest.Asserter = (Hl7.Fhir.Model.ResourceReference)Asserter.DeepCopy();
                if(Stage != null) dest.Stage = new List<Hl7.Fhir.Model.Condition.StageComponent>(Stage.DeepCopy());
                if(Evidence != null) dest.Evidence = new List<Hl7.Fhir.Model.Condition.EvidenceComponent>(Evidence.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Condition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Condition;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(ClinicalStatus, otherT.ClinicalStatus)) return false;
            if( !DeepComparable.Matches(VerificationStatus, otherT.VerificationStatus)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Severity, otherT.Severity)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Onset, otherT.Onset)) return false;
            if( !DeepComparable.Matches(Abatement, otherT.Abatement)) return false;
            if( !DeepComparable.Matches(RecordedDateElement, otherT.RecordedDateElement)) return false;
            if( !DeepComparable.Matches(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.Matches(Asserter, otherT.Asserter)) return false;
            if( !DeepComparable.Matches(Stage, otherT.Stage)) return false;
            if( !DeepComparable.Matches(Evidence, otherT.Evidence)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Condition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(ClinicalStatus, otherT.ClinicalStatus)) return false;
            if( !DeepComparable.IsExactly(VerificationStatus, otherT.VerificationStatus)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Severity, otherT.Severity)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Onset, otherT.Onset)) return false;
            if( !DeepComparable.IsExactly(Abatement, otherT.Abatement)) return false;
            if( !DeepComparable.IsExactly(RecordedDateElement, otherT.RecordedDateElement)) return false;
            if( !DeepComparable.IsExactly(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.IsExactly(Asserter, otherT.Asserter)) return false;
            if( !DeepComparable.IsExactly(Stage, otherT.Stage)) return false;
            if( !DeepComparable.IsExactly(Evidence, otherT.Evidence)) return false;
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
				if (ClinicalStatus != null) yield return ClinicalStatus;
				if (VerificationStatus != null) yield return VerificationStatus;
				foreach (var elem in Category) { if (elem != null) yield return elem; }
				if (Severity != null) yield return Severity;
				if (Code != null) yield return Code;
				foreach (var elem in BodySite) { if (elem != null) yield return elem; }
				if (Subject != null) yield return Subject;
				if (Encounter != null) yield return Encounter;
				if (Onset != null) yield return Onset;
				if (Abatement != null) yield return Abatement;
				if (RecordedDateElement != null) yield return RecordedDateElement;
				if (Recorder != null) yield return Recorder;
				if (Asserter != null) yield return Asserter;
				foreach (var elem in Stage) { if (elem != null) yield return elem; }
				foreach (var elem in Evidence) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (ClinicalStatus != null) yield return new ElementValue("clinicalStatus", ClinicalStatus);
                if (VerificationStatus != null) yield return new ElementValue("verificationStatus", VerificationStatus);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (Severity != null) yield return new ElementValue("severity", Severity);
                if (Code != null) yield return new ElementValue("code", Code);
                foreach (var elem in BodySite) { if (elem != null) yield return new ElementValue("bodySite", elem); }
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (Onset != null) yield return new ElementValue("onset", Onset);
                if (Abatement != null) yield return new ElementValue("abatement", Abatement);
                if (RecordedDateElement != null) yield return new ElementValue("recordedDate", RecordedDateElement);
                if (Recorder != null) yield return new ElementValue("recorder", Recorder);
                if (Asserter != null) yield return new ElementValue("asserter", Asserter);
                foreach (var elem in Stage) { if (elem != null) yield return new ElementValue("stage", elem); }
                foreach (var elem in Evidence) { if (elem != null) yield return new ElementValue("evidence", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
            }
        }

    }
    
}
