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
// Generated for FHIR v1.6.0
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

        [FhirType("DosageComponent")]
        [DataContract]
        public partial class DosageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DosageComponent"; } }
            
            /// <summary>
            /// Free text dosage instructions as reported by the information source
            /// </summary>
            [FhirElement("text", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Free text dosage instructions as reported by the information source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                if (value == null)
                      TextElement = null; 
                    else
                        TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            /// <summary>
            /// Supplemental instructions - e.g. "with meals"
            /// </summary>
            [FhirElement("additionalInstructions", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> AdditionalInstructions
            {
                get { if(_AdditionalInstructions==null) _AdditionalInstructions = new List<Hl7.Fhir.Model.CodeableConcept>(); return _AdditionalInstructions; }
                set { _AdditionalInstructions = value; OnPropertyChanged("AdditionalInstructions"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _AdditionalInstructions;
            
            /// <summary>
            /// When/how often was medication taken
            /// </summary>
            [FhirElement("timing", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Timing Timing
            {
                get { return _Timing; }
                set { _Timing = value; OnPropertyChanged("Timing"); }
            }
            
            private Hl7.Fhir.Model.Timing _Timing;
            
            /// <summary>
            /// Take "as needed" (for x)
            /// </summary>
            [FhirElement("asNeeded", Order=70, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [DataMember]
            public Hl7.Fhir.Model.Element AsNeeded
            {
                get { return _AsNeeded; }
                set { _AsNeeded = value; OnPropertyChanged("AsNeeded"); }
            }
            
            private Hl7.Fhir.Model.Element _AsNeeded;
            
            /// <summary>
            /// Where (on body) medication is/was administered
            /// </summary>
            [FhirElement("site", Order=80, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Site
            {
                get { return _Site; }
                set { _Site = value; OnPropertyChanged("Site"); }
            }
            
            private Hl7.Fhir.Model.Element _Site;
            
            /// <summary>
            /// How the medication entered the body
            /// </summary>
            [FhirElement("route", Order=90)]
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
            [FhirElement("method", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method
            {
                get { return _Method; }
                set { _Method = value; OnPropertyChanged("Method"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Method;
            
            /// <summary>
            /// Amount of medication per dose
            /// </summary>
            [FhirElement("dose", Order=110, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.SimpleQuantity),typeof(Hl7.Fhir.Model.Range))]
            [DataMember]
            public Hl7.Fhir.Model.Element Dose
            {
                get { return _Dose; }
                set { _Dose = value; OnPropertyChanged("Dose"); }
            }
            
            private Hl7.Fhir.Model.Element _Dose;
            
            /// <summary>
            /// Dose quantity per unit of time
            /// </summary>
            [FhirElement("rate", Order=120, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.SimpleQuantity))]
            [DataMember]
            public Hl7.Fhir.Model.Element Rate
            {
                get { return _Rate; }
                set { _Rate = value; OnPropertyChanged("Rate"); }
            }
            
            private Hl7.Fhir.Model.Element _Rate;
            
            /// <summary>
            /// Maximum dose that was consumed per unit of time
            /// </summary>
            [FhirElement("maxDosePerPeriod", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio MaxDosePerPeriod
            {
                get { return _MaxDosePerPeriod; }
                set { _MaxDosePerPeriod = value; OnPropertyChanged("MaxDosePerPeriod"); }
            }
            
            private Hl7.Fhir.Model.Ratio _MaxDosePerPeriod;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DosageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(AdditionalInstructions != null) dest.AdditionalInstructions = new List<Hl7.Fhir.Model.CodeableConcept>(AdditionalInstructions.DeepCopy());
                    if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Timing)Timing.DeepCopy();
                    if(AsNeeded != null) dest.AsNeeded = (Hl7.Fhir.Model.Element)AsNeeded.DeepCopy();
                    if(Site != null) dest.Site = (Hl7.Fhir.Model.Element)Site.DeepCopy();
                    if(Route != null) dest.Route = (Hl7.Fhir.Model.CodeableConcept)Route.DeepCopy();
                    if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                    if(Dose != null) dest.Dose = (Hl7.Fhir.Model.Element)Dose.DeepCopy();
                    if(Rate != null) dest.Rate = (Hl7.Fhir.Model.Element)Rate.DeepCopy();
                    if(MaxDosePerPeriod != null) dest.MaxDosePerPeriod = (Hl7.Fhir.Model.Ratio)MaxDosePerPeriod.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DosageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DosageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(AdditionalInstructions, otherT.AdditionalInstructions)) return false;
                if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
                if( !DeepComparable.Matches(AsNeeded, otherT.AsNeeded)) return false;
                if( !DeepComparable.Matches(Site, otherT.Site)) return false;
                if( !DeepComparable.Matches(Route, otherT.Route)) return false;
                if( !DeepComparable.Matches(Method, otherT.Method)) return false;
                if( !DeepComparable.Matches(Dose, otherT.Dose)) return false;
                if( !DeepComparable.Matches(Rate, otherT.Rate)) return false;
                if( !DeepComparable.Matches(MaxDosePerPeriod, otherT.MaxDosePerPeriod)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DosageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(AdditionalInstructions, otherT.AdditionalInstructions)) return false;
                if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
                if( !DeepComparable.IsExactly(AsNeeded, otherT.AsNeeded)) return false;
                if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
                if( !DeepComparable.IsExactly(Route, otherT.Route)) return false;
                if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
                if( !DeepComparable.IsExactly(Dose, otherT.Dose)) return false;
                if( !DeepComparable.IsExactly(Rate, otherT.Rate)) return false;
                if( !DeepComparable.IsExactly(MaxDosePerPeriod, otherT.MaxDosePerPeriod)) return false;
                
                return true;
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
        [FhirElement("patient", InSummary=true, Order=120)]
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
        /// Person who provided the information about the taking of this medication
        /// </summary>
        [FhirElement("informationSource", Order=140)]
        [References("Patient","Practitioner","RelatedPerson")]
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
        [FhirElement("supportingInformation", Order=150)]
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
        /// True if medication is/was not being taken
        /// </summary>
        [FhirElement("notTaken", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean NotTakenElement
        {
            get { return _NotTakenElement; }
            set { _NotTakenElement = value; OnPropertyChanged("NotTakenElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _NotTakenElement;
        
        /// <summary>
        /// True if medication is/was not being taken
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? NotTaken
        {
            get { return NotTakenElement != null ? NotTakenElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  NotTakenElement = null; 
                else
                  NotTakenElement = new Hl7.Fhir.Model.FhirBoolean(value);
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
        [FhirElement("reasonForUseCode", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonForUseCode
        {
            get { if(_ReasonForUseCode==null) _ReasonForUseCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonForUseCode; }
            set { _ReasonForUseCode = value; OnPropertyChanged("ReasonForUseCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonForUseCode;
        
        /// <summary>
        /// Condition that supports why the medication is being/was taken
        /// </summary>
        [FhirElement("reasonForUseReference", Order=200)]
        [References("Condition")]
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
        public List<Hl7.Fhir.Model.MedicationStatement.DosageComponent> Dosage
        {
            get { if(_Dosage==null) _Dosage = new List<Hl7.Fhir.Model.MedicationStatement.DosageComponent>(); return _Dosage; }
            set { _Dosage = value; OnPropertyChanged("Dosage"); }
        }
        
        private List<Hl7.Fhir.Model.MedicationStatement.DosageComponent> _Dosage;
        

        public static ElementDefinition.ConstraintComponent MedicationStatement_MST_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "reasonForUseReference.empty() or notTaken = false",
            Key = "mst-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Reason for use reference is only permitted if notTaken is false",
            Xpath = "not(exists(*[starts-with(local-name(.), 'reasonForUseReference')]) and f:notTaken/@value=true())"
        };

        public static ElementDefinition.ConstraintComponent MedicationStatement_MST_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "reasonNotTaken.empty() or notTaken = true",
            Key = "mst-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Reason not taken is only permitted if notTaken is true",
            Xpath = "not(exists(f:reasonNotTaken) and f:notTaken/@value=false())"
        };

        public static ElementDefinition.ConstraintComponent MedicationStatement_MST_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "reasonForUseCode.empty() or notTaken = false",
            Key = "mst-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Reason for use code is only permitted if notTaken is false",
            Xpath = "not(exists(*[starts-with(local-name(.), 'reasonForUseCode')]) and f:notTaken/@value=true())"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(MedicationStatement_MST_3);
            InvariantConstraints.Add(MedicationStatement_MST_1);
            InvariantConstraints.Add(MedicationStatement_MST_2);
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
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                if(InformationSource != null) dest.InformationSource = (Hl7.Fhir.Model.ResourceReference)InformationSource.DeepCopy();
                if(SupportingInformation != null) dest.SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(SupportingInformation.DeepCopy());
                if(DateAssertedElement != null) dest.DateAssertedElement = (Hl7.Fhir.Model.FhirDateTime)DateAssertedElement.DeepCopy();
                if(NotTakenElement != null) dest.NotTakenElement = (Hl7.Fhir.Model.FhirBoolean)NotTakenElement.DeepCopy();
                if(ReasonNotTaken != null) dest.ReasonNotTaken = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonNotTaken.DeepCopy());
                if(ReasonForUseCode != null) dest.ReasonForUseCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonForUseCode.DeepCopy());
                if(ReasonForUseReference != null) dest.ReasonForUseReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonForUseReference.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(CategoryElement != null) dest.CategoryElement = (Hl7.Fhir.Model.Code)CategoryElement.DeepCopy();
                if(Dosage != null) dest.Dosage = new List<Hl7.Fhir.Model.MedicationStatement.DosageComponent>(Dosage.DeepCopy());
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
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
            if( !DeepComparable.Matches(InformationSource, otherT.InformationSource)) return false;
            if( !DeepComparable.Matches(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.Matches(DateAssertedElement, otherT.DateAssertedElement)) return false;
            if( !DeepComparable.Matches(NotTakenElement, otherT.NotTakenElement)) return false;
            if( !DeepComparable.Matches(ReasonNotTaken, otherT.ReasonNotTaken)) return false;
            if( !DeepComparable.Matches(ReasonForUseCode, otherT.ReasonForUseCode)) return false;
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
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
            if( !DeepComparable.IsExactly(InformationSource, otherT.InformationSource)) return false;
            if( !DeepComparable.IsExactly(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.IsExactly(DateAssertedElement, otherT.DateAssertedElement)) return false;
            if( !DeepComparable.IsExactly(NotTakenElement, otherT.NotTakenElement)) return false;
            if( !DeepComparable.IsExactly(ReasonNotTaken, otherT.ReasonNotTaken)) return false;
            if( !DeepComparable.IsExactly(ReasonForUseCode, otherT.ReasonForUseCode)) return false;
            if( !DeepComparable.IsExactly(ReasonForUseReference, otherT.ReasonForUseReference)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.IsExactly(Dosage, otherT.Dosage)) return false;
            
            return true;
        }
        
    }
    
}
