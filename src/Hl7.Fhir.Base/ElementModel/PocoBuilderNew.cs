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
internal class PocoBuilderNew(ModelInspector inspector, PocoBuilderSettings? settings = null)
{
    public PocoBuilderSettings? Settings { get; } = settings;

    /// <summary>
    /// Build a POCO from an <see cref="ITypedElement"/>.
    /// </summary>
    public Base BuildFrom(ITypedElement source)
    {
        if (source == null) throw Error.ArgumentNull(nameof(source));

        var mappings = ElementFactory.ForElement(source, null, inspector);
        return readFromElement(source, mappings);
    }

    private Base readFromElement(ITypedElement node, ElementFactory factory)
    {
        IDictionary<string, object> newInstance = buildNewInstance(factory);

        // Capture the instance type if this is a dynamic type.
        if(newInstance is IDynamicType dt)
            dt.DynamicTypeName = node.InstanceType;

        // Value is a kind of pseudo-property, so we need to handle it separately.
        // If this is a standard Fhir primitive, we need to convert the ITypedElement.Value
        // to the used ObjectValue, if not, just set the value immediately on the DynamicPrimitive.
        if (node.Value is { } value)
        {
            newInstance["value"] = newInstance is DynamicPrimitive ?
                value :
                convertTypedElementValue(value, node.InstanceType);
        }

        // Now, read the children
        foreach (var child in node.Children())
        {
            var childFactory = ElementFactory.ForElement(child, factory.ClassMapping, inspector);
            var convertedValue = readFromElement(child, childFactory);

            try
            {
                setOrAddProperty(child, newInstance, convertedValue, childFactory);
            }
            catch (InvalidCastException e)
            {
                // In case the InstanceType does not agree with the actual POCO type of the property, the
                // setOrAddProperty method will throw an InvalidCastException. In this case, we should salvage
                // the data we have so far, and put it in an annotation.
                // This will be fixed in https://github.com/FirelyTeam/firely-net-sdk/issues/2908.
                // For now, just throw.
                Console.WriteLine(e);
                throw;
            }
        }

        return (Base)newInstance;
    }

    private static IDictionary<string, object> buildNewInstance(ElementFactory factory)
    {
        return factory.MakeInstance() switch
        {
            IDictionary<string,object> b => b,
            _ => throw Error.InvalidOperation($"Class Factory for '{factory.ClassMapping.Name}' did not return a dictionary, which is required for " +
                        $"building up POCO's dynamically.")
        };
    }

    private void setOrAddProperty(ITypedElement node, IDictionary<string, object> target,
        Base convertedValue, ElementFactory factory)
    {
        // If this element *could* be repeating (either we don't know the definition, or it really is defined
        // to be a collection, then check to see if there are already items present.
        var couldBeCollection = (node.Definition is null && factory.ElementMapping is null)
                                || node.Definition?.IsCollection == true
                                || factory.ElementMapping?.IsCollection == true;
        var existing = couldBeCollection && target.TryGetValue(node.Name, out var existingValue) ? existingValue : null;

        // If there are, just add this new value.
        if (existing is IList list)
        {
            list.Add(convertedValue);
        }

        // If we already have a value, but it's not a list, we know we are now dealing with a list.
        // So, create a list, and add both the existing and the new value. Note that assigning a list to
        // that same property only works if this element is in the overflow and we did not know it was a list
        // before. In all other cases, the indexed assignment will fail.
        else if(existing is not null)
        {
            // Due to List<T> being invariant, we need to create a new list of the type of the property,
            // not of the instance.
            var newList =  factory.MakeList();
            newList.Add(existing);
            newList.Add(convertedValue);
            target[node.Name] = newList;
        }

        // No existing value, but we know it's a collection, so create a list and add the element.
        else if (node.Definition?.IsCollection == true || factory.ElementMapping?.IsCollection == true)
        {
            // Due to List<T> being invariant, we need to create a new list of the type of the property,
            // not of the instance.
            // Due to List<T> being invariant, we need to create a new list of the type of the property,
            // not of the instance.
            var newList = factory.MakeList();
            newList.Add(convertedValue);
            target[node.Name] = newList;
        }

        // No existing value, and not a list, just set the element.
        else
        {
            // Note that some exceptional primitive properties (like Extension.url and Element.id) are
            // represented in the POCO as .NET primitives, not as FHIR datatypes, so we need to get the value out.
            if (factory.ElementMapping?.IsPrimitive == true && convertedValue is PrimitiveType { ObjectValue: { } value })
                target[node.Name] = value;
            else
                target[node.Name] = convertedValue;
        }
    }

    /// <summary>
    /// Convert the value of a typed element to a value that can be set on a POCO property.
    /// </summary>
    private static object convertTypedElementValue(object value, string? instanceType)
    {
        return value switch
        {
            // Instants are converted to DateTimeOffset, and should by definition have a timezone in their
            // serialization, but if it does not, we'll use UTC.
            ET.DateTime inst when instanceType == "instant" => inst.ToDateTimeOffset(TimeSpan.Zero),

            // all "other" date/time types are just strings, since that is how the POCO's represent the
            // partial date/time types in ObjectValue.
            ET.DateTime => value.ToString()!,
            ET.Time => value.ToString()!,
            ET.Date => value.ToString()!,

            // Base64Binary is a string of base64 encoded data, and the POCO's use byte[] for this.
            string uuenc when instanceType == "base64Binary" => Convert.FromBase64String(uuenc),

            // All other primitives are one-on-one convertible to their .NET counterparts.
            _ => value
        };
    }


    private class ElementFactory
    {
        private static readonly string DYNAMIC_RESOURCE_TYPE_NAME = new DynamicResource().TypeName;
        private static readonly string DYNAMIC_DATATYPE_TYPE_NAME = new DynamicDataType().TypeName;
        private static readonly string DYNAMIC_PRIMITIVE_TYPE_NAME = new DynamicPrimitive().TypeName;

        private ElementFactory(ClassMapping classMapping, PropertyMapping? elementMapping, Func<IList> listFactory)
        {
            _listFactory = listFactory;
            ElementMapping = elementMapping;
            ClassMapping = classMapping;
        }

        private ElementFactory(ClassMapping classMapping, PropertyMapping? elementMapping)
        {
            _listFactory = classMapping.ListFactory;
            ElementMapping = elementMapping;
            ClassMapping = classMapping;
        }

        public object MakeInstance() => ClassMapping.Factory();
        public IList MakeList() => _listFactory();

        private readonly Func<IList> _listFactory;

        public PropertyMapping? ElementMapping { get; }
        public ClassMapping ClassMapping { get; }

        public static ElementFactory ForElement(ITypedElement node, ClassMapping? parentMapping, ModelInspector inspector)
        {
            var propertyMapping = parentMapping?.FindMappedElementByName(node.Name);
            var elementClassMapping = propertyMapping is not null
                ? getClassMapping(propertyMapping.ImplementingType, inspector)
                : null;

            // If the node is a code, wuse the more specific property type (a Code<T>) instead of the instance type.
            if (node.InstanceType == "code" && propertyMapping?.NativeProperty.PropertyType.IsConstructedGenericType == true)
                return new ElementFactory(elementClassMapping!, propertyMapping);

            // If the node is a BackboneElement, derive the actual type from the POCO since this will be the
            // actual concrete subclass of BackboneElement. As a fallback, just us a DynamicElement (uncommon).
            if (node.InstanceType is "BackboneElement" or "Element")
            {
                var backboneMapping = elementClassMapping ?? determineDynamicMapping(node, inspector);
                return new ElementFactory(backboneMapping, propertyMapping);
            }

            // Normal case: Resolve the instance type through the inspector.
            // For creating a list, normally we should use the ListFactory of the POCO property's class mapping,
            // but if that is unknown, use the ListFactory of the instance type instead.
            // The reason we need to do this is that the property may be a List of a more generic type than the instance,
            // and since List<T> is invariant, we need to create a List with the correct base class, as specified
            // in the POCO.
            if (node.InstanceType is { } instanceType && inspector.FindClassMapping(instanceType) is { } mapping)
                return new ElementFactory(mapping, propertyMapping, elementClassMapping?.ListFactory ?? mapping.ListFactory);

            // No instance type, guess what the best dynamic type is.
            return new ElementFactory(determineDynamicMapping(node, inspector), propertyMapping);
        }

        private static ClassMapping determineDynamicMapping(ITypedElement node, ModelInspector inspector)
        {
            // Ok, so the node does not have a type, or the type is not known. So, let's use one
            // of the applicable dynamic types. If the node has a resource name (but it was unknown),
            // we'll create a DynamicResource. If the node has a Value, it's a DynamicPrimitive,
            // otherwise it's a DynamicDataType.
            // Design question: there might be a "strict" option, where we will not create Dynamic types
            // for unknown types, but throw an error instead.
            if (node.Value is not null || (node.InstanceType is { } it && char.IsLower(it[0])))
                return determineBestPrimitiveMapping(node, inspector);

            if (node.Annotation<IResourceTypeSupplier>() is not null)
                return getClassMapping(DYNAMIC_RESOURCE_TYPE_NAME, inspector);

            return getClassMapping(DYNAMIC_DATATYPE_TYPE_NAME, inspector);
        }

        private static ClassMapping getClassMapping(string dynTypeName, ModelInspector inspector) =>
            inspector.FindClassMapping(dynTypeName) ??
            throw Error.InvalidOperation($"Cannot find ClassMapping for type '{dynTypeName}'.");

        private static ClassMapping getClassMapping(Type t, ModelInspector inspector) =>
            inspector.FindOrImportClassMapping(t) ??
            throw Error.InvalidOperation($"Cannot find ClassMapping for type '{t.Name}'.");

        private static ClassMapping getClassMapping<T>(ModelInspector inspector) =>
            getClassMapping(typeof(T), inspector);

        private static ClassMapping determineBestPrimitiveMapping(ITypedElement node, ModelInspector inspector)
        {
            return node.Value switch
            {
                ET.DateTime => getClassMapping<FhirDateTime>(inspector),
                ET.Date => getClassMapping<Date>(inspector),
                ET.Time => getClassMapping<Time>(inspector),
                decimal => getClassMapping<FhirDecimal>(inspector),
                bool => getClassMapping<FhirBoolean>(inspector),
                int => getClassMapping<Integer>(inspector),
                long => getClassMapping<Integer64>(inspector),
                string => getClassMapping<FhirString>(inspector),
                _ => getClassMapping(DYNAMIC_PRIMITIVE_TYPE_NAME, inspector)
            };
        }
    }
}