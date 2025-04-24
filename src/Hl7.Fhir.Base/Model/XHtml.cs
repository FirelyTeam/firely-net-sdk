/*
  Copyright (c) 2011-2012, HL7, Inc
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

using Hl7.Fhir.Utility;
using System;
using Hl7.Fhir.Validation;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml;
using P = Hl7.Fhir.ElementModel.Types;
using COVE=Hl7.Fhir.Validation.CodedValidationException;
#nullable enable

namespace Hl7.Fhir.Model;

/// <summary>
/// Helper functions to work with FHIR XHtml in narrative.
/// </summary>
public partial class XHtml
{
    /// <summary>
    /// Validates the JsonValue.
    /// </summary>
    protected internal override COVE? ValidateObjectValue(PocoValidationContext? context) =>
        JsonValue switch
        {
            null => null,
            string xml => ValidateXmlLiteral(xml, context),
            _ => COVE.INCORRECT_LITERAL_VALUE_TYPE(context, JsonValue, this.TypeName)
        };

    internal static COVE? ValidateXmlLiteral(string xml, PocoValidationContext? context)
    {
        return context?.NarrativeValidation switch
        {
            null => null,
            NarrativeValidationKind.None => null,
            NarrativeValidationKind.Xml => IsValidXml(xml, out var error) ? null : make(error, null),
            NarrativeValidationKind.FhirXhtml => IsValidNarrativeXhtml(xml, out var malformedXmlError,
                    out var invalidNarrativeErrors) ? null : make(malformedXmlError, invalidNarrativeErrors),
            var kind => throw new NotSupportedException($"Encountered unknown narrative validation kind '{kind}'.")
        };

        COVE? make(string? malformedXmlError, string[]? invalidNarrativeErrors) =>
            malformedXmlError is not null
                ? COVE.NARRATIVE_XML_IS_MALFORMED(context, malformedXmlError)
                : invalidNarrativeErrors?.Any() == true
                    ? COVE.NARRATIVE_XML_IS_INVALID(context, string.Join(", ", invalidNarrativeErrors))
                    : null;
    }

    /// <summary>
    /// Verifies the given string of XML against the FHIR narrative requirements from https://www.hl7.org/fhir/narrative.html.
    /// </summary>
    public static bool IsValidNarrativeXhtml(string text, out string? malformedXmlError, out string[] invalidNarrativeErrors)
    {
        try
        {
            var doc = SerializationUtil.XDocumentFromXmlText(text);
            malformedXmlError = null;
            invalidNarrativeErrors = SerializationUtil.RunFhirXhtmlSchemaValidation(doc);
            return !invalidNarrativeErrors.Any();
        }
        catch (FormatException fe)
        {
            malformedXmlError = fe.Message;
            invalidNarrativeErrors = [];
            return false;
        }
    }

    /// <summary>
    /// Validates whether the given string of Xml is well-formatted.
    /// </summary>
    public static bool IsValidXml(string value, out string? malformedXmlError)
    {
        try
        {
            using var reader = SerializationUtil.XmlReaderFromXmlText(value);
            while (reader.Read()) ;
            malformedXmlError = null;
            return true;
        }
        catch (XmlException xmlE)
        {
            malformedXmlError = xmlE.Message;
            return false;
        }
    }

    protected internal override P.Any? TryConvertToSystemTypeInternal() => Value is not null ? new P.String(Value) : null;
}