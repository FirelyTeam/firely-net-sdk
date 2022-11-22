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
    /// Measurement, calculation or setting capability of a medical device
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "DeviceMetric", IsResource=true)]
    [DataContract]
    public partial class DeviceMetric : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IDeviceMetric, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DeviceMetric; } }
        [NotMapped]
        public override string TypeName { get { return "DeviceMetric"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "CalibrationComponent")]
        [DataContract]
        public partial class CalibrationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IDeviceMetricCalibrationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CalibrationComponent"; } }
            
            /// <summary>
            /// unspecified | offset | gain | two-point
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DeviceMetricCalibrationType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DeviceMetricCalibrationType> _TypeElement;
            
            /// <summary>
            /// unspecified | offset | gain | two-point
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DeviceMetricCalibrationType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.DeviceMetricCalibrationType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// not-calibrated | calibration-required | calibrated | unspecified
            /// </summary>
            [FhirElement("state", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DeviceMetricCalibrationState> StateElement
            {
                get { return _StateElement; }
                set { _StateElement = value; OnPropertyChanged("StateElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DeviceMetricCalibrationState> _StateElement;
            
            /// <summary>
            /// not-calibrated | calibration-required | calibrated | unspecified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DeviceMetricCalibrationState? State
            {
                get { return StateElement != null ? StateElement.Value : null; }
                set
                {
                    if (value == null)
                        StateElement = null;
                    else
                        StateElement = new Code<Hl7.Fhir.Model.DeviceMetricCalibrationState>(value);
                    OnPropertyChanged("State");
                }
            }
            
            /// <summary>
            /// Describes the time last calibration has been performed
            /// </summary>
            [FhirElement("time", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Instant TimeElement
            {
                get { return _TimeElement; }
                set { _TimeElement = value; OnPropertyChanged("TimeElement"); }
            }
            
            private Hl7.Fhir.Model.Instant _TimeElement;
            
            /// <summary>
            /// Describes the time last calibration has been performed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public DateTimeOffset? Time
            {
                get { return TimeElement != null ? TimeElement.Value : null; }
                set
                {
                    if (value == null)
                        TimeElement = null;
                    else
                        TimeElement = new Hl7.Fhir.Model.Instant(value);
                    OnPropertyChanged("Time");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CalibrationComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TypeElement?.Serialize(sink);
                sink.Element("state", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StateElement?.Serialize(sink);
                sink.Element("time", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TimeElement?.Serialize(sink);
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
                    case "state":
                        StateElement = source.PopulateValue(StateElement);
                        return true;
                    case "_state":
                        StateElement = source.Populate(StateElement);
                        return true;
                    case "time":
                        TimeElement = source.PopulateValue(TimeElement);
                        return true;
                    case "_time":
                        TimeElement = source.Populate(TimeElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CalibrationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.DeviceMetricCalibrationType>)TypeElement.DeepCopy();
                    if(StateElement != null) dest.StateElement = (Code<Hl7.Fhir.Model.DeviceMetricCalibrationState>)StateElement.DeepCopy();
                    if(TimeElement != null) dest.TimeElement = (Hl7.Fhir.Model.Instant)TimeElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CalibrationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CalibrationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(StateElement, otherT.StateElement)) return false;
                if( !DeepComparable.Matches(TimeElement, otherT.TimeElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CalibrationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(StateElement, otherT.StateElement)) return false;
                if( !DeepComparable.IsExactly(TimeElement, otherT.TimeElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (StateElement != null) yield return StateElement;
                    if (TimeElement != null) yield return TimeElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (StateElement != null) yield return new ElementValue("state", StateElement);
                    if (TimeElement != null) yield return new ElementValue("time", TimeElement);
                }
            }
        
        
        }
        
        [NotMapped]
        Hl7.Fhir.Model.ITiming Hl7.Fhir.Model.IDeviceMetric.MeasurementPeriod { get { return MeasurementPeriod; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IDeviceMetricCalibrationComponent> Hl7.Fhir.Model.IDeviceMetric.Calibration { get { return Calibration; } }
    
        
        /// <summary>
        /// Type of metric
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Unique identifier of this DeviceMetric
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Unit of metric
        /// </summary>
        [FhirElement("unit", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Unit
        {
            get { return _Unit; }
            set { _Unit = value; OnPropertyChanged("Unit"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Unit;
        
        /// <summary>
        /// Describes the link to the source Device
        /// </summary>
        [FhirElement("source", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Source;
        
        /// <summary>
        /// Describes the link to the parent DeviceComponent
        /// </summary>
        [FhirElement("parent", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("DeviceComponent")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Parent
        {
            get { return _Parent; }
            set { _Parent = value; OnPropertyChanged("Parent"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Parent;
        
        /// <summary>
        /// on | off | standby
        /// </summary>
        [FhirElement("operationalStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.DeviceMetricOperationalStatus> OperationalStatusElement
        {
            get { return _OperationalStatusElement; }
            set { _OperationalStatusElement = value; OnPropertyChanged("OperationalStatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.DeviceMetricOperationalStatus> _OperationalStatusElement;
        
        /// <summary>
        /// on | off | standby
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.DeviceMetricOperationalStatus? OperationalStatus
        {
            get { return OperationalStatusElement != null ? OperationalStatusElement.Value : null; }
            set
            {
                if (value == null)
                    OperationalStatusElement = null;
                else
                    OperationalStatusElement = new Code<Hl7.Fhir.Model.DSTU2.DeviceMetricOperationalStatus>(value);
                OnPropertyChanged("OperationalStatus");
            }
        }
        
        /// <summary>
        /// black | red | green | yellow | blue | magenta | cyan | white
        /// </summary>
        [FhirElement("color", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DeviceMetricColor> ColorElement
        {
            get { return _ColorElement; }
            set { _ColorElement = value; OnPropertyChanged("ColorElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DeviceMetricColor> _ColorElement;
        
        /// <summary>
        /// black | red | green | yellow | blue | magenta | cyan | white
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DeviceMetricColor? Color
        {
            get { return ColorElement != null ? ColorElement.Value : null; }
            set
            {
                if (value == null)
                    ColorElement = null;
                else
                    ColorElement = new Code<Hl7.Fhir.Model.DeviceMetricColor>(value);
                OnPropertyChanged("Color");
            }
        }
        
        /// <summary>
        /// measurement | setting | calculation | unspecified
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DeviceMetricCategory> CategoryElement
        {
            get { return _CategoryElement; }
            set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DeviceMetricCategory> _CategoryElement;
        
        /// <summary>
        /// measurement | setting | calculation | unspecified
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DeviceMetricCategory? Category
        {
            get { return CategoryElement != null ? CategoryElement.Value : null; }
            set
            {
                if (value == null)
                    CategoryElement = null;
                else
                    CategoryElement = new Code<Hl7.Fhir.Model.DeviceMetricCategory>(value);
                OnPropertyChanged("Category");
            }
        }
        
        /// <summary>
        /// Describes the measurement repetition time
        /// </summary>
        [FhirElement("measurementPeriod", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.DSTU2.Timing MeasurementPeriod
        {
            get { return _MeasurementPeriod; }
            set { _MeasurementPeriod = value; OnPropertyChanged("MeasurementPeriod"); }
        }
        
        private Hl7.Fhir.Model.DSTU2.Timing _MeasurementPeriod;
        
        /// <summary>
        /// Describes the calibrations that have been performed or that are required to be performed
        /// </summary>
        [FhirElement("calibration", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<CalibrationComponent> Calibration
        {
            get { if(_Calibration==null) _Calibration = new List<CalibrationComponent>(); return _Calibration; }
            set { _Calibration = value; OnPropertyChanged("Calibration"); }
        }
        
        private List<CalibrationComponent> _Calibration;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DeviceMetric;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(Unit != null) dest.Unit = (Hl7.Fhir.Model.CodeableConcept)Unit.DeepCopy();
                if(Source != null) dest.Source = (Hl7.Fhir.Model.ResourceReference)Source.DeepCopy();
                if(Parent != null) dest.Parent = (Hl7.Fhir.Model.ResourceReference)Parent.DeepCopy();
                if(OperationalStatusElement != null) dest.OperationalStatusElement = (Code<Hl7.Fhir.Model.DSTU2.DeviceMetricOperationalStatus>)OperationalStatusElement.DeepCopy();
                if(ColorElement != null) dest.ColorElement = (Code<Hl7.Fhir.Model.DeviceMetricColor>)ColorElement.DeepCopy();
                if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.DeviceMetricCategory>)CategoryElement.DeepCopy();
                if(MeasurementPeriod != null) dest.MeasurementPeriod = (Hl7.Fhir.Model.DSTU2.Timing)MeasurementPeriod.DeepCopy();
                if(Calibration != null) dest.Calibration = new List<CalibrationComponent>(Calibration.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new DeviceMetric());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DeviceMetric;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Unit, otherT.Unit)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Parent, otherT.Parent)) return false;
            if( !DeepComparable.Matches(OperationalStatusElement, otherT.OperationalStatusElement)) return false;
            if( !DeepComparable.Matches(ColorElement, otherT.ColorElement)) return false;
            if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.Matches(MeasurementPeriod, otherT.MeasurementPeriod)) return false;
            if( !DeepComparable.Matches(Calibration, otherT.Calibration)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DeviceMetric;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Unit, otherT.Unit)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Parent, otherT.Parent)) return false;
            if( !DeepComparable.IsExactly(OperationalStatusElement, otherT.OperationalStatusElement)) return false;
            if( !DeepComparable.IsExactly(ColorElement, otherT.ColorElement)) return false;
            if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
            if( !DeepComparable.IsExactly(MeasurementPeriod, otherT.MeasurementPeriod)) return false;
            if( !DeepComparable.IsExactly(Calibration, otherT.Calibration)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("DeviceMetric");
            base.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Type?.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Identifier?.Serialize(sink);
            sink.Element("unit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Unit?.Serialize(sink);
            sink.Element("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Source?.Serialize(sink);
            sink.Element("parent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Parent?.Serialize(sink);
            sink.Element("operationalStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OperationalStatusElement?.Serialize(sink);
            sink.Element("color", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ColorElement?.Serialize(sink);
            sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); CategoryElement?.Serialize(sink);
            sink.Element("measurementPeriod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MeasurementPeriod?.Serialize(sink);
            sink.BeginList("calibration", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Calibration)
            {
                item?.Serialize(sink);
            }
            sink.End();
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
                    Type = source.Populate(Type);
                    return true;
                case "identifier":
                    Identifier = source.Populate(Identifier);
                    return true;
                case "unit":
                    Unit = source.Populate(Unit);
                    return true;
                case "source":
                    Source = source.Populate(Source);
                    return true;
                case "parent":
                    Parent = source.Populate(Parent);
                    return true;
                case "operationalStatus":
                    OperationalStatusElement = source.PopulateValue(OperationalStatusElement);
                    return true;
                case "_operationalStatus":
                    OperationalStatusElement = source.Populate(OperationalStatusElement);
                    return true;
                case "color":
                    ColorElement = source.PopulateValue(ColorElement);
                    return true;
                case "_color":
                    ColorElement = source.Populate(ColorElement);
                    return true;
                case "category":
                    CategoryElement = source.PopulateValue(CategoryElement);
                    return true;
                case "_category":
                    CategoryElement = source.Populate(CategoryElement);
                    return true;
                case "measurementPeriod":
                    MeasurementPeriod = source.Populate(MeasurementPeriod);
                    return true;
                case "calibration":
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
                case "calibration":
                    source.PopulateListItem(Calibration, index);
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
                if (Type != null) yield return Type;
                if (Identifier != null) yield return Identifier;
                if (Unit != null) yield return Unit;
                if (Source != null) yield return Source;
                if (Parent != null) yield return Parent;
                if (OperationalStatusElement != null) yield return OperationalStatusElement;
                if (ColorElement != null) yield return ColorElement;
                if (CategoryElement != null) yield return CategoryElement;
                if (MeasurementPeriod != null) yield return MeasurementPeriod;
                foreach (var elem in Calibration) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Type != null) yield return new ElementValue("type", Type);
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (Unit != null) yield return new ElementValue("unit", Unit);
                if (Source != null) yield return new ElementValue("source", Source);
                if (Parent != null) yield return new ElementValue("parent", Parent);
                if (OperationalStatusElement != null) yield return new ElementValue("operationalStatus", OperationalStatusElement);
                if (ColorElement != null) yield return new ElementValue("color", ColorElement);
                if (CategoryElement != null) yield return new ElementValue("category", CategoryElement);
                if (MeasurementPeriod != null) yield return new ElementValue("measurementPeriod", MeasurementPeriod);
                foreach (var elem in Calibration) { if (elem != null) yield return new ElementValue("calibration", elem); }
            }
        }
    
    }

}
