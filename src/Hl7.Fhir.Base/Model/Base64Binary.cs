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

using Hl7.Fhir.ElementModel.Types;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading;
using COVE=Hl7.Fhir.Validation.CodedValidationException;
using P=Hl7.Fhir.ElementModel.Types;

#nullable enable

namespace Hl7.Fhir.Model;

public partial class Base64Binary
{
    [CLSCompliant(false)]
    [FhirElement("value", IsPrimitiveValue = true, XmlSerialization = XmlRepresentation.XmlAttr, InSummary = true,
        Order = 30)]
    [AllowedTypes(typeof(P.String))]
    [DataMember]
    public byte[]? Value
    {
        get
        {
            if (validateAndGetParsedValue(out var parsed) is {} error)
                throw error;

            return parsed;
        }

        set
        {
            _parsedValue = value;
            base.JsonValue = null;
            OnPropertyChanged("Value");
        }
    }

    public override object? JsonValue
    {
        get
        {
            // Without bytes there is nothing to convert, and the literal is the value. This is the state after
            // deserialization, and it needs no lock (see the comment on _parsedValue).
            if (_parsedValue is null && base.JsonValue is { } literal)
                return literal;

            lock (valueLock)
            {
                // A non-canonical literal is retained next to the parsed bytes (see getParsedValue). Callers can
                // modify the array returned by Value in place, so only keep the literal while it still decodes to
                // the cached bytes; otherwise the bytes win and the literal is regenerated from them.
                if (_parsedValue is { } parsed && (base.JsonValue is null || !retainedLiteralMatchesBytes(parsed)))
                {
                    base.JsonValue = Convert.ToBase64String(parsed);
                    _parsedValue = null;    // Clear the parsed value to free up memory
                }

                return base.JsonValue;
            }
        }
        set
        {
            base.JsonValue = value;
            _parsedValue = null;
        }
    }

    // Reading Value moves the data from the literal to the bytes, and reading JsonValue moves it back, so that only
    // one of the two is kept in memory. Each move changes two fields. Several threads may read the same instance at
    // the same time, so:
    // - the moves are made under a lock, which makes each of them one step for the other readers under the lock;
    // - a read outside the lock only ever returns data that it has seen itself: Value returns bytes that it found,
    //   and JsonValue returns a literal that it found after it found no bytes. Anything else goes to the lock.
    //   That covers the two common reads, Value when the bytes are there and JsonValue when only the literal is.
    //   An instance without a value does take the lock, because "no bytes and no literal" is also what a reader
    //   can see while two moves pass by;
    // - the bytes are volatile, and the move back to the literal writes the literal before it clears the bytes. On
    //   every platform (ARM64 included) a reader that finds the bytes cleared therefore finds that literal, or a
    //   newer state.
    // The setters take no lock, as before: changing a value while other threads read it is up to the caller.
    [NonSerialized]  // To prevent binary serialization from serializing this field
    private volatile byte[]? _parsedValue = null;

    // The lock is a private object and never the instance itself, which other code can lock too (SetAnnotation does).
    // It is created when the first move needs it, so an instance that is only deserialized and serialized has none.
    [NonSerialized]  // To prevent binary serialization from serializing this field
    private object? _valueLock;

    private object valueLock =>
        _valueLock ?? Interlocked.CompareExchange(ref _valueLock, new object(), null) ?? _valueLock!;

    /// <summary>
    /// Validates the JsonValue and updates the internal cached byte[] Value, releasing
    /// the data in JsonValue to save memory if it is exactly the canonical base64 encoding
    /// of the parsed bytes.
    /// </summary>
    protected internal override COVE? ValidateObjectValue(PocoValidationContext? context) =>
        getParsedValue(context, out _);

    // Gets the cached bytes, parsing the JsonValue first when it has not been parsed yet.
    private COVE? getParsedValue(PocoValidationContext? context, out byte[]? parsed)
    {
        // Bytes that were there at any moment are the value, also when a concurrent read of JsonValue
        // releases them right after, so this needs no lock.
        parsed = _parsedValue;
        if (parsed is not null) return null;

        object? jsonValue;

        lock (valueLock)
        {
            parsed = _parsedValue;
            if (parsed is not null) return null;

            jsonValue = base.JsonValue;
            if (jsonValue is null) return null;

            if (jsonValue is string unparsed && (parsed = doParse(unparsed)) is not null)
            {
                // Clear the string value to free up memory if we have successfully parsed the value,
                // but only if the string can be regenerated exactly from the bytes. The decoder is lenient
                // (e.g. it ignores whitespace), so a non-canonical literal must be kept as-is: validating
                // should never rewrite the data of the user.
                var releaseLiteral = string.Equals(Convert.ToBase64String(parsed), unparsed, System.StringComparison.Ordinal);

                _parsedValue = parsed;
                if (releaseLiteral) base.JsonValue = null;

                return null;
            }
        }

        // The error is built outside the lock, because that calls out to the validation context.
        return jsonValue is string invalid
            ? COVE.INVALID_BASE64_VALUE(context, invalid)
            : COVE.INCORRECT_LITERAL_VALUE_TYPE(context, jsonValue, this.TypeName);
    }

    // Value validates through ValidateObjectValue, as it always did, so that an override in a subclass decides
    // whether the value is valid. The bytes are then fetched separately, because a concurrent read of JsonValue
    // may have released them again in the meantime.
    private COVE? validateAndGetParsedValue(out byte[]? parsed)
    {
        parsed = null;
        if (ValidateObjectValue(null) is { } error) return error;

        _ = getParsedValue(null, out parsed);
        return null;
    }

    private bool retainedLiteralMatchesBytes(byte[] parsed) =>
        base.JsonValue is string literal && doParse(literal) is { } decoded && decoded.AsSpan().SequenceEqual(parsed);

    private static byte[]? doParse(string literal)
    {
        try
        {
            return Convert.FromBase64String(literal);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Checks whether the given literal is correctly formatted.
    /// </summary>
    public static bool IsValidValue(string value) => doParse(value) is not null;


    /// <summary>
    /// Constructs a Base64Binary instance from a string of base64-encoded data.
    /// </summary>
    public static Base64Binary FromBase64String(string base64Data) =>
        new() { JsonValue = base64Data };

    /// <summary>
    /// Constructs a Base64Binary instance from a string of human-readable text.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static Base64Binary FromText(string text) =>
        new(System.Text.Encoding.UTF8.GetBytes(text));


    /// <summary>
    /// Converts this binary to a Base64-encoded <see cref="P.String" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">The value of this binary is null,
    /// which is not valid for System strings.</exception>
    public P.String ToSystemString() => (P.String?)TryConvertToSystemTypeInternal() ??
                                        throw new InvalidOperationException("Value is null.");

    protected internal override Any? TryConvertToSystemTypeInternal() =>
        JsonValue is string s
        ? new P.String(s)
        : null;
}