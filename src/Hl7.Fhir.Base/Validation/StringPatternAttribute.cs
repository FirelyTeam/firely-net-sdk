using Hl7.Fhir.Model;
using System;
using System.ComponentModel.DataAnnotations;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

namespace Hl7.Fhir.Validation;

#nullable enable

/// <summary>
/// Validates an Uri value against the FHIR rules for Uri.
/// </summary>
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public class StringPatternAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value switch
        {
            null => ValidationResult.Success,
            string s when FhirString.IsValidValue(s) => ValidationResult.Success,
            string s => COVE.INVALID_STRING_LENGTH(validationContext, validationContext.MemberName!, s).AsResult(validationContext),
            _ => throw new ArgumentException($"{nameof(StringPatternAttribute)} attributes can only be applied to string properties.")
        };
}