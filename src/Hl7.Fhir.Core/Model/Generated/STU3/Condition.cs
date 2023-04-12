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
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// Detailed information about conditions, problems or diagnoses
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "Condition", IsResource=true)]
    [DataContract]
    public partial class Condition : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ICondition, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Condition; } }
        [NotMapped]
        public override string TypeName { get { return "Condition"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "StageComponent")]
        [DataContract]
        public partial class StageComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IConditionStageComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("StageComponent");
                base.Serialize(sink);
                sink.Element("summary", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Summary?.Serialize(sink);
                sink.BeginList("assessment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Assessment)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "summary":
                        Summary = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "assessment":
                        Assessment = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                }
                return false;
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "EvidenceComponent")]
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
                sink.BeginList("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Code)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("detail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Detail)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "code":
                        Code = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "detail":
                        Detail = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                }
                return false;
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
                        source.SetList(this, jsonPropertyName);
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
                    case "code":
                        source.PopulateListItem(Code, index);
                        return true;
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
        /// active | recurrence | inactive | remission | resolved
        /// </summary>
        [FhirElement("clinicalStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.ConditionClinicalStatusCodes> ClinicalStatusElement
        {
            get { return _ClinicalStatusElement; }
            set { _ClinicalStatusElement = value; OnPropertyChanged("ClinicalStatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.ConditionClinicalStatusCodes> _ClinicalStatusElement;
        
        /// <summary>
        /// active | recurrence | inactive | remission | resolved
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.ConditionClinicalStatusCodes? ClinicalStatus
        {
            get { return ClinicalStatusElement != null ? ClinicalStatusElement.Value : null; }
            set
            {
                if (value == null)
                    ClinicalStatusElement = null;
                else
                    ClinicalStatusElement = new Code<Hl7.Fhir.Model.STU3.ConditionClinicalStatusCodes>(value);
                OnPropertyChanged("ClinicalStatus");
            }
        }
        
        /// <summary>
        /// provisional | differential | confirmed | refuted | entered-in-error | unknown
        /// </summary>
        [FhirElement("verificationStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
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
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
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
        [FhirElement("bodySite", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
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
        /// Who has the condition?
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
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
        /// Encounter or episode when condition first asserted
        /// </summary>
        [FhirElement("context", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// Estimated or actual date,  date-time, or age
        /// </summary>
        [FhirElement("onset", InSummary=Hl7.Fhir.Model.Version.All, Order=180, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.STU3.Age),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
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
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.STU3.Age),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Abatement
        {
            get { return _Abatement; }
            set { _Abatement = value; OnPropertyChanged("Abatement"); }
        }
        
        private Hl7.Fhir.Model.Element _Abatement;
        
        /// <summary>
        /// Date record was believed accurate
        /// </summary>
        [FhirElement("assertedDate", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AssertedDateElement
        {
            get { return _AssertedDateElement; }
            set { _AssertedDateElement = value; OnPropertyChanged("AssertedDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _AssertedDateElement;
        
        /// <summary>
        /// Date record was believed accurate
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AssertedDate
        {
            get { return AssertedDateElement != null ? AssertedDateElement.Value : null; }
            set
            {
                if (value == null)
                    AssertedDateElement = null;
                else
                    AssertedDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("AssertedDate");
            }
        }
        
        /// <summary>
        /// Person who asserts this condition
        /// </summary>
        [FhirElement("asserter", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [References("Practitioner","Patient","RelatedPerson")]
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
        [FhirElement("stage", Order=220)]
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
        [FhirElement("evidence", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<EvidenceComponent> Evidence
        {
            get { if(_Evidence==null) _Evidence = new List<EvidenceComponent>(); return _Evidence; }
            set { _Evidence = value; OnPropertyChanged("Evidence"); }
        }
        
        private List<EvidenceComponent> _Evidence;
        
        /// <summary>
        /// Additional information about the Condition
        /// </summary>
        [FhirElement("note", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
    
    
        public static ElementDefinitionConstraint[] Condition_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "con-4",
                severity: ConstraintSeverity.Warning,
                expression: "abatement.empty() or (abatement as boolean).not()  or clinicalStatus='resolved' or clinicalStatus='remission' or clinicalStatus='inactive'",
                human: "If condition is abated, then clinicalStatus must be either inactive, resolved, or remission",
                xpath: "not(f:abatementBoolean/@value=true() or (not(exists(f:abatementBoolean)) and exists(*[starts-with(local-name(.), 'abatement')])) or f:clinicalStatus/@value=('resolved', 'remission', 'inactive'))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "con-3",
                severity: ConstraintSeverity.Warning,
                expression: "verificationStatus='entered-in-error' or clinicalStatus.exists()",
                human: "Condition.clinicalStatus SHALL be present if verificationStatus is not entered-in-error",
                xpath: "f:verificationStatus/@value='entered-in-error' or exists(f:clinicalStatus)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "con-1",
                severity: ConstraintSeverity.Warning,
                expression: "stage.all(summary.exists() or assessment.exists())",
                human: "Stage SHALL have summary or assessment",
                xpath: "exists(f:summary) or exists(f:assessment)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "con-2",
                severity: ConstraintSeverity.Warning,
                expression: "evidence.all(code.exists() or detail.exists())",
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
                if(ClinicalStatusElement != null) dest.ClinicalStatusElement = (Code<Hl7.Fhir.Model.STU3.ConditionClinicalStatusCodes>)ClinicalStatusElement.DeepCopy();
                if(VerificationStatusElement != null) dest.VerificationStatusElement = (Code<Hl7.Fhir.Model.ConditionVerificationStatus>)VerificationStatusElement.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Severity != null) dest.Severity = (Hl7.Fhir.Model.CodeableConcept)Severity.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(BodySite.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(Onset != null) dest.Onset = (Hl7.Fhir.Model.Element)Onset.DeepCopy();
                if(Abatement != null) dest.Abatement = (Hl7.Fhir.Model.Element)Abatement.DeepCopy();
                if(AssertedDateElement != null) dest.AssertedDateElement = (Hl7.Fhir.Model.FhirDateTime)AssertedDateElement.DeepCopy();
                if(Asserter != null) dest.Asserter = (Hl7.Fhir.Model.ResourceReference)Asserter.DeepCopy();
                if(Stage != null) dest.Stage = (StageComponent)Stage.DeepCopy();
                if(Evidence != null) dest.Evidence = new List<EvidenceComponent>(Evidence.DeepCopy());
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
            if( !DeepComparable.Matches(ClinicalStatusElement, otherT.ClinicalStatusElement)) return false;
            if( !DeepComparable.Matches(VerificationStatusElement, otherT.VerificationStatusElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Severity, otherT.Severity)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(Onset, otherT.Onset)) return false;
            if( !DeepComparable.Matches(Abatement, otherT.Abatement)) return false;
            if( !DeepComparable.Matches(AssertedDateElement, otherT.AssertedDateElement)) return false;
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
            if( !DeepComparable.IsExactly(ClinicalStatusElement, otherT.ClinicalStatusElement)) return false;
            if( !DeepComparable.IsExactly(VerificationStatusElement, otherT.VerificationStatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Severity, otherT.Severity)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(Onset, otherT.Onset)) return false;
            if( !DeepComparable.IsExactly(Abatement, otherT.Abatement)) return false;
            if( !DeepComparable.IsExactly(AssertedDateElement, otherT.AssertedDateElement)) return false;
            if( !DeepComparable.IsExactly(Asserter, otherT.Asserter)) return false;
            if( !DeepComparable.IsExactly(Stage, otherT.Stage)) return false;
            if( !DeepComparable.IsExactly(Evidence, otherT.Evidence)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
        
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
            sink.Element("clinicalStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ClinicalStatusElement?.Serialize(sink);
            sink.Element("verificationStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VerificationStatusElement?.Serialize(sink);
            sink.BeginList("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Category)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("severity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Severity?.Serialize(sink);
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Code?.Serialize(sink);
            sink.BeginList("bodySite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in BodySite)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Context?.Serialize(sink);
            sink.Element("onset", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Onset?.Serialize(sink);
            sink.Element("abatement", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Abatement?.Serialize(sink);
            sink.Element("assertedDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AssertedDateElement?.Serialize(sink);
            sink.Element("asserter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Asserter?.Serialize(sink);
            sink.Element("stage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Stage?.Serialize(sink);
            sink.BeginList("evidence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Evidence)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "clinicalStatus":
                    ClinicalStatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.ConditionClinicalStatusCodes>>();
                    return true;
                case "verificationStatus":
                    VerificationStatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ConditionVerificationStatus>>();
                    return true;
                case "category":
                    Category = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "severity":
                    Severity = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "code":
                    Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "bodySite":
                    BodySite = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "context":
                    Context = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "onsetDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Onset, "onset");
                    Onset = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "onsetAge":
                    source.CheckDuplicates<Hl7.Fhir.Model.STU3.Age>(Onset, "onset");
                    Onset = source.Get<Hl7.Fhir.Model.STU3.Age>();
                    return true;
                case "onsetPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Onset, "onset");
                    Onset = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "onsetRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Onset, "onset");
                    Onset = source.Get<Hl7.Fhir.Model.Range>();
                    return true;
                case "onsetString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Onset, "onset");
                    Onset = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "abatementDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Abatement, "abatement");
                    Abatement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "abatementAge":
                    source.CheckDuplicates<Hl7.Fhir.Model.STU3.Age>(Abatement, "abatement");
                    Abatement = source.Get<Hl7.Fhir.Model.STU3.Age>();
                    return true;
                case "abatementBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Abatement, "abatement");
                    Abatement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "abatementPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Abatement, "abatement");
                    Abatement = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "abatementRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Abatement, "abatement");
                    Abatement = source.Get<Hl7.Fhir.Model.Range>();
                    return true;
                case "abatementString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Abatement, "abatement");
                    Abatement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "assertedDate":
                    AssertedDateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "asserter":
                    Asserter = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "stage":
                    Stage = source.Get<StageComponent>();
                    return true;
                case "evidence":
                    Evidence = source.GetList<EvidenceComponent>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
            }
            return false;
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
                case "category":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "severity":
                    Severity = source.Populate(Severity);
                    return true;
                case "code":
                    Code = source.Populate(Code);
                    return true;
                case "bodySite":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "context":
                    Context = source.Populate(Context);
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
                    source.CheckDuplicates<Hl7.Fhir.Model.STU3.Age>(Onset, "onset");
                    Onset = source.Populate(Onset as Hl7.Fhir.Model.STU3.Age);
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
                    source.CheckDuplicates<Hl7.Fhir.Model.STU3.Age>(Abatement, "abatement");
                    Abatement = source.Populate(Abatement as Hl7.Fhir.Model.STU3.Age);
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
                case "assertedDate":
                    AssertedDateElement = source.PopulateValue(AssertedDateElement);
                    return true;
                case "_assertedDate":
                    AssertedDateElement = source.Populate(AssertedDateElement);
                    return true;
                case "asserter":
                    Asserter = source.Populate(Asserter);
                    return true;
                case "stage":
                    Stage = source.Populate(Stage);
                    return true;
                case "evidence":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
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
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "category":
                    source.PopulateListItem(Category, index);
                    return true;
                case "bodySite":
                    source.PopulateListItem(BodySite, index);
                    return true;
                case "evidence":
                    source.PopulateListItem(Evidence, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
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
                if (ClinicalStatusElement != null) yield return ClinicalStatusElement;
                if (VerificationStatusElement != null) yield return VerificationStatusElement;
                foreach (var elem in Category) { if (elem != null) yield return elem; }
                if (Severity != null) yield return Severity;
                if (Code != null) yield return Code;
                foreach (var elem in BodySite) { if (elem != null) yield return elem; }
                if (Subject != null) yield return Subject;
                if (Context != null) yield return Context;
                if (Onset != null) yield return Onset;
                if (Abatement != null) yield return Abatement;
                if (AssertedDateElement != null) yield return AssertedDateElement;
                if (Asserter != null) yield return Asserter;
                if (Stage != null) yield return Stage;
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
                if (ClinicalStatusElement != null) yield return new ElementValue("clinicalStatus", ClinicalStatusElement);
                if (VerificationStatusElement != null) yield return new ElementValue("verificationStatus", VerificationStatusElement);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (Severity != null) yield return new ElementValue("severity", Severity);
                if (Code != null) yield return new ElementValue("code", Code);
                foreach (var elem in BodySite) { if (elem != null) yield return new ElementValue("bodySite", elem); }
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (Onset != null) yield return new ElementValue("onset", Onset);
                if (Abatement != null) yield return new ElementValue("abatement", Abatement);
                if (AssertedDateElement != null) yield return new ElementValue("assertedDate", AssertedDateElement);
                if (Asserter != null) yield return new ElementValue("asserter", Asserter);
                if (Stage != null) yield return new ElementValue("stage", Stage);
                foreach (var elem in Evidence) { if (elem != null) yield return new ElementValue("evidence", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
            }
        }
    
    }

}
