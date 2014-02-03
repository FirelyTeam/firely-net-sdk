using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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
// Generated on Mon, Feb 3, 2014 11:56+0100 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A schedule that specifies an event that may occur multiple times
    /// </summary>
    [FhirType("Schedule")]
    [DataContract]
    public partial class Schedule : Hl7.Fhir.Model.Element
    {
        /// <summary>
        /// A unit of time (units from UCUM)
        /// </summary>
        [FhirEnumeration("UnitsOfTime")]
        public enum UnitsOfTime
        {
            [EnumLiteral("s")]
            S, // second.
            [EnumLiteral("min")]
            Min, // minute.
            [EnumLiteral("h")]
            H, // hour.
            [EnumLiteral("d")]
            D, // day.
            [EnumLiteral("wk")]
            Wk, // week.
            [EnumLiteral("mo")]
            Mo, // month.
            [EnumLiteral("a")]
            A, // year.
        }
        
        /// <summary>
        /// Real world event that the schedule relates to
        /// </summary>
        [FhirEnumeration("EventTiming")]
        public enum EventTiming
        {
            [EnumLiteral("HS")]
            HS, // event occurs [duration] before the hour of sleep (or trying to).
            [EnumLiteral("WAKE")]
            WAKE, // event occurs [duration] after waking.
            [EnumLiteral("AC")]
            AC, // event occurs [duration] before a meal (from the Latin ante cibus).
            [EnumLiteral("ACM")]
            ACM, // event occurs [duration] before breakfast (from the Latin ante cibus matutinus).
            [EnumLiteral("ACD")]
            ACD, // event occurs [duration] before lunch (from the Latin ante cibus diurnus).
            [EnumLiteral("ACV")]
            ACV, // event occurs [duration] before dinner (from the Latin ante cibus vespertinus).
            [EnumLiteral("PC")]
            PC, // event occurs [duration] after a meal (from the Latin post cibus).
            [EnumLiteral("PCM")]
            PCM, // event occurs [duration] after breakfast (from the Latin post cibus matutinus).
            [EnumLiteral("PCD")]
            PCD, // event occurs [duration] after lunch (from the Latin post cibus diurnus).
            [EnumLiteral("PCV")]
            PCV, // event occurs [duration] after dinner (from the Latin post cibus vespertinus).
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ScheduleRepeatComponent")]
        [DataContract]
        public partial class ScheduleRepeatComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Event occurs frequency times per duration
            /// </summary>
            [FhirElement("frequency", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Integer FrequencyElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// HS | WAKE | AC | ACM | ACD | ACV | PC | PCM | PCD | PCV - common life events
            /// </summary>
            [FhirElement("when", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Schedule.EventTiming> WhenElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Schedule.EventTiming? When
            {
                get { return WhenElement != null ? WhenElement.Value : null; }
                set
                {
                    if(value == null)
                      WhenElement = null; 
                    else
                      WhenElement = new Code<Hl7.Fhir.Model.Schedule.EventTiming>(value);
                }
            }
            
            /// <summary>
            /// Repeating or event-related duration
            /// </summary>
            [FhirElement("duration", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal DurationElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// s | min | h | d | wk | mo | a - unit of time (UCUM)
            /// </summary>
            [FhirElement("units", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Schedule.UnitsOfTime> UnitsElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Schedule.UnitsOfTime? Units
            {
                get { return UnitsElement != null ? UnitsElement.Value : null; }
                set
                {
                    if(value == null)
                      UnitsElement = null; 
                    else
                      UnitsElement = new Code<Hl7.Fhir.Model.Schedule.UnitsOfTime>(value);
                }
            }
            
            /// <summary>
            /// Number of times to repeat
            /// </summary>
            [FhirElement("count", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Integer CountElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// When to stop repeats
            /// </summary>
            [FhirElement("end", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime EndElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string End
            {
                get { return EndElement != null ? EndElement.Value : null; }
                set
                {
                    if(value == null)
                      EndElement = null; 
                    else
                      EndElement = new Hl7.Fhir.Model.FhirDateTime(value);
                }
            }
            
        }
        
        
        /// <summary>
        /// When the event occurs
        /// </summary>
        [FhirElement("event", Order=40)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Period> Event { get; set; }
        
        /// <summary>
        /// Only if there is none or one event
        /// </summary>
        [FhirElement("repeat", Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.Schedule.ScheduleRepeatComponent Repeat { get; set; }
        
    }
    
}
