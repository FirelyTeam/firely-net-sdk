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

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model
{

    public partial class Time
    {
        public const string FMT_HOURMINSEC = "{0:D2}:{1:D2}:{2:D2}";

        public Time(int hour, int minute, int second) : this(string.Format(CultureInfo.InvariantCulture, FMT_HOURMINSEC, hour, minute, second))
        {
            // Nothing
        }

        /// <summary>
        /// Takes the hour, minute and second of a given <see cref="DateTimeOffset"/> in the indicated timezone, and uses this
        /// to construct a new Time.
        /// </summary>
        public static Time FromDateTimeOffset(DateTimeOffset dto) => new(dto.Hour, dto.Minute, dto.Second);

        public static Time Now() => FromDateTimeOffset(DateTimeOffset.Now);

        public static Time UtcNow() => FromDateTimeOffset(DateTimeOffset.UtcNow);

        [NonSerialized]  // To prevent binary serialization from serializing this field
        private P.Time? _parsedValue = null;

        // This is a sentintel value that marks that the current string representation is
        // not parseable, so we don't have to try again. It's value is never used, it's just
        // checked by reference.
        private static readonly P.Time INVALID_VALUE = P.Time.FromDateTimeOffset(DateTimeOffset.MinValue);

        /// <summary>
        /// Converts a Fhir Time to a <see cref="P.Time"/>.
        /// </summary>
        /// <returns>true if the Fhir Time contains a valid time string, false otherwise.</returns>
        public bool TryToTime([NotNullWhen(true)] out P.Time? time)
        {
            if (_parsedValue is null)
            {
                if (Value is not null && !P.Time.TryParse(Value, out _parsedValue))
                    _parsedValue = INVALID_VALUE;
            }

            if (hasInvalidParsedValue())
            {
                time = null;
                return false;
            }
            else
            {
                time = _parsedValue!;
                return true;
            }

            bool hasInvalidParsedValue() => ReferenceEquals(_parsedValue, INVALID_VALUE);
        }

        /// <summary>
        /// Converts a Fhir Time to a <see cref="P.Time"/>.
        /// </summary>
        /// <returns>The Time, or null if the <see cref="Value"/> is null.</returns>
        /// <exception cref="FormatException">Thrown when the Value does not contain a valid FHIR Time.</exception>
        public P.Time ToTime() => TryToTime(out var dt) ? dt : throw new FormatException($"String '{Value}' was not recognized as a valid time.");

        protected override void OnObjectValueChanged()
        {
            _parsedValue = null;
            base.OnObjectValueChanged();
        }

        /// <summary>
        /// Converts this Fhir Time to a <see cref="TimeSpan"/>.
        /// </summary>
        public TimeSpan ToTimeSpan() =>
            TryToTimeSpan(out var dt) ? dt :
                throw new FormatException($"Time '{Value}' was null or not recognized as a valid time.");

        /// <summary>
        /// Convert this FhirDateTime to a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <returns>True if the value of the Fhir Time is not null and can be parsed as a Time without an offset, false otherwise.</returns>
        public bool TryToTimeSpan(out TimeSpan dto)
        {
            if (Value is not null && TryToTime(out var dt) && !dt.HasOffset)
            {
                dto = dt.ToTimeSpan();
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
        public static bool IsValidValue(string value) => P.Time.TryParse(value, out var parsed) && !parsed.HasOffset;
    }
}

#nullable restore