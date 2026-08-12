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
using System;
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
        // An element is unknown when there is no mapping for it at all, or when its mapping was
        // fabricated on the fly by the deserializer for an element it did not recognize (an
        // "ad-hoc" mapping). Note that the latter can also happen on a custom class: custom
        // properties that are a member of their declaring class are known and validated normally,
        // but an unrecognized element on such a class gets an ad-hoc mapping too, and must still
        // be reported here.
        if (propertyMapping is null || propertyMapping.IsPrimitive || isAdHocMapping(propertyMapping))
        {
            var serializedForm = propertyValue is Base b && b.Annotation<XmlRepresentationAnnotation>() is not null
                ? "attribute"
                : "element";

            // If the unknown name is a case-insensitive near miss of a defined element, the reason
            // it is unknown is its incorrect casing - report the casing violation instead of the
            // generic unknown element. So, a name always produces at most one of these errors:
            // WRONG_CASED_ELEMENT when it nearly matches a defined element, UNKNOWN_ELEMENT when
            // it does not match anything at all.
            var declaringMapping = propertyMapping?.DeclaringClass
                ?? context.ModelInspector.FindOrImportClassMapping(context.ObjectInstance);
            if (declaringMapping?.TryFindElement(name) is { IsExactCase: false } nearMiss)
                return [CodedValidationException.WRONG_CASED_ELEMENT(context, name, nearMiss.CanonicalName)];

            return [CodedValidationException.UNKNOWN_ELEMENT(context, name, serializedForm)];
        }

        // The name - as encountered in the serialized form, or used as a key in the dictionary
        // interface - may differ from the element's defined name only by casing (for choice
        // elements: including the casing of the type suffix). The value is still validated against
        // the element it nearly matches, but the casing violation itself is reported as well.
        CodedValidationException[] caseErrors =
            !string.Equals(name, propertyMapping.Name, StringComparison.Ordinal)
            && propertyMapping.DeclaringClass.TryFindElement(name) is { IsExactCase: false } wrongCased
                ? [CodedValidationException.WRONG_CASED_ELEMENT(context, name, wrongCased.CanonicalName)]
                : [];

        // if context doesn't have MemberName, set it explicitly
        context.MemberName ??= propertyMapping.NativeProperty?.Name;

        // check whether the value is assignable to the property, we'll complain in runAttributeValidation about other issues
        if (!propertyMapping.PropertyType.IsInstanceOfType(propertyValue))
        {
            return [
                ..caseErrors,
                CodedValidationException.FromTypes(propertyMapping.PropertyType, propertyValue, context, propertyMapping.NativeProperty?.Name ?? propertyMapping.Name),
                ..runAttributeValidation(propertyValue, propertyMapping.ValidationAttributes, context)
            ];
        }

        if (caseErrors.Length == 0)
            return runAttributeValidation(propertyValue, propertyMapping.ValidationAttributes, context);

        return [.. caseErrors, .. runAttributeValidation(propertyValue, propertyMapping.ValidationAttributes, context)];
    }

   /// <inheritdoc />
    public virtual IReadOnlyCollection<CodedValidationException> ValidateObject(Base instance, ClassMapping classMapping, PocoValidationContext context)
    {
        // Validating an object is done for every object encountered while deserializing, and the
        // common case is that it produces no errors at all - so the list collecting them is only
        // created once there is something to collect.
        List<CodedValidationException>? errors = null;

        // If we encounter a dynamic resource that is not backed by a custom mapping registered
        // with the inspector, we'll report that we have encountered an unknown resource type.
        if (instance is DynamicResource dr
            && !BaseFhirJsonDeserializer.IsUnnamedResourceMapping(classMapping)
            && !isRegisteredCustomMapping(classMapping, context))
            (errors ??= []).Add(CodedValidationException.UNKNOWN_RESOURCE_TYPE(context, dr.DynamicTypeName ?? "(unnamed)"));

        // Make sure we detect missing values - go over all members that have cardinality constraints
        // and invoke those if there is no value (if there was a value, ValidateProperty will have been
        // called on it while deserializing the member). Which members those are is a property of the
        // class, so it is determined once (see ClassMapping.MandatoryElements) instead of per instance.
        foreach (var propMapping in classMapping!.MandatoryElements)
        {
            var propValue = instance.TryGetValue(propMapping.Name, out var val) ? val : null;

            if (propValue is null || ReflectionHelper.IsRepeatingElement(propValue, out var list) && list.Count == 0)
            {
                // Add the name of the property to the path, so we can display the correct name of the element,
                // even if it does not really contain any values.
                var nestedContext = context with { PathProducer = () => $"{context.PathProducer()}.{propMapping.Name}", MemberName = propMapping.NativeProperty?.Name };

                addRange(ref errors, runAttributeValidation(propValue, propMapping.MandatoryCardinality, nestedContext));
            }
        }

        // Validate the attributes on this instance itself
        addRange(ref errors, runAttributeValidation(instance, classMapping.ValidationAttributes, context));

        // Now, run the object-level validation
        addRange(ref errors, instance.ValidateInvariants(context));

        return errors ?? (IReadOnlyCollection<CodedValidationException>)[];
    }

    /// <remarks>Runs on every property and every object encountered while deserializing, where the
    /// common case is a handful of attributes that all pass, so this avoids allocating anything at all
    /// (no enumerators, no result collection) unless a validation actually fails.</remarks>
    private static IReadOnlyCollection<CodedValidationException> runAttributeValidation(
        object? candidateValue,
        ValidatingFhirModelAttribute[] attributes,
        PocoValidationContext validationContext)
    {
        List<CodedValidationException>? errors = null;

        foreach (var attribute in attributes)
            addRange(ref errors, attribute.Validate(candidateValue, validationContext));

        return errors ?? (IReadOnlyCollection<CodedValidationException>)[];
    }

    /// <summary>
    /// Appends <paramref name="source"/> to <paramref name="target"/>, creating the list on first use.
    /// </summary>
    /// <remarks>Validation of a property or object usually yields nothing, so the list collecting the
    /// errors is only allocated once there is actually something to collect.</remarks>
    private static void addRange(ref List<CodedValidationException>? target, IReadOnlyCollection<CodedValidationException> source)
    {
        if (source.Count == 0) return;

        (target ??= new List<CodedValidationException>(source.Count)).AddRange(source);
    }

    /// <summary>
    /// Whether this is an ad-hoc mapping, created on the fly (e.g. by the deserializer) for a
    /// property that is not part of its declaring class. Custom mappings that are a member of
    /// their declaring class represent known elements and should be validated normally.
    /// </summary>
    private static bool isAdHocMapping(PropertyMapping propertyMapping) =>
        propertyMapping.NativeProperty is null &&
        !ReferenceEquals(propertyMapping.DeclaringClass.FindMappedElementByName(propertyMapping.Name), propertyMapping);

    /// <summary>
    /// Whether this is a custom mapping that has been registered with the inspector, as opposed
    /// to an ad-hoc mapping created by the deserializer for an unknown resource type.
    /// </summary>
    private static bool isRegisteredCustomMapping(ClassMapping classMapping, PocoValidationContext context) =>
        classMapping.IsCustomMapping &&
        ReferenceEquals(context.ModelInspector.FindClassMapping(classMapping.Name), classMapping);
}