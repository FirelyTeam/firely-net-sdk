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
                .SetLocation(context.Path);

            reportedErrors = runAttributeValidation(instance, context.ElementMapping.ValidationAttributes, validationContext);
        }

        /// <inheritdoc />
        public void ValidateInstance(object? instance, in InstanceDeserializationContext context, out CodedValidationException[]? reportedErrors)
        {
            var validationContext = new ValidationContext(instance ?? new object())
                .SetValidateRecursively(false)    // Don't go deeper - we've already validated the children because we're parsing bottom-up.
                .SetNarrativeValidationKind(NarrativeValidation)
                .SetPositionInfo(new PositionInfo((int)context.LineNumber, (int)context.LinePosition))
                .SetLocation(context.Path);

            reportedErrors = runAttributeValidation(instance, context.InstanceMapping.ValidationAttributes, validationContext);

            // Now, just like Validator.Validate, run the IValidatableObject if applicable
            if (instance is IValidatableObject ivo)
            {
                var extraErrors = ivo.Validate(validationContext).ToList();
                if (extraErrors is not null && extraErrors.Any(e => e != ValidationResult.Success))
                {
                    var codedErrors = extraErrors.OfType<CodedValidationResult>().Select(cvr => cvr.ValidationException);
                    if (codedErrors.Count() != extraErrors.Count)
                        throw new InvalidOperationException($"Validation attributes should return a {nameof(CodedValidationResult)}.");

                    reportedErrors = (reportedErrors is not null ? reportedErrors.Concat(codedErrors) : codedErrors).ToArray();
                }
            }
        }

        private CodedValidationException[]? runAttributeValidation(
            object? candidateValue,
            ValidationAttribute[] attributes,
            ValidationContext validationContext)
        {

            // Avoid allocation of a list for every validation until we really have something to report.
            List<CodedValidationException>? errors = null;

            foreach (var va in attributes)
            {
                if (va.GetValidationResult(candidateValue, validationContext) is object vr)
                {
                    if (vr is CodedValidationResult cvr)
                        addError(cvr.ValidationException);
                    else
                        throw new InvalidOperationException($"Validation attributes should return a {nameof(CodedValidationResult)}.");

                    void addError(CodedValidationException e)
                    {
                        if (errors is null) errors = new();
                        errors.Add(e);
                    }
                }
            }

            return errors?.ToArray();
        }
    }
}

#nullable restore