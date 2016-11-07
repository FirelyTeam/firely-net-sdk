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
// Generated for FHIR v1.7.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Prescription of medication to for patient
    /// </summary>
    [FhirType("MedicationRequest", IsResource=true)]
    [DataContract]
    public partial class MedicationRequest : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicationRequest; } }
        [NotMapped]
        public override string TypeName { get { return "MedicationRequest"; } }
        
        /// <summary>
        /// A coded concept specifying the state of the prescribing event. Describes the lifecycle of the prescription
        /// (url: http://hl7.org/fhir/ValueSet/medication-request-status)
        /// </summary>
        [FhirEnumeration("MedicationRequestStatus")]
        public enum MedicationRequestStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-request-status)
            /// </summary>
            [EnumLiteral("active"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-request-status)
            /// </summary>
            [EnumLiteral("on-hold"), Description("On Hold")]
            OnHold,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-request-status)
            /// </summary>
            [EnumLiteral("cancelled"), Description("Cancelled")]
            Cancelled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-request-status)
            /// </summary>
            [EnumLiteral("completed"), Description("Completed")]
            Completed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-request-status)
            /// </summary>
            [EnumLiteral("entered-in-error"), Description("Entered In Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-request-status)
            /// </summary>
            [EnumLiteral("stopped"), Description("Stopped")]
            Stopped,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/medication-request-status)
            /// </summary>
            [EnumLiteral("draft"), Description("Draft")]
            Draft,
        }

        [FhirType("DispenseRequestComponent")]
        [DataContract]
        public partial class DispenseRequestComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DispenseRequestComponent"; } }
            
            /// <summary>
            /// Time period supply is authorized for
            /// </summary>
            [FhirElement("validityPeriod", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Period ValidityPeriod
            {
                get { return _ValidityPeriod; }
                set { _ValidityPeriod = value; OnPropertyChanged("ValidityPeriod"); }
            }
            
            private Hl7.Fhir.Model.Period _ValidityPeriod;
            
            /// <summary>
            /// Number of refills authorized
            /// </summary>
            [FhirElement("numberOfRepeatsAllowed", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt NumberOfRepeatsAllowedElement
            {
                get { return _NumberOfRepeatsAllowedElement; }
                set { _NumberOfRepeatsAllowedElement = value; OnPropertyChanged("NumberOfRepeatsAllowedElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _NumberOfRepeatsAllowedElement;
            
            /// <summary>
            /// Number of refills authorized
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? NumberOfRepeatsAllowed
            {
                get { return NumberOfRepeatsAllowedElement != null ? NumberOfRepeatsAllowedElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        NumberOfRepeatsAllowedElement = null; 
                    else
                        NumberOfRepeatsAllowedElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("NumberOfRepeatsAllowed");
                }
            }
            
            /// <summary>
            /// Amount of medication to supply per dispense
            /// </summary>
            [FhirElement("quantity", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Number of days supply per dispense
            /// </summary>
            [FhirElement("expectedSupplyDuration", Order=70)]
            [DataMember]
            public Duration ExpectedSupplyDuration
            {
                get { return _ExpectedSupplyDuration; }
                set { _ExpectedSupplyDuration = value; OnPropertyChanged("ExpectedSupplyDuration"); }
            }
            
            private Duration _ExpectedSupplyDuration;
            
            /// <summary>
            /// Intended dispenser
            /// </summary>
            [FhirElement("performer", Order=80)]
            [References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Performer
            {
                get { return _Performer; }
                set { _Performer = value; OnPropertyChanged("Performer"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Performer;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DispenseRequestComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ValidityPeriod != null) dest.ValidityPeriod = (Hl7.Fhir.Model.Period)ValidityPeriod.DeepCopy();
                    if(NumberOfRepeatsAllowedElement != null) dest.NumberOfRepeatsAllowedElement = (Hl7.Fhir.Model.PositiveInt)NumberOfRepeatsAllowedElement.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(ExpectedSupplyDuration != null) dest.ExpectedSupplyDuration = (Duration)ExpectedSupplyDuration.DeepCopy();
                    if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DispenseRequestComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DispenseRequestComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ValidityPeriod, otherT.ValidityPeriod)) return false;
                if( !DeepComparable.Matches(NumberOfRepeatsAllowedElement, otherT.NumberOfRepeatsAllowedElement)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(ExpectedSupplyDuration, otherT.ExpectedSupplyDuration)) return false;
                if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DispenseRequestComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ValidityPeriod, otherT.ValidityPeriod)) return false;
                if( !DeepComparable.IsExactly(NumberOfRepeatsAllowedElement, otherT.NumberOfRepeatsAllowedElement)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(ExpectedSupplyDuration, otherT.ExpectedSupplyDuration)) return false;
                if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // DispenseRequestComponent elements
                    if (ValidityPeriod != null) yield return ValidityPeriod;
                    if (NumberOfRepeatsAllowedElement != null) yield return NumberOfRepeatsAllowedElement;
                    if (Quantity != null) yield return Quantity;
                    if (ExpectedSupplyDuration != null) yield return ExpectedSupplyDuration;
                    if (Performer != null) yield return Performer;
                }
            }
            
        }
        
        
        [FhirType("SubstitutionComponent")]
        [DataContract]
        public partial class SubstitutionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SubstitutionComponent"; } }
            
            /// <summary>
            /// Whether substitution is allowed or not
            /// </summary>
            [FhirElement("allowed", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AllowedElement
            {
                get { return _AllowedElement; }
                set { _AllowedElement = value; OnPropertyChanged("AllowedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AllowedElement;
            
            /// <summary>
            /// Whether substitution is allowed or not
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Allowed
            {
                get { return AllowedElement != null ? AllowedElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        AllowedElement = null; 
                    else
                        AllowedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Allowed");
                }
            }
            
            /// <summary>
            /// Why should (not) substitution be made
            /// </summary>
            [FhirElement("reason", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Reason;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubstitutionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AllowedElement != null) dest.AllowedElement = (Hl7.Fhir.Model.FhirBoolean)AllowedElement.DeepCopy();
                    if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SubstitutionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubstitutionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AllowedElement, otherT.AllowedElement)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubstitutionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AllowedElement, otherT.AllowedElement)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // SubstitutionComponent elements
                    if (AllowedElement != null) yield return AllowedElement;
                    if (Reason != null) yield return Reason;
                }
            }
            
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
        /// Protocol or definition
        /// </summary>
        [FhirElement("definition", InSummary=true, Order=100)]
        [References("ActivityDefinition","PlanDefinition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Definition
        {
            get { if(_Definition==null) _Definition = new List<Hl7.Fhir.Model.ResourceReference>(); return _Definition; }
            set { _Definition = value; OnPropertyChanged("Definition"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Definition;
        
        /// <summary>
        /// What request fulfills
        /// </summary>
        [FhirElement("basedOn", InSummary=true, Order=110)]
        [References("CarePlan","DiagnosticRequest","MedicationRequest","ProcedureRequest","ReferralRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Identifier of composite
        /// </summary>
        [FhirElement("requisition", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Requisition
        {
            get { return _Requisition; }
            set { _Requisition = value; OnPropertyChanged("Requisition"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Requisition;
        
        /// <summary>
        /// active | on-hold | cancelled | completed | entered-in-error | stopped | draft
        /// </summary>
        [FhirElement("status", InSummary=true, Order=130)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MedicationRequest.MedicationRequestStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.MedicationRequest.MedicationRequestStatus> _StatusElement;
        
        /// <summary>
        /// active | on-hold | cancelled | completed | entered-in-error | stopped | draft
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MedicationRequest.MedicationRequestStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.MedicationRequest.MedicationRequestStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// proposal | plan | original-order
        /// </summary>
        [FhirElement("stage", InSummary=true, Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Stage
        {
            get { return _Stage; }
            set { _Stage = value; OnPropertyChanged("Stage"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Stage;
        
        /// <summary>
        /// Medication to be taken
        /// </summary>
        [FhirElement("medication", InSummary=true, Order=150, Choice=ChoiceType.DatatypeChoice)]
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
        /// Who prescription is for
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=160)]
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
        /// Created during encounter/admission/stay
        /// </summary>
        [FhirElement("context", Order=170)]
        [References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// Information to support ordering of the medication
        /// </summary>
        [FhirElement("supportingInformation", Order=180)]
        [References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SupportingInformation
        {
            get { if(_SupportingInformation==null) _SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(); return _SupportingInformation; }
            set { _SupportingInformation = value; OnPropertyChanged("SupportingInformation"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SupportingInformation;
        
        /// <summary>
        /// When prescription was initially authorized
        /// </summary>
        [FhirElement("dateWritten", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateWrittenElement
        {
            get { return _DateWrittenElement; }
            set { _DateWrittenElement = value; OnPropertyChanged("DateWrittenElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateWrittenElement;
        
        /// <summary>
        /// When prescription was initially authorized
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DateWritten
        {
            get { return DateWrittenElement != null ? DateWrittenElement.Value : null; }
            set
            {
                if (value == null)
                  DateWrittenElement = null; 
                else
                  DateWrittenElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("DateWritten");
            }
        }
        
        /// <summary>
        /// Who ordered the initial medication(s)
        /// </summary>
        [FhirElement("prescriber", InSummary=true, Order=200)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Prescriber
        {
            get { return _Prescriber; }
            set { _Prescriber = value; OnPropertyChanged("Prescriber"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Prescriber;
        
        /// <summary>
        /// Reason or indication for writing the prescription
        /// </summary>
        [FhirElement("reasonCode", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Condition or Observation that supports why the prescription is being written
        /// </summary>
        [FhirElement("reasonReference", Order=220)]
        [References("Condition","Observation")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// Information about the prescription
        /// </summary>
        [FhirElement("note", Order=230)]
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
        [FhirElement("category", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// How the medication should be taken
        /// </summary>
        [FhirElement("dosageInstruction", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DosageInstruction> DosageInstruction
        {
            get { if(_DosageInstruction==null) _DosageInstruction = new List<DosageInstruction>(); return _DosageInstruction; }
            set { _DosageInstruction = value; OnPropertyChanged("DosageInstruction"); }
        }
        
        private List<DosageInstruction> _DosageInstruction;
        
        /// <summary>
        /// Medication supply authorization
        /// </summary>
        [FhirElement("dispenseRequest", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.MedicationRequest.DispenseRequestComponent DispenseRequest
        {
            get { return _DispenseRequest; }
            set { _DispenseRequest = value; OnPropertyChanged("DispenseRequest"); }
        }
        
        private Hl7.Fhir.Model.MedicationRequest.DispenseRequestComponent _DispenseRequest;
        
        /// <summary>
        /// Any restrictions on medication substitution
        /// </summary>
        [FhirElement("substitution", Order=270)]
        [DataMember]
        public Hl7.Fhir.Model.MedicationRequest.SubstitutionComponent Substitution
        {
            get { return _Substitution; }
            set { _Substitution = value; OnPropertyChanged("Substitution"); }
        }
        
        private Hl7.Fhir.Model.MedicationRequest.SubstitutionComponent _Substitution;
        
        /// <summary>
        /// An order/prescription that this supersedes
        /// </summary>
        [FhirElement("priorPrescription", Order=280)]
        [References("MedicationRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PriorPrescription
        {
            get { return _PriorPrescription; }
            set { _PriorPrescription = value; OnPropertyChanged("PriorPrescription"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _PriorPrescription;
        
        /// <summary>
        /// A list of events of interest in the lifecycle
        /// </summary>
        [FhirElement("eventHistory", Order=290)]
        [References("Provenance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> EventHistory
        {
            get { if(_EventHistory==null) _EventHistory = new List<Hl7.Fhir.Model.ResourceReference>(); return _EventHistory; }
            set { _EventHistory = value; OnPropertyChanged("EventHistory"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _EventHistory;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicationRequest;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Definition != null) dest.Definition = new List<Hl7.Fhir.Model.ResourceReference>(Definition.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(Requisition != null) dest.Requisition = (Hl7.Fhir.Model.Identifier)Requisition.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.MedicationRequest.MedicationRequestStatus>)StatusElement.DeepCopy();
                if(Stage != null) dest.Stage = (Hl7.Fhir.Model.CodeableConcept)Stage.DeepCopy();
                if(Medication != null) dest.Medication = (Hl7.Fhir.Model.Element)Medication.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(SupportingInformation != null) dest.SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(SupportingInformation.DeepCopy());
                if(DateWrittenElement != null) dest.DateWrittenElement = (Hl7.Fhir.Model.FhirDateTime)DateWrittenElement.DeepCopy();
                if(Prescriber != null) dest.Prescriber = (Hl7.Fhir.Model.ResourceReference)Prescriber.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(DosageInstruction != null) dest.DosageInstruction = new List<DosageInstruction>(DosageInstruction.DeepCopy());
                if(DispenseRequest != null) dest.DispenseRequest = (Hl7.Fhir.Model.MedicationRequest.DispenseRequestComponent)DispenseRequest.DeepCopy();
                if(Substitution != null) dest.Substitution = (Hl7.Fhir.Model.MedicationRequest.SubstitutionComponent)Substitution.DeepCopy();
                if(PriorPrescription != null) dest.PriorPrescription = (Hl7.Fhir.Model.ResourceReference)PriorPrescription.DeepCopy();
                if(EventHistory != null) dest.EventHistory = new List<Hl7.Fhir.Model.ResourceReference>(EventHistory.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new MedicationRequest());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicationRequest;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(Requisition, otherT.Requisition)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Stage, otherT.Stage)) return false;
            if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.Matches(DateWrittenElement, otherT.DateWrittenElement)) return false;
            if( !DeepComparable.Matches(Prescriber, otherT.Prescriber)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(DosageInstruction, otherT.DosageInstruction)) return false;
            if( !DeepComparable.Matches(DispenseRequest, otherT.DispenseRequest)) return false;
            if( !DeepComparable.Matches(Substitution, otherT.Substitution)) return false;
            if( !DeepComparable.Matches(PriorPrescription, otherT.PriorPrescription)) return false;
            if( !DeepComparable.Matches(EventHistory, otherT.EventHistory)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicationRequest;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(Requisition, otherT.Requisition)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Stage, otherT.Stage)) return false;
            if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.IsExactly(DateWrittenElement, otherT.DateWrittenElement)) return false;
            if( !DeepComparable.IsExactly(Prescriber, otherT.Prescriber)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(DosageInstruction, otherT.DosageInstruction)) return false;
            if( !DeepComparable.IsExactly(DispenseRequest, otherT.DispenseRequest)) return false;
            if( !DeepComparable.IsExactly(Substitution, otherT.Substitution)) return false;
            if( !DeepComparable.IsExactly(PriorPrescription, otherT.PriorPrescription)) return false;
            if( !DeepComparable.IsExactly(EventHistory, otherT.EventHistory)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
				// MedicationRequest elements
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				foreach (var elem in Definition) { if (elem != null) yield return elem; }
				foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
				if (Requisition != null) yield return Requisition;
				if (StatusElement != null) yield return StatusElement;
				if (Stage != null) yield return Stage;
				if (Medication != null) yield return Medication;
				if (Patient != null) yield return Patient;
				if (Context != null) yield return Context;
				foreach (var elem in SupportingInformation) { if (elem != null) yield return elem; }
				if (DateWrittenElement != null) yield return DateWrittenElement;
				if (Prescriber != null) yield return Prescriber;
				foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
				if (Category != null) yield return Category;
				foreach (var elem in DosageInstruction) { if (elem != null) yield return elem; }
				if (DispenseRequest != null) yield return DispenseRequest;
				if (Substitution != null) yield return Substitution;
				if (PriorPrescription != null) yield return PriorPrescription;
				foreach (var elem in EventHistory) { if (elem != null) yield return elem; }
            }
        }
    }
    
}
