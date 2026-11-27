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
using System.Xml;
using P = Hl7.Fhir.ElementModel.Types;
using COVE=Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Model;

public partial class Integer64
{
    [CLSCompliant(false)]
    [FhirElement("value", IsPrimitiveValue=true, XmlSerialization=XmlRepresentation.XmlAttr, InSummary=true, Order=30)]
    [AllowedTypes(typeof(P.Long))]
    [DataMember]
    public long? Value
    {
        get
        {
            if (ValidateObjectValue(null) is {} error)
                throw error;

            return _hasParsedValue ? _parsedValue : null;
        }

        set
        {
            _parsedValue = value.GetValueOrDefault();
            _hasParsedValue = value.HasValue;
            base.JsonValue = null;
            OnPropertyChanged("Value");
        }
    }

    // The cached value is a value and a flag instead of one nullable field, because several threads may read the
    // same instance at the same time and a nullable value is not written in one step. The value is written before
    // the flag, and only the setters clear the flag, so a reader that sees the flag also sees the value.
    [NonSerialized]  // To prevent binary serialization from serializing this field
    private long _parsedValue;

    [NonSerialized]  // To prevent binary serialization from serializing this field
    private volatile bool _hasParsedValue;

    /// <summary>
    /// Validates the JsonValue and updates the internal cached long value.
    /// </summary>
    protected internal override COVE? ValidateObjectValue(PocoValidationContext? context)
    {
        // This reads each field once and only ever stores a successfully parsed value: a reader on another
        // thread never sees the cached value go away.
        if (_hasParsedValue) return null;

        var jsonValue = base.JsonValue;
        if (jsonValue is null) return null;

        if (jsonValue is not string unparsed)
            return COVE.INCORRECT_LITERAL_VALUE_TYPE(context, jsonValue, this.TypeName);

        if (doParse(unparsed) is not { } parsed)
            return COVE.LITERAL_INVALID(context, jsonValue, this.TypeName);

        _parsedValue = parsed;
        _hasParsedValue = true;
        return null;
    }

    private static long? doParse(string literal) =>
        P.Long.TryParse(literal, out var parsedLong) ? parsedLong.Value : null;

    /// <summary>
    /// Checks whether the given literal is correctly formatted.
    /// </summary>
    public static bool IsValidValue(string value) => doParse(value) is not null;

    public override object? JsonValue
    {
        get
        {
            if (_hasParsedValue && base.JsonValue is null)
                base.JsonValue = XmlConvert.ToString(_parsedValue);

            return base.JsonValue;
        }
        set
        {
            base.JsonValue = value;
            _hasParsedValue = false;
        }
    }



    /// <summary>
    /// Converts an Insteger64 to a <see cref="P.Long"/>.
    /// </summary>
    /// <returns>true if the Integer64 contains a valid date/time string, false otherwise.</returns>
    public bool TryToSystemLong([NotNullWhen(true)] out P.Long? longValue)
    {
        longValue = ValidateObjectValue(null) is null && _hasParsedValue ? new P.Long(_parsedValue) : null;
        return longValue is not null;
    }

    /// <summary>
    /// Converts this Integer64 to a <see cref="P.Long" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">The Value of this Integer64 is null,
    /// which is not valid for System longs.</exception>
    public P.Long ToSystemLong()
    {
        if (ValidateObjectValue(null) is { } error)
            throw error;

        if (!_hasParsedValue)
            throw new InvalidOperationException("Value is null.");

        return new P.Long(_parsedValue);
    }

    protected internal override P.Any? TryConvertToSystemTypeInternal() => TryToSystemLong(out var longValue) ? longValue : null;
}