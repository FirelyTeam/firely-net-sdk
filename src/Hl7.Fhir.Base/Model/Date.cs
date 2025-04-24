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

    [NonSerialized] // To prevent binary serialization from serializing this field
    private P.Date? _parsedValue = null;

    /// <summary>
    /// Validates the JsonValue and updates the internal cached Date value.
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
        if (ValidateObjectValue(null) is not null || _parsedValue is null)
        {
            date = null;
            return false;
        }

        date = _parsedValue;
        return true;
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

        if(_parsedValue is null)
            throw new InvalidOperationException("Value is null");

        return _parsedValue;
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