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
    /// A reply to an appointment request for a patient and/or practitioner(s), such as a confirmation or rejection
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "AppointmentResponse", IsResource=true)]
    [DataContract]
    public partial class AppointmentResponse : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IAppointmentResponse, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.AppointmentResponse; } }
        [NotMapped]
        public override string TypeName { get { return "AppointmentResponse"; } }
    
        
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
        /// Appointment this response relates to
        /// </summary>
        [FhirElement("appointment", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
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
                if (value == null)
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
                if (value == null)
                    EndElement = null;
                else
                    EndElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("End");
            }
        }
        
        /// <summary>
        /// Role of participant in the appointment
        /// </summary>
        [FhirElement("participantType", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
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
        [FhirElement("actor", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
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
        [FhirElement("participantStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.ParticipantStatus> ParticipantStatusElement
        {
            get { return _ParticipantStatusElement; }
            set { _ParticipantStatusElement = value; OnPropertyChanged("ParticipantStatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.ParticipantStatus> _ParticipantStatusElement;
        
        /// <summary>
        /// accepted | declined | tentative | in-process | completed | needs-action
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.ParticipantStatus? ParticipantStatus
        {
            get { return ParticipantStatusElement != null ? ParticipantStatusElement.Value : null; }
            set
            {
                if (value == null)
                    ParticipantStatusElement = null;
                else
                    ParticipantStatusElement = new Code<Hl7.Fhir.Model.DSTU2.ParticipantStatus>(value);
                OnPropertyChanged("ParticipantStatus");
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
    
    
        public static ElementDefinitionConstraint[] AppointmentResponse_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "apr-1",
                severity: ConstraintSeverity.Warning,
                expression: "participantType or actor",
                human: "Either the participantType or actor must be specified",
                xpath: "(exists(f:participantType) or exists(f:actor))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(AppointmentResponse_Constraints);
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
                if(ParticipantStatusElement != null) dest.ParticipantStatusElement = (Code<Hl7.Fhir.Model.DSTU2.ParticipantStatus>)ParticipantStatusElement.DeepCopy();
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
            if( !DeepComparable.Matches(ParticipantStatusElement, otherT.ParticipantStatusElement)) return false;
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
            if( !DeepComparable.IsExactly(ParticipantStatusElement, otherT.ParticipantStatusElement)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("AppointmentResponse");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("appointment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Appointment?.Serialize(sink);
            sink.Element("start", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); StartElement?.Serialize(sink);
            sink.Element("end", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); EndElement?.Serialize(sink);
            sink.BeginList("participantType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ParticipantType)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Actor?.Serialize(sink);
            sink.Element("participantStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ParticipantStatusElement?.Serialize(sink);
            sink.Element("comment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CommentElement?.Serialize(sink);
            sink.End();
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
                case "appointment":
                    Appointment = source.Populate(Appointment);
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
                case "participantType":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "actor":
                    Actor = source.Populate(Actor);
                    return true;
                case "participantStatus":
                    ParticipantStatusElement = source.PopulateValue(ParticipantStatusElement);
                    return true;
                case "_participantStatus":
                    ParticipantStatusElement = source.Populate(ParticipantStatusElement);
                    return true;
                case "comment":
                    CommentElement = source.PopulateValue(CommentElement);
                    return true;
                case "_comment":
                    CommentElement = source.Populate(CommentElement);
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
                case "participantType":
                    source.PopulateListItem(ParticipantType, index);
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
                if (Appointment != null) yield return Appointment;
                if (StartElement != null) yield return StartElement;
                if (EndElement != null) yield return EndElement;
                foreach (var elem in ParticipantType) { if (elem != null) yield return elem; }
                if (Actor != null) yield return Actor;
                if (ParticipantStatusElement != null) yield return ParticipantStatusElement;
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
                if (ParticipantStatusElement != null) yield return new ElementValue("participantStatus", ParticipantStatusElement);
                if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
            }
        }
    
    }

}
