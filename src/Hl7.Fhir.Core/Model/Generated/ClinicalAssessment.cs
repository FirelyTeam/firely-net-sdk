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
// Generated on Mon, Dec 22, 2014 15:52+0100 for FHIR v0.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A clinical assessment performed when planning treatments and management strategies for a patient
    /// </summary>
    [FhirType("ClinicalAssessment", IsResource=true)]
    [DataContract]
    public partial class ClinicalAssessment : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ClinicalAssessment; } }
        [NotMapped]
        public override string TypeName { get { return "ClinicalAssessment"; } }
        
        [FhirType("ClinicalAssessmentRuledOutComponent")]
        [DataContract]
        public partial class ClinicalAssessmentRuledOutComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ClinicalAssessmentRuledOutComponent"; } }
            
            /// <summary>
            /// Specific text of code for diagnosis
            /// </summary>
            [FhirElement("item", InSummary=true, Order=20)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Item;
            
            /// <summary>
            /// Grounds for elimination
            /// </summary>
            [FhirElement("reason", InSummary=true, Order=30)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ReasonElement
            {
                get { return _ReasonElement; }
                set { _ReasonElement = value; OnPropertyChanged("ReasonElement"); }
            }
            private Hl7.Fhir.Model.FhirString _ReasonElement;
            
            /// <summary>
            /// Grounds for elimination
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Reason
            {
                get { return ReasonElement != null ? ReasonElement.Value : null; }
                set
                {
                    if(value == null)
                      ReasonElement = null; 
                    else
                      ReasonElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Reason");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ClinicalAssessmentRuledOutComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Item != null) dest.Item = (Hl7.Fhir.Model.CodeableConcept)Item.DeepCopy();
                    if(ReasonElement != null) dest.ReasonElement = (Hl7.Fhir.Model.FhirString)ReasonElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ClinicalAssessmentRuledOutComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ClinicalAssessmentRuledOutComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
                if( !DeepComparable.Matches(ReasonElement, otherT.ReasonElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ClinicalAssessmentRuledOutComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
                if( !DeepComparable.IsExactly(ReasonElement, otherT.ReasonElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ClinicalAssessmentInvestigationsComponent")]
        [DataContract]
        public partial class ClinicalAssessmentInvestigationsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ClinicalAssessmentInvestigationsComponent"; } }
            
            /// <summary>
            /// A name/code for the set
            /// </summary>
            [FhirElement("code", InSummary=true, Order=20)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Record of a specific investigation
            /// </summary>
            [FhirElement("item", InSummary=true, Order=30)]
            [References("Observation","QuestionnaireAnswers","FamilyHistory","DiagnosticReport")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Item
            {
                get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.ResourceReference>(); return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _Item;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ClinicalAssessmentInvestigationsComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Item != null) dest.Item = new List<Hl7.Fhir.Model.ResourceReference>(Item.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ClinicalAssessmentInvestigationsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ClinicalAssessmentInvestigationsComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ClinicalAssessmentInvestigationsComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ClinicalAssessmentDiagnosisComponent")]
        [DataContract]
        public partial class ClinicalAssessmentDiagnosisComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ClinicalAssessmentDiagnosisComponent"; } }
            
            /// <summary>
            /// Specific text or code for diagnosis
            /// </summary>
            [FhirElement("item", InSummary=true, Order=20)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Item;
            
            /// <summary>
            /// Which investigations support diagnosis
            /// </summary>
            [FhirElement("cause", InSummary=true, Order=30)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CauseElement
            {
                get { return _CauseElement; }
                set { _CauseElement = value; OnPropertyChanged("CauseElement"); }
            }
            private Hl7.Fhir.Model.FhirString _CauseElement;
            
            /// <summary>
            /// Which investigations support diagnosis
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Cause
            {
                get { return CauseElement != null ? CauseElement.Value : null; }
                set
                {
                    if(value == null)
                      CauseElement = null; 
                    else
                      CauseElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Cause");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ClinicalAssessmentDiagnosisComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Item != null) dest.Item = (Hl7.Fhir.Model.CodeableConcept)Item.DeepCopy();
                    if(CauseElement != null) dest.CauseElement = (Hl7.Fhir.Model.FhirString)CauseElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ClinicalAssessmentDiagnosisComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ClinicalAssessmentDiagnosisComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
                if( !DeepComparable.Matches(CauseElement, otherT.CauseElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ClinicalAssessmentDiagnosisComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
                if( !DeepComparable.IsExactly(CauseElement, otherT.CauseElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// The patient being asssesed
        /// </summary>
        [FhirElement("patient", Order=50)]
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
        /// The clinicial performing the assessment
        /// </summary>
        [FhirElement("assessor", Order=60)]
        [References("Practitioner")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Assessor
        {
            get { return _Assessor; }
            set { _Assessor = value; OnPropertyChanged("Assessor"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Assessor;
        
        /// <summary>
        /// When the assessment occurred
        /// </summary>
        [FhirElement("date", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// When the assessment occurred
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if(value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Why/how the assessment was performed
        /// </summary>
        [FhirElement("description", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Why/how the assessment was performed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if(value == null)
                  DescriptionElement = null; 
                else
                  DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// Reference to last assessment
        /// </summary>
        [FhirElement("previous", Order=90)]
        [References("ClinicalAssessment")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Previous
        {
            get { return _Previous; }
            set { _Previous = value; OnPropertyChanged("Previous"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Previous;
        
        /// <summary>
        /// General assessment of patient state
        /// </summary>
        [FhirElement("problem", Order=100)]
        [References("Condition","AllergyIntolerance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Problem
        {
            get { if(_Problem==null) _Problem = new List<Hl7.Fhir.Model.ResourceReference>(); return _Problem; }
            set { _Problem = value; OnPropertyChanged("Problem"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Problem;
        
        /// <summary>
        /// A specific careplan that prompted this assessment
        /// </summary>
        [FhirElement("careplan", Order=110)]
        [References("CarePlan")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Careplan
        {
            get { return _Careplan; }
            set { _Careplan = value; OnPropertyChanged("Careplan"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Careplan;
        
        /// <summary>
        /// A specific referral that lead to this assessment
        /// </summary>
        [FhirElement("referral", Order=120)]
        [References("ReferralRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Referral
        {
            get { return _Referral; }
            set { _Referral = value; OnPropertyChanged("Referral"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Referral;
        
        /// <summary>
        /// One or more sets of investigations (signs, symptions, etc)
        /// </summary>
        [FhirElement("investigations", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ClinicalAssessment.ClinicalAssessmentInvestigationsComponent> Investigations
        {
            get { if(_Investigations==null) _Investigations = new List<Hl7.Fhir.Model.ClinicalAssessment.ClinicalAssessmentInvestigationsComponent>(); return _Investigations; }
            set { _Investigations = value; OnPropertyChanged("Investigations"); }
        }
        private List<Hl7.Fhir.Model.ClinicalAssessment.ClinicalAssessmentInvestigationsComponent> _Investigations;
        
        /// <summary>
        /// Clinical Protocol followed
        /// </summary>
        [FhirElement("protocol", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri ProtocolElement
        {
            get { return _ProtocolElement; }
            set { _ProtocolElement = value; OnPropertyChanged("ProtocolElement"); }
        }
        private Hl7.Fhir.Model.FhirUri _ProtocolElement;
        
        /// <summary>
        /// Clinical Protocol followed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Protocol
        {
            get { return ProtocolElement != null ? ProtocolElement.Value : null; }
            set
            {
                if(value == null)
                  ProtocolElement = null; 
                else
                  ProtocolElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Protocol");
            }
        }
        
        /// <summary>
        /// Summary of the assessment
        /// </summary>
        [FhirElement("summary", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SummaryElement
        {
            get { return _SummaryElement; }
            set { _SummaryElement = value; OnPropertyChanged("SummaryElement"); }
        }
        private Hl7.Fhir.Model.FhirString _SummaryElement;
        
        /// <summary>
        /// Summary of the assessment
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Summary
        {
            get { return SummaryElement != null ? SummaryElement.Value : null; }
            set
            {
                if(value == null)
                  SummaryElement = null; 
                else
                  SummaryElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Summary");
            }
        }
        
        /// <summary>
        /// Possible or likely diagnosis
        /// </summary>
        [FhirElement("diagnosis", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ClinicalAssessment.ClinicalAssessmentDiagnosisComponent> Diagnosis
        {
            get { if(_Diagnosis==null) _Diagnosis = new List<Hl7.Fhir.Model.ClinicalAssessment.ClinicalAssessmentDiagnosisComponent>(); return _Diagnosis; }
            set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
        }
        private List<Hl7.Fhir.Model.ClinicalAssessment.ClinicalAssessmentDiagnosisComponent> _Diagnosis;
        
        /// <summary>
        /// Diagnosies/conditions resolved since previous assessment
        /// </summary>
        [FhirElement("resolved", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Resolved
        {
            get { if(_Resolved==null) _Resolved = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Resolved; }
            set { _Resolved = value; OnPropertyChanged("Resolved"); }
        }
        private List<Hl7.Fhir.Model.CodeableConcept> _Resolved;
        
        /// <summary>
        /// Diagnosis considered not possible
        /// </summary>
        [FhirElement("ruledOut", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ClinicalAssessment.ClinicalAssessmentRuledOutComponent> RuledOut
        {
            get { if(_RuledOut==null) _RuledOut = new List<Hl7.Fhir.Model.ClinicalAssessment.ClinicalAssessmentRuledOutComponent>(); return _RuledOut; }
            set { _RuledOut = value; OnPropertyChanged("RuledOut"); }
        }
        private List<Hl7.Fhir.Model.ClinicalAssessment.ClinicalAssessmentRuledOutComponent> _RuledOut;
        
        /// <summary>
        /// Estimate of likely outcome
        /// </summary>
        [FhirElement("prognosis", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PrognosisElement
        {
            get { return _PrognosisElement; }
            set { _PrognosisElement = value; OnPropertyChanged("PrognosisElement"); }
        }
        private Hl7.Fhir.Model.FhirString _PrognosisElement;
        
        /// <summary>
        /// Estimate of likely outcome
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Prognosis
        {
            get { return PrognosisElement != null ? PrognosisElement.Value : null; }
            set
            {
                if(value == null)
                  PrognosisElement = null; 
                else
                  PrognosisElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Prognosis");
            }
        }
        
        /// <summary>
        /// Plan of action after assessment
        /// </summary>
        [FhirElement("plan", Order=200)]
        [References("CarePlan")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Plan
        {
            get { return _Plan; }
            set { _Plan = value; OnPropertyChanged("Plan"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Plan;
        
        /// <summary>
        /// Actions taken during assessment
        /// </summary>
        [FhirElement("action", Order=210)]
        [References("ReferralRequest","ProcedureRequest","Procedure","MedicationPrescription","DiagnosticOrder","NutritionOrder","Supply","Appointment")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Action
        {
            get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.ResourceReference>(); return _Action; }
            set { _Action = value; OnPropertyChanged("Action"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Action;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ClinicalAssessment;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Assessor != null) dest.Assessor = (Hl7.Fhir.Model.ResourceReference)Assessor.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Previous != null) dest.Previous = (Hl7.Fhir.Model.ResourceReference)Previous.DeepCopy();
                if(Problem != null) dest.Problem = new List<Hl7.Fhir.Model.ResourceReference>(Problem.DeepCopy());
                if(Careplan != null) dest.Careplan = (Hl7.Fhir.Model.ResourceReference)Careplan.DeepCopy();
                if(Referral != null) dest.Referral = (Hl7.Fhir.Model.ResourceReference)Referral.DeepCopy();
                if(Investigations != null) dest.Investigations = new List<Hl7.Fhir.Model.ClinicalAssessment.ClinicalAssessmentInvestigationsComponent>(Investigations.DeepCopy());
                if(ProtocolElement != null) dest.ProtocolElement = (Hl7.Fhir.Model.FhirUri)ProtocolElement.DeepCopy();
                if(SummaryElement != null) dest.SummaryElement = (Hl7.Fhir.Model.FhirString)SummaryElement.DeepCopy();
                if(Diagnosis != null) dest.Diagnosis = new List<Hl7.Fhir.Model.ClinicalAssessment.ClinicalAssessmentDiagnosisComponent>(Diagnosis.DeepCopy());
                if(Resolved != null) dest.Resolved = new List<Hl7.Fhir.Model.CodeableConcept>(Resolved.DeepCopy());
                if(RuledOut != null) dest.RuledOut = new List<Hl7.Fhir.Model.ClinicalAssessment.ClinicalAssessmentRuledOutComponent>(RuledOut.DeepCopy());
                if(PrognosisElement != null) dest.PrognosisElement = (Hl7.Fhir.Model.FhirString)PrognosisElement.DeepCopy();
                if(Plan != null) dest.Plan = (Hl7.Fhir.Model.ResourceReference)Plan.DeepCopy();
                if(Action != null) dest.Action = new List<Hl7.Fhir.Model.ResourceReference>(Action.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ClinicalAssessment());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ClinicalAssessment;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Assessor, otherT.Assessor)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Previous, otherT.Previous)) return false;
            if( !DeepComparable.Matches(Problem, otherT.Problem)) return false;
            if( !DeepComparable.Matches(Careplan, otherT.Careplan)) return false;
            if( !DeepComparable.Matches(Referral, otherT.Referral)) return false;
            if( !DeepComparable.Matches(Investigations, otherT.Investigations)) return false;
            if( !DeepComparable.Matches(ProtocolElement, otherT.ProtocolElement)) return false;
            if( !DeepComparable.Matches(SummaryElement, otherT.SummaryElement)) return false;
            if( !DeepComparable.Matches(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.Matches(Resolved, otherT.Resolved)) return false;
            if( !DeepComparable.Matches(RuledOut, otherT.RuledOut)) return false;
            if( !DeepComparable.Matches(PrognosisElement, otherT.PrognosisElement)) return false;
            if( !DeepComparable.Matches(Plan, otherT.Plan)) return false;
            if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ClinicalAssessment;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Assessor, otherT.Assessor)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Previous, otherT.Previous)) return false;
            if( !DeepComparable.IsExactly(Problem, otherT.Problem)) return false;
            if( !DeepComparable.IsExactly(Careplan, otherT.Careplan)) return false;
            if( !DeepComparable.IsExactly(Referral, otherT.Referral)) return false;
            if( !DeepComparable.IsExactly(Investigations, otherT.Investigations)) return false;
            if( !DeepComparable.IsExactly(ProtocolElement, otherT.ProtocolElement)) return false;
            if( !DeepComparable.IsExactly(SummaryElement, otherT.SummaryElement)) return false;
            if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.IsExactly(Resolved, otherT.Resolved)) return false;
            if( !DeepComparable.IsExactly(RuledOut, otherT.RuledOut)) return false;
            if( !DeepComparable.IsExactly(PrognosisElement, otherT.PrognosisElement)) return false;
            if( !DeepComparable.IsExactly(Plan, otherT.Plan)) return false;
            if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            
            return true;
        }
        
    }
    
}
