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
    /// Investigation to increase healthcare-related patient-independent knowledge
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "ResearchStudy", IsResource=true)]
    [DataContract]
    public partial class ResearchStudy : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IResearchStudy, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ResearchStudy; } }
        [NotMapped]
        public override string TypeName { get { return "ResearchStudy"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ArmComponent")]
        [DataContract]
        public partial class ArmComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IResearchStudyArmComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ArmComponent"; } }
            
            /// <summary>
            /// Label for study arm
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Label for study arm
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
            /// Categorization of study arm
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Short explanation of study path
            /// </summary>
            [FhirElement("description", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Short explanation of study path
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ArmComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Code?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
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
                    case "name":
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
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
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ArmComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ArmComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ArmComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ArmComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (Code != null) yield return Code;
                    if (DescriptionElement != null) yield return DescriptionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactDetail> Hl7.Fhir.Model.IResearchStudy.Contact { get { return Contact; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IRelatedArtifact> Hl7.Fhir.Model.IResearchStudy.RelatedArtifact { get { return RelatedArtifact; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IResearchStudyArmComponent> Hl7.Fhir.Model.IResearchStudy.Arm { get { return Arm; } }
    
        
        /// <summary>
        /// Business Identifier for study
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
        /// Name for this study
        /// </summary>
        [FhirElement("title", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name for this study
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if (value == null)
                    TitleElement = null;
                else
                    TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }
        
        /// <summary>
        /// Steps followed in executing study
        /// </summary>
        [FhirElement("protocol", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [References("PlanDefinition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Protocol
        {
            get { if(_Protocol==null) _Protocol = new List<Hl7.Fhir.Model.ResourceReference>(); return _Protocol; }
            set { _Protocol = value; OnPropertyChanged("Protocol"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Protocol;
        
        /// <summary>
        /// Part of larger study
        /// </summary>
        [FhirElement("partOf", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("ResearchStudy")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PartOf
        {
            get { if(_PartOf==null) _PartOf = new List<Hl7.Fhir.Model.ResourceReference>(); return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PartOf;
        
        /// <summary>
        /// draft | in-progress | suspended | stopped | completed | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.ResearchStudyStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.ResearchStudyStatus> _StatusElement;
        
        /// <summary>
        /// draft | in-progress | suspended | stopped | completed | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.ResearchStudyStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.STU3.ResearchStudyStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Classifications for the study
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
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
        /// Drugs, devices, conditions, etc. under study
        /// </summary>
        [FhirElement("focus", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Focus
        {
            get { if(_Focus==null) _Focus = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Focus; }
            set { _Focus = value; OnPropertyChanged("Focus"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Focus;
        
        /// <summary>
        /// Contact details for the study
        /// </summary>
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.ContactDetail> _Contact;
        
        /// <summary>
        /// References and dependencies
        /// </summary>
        [FhirElement("relatedArtifact", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.RelatedArtifact> RelatedArtifact
        {
            get { if(_RelatedArtifact==null) _RelatedArtifact = new List<Hl7.Fhir.Model.STU3.RelatedArtifact>(); return _RelatedArtifact; }
            set { _RelatedArtifact = value; OnPropertyChanged("RelatedArtifact"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.RelatedArtifact> _RelatedArtifact;
        
        /// <summary>
        /// Used to search for the study
        /// </summary>
        [FhirElement("keyword", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Keyword
        {
            get { if(_Keyword==null) _Keyword = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Keyword; }
            set { _Keyword = value; OnPropertyChanged("Keyword"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Keyword;
        
        /// <summary>
        /// Geographic region(s) for study
        /// </summary>
        [FhirElement("jurisdiction", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// What this is study doing
        /// </summary>
        [FhirElement("description", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _DescriptionElement;
        
        /// <summary>
        /// What this is study doing
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
                    DescriptionElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// Inclusion &amp; exclusion criteria
        /// </summary>
        [FhirElement("enrollment", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [References("Group")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Enrollment
        {
            get { if(_Enrollment==null) _Enrollment = new List<Hl7.Fhir.Model.ResourceReference>(); return _Enrollment; }
            set { _Enrollment = value; OnPropertyChanged("Enrollment"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Enrollment;
        
        /// <summary>
        /// When the study began and ended
        /// </summary>
        [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Organization responsible for the study
        /// </summary>
        [FhirElement("sponsor", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Sponsor
        {
            get { return _Sponsor; }
            set { _Sponsor = value; OnPropertyChanged("Sponsor"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Sponsor;
        
        /// <summary>
        /// The individual responsible for the study
        /// </summary>
        [FhirElement("principalInvestigator", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PrincipalInvestigator
        {
            get { return _PrincipalInvestigator; }
            set { _PrincipalInvestigator = value; OnPropertyChanged("PrincipalInvestigator"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _PrincipalInvestigator;
        
        /// <summary>
        /// Location involved in study execution
        /// </summary>
        [FhirElement("site", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [References("Location")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Site
        {
            get { if(_Site==null) _Site = new List<Hl7.Fhir.Model.ResourceReference>(); return _Site; }
            set { _Site = value; OnPropertyChanged("Site"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Site;
        
        /// <summary>
        /// Reason for terminating study early
        /// </summary>
        [FhirElement("reasonStopped", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ReasonStopped
        {
            get { return _ReasonStopped; }
            set { _ReasonStopped = value; OnPropertyChanged("ReasonStopped"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ReasonStopped;
        
        /// <summary>
        /// Comments made about the event
        /// </summary>
        [FhirElement("note", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Defined path through the study for a subject
        /// </summary>
        [FhirElement("arm", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ArmComponent> Arm
        {
            get { if(_Arm==null) _Arm = new List<ArmComponent>(); return _Arm; }
            set { _Arm = value; OnPropertyChanged("Arm"); }
        }
        
        private List<ArmComponent> _Arm;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ResearchStudy;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(Protocol != null) dest.Protocol = new List<Hl7.Fhir.Model.ResourceReference>(Protocol.DeepCopy());
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.STU3.ResearchStudyStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Focus != null) dest.Focus = new List<Hl7.Fhir.Model.CodeableConcept>(Focus.DeepCopy());
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(Contact.DeepCopy());
                if(RelatedArtifact != null) dest.RelatedArtifact = new List<Hl7.Fhir.Model.STU3.RelatedArtifact>(RelatedArtifact.DeepCopy());
                if(Keyword != null) dest.Keyword = new List<Hl7.Fhir.Model.CodeableConcept>(Keyword.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                if(Enrollment != null) dest.Enrollment = new List<Hl7.Fhir.Model.ResourceReference>(Enrollment.DeepCopy());
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Sponsor != null) dest.Sponsor = (Hl7.Fhir.Model.ResourceReference)Sponsor.DeepCopy();
                if(PrincipalInvestigator != null) dest.PrincipalInvestigator = (Hl7.Fhir.Model.ResourceReference)PrincipalInvestigator.DeepCopy();
                if(Site != null) dest.Site = new List<Hl7.Fhir.Model.ResourceReference>(Site.DeepCopy());
                if(ReasonStopped != null) dest.ReasonStopped = (Hl7.Fhir.Model.CodeableConcept)ReasonStopped.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Arm != null) dest.Arm = new List<ArmComponent>(Arm.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ResearchStudy());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ResearchStudy;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(Protocol, otherT.Protocol)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Focus, otherT.Focus)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(RelatedArtifact, otherT.RelatedArtifact)) return false;
            if( !DeepComparable.Matches(Keyword, otherT.Keyword)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Enrollment, otherT.Enrollment)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Sponsor, otherT.Sponsor)) return false;
            if( !DeepComparable.Matches(PrincipalInvestigator, otherT.PrincipalInvestigator)) return false;
            if( !DeepComparable.Matches(Site, otherT.Site)) return false;
            if( !DeepComparable.Matches(ReasonStopped, otherT.ReasonStopped)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Arm, otherT.Arm)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ResearchStudy;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(Protocol, otherT.Protocol)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Focus, otherT.Focus)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(RelatedArtifact, otherT.RelatedArtifact)) return false;
            if( !DeepComparable.IsExactly(Keyword, otherT.Keyword)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Enrollment, otherT.Enrollment)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Sponsor, otherT.Sponsor)) return false;
            if( !DeepComparable.IsExactly(PrincipalInvestigator, otherT.PrincipalInvestigator)) return false;
            if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
            if( !DeepComparable.IsExactly(ReasonStopped, otherT.ReasonStopped)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Arm, otherT.Arm)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ResearchStudy");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TitleElement?.Serialize(sink);
            sink.BeginList("protocol", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Protocol)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("partOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in PartOf)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.BeginList("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Category)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("focus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Focus)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("relatedArtifact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in RelatedArtifact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("keyword", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Keyword)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("jurisdiction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Jurisdiction)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
            sink.BeginList("enrollment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Enrollment)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
            sink.Element("sponsor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Sponsor?.Serialize(sink);
            sink.Element("principalInvestigator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PrincipalInvestigator?.Serialize(sink);
            sink.BeginList("site", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Site)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("reasonStopped", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReasonStopped?.Serialize(sink);
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("arm", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Arm)
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
                case "title":
                    TitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "protocol":
                    Protocol = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "partOf":
                    PartOf = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.ResearchStudyStatus>>();
                    return true;
                case "category":
                    Category = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "focus":
                    Focus = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "contact":
                    Contact = source.GetList<Hl7.Fhir.Model.STU3.ContactDetail>();
                    return true;
                case "relatedArtifact":
                    RelatedArtifact = source.GetList<Hl7.Fhir.Model.STU3.RelatedArtifact>();
                    return true;
                case "keyword":
                    Keyword = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "jurisdiction":
                    Jurisdiction = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.Markdown>();
                    return true;
                case "enrollment":
                    Enrollment = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "period":
                    Period = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "sponsor":
                    Sponsor = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "principalInvestigator":
                    PrincipalInvestigator = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "site":
                    Site = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "reasonStopped":
                    ReasonStopped = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "arm":
                    Arm = source.GetList<ArmComponent>();
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
                case "title":
                    TitleElement = source.PopulateValue(TitleElement);
                    return true;
                case "_title":
                    TitleElement = source.Populate(TitleElement);
                    return true;
                case "protocol":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "partOf":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "category":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "focus":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "relatedArtifact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "keyword":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "jurisdiction":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "enrollment":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "period":
                    Period = source.Populate(Period);
                    return true;
                case "sponsor":
                    Sponsor = source.Populate(Sponsor);
                    return true;
                case "principalInvestigator":
                    PrincipalInvestigator = source.Populate(PrincipalInvestigator);
                    return true;
                case "site":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonStopped":
                    ReasonStopped = source.Populate(ReasonStopped);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "arm":
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
                case "protocol":
                    source.PopulateListItem(Protocol, index);
                    return true;
                case "partOf":
                    source.PopulateListItem(PartOf, index);
                    return true;
                case "category":
                    source.PopulateListItem(Category, index);
                    return true;
                case "focus":
                    source.PopulateListItem(Focus, index);
                    return true;
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "relatedArtifact":
                    source.PopulateListItem(RelatedArtifact, index);
                    return true;
                case "keyword":
                    source.PopulateListItem(Keyword, index);
                    return true;
                case "jurisdiction":
                    source.PopulateListItem(Jurisdiction, index);
                    return true;
                case "enrollment":
                    source.PopulateListItem(Enrollment, index);
                    return true;
                case "site":
                    source.PopulateListItem(Site, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "arm":
                    source.PopulateListItem(Arm, index);
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
                if (TitleElement != null) yield return TitleElement;
                foreach (var elem in Protocol) { if (elem != null) yield return elem; }
                foreach (var elem in PartOf) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                foreach (var elem in Category) { if (elem != null) yield return elem; }
                foreach (var elem in Focus) { if (elem != null) yield return elem; }
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                foreach (var elem in RelatedArtifact) { if (elem != null) yield return elem; }
                foreach (var elem in Keyword) { if (elem != null) yield return elem; }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in Enrollment) { if (elem != null) yield return elem; }
                if (Period != null) yield return Period;
                if (Sponsor != null) yield return Sponsor;
                if (PrincipalInvestigator != null) yield return PrincipalInvestigator;
                foreach (var elem in Site) { if (elem != null) yield return elem; }
                if (ReasonStopped != null) yield return ReasonStopped;
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in Arm) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                foreach (var elem in Protocol) { if (elem != null) yield return new ElementValue("protocol", elem); }
                foreach (var elem in PartOf) { if (elem != null) yield return new ElementValue("partOf", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                foreach (var elem in Focus) { if (elem != null) yield return new ElementValue("focus", elem); }
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                foreach (var elem in RelatedArtifact) { if (elem != null) yield return new ElementValue("relatedArtifact", elem); }
                foreach (var elem in Keyword) { if (elem != null) yield return new ElementValue("keyword", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in Enrollment) { if (elem != null) yield return new ElementValue("enrollment", elem); }
                if (Period != null) yield return new ElementValue("period", Period);
                if (Sponsor != null) yield return new ElementValue("sponsor", Sponsor);
                if (PrincipalInvestigator != null) yield return new ElementValue("principalInvestigator", PrincipalInvestigator);
                foreach (var elem in Site) { if (elem != null) yield return new ElementValue("site", elem); }
                if (ReasonStopped != null) yield return new ElementValue("reasonStopped", ReasonStopped);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in Arm) { if (elem != null) yield return new ElementValue("arm", elem); }
            }
        }
    
    }

}
