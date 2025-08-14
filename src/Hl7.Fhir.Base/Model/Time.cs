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

using Hl7.Fhir.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using P = Hl7.Fhir.ElementModel.Types;
using COVE=Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Model;

public partial class Time
{
    public const string FMT_HOURMINSEC = "{0:D2}:{1:D2}:{2:D2}";
    public const string FMT_HOURMINSECMS = "{0:D2}:{1:D2}:{2:D2}.{3:D2}";

    public Time(int hour, int minute, int second) : this(string.Format(CultureInfo.InvariantCulture, FMT_HOURMINSEC, hour, minute, second))
    {
        // Nothing
    }
    
    public Time(int hour, int minute, int second, int millis) :
        this(string.Format(CultureInfo.InvariantCulture, FMT_HOURMINSECMS, hour, minute, second, millis))
    {
        // Nothing
    }

    /// <summary>
    /// Takes the hour, minute and second of a given <see cref="DateTimeOffset"/> in the indicated timezone, and uses this
    /// to construct a new Time.
    /// </summary>
    /// <remarks>Note that by default, milliseconds are not included in the Time value. This is due to
    /// the nature of the FHIR Time datatype, which is normally used to communicate a time of day, e.g. for
    /// a medication administration. It is unusual to include milliseconds in such a time.
    /// </remarks>
    public static Time FromDateTimeOffset(DateTimeOffset dto, bool includeMillis = false) =>
        includeMillis
            ? new Time(dto.Hour, dto.Minute, dto.Second, dto.Millisecond)
            : new Time(dto.Hour, dto.Minute, dto.Second);

    public static Time Now() => FromDateTimeOffset(DateTimeOffset.Now);

    public static Time UtcNow() => FromDateTimeOffset(DateTimeOffset.UtcNow);

    [NonSerialized]  // To prevent binary serialization from serializing this field
    private P.Time? _parsedValue = null;

    /// <summary>
    /// Validates the JsonValue and updates the internal cached Time value.
    /// </summary>
    protected internal override COVE? ValidateObjectValue(PocoValidationContext? context)
    {
        if (_parsedValue is not null || base.JsonValue is null) return null;

        _parsedValue = null;

        if (base.JsonValue is not string unparsed)
            return COVE.INCORRECT_LITERAL_VALUE_TYPE(context, base.JsonValue, this.TypeName);

        _parsedValue = doParse(unparsed);
        return _parsedValue is null ? COVE.LITERAL_INVALID(context, base.JsonValue, this.TypeName) : null;
    }

    private static P.Time? doParse(string value) =>
        P.Time.TryParse(value, out var v) && !v.HasOffset ? v : null;

    /// <summary>
    /// Checks whether the given literal is correctly formatted.
    /// </summary>
    public static bool IsValidValue(string value) => doParse(value) is not null;

    /// <summary>
    /// Converts a Fhir Time to a <see cref="P.Time"/>.
    /// </summary>
    /// <returns>true if the Fhir Time contains a valid time string, false otherwise.</returns>
    public bool TryToSystemTime([NotNullWhen(true)] out P.Time? time)
    {
        if (ValidateObjectValue(null) is not null || _parsedValue is null)
        {
            time = null;
            return false;
        }

        time = _parsedValue;
        return true;
    }

    /// <summary>
    /// Converts a Fhir Time to a <see cref="P.Time"/>.
    /// </summary>
    /// <returns>The Time, or null if the <see cref="Value"/> is null.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the Value is null.</exception>
    /// <exception cref="FormatException">Thrown when the Value does not contain a valid FHIR Time.</exception>
    public P.Time ToSystemTime()
    {
        if (ValidateObjectValue(null) is {} error)
            throw error;

        if(_parsedValue is null)
            throw new InvalidOperationException("Value is null");

        return _parsedValue;
    }

    protected internal override P.Any? TryConvertToSystemTypeInternal() => TryToSystemTime(out var date) ? date : null;

    public override object? JsonValue
    {
        get => base.JsonValue;
        set
        {
            base.JsonValue = value;
            _parsedValue = null;
        }
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
        if (TryToSystemTime(out var dt) && !dt.HasOffset)
        {
            dto = dt.ToTimeSpan();
            return true;
        }

        dto = TimeSpan.Zero;
        return false;
    }
}