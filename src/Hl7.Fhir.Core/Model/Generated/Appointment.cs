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
        /// The free/busy status of an appointment.
        /// (url: http://hl7.org/fhir/ValueSet/appointmentstatus)
        /// </summary>
        [FhirEnumeration("AppointmentStatus")]
        public enum AppointmentStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/appointmentstatus)
            /// </summary>
            [EnumLiteral("proposed", "http://hl7.org/fhir/appointmentstatus"), Description("Proposed")]
            Proposed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/appointmentstatus)
            /// </summary>
            [EnumLiteral("pending", "http://hl7.org/fhir/appointmentstatus"), Description("Pending")]
            Pending,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/appointmentstatus)
            /// </summary>
            [EnumLiteral("booked", "http://hl7.org/fhir/appointmentstatus"), Description("Booked")]
            Booked,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/appointmentstatus)
            /// </summary>
            [EnumLiteral("arrived", "http://hl7.org/fhir/appointmentstatus"), Description("Arrived")]
            Arrived,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/appointmentstatus)
            /// </summary>
            [EnumLiteral("fulfilled", "http://hl7.org/fhir/appointmentstatus"), Description("Fulfilled")]
            Fulfilled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/appointmentstatus)
            /// </summary>
            [EnumLiteral("cancelled", "http://hl7.org/fhir/appointmentstatus"), Description("Cancelled")]
            Cancelled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/appointmentstatus)
            /// </summary>
            [EnumLiteral("noshow", "http://hl7.org/fhir/appointmentstatus"), Description("No Show")]
            Noshow,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/appointmentstatus)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/appointmentstatus"), Description("Entered in error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/appointmentstatus)
            /// </summary>
            [EnumLiteral("checked-in", "http://hl7.org/fhir/appointmentstatus"), Description("Checked In")]
            CheckedIn,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/appointmentstatus)
            /// </summary>
            [EnumLiteral("waitlist", "http://hl7.org/fhir/appointmentstatus"), Description("Waitlisted")]
            Waitlist,
        }

        /// <summary>
        /// Is the Participant required to attend the appointment.
        /// (url: http://hl7.org/fhir/ValueSet/participantrequired)
        /// </summary>
        [FhirEnumeration("ParticipantRequired")]
        public enum ParticipantRequired
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/participantrequired)
            /// </summary>
            [EnumLiteral("required", "http://hl7.org/fhir/participantrequired"), Description("Required")]
            Required,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/participantrequired)
            /// </summary>
            [EnumLiteral("optional", "http://hl7.org/fhir/participantrequired"), Description("Optional")]
            Optional,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/participantrequired)
            /// </summary>
            [EnumLiteral("information-only", "http://hl7.org/fhir/participantrequired"), Description("Information Only")]
            InformationOnly,
        }

        [FhirType("ParticipantComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ParticipantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ParticipantComponent"; } }
            
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
            /// Person, Location/HealthcareService or Device
            /// </summary>
            [FhirElement("actor", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Patient","Practitioner","PractitionerRole","RelatedPerson","Device","HealthcareService","Location")]
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
                    if (!value.HasValue)
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
            public Code<Hl7.Fhir.Model.ParticipationStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ParticipationStatus> _StatusElement;
            
            /// <summary>
            /// accepted | declined | tentative | needs-action
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ParticipationStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        StatusElement = null; 
                    else
                        StatusElement = new Code<Hl7.Fhir.Model.ParticipationStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// Participation period of the actor
            /// </summary>
            [FhirElement("period", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParticipantComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    if(RequiredElement != null) dest.RequiredElement = (Code<Hl7.Fhir.Model.Appointment.ParticipantRequired>)RequiredElement.DeepCopy();
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ParticipationStatus>)StatusElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                if( !DeepComparable.Matches(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                if( !DeepComparable.IsExactly(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Type) { if (elem != null) yield return elem; }
                    if (Actor != null) yield return Actor;
                    if (RequiredElement != null) yield return RequiredElement;
                    if (StatusElement != null) yield return StatusElement;
                    if (Period != null) yield return Period;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                    if (Actor != null) yield return new ElementValue("actor", Actor);
                    if (RequiredElement != null) yield return new ElementValue("required", RequiredElement);
                    if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                    if (Period != null) yield return new ElementValue("period", Period);
                }
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
        /// proposed | pending | booked | arrived | fulfilled | cancelled | noshow | entered-in-error | checked-in | waitlist
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
        /// proposed | pending | booked | arrived | fulfilled | cancelled | noshow | entered-in-error | checked-in | waitlist
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Appointment.AppointmentStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Appointment.AppointmentStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// The coded reason for the appointment being cancelled
        /// </summary>
        [FhirElement("cancelationReason", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept CancelationReason
        {
            get { return _CancelationReason; }
            set { _CancelationReason = value; OnPropertyChanged("CancelationReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _CancelationReason;
        
        /// <summary>
        /// A broad categorization of the service that is to be performed during this appointment
        /// </summary>
        [FhirElement("serviceCategory", InSummary=true, Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ServiceCategory
        {
            get { if(_ServiceCategory==null) _ServiceCategory = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ServiceCategory; }
            set { _ServiceCategory = value; OnPropertyChanged("ServiceCategory"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ServiceCategory;
        
        /// <summary>
        /// The specific service that is to be performed during this appointment
        /// </summary>
        [FhirElement("serviceType", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ServiceType
        {
            get { if(_ServiceType==null) _ServiceType = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ServiceType; }
            set { _ServiceType = value; OnPropertyChanged("ServiceType"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ServiceType;
        
        /// <summary>
        /// The specialty of a practitioner that would be required to perform the service requested in this appointment
        /// </summary>
        [FhirElement("specialty", InSummary=true, Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Specialty
        {
            get { if(_Specialty==null) _Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Specialty; }
            set { _Specialty = value; OnPropertyChanged("Specialty"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Specialty;
        
        /// <summary>
        /// The style of appointment or patient that has been booked in the slot (not service type)
        /// </summary>
        [FhirElement("appointmentType", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept AppointmentType
        {
            get { return _AppointmentType; }
            set { _AppointmentType = value; OnPropertyChanged("AppointmentType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _AppointmentType;
        
        /// <summary>
        /// Coded reason this appointment is scheduled
        /// </summary>
        [FhirElement("reasonCode", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Reason the appointment is to take place (resource)
        /// </summary>
        [FhirElement("reasonReference", Order=170)]
        [CLSCompliant(false)]
		[References("Condition","Procedure","Observation","ImmunizationRecommendation")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// Used to make informed decisions if needing to re-prioritize
        /// </summary>
        [FhirElement("priority", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _PriorityElement;
        
        /// <summary>
        /// Used to make informed decisions if needing to re-prioritize
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  PriorityElement = null; 
                else
                  PriorityElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// Shown on a subject line in a meeting request, or appointment list
        /// </summary>
        [FhirElement("description", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Shown on a subject line in a meeting request, or appointment list
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
        /// Additional information to support the appointment
        /// </summary>
        [FhirElement("supportingInformation", Order=200)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SupportingInformation
        {
            get { if(_SupportingInformation==null) _SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(); return _SupportingInformation; }
            set { _SupportingInformation = value; OnPropertyChanged("SupportingInformation"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SupportingInformation;
        
        /// <summary>
        /// When appointment is to take place
        /// </summary>
        [FhirElement("start", InSummary=true, Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Instant StartElement
        {
            get { return _StartElement; }
            set { _StartElement = value; OnPropertyChanged("StartElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _StartElement;
        
        /// <summary>
        /// When appointment is to take place
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
        /// When appointment is to conclude
        /// </summary>
        [FhirElement("end", InSummary=true, Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Instant EndElement
        {
            get { return _EndElement; }
            set { _EndElement = value; OnPropertyChanged("EndElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _EndElement;
        
        /// <summary>
        /// When appointment is to conclude
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
        /// Can be less than start/end (e.g. estimate)
        /// </summary>
        [FhirElement("minutesDuration", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.PositiveInt MinutesDurationElement
        {
            get { return _MinutesDurationElement; }
            set { _MinutesDurationElement = value; OnPropertyChanged("MinutesDurationElement"); }
        }
        
        private Hl7.Fhir.Model.PositiveInt _MinutesDurationElement;
        
        /// <summary>
        /// Can be less than start/end (e.g. estimate)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? MinutesDuration
        {
            get { return MinutesDurationElement != null ? MinutesDurationElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  MinutesDurationElement = null; 
                else
                  MinutesDurationElement = new Hl7.Fhir.Model.PositiveInt(value);
                OnPropertyChanged("MinutesDuration");
            }
        }
        
        /// <summary>
        /// The slots that this appointment is filling
        /// </summary>
        [FhirElement("slot", Order=240)]
        [CLSCompliant(false)]
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
        /// The date that this appointment was initially created
        /// </summary>
        [FhirElement("created", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        /// <summary>
        /// The date that this appointment was initially created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Created
        {
            get { return CreatedElement != null ? CreatedElement.Value : null; }
            set
            {
                if (value == null)
                  CreatedElement = null; 
                else
                  CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// Additional comments
        /// </summary>
        [FhirElement("comment", Order=260)]
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
        
        /// <summary>
        /// Detailed information and instructions for the patient
        /// </summary>
        [FhirElement("patientInstruction", Order=270)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PatientInstructionElement
        {
            get { return _PatientInstructionElement; }
            set { _PatientInstructionElement = value; OnPropertyChanged("PatientInstructionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PatientInstructionElement;
        
        /// <summary>
        /// Detailed information and instructions for the patient
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PatientInstruction
        {
            get { return PatientInstructionElement != null ? PatientInstructionElement.Value : null; }
            set
            {
                if (value == null)
                  PatientInstructionElement = null; 
                else
                  PatientInstructionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("PatientInstruction");
            }
        }
        
        /// <summary>
        /// The service request this appointment is allocated to assess
        /// </summary>
        [FhirElement("basedOn", Order=280)]
        [CLSCompliant(false)]
		[References("ServiceRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Participants involved in appointment
        /// </summary>
        [FhirElement("participant", Order=290)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Appointment.ParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<Hl7.Fhir.Model.Appointment.ParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<Hl7.Fhir.Model.Appointment.ParticipantComponent> _Participant;
        
        /// <summary>
        /// Potential date/time interval(s) requested to allocate the appointment within
        /// </summary>
        [FhirElement("requestedPeriod", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Period> RequestedPeriod
        {
            get { if(_RequestedPeriod==null) _RequestedPeriod = new List<Hl7.Fhir.Model.Period>(); return _RequestedPeriod; }
            set { _RequestedPeriod = value; OnPropertyChanged("RequestedPeriod"); }
        }
        
        private List<Hl7.Fhir.Model.Period> _RequestedPeriod;
        

        public static ElementDefinition.ConstraintComponent Appointment_APP_4 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "Appointment.cancelationReason.exists() implies (Appointment.status='no-show' or Appointment.status='cancelled')",
            Key = "app-4",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Cancelation reason is only used for appointments that have been cancelled, or no-show",
            Xpath = "not(exists(f:cancellationReason)) or f:status/@value=('no-show', 'cancelled')"
        };

        public static ElementDefinition.ConstraintComponent Appointment_APP_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "(start.exists() and end.exists()) or (status in ('proposed' | 'cancelled' | 'waitlist'))",
            Key = "app-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Only proposed or cancelled appointments can be missing start/end dates",
            Xpath = "((exists(f:start) and exists(f:end)) or (f:status/@value='proposed') or (f:status/@value='cancelled') or (f:status/@value='waitlist'))"
        };

        public static ElementDefinition.ConstraintComponent Appointment_APP_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "start.exists() = end.exists()",
            Key = "app-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Either start and end are specified, or neither",
            Xpath = "((exists(f:start) and exists(f:end)) or (not(exists(f:start)) and not(exists(f:end))))"
        };

        public static ElementDefinition.ConstraintComponent Appointment_APP_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "participant.all(type.exists() or actor.exists())",
            Key = "app-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Either the type or actor on the participant SHALL be specified",
            Xpath = "(exists(f:type) or exists(f:actor))"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(Appointment_APP_4);
            InvariantConstraints.Add(Appointment_APP_3);
            InvariantConstraints.Add(Appointment_APP_2);
            InvariantConstraints.Add(Appointment_APP_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Appointment;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Appointment.AppointmentStatus>)StatusElement.DeepCopy();
                if(CancelationReason != null) dest.CancelationReason = (Hl7.Fhir.Model.CodeableConcept)CancelationReason.DeepCopy();
                if(ServiceCategory != null) dest.ServiceCategory = new List<Hl7.Fhir.Model.CodeableConcept>(ServiceCategory.DeepCopy());
                if(ServiceType != null) dest.ServiceType = new List<Hl7.Fhir.Model.CodeableConcept>(ServiceType.DeepCopy());
                if(Specialty != null) dest.Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(Specialty.DeepCopy());
                if(AppointmentType != null) dest.AppointmentType = (Hl7.Fhir.Model.CodeableConcept)AppointmentType.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(PriorityElement != null) dest.PriorityElement = (Hl7.Fhir.Model.UnsignedInt)PriorityElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(SupportingInformation != null) dest.SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(SupportingInformation.DeepCopy());
                if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Instant)StartElement.DeepCopy();
                if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Instant)EndElement.DeepCopy();
                if(MinutesDurationElement != null) dest.MinutesDurationElement = (Hl7.Fhir.Model.PositiveInt)MinutesDurationElement.DeepCopy();
                if(Slot != null) dest.Slot = new List<Hl7.Fhir.Model.ResourceReference>(Slot.DeepCopy());
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                if(PatientInstructionElement != null) dest.PatientInstructionElement = (Hl7.Fhir.Model.FhirString)PatientInstructionElement.DeepCopy();
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.Appointment.ParticipantComponent>(Participant.DeepCopy());
                if(RequestedPeriod != null) dest.RequestedPeriod = new List<Hl7.Fhir.Model.Period>(RequestedPeriod.DeepCopy());
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
            if( !DeepComparable.Matches(CancelationReason, otherT.CancelationReason)) return false;
            if( !DeepComparable.Matches(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.Matches(ServiceType, otherT.ServiceType)) return false;
            if( !DeepComparable.Matches(Specialty, otherT.Specialty)) return false;
            if( !DeepComparable.Matches(AppointmentType, otherT.AppointmentType)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
            if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.Matches(MinutesDurationElement, otherT.MinutesDurationElement)) return false;
            if( !DeepComparable.Matches(Slot, otherT.Slot)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.Matches(PatientInstructionElement, otherT.PatientInstructionElement)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
            if( !DeepComparable.Matches(RequestedPeriod, otherT.RequestedPeriod)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Appointment;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(CancelationReason, otherT.CancelationReason)) return false;
            if( !DeepComparable.IsExactly(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.IsExactly(ServiceType, otherT.ServiceType)) return false;
            if( !DeepComparable.IsExactly(Specialty, otherT.Specialty)) return false;
            if( !DeepComparable.IsExactly(AppointmentType, otherT.AppointmentType)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
            if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.IsExactly(MinutesDurationElement, otherT.MinutesDurationElement)) return false;
            if( !DeepComparable.IsExactly(Slot, otherT.Slot)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.IsExactly(PatientInstructionElement, otherT.PatientInstructionElement)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            if( !DeepComparable.IsExactly(RequestedPeriod, otherT.RequestedPeriod)) return false;
            
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
				if (CancelationReason != null) yield return CancelationReason;
				foreach (var elem in ServiceCategory) { if (elem != null) yield return elem; }
				foreach (var elem in ServiceType) { if (elem != null) yield return elem; }
				foreach (var elem in Specialty) { if (elem != null) yield return elem; }
				if (AppointmentType != null) yield return AppointmentType;
				foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
				if (PriorityElement != null) yield return PriorityElement;
				if (DescriptionElement != null) yield return DescriptionElement;
				foreach (var elem in SupportingInformation) { if (elem != null) yield return elem; }
				if (StartElement != null) yield return StartElement;
				if (EndElement != null) yield return EndElement;
				if (MinutesDurationElement != null) yield return MinutesDurationElement;
				foreach (var elem in Slot) { if (elem != null) yield return elem; }
				if (CreatedElement != null) yield return CreatedElement;
				if (CommentElement != null) yield return CommentElement;
				if (PatientInstructionElement != null) yield return PatientInstructionElement;
				foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
				foreach (var elem in Participant) { if (elem != null) yield return elem; }
				foreach (var elem in RequestedPeriod) { if (elem != null) yield return elem; }
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
                if (CancelationReason != null) yield return new ElementValue("cancelationReason", CancelationReason);
                foreach (var elem in ServiceCategory) { if (elem != null) yield return new ElementValue("serviceCategory", elem); }
                foreach (var elem in ServiceType) { if (elem != null) yield return new ElementValue("serviceType", elem); }
                foreach (var elem in Specialty) { if (elem != null) yield return new ElementValue("specialty", elem); }
                if (AppointmentType != null) yield return new ElementValue("appointmentType", AppointmentType);
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in SupportingInformation) { if (elem != null) yield return new ElementValue("supportingInformation", elem); }
                if (StartElement != null) yield return new ElementValue("start", StartElement);
                if (EndElement != null) yield return new ElementValue("end", EndElement);
                if (MinutesDurationElement != null) yield return new ElementValue("minutesDuration", MinutesDurationElement);
                foreach (var elem in Slot) { if (elem != null) yield return new ElementValue("slot", elem); }
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                if (PatientInstructionElement != null) yield return new ElementValue("patientInstruction", PatientInstructionElement);
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                foreach (var elem in Participant) { if (elem != null) yield return new ElementValue("participant", elem); }
                foreach (var elem in RequestedPeriod) { if (elem != null) yield return new ElementValue("requestedPeriod", elem); }
            }
        }

    }
    
}
