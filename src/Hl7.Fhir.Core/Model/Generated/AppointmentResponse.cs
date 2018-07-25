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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A reply to an appointment request for a patient and/or practitioner(s), such as a confirmation or rejection
    /// </summary>
    [FhirType("AppointmentResponse", IsResource=true)]
    [DataContract]
    public partial class AppointmentResponse : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.AppointmentResponse; } }
        [NotMapped]
        public override string TypeName { get { return "AppointmentResponse"; } }
        
        /// <summary>
        /// The Participation status of an appointment.
        /// (url: http://hl7.org/fhir/ValueSet/participantstatus)
        /// </summary>
        [FhirEnumeration("ParticipantStatus")]
        public enum ParticipantStatus
        {
            /// <summary>
            /// The appointment participant has accepted that they can attend the appointment at the time specified in the AppointmentResponse.
            /// (system: http://hl7.org/fhir/participantstatus)
            /// </summary>
            [EnumLiteral("accepted", "http://hl7.org/fhir/participantstatus"), Description("Accepted")]
            Accepted,
            /// <summary>
            /// The appointment participant has declined the appointment.
            /// (system: http://hl7.org/fhir/participantstatus)
            /// </summary>
            [EnumLiteral("declined", "http://hl7.org/fhir/participantstatus"), Description("Declined")]
            Declined,
            /// <summary>
            /// The appointment participant has tentatively accepted the appointment.
            /// (system: http://hl7.org/fhir/participantstatus)
            /// </summary>
            [EnumLiteral("tentative", "http://hl7.org/fhir/participantstatus"), Description("Tentative")]
            Tentative,
            /// <summary>
            /// The participant has in-process the appointment.
            /// (system: http://hl7.org/fhir/participantstatus)
            /// </summary>
            [EnumLiteral("in-process", "http://hl7.org/fhir/participantstatus"), Description("In Process")]
            InProcess,
            /// <summary>
            /// The participant has completed the appointment.
            /// (system: http://hl7.org/fhir/participantstatus)
            /// </summary>
            [EnumLiteral("completed", "http://hl7.org/fhir/participantstatus"), Description("Completed")]
            Completed,
            /// <summary>
            /// This is the intitial status of an appointment participant until a participant has replied. It implies that there is no commitment for the appointment.
            /// (system: http://hl7.org/fhir/participantstatus)
            /// </summary>
            [EnumLiteral("needs-action", "http://hl7.org/fhir/participantstatus"), Description("Needs Action")]
            NeedsAction,
        }

        /// <summary>
        /// External Ids for this item
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
        /// Appointment this response relates to
        /// </summary>
        [FhirElement("appointment", InSummary=true, Order=100)]
        [CLSCompliant(false)]
		[References("Appointment")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Appointment
        {
            get { return _Appointment; }
            set { _Appointment = value; OnPropertyChanged("Appointment"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Appointment;
        
        /// <summary>
        /// Time from appointment, or requested new start time
        /// </summary>
        [FhirElement("start", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Instant StartElement
        {
            get { return _StartElement; }
            set { _StartElement = value; OnPropertyChanged("StartElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _StartElement;
        
        /// <summary>
        /// Time from appointment, or requested new start time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Start
        {
            get { return StartElement != null ? StartElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StartElement = null; 
                else
                  StartElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Start");
            }
        }
        
        /// <summary>
        /// Time from appointment, or requested new end time
        /// </summary>
        [FhirElement("end", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Instant EndElement
        {
            get { return _EndElement; }
            set { _EndElement = value; OnPropertyChanged("EndElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _EndElement;
        
        /// <summary>
        /// Time from appointment, or requested new end time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? End
        {
            get { return EndElement != null ? EndElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  EndElement = null; 
                else
                  EndElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("End");
            }
        }
        
        /// <summary>
        /// Role of participant in the appointment
        /// </summary>
        [FhirElement("participantType", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ParticipantType
        {
            get { if(_ParticipantType==null) _ParticipantType = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ParticipantType; }
            set { _ParticipantType = value; OnPropertyChanged("ParticipantType"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ParticipantType;
        
        /// <summary>
        /// Person, Location/HealthcareService or Device
        /// </summary>
        [FhirElement("actor", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References("Patient","Practitioner","RelatedPerson","Device","HealthcareService","Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Actor
        {
            get { return _Actor; }
            set { _Actor = value; OnPropertyChanged("Actor"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Actor;
        
        /// <summary>
        /// accepted | declined | tentative | in-process | completed | needs-action
        /// </summary>
        [FhirElement("participantStatus", InSummary=true, Order=150)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AppointmentResponse.ParticipantStatus> ParticipantStatus_Element
        {
            get { return _ParticipantStatus_Element; }
            set { _ParticipantStatus_Element = value; OnPropertyChanged("ParticipantStatus_Element"); }
        }
        
        private Code<Hl7.Fhir.Model.AppointmentResponse.ParticipantStatus> _ParticipantStatus_Element;
        
        /// <summary>
        /// accepted | declined | tentative | in-process | completed | needs-action
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AppointmentResponse.ParticipantStatus? ParticipantStatus_
        {
            get { return ParticipantStatus_Element != null ? ParticipantStatus_Element.Value : null; }
            set
            {
                if (!value.HasValue)
                  ParticipantStatus_Element = null; 
                else
                  ParticipantStatus_Element = new Code<Hl7.Fhir.Model.AppointmentResponse.ParticipantStatus>(value);
                OnPropertyChanged("ParticipantStatus_");
            }
        }
        
        /// <summary>
        /// Additional comments
        /// </summary>
        [FhirElement("comment", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentElement
        {
            get { return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CommentElement;
        
        /// <summary>
        /// Additional comments
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comment
        {
            get { return CommentElement != null ? CommentElement.Value : null; }
            set
            {
                if (value == null)
                  CommentElement = null; 
                else
                  CommentElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Comment");
            }
        }
        

        public static ElementDefinition.ConstraintComponent AppointmentResponse_APR_1 = new ElementDefinition.ConstraintComponent()
        {
            Extension = new List<Model.Extension>() { new Model.Extension("http://hl7.org/fhir/StructureDefinition/structuredefinition-expression", new FhirString("participantType or actor"))},
            Key = "apr-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Either the participantType or actor must be specified",
            Xpath = "(exists(f:participantType) or exists(f:actor))"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(AppointmentResponse_APR_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as AppointmentResponse;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Appointment != null) dest.Appointment = (Hl7.Fhir.Model.ResourceReference)Appointment.DeepCopy();
                if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Instant)StartElement.DeepCopy();
                if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Instant)EndElement.DeepCopy();
                if(ParticipantType != null) dest.ParticipantType = new List<Hl7.Fhir.Model.CodeableConcept>(ParticipantType.DeepCopy());
                if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                if(ParticipantStatus_Element != null) dest.ParticipantStatus_Element = (Code<Hl7.Fhir.Model.AppointmentResponse.ParticipantStatus>)ParticipantStatus_Element.DeepCopy();
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new AppointmentResponse());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as AppointmentResponse;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Appointment, otherT.Appointment)) return false;
            if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
            if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.Matches(ParticipantType, otherT.ParticipantType)) return false;
            if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
            if( !DeepComparable.Matches(ParticipantStatus_Element, otherT.ParticipantStatus_Element)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as AppointmentResponse;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Appointment, otherT.Appointment)) return false;
            if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
            if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.IsExactly(ParticipantType, otherT.ParticipantType)) return false;
            if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
            if( !DeepComparable.IsExactly(ParticipantStatus_Element, otherT.ParticipantStatus_Element)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (Appointment != null) yield return Appointment;
				if (StartElement != null) yield return StartElement;
				if (EndElement != null) yield return EndElement;
				foreach (var elem in ParticipantType) { if (elem != null) yield return elem; }
				if (Actor != null) yield return Actor;
				if (ParticipantStatus_Element != null) yield return ParticipantStatus_Element;
				if (CommentElement != null) yield return CommentElement;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Appointment != null) yield return new ElementValue("appointment", Appointment);
                if (StartElement != null) yield return new ElementValue("start", StartElement);
                if (EndElement != null) yield return new ElementValue("end", EndElement);
                foreach (var elem in ParticipantType) { if (elem != null) yield return new ElementValue("participantType", elem); }
                if (Actor != null) yield return new ElementValue("actor", Actor);
                if (ParticipantStatus_Element != null) yield return new ElementValue("participantStatus", ParticipantStatus_Element);
                if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
            }
        }

    }
    
}
