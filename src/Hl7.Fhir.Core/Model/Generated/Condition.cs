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
        
        /// <summary>
        /// The clinical status of the Condition or diagnosis
        /// </summary>
        [FhirEnumeration("ConditionClinicalStatus")]
        public enum ConditionClinicalStatus
        {
            /// <summary>
            /// This is a tentative diagnosis - still a candidate that is under consideration.
            /// </summary>
            [EnumLiteral("provisional")]
            Provisional,
            /// <summary>
            /// The patient is being treated on the basis that this is the condition, but it is still not confirmed.
            /// </summary>
            [EnumLiteral("working")]
            Working,
            /// <summary>
            /// There is sufficient diagnostic and/or clinical evidence to treat this as a confirmed condition.
            /// </summary>
            [EnumLiteral("confirmed")]
            Confirmed,
            /// <summary>
            /// This condition has been ruled out by diagnostic and clinical evidence.
            /// </summary>
            [EnumLiteral("refuted")]
            Refuted,
            /// <summary>
            /// The statement was entered in error and Is not valid.
            /// </summary>
            [EnumLiteral("entered-in-error")]
            EnteredInError,
            /// <summary>
            /// The condition status is unknown.  Note that "unknown" is a value of last resort and every attempt should be made to provide a meaningful value other than "unknown".
            /// </summary>
            [EnumLiteral("unknown")]
            Unknown,
        }
        
        [FhirType("ConditionEvidenceComponent")]
        [DataContract]
        public partial class ConditionEvidenceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConditionEvidenceComponent"; } }
            
            /// <summary>
            /// Manifestation/symptom
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Supporting information found elsewhere
            /// </summary>
            [FhirElement("detail", InSummary=true, Order=50)]
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
                var dest = other as ConditionEvidenceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.ResourceReference>(Detail.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConditionEvidenceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConditionEvidenceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConditionEvidenceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConditionDueToComponent")]
        [DataContract]
        public partial class ConditionDueToComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConditionDueToComponent"; } }
            
            /// <summary>
            /// Relationship target by means of a predefined code
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Relationship target resource
            /// </summary>
            [FhirElement("target", InSummary=true, Order=50)]
            [References("Condition","Procedure","MedicationAdministration","Immunization","MedicationStatement")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Target;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConditionDueToComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConditionDueToComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConditionDueToComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConditionDueToComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConditionOccurredFollowingComponent")]
        [DataContract]
        public partial class ConditionOccurredFollowingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConditionOccurredFollowingComponent"; } }
            
            /// <summary>
            /// Relationship target by means of a predefined code
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Relationship target resource
            /// </summary>
            [FhirElement("target", InSummary=true, Order=50)]
            [References("Condition","Procedure","MedicationAdministration","Immunization","MedicationStatement")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Target;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConditionOccurredFollowingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConditionOccurredFollowingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConditionOccurredFollowingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConditionOccurredFollowingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConditionStageComponent")]
        [DataContract]
        public partial class ConditionStageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConditionStageComponent"; } }
            
            /// <summary>
            /// Simple summary (disease specific)
            /// </summary>
            [FhirElement("summary", InSummary=true, Order=40)]
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
            [FhirElement("assessment", InSummary=true, Order=50)]
            [References("ClinicalImpression","DiagnosticReport","Observation")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Assessment
            {
                get { if(_Assessment==null) _Assessment = new List<Hl7.Fhir.Model.ResourceReference>(); return _Assessment; }
                set { _Assessment = value; OnPropertyChanged("Assessment"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Assessment;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConditionStageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Summary != null) dest.Summary = (Hl7.Fhir.Model.CodeableConcept)Summary.DeepCopy();
                    if(Assessment != null) dest.Assessment = new List<Hl7.Fhir.Model.ResourceReference>(Assessment.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConditionStageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConditionStageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Summary, otherT.Summary)) return false;
                if( !DeepComparable.Matches(Assessment, otherT.Assessment)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConditionStageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Summary, otherT.Summary)) return false;
                if( !DeepComparable.IsExactly(Assessment, otherT.Assessment)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConditionLocationComponent")]
        [DataContract]
        public partial class ConditionLocationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConditionLocationComponent"; } }
            
            /// <summary>
            /// Location - may include laterality
            /// </summary>
            [FhirElement("site", InSummary=true, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Site
            {
                get { return _Site; }
                set { _Site = value; OnPropertyChanged("Site"); }
            }
            
            private Hl7.Fhir.Model.Element _Site;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConditionLocationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Site != null) dest.Site = (Hl7.Fhir.Model.Element)Site.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConditionLocationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConditionLocationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Site, otherT.Site)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConditionLocationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External Ids for this condition
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
        /// Who has the condition?
        /// </summary>
        [FhirElement("patient", Order=100)]
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
        /// Encounter when condition first asserted
        /// </summary>
        [FhirElement("encounter", Order=110)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Person who asserts this condition
        /// </summary>
        [FhirElement("asserter", Order=120)]
        [References("Practitioner","Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Asserter
        {
            get { return _Asserter; }
            set { _Asserter = value; OnPropertyChanged("Asserter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Asserter;
        
        /// <summary>
        /// When first detected/suspected/entered
        /// </summary>
        [FhirElement("dateAsserted", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Date DateAssertedElement
        {
            get { return _DateAssertedElement; }
            set { _DateAssertedElement = value; OnPropertyChanged("DateAssertedElement"); }
        }
        
        private Hl7.Fhir.Model.Date _DateAssertedElement;
        
        /// <summary>
        /// When first detected/suspected/entered
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
                  DateAssertedElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("DateAsserted");
            }
        }
        
        /// <summary>
        /// Identification of the condition, problem or diagnosis
        /// </summary>
        [FhirElement("code", Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// E.g. complaint | symptom | finding | diagnosis
        /// </summary>
        [FhirElement("category", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// provisional | working | confirmed | refuted | entered-in-error | unknown
        /// </summary>
        [FhirElement("clinicalStatus", Order=160)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Condition.ConditionClinicalStatus> ClinicalStatusElement
        {
            get { return _ClinicalStatusElement; }
            set { _ClinicalStatusElement = value; OnPropertyChanged("ClinicalStatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Condition.ConditionClinicalStatus> _ClinicalStatusElement;
        
        /// <summary>
        /// provisional | working | confirmed | refuted | entered-in-error | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Condition.ConditionClinicalStatus? ClinicalStatus
        {
            get { return ClinicalStatusElement != null ? ClinicalStatusElement.Value : null; }
            set
            {
                if(value == null)
                  ClinicalStatusElement = null; 
                else
                  ClinicalStatusElement = new Code<Hl7.Fhir.Model.Condition.ConditionClinicalStatus>(value);
                OnPropertyChanged("ClinicalStatus");
            }
        }
        
        /// <summary>
        /// Subjective severity of condition
        /// </summary>
        [FhirElement("severity", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Severity
        {
            get { return _Severity; }
            set { _Severity = value; OnPropertyChanged("Severity"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Severity;
        
        /// <summary>
        /// Estimated or actual date,  date-time, or age
        /// </summary>
        [FhirElement("onset", Order=180, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Age),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Onset
        {
            get { return _Onset; }
            set { _Onset = value; OnPropertyChanged("Onset"); }
        }
        
        private Hl7.Fhir.Model.Element _Onset;
        
        /// <summary>
        /// If/when in resolution/remission
        /// </summary>
        [FhirElement("abatement", Order=190, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Age),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Abatement
        {
            get { return _Abatement; }
            set { _Abatement = value; OnPropertyChanged("Abatement"); }
        }
        
        private Hl7.Fhir.Model.Element _Abatement;
        
        /// <summary>
        /// Stage/grade, usually assessed formally
        /// </summary>
        [FhirElement("stage", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.Condition.ConditionStageComponent Stage
        {
            get { return _Stage; }
            set { _Stage = value; OnPropertyChanged("Stage"); }
        }
        
        private Hl7.Fhir.Model.Condition.ConditionStageComponent _Stage;
        
        /// <summary>
        /// Supporting evidence
        /// </summary>
        [FhirElement("evidence", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Condition.ConditionEvidenceComponent> Evidence
        {
            get { if(_Evidence==null) _Evidence = new List<Hl7.Fhir.Model.Condition.ConditionEvidenceComponent>(); return _Evidence; }
            set { _Evidence = value; OnPropertyChanged("Evidence"); }
        }
        
        private List<Hl7.Fhir.Model.Condition.ConditionEvidenceComponent> _Evidence;
        
        /// <summary>
        /// Anatomical location, if relevant
        /// </summary>
        [FhirElement("location", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Condition.ConditionLocationComponent> Location
        {
            get { if(_Location==null) _Location = new List<Hl7.Fhir.Model.Condition.ConditionLocationComponent>(); return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        
        private List<Hl7.Fhir.Model.Condition.ConditionLocationComponent> _Location;
        
        /// <summary>
        /// Causes for this Condition
        /// </summary>
        [FhirElement("dueTo", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Condition.ConditionDueToComponent> DueTo
        {
            get { if(_DueTo==null) _DueTo = new List<Hl7.Fhir.Model.Condition.ConditionDueToComponent>(); return _DueTo; }
            set { _DueTo = value; OnPropertyChanged("DueTo"); }
        }
        
        private List<Hl7.Fhir.Model.Condition.ConditionDueToComponent> _DueTo;
        
        /// <summary>
        /// Precedent for this Condition
        /// </summary>
        [FhirElement("occurredFollowing", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Condition.ConditionOccurredFollowingComponent> OccurredFollowing
        {
            get { if(_OccurredFollowing==null) _OccurredFollowing = new List<Hl7.Fhir.Model.Condition.ConditionOccurredFollowingComponent>(); return _OccurredFollowing; }
            set { _OccurredFollowing = value; OnPropertyChanged("OccurredFollowing"); }
        }
        
        private List<Hl7.Fhir.Model.Condition.ConditionOccurredFollowingComponent> _OccurredFollowing;
        
        /// <summary>
        /// Additional information about the Condition
        /// </summary>
        [FhirElement("notes", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NotesElement
        {
            get { return _NotesElement; }
            set { _NotesElement = value; OnPropertyChanged("NotesElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NotesElement;
        
        /// <summary>
        /// Additional information about the Condition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Notes
        {
            get { return NotesElement != null ? NotesElement.Value : null; }
            set
            {
                if(value == null)
                  NotesElement = null; 
                else
                  NotesElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Notes");
            }
        }
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Condition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Asserter != null) dest.Asserter = (Hl7.Fhir.Model.ResourceReference)Asserter.DeepCopy();
                if(DateAssertedElement != null) dest.DateAssertedElement = (Hl7.Fhir.Model.Date)DateAssertedElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(ClinicalStatusElement != null) dest.ClinicalStatusElement = (Code<Hl7.Fhir.Model.Condition.ConditionClinicalStatus>)ClinicalStatusElement.DeepCopy();
                if(Severity != null) dest.Severity = (Hl7.Fhir.Model.CodeableConcept)Severity.DeepCopy();
                if(Onset != null) dest.Onset = (Hl7.Fhir.Model.Element)Onset.DeepCopy();
                if(Abatement != null) dest.Abatement = (Hl7.Fhir.Model.Element)Abatement.DeepCopy();
                if(Stage != null) dest.Stage = (Hl7.Fhir.Model.Condition.ConditionStageComponent)Stage.DeepCopy();
                if(Evidence != null) dest.Evidence = new List<Hl7.Fhir.Model.Condition.ConditionEvidenceComponent>(Evidence.DeepCopy());
                if(Location != null) dest.Location = new List<Hl7.Fhir.Model.Condition.ConditionLocationComponent>(Location.DeepCopy());
                if(DueTo != null) dest.DueTo = new List<Hl7.Fhir.Model.Condition.ConditionDueToComponent>(DueTo.DeepCopy());
                if(OccurredFollowing != null) dest.OccurredFollowing = new List<Hl7.Fhir.Model.Condition.ConditionOccurredFollowingComponent>(OccurredFollowing.DeepCopy());
                if(NotesElement != null) dest.NotesElement = (Hl7.Fhir.Model.FhirString)NotesElement.DeepCopy();
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
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Asserter, otherT.Asserter)) return false;
            if( !DeepComparable.Matches(DateAssertedElement, otherT.DateAssertedElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(ClinicalStatusElement, otherT.ClinicalStatusElement)) return false;
            if( !DeepComparable.Matches(Severity, otherT.Severity)) return false;
            if( !DeepComparable.Matches(Onset, otherT.Onset)) return false;
            if( !DeepComparable.Matches(Abatement, otherT.Abatement)) return false;
            if( !DeepComparable.Matches(Stage, otherT.Stage)) return false;
            if( !DeepComparable.Matches(Evidence, otherT.Evidence)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(DueTo, otherT.DueTo)) return false;
            if( !DeepComparable.Matches(OccurredFollowing, otherT.OccurredFollowing)) return false;
            if( !DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Condition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Asserter, otherT.Asserter)) return false;
            if( !DeepComparable.IsExactly(DateAssertedElement, otherT.DateAssertedElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(ClinicalStatusElement, otherT.ClinicalStatusElement)) return false;
            if( !DeepComparable.IsExactly(Severity, otherT.Severity)) return false;
            if( !DeepComparable.IsExactly(Onset, otherT.Onset)) return false;
            if( !DeepComparable.IsExactly(Abatement, otherT.Abatement)) return false;
            if( !DeepComparable.IsExactly(Stage, otherT.Stage)) return false;
            if( !DeepComparable.IsExactly(Evidence, otherT.Evidence)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(DueTo, otherT.DueTo)) return false;
            if( !DeepComparable.IsExactly(OccurredFollowing, otherT.OccurredFollowing)) return false;
            if( !DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;
            
            return true;
        }
        
    }
    
}
