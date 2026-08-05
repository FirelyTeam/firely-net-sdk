/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Hl7.Fhir.Introspection;


/// <summary>
/// A container for the metadata of a FHIR datatype as present on the (generated) .NET POCO class.
/// </summary>
public class ClassMapping(
    ModelInspector parent,
    string name,
    Type nativeType,
    ClassMapping.PropertyMapper propertyMapper)
    : IStructureDefinitionSummary
{
    public delegate IEnumerable<PropertyMapping> PropertyMapper(ClassMapping declaringClass);

    public ClassMapping(ModelInspector parent, string name, Type nativeType,
        IEnumerable<PropertyMapping> properties) : this(parent, name, nativeType, _ => properties)
    {
        // Nothing
    }

    public ClassMapping(ModelInspector parent, string name, Type nativeType)
         : this(parent, name, nativeType, DefaultPropertyMapper)
    {
        // Nothing
    }

    /// <summary>
    /// Returns <c>true</c> when this class is a custom mapping, basically a dynamic resource/type with
    /// its own name, not being the default "DynamicType" or "DynamicResource".
    /// </summary>
    public bool IsCustomMapping => typeof(IDynamicType).IsAssignableFrom(NativeType) && NativeType.Name != Name;

    /// <summary>
    /// The <see cref="ModelInspector"/> for which this mapping was created.
    /// </summary>
    public ModelInspector Inspector { get; } = parent;

    /// <summary>
    /// The FHIR release which this mapping reflects.
    /// </summary>
    /// <remarks>The mapping will contain the metadata that applies to this version (or older), using the
    /// newest metadata when multiple exist.</remarks>
    public FhirRelease Release => Inspector.FhirRelease;

    /// <summary>
    /// Name of the mapping.
    /// </summary>
    /// <remarks>
    /// This is the FHIR name for the type as specified in <see cref="FhirTypeAttribute.Name"/> but not always:
    /// <list type="bullet">
    /// <item>FHIR <c>code</c> types with required bindings are modelled in the POCO as a <see cref="Code{T}"/>,
    /// the mapping name for these will be <c>code&lt;name of enum&gt;</c></item>
    /// <item>The System/CQL primitives from <see cref="Hl7.Fhir.ElementModel.Types"/> all have their names
    /// prepended with "System.", e.g. <c>System.Integer</c>.</item>
    /// <item>.NET primitive types have their <see cref="Type.FullName"/> name prepended with "Net.", e.g. <c>Net.System.Int32</c>.</item>
    /// </list>
    /// </remarks>
    public string Name { get; } = name;

    /// <summary>
    /// The .NET class that implements the FHIR datatype/resource
    /// </summary>
    public Type NativeType { get; } = nativeType;

    /// <summary>
    /// The element is of an atomic .NET type, not a FHIR generated POCO.
    /// </summary>
    public bool IsPrimitive { get; init; } = false;

    /// <summary>
    /// If this mapping represents a <c>Code&lt;T&gt;</c>, this property will hold the enum type T.
    /// </summary>
    public Type? EnumType { get; init; }

    /// <summary>
    /// Indicates whether this class represents the nested complex type for a backbone element.
    /// </summary>
    public bool IsBackboneType { get; init; } = false;

    /// <summary>
    /// The canonical for the StructureDefinition defining this type
    /// </summary>
    /// <remarks>Will be null for backbone types.</remarks>
    public string? Canonical { get; init; }

    /// <summary>
    /// The collection of zero or more <see cref="ValidationAttribute"/> (or subclasses) declared
    /// on this class.
    /// </summary>
    public ValidatingFhirModelAttribute[] ValidationAttributes { get; private set; } = [];

    /// <summary>
    /// Inspects the given type, extracting metadata from its attributes and creating a new <see cref="ClassMapping"/>.
    /// </summary>
    internal static bool TryCreate(ModelInspector parent, Type type, [NotNullWhen(true)]out ClassMapping? result)
    {
        // Simulate reading the ClassMappings from the primitive types (from http://hl7.org/fhirpath namespace).
        // These are in fact defined as POCOs in Hl7.Fhir.ElementModel.Types,
        // but we cannot reflect on them, mainly because the current organization of our assemblies and
        // namespaces make it impossible to include them under Introspection. This is not a showstopper,
        // since these basic primitives have hardly any additional metadata apart from their names.
        if (typeof(ElementModel.Types.Any).GetTypeInfo().IsAssignableFrom(type))
        {
            result = buildCqlClassMapping(type, parent);
            return true;
        }

        // We could (and maybe should) be able to reflect on any type - turning these mappings into general
        // System.Reflection caching classes. I have not done that, but we do need the mappings for the
        // primitive .NET types used in the POCOs (for Element.id etc) too to make the code using the
        // classmappings more consistent in handling both FHIR and .NET datatypes.
        if (SupportedDotNetPrimitiveTypes.Contains(type))
        {
            result = buildNetPrimitiveClassMapping(type, parent);
            return true;
        }

        result = null;

        if (type.IsGenericTypeDefinition)
        {
            Message.Info("Type {0} is marked as a FhirType and is an open generic type, which cannot be used directly to represent a FHIR datatype", type.Name);
            return false;
        }

        // Now continue with the normal algorithm, types adorned with the [FhirTypeAttribute]
        if (type.GetCustomAttribute<FhirTypeAttribute>() is not { } typeAttribute) return false;

        result = new ClassMapping(parent, collectTypeName(typeAttribute, type), type)
        {
            EnumType = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Code<>) ?
                type.GenericTypeArguments[0] : null,
            IsBackboneType = typeAttribute.IsBackboneType,
            Canonical = typeAttribute.Canonical,
            ValidationAttributes = type.GetFhirModelAttributes<ValidatingFhirModelAttribute>(parent.FhirRelease).ToArray(),
        };

        return true;
    }

    /// <summary>
    /// Is <c>true</c> when this class represents a Resource datatype.
    /// </summary>
    public bool IsResource => typeof(Resource).IsAssignableFrom(NativeType);

    /// <summary>
    /// Is <c>true</c> when this class represents a FHIR primitive
    /// </summary>
    /// <remarks>This is different from a .NET primitive, as FHIR primitives are complex types with a primitive value.</remarks>
    public bool IsFhirPrimitive => typeof(PrimitiveType).IsAssignableFrom(NativeType);

    /// <summary>
    /// Indicates whether this class can be used for binding.
    /// </summary>
    public bool IsBindable => typeof(ICoded).IsAssignableFrom(NativeType);

    /// <summary>
    /// Is <c>true</c> when this class represents a code with a required binding.
    /// </summary>
    /// <remarks>See <see cref="Name"></see>.</remarks>
    public bool IsCodeOfT => EnumType is not null;

    // This list is created lazily. This not only improves initial startup time of
    // applications but also ensures circular references between types will not cause loops.
    private PropertyMappingCollection? _mappings;
    private object? _mappingsLock;

    // Note on the `_field ?? LazyInitializer.EnsureInitialized(...)` shape of this member, the
    // other lazily initialized members below and the equivalents in PropertyMapping and
    // PropertyMappingCollection: calling EnsureInitialized unconditionally would allocate its
    // factory delegate on *every* access, including the warm path where the field has long been
    // initialized, and these members are read per element on the (de)serialization path. The
    // null-coalescing read short-circuits that: once the field is initialized, an access is an
    // ordinary read that allocates nothing, and the factory delegate is only created on the
    // once-only cold path. The ordinary read is enough because the runtime guarantees that
    // object reference stores are release stores and that data-dependent reads are ordered, so a
    // thread observing the field non-null also observes the fully initialized object; see
    // https://github.com/dotnet/runtime/blob/main/docs/design/specs/Memory-model.md.
    //
    // This member uses the EnsureInitialized overload with a syncLock, so that the (possibly
    // user-supplied) property mapper building this mutable collection runs exactly once. The other
    // lazy members have idempotent factories whose results are never mutated afterwards, so they
    // use the lock-free overload, where racing factories may run concurrently but only a single
    // result is ever published and handed out.
    private PropertyMappingCollection PropertyMappingsInternal =>
        _mappings ?? LazyInitializer.EnsureInitialized(ref _mappings, ref _mappingsLock, createPropertyMappings)!;

    private PropertyMappingCollection createPropertyMappings()
    {
        var properties = propertyMapper(this).ToList();
        if(properties.FirstOrDefault(m => m.DeclaringClass != this) is {} errorMapping)
            throw new InvalidOperationException($"PropertyMapping '{errorMapping.Name}' is already used for another ClassMapping '{errorMapping.DeclaringClass.Name}'.");

        return new PropertyMappingCollection(properties);
    }

    /// <summary>
    /// List of PropertyMappings for this class, in the order of listing in the FHIR specification.
    /// </summary>
    public ICollection<PropertyMapping> PropertyMappings => PropertyMappingsInternal;

    /// <summary>
    /// Holds a reference to a property that represents the value of a FHIR Primitive. This
    /// property will also be present in the PropertyMappings collection. If this class has
    /// no such property, it is null.
    /// </summary>
    public PropertyMapping? PrimitiveValueProperty => PropertyMappingsInternal.PrimitiveValueProperty;

    /// <summary>
    /// This indicates that this class is representing the Patient data (and implements <see cref="IPatient"/>).
    /// </summary>
    public bool IsPatientClass => typeof(IPatient).IsAssignableFrom(NativeType);

    /// <summary>
    /// Whether the reflected type has a member that represent a primitive value.
    /// </summary>
    public bool HasPrimitiveValueMember => PropertyMappingsInternal.HasPrimitiveValueMember;

    /// <summary>
    /// Returns the mapping for an element of this class by its name.
    /// </summary>
    public PropertyMapping? FindMappedElementByName(string name) =>
        name != null
            ? PropertyMappingsInternal.ByName.GetValueOrDefault(name)
            : throw Error.ArgumentNull(nameof(name));

    /// <summary>
    /// Returns the mapping for an element of this class by a name that
    /// might be suffixed by a type name (e.g. for choice elements).
    /// </summary>
    /// <remarks>Will also return properties for which the name is exactly the same,
    /// so for where there is no suffix. In this case, however, <see cref="FindMappedElementByName(string)"/>
    /// is faster. The element-name part of the name is matched case-sensitively (the type suffix is
    /// matched case-insensitively by the callers that resolve it).
    /// </remarks>
    public PropertyMapping? FindMappedElementByChoiceName(string name)
    {
        if (name == null) throw Error.ArgumentNull(nameof(name));

        // Returns correct mapping for unsuffixed names.
        if (FindMappedElementByName(name) is { } pm) return pm;

        return findChoiceMatch(name, StringComparison.Ordinal);
    }

    private PropertyMapping? findChoiceMatch(string name, StringComparison comparisonType)
    {
        // Check the choice elements for a prefix match.
        var matches = PropertyMappingsInternal.ChoiceProperties
            .Where(m => name.StartsWith(m.Name, comparisonType)).ToList();

        // Loop through possible matches and return the longest match.
        if (matches.Any())
        {
            return (matches.Count == 1)
                ? matches[0]
                : matches.Aggregate((l, r) => l.Name.Length > r.Name.Length ? l : r);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Looks up the element that a (possibly choice-suffixed) serialized name refers to, matching
    /// names case-insensitively, and reports whether the given name matches the definition exactly.
    /// </summary>
    /// <param name="name">The name for the element as encountered in the serialized form.</param>
    /// <returns>The result of the lookup, or <c>null</c> when the name does not match any element
    /// of this class, not even when compared case-insensitively.</returns>
    /// <remarks>This is the single source of truth for detecting case-sensitivity violations in
    /// element names: FHIR names are case-sensitive, but this lookup will also find "near misses"
    /// that differ from a defined element name only by casing. Callers decide, based on
    /// <see cref="ElementLookupResult.IsExactCase"/>, whether to bind the data to the found
    /// element and/or to report an error.</remarks>
    internal ElementLookupResult? TryFindElement(string name)
    {
        if (name == null) throw Error.ArgumentNull(nameof(name));

        // Unsuffixed names: found via the (case-insensitive) name dictionary.
        if (FindMappedElementByName(name) is { } byName)
            return new ElementLookupResult(
                byName,
                byName.Name,
                IsExactCase: string.Equals(name, byName.Name, StringComparison.Ordinal));

        // Choice elements: the name is the element name plus a type suffix. The name dictionary
        // was already consulted above, so only the choice-prefix scan remains to be done here.
        if (findChoiceMatch(name, StringComparison.OrdinalIgnoreCase) is { } byChoice)
        {
            var canonicalName = byChoice.Name + normalizeChoiceSuffix(name[byChoice.Name.Length..]);
            return new ElementLookupResult(
                byChoice,
                canonicalName,
                IsExactCase: string.Equals(name, canonicalName, StringComparison.Ordinal));
        }

        return null;

        // Resolves the suffix to its actual datatype mapping (case-insensitively) so the canonical
        // name we suggest is correct even when the suffix casing is wrong. Suffixes that do not
        // resolve to a known type carry no case information, so they are kept as-is.
        string normalizeChoiceSuffix(string suffix) =>
            suffix.Length > 0 && Inspector.FindClassMapping(suffix) is { } typeMapping
                ? char.ToUpperInvariant(typeMapping.Name[0]) + typeMapping.Name[1..]
                : suffix;
    }

    #region IStructureDefinitionSummary members
    /// <inheritdoc />
    string IStructureDefinitionSummary.TypeName =>
        this switch
        {
            { IsCodeOfT: true } => "code",
            { IsBackboneType: true, IsCustomMapping: true } => "BackboneElement",
            { IsBackboneType: true } => NativeType.CanBeTreatedAsType(typeof(BackboneElement)) ?
                "BackboneElement"
                : "Element",
            _ => Name
        };

    /// <summary>
    /// Whether this type is an abstract type. Can only be set when the mapping is created;
    /// when not set, this is derived from the <see cref="NativeType"/>. Mappings that are not
    /// backed by a .NET type (e.g. mappings built from a StructureDefinition) should set this
    /// explicitly, since their (dynamic) native type does not reflect the FHIR type's abstractness.
    /// </summary>
    public bool? IsAbstract { get; init; }

    /// <inheritdoc />
    bool IStructureDefinitionSummary.IsAbstract =>
        IsAbstract ?? (((IStructureDefinitionSummary)this).TypeName == "BackboneElement" || NativeType.IsAbstract);

    /// <inheritdoc />
    bool IStructureDefinitionSummary.IsResource => IsResource;

    /// <inheritdoc />
    IReadOnlyCollection<IElementDefinitionSummary> IStructureDefinitionSummary.GetElements() =>
        PropertyMappingsInternal.ByOrder.Where(pm => !pm.RepresentsValueElement).ToList();

    #endregion

    /// <summary>
    /// Gets or sets a delegate that, when called, creates an instance for the <see cref="NativeType"/> represented by this mapping.
    /// </summary>
    /// <remarks>If not set, the default constructor for the <see cref="NativeType"/> will be used.</remarks>
    public Base CreateInstance()
    {
        var factory = _factory ?? LazyInitializer.EnsureInitialized(ref _factory, NativeType.BuildFactoryMethod)!;
        var newInstance = factory();
        if (newInstance is IDynamicType idt) idt.DynamicTypeName = Name;
        return (Base)newInstance;
    }

    private Func<object>? _factory;

    /// <summary>
    /// Gets or sets a delegate that, when called, creates an instance of a List of the <see cref="NativeType"/> represented by this mapping.
    /// </summary>
    /// <remarks>If not set, the default List constructor for the <see cref="NativeType"/> will be used.</remarks>
    public IList CreateList()
    {
        var factory = _listFactory ?? LazyInitializer.EnsureInitialized(ref _listFactory, NativeType.BuildListFactoryMethod)!;
        return factory();
    }

    private Func<IList>? _listFactory;

    /// <summary>
    /// Enumerate this class' properties using reflection to create PropertyMappings.
    /// </summary>
    /// <remarks>This is the mapper used when no other mapper is specified in the constructor.</remarks>
    public static IEnumerable<PropertyMapping> DefaultPropertyMapper(ClassMapping parent)
    {
        var properties = selectNearestProperties(ReflectionHelper.FindPublicProperties(parent.NativeType));

        foreach (var property in properties)
        {
            if (!PropertyMapping.TryCreate(property, out var propMapping, parent)) continue;
            yield return propMapping;
        }
    }

    /// <summary>
    /// When redefining a property using `new` in a subclass, the property will be present multiple times in the
    /// list of properties. This method will select the property from the "closest" declaring type in the
    /// inheritance hierarchy to the type of the class mapping.
    /// </summary>
    private static IEnumerable<PropertyInfo> selectNearestProperties(IReadOnlyCollection<PropertyInfo> properties)
    {
        var hierarchyComparer = Comparer<PropertyInfo>.Create(compareInheritance);
        var ordered = properties.OrderBy(p => p, hierarchyComparer);
        return ordered.GroupBy(p => p.Name).Select(g => g.First()).ToList();

        static int compareInheritance(PropertyInfo x, PropertyInfo y)
        {
            if (x.DeclaringType == y.DeclaringType) return 0;
            if (x.DeclaringType!.IsAssignableFrom(y.DeclaringType)) return 1;
            if (y.DeclaringType!.IsAssignableFrom(x.DeclaringType)) return -1;
            return 0;
        }
    }

    private static string collectTypeName(FhirTypeAttribute attr, Type type)
    {
        var name = attr.Name;

        if (ReflectionHelper.IsClosedGenericType(type))
        {
            name += "<";
            name += string.Join(",", type.GetTypeInfo().GenericTypeArguments.Select(arg => arg.FullName));
            name += ">";
        }

        return name;
    }

    // This is the list of .NET "primitive" types that can be used in the generated POCOs and that we
    // can generate ClassMappings for.
    internal static Type[] SupportedDotNetPrimitiveTypes =
    [
        typeof(int), typeof(uint), typeof(long), typeof(ulong),
        typeof(float), typeof(double), typeof(decimal),
        typeof(string),
        typeof(bool),
        typeof(DateTimeOffset),
        typeof(byte[]),
        typeof(Enum),
        typeof(object)
    ];

    private static ClassMapping buildCqlClassMapping(Type t, ModelInspector inspector) =>
        new(inspector, "System." + t.Name, t);

    private static ClassMapping buildNetPrimitiveClassMapping(Type t, ModelInspector inspector) =>
        new(inspector, "Net." + t.FullName, t) { IsPrimitive = true };
    
    private static ClassMapping? _resourceMapping;
    internal static ClassMapping Resource => _resourceMapping ??= ModelInspector.Base.FindOrImportClassMapping(typeof(Resource)) ??
                                             throw new InvalidOperationException($"{nameof(Resource)} mapping not found in Base.");
    private static ClassMapping? _dynResMapping;
    internal static ClassMapping DynamicResource => _dynResMapping ??= ModelInspector.Base.FindOrImportClassMapping(typeof(DynamicResource)) ??
                                                    throw new InvalidOperationException($"{nameof(DynamicResource)} mapping not found in Base.");
    private static ClassMapping? _dynPrimMapping;
    internal static ClassMapping DynamicPrimitive => _dynPrimMapping ??= ModelInspector.Base.FindOrImportClassMapping(typeof(DynamicPrimitive)) ??
                                                     throw new InvalidOperationException($"{nameof(DynamicPrimitive)} mapping not found in Base.");
    private static ClassMapping? _dynDataMapping;
    internal static ClassMapping DynamicDataType => _dynDataMapping ??= ModelInspector.Base.FindOrImportClassMapping(typeof(DynamicDataType)) ??
                                                    throw new InvalidOperationException($"{nameof(DynamicDataType)} mapping not found in Base.");
}

/// <summary>
/// The result of looking up an element by a serialized name using <see cref="ClassMapping.TryFindElement(string)"/>.
/// </summary>
/// <param name="Mapping">The element that the name refers to (for choice elements: the unsuffixed choice element).</param>
/// <param name="CanonicalName">The correctly-cased form of the given name, including a correctly-cased
/// type suffix for choice elements. This is the name that should have been used in the serialized form.</param>
/// <param name="IsExactCase">Whether the given name is exactly equal to <paramref name="CanonicalName"/>.
/// When <c>false</c>, the name is a case-sensitivity violation ("near miss").</param>
internal sealed record ElementLookupResult(
    PropertyMapping Mapping,
    string CanonicalName,
    bool IsExactCase);