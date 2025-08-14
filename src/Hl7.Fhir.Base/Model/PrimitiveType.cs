/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Serialization;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model;

public partial class PrimitiveType : P.IToSystemPrimitive
{
    /// <inheritdoc cref="JsonValue"/>
    [Obsolete("The underlying values used by ObjectValue for base64, instant and integer64 have changed, and these will now contain a string instead, to align with the FHIR Json specification for these types. We have renamed this property to JsonValue to reflect this.")]
    public object? ObjectValue { get => JsonValue; set => JsonValue = value; }

    /// <summary>
    /// The value of the primitive, as used in Json. This means a string, except for FhirBoolean (bool),
    /// Integer/PositiveInt/UnsignedInt (int) and FhirDecimal (decimal).
    /// </summary>
    /// <remarks>JsonValue may contain incorrect data, as deserializers will use this property to store
    /// the original serialized value as-is, and parsers will serialize this value as-is to allow for
    /// roundtripping (as much as possible) of serialized forms.</remarks>
    public virtual object? JsonValue { get; set; }

    /// <inheritdoc/>
    public override string? ToString()
    {
        // The primitive can exist without a value (when there is an extension present)
        // so we need to be able to handle when there is no extension present
        return JsonValue is null ? null : PrimitiveTypeConverter.ConvertTo<string>(JsonValue);
    }

    /// <summary>
    /// Returns true if the primitive has any child elements (currently in FHIR this can
    /// be only the element id and zero or more extensions).
    /// </summary>
    public bool HasElements => ElementIdElement?.JsonValue is not null || Extension.Any();

    protected internal abstract P.Any? TryConvertToSystemTypeInternal();

    /// <inheritdoc />
    bool P.IToSystemPrimitive.TryConvertToSystemType([NotNullWhen(true)] out P.Any? result)
    {
        result = TryConvertToSystemTypeInternal();
        return result is not null;
    }

    protected internal override IReadOnlyCollection<CodedValidationException> ValidateInvariants(
        PocoValidationContext validationContext)
    {
        IReadOnlyCollection<CodedValidationException> baseResults = [];
        
        // for now, there is only one invariant on base: value cannot be null if there are no other children.
        // this is an optimization. If we ever add more invariants to base, we should revise this.
        if (this.JsonValue is null) 
            baseResults = base.ValidateInvariants(validationContext);
        return ValidateObjectValue(validationContext) is { } result ? [..baseResults, result] : baseResults;
    }

    /// <summary>
    /// Validates the JsonValue. Some subclasses will also, as a side-effect, update
    /// their internal cache if parsing and validating is expensive.
    /// </summary>
    protected internal abstract CodedValidationException? ValidateObjectValue(PocoValidationContext? validationContext);

    public bool HasValidValue() => ValidateObjectValue(null) is null;

    internal object? ToITypedElementValue()
    {
        try
        {
            return this switch
            {
                Instant { Value: { } ins } => P.DateTime.FromDateTimeOffset(ins),
                Time { Value: { } time } => P.Time.Parse(time),
                Date { Value: { } dt } => P.Date.Parse(dt),
                FhirDateTime { Value: { } fdt } => P.DateTime.Parse(fdt),
                Integer fint => fint.Value,
                Integer64 fint64 => fint64.Value,
                PositiveInt pint => pint.Value,
                UnsignedInt unsint => unsint.Value,
                Base64Binary { JsonValue: { } b64 } => b64,
                { } prim => prim.JsonValue
            };
        }
        catch (FormatException)
        {
            // If it fails, just return the unparsed contents
            return this.JsonValue;
        }
    }
}