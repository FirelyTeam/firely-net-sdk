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
using ElementMappingInfo = (Hl7.Fhir.Introspection.ClassMapping Class, Hl7.Fhir.Introspection.PropertyMapping? Property);

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Traverses an <see cref="ITypedElement"/> tree and constructs a POCO from it.
/// </summary>
/// <param name="inspector">The inspector providing the necessary metadata about the FHIR POCO classes
/// used in the construction.</param>
internal class PocoBuilderNew(ModelInspector inspector)
{
    /// <summary>
    /// Build a POCO from an <see cref="ITypedElement"/>.
    /// </summary>
    public Base BuildFrom(ITypedElement source)
    {
        if (source == null) throw Error.ArgumentNull(nameof(source));

        var mappings = getMappingInfoForElement(source, null);
        return readFromElement(source, mappings);
    }

    private static readonly string DYNAMIC_RESOURCE_TYPE_NAME = new DynamicResource().TypeName;
    private static readonly string DYNAMIC_DATATYPE_TYPE_NAME = new DynamicDataType().TypeName;
    private static readonly string DYNAMIC_PRIMITIVE_TYPE_NAME = new DynamicPrimitive().TypeName;

    private Base readFromElement(ITypedElement node, ElementMappingInfo mappingInfo)
    {
        IDictionary<string, object> newInstance = buildNewInstance(mappingInfo.Class);

        // Capture the instance type if this is a dynamic type.
        if(newInstance is IDynamicType dt)
            dt.DynamicTypeName = node.InstanceType;

        // Value is a kind of pseudo-property, so we need to handle it separately.
        if (node.Value is { } value)
            newInstance["value"] = convertTypedElementValue(value, node.InstanceType);

        // Now, read the children
        foreach (var child in node.Children())
        {
            var mappings = getMappingInfoForElement(child, mappingInfo.Class);
            var convertedValue = readFromElement(child, mappings);

            try
            {
                setOrAddProperty(child, newInstance, convertedValue, mappings);
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

    private static IDictionary<string, object> buildNewInstance(ClassMapping classMapping)
    {
        return classMapping.Factory() switch
        {
            IDictionary<string,object> b => b,
            _ => throw Error.InvalidOperation($"Class Factory for '{classMapping.Name}' did not return a dictionary, which is required for " +
                        $"building up POCO's dynamically.")
        };
    }

    private static void setOrAddProperty(ITypedElement node, IDictionary<string, object> target,
        Base convertedValue, ElementMappingInfo childMappingInfo)
    {
        // If this element *could* be repeating (either we don't know the definition, or it really is defined
        // to be a collection, then check to see if there are already items present.
        var couldBeCollection = (node.Definition is null && childMappingInfo.Property is null)
                                || node.Definition?.IsCollection == true
                                || childMappingInfo.Property?.IsCollection == true;
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
            var newList = childMappingInfo.Class.ListFactory();
            newList.Add(existing);
            newList.Add(convertedValue);
            target[node.Name] = newList;
        }

        // No existing value, but we know it's a collection, so create a list and add the element.
        else if (node.Definition?.IsCollection == true || childMappingInfo.Property?.IsCollection == true)
        {
            var newList = childMappingInfo.Class.ListFactory();
            newList.Add(convertedValue);
            target[node.Name] = newList;
        }

        // No existing value, and not a list, just set the element.
        else
        {
            // Note that some exceptional primitive properties (like Extension.url and Element.id) are
            // represented in the POCO as .NET primitives, not as FHIR datatypes, so we need to get the value out.
            if (childMappingInfo.Property?.IsPrimitive == true && convertedValue is PrimitiveType { ObjectValue: { } value })
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

    private ElementMappingInfo getMappingInfoForElement(ITypedElement node, ClassMapping? parentMapping)
    {
        var elementMapping = parentMapping?.FindMappedElementByName(node.Name);

        // Although InstanceType suggests this is a run-time type, we have misdesigned this property
        // to also return abstract types, in this case, Backbones. Would love to fix this in SDK6.0,
        // but it would be one of the bigger breaking behavioural changes.
        // In any case, try to derive the actual type from the POCO, since the InstanceType won't help here.
        if(node.InstanceType is "BackboneElement" or "Element")
        {
            return (elementMapping?.PropertyTypeMapping ?? getDefaultMapping(DYNAMIC_DATATYPE_TYPE_NAME), elementMapping);
        };

        // Resolve the instance type through the inspector. This means that the type must be known by name by the
        // inspector, so all types need to have been loaded in advance.
        // We are currently not requiring this (although it is already good practice), but since
        // the old code used `FindOrImport`, and used the element's type in the PropertyMapping, we would create
        // mappings for types we didn't know about. This gave subtle error, e.g. where the type you are parsing
        // is in Base, but contains elements from Conformance (like Bundle.entry). In this case, we would create the
        // mapping for such types, but we would not know the correct FHIR version (since we're dealing with Base, which
        // is shared), so if the loaded types contained `Since` attributes, we would get the wrong version.
        // Forcing the user to load the correct FHIR version into our inspector would do away with this incorrect
        // behaviour.
        if (node.InstanceType is { } instanceType && inspector.FindClassMapping(instanceType) is { } mapping)
        {
            return (mapping, elementMapping);
        }

        // Ok, so the node does not have a type, or the type is not known. So, let's use one
        // of the applicable dynamic types. If the node has a resource name (but it was unknown),
        // we'll create a DynamicResource. If the node has a Value, it's a DynamicPrimitive,
        // otherwise it's a DynamicDataType.
        // Design question: there might be a "strict" option, where we will not create Dynamic types
        // for unknown types, but throw an error instead.
        if(node.Value is not null)
            return (getDefaultMapping(DYNAMIC_PRIMITIVE_TYPE_NAME), elementMapping);

        if (node.Annotation<IResourceTypeSupplier>() is not null)
            return (getDefaultMapping(DYNAMIC_RESOURCE_TYPE_NAME), elementMapping);

        return (getDefaultMapping(DYNAMIC_DATATYPE_TYPE_NAME), elementMapping);

        ClassMapping getDefaultMapping(string dynTypeName) =>
            inspector.FindClassMapping(dynTypeName) ??
            throw Error.InvalidOperation($"Cannot find ClassMapping for dynamic type '{dynTypeName}'.");
    }
}