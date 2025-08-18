/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using P=Hl7.Fhir.ElementModel.Types;

#nullable enable

namespace Hl7.Fhir.Introspection;

/// <summary>
/// A container for the metadata of an element of a FHIR datatype as present on a property of a (generated) .NET POCO class.
/// </summary>
[System.Diagnostics.DebuggerDisplay(@"\{Name={Name} ElementType={ImplementingType.Name}}")]
public class PropertyMapping : IElementDefinitionSummary
{
 /// <summary>
    /// A bare-bones constructor that creates a new PropertyMapping with just the Name, DeclaringClass,
    /// NativeProperty properties set. All other required properties should be initialized using an object initializer.
    /// </summary>
    /// <remarks>This constructor is mainly useful for generated code that precalculates and creates property mappings.</remarks>
    public PropertyMapping(
        ClassMapping declaringClass,
        string name,
        PropertyInfo nativeProperty)
    {
        DeclaringClass = declaringClass;
        Name = name;
        NativeProperty = nativeProperty;
        _propertyType = null;
    }

    /// <summary>
    /// Creates a custom PropertyMapping representing the metadata for a property in the overflow.
    /// This constructor will initialize the DeclaringClass, Name, PropertyTypeMapping, and the
    /// required properties based on the given <paramref name="propertyType"/> and <paramref name="allowedTypes"/>.
    /// </summary>
    [SetsRequiredMembers]
    public PropertyMapping(ClassMapping declaringClass, string name, Type propertyType, Type[]? allowedTypes = null)
     : this(declaringClass, name, nativeProperty: null!)
    {
        _ = ReflectionHelper.TryGetRepeatingElementType(propertyType, out var collectionItemType);

        // Get to the actual (native) type representing this element
        var implementingType = collectionItemType ?? propertyType;
        if (Nullable.GetUnderlyingType(implementingType) is { } underlyingType) implementingType = underlyingType;

        if (declaringClass.Inspector.FindOrImportClassMapping(implementingType) is not {} propertyTypeMapping)
            throw new InvalidOperationException($"Custom property {name} is of type " +
                                                $"{implementingType}, for which a classmapping cannot be found.");

        Choice = allowedTypes is not null ? ChoiceType.DatatypeChoice : ChoiceType.None;
        IsCollection = collectionItemType is not null;
        PropertyTypeMapping = propertyTypeMapping;
        FhirType = allowedTypes is not null ? allowedTypes.ToArray() : [implementingType];
        ImplementingType = implementingType;
        _propertyType = propertyType;
    }

    /// <summary>
    /// Returns <c>true</c> when this class is a custom mapping, basically a dynamic resource/type with
    /// its own name, not being the default "DynamicType" or "DynamicResource".
    /// </summary>
    public bool IsCustomMapping => NativeProperty is null;
    /// <summary>
    /// The name of the element in the FHIR specification.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The ClassMapping for the type this property is a member of.
    /// </summary>
    public ClassMapping DeclaringClass { get; }

    /// <summary>
    /// The original <see cref="PropertyInfo"/> the metadata was obtained from.
    /// </summary>
    public PropertyInfo? NativeProperty { get; }

    private readonly Type? _propertyType;

    /// <summary>
    /// This is the type of the property, exactly as it is declared in the POCO class.
    /// </summary>
    public Type PropertyType => _propertyType ?? NativeProperty?.PropertyType ??
        throw new InvalidOperationException("PropertyType is not set and NativeProperty is null. This should have been" +
                                            "caught by the constructor.");

    /// <summary>
    /// The native type of the element.
    /// </summary>
    /// <remarks>If the element is a collection or is nullable, this reflects the
    /// collection item or the type that is made nullable respectively.
    /// </remarks>
    public required Type ImplementingType { get; init; }

    /// <summary>
    /// The list of possible FHIR types for this element, listed as the representative .NET types.
    /// For non-choice types this is a single Type, for choices this is either a list of Types or
    /// just <see cref="Hl7.Fhir.Model.DataType"/>.
    /// </summary>
    /// <remark>These are the defined (choice) types for this element as specified in the
    /// FHIR data definitions. It is derived from the actual property type,
    /// or, if present, via a list of types in the [AllowedTypes] attribute. Finally,
    /// it the property type does not represent FHIR metadata, it is overridden using
    /// the [DeclaredType] attribute.
    /// </remark>
    public required Type[] FhirType { get; init; }

    /// <summary>
    /// The <see cref="ClassMapping" /> that represents the type of this property.
    /// </summary>
    /// <remarks>This is effectively the ClassMapping for the <see cref="ImplementingType" /> unless a
    /// <see cref="AllowedTypesAttribute" /> specifies otherwise.</remarks>
    public required ClassMapping PropertyTypeMapping { get; init; }

    /// <summary>
    /// Whether the element can repeat.
    /// </summary>
    public bool IsCollection { get; init; }

    /// <summary>
    /// The element is of an atomic .NET type, not a FHIR generated POCO.
    /// </summary>
    public bool IsPrimitive { get; init; }

    /// <summary>
    /// The element is a primitive (<seealso cref="IsPrimitive"/>) and
    /// represents the primitive `value` attribute/property in the FHIR serialization.
    /// </summary>
    public bool RepresentsValueElement { get; init; }

    /// <summary>
    /// Whether the element appears in _summary
    /// (see https://www.hl7.org/fhir/search.html#summary)
    /// </summary>
    public bool InSummary { get; init; }

    /// <summary>
    /// If this modifies the meaning of other elements
    /// (see https://www.hl7.org/fhir/conformance-rules.html#isModifier)
    /// </summary>
    public bool IsModifier { get; init; }

    /// <summary>
    /// Five W's mappings of the element.
    /// <remarks>it represents the exact element name of one the elements of the
    /// <c>FiveWs</c> pattern from http://hl7.org/fhir/fivews.html. Choice elements are spelled with the
    /// [x] suffix, like <c>done[x]</c>. </remarks>
    /// </summary>
    public string? FiveWs { get; init; }

    /// <summary>
    /// Whether the element has a cardinality higher than 0.
    /// </summary>
    public bool IsMandatoryElement { get; init; }

    /// <summary>
    /// The numeric order of the element (relevant for the XML serialization, which
    /// needs to be in order).
    /// </summary>
    public int? Order { get; init; }

    /// <summary>
    /// How this element is represented in the XML serialization.
    /// </summary>
    public XmlRepresentation SerializationHint { get; init; }

    /// <summary>
    /// Specifies whether this element contains a choice (either a choice element or a
    /// contained resource).
    /// </summary>
    /// <remarks>In the case of a DataChoice, these elements have names ending in [x] in
    /// the StructureDefinition and allow a (possibly restricted) set of types to be used.
    /// These are reflected in the <see cref="FhirType"/> property.</remarks>
    public ChoiceType Choice { get; init; }

    /// <summary>
    /// The collection of zero or more <see cref="ValidationAttribute"/> (or subclasses) declared
    /// on this property.
    /// </summary>
    public ValidatingFhirModelAttribute[] ValidationAttributes { get; init; } = [];


    /// <summary>
    /// For a bound element, this is the name of the binding.
    /// </summary>
    public string? BindingName { get; init; }

    /// <summary>
    /// The <see cref="ModelInspector"/> for which this mapping was created.
    /// </summary>
    public ModelInspector Inspector => DeclaringClass.Inspector;

    /// <summary>
    /// The release of FHIR for which the metadata was extracted from the property.
    /// </summary>
    public FhirRelease Release => DeclaringClass.Release;

    /// <summary>
    /// Inspects the given PropertyInfo, extracting metadata from its attributes and creating a new <see cref="PropertyMapping"/>.
    /// </summary>
    /// <remarks>There should generally be no reason to call this method, as you can easily get the required PropertyMapping via
    /// a ClassMapping - which will cache this information as well. This constructor is public for historical reasons only.</remarks>
    public static bool TryCreate(PropertyInfo prop, [NotNullWhen(true)] out PropertyMapping? result, ClassMapping declaringClass)
    {
        result = null;
        var release = declaringClass.Release;

        // If there is no [FhirElement] on the property, skip it
        if (prop.GetFhirModelAttribute<FhirElementAttribute>(release) is not { } elementAttr) return false;

        // If there is an explicit [NotMapped] on the property, skip it
        // (in combination with `Since` useful to remove a property from the serialization)
        if (prop.GetFhirModelAttribute<NotMappedAttribute>(release) is not null) return false;

        _ = ReflectionHelper.TryGetRepeatingElementType(prop.PropertyType, out var collectionItemType);

        var cardinalityAttr = prop.GetFhirModelAttribute<CardinalityAttribute>(release);

        // Get to the actual (native) type representing this element
        var implementingType = collectionItemType ?? prop.PropertyType;
        if (Nullable.GetUnderlyingType(implementingType) is { } underlyingType) implementingType = underlyingType;

        // The [AllowedTypes] attribute can specify a set of allowed types for this element.
        // If this is a choice element, then take this list as the declared list of FHIR types,
        // otherwise assume this is the implementing FHIR type above
        var overridingTypes = prop.GetFhirModelAttribute<AllowedTypesAttribute>(release) switch
        {
            { OpenChoice: true } => declaringClass.Inspector.OpenTypes,
            { Types: { Length: > 0 } types } => types,
            _ => null
        };
        
        Type mappingType = determineMappingType(overridingTypes, implementingType, elementAttr, declaringClass.Name);

        if (declaringClass.Inspector.FindOrImportClassMapping(mappingType) is not {} propertyTypeMapping)
            throw new InvalidOperationException($"Property {prop.Name} in class {prop.DeclaringType!.Name} is of type " +
                                                $"{mappingType}, for which a classmapping cannot be found.");

        // FhirTypes is either the explicitly listed types in the [AllowedTypes] attribute, or the
        // mappingType we have determined before.
        var fhirTypes = overridingTypes ?? [mappingType];
        var isPrimitive = isAllowedNativeTypeForDataTypeValue(implementingType);

        result = new PropertyMapping(declaringClass, elementAttr.Name, prop)
        {
            InSummary = elementAttr.InSummary,
            IsModifier = elementAttr.IsModifier,
            Choice = elementAttr.Choice,
            SerializationHint = elementAttr.XmlSerialization,
            Order = elementAttr.Order,
            IsCollection = collectionItemType is not null,
            IsMandatoryElement = cardinalityAttr?.Min > 0,
            IsPrimitive = isPrimitive,
            RepresentsValueElement = elementAttr.IsPrimitiveValue,
            ValidationAttributes = prop.GetValidatingAttributes(release).ToArray(),
            FiveWs = elementAttr.FiveWs,
            BindingName = prop.GetFhirModelAttribute<BindingAttribute>(release)?.Name,
            PropertyTypeMapping = propertyTypeMapping,
            FhirType = fhirTypes,
            ImplementingType = implementingType,
        };

        return true;
    }

    private static Type determineMappingType(Type[]? overridingTypes, Type implementingType, FhirElementAttribute felem, string parentTypeName)
    {
        // There are a few cases where AllowedTypes is used to specify the "correct" concrete FhirType to
        // use when ImplementingType is not the right type to base the mapping on:
        // * For elements that use DataType and have a specific AllowedType per version (e.g. Signature.who)
        // * For the value element of Primitives, that need to be mapped to CQL types.
        if (overridingTypes?.Length == 1)
            return overridingTypes[0];

        // The special "value" properties of primitive types are mapped to the CQL types.
        if (isAllowedNativeTypeForDataTypeValue(implementingType) && felem.IsPrimitiveValue)
        {
            return parentTypeName switch
            {
                "boolean" => typeof(P.Boolean),
                "integer" or "unsignedInt" or "positiveInt" => typeof(P.Integer),
                "integer64" => typeof(P.Long),
                "time" => typeof(P.Time),
                "date" => typeof(P.Date),
                "instant" or "datetime" => typeof(P.DateTime),
                "decimal" => typeof(P.Decimal),
                _ => typeof(P.String)
            };
        }

        // For all enums, we use the single mapping for Enum
        if (typeof(Enum).IsAssignableFrom(implementingType))
            return typeof(Enum);

        // For all Code<T>, we use the mapping for Coding
        if (implementingType.IsConstructedGenericType && implementingType.GetGenericTypeDefinition() == typeof(Code<>))
            return typeof(Code);

        // Otherwise, we simply use the mapping for the actual implementing type.
        return implementingType;
    }

    /// <summary>
    /// This function tried to figure out a concrete type for the element represented by this property.
    /// If it cannot derive a concrete type, it will just return <see cref="ImplementingType"/>.
    /// </summary>
    internal Type GetInstantiableType()
    {
        if (!ImplementingType.IsAbstract)
            return ImplementingType;

        // Ok, so we're in abstract type land, maybe FhirType can help us
        if (FhirType.Length == 1)
            return FhirType[0];

        // No, just return ImplementingType then.
        return ImplementingType;
    }

    internal string QualifiedPropName => $"{DeclaringClass.Name}.{Name}";

    private static bool isAllowedNativeTypeForDataTypeValue(Type type) =>
        type.IsEnum || ClassMapping.SupportedDotNetPrimitiveTypes.Contains(type);

// I have disabled this code since the deserializer and serializer no longer use these properties
// instead they are calling TryGetValue and TrySetValue of the new dynamic/overflow subsystem.
// Since these properties would enable some novel usecases in the future (like creating custom properties
// that set cross-version extensions), I have left the code in place.
 #if USE_GETTER_SETTER_AND_CODEGEN

    /// <summary>
    /// The function to use to get the value of this property on an instance of the class.
    /// </summary>
    /// <remarks>If not explicitly set, the framework will determine the fastest way to get the value
    /// the first time get is called.</remarks>
    public Func<Base, object?> Getter
    {
        get => LazyInitializer.EnsureInitialized(ref _getter, buildValueGetter)!;
        init => _getter = value;
    }

    private Func<Base, object?>? _getter;

    private Func<Base, object?> buildValueGetter()
    {
        // If this is a custom property, or this platform does not support code generation,
        // use dictionary access, otherwise use the generated getter.
        if (NativeProperty?.GetValueGetter<Base>() is { } getter) return getter;
        return b => b.TryGetValue(Name, out var value) ? value : null;
    }

    /// <summary>
    /// The function to use to set the value of this property on an instance of the class.
    /// </summary>
    /// <remarks>If not explicitly set, the framework will determine the fastest way to get the value
    /// the first time set is called.</remarks>
    public Action<Base, object?> Setter
    {
        get => LazyInitializer.EnsureInitialized(ref _setter, buildValueSetter)!;
        init => _setter = value;
    }

    private Action<Base, object?>? _setter;

    private Action<Base, object?> buildValueSetter()
    {
        // If this is a not custom property and the platform support codes generation,
        // use the generated setter, unless we need overflow (type inequality).
        if (NativeProperty?.GetValueSetter<Base>() is {} setter)
        {
            return (b, v) =>
            {
                if (v is null || NativeProperty.PropertyType.IsInstanceOfType(v))
                    setter(b, v);
                else
                    b.SetValue(Name, v);
            };
        }

        return (b,v) => b.SetValue(Name, v);
    }
#endif

    public PropertyMapping PromoteToList()
    {
        if(FhirType.Length > 1) throw new InvalidOperationException("Cannot promote a choice element to a list");

        var listType = typeof(List<>).MakeGenericType(ImplementingType);
        return new PropertyMapping(DeclaringClass, Name, listType);
    }

    #region IElementDefinitionSummary members
    string IElementDefinitionSummary.ElementName => this.Name;

    bool IElementDefinitionSummary.IsCollection => this.IsCollection;

    bool IElementDefinitionSummary.IsRequired => this.IsMandatoryElement;

    bool IElementDefinitionSummary.InSummary => this.InSummary;

    /// <inheritdoc/>
    bool IElementDefinitionSummary.IsModifier => this.IsModifier;

    bool IElementDefinitionSummary.IsChoiceElement => this.Choice == ChoiceType.DatatypeChoice;

    bool IElementDefinitionSummary.IsResource => this.Choice == ChoiceType.ResourceChoice;

    string? IElementDefinitionSummary.DefaultTypeName => null;

    ITypeSerializationInfo[] IElementDefinitionSummary.Type
    {
        get
        {
            LazyInitializer.EnsureInitialized(ref _types, buildTypes);
            return _types!;
        }
    }

    private ITypeSerializationInfo[]? _types;

    string? IElementDefinitionSummary.NonDefaultNamespace => null;

    XmlRepresentation IElementDefinitionSummary.Representation =>
        SerializationHint != XmlRepresentation.None ?
            SerializationHint : XmlRepresentation.XmlElement;

    int IElementDefinitionSummary.Order => Order ?? Int32.MaxValue;

    private ITypeSerializationInfo[] buildTypes()
    {
        if (PropertyTypeMapping.IsBackboneType)
            return [PropertyTypeMapping];

        if (IsPrimitive)
        {
            throw new NotSupportedException(
                $"Encountered unexpected primitive type {Name} for ITypedElement.InstanceType.");
        }

        var names = FhirType.Select(getFhirTypeName);
        return names.Select(n => (ITypeSerializationInfo)new PocoTypeReferenceInfo(n)).ToArray();

        string getFhirTypeName(Type ft)
        {
            // The special case where the mapping name is a backbone element name can safely
            // be ignored here, since that is handled by the first case in the if statement above.
            return Inspector.FindOrImportClassMapping(ft) is {} tm
                ? ((IStructureDefinitionSummary)tm).TypeName
                : throw new NotSupportedException($"Type '{ft.Name}' is listed as an allowed type for property " +
                                                  $"'{QualifiedPropName}', but it does not seem to" +
                                                  $"be a valid FHIR type POCO.");
        }
    }

    private readonly struct PocoTypeReferenceInfo(string canonical) : IStructureDefinitionReference
    {
        public string ReferredType { get; } = canonical;
    }

    #endregion
}