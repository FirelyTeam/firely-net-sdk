#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;

namespace Hl7.Fhir.ElementModel;

internal partial class NewPocoBuilder
{
    private Base readFromElement(ISourceNode node, ClassMapping classMapping)
    {
        if (node is PocoNode { Poco: { } poco } &&
            classMapping.NativeType.IsInstanceOfType(poco) &&
            poco is not IDynamicType)
        {
            return poco;
        }

        var newInstance = buildNewInstance(classMapping, node.Text is not null);
        attachAnnotationsFrom(node, newInstance);
        setDynamicTypeName(newInstance, node.Annotation<IResourceTypeSupplier>()?.ResourceType, classMapping);

        if (node.Text is { } text)
        {
            var objectValue = newInstance is DynamicPrimitive
                ? text
                : convertSourceNodeValue(text, classMapping);

            if (newInstance is PrimitiveType pt)
                pt.JsonValue = objectValue;
            else
                raiseFormatError($"{node.Name} is a primitive with value '{text}', but the target POCO is a {newInstance.GetType()}, " +
                                 $"which is not FHIR primitive.", node.Location);

            validateEnumLiteral(text, objectValue, classMapping, node.Location);
        }

        foreach (var child in node.Children())
        {
            var propertyMapping = classMapping.FindMappedElementByChoiceName(child.Name);

            if (propertyMapping is null && settings?.IgnoreUnknownMembers == false)
                raiseFormatError($"Encountered unknown member '{child.Name}' while de-serializing", child.Location);

            var childClassMapping = classMappingForElement(child, propertyMapping);
            var convertedValue = readFromElement(child, childClassMapping);
            var propertyName = propertyMapping?.Name ?? child.Name;
            var annotateAsChoice = propertyMapping?.Choice == ChoiceType.DatatypeChoice &&
                                   propertyName != child.Name &&
                                   convertedValue is IDynamicType;

            assignOrAddProperty(
                propertyName,
                newInstance,
                convertedValue,
                propertyMapping,
                propertyMapping?.IsCollection == true,
                annotateAsChoice);
        }

        return newInstance;
    }

    private ClassMapping classMappingForElement(ISourceNode node, PropertyMapping? propertyMapping, Type? typeHint = null)
    {
        if (typeHint is not null)
            return getClassMapping(typeHint);

        if (node.Annotation<IResourceTypeSupplier>()?.ResourceType is { } resourceType)
            return inspector.FindClassMapping(resourceType) is { IsResource: true } resourceMapping
                ? resourceMapping
                : new ClassMapping(inspector, resourceType, typeof(DynamicResource));

        var propertyClassMapping = propertyMapping is not null
            ? getClassMapping(propertyMapping.GetInstantiableType())
            : null;

        if (propertyMapping?.Choice == ChoiceType.DatatypeChoice)
        {
            var choiceSuffix = getChoiceTypeSuffix(node, propertyMapping);
            if (!string.IsNullOrEmpty(choiceSuffix))
            {
                if (inspector.FindClassMapping(choiceSuffix) is { } choiceMapping && typeof(Base).IsAssignableFrom(choiceMapping.NativeType))
                    return choiceMapping;

                return new ClassMapping(inspector, choiceSuffix, determineBestDynamicMappingForElement(node).NativeType);
            }
        }

        if (propertyClassMapping is { NativeType.IsAbstract: false, IsPrimitive: false })
            return propertyClassMapping;

        if (propertyMapping?.IsPrimitive == true)
            return ClassMapping.DynamicPrimitive;

        if (propertyClassMapping is not null)
            return determineBestDynamicMappingForType(propertyClassMapping.NativeType, node.Text is not null);

        if (inspector.FindClassMapping(node.Name) is { } exactMapping && typeof(Base).IsAssignableFrom(exactMapping.NativeType))
            return exactMapping;

        return determineBestDynamicMappingForElement(node);
    }

    private static string getChoiceTypeSuffix(ISourceNode node, PropertyMapping propertyMapping) =>
        node.Name.Length > propertyMapping.Name.Length
            ? node.Name[propertyMapping.Name.Length..]
            : string.Empty;

    private static object convertSourceNodeValue(string text, ClassMapping classMapping)
    {
        if (classMapping.EnumType is not null || classMapping.PrimitiveValueProperty is not { } primitiveValueProperty)
            return text;

        var convertedValue = PrimitiveTypeConverter.ConvertTo(text, primitiveValueProperty.ImplementingType);
        return normalizePrimitiveValueForPocoStorage(convertedValue);
    }

    private ClassMapping determineBestDynamicMappingForElement(ISourceNode node)
    {
        if (node.Annotation<IResourceTypeSupplier>()?.ResourceType is not null)
            return ClassMapping.DynamicResource;

        if (inspector.FindClassMapping(node.Name) is { } exactMapping && typeof(Base).IsAssignableFrom(exactMapping.NativeType))
            return exactMapping;

        return node.Text is { }
            ? new ClassMapping(inspector, node.Name, typeof(DynamicPrimitive))
            : new ClassMapping(inspector, node.Name, typeof(DynamicDataType));
    }
}


