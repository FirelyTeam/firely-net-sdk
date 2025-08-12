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

using Hl7.Fhir.Serialization;
using Hl7.Fhir.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using P = Hl7.Fhir.ElementModel.Types;
using COVE=Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Model;

public partial class FhirDateTime
{
    /// <summary>
    /// A <c>string.Format</c> pattern to use when formatting a full datetime with timezone.
    /// </summary>
    public const string FMT_FULL = "yyyy-MM-dd'T'HH:mm:ssK";

    /// <summary>
    /// A <c>string.Format</c> pattern to use when formatting a year.
    /// </summary>
    public const string FMT_YEAR = "{0:D4}";

    /// <summary>
    /// A <c>string.Format</c> pattern to use when formatting a year and month.
    /// </summary>
    public const string FMT_YEARMONTH = "{0:D4}-{1:D2}";

    /// <summary>
    /// A <c>string.Format</c> pattern to use when formatting a date.
    /// </summary>
    public const string FMT_YEARMONTHDAY = "{0:D4}-{1:D2}-{2:D2}";

    public FhirDateTime(DateTimeOffset dt) : this(PrimitiveTypeConverter.ConvertTo<string>(dt))
    {
    }

    public FhirDateTime(int year, int month, int day, int hr, int min, int sec, int millis, TimeSpan offset)
        : this(new DateTimeOffset(year, month, day, hr, min, sec, millis, offset))
    {
    }

    public FhirDateTime(int year, int month, int day, int hr, int min, int sec, TimeSpan offset)
        : this(new DateTimeOffset(year, month, day, hr, min, sec, offset))
    {
    }

    public FhirDateTime(int year, int month, int day)
        : this(string.Format(System.Globalization.CultureInfo.InvariantCulture, FMT_YEARMONTHDAY, year, month, day))
    {
    }

    public FhirDateTime(int year, int month)
        : this(string.Format(System.Globalization.CultureInfo.InvariantCulture, FMT_YEARMONTH, year, month))
    {
    }

    public FhirDateTime(int year)
        : this(string.Format(System.Globalization.CultureInfo.InvariantCulture, FMT_YEAR, year))
    {
    }

    public static FhirDateTime Now() => new(DateTimeOffset.Now);

    [NonSerialized]  // To prevent binary serialization from serializing this field
    private P.DateTime? _parsedValue = null;

    /// <summary>
    /// Validates the JsonValue and updates the internal cached DateTime value.
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

    private static P.DateTime? doParse(string literal) =>
        P.DateTime.TryParse(literal, out var parsed) &&
        (parsed.Precision <= P.DateTimePrecision.Day == !parsed.HasOffset) ? parsed : null;

    /// <summary>
    /// Checks whether the given literal is correctly formatted.
    /// </summary>
    public static bool IsValidValue(string value) => doParse(value) is not null;


    /// <summary>
    /// Converts a FhirDateTime to a <see cref="P.DateTime"/>.
    /// </summary>
    /// <returns>true if the FhirDateTime contains a valid date/time string, false otherwise.</returns>
    public bool TryToSystemDateTime([NotNullWhen(true)] out P.DateTime? dateTime)
    {
        if (ValidateObjectValue(null) is not null || _parsedValue is null)
        {
            dateTime = null;
            return false;
        }

        dateTime = _parsedValue;
        return true;
    }

    /// <summary>
    /// Converts a FhirDateTime to a <see cref="P.DateTime"/>.
    /// </summary>
    /// <returns>The DateTime, or null if the <see cref="Value"/> is null.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the Value is null.</exception>
    /// <exception cref="FormatException">Thrown when the Value does not contain a valid FHIR DateTime.</exception>
    public P.DateTime ToSystemDateTime()
    {
        if (ValidateObjectValue(null) is { } error)
            throw error;

        if (_parsedValue is null)
            throw new InvalidOperationException("Value is null.");

        return _parsedValue;
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

    /// <summary>
    /// Converts this Fhir FhirDateTime to a <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="zone">Ensures the returned DateTimeOffset uses the the specified zone.</param>
    /// <remarks>In .NET the minimal value for DateTimeOffset is 1/1/0001 12:00:00 AM +00:00. That means,for example,
    /// a FhirDateTime of "0001-01-01T00:00:00+01:00" could not be converted to a DateTimeOffset. In that case a
    /// ArgumentOutOfRangeException will be thrown.</remarks>
    /// <returns>A DateTimeOffset filled out to midnight, january 1 (UTC) in case of a partial date/time. If the Fhir DateTime
    /// does not specify a timezone, the UTC (Coordinated Universal Time) is assumed. Note that the zone parameter has no
    /// effect on this, this merely converts the given Fhir datetime to the desired timezone</returns>
    public DateTimeOffset ToDateTimeOffset(TimeSpan zone)
    {
        var dt = ToSystemDateTime();

        // Since Value is not null and the parsed value is valid, dto will not be null
        return dt.ToDateTimeOffset(TimeSpan.Zero).ToOffset(zone);
    }

    /// <summary>
    /// Convert this FhirDateTime to a <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <returns>True if the value of the FhirDateTime is not null, can be parsed as a DateTimeOffset and has a
    /// specified timezone, false otherwise.</returns>
    public bool TryToDateTimeOffset(out DateTimeOffset dto)
    {
        if (TryToSystemDateTime(out var dt) && dt.Offset is not null)
        {
            dto = dt.ToDateTimeOffset(dt.Offset.Value);
            return true;
        }

        dto = default;
        return false;
    }

    /// <summary>
    /// Convert this FhirDateTime to a <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="defaultOffset">Used when the partial FhirDateTime does not have an offset specified.</param>
    /// <param name="dto">The converted <see cref="DateTimeOffset"/>.</param>
    /// <returns>True if the value of the FhirDateTime is not null and can be parsed as a DateTimeOffset, false otherwise.</returns>
    public bool TryToDateTimeOffset(TimeSpan defaultOffset, out DateTimeOffset dto)
    {
        if (TryToSystemDateTime(out var dt))
        {
            dto = dt.ToDateTimeOffset(defaultOffset);
            return true;
        }

        dto = default;
        return false;
    }

    protected internal override P.Any? TryConvertToSystemTypeInternal() => TryToSystemDateTime(out var date) ? date : null;
}