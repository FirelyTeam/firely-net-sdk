﻿using System;
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
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Clinical issue with action
    /// </summary>
    [FhirType("DetectedIssue", IsResource=true)]
    [DataContract]
    public partial class DetectedIssue : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DetectedIssue; } }
        [NotMapped]
        public override string TypeName { get { return "DetectedIssue"; } }
        
        /// <summary>
        /// Indicates the potential degree of impact of the identified issue on the patient.
        /// (url: http://hl7.org/fhir/ValueSet/detectedissue-severity)
        /// </summary>
        [FhirEnumeration("DetectedIssueSeverity")]
        public enum DetectedIssueSeverity
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/detectedissue-severity)
            /// </summary>
            [EnumLiteral("high", "http://hl7.org/fhir/detectedissue-severity"), Description("High")]
            High,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/detectedissue-severity)
            /// </summary>
            [EnumLiteral("moderate", "http://hl7.org/fhir/detectedissue-severity"), Description("Moderate")]
            Moderate,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/detectedissue-severity)
            /// </summary>
            [EnumLiteral("low", "http://hl7.org/fhir/detectedissue-severity"), Description("Low")]
            Low,
        }

        [FhirType("EvidenceComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class EvidenceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "EvidenceComponent"; } }
            
            /// <summary>
            /// Manifestation
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Code
            {
                get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Code;
            
            /// <summary>
            /// Supporting information
            /// </summary>
            [FhirElement("detail", Order=50)]
            [CLSCompliant(false)]
			[References()]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Detail
            {
                get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.ResourceReference>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Detail;
            
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
        
        
        [FhirType("MitigationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class MitigationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "MitigationComponent"; } }
            
            /// <summary>
            /// What mitigation?
            /// </summary>
            [FhirElement("action", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Action
            {
                get { return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Action;
            
            /// <summary>
            /// Date committed
            /// </summary>
            [FhirElement("date", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _DateElement;
            
            /// <summary>
            /// Date committed
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
            /// Who is committing?
            /// </summary>
            [FhirElement("author", Order=60)]
            [CLSCompliant(false)]
			[References("Practitioner","PractitionerRole")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Author
            {
                get { return _Author; }
                set { _Author = value; OnPropertyChanged("Author"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Author;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MitigationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Action != null) dest.Action = (Hl7.Fhir.Model.CodeableConcept)Action.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                    if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MitigationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MitigationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Author, otherT.Author)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MitigationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Action != null) yield return Action;
                    if (DateElement != null) yield return DateElement;
                    if (Author != null) yield return Author;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Action != null) yield return new ElementValue("action", Action);
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Author != null) yield return new ElementValue("author", Author);
                }
            }

            
        }
        
        
        /// <summary>
        /// Unique id for the detected issue
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// registered | preliminary | final | amended +
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ObservationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ObservationStatus> _StatusElement;
        
        /// <summary>
        /// registered | preliminary | final | amended +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ObservationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ObservationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Issue Category, e.g. drug-drug, duplicate therapy, etc.
        /// </summary>
        [FhirElement("code", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// high | moderate | low
        /// </summary>
        [FhirElement("severity", InSummary=true, Order=120)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DetectedIssue.DetectedIssueSeverity> SeverityElement
        {
            get { return _SeverityElement; }
            set { _SeverityElement = value; OnPropertyChanged("SeverityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DetectedIssue.DetectedIssueSeverity> _SeverityElement;
        
        /// <summary>
        /// high | moderate | low
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DetectedIssue.DetectedIssueSeverity? Severity
        {
            get { return SeverityElement != null ? SeverityElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  SeverityElement = null; 
                else
                  SeverityElement = new Code<Hl7.Fhir.Model.DetectedIssue.DetectedIssueSeverity>(value);
                OnPropertyChanged("Severity");
            }
        }
        
        /// <summary>
        /// Associated patient
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// When identified
        /// </summary>
        [FhirElement("identified", InSummary=true, Order=140, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Identified
        {
            get { return _Identified; }
            set { _Identified = value; OnPropertyChanged("Identified"); }
        }
        
        private Hl7.Fhir.Model.Element _Identified;
        
        /// <summary>
        /// The provider or device that identified the issue
        /// </summary>
        [FhirElement("author", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole","Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// Problem resource
        /// </summary>
        [FhirElement("implicated", InSummary=true, Order=160)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Implicated
        {
            get { if(_Implicated==null) _Implicated = new List<Hl7.Fhir.Model.ResourceReference>(); return _Implicated; }
            set { _Implicated = value; OnPropertyChanged("Implicated"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Implicated;
        
        /// <summary>
        /// Supporting evidence
        /// </summary>
        [FhirElement("evidence", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DetectedIssue.EvidenceComponent> Evidence
        {
            get { if(_Evidence==null) _Evidence = new List<Hl7.Fhir.Model.DetectedIssue.EvidenceComponent>(); return _Evidence; }
            set { _Evidence = value; OnPropertyChanged("Evidence"); }
        }
        
        private List<Hl7.Fhir.Model.DetectedIssue.EvidenceComponent> _Evidence;
        
        /// <summary>
        /// Description and context
        /// </summary>
        [FhirElement("detail", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DetailElement
        {
            get { return _DetailElement; }
            set { _DetailElement = value; OnPropertyChanged("DetailElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DetailElement;
        
        /// <summary>
        /// Description and context
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Detail
        {
            get { return DetailElement != null ? DetailElement.Value : null; }
            set
            {
                if (value == null)
                  DetailElement = null; 
                else
                  DetailElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Detail");
            }
        }
        
        /// <summary>
        /// Authority for issue
        /// </summary>
        [FhirElement("reference", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri ReferenceElement
        {
            get { return _ReferenceElement; }
            set { _ReferenceElement = value; OnPropertyChanged("ReferenceElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _ReferenceElement;
        
        /// <summary>
        /// Authority for issue
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Reference
        {
            get { return ReferenceElement != null ? ReferenceElement.Value : null; }
            set
            {
                if (value == null)
                  ReferenceElement = null; 
                else
                  ReferenceElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Reference");
            }
        }
        
        /// <summary>
        /// Step taken to address
        /// </summary>
        [FhirElement("mitigation", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DetectedIssue.MitigationComponent> Mitigation
        {
            get { if(_Mitigation==null) _Mitigation = new List<Hl7.Fhir.Model.DetectedIssue.MitigationComponent>(); return _Mitigation; }
            set { _Mitigation = value; OnPropertyChanged("Mitigation"); }
        }
        
        private List<Hl7.Fhir.Model.DetectedIssue.MitigationComponent> _Mitigation;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DetectedIssue;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ObservationStatus>)StatusElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(SeverityElement != null) dest.SeverityElement = (Code<Hl7.Fhir.Model.DetectedIssue.DetectedIssueSeverity>)SeverityElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Identified != null) dest.Identified = (Hl7.Fhir.Model.Element)Identified.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(Implicated != null) dest.Implicated = new List<Hl7.Fhir.Model.ResourceReference>(Implicated.DeepCopy());
                if(Evidence != null) dest.Evidence = new List<Hl7.Fhir.Model.DetectedIssue.EvidenceComponent>(Evidence.DeepCopy());
                if(DetailElement != null) dest.DetailElement = (Hl7.Fhir.Model.FhirString)DetailElement.DeepCopy();
                if(ReferenceElement != null) dest.ReferenceElement = (Hl7.Fhir.Model.FhirUri)ReferenceElement.DeepCopy();
                if(Mitigation != null) dest.Mitigation = new List<Hl7.Fhir.Model.DetectedIssue.MitigationComponent>(Mitigation.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new DetectedIssue());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DetectedIssue;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(SeverityElement, otherT.SeverityElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Identified, otherT.Identified)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Implicated, otherT.Implicated)) return false;
            if( !DeepComparable.Matches(Evidence, otherT.Evidence)) return false;
            if( !DeepComparable.Matches(DetailElement, otherT.DetailElement)) return false;
            if( !DeepComparable.Matches(ReferenceElement, otherT.ReferenceElement)) return false;
            if( !DeepComparable.Matches(Mitigation, otherT.Mitigation)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DetectedIssue;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Identified, otherT.Identified)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Implicated, otherT.Implicated)) return false;
            if( !DeepComparable.IsExactly(Evidence, otherT.Evidence)) return false;
            if( !DeepComparable.IsExactly(DetailElement, otherT.DetailElement)) return false;
            if( !DeepComparable.IsExactly(ReferenceElement, otherT.ReferenceElement)) return false;
            if( !DeepComparable.IsExactly(Mitigation, otherT.Mitigation)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (Code != null) yield return Code;
				if (SeverityElement != null) yield return SeverityElement;
				if (Patient != null) yield return Patient;
				if (Identified != null) yield return Identified;
				if (Author != null) yield return Author;
				foreach (var elem in Implicated) { if (elem != null) yield return elem; }
				foreach (var elem in Evidence) { if (elem != null) yield return elem; }
				if (DetailElement != null) yield return DetailElement;
				if (ReferenceElement != null) yield return ReferenceElement;
				foreach (var elem in Mitigation) { if (elem != null) yield return elem; }
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
                if (Code != null) yield return new ElementValue("code", Code);
                if (SeverityElement != null) yield return new ElementValue("severity", SeverityElement);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Identified != null) yield return new ElementValue("identified", Identified);
                if (Author != null) yield return new ElementValue("author", Author);
                foreach (var elem in Implicated) { if (elem != null) yield return new ElementValue("implicated", elem); }
                foreach (var elem in Evidence) { if (elem != null) yield return new ElementValue("evidence", elem); }
                if (DetailElement != null) yield return new ElementValue("detail", DetailElement);
                if (ReferenceElement != null) yield return new ElementValue("reference", ReferenceElement);
                foreach (var elem in Mitigation) { if (elem != null) yield return new ElementValue("mitigation", elem); }
            }
        }

    }
    
}
