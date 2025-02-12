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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    static Code()
    {
        if (!typeof(T).IsEnum())
            throw new ArgumentException("T must be an enumerated type");
    }

    public override string TypeName => "code";

    public Code() : this(null) { }

    public Code(T? value)
    {
        Value = value;
    }

    protected override Type ObjectValueType => typeof(string);

    [NonSerialized] // To prevent binary serialization from serializing this field
    private T? _parsedValue = null;

    private bool tryGetParsedValue(out T? parsed)
    {
        parsed = _parsedValue;
        if (_parsedValue is not null || ObjectValue is null) return true;
        if (ObjectValue is not string unparsed) return false;
        if (EnumUtility.ParseLiteral<T>(unparsed) is not { } e) return false;

        parsed = e;
        return true;
    }

    public override object? ObjectValue
    {
        get
        {
            if (_parsedValue is not null && base.ObjectValue is null)
                base.ObjectValue = _parsedValue.GetLiteral();

            return base.ObjectValue;
        }
        set
        {
            base.ObjectValue = value;
            _parsedValue = null;
        }
    }


    // Primitive value of element
    [FhirElement("value", IsPrimitiveValue = true, XmlSerialization = XmlRepresentation.XmlAttr, InSummary = true, Order = 30)]
    [DataMember]
    new public T? Value
    {
        get
        {
            if (!tryGetParsedValue(out var value))
                throw new InvalidCastException($"Value '{ObjectValue}' of type {ObjectValue!.GetType()} is not a correct string literal for an Coded enum of type {typeof(T)}.");

            return value;
        }

        set
        {
            _parsedValue = value;
            base.ObjectValue = null;
            OnPropertyChanged("Value");
        }
    }


    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var baseResults = base.Validate(validationContext);
        if (baseResults.Any()) return baseResults; // Try to avoid duplicative errors.

        if (HasValidValue())
            return baseResults;

        var result = COVE.INVALID_CODED_VALUE(validationContext, ObjectValue, EnumUtility.GetName<T>()).AsResult(validationContext);
        return baseResults.Append(result);
    }

    /// <summary>
    /// Checks whether the given literal is one of the enum values for this T.
    /// </summary>
    public bool HasValidValue() => tryGetParsedValue(out _);

    /// <inheritdoc cref="HasValidValue"/>
    public new static bool IsValidValue(string value)
    {
        var code = new Code<T>() { ObjectValue = value };
        return code.HasValidValue();
    }

    /// <inheritdoc />
    public override IEnumerable<Coding> ToCodings() => [new(Value?.GetSystem(), Value?.GetLiteral())];

    protected internal override P.Any? TryConvertToSystemTypeInternal() =>
        Value is not null ? new P.Code(Value.GetSystem(), Value.GetLiteral()!, display: null, version: null) : null;

    protected internal override Base DeepCopyInternal()
    {
        var instance = new Code<T>();
        CopyToInternal(instance);
        return instance;
    }
}