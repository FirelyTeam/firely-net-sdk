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
// Generated on Fri, Jan 24, 2014 09:44-0600 for FHIR v0.12
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// (informative) A scheduled appointment for a patient and/or practitioner(s) where a service may take place
    /// </summary>
    [FhirType("Appointment", IsResource=true)]
    [DataContract]
    public partial class Appointment : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The free/busy status of an appointment
        /// </summary>
        [FhirEnumeration("AppointmentStatus")]
        public enum AppointmentStatus
        {
            [EnumLiteral("busy")]
            Busy, // The participant(s) will be unavailable during this appointment.
            [EnumLiteral("free")]
            Free, // The participant(s) will still be available during this appointment.
            [EnumLiteral("tentative")]
            Tentative, // This appointment has not been confirmed, and may become available.
            [EnumLiteral("outofoffice")]
            Outofoffice, // The participant(s) will not be at the usual location.
        }
        
        /// <summary>
        /// Is the Participant required to attend the appointment
        /// </summary>
        [FhirEnumeration("ParticipantRequired")]
        public enum ParticipantRequired
        {
            [EnumLiteral("required")]
            Required, // The participant is required to attend the appointment.
            [EnumLiteral("optional")]
            Optional, // The participant may optionally attend the appointment.
            [EnumLiteral("information-only")]
            InformationOnly, // The participant is not required to attend the appointment (appointment is about them, not for them).
        }
        
        /// <summary>
        /// The Participation status of an appointment
        /// </summary>
        [FhirEnumeration("ParticipationStatus")]
        public enum ParticipationStatus
        {
            [EnumLiteral("accepted")]
            Accepted, // The participant has accepted the appointment.
            [EnumLiteral("declined")]
            Declined, // The participant has declined the appointment.
            [EnumLiteral("tentative")]
            Tentative, // The participant has tentative the appointment.
            [EnumLiteral("in-process")]
            InProcess, // The participant has in-process the appointment.
            [EnumLiteral("completed")]
            Completed, // The participant has completed the appointment.
            [EnumLiteral("needs-action")]
            NeedsAction, // The participant has needs-action the appointment.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("AppointmentParticipantComponent")]
        [DataContract]
        public partial class AppointmentParticipantComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Role of participant in the appointment
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Type { get; set; }
            
            /// <summary>
            /// A Person of device that is participating in the appointment
            /// </summary>
            [FhirElement("individual", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Individual { get; set; }
            
            /// <summary>
            /// required | optional | information-only
            /// </summary>
            [FhirElement("required", Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Appointment.ParticipantRequired> RequiredElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// accepted | declined | tentative | in-process | completed | needs-action
            /// </summary>
            [FhirElement("status", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Appointment.ParticipationStatus> StatusElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Observations that lead to the creation of this appointment. (Is this 80%)
            /// </summary>
            [FhirElement("observation", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Observation { get; set; }
            
        }
        
        
        /// <summary>
        /// External Ids for this item
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// The priority of the appointment. Can be used to make informed decisions if needing to re-prioritize appointments. (The iCal Standard specifies 0 as undefined, 1 as highest, 9 as lowest priority) (Need to change back to CodeableConcept)
        /// </summary>
        [FhirElement("priority", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.Integer PriorityElement { get; set; }
        
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
                  PriorityElement = new Hl7.Fhir.Model.Integer(value);
            }
        }
        
        /// <summary>
        /// The overall status of the Appointment
        /// </summary>
        [FhirElement("status", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Code StatusElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Hl7.Fhir.Model.Code(value);
            }
        }
        
        /// <summary>
        /// The brief description of the appointment as would be shown on a subject line in a meeting request, or appointment list. Detailed or expanded information should be put in the comment field
        /// </summary>
        [FhirElement("description", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Date/Time that the appointment is to take place
        /// </summary>
        [FhirElement("start", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant StartElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Date/Time that the appointment is to conclude
        /// </summary>
        [FhirElement("end", Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant EndElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// The recurrence schedule for the appointment. The end date in the schedule marks the end of the recurrence(s), not the end of an individual appointment
        /// </summary>
        [FhirElement("schedule", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Schedule Schedule { get; set; }
        
        /// <summary>
        /// The timezone that the times are to be converted to. Required for recurring appointments to remain accurate where the schedule makes the appointment cross a daylight saving boundry
        /// </summary>
        [FhirElement("timezone", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TimezoneElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Timezone
        {
            get { return TimezoneElement != null ? TimezoneElement.Value : null; }
            set
            {
                if(value == null)
                  TimezoneElement = null; 
                else
                  TimezoneElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// The slot that this appointment is filling. If provided then the schedule will not be provided as slots are not recursive, and the start/end values MUST be the same as from the slot
        /// </summary>
        [FhirElement("slot", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Slot { get; set; }
        
        /// <summary>
        /// The primary location that this appointment is to take place
        /// </summary>
        [FhirElement("location", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location { get; set; }
        
        /// <summary>
        /// Additional comments about the appointment
        /// </summary>
        [FhirElement("comment", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// An Order that lead to the creation of this appointment
        /// </summary>
        [FhirElement("order", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Order { get; set; }
        
        /// <summary>
        /// List of participants involved in the appointment
        /// </summary>
        [FhirElement("participant", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Appointment.AppointmentParticipantComponent> Participant { get; set; }
        
        /// <summary>
        /// Who recorded the appointment
        /// </summary>
        [FhirElement("recorder", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recorder { get; set; }
        
        /// <summary>
        /// Date when the sensitivity was recorded
        /// </summary>
        [FhirElement("recordedDate", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime RecordedDateElement { get; set; }
        
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
            }
        }
        
    }
    
}
