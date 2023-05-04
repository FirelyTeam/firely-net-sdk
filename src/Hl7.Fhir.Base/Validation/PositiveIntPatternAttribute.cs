﻿/* 
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
    /// Validates an PositiveInt value against the FHIR rules for positiveInt.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class PostiveIntPatternAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
            value switch
            {
                null => ValidationResult.Success,
                int i when PositiveInt.IsValidValue(i.ToString()) => ValidationResult.Success,
                int i => COVE.POSITIVEINT_LITERAL_INVALID.AsResult(validationContext, i),
                _ => throw new ArgumentException($"{nameof(PostiveIntPatternAttribute)} attributes can only be applied to string properties.")
            };
    }
}

#nullable restore