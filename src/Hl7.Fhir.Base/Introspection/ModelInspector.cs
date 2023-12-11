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

namespace Hl7.Fhir.Introspection
{
    /// <summary>
    /// A cache of FHIR type mappings found on .NET classes.
    /// </summary>
    /// <remarks>POCO's in the "base" assembly
    /// can reflect the definition of multiple releases of FHIR using <see cref="IFhirVersionDependent"/>
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
        /// Finds or creates the <see cref="ClassMapping"/> for a given type.
        /// Calling this function repeatedly for the same type will return the same ClassMapping.
        /// </summary>
        /// <remarks>If the type given is FHIR Release specific, the returned mapping will contain
        /// metadata for that release only. If the type is from the base assembly, it will contain
        /// metadata for that type from the most recent release of the base assembly.</remarks>
        public static ClassMapping? GetClassMappingForType(Type t) =>
            ForAssembly(t.Assembly).FindOrImportClassMapping(t);

        /// <summary>
        /// Returns a fully configured <see cref="ModelInspector"/> with the
        /// FHIR metadata contents of the given assembly, plus all the assemblies it depends upon.
        /// Calling this function repeatedly for the same assembly will return the same inspector.
        /// </summary>
        /// <remarks>If the assembly given is FHIR Release specific, the returned inspector will contain
        /// metadata for that release only. If the assembly is the base assembly, it will contain
        /// metadata for the most recent release for those base classes.</remarks>
        public static ModelInspector ForAssembly(Assembly a)
        {
            return _inspectedAssemblies.GetOrAdd(a.FullName ?? throw Error.ArgumentNull(nameof(a.FullName)), _ => configureInspector(a));

            // NB: The concurrent dictionary will make sure only one of these initializers will run at the same time,
            // so there is no additional locking done in these nested functions.
            static ModelInspector configureInspector(Assembly a)
            {
                if (a.GetCustomAttribute<FhirModelAssemblyAttribute>() is not FhirModelAssemblyAttribute modelAssemblyAttr)
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

                void importRecursively(Assembly a)
                {
                    if (imported.Contains(a)) return;

                    newInspector.Import(a);
                    imported.Add(a);

                    var referencedFhirAssemblies = a.GetReferencedAssemblies()
                            .Select(an => Assembly.Load(an))
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
        public static ModelInspector ForType(Type type) => ForAssembly(type.Assembly);

        /// <summary>
        /// Returns a fully configured <see cref="ModelInspector"/> with the
        /// FHIR metadata contents of the assembly where <typeparamref name="T"/> resides. Calling this function 
        /// repeatedly for the same type will return the same inspector.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static ModelInspector ForType<T>() where T : Resource => ForType(typeof(T));

        /// <summary>
        /// Returns a fully configured <see cref="ModelInspector"/> with the
        /// FHIR metadata contents of the base assembly
        /// </summary>
        public static ModelInspector Base => ForType(typeof(ModelInspector));

        /// <summary>
        /// Constructs a ModelInspector that will reflect the FHIR metadata for the given FHIR release
        /// </summary>
        public ModelInspector(FhirRelease fhirRelease)
        {
            FhirRelease = fhirRelease;
        }

        /// <summary>
        /// The release of FHIR (i.e. STU3, R4) that this metadata is constructor for.
        /// </summary>
        /// <remarks>This is taken in consideration when encountering versioned FHIR attributes, to be able to 
        /// use a single POCO class to reflect the members for different FHIR releases.</remarks>
        public FhirRelease FhirRelease { get; init; }

        /// <summary>
        /// The detected version of FHIR (i.e. 4.0.2) on the loaded assembly.
        /// </summary>
        /// <remarks>This is taken from the ModelInfo.Version string when the ModelInspector
        /// reflects on a satellite assembly.</remarks>
        public string? FhirVersion { get; private set; }

        private readonly EnumMappingCollection _enumMappings = new();

        private const string MODELINFO_CLASSNAME = "ModelInfo";
        private const string MODELINFO_VERSION_MEMBER = "Version";

        /// <summary>
        /// Locates all types and enums in the assembly representing FHIR metadata and extracts
        /// the data into <see cref="ClassMapping"/> and <see cref="EnumMapping"/>
        /// </summary>
        public IReadOnlyList<ClassMapping> Import(Assembly assembly)
        {
            if (assembly == null) throw Error.ArgumentNull(nameof(assembly));

            IEnumerable<Type> exportedTypes = assembly.ExportedTypes;

            // Try to derive the literal FHIR version (e.g. 4.0.3) from the ModelInfo. This will only work
            // if the added assembly is the satellite for a FHIR release.
            if (exportedTypes.SingleOrDefault(et => et.Name == MODELINFO_CLASSNAME) is { } mi &&
                mi.GetProperty(MODELINFO_VERSION_MEMBER, BindingFlags.Static | BindingFlags.Public) is { } pi)
            {
                FhirVersion = pi.GetValue(null) as string;   // null, since this is a static property
            }

            // Find and extract all EnumMappings
            var exportedEnums = exportedTypes.Where(et => et.IsEnum);
            extractEnums(exportedEnums);

            // Find and extract all ClassMappings
            var exportedClasses = exportedTypes.Where(et => et.IsClass && !et.IsEnum);
            return exportedClasses.Select(t => ImportType(t))
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
            if (!ClassMapping.TryGetMappingForType(type, FhirRelease, out var mapping))
                return null;

            _classMappings.Add(mapping!);

            var nestedTypes = type.GetNestedTypes(BindingFlags.Public);
            var nestedEnums = nestedTypes.Where(t => t.IsEnum);
            extractEnums(nestedEnums);

            var nestedClasses = nestedTypes.Where(t => t.IsClass && !t.IsEnum);
            extractBackbonesFromClasses(nestedClasses);

            return mapping;
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
        public ClassMapping? FindOrImportClassMapping(Type nativeType) =>
            FindClassMapping(nativeType) ?? ImportType(nativeType);

        /// <summary>
        /// Retrieves an already imported <see cref="ClassMapping" /> given a FHIR type name.
        /// </summary>
        /// <remarks>The search for the mapping by namem is case-insensitive.</remarks>
        public ClassMapping? FindClassMapping(string fhirTypeName) =>
            _classMappings.ByName.TryGetValue(fhirTypeName, out var entry) ? entry : null;

        /// <summary>
        /// Retrieves an already imported <see cref="ClassMapping" /> given a Type.
        /// </summary>
        public ClassMapping? FindClassMapping(Type t) =>
            _classMappings.ByType.TryGetValue(t, out var entry) ? entry : null;

        /// <summary>
        /// Retrieves an already imported <see cref="ClassMapping" /> given a canonical.
        /// </summary>
        public ClassMapping? FindClassMappingByCanonical(string canonical) =>
            _classMappings.ByCanonical.TryGetValue(canonical, out var entry) ? entry : null;

        /// <summary>
        /// Retrieves an already imported <see cref="EnumMapping"/>, given the name of the valueset.
        /// </summary>
        public EnumMapping? FindEnumMapping(string valuesetName) =>
            _enumMappings.ByName.TryGetValue(valuesetName, out var entry) ? entry : null;

        /// <summary>
        /// Retrieves an already imported <see cref="EnumMapping" /> given the enum Type.
        /// </summary>
        public EnumMapping? FindEnumMapping(Type t) =>
            _enumMappings.ByType.TryGetValue(t, out var entry) ? entry : null;

        /// <summary>
        /// Retrieves an already imported <see cref="EnumMapping" /> given the valueset canonical.
        /// </summary>
        public EnumMapping? FindEnumMappingByCanonical(string canonical) =>
            _enumMappings.ByCanonical.TryGetValue(canonical, out var entry) ? entry : null;

        /// <summary>
        /// The class mapping representing the Cql Patient type for the inspected model.
        /// </summary>
        public ClassMapping? PatientMapping => ClassMappings.FirstOrDefault(cm => cm.IsPatientClass);

        /// <summary>
        /// List of ClassMappings registered with the inspector.
        /// </summary>
        public ICollection<ClassMapping> ClassMappings => _classMappings.ByName.Values.ToList();

        private readonly ClassMappingCollection _classMappings = new();

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

        public Canonical? CanonicalUriForFhirCoreType(string typeName) => Canonical.CanonicalUriForFhirCoreType(typeName);

        public Canonical? CanonicalUriForFhirCoreType(Type type) => GetFhirTypeNameForType(type) is { } name ? CanonicalUriForFhirCoreType(name) : null;

        public Type? GetTypeForFhirType(string name) => FindClassMapping(name) is { } mapping ? mapping.NativeType : null;

        public bool IsBindable(string type) => FindClassMapping(type) is { } mapping && mapping.IsBindable;

        public bool IsConformanceResource(string name) => GetTypeForFhirType(name) is { } type && IsConformanceResource(type);

        public bool IsConformanceResource(Type type) => type.CanBeTreatedAsType(typeof(IConformanceResource));

        public bool IsCoreModelType(string name) => FindClassMapping(name) is not null;

        public bool IsCoreModelType(Type type) => FindClassMapping(type) is not null;

        public bool IsCoreModelTypeUri(Uri uri) => uri is not null
                // [WMR 20181025] Issue #746
                // Note: FhirCoreProfileBaseUri.IsBaseOf(new Uri("Dummy", UriKind.RelativeOrAbsolute)) = true...?!
                && uri.IsAbsoluteUri
                && Canonical.FHIR_CORE_PROFILE_BASE_URI.IsBaseOf(uri)
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

        public bool IsDataType(string name) => FindClassMapping(name) is { } mapping && !mapping.IsFhirPrimitive && !mapping.IsResource;

        public bool IsDataType(Type type) => FindClassMapping(type) is { } mapping && !mapping.IsFhirPrimitive && !mapping.IsResource;

        public bool IsInstanceTypeFor(string superclass, string subclass)
        {
            var superType = GetTypeForFhirType(superclass);
            var subType = GetTypeForFhirType(subclass);

            return subType is not null && superType is not null && IsInstanceTypeFor(superType, subType);
        }

        public bool IsInstanceTypeFor(Type superclass, Type subclass) => superclass == subclass || superclass.IsAssignableFrom(subclass);

        public bool IsKnownResource(string name) => FindClassMapping(name) is { } mapping && mapping.IsResource;

        public bool IsKnownResource(Type type) => FindClassMapping(type) is { } mapping && mapping.IsResource;

        public bool IsPrimitive(string name) => FindClassMapping(name)?.IsFhirPrimitive ?? false;

        public bool IsPrimitive(Type type) => FindClassMapping(type)?.IsFhirPrimitive ?? false;

        public bool IsReference(string name) => GetTypeForFhirType(name) is { } type && IsReference(type);

        public bool IsReference(Type type) => type.CanBeTreatedAsType(typeof(ResourceReference));

        public string? GetFhirTypeNameForType(Type type) => FindClassMapping(type) is { } mapping ? mapping.Name : null;

        #endregion
    }
}

#nullable restore