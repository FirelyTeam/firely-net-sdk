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
    /// <summary>
    /// The value of the primitive, stored as an object. Will generally contain the same value as the
    /// `Value` property and allows the user to retrieve a primitive value regardless of actual type.
    /// </summary>
    /// <remarks>Both <c>Value</c> and <c>ObjectValue</c> may contain invalid values according to the
    /// primitive's official domain. E.g. <c>Value</c> is a <c>string</c> for <see cref="FhirDateTime"/>,
    /// and may contain illegally formatted values. Additionally, the deserializers will use this property
    /// to store the original serialized string form of the value in the wire format when a parsing error is
    /// encountered.</remarks>

    public virtual object? ObjectValue { get; set; }

    /// <inheritdoc/>
    public override string? ToString()
    {
        // The primitive can exist without a value (when there is an extension present)
        // so we need to be able to handle when there is no extension present
        return ObjectValue is null ? null : PrimitiveTypeConverter.ConvertTo<string>(ObjectValue);
    }

    /// <summary>
    /// Returns true if the primitive has any child elements (currently in FHIR this can
    /// be only the element id and zero or more extensions).
    /// </summary>
    public bool HasElements => ElementIdElement?.ObjectValue is not null || Extension?.Any() == true;

    protected internal abstract P.Any? TryConvertToSystemTypeInternal();

    /// <inheritdoc />
    bool P.IToSystemPrimitive.TryConvertToSystemType([NotNullWhen(true)] out P.Any? result)
    {
        result = TryConvertToSystemTypeInternal();
        return result is not null;
    }

    protected internal override IReadOnlyCollection<CodedValidationException> ValidateInvariants(PocoValidationContext validationContext) =>
        ValidateObjectValue(validationContext) is { } result ? [result] : [];

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
                Base64Binary { ObjectValue: { } b64 } => b64,
                { } prim => prim.ObjectValue
            };
        }
        catch (FormatException)
        {
            // If it fails, just return the unparsed contents
            return this.ObjectValue;
        }
    }
}