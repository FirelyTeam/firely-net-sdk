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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A clinical assessment performed when planning treatments and management strategies for a patient
    /// </summary>
    [FhirType("ClinicalImpression", IsResource=true)]
    [DataContract]
    public partial class ClinicalImpression : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ClinicalImpression; } }
        [NotMapped]
        public override string TypeName { get { return "ClinicalImpression"; } }
        
        /// <summary>
        /// The workflow state of a clinical impression.
        /// (url: http://hl7.org/fhir/ValueSet/clinical-impression-status)
        /// </summary>
        [FhirEnumeration("ClinicalImpressionStatus")]
        public enum ClinicalImpressionStatus
        {
            /// <summary>
            /// The assessment is still on-going and results are not yet final.
            /// (system: http://hl7.org/fhir/clinical-impression-status)
            /// </summary>
            [EnumLiteral("in-progress", "http://hl7.org/fhir/clinical-impression-status"), Description("In progress")]
            InProgress,
            /// <summary>
            /// The assessment is done and the results are final.
            /// (system: http://hl7.org/fhir/clinical-impression-status)
            /// </summary>
            [EnumLiteral("completed", "http://hl7.org/fhir/clinical-impression-status"), Description("Completed")]
            Completed,
            /// <summary>
            /// This assessment was never actually done and the record is erroneous (e.g. Wrong patient).
            /// (system: http://hl7.org/fhir/clinical-impression-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/clinical-impression-status"), Description("Entered in Error")]
            EnteredInError,
        }

        [FhirType("InvestigationsComponent")]
        [DataContract]
        public partial class InvestigationsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "InvestigationsComponent"; } }
            
            /// <summary>
            /// A name/code for the set
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
            /// Record of a specific investigation
            /// </summary>
            [FhirElement("item", Order=50)]
            [CLSCompliant(false)]
			[References("Observation","QuestionnaireResponse","FamilyMemberHistory","DiagnosticReport")]
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
                var dest = other as InvestigationsComponent;
                
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
                return CopyTo(new InvestigationsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InvestigationsComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InvestigationsComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    foreach (var elem in Item) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                }
            }

            
        }
        
        
        [FhirType("FindingComponent")]
        [DataContract]
        public partial class FindingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "FindingComponent"; } }
            
            /// <summary>
            /// Specific text or code for finding
            /// </summary>
            [FhirElement("item", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Item;
            
            /// <summary>
            /// Which investigations support finding
            /// </summary>
            [FhirElement("cause", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CauseElement
            {
                get { return _CauseElement; }
                set { _CauseElement = value; OnPropertyChanged("CauseElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CauseElement;
            
            /// <summary>
            /// Which investigations support finding
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Cause
            {
                get { return CauseElement != null ? CauseElement.Value : null; }
                set
                {
                    if (value == null)
                        CauseElement = null; 
                    else
                        CauseElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Cause");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FindingComponent;
                
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
                return CopyTo(new FindingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FindingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
                if( !DeepComparable.Matches(CauseElement, otherT.CauseElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FindingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
                if( !DeepComparable.IsExactly(CauseElement, otherT.CauseElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Item != null) yield return Item;
                    if (CauseElement != null) yield return CauseElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Item != null) yield return new ElementValue("item", Item);
                    if (CauseElement != null) yield return new ElementValue("cause", CauseElement);
                }
            }

            
        }
        
        
        [FhirType("RuledOutComponent")]
        [DataContract]
        public partial class RuledOutComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "RuledOutComponent"; } }
            
            /// <summary>
            /// Specific text of code for diagnosis
            /// </summary>
            [FhirElement("item", Order=40)]
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
            [FhirElement("reason", Order=50)]
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
                    if (value == null)
                        ReasonElement = null; 
                    else
                        ReasonElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Reason");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RuledOutComponent;
                
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
                return CopyTo(new RuledOutComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RuledOutComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
                if( !DeepComparable.Matches(ReasonElement, otherT.ReasonElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RuledOutComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
                if( !DeepComparable.IsExactly(ReasonElement, otherT.ReasonElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Item != null) yield return Item;
                    if (ReasonElement != null) yield return ReasonElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Item != null) yield return new ElementValue("item", Item);
                    if (ReasonElement != null) yield return new ElementValue("reason", ReasonElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// The patient being assessed
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=90)]
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
        /// The clinician performing the assessment
        /// </summary>
        [FhirElement("assessor", InSummary=true, Order=100)]
        [CLSCompliant(false)]
		[References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Assessor
        {
            get { return _Assessor; }
            set { _Assessor = value; OnPropertyChanged("Assessor"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Assessor;
        
        /// <summary>
        /// in-progress | completed | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ClinicalImpression.ClinicalImpressionStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ClinicalImpression.ClinicalImpressionStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | completed | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ClinicalImpression.ClinicalImpressionStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ClinicalImpression.ClinicalImpressionStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// When the assessment occurred
        /// </summary>
        [FhirElement("date", InSummary=true, Order=120)]
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
                if (value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Why/how the assessment was performed
        /// </summary>
        [FhirElement("description", InSummary=true, Order=130)]
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
                if (value == null)
                  DescriptionElement = null; 
                else
                  DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// Reference to last assessment
        /// </summary>
        [FhirElement("previous", Order=140)]
        [CLSCompliant(false)]
		[References("ClinicalImpression")]
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
        [FhirElement("problem", InSummary=true, Order=150)]
        [CLSCompliant(false)]
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
        /// Request or event that necessitated this assessment
        /// </summary>
        [FhirElement("trigger", Order=160, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Trigger
        {
            get { return _Trigger; }
            set { _Trigger = value; OnPropertyChanged("Trigger"); }
        }
        
        private Hl7.Fhir.Model.Element _Trigger;
        
        /// <summary>
        /// One or more sets of investigations (signs, symptions, etc.)
        /// </summary>
        [FhirElement("investigations", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ClinicalImpression.InvestigationsComponent> Investigations
        {
            get { if(_Investigations==null) _Investigations = new List<Hl7.Fhir.Model.ClinicalImpression.InvestigationsComponent>(); return _Investigations; }
            set { _Investigations = value; OnPropertyChanged("Investigations"); }
        }
        
        private List<Hl7.Fhir.Model.ClinicalImpression.InvestigationsComponent> _Investigations;
        
        /// <summary>
        /// Clinical Protocol followed
        /// </summary>
        [FhirElement("protocol", Order=180)]
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
                if (value == null)
                  ProtocolElement = null; 
                else
                  ProtocolElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Protocol");
            }
        }
        
        /// <summary>
        /// Summary of the assessment
        /// </summary>
        [FhirElement("summary", Order=190)]
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
                if (value == null)
                  SummaryElement = null; 
                else
                  SummaryElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Summary");
            }
        }
        
        /// <summary>
        /// Possible or likely findings and diagnoses
        /// </summary>
        [FhirElement("finding", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ClinicalImpression.FindingComponent> Finding
        {
            get { if(_Finding==null) _Finding = new List<Hl7.Fhir.Model.ClinicalImpression.FindingComponent>(); return _Finding; }
            set { _Finding = value; OnPropertyChanged("Finding"); }
        }
        
        private List<Hl7.Fhir.Model.ClinicalImpression.FindingComponent> _Finding;
        
        /// <summary>
        /// Diagnoses/conditions resolved since previous assessment
        /// </summary>
        [FhirElement("resolved", Order=210)]
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
        [FhirElement("ruledOut", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ClinicalImpression.RuledOutComponent> RuledOut
        {
            get { if(_RuledOut==null) _RuledOut = new List<Hl7.Fhir.Model.ClinicalImpression.RuledOutComponent>(); return _RuledOut; }
            set { _RuledOut = value; OnPropertyChanged("RuledOut"); }
        }
        
        private List<Hl7.Fhir.Model.ClinicalImpression.RuledOutComponent> _RuledOut;
        
        /// <summary>
        /// Estimate of likely outcome
        /// </summary>
        [FhirElement("prognosis", Order=230)]
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
                if (value == null)
                  PrognosisElement = null; 
                else
                  PrognosisElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Prognosis");
            }
        }
        
        /// <summary>
        /// Plan of action after assessment
        /// </summary>
        [FhirElement("plan", Order=240)]
        [CLSCompliant(false)]
		[References("CarePlan","Appointment","CommunicationRequest","DeviceUseRequest","DiagnosticOrder","MedicationOrder","NutritionOrder","Order","ProcedureRequest","ProcessRequest","ReferralRequest","SupplyRequest","VisionPrescription")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Plan
        {
            get { if(_Plan==null) _Plan = new List<Hl7.Fhir.Model.ResourceReference>(); return _Plan; }
            set { _Plan = value; OnPropertyChanged("Plan"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Plan;
        
        /// <summary>
        /// Actions taken during assessment
        /// </summary>
        [FhirElement("action", Order=250)]
        [CLSCompliant(false)]
		[References("ReferralRequest","ProcedureRequest","Procedure","MedicationOrder","DiagnosticOrder","NutritionOrder","SupplyRequest","Appointment")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Action
        {
            get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.ResourceReference>(); return _Action; }
            set { _Action = value; OnPropertyChanged("Action"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Action;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ClinicalImpression;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Assessor != null) dest.Assessor = (Hl7.Fhir.Model.ResourceReference)Assessor.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ClinicalImpression.ClinicalImpressionStatus>)StatusElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Previous != null) dest.Previous = (Hl7.Fhir.Model.ResourceReference)Previous.DeepCopy();
                if(Problem != null) dest.Problem = new List<Hl7.Fhir.Model.ResourceReference>(Problem.DeepCopy());
                if(Trigger != null) dest.Trigger = (Hl7.Fhir.Model.Element)Trigger.DeepCopy();
                if(Investigations != null) dest.Investigations = new List<Hl7.Fhir.Model.ClinicalImpression.InvestigationsComponent>(Investigations.DeepCopy());
                if(ProtocolElement != null) dest.ProtocolElement = (Hl7.Fhir.Model.FhirUri)ProtocolElement.DeepCopy();
                if(SummaryElement != null) dest.SummaryElement = (Hl7.Fhir.Model.FhirString)SummaryElement.DeepCopy();
                if(Finding != null) dest.Finding = new List<Hl7.Fhir.Model.ClinicalImpression.FindingComponent>(Finding.DeepCopy());
                if(Resolved != null) dest.Resolved = new List<Hl7.Fhir.Model.CodeableConcept>(Resolved.DeepCopy());
                if(RuledOut != null) dest.RuledOut = new List<Hl7.Fhir.Model.ClinicalImpression.RuledOutComponent>(RuledOut.DeepCopy());
                if(PrognosisElement != null) dest.PrognosisElement = (Hl7.Fhir.Model.FhirString)PrognosisElement.DeepCopy();
                if(Plan != null) dest.Plan = new List<Hl7.Fhir.Model.ResourceReference>(Plan.DeepCopy());
                if(Action != null) dest.Action = new List<Hl7.Fhir.Model.ResourceReference>(Action.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ClinicalImpression());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ClinicalImpression;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Assessor, otherT.Assessor)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Previous, otherT.Previous)) return false;
            if( !DeepComparable.Matches(Problem, otherT.Problem)) return false;
            if( !DeepComparable.Matches(Trigger, otherT.Trigger)) return false;
            if( !DeepComparable.Matches(Investigations, otherT.Investigations)) return false;
            if( !DeepComparable.Matches(ProtocolElement, otherT.ProtocolElement)) return false;
            if( !DeepComparable.Matches(SummaryElement, otherT.SummaryElement)) return false;
            if( !DeepComparable.Matches(Finding, otherT.Finding)) return false;
            if( !DeepComparable.Matches(Resolved, otherT.Resolved)) return false;
            if( !DeepComparable.Matches(RuledOut, otherT.RuledOut)) return false;
            if( !DeepComparable.Matches(PrognosisElement, otherT.PrognosisElement)) return false;
            if( !DeepComparable.Matches(Plan, otherT.Plan)) return false;
            if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ClinicalImpression;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Assessor, otherT.Assessor)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Previous, otherT.Previous)) return false;
            if( !DeepComparable.IsExactly(Problem, otherT.Problem)) return false;
            if( !DeepComparable.IsExactly(Trigger, otherT.Trigger)) return false;
            if( !DeepComparable.IsExactly(Investigations, otherT.Investigations)) return false;
            if( !DeepComparable.IsExactly(ProtocolElement, otherT.ProtocolElement)) return false;
            if( !DeepComparable.IsExactly(SummaryElement, otherT.SummaryElement)) return false;
            if( !DeepComparable.IsExactly(Finding, otherT.Finding)) return false;
            if( !DeepComparable.IsExactly(Resolved, otherT.Resolved)) return false;
            if( !DeepComparable.IsExactly(RuledOut, otherT.RuledOut)) return false;
            if( !DeepComparable.IsExactly(PrognosisElement, otherT.PrognosisElement)) return false;
            if( !DeepComparable.IsExactly(Plan, otherT.Plan)) return false;
            if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Patient != null) yield return Patient;
				if (Assessor != null) yield return Assessor;
				if (StatusElement != null) yield return StatusElement;
				if (DateElement != null) yield return DateElement;
				if (DescriptionElement != null) yield return DescriptionElement;
				if (Previous != null) yield return Previous;
				foreach (var elem in Problem) { if (elem != null) yield return elem; }
				if (Trigger != null) yield return Trigger;
				foreach (var elem in Investigations) { if (elem != null) yield return elem; }
				if (ProtocolElement != null) yield return ProtocolElement;
				if (SummaryElement != null) yield return SummaryElement;
				foreach (var elem in Finding) { if (elem != null) yield return elem; }
				foreach (var elem in Resolved) { if (elem != null) yield return elem; }
				foreach (var elem in RuledOut) { if (elem != null) yield return elem; }
				if (PrognosisElement != null) yield return PrognosisElement;
				foreach (var elem in Plan) { if (elem != null) yield return elem; }
				foreach (var elem in Action) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Assessor != null) yield return new ElementValue("assessor", Assessor);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (Previous != null) yield return new ElementValue("previous", Previous);
                foreach (var elem in Problem) { if (elem != null) yield return new ElementValue("problem", elem); }
                if (Trigger != null) yield return new ElementValue("trigger", Trigger);
                foreach (var elem in Investigations) { if (elem != null) yield return new ElementValue("investigations", elem); }
                if (ProtocolElement != null) yield return new ElementValue("protocol", ProtocolElement);
                if (SummaryElement != null) yield return new ElementValue("summary", SummaryElement);
                foreach (var elem in Finding) { if (elem != null) yield return new ElementValue("finding", elem); }
                foreach (var elem in Resolved) { if (elem != null) yield return new ElementValue("resolved", elem); }
                foreach (var elem in RuledOut) { if (elem != null) yield return new ElementValue("ruledOut", elem); }
                if (PrognosisElement != null) yield return new ElementValue("prognosis", PrognosisElement);
                foreach (var elem in Plan) { if (elem != null) yield return new ElementValue("plan", elem); }
                foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
            }
        }

    }
    
}
