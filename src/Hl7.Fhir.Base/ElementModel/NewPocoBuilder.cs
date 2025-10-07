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
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    public Base BuildFrom(ITypedElement source, Type? typeHint = null)
    {
        if (source == null) throw Error.ArgumentNull(nameof(source));

        var classMapping = classMappingForElement(source, null, typeHint);
        return readFromElement(source, classMapping);
    }

    private Base readFromElement(ITypedElement node, ClassMapping classMapping)
    {
        var newInstance = buildNewInstance(classMapping, node.Value is { });

        // add a link back to TypedElement to persist it's annotations on pocos
        // this is specifically for backwards compatibility with many implementations of ITypedElement wrappers that implement their own annotations
        // Base will then check for this annotation and call the original TypedElement.Annotations()
        if(node is IAnnotated an)
            newInstance.AddAnnotation(new TypedElementAnnotatedProvider(an));

        // Capture the instance type if this is a dynamic type.
        if (newInstance is IDynamicType dt)
            dt.DynamicTypeName = node.InstanceType ?? node.Annotation<IResourceTypeSupplier>()?.ResourceType ?? classMapping.GetTypeName();

        // Value is a kind of pseudo-property, so we need to handle it separately.
        // If this is a standard Fhir primitive, we need to convert the ITypedElement.Value
        // to the used ObjectValue, if not, just set the value immediately on the DynamicPrimitive.
        if (node.Value is { } value)
        {
            object objectValue;
            if (newInstance is DynamicPrimitive)
                objectValue = value;
            
            // the ITypedElement is a PocoNode built with no information about the Poco, whether built with ModelInspector.Base,
            // or representing a custom resource - it will be using DynamicPrimitive to store the values typed as in serialization source.
            // With numeric values the JsonValue will be already good enough, but with strings it can represent FhirDateTime, FhirUri etc.
            // now that we have ClassMapping, we can check what is the expected primitive type and convert the string value accordingly
            else if (node is PocoNode { Poco: IDynamicType } && value is string s && classMapping.PrimitiveValueProperty is not null)
                objectValue = PrimitiveTypeConverter.ConvertTo(s, classMapping.PrimitiveValueProperty.ImplementingType);
            else
                objectValue = convertTypedElementValue(value);

            if (newInstance is PrimitiveType pt)
                pt.JsonValue = objectValue;
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
            var propertyMapping = classMapping.FindMappedElementByChoiceName(child.Name);
            
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

    private static Base buildNewInstance(ClassMapping mapping, bool hasValue)
    {
        if (hasValue && !mapping.IsFhirPrimitive)
            return new DynamicPrimitive();
        
        if (mapping.NativeType.IsAbstract) 
            return mapping.IsResource ? new DynamicResource() : new DynamicDataType();
        
        if (mapping.CreateInstance() is Base b) return b;

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
            return elementMapping.CreateList();
        }

        var propertyClassMapping = getClassMapping(propertyMapping.ImplementingType);
        return propertyClassMapping.CreateList();
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
            
            if (node.Name.Substring(propertyMapping!.Name.Length) is { Length: > 0} choice && inspector.FindClassMapping(choice) is {} cm)
                return cm;
        }
        
        // If we have a concrete instanceType, and it's not the same as the property type, we need to
        // check if we have a mapping for it. If we do, we can use that.
        // Note that this is not the same as the "best" mapping, which is determined below.
        // We "purposefully" create the suboptimal mapping anyway so our instance type is preserved.
        if (node.InstanceType is { } instanceType)
        {
            if (instanceType == propertyClassMapping?.GetTypeName() || (instanceType == "code" && propertyClassMapping?.IsCodeOfT is true))
                return propertyClassMapping; // propertyClassMapping matches the instanceType, we can safely use that
            
            // try to get mapping for instanceType, but only if we're not in a dynamic context
            if (!instanceType.StartsWith("Dynamic") && inspector.FindClassMapping(instanceType) is { } mapping && typeof(Base).IsAssignableFrom(mapping.NativeType))
                return mapping;
        }

        // Normal case, we have a property mapping, and it's not abstract, so we can use the actual
        // type used by the POCO. The "IsPrimitive" check is a bit of a hack, and is there to avoid
        // us coming up with .NET string mappings for Extension.url and Element.id. This can go when
        // we have solved https://github.com/FirelyTeam/firely-net-sdk/issues/2963.

        // Note the else here, since we never want to return the propertyClassMapping if we have an
        // instanceType which does not correspond to that mapping
        else if (propertyClassMapping is { NativeType.IsAbstract: false, IsPrimitive: false })
            return propertyClassMapping;

        // We don't know the type, but we know the type being requested
        if (typeHint is not null)
            return getClassMapping(typeHint);
        
        // No useable concrete type in the property, nor in the instance type, so we need to create
        // one of our dynamic flavours. If we do have an abstract type of the property, we can use that
        // as a hint.
        if (propertyClassMapping is not null)
            return determineBestDynamicMappingForType(node, propertyClassMapping.NativeType);

        // Failing all that, guess what the best dynamic type is based on the instance data.
        return determineBestDynamicMappingForElement(node);
    }

    /// <summary>
    /// Determine the "best" dynamic type, based on the abstract type of a POCO property.
    /// </summary>
    /// <exception cref="NotSupportedException">The POCO's property is not a Resource or DataType
    /// subclass.</exception>
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
            return determineBestPrimitiveMapping();

        if (node.Annotation<IResourceTypeSupplier>()?.ResourceType is not null || node.Definition?.IsResource is true)
            return ClassMapping.DynamicResource;

        return ClassMapping.DynamicDataType;

        // Instead of just picking a DynamicPrimitive, we can try to pick the best primitive type
        // based on the ITypedElement's value.
        ClassMapping determineBestPrimitiveMapping()
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
                // when TypedElement was built on a SourceNode without type information, string backed types would
                // be unrecognizable from actual string data, so let's default to DynamicPrimitive
                // string => getClassMapping<FhirString>(),
                _ => ClassMapping.DynamicPrimitive
            };
        }
    }

    private ClassMapping getClassMapping(string dynTypeName) =>
        inspector.FindClassMapping(dynTypeName) ??
        throw Error.InvalidOperation($"Cannot find ClassMapping for type '{dynTypeName}'.");

    private ClassMapping getClassMapping(Type t) =>
        inspector.FindClassMapping(t) ?? 
        (ClassMapping.TryCreate(inspector, t, out var newMapping) 
            ? newMapping 
            : throw Error.InvalidOperation($"Cannot find ClassMapping for type '{t.Name}'."));

    private ClassMapping getClassMapping<T>() => getClassMapping(typeof(T));

    private void setOrAddProperty(ITypedElement node, Base target,
        Base convertedValue, PropertyMapping? propertyMapping)
    {
        // Original ITypedElement had more detailed information about the type of data it represents than the Poco we're building now.
        // If we had no information of what to build, we will default to using Dynamic types to represent it.
        // In this case we will annotate the Poco element with information that it is a Choice type.
        // Then serializers will check for it to determine whether it should append the choice type to the property name when serializing it.
        // It will allow us to serialize the Poco back to xml/json correctly, as well as build properly typed Poco
        // when using PocoNode implementing ITypedElement/ISourceNode to call ToPoco<T>()
        // Covered by tests PocoNodeSerializationRoundtrip.CanConvertCircularPocoNode and https://github.com/FirelyTeam/firely-net-sdk/issues/3278
        if(propertyMapping is null && node.Definition?.IsChoiceElement is true)
            convertedValue.AddAnnotation(new ChoiceElementAnnotation());
        
        var existing = target.TryGetValue(node.Name, out var existingValue) ? existingValue : null;

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
        if (existing is not null)
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
            if (propertyMapping?.IsPrimitive == true && convertedValue is PrimitiveType { JsonValue: { } value })
                target[node.Name] = value;
            else
                target[node.Name] = convertedValue;
        }
        catch (InvalidCastException)
        {
            var typeString = convertedValue is IDynamicType it ? it.DynamicTypeName : convertedValue.GetType().Name;
            throw Error.InvalidOperation($"Cannot assign data of type {typeString} to property '{node.Name}'.");
        }
    }

    /// <summary>
    /// Convert the value of a typed element to a value that can be set on a POCO property.
    /// </summary>
    private static object convertTypedElementValue(object value)
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