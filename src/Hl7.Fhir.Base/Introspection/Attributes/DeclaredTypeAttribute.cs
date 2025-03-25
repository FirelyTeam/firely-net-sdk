/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Introspection;

/// <summary>
/// Used to indicate that the type for this property in the POCO
/// does not represent the type in the FHIR specification, but rather the type given
/// in the constructor to this attribute.
/// </summary>
[CLSCompliant(false)]
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class DeclaredTypeAttribute(Type t) : ValidatingFhirModelAttribute
{
    public Type Type { get; set; } = t;

    public override IReadOnlyCollection<CodedValidationException> Validate(object? value, ValidationContext validationContext)
    {
        if (value.IsValidValueForDeclaredType(Type))
        {
            return [];
        }

        return [CodedValidationException.FromTypes(Type, value, validationContext)];
    }
}