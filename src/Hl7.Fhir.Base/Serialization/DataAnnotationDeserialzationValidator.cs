/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hl7.Fhir.Serialization;


/// <summary>
/// This validator uses the System.ComponentModel.DataAnnotations attributes to validate an instance,
/// but simulates Validator.ValidateObject(), to avoid using reflection and use the cached reflection
/// information on <see cref="ClassMapping"/> and <see cref="PropertyMapping"/>.
/// </summary>
public class DataAnnotationDeserialzationValidator : IDeserializationValidator
{
    public static readonly DataAnnotationDeserialzationValidator Default = new();

    /// <inheritdoc />
    public virtual void ValidateProperty(object? propertyValue, in PropertyDeserializationContext context, out IReadOnlyCollection<CodedValidationException> reportedErrors)
    {
        var validationContext = new ValidationContext(context.ObjectInstance)
            .SetValidateRecursively(false)    // Don't go deeper - we've already validated the children because we're parsing bottom-up.
            .SetNarrativeValidationKind(context.NarrativeValidation)
            .SetPositionInfo(new PositionInfo((int)context.LineNumber, (int)context.LinePosition))
            .SetLocationProducer(context.PathStack.GetInstancePath);

        reportedErrors = runAttributeValidation(propertyValue, context.ElementMapping.ValidationAttributes, validationContext);
    }

    /// <inheritdoc />
    public virtual void ValidateInstance(Base instance, in InstanceDeserializationContext context,
        out IReadOnlyCollection<CodedValidationException> reportedErrors)
    {
        var validationContext = new ValidationContext(instance)
            .SetValidateRecursively(false)    // Don't go deeper - we've already validated the children because we're parsing bottom-up.
            .SetNarrativeValidationKind(context.NarrativeValidation)
            .SetPositionInfo(new PositionInfo((int)context.LineNumber, (int)context.LinePosition))
            .SetLocationProducer(context.PathStack.GetInstancePath);

        IEnumerable<CodedValidationException> errors = [];

        // Make sure we detect missing values - go over all members that have cardinality constraints
        // and invoke those if there is no value (if there was a value, ValidateProperty will have been
        // called on it while deserializing the member).
        foreach (var propMapping in context.InstanceMapping.PropertyMappings)
        {
            var cardinality = propMapping.ValidationAttributes.OfType<CardinalityAttribute>().SingleOrDefault();
            if (cardinality is not null && cardinality.Min > 0)
            {
                // Note that some Value accessors (for Code<T>.Value for example) can throw, but there are
                // no Cardinality constraints on those, so we don't have to worry about that now.
                var propValue = propMapping.GetValue(instance);

                if (propValue is null || ReflectionHelper.IsRepeatingElement(propValue, out var list) && list.Count == 0)
                {
                    // Add the name of the property to the path, so we can display the correct name of the element,
                    // even if it does not really contain any values.
                    var nestedContext = validationContext.IntoEmptyProperty(propMapping.Name);

                    errors = errors.Concat(runAttributeValidation(propValue, [cardinality], nestedContext));
                }
            }
        }

        // Validate the attributes on this instance itself
        errors = errors.Concat(runAttributeValidation(instance, context.InstanceMapping.ValidationAttributes, validationContext));

        // Now, just like Validator.Validate, run the IValidatableObject if applicable
        if (instance is IValidatableObject ivo)
        {
            var extraErrors = ivo.Validate(validationContext).ToList();
            if (extraErrors.Any(e => e != ValidationResult.Success))
            {
                var codedErrors = extraErrors.OfType<CodedValidationResult>().Select(cvr => cvr.ValidationException).ToList();
                if (codedErrors.Count != extraErrors.Count)
                    throw new InvalidOperationException($"IValidatableObject.Validates should return one or more {nameof(CodedValidationResult)}.");

                errors = errors.Concat(codedErrors);
            }
        }

        reportedErrors = errors.ToArray();
    }

    private static IEnumerable<CodedValidationException>? add(IEnumerable<CodedValidationException>? errors, IEnumerable<CodedValidationException>? moreErrors)
    {
        return moreErrors is null ?
            errors
            : errors is not null ? errors.Concat(moreErrors) : moreErrors;
    }

    private static IReadOnlyCollection<CodedValidationException> runAttributeValidation(
        object? candidateValue,
        ValidatingFhirModelAttribute[] attributes,
        ValidationContext validationContext)
    {
        return attributes.SelectMany(vfma => vfma.Validate(candidateValue, validationContext)).ToArray();
    }
}