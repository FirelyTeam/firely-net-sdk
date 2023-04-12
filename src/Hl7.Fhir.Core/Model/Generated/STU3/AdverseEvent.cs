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
    /// Medical care, research study or other healthcare event causing physical injury
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "AdverseEvent", IsResource=true)]
    [DataContract]
    public partial class AdverseEvent : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IAdverseEvent, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.AdverseEvent; } }
        [NotMapped]
        public override string TypeName { get { return "AdverseEvent"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SuspectEntityComponent")]
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
            [References("Substance","Medication","MedicationAdministration","MedicationStatement","Device")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Instance
            {
                get { return _Instance; }
                set { _Instance = value; OnPropertyChanged("Instance"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Instance;
            
            /// <summary>
            /// causality1 | causality2
            /// </summary>
            [FhirElement("causality", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.AdverseEventCausality> CausalityElement
            {
                get { return _CausalityElement; }
                set { _CausalityElement = value; OnPropertyChanged("CausalityElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.AdverseEventCausality> _CausalityElement;
            
            /// <summary>
            /// causality1 | causality2
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.AdverseEventCausality? Causality
            {
                get { return CausalityElement != null ? CausalityElement.Value : null; }
                set
                {
                    if (value == null)
                        CausalityElement = null;
                    else
                        CausalityElement = new Code<Hl7.Fhir.Model.STU3.AdverseEventCausality>(value);
                    OnPropertyChanged("Causality");
                }
            }
            
            /// <summary>
            /// assess1 | assess2
            /// </summary>
            [FhirElement("causalityAssessment", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept CausalityAssessment
            {
                get { return _CausalityAssessment; }
                set { _CausalityAssessment = value; OnPropertyChanged("CausalityAssessment"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _CausalityAssessment;
            
            /// <summary>
            /// AdverseEvent.suspectEntity.causalityProductRelatedness
            /// </summary>
            [FhirElement("causalityProductRelatedness", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CausalityProductRelatednessElement
            {
                get { return _CausalityProductRelatednessElement; }
                set { _CausalityProductRelatednessElement = value; OnPropertyChanged("CausalityProductRelatednessElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CausalityProductRelatednessElement;
            
            /// <summary>
            /// AdverseEvent.suspectEntity.causalityProductRelatedness
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CausalityProductRelatedness
            {
                get { return CausalityProductRelatednessElement != null ? CausalityProductRelatednessElement.Value : null; }
                set
                {
                    if (value == null)
                        CausalityProductRelatednessElement = null;
                    else
                        CausalityProductRelatednessElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("CausalityProductRelatedness");
                }
            }
            
            /// <summary>
            /// method1 | method2
            /// </summary>
            [FhirElement("causalityMethod", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept CausalityMethod
            {
                get { return _CausalityMethod; }
                set { _CausalityMethod = value; OnPropertyChanged("CausalityMethod"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _CausalityMethod;
            
            /// <summary>
            /// AdverseEvent.suspectEntity.causalityAuthor
            /// </summary>
            [FhirElement("causalityAuthor", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [References("Practitioner","PractitionerRole")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference CausalityAuthor
            {
                get { return _CausalityAuthor; }
                set { _CausalityAuthor = value; OnPropertyChanged("CausalityAuthor"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _CausalityAuthor;
            
            /// <summary>
            /// result1 | result2
            /// </summary>
            [FhirElement("causalityResult", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept CausalityResult
            {
                get { return _CausalityResult; }
                set { _CausalityResult = value; OnPropertyChanged("CausalityResult"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _CausalityResult;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SuspectEntityComponent");
                base.Serialize(sink);
                sink.Element("instance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Instance?.Serialize(sink);
                sink.Element("causality", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CausalityElement?.Serialize(sink);
                sink.Element("causalityAssessment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CausalityAssessment?.Serialize(sink);
                sink.Element("causalityProductRelatedness", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CausalityProductRelatednessElement?.Serialize(sink);
                sink.Element("causalityMethod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CausalityMethod?.Serialize(sink);
                sink.Element("causalityAuthor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CausalityAuthor?.Serialize(sink);
                sink.Element("causalityResult", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CausalityResult?.Serialize(sink);
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
                    case "instance":
                        Instance = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "causality":
                        CausalityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.AdverseEventCausality>>();
                        return true;
                    case "causalityAssessment":
                        CausalityAssessment = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "causalityProductRelatedness":
                        CausalityProductRelatednessElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "causalityMethod":
                        CausalityMethod = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "causalityAuthor":
                        CausalityAuthor = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "causalityResult":
                        CausalityResult = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                    case "instance":
                        Instance = source.Populate(Instance);
                        return true;
                    case "causality":
                        CausalityElement = source.PopulateValue(CausalityElement);
                        return true;
                    case "_causality":
                        CausalityElement = source.Populate(CausalityElement);
                        return true;
                    case "causalityAssessment":
                        CausalityAssessment = source.Populate(CausalityAssessment);
                        return true;
                    case "causalityProductRelatedness":
                        CausalityProductRelatednessElement = source.PopulateValue(CausalityProductRelatednessElement);
                        return true;
                    case "_causalityProductRelatedness":
                        CausalityProductRelatednessElement = source.Populate(CausalityProductRelatednessElement);
                        return true;
                    case "causalityMethod":
                        CausalityMethod = source.Populate(CausalityMethod);
                        return true;
                    case "causalityAuthor":
                        CausalityAuthor = source.Populate(CausalityAuthor);
                        return true;
                    case "causalityResult":
                        CausalityResult = source.Populate(CausalityResult);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SuspectEntityComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Instance != null) dest.Instance = (Hl7.Fhir.Model.ResourceReference)Instance.DeepCopy();
                    if(CausalityElement != null) dest.CausalityElement = (Code<Hl7.Fhir.Model.STU3.AdverseEventCausality>)CausalityElement.DeepCopy();
                    if(CausalityAssessment != null) dest.CausalityAssessment = (Hl7.Fhir.Model.CodeableConcept)CausalityAssessment.DeepCopy();
                    if(CausalityProductRelatednessElement != null) dest.CausalityProductRelatednessElement = (Hl7.Fhir.Model.FhirString)CausalityProductRelatednessElement.DeepCopy();
                    if(CausalityMethod != null) dest.CausalityMethod = (Hl7.Fhir.Model.CodeableConcept)CausalityMethod.DeepCopy();
                    if(CausalityAuthor != null) dest.CausalityAuthor = (Hl7.Fhir.Model.ResourceReference)CausalityAuthor.DeepCopy();
                    if(CausalityResult != null) dest.CausalityResult = (Hl7.Fhir.Model.CodeableConcept)CausalityResult.DeepCopy();
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
                if( !DeepComparable.Matches(CausalityElement, otherT.CausalityElement)) return false;
                if( !DeepComparable.Matches(CausalityAssessment, otherT.CausalityAssessment)) return false;
                if( !DeepComparable.Matches(CausalityProductRelatednessElement, otherT.CausalityProductRelatednessElement)) return false;
                if( !DeepComparable.Matches(CausalityMethod, otherT.CausalityMethod)) return false;
                if( !DeepComparable.Matches(CausalityAuthor, otherT.CausalityAuthor)) return false;
                if( !DeepComparable.Matches(CausalityResult, otherT.CausalityResult)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SuspectEntityComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Instance, otherT.Instance)) return false;
                if( !DeepComparable.IsExactly(CausalityElement, otherT.CausalityElement)) return false;
                if( !DeepComparable.IsExactly(CausalityAssessment, otherT.CausalityAssessment)) return false;
                if( !DeepComparable.IsExactly(CausalityProductRelatednessElement, otherT.CausalityProductRelatednessElement)) return false;
                if( !DeepComparable.IsExactly(CausalityMethod, otherT.CausalityMethod)) return false;
                if( !DeepComparable.IsExactly(CausalityAuthor, otherT.CausalityAuthor)) return false;
                if( !DeepComparable.IsExactly(CausalityResult, otherT.CausalityResult)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Instance != null) yield return Instance;
                    if (CausalityElement != null) yield return CausalityElement;
                    if (CausalityAssessment != null) yield return CausalityAssessment;
                    if (CausalityProductRelatednessElement != null) yield return CausalityProductRelatednessElement;
                    if (CausalityMethod != null) yield return CausalityMethod;
                    if (CausalityAuthor != null) yield return CausalityAuthor;
                    if (CausalityResult != null) yield return CausalityResult;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Instance != null) yield return new ElementValue("instance", Instance);
                    if (CausalityElement != null) yield return new ElementValue("causality", CausalityElement);
                    if (CausalityAssessment != null) yield return new ElementValue("causalityAssessment", CausalityAssessment);
                    if (CausalityProductRelatednessElement != null) yield return new ElementValue("causalityProductRelatedness", CausalityProductRelatednessElement);
                    if (CausalityMethod != null) yield return new ElementValue("causalityMethod", CausalityMethod);
                    if (CausalityAuthor != null) yield return new ElementValue("causalityAuthor", CausalityAuthor);
                    if (CausalityResult != null) yield return new ElementValue("causalityResult", CausalityResult);
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
        /// AE | PAE <br/>
        /// An adverse event is an event that caused harm to a patient,  an adverse reaction is a something that is a subject-specific event that is a result of an exposure to a medication, food, device or environmental substance, a potential adverse event is something that occurred and that could have caused harm to a patient but did not
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.AdverseEventCategory> CategoryElement
        {
            get { return _CategoryElement; }
            set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.AdverseEventCategory> _CategoryElement;
        
        /// <summary>
        /// AE | PAE <br/>
        /// An adverse event is an event that caused harm to a patient,  an adverse reaction is a something that is a subject-specific event that is a result of an exposure to a medication, food, device or environmental substance, a potential adverse event is something that occurred and that could have caused harm to a patient but did not
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.AdverseEventCategory? Category
        {
            get { return CategoryElement != null ? CategoryElement.Value : null; }
            set
            {
                if (value == null)
                    CategoryElement = null;
                else
                    CategoryElement = new Code<Hl7.Fhir.Model.STU3.AdverseEventCategory>(value);
                OnPropertyChanged("Category");
            }
        }
        
        /// <summary>
        /// actual | potential
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Subject or group impacted by event
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("Patient","ResearchSubject","Medication","Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// When the event occurred
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
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
        /// Adverse Reaction Events linked to exposure to substance
        /// </summary>
        [FhirElement("reaction", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Condition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Reaction
        {
            get { if(_Reaction==null) _Reaction = new List<Hl7.Fhir.Model.ResourceReference>(); return _Reaction; }
            set { _Reaction = value; OnPropertyChanged("Reaction"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Reaction;
        
        /// <summary>
        /// Location where adverse event occurred
        /// </summary>
        [FhirElement("location", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
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
        /// Mild | Moderate | Severe
        /// </summary>
        [FhirElement("seriousness", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Seriousness
        {
            get { return _Seriousness; }
            set { _Seriousness = value; OnPropertyChanged("Seriousness"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Seriousness;
        
        /// <summary>
        /// resolved | recovering | ongoing | resolvedWithSequelae | fatal | unknown
        /// </summary>
        [FhirElement("outcome", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
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
        [FhirElement("recorder", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [References("Patient","Practitioner","RelatedPerson")]
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
        [FhirElement("eventParticipant", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [References("Practitioner","Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference EventParticipant
        {
            get { return _EventParticipant; }
            set { _EventParticipant = value; OnPropertyChanged("EventParticipant"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _EventParticipant;
        
        /// <summary>
        /// Description of the adverse event
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Description of the adverse event
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
        /// The suspected agent causing the adverse event
        /// </summary>
        [FhirElement("suspectEntity", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
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
        [FhirElement("subjectMedicalHistory", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [References("Condition","Observation","AllergyIntolerance","FamilyMemberHistory","Immunization","Procedure")]
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
        [FhirElement("referenceDocument", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
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
        [FhirElement("study", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
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
                if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.STU3.AdverseEventCategory>)CategoryElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Reaction != null) dest.Reaction = new List<Hl7.Fhir.Model.ResourceReference>(Reaction.DeepCopy());
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(Seriousness != null) dest.Seriousness = (Hl7.Fhir.Model.CodeableConcept)Seriousness.DeepCopy();
                if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                if(Recorder != null) dest.Recorder = (Hl7.Fhir.Model.ResourceReference)Recorder.DeepCopy();
                if(EventParticipant != null) dest.EventParticipant = (Hl7.Fhir.Model.ResourceReference)EventParticipant.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
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
            if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Reaction, otherT.Reaction)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(Seriousness, otherT.Seriousness)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.Matches(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.Matches(EventParticipant, otherT.EventParticipant)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
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
            if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Reaction, otherT.Reaction)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(Seriousness, otherT.Seriousness)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.IsExactly(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.IsExactly(EventParticipant, otherT.EventParticipant)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
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
            sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CategoryElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Subject?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.BeginList("reaction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Reaction)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Location?.Serialize(sink);
            sink.Element("seriousness", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Seriousness?.Serialize(sink);
            sink.Element("outcome", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Outcome?.Serialize(sink);
            sink.Element("recorder", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Recorder?.Serialize(sink);
            sink.Element("eventParticipant", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EventParticipant?.Serialize(sink);
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
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
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "identifier":
                    Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "category":
                    CategoryElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.AdverseEventCategory>>();
                    return true;
                case "type":
                    Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "date":
                    DateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "reaction":
                    Reaction = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "location":
                    Location = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "seriousness":
                    Seriousness = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "outcome":
                    Outcome = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "recorder":
                    Recorder = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "eventParticipant":
                    EventParticipant = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "suspectEntity":
                    SuspectEntity = source.GetList<SuspectEntityComponent>();
                    return true;
                case "subjectMedicalHistory":
                    SubjectMedicalHistory = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "referenceDocument":
                    ReferenceDocument = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "study":
                    Study = source.GetList<Hl7.Fhir.Model.ResourceReference>();
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
                    Identifier = source.Populate(Identifier);
                    return true;
                case "category":
                    CategoryElement = source.PopulateValue(CategoryElement);
                    return true;
                case "_category":
                    CategoryElement = source.Populate(CategoryElement);
                    return true;
                case "type":
                    Type = source.Populate(Type);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "reaction":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "location":
                    Location = source.Populate(Location);
                    return true;
                case "seriousness":
                    Seriousness = source.Populate(Seriousness);
                    return true;
                case "outcome":
                    Outcome = source.Populate(Outcome);
                    return true;
                case "recorder":
                    Recorder = source.Populate(Recorder);
                    return true;
                case "eventParticipant":
                    EventParticipant = source.Populate(EventParticipant);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "suspectEntity":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "subjectMedicalHistory":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "referenceDocument":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "study":
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
                case "reaction":
                    source.PopulateListItem(Reaction, index);
                    return true;
                case "suspectEntity":
                    source.PopulateListItem(SuspectEntity, index);
                    return true;
                case "subjectMedicalHistory":
                    source.PopulateListItem(SubjectMedicalHistory, index);
                    return true;
                case "referenceDocument":
                    source.PopulateListItem(ReferenceDocument, index);
                    return true;
                case "study":
                    source.PopulateListItem(Study, index);
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
                if (Identifier != null) yield return Identifier;
                if (CategoryElement != null) yield return CategoryElement;
                if (Type != null) yield return Type;
                if (Subject != null) yield return Subject;
                if (DateElement != null) yield return DateElement;
                foreach (var elem in Reaction) { if (elem != null) yield return elem; }
                if (Location != null) yield return Location;
                if (Seriousness != null) yield return Seriousness;
                if (Outcome != null) yield return Outcome;
                if (Recorder != null) yield return Recorder;
                if (EventParticipant != null) yield return EventParticipant;
                if (DescriptionElement != null) yield return DescriptionElement;
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
                if (CategoryElement != null) yield return new ElementValue("category", CategoryElement);
                if (Type != null) yield return new ElementValue("type", Type);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                foreach (var elem in Reaction) { if (elem != null) yield return new ElementValue("reaction", elem); }
                if (Location != null) yield return new ElementValue("location", Location);
                if (Seriousness != null) yield return new ElementValue("seriousness", Seriousness);
                if (Outcome != null) yield return new ElementValue("outcome", Outcome);
                if (Recorder != null) yield return new ElementValue("recorder", Recorder);
                if (EventParticipant != null) yield return new ElementValue("eventParticipant", EventParticipant);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in SuspectEntity) { if (elem != null) yield return new ElementValue("suspectEntity", elem); }
                foreach (var elem in SubjectMedicalHistory) { if (elem != null) yield return new ElementValue("subjectMedicalHistory", elem); }
                foreach (var elem in ReferenceDocument) { if (elem != null) yield return new ElementValue("referenceDocument", elem); }
                foreach (var elem in Study) { if (elem != null) yield return new ElementValue("study", elem); }
            }
        }
    
    }

}
