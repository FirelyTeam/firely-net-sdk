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
    /// Validates a datetime value against the FHIR rules for time.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class TimePatternAttribute : ValidationAttribute
    {
        /// <inheritdoc/>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
            value switch
            {
                null => ValidationResult.Success,
                string s when Time.IsValidValue(s) => ValidationResult.Success,
                string s => COVE.TIME_LITERAL_INVALID.AsResult(validationContext, s),
                _ => throw new ArgumentException($"{nameof(TimePatternAttribute)} attributes can only be applied to string properties.")
            };
    }
}

#nullable restore