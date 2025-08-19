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
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Validation;

/// <summary>
/// This validator uses the System.ComponentModel.DataAnnotations attributes to validate an instance,
/// but simulates Validator.ValidateObject(), to avoid using reflection and use the cached reflection
/// information on <see cref="ClassMapping"/> and <see cref="PropertyMapping"/>.
/// </summary>
public class FhirAttributeValidator : IPocoValidator
{
    public static readonly FhirAttributeValidator Default = new();

    /// <inheritdoc />
    public virtual IReadOnlyCollection<CodedValidationException> ValidateProperty(
        string name,
        object? propertyValue,
        PropertyMapping? propertyMapping,
        PocoValidationContext context)
    {
        if (propertyMapping?.NativeProperty is null || propertyMapping.IsPrimitive)
        {
            var serializedForm = propertyValue is Base b && b.Annotation<XmlRepresentationAnnotation>() is not null
                ? "attribute"
                : "element";
            return [CodedValidationException.UNKNOWN_ELEMENT(context, name, serializedForm)];
        }

        // check whether the value is assignable to the property, we'll complain in runAttributeValidation about other issues
        if (!propertyMapping.PropertyType.IsInstanceOfType(propertyValue))
        {
            return [
                CodedValidationException.FromTypes(propertyMapping.PropertyType, propertyValue, context),
                ..runAttributeValidation(propertyValue, propertyMapping.ValidationAttributes, context)
            ];
        }

        return runAttributeValidation(propertyValue, propertyMapping.ValidationAttributes, context);
    }

   /// <inheritdoc />
    public virtual IReadOnlyCollection<CodedValidationException> ValidateObject(Base instance, ClassMapping classMapping, PocoValidationContext context)
    {
        var errors = new List<CodedValidationException>();

        // For now, if we encounter a dynamic resource, we'll report that we have encountered an unknown
        // resource type. In a future version, users will able to register custom dynamic types that are
        // "known" to the validator, in which case those would not be reported here.
        if (instance is DynamicResource dr && !BaseFhirJsonDeserializer.IsUnnamedResourceMapping(classMapping))
            errors.Add(CodedValidationException.UNKNOWN_RESOURCE_TYPE(context, dr.DynamicTypeName ?? "(unnamed)"));

        // Make sure we detect missing values - go over all members that have cardinality constraints
        // and invoke those if there is no value (if there was a value, ValidateProperty will have been
        // called on it while deserializing the member).
        foreach (var propMapping in classMapping!.PropertyMappings)
        {
            var cardinality = propMapping.ValidationAttributes.OfType<CardinalityAttribute>().SingleOrDefault();
            if (cardinality is not null && cardinality.Min > 0)
            {
                var propValue = instance.TryGetValue(propMapping.Name, out var val) ? val : null;

                if (propValue is null || ReflectionHelper.IsRepeatingElement(propValue, out var list) && list.Count == 0)
                {
                    // Add the name of the property to the path, so we can display the correct name of the element,
                    // even if it does not really contain any values.
                    var nestedContext = context with { PathProducer = () => $"{context.PathProducer()}.{propMapping.Name}" };

                    errors.AddRange(runAttributeValidation(propValue, [cardinality], nestedContext));
                }
            }
        }

        // Validate the attributes on this instance itself
        errors.AddRange(runAttributeValidation(instance, classMapping.ValidationAttributes, context));

        // Now, run the object-level validation
        errors.AddRange(instance.ValidateInvariants(context));

        return errors;
    }

    private static IReadOnlyCollection<CodedValidationException> runAttributeValidation(
        object? candidateValue,
        ValidatingFhirModelAttribute[] attributes,
        PocoValidationContext validationContext) =>
        attributes.SelectMany(vfma => vfma.Validate(candidateValue, validationContext)).ToArray();
}