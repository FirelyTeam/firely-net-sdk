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
using System.Reflection;

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
    private static readonly string DYNAMIC_PRIMITIVE_TYPE_NAME = new DynamicDataType().TypeName;

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

        // Now, create an instance from this mapping.
        // Note: There may be ClassMappings for .NET and CQL types, but we really cannot handle those and they
        // will not normally appear on InstanceType for us.
        var newInstance = classMapping.Factory() switch
        {
            Base b => b.AsDictionary(),
            _ => throw Error.InvalidOperation($"Can only handle Base-derived POCOs, which '{classMapping.NativeType.Name}' is not.")
        };

        // Value is a kind of pseudo-property, so we need to handle it separately.
        // Note: this will set the underlying ObjectValue property on the PrimitiveType instance at this moment,
        // which might not be exactly the expected type. Certainly, it is not expecting the CQL types used
        // on node.Value, so we still have some mapping work to do here.
        // If the ITypedElement.InstanceType does not agree with the type of the element in the poco, all use of
        // the indexers/list.Add below will throw. This is highly unlikely (assuming the source of metadata in the
        // TypedElement is the same as the reflected data on the POCO - but this is not guaranteed). If we want,
        // we can turn it into an error annotation.
        // This will also go wrong where the [DeclaredType] (which is what InstanceType is) is different from
        // the property type in the POCO - but I hope all declared types are assignable to the property types.
        // Need to check this. There is no guarantee in any case.
        if (node.Value is { } value)
            newInstance["value"] = value;

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

            // Note the `Definition?` here. So if this ITypedElement is untyped (we have lost
            // track of the types since we encountered an unknown type), we will never detect
            // lists. If such an element would repeat, we would overwrite the previous occurrence,
            // which is nasty. We should try to detect that an element repeats after all, and swap the
            // existing instance out for a list, to make sure we don't lose data.
            if (node.Definition?.IsCollection == true)
            {
                var list = newInstance.TryGetValue(child.Name, out var existing)
                    ? (IList)existing
                    : classMapping.ListFactory();

                list.Add(convertedValue);
            }
            else
            {
                newInstance[child.Name] = convertedValue;
            }
        }

        return (Base)newInstance;
    }

    private ClassMapping getClassMappingForInstanceType(ITypedElement node)
    {
        if(node.InstanceType is {} instanceType && inspector.FindClassMapping(instanceType) is { } mapping)
            return mapping;

        // Ok, so the node does not have a type, or the type is not known. So, let's use one
        // of the applicable dynamic types. If the node has a resource name (but it was unknown),
        // we'll create a DynamicResource. If the node has a Value, it's a DynamicPrimitive,
        // otherwise it's a DynamicDataType.
        // TODO: if InstanceType has a value, but that type is unknown, we should still set the
        // TypeName of the created Dynamic below to that string.
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