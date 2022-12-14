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
    /// <remarks>POCO's in the "common" assemblies
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
        /// metadata for that release only. If the type is from the common assembly, it will contain
        /// metadata for that type from the most recent release of the common assembly.</remarks>
        public static ClassMapping? GetClassMappingForType(Type t) =>
            ForAssembly(t.GetTypeInfo().Assembly).FindOrImportClassMapping(t);

        /// <summary>
        /// Returns a fully configured <see cref="ModelInspector"/> with the
        /// FHIR metadata contents of the given assembly. Calling this function repeatedly for
        /// the same assembly will return the same inspector.
        /// </summary>
        /// <remarks>If the assembly given is FHIR Release specific, the returned inspector will contain
        /// metadata for that release only. If the assembly is the common assembly, it will contain
        /// metadata for the most recent release for those common classes.</remarks>
        public static ModelInspector ForAssembly(Assembly a)
        {
            return _inspectedAssemblies.GetOrAdd(a.FullName ?? throw Error.ArgumentNull(nameof(a.FullName)), _ => configureInspector(a));

            static ModelInspector configureInspector(Assembly a)
            {
                if (a.GetCustomAttribute<FhirModelAssemblyAttribute>() is not FhirModelAssemblyAttribute modelAssemblyAttr)
                    throw new InvalidOperationException($"Assembly {a.FullName} cannot be used to supply FHIR metadata," +
                        $" as it is not marked with a {nameof(FhirModelAssemblyAttribute)} attribute.");

                var newInspector = new ModelInspector(modelAssemblyAttr.Since);
                newInspector.Import(a);

                // Make sure we always include the common types too. 
                var commonAssembly = typeof(Resource).GetTypeInfo().Assembly;
                if (a.FullName != commonAssembly.FullName)
                    newInspector.Import(commonAssembly);

                // And finally, the System/CQL primitive types
                foreach (var cqlType in getCqlTypes())
                    newInspector.ImportType(cqlType);

                return newInspector;

                static IEnumerable<Type> getCqlTypes()
                {
                    return typeof(ElementModel.Types.Any).GetTypeInfo().Assembly.GetExportedTypes().
                        Where(typ => typeof(ElementModel.Types.Any).IsAssignableFrom(typ));
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
        /// FHIR metadata contents of the common assembly
        /// </summary>
        public static ModelInspector Common => ForType(typeof(ModelInspector));

        /// <summary>
        /// Constructs a ModelInspector that will reflect the FHIR metadata for the given FHIR release
        /// </summary>
        public ModelInspector(FhirRelease fhirRelease)
        {
            FhirRelease = fhirRelease;
        }

        public readonly FhirRelease FhirRelease;

        // Index for easy lookup of datatypes.
        private readonly ConcurrentDictionary<string, ClassMapping> _classMappingsByName =
            new(StringComparer.OrdinalIgnoreCase);

        private readonly ConcurrentDictionary<Type, ClassMapping> _classMappingsByType = new();

        private readonly ConcurrentDictionary<string, ClassMapping> _classMappingsByCanonical = new();

        /// <summary>
        /// Locates all types in the assembly representing FHIR metadata and extracts
        /// the data as <see cref="ClassMapping"/>s.
        /// </summary>
        public IReadOnlyList<ClassMapping> Import(Assembly assembly)
        {
            if (assembly == null) throw Error.ArgumentNull(nameof(assembly));

            IEnumerable<Type> exportedTypes = assembly.ExportedTypes;

            return exportedTypes.Select(t => ImportType(t))
                .Where(cm => cm is not null)
                .ToList()!;
        }

        /// <summary>
        /// Extracts the FHIR metadata from a <see cref="Type"/> into a <see cref="ClassMapping"/>.
        /// </summary>
        public ClassMapping? ImportType(Type type)
        {
            // When explicitly importing a (newer?) class mapping for the same
            // model type name, overwrite the old entry.            
            if (!ClassMapping.TryGetMappingForType(type, FhirRelease, out var mapping))
                return null;

            RegisterTypeMapping(type, mapping!);
            return mapping;
        }

        internal void RegisterTypeMapping(Type t, ClassMapping mapping)
        {
            _classMappingsByName[mapping!.Name] = mapping;
            _classMappingsByType[t] = mapping;

            if (mapping.Canonical is not null)
                _classMappingsByCanonical[mapping.Canonical] = mapping;
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
            _classMappingsByName.TryGetValue(fhirTypeName, out var entry) ? entry : null;

        /// <summary>
        /// Retrieves an already imported <see cref="ClassMapping" /> given a Type.
        /// </summary>
        public ClassMapping? FindClassMapping(Type t) =>
            _classMappingsByType.TryGetValue(t, out var entry) ? entry : null;

        /// <summary>
        /// Retrieves an already imported <see cref="ClassMapping" /> given a canonical.
        /// </summary>
        public ClassMapping? FindClassMappingByCanonical(string canonical) =>
            _classMappingsByCanonical.TryGetValue(canonical, out var entry) ? entry : null;

        /// <summary>
        /// List of PropertyMappings for this class, in the order of listing in the FHIR specification.
        /// </summary>
        public ICollection<ClassMapping> ClassMappings => _classMappingsByName.Values;

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