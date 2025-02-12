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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model;

public partial class Instant
{
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

    protected override Type ObjectValueType => typeof(string);

    [NonSerialized]  // To prevent binary serialization from serializing this field
    private P.DateTime? _parsedValue = null;

    private bool tryGetParsedValue(out P.DateTime? dto)
    {
        dto = _parsedValue;
        if (_parsedValue is not null || ObjectValue is null) return true;
        if (ObjectValue is not string unparsed) return false;
        if (!P.DateTime.TryParse(unparsed, out _parsedValue) || !_parsedValue.IsInstant) return false;

        dto = _parsedValue;
        return true;
    }

    public override object? ObjectValue
    {
        get
        {
            if (_parsedValue is not null && base.ObjectValue is null)
                base.ObjectValue = _parsedValue.ToString();

            return base.ObjectValue;
        }
        set
        {
            base.ObjectValue = value;
            _parsedValue = null;
        }
    }

    public partial DateTimeOffset? Value
    {
        get
        {
            if (!tryGetParsedValue(out var value))
                throw new InvalidCastException($"Value '{ObjectValue}' of type {ObjectValue!.GetType()} is not a correct literal for a DateTime.");

            return value?.ToDateTimeOffset(TimeSpan.Zero);
        }

        set
        {
            _parsedValue = value is null ? null : P.DateTime.FromDateTimeOffset(value.Value);
            base.ObjectValue = null;
            OnPropertyChanged("Value");
        }
    }

    /// <summary>
    /// Checks whether the given literal is a correctly formatted Instant, with a precision higher than seconds.
    /// </summary>
    public bool HasValidValue() => tryGetParsedValue(out _);

     /// <inheritdoc cref="HasValidValue"/>
    public static bool IsValidValue(string value)
    {
        var i = new Instant { ObjectValue = value };
        return i.HasValidValue();
    }

    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var baseResults = base.Validate(validationContext);
        if (baseResults.Any()) return baseResults; // Try to avoid duplicative errors.

        if (HasValidValue())
            return baseResults;

        var result = CodedValidationException.INSTANT_LITERAL_INVALID(validationContext, ObjectValue).AsResult(validationContext);
        return baseResults.Append(result);
    }

    /// <summary>
    /// Converts this Instant to a <see cref="P.DateTime" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">The Value of this DateTime is null.</exception>
    public P.DateTime ToSystemDateTime() =>
        (P.DateTime?)TryConvertToSystemTypeInternal() ??
           throw new InvalidOperationException("Instant's value is null and can therefore not be converted to a System DateTime.");

    protected internal override P.Any? TryConvertToSystemTypeInternal() =>
        tryGetParsedValue(out var parsed) ? parsed : null;
}