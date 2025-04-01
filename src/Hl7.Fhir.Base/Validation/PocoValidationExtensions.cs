/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Validation;

/// <summary>
/// Extension methods on POCOs to invoke validation.
/// </summary>
public static class PocoValidationExtensions
{
    /// <summary>
    /// Validate an object and its members against any <see cref="ValidationAttribute" />s present.
    /// </summary>
    /// <param name="poco">The POCO to validate</param>
    /// <param name="inspector">The model metadata to use for validation.</param>
    /// <param name="narrativeValidation">The kind of narrative validation to perform when validating <see cref="XHtml"/>.</param>
    /// <param name="validator"></param>
    public static IReadOnlyCollection<CodedValidationException> Validate(
        this Base poco,
        NarrativeValidationKind narrativeValidation = NarrativeValidationKind.FhirXhtml,
        ModelInspector? inspector = null, IPocoValidator? validator = null)
    {
        inspector ??= ModelInspector.ForType(poco.GetType());
        validator ??= new FhirAttributeValidator();
        var validationContext = buildContext(poco, null!, narrativeValidation);

        return doObjectValidation(poco, inspector, validationContext, validator);
    }

    private static PocoValidationContext buildContext(Base instance, ModelInspector inspector, NarrativeValidationKind kind)
    {
        var newContext = new PocoValidationContext(instance, inspector, producer, 0, 0, kind) { ValidateObjectOnly = false };
        return newContext;

        string producer() => instance.TypeName;
    }

    private static IReadOnlyCollection<CodedValidationException> doObjectValidation(Base value, ModelInspector inspector, PocoValidationContext validationContext, IPocoValidator validator)
    {
        var errors = new List<CodedValidationException>();

        var classMapping = inspector.FindClassMapping(value.GetType());
        if (classMapping is null) return [];

        // Step 1: Validate the object properties.
        foreach (var (name,propValue) in value.EnumerateElements())
        {
            var propMapping = classMapping.FindMappedElementByName(name);
            var childContext = validationContext.IntoPath(name);
            errors.AddRange(validator.ValidateProperty(name, propValue, propMapping, childContext));

            if (!validationContext.ValidateObjectOnly)
                errors.AddRange(doNestedValidation(childContext, name, propValue, validator));
        }

        // Step 2: Validate the object itself.
        errors.AddRange(validator.ValidateObject(value, classMapping, validationContext));

        return errors;
    }

    private static IReadOnlyCollection<CodedValidationException> doNestedValidation(PocoValidationContext context, string propName, object value, IPocoValidator validator)
    {
        var errors = new List<CodedValidationException>();

        switch (value)
        {
            case IList<Base> list:
                {
                    foreach (var element in list)
                    {
                        var result = doObjectValidation(element, context.ModelInspector, context, validator);
                        if (result.Any()) return result;
                    }

                    break;
                }
            case Base b:
                {
                    var nestedContext = context.IntoPath(propName);
                    return doObjectValidation(b, context.ModelInspector, nestedContext, validator);
                }
        }

        return [];
    }

    internal static Func<string> IntoPath(this Func<string> parent, string propName) => () => $"{parent()}.{propName}";
    internal static PocoValidationContext IntoPath(this PocoValidationContext parent, string propName) =>
        parent with { PathProducer = parent.PathProducer.IntoPath(propName) };
}