#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using ET = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel;

internal partial class NewPocoBuilder
{
    private static string getMappingTypeName(ClassMapping classMapping) =>
        ((IStructureDefinitionSummary)classMapping).TypeName;

    private static void raiseFormatError(string message, string location) =>
        throw Error.Format("While building a POCO: " + message, location);

    private static Base buildNewInstance(ClassMapping mapping, bool hasValue)
    {
        if (hasValue && !mapping.IsFhirPrimitive)
            return new DynamicPrimitive();

        if (mapping.NativeType.IsAbstract)
            return mapping.IsResource ? new DynamicResource() : new DynamicDataType();

        if (mapping.CreateInstance() is { } b) return b;

        throw Error.InvalidOperation($"Class Factory for '{mapping.Name}' did not return a " +
                                     $"Base, which is required for " +
                                     $"building up POCO's dynamically.");
    }

    private static void attachAnnotationsFrom(object node, Base newInstance)
    {
        // add a link back to the original node to persist its annotations on pocos
        if (node is IAnnotated annotated)
            newInstance.AddAnnotation(new TypedElementAnnotatedProvider(annotated));
    }

    private static void setDynamicTypeName(Base newInstance, string? explicitTypeName, ClassMapping classMapping)
    {
        if (newInstance is IDynamicType dynamicType)
            dynamicType.DynamicTypeName = explicitTypeName ?? getMappingTypeName(classMapping);
    }

    private void validateEnumLiteral(object originalValue, object storedValue, ClassMapping classMapping, string location)
    {
        if (settings?.AllowUnrecognizedEnums == false &&
            classMapping.EnumType is not null &&
            storedValue is string enumLiteral &&
            EnumUtility.ParseLiteral(enumLiteral, classMapping.EnumType) == null)
        {
            raiseFormatError(
                $"Literal '{originalValue}' is not a valid value for enumeration '{classMapping.EnumType.Name}'",
                location);
        }
    }

    private IList buildNewList(PropertyMapping? propertyMapping, Type elementType)
    {
        // For lists, we need to create a list of exactly the type that the property expects,
        // if we don't know the property type, we'll just create a list of Base, so any type
        // that we find will fit (it's going in the overflow anyway, so we can chose the
        // type of list to use).
        if (propertyMapping is null)
        {
            var elementMapping = getClassMapping(elementType);
            return elementMapping.CreateList();
        }

        var propertyClassMapping = getClassMapping(propertyMapping.ImplementingType);
        return propertyClassMapping.CreateList();
    }

    private ClassMapping getClassMapping(Type t) =>
        inspector.FindClassMapping(t) ??
        (ClassMapping.TryCreate(inspector, t, out var newMapping)
            ? newMapping
            : throw Error.InvalidOperation($"Cannot find ClassMapping for type '{t.Name}'."));

    private ClassMapping getClassMapping<T>() => getClassMapping(typeof(T));

    private void assignOrAddProperty(
        string propertyName,
        Base target,
        Base convertedValue,
        PropertyMapping? propertyMapping,
        bool isCollection,
        bool annotateAsChoice)
    {
        // Original inputs can contain more detailed type information than the Poco we're building now.
        // If we had no concrete information about what to build, we may default to Dynamic types and
        // need to retain that this value came from a choice element for correct roundtripping.
        if (annotateAsChoice)
            convertedValue.AddAnnotation(new ChoiceElementAnnotation());

        var existing = target.TryGetValue(propertyName, out var existingValue) ? existingValue : null;

        if (existing is IList list)
        {
            try
            {
                list.Add(convertedValue);
                return;
            }
            catch (ArgumentException)
            {
                throw new InvalidOperationException(
                    $"Cannot add element of type '{convertedValue.GetType()}' to property '{propertyName}' of type '{list.GetType()}'.");
            }
        }

        if (existing is not null)
        {
            var dynamicTypeHint = existing.GetType() != convertedValue.GetType() ? typeof(Base) : existing.GetType();
            var newList = buildNewList(propertyMapping, dynamicTypeHint);
            newList.Add(existing);
            newList.Add(convertedValue);

            try
            {
                target[propertyName] = newList;
            }
            catch (InvalidCastException)
            {
                throw new InvalidOperationException(
                    $"Cannot assign list of type '{newList.GetType()}' to property '{propertyName}' of type '{target.GetType()}'.");
            }

            return;
        }

        if (isCollection)
        {
            var newList = buildNewList(propertyMapping, convertedValue.GetType());
            newList.Add(convertedValue);
            target[propertyName] = newList;
            return;
        }

        try
        {
            if (propertyMapping?.IsPrimitive == true && convertedValue is PrimitiveType { JsonValue: { } value })
                target[propertyName] = value;
            else
                target[propertyName] = convertedValue;
        }
        catch (InvalidCastException)
        {
            var typeString = convertedValue is IDynamicType dynamicType ? dynamicType.DynamicTypeName : convertedValue.GetType().Name;
            throw Error.InvalidOperation($"Cannot assign data of type {typeString} to property '{propertyName}'.");
        }
    }

    /// <exception cref="NotSupportedException">The property type is not a Resource or DataType subclass.</exception>
    private static ClassMapping determineBestDynamicMappingForType(Type elementType, bool hasValue)
    {
        if (typeof(Resource).IsAssignableFrom(elementType))
            return ClassMapping.DynamicResource;
        if (typeof(PrimitiveType).IsAssignableFrom(elementType) || hasValue)
            return ClassMapping.DynamicPrimitive;
        if (typeof(DataType).IsAssignableFrom(elementType))
            return ClassMapping.DynamicDataType;

        throw new NotSupportedException($"Cannot determine dynamic type for abstract type '{elementType.Name}'.");
    }

    private static object normalizePrimitiveValueForPocoStorage(object value) => value switch
    {
        // Some POCO primitive JsonValue properties store the canonical string form.
        ET.DateTime => value.ToString()!,
        ET.Time => value.ToString()!,
        ET.Date => value.ToString()!,

        // Integer64 uses string in the POCOs.
        long l => new ET.Long(l).ToString(),

        _ => value
    };
}



