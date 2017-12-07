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

using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay(@"\{{Value}}")]
    public partial class FhirDateTime : IStringValue
    {
        public FhirDateTime(DateTimeOffset dt) : this(PrimitiveTypeConverter.ConvertTo<string>(dt))
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
            : this(String.Format(System.Globalization.CultureInfo.InvariantCulture, FMT_YEARMONTHDAY, year, month, day))
        {
        }

        public FhirDateTime(int year, int month)
            : this( String.Format(System.Globalization.CultureInfo.InvariantCulture, FMT_YEARMONTH, year,month) )
        {
        }

        public FhirDateTime(int year)
            : this(String.Format(System.Globalization.CultureInfo.InvariantCulture, FMT_YEAR, year))
        {
        }

        
        public const string FMT_FULL = "yyyy-MM-dd'T'HH:mm:ssK";
        public const string FMT_YEAR = "{0:D4}";
        public const string FMT_YEARMONTH = "{0:D4}-{1:D2}";
        public const string FMT_YEARMONTHDAY = "{0:D4}-{1:D2}-{2:D2}";

        public static FhirDateTime Now()
        {
            return new FhirDateTime(PrimitiveTypeConverter.ConvertTo<string>(DateTimeOffset.Now));
        }

        /// <summary>
        /// Converts this Fhir DateTime as a .NET DateTimeOffset
        /// </summary>
        /// <param name="zone">Optional. Ensures the returned DateTimeOffset uses the the specified zone.</param>
        /// <remarks>In .NET the minimal value for DateTimeOffset is 1/1/0001 12:00:00 AM +00:00. That means,for example, 
        /// a FhirDateTime of "0001-01-01T00:00:00+01:00" could not be converted to a DateTimeOffset. In that case a 
        /// ArgumentOutOfRangeException will be thrown.</remarks>
        /// <returns>A DateTimeOffset filled out to midnight, january 1 (UTC) in case of a partial date/time. If the Fhir DateTime
        /// does not specify a timezone, the UTC (Coordinated Universal Time) is assumed. Note that the zone parameter has no 
        /// effect on this, this merely converts the given Fhir datetime to the desired timezone</returns>
        public DateTimeOffset ToDateTimeOffset(TimeSpan? zone = null)
        {
            if (this.Value == null) throw new InvalidOperationException("FhirDateTime's value is null");

            // ToDateTimeOffset() will convert partial date/times by filling out to midnight/january 1 UTC
            // When there's no timezone, the UTC is assumed
            var dto = PrimitiveTypeConverter.ConvertTo<DateTimeOffset>(this.Value);

            //NB: There's a useful TimeZone class, but Portable45 does not support it
            if (zone != null) dto = dto.ToOffset(zone.Value);

            return dto;
        }

        public DateTime? ToDateTime()
        {
            if (this.Value == null) return null;

            return PrimitiveTypeConverter.ConvertTo<DateTime>(this.Value);
        }

        public Primitives.PartialDateTime? ToPartialDateTime()
        {
            if (Value != null)
                return Primitives.PartialDateTime.Parse(Value);
            else
                return null;
        }

        public static bool IsValidValue(string value)
        {
            return Regex.IsMatch(value as string, "^" + FhirDateTime.PATTERN + "$", RegexOptions.Singleline);

            //TODO: Additional checks not implementable by the regex
        }

        public static bool operator >(FhirDateTime a, FhirDateTime b)
        {
            var aValue = !Object.ReferenceEquals(a, null) ? a.Value : null;
            var bValue = !Object.ReferenceEquals(b, null) ? b.Value : null;

            if (aValue == null) return bValue == null;
            if (bValue == null) return false;

            return Primitives.PartialDateTime.Parse(a.Value) > Primitives.PartialDateTime.Parse(b.Value);
        }

        public static bool operator >=(FhirDateTime a, FhirDateTime b)
        {
            var aValue = !Object.ReferenceEquals(a, null) ? a.Value : null;
            var bValue = !Object.ReferenceEquals(b, null) ? b.Value : null;

            if (aValue == null) return bValue == null;
            if (bValue == null) return false;

            return Primitives.PartialDateTime.Parse(a.Value) >= Primitives.PartialDateTime.Parse(b.Value);
        }

        public static bool operator <(FhirDateTime a, FhirDateTime b)
        {
            var aValue = !Object.ReferenceEquals(a, null) ? a.Value : null;
            var bValue = !Object.ReferenceEquals(b, null) ? b.Value : null;

            if (aValue == null) return bValue == null;
            if (bValue == null) return false;

            return Primitives.PartialDateTime.Parse(a.Value) < Primitives.PartialDateTime.Parse(b.Value);
        }

        public static bool operator <=(FhirDateTime a, FhirDateTime b)
        {
            var aValue = !Object.ReferenceEquals(a, null) ? a.Value : null;
            var bValue = !Object.ReferenceEquals(b, null) ? b.Value : null;

            if (aValue == null) return bValue == null;
            if (bValue == null) return false;

            return Primitives.PartialDateTime.Parse(a.Value) <= Primitives.PartialDateTime.Parse(b.Value);
        }

        /// <summary>
        /// If you use this operator, you should check that a modifierExtension isn't changing the meaning
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(FhirDateTime a, FhirDateTime b)
        {
            return Object.Equals(a, b);
        }

        /// <summary>
        /// If you use this operator, you should check that a modifierExtension isn't changing the meaning
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(FhirDateTime a, FhirDateTime b)
        {
            return !Object.Equals(a, b);
        }

        public override bool Equals(object obj)
        {
            if (obj is FhirDateTime)
            {
                var other = (FhirDateTime)obj;
                var otherValue = !Object.ReferenceEquals(other, null) ? other.Value : null;

                if (Value == null) return otherValue == null;
                if (otherValue == null) return false;

                if (this.Value == otherValue) return true; // Default reference/string comparison works in most cases

                var left = Primitives.PartialDateTime.Parse(Value);
                var right = Primitives.PartialDateTime.Parse(otherValue);

                return left == right;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
