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
    /// (informative) A response to a scheduled appointment for a patient and/or practitioner(s)
    /// </summary>
    [FhirType("AppointmentResponse", IsResource=true)]
    [DataContract]
    public partial class AppointmentResponse : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The Participation status of an appointment
        /// </summary>
        [FhirEnumeration("ParticipantStatus")]
        public enum ParticipantStatus
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
        /// External Ids for this item
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// Parent appointment that this response is replying to
        /// </summary>
        [FhirElement("appointment", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Appointment { get; set; }
        
        /// <summary>
        /// Role of participant in the appointment
        /// </summary>
        [FhirElement("participantType", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ParticipantType { get; set; }
        
        /// <summary>
        /// A Person of device that is participating in the appointment
        /// </summary>
        [FhirElement("individual", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Individual { get; set; }
        
        /// <summary>
        /// accepted | declined | tentative | in-process | completed | needs-action
        /// </summary>
        [FhirElement("participantStatus", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.AppointmentResponse.ParticipantStatus> ParticipantStatus_Element { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.AppointmentResponse.ParticipantStatus? ParticipantStatus_
        {
            get { return ParticipantStatus_Element != null ? ParticipantStatus_Element.Value : null; }
            set
            {
                if(value == null)
                  ParticipantStatus_Element = null; 
                else
                  ParticipantStatus_Element = new Code<Hl7.Fhir.Model.AppointmentResponse.ParticipantStatus>(value);
            }
        }
        
        /// <summary>
        /// Additional comments about the appointment
        /// </summary>
        [FhirElement("comment", Order=120)]
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
        /// Date/Time that the appointment is to take place
        /// </summary>
        [FhirElement("start", Order=130)]
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
        [FhirElement("end", Order=140)]
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
        [FhirElement("schedule", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Schedule Schedule { get; set; }
        
        /// <summary>
        /// The timezone that the times are to be converted to. Required for recurring appointments to remain accurate where the schedule makes the appointment cross a daylight saving boundry
        /// </summary>
        [FhirElement("timezone", Order=160)]
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
        /// Who recorded the appointment response
        /// </summary>
        [FhirElement("recorder", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recorder { get; set; }
        
        /// <summary>
        /// Date when the response was recorded or last updated
        /// </summary>
        [FhirElement("recordedDate", Order=180)]
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
