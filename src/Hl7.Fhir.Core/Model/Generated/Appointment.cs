using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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

//
// Generated on Thu, Apr 2, 2015 14:21+0200 for FHIR v0.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A booking of a healthcare event among patient(s), practitioner(s), related person(s) and/or device(s) for a specific date/time. This may result in one or more Encounter(s)
    /// </summary>
    [FhirType("Appointment", IsResource=true)]
    [DataContract]
    public partial class Appointment : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Appointment; } }
        [NotMapped]
        public override string TypeName { get { return "Appointment"; } }
        
        /// <summary>
        /// The free/busy status of an appointment
        /// </summary>
        [FhirEnumeration("AppointmentStatus")]
        public enum AppointmentStatus
        {
            /// <summary>
            /// Some or all of the participant(s) have not finalized their acceptance of the appointment request.
            /// </summary>
            [EnumLiteral("pending")]
            Pending,
            /// <summary>
            /// All participant(s) have been considered and the appointment is confirmed to go ahead at the date/times specified.
            /// </summary>
            [EnumLiteral("booked")]
            Booked,
            /// <summary>
            /// Some of the patients have arrived.
            /// </summary>
            [EnumLiteral("arrived")]
            Arrived,
            /// <summary>
            /// This appointment has completed and may have resulted in an encounter.
            /// </summary>
            [EnumLiteral("fulfilled")]
            Fulfilled,
            /// <summary>
            /// The appointment has been cancelled.
            /// </summary>
            [EnumLiteral("cancelled")]
            Cancelled,
            /// <summary>
            /// Some or all of the participant(s) have not/did not appear for the appointment (usually the patient).
            /// </summary>
            [EnumLiteral("noshow")]
            Noshow,
        }
        
        /// <summary>
        /// Is the Participant required to attend the appointment
        /// </summary>
        [FhirEnumeration("ParticipantRequired")]
        public enum ParticipantRequired
        {
            /// <summary>
            /// The participant is required to attend the appointment.
            /// </summary>
            [EnumLiteral("required")]
            Required,
            /// <summary>
            /// The participant may optionally attend the appointment.
            /// </summary>
            [EnumLiteral("optional")]
            Optional,
            /// <summary>
            /// The participant is excluded from the appointment, and may not be informed of the appointment taking place. (appointment is about them, not for them - such as 2 doctors discussing results about a patient's test).
            /// </summary>
            [EnumLiteral("information-only")]
            InformationOnly,
        }
        
        /// <summary>
        /// The Participation status of an appointment
        /// </summary>
        [FhirEnumeration("ParticipationStatus")]
        public enum ParticipationStatus
        {
            /// <summary>
            /// The participant has accepted the appointment.
            /// </summary>
            [EnumLiteral("accepted")]
            Accepted,
            /// <summary>
            /// The participant has declined the appointment and will not participate in the appointment.
            /// </summary>
            [EnumLiteral("declined")]
            Declined,
            /// <summary>
            /// The participant has  tentatively accepted the appointment. This could be automatically created by a system and requires further processing before it can be accepted. There is no commitment that attendance will occur.
            /// </summary>
            [EnumLiteral("tentative")]
            Tentative,
            /// <summary>
            /// The participant needs to indicate if they accept the appointment by changing this status to one of the other statuses.
            /// </summary>
            [EnumLiteral("needs-action")]
            NeedsAction,
        }
        
        [FhirType("AppointmentParticipantComponent")]
        [DataContract]
        public partial class AppointmentParticipantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "AppointmentParticipantComponent"; } }
            
            /// <summary>
            /// Role of participant in the appointment
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Type;
            
            /// <summary>
            /// A Person, Location/HealthcareService or Device that is participating in the appointment
            /// </summary>
            [FhirElement("actor", InSummary=true, Order=50)]
            [References("Patient","Practitioner","RelatedPerson","Device","HealthcareService","Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Actor
            {
                get { return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Actor;
            
            /// <summary>
            /// required | optional | information-only
            /// </summary>
            [FhirElement("required", InSummary=true, Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Appointment.ParticipantRequired> RequiredElement
            {
                get { return _RequiredElement; }
                set { _RequiredElement = value; OnPropertyChanged("RequiredElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Appointment.ParticipantRequired> _RequiredElement;
            
            /// <summary>
            /// required | optional | information-only
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Appointment.ParticipantRequired? Required
            {
                get { return RequiredElement != null ? RequiredElement.Value : null; }
                set
                {
                    if(value == null)
                      RequiredElement = null; 
                    else
                      RequiredElement = new Code<Hl7.Fhir.Model.Appointment.ParticipantRequired>(value);
                    OnPropertyChanged("Required");
                }
            }
            
            /// <summary>
            /// accepted | declined | tentative | needs-action
            /// </summary>
            [FhirElement("status", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Appointment.ParticipationStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Appointment.ParticipationStatus> _StatusElement;
            
            /// <summary>
            /// accepted | declined | tentative | needs-action
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Appointment.ParticipationStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if(value == null)
                      StatusElement = null; 
                    else
                      StatusElement = new Code<Hl7.Fhir.Model.Appointment.ParticipationStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AppointmentParticipantComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    if(RequiredElement != null) dest.RequiredElement = (Code<Hl7.Fhir.Model.Appointment.ParticipantRequired>)RequiredElement.DeepCopy();
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Appointment.ParticipationStatus>)StatusElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AppointmentParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AppointmentParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                if( !DeepComparable.Matches(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AppointmentParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                if( !DeepComparable.IsExactly(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                
                return true;
            }
            
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
        /// pending | booked | arrived | fulfilled | cancelled | noshow
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Appointment.AppointmentStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Appointment.AppointmentStatus> _StatusElement;
        
        /// <summary>
        /// pending | booked | arrived | fulfilled | cancelled | noshow
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Appointment.AppointmentStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Appointment.AppointmentStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// The type of appointment that is being booked
        /// </summary>
        [FhirElement("type", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// The reason that this appointment is being scheduled, this is more clinical than administrative
        /// </summary>
        [FhirElement("reason", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Reason;
        
        /// <summary>
        /// The priority of the appointment. Can be used to make informed decisions if needing to re-prioritize appointments. (The iCal Standard specifies 0 as undefined, 1 as highest, 9 as lowest priority)
        /// </summary>
        [FhirElement("priority", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _PriorityElement;
        
        /// <summary>
        /// The priority of the appointment. Can be used to make informed decisions if needing to re-prioritize appointments. (The iCal Standard specifies 0 as undefined, 1 as highest, 9 as lowest priority)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if(value == null)
                  PriorityElement = null; 
                else
                  PriorityElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// The brief description of the appointment as would be shown on a subject line in a meeting request, or appointment list. Detailed or expanded information should be put in the comment field
        /// </summary>
        [FhirElement("description", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// The brief description of the appointment as would be shown on a subject line in a meeting request, or appointment list. Detailed or expanded information should be put in the comment field
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if(value == null)
                  DescriptionElement = null; 
                else
                  DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// Date/Time that the appointment is to take place
        /// </summary>
        [FhirElement("start", InSummary=true, Order=150)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant StartElement
        {
            get { return _StartElement; }
            set { _StartElement = value; OnPropertyChanged("StartElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _StartElement;
        
        /// <summary>
        /// Date/Time that the appointment is to take place
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Start
        {
            get { return StartElement != null ? StartElement.Value : null; }
            set
            {
                if(value == null)
                  StartElement = null; 
                else
                  StartElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Start");
            }
        }
        
        /// <summary>
        /// Date/Time that the appointment is to conclude
        /// </summary>
        [FhirElement("end", InSummary=true, Order=160)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant EndElement
        {
            get { return _EndElement; }
            set { _EndElement = value; OnPropertyChanged("EndElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _EndElement;
        
        /// <summary>
        /// Date/Time that the appointment is to conclude
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? End
        {
            get { return EndElement != null ? EndElement.Value : null; }
            set
            {
                if(value == null)
                  EndElement = null; 
                else
                  EndElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("End");
            }
        }
        
        /// <summary>
        /// The slot that this appointment is filling. If provided then the schedule will not be provided as slots are not recursive, and the start/end values MUST be the same as from the slot
        /// </summary>
        [FhirElement("slot", Order=170)]
        [References("Slot")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Slot
        {
            get { if(_Slot==null) _Slot = new List<Hl7.Fhir.Model.ResourceReference>(); return _Slot; }
            set { _Slot = value; OnPropertyChanged("Slot"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Slot;
        
        /// <summary>
        /// Additional comments about the appointment
        /// </summary>
        [FhirElement("comment", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentElement
        {
            get { return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CommentElement;
        
        /// <summary>
        /// Additional comments about the appointment
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comment
        {
            get { return CommentElement != null ? CommentElement.Value : null; }
            set
            {
                if(value == null)
                  CommentElement = null; 
                else
                  CommentElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Comment");
            }
        }
        
        /// <summary>
        /// An Order that lead to the creation of this appointment
        /// </summary>
        [FhirElement("order", Order=190)]
        [References("Order")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Order
        {
            get { return _Order; }
            set { _Order = value; OnPropertyChanged("Order"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Order;
        
        /// <summary>
        /// List of participants involved in the appointment
        /// </summary>
        [FhirElement("participant", Order=200)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Appointment.AppointmentParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<Hl7.Fhir.Model.Appointment.AppointmentParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<Hl7.Fhir.Model.Appointment.AppointmentParticipantComponent> _Participant;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Appointment;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Appointment.AppointmentStatus>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Hl7.Fhir.Model.UnsignedInt)PriorityElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Instant)StartElement.DeepCopy();
                if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Instant)EndElement.DeepCopy();
                if(Slot != null) dest.Slot = new List<Hl7.Fhir.Model.ResourceReference>(Slot.DeepCopy());
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                if(Order != null) dest.Order = (Hl7.Fhir.Model.ResourceReference)Order.DeepCopy();
                if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.Appointment.AppointmentParticipantComponent>(Participant.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Appointment());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Appointment;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
            if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.Matches(Slot, otherT.Slot)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.Matches(Order, otherT.Order)) return false;
            if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Appointment;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
            if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.IsExactly(Slot, otherT.Slot)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.IsExactly(Order, otherT.Order)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            
            return true;
        }
        
    }
    
}
