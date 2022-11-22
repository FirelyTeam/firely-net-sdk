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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// Information about patient's relatives, relevant for patient
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "FamilyMemberHistory", IsResource=true)]
    [DataContract]
    public partial class FamilyMemberHistory : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IFamilyMemberHistory, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.FamilyMemberHistory; } }
        [NotMapped]
        public override string TypeName { get { return "FamilyMemberHistory"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ConditionComponent")]
        [DataContract]
        public partial class ConditionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IFamilyMemberHistoryConditionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
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
            /// Whether the condition contributed to the cause of death
            /// </summary>
            [FhirElement("contributedToDeath", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ContributedToDeathElement
            {
                get { return _ContributedToDeathElement; }
                set { _ContributedToDeathElement = value; OnPropertyChanged("ContributedToDeathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ContributedToDeathElement;
            
            /// <summary>
            /// Whether the condition contributed to the cause of death
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? ContributedToDeath
            {
                get { return ContributedToDeathElement != null ? ContributedToDeathElement.Value : null; }
                set
                {
                    if (value == null)
                        ContributedToDeathElement = null;
                    else
                        ContributedToDeathElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ContributedToDeath");
                }
            }
            
            /// <summary>
            /// When condition first manifested
            /// </summary>
            [FhirElement("onset", Order=70, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.R4.Age),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.FhirString))]
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
            [FhirElement("note", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Annotation> Note
            {
                get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
                set { _Note = value; OnPropertyChanged("Note"); }
            }
            
            private List<Hl7.Fhir.Model.Annotation> _Note;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ConditionComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Code?.Serialize(sink);
                sink.Element("outcome", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Outcome?.Serialize(sink);
                sink.Element("contributedToDeath", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ContributedToDeathElement?.Serialize(sink);
                sink.Element("onset", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Onset?.Serialize(sink);
                sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Note)
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
                    case "outcome":
                        Outcome = source.Populate(Outcome);
                        return true;
                    case "contributedToDeath":
                        ContributedToDeathElement = source.PopulateValue(ContributedToDeathElement);
                        return true;
                    case "_contributedToDeath":
                        ContributedToDeathElement = source.Populate(ContributedToDeathElement);
                        return true;
                    case "onsetAge":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Age>(Onset, "onset");
                        Onset = source.Populate(Onset as Hl7.Fhir.Model.R4.Age);
                        return true;
                    case "onsetRange":
                        source.CheckDuplicates<Hl7.Fhir.Model.Range>(Onset, "onset");
                        Onset = source.Populate(Onset as Hl7.Fhir.Model.Range);
                        return true;
                    case "onsetPeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Onset, "onset");
                        Onset = source.Populate(Onset as Hl7.Fhir.Model.Period);
                        return true;
                    case "onsetString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Onset, "onset");
                        Onset = source.PopulateValue(Onset as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_onsetString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Onset, "onset");
                        Onset = source.Populate(Onset as Hl7.Fhir.Model.FhirString);
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
                    case "note":
                        source.PopulateListItem(Note, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConditionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                    if(ContributedToDeathElement != null) dest.ContributedToDeathElement = (Hl7.Fhir.Model.FhirBoolean)ContributedToDeathElement.DeepCopy();
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
                if( !DeepComparable.Matches(ContributedToDeathElement, otherT.ContributedToDeathElement)) return false;
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
                if( !DeepComparable.IsExactly(ContributedToDeathElement, otherT.ContributedToDeathElement)) return false;
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
                    if (ContributedToDeathElement != null) yield return ContributedToDeathElement;
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
                    if (ContributedToDeathElement != null) yield return new ElementValue("contributedToDeath", ContributedToDeathElement);
                    if (Onset != null) yield return new ElementValue("onset", Onset);
                    foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IFamilyMemberHistoryConditionComponent> Hl7.Fhir.Model.IFamilyMemberHistory.Condition { get { return Condition; } }
    
        
        /// <summary>
        /// External Id(s) for this record
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
        /// Instantiates FHIR protocol or definition
        /// </summary>
        [FhirElement("instantiatesCanonical", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> InstantiatesCanonicalElement
        {
            get { if(_InstantiatesCanonicalElement==null) _InstantiatesCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(); return _InstantiatesCanonicalElement; }
            set { _InstantiatesCanonicalElement = value; OnPropertyChanged("InstantiatesCanonicalElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _InstantiatesCanonicalElement;
        
        /// <summary>
        /// Instantiates FHIR protocol or definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> InstantiatesCanonical
        {
            get { return InstantiatesCanonicalElement != null ? InstantiatesCanonicalElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    InstantiatesCanonicalElement = null;
                else
                    InstantiatesCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("InstantiatesCanonical");
            }
        }
        
        /// <summary>
        /// Instantiates external protocol or definition
        /// </summary>
        [FhirElement("instantiatesUri", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> InstantiatesUriElement
        {
            get { if(_InstantiatesUriElement==null) _InstantiatesUriElement = new List<Hl7.Fhir.Model.FhirUri>(); return _InstantiatesUriElement; }
            set { _InstantiatesUriElement = value; OnPropertyChanged("InstantiatesUriElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _InstantiatesUriElement;
        
        /// <summary>
        /// Instantiates external protocol or definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> InstantiatesUri
        {
            get { return InstantiatesUriElement != null ? InstantiatesUriElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    InstantiatesUriElement = null;
                else
                    InstantiatesUriElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("InstantiatesUri");
            }
        }
        
        /// <summary>
        /// partial | completed | entered-in-error | health-unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.FamilyHistoryStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.FamilyHistoryStatus> _StatusElement;
        
        /// <summary>
        /// partial | completed | entered-in-error | health-unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.FamilyHistoryStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.FamilyHistoryStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// subject-unknown | withheld | unable-to-obtain | deferred
        /// </summary>
        [FhirElement("dataAbsentReason", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept DataAbsentReason
        {
            get { return _DataAbsentReason; }
            set { _DataAbsentReason = value; OnPropertyChanged("DataAbsentReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _DataAbsentReason;
        
        /// <summary>
        /// Patient history is about
        /// </summary>
        [FhirElement("patient", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
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
        /// When history was recorded or last updated
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// When history was recorded or last updated
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
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
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
        [FhirElement("relationship", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
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
        [FhirElement("sex", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Sex
        {
            get { return _Sex; }
            set { _Sex = value; OnPropertyChanged("Sex"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Sex;
        
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
        [FhirElement("age", InSummary=Hl7.Fhir.Model.Version.All, Order=200, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.R4.Age),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
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
        [FhirElement("estimatedAge", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
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
                if (value == null)
                    EstimatedAgeElement = null;
                else
                    EstimatedAgeElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("EstimatedAge");
            }
        }
        
        /// <summary>
        /// Dead? How old/when?
        /// </summary>
        [FhirElement("deceased", InSummary=Hl7.Fhir.Model.Version.All, Order=220, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.R4.Age),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirString))]
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
        [FhirElement("reasonCode", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
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
        [FhirElement("reasonReference", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [References("Condition","Observation","AllergyIntolerance","QuestionnaireResponse","DiagnosticReport","DocumentReference")]
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
        public List<ConditionComponent> Condition
        {
            get { if(_Condition==null) _Condition = new List<ConditionComponent>(); return _Condition; }
            set { _Condition = value; OnPropertyChanged("Condition"); }
        }
        
        private List<ConditionComponent> _Condition;
    
    
        public static ElementDefinitionConstraint[] FamilyMemberHistory_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "fhs-2",
                severity: ConstraintSeverity.Warning,
                expression: "age.exists() or estimatedAge.empty()",
                human: "Can only have estimatedAge if age[x] is present",
                xpath: "exists(*[starts-with(local-name(.), 'age')]) or not(exists(f:estimatedAge))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "fhs-1",
                severity: ConstraintSeverity.Warning,
                expression: "age.empty() or born.empty()",
                human: "Can have age[x] or born[x], but not both",
                xpath: "not (*[starts-with(local-name(.), 'age')] and *[starts-with(local-name(.), 'birth')])"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(FamilyMemberHistory_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as FamilyMemberHistory;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(InstantiatesCanonicalElement != null) dest.InstantiatesCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(InstantiatesCanonicalElement.DeepCopy());
                if(InstantiatesUriElement != null) dest.InstantiatesUriElement = new List<Hl7.Fhir.Model.FhirUri>(InstantiatesUriElement.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FamilyHistoryStatus>)StatusElement.DeepCopy();
                if(DataAbsentReason != null) dest.DataAbsentReason = (Hl7.Fhir.Model.CodeableConcept)DataAbsentReason.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.CodeableConcept)Relationship.DeepCopy();
                if(Sex != null) dest.Sex = (Hl7.Fhir.Model.CodeableConcept)Sex.DeepCopy();
                if(Born != null) dest.Born = (Hl7.Fhir.Model.Element)Born.DeepCopy();
                if(Age != null) dest.Age = (Hl7.Fhir.Model.Element)Age.DeepCopy();
                if(EstimatedAgeElement != null) dest.EstimatedAgeElement = (Hl7.Fhir.Model.FhirBoolean)EstimatedAgeElement.DeepCopy();
                if(Deceased != null) dest.Deceased = (Hl7.Fhir.Model.Element)Deceased.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Condition != null) dest.Condition = new List<ConditionComponent>(Condition.DeepCopy());
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
            if( !DeepComparable.Matches(InstantiatesCanonicalElement, otherT.InstantiatesCanonicalElement)) return false;
            if( !DeepComparable.Matches(InstantiatesUriElement, otherT.InstantiatesUriElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(DataAbsentReason, otherT.DataAbsentReason)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
            if( !DeepComparable.Matches(Sex, otherT.Sex)) return false;
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
            if( !DeepComparable.IsExactly(InstantiatesCanonicalElement, otherT.InstantiatesCanonicalElement)) return false;
            if( !DeepComparable.IsExactly(InstantiatesUriElement, otherT.InstantiatesUriElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(DataAbsentReason, otherT.DataAbsentReason)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
            if( !DeepComparable.IsExactly(Sex, otherT.Sex)) return false;
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
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("FamilyMemberHistory");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("instantiatesCanonical", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(InstantiatesCanonicalElement);
            sink.End();
            sink.BeginList("instantiatesUri", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(InstantiatesUriElement);
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("dataAbsentReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DataAbsentReason?.Serialize(sink);
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Patient?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("relationship", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Relationship?.Serialize(sink);
            sink.Element("sex", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Sex?.Serialize(sink);
            sink.Element("born", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Born?.Serialize(sink);
            sink.Element("age", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Age?.Serialize(sink);
            sink.Element("estimatedAge", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EstimatedAgeElement?.Serialize(sink);
            sink.Element("deceased", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Deceased?.Serialize(sink);
            sink.BeginList("reasonCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ReasonCode)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("reasonReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ReasonReference)
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
            sink.BeginList("condition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Condition)
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
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "instantiatesCanonical":
                case "_instantiatesCanonical":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "instantiatesUri":
                case "_instantiatesUri":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "dataAbsentReason":
                    DataAbsentReason = source.Populate(DataAbsentReason);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
                    return true;
                case "relationship":
                    Relationship = source.Populate(Relationship);
                    return true;
                case "sex":
                    Sex = source.Populate(Sex);
                    return true;
                case "bornPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Born, "born");
                    Born = source.Populate(Born as Hl7.Fhir.Model.Period);
                    return true;
                case "bornDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Born, "born");
                    Born = source.PopulateValue(Born as Hl7.Fhir.Model.Date);
                    return true;
                case "_bornDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Born, "born");
                    Born = source.Populate(Born as Hl7.Fhir.Model.Date);
                    return true;
                case "bornString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Born, "born");
                    Born = source.PopulateValue(Born as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_bornString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Born, "born");
                    Born = source.Populate(Born as Hl7.Fhir.Model.FhirString);
                    return true;
                case "ageAge":
                    source.CheckDuplicates<Hl7.Fhir.Model.R4.Age>(Age, "age");
                    Age = source.Populate(Age as Hl7.Fhir.Model.R4.Age);
                    return true;
                case "ageRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Age, "age");
                    Age = source.Populate(Age as Hl7.Fhir.Model.Range);
                    return true;
                case "ageString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Age, "age");
                    Age = source.PopulateValue(Age as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_ageString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Age, "age");
                    Age = source.Populate(Age as Hl7.Fhir.Model.FhirString);
                    return true;
                case "estimatedAge":
                    EstimatedAgeElement = source.PopulateValue(EstimatedAgeElement);
                    return true;
                case "_estimatedAge":
                    EstimatedAgeElement = source.Populate(EstimatedAgeElement);
                    return true;
                case "deceasedBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Deceased, "deceased");
                    Deceased = source.PopulateValue(Deceased as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "_deceasedBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Deceased, "deceased");
                    Deceased = source.Populate(Deceased as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "deceasedAge":
                    source.CheckDuplicates<Hl7.Fhir.Model.R4.Age>(Deceased, "deceased");
                    Deceased = source.Populate(Deceased as Hl7.Fhir.Model.R4.Age);
                    return true;
                case "deceasedRange":
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Deceased, "deceased");
                    Deceased = source.Populate(Deceased as Hl7.Fhir.Model.Range);
                    return true;
                case "deceasedDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Deceased, "deceased");
                    Deceased = source.PopulateValue(Deceased as Hl7.Fhir.Model.Date);
                    return true;
                case "_deceasedDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Deceased, "deceased");
                    Deceased = source.Populate(Deceased as Hl7.Fhir.Model.Date);
                    return true;
                case "deceasedString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Deceased, "deceased");
                    Deceased = source.PopulateValue(Deceased as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_deceasedString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Deceased, "deceased");
                    Deceased = source.Populate(Deceased as Hl7.Fhir.Model.FhirString);
                    return true;
                case "reasonCode":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonReference":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "condition":
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
                case "instantiatesCanonical":
                    source.PopulatePrimitiveListItemValue(InstantiatesCanonicalElement, index);
                    return true;
                case "_instantiatesCanonical":
                    source.PopulatePrimitiveListItem(InstantiatesCanonicalElement, index);
                    return true;
                case "instantiatesUri":
                    source.PopulatePrimitiveListItemValue(InstantiatesUriElement, index);
                    return true;
                case "_instantiatesUri":
                    source.PopulatePrimitiveListItem(InstantiatesUriElement, index);
                    return true;
                case "reasonCode":
                    source.PopulateListItem(ReasonCode, index);
                    return true;
                case "reasonReference":
                    source.PopulateListItem(ReasonReference, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "condition":
                    source.PopulateListItem(Condition, index);
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
                foreach (var elem in InstantiatesCanonicalElement) { if (elem != null) yield return elem; }
                foreach (var elem in InstantiatesUriElement) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                if (DataAbsentReason != null) yield return DataAbsentReason;
                if (Patient != null) yield return Patient;
                if (DateElement != null) yield return DateElement;
                if (NameElement != null) yield return NameElement;
                if (Relationship != null) yield return Relationship;
                if (Sex != null) yield return Sex;
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
                foreach (var elem in InstantiatesCanonicalElement) { if (elem != null) yield return new ElementValue("instantiatesCanonical", elem); }
                foreach (var elem in InstantiatesUriElement) { if (elem != null) yield return new ElementValue("instantiatesUri", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (DataAbsentReason != null) yield return new ElementValue("dataAbsentReason", DataAbsentReason);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (Relationship != null) yield return new ElementValue("relationship", Relationship);
                if (Sex != null) yield return new ElementValue("sex", Sex);
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
