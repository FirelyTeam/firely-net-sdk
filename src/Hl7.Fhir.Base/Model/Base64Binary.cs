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
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using P=Hl7.Fhir.ElementModel.Types;

#nullable enable

namespace Hl7.Fhir.Model;

public partial class Base64Binary
{
    /// <summary>
    /// Constructs a Base64Binary instance from a string of base64-encoded data.
    /// </summary>
    public static Base64Binary FromBase64String(string base64Data) =>
        new() { ObjectValue = base64Data };

    /// <summary>
    /// Constructs a Base64Binary instance from a string of human-readable text.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static Base64Binary FromText(string text) =>
        new(System.Text.Encoding.UTF8.GetBytes(text));

    [NonSerialized]  // To prevent binary serialization from serializing this field
    private byte[]? _parsedValue = null;

    protected override void OnObjectValueChanged()
    {
        _parsedValue = null;
        base.OnObjectValueChanged();
    }

    public partial byte[]? Value
    {
        get
        {
            if (_parsedValue is null && ObjectValue is not null)
            {
                if(ObjectValue is string base64data)
                    _parsedValue = Convert.FromBase64String(base64data);
                else
                {
                    throw new InvalidCastException($"Cannot convert value '{ObjectValue}' of type {ObjectValue.GetType()} to a byte array.");
                }
            }

            return _parsedValue;
        }

        set
        {
            ObjectValue = value is null ? null : Convert.ToBase64String(value);
            OnPropertyChanged("Value");
        }
    }

    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var baseResults = base.Validate(validationContext);

        if (HasValidValue())
            return baseResults;

        var result = CodedValidationException.INVALID_BASE64_VALUE(validationContext, ObjectValue).AsResult(validationContext);
        return baseResults.Append(result);
    }

    public bool HasValidValue()
    {
        try
        {
            // There is no TryDecode(), so this is all we can do.
            _ = Value;
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Checks whether the given literal is correctly formatted.
    /// </summary>
    public static bool IsValidValue(string value)
    {
        try
        {
            var b64 = FromBase64String(value);
            _ = b64.Value;  // triggers b64 decoding
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Converts this binary to a Base64-encoded <see cref="P.String" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">The value of this binary is null,
    /// which is not valid for System strings.</exception>
    public P.String ToSystemString() => (P.String?)TryConvertToSystemTypeInternal() ??
                                        throw new InvalidOperationException("Value is null.");

    protected internal override Any? TryConvertToSystemTypeInternal() =>
        Value is not null
        ? new P.String(Convert.ToBase64String(Value))
        : null;
}