/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

#nullable enable

namespace Hl7.Fhir.Introspection;

/// <summary>
/// Validates the type of a property against the allowed type choices.
/// </summary>
[CLSCompliant(false)]
[AttributeUsage(AttributeTargets.Property)]
public class AllowedTypesAttribute(params Type[] types) : ValidatingFhirModelAttribute
{
    /// <summary>
    /// The list of types that are allowed for the instance.
    /// </summary>
    public Type[] Types { get; set; } = types;

    /// <inheritdoc />
    public override IReadOnlyCollection<CodedValidationException> Validate(object? value, ValidationContext validationContext)
    {
        if (value is null) return [];

        IReadOnlyCollection<CodedValidationException> result = [];

        if (value is IReadOnlyCollection<Base> list)
        {
            foreach (var item in list)
            {
                result = validateValue(item, validationContext);
                if (result.Any()) break;
            }
        }
        else
        {
            result = validateValue(value, validationContext);
        }

        return result;
    }

    private IReadOnlyCollection<CodedValidationException> validateValue(object? item, ValidationContext context) =>
        item is null || Types.Any(t => t.IsInstanceOfType(item))
            ? []
            : [COVE.CHOICE_TYPE_NOT_ALLOWED(context, ModelInspector.GetClassMappingForType(item.GetType())?.Name ?? item.GetType().Name)];
}