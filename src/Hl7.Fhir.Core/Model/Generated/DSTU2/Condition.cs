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
namespace Hl7.Fhir.Model.DSTU2
{
    /// <summary>
    /// Detailed information about conditions, problems or diagnoses
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "Condition", IsResource=true)]
    [DataContract]
    public partial class Condition : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ICondition, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Condition; } }
        [NotMapped]
        public override string TypeName { get { return "Condition"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "StageComponent")]
        [DataContract]
        public partial class StageComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IConditionStageComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "StageComponent"; } }
            
            /// <summary>
            /// Simple summary (disease specific)
            /// </summary>
            [FhirElement("summary", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
            [FhirElement("assessment", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("StageComponent");
                base.Serialize(sink);
                sink.Element("summary", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Summary?.Serialize(sink);
                sink.BeginList("assessment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Assessment)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "summary":
                        Summary = source.Populate(Summary);
                        return true;
                    case "assessment":
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "assessment":
                        source.PopulateListItem(Assessment, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StageComponent;
            
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
                 return CopyTo(new StageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StageComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Summary, otherT.Summary)) return false;
                if( !DeepComparable.Matches(Assessment, otherT.Assessment)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StageComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Summary, otherT.Summary)) return false;
                if( !DeepComparable.IsExactly(Assessment, otherT.Assessment)) return false;
            
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
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "EvidenceComponent")]
        [DataContract]
        public partial class EvidenceComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IConditionEvidenceComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "EvidenceComponent"; } }
            
            /// <summary>
            /// Manifestation/symptom
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
            [FhirElement("detail", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Detail
            {
                get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.ResourceReference>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Detail;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("EvidenceComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Code?.Serialize(sink);
                sink.BeginList("detail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Detail)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "detail":
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "detail":
                        source.PopulateListItem(Detail, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EvidenceComponent;
            
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
                    if (Code != null) yield return Code;
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IConditionEvidenceComponent> Hl7.Fhir.Model.ICondition.Evidence { get { return Evidence; } }
    
        
        /// <summary>
        /// External Ids for this condition
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
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
        [FhirElement("patient", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
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
        /// Encounter when condition first asserted
        /// </summary>
        [FhirElement("encounter", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
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
        /// Person who asserts this condition
        /// </summary>
        [FhirElement("asserter", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("Practitioner","Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Asserter
        {
            get { return _Asserter; }
            set { _Asserter = value; OnPropertyChanged("Asserter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Asserter;
        
        /// <summary>
        /// When first entered
        /// </summary>
        [FhirElement("dateRecorded", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Date DateRecordedElement
        {
            get { return _DateRecordedElement; }
            set { _DateRecordedElement = value; OnPropertyChanged("DateRecordedElement"); }
        }
        
        private Hl7.Fhir.Model.Date _DateRecordedElement;
        
        /// <summary>
        /// When first entered
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DateRecorded
        {
            get { return DateRecordedElement != null ? DateRecordedElement.Value : null; }
            set
            {
                if (value == null)
                    DateRecordedElement = null;
                else
                    DateRecordedElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("DateRecorded");
            }
        }
        
        /// <summary>
        /// Identification of the condition, problem or diagnosis
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// complaint | symptom | finding | diagnosis
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// active | relapse | remission | resolved
        /// </summary>
        [FhirElement("clinicalStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Code ClinicalStatusElement
        {
            get { return _ClinicalStatusElement; }
            set { _ClinicalStatusElement = value; OnPropertyChanged("ClinicalStatusElement"); }
        }
        
        private Hl7.Fhir.Model.Code _ClinicalStatusElement;
        
        /// <summary>
        /// active | relapse | remission | resolved
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ClinicalStatus
        {
            get { return ClinicalStatusElement != null ? ClinicalStatusElement.Value : null; }
            set
            {
                if (value == null)
                    ClinicalStatusElement = null;
                else
                    ClinicalStatusElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("ClinicalStatus");
            }
        }
        
        /// <summary>
        /// provisional | differential | confirmed | refuted | entered-in-error | unknown
        /// </summary>
        [FhirElement("verificationStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ConditionVerificationStatus> VerificationStatusElement
        {
            get { return _VerificationStatusElement; }
            set { _VerificationStatusElement = value; OnPropertyChanged("VerificationStatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ConditionVerificationStatus> _VerificationStatusElement;
        
        /// <summary>
        /// provisional | differential | confirmed | refuted | entered-in-error | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ConditionVerificationStatus? VerificationStatus
        {
            get { return VerificationStatusElement != null ? VerificationStatusElement.Value : null; }
            set
            {
                if (value == null)
                    VerificationStatusElement = null;
                else
                    VerificationStatusElement = new Code<Hl7.Fhir.Model.ConditionVerificationStatus>(value);
                OnPropertyChanged("VerificationStatus");
            }
        }
        
        /// <summary>
        /// Subjective severity of condition
        /// </summary>
        [FhirElement("severity", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
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
        [FhirElement("onset", InSummary=Hl7.Fhir.Model.Version.All, Order=190, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.DSTU2.Age),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
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
        [FhirElement("abatement", InSummary=Hl7.Fhir.Model.Version.All, Order=200, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.DSTU2.Age),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
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
        [FhirElement("stage", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public StageComponent Stage
        {
            get { return _Stage; }
            set { _Stage = value; OnPropertyChanged("Stage"); }
        }
        
        private StageComponent _Stage;
        
        /// <summary>
        /// Supporting evidence
        /// </summary>
        [FhirElement("evidence", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<EvidenceComponent> Evidence
        {
            get { if(_Evidence==null) _Evidence = new List<EvidenceComponent>(); return _Evidence; }
            set { _Evidence = value; OnPropertyChanged("Evidence"); }
        }
        
        private List<EvidenceComponent> _Evidence;
        
        /// <summary>
        /// Anatomical location, if relevant
        /// </summary>
        [FhirElement("bodySite", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> BodySite
        {
            get { if(_BodySite==null) _BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(); return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _BodySite;
        
        /// <summary>
        /// Additional information about the Condition
        /// </summary>
        [FhirElement("notes", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
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
                if (value == null)
                    NotesElement = null;
                else
                    NotesElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Notes");
            }
        }
    
    
        public static ElementDefinitionConstraint[] Condition_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "con-1",
                severity: ConstraintSeverity.Warning,
                expression: "stage.all(summary or assessment)",
                human: "Stage SHALL have summary or assessment",
                xpath: "exists(f:summary) or exists(f:assessment)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "con-2",
                severity: ConstraintSeverity.Warning,
                expression: "evidence.all(code or detail)",
                human: "evidence SHALL have code or details",
                xpath: "exists(f:code) or exists(f:detail)"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Condition_Constraints);
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
                if(DateRecordedElement != null) dest.DateRecordedElement = (Hl7.Fhir.Model.Date)DateRecordedElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(ClinicalStatusElement != null) dest.ClinicalStatusElement = (Hl7.Fhir.Model.Code)ClinicalStatusElement.DeepCopy();
                if(VerificationStatusElement != null) dest.VerificationStatusElement = (Code<Hl7.Fhir.Model.ConditionVerificationStatus>)VerificationStatusElement.DeepCopy();
                if(Severity != null) dest.Severity = (Hl7.Fhir.Model.CodeableConcept)Severity.DeepCopy();
                if(Onset != null) dest.Onset = (Hl7.Fhir.Model.Element)Onset.DeepCopy();
                if(Abatement != null) dest.Abatement = (Hl7.Fhir.Model.Element)Abatement.DeepCopy();
                if(Stage != null) dest.Stage = (StageComponent)Stage.DeepCopy();
                if(Evidence != null) dest.Evidence = new List<EvidenceComponent>(Evidence.DeepCopy());
                if(BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(BodySite.DeepCopy());
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
            if( !DeepComparable.Matches(DateRecordedElement, otherT.DateRecordedElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(ClinicalStatusElement, otherT.ClinicalStatusElement)) return false;
            if( !DeepComparable.Matches(VerificationStatusElement, otherT.VerificationStatusElement)) return false;
            if( !DeepComparable.Matches(Severity, otherT.Severity)) return false;
            if( !DeepComparable.Matches(Onset, otherT.Onset)) return false;
            if( !DeepComparable.Matches(Abatement, otherT.Abatement)) return false;
            if( !DeepComparable.Matches(Stage, otherT.Stage)) return false;
            if( !DeepComparable.Matches(Evidence, otherT.Evidence)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
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
            if( !DeepComparable.IsExactly(DateRecordedElement, otherT.DateRecordedElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(ClinicalStatusElement, otherT.ClinicalStatusElement)) return false;
            if( !DeepComparable.IsExactly(VerificationStatusElement, otherT.VerificationStatusElement)) return false;
            if( !DeepComparable.IsExactly(Severity, otherT.Severity)) return false;
            if( !DeepComparable.IsExactly(Onset, otherT.Onset)) return false;
            if( !DeepComparable.IsExactly(Abatement, otherT.Abatement)) return false;
            if( !DeepComparable.IsExactly(Stage, otherT.Stage)) return false;
            if( !DeepComparable.IsExactly(Evidence, otherT.Evidence)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Condition");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Patient?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Encounter?.Serialize(sink);
            sink.Element("asserter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Asserter?.Serialize(sink);
            sink.Element("dateRecorded", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateRecordedElement?.Serialize(sink);
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
            sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Category?.Serialize(sink);
            sink.Element("clinicalStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ClinicalStatusElement?.Serialize(sink);
            sink.Element("verificationStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); VerificationStatusElement?.Serialize(sink);
            sink.Element("severity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Severity?.Serialize(sink);
            sink.Element("onset", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Onset?.Serialize(sink);
            sink.Element("abatement", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Abatement?.Serialize(sink);
            sink.Element("stage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Stage?.Serialize(sink);
            sink.BeginList("evidence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Evidence)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("bodySite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in BodySite)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("notes", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NotesElement?.Serialize(sink);
            sink.End();
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "asserter":
                    Asserter = source.Populate(Asserter);
                    return true;
                case "dateRecorded":
                    DateRecordedElement = source.PopulateValue(DateRecordedElement);
                    return true;
                case "_dateRecorded":
                    DateRecordedElement = source.Populate(DateRecordedElement);
                    return true;
                case "code":
                    Code = source.Populate(Code);
                    return true;
                case "category":
                    Category = source.Populate(Category);
                    return true;
                case "clinicalStatus":
                    ClinicalStatusElement = source.PopulateValue(ClinicalStatusElement);
                    return true;
                case "_clinicalStatus":
                    ClinicalStatusElement = source.Populate(ClinicalStatusElement);
                    return true;
                case "verificationStatus":
                    VerificationStatusElement = source.PopulateValue(VerificationStatusElement);
                    return true;
                case "_verificationStatus":
                    VerificationStatusElement = source.Populate(VerificationStatusElement);
                    return true;
                case "severity":
                    Severity = source.Populate(Severity);
                    return true;
                case "onsetDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Onset, "onset");
                    Onset = source.PopulateValue(Onset as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_onsetDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Onset, "onset");
                    Onset = source.Populate(Onset as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "onsetAge":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Age>(Onset, "onset");
                    Onset = source.Populate(Onset as Hl7.Fhir.Model.DSTU2.Age);
                    return true;
                case "onsetPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Onset, "onset");
                    Onset = source.Populate(Onset as Hl7.Fhir.Model.Period);
                    return true;
                case "onsetRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Onset, "onset");
                    Onset = source.Populate(Onset as Hl7.Fhir.Model.Range);
                    return true;
                case "onsetString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Onset, "onset");
                    Onset = source.PopulateValue(Onset as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_onsetString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Onset, "onset");
                    Onset = source.Populate(Onset as Hl7.Fhir.Model.FhirString);
                    return true;
                case "abatementDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Abatement, "abatement");
                    Abatement = source.PopulateValue(Abatement as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_abatementDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Abatement, "abatement");
                    Abatement = source.Populate(Abatement as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "abatementAge":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Age>(Abatement, "abatement");
                    Abatement = source.Populate(Abatement as Hl7.Fhir.Model.DSTU2.Age);
                    return true;
                case "abatementBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Abatement, "abatement");
                    Abatement = source.PopulateValue(Abatement as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "_abatementBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Abatement, "abatement");
                    Abatement = source.Populate(Abatement as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "abatementPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Abatement, "abatement");
                    Abatement = source.Populate(Abatement as Hl7.Fhir.Model.Period);
                    return true;
                case "abatementRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Abatement, "abatement");
                    Abatement = source.Populate(Abatement as Hl7.Fhir.Model.Range);
                    return true;
                case "abatementString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Abatement, "abatement");
                    Abatement = source.PopulateValue(Abatement as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_abatementString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Abatement, "abatement");
                    Abatement = source.Populate(Abatement as Hl7.Fhir.Model.FhirString);
                    return true;
                case "stage":
                    Stage = source.Populate(Stage);
                    return true;
                case "evidence":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "bodySite":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "notes":
                    NotesElement = source.PopulateValue(NotesElement);
                    return true;
                case "_notes":
                    NotesElement = source.Populate(NotesElement);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "evidence":
                    source.PopulateListItem(Evidence, index);
                    return true;
                case "bodySite":
                    source.PopulateListItem(BodySite, index);
                    return true;
            }
            return false;
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (Patient != null) yield return Patient;
                if (Encounter != null) yield return Encounter;
                if (Asserter != null) yield return Asserter;
                if (DateRecordedElement != null) yield return DateRecordedElement;
                if (Code != null) yield return Code;
                if (Category != null) yield return Category;
                if (ClinicalStatusElement != null) yield return ClinicalStatusElement;
                if (VerificationStatusElement != null) yield return VerificationStatusElement;
                if (Severity != null) yield return Severity;
                if (Onset != null) yield return Onset;
                if (Abatement != null) yield return Abatement;
                if (Stage != null) yield return Stage;
                foreach (var elem in Evidence) { if (elem != null) yield return elem; }
                foreach (var elem in BodySite) { if (elem != null) yield return elem; }
                if (NotesElement != null) yield return NotesElement;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (Asserter != null) yield return new ElementValue("asserter", Asserter);
                if (DateRecordedElement != null) yield return new ElementValue("dateRecorded", DateRecordedElement);
                if (Code != null) yield return new ElementValue("code", Code);
                if (Category != null) yield return new ElementValue("category", Category);
                if (ClinicalStatusElement != null) yield return new ElementValue("clinicalStatus", ClinicalStatusElement);
                if (VerificationStatusElement != null) yield return new ElementValue("verificationStatus", VerificationStatusElement);
                if (Severity != null) yield return new ElementValue("severity", Severity);
                if (Onset != null) yield return new ElementValue("onset", Onset);
                if (Abatement != null) yield return new ElementValue("abatement", Abatement);
                if (Stage != null) yield return new ElementValue("stage", Stage);
                foreach (var elem in Evidence) { if (elem != null) yield return new ElementValue("evidence", elem); }
                foreach (var elem in BodySite) { if (elem != null) yield return new ElementValue("bodySite", elem); }
                if (NotesElement != null) yield return new ElementValue("notes", NotesElement);
            }
        }
    
    }

}
