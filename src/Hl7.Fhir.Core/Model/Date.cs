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
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay(@"\{{Value}}")]
    public partial class Date : IStringValue
    {
        public Date(int year, int month, int day)
            : this(String.Format(Model.FhirDateTime.FMT_YEARMONTHDAY, year, month, day))
        {
        }

        public Date(int year, int month)
            : this( String.Format(Model.FhirDateTime.FMT_YEARMONTH,year,month) )
        {
        }

        public Date(int year): this( String.Format(Model.FhirDateTime.FMT_YEAR, year))
        {
        }

        public static Date Today()
        {
            return new Date(DateTime.Now.ToString("yyyy-MM-dd"));
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
            return Regex.IsMatch(value, "^" + Date.PATTERN + "$", RegexOptions.Singleline);
        }

        public static bool operator >(Date a, Date b)
        {
            var aValue = !Object.ReferenceEquals(a,null) ? a.Value : null;
            var bValue = !Object.ReferenceEquals(b, null) ? b.Value : null;

            if (aValue == null) return bValue == null;
            if (bValue == null) return false;

            return Primitives.PartialDateTime.Parse(a.Value) > Primitives.PartialDateTime.Parse(b.Value);
        }

        public static bool operator >=(Date a, Date b)
        {
            var aValue = !Object.ReferenceEquals(a, null) ? a.Value : null;
            var bValue = !Object.ReferenceEquals(b, null) ? b.Value : null;

            if (aValue == null) return bValue == null;
            if (bValue == null) return false;

            return Primitives.PartialDateTime.Parse(a.Value) >= Primitives.PartialDateTime.Parse(b.Value);
        }

        public static bool operator <(Date a, Date b)
        {
            var aValue = !Object.ReferenceEquals(a, null) ? a.Value : null;
            var bValue = !Object.ReferenceEquals(b, null) ? b.Value : null;

            if (aValue == null) return bValue == null;
            if (bValue == null) return false;

            return Primitives.PartialDateTime.Parse(a.Value) < Primitives.PartialDateTime.Parse(b.Value);
        }

        public static bool operator <=(Date a, Date b)
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
        public static bool operator ==(Date a, Date b)
        {
            return Object.Equals(a, b);
        }

        /// <summary>
        /// If you use this operator, you should check that a modifierExtension isn't changing the meaning
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Date a, Date b)
        {
            return !Object.Equals(a, b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Date other)
            {
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
