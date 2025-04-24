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

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using P = Hl7.Fhir.ElementModel.Types;
using COVE=Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Model;

public partial class Instant
{
    [CLSCompliant(false)]
    [FhirElement("value", IsPrimitiveValue=true, XmlSerialization=XmlRepresentation.XmlAttr, InSummary=true, Order=30)]
    [AllowedTypes(typeof(P.DateTime))]
    [DataMember]
    public DateTimeOffset? Value
    {
        get
        {
            if (ValidateObjectValue(null) is {} error)
                throw error;

            return _parsedValue?.ToDateTimeOffset(TimeSpan.Zero);
        }

        set
        {
            _parsedValue = value is null ? null : P.DateTime.FromDateTimeOffset(value.Value);
            base.JsonValue = null;
            OnPropertyChanged("Value");
        }
    }


    public static Instant FromLocalDateTime(int year, int month, int day,
        int hour, int min, int sec, int millis = 0) =>
        new(new DateTimeOffset(year, month, day, hour, min, sec, millis, DateTimeOffset.Now.Offset));

    public static Instant FromDateTimeUtc(int year, int month, int day,
        int hour, int min, int sec, int millis = 0) =>
        new(new DateTimeOffset(year, month, day, hour, min, sec, millis,
            TimeSpan.Zero));

    /// <summary>
    /// Returns an Instant initialized with the current date and time.
    /// </summary>
    /// <returns></returns>
    public static Instant Now() => new(DateTimeOffset.Now);

    [NonSerialized]  // To prevent binary serialization from serializing this field
    private P.DateTime? _parsedValue = null;

    /// <summary>
    /// Validates the JsonValue and updates the internal cached CQL DateTime value.
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
        P.DateTime.TryParse(literal, out var parsed) && parsed.IsInstant ? parsed : null;

    /// <summary>
    /// Checks whether the given literal is correctly formatted.
    /// </summary>
    public static bool IsValidValue(string value) => doParse(value) is not null;

    public override object? JsonValue
    {
        get
        {
            if (_parsedValue is not null && base.JsonValue is null)
                base.JsonValue = _parsedValue.ToString();

            return base.JsonValue;
        }
        set
        {
            base.JsonValue = value;
            _parsedValue = null;
        }
    }

    /// <summary>
    /// Converts an Instant to a <see cref="P.DateTime"/>.
    /// </summary>
    /// <returns>true if the Instant contains a valid date/time string, false otherwise.</returns>
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
/// Converts this Instant to a <see cref="P.DateTime" />.
/// </summary>
/// <exception cref="InvalidOperationException">The Value of this DateTime is null.</exception>

    public P.DateTime ToSystemDateTime()
    {
        if (ValidateObjectValue(null) is { } error)
            throw error;

        if (_parsedValue is null)
            throw new InvalidOperationException("Value is null.");

        return _parsedValue;
    }

    protected internal override P.Any? TryConvertToSystemTypeInternal() => TryToSystemDateTime(out var date) ? date : null;
}