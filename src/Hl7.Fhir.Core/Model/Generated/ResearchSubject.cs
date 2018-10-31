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
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Investigation to increase healthcare-related patient-independent knowledge
    /// </summary>
    [FhirType("ResearchSubject", IsResource=true)]
    [DataContract]
    public partial class ResearchSubject : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ResearchSubject; } }
        [NotMapped]
        public override string TypeName { get { return "ResearchSubject"; } }
        
        /// <summary>
        /// Indicates the progression of a study subject through a study
        /// (url: http://hl7.org/fhir/ValueSet/research-subject-status)
        /// </summary>
        [FhirEnumeration("ResearchSubjectStatus")]
        public enum ResearchSubjectStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/research-subject-status)
            /// </summary>
            [EnumLiteral("candidate", "http://hl7.org/fhir/research-subject-status"), Description("Candidate")]
            Candidate,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/research-subject-status)
            /// </summary>
            [EnumLiteral("enrolled", "http://hl7.org/fhir/research-subject-status"), Description("Enrolled")]
            Enrolled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/research-subject-status)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/research-subject-status"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/research-subject-status)
            /// </summary>
            [EnumLiteral("suspended", "http://hl7.org/fhir/research-subject-status"), Description("Suspended")]
            Suspended,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/research-subject-status)
            /// </summary>
            [EnumLiteral("withdrawn", "http://hl7.org/fhir/research-subject-status"), Description("Withdrawn")]
            Withdrawn,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/research-subject-status)
            /// </summary>
            [EnumLiteral("completed", "http://hl7.org/fhir/research-subject-status"), Description("Completed")]
            Completed,
        }

        /// <summary>
        /// Business Identifier for research subject
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// candidate | enrolled | active | suspended | withdrawn | completed
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ResearchSubject.ResearchSubjectStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ResearchSubject.ResearchSubjectStatus> _StatusElement;
        
        /// <summary>
        /// candidate | enrolled | active | suspended | withdrawn | completed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ResearchSubject.ResearchSubjectStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ResearchSubject.ResearchSubjectStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Start and end of participation
        /// </summary>
        [FhirElement("period", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Study subject is part of
        /// </summary>
        [FhirElement("study", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References("ResearchStudy")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Study
        {
            get { return _Study; }
            set { _Study = value; OnPropertyChanged("Study"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Study;
        
        /// <summary>
        /// Who is part of study
        /// </summary>
        [FhirElement("individual", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Individual
        {
            get { return _Individual; }
            set { _Individual = value; OnPropertyChanged("Individual"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Individual;
        
        /// <summary>
        /// What path should be followed
        /// </summary>
        [FhirElement("assignedArm", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString AssignedArmElement
        {
            get { return _AssignedArmElement; }
            set { _AssignedArmElement = value; OnPropertyChanged("AssignedArmElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _AssignedArmElement;
        
        /// <summary>
        /// What path should be followed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AssignedArm
        {
            get { return AssignedArmElement != null ? AssignedArmElement.Value : null; }
            set
            {
                if (value == null)
                  AssignedArmElement = null; 
                else
                  AssignedArmElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("AssignedArm");
            }
        }
        
        /// <summary>
        /// What path was followed
        /// </summary>
        [FhirElement("actualArm", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ActualArmElement
        {
            get { return _ActualArmElement; }
            set { _ActualArmElement = value; OnPropertyChanged("ActualArmElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ActualArmElement;
        
        /// <summary>
        /// What path was followed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ActualArm
        {
            get { return ActualArmElement != null ? ActualArmElement.Value : null; }
            set
            {
                if (value == null)
                  ActualArmElement = null; 
                else
                  ActualArmElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ActualArm");
            }
        }
        
        /// <summary>
        /// Agreement to participate in study
        /// </summary>
        [FhirElement("consent", Order=160)]
        [CLSCompliant(false)]
		[References("Consent")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Consent
        {
            get { return _Consent; }
            set { _Consent = value; OnPropertyChanged("Consent"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Consent;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ResearchSubject;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ResearchSubject.ResearchSubjectStatus>)StatusElement.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Study != null) dest.Study = (Hl7.Fhir.Model.ResourceReference)Study.DeepCopy();
                if(Individual != null) dest.Individual = (Hl7.Fhir.Model.ResourceReference)Individual.DeepCopy();
                if(AssignedArmElement != null) dest.AssignedArmElement = (Hl7.Fhir.Model.FhirString)AssignedArmElement.DeepCopy();
                if(ActualArmElement != null) dest.ActualArmElement = (Hl7.Fhir.Model.FhirString)ActualArmElement.DeepCopy();
                if(Consent != null) dest.Consent = (Hl7.Fhir.Model.ResourceReference)Consent.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ResearchSubject());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ResearchSubject;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Study, otherT.Study)) return false;
            if( !DeepComparable.Matches(Individual, otherT.Individual)) return false;
            if( !DeepComparable.Matches(AssignedArmElement, otherT.AssignedArmElement)) return false;
            if( !DeepComparable.Matches(ActualArmElement, otherT.ActualArmElement)) return false;
            if( !DeepComparable.Matches(Consent, otherT.Consent)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ResearchSubject;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Study, otherT.Study)) return false;
            if( !DeepComparable.IsExactly(Individual, otherT.Individual)) return false;
            if( !DeepComparable.IsExactly(AssignedArmElement, otherT.AssignedArmElement)) return false;
            if( !DeepComparable.IsExactly(ActualArmElement, otherT.ActualArmElement)) return false;
            if( !DeepComparable.IsExactly(Consent, otherT.Consent)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Identifier != null) yield return Identifier;
				if (StatusElement != null) yield return StatusElement;
				if (Period != null) yield return Period;
				if (Study != null) yield return Study;
				if (Individual != null) yield return Individual;
				if (AssignedArmElement != null) yield return AssignedArmElement;
				if (ActualArmElement != null) yield return ActualArmElement;
				if (Consent != null) yield return Consent;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Period != null) yield return new ElementValue("period", Period);
                if (Study != null) yield return new ElementValue("study", Study);
                if (Individual != null) yield return new ElementValue("individual", Individual);
                if (AssignedArmElement != null) yield return new ElementValue("assignedArm", AssignedArmElement);
                if (ActualArmElement != null) yield return new ElementValue("actualArm", ActualArmElement);
                if (Consent != null) yield return new ElementValue("consent", Consent);
            }
        }

    }
    
}
