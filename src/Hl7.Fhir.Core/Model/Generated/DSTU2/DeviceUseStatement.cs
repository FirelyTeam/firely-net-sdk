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
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "DeviceUseStatement", IsResource=true)]
    [DataContract]
    public partial class DeviceUseStatement : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IDeviceUseStatement, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DeviceUseStatement; } }
        [NotMapped]
        public override string TypeName { get { return "DeviceUseStatement"; } }
    
        
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
        
        [FhirElement("whenUsed", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period WhenUsed
        {
            get { return _WhenUsed; }
            set { _WhenUsed = value; OnPropertyChanged("WhenUsed"); }
        }
        
        private Hl7.Fhir.Model.Period _WhenUsed;
        
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
        
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        [FhirElement("indication", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Indication
        {
            get { if(_Indication==null) _Indication = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Indication; }
            set { _Indication = value; OnPropertyChanged("Indication"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Indication;
        
        [FhirElement("notes", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> NotesElement
        {
            get { if(_NotesElement==null) _NotesElement = new List<Hl7.Fhir.Model.FhirString>(); return _NotesElement; }
            set { _NotesElement = value; OnPropertyChanged("NotesElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _NotesElement;
        
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
        
        [FhirElement("recordedOn", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime RecordedOnElement
        {
            get { return _RecordedOnElement; }
            set { _RecordedOnElement = value; OnPropertyChanged("RecordedOnElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _RecordedOnElement;
        
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
        
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
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
        
        [FhirElement("timing", InSummary=Hl7.Fhir.Model.Version.All, Order=170, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.DSTU2.Timing),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.FhirDateTime))]
        [DataMember]
        public Hl7.Fhir.Model.Element Timing
        {
            get { return _Timing; }
            set { _Timing = value; OnPropertyChanged("Timing"); }
        }
        
        private Hl7.Fhir.Model.Element _Timing;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DeviceUseStatement;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.Element)BodySite.DeepCopy();
                if(WhenUsed != null) dest.WhenUsed = (Hl7.Fhir.Model.Period)WhenUsed.DeepCopy();
                if(Device != null) dest.Device = (Hl7.Fhir.Model.ResourceReference)Device.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Indication != null) dest.Indication = new List<Hl7.Fhir.Model.CodeableConcept>(Indication.DeepCopy());
                if(NotesElement != null) dest.NotesElement = new List<Hl7.Fhir.Model.FhirString>(NotesElement.DeepCopy());
                if(RecordedOnElement != null) dest.RecordedOnElement = (Hl7.Fhir.Model.FhirDateTime)RecordedOnElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new DeviceUseStatement());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DeviceUseStatement;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(WhenUsed, otherT.WhenUsed)) return false;
            if( !DeepComparable.Matches(Device, otherT.Device)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Indication, otherT.Indication)) return false;
            if( !DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.Matches(RecordedOnElement, otherT.RecordedOnElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DeviceUseStatement;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(WhenUsed, otherT.WhenUsed)) return false;
            if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Indication, otherT.Indication)) return false;
            if( !DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.IsExactly(RecordedOnElement, otherT.RecordedOnElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("DeviceUseStatement");
            base.Serialize(sink);
            sink.Element("bodySite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); BodySite?.Serialize(sink);
            sink.Element("whenUsed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); WhenUsed?.Serialize(sink);
            sink.Element("device", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Device?.Serialize(sink);
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
            sink.Element("recordedOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RecordedOnElement?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("timing", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Timing?.Serialize(sink);
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
                case "whenUsed":
                    WhenUsed = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "device":
                    Device = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                case "whenUsed":
                    WhenUsed = source.Populate(WhenUsed);
                    return true;
                case "device":
                    Device = source.Populate(Device);
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
                if (WhenUsed != null) yield return WhenUsed;
                if (Device != null) yield return Device;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                foreach (var elem in Indication) { if (elem != null) yield return elem; }
                foreach (var elem in NotesElement) { if (elem != null) yield return elem; }
                if (RecordedOnElement != null) yield return RecordedOnElement;
                if (Subject != null) yield return Subject;
                if (Timing != null) yield return Timing;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                if (WhenUsed != null) yield return new ElementValue("whenUsed", WhenUsed);
                if (Device != null) yield return new ElementValue("device", Device);
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in Indication) { if (elem != null) yield return new ElementValue("indication", elem); }
                foreach (var elem in NotesElement) { if (elem != null) yield return new ElementValue("notes", elem); }
                if (RecordedOnElement != null) yield return new ElementValue("recordedOn", RecordedOnElement);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Timing != null) yield return new ElementValue("timing", Timing);
            }
        }
    
    }

}
