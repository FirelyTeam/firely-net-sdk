/*
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using ET = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel;

/// <summary>
/// Traverses an <see cref="ITypedElement"/> tree and constructs a POCO from it.
/// </summary>
/// <param name="inspector">The inspector providing the necessary metadata about the FHIR POCO classes
/// <param name="settings">Configuration for building the POCO.</param>
/// used in the construction.</param>
internal class NewPocoBuilder(ModelInspector inspector, PocoBuilderSettings? settings = null)
{
    /// <summary>
    /// Build a POCO from an <see cref="ITypedElement"/>.
    /// </summary>
    public Base BuildFrom(ITypedElement source)
    {
        if (source == null) throw Error.ArgumentNull(nameof(source));

        var classMapping = classMappingForElement(source, null);
        return readFromElement(source, classMapping);
    }

    private Base readFromElement(ITypedElement node, ClassMapping classMapping)
    {
        var newInstance = buildNewInstance(classMapping);

        // Capture the instance type if this is a dynamic type.
        if(newInstance is IDynamicType dt)
            dt.DynamicTypeName = node.InstanceType;

        // Value is a kind of pseudo-property, so we need to handle it separately.
        // If this is a standard Fhir primitive, we need to convert the ITypedElement.Value
        // to the used ObjectValue, if not, just set the value immediately on the DynamicPrimitive.
        if (node.Value is { } value)
        {
            var objectValue = newInstance is DynamicPrimitive ?
                value :
                convertTypedElementValue(value, node.InstanceType);

            if(newInstance is PrimitiveType pt)
                pt.ObjectValue = objectValue;
            else
                raiseFormatError($"{node.Name} is a primitive of type {value.GetType()}, but the target POCO is a {newInstance.GetType()}, " +
                                 $"which is not FHIR primitive.", node.Location);

            if (settings?.AllowUnrecognizedEnums == false &&
                classMapping.EnumType is not null &&
                objectValue is string enumLiteral)
            {
                // Backwards-compatible check for enums. Although our POCOs accept strings rather
                // than enum values, this check is still useful for catching typos in the data and may
                // be used by older code.
                if (EnumUtility.ParseLiteral(enumLiteral, classMapping.EnumType) == null)
                    raiseFormatError(
                        $"Literal '{value}' is not a valid value for enumeration '{classMapping.EnumType.Name}'",
                        node.Location);
            }
        }

        // Now, read the children
        foreach (var child in node.Children())
        {
            var propertyMapping = classMapping.FindMappedElementByName(child.Name);

            if (propertyMapping is null && settings?.IgnoreUnknownMembers == false)
                raiseFormatError($"Encountered unknown member '{child.Name}' while de-serializing", child.Location);

            var childClassMapping = classMappingForElement(child, propertyMapping);
            var convertedValue = readFromElement(child, childClassMapping);

            // In case the convertedValue does not agree with the actual POCO type of the property, this
            // method will throw an InvalidCastException. Later, we could salvage
            // the data we have so far, and put it in an annotation.
            // This will be fixed in https://github.com/FirelyTeam/firely-net-sdk/issues/2908.
            setOrAddProperty(child, newInstance, convertedValue, propertyMapping);
        }

        return newInstance;
    }

    private static void raiseFormatError(string message, string location)
    {
        throw Error.Format("While building a POCO: " + message, location);
    }

    private static Base buildNewInstance(ClassMapping mapping)
    {
        if (mapping.Factory() is Base b) return b;

        throw Error.InvalidOperation($"Class Factory for '{mapping.Name}' did not return a " +
                       $"Base, which is required for " +
                        $"building up POCO's dynamically.");
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
            return elementMapping.ListFactory();
        }

        var propertyClassMapping = getClassMapping(propertyMapping.ImplementingType);
        return propertyClassMapping.ListFactory() ?? new List<Base>();
    }

    private ClassMapping classMappingForElement(ITypedElement node, PropertyMapping? propertyMapping)
    {
        var propertyClassMapping = propertyMapping is not null
            ? getClassMapping(propertyMapping.ImplementingType)
            : null;

        // Normal case, we have a property mapping, and it's not abstract, so we can use the actual
        // type used by the POCO. The "IsPrimitive" check is a bit of a hack, and is there to avoid
        // us coming up with .NET string mappings for Extension.url and Element.id. This can go when
        // we have solved https://github.com/FirelyTeam/firely-net-sdk/issues/2963.
        if (propertyClassMapping is { NativeType.IsAbstract: false, IsPrimitive: false })
            return propertyClassMapping;

        // Otherwise, let's use the ITypedElement's instance type.
        if (node.InstanceType is { } instanceType &&
            inspector.FindClassMapping(instanceType) is { NativeType.IsAbstract: false } mapping)
            return mapping;

        // No useable concrete type in the property, nor in the instance type, so we need to create
        // one of our dynamic flavours. If we do have an abstract type of the property, we can use that
        // as a hint.
        if(propertyClassMapping is not null)
            return determineBestDynamicMappingForType(propertyClassMapping.NativeType);

        // Failing all that, guess what the best dynamic type is based on the instance data.
        return determineBestDynamicMappingForElement(node);
    }

    private static readonly string DYNAMIC_RESOURCE_TYPE_NAME = new DynamicResource().TypeName;
    private static readonly string DYNAMIC_DATATYPE_TYPE_NAME = new DynamicDataType().TypeName;
    private static readonly string DYNAMIC_PRIMITIVE_TYPE_NAME = new DynamicPrimitive().TypeName;

    /// <summary>
    /// Determine the "best" dynamic type, based on the abstract type of a POCO property.
    /// </summary>
    /// <exception cref="NotSupportedException">The POCO's property is not a Resource or DataType
    /// subclass.</exception>
    private ClassMapping determineBestDynamicMappingForType(Type elementType)
    {
        if(typeof(Resource).IsAssignableFrom(elementType))
            return getClassMapping(DYNAMIC_RESOURCE_TYPE_NAME);
        if(typeof(PrimitiveType).IsAssignableFrom(elementType))
            return getClassMapping(DYNAMIC_PRIMITIVE_TYPE_NAME);
        if(typeof(DataType).IsAssignableFrom(elementType))
            return getClassMapping(DYNAMIC_DATATYPE_TYPE_NAME);

        throw new NotSupportedException($"Cannot determine dynamic type for abstract type '{elementType.Name}'.");
    }

    /// <summary>
    /// Determine the "best" dynamic type based on the actual contents of the ITypedElement.
    /// </summary>
    private ClassMapping determineBestDynamicMappingForElement(ITypedElement node)
    {
        if (node.Value is not null || (node.InstanceType is { } it && char.IsLower(it[0])))
            return determineBestPrimitiveMapping();

        if (node.Annotation<IResourceTypeSupplier>() is not null)
            return getClassMapping(DYNAMIC_RESOURCE_TYPE_NAME);

        return getClassMapping(DYNAMIC_DATATYPE_TYPE_NAME);

        // Instead of just picking a DynamicPrimitive, we can try to pick the best primitive type
        // based on the ITypedElement's value.
        ClassMapping determineBestPrimitiveMapping()
        {
            return node.Value switch
            {
                ET.DateTime => getClassMapping<FhirDateTime>(),
                ET.Date => getClassMapping<Date>(),
                ET.Time => getClassMapping<Time>(),
                decimal => getClassMapping<FhirDecimal>(),
                bool => getClassMapping<FhirBoolean>(),
                int => getClassMapping<Integer>(),
                long => getClassMapping<Integer64>(),
                string => getClassMapping<FhirString>(),
                _ => getClassMapping(DYNAMIC_PRIMITIVE_TYPE_NAME)
            };
        }
    }

    private ClassMapping getClassMapping(string dynTypeName) =>
        inspector.FindClassMapping(dynTypeName) ??
        throw Error.InvalidOperation($"Cannot find ClassMapping for type '{dynTypeName}'.");

    private ClassMapping getClassMapping(Type t) =>
        inspector.FindOrImportClassMapping(t) ??
        throw Error.InvalidOperation($"Cannot find ClassMapping for type '{t.Name}'.");

    private ClassMapping getClassMapping<T>() => getClassMapping(typeof(T));

    private void setOrAddProperty(ITypedElement node, Base target,
        Base convertedValue, PropertyMapping? propertyMapping)
    {
        // If this element *could* be repeating (either we don't know the definition, or it really is defined
        // to be a collection, then check to see if there are already items present.
        var couldBeCollection = (node.Definition is null && propertyMapping is null)
                                || node.Definition?.IsCollection == true
                                || propertyMapping?.IsCollection == true;
        var existing = couldBeCollection && target.TryGetValue(node.Name, out var existingValue) ? existingValue : null;

        // If there are, just add this new value.
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
                    $"Cannot add element of type '{convertedValue.GetType()}' to property '{node.Name}' of type '{list.GetType()}'.");
            }
        }

        // If we already have a value, but it's not a list, we know we are now dealing with a list.
        // So, create a list, and add both the existing and the new value. Note that assigning a list to
        // that same property only works if this element is in the overflow and we did not know it was a list
        // before. In all other cases, the indexed assignment will fail.
        if(existing is not null)
        {
            var dynamicTypeHint = existing.GetType() != convertedValue.GetType() ? typeof(Base) : existing.GetType();
            var newList = buildNewList(propertyMapping, dynamicTypeHint);
            newList.Add(existing);
            newList.Add(convertedValue);

            try
            {
                target[node.Name] = newList;
            }
            catch (InvalidCastException)
            {
                throw new InvalidOperationException(
                    $"Cannot assign list of type '{newList.GetType()}' to property '{node.Name}' of type '{target.GetType()}'.");
            }

            return;
        }

        // No existing value, but we know it's a collection, so create a list and add the element.
        if (node.Definition?.IsCollection == true || propertyMapping?.IsCollection == true)
        {
            var newList = buildNewList(propertyMapping, convertedValue.GetType());
            newList.Add(convertedValue);

            // This should always work, so I am not catching InvalidCastException here.
            target[node.Name] = newList;
            return;
        }

        // No existing value, and not a list, just set the element.
        // Note that some exceptional primitive properties (like Extension.url and Element.id) are
        // represented in the POCO as .NET primitives, not as FHIR datatypes, so we need to get the value out.
        try
        {
            if (propertyMapping?.IsPrimitive == true && convertedValue is PrimitiveType { ObjectValue: { } value })
                target[node.Name] = value;
            else
                target[node.Name] = convertedValue;
        }
        catch (InvalidCastException)
        {
            throw Error.InvalidOperation($"Cannot assign data of type {convertedValue.GetType()} to to property '{node.Name}'.");
        }
    }

    /// <summary>
    /// Convert the value of a typed element to a value that can be set on a POCO property.
    /// </summary>
    private static object convertTypedElementValue(object value, string? instanceType)
    {
        return value switch
        {
            // Some ITypedElement date/time values are strings in the POCO's ObjectValue.
            ET.DateTime => value.ToString()!,
            ET.Time => value.ToString()!,
            ET.Date => value.ToString()!,

            // Integer64 uses string in the POCOs
            long l => new ET.Long(l).ToString(),

            // All other primitives are one-on-one convertible to their .NET counterparts.
            _ => value
        };
    }
}