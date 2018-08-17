using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;

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
    /// A timing schedule that specifies an event that may occur multiple times
    /// </summary>
    [FhirType("Timing")]
    [DataContract]
    public partial class Timing : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Timing"; } }
        
        /// <summary>
        /// A unit of time (units from UCUM).
        /// (url: http://hl7.org/fhir/ValueSet/units-of-time)
        /// </summary>
        [FhirEnumeration("UnitsOfTime")]
        public enum UnitsOfTime
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://unitsofmeasure.org)
            /// </summary>
            [EnumLiteral("s", "http://unitsofmeasure.org"), Description("second")]
            S,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://unitsofmeasure.org)
            /// </summary>
            [EnumLiteral("min", "http://unitsofmeasure.org"), Description("minute")]
            Min,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://unitsofmeasure.org)
            /// </summary>
            [EnumLiteral("h", "http://unitsofmeasure.org"), Description("hour")]
            H,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://unitsofmeasure.org)
            /// </summary>
            [EnumLiteral("d", "http://unitsofmeasure.org"), Description("day")]
            D,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://unitsofmeasure.org)
            /// </summary>
            [EnumLiteral("wk", "http://unitsofmeasure.org"), Description("week")]
            Wk,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://unitsofmeasure.org)
            /// </summary>
            [EnumLiteral("mo", "http://unitsofmeasure.org"), Description("month")]
            Mo,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://unitsofmeasure.org)
            /// </summary>
            [EnumLiteral("a", "http://unitsofmeasure.org"), Description("year")]
            A,
        }

        /// <summary>
        /// Real world event that the relating to the schedule.
        /// (url: http://hl7.org/fhir/ValueSet/event-timing)
        /// </summary>
        [FhirEnumeration("EventTiming")]
        public enum EventTiming
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("HS", "http://hl7.org/fhir/v3/TimingEvent"), Description("HS")]
            HS,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("WAKE", "http://hl7.org/fhir/v3/TimingEvent"), Description("WAKE")]
            WAKE,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("C", "http://hl7.org/fhir/v3/TimingEvent"), Description("C")]
            C,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("CM", "http://hl7.org/fhir/v3/TimingEvent"), Description("CM")]
            CM,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("CD", "http://hl7.org/fhir/v3/TimingEvent"), Description("CD")]
            CD,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("CV", "http://hl7.org/fhir/v3/TimingEvent"), Description("CV")]
            CV,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("AC", "http://hl7.org/fhir/v3/TimingEvent"), Description("AC")]
            AC,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("ACM", "http://hl7.org/fhir/v3/TimingEvent"), Description("ACM")]
            ACM,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("ACD", "http://hl7.org/fhir/v3/TimingEvent"), Description("ACD")]
            ACD,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("ACV", "http://hl7.org/fhir/v3/TimingEvent"), Description("ACV")]
            ACV,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("PC", "http://hl7.org/fhir/v3/TimingEvent"), Description("PC")]
            PC,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("PCM", "http://hl7.org/fhir/v3/TimingEvent"), Description("PCM")]
            PCM,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("PCD", "http://hl7.org/fhir/v3/TimingEvent"), Description("PCD")]
            PCD,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/v3/TimingEvent)
            /// </summary>
            [EnumLiteral("PCV", "http://hl7.org/fhir/v3/TimingEvent"), Description("PCV")]
            PCV,
        }

        [FhirType("RepeatComponent")]
        [DataContract]
        public partial class RepeatComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "RepeatComponent"; } }
            
            /// <summary>
            /// Length/Range of lengths, or (Start and/or end) limits
            /// </summary>
            [FhirElement("bounds", InSummary=true, Order=40, Choice=ChoiceType.DatatypeChoice)]
			[CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Duration),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Bounds
            {
                get { return _Bounds; }
                set { _Bounds = value; OnPropertyChanged("Bounds"); }
            }
            
            private Hl7.Fhir.Model.Element _Bounds;
            
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
                    if (!value.HasValue)
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
                    if (!value.HasValue)
                      DurationElement = null; 
                    else
                      DurationElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Duration");
                }
            }
            
            /// <summary>
            /// How long when it happens (Max)
            /// </summary>
            [FhirElement("durationMax", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal DurationMaxElement
            {
                get { return _DurationMaxElement; }
                set { _DurationMaxElement = value; OnPropertyChanged("DurationMaxElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _DurationMaxElement;
            
            /// <summary>
            /// How long when it happens (Max)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? DurationMax
            {
                get { return DurationMaxElement != null ? DurationMaxElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                      DurationMaxElement = null; 
                    else
                      DurationMaxElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("DurationMax");
                }
            }
            
            /// <summary>
            /// s | min | h | d | wk | mo | a - unit of time (UCUM)
            /// </summary>
            [FhirElement("durationUnits", InSummary=true, Order=80)]
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
                    if (!value.HasValue)
                      DurationUnitsElement = null; 
                    else
                      DurationUnitsElement = new Code<Hl7.Fhir.Model.Timing.UnitsOfTime>(value);
                    OnPropertyChanged("DurationUnits");
                }
            }
            
            /// <summary>
            /// Event occurs frequency times per period
            /// </summary>
            [FhirElement("frequency", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Integer FrequencyElement
            {
                get { return _FrequencyElement; }
                set { _FrequencyElement = value; OnPropertyChanged("FrequencyElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _FrequencyElement;
            
            /// <summary>
            /// Event occurs frequency times per period
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Frequency
            {
                get { return FrequencyElement != null ? FrequencyElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                      FrequencyElement = null; 
                    else
                      FrequencyElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Frequency");
                }
            }
            
            /// <summary>
            /// Event occurs up to frequencyMax times per period
            /// </summary>
            [FhirElement("frequencyMax", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Integer FrequencyMaxElement
            {
                get { return _FrequencyMaxElement; }
                set { _FrequencyMaxElement = value; OnPropertyChanged("FrequencyMaxElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _FrequencyMaxElement;
            
            /// <summary>
            /// Event occurs up to frequencyMax times per period
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? FrequencyMax
            {
                get { return FrequencyMaxElement != null ? FrequencyMaxElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                      FrequencyMaxElement = null; 
                    else
                      FrequencyMaxElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("FrequencyMax");
                }
            }
            
            /// <summary>
            /// Event occurs frequency times per period
            /// </summary>
            [FhirElement("period", InSummary=true, Order=110)]
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
                    if (!value.HasValue)
                      PeriodElement = null; 
                    else
                      PeriodElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Period");
                }
            }
            
            /// <summary>
            /// Upper limit of period (3-4 hours)
            /// </summary>
            [FhirElement("periodMax", InSummary=true, Order=120)]
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
                    if (!value.HasValue)
                      PeriodMaxElement = null; 
                    else
                      PeriodMaxElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("PeriodMax");
                }
            }
            
            /// <summary>
            /// s | min | h | d | wk | mo | a - unit of time (UCUM)
            /// </summary>
            [FhirElement("periodUnits", InSummary=true, Order=130)]
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
                    if (!value.HasValue)
                      PeriodUnitsElement = null; 
                    else
                      PeriodUnitsElement = new Code<Hl7.Fhir.Model.Timing.UnitsOfTime>(value);
                    OnPropertyChanged("PeriodUnits");
                }
            }
            
            /// <summary>
            /// Regular life events the event is tied to
            /// </summary>
            [FhirElement("when", InSummary=true, Order=140)]
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
                    if (!value.HasValue)
                      WhenElement = null; 
                    else
                      WhenElement = new Code<Hl7.Fhir.Model.Timing.EventTiming>(value);
                    OnPropertyChanged("When");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RepeatComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Bounds != null) dest.Bounds = (Hl7.Fhir.Model.Element)Bounds.DeepCopy();
                    if(CountElement != null) dest.CountElement = (Hl7.Fhir.Model.Integer)CountElement.DeepCopy();
                    if(DurationElement != null) dest.DurationElement = (Hl7.Fhir.Model.FhirDecimal)DurationElement.DeepCopy();
                    if(DurationMaxElement != null) dest.DurationMaxElement = (Hl7.Fhir.Model.FhirDecimal)DurationMaxElement.DeepCopy();
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
                return CopyTo(new RepeatComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RepeatComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Bounds, otherT.Bounds)) return false;
                if( !DeepComparable.Matches(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.Matches(DurationElement, otherT.DurationElement)) return false;
                if( !DeepComparable.Matches(DurationMaxElement, otherT.DurationMaxElement)) return false;
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
                var otherT = other as RepeatComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Bounds, otherT.Bounds)) return false;
                if( !DeepComparable.IsExactly(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.IsExactly(DurationElement, otherT.DurationElement)) return false;
                if( !DeepComparable.IsExactly(DurationMaxElement, otherT.DurationMaxElement)) return false;
                if( !DeepComparable.IsExactly(DurationUnitsElement, otherT.DurationUnitsElement)) return false;
                if( !DeepComparable.IsExactly(FrequencyElement, otherT.FrequencyElement)) return false;
                if( !DeepComparable.IsExactly(FrequencyMaxElement, otherT.FrequencyMaxElement)) return false;
                if( !DeepComparable.IsExactly(PeriodElement, otherT.PeriodElement)) return false;
                if( !DeepComparable.IsExactly(PeriodMaxElement, otherT.PeriodMaxElement)) return false;
                if( !DeepComparable.IsExactly(PeriodUnitsElement, otherT.PeriodUnitsElement)) return false;
                if( !DeepComparable.IsExactly(WhenElement, otherT.WhenElement)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Bounds != null) yield return Bounds;
                    if (CountElement != null) yield return CountElement;
                    if (DurationElement != null) yield return DurationElement;
                    if (DurationMaxElement != null) yield return DurationMaxElement;
                    if (DurationUnitsElement != null) yield return DurationUnitsElement;
                    if (FrequencyElement != null) yield return FrequencyElement;
                    if (FrequencyMaxElement != null) yield return FrequencyMaxElement;
                    if (PeriodElement != null) yield return PeriodElement;
                    if (PeriodMaxElement != null) yield return PeriodMaxElement;
                    if (PeriodUnitsElement != null) yield return PeriodUnitsElement;
                    if (WhenElement != null) yield return WhenElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    if (Bounds != null) yield return new ElementValue("bounds", Bounds);
                    if (CountElement != null) yield return new ElementValue("count", CountElement);
                    if (DurationElement != null) yield return new ElementValue("duration", DurationElement);
                    if (DurationMaxElement != null) yield return new ElementValue("durationMax", DurationMaxElement);
                    if (DurationUnitsElement != null) yield return new ElementValue("durationUnits", DurationUnitsElement);
                    if (FrequencyElement != null) yield return new ElementValue("frequency", FrequencyElement);
                    if (FrequencyMaxElement != null) yield return new ElementValue("frequencyMax", FrequencyMaxElement);
                    if (PeriodElement != null) yield return new ElementValue("period", PeriodElement);
                    if (PeriodMaxElement != null) yield return new ElementValue("periodMax", PeriodMaxElement);
                    if (PeriodUnitsElement != null) yield return new ElementValue("periodUnits", PeriodUnitsElement);
                    if (WhenElement != null) yield return new ElementValue("when", WhenElement);
 
                } 
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
                if (value == null)
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
        public Hl7.Fhir.Model.Timing.RepeatComponent Repeat
        {
            get { return _Repeat; }
            set { _Repeat = value; OnPropertyChanged("Repeat"); }
        }
        
        private Hl7.Fhir.Model.Timing.RepeatComponent _Repeat;
        
        /// <summary>
        /// QD | QOD | Q4H | Q6H | BID | TID | QID | AM | PM +
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
                if(Repeat != null) dest.Repeat = (Hl7.Fhir.Model.Timing.RepeatComponent)Repeat.DeepCopy();
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

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in EventElement) { if (elem != null) yield return elem; }
                if (Repeat != null) yield return Repeat;
                if (Code != null) yield return Code;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren 
        { 
            get 
            { 
                foreach (var item in base.NamedChildren) yield return item; 
                foreach (var elem in EventElement) { if (elem != null) yield return new ElementValue("event", elem); }
                if (Repeat != null) yield return new ElementValue("repeat", Repeat);
                if (Code != null) yield return new ElementValue("code", Code);
 
            } 
        } 
    
    
    }
    
}
