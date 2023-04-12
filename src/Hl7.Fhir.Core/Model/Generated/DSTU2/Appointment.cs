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
namespace Hl7.Fhir.Model.DSTU2
{
    /// <summary>
    /// A booking of a healthcare event among patient(s), practitioner(s), related person(s) and/or device(s) for a specific date/time. This may result in one or more Encounter(s)
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "Appointment", IsResource=true)]
    [DataContract]
    public partial class Appointment : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IAppointment, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Appointment; } }
        [NotMapped]
        public override string TypeName { get { return "Appointment"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ParticipantComponent")]
        [DataContract]
        public partial class ParticipantComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IAppointmentParticipantComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParticipantComponent"; } }
            
            /// <summary>
            /// Role of participant in the appointment
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
            [FhirElement("actor", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
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
            /// required | optional | information-only
            /// </summary>
            [FhirElement("required", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ParticipantRequired> RequiredElement
            {
                get { return _RequiredElement; }
                set { _RequiredElement = value; OnPropertyChanged("RequiredElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ParticipantRequired> _RequiredElement;
            
            /// <summary>
            /// required | optional | information-only
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ParticipantRequired? Required
            {
                get { return RequiredElement != null ? RequiredElement.Value : null; }
                set
                {
                    if (value == null)
                        RequiredElement = null;
                    else
                        RequiredElement = new Code<Hl7.Fhir.Model.ParticipantRequired>(value);
                    OnPropertyChanged("Required");
                }
            }
            
            /// <summary>
            /// accepted | declined | tentative | needs-action
            /// </summary>
            [FhirElement("status", Order=70)]
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
                    if (value == null)
                        StatusElement = null;
                    else
                        StatusElement = new Code<Hl7.Fhir.Model.ParticipationStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParticipantComponent");
                base.Serialize(sink);
                sink.BeginList("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Type)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Actor?.Serialize(sink);
                sink.Element("required", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RequiredElement?.Serialize(sink);
                sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); StatusElement?.Serialize(sink);
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
                    case "type":
                        Type = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "actor":
                        Actor = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "required":
                        RequiredElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ParticipantRequired>>();
                        return true;
                    case "status":
                        StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ParticipationStatus>>();
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
                    case "type":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "actor":
                        Actor = source.Populate(Actor);
                        return true;
                    case "required":
                        RequiredElement = source.PopulateValue(RequiredElement);
                        return true;
                    case "_required":
                        RequiredElement = source.Populate(RequiredElement);
                        return true;
                    case "status":
                        StatusElement = source.PopulateValue(StatusElement);
                        return true;
                    case "_status":
                        StatusElement = source.Populate(StatusElement);
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
                    case "type":
                        source.PopulateListItem(Type, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParticipantComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    if(RequiredElement != null) dest.RequiredElement = (Code<Hl7.Fhir.Model.ParticipantRequired>)RequiredElement.DeepCopy();
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ParticipationStatus>)StatusElement.DeepCopy();
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
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IAppointmentParticipantComponent> Hl7.Fhir.Model.IAppointment.Participant { get { return Participant; } }
    
        
        /// <summary>
        /// External Ids for this item
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
        /// proposed | pending | booked | arrived | fulfilled | cancelled | noshow
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.AppointmentStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.AppointmentStatus> _StatusElement;
        
        /// <summary>
        /// proposed | pending | booked | arrived | fulfilled | cancelled | noshow
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.AppointmentStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.AppointmentStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// The type of appointment that is being booked
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Reason this appointment is scheduled
        /// </summary>
        [FhirElement("reason", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Reason;
        
        /// <summary>
        /// Used to make informed decisions if needing to re-prioritize
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
                if (value == null)
                    PriorityElement = null;
                else
                    PriorityElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// Shown on a subject line in a meeting request, or appointment list
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
        /// When appointment is to take place
        /// </summary>
        [FhirElement("start", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
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
                if (value == null)
                    StartElement = null;
                else
                    StartElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Start");
            }
        }
        
        /// <summary>
        /// When appointment is to conclude
        /// </summary>
        [FhirElement("end", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
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
                if (value == null)
                    EndElement = null;
                else
                    EndElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("End");
            }
        }
        
        /// <summary>
        /// Can be less than start/end (e.g. estimate)
        /// </summary>
        [FhirElement("minutesDuration", Order=170)]
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
                if (value == null)
                    MinutesDurationElement = null;
                else
                    MinutesDurationElement = new Hl7.Fhir.Model.PositiveInt(value);
                OnPropertyChanged("MinutesDuration");
            }
        }
        
        /// <summary>
        /// If provided, then no schedule and start/end values MUST match slot
        /// </summary>
        [FhirElement("slot", Order=180)]
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
        /// Additional comments
        /// </summary>
        [FhirElement("comment", Order=190)]
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
        /// Participants involved in appointment
        /// </summary>
        [FhirElement("participant", Order=200)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<ParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<ParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<ParticipantComponent> _Participant;
    
    
        public static ElementDefinitionConstraint[] Appointment_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "app-3",
                severity: ConstraintSeverity.Warning,
                expression: "(start.exists() and end.exists()) or (status = 'proposed') or (status = 'cancelled')",
                human: "Only proposed or cancelled appointments can be missing start/end dates",
                xpath: "((exists(f:start) and exists(f:end)) or (f:status/@value='proposed') or (f:status/@value='cancelled'))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "app-2",
                severity: ConstraintSeverity.Warning,
                expression: "start.empty() xor end.exists()",
                human: "Either start and end are specified, or neither",
                xpath: "((exists(f:start) and exists(f:end)) or (not(exists(f:start)) and not(exists(f:end))))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "app-1",
                severity: ConstraintSeverity.Warning,
                expression: "participant.all(type.exists() or actor.exists())",
                human: "Either the type or actor on the participant MUST be specified",
                xpath: "(exists(f:type) or exists(f:actor))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Appointment_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Appointment;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.AppointmentStatus>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Hl7.Fhir.Model.UnsignedInt)PriorityElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Instant)StartElement.DeepCopy();
                if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Instant)EndElement.DeepCopy();
                if(MinutesDurationElement != null) dest.MinutesDurationElement = (Hl7.Fhir.Model.PositiveInt)MinutesDurationElement.DeepCopy();
                if(Slot != null) dest.Slot = new List<Hl7.Fhir.Model.ResourceReference>(Slot.DeepCopy());
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                if(Participant != null) dest.Participant = new List<ParticipantComponent>(Participant.DeepCopy());
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
            if( !DeepComparable.Matches(MinutesDurationElement, otherT.MinutesDurationElement)) return false;
            if( !DeepComparable.Matches(Slot, otherT.Slot)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
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
            if( !DeepComparable.IsExactly(MinutesDurationElement, otherT.MinutesDurationElement)) return false;
            if( !DeepComparable.IsExactly(Slot, otherT.Slot)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Appointment");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
            sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Reason?.Serialize(sink);
            sink.Element("priority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PriorityElement?.Serialize(sink);
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
            sink.Element("start", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StartElement?.Serialize(sink);
            sink.Element("end", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EndElement?.Serialize(sink);
            sink.Element("minutesDuration", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); MinutesDurationElement?.Serialize(sink);
            sink.BeginList("slot", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Slot)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("comment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CommentElement?.Serialize(sink);
            sink.BeginList("participant", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
            foreach(var item in Participant)
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
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.AppointmentStatus>>();
                    return true;
                case "type":
                    Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "reason":
                    Reason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "priority":
                    PriorityElement = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "start":
                    StartElement = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "end":
                    EndElement = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "minutesDuration":
                    MinutesDurationElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                    return true;
                case "slot":
                    Slot = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "comment":
                    CommentElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "participant":
                    Participant = source.GetList<ParticipantComponent>();
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
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "type":
                    Type = source.Populate(Type);
                    return true;
                case "reason":
                    Reason = source.Populate(Reason);
                    return true;
                case "priority":
                    PriorityElement = source.PopulateValue(PriorityElement);
                    return true;
                case "_priority":
                    PriorityElement = source.Populate(PriorityElement);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "start":
                    StartElement = source.PopulateValue(StartElement);
                    return true;
                case "_start":
                    StartElement = source.Populate(StartElement);
                    return true;
                case "end":
                    EndElement = source.PopulateValue(EndElement);
                    return true;
                case "_end":
                    EndElement = source.Populate(EndElement);
                    return true;
                case "minutesDuration":
                    MinutesDurationElement = source.PopulateValue(MinutesDurationElement);
                    return true;
                case "_minutesDuration":
                    MinutesDurationElement = source.Populate(MinutesDurationElement);
                    return true;
                case "slot":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "comment":
                    CommentElement = source.PopulateValue(CommentElement);
                    return true;
                case "_comment":
                    CommentElement = source.Populate(CommentElement);
                    return true;
                case "participant":
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
                case "slot":
                    source.PopulateListItem(Slot, index);
                    return true;
                case "participant":
                    source.PopulateListItem(Participant, index);
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
                if (StatusElement != null) yield return StatusElement;
                if (Type != null) yield return Type;
                if (Reason != null) yield return Reason;
                if (PriorityElement != null) yield return PriorityElement;
                if (DescriptionElement != null) yield return DescriptionElement;
                if (StartElement != null) yield return StartElement;
                if (EndElement != null) yield return EndElement;
                if (MinutesDurationElement != null) yield return MinutesDurationElement;
                foreach (var elem in Slot) { if (elem != null) yield return elem; }
                if (CommentElement != null) yield return CommentElement;
                foreach (var elem in Participant) { if (elem != null) yield return elem; }
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
                if (Type != null) yield return new ElementValue("type", Type);
                if (Reason != null) yield return new ElementValue("reason", Reason);
                if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (StartElement != null) yield return new ElementValue("start", StartElement);
                if (EndElement != null) yield return new ElementValue("end", EndElement);
                if (MinutesDurationElement != null) yield return new ElementValue("minutesDuration", MinutesDurationElement);
                foreach (var elem in Slot) { if (elem != null) yield return new ElementValue("slot", elem); }
                if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                foreach (var elem in Participant) { if (elem != null) yield return new ElementValue("participant", elem); }
            }
        }
    
    }

}
