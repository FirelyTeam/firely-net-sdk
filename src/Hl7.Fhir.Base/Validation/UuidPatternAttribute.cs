/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.ComponentModel.DataAnnotations;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

#nullable enable

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// Validates an Uuid value against the FHIR rules for Uuid.
    /// </summary>

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class UuidPatternAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
            value switch
            {
                null => ValidationResult.Success,
                string s when Uuid.IsValidValue(s) => ValidationResult.Success,
                string s => COVE.UUID_LITERAL_INVALID.AsResult(validationContext, s),
                _ => throw new ArgumentException($"{nameof(UuidPatternAttribute)} attributes can only be applied to string properties.")
            };
    }
}

#nullable restore