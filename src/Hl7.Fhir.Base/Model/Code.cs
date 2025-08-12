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
using System.Text.RegularExpressions;
using P=Hl7.Fhir.ElementModel.Types;
using COVE=Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Model;

public partial class Code : ICoded
{
    /// <summary>
    /// Converts this Code to a <see cref="P.Code"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">The value of this code is null,
    /// which is not valid for System Codes.</exception>
    public P.Code ToSystemCode() =>
        (P.Code?)TryConvertToSystemTypeInternal() ??
        throw new InvalidOperationException("Value is null.");

    protected internal override P.Any? TryConvertToSystemTypeInternal() =>
        Value is not null
            ? new P.Code(system: null, code: Value, display: null, version: null)
            : null;

    /// <summary>
    /// Validates the JsonValue.
    /// </summary>
    protected internal override COVE? ValidateObjectValue(PocoValidationContext? context) =>
        JsonValue switch
        {
            null => null,
            string unparsed => IsValidValue(unparsed)
                ? null
                : COVE.LITERAL_INVALID(context, unparsed, this.TypeName),
            _ => COVE.INCORRECT_LITERAL_VALUE_TYPE(context, JsonValue, this.TypeName)
        };

    /// <summary>
    /// Checks whether the given literal is correctly formatted.
    /// </summary>
    public static bool IsValidValue(string value) => Regex.IsMatch(value, "^" + PATTERN + "$", RegexOptions.Singleline);

    /// <inheritdoc cref="ICoded.ToCodings"/>
    public virtual IReadOnlyCollection<Coding> ToCodings() => [new(system: null, code: Value)];

    /// <summary>
    /// The literal of the code value, which is the same as the <see cref="Value"/>.
    /// </summary>
    public virtual string? Literal => Value;

    /// <summary>
    /// The system of the code value, which is always null for a Code.
    /// </summary>
    public virtual string? System => null;
}