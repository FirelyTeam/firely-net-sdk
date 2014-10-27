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
    /// Measurements and simple assertions
    /// </summary>
    [FhirType("Observation", IsResource=true)]
    [DataContract]
    public partial class Observation : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Codes that provide an estimate of the degree to which quality issues have impacted on the value of an observation
        /// </summary>
        [FhirEnumeration("ObservationReliability")]
        public enum ObservationReliability
        {
            /// <summary>
            /// The result has no reliability concerns.
            /// </summary>
            [EnumLiteral("ok")]
            Ok,
            /// <summary>
            /// An early estimate of value; measurement is still occurring.
            /// </summary>
            [EnumLiteral("ongoing")]
            Ongoing,
            /// <summary>
            /// An early estimate of value; processing is still occurring.
            /// </summary>
            [EnumLiteral("early")]
            Early,
            /// <summary>
            /// The observation value should be treated with care.
            /// </summary>
            [EnumLiteral("questionable")]
            Questionable,
            /// <summary>
            /// The result has been generated while calibration is occurring.
            /// </summary>
            [EnumLiteral("calibrating")]
            Calibrating,
            /// <summary>
            /// The observation could not be completed because of an error.
            /// </summary>
            [EnumLiteral("error")]
            Error,
            /// <summary>
            /// No observation value was available.
            /// </summary>
            [EnumLiteral("unknown")]
            Unknown,
        }
        
        /// <summary>
        /// Codes providing the status of an observation
        /// </summary>
        [FhirEnumeration("ObservationStatus")]
        public enum ObservationStatus
        {
            /// <summary>
            /// The existence of the observation is registered, but there is no result yet available.
            /// </summary>
            [EnumLiteral("registered")]
            Registered,
            /// <summary>
            /// This is an initial or interim observation: data may be incomplete or unverified.
            /// </summary>
            [EnumLiteral("preliminary")]
            Preliminary,
            /// <summary>
            /// The observation is complete and verified by an authorized person.
            /// </summary>
            [EnumLiteral("final")]
            Final,
            /// <summary>
            /// The observation has been modified subsequent to being Final, and is complete and verified by an authorized person.
            /// </summary>
            [EnumLiteral("amended")]
            Amended,
            /// <summary>
            /// The observation is unavailable because the measurement was not started or not completed (also sometimes called "aborted").
            /// </summary>
            [EnumLiteral("cancelled")]
            Cancelled,
            /// <summary>
            /// The observation has been withdrawn following previous Final release.
            /// </summary>
            [EnumLiteral("entered in error")]
            EnteredInError,
        }
        
        /// <summary>
        /// Codes specifying how two observations are related
        /// </summary>
        [FhirEnumeration("ObservationRelationshipType")]
        public enum ObservationRelationshipType
        {
            /// <summary>
            /// The target observation is a component of this observation (e.g. Systolic and Diastolic Blood Pressure).
            /// </summary>
            [EnumLiteral("has-component")]
            HasComponent,
            /// <summary>
            /// This observation is a group observation (e.g. a battery, a panel of tests, a set of vital sign measurements) that includes the target as a member of the group.
            /// </summary>
            [EnumLiteral("has-member")]
            HasMember,
            /// <summary>
            /// The target observation is part of the information from which this observation value is derived (e.g. calculated anion gap, Apgar score).
            /// </summary>
            [EnumLiteral("derived-from")]
            DerivedFrom,
            /// <summary>
            /// This observation follows the target observation (e.g. timed tests such as Glucose Tolerance Test).
            /// </summary>
            [EnumLiteral("sequel-to")]
            SequelTo,
            /// <summary>
            /// This observation replaces a previous observation (i.e. a revised value). The target observation is now obsolete.
            /// </summary>
            [EnumLiteral("replaces")]
            Replaces,
            /// <summary>
            /// The value of the target observation qualifies (refines) the semantics of the source observation (e.g. a lipaemia measure target from a plasma measure).
            /// </summary>
            [EnumLiteral("qualified-by")]
            QualifiedBy,
            /// <summary>
            /// The value of the target observation interferes (degardes quality, or prevents valid observation) with the semantics of the source observation (e.g. a hemolysis measure target from a plasma potassium measure which has no value).
            /// </summary>
            [EnumLiteral("interfered-by")]
            InterferedBy,
        }
        
        [FhirType("ObservationReferenceRangeComponent")]
        [DataContract]
        public partial class ObservationReferenceRangeComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Low Range, if relevant
            /// </summary>
            [FhirElement("low", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Low
            {
                get { return _Low; }
                set { _Low = value; OnPropertyChanged("Low"); }
            }
            private Hl7.Fhir.Model.Quantity _Low;
            
            /// <summary>
            /// High Range, if relevant
            /// </summary>
            [FhirElement("high", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity High
            {
                get { return _High; }
                set { _High = value; OnPropertyChanged("High"); }
            }
            private Hl7.Fhir.Model.Quantity _High;
            
            /// <summary>
            /// Indicates the meaning/use of this range of this range
            /// </summary>
            [FhirElement("meaning", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Meaning
            {
                get { return _Meaning; }
                set { _Meaning = value; OnPropertyChanged("Meaning"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Meaning;
            
            /// <summary>
            /// Applicable age range, if relevant
            /// </summary>
            [FhirElement("age", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Range Age
            {
                get { return _Age; }
                set { _Age = value; OnPropertyChanged("Age"); }
            }
            private Hl7.Fhir.Model.Range _Age;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ObservationReferenceRangeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Low != null) dest.Low = (Hl7.Fhir.Model.Quantity)Low.DeepCopy();
                    if(High != null) dest.High = (Hl7.Fhir.Model.Quantity)High.DeepCopy();
                    if(Meaning != null) dest.Meaning = (Hl7.Fhir.Model.CodeableConcept)Meaning.DeepCopy();
                    if(Age != null) dest.Age = (Hl7.Fhir.Model.Range)Age.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ObservationReferenceRangeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ObservationReferenceRangeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Low, otherT.Low)) return false;
                if( !DeepComparable.Matches(High, otherT.High)) return false;
                if( !DeepComparable.Matches(Meaning, otherT.Meaning)) return false;
                if( !DeepComparable.Matches(Age, otherT.Age)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ObservationReferenceRangeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Low, otherT.Low)) return false;
                if( !DeepComparable.IsExactly(High, otherT.High)) return false;
                if( !DeepComparable.IsExactly(Meaning, otherT.Meaning)) return false;
                if( !DeepComparable.IsExactly(Age, otherT.Age)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ObservationRelatedComponent")]
        [DataContract]
        public partial class ObservationRelatedComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// has-component | has-member | derived-from | sequel-to | replaces | qualified-by | interfered-by
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Observation.ObservationRelationshipType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Code<Hl7.Fhir.Model.Observation.ObservationRelationshipType> _TypeElement;
            
            /// <summary>
            /// has-component | has-member | derived-from | sequel-to | replaces | qualified-by | interfered-by
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Observation.ObservationRelationshipType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.Observation.ObservationRelationshipType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Observation that is related to this one
            /// </summary>
            [FhirElement("target", InSummary=true, Order=50)]
            [References("Observation")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Target;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ObservationRelatedComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Observation.ObservationRelationshipType>)TypeElement.DeepCopy();
                    if(Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ObservationRelatedComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ObservationRelatedComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ObservationRelatedComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Type of observation (code / type)
        /// </summary>
        [FhirElement("name", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Name;
        
        /// <summary>
        /// Actual result
        /// </summary>
        [FhirElement("value", Order=80, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.SampledData),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element Value
        {
            get { return _Value; }
            set { _Value = value; OnPropertyChanged("Value"); }
        }
        private Hl7.Fhir.Model.Element _Value;
        
        /// <summary>
        /// High, low, normal, etc.
        /// </summary>
        [FhirElement("interpretation", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Interpretation
        {
            get { return _Interpretation; }
            set { _Interpretation = value; OnPropertyChanged("Interpretation"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Interpretation;
        
        /// <summary>
        /// Comments about result
        /// </summary>
        [FhirElement("comments", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentsElement
        {
            get { return _CommentsElement; }
            set { _CommentsElement = value; OnPropertyChanged("CommentsElement"); }
        }
        private Hl7.Fhir.Model.FhirString _CommentsElement;
        
        /// <summary>
        /// Comments about result
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comments
        {
            get { return CommentsElement != null ? CommentsElement.Value : null; }
            set
            {
                if(value == null)
                  CommentsElement = null; 
                else
                  CommentsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Comments");
            }
        }
        
        /// <summary>
        /// Physiologically Relevant time/time-period for observation
        /// </summary>
        [FhirElement("applies", Order=110, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Applies
        {
            get { return _Applies; }
            set { _Applies = value; OnPropertyChanged("Applies"); }
        }
        private Hl7.Fhir.Model.Element _Applies;
        
        /// <summary>
        /// Date/Time this was made available
        /// </summary>
        [FhirElement("issued", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Instant IssuedElement
        {
            get { return _IssuedElement; }
            set { _IssuedElement = value; OnPropertyChanged("IssuedElement"); }
        }
        private Hl7.Fhir.Model.Instant _IssuedElement;
        
        /// <summary>
        /// Date/Time this was made available
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Issued
        {
            get { return IssuedElement != null ? IssuedElement.Value : null; }
            set
            {
                if(value == null)
                  IssuedElement = null; 
                else
                  IssuedElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Issued");
            }
        }
        
        /// <summary>
        /// registered | preliminary | final | amended +
        /// </summary>
        [FhirElement("status", Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Observation.ObservationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.Observation.ObservationStatus> _StatusElement;
        
        /// <summary>
        /// registered | preliminary | final | amended +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Observation.ObservationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Observation.ObservationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// ok | ongoing | early | questionable | calibrating | error +
        /// </summary>
        [FhirElement("reliability", Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Observation.ObservationReliability> ReliabilityElement
        {
            get { return _ReliabilityElement; }
            set { _ReliabilityElement = value; OnPropertyChanged("ReliabilityElement"); }
        }
        private Code<Hl7.Fhir.Model.Observation.ObservationReliability> _ReliabilityElement;
        
        /// <summary>
        /// ok | ongoing | early | questionable | calibrating | error +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Observation.ObservationReliability? Reliability
        {
            get { return ReliabilityElement != null ? ReliabilityElement.Value : null; }
            set
            {
                if(value == null)
                  ReliabilityElement = null; 
                else
                  ReliabilityElement = new Code<Hl7.Fhir.Model.Observation.ObservationReliability>(value);
                OnPropertyChanged("Reliability");
            }
        }
        
        /// <summary>
        /// Observed body part
        /// </summary>
        [FhirElement("bodySite", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept BodySite
        {
            get { return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _BodySite;
        
        /// <summary>
        /// How it was done
        /// </summary>
        [FhirElement("method", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Method
        {
            get { return _Method; }
            set { _Method = value; OnPropertyChanged("Method"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Method;
        
        /// <summary>
        /// Unique Id for this particular observation
        /// </summary>
        [FhirElement("identifier", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Who and/or what this is about
        /// </summary>
        [FhirElement("subject", Order=180)]
        [References("Patient","Group","Device","Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Specimen used for this observation
        /// </summary>
        [FhirElement("specimen", Order=190)]
        [References("Specimen")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Specimen
        {
            get { return _Specimen; }
            set { _Specimen = value; OnPropertyChanged("Specimen"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Specimen;
        
        /// <summary>
        /// Who did the observation
        /// </summary>
        [FhirElement("performer", Order=200)]
        [References("Practitioner","Device","Organization")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Performer
        {
            get { return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Performer;
        
        /// <summary>
        /// Provides guide for interpretation
        /// </summary>
        [FhirElement("referenceRange", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Observation.ObservationReferenceRangeComponent> ReferenceRange
        {
            get { return _ReferenceRange; }
            set { _ReferenceRange = value; OnPropertyChanged("ReferenceRange"); }
        }
        private List<Hl7.Fhir.Model.Observation.ObservationReferenceRangeComponent> _ReferenceRange;
        
        /// <summary>
        /// Observations related to this observation
        /// </summary>
        [FhirElement("related", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Observation.ObservationRelatedComponent> Related
        {
            get { return _Related; }
            set { _Related = value; OnPropertyChanged("Related"); }
        }
        private List<Hl7.Fhir.Model.Observation.ObservationRelatedComponent> _Related;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Observation;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Name != null) dest.Name = (Hl7.Fhir.Model.CodeableConcept)Name.DeepCopy();
                if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                if(Interpretation != null) dest.Interpretation = (Hl7.Fhir.Model.CodeableConcept)Interpretation.DeepCopy();
                if(CommentsElement != null) dest.CommentsElement = (Hl7.Fhir.Model.FhirString)CommentsElement.DeepCopy();
                if(Applies != null) dest.Applies = (Hl7.Fhir.Model.Element)Applies.DeepCopy();
                if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.Instant)IssuedElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Observation.ObservationStatus>)StatusElement.DeepCopy();
                if(ReliabilityElement != null) dest.ReliabilityElement = (Code<Hl7.Fhir.Model.Observation.ObservationReliability>)ReliabilityElement.DeepCopy();
                if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.CodeableConcept)BodySite.DeepCopy();
                if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Specimen != null) dest.Specimen = (Hl7.Fhir.Model.ResourceReference)Specimen.DeepCopy();
                if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ResourceReference>(Performer.DeepCopy());
                if(ReferenceRange != null) dest.ReferenceRange = new List<Hl7.Fhir.Model.Observation.ObservationReferenceRangeComponent>(ReferenceRange.DeepCopy());
                if(Related != null) dest.Related = new List<Hl7.Fhir.Model.Observation.ObservationRelatedComponent>(Related.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Observation());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Observation;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Name, otherT.Name)) return false;
            if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            if( !DeepComparable.Matches(Interpretation, otherT.Interpretation)) return false;
            if( !DeepComparable.Matches(CommentsElement, otherT.CommentsElement)) return false;
            if( !DeepComparable.Matches(Applies, otherT.Applies)) return false;
            if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ReliabilityElement, otherT.ReliabilityElement)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Method, otherT.Method)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(ReferenceRange, otherT.ReferenceRange)) return false;
            if( !DeepComparable.Matches(Related, otherT.Related)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Observation;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
            if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            if( !DeepComparable.IsExactly(Interpretation, otherT.Interpretation)) return false;
            if( !DeepComparable.IsExactly(CommentsElement, otherT.CommentsElement)) return false;
            if( !DeepComparable.IsExactly(Applies, otherT.Applies)) return false;
            if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ReliabilityElement, otherT.ReliabilityElement)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(ReferenceRange, otherT.ReferenceRange)) return false;
            if( !DeepComparable.IsExactly(Related, otherT.Related)) return false;
            
            return true;
        }
        
    }
    
}
