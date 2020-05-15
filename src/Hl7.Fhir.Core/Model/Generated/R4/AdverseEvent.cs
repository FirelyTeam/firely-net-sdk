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
    /// Medical care, research study or other healthcare event causing physical injury
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "AdverseEvent", IsResource=true)]
    [DataContract]
    public partial class AdverseEvent : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IAdverseEvent, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.AdverseEvent; } }
        [NotMapped]
        public override string TypeName { get { return "AdverseEvent"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SuspectEntityComponent")]
        [DataContract]
        public partial class SuspectEntityComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IAdverseEventSuspectEntityComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SuspectEntityComponent"; } }
            
            /// <summary>
            /// Refers to the specific entity that caused the adverse event
            /// </summary>
            [FhirElement("instance", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [References("Immunization","Procedure","Substance","Medication","MedicationAdministration","MedicationStatement","Device")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Instance
            {
                get { return _Instance; }
                set { _Instance = value; OnPropertyChanged("Instance"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Instance;
            
            /// <summary>
            /// Information on the possible cause of the event
            /// </summary>
            [FhirElement("causality", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<CausalityComponent> Causality
            {
                get { if(_Causality==null) _Causality = new List<CausalityComponent>(); return _Causality; }
                set { _Causality = value; OnPropertyChanged("Causality"); }
            }
            
            private List<CausalityComponent> _Causality;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SuspectEntityComponent");
                base.Serialize(sink);
                sink.Element("instance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Instance?.Serialize(sink);
                sink.BeginList("causality", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Causality)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SuspectEntityComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Instance != null) dest.Instance = (Hl7.Fhir.Model.ResourceReference)Instance.DeepCopy();
                    if(Causality != null) dest.Causality = new List<CausalityComponent>(Causality.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SuspectEntityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SuspectEntityComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Instance, otherT.Instance)) return false;
                if( !DeepComparable.Matches(Causality, otherT.Causality)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SuspectEntityComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Instance, otherT.Instance)) return false;
                if( !DeepComparable.IsExactly(Causality, otherT.Causality)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Instance != null) yield return Instance;
                    foreach (var elem in Causality) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Instance != null) yield return new ElementValue("instance", Instance);
                    foreach (var elem in Causality) { if (elem != null) yield return new ElementValue("causality", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "CausalityComponent")]
        [DataContract]
        public partial class CausalityComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CausalityComponent"; } }
            
            /// <summary>
            /// Assessment of if the entity caused the event
            /// </summary>
            [FhirElement("assessment", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Assessment
            {
                get { return _Assessment; }
                set { _Assessment = value; OnPropertyChanged("Assessment"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Assessment;
            
            /// <summary>
            /// AdverseEvent.suspectEntity.causalityProductRelatedness
            /// </summary>
            [FhirElement("productRelatedness", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ProductRelatednessElement
            {
                get { return _ProductRelatednessElement; }
                set { _ProductRelatednessElement = value; OnPropertyChanged("ProductRelatednessElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ProductRelatednessElement;
            
            /// <summary>
            /// AdverseEvent.suspectEntity.causalityProductRelatedness
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ProductRelatedness
            {
                get { return ProductRelatednessElement != null ? ProductRelatednessElement.Value : null; }
                set
                {
                    if (value == null)
                        ProductRelatednessElement = null;
                    else
                        ProductRelatednessElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ProductRelatedness");
                }
            }
            
            /// <summary>
            /// AdverseEvent.suspectEntity.causalityAuthor
            /// </summary>
            [FhirElement("author", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [References("Practitioner","PractitionerRole")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Author
            {
                get { return _Author; }
                set { _Author = value; OnPropertyChanged("Author"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Author;
            
            /// <summary>
            /// ProbabilityScale | Bayesian | Checklist
            /// </summary>
            [FhirElement("method", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method
            {
                get { return _Method; }
                set { _Method = value; OnPropertyChanged("Method"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Method;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CausalityComponent");
                base.Serialize(sink);
                sink.Element("assessment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Assessment?.Serialize(sink);
                sink.Element("productRelatedness", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ProductRelatednessElement?.Serialize(sink);
                sink.Element("author", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Author?.Serialize(sink);
                sink.Element("method", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Method?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CausalityComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Assessment != null) dest.Assessment = (Hl7.Fhir.Model.CodeableConcept)Assessment.DeepCopy();
                    if(ProductRelatednessElement != null) dest.ProductRelatednessElement = (Hl7.Fhir.Model.FhirString)ProductRelatednessElement.DeepCopy();
                    if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                    if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CausalityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CausalityComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Assessment, otherT.Assessment)) return false;
                if( !DeepComparable.Matches(ProductRelatednessElement, otherT.ProductRelatednessElement)) return false;
                if( !DeepComparable.Matches(Author, otherT.Author)) return false;
                if( !DeepComparable.Matches(Method, otherT.Method)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CausalityComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Assessment, otherT.Assessment)) return false;
                if( !DeepComparable.IsExactly(ProductRelatednessElement, otherT.ProductRelatednessElement)) return false;
                if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
                if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Assessment != null) yield return Assessment;
                    if (ProductRelatednessElement != null) yield return ProductRelatednessElement;
                    if (Author != null) yield return Author;
                    if (Method != null) yield return Method;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Assessment != null) yield return new ElementValue("assessment", Assessment);
                    if (ProductRelatednessElement != null) yield return new ElementValue("productRelatedness", ProductRelatednessElement);
                    if (Author != null) yield return new ElementValue("author", Author);
                    if (Method != null) yield return new ElementValue("method", Method);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IAdverseEventSuspectEntityComponent> Hl7.Fhir.Model.IAdverseEvent.SuspectEntity { get { return SuspectEntity; } }
    
        
        /// <summary>
        /// Business identifier for the event
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// actual | potential
        /// </summary>
        [FhirElement("actuality", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.AdverseEventActuality> ActualityElement
        {
            get { return _ActualityElement; }
            set { _ActualityElement = value; OnPropertyChanged("ActualityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.AdverseEventActuality> _ActualityElement;
        
        /// <summary>
        /// actual | potential
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.AdverseEventActuality? Actuality
        {
            get { return ActualityElement != null ? ActualityElement.Value : null; }
            set
            {
                if (value == null)
                    ActualityElement = null;
                else
                    ActualityElement = new Code<Hl7.Fhir.Model.R4.AdverseEventActuality>(value);
                OnPropertyChanged("Actuality");
            }
        }
        
        /// <summary>
        /// product-problem | product-quality | product-use-error | wrong-dose | incorrect-prescribing-information | wrong-technique | wrong-route-of-administration | wrong-rate | wrong-duration | wrong-time | expired-drug | medical-device-use-error | problem-different-manufacturer | unsafe-physical-environment
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// Type of the event itself in relation to the subject
        /// </summary>
        [FhirElement("event", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Event
        {
            get { return _Event; }
            set { _Event = value; OnPropertyChanged("Event"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Event;
        
        /// <summary>
        /// Subject impacted by event
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Patient","Group","Practitioner","RelatedPerson")]
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
        [FhirElement("encounter", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
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
        /// When the event occurred
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
        /// When the event occurred
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
        /// When the event was detected
        /// </summary>
        [FhirElement("detected", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DetectedElement
        {
            get { return _DetectedElement; }
            set { _DetectedElement = value; OnPropertyChanged("DetectedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DetectedElement;
        
        /// <summary>
        /// When the event was detected
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Detected
        {
            get { return DetectedElement != null ? DetectedElement.Value : null; }
            set
            {
                if (value == null)
                    DetectedElement = null;
                else
                    DetectedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Detected");
            }
        }
        
        /// <summary>
        /// When the event was recorded
        /// </summary>
        [FhirElement("recordedDate", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime RecordedDateElement
        {
            get { return _RecordedDateElement; }
            set { _RecordedDateElement = value; OnPropertyChanged("RecordedDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _RecordedDateElement;
        
        /// <summary>
        /// When the event was recorded
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
        /// Effect on the subject due to this event
        /// </summary>
        [FhirElement("resultingCondition", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [References("Condition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ResultingCondition
        {
            get { if(_ResultingCondition==null) _ResultingCondition = new List<Hl7.Fhir.Model.ResourceReference>(); return _ResultingCondition; }
            set { _ResultingCondition = value; OnPropertyChanged("ResultingCondition"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ResultingCondition;
        
        /// <summary>
        /// Location where adverse event occurred
        /// </summary>
        [FhirElement("location", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Location;
        
        /// <summary>
        /// Seriousness of the event
        /// </summary>
        [FhirElement("seriousness", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Seriousness
        {
            get { return _Seriousness; }
            set { _Seriousness = value; OnPropertyChanged("Seriousness"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Seriousness;
        
        /// <summary>
        /// mild | moderate | severe
        /// </summary>
        [FhirElement("severity", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Severity
        {
            get { return _Severity; }
            set { _Severity = value; OnPropertyChanged("Severity"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Severity;
        
        /// <summary>
        /// resolved | recovering | ongoing | resolvedWithSequelae | fatal | unknown
        /// </summary>
        [FhirElement("outcome", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Outcome
        {
            get { return _Outcome; }
            set { _Outcome = value; OnPropertyChanged("Outcome"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Outcome;
        
        /// <summary>
        /// Who recorded the adverse event
        /// </summary>
        [FhirElement("recorder", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [References("Patient","Practitioner","PractitionerRole","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recorder
        {
            get { return _Recorder; }
            set { _Recorder = value; OnPropertyChanged("Recorder"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Recorder;
        
        /// <summary>
        /// Who  was involved in the adverse event or the potential adverse event
        /// </summary>
        [FhirElement("contributor", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole","Device")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Contributor
        {
            get { if(_Contributor==null) _Contributor = new List<Hl7.Fhir.Model.ResourceReference>(); return _Contributor; }
            set { _Contributor = value; OnPropertyChanged("Contributor"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Contributor;
        
        /// <summary>
        /// The suspected agent causing the adverse event
        /// </summary>
        [FhirElement("suspectEntity", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SuspectEntityComponent> SuspectEntity
        {
            get { if(_SuspectEntity==null) _SuspectEntity = new List<SuspectEntityComponent>(); return _SuspectEntity; }
            set { _SuspectEntity = value; OnPropertyChanged("SuspectEntity"); }
        }
        
        private List<SuspectEntityComponent> _SuspectEntity;
        
        /// <summary>
        /// AdverseEvent.subjectMedicalHistory
        /// </summary>
        [FhirElement("subjectMedicalHistory", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [References("Condition","Observation","AllergyIntolerance","FamilyMemberHistory","Immunization","Procedure","Media","DocumentReference")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SubjectMedicalHistory
        {
            get { if(_SubjectMedicalHistory==null) _SubjectMedicalHistory = new List<Hl7.Fhir.Model.ResourceReference>(); return _SubjectMedicalHistory; }
            set { _SubjectMedicalHistory = value; OnPropertyChanged("SubjectMedicalHistory"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SubjectMedicalHistory;
        
        /// <summary>
        /// AdverseEvent.referenceDocument
        /// </summary>
        [FhirElement("referenceDocument", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [References("DocumentReference")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReferenceDocument
        {
            get { if(_ReferenceDocument==null) _ReferenceDocument = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReferenceDocument; }
            set { _ReferenceDocument = value; OnPropertyChanged("ReferenceDocument"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReferenceDocument;
        
        /// <summary>
        /// AdverseEvent.study
        /// </summary>
        [FhirElement("study", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [References("ResearchStudy")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Study
        {
            get { if(_Study==null) _Study = new List<Hl7.Fhir.Model.ResourceReference>(); return _Study; }
            set { _Study = value; OnPropertyChanged("Study"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Study;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as AdverseEvent;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(ActualityElement != null) dest.ActualityElement = (Code<Hl7.Fhir.Model.R4.AdverseEventActuality>)ActualityElement.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Event != null) dest.Event = (Hl7.Fhir.Model.CodeableConcept)Event.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(DetectedElement != null) dest.DetectedElement = (Hl7.Fhir.Model.FhirDateTime)DetectedElement.DeepCopy();
                if(RecordedDateElement != null) dest.RecordedDateElement = (Hl7.Fhir.Model.FhirDateTime)RecordedDateElement.DeepCopy();
                if(ResultingCondition != null) dest.ResultingCondition = new List<Hl7.Fhir.Model.ResourceReference>(ResultingCondition.DeepCopy());
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(Seriousness != null) dest.Seriousness = (Hl7.Fhir.Model.CodeableConcept)Seriousness.DeepCopy();
                if(Severity != null) dest.Severity = (Hl7.Fhir.Model.CodeableConcept)Severity.DeepCopy();
                if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                if(Recorder != null) dest.Recorder = (Hl7.Fhir.Model.ResourceReference)Recorder.DeepCopy();
                if(Contributor != null) dest.Contributor = new List<Hl7.Fhir.Model.ResourceReference>(Contributor.DeepCopy());
                if(SuspectEntity != null) dest.SuspectEntity = new List<SuspectEntityComponent>(SuspectEntity.DeepCopy());
                if(SubjectMedicalHistory != null) dest.SubjectMedicalHistory = new List<Hl7.Fhir.Model.ResourceReference>(SubjectMedicalHistory.DeepCopy());
                if(ReferenceDocument != null) dest.ReferenceDocument = new List<Hl7.Fhir.Model.ResourceReference>(ReferenceDocument.DeepCopy());
                if(Study != null) dest.Study = new List<Hl7.Fhir.Model.ResourceReference>(Study.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new AdverseEvent());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as AdverseEvent;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(ActualityElement, otherT.ActualityElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Event, otherT.Event)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(DetectedElement, otherT.DetectedElement)) return false;
            if( !DeepComparable.Matches(RecordedDateElement, otherT.RecordedDateElement)) return false;
            if( !DeepComparable.Matches(ResultingCondition, otherT.ResultingCondition)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(Seriousness, otherT.Seriousness)) return false;
            if( !DeepComparable.Matches(Severity, otherT.Severity)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.Matches(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.Matches(Contributor, otherT.Contributor)) return false;
            if( !DeepComparable.Matches(SuspectEntity, otherT.SuspectEntity)) return false;
            if( !DeepComparable.Matches(SubjectMedicalHistory, otherT.SubjectMedicalHistory)) return false;
            if( !DeepComparable.Matches(ReferenceDocument, otherT.ReferenceDocument)) return false;
            if( !DeepComparable.Matches(Study, otherT.Study)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as AdverseEvent;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(ActualityElement, otherT.ActualityElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(DetectedElement, otherT.DetectedElement)) return false;
            if( !DeepComparable.IsExactly(RecordedDateElement, otherT.RecordedDateElement)) return false;
            if( !DeepComparable.IsExactly(ResultingCondition, otherT.ResultingCondition)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(Seriousness, otherT.Seriousness)) return false;
            if( !DeepComparable.IsExactly(Severity, otherT.Severity)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.IsExactly(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.IsExactly(Contributor, otherT.Contributor)) return false;
            if( !DeepComparable.IsExactly(SuspectEntity, otherT.SuspectEntity)) return false;
            if( !DeepComparable.IsExactly(SubjectMedicalHistory, otherT.SubjectMedicalHistory)) return false;
            if( !DeepComparable.IsExactly(ReferenceDocument, otherT.ReferenceDocument)) return false;
            if( !DeepComparable.IsExactly(Study, otherT.Study)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("AdverseEvent");
            base.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("actuality", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ActualityElement?.Serialize(sink);
            sink.BeginList("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Category)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("event", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Event?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Encounter?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.Element("detected", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DetectedElement?.Serialize(sink);
            sink.Element("recordedDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RecordedDateElement?.Serialize(sink);
            sink.BeginList("resultingCondition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ResultingCondition)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Location?.Serialize(sink);
            sink.Element("seriousness", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Seriousness?.Serialize(sink);
            sink.Element("severity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Severity?.Serialize(sink);
            sink.Element("outcome", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Outcome?.Serialize(sink);
            sink.Element("recorder", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Recorder?.Serialize(sink);
            sink.BeginList("contributor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contributor)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("suspectEntity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in SuspectEntity)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("subjectMedicalHistory", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in SubjectMedicalHistory)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("referenceDocument", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ReferenceDocument)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("study", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Study)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (Identifier != null) yield return Identifier;
                if (ActualityElement != null) yield return ActualityElement;
                foreach (var elem in Category) { if (elem != null) yield return elem; }
                if (Event != null) yield return Event;
                if (Subject != null) yield return Subject;
                if (Encounter != null) yield return Encounter;
                if (DateElement != null) yield return DateElement;
                if (DetectedElement != null) yield return DetectedElement;
                if (RecordedDateElement != null) yield return RecordedDateElement;
                foreach (var elem in ResultingCondition) { if (elem != null) yield return elem; }
                if (Location != null) yield return Location;
                if (Seriousness != null) yield return Seriousness;
                if (Severity != null) yield return Severity;
                if (Outcome != null) yield return Outcome;
                if (Recorder != null) yield return Recorder;
                foreach (var elem in Contributor) { if (elem != null) yield return elem; }
                foreach (var elem in SuspectEntity) { if (elem != null) yield return elem; }
                foreach (var elem in SubjectMedicalHistory) { if (elem != null) yield return elem; }
                foreach (var elem in ReferenceDocument) { if (elem != null) yield return elem; }
                foreach (var elem in Study) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (ActualityElement != null) yield return new ElementValue("actuality", ActualityElement);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (Event != null) yield return new ElementValue("event", Event);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (DetectedElement != null) yield return new ElementValue("detected", DetectedElement);
                if (RecordedDateElement != null) yield return new ElementValue("recordedDate", RecordedDateElement);
                foreach (var elem in ResultingCondition) { if (elem != null) yield return new ElementValue("resultingCondition", elem); }
                if (Location != null) yield return new ElementValue("location", Location);
                if (Seriousness != null) yield return new ElementValue("seriousness", Seriousness);
                if (Severity != null) yield return new ElementValue("severity", Severity);
                if (Outcome != null) yield return new ElementValue("outcome", Outcome);
                if (Recorder != null) yield return new ElementValue("recorder", Recorder);
                foreach (var elem in Contributor) { if (elem != null) yield return new ElementValue("contributor", elem); }
                foreach (var elem in SuspectEntity) { if (elem != null) yield return new ElementValue("suspectEntity", elem); }
                foreach (var elem in SubjectMedicalHistory) { if (elem != null) yield return new ElementValue("subjectMedicalHistory", elem); }
                foreach (var elem in ReferenceDocument) { if (elem != null) yield return new ElementValue("referenceDocument", elem); }
                foreach (var elem in Study) { if (elem != null) yield return new ElementValue("study", elem); }
            }
        }
    
    }

}
