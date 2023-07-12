/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// This validator uses the System.ComponentModel.DataAnnotations attributes to validate an instance,
    /// but simulates Validator.ValidateObject(), to avoid using reflection and use the cached reflection
    /// information on <see cref="ClassMapping"/> and <see cref="PropertyMapping"/>.
    /// </summary>
    public class DataAnnotationDeserialzationValidator : IDeserializationValidator
    {
        public static readonly DataAnnotationDeserialzationValidator Default = new();

        /// <summary>
        /// For performance reasons, validation of Xhtml again the rules specified in the FHIR
        /// specification for Narrative (http://hl7.org/fhir/narrative.html#2.4.0) is turned off by
        /// default. Set this property to any other value than <see cref="NarrativeValidationKind.None"/>
        /// to perform validation.
        /// </summary>
        public NarrativeValidationKind NarrativeValidation { get; } = NarrativeValidationKind.None;

        public DataAnnotationDeserialzationValidator(
            NarrativeValidationKind narrativeValidation = NarrativeValidationKind.None)
        {
            NarrativeValidation = narrativeValidation;
        }

        /// <inheritdoc cref="IDeserializationValidator.ValidateProperty(ref object?, in PropertyDeserializationContext, out CodedValidationException[])"/>
        public void ValidateProperty(ref object? instance, in PropertyDeserializationContext context, out CodedValidationException[]? reportedErrors)
        {
            var validationContext = new ValidationContext(instance ?? new object())
                .SetValidateRecursively(false)    // Don't go deeper - we've already validated the children because we're parsing bottom-up.
                .SetNarrativeValidationKind(NarrativeValidation)
                .SetPositionInfo(new PositionInfo((int)context.LineNumber, (int)context.LinePosition))
                .SetLocation(context.PathStack);

            reportedErrors = runAttributeValidation(instance, context.ElementMapping.ValidationAttributes, validationContext);
        }

        /// <inheritdoc />
        public void ValidateInstance(object? instance, in InstanceDeserializationContext context, out CodedValidationException[]? reportedErrors)
        {
            var validationContext = new ValidationContext(instance ?? new object())
                .SetValidateRecursively(false)    // Don't go deeper - we've already validated the children because we're parsing bottom-up.
                .SetNarrativeValidationKind(NarrativeValidation)
                .SetPositionInfo(new PositionInfo((int)context.LineNumber, (int)context.LinePosition))
                .SetLocation(context.PathStack);

            if (instance is null)
            {
                reportedErrors = null;
                return;
            }

            IEnumerable<CodedValidationException>? errors = null;

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

                        errors = add(errors, runAttributeValidation(propValue, new[] { cardinality }, nestedContext));
                    }
                }
            }

            // Validate the attributes on this instance itself
            errors = add(errors, runAttributeValidation(instance, context.InstanceMapping.ValidationAttributes, validationContext));

            // Now, just like Validator.Validate, run the IValidatableObject if applicable
            if (instance is IValidatableObject ivo)
            {
                var extraErrors = ivo.Validate(validationContext).ToList();
                if (extraErrors is not null && extraErrors.Any(e => e != ValidationResult.Success))
                {
                    var codedErrors = extraErrors.OfType<CodedValidationResult>().Select(cvr => cvr.ValidationException);
                    if (codedErrors.Count() != extraErrors.Count)
                        throw new InvalidOperationException($"IValidatableObject.Validates should return one or more {nameof(CodedValidationResult)}.");

                    errors = add(errors, codedErrors);
                }
            }

            reportedErrors = errors?.ToArray();
            return;
        }

        private IEnumerable<CodedValidationException>? add(IEnumerable<CodedValidationException>? errors, IEnumerable<CodedValidationException>? moreErrors)
        {
            return moreErrors is null ?
                errors
                : errors is not null ? errors.Concat(moreErrors) : moreErrors;
        }

        private CodedValidationException[]? runAttributeValidation(
            object? candidateValue,
            ValidationAttribute[] attributes,
            ValidationContext validationContext)
        {

            // Avoid allocation of a list for every validation until we really have something to report.
            IEnumerable<CodedValidationException>? errors = null;

            foreach (var va in attributes)
            {
                if (va.GetValidationResult(candidateValue, validationContext) is object vr)
                {
                    if (vr is CodedValidationResult cvr)
                        errors = add(errors, new[] { cvr.ValidationException });
                    else
                        throw new InvalidOperationException($"Validation attributes should return a {nameof(CodedValidationResult)}.");
                }
            }

            return errors?.ToArray();
        }
    }
}

#nullable restore