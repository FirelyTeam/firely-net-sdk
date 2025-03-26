/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

#nullable enable

namespace Hl7.Fhir.Validation;

/// <summary>
/// Utility methods for invoking .NET's <see cref="ValidationAttribute"/>-based validation mechanism.
/// </summary>
public static class DotNetAttributeValidation
{
    /// <summary>
    /// Validate and object and its members against any <see cref="ValidationAttribute" />s present.
    /// Will throw when a validation error is encountered.
    /// </summary>
    /// <param name="poco">The POCO to validate</param>
    /// <param name="inspector">The model metadata to use for validation.</param>
    /// <param name="recurse">Whether to validate the object recursively, by also validating the contents of each property of the object.</param>
    /// <param name="narrativeValidation">The kind of narrative validation to perform when validating <see cref="XHtml"/>.</param>
    public static void Validate(
        this Base poco,
        bool recurse = false,
        NarrativeValidationKind narrativeValidation = NarrativeValidationKind.FhirXhtml,
        ModelInspector? inspector = null)
    {
        var validationContext = buildContext(poco, inspector, recurse, narrativeValidation);
        Validator.ValidateObject(poco, validationContext, true);
    }

    /// <summary>
    /// Validate an object and its members against any <see cref="ValidationAttribute" />s present.
    /// </summary>
    /// <param name="poco">The POCO to validate</param>
    /// <param name="inspector">The model metadata to use for validation.</param>
    /// <param name="recurse">Whether to validate the object recursively, by also validating the contents of each property of the object.</param>
    /// <param name="narrativeValidation">The kind of narrative validation to perform when validating <see cref="XHtml"/>.</param>
    /// <param name="validationResults">A collection to which any validation errors will be added.</param>
    /// <remarks>If <paramref name="validationResults"/> is <c>null</c>, no errors will be returned.</remarks>
    public static bool TryValidate(
        this Base poco,
        ICollection<ValidationResult>? validationResults = null,
        bool recurse = false,
        NarrativeValidationKind narrativeValidation = NarrativeValidationKind.FhirXhtml,
        ModelInspector? inspector = null)
    {
        var validationContext = buildContext(poco, inspector, recurse, narrativeValidation);

        // Validate the object, also calling the validators on each child property.
        var results = validationResults ?? [];
        return Validator.TryValidateObject(poco, validationContext, results, validateAllProperties: true);
    }

    internal static ValidationContext IntoPath(this ValidationContext ctx, Base poco, string nestedElementName)
    {
        var location = ctx.GetLocationProducer();

        var newContext = new ValidationContext(poco, ctx, ctx.Items);

        if (location is not null)
            newContext.SetLocationProducer(() => $"{location()}.{nestedElementName}");
        else
            newContext.SetLocationProducer(() => nestedElementName);

        return newContext;
    }

    /// <summary>
    /// This is very similar to IntoPath except that it doesn't walk into the actual property
    /// as the property does not exist, but we do need to set the membername so that the error message
    /// is created correctly identifies the location in the message (in the context of the location)
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="elementName"></param>
    /// <returns></returns>
    internal static ValidationContext IntoEmptyProperty(this ValidationContext ctx, string elementName) =>
        new(ctx.ObjectInstance, ctx, ctx.Items) { MemberName = elementName };

    private static ValidationContext buildContext(Base instance, ModelInspector? inspector, bool recurse, NarrativeValidationKind kind)
    {
        inspector ??= ModelInspector.ForType(instance.GetType());

        var services = new ServiceContainer();
        services.AddService(typeof(IPocoValidator), new FhirAttributeValidator());
        services.AddService(typeof(ModelInspector), inspector);

        var newContext = new ValidationContext(instance, services, null);

        //newContext.SetModelInspector(inspector);
        newContext.SetValidateRecursively(recurse);
        newContext.SetNarrativeValidationKind(kind);
        newContext.SetLocationProducer(() => instance.GetType().Name);
        return newContext;
    }
} 