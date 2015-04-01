/*
  Copyright (c) 2011-2012, HL7, Inc
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



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay(@"\{{Value}}")]
    public partial class FhirDateTime
    {
        public FhirDateTime(DateTimeOffset dt) : this(dt.ToString(FMT_FULL))
        {
        }

        public FhirDateTime(DateTime dt) : this( new DateTimeOffset(dt) )
        {
        }

        public FhirDateTime(int year, int month, int day, int hr, int min, int sec = 0)
            : this(new DateTime(year,month,day,hr,min,sec,DateTimeKind.Local))
        {
        }

        public FhirDateTime(int year, int month, int day)
            : this(String.Format(FMT_YEARMONTHDAY, year, month, day))
        {
        }

        public FhirDateTime(int year, int month)
            : this( String.Format(FMT_YEARMONTH,year,month) )
        {
        }

        public FhirDateTime(int year)
            : this(String.Format(FMT_YEAR, year))
        {
        }

        
        public const string FMT_FULL = "yyyy-MM-dd'T'HH:mm:ssK";
        public const string FMT_YEAR = "{0:D4}";
        public const string FMT_YEARMONTH = "{0:D4}-{1:D2}";
        public const string FMT_YEARMONTHDAY = "{0:D4}-{1:D2}-{2:D2}";

        public static FhirDateTime Now()
        {
            return new FhirDateTime(DateTimeOffset.Now.ToString(FMT_FULL));
        }

        /// <summary>
        /// Converts this Fhir DateTime as a .NET DateTimeOffset
        /// </summary>
        /// <param name="zone">Optional. Ensures the returned DateTimeOffset uses the the specified zone.</param>
        /// <returns>A DateTimeOffset filled out to midnight, january 1 in case of a partial date/time. If the Fhir DateTime
        /// does not specify a timezone, the local timezone of the machine is assumed. Note that the zone parameter has no 
        /// effect on this, this merely converts the given Fhir datetime to the desired timezone</returns>
        public DateTimeOffset ToDateTimeOffset(TimeSpan? zone = null)
        {
            // ToDateTimeOffset() will convert partial date/times by filling out to midnight/january 1
            // When there's no timezone, the local timezone is assumed
            var dto = XmlConvert.ToDateTimeOffset(this.Value);

            //NB: There's a useful TimeZone class, but Portable45 does not support it
            if (zone != null) dto = dto.ToOffset(zone.Value);

            return dto;
        }

        public static bool IsValidValue(string value)
        {
            return Regex.IsMatch(value as string, "^" + FhirDateTime.PATTERN + "$", RegexOptions.Singleline);

            //TODO: Additional checks not implementable by the regex
        }

    }
}
