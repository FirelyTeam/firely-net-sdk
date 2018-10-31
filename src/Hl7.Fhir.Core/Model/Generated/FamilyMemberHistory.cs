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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information about patient's relatives, relevant for patient
    /// </summary>
    [FhirType("FamilyMemberHistory", IsResource=true)]
    [DataContract]
    public partial class FamilyMemberHistory : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.FamilyMemberHistory; } }
        [NotMapped]
        public override string TypeName { get { return "FamilyMemberHistory"; } }
        
        /// <summary>
        /// A code that identifies the status of the family history record.
        /// (url: http://hl7.org/fhir/ValueSet/history-status)
        /// </summary>
        [FhirEnumeration("FamilyHistoryStatus")]
        public enum FamilyHistoryStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/history-status)
            /// </summary>
            [EnumLiteral("partial", "http://hl7.org/fhir/history-status"), Description("Partial")]
            Partial,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/history-status)
            /// </summary>
            [EnumLiteral("completed", "http://hl7.org/fhir/history-status"), Description("Completed")]
            Completed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/history-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/history-status"), Description("Entered in error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/history-status)
            /// </summary>
            [EnumLiteral("health-unknown", "http://hl7.org/fhir/history-status"), Description("Health unknown")]
            HealthUnknown,
        }

        [FhirType("ConditionComponent")]
        [DataContract]
        public partial class ConditionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ConditionComponent"; } }
            
            /// <summary>
            /// Condition suffered by relation
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// deceased | permanent disability | etc.
            /// </summary>
            [FhirElement("outcome", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Outcome
            {
                get { return _Outcome; }
                set { _Outcome = value; OnPropertyChanged("Outcome"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Outcome;
            
            /// <summary>
            /// When condition first manifested
            /// </summary>
            [FhirElement("onset", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Age),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element Onset
            {
                get { return _Onset; }
                set { _Onset = value; OnPropertyChanged("Onset"); }
            }
            
            private Hl7.Fhir.Model.Element _Onset;
            
            /// <summary>
            /// Extra information about condition
            /// </summary>
            [FhirElement("note", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Annotation> Note
            {
                get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
                set { _Note = value; OnPropertyChanged("Note"); }
            }
            
            private List<Hl7.Fhir.Model.Annotation> _Note;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConditionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                    if(Onset != null) dest.Onset = (Hl7.Fhir.Model.Element)Onset.DeepCopy();
                    if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConditionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConditionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
                if( !DeepComparable.Matches(Onset, otherT.Onset)) return false;
                if( !DeepComparable.Matches(Note, otherT.Note)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConditionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
                if( !DeepComparable.IsExactly(Onset, otherT.Onset)) return false;
                if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (Outcome != null) yield return Outcome;
                    if (Onset != null) yield return Onset;
                    foreach (var elem in Note) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Outcome != null) yield return new ElementValue("outcome", Outcome);
                    if (Onset != null) yield return new ElementValue("onset", Onset);
                    foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                }
            }

            
        }
        
        
        /// <summary>
        /// External Id(s) for this record
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
        /// Instantiates protocol or definition
        /// </summary>
        [FhirElement("definition", InSummary=true, Order=100)]
        [CLSCompliant(false)]
		[References("PlanDefinition","Questionnaire")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Definition
        {
            get { if(_Definition==null) _Definition = new List<Hl7.Fhir.Model.ResourceReference>(); return _Definition; }
            set { _Definition = value; OnPropertyChanged("Definition"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Definition;
        
        /// <summary>
        /// partial | completed | entered-in-error | health-unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.FamilyMemberHistory.FamilyHistoryStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.FamilyMemberHistory.FamilyHistoryStatus> _StatusElement;
        
        /// <summary>
        /// partial | completed | entered-in-error | health-unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.FamilyMemberHistory.FamilyHistoryStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.FamilyMemberHistory.FamilyHistoryStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// The taking of a family member's history did not occur
        /// </summary>
        [FhirElement("notDone", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean NotDoneElement
        {
            get { return _NotDoneElement; }
            set { _NotDoneElement = value; OnPropertyChanged("NotDoneElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _NotDoneElement;
        
        /// <summary>
        /// The taking of a family member's history did not occur
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? NotDone
        {
            get { return NotDoneElement != null ? NotDoneElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  NotDoneElement = null; 
                else
                  NotDoneElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("NotDone");
            }
        }
        
        /// <summary>
        /// subject-unknown | withheld | unable-to-obtain | deferred
        /// </summary>
        [FhirElement("notDoneReason", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept NotDoneReason
        {
            get { return _NotDoneReason; }
            set { _NotDoneReason = value; OnPropertyChanged("NotDoneReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _NotDoneReason;
        
        /// <summary>
        /// Patient history is about
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// When history was captured/updated
        /// </summary>
        [FhirElement("date", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// When history was captured/updated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if (value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// The family member described
        /// </summary>
        [FhirElement("name", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// The family member described
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if (value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Relationship to the subject
        /// </summary>
        [FhirElement("relationship", InSummary=true, Order=170)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Relationship
        {
            get { return _Relationship; }
            set { _Relationship = value; OnPropertyChanged("Relationship"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Relationship;
        
        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        [FhirElement("gender", InSummary=true, Order=180)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AdministrativeGender> GenderElement
        {
            get { return _GenderElement; }
            set { _GenderElement = value; OnPropertyChanged("GenderElement"); }
        }
        
        private Code<Hl7.Fhir.Model.AdministrativeGender> _GenderElement;
        
        /// <summary>
        /// male | female | other | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AdministrativeGender? Gender
        {
            get { return GenderElement != null ? GenderElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  GenderElement = null; 
                else
                  GenderElement = new Code<Hl7.Fhir.Model.AdministrativeGender>(value);
                OnPropertyChanged("Gender");
            }
        }
        
        /// <summary>
        /// (approximate) date of birth
        /// </summary>
        [FhirElement("born", Order=190, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Born
        {
            get { return _Born; }
            set { _Born = value; OnPropertyChanged("Born"); }
        }
        
        private Hl7.Fhir.Model.Element _Born;
        
        /// <summary>
        /// (approximate) age
        /// </summary>
        [FhirElement("age", InSummary=true, Order=200, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Age),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Age
        {
            get { return _Age; }
            set { _Age = value; OnPropertyChanged("Age"); }
        }
        
        private Hl7.Fhir.Model.Element _Age;
        
        /// <summary>
        /// Age is estimated?
        /// </summary>
        [FhirElement("estimatedAge", InSummary=true, Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean EstimatedAgeElement
        {
            get { return _EstimatedAgeElement; }
            set { _EstimatedAgeElement = value; OnPropertyChanged("EstimatedAgeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _EstimatedAgeElement;
        
        /// <summary>
        /// Age is estimated?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? EstimatedAge
        {
            get { return EstimatedAgeElement != null ? EstimatedAgeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  EstimatedAgeElement = null; 
                else
                  EstimatedAgeElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("EstimatedAge");
            }
        }
        
        /// <summary>
        /// Dead? How old/when?
        /// </summary>
        [FhirElement("deceased", InSummary=true, Order=220, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Age),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Deceased
        {
            get { return _Deceased; }
            set { _Deceased = value; OnPropertyChanged("Deceased"); }
        }
        
        private Hl7.Fhir.Model.Element _Deceased;
        
        /// <summary>
        /// Why was family member history performed?
        /// </summary>
        [FhirElement("reasonCode", InSummary=true, Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Why was family member history performed?
        /// </summary>
        [FhirElement("reasonReference", InSummary=true, Order=240)]
        [CLSCompliant(false)]
		[References("Condition","Observation","AllergyIntolerance","QuestionnaireResponse")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// General note about related person
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
        
        /// <summary>
        /// Condition that the related person had
        /// </summary>
        [FhirElement("condition", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FamilyMemberHistory.ConditionComponent> Condition
        {
            get { if(_Condition==null) _Condition = new List<Hl7.Fhir.Model.FamilyMemberHistory.ConditionComponent>(); return _Condition; }
            set { _Condition = value; OnPropertyChanged("Condition"); }
        }
        
        private List<Hl7.Fhir.Model.FamilyMemberHistory.ConditionComponent> _Condition;
        

        public static ElementDefinition.ConstraintComponent FamilyMemberHistory_FHS_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "age.exists() or estimatedAge.empty()",
            Key = "fhs-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Can only have estimatedAge if age[x] is present",
            Xpath = "exists(*[starts-with(local-name(.), 'age')]) or not(exists(f:estimatedAge))"
        };

        public static ElementDefinition.ConstraintComponent FamilyMemberHistory_FHS_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "notDone or notDoneReason.exists().not()",
            Key = "fhs-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Not Done Reason can only be specified if NotDone is \"true\"",
            Xpath = "f:notDone/@value=true() or not(exists(f:notDoneReason))"
        };

        public static ElementDefinition.ConstraintComponent FamilyMemberHistory_FHS_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "age.empty() or born.empty()",
            Key = "fhs-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Can have age[x] or born[x], but not both",
            Xpath = "not (*[starts-with(local-name(.), 'age')] and *[starts-with(local-name(.), 'birth')])"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(FamilyMemberHistory_FHS_2);
            InvariantConstraints.Add(FamilyMemberHistory_FHS_3);
            InvariantConstraints.Add(FamilyMemberHistory_FHS_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as FamilyMemberHistory;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Definition != null) dest.Definition = new List<Hl7.Fhir.Model.ResourceReference>(Definition.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FamilyMemberHistory.FamilyHistoryStatus>)StatusElement.DeepCopy();
                if(NotDoneElement != null) dest.NotDoneElement = (Hl7.Fhir.Model.FhirBoolean)NotDoneElement.DeepCopy();
                if(NotDoneReason != null) dest.NotDoneReason = (Hl7.Fhir.Model.CodeableConcept)NotDoneReason.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.CodeableConcept)Relationship.DeepCopy();
                if(GenderElement != null) dest.GenderElement = (Code<Hl7.Fhir.Model.AdministrativeGender>)GenderElement.DeepCopy();
                if(Born != null) dest.Born = (Hl7.Fhir.Model.Element)Born.DeepCopy();
                if(Age != null) dest.Age = (Hl7.Fhir.Model.Element)Age.DeepCopy();
                if(EstimatedAgeElement != null) dest.EstimatedAgeElement = (Hl7.Fhir.Model.FhirBoolean)EstimatedAgeElement.DeepCopy();
                if(Deceased != null) dest.Deceased = (Hl7.Fhir.Model.Element)Deceased.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Condition != null) dest.Condition = new List<Hl7.Fhir.Model.FamilyMemberHistory.ConditionComponent>(Condition.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new FamilyMemberHistory());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as FamilyMemberHistory;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(NotDoneElement, otherT.NotDoneElement)) return false;
            if( !DeepComparable.Matches(NotDoneReason, otherT.NotDoneReason)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
            if( !DeepComparable.Matches(GenderElement, otherT.GenderElement)) return false;
            if( !DeepComparable.Matches(Born, otherT.Born)) return false;
            if( !DeepComparable.Matches(Age, otherT.Age)) return false;
            if( !DeepComparable.Matches(EstimatedAgeElement, otherT.EstimatedAgeElement)) return false;
            if( !DeepComparable.Matches(Deceased, otherT.Deceased)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Condition, otherT.Condition)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as FamilyMemberHistory;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(NotDoneElement, otherT.NotDoneElement)) return false;
            if( !DeepComparable.IsExactly(NotDoneReason, otherT.NotDoneReason)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
            if( !DeepComparable.IsExactly(GenderElement, otherT.GenderElement)) return false;
            if( !DeepComparable.IsExactly(Born, otherT.Born)) return false;
            if( !DeepComparable.IsExactly(Age, otherT.Age)) return false;
            if( !DeepComparable.IsExactly(EstimatedAgeElement, otherT.EstimatedAgeElement)) return false;
            if( !DeepComparable.IsExactly(Deceased, otherT.Deceased)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Condition, otherT.Condition)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				foreach (var elem in Definition) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (NotDoneElement != null) yield return NotDoneElement;
				if (NotDoneReason != null) yield return NotDoneReason;
				if (Patient != null) yield return Patient;
				if (DateElement != null) yield return DateElement;
				if (NameElement != null) yield return NameElement;
				if (Relationship != null) yield return Relationship;
				if (GenderElement != null) yield return GenderElement;
				if (Born != null) yield return Born;
				if (Age != null) yield return Age;
				if (EstimatedAgeElement != null) yield return EstimatedAgeElement;
				if (Deceased != null) yield return Deceased;
				foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
				foreach (var elem in Condition) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in Definition) { if (elem != null) yield return new ElementValue("definition", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (NotDoneElement != null) yield return new ElementValue("notDone", NotDoneElement);
                if (NotDoneReason != null) yield return new ElementValue("notDoneReason", NotDoneReason);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (Relationship != null) yield return new ElementValue("relationship", Relationship);
                if (GenderElement != null) yield return new ElementValue("gender", GenderElement);
                if (Born != null) yield return new ElementValue("born", Born);
                if (Age != null) yield return new ElementValue("age", Age);
                if (EstimatedAgeElement != null) yield return new ElementValue("estimatedAge", EstimatedAgeElement);
                if (Deceased != null) yield return new ElementValue("deceased", Deceased);
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in Condition) { if (elem != null) yield return new ElementValue("condition", elem); }
            }
        }

    }
    
}
