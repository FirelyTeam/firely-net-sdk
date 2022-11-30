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
    /// A clinical assessment performed when planning treatments and management strategies for a patient
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "ClinicalImpression", IsResource=true)]
    [DataContract]
    public partial class ClinicalImpression : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IClinicalImpression, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ClinicalImpression; } }
        [NotMapped]
        public override string TypeName { get { return "ClinicalImpression"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "InvestigationComponent")]
        [DataContract]
        public partial class InvestigationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "InvestigationComponent"; } }
            
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
            [References("Observation","QuestionnaireResponse","FamilyMemberHistory","DiagnosticReport","RiskAssessment","ImagingStudy","Media")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Item
            {
                get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.ResourceReference>(); return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Item;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("InvestigationComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Code?.Serialize(sink);
                sink.BeginList("item", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Item)
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
                    case "item":
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
                    case "item":
                        source.PopulateListItem(Item, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InvestigationComponent;
            
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
                 return CopyTo(new InvestigationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InvestigationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InvestigationComponent;
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "FindingComponent")]
        [DataContract]
        public partial class FindingComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IClinicalImpressionFindingComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "FindingComponent"; } }
            
            /// <summary>
            /// What was found
            /// </summary>
            [FhirElement("itemCodeableConcept", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ItemCodeableConcept
            {
                get { return _ItemCodeableConcept; }
                set { _ItemCodeableConcept = value; OnPropertyChanged("ItemCodeableConcept"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ItemCodeableConcept;
            
            /// <summary>
            /// What was found
            /// </summary>
            [FhirElement("itemReference", Order=50)]
            [CLSCompliant(false)]
            [References("Condition","Observation","Media")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference ItemReference
            {
                get { return _ItemReference; }
                set { _ItemReference = value; OnPropertyChanged("ItemReference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _ItemReference;
            
            /// <summary>
            /// Which investigations support finding
            /// </summary>
            [FhirElement("basis", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString BasisElement
            {
                get { return _BasisElement; }
                set { _BasisElement = value; OnPropertyChanged("BasisElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _BasisElement;
            
            /// <summary>
            /// Which investigations support finding
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Basis
            {
                get { return BasisElement != null ? BasisElement.Value : null; }
                set
                {
                    if (value == null)
                        BasisElement = null;
                    else
                        BasisElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Basis");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("FindingComponent");
                base.Serialize(sink);
                sink.Element("itemCodeableConcept", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ItemCodeableConcept?.Serialize(sink);
                sink.Element("itemReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ItemReference?.Serialize(sink);
                sink.Element("basis", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); BasisElement?.Serialize(sink);
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
                    case "itemCodeableConcept":
                        ItemCodeableConcept = source.Populate(ItemCodeableConcept);
                        return true;
                    case "itemReference":
                        ItemReference = source.Populate(ItemReference);
                        return true;
                    case "basis":
                        BasisElement = source.PopulateValue(BasisElement);
                        return true;
                    case "_basis":
                        BasisElement = source.Populate(BasisElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FindingComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ItemCodeableConcept != null) dest.ItemCodeableConcept = (Hl7.Fhir.Model.CodeableConcept)ItemCodeableConcept.DeepCopy();
                    if(ItemReference != null) dest.ItemReference = (Hl7.Fhir.Model.ResourceReference)ItemReference.DeepCopy();
                    if(BasisElement != null) dest.BasisElement = (Hl7.Fhir.Model.FhirString)BasisElement.DeepCopy();
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
                if( !DeepComparable.Matches(ItemCodeableConcept, otherT.ItemCodeableConcept)) return false;
                if( !DeepComparable.Matches(ItemReference, otherT.ItemReference)) return false;
                if( !DeepComparable.Matches(BasisElement, otherT.BasisElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FindingComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ItemCodeableConcept, otherT.ItemCodeableConcept)) return false;
                if( !DeepComparable.IsExactly(ItemReference, otherT.ItemReference)) return false;
                if( !DeepComparable.IsExactly(BasisElement, otherT.BasisElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ItemCodeableConcept != null) yield return ItemCodeableConcept;
                    if (ItemReference != null) yield return ItemReference;
                    if (BasisElement != null) yield return BasisElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ItemCodeableConcept != null) yield return new ElementValue("itemCodeableConcept", ItemCodeableConcept);
                    if (ItemReference != null) yield return new ElementValue("itemReference", ItemReference);
                    if (BasisElement != null) yield return new ElementValue("basis", BasisElement);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IClinicalImpressionFindingComponent> Hl7.Fhir.Model.IClinicalImpression.Finding { get { return Finding; } }
    
        
        /// <summary>
        /// Business identifier
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
        /// in-progress | completed | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.ClinicalImpressionStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.ClinicalImpressionStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | completed | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.ClinicalImpressionStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.R4.ClinicalImpressionStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reason for current status
        /// </summary>
        [FhirElement("statusReason", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept StatusReason
        {
            get { return _StatusReason; }
            set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _StatusReason;
        
        /// <summary>
        /// Kind of assessment performed
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// Why/how the assessment was performed
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
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
        /// Patient or group assessed
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
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
        /// Encounter created as part of
        /// </summary>
        [FhirElement("encounter", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
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
        /// Time of assessment
        /// </summary>
        [FhirElement("effective", InSummary=Hl7.Fhir.Model.Version.All, Order=160, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Effective
        {
            get { return _Effective; }
            set { _Effective = value; OnPropertyChanged("Effective"); }
        }
        
        private Hl7.Fhir.Model.Element _Effective;
        
        /// <summary>
        /// When the assessment was documented
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// When the assessment was documented
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
        /// The clinician performing the assessment
        /// </summary>
        [FhirElement("assessor", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Assessor
        {
            get { return _Assessor; }
            set { _Assessor = value; OnPropertyChanged("Assessor"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Assessor;
        
        /// <summary>
        /// Reference to last assessment
        /// </summary>
        [FhirElement("previous", Order=190)]
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
        /// Relevant impressions of patient state
        /// </summary>
        [FhirElement("problem", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
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
        /// One or more sets of investigations (signs, symptoms, etc.)
        /// </summary>
        [FhirElement("investigation", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<InvestigationComponent> Investigation
        {
            get { if(_Investigation==null) _Investigation = new List<InvestigationComponent>(); return _Investigation; }
            set { _Investigation = value; OnPropertyChanged("Investigation"); }
        }
        
        private List<InvestigationComponent> _Investigation;
        
        /// <summary>
        /// Clinical Protocol followed
        /// </summary>
        [FhirElement("protocol", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> ProtocolElement
        {
            get { if(_ProtocolElement==null) _ProtocolElement = new List<Hl7.Fhir.Model.FhirUri>(); return _ProtocolElement; }
            set { _ProtocolElement = value; OnPropertyChanged("ProtocolElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _ProtocolElement;
        
        /// <summary>
        /// Clinical Protocol followed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Protocol
        {
            get { return ProtocolElement != null ? ProtocolElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    ProtocolElement = null;
                else
                    ProtocolElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("Protocol");
            }
        }
        
        /// <summary>
        /// Summary of the assessment
        /// </summary>
        [FhirElement("summary", Order=230)]
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
        [FhirElement("finding", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<FindingComponent> Finding
        {
            get { if(_Finding==null) _Finding = new List<FindingComponent>(); return _Finding; }
            set { _Finding = value; OnPropertyChanged("Finding"); }
        }
        
        private List<FindingComponent> _Finding;
        
        /// <summary>
        /// Estimate of likely outcome
        /// </summary>
        [FhirElement("prognosisCodeableConcept", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> PrognosisCodeableConcept
        {
            get { if(_PrognosisCodeableConcept==null) _PrognosisCodeableConcept = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PrognosisCodeableConcept; }
            set { _PrognosisCodeableConcept = value; OnPropertyChanged("PrognosisCodeableConcept"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _PrognosisCodeableConcept;
        
        /// <summary>
        /// RiskAssessment expressing likely outcome
        /// </summary>
        [FhirElement("prognosisReference", Order=260)]
        [CLSCompliant(false)]
        [References("RiskAssessment")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PrognosisReference
        {
            get { if(_PrognosisReference==null) _PrognosisReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _PrognosisReference; }
            set { _PrognosisReference = value; OnPropertyChanged("PrognosisReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PrognosisReference;
        
        /// <summary>
        /// Information supporting the clinical impression
        /// </summary>
        [FhirElement("supportingInfo", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SupportingInfo
        {
            get { if(_SupportingInfo==null) _SupportingInfo = new List<Hl7.Fhir.Model.ResourceReference>(); return _SupportingInfo; }
            set { _SupportingInfo = value; OnPropertyChanged("SupportingInfo"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SupportingInfo;
        
        /// <summary>
        /// Comments made about the ClinicalImpression
        /// </summary>
        [FhirElement("note", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ClinicalImpression;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.R4.ClinicalImpressionStatus>)StatusElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = (Hl7.Fhir.Model.CodeableConcept)StatusReason.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Assessor != null) dest.Assessor = (Hl7.Fhir.Model.ResourceReference)Assessor.DeepCopy();
                if(Previous != null) dest.Previous = (Hl7.Fhir.Model.ResourceReference)Previous.DeepCopy();
                if(Problem != null) dest.Problem = new List<Hl7.Fhir.Model.ResourceReference>(Problem.DeepCopy());
                if(Investigation != null) dest.Investigation = new List<InvestigationComponent>(Investigation.DeepCopy());
                if(ProtocolElement != null) dest.ProtocolElement = new List<Hl7.Fhir.Model.FhirUri>(ProtocolElement.DeepCopy());
                if(SummaryElement != null) dest.SummaryElement = (Hl7.Fhir.Model.FhirString)SummaryElement.DeepCopy();
                if(Finding != null) dest.Finding = new List<FindingComponent>(Finding.DeepCopy());
                if(PrognosisCodeableConcept != null) dest.PrognosisCodeableConcept = new List<Hl7.Fhir.Model.CodeableConcept>(PrognosisCodeableConcept.DeepCopy());
                if(PrognosisReference != null) dest.PrognosisReference = new List<Hl7.Fhir.Model.ResourceReference>(PrognosisReference.DeepCopy());
                if(SupportingInfo != null) dest.SupportingInfo = new List<Hl7.Fhir.Model.ResourceReference>(SupportingInfo.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
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
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Assessor, otherT.Assessor)) return false;
            if( !DeepComparable.Matches(Previous, otherT.Previous)) return false;
            if( !DeepComparable.Matches(Problem, otherT.Problem)) return false;
            if( !DeepComparable.Matches(Investigation, otherT.Investigation)) return false;
            if( !DeepComparable.Matches(ProtocolElement, otherT.ProtocolElement)) return false;
            if( !DeepComparable.Matches(SummaryElement, otherT.SummaryElement)) return false;
            if( !DeepComparable.Matches(Finding, otherT.Finding)) return false;
            if( !DeepComparable.Matches(PrognosisCodeableConcept, otherT.PrognosisCodeableConcept)) return false;
            if( !DeepComparable.Matches(PrognosisReference, otherT.PrognosisReference)) return false;
            if( !DeepComparable.Matches(SupportingInfo, otherT.SupportingInfo)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ClinicalImpression;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Assessor, otherT.Assessor)) return false;
            if( !DeepComparable.IsExactly(Previous, otherT.Previous)) return false;
            if( !DeepComparable.IsExactly(Problem, otherT.Problem)) return false;
            if( !DeepComparable.IsExactly(Investigation, otherT.Investigation)) return false;
            if( !DeepComparable.IsExactly(ProtocolElement, otherT.ProtocolElement)) return false;
            if( !DeepComparable.IsExactly(SummaryElement, otherT.SummaryElement)) return false;
            if( !DeepComparable.IsExactly(Finding, otherT.Finding)) return false;
            if( !DeepComparable.IsExactly(PrognosisCodeableConcept, otherT.PrognosisCodeableConcept)) return false;
            if( !DeepComparable.IsExactly(PrognosisReference, otherT.PrognosisReference)) return false;
            if( !DeepComparable.IsExactly(SupportingInfo, otherT.SupportingInfo)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ClinicalImpression");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("statusReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); StatusReason?.Serialize(sink);
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Code?.Serialize(sink);
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Encounter?.Serialize(sink);
            sink.Element("effective", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Effective?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.Element("assessor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Assessor?.Serialize(sink);
            sink.Element("previous", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Previous?.Serialize(sink);
            sink.BeginList("problem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Problem)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("investigation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Investigation)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("protocol", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            sink.Serialize(ProtocolElement);
            sink.End();
            sink.Element("summary", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SummaryElement?.Serialize(sink);
            sink.BeginList("finding", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Finding)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("prognosisCodeableConcept", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in PrognosisCodeableConcept)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("prognosisReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in PrognosisReference)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("supportingInfo", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in SupportingInfo)
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
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "statusReason":
                    StatusReason = source.Populate(StatusReason);
                    return true;
                case "code":
                    Code = source.Populate(Code);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "effectiveDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Effective, "effective");
                    Effective = source.PopulateValue(Effective as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_effectiveDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Effective, "effective");
                    Effective = source.Populate(Effective as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "effectivePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Effective, "effective");
                    Effective = source.Populate(Effective as Hl7.Fhir.Model.Period);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "assessor":
                    Assessor = source.Populate(Assessor);
                    return true;
                case "previous":
                    Previous = source.Populate(Previous);
                    return true;
                case "problem":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "investigation":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "protocol":
                case "_protocol":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "summary":
                    SummaryElement = source.PopulateValue(SummaryElement);
                    return true;
                case "_summary":
                    SummaryElement = source.Populate(SummaryElement);
                    return true;
                case "finding":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "prognosisCodeableConcept":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "prognosisReference":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "supportingInfo":
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
                case "problem":
                    source.PopulateListItem(Problem, index);
                    return true;
                case "investigation":
                    source.PopulateListItem(Investigation, index);
                    return true;
                case "protocol":
                    source.PopulatePrimitiveListItemValue(ProtocolElement, index);
                    return true;
                case "_protocol":
                    source.PopulatePrimitiveListItem(ProtocolElement, index);
                    return true;
                case "finding":
                    source.PopulateListItem(Finding, index);
                    return true;
                case "prognosisCodeableConcept":
                    source.PopulateListItem(PrognosisCodeableConcept, index);
                    return true;
                case "prognosisReference":
                    source.PopulateListItem(PrognosisReference, index);
                    return true;
                case "supportingInfo":
                    source.PopulateListItem(SupportingInfo, index);
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
                if (StatusElement != null) yield return StatusElement;
                if (StatusReason != null) yield return StatusReason;
                if (Code != null) yield return Code;
                if (DescriptionElement != null) yield return DescriptionElement;
                if (Subject != null) yield return Subject;
                if (Encounter != null) yield return Encounter;
                if (Effective != null) yield return Effective;
                if (DateElement != null) yield return DateElement;
                if (Assessor != null) yield return Assessor;
                if (Previous != null) yield return Previous;
                foreach (var elem in Problem) { if (elem != null) yield return elem; }
                foreach (var elem in Investigation) { if (elem != null) yield return elem; }
                foreach (var elem in ProtocolElement) { if (elem != null) yield return elem; }
                if (SummaryElement != null) yield return SummaryElement;
                foreach (var elem in Finding) { if (elem != null) yield return elem; }
                foreach (var elem in PrognosisCodeableConcept) { if (elem != null) yield return elem; }
                foreach (var elem in PrognosisReference) { if (elem != null) yield return elem; }
                foreach (var elem in SupportingInfo) { if (elem != null) yield return elem; }
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
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (StatusReason != null) yield return new ElementValue("statusReason", StatusReason);
                if (Code != null) yield return new ElementValue("code", Code);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (Effective != null) yield return new ElementValue("effective", Effective);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (Assessor != null) yield return new ElementValue("assessor", Assessor);
                if (Previous != null) yield return new ElementValue("previous", Previous);
                foreach (var elem in Problem) { if (elem != null) yield return new ElementValue("problem", elem); }
                foreach (var elem in Investigation) { if (elem != null) yield return new ElementValue("investigation", elem); }
                foreach (var elem in ProtocolElement) { if (elem != null) yield return new ElementValue("protocol", elem); }
                if (SummaryElement != null) yield return new ElementValue("summary", SummaryElement);
                foreach (var elem in Finding) { if (elem != null) yield return new ElementValue("finding", elem); }
                foreach (var elem in PrognosisCodeableConcept) { if (elem != null) yield return new ElementValue("prognosisCodeableConcept", elem); }
                foreach (var elem in PrognosisReference) { if (elem != null) yield return new ElementValue("prognosisReference", elem); }
                foreach (var elem in SupportingInfo) { if (elem != null) yield return new ElementValue("supportingInfo", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
            }
        }
    
    }

}
