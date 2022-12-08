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

using Hl7.Fhir.Serialization;
using System;

#nullable enable

namespace Hl7.Fhir.Model
{
    public partial class Date
    {
        public Date(int year, int month, int day)
            : this(string.Format(FhirDateTime.FMT_YEARMONTHDAY, year, month, day))
        {
        }

        public Date(int year, int month)
            : this(string.Format(FhirDateTime.FMT_YEARMONTH, year, month))
        {
        }

        public Date(int year) : this(string.Format(FhirDateTime.FMT_YEAR, year))
        {
        }

        /// <summary>
        /// Gets the current date in the local timezone
        /// </summary>
        /// <returns>Gets the current date in the local timezone</returns>
        public static Date Today() => new(DateTimeOffset.Now.ToString("yyyy-MM-dd"));

        /// <summary>
        /// Gets the current date in the timezone UTC
        /// </summary>
        /// <returns>Gets the current date in the timezone UTC</returns>
        public static Date UtcToday() => new(DateTimeOffset.UtcNow.ToString("yyyy-MM-dd"));

        [Obsolete("Use ToDateTimeOffset instead")]
        public DateTime? ToDateTime() =>
            Value == null ? null : PrimitiveTypeConverter.ConvertTo<DateTimeOffset>(Value).DateTime;

        /// <summary>
        /// Converts this instance of a (partial) date into a .NET <see cref="DateTimeOffset"/>.
        /// </summary>
        public DateTimeOffset? ToDateTimeOffset() =>
            Value == null ? null : PrimitiveTypeConverter.ConvertTo<DateTimeOffset>(Value);

        /// <summary>
        /// Checks whether the given literal is correctly formatted.
        /// </summary>
        public static bool IsValidValue(string value) => ElementModel.Types.Date.TryParse(value, out var parsed) && !parsed.HasOffset;
    }
}

#nullable restore