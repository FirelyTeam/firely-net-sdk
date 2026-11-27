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

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using COVE = Hl7.Fhir.Validation.CodedValidationException;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model;

/// <summary>
/// A <see cref="Code"/> that has a limited set of values and which <see cref="Code.Value"/> can therefore
/// be represented as an enumerated type.
/// </summary>
[Serializable]
[FhirType("codeOfT")]
[DataContract]
[System.Diagnostics.DebuggerDisplay(@"\{Value={Value}}")]
public class Code<T> : Code, INullableValue<T> where T : struct, Enum
{
    public override string TypeName => "code";

    public Code() : this(null) { }

    public Code(T? value)
    {
        Value = value;
    }

    // The cached value is a value and a flag instead of one nullable field, because several threads may read the
    // same instance at the same time and a nullable value is not written in one step. The value is written before
    // the flag, and only the setters clear the flag, so a reader that sees the flag also sees the value.
    [NonSerialized] // To prevent binary serialization from serializing this field
    private T _parsedValue;

    [NonSerialized] // To prevent binary serialization from serializing this field
    private volatile bool _hasParsedValue;

    public override object? JsonValue
    {
        get
        {
            if (_hasParsedValue && base.JsonValue is null)
                base.JsonValue = _parsedValue.GetLiteral();

            return base.JsonValue;
        }
        set
        {
            base.JsonValue = value;
            _hasParsedValue = false;
        }
    }


    // Primitive value of element
    [FhirElement("value", IsPrimitiveValue = true, XmlSerialization = XmlRepresentation.XmlAttr, InSummary = true, Order = 30)]
    [DataMember]
    new public T? Value
    {
        get
        {
            if (ValidateObjectValue(null) is { } error)
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

    /// <summary>
    /// Validates the JsonValue and updates the internal cached enum Value.
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

        if(string.IsNullOrWhiteSpace(unparsed))
            return COVE.LITERAL_INVALID(context, unparsed,  this.TypeName);

        if (doParse(unparsed) is not { } parsed)
            return COVE.INVALID_CODED_VALUE(context, unparsed, EnumUtility.GetName<T>());

        _parsedValue = parsed;
        _hasParsedValue = true;
        return null;
    }

    private static T? doParse(string literal) =>  EnumUtility.ParseLiteral<T>(literal);

    /// <summary>
    /// Checks whether the given literal is correctly formatted.
    /// </summary>
    public static new bool IsValidValue(string value) => doParse(value) is not null;

    /// <inheritdoc />
    public override IReadOnlyCollection<Coding> ToCodings() => [new(Value?.GetSystem(), Value?.GetLiteral())];

    /// <summary>
    /// The literal of the code value, taken from the enum that is in <see cref="Value"/>.
    /// </summary>
    public override string? Literal => Value?.GetLiteral();

    /// <summary>
    /// The system of the code value, taken from the enum that is in <see cref="Value"/>.
    /// </summary>
    public override string? System => Value?.GetSystem();

    protected internal override P.Any? TryConvertToSystemTypeInternal() =>
        Value is not null ? new P.Code(Value.GetSystem(), Value.GetLiteral()!, display: null, version: null) : null;

    protected internal override Base DeepCopyInternal()
    {
        var instance = new Code<T>();
        CopyToInternal(instance);
        return instance;
    }
}