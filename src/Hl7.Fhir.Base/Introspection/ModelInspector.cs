/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Introspection;

/// <summary>
/// A cache of FHIR type mappings found on .NET classes.
/// </summary>
/// <remarks>POCO's in the "base" assembly
/// can reflect the definition of multiple releases of FHIR using <see cref="FhirModelAttribute"/>
/// attributes. A <see cref="ModelInspector"/> will always capture the metadata for one such
/// <see cref="Specification.FhirRelease" /> which is passed to it in the constructor.
/// </remarks>
public class ModelInspector : IStructureDefinitionSummaryProvider, IModelInfo
{
    private static readonly ConcurrentDictionary<string, ModelInspector> _inspectedAssemblies = new();

    /// <summary>
    /// Removes all known mappings from the inspector.
    /// </summary>
    public static void Clear() => _inspectedAssemblies.Clear();

    /// <summary>
    /// Returns a fully configured <see cref="ModelInspector"/> with the
    /// FHIR metadata contents of the given assembly, plus all the assemblies it depends upon.
    /// Calling this function repeatedly for the same assembly will return the same inspector.
    /// </summary>
    /// <remarks>If the assembly given is FHIR Release specific, the returned inspector will contain
    /// metadata for that release only. If the assembly is the base assembly, it will contain
    /// metadata for the most recent release for those base classes.</remarks>
    [Obsolete("ModelInspectors should be retrieved from the ModelInfo, or while using Base only, from ModelInspector.Base.")]
    public static ModelInspector ForAssembly(Assembly a)
    {
        return _inspectedAssemblies.GetOrAdd(a.FullName ?? throw Error.ArgumentNull(nameof(a.FullName)),
            _ => configureInspector());

        // NB: The concurrent dictionary will make sure only one of these initializers will run at the same time,
        // so there is no additional locking done in these nested functions.
        ModelInspector configureInspector()
        {
            if (a.GetCustomAttribute<FhirModelAssemblyAttribute>() is not { } modelAssemblyAttr)
                throw new InvalidOperationException($"Assembly {a.FullName} cannot be used to supply FHIR metadata," +
                                                    $" as it is not marked with a {nameof(FhirModelAssemblyAttribute)} attribute.");

            var newInspector = new ModelInspector(modelAssemblyAttr.Since);
            var imported = new List<Assembly>();
            importRecursively(a);

            // Finally, add the System/CQL primitive types
            foreach (var cqlType in getCqlTypes())
                newInspector.ImportType(cqlType);

            return newInspector;

            static IEnumerable<Type> getCqlTypes()
            {
                return typeof(ElementModel.Types.Any).GetTypeInfo().Assembly.GetExportedTypes().
                    Where(typ => typeof(ElementModel.Types.Any).IsAssignableFrom(typ));
            }

            void importRecursively(Assembly assembly)
            {
                if (imported.Contains(assembly)) return;

                newInspector.Import(assembly);
                imported.Add(assembly);

                var referencedFhirAssemblies = assembly.GetReferencedAssemblies()
                    .Select(Assembly.Load)
                    .Where(isFhirModelAssembly);

                foreach (var ra in referencedFhirAssemblies)
                    importRecursively(ra);

                static bool isFhirModelAssembly(Assembly a) =>
                    a.GetCustomAttribute<FhirModelAssemblyAttribute>() is not null;
            }
        }
    }

    /// <summary>
    /// Returns a fully configured <see cref="ModelInspector"/> with the
    /// FHIR metadata contents of the assembly where <paramref name="type"/> resides. Calling this function
    /// repeatedly for the same type will return the same inspector.
    /// </summary>
    /// <param name="type"></param>
    [Obsolete("ModelInspectors should be retrieved from the ModelInfo, or while using Base only, from ModelInspector.Base.")]
    public static ModelInspector ForType(Type type) => ForAssembly(type.Assembly);

    /// <summary>
    /// Returns a fully configured <see cref="ModelInspector"/> with the
    /// FHIR metadata contents of the assembly where <typeparamref name="T"/> resides. Calling this function
    /// repeatedly for the same type will return the same inspector.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete("ModelInspectors should be retrieved from the ModelInfo, or while using Base only, from ModelInspector.Base.")]
    public static ModelInspector ForType<T>() where T : Resource => ForType(typeof(T));

    /// <summary>
    /// Returns a fully configured <see cref="ModelInspector"/> with the
    /// FHIR metadata contents of the base assembly
    /// </summary>
#pragma warning disable CS0618 // Type or member is obsolete
    public static ModelInspector Base => ForAssembly(typeof(ModelInspector).Assembly);
#pragma warning restore CS0618 // Type or member is obsolete

    /// <summary>
    /// Constructs an empty <see cref="ModelInspector"/> with the given FHIR release.
    /// </summary>
    public ModelInspector(FhirRelease fhirRelease)
    {
        FhirRelease = fhirRelease;
    }

    /// <summary>
    /// Constructs a <see cref="ModelInspector"/> with the given predefined mappings.
    /// </summary>
    public ModelInspector(FhirRelease fhirRelease, IEnumerable<ClassMapping> classMappings,
        IEnumerable<EnumMapping> enumMappings) : this(fhirRelease)
    {
        _classMappings = new ClassMappingCollection(classMappings);
        _enumMappings = new EnumMappingCollection(enumMappings);
    }

    /// <summary>
    /// The release of FHIR (i.e. STU3, R4) that this metadata is constructor for.
    /// </summary>
    /// <remarks>This is taken in consideration when encountering versioned FHIR attributes, to be able to
    /// use a single POCO class to reflect the members for different FHIR releases.</remarks>
    public FhirRelease FhirRelease { get; }

    /// <summary>
    /// The detected version of FHIR (i.e. 4.0.2) on the loaded assembly.
    /// </summary>
    /// <remarks>This is taken from the ModelInfo.Version string when the ModelInspector
    /// reflects on a satellite assembly.</remarks>
    public string? FhirVersion { get; private set; }
    
    public Type[] OpenTypes { get; private set; } = [];

    private readonly EnumMappingCollection _enumMappings = new();

    private const string MODELINFO_CLASSNAME = "ModelInfo";
    private const string MODELINFO_VERSION_MEMBER = "Version";
    private const string MODELINFO_OPENTYPES_MEMBER = "OpenTypes";

    /// <summary>
    /// Locates all types and enums in the assembly representing FHIR metadata and extracts
    /// the data into <see cref="ClassMapping"/> and <see cref="EnumMapping"/>
    /// </summary>
    public IReadOnlyList<ClassMapping> Import(Assembly assembly)
    {
        if (assembly == null) throw Error.ArgumentNull(nameof(assembly));

        var exportedTypes = assembly.ExportedTypes.ToList();

        // Try to derive the literal FHIR version (e.g. 4.0.3) from the ModelInfo. This will only work
        // if the added assembly is the satellite for a FHIR release.
        if (exportedTypes.SingleOrDefault(et => et.Name == MODELINFO_CLASSNAME) is { } mi)
        {
            if (mi.GetProperty(MODELINFO_VERSION_MEMBER, BindingFlags.Static | BindingFlags.Public) is { } verPi)
                FhirVersion = verPi.GetValue(null) as string; // null, since this is a static property
            if (mi.GetField(MODELINFO_OPENTYPES_MEMBER, BindingFlags.Static | BindingFlags.Public) is { } openTypesPi)
                OpenTypes = openTypesPi.GetValue(null) as Type[] ?? [];
        }
        
        if (OpenTypes.Length == 0)
            OpenTypes = [typeof(DataType)];

        // Find and extract all EnumMappings
        var exportedEnums = exportedTypes.Where(et => et.IsEnum);
        extractEnums(exportedEnums);

        // Find and extract all ClassMappings
        var exportedClasses = exportedTypes.Where(et => et is { IsClass: true, IsEnum: false });
        return exportedClasses.Select(ImportType)
            .Where(cm => cm is not null)
            .ToList()!;
    }

    /// <summary>
    /// Extracts the FHIR metadata from a <see cref="Type"/> into a <see cref="ClassMapping"/> and
    /// possibly multiple <see cref="EnumMappings"/>.
    /// </summary>
    /// <returns>The created ClassMapping.</returns>
    public ClassMapping? ImportType(Type type)
    {
        if (FindClassMapping(type) is { } existingMapping) return existingMapping;

        if(!ClassMapping.TryCreate(this, type, out var newMapping))
            return null;

        _classMappings.Add(newMapping);

        var nestedTypes = type.GetNestedTypes(BindingFlags.Public);
        var nestedEnums = nestedTypes.Where(t => t.IsEnum);
        extractEnums(nestedEnums);

        var nestedClasses = nestedTypes.Where(t => t is { IsClass: true, IsEnum: false });
        extractBackbonesFromClasses(nestedClasses);

        return newMapping;
    }

    private void extractEnums(IEnumerable<Type> enumTypes)
    {
        foreach (var enumType in enumTypes)
        {
            var success = EnumMapping.TryGetMappingForEnum(enumType, FhirRelease, out var mapping);
            if (success) _enumMappings.Add(mapping!);
        }
    }

    private void extractBackbonesFromClasses(IEnumerable<Type> classTypes)
    {
        foreach (var classType in classTypes)
        {
            _ = ImportType(classType);
        }
    }

    /// <summary>
    /// Tries to retrieve an already imported <see cref="ClassMapping"/> and will import
    /// it when not found.
    /// </summary>
    /// <returns>May return <c>null</c> if the type cannot be imported.</returns>
    public ClassMapping? FindOrImportClassMapping(Type nativeType) => ImportType(nativeType);

    /// <summary>
    /// Retrieves an already imported <see cref="ClassMapping" /> given a FHIR type name.
    /// </summary>
    /// <remarks>The search for the mapping by namem is case-insensitive.</remarks>
    public ClassMapping? FindClassMapping(string fhirTypeName) =>
        _classMappings.ByName.GetValueOrDefault(fhirTypeName);

    /// <summary>
    /// Retrieves an already imported <see cref="ClassMapping" /> given a Type.
    /// </summary>
    public ClassMapping? FindClassMapping(Type t) =>
        _classMappings.ByType.GetValueOrDefault(t);

    /// <summary>
    /// Retrieves an already imported <see cref="ClassMapping" /> given a canonical.
    /// </summary>
    public ClassMapping? FindClassMappingByCanonical(string canonical) =>
        _classMappings.ByCanonical.GetValueOrDefault(canonical);

    /// <summary>
    /// Retrieves an already imported <see cref="EnumMapping"/>, given the name of the valueset.
    /// </summary>
    public EnumMapping? FindEnumMapping(string valuesetName) =>
        _enumMappings.ByName.GetValueOrDefault(valuesetName);

    /// <summary>
    /// Retrieves an already imported <see cref="EnumMapping" /> given the enum Type.
    /// </summary>
    public EnumMapping? FindEnumMapping(Type t) =>
        _enumMappings.ByType.GetValueOrDefault(t);

    /// <summary>
    /// Retrieves an already imported <see cref="EnumMapping" /> given the valueset canonical.
    /// </summary>
    public EnumMapping? FindEnumMappingByCanonical(string canonical) =>
        _enumMappings.ByCanonical.GetValueOrDefault(canonical);

    /// <summary>
    /// The class mapping representing the Cql Patient type for the inspected model.
    /// </summary>
    public ClassMapping? PatientMapping => ClassMappings.FirstOrDefault(cm => cm.IsPatientClass);

    /// <summary>
    /// List of ClassMappings registered with the inspector.
    /// </summary>
    public ICollection<ClassMapping> ClassMappings => _classMappings;

    private readonly ClassMappingCollection _classMappings = [];

    /// <summary>
    /// List of EnumMappings registered with the inspector.
    /// </summary>
    public IEnumerable<EnumMapping> EnumMappings => _enumMappings.ByName.Values;

    /// <inheritdoc cref="IStructureDefinitionSummaryProvider.Provide(string)"/>
    public IStructureDefinitionSummary? Provide(string canonical) =>
        canonical.Contains('/') ?
            FindClassMappingByCanonical(canonical)
            : FindClassMapping(canonical);

    #region IModelInfo

    public Canonical CanonicalUriForFhirCoreType(string typeName) => Canonical.ForCoreType(typeName);

    public Canonical? CanonicalUriForFhirCoreType(Type type) => GetFhirTypeNameForType(type) is { } name ? CanonicalUriForFhirCoreType(name) : null;

    public Type? GetTypeForFhirType(string name) => FindClassMapping(name) is { } mapping ? mapping.NativeType : null;

    public bool IsBindable(string type) => FindClassMapping(type) is { IsBindable: true };

    public bool IsConformanceResource(string name) => GetTypeForFhirType(name) is { } type && IsConformanceResource(type);

    public bool IsConformanceResource(Type type) => type.CanBeTreatedAsType(typeof(IConformanceResource));

    public bool IsCoreModelType(string name) => FindClassMapping(name) is not null;

    public bool IsCoreModelType(Type type) => FindClassMapping(type) is not null;

    public bool IsCoreModelTypeUri(Uri uri) =>
        uri.IsAbsoluteUri && Canonical.FHIR_CORE_PROFILE_BASE_URI.IsBaseOf(uri)
                          && IsCoreModelType(Canonical.FHIR_CORE_PROFILE_BASE_URI.MakeRelativeUri(uri).ToString());

    public bool IsCoreSuperType(string name) => GetTypeForFhirType(name) is { } type && IsCoreSuperType(type);

    public bool IsCoreSuperType(Type type) =>
        type == typeof(Base) ||
        type == typeof(Resource) ||
        type == typeof(DomainResource) ||
        type == typeof(Element) ||
        type == typeof(BackboneElement) ||
        type == typeof(DataType) ||
        type == typeof(PrimitiveType) ||
        type == typeof(BackboneType);

    public bool IsDataType(string name) => FindClassMapping(name) is { IsFhirPrimitive: false, IsResource: false };

    public bool IsDataType(Type type) => FindClassMapping(type) is { IsFhirPrimitive: false, IsResource: false };

    public bool IsInstanceTypeFor(string superclass, string subclass)
    {
        var superType = GetTypeForFhirType(superclass);
        var subType = GetTypeForFhirType(subclass);

        return subType is not null && superType is not null && IsInstanceTypeFor(superType, subType);
    }

    public bool IsInstanceTypeFor(Type superclass, Type subclass) => superclass == subclass || superclass.IsAssignableFrom(subclass);

    public bool IsKnownResource(string name) => FindClassMapping(name) is { IsResource: true };

    public bool IsKnownResource(Type type) => FindClassMapping(type) is { IsResource: true };

    public bool IsPrimitive(string name) => FindClassMapping(name)?.IsFhirPrimitive ?? false;

    public bool IsPrimitive(Type type) => FindClassMapping(type)?.IsFhirPrimitive ?? false;

    public bool IsReference(string name) => GetTypeForFhirType(name) is { } type && IsReference(type);

    public bool IsReference(Type type) => type.CanBeTreatedAsType(typeof(ResourceReference));

    public string? GetFhirTypeNameForType(Type type) => FindClassMapping(type) is { } mapping ? mapping.Name : null;

    #endregion
}