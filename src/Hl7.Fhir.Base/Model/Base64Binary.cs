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
            if (ValidateObjectValue(null) is {} error)
                throw error;

            return _parsedValue;
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
            if (_parsedValue is not null && base.JsonValue is null)
            {
                base.JsonValue = Convert.ToBase64String(_parsedValue);
                _parsedValue = null;    // Clear the parsed value to free up memory
            }

            return base.JsonValue;
        }
        set
        {
            base.JsonValue = value;
            _parsedValue = null;
        }
    }

    [NonSerialized]  // To prevent binary serialization from serializing this field
    private byte[]? _parsedValue = null;

    /// <summary>
    /// Validates the JsonValue and updates the internal cached byte[] Value, releasing
    /// the data in JsonValue to save memory.
    /// </summary>
    protected internal override COVE? ValidateObjectValue(PocoValidationContext? context)
    {
        if (_parsedValue is not null || base.JsonValue is null) return null;
        _parsedValue = null;

        if (base.JsonValue is not string unparsed)
            return COVE.INCORRECT_LITERAL_VALUE_TYPE(context, JsonValue, this.TypeName);

        _parsedValue = doParse(unparsed);

        // Clear the string value to free up memory if we have successfully parsed the value.
        if(_parsedValue is not null)
            base.JsonValue = null;

        return _parsedValue is null ? COVE.INVALID_BASE64_VALUE(context, unparsed) : null;
    }

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