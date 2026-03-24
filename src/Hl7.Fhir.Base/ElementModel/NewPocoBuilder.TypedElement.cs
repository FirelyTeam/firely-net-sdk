#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using ET = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel;

internal partial class NewPocoBuilder
{
    private Base readFromElement(ITypedElement node, ClassMapping classMapping)
    {
        var newInstance = buildNewInstance(classMapping, node.Value is { });
        attachAnnotationsFrom(node, newInstance);
        setDynamicTypeName(newInstance, node.InstanceType ?? node.Annotation<IResourceTypeSupplier>()?.ResourceType, classMapping);

        if (node.Value is { } value)
        {
            object objectValue;
            if (newInstance is DynamicPrimitive)
                objectValue = value;
            else if (node is PocoNode { Poco: IDynamicType } && value is string s && classMapping.PrimitiveValueProperty is not null)
                objectValue = PrimitiveTypeConverter.ConvertTo(s, classMapping.PrimitiveValueProperty.ImplementingType);
            else
                objectValue = normalizePrimitiveValueForPocoStorage(value);

            if (newInstance is PrimitiveType pt)
                pt.JsonValue = objectValue;
            else
                raiseFormatError($"{node.Name} is a primitive of type {value.GetType()}, but the target POCO is a {newInstance.GetType()}, " +
                                 $"which is not FHIR primitive.", node.Location);

            validateEnumLiteral(value, objectValue, classMapping, node.Location);
        }

        foreach (var child in node.Children())
        {
            var propertyMapping = classMapping.FindMappedElementByChoiceName(child.Name);

            if (propertyMapping is null && _settings?.IgnoreUnknownMembers == false)
                raiseFormatError($"Encountered unknown member '{child.Name}' while de-serializing", child.Location);

            var childClassMapping = classMappingForElement(child, propertyMapping);
            var convertedValue = readFromElement(child, childClassMapping);
            assignOrAddProperty(
                child.Name,
                newInstance,
                convertedValue,
                propertyMapping,
                child.Definition?.IsCollection == true || propertyMapping?.IsCollection == true,
                propertyMapping is null && child.Definition?.IsChoiceElement is true);
        }

        return newInstance;
    }

    private ClassMapping classMappingForElement(ITypedElement node, PropertyMapping? propertyMapping, Type? typeHint = null)
    {
        var propertyClassMapping = propertyMapping is not null
            ? getClassMapping(propertyMapping.ImplementingType)
            : null;

        // we're coming from a context where original PocoNode was built without necessary
        // type information - that would result in instanceType being wrong
        // we have type info now, we can use it to determine the type of the property
        if (node is PocoNode { Poco: IDynamicType } && propertyClassMapping is not null)
        {
            if (propertyClassMapping is { NativeType.IsAbstract: false })
                return propertyClassMapping;

            if (node.Name.Substring(propertyMapping!.Name.Length) is { Length: > 0 } choice && _inspector.FindClassMapping(choice) is { } cm)
                return cm;
        }

        if (node.InstanceType is { } instanceType)
        {
            if (instanceType == (propertyClassMapping is not null ? getMappingTypeName(propertyClassMapping) : null) ||
                (instanceType == "code" && propertyClassMapping?.IsCodeOfT is true))
                return propertyClassMapping!;

            if (!instanceType.StartsWith("Dynamic") && _inspector.FindClassMapping(instanceType) is { } mapping && typeof(Base).IsAssignableFrom(mapping.NativeType))
                return mapping;
        }
        else if (propertyClassMapping is { NativeType.IsAbstract: false, IsPrimitive: false })
            return propertyClassMapping;

        if (typeHint is not null)
            return getClassMapping(typeHint);

        if (propertyClassMapping is not null)
            return determineBestDynamicMappingForType(node, propertyClassMapping.NativeType);

        return determineBestDynamicMappingForElement(node);
    }

    /// <summary>
    /// Determine the "best" dynamic type, based on the abstract type of a POCO property.
    /// </summary>
    /// <exception cref="NotSupportedException">The POCO's property is not a Resource or DataType subclass.</exception>
    private ClassMapping determineBestDynamicMappingForType(ITypedElement node, Type elementType)
    {
        if (typeof(Resource).IsAssignableFrom(elementType))
            return ClassMapping.DynamicResource;
        if (typeof(PrimitiveType).IsAssignableFrom(elementType) || node.Value is { })
            return ClassMapping.DynamicPrimitive;
        if (typeof(DataType).IsAssignableFrom(elementType))
            return ClassMapping.DynamicDataType;

        throw new NotSupportedException($"Cannot determine dynamic type for abstract type '{elementType.Name}'.");
    }

    /// <summary>
    /// Determine the "best" dynamic type based on the actual contents of the ITypedElement.
    /// </summary>
    private ClassMapping determineBestDynamicMappingForElement(ITypedElement node)
    {
        if (node.Value is not null || (node.InstanceType is { } it && char.IsLower(it[0])))
            return DetermineBestPrimitiveMapping();

        if (node.Annotation<IResourceTypeSupplier>()?.ResourceType is not null || node.Definition?.IsResource is true)
            return ClassMapping.DynamicResource;

        return ClassMapping.DynamicDataType;

        ClassMapping DetermineBestPrimitiveMapping()
        {
            return node.Value switch
            {
                ET.DateTime => getClassMapping<FhirDateTime>(),
                string when node.InstanceType is "System.DateTime" => getClassMapping<FhirDateTime>(),
                ET.Date => getClassMapping<Date>(),
                string when node.InstanceType is "System.Date" => getClassMapping<Date>(),
                ET.Time => getClassMapping<Time>(),
                string when node.InstanceType is "System.Time" => getClassMapping<Time>(),
                decimal => getClassMapping<FhirDecimal>(),
                bool => getClassMapping<FhirBoolean>(),
                int => getClassMapping<Integer>(),
                long => getClassMapping<Integer64>(),
                _ => ClassMapping.DynamicPrimitive
            };
        }
    }
}



