#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using System;
using ET = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel;

internal partial class NewPocoBuilder
{
    private Base readFromElement(ITypedElement node, ClassMapping classMapping)
    {
        var newInstance = buildNewInstance(classMapping, node.Value is { });

        // add a link back to TypedElement to persist its annotations on pocos.
        // This is specifically for backwards compatibility with many implementations of ITypedElement
        // wrappers that implement their own annotations. Base will then check for this annotation and
        // call the original TypedElement.Annotations().
        attachAnnotationsFrom(node, newInstance);

        // Capture the instance type if this is a dynamic type.
        setDynamicTypeName(newInstance, node.InstanceType ?? node.Annotation<IResourceTypeSupplier>()?.ResourceType, classMapping);

        // Value is a kind of pseudo-property, so we need to handle it separately.
        // If this is a standard Fhir primitive, we need to convert the ITypedElement.Value
        // to the used ObjectValue, if not, just set the value immediately on the DynamicPrimitive.
        if (node.Value is { } value)
        {
            object objectValue;
            if (newInstance is DynamicPrimitive)
                objectValue = value;
            
            // The ITypedElement is a PocoNode built with no information about the Poco — whether built
            // with ModelInspector.Base or representing a custom resource — so it uses DynamicPrimitive
            // to store values typed as in the serialization source. With numeric values the JsonValue
            // will already be correct, but with strings it can represent FhirDateTime, FhirUri, etc.
            // Now that we have the ClassMapping, convert the string to the expected primitive type.
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

        // Now, read the children
        foreach (var child in node.Children())
        {
            var propertyMapping = classMapping.FindMappedElementByChoiceName(child.Name);

            if (propertyMapping is null && settings?.IgnoreUnknownMembers == false)
                raiseFormatError($"Encountered unknown member '{child.Name}' while de-serializing", child.Location);

            var childClassMapping = classMappingForElement(child, propertyMapping);
            var convertedValue = readFromElement(child, childClassMapping);

            // In case the convertedValue does not agree with the actual POCO type of the property, this
            // method will throw an InvalidCastException. Later, we could salvage the data we have so far
            // and put it in an annotation. This will be fixed in
            // https://github.com/FirelyTeam/firely-net-sdk/issues/2908.
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
            ? getClassMapping(propertyMapping.GetInstantiableType())
            : null;

        // we're coming from a context where original PocoNode was built without necessary
        // type information - that would result in instanceType being wrong
        // we have type info now, we can use it to determine the type of the property
        if (node is PocoNode { Poco: IDynamicType } && propertyClassMapping is not null)
        {
            if (propertyClassMapping is { NativeType.IsAbstract: false })
                return propertyClassMapping;

            if (node.Name.Substring(propertyMapping!.Name.Length) is { Length: > 0 } choice && inspector.FindClassMapping(choice) is { } cm)
                return cm;
        }

        // If we have a concrete instanceType, and it's not the same as the property type, we need to
        // check if we have a mapping for it. If we do, we can use that.
        // Note that this is not the same as the "best" mapping, which is determined below.
        // We "purposefully" create the suboptimal mapping anyway so our instance type is preserved.
        if (node.InstanceType is { } instanceType)
        {
            if (instanceType == (propertyClassMapping is not null ? getMappingTypeName(propertyClassMapping) : null) ||
                (instanceType == "code" && propertyClassMapping?.IsCodeOfT is true))
                return propertyClassMapping!; // propertyClassMapping matches the instanceType, we can safely use that

            // try to get mapping for instanceType, but only if we're not in a dynamic context
            if (!instanceType.StartsWith("Dynamic") && inspector.FindClassMapping(instanceType) is { } mapping && typeof(Base).IsAssignableFrom(mapping.NativeType))
                return mapping;
        }

        // Normal case: we have a property mapping and it's not abstract, so we can use the actual
        // type used by the POCO. The "IsPrimitive" check avoids picking up .NET string mappings for
        // Extension.url and Element.id. This can go when
        // https://github.com/FirelyTeam/firely-net-sdk/issues/2963 is solved.
        //
        // Note the else here: we never return the propertyClassMapping when we have an instanceType
        // that does not correspond to that mapping.
        else if (propertyClassMapping is { NativeType.IsAbstract: false, IsPrimitive: false })
            return propertyClassMapping;

        // We don't know the type, but we know the type being requested.
        if (typeHint is not null)
            return getClassMapping(typeHint);

        // No usable concrete type in the property, nor in the instance type, so create one of our
        // dynamic flavours. If we do have an abstract type on the property, use that as a hint.
        if (propertyClassMapping is not null)
            return determineBestDynamicMappingForType(propertyClassMapping.NativeType, node.Value is { });

        // Failing all that, guess the best dynamic type based on the instance data.
        return determineBestDynamicMappingForElement(node);
    }

    /// <summary>
    /// Determines whether the type described by this summary is a primitive: in FHIR, those are the types
    /// with a "value" child, which is represented as an attribute in Xml.
    /// </summary>
    private static bool isPrimitiveType(IStructureDefinitionSummary summary)
    {
        foreach (var element in summary.GetElements())
        {
            if (element.ElementName == "value" && element.Representation == XmlRepresentation.XmlAttr)
                return true;
        }

        return false;
    }

    /// <summary>
    /// In FHIR, primitive types are the only types whose name starts with a lowercase letter, so this is
    /// used as a heuristic to recognize them when we have no definition to go on. Note that types can also
    /// be identified by their canonical url (as is the case for logical models), which would incorrectly
    /// trip this heuristic - those are excluded here.
    /// </summary>
    private static bool isPrimitiveTypeName(string typeName) =>
        char.IsLower(typeName[0]) && !new Canonical(typeName).IsAbsolute;

    /// <summary>
    /// Determine the "best" dynamic type based on the actual contents of the ITypedElement.
    /// </summary>
    private ClassMapping determineBestDynamicMappingForElement(ITypedElement node)
    {
        if (node.Value is not null)
            return DetermineBestPrimitiveMapping();

        // When the definition tells us the type of this element, use it to decide primitive versus complex -
        // that beats guessing based on the shape of the type's name (which is all we can do below).
        if (node.Definition?.Type is [IStructureDefinitionSummary summary])
            return isPrimitiveType(summary)
                ? DetermineBestPrimitiveMapping()
                : isResource(summary.IsResource) ? ClassMapping.DynamicResource : ClassMapping.DynamicDataType;

        if (node.InstanceType is { } it && isPrimitiveTypeName(it))
            return DetermineBestPrimitiveMapping();

        return isResource(false) ? ClassMapping.DynamicResource : ClassMapping.DynamicDataType;

        bool isResource(bool typeIsResource) =>
            typeIsResource
            || node.Annotation<IResourceTypeSupplier>()?.ResourceType is not null
            || node.Definition?.IsResource is true;

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



