using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Drug, food, environmental and others
    /// </summary>
    [FhirType("AllergyIntolerance", IsResource=true)]
    [DataContract]
    public partial class AllergyIntolerance : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// The status of the adverse sensitivity
        /// </summary>
        [FhirEnumeration("SensitivityStatus")]
        public enum SensitivityStatus
        {
            /// <summary>
            /// A suspected sensitivity to a substance.
            /// </summary>
            [EnumLiteral("suspected")]
            Suspected,
            /// <summary>
            /// The sensitivity has been confirmed and is active.
            /// </summary>
            [EnumLiteral("confirmed")]
            Confirmed,
            /// <summary>
            /// The sensitivity has been shown to never have existed.
            /// </summary>
            [EnumLiteral("refuted")]
            Refuted,
            /// <summary>
            /// The sensitivity used to exist but no longer does.
            /// </summary>
            [EnumLiteral("resolved")]
            Resolved,
        }
        
        /// <summary>
        /// The criticality of an adverse sensitivity
        /// </summary>
        [FhirEnumeration("Criticality")]
        public enum Criticality
        {
            /// <summary>
            /// Likely to result in death if re-exposed.
            /// </summary>
            [EnumLiteral("fatal")]
            Fatal,
            /// <summary>
            /// Likely to result in reactions that will need to be treated if re-exposed.
            /// </summary>
            [EnumLiteral("high")]
            High,
            /// <summary>
            /// Likely to result in reactions that will inconvenience the subject.
            /// </summary>
            [EnumLiteral("medium")]
            Medium,
            /// <summary>
            /// Not likely to result in any inconveniences for the subject.
            /// </summary>
            [EnumLiteral("low")]
            Low,
        }
        
        /// <summary>
        /// The type of an adverse sensitivity
        /// </summary>
        [FhirEnumeration("SensitivityType")]
        public enum SensitivityType
        {
            /// <summary>
            /// Allergic Reaction.
            /// </summary>
            [EnumLiteral("allergy")]
            Allergy,
            /// <summary>
            /// Non-Allergic Reaction.
            /// </summary>
            [EnumLiteral("intolerance")]
            Intolerance,
            /// <summary>
            /// Unknown type.
            /// </summary>
            [EnumLiteral("unknown")]
            Unknown,
        }
        
        /// <summary>
        /// External Ids for this item
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// fatal | high | medium | low
        /// </summary>
        [FhirElement("criticality", Order=80)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AllergyIntolerance.Criticality> Criticality_Element
        {
            get { return _Criticality_Element; }
            set { _Criticality_Element = value; OnPropertyChanged("Criticality_Element"); }
        }
        private Code<Hl7.Fhir.Model.AllergyIntolerance.Criticality> _Criticality_Element;
        
        /// <summary>
        /// fatal | high | medium | low
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AllergyIntolerance.Criticality? Criticality_
        {
            get { return Criticality_Element != null ? Criticality_Element.Value : null; }
            set
            {
                if(value == null)
                  Criticality_Element = null; 
                else
                  Criticality_Element = new Code<Hl7.Fhir.Model.AllergyIntolerance.Criticality>(value);
                OnPropertyChanged("Criticality_");
            }
        }
        
        /// <summary>
        /// allergy | intolerance | unknown
        /// </summary>
        [FhirElement("sensitivityType", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AllergyIntolerance.SensitivityType> SensitivityType_Element
        {
            get { return _SensitivityType_Element; }
            set { _SensitivityType_Element = value; OnPropertyChanged("SensitivityType_Element"); }
        }
        private Code<Hl7.Fhir.Model.AllergyIntolerance.SensitivityType> _SensitivityType_Element;
        
        /// <summary>
        /// allergy | intolerance | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AllergyIntolerance.SensitivityType? SensitivityType_
        {
            get { return SensitivityType_Element != null ? SensitivityType_Element.Value : null; }
            set
            {
                if(value == null)
                  SensitivityType_Element = null; 
                else
                  SensitivityType_Element = new Code<Hl7.Fhir.Model.AllergyIntolerance.SensitivityType>(value);
                OnPropertyChanged("SensitivityType_");
            }
        }
        
        /// <summary>
        /// When recorded
        /// </summary>
        [FhirElement("recordedDate", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime RecordedDateElement
        {
            get { return _RecordedDateElement; }
            set { _RecordedDateElement = value; OnPropertyChanged("RecordedDateElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _RecordedDateElement;
        
        /// <summary>
        /// When recorded
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RecordedDate
        {
            get { return RecordedDateElement != null ? RecordedDateElement.Value : null; }
            set
            {
                if(value == null)
                  RecordedDateElement = null; 
                else
                  RecordedDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("RecordedDate");
            }
        }
        
        /// <summary>
        /// suspected | confirmed | refuted | resolved
        /// </summary>
        [FhirElement("status", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AllergyIntolerance.SensitivityStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.AllergyIntolerance.SensitivityStatus> _StatusElement;
        
        /// <summary>
        /// suspected | confirmed | refuted | resolved
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AllergyIntolerance.SensitivityStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.AllergyIntolerance.SensitivityStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Who the sensitivity is for
        /// </summary>
        [FhirElement("subject", Order=120)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Who recorded the sensitivity
        /// </summary>
        [FhirElement("recorder", Order=130)]
        [References("Practitioner","Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recorder
        {
            get { return _Recorder; }
            set { _Recorder = value; OnPropertyChanged("Recorder"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Recorder;
        
        /// <summary>
        /// The substance that causes the sensitivity
        /// </summary>
        [FhirElement("substance", Order=140)]
        [References("Substance")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Substance
        {
            get { return _Substance; }
            set { _Substance = value; OnPropertyChanged("Substance"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Substance;
        
        /// <summary>
        /// Reactions associated with the sensitivity
        /// </summary>
        [FhirElement("reaction", Order=150)]
        [References("AdverseReaction")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Reaction
        {
            get { return _Reaction; }
            set { _Reaction = value; OnPropertyChanged("Reaction"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Reaction;
        
        /// <summary>
        /// Observations that confirm or refute
        /// </summary>
        [FhirElement("sensitivityTest", Order=160)]
        [References("Observation")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SensitivityTest
        {
            get { return _SensitivityTest; }
            set { _SensitivityTest = value; OnPropertyChanged("SensitivityTest"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _SensitivityTest;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as AllergyIntolerance;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Criticality_Element != null) dest.Criticality_Element = (Code<Hl7.Fhir.Model.AllergyIntolerance.Criticality>)Criticality_Element.DeepCopy();
                if(SensitivityType_Element != null) dest.SensitivityType_Element = (Code<Hl7.Fhir.Model.AllergyIntolerance.SensitivityType>)SensitivityType_Element.DeepCopy();
                if(RecordedDateElement != null) dest.RecordedDateElement = (Hl7.Fhir.Model.FhirDateTime)RecordedDateElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.AllergyIntolerance.SensitivityStatus>)StatusElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Recorder != null) dest.Recorder = (Hl7.Fhir.Model.ResourceReference)Recorder.DeepCopy();
                if(Substance != null) dest.Substance = (Hl7.Fhir.Model.ResourceReference)Substance.DeepCopy();
                if(Reaction != null) dest.Reaction = new List<Hl7.Fhir.Model.ResourceReference>(Reaction.DeepCopy());
                if(SensitivityTest != null) dest.SensitivityTest = new List<Hl7.Fhir.Model.ResourceReference>(SensitivityTest.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new AllergyIntolerance());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as AllergyIntolerance;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Criticality_Element, otherT.Criticality_Element)) return false;
            if( !DeepComparable.Matches(SensitivityType_Element, otherT.SensitivityType_Element)) return false;
            if( !DeepComparable.Matches(RecordedDateElement, otherT.RecordedDateElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.Matches(Substance, otherT.Substance)) return false;
            if( !DeepComparable.Matches(Reaction, otherT.Reaction)) return false;
            if( !DeepComparable.Matches(SensitivityTest, otherT.SensitivityTest)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as AllergyIntolerance;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Criticality_Element, otherT.Criticality_Element)) return false;
            if( !DeepComparable.IsExactly(SensitivityType_Element, otherT.SensitivityType_Element)) return false;
            if( !DeepComparable.IsExactly(RecordedDateElement, otherT.RecordedDateElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.IsExactly(Substance, otherT.Substance)) return false;
            if( !DeepComparable.IsExactly(Reaction, otherT.Reaction)) return false;
            if( !DeepComparable.IsExactly(SensitivityTest, otherT.SensitivityTest)) return false;
            
            return true;
        }
        
    }
    
}
