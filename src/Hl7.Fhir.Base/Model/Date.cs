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

using System;
using System.Diagnostics.CodeAnalysis;
using P = Hl7.Fhir.ElementModel.Types;

#nullable enable

namespace Hl7.Fhir.Model
{
    public partial class Date
    {
        public Date(int year, int month, int day)
            : this(string.Format(System.Globalization.CultureInfo.InvariantCulture, FhirDateTime.FMT_YEARMONTHDAY, year, month, day))
        {
        }

        public Date(int year, int month)
            : this(string.Format(System.Globalization.CultureInfo.InvariantCulture, FhirDateTime.FMT_YEARMONTH, year, month))
        {
        }

        public Date(int year) : this(string.Format(System.Globalization.CultureInfo.InvariantCulture, FhirDateTime.FMT_YEAR, year))
        {
        }

        public static Date FromDateTimeOffset(DateTimeOffset date) => new(date.Year, date.Month, date.Day);

        /// <summary>
        /// Gets the current date in the local timezone.
        /// </summary>
        public static Date Today() => FromDateTimeOffset(DateTimeOffset.Now);

        /// <summary>
        /// Gets the current date in UTC.
        /// </summary>
        public static Date UtcToday() => FromDateTimeOffset(DateTimeOffset.UtcNow);

        [NonSerialized]  // To prevent binary serialization from serializing this field
        private P.Date? _parsedValue = null;

        // This is a sentinel value that marks that the current string representation is
        // not parseable, so we don't have to try again. It's value is never used, it's just
        // checked by reference.
        private static readonly P.Date INVALID_VALUE = P.Date.FromDateTimeOffset(DateTimeOffset.MinValue);

        /// <summary>
        /// Converts a Fhir Date to a <see cref="P.Date"/>.
        /// </summary>
        /// <returns>true if the Fhir Date contains a valid date string, false otherwise.</returns>
        public bool TryToDate([NotNullWhen(true)] out P.Date? date)
        {
            if (_parsedValue is null)
            {
                if (Value is not null && !(P.Date.TryParse(Value, out _parsedValue) && !_parsedValue!.HasOffset))
                    _parsedValue = INVALID_VALUE;
            }

            if (hasInvalidParsedValue())
            {
                date = null;
                return false;
            }
            else
            {
                date = _parsedValue!;
                return true;
            }

            bool hasInvalidParsedValue() => ReferenceEquals(_parsedValue, INVALID_VALUE);
        }

        /// <summary>
        /// Converts a Fhir Date to a <see cref="P.Date"/>.
        /// </summary>
        /// <returns>The Date, or null if the <see cref="Value"/> is null.</returns>
        /// <exception cref="FormatException">Thrown when the Value does not contain a valid FHIR Date.</exception>
        public P.Date? ToDate() => TryToDate(out var dt) ? dt : throw new FormatException($"String '{Value}' was not recognized as a valid date.");

        protected override void OnObjectValueChanged()
        {
            _parsedValue = null;
            base.OnObjectValueChanged();
        }

        /// <summary>
        /// Converts this Fhir Fhir Date to a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <returns>A DateTimeOffset filled out to midnight, january 1 (UTC) in case of a partial date.</returns>
        public DateTimeOffset? ToDateTimeOffset()
        {
            if (Value == null) return null;   // Note: this behaviour is inconsistent with ToDateTimeOffset() in FhirDateTime

            // ToDateTimeOffset() will convert partial date/times by filling out to midnight/january 1 UTC
            if (!TryToDate(out var dt))
                throw new FormatException($"Date '{Value}' was not recognized as a valid datetime.");

            // Since Value is not null and the parsed value is valid, dto will not be null
            return dt!.ToDateTimeOffset(TimeSpan.Zero);
        }

        /// <summary>
        /// Convert this Fhir Date to a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <returns>True if the value of the Fhir Date is not null and can be parsed as a DateTimeOffset, false otherwise.</returns>
        public bool TryToDateTimeOffset(out DateTimeOffset dto)
        {
            if (Value is not null && TryToDate(out var dt))
            {
                dto = dt.ToDateTimeOffset(TimeSpan.Zero);
                return true;
            }
            else
            {
                dto = default;
                return false;
            }
        }

        /// <summary>
        /// Checks whether the given literal is correctly formatted.
        /// </summary>
        public static bool IsValidValue(string value) => P.Date.TryParse(value, out var parsed) && !parsed.HasOffset;
    }
}

#nullable restore