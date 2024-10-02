#nullable enable
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Introspection;

/// <summary>
/// A container for the metadata of a FHIR datatype as present on the (generated) .NET POCO class.
/// </summary>
public class PocoClassMapping : ClassMapping
{
    /// <summary>
    /// Construct a default mapping for a type by reflecting on the FHIR metadata attributes.
    /// </summary>
    public PocoClassMapping(string name, Type nativeType, FhirRelease release)
        : base(name, nativeType, determineKind(nativeType), release, defaultPropertyMapper)
    {
        // Nothing
    }

    /// <summary>
    /// Construct a default mapping for a type by reflecting on the FHIR metadata attributes,
    /// but the properties are provided lazily by the caller.
    /// </summary>
    public PocoClassMapping(string name, Type nativeType, FhirRelease release, Func<IEnumerable<PropertyMapping>> propertyMapper)
        :base(name, nativeType, determineKind(nativeType), release, propertyMapper)
    {
        // Nothing
    }

    /// <summary>
    /// Construct a default mapping for a type by reflecting on the FHIR metadata attributes, using the
    /// properties passed in to the constructor.
    /// </summary>
    internal PocoClassMapping(string name, Type nativeType, FhirRelease release, IEnumerable<PropertyMapping> propertyMappings)
        :base(name, nativeType, determineKind(nativeType), release, propertyMappings)
    {
        // Nothing
    }

    private static DataTypeKind determineKind(Type nativeType)
    {
        if(typeof(Resource).IsAssignableFrom(nativeType))
            return DataTypeKind.Resource;

        if(typeof(PrimitiveType).IsAssignableFrom(nativeType))
            return DataTypeKind.Primitive;

        return DataTypeKind.Complex;
    }

    // Enumerate this class' properties using reflection and create PropertyMappings.
    // Is used when no external mapping has been passed to the constructor.
    private IEnumerable<PropertyMapping> defaultPropertyMapper()
    {
        foreach (var property in ReflectionHelper.FindPublicProperties(NativType))
        {
            if (!PropertyMapping.TryCreate(property, out var propMapping, this)) continue;
            yield return propMapping!;
        }
    }
}