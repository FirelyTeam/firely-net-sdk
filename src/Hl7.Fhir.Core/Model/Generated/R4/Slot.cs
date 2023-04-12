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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// A slot of time on a schedule that may be available for booking appointments
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Slot", IsResource=true)]
    [DataContract]
    public partial class Slot : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ISlot, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Slot; } }
        [NotMapped]
        public override string TypeName { get { return "Slot"; } }
    
        
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
        /// A broad categorization of the service that is to be performed during this appointment
        /// </summary>
        [FhirElement("serviceCategory", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ServiceCategory
        {
            get { if(_ServiceCategory==null) _ServiceCategory = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ServiceCategory; }
            set { _ServiceCategory = value; OnPropertyChanged("ServiceCategory"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ServiceCategory;
        
        /// <summary>
        /// The type of appointments that can be booked into this slot (ideally this would be an identifiable service - which is at a location, rather than the location itself). If provided then this overrides the value provided on the availability resource
        /// </summary>
        [FhirElement("serviceType", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
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
        [FhirElement("specialty", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Specialty
        {
            get { if(_Specialty==null) _Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Specialty; }
            set { _Specialty = value; OnPropertyChanged("Specialty"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Specialty;
        
        /// <summary>
        /// The style of appointment or patient that may be booked in the slot (not service type)
        /// </summary>
        [FhirElement("appointmentType", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept AppointmentType
        {
            get { return _AppointmentType; }
            set { _AppointmentType = value; OnPropertyChanged("AppointmentType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _AppointmentType;
        
        /// <summary>
        /// The schedule resource that this slot defines an interval of status information
        /// </summary>
        [FhirElement("schedule", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Schedule")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Schedule
        {
            get { return _Schedule; }
            set { _Schedule = value; OnPropertyChanged("Schedule"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Schedule;
        
        /// <summary>
        /// busy | free | busy-unavailable | busy-tentative | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.SlotStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.SlotStatus> _StatusElement;
        
        /// <summary>
        /// busy | free | busy-unavailable | busy-tentative | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.SlotStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.R4.SlotStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Date/Time that the slot is to begin
        /// </summary>
        [FhirElement("start", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant StartElement
        {
            get { return _StartElement; }
            set { _StartElement = value; OnPropertyChanged("StartElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _StartElement;
        
        /// <summary>
        /// Date/Time that the slot is to begin
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
        /// Date/Time that the slot is to conclude
        /// </summary>
        [FhirElement("end", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant EndElement
        {
            get { return _EndElement; }
            set { _EndElement = value; OnPropertyChanged("EndElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _EndElement;
        
        /// <summary>
        /// Date/Time that the slot is to conclude
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
        /// This slot has already been overbooked, appointments are unlikely to be accepted for this time
        /// </summary>
        [FhirElement("overbooked", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean OverbookedElement
        {
            get { return _OverbookedElement; }
            set { _OverbookedElement = value; OnPropertyChanged("OverbookedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _OverbookedElement;
        
        /// <summary>
        /// This slot has already been overbooked, appointments are unlikely to be accepted for this time
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Overbooked
        {
            get { return OverbookedElement != null ? OverbookedElement.Value : null; }
            set
            {
                if (value == null)
                    OverbookedElement = null;
                else
                    OverbookedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Overbooked");
            }
        }
        
        /// <summary>
        /// Comments on the slot to describe any extended information. Such as custom constraints on the slot
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
        /// Comments on the slot to describe any extended information. Such as custom constraints on the slot
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
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Slot;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(ServiceCategory != null) dest.ServiceCategory = new List<Hl7.Fhir.Model.CodeableConcept>(ServiceCategory.DeepCopy());
                if(ServiceType != null) dest.ServiceType = new List<Hl7.Fhir.Model.CodeableConcept>(ServiceType.DeepCopy());
                if(Specialty != null) dest.Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(Specialty.DeepCopy());
                if(AppointmentType != null) dest.AppointmentType = (Hl7.Fhir.Model.CodeableConcept)AppointmentType.DeepCopy();
                if(Schedule != null) dest.Schedule = (Hl7.Fhir.Model.ResourceReference)Schedule.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.R4.SlotStatus>)StatusElement.DeepCopy();
                if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Instant)StartElement.DeepCopy();
                if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Instant)EndElement.DeepCopy();
                if(OverbookedElement != null) dest.OverbookedElement = (Hl7.Fhir.Model.FhirBoolean)OverbookedElement.DeepCopy();
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Slot());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Slot;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.Matches(ServiceType, otherT.ServiceType)) return false;
            if( !DeepComparable.Matches(Specialty, otherT.Specialty)) return false;
            if( !DeepComparable.Matches(AppointmentType, otherT.AppointmentType)) return false;
            if( !DeepComparable.Matches(Schedule, otherT.Schedule)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
            if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.Matches(OverbookedElement, otherT.OverbookedElement)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Slot;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.IsExactly(ServiceType, otherT.ServiceType)) return false;
            if( !DeepComparable.IsExactly(Specialty, otherT.Specialty)) return false;
            if( !DeepComparable.IsExactly(AppointmentType, otherT.AppointmentType)) return false;
            if( !DeepComparable.IsExactly(Schedule, otherT.Schedule)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
            if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.IsExactly(OverbookedElement, otherT.OverbookedElement)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Slot");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("serviceCategory", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ServiceCategory)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("serviceType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ServiceType)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("specialty", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Specialty)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("appointmentType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AppointmentType?.Serialize(sink);
            sink.Element("schedule", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Schedule?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("start", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StartElement?.Serialize(sink);
            sink.Element("end", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); EndElement?.Serialize(sink);
            sink.Element("overbooked", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OverbookedElement?.Serialize(sink);
            sink.Element("comment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CommentElement?.Serialize(sink);
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
                case "serviceCategory":
                    ServiceCategory = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "serviceType":
                    ServiceType = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "specialty":
                    Specialty = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "appointmentType":
                    AppointmentType = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "schedule":
                    Schedule = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.SlotStatus>>();
                    return true;
                case "start":
                    StartElement = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "end":
                    EndElement = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "overbooked":
                    OverbookedElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "comment":
                    CommentElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                case "serviceCategory":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "serviceType":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "specialty":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "appointmentType":
                    AppointmentType = source.Populate(AppointmentType);
                    return true;
                case "schedule":
                    Schedule = source.Populate(Schedule);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
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
                case "overbooked":
                    OverbookedElement = source.PopulateValue(OverbookedElement);
                    return true;
                case "_overbooked":
                    OverbookedElement = source.Populate(OverbookedElement);
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
                case "serviceCategory":
                    source.PopulateListItem(ServiceCategory, index);
                    return true;
                case "serviceType":
                    source.PopulateListItem(ServiceType, index);
                    return true;
                case "specialty":
                    source.PopulateListItem(Specialty, index);
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
                foreach (var elem in ServiceCategory) { if (elem != null) yield return elem; }
                foreach (var elem in ServiceType) { if (elem != null) yield return elem; }
                foreach (var elem in Specialty) { if (elem != null) yield return elem; }
                if (AppointmentType != null) yield return AppointmentType;
                if (Schedule != null) yield return Schedule;
                if (StatusElement != null) yield return StatusElement;
                if (StartElement != null) yield return StartElement;
                if (EndElement != null) yield return EndElement;
                if (OverbookedElement != null) yield return OverbookedElement;
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
                foreach (var elem in ServiceCategory) { if (elem != null) yield return new ElementValue("serviceCategory", elem); }
                foreach (var elem in ServiceType) { if (elem != null) yield return new ElementValue("serviceType", elem); }
                foreach (var elem in Specialty) { if (elem != null) yield return new ElementValue("specialty", elem); }
                if (AppointmentType != null) yield return new ElementValue("appointmentType", AppointmentType);
                if (Schedule != null) yield return new ElementValue("schedule", Schedule);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (StartElement != null) yield return new ElementValue("start", StartElement);
                if (EndElement != null) yield return new ElementValue("end", EndElement);
                if (OverbookedElement != null) yield return new ElementValue("overbooked", OverbookedElement);
                if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
            }
        }
    
    }

}
