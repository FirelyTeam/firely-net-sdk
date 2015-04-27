using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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
// Generated on Thu, Apr 2, 2015 14:21+0200 for FHIR v0.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A timing schedule that specifies an event that may occur multiple times
    /// </summary>
    [FhirType("Timing")]
    [DataContract]
    public partial class Timing : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Timing"; } }
        
        /// <summary>
        /// A unit of time (units from UCUM)
        /// </summary>
        [FhirEnumeration("UnitsOfTime")]
        public enum UnitsOfTime
        {
            /// <summary>
            /// second.
            /// </summary>
            [EnumLiteral("s")]
            S,
            /// <summary>
            /// minute.
            /// </summary>
            [EnumLiteral("min")]
            Min,
            /// <summary>
            /// hour.
            /// </summary>
            [EnumLiteral("h")]
            H,
            /// <summary>
            /// day.
            /// </summary>
            [EnumLiteral("d")]
            D,
            /// <summary>
            /// week.
            /// </summary>
            [EnumLiteral("wk")]
            Wk,
            /// <summary>
            /// month.
            /// </summary>
            [EnumLiteral("mo")]
            Mo,
            /// <summary>
            /// year.
            /// </summary>
            [EnumLiteral("a")]
            A,
        }
        
        /// <summary>
        /// Real world event that the schedule relates to
        /// </summary>
        [FhirEnumeration("EventTiming")]
        public enum EventTiming
        {
            /// <summary>
            /// event occurs [duration] before the hour of sleep (or trying to).
            /// </summary>
            [EnumLiteral("HS")]
            HS,
            /// <summary>
            /// event occurs [duration] after waking.
            /// </summary>
            [EnumLiteral("WAKE")]
            WAKE,
            /// <summary>
            /// event occurs at a meal (from the Latin cibus).
            /// </summary>
            [EnumLiteral("C")]
            C,
            /// <summary>
            /// event occurs at breakfast (from the Latin cibus matutinus).
            /// </summary>
            [EnumLiteral("CM")]
            CM,
            /// <summary>
            /// event occurs at lunch (from the Latin cibus diurnus).
            /// </summary>
            [EnumLiteral("CD")]
            CD,
            /// <summary>
            /// event occurs at dinner (from the Latin ante cibus vespertinus).
            /// </summary>
            [EnumLiteral("CV")]
            CV,
            /// <summary>
            /// event occurs [duration] before a meal (from the Latin ante cibus).
            /// </summary>
            [EnumLiteral("AC")]
            AC,
            /// <summary>
            /// event occurs [duration] before breakfast (from the Latin ante cibus matutinus).
            /// </summary>
            [EnumLiteral("ACM")]
            ACM,
            /// <summary>
            /// event occurs [duration] before lunch (from the Latin ante cibus diurnus).
            /// </summary>
            [EnumLiteral("ACD")]
            ACD,
            /// <summary>
            /// event occurs [duration] before dinner (from the Latin ante cibus vespertinus).
            /// </summary>
            [EnumLiteral("ACV")]
            ACV,
            /// <summary>
            /// event occurs [duration] after a meal (from the Latin post cibus).
            /// </summary>
            [EnumLiteral("PC")]
            PC,
            /// <summary>
            /// event occurs [duration] after breakfast (from the Latin post cibus matutinus).
            /// </summary>
            [EnumLiteral("PCM")]
            PCM,
            /// <summary>
            /// event occurs [duration] after lunch (from the Latin post cibus diurnus).
            /// </summary>
            [EnumLiteral("PCD")]
            PCD,
            /// <summary>
            /// event occurs [duration] after dinner (from the Latin post cibus vespertinus).
            /// </summary>
            [EnumLiteral("PCV")]
            PCV,
        }
        
        /// <summary>
        /// Code for a known / defined timing pattern
        /// </summary>
        [FhirEnumeration("TimingAbbreviation")]
        public enum TimingAbbreviation
        {
            /// <summary>
            /// Two times a day at institution specified time.
            /// </summary>
            [EnumLiteral("BID")]
            BID,
            /// <summary>
            /// Three times a day at institution specified time.
            /// </summary>
            [EnumLiteral("TID")]
            TID,
            /// <summary>
            /// Four times a day at institution specified time.
            /// </summary>
            [EnumLiteral("QID")]
            QID,
            /// <summary>
            /// Every morning at institution specified times.
            /// </summary>
            [EnumLiteral("AM")]
            AM,
            /// <summary>
            /// Every afternoon at institution specified times.
            /// </summary>
            [EnumLiteral("PM")]
            PM,
        }
        
        [FhirType("TimingRepeatComponent")]
        [DataContract]
        public partial class TimingRepeatComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TimingRepeatComponent"; } }
            
            /// <summary>
            /// Start and/or end limits
            /// </summary>
            [FhirElement("bounds", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Period Bounds
            {
                get { return _Bounds; }
                set { _Bounds = value; OnPropertyChanged("Bounds"); }
            }
            
            private Hl7.Fhir.Model.Period _Bounds;
            
            /// <summary>
            /// Number of times to repeat
            /// </summary>
            [FhirElement("count", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Integer CountElement
            {
                get { return _CountElement; }
                set { _CountElement = value; OnPropertyChanged("CountElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _CountElement;
            
            /// <summary>
            /// Number of times to repeat
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Count
            {
                get { return CountElement != null ? CountElement.Value : null; }
                set
                {
                    if(value == null)
                      CountElement = null; 
                    else
                      CountElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Count");
                }
            }
            
            /// <summary>
            /// How long when it happens
            /// </summary>
            [FhirElement("duration", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal DurationElement
            {
                get { return _DurationElement; }
                set { _DurationElement = value; OnPropertyChanged("DurationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _DurationElement;
            
            /// <summary>
            /// How long when it happens
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Duration
            {
                get { return DurationElement != null ? DurationElement.Value : null; }
                set
                {
                    if(value == null)
                      DurationElement = null; 
                    else
                      DurationElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Duration");
                }
            }
            
            /// <summary>
            /// s | min | h | d | wk | mo | a - unit of time (UCUM)
            /// </summary>
            [FhirElement("durationUnits", InSummary=true, Order=70)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Timing.UnitsOfTime> DurationUnitsElement
            {
                get { return _DurationUnitsElement; }
                set { _DurationUnitsElement = value; OnPropertyChanged("DurationUnitsElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Timing.UnitsOfTime> _DurationUnitsElement;
            
            /// <summary>
            /// s | min | h | d | wk | mo | a - unit of time (UCUM)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Timing.UnitsOfTime? DurationUnits
            {
                get { return DurationUnitsElement != null ? DurationUnitsElement.Value : null; }
                set
                {
                    if(value == null)
                      DurationUnitsElement = null; 
                    else
                      DurationUnitsElement = new Code<Hl7.Fhir.Model.Timing.UnitsOfTime>(value);
                    OnPropertyChanged("DurationUnits");
                }
            }
            
            /// <summary>
            /// Event occurs frequency times per duration
            /// </summary>
            [FhirElement("frequency", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Integer FrequencyElement
            {
                get { return _FrequencyElement; }
                set { _FrequencyElement = value; OnPropertyChanged("FrequencyElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _FrequencyElement;
            
            /// <summary>
            /// Event occurs frequency times per duration
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Frequency
            {
                get { return FrequencyElement != null ? FrequencyElement.Value : null; }
                set
                {
                    if(value == null)
                      FrequencyElement = null; 
                    else
                      FrequencyElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Frequency");
                }
            }
            
            /// <summary>
            /// Event occurs frequency times per duration
            /// </summary>
            [FhirElement("frequencyMax", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Integer FrequencyMaxElement
            {
                get { return _FrequencyMaxElement; }
                set { _FrequencyMaxElement = value; OnPropertyChanged("FrequencyMaxElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _FrequencyMaxElement;
            
            /// <summary>
            /// Event occurs frequency times per duration
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? FrequencyMax
            {
                get { return FrequencyMaxElement != null ? FrequencyMaxElement.Value : null; }
                set
                {
                    if(value == null)
                      FrequencyMaxElement = null; 
                    else
                      FrequencyMaxElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("FrequencyMax");
                }
            }
            
            /// <summary>
            /// Event occurs frequency times per period
            /// </summary>
            [FhirElement("period", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PeriodElement
            {
                get { return _PeriodElement; }
                set { _PeriodElement = value; OnPropertyChanged("PeriodElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PeriodElement;
            
            /// <summary>
            /// Event occurs frequency times per period
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Period
            {
                get { return PeriodElement != null ? PeriodElement.Value : null; }
                set
                {
                    if(value == null)
                      PeriodElement = null; 
                    else
                      PeriodElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Period");
                }
            }
            
            /// <summary>
            /// Upper limit of period (3-4 hours)
            /// </summary>
            [FhirElement("periodMax", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PeriodMaxElement
            {
                get { return _PeriodMaxElement; }
                set { _PeriodMaxElement = value; OnPropertyChanged("PeriodMaxElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PeriodMaxElement;
            
            /// <summary>
            /// Upper limit of period (3-4 hours)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? PeriodMax
            {
                get { return PeriodMaxElement != null ? PeriodMaxElement.Value : null; }
                set
                {
                    if(value == null)
                      PeriodMaxElement = null; 
                    else
                      PeriodMaxElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("PeriodMax");
                }
            }
            
            /// <summary>
            /// s | min | h | d | wk | mo | a - unit of time (UCUM)
            /// </summary>
            [FhirElement("periodUnits", InSummary=true, Order=120)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Timing.UnitsOfTime> PeriodUnitsElement
            {
                get { return _PeriodUnitsElement; }
                set { _PeriodUnitsElement = value; OnPropertyChanged("PeriodUnitsElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Timing.UnitsOfTime> _PeriodUnitsElement;
            
            /// <summary>
            /// s | min | h | d | wk | mo | a - unit of time (UCUM)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Timing.UnitsOfTime? PeriodUnits
            {
                get { return PeriodUnitsElement != null ? PeriodUnitsElement.Value : null; }
                set
                {
                    if(value == null)
                      PeriodUnitsElement = null; 
                    else
                      PeriodUnitsElement = new Code<Hl7.Fhir.Model.Timing.UnitsOfTime>(value);
                    OnPropertyChanged("PeriodUnits");
                }
            }
            
            /// <summary>
            /// Regular life events the event is tied to
            /// </summary>
            [FhirElement("when", InSummary=true, Order=130)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Timing.EventTiming> WhenElement
            {
                get { return _WhenElement; }
                set { _WhenElement = value; OnPropertyChanged("WhenElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Timing.EventTiming> _WhenElement;
            
            /// <summary>
            /// Regular life events the event is tied to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Timing.EventTiming? When
            {
                get { return WhenElement != null ? WhenElement.Value : null; }
                set
                {
                    if(value == null)
                      WhenElement = null; 
                    else
                      WhenElement = new Code<Hl7.Fhir.Model.Timing.EventTiming>(value);
                    OnPropertyChanged("When");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TimingRepeatComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Bounds != null) dest.Bounds = (Hl7.Fhir.Model.Period)Bounds.DeepCopy();
                    if(CountElement != null) dest.CountElement = (Hl7.Fhir.Model.Integer)CountElement.DeepCopy();
                    if(DurationElement != null) dest.DurationElement = (Hl7.Fhir.Model.FhirDecimal)DurationElement.DeepCopy();
                    if(DurationUnitsElement != null) dest.DurationUnitsElement = (Code<Hl7.Fhir.Model.Timing.UnitsOfTime>)DurationUnitsElement.DeepCopy();
                    if(FrequencyElement != null) dest.FrequencyElement = (Hl7.Fhir.Model.Integer)FrequencyElement.DeepCopy();
                    if(FrequencyMaxElement != null) dest.FrequencyMaxElement = (Hl7.Fhir.Model.Integer)FrequencyMaxElement.DeepCopy();
                    if(PeriodElement != null) dest.PeriodElement = (Hl7.Fhir.Model.FhirDecimal)PeriodElement.DeepCopy();
                    if(PeriodMaxElement != null) dest.PeriodMaxElement = (Hl7.Fhir.Model.FhirDecimal)PeriodMaxElement.DeepCopy();
                    if(PeriodUnitsElement != null) dest.PeriodUnitsElement = (Code<Hl7.Fhir.Model.Timing.UnitsOfTime>)PeriodUnitsElement.DeepCopy();
                    if(WhenElement != null) dest.WhenElement = (Code<Hl7.Fhir.Model.Timing.EventTiming>)WhenElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TimingRepeatComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TimingRepeatComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Bounds, otherT.Bounds)) return false;
                if( !DeepComparable.Matches(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.Matches(DurationElement, otherT.DurationElement)) return false;
                if( !DeepComparable.Matches(DurationUnitsElement, otherT.DurationUnitsElement)) return false;
                if( !DeepComparable.Matches(FrequencyElement, otherT.FrequencyElement)) return false;
                if( !DeepComparable.Matches(FrequencyMaxElement, otherT.FrequencyMaxElement)) return false;
                if( !DeepComparable.Matches(PeriodElement, otherT.PeriodElement)) return false;
                if( !DeepComparable.Matches(PeriodMaxElement, otherT.PeriodMaxElement)) return false;
                if( !DeepComparable.Matches(PeriodUnitsElement, otherT.PeriodUnitsElement)) return false;
                if( !DeepComparable.Matches(WhenElement, otherT.WhenElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TimingRepeatComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Bounds, otherT.Bounds)) return false;
                if( !DeepComparable.IsExactly(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.IsExactly(DurationElement, otherT.DurationElement)) return false;
                if( !DeepComparable.IsExactly(DurationUnitsElement, otherT.DurationUnitsElement)) return false;
                if( !DeepComparable.IsExactly(FrequencyElement, otherT.FrequencyElement)) return false;
                if( !DeepComparable.IsExactly(FrequencyMaxElement, otherT.FrequencyMaxElement)) return false;
                if( !DeepComparable.IsExactly(PeriodElement, otherT.PeriodElement)) return false;
                if( !DeepComparable.IsExactly(PeriodMaxElement, otherT.PeriodMaxElement)) return false;
                if( !DeepComparable.IsExactly(PeriodUnitsElement, otherT.PeriodUnitsElement)) return false;
                if( !DeepComparable.IsExactly(WhenElement, otherT.WhenElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// When the event occurs
        /// </summary>
        [FhirElement("event", InSummary=true, Order=30)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirDateTime> EventElement
        {
            get { if(_EventElement==null) _EventElement = new List<Hl7.Fhir.Model.FhirDateTime>(); return _EventElement; }
            set { _EventElement = value; OnPropertyChanged("EventElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirDateTime> _EventElement;
        
        /// <summary>
        /// When the event occurs
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Event
        {
            get { return EventElement != null ? EventElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  EventElement = null; 
                else
                  EventElement = new List<Hl7.Fhir.Model.FhirDateTime>(value.Select(elem=>new Hl7.Fhir.Model.FhirDateTime(elem)));
                OnPropertyChanged("Event");
            }
        }
        
        /// <summary>
        /// When the event is to occur
        /// </summary>
        [FhirElement("repeat", InSummary=true, Order=40)]
        [DataMember]
        public Hl7.Fhir.Model.Timing.TimingRepeatComponent Repeat
        {
            get { return _Repeat; }
            set { _Repeat = value; OnPropertyChanged("Repeat"); }
        }
        
        private Hl7.Fhir.Model.Timing.TimingRepeatComponent _Repeat;
        
        /// <summary>
        /// BID | TID | QID | AM | PM +
        /// </summary>
        [FhirElement("code", InSummary=true, Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Timing;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(EventElement != null) dest.EventElement = new List<Hl7.Fhir.Model.FhirDateTime>(EventElement.DeepCopy());
                if(Repeat != null) dest.Repeat = (Hl7.Fhir.Model.Timing.TimingRepeatComponent)Repeat.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Timing());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Timing;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(EventElement, otherT.EventElement)) return false;
            if( !DeepComparable.Matches(Repeat, otherT.Repeat)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Timing;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(EventElement, otherT.EventElement)) return false;
            if( !DeepComparable.IsExactly(Repeat, otherT.Repeat)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            
            return true;
        }
        
    }
    
}
