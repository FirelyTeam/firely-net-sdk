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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Hl7.Fhir.Introspection
{

    /// <summary>
    /// A container for the metadata of a FHIR datatype.
    /// </summary>
    public abstract class ClassMapping : IStructureDefinitionSummary
    {
        /// <summary>
        /// Construct a default mapping for a type by reflecting on the FHIR metadata attributes,
        /// but the properties are provided lazily by the caller.
        /// </summary>
        //TODO: I think FhirRelease can go out too, as it is an aspect of the reflection (and thus the
        //ModelInspector/ClassMappingCollection) not, the ClassMapping itself.

        protected ClassMapping(string name, Type nativeType, DataTypeKind kind, FhirRelease release, Func<IEnumerable<PropertyMapping>> propertyMapper)
        {
            Name = name;
            Release = release;
            Kind = kind;
            NativType = nativeType;
            _propertyMapper = propertyMapper;
        }

        /// <summary>
        /// Construct a default mapping for a type by reflecting on the FHIR metadata attributes, using the
        /// properties passed in to the constructor.
        /// </summary>
        protected ClassMapping(string name, Type nativeType, DataTypeKind kind, FhirRelease release, IEnumerable<PropertyMapping> propertyMappings)
            :this(name, nativeType, kind, release, () => propertyMappings)
        {
            // TODO: creating a closure for every classmapping (as in the parameter above) is wasteful, and we should actually
            // just have a member holding all mappings, that is possibly filled delayed with a function.
            // Nothing
        }

        // TODO: The next fields and functions for caching should not be here: it is a cache for POCO mappings,
        // and thus an aspect of the reflection code.

        private static readonly ConcurrentDictionary<(Type, FhirRelease), ClassMapping?> _mappedClasses = new();

        public static void Clear() => _mappedClasses.Clear();

        /// <summary>
        /// Gets the <see cref="ClassMapping"/> for the given <see cref="Type"/>. Calling this function multiple
        /// times for the same type and release will return the same ClassMapping.
        /// </summary>
        /// <returns>true if the mapping was found or false if it was not one of the supported and reflectable types.</returns>
        /// <remarks>For classes shared across FHIR versions, there may be metadata present for different versions
        /// of FHIR, the <paramref name="release"/> is used to select which subset of metadata to extract. </remarks>
        /// <seealso cref="TryCreate(Type, out PocoClassMapping?, FhirRelease)"/>
        public static bool TryGetMappingForType(Type t, FhirRelease release, [NotNullWhen(true)] out ClassMapping? mapping)
        {
            mapping = _mappedClasses.GetOrAdd((t, release), createMapping);
            return mapping is not null;

            static ClassMapping? createMapping((Type, FhirRelease) typeAndRelease) =>
                TryCreate(typeAndRelease.Item1, out var m, typeAndRelease.Item2) ? m : null;
        }

        /// <summary>
        /// Inspects the given type, extracting metadata from its attributes and creating a new <see cref="ClassMapping"/>.
        /// </summary>
        /// <remarks>For classes shared across FHIR versions, there may be metadata present for different versions
        /// of FHIR, the <paramref name="release"/> is used to select which subset of metadata to extract.</remarks>
        public static bool TryCreate(Type type, [NotNullWhen(true)]out PocoClassMapping? result, FhirRelease release = (FhirRelease)int.MaxValue)
        {
            // Simulate reading the ClassMappings from the primitive types (from http://hl7.org/fhirpath namespace).
            // These are in fact defined as POCOs in Hl7.Fhir.ElementModel.Types,
            // but we cannot reflect on them, mainly because the current organization of our assemblies and
            // namespaces make it impossible to include them under Introspection. This is not a showstopper,
            // since these basic primitives have hardly any additional metadata apart from their names.
            if (typeof(ElementModel.Types.Any).GetTypeInfo().IsAssignableFrom(type))
            {
                result = buildCqlClassMapping(type, release);
                return true;
            }

            // We could (and maybe should) be able to reflect on any type - turning these mappings into general
            // System.Reflection caching classes. I have not done that, but we do need the mappings for the
            // primitive .NET types used in the POCOs (for Element.id etc) too to make the code using the
            // classmappings more consistent in handling both FHIR and .NET datatypes.
            if (SupportedDotNetPrimitiveTypes.Contains(type))
            {
                result = buildNetPrimitiveClassMapping(type, release);
                return true;
            }

            result = null;

            if (ReflectionHelper.IsOpenGenericTypeDefinition(type))
            {
                Message.Info("Type {0} is marked as a FhirType and is an open generic type, which cannot be used directly to represent a FHIR datatype", type.Name);
                return false;
            }

            // Now continue with the normal algorithm, types adorned with the [FhirTypeAttribute]
            if (ReflectionHelper.GetAttribute<FhirTypeAttribute>(type.GetTypeInfo(), release) is not { } typeAttribute) return false;

            var backboneAttribute = ReflectionHelper.GetAttribute<BackboneTypeAttribute>(type, release);

            result = new PocoClassMapping(collectTypeName(typeAttribute, backboneAttribute?.DefinitionPath, type), type, release)
            {
                IsBackboneType = typeAttribute.IsNestedType || backboneAttribute is not null,
                IsBindable = ReflectionHelper.GetAttribute<BindableAttribute>(type.GetTypeInfo(), release)?.IsBindable ?? false,
                Canonical = typeAttribute.Canonical,
                ValidationAttributes = ReflectionHelper.GetAttributes<ValidationAttribute>(type.GetTypeInfo(), release).ToArray(),
                StructureDefinitionSummaryTypeName = determineSdsTypeName(),
                Factory = null!,
                ListFactory = null!,
                IsPrimitive = false
            };

            string? determineSdsTypeName()
            {
                if (backboneAttribute is not null)
                {
                    return type.CanBeTreatedAsType(typeof(BackboneElement))
                        ? "BackboneElement"
                        : "Element";
                }

                if (ReflectionHelper.IsCodeOfT(type))
                    return "code";

                return null;
            }

            return true;
        }

        /// <summary>
        /// The FHIR release which this mapping reflects.
        /// </summary>
        /// <remarks>The mapping will contain the metadata that applies to this version (or older), using the
        /// newest metadata when multiple exist.</remarks>
        public FhirRelease Release { get; }

        /// <summary>
        /// Name of the mapping.
        /// </summary>
        /// <remarks>
        /// This is the FHIR name for the type as specified in <see cref="FhirTypeAttribute.Name"/> but not always:
        /// <list type="bullet">
        /// <item>Backbone elements have the name of the element they are nested in, e.g. <c>Patient.contact</c>.</item>
        /// <item>FHIR <c>code</c> types with required bindings are modelled in the POCO as a <see cref="Code{T}"/>,
        /// the mapping name for these will be <c>code&lt;name of enum&gt;</c></item>
        /// <item>The System/CQL primitives from <see cref="Hl7.Fhir.ElementModel.Types"/> all have their names
        /// prepended with "System.", e.g. <c>System.Integer</c>.</item>
        /// <item>.NET primitive types have their <see cref="Type.FullName"/> name prepended with "Net.", e.g. <c>Net.System.Int32</c>.</item>
        /// </list>
        /// </remarks>
        public string Name { get; }

        /// <summary>
        /// Determines the kind of datatype this class represents.
        /// </summary>
        public DataTypeKind Kind { get; }

        /// <summary>
        /// The .NET class that implements the FHIR datatype/resource
        /// </summary>
        public Type NativType { get; }


        /// <summary>
        /// Is <c>true</c> when this class represents a Resource datatype.
        /// </summary>
        [Obsolete("Check the kind of datatype using the Kind property.")]
        public bool IsResource => Kind == DataTypeKind.Resource;

        /// <summary>
        /// Is <c>true</c> when this class represents a FHIR primitive
        /// </summary>
        /// <remarks>This is different from a .NET primitive, as FHIR primitives are complex types with a primitive value.</remarks>
        [Obsolete("Check the kind of datatype using the Kind property.")]
        public bool IsFhirPrimitive => Kind == DataTypeKind.Primitive;

        /// <summary>
        /// The element is of an atomic .NET type, not a FHIR generated POCO.
        /// </summary>
        public bool IsPrimitive { get; init; } = false;

        /// <summary>
        /// Indicates whether this class represents the nested complex type for a backbone element.
        /// </summary>
        [Obsolete("These types are now generally called Backbone types, so use IsBackboneType instead.")]
        public bool IsNestedType { get => IsBackboneType; set => _isBackboneType = value; }

        /// <summary>
        /// Indicates whether this class represents the nested complex type for a backbone element.
        /// </summary>
        public bool IsBackboneType { get => _isBackboneType; init => _isBackboneType = value; }

        private bool _isBackboneType;

        /// <summary>
        /// If this is a backbone type (<see cref="IsBackboneType"/>), then this contains the path
        /// in the StructureDefinition where the backbone was defined first.
        /// </summary>
        [Obsolete("When this is a Backbone type, the <see cref=\"Name\"/> will be the same as the definition path.")]
        public string? DefinitionPath => IsBackboneType ? Name : null;

        /// <summary>
        /// Indicates whether this class can be used for binding.
        /// </summary>
        public bool IsBindable { get; init; } = false;

        /// <summary>
        /// Override for this type's <see cref="Name"/> if it differs from
        /// <see cref="IStructureDefinitionSummary.TypeName"/>.
        /// </summary>
        /// <remarks>This is a highly specialized use case for backbone types and types based on CodeOfT
        /// and should not normally be set.</remarks>
        public string? StructureDefinitionSummaryTypeName { get; init; }

        /// <summary>
        /// The canonical for the StructureDefinition defining this type (if any).
        /// </summary>
        /// <remarks>Will be null for backbone types, or when there is no known backing
        /// StructureDefinition.</remarks>
        public string? Canonical { get; init; }

        /// <summary>
        /// The collection of zero or more <see cref="ValidationAttribute"/> (or subclasses) declared
        /// on this class.
        /// </summary>
        public ValidationAttribute[] ValidationAttributes { get; init; } = [];

        // This list is created lazily. This not only improves initial startup time of 
        // applications but also ensures circular references between types will not cause loops.
        private PropertyMappingCollection? _mappings;
        private readonly Func<IEnumerable<PropertyMapping>> _propertyMapper;

        private PropertyMappingCollection AllPropertyMappings =>
                LazyInitializer.EnsureInitialized(ref _mappings,
                    () => new PropertyMappingCollection(this, _propertyMapper()))!;

        /// <summary>
        /// List of PropertyMappings for this class, in the order of listing in the FHIR specification.
        /// </summary>
        public IReadOnlyList<PropertyMapping> PropertyMappings => AllPropertyMappings.ByOrder;

        /// <summary>
        /// Holds a reference to a property that represents the value of a FHIR Primitive. This
        /// property will also be present in the PropertyMappings collection. If this class has 
        /// no such property, it is null. 
        /// </summary>
        public PropertyMapping? PrimitiveValueProperty => PropertyMappings.SingleOrDefault(pm => pm.RepresentsValueElement);

        /// <summary>
        /// This indicates that this class is representing the Patient data (and implements <see cref="IPatient"/>).
        /// </summary>
        public bool IsPatientClass => typeof(IPatient).IsAssignableFrom(NativType);

        /// <summary>
        /// Returns the mapping for an element of this class by its name.
        /// </summary>
        public PropertyMapping? FindMappedElementByName(string name) =>
            name != null
                ? AllPropertyMappings.ByName.GetValueOrDefault(name)
                : throw Error.ArgumentNull(nameof(name));

        /// <summary>
        /// Returns the mapping for an element of this class by a name that
        /// might be suffixed by a type name (e.g. for choice elements).
        /// </summary>
        /// <remarks>Will also return properties for which the name is exactly the same,
        /// so for where there is no suffix. In this case, however, <see cref="FindMappedElementByName(string)"/>
        /// is faster.
        /// </remarks>
        public PropertyMapping? FindMappedElementByChoiceName(string name)
        {
            if (name == null) throw Error.ArgumentNull(nameof(name));

            // Returns correct mapping for unsuffixed names.
            if (FindMappedElementByName(name) is { } pm) return pm;

            // Now, check the choice elements for a match.
            var matches = AllPropertyMappings.ChoiceProperties
                .Where(m => name.StartsWith(m.Name)).ToList();

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

        #region IStructureDefinitionSummary members
        /// <inheritdoc />
        string IStructureDefinitionSummary.TypeName => StructureDefinitionSummaryTypeName ?? Name;

        /// <inheritdoc />
        bool IStructureDefinitionSummary.IsResource => Kind == DataTypeKind.Resource;

        /// <inheritdoc />
        IReadOnlyCollection<IElementDefinitionSummary> IStructureDefinitionSummary.GetElements() =>
            PropertyMappings.Where(pm => !pm.RepresentsValueElement).ToList();

        #endregion

        /// <summary>
        /// Gets or sets a delegate that, when called, creates an instance for the <see cref="NativType"/> represented by this mapping.
        /// </summary>
        /// <remarks>If not set, the default constructor for the <see cref="NativType"/> will be used.</remarks>
        public Func<object> Factory
        {
            get => LazyInitializer.EnsureInitialized(ref _factory, NativType.BuildFactoryMethod)!;
            set => _factory = value;
        }

        private Func<object>? _factory;


        /// <summary>
        /// Gets or sets a delegate that, when called, creates an instance of a List of the <see cref="NativType"/> represented by this mapping.
        /// </summary>
        /// <remarks>If not set, the default List constructor for the <see cref="NativType"/> will be used.</remarks>
        public Func<IList> ListFactory
        {
            get => LazyInitializer.EnsureInitialized(ref _listFactory, NativType.BuildListFactoryMethod)!;
            set => _listFactory = value;
        }

        private Func<IList>? _listFactory;

        private static string collectTypeName(FhirTypeAttribute attr, string? bbDefinitionPath, Type type)
        {
            var name = bbDefinitionPath ?? attr.Name;

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
            typeof(Enum)
        ];

        private static PocoClassMapping buildCqlClassMapping(Type t, FhirRelease release) =>
            new("System." + t.Name, t, release);

        private static PocoClassMapping buildNetPrimitiveClassMapping(Type t, FhirRelease release) =>
            new("Net." + t.FullName, t, release) { IsPrimitive = true };
    }
}

#nullable restore