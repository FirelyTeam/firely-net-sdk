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
using P = Hl7.Fhir.ElementModel.Types;
using COVE=Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Model;

public partial class FhirUri : ICoded
{
    public FhirUri(Uri uri)
    {
        Value = uri.OriginalString;
    }

    /// <summary>
    /// Validates the JsonValue.
    /// </summary>
    protected internal override COVE? ValidateObjectValue(PocoValidationContext? context) =>
        JsonValue switch
        {
            null => null,
            string unparsed when IsValidValue(unparsed) => null,
            string unparsed => COVE.LITERAL_INVALID(context, unparsed, this.TypeName),
            _ => COVE.INCORRECT_LITERAL_VALUE_TYPE(context, JsonValue, this.TypeName)
        };

    /// <summary>
    /// Checks whether the given literal is correctly formatted.
    /// </summary>
    /// <remarks>Due to the way we use Urls in FHIR, some "valid" FHIR urls are
    /// actually no valid according to <see cref="Uri.IsWellFormedUriString(string?, UriKind)"/></remarks>
    public static bool IsValidValue(string value)
    {
        Uri uri;
        
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            uri = new Uri(value, UriKind.RelativeOrAbsolute);
        }
        catch
        {
            return false;
        }

        if (!uri.IsAbsoluteUri) return true;

        var uris = uri.ToString();

        if (uris.StartsWith("urn:oid:") && !Oid.IsValidValue(uris))
            return false;
        if (uris.StartsWith("urn:uuid:") && !Uuid.IsValidValue(uris))
            return false;

        return true;
    }

    /// <summary>
    /// Converts this FhirUri to a <see cref="P.String" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">The Value of this FhirUri is null,
    /// which is not valid for System strings.</exception>
    public P.String ToSystemString() =>
        (P.String?)TryConvertToSystemTypeInternal() ?? throw new InvalidOperationException("Value is null.");

    protected internal override P.Any? TryConvertToSystemTypeInternal() => Value is not null ? new P.String(Value) : null;

    public IReadOnlyCollection<Coding> ToCodings() => [new(null, Value)];
}