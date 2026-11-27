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
using P = Hl7.Fhir.ElementModel.Types;
using COVE=Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Model;

public partial class Date
{
    public Date(int year, int month, int day)
        : this(string.Format(System.Globalization.CultureInfo.InvariantCulture, FhirDateTime.FMT_YEARMONTHDAY, year,
            month, day))
    {
    }

    public Date(int year, int month)
        : this(string.Format(System.Globalization.CultureInfo.InvariantCulture, FhirDateTime.FMT_YEARMONTH, year,
            month))
    {
    }

    public Date(int year) : this(string.Format(System.Globalization.CultureInfo.InvariantCulture, FhirDateTime.FMT_YEAR,
        year))
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

    // Several threads may read the same instance at the same time, and the first of them to need the parsed value
    // stores it here. The field is volatile, so that on every platform (ARM64 included) a thread that reads the
    // reference also sees the object it points to completely built. The .NET runtime already ensures that for an
    // object that does not change after it is built; volatile makes it independent of that.
    // A reference is always written in one step, so it is never seen half-written, in a 32-bit process either.
    [NonSerialized]  // To prevent binary serialization from serializing this field
    private volatile P.Date? _parsedValue = null;

    /// <summary>
    /// Validates the JsonValue and updates the internal cached Date value.
    /// </summary>
    protected internal override COVE? ValidateObjectValue(PocoValidationContext? context)
    {
        // This reads each field once and only ever stores a successfully parsed value, so a reader on another
        // thread never sees the cached value go back to null. Two threads may both parse; they store equal values.
        if (_parsedValue is not null) return null;

        var jsonValue = base.JsonValue;
        if (jsonValue is null) return null;

        if (jsonValue is not string unparsed)
            return COVE.INCORRECT_LITERAL_VALUE_TYPE(context, jsonValue, this.TypeName);

        var parsed = doParse(unparsed);
        if (parsed is null) return COVE.LITERAL_INVALID(context, jsonValue, this.TypeName);

        _parsedValue = parsed;
        return null;
    }

    private static P.Date? doParse(string value) =>
        P.Date.TryParse(value, out var v) && !v.HasOffset ? v : null;

    /// <summary>
    /// Checks whether the given literal is correctly formatted.
    /// </summary>
    public static bool IsValidValue(string value) => doParse(value) is not null;

    /// <summary>
    /// Converts a Fhir Date to a CQL <see cref="P.Date"/>.
    /// </summary>
    /// <returns>true if the Fhir Date contains a valid date string, false otherwise.</returns>
    public bool TryToSystemDate([NotNullWhen(true)] out P.Date? date)
    {
        date = ValidateObjectValue(null) is null ? _parsedValue : null;
        return date is not null;
    }

    /// <summary>
    /// Converts a Fhir Date to a CQL <see cref="P.Date"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the Value is null.</exception>
    /// <exception cref="FormatException">Thrown when the Value does not contain a valid FHIR Date.</exception>
    public P.Date ToSystemDate()
    {
        if (ValidateObjectValue(null) is {} error)
            throw error;

        return _parsedValue ?? throw new InvalidOperationException("Value is null");
    }

    protected internal override P.Any? TryConvertToSystemTypeInternal() => TryToSystemDate(out var date) ? date : null;

    /// <summary>
    /// Converts this Fhir Fhir Date to a <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <returns>A DateTimeOffset filled out to midnight, january 1 (UTC) in case of a partial date.</returns>
    public DateTimeOffset ToDateTimeOffset()
    {
        var dt = ToSystemDate();

        // Since Value is not null and the parsed value is valid, dto will not be null
        return dt.ToDateTimeOffset(TimeSpan.Zero);
    }

    /// <summary>
    /// Convert this Fhir Date to a <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <returns>True if the value of the Fhir Date is not null and can be parsed as a DateTimeOffset, false otherwise.</returns>
    public bool TryToDateTimeOffset(out DateTimeOffset dto)
    {
        if (TryToSystemDate(out var dt))
        {
            dto = dt.ToDateTimeOffset(TimeSpan.Zero);
            return true;
        }

        dto = default;
        return false;
    }


    public override object? JsonValue
    {
        get => base.JsonValue;
        set
        {
            base.JsonValue = value;
            _parsedValue = null;
        }
    }
}