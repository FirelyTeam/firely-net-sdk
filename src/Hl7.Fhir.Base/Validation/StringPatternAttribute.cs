using Hl7.Fhir.Model;
using System;
using System.ComponentModel.DataAnnotations;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Validation;

#nullable enable

/// <summary>
/// Validates an Uri value against the FHIR rules for Uri.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class StringPatternAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value switch
        {
            null => ValidationResult.Success,
            string s when FhirString.IsValidValue(s) => ValidationResult.Success,
            string s => COVE.INVALID_STRING_LENGTH(validationContext, validationContext.MemberName!, s).AsResult(validationContext),
            _ => ValidationResult.Success // Will happen during deserialization calls, where the raw value is fed to the attribute validation logic.
        };
}