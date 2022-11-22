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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// Defines an expected trigger for a module
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "TriggerDefinition")]
    [DataContract]
    public partial class TriggerDefinition : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.ITriggerDefinition, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "TriggerDefinition"; } }
    
        
        /// <summary>
        /// named-event | periodic | data-added | data-modified | data-removed | data-accessed | data-access-ended
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.TriggerType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.TriggerType> _TypeElement;
        
        /// <summary>
        /// named-event | periodic | data-added | data-modified | data-removed | data-accessed | data-access-ended
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.TriggerType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Code<Hl7.Fhir.Model.STU3.TriggerType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Triggering event name
        /// </summary>
        [FhirElement("eventName", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString EventNameElement
        {
            get { return _EventNameElement; }
            set { _EventNameElement = value; OnPropertyChanged("EventNameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _EventNameElement;
        
        /// <summary>
        /// Triggering event name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string EventName
        {
            get { return EventNameElement != null ? EventNameElement.Value : null; }
            set
            {
                if (value == null)
                    EventNameElement = null;
                else
                    EventNameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("EventName");
            }
        }
        
        /// <summary>
        /// Timing of the event
        /// </summary>
        [FhirElement("eventTiming", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.STU3.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime))]
        [DataMember]
        public Hl7.Fhir.Model.Element EventTiming
        {
            get { return _EventTiming; }
            set { _EventTiming = value; OnPropertyChanged("EventTiming"); }
        }
        
        private Hl7.Fhir.Model.Element _EventTiming;
        
        /// <summary>
        /// Triggering data of the event
        /// </summary>
        [FhirElement("eventData", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.STU3.DataRequirement EventData
        {
            get { return _EventData; }
            set { _EventData = value; OnPropertyChanged("EventData"); }
        }
        
        private Hl7.Fhir.Model.STU3.DataRequirement _EventData;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TriggerDefinition;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.STU3.TriggerType>)TypeElement.DeepCopy();
                if(EventNameElement != null) dest.EventNameElement = (Hl7.Fhir.Model.FhirString)EventNameElement.DeepCopy();
                if(EventTiming != null) dest.EventTiming = (Hl7.Fhir.Model.Element)EventTiming.DeepCopy();
                if(EventData != null) dest.EventData = (Hl7.Fhir.Model.STU3.DataRequirement)EventData.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new TriggerDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as TriggerDefinition;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(EventNameElement, otherT.EventNameElement)) return false;
            if( !DeepComparable.Matches(EventTiming, otherT.EventTiming)) return false;
            if( !DeepComparable.Matches(EventData, otherT.EventData)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as TriggerDefinition;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(EventNameElement, otherT.EventNameElement)) return false;
            if( !DeepComparable.IsExactly(EventTiming, otherT.EventTiming)) return false;
            if( !DeepComparable.IsExactly(EventData, otherT.EventData)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("TriggerDefinition");
            base.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
            sink.Element("eventName", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EventNameElement?.Serialize(sink);
            sink.Element("eventTiming", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); EventTiming?.Serialize(sink);
            sink.Element("eventData", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EventData?.Serialize(sink);
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
                case "type":
                    TypeElement = source.PopulateValue(TypeElement);
                    return true;
                case "_type":
                    TypeElement = source.Populate(TypeElement);
                    return true;
                case "eventName":
                    EventNameElement = source.PopulateValue(EventNameElement);
                    return true;
                case "_eventName":
                    EventNameElement = source.Populate(EventNameElement);
                    return true;
                case "eventTimingTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(EventTiming, "eventTiming");
                    EventTiming = source.Populate(EventTiming as Hl7.Fhir.Model.STU3.Timing);
                    return true;
                case "eventTimingReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(EventTiming, "eventTiming");
                    EventTiming = source.Populate(EventTiming as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "eventTimingDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(EventTiming, "eventTiming");
                    EventTiming = source.PopulateValue(EventTiming as Hl7.Fhir.Model.Date);
                    return true;
                case "_eventTimingDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(EventTiming, "eventTiming");
                    EventTiming = source.Populate(EventTiming as Hl7.Fhir.Model.Date);
                    return true;
                case "eventTimingDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(EventTiming, "eventTiming");
                    EventTiming = source.PopulateValue(EventTiming as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_eventTimingDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(EventTiming, "eventTiming");
                    EventTiming = source.Populate(EventTiming as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "eventData":
                    EventData = source.Populate(EventData);
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
                if (TypeElement != null) yield return TypeElement;
                if (EventNameElement != null) yield return EventNameElement;
                if (EventTiming != null) yield return EventTiming;
                if (EventData != null) yield return EventData;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (EventNameElement != null) yield return new ElementValue("eventName", EventNameElement);
                if (EventTiming != null) yield return new ElementValue("eventTiming", EventTiming);
                if (EventData != null) yield return new ElementValue("eventData", EventData);
            }
        }
    
    }

}
