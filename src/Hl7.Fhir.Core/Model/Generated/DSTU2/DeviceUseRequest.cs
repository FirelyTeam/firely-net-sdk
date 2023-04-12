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
    /// A request for a patient to use or be given a medical device
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "DeviceUseRequest", IsResource=true)]
    [DataContract]
    public partial class DeviceUseRequest : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DeviceUseRequest; } }
        [NotMapped]
        public override string TypeName { get { return "DeviceUseRequest"; } }
    
        
        /// <summary>
        /// Target body site
        /// </summary>
        [FhirElement("bodySite", InSummary=Hl7.Fhir.Model.Version.All, Order=90, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element BodySite
        {
            get { return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private Hl7.Fhir.Model.Element _BodySite;
        
        /// <summary>
        /// proposed | planned | requested | received | accepted | in-progress | completed | suspended | rejected | aborted
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.DeviceUseRequestStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.DeviceUseRequestStatus> _StatusElement;
        
        /// <summary>
        /// proposed | planned | requested | received | accepted | in-progress | completed | suspended | rejected | aborted
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.DeviceUseRequestStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.DeviceUseRequestStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Device requested
        /// </summary>
        [FhirElement("device", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [References("Device")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Device
        {
            get { return _Device; }
            set { _Device = value; OnPropertyChanged("Device"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Device;
        
        /// <summary>
        /// Encounter motivating request
        /// </summary>
        [FhirElement("encounter", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Request identifier
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
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
        /// Reason for request
        /// </summary>
        [FhirElement("indication", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Indication
        {
            get { if(_Indication==null) _Indication = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Indication; }
            set { _Indication = value; OnPropertyChanged("Indication"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Indication;
        
        /// <summary>
        /// Notes or comments
        /// </summary>
        [FhirElement("notes", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> NotesElement
        {
            get { if(_NotesElement==null) _NotesElement = new List<Hl7.Fhir.Model.FhirString>(); return _NotesElement; }
            set { _NotesElement = value; OnPropertyChanged("NotesElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _NotesElement;
        
        /// <summary>
        /// Notes or comments
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Notes
        {
            get { return NotesElement != null ? NotesElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    NotesElement = null;
                else
                    NotesElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Notes");
            }
        }
        
        /// <summary>
        /// PRN
        /// </summary>
        [FhirElement("prnReason", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> PrnReason
        {
            get { if(_PrnReason==null) _PrnReason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PrnReason; }
            set { _PrnReason = value; OnPropertyChanged("PrnReason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _PrnReason;
        
        /// <summary>
        /// When ordered
        /// </summary>
        [FhirElement("orderedOn", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime OrderedOnElement
        {
            get { return _OrderedOnElement; }
            set { _OrderedOnElement = value; OnPropertyChanged("OrderedOnElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _OrderedOnElement;
        
        /// <summary>
        /// When ordered
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string OrderedOn
        {
            get { return OrderedOnElement != null ? OrderedOnElement.Value : null; }
            set
            {
                if (value == null)
                    OrderedOnElement = null;
                else
                    OrderedOnElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("OrderedOn");
            }
        }
        
        /// <summary>
        /// When recorded
        /// </summary>
        [FhirElement("recordedOn", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime RecordedOnElement
        {
            get { return _RecordedOnElement; }
            set { _RecordedOnElement = value; OnPropertyChanged("RecordedOnElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _RecordedOnElement;
        
        /// <summary>
        /// When recorded
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RecordedOn
        {
            get { return RecordedOnElement != null ? RecordedOnElement.Value : null; }
            set
            {
                if (value == null)
                    RecordedOnElement = null;
                else
                    RecordedOnElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("RecordedOn");
            }
        }
        
        /// <summary>
        /// Focus of request
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
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
        /// Schedule for use
        /// </summary>
        [FhirElement("timing", InSummary=Hl7.Fhir.Model.Version.All, Order=200, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.DSTU2.Timing),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.FhirDateTime))]
        [DataMember]
        public Hl7.Fhir.Model.Element Timing
        {
            get { return _Timing; }
            set { _Timing = value; OnPropertyChanged("Timing"); }
        }
        
        private Hl7.Fhir.Model.Element _Timing;
        
        /// <summary>
        /// routine | urgent | stat | asap
        /// </summary>
        [FhirElement("priority", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.DeviceUseRequestPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.DeviceUseRequestPriority> _PriorityElement;
        
        /// <summary>
        /// routine | urgent | stat | asap
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.DeviceUseRequestPriority? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if (value == null)
                    PriorityElement = null;
                else
                    PriorityElement = new Code<Hl7.Fhir.Model.DSTU2.DeviceUseRequestPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DeviceUseRequest;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.Element)BodySite.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.DeviceUseRequestStatus>)StatusElement.DeepCopy();
                if(Device != null) dest.Device = (Hl7.Fhir.Model.ResourceReference)Device.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Indication != null) dest.Indication = new List<Hl7.Fhir.Model.CodeableConcept>(Indication.DeepCopy());
                if(NotesElement != null) dest.NotesElement = new List<Hl7.Fhir.Model.FhirString>(NotesElement.DeepCopy());
                if(PrnReason != null) dest.PrnReason = new List<Hl7.Fhir.Model.CodeableConcept>(PrnReason.DeepCopy());
                if(OrderedOnElement != null) dest.OrderedOnElement = (Hl7.Fhir.Model.FhirDateTime)OrderedOnElement.DeepCopy();
                if(RecordedOnElement != null) dest.RecordedOnElement = (Hl7.Fhir.Model.FhirDateTime)RecordedOnElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.DSTU2.DeviceUseRequestPriority>)PriorityElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new DeviceUseRequest());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DeviceUseRequest;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Device, otherT.Device)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Indication, otherT.Indication)) return false;
            if( !DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.Matches(PrnReason, otherT.PrnReason)) return false;
            if( !DeepComparable.Matches(OrderedOnElement, otherT.OrderedOnElement)) return false;
            if( !DeepComparable.Matches(RecordedOnElement, otherT.RecordedOnElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DeviceUseRequest;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Indication, otherT.Indication)) return false;
            if( !DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.IsExactly(PrnReason, otherT.PrnReason)) return false;
            if( !DeepComparable.IsExactly(OrderedOnElement, otherT.OrderedOnElement)) return false;
            if( !DeepComparable.IsExactly(RecordedOnElement, otherT.RecordedOnElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("DeviceUseRequest");
            base.Serialize(sink);
            sink.Element("bodySite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); BodySite?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.Element("device", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Device?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Encounter?.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("indication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Indication)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("notes", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(NotesElement);
            sink.End();
            sink.BeginList("prnReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in PrnReason)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("orderedOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OrderedOnElement?.Serialize(sink);
            sink.Element("recordedOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RecordedOnElement?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("timing", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Timing?.Serialize(sink);
            sink.Element("priority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PriorityElement?.Serialize(sink);
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
                case "bodySiteCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(BodySite, "bodySite");
                    BodySite = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "bodySiteReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(BodySite, "bodySite");
                    BodySite = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.DeviceUseRequestStatus>>();
                    return true;
                case "device":
                    Device = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "encounter":
                    Encounter = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "indication":
                    Indication = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "notes":
                    NotesElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "prnReason":
                    PrnReason = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "orderedOn":
                    OrderedOnElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "recordedOn":
                    RecordedOnElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "timingTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.DSTU2.Timing>();
                    return true;
                case "timingPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "timingDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "priority":
                    PriorityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.DeviceUseRequestPriority>>();
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
                case "bodySiteCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(BodySite, "bodySite");
                    BodySite = source.Populate(BodySite as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "bodySiteReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(BodySite, "bodySite");
                    BodySite = source.Populate(BodySite as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "device":
                    Device = source.Populate(Device);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "indication":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "notes":
                case "_notes":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "prnReason":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "orderedOn":
                    OrderedOnElement = source.PopulateValue(OrderedOnElement);
                    return true;
                case "_orderedOn":
                    OrderedOnElement = source.Populate(OrderedOnElement);
                    return true;
                case "recordedOn":
                    RecordedOnElement = source.PopulateValue(RecordedOnElement);
                    return true;
                case "_recordedOn":
                    RecordedOnElement = source.Populate(RecordedOnElement);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "timingTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.DSTU2.Timing);
                    return true;
                case "timingPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.Period);
                    return true;
                case "timingDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Timing, "timing");
                    Timing = source.PopulateValue(Timing as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_timingDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "priority":
                    PriorityElement = source.PopulateValue(PriorityElement);
                    return true;
                case "_priority":
                    PriorityElement = source.Populate(PriorityElement);
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
                case "indication":
                    source.PopulateListItem(Indication, index);
                    return true;
                case "notes":
                    source.PopulatePrimitiveListItemValue(NotesElement, index);
                    return true;
                case "_notes":
                    source.PopulatePrimitiveListItem(NotesElement, index);
                    return true;
                case "prnReason":
                    source.PopulateListItem(PrnReason, index);
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
                if (BodySite != null) yield return BodySite;
                if (StatusElement != null) yield return StatusElement;
                if (Device != null) yield return Device;
                if (Encounter != null) yield return Encounter;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                foreach (var elem in Indication) { if (elem != null) yield return elem; }
                foreach (var elem in NotesElement) { if (elem != null) yield return elem; }
                foreach (var elem in PrnReason) { if (elem != null) yield return elem; }
                if (OrderedOnElement != null) yield return OrderedOnElement;
                if (RecordedOnElement != null) yield return RecordedOnElement;
                if (Subject != null) yield return Subject;
                if (Timing != null) yield return Timing;
                if (PriorityElement != null) yield return PriorityElement;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Device != null) yield return new ElementValue("device", Device);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in Indication) { if (elem != null) yield return new ElementValue("indication", elem); }
                foreach (var elem in NotesElement) { if (elem != null) yield return new ElementValue("notes", elem); }
                foreach (var elem in PrnReason) { if (elem != null) yield return new ElementValue("prnReason", elem); }
                if (OrderedOnElement != null) yield return new ElementValue("orderedOn", OrderedOnElement);
                if (RecordedOnElement != null) yield return new ElementValue("recordedOn", RecordedOnElement);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Timing != null) yield return new ElementValue("timing", Timing);
                if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
            }
        }
    
    }

}
