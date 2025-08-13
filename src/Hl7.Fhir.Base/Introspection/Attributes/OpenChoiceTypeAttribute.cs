using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Introspection;

/// <summary>
/// Validates the type of a property against the allowed type choices.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class OpenChoiceTypeAttribute : ValidatingFhirModelAttribute
{
    public override IReadOnlyCollection<CodedValidationException> Validate(object value, PocoValidationContext validationContext) => 
        new AllowedTypesAttribute(validationContext.ModelInspector.OpenTypes).Validate(value, validationContext);
}