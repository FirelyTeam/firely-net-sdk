using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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

//
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Measurement, calculation or setting capability of a medical device
    /// </summary>
    [FhirType("DeviceMetric", IsResource=true)]
    [DataContract]
    public partial class DeviceMetric : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DeviceMetric; } }
        [NotMapped]
        public override string TypeName { get { return "DeviceMetric"; } }
        
        /// <summary>
        /// Describes the operational status of the DeviceMetric.
        /// (url: http://hl7.org/fhir/ValueSet/metric-operational-status)
        /// </summary>
        [FhirEnumeration("DeviceMetricOperationalStatus")]
        public enum DeviceMetricOperationalStatus
        {
            /// <summary>
            /// The DeviceMetric is operating and will generate DeviceObservations.
            /// (system: http://hl7.org/fhir/metric-operational-status)
            /// </summary>
            [EnumLiteral("on"), Description("On")]
            On,
            /// <summary>
            /// The DeviceMetric is not operating.
            /// (system: http://hl7.org/fhir/metric-operational-status)
            /// </summary>
            [EnumLiteral("off"), Description("Off")]
            Off,
            /// <summary>
            /// The DeviceMetric is operating, but will not generate any DeviceObservations.
            /// (system: http://hl7.org/fhir/metric-operational-status)
            /// </summary>
            [EnumLiteral("standby"), Description("Standby")]
            Standby,
        }

        /// <summary>
        /// Describes the typical color of representation.
        /// (url: http://hl7.org/fhir/ValueSet/metric-color)
        /// </summary>
        [FhirEnumeration("DeviceMetricColor")]
        public enum DeviceMetricColor
        {
            /// <summary>
            /// Color for representation - black.
            /// (system: http://hl7.org/fhir/metric-color)
            /// </summary>
            [EnumLiteral("black"), Description("Color Black")]
            Black,
            /// <summary>
            /// Color for representation - red.
            /// (system: http://hl7.org/fhir/metric-color)
            /// </summary>
            [EnumLiteral("red"), Description("Color Red")]
            Red,
            /// <summary>
            /// Color for representation - green.
            /// (system: http://hl7.org/fhir/metric-color)
            /// </summary>
            [EnumLiteral("green"), Description("Color Green")]
            Green,
            /// <summary>
            /// Color for representation - yellow.
            /// (system: http://hl7.org/fhir/metric-color)
            /// </summary>
            [EnumLiteral("yellow"), Description("Color Yellow")]
            Yellow,
            /// <summary>
            /// Color for representation - blue.
            /// (system: http://hl7.org/fhir/metric-color)
            /// </summary>
            [EnumLiteral("blue"), Description("Color Blue")]
            Blue,
            /// <summary>
            /// Color for representation - magenta.
            /// (system: http://hl7.org/fhir/metric-color)
            /// </summary>
            [EnumLiteral("magenta"), Description("Color Magenta")]
            Magenta,
            /// <summary>
            /// Color for representation - cyan.
            /// (system: http://hl7.org/fhir/metric-color)
            /// </summary>
            [EnumLiteral("cyan"), Description("Color Cyan")]
            Cyan,
            /// <summary>
            /// Color for representation - white.
            /// (system: http://hl7.org/fhir/metric-color)
            /// </summary>
            [EnumLiteral("white"), Description("Color White")]
            White,
        }

        /// <summary>
        /// Describes the category of the metric.
        /// (url: http://hl7.org/fhir/ValueSet/metric-category)
        /// </summary>
        [FhirEnumeration("DeviceMetricCategory")]
        public enum DeviceMetricCategory
        {
            /// <summary>
            /// DeviceObservations generated for this DeviceMetric are measured.
            /// (system: http://hl7.org/fhir/metric-category)
            /// </summary>
            [EnumLiteral("measurement"), Description("Measurement")]
            Measurement,
            /// <summary>
            /// DeviceObservations generated for this DeviceMetric is a setting that will influence the behavior of the Device.
            /// (system: http://hl7.org/fhir/metric-category)
            /// </summary>
            [EnumLiteral("setting"), Description("Setting")]
            Setting,
            /// <summary>
            /// DeviceObservations generated for this DeviceMetric are calculated.
            /// (system: http://hl7.org/fhir/metric-category)
            /// </summary>
            [EnumLiteral("calculation"), Description("Calculation")]
            Calculation,
            /// <summary>
            /// The category of this DeviceMetric is unspecified.
            /// (system: http://hl7.org/fhir/metric-category)
            /// </summary>
            [EnumLiteral("unspecified"), Description("Unspecified")]
            Unspecified,
        }

        /// <summary>
        /// Describes the type of a metric calibration.
        /// (url: http://hl7.org/fhir/ValueSet/metric-calibration-type)
        /// </summary>
        [FhirEnumeration("DeviceMetricCalibrationType")]
        public enum DeviceMetricCalibrationType
        {
            /// <summary>
            /// TODO
            /// (system: http://hl7.org/fhir/metric-calibration-type)
            /// </summary>
            [EnumLiteral("unspecified"), Description("Unspecified")]
            Unspecified,
            /// <summary>
            /// TODO
            /// (system: http://hl7.org/fhir/metric-calibration-type)
            /// </summary>
            [EnumLiteral("offset"), Description("Offset")]
            Offset,
            /// <summary>
            /// TODO
            /// (system: http://hl7.org/fhir/metric-calibration-type)
            /// </summary>
            [EnumLiteral("gain"), Description("Gain")]
            Gain,
            /// <summary>
            /// TODO
            /// (system: http://hl7.org/fhir/metric-calibration-type)
            /// </summary>
            [EnumLiteral("two-point"), Description("Two Point")]
            TwoPoint,
        }

        /// <summary>
        /// Describes the state of a metric calibration.
        /// (url: http://hl7.org/fhir/ValueSet/metric-calibration-state)
        /// </summary>
        [FhirEnumeration("DeviceMetricCalibrationState")]
        public enum DeviceMetricCalibrationState
        {
            /// <summary>
            /// The metric has not been calibrated.
            /// (system: http://hl7.org/fhir/metric-calibration-state)
            /// </summary>
            [EnumLiteral("not-calibrated"), Description("Not Calibrated")]
            NotCalibrated,
            /// <summary>
            /// The metric needs to be calibrated.
            /// (system: http://hl7.org/fhir/metric-calibration-state)
            /// </summary>
            [EnumLiteral("calibration-required"), Description("Calibration Required")]
            CalibrationRequired,
            /// <summary>
            /// The metric has been calibrated.
            /// (system: http://hl7.org/fhir/metric-calibration-state)
            /// </summary>
            [EnumLiteral("calibrated"), Description("Calibrated")]
            Calibrated,
            /// <summary>
            /// The state of calibration of this metric is unspecified.
            /// (system: http://hl7.org/fhir/metric-calibration-state)
            /// </summary>
            [EnumLiteral("unspecified"), Description("Unspecified")]
            Unspecified,
        }

        [FhirType("CalibrationComponent")]
        [DataContract]
        public partial class CalibrationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CalibrationComponent"; } }
            
            /// <summary>
            /// unspecified | offset | gain | two-point
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricCalibrationType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricCalibrationType> _TypeElement;
            
            /// <summary>
            /// unspecified | offset | gain | two-point
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DeviceMetric.DeviceMetricCalibrationType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricCalibrationType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// not-calibrated | calibration-required | calibrated | unspecified
            /// </summary>
            [FhirElement("state", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricCalibrationState> StateElement
            {
                get { return _StateElement; }
                set { _StateElement = value; OnPropertyChanged("StateElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricCalibrationState> _StateElement;
            
            /// <summary>
            /// not-calibrated | calibration-required | calibrated | unspecified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DeviceMetric.DeviceMetricCalibrationState? State
            {
                get { return StateElement != null ? StateElement.Value : null; }
                set
                {
                    if(value == null)
                      StateElement = null; 
                    else
                      StateElement = new Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricCalibrationState>(value);
                    OnPropertyChanged("State");
                }
            }
            
            /// <summary>
            /// Describes the time last calibration has been performed
            /// </summary>
            [FhirElement("time", InSummary=true, Order=60)]
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
                    if(value == null)
                      TimeElement = null; 
                    else
                      TimeElement = new Hl7.Fhir.Model.Instant(value);
                    OnPropertyChanged("Time");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CalibrationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricCalibrationType>)TypeElement.DeepCopy();
                    if(StateElement != null) dest.StateElement = (Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricCalibrationState>)StateElement.DeepCopy();
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
            
        }
        
        
        /// <summary>
        /// Type of metric
        /// </summary>
        [FhirElement("type", InSummary=true, Order=90)]
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
        [FhirElement("identifier", InSummary=true, Order=100)]
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
        [FhirElement("unit", InSummary=true, Order=110)]
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
        [FhirElement("source", InSummary=true, Order=120)]
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
        [FhirElement("parent", InSummary=true, Order=130)]
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
        [FhirElement("operationalStatus", InSummary=true, Order=140)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricOperationalStatus> OperationalStatusElement
        {
            get { return _OperationalStatusElement; }
            set { _OperationalStatusElement = value; OnPropertyChanged("OperationalStatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricOperationalStatus> _OperationalStatusElement;
        
        /// <summary>
        /// on | off | standby
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DeviceMetric.DeviceMetricOperationalStatus? OperationalStatus
        {
            get { return OperationalStatusElement != null ? OperationalStatusElement.Value : null; }
            set
            {
                if(value == null)
                  OperationalStatusElement = null; 
                else
                  OperationalStatusElement = new Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricOperationalStatus>(value);
                OnPropertyChanged("OperationalStatus");
            }
        }
        
        /// <summary>
        /// black | red | green | yellow | blue | magenta | cyan | white
        /// </summary>
        [FhirElement("color", InSummary=true, Order=150)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricColor> ColorElement
        {
            get { return _ColorElement; }
            set { _ColorElement = value; OnPropertyChanged("ColorElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricColor> _ColorElement;
        
        /// <summary>
        /// black | red | green | yellow | blue | magenta | cyan | white
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DeviceMetric.DeviceMetricColor? Color
        {
            get { return ColorElement != null ? ColorElement.Value : null; }
            set
            {
                if(value == null)
                  ColorElement = null; 
                else
                  ColorElement = new Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricColor>(value);
                OnPropertyChanged("Color");
            }
        }
        
        /// <summary>
        /// measurement | setting | calculation | unspecified
        /// </summary>
        [FhirElement("category", InSummary=true, Order=160)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricCategory> CategoryElement
        {
            get { return _CategoryElement; }
            set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricCategory> _CategoryElement;
        
        /// <summary>
        /// measurement | setting | calculation | unspecified
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DeviceMetric.DeviceMetricCategory? Category
        {
            get { return CategoryElement != null ? CategoryElement.Value : null; }
            set
            {
                if(value == null)
                  CategoryElement = null; 
                else
                  CategoryElement = new Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricCategory>(value);
                OnPropertyChanged("Category");
            }
        }
        
        /// <summary>
        /// Describes the measurement repetition time
        /// </summary>
        [FhirElement("measurementPeriod", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Timing MeasurementPeriod
        {
            get { return _MeasurementPeriod; }
            set { _MeasurementPeriod = value; OnPropertyChanged("MeasurementPeriod"); }
        }
        
        private Hl7.Fhir.Model.Timing _MeasurementPeriod;
        
        /// <summary>
        /// Describes the calibrations that have been performed or that are required to be performed
        /// </summary>
        [FhirElement("calibration", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DeviceMetric.CalibrationComponent> Calibration
        {
            get { if(_Calibration==null) _Calibration = new List<Hl7.Fhir.Model.DeviceMetric.CalibrationComponent>(); return _Calibration; }
            set { _Calibration = value; OnPropertyChanged("Calibration"); }
        }
        
        private List<Hl7.Fhir.Model.DeviceMetric.CalibrationComponent> _Calibration;
        
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
                if(OperationalStatusElement != null) dest.OperationalStatusElement = (Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricOperationalStatus>)OperationalStatusElement.DeepCopy();
                if(ColorElement != null) dest.ColorElement = (Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricColor>)ColorElement.DeepCopy();
                if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.DeviceMetric.DeviceMetricCategory>)CategoryElement.DeepCopy();
                if(MeasurementPeriod != null) dest.MeasurementPeriod = (Hl7.Fhir.Model.Timing)MeasurementPeriod.DeepCopy();
                if(Calibration != null) dest.Calibration = new List<Hl7.Fhir.Model.DeviceMetric.CalibrationComponent>(Calibration.DeepCopy());
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
        
    }
    
}
