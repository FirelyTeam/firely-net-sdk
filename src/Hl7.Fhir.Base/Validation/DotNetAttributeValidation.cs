/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable enable

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// Utility methods for invoking .NET's <see cref="ValidationAttribute"/>-based validation mechanism.
    /// </summary>
    public static class DotNetAttributeValidation
    {
        /// <summary>
        /// Validate and object and its members against any <see cref="ValidationAttribute" />s present. 
        /// Will throw when a validation error is encountered.
        /// </summary>
        /// <param name="value">The object to validate</param>
        /// <param name="recurse">Whether to validate the object recursively, by also validating the contents of each property of the object.</param>
        /// <param name="narrativeValidation">The kind of narrative validation to perform when validating <see cref="XHtml"/>.</param>
        public static void Validate(
            this Base value,
            bool recurse = false,
            NarrativeValidationKind narrativeValidation = NarrativeValidationKind.FhirXhtml) =>
                Validate((object)value, recurse, narrativeValidation);

        /// <inheritdoc cref="Validate(Base, bool, NarrativeValidationKind)"/>
        internal static void Validate(object value, bool recurse = false, NarrativeValidationKind narrativeValidation = NarrativeValidationKind.FhirXhtml)
        {
            var validationContext = buildContext(recurse, narrativeValidation, value);
            Validator.ValidateObject(value, validationContext, true);
        }

        /// <summary>
        /// Validate an object and its members against any <see cref="ValidationAttribute" />s present. 
        /// </summary>
        /// <remarks>If <paramref name="validationResults"/> is <c>null</c>, no errors will be returned.</remarks>
        public static bool TryValidate(this Base value, ICollection<ValidationResult>? validationResults = null, bool recurse = false, NarrativeValidationKind narrativeValidation = NarrativeValidationKind.FhirXhtml)
            => TryValidate((object)value, validationResults, recurse, narrativeValidation);

        /// <inheritdoc cref="TryValidate(Base, ICollection{ValidationResult}?, bool, NarrativeValidationKind)"/>
        internal static bool TryValidate(object value, ICollection<ValidationResult>? validationResults = null, bool recurse = false, NarrativeValidationKind narrativeValidation = NarrativeValidationKind.FhirXhtml)
        {
            var validationContext = buildContext(recurse, narrativeValidation, value);

            // Validate the object, also calling the validators on each child property.
            var results = validationResults ?? new List<ValidationResult>();
            return Validator.TryValidateObject((object)value, validationContext, results, validateAllProperties: true);
        }

        /// <inheritdoc cref="TryValidate(Base, ICollection{ValidationResult}?, bool, NarrativeValidationKind)"/>
        internal static bool TryValidate(object value, ValidationContext context, ICollection<ValidationResult>? validationResults)
        {
            // Validate the object, also calling the validators on each child property.
            var results = validationResults ?? new List<ValidationResult>();
            return Validator.TryValidateObject(value, context, results, validateAllProperties: true);
        }

        internal static ValidationContext IntoPath(this ValidationContext ctx, object instance, string nestedElementName)
        {
            var location = ctx.GetLocation();

            var newContext = new ValidationContext(instance, ctx.Items);

            if (location is not null)
                newContext.SetLocation($"{location}.{nestedElementName}");
            else
                newContext.SetLocation(nestedElementName);

            return newContext;
        }

        private static ValidationContext buildContext(bool recurse, NarrativeValidationKind kind, object instance)
        {
            ValidationContext newContext = new(instance);

            newContext.SetValidateRecursively(recurse);
            newContext.SetNarrativeValidationKind(kind);
            newContext.SetLocation(instance.GetType().Name);
            return newContext;
        }

    }

}

#nullable restore