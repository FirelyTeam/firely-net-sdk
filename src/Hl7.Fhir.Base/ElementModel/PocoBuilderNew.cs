/*
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using ET = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Serialization;

internal class PocoBuilderNew(ModelInspector inspector)
{
    /// <summary>
    /// Build a POCO from an <see cref="ITypedElement"/>.
    /// </summary>
    public Base BuildFrom(ITypedElement source)
    {
        if (source == null) throw Error.ArgumentNull(nameof(source));

        return readFromElement(source);
    }

    private static readonly string DYNAMIC_RESOURCE_TYPE_NAME = new DynamicResource().TypeName;
    private static readonly string DYNAMIC_DATATYPE_TYPE_NAME = new DynamicDataType().TypeName;
    private static readonly string DYNAMIC_PRIMITIVE_TYPE_NAME = new DynamicPrimitive().TypeName;

    private Base readFromElement(ITypedElement node, ClassMapping? backboneClass=null)
    {
        // The classMapping we need to use is either the one provided by the node, or the default Dynamic one.
        // Design note: This means that the type must be known by name through the inspector, so all types need
        // to have been loaded in advance.
        // We are currently not requiring this (although it is already good practice), but since
        // the old code used `FindOrImport`, and used the element's type in the PropertyMapping, we would create
        // mappings for types we didn't know about. This gave subtle error, e.g. where the type you are parsing
        // is in Base, but contains elements from Conformance (like Bundle.entry). In this case, we would create the
        // mapping for such types, but we would not know the correct FHIR version (since we're dealing with Base, which
        // is shared), so if the loaded types contained `Since` attributes, we would get the wrong version.
        // Forcing the user to load the correct FHIR version into our inspector would do away with this incorrect
        // behaviour.
        // If the node's InstanceType is a backbone, we're getting the classMapping for that backbone passed in
        // by our caller, so use that instead.
        var classMapping = backboneClass ?? getClassMappingForInstanceType(node);
        IDictionary<string, object> newInstance = buildNewInstance(classMapping, node.InstanceType);

        // Value is a kind of pseudo-property, so we need to handle it separately.
        if (node.Value is { } value)
            newInstance["value"] = convertTypedElementValue(value, node.InstanceType);

        // Now, read the children
        foreach (var child in node.Children())
        {
            // Although InstanceType suggests this is a run-time type, we have misdesigned this property
            // to also return abstract types, in this case, Backbones. Would love to fix this in SDK6.0,
            // but it would be one of the bigger breaking behavioural changes.
            var convertedValue = child.InstanceType switch
            {
                "BackboneElement" or "Element" =>
                    readFromElement(child, classMapping.FindMappedElementByName(child.Name)?.PropertyTypeMapping),
                _ => readFromElement(child)
            };

            try
            {
                setOrAddProperty(child, newInstance, convertedValue, classMapping);
            }
            catch (InvalidCastException e)
            {
                // In case the InstanceType does not agree with the actual POCO type of the property, the
                // setOrAddProperty method will throw an InvalidCastException. In this case, we should salvage
                // the data we have so far, and put it in an annotation.
                Console.WriteLine(e);
                throw;
            }
        }

        return (Base)newInstance;
    }

    private static IDictionary<string, object> buildNewInstance(ClassMapping classMapping, string? instanceType)
    {
        var newInstance = classMapping.Factory() switch
        {
            IDictionary<string,object> b => b,
            _ => throw Error.InvalidOperation($"Class Factory for '{classMapping.Name}' did not return a dictionary, which is required for " +
                        $"building up POCO's dynamically.")
        };

        if(newInstance is IDynamicType dt)
            dt.DynamicTypeName = instanceType;

        return newInstance;
    }

    private static void setOrAddProperty(ITypedElement node, IDictionary<string, object> target,
        Base convertedValue, ClassMapping parentClassMapping)
    {
        // If this element *could* be repeating (either we don't know the definition, or it really is defined
        // to be a collection, then check to see if there are already items present.
        var couldBeCollection = node.Definition is null || node.Definition.IsCollection;
        var existing = couldBeCollection && target.TryGetValue(node.Name, out var existingValue) ? existingValue : null;

        // If there are, just add this new value.
        if (existing is IList list)
        {
            list.Add(convertedValue);
        }

        // If we already have a value, but it's not a list, we're encountering a second element for the
        // same element. Create a list, and add both the existing and the new value.
        else if(existing is not null)
        {
            var newList = listFactory();
            newList.Add(existing);
            newList.Add(convertedValue);
            target[node.Name] = newList;
        }

        // No existing value, but we know it's a collection, so create a list and add the element.
        else if (node.Definition?.IsCollection == true)
        {
            var newList = listFactory();
            newList.Add(convertedValue);
            target[node.Name] = newList;
        }

        // No existing value, and not a list, just set the element.
        else
        {
            target[node.Name] = convertedValue;
        }
    }

    private static void safeSet(string key, object source)
    {
        target = source;
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

    private ClassMapping getClassMappingForInstanceType(ITypedElement node)
    {
        if(node.InstanceType is {} instanceType && inspector.FindClassMapping(instanceType) is { } mapping)
            return mapping;

        // Ok, so the node does not have a type, or the type is not known. So, let's use one
        // of the applicable dynamic types. If the node has a resource name (but it was unknown),
        // we'll create a DynamicResource. If the node has a Value, it's a DynamicPrimitive,
        // otherwise it's a DynamicDataType.
        // Design question: there might be a "strict" option, where we will not create Dynamic types
        // for unknown types, but throw an error instead.
        if(node.Value is not null)
            return getDefaultMapping(DYNAMIC_PRIMITIVE_TYPE_NAME);

        if (node.Annotation<IResourceTypeSupplier>() is not null)
            return getDefaultMapping(DYNAMIC_RESOURCE_TYPE_NAME);

        return getDefaultMapping(DYNAMIC_DATATYPE_TYPE_NAME);

        ClassMapping getDefaultMapping(string dynTypeName) =>
            inspector.FindClassMapping(dynTypeName) ??
            throw Error.InvalidOperation($"Cannot find ClassMapping for dynamic type '{dynTypeName}'.");
    }
}