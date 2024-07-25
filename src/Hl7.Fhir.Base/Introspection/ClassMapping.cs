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
    /// A container for the metadata of a FHIR datatype as present on the (generated) .NET POCO class.
    /// </summary>
    public class PocoClassMapping : ClassMapping
    {
        /// <summary>
        /// Construct a default mapping for a type by reflecting on the FHIR metadata attributes.
        /// </summary>
        public PocoClassMapping(string name, Type nativeType, FhirRelease release)
            : base(name, nativeType, release)
        {
            // Nothing
        }

        /// <summary>
        /// Construct a default mapping for a type by reflecting on the FHIR metadata attributes,
        /// but the properties are provided lazily by the caller.
        /// </summary>
        public PocoClassMapping(string name, Type nativeType, FhirRelease release, Func<IEnumerable<PropertyMapping>> propertyMapper)
            :base(name, nativeType,release, propertyMapper)
        {
            // Nothing
        }

        /// <summary>
        /// Construct a default mapping for a type by reflecting on the FHIR metadata attributes, using the
        /// properties passed in to the constructor.
        /// </summary>
        internal PocoClassMapping(string name, Type nativeType, FhirRelease release, IEnumerable<PropertyMapping> propertyMappings)
            :base(name, nativeType,release, propertyMappings)
        {
            // Nothing
        }
    }


    /// <summary>
    /// A container for the metadata of a FHIR datatype.
    /// </summary>
    public abstract class ClassMapping : IStructureDefinitionSummary
    {
        /// <summary>
        /// Construct a default mapping for a type by reflecting on the FHIR metadata attributes.
        /// </summary>
        internal ClassMapping(string name, Type nativeType, FhirRelease release)
        {
            Name = name;
            NativeType = nativeType;
            Release = release;
            _propertyMapper = defaultPropertyMapper;
        }

        /// <summary>
        /// Construct a default mapping for a type by reflecting on the FHIR metadata attributes,
        /// but the properties are provided lazily by the caller.
        /// </summary>
        internal ClassMapping(string name, Type nativeType, FhirRelease release, Func<IEnumerable<PropertyMapping>> propertyMapper)
            :this(name, nativeType,release)
        {
            _propertyMapper = propertyMapper;
        }

        /// <summary>
        /// Construct a default mapping for a type by reflecting on the FHIR metadata attributes, using the
        /// properties passed in to the constructor.
        /// </summary>
        internal ClassMapping(string name, Type nativeType, FhirRelease release, IEnumerable<PropertyMapping> propertyMappings)
            :this(name, nativeType,release)
        {
            _propertyMapper = () => propertyMappings;
        }

        private static readonly ConcurrentDictionary<(Type, FhirRelease), ClassMapping?> _mappedClasses = new();

        public static void Clear() => _mappedClasses.Clear();

        /// <summary>
        /// Gets the <see cref="ClassMapping"/> for the given <see cref="Type"/>. Calling this function multiple
        /// times for the same type and release will return the same ClassMapping.
        /// </summary>
        /// <returns>true if the mapping was found or false if it was not one of the supported and reflectable types.</returns>
        /// <remarks>For classes shared across FHIR versions, there may be metadata present for different versions
        /// of FHIR, the <paramref name="release"/> is used to select which subset of metadata to extract. </remarks>
        /// <seealso cref="TryCreate(Type, out ClassMapping?, FhirRelease)"/>
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
        public static bool TryCreate(Type type, [NotNullWhen(true)]out ClassMapping? result, FhirRelease release = (FhirRelease)int.MaxValue)
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

            result = new PocoClassMapping(collectTypeName(typeAttribute, type), type, release)
            {
                IsBackboneType = typeAttribute.IsNestedType || backboneAttribute is not null,
                DefinitionPath = backboneAttribute?.DefinitionPath,
                IsBindable = ReflectionHelper.GetAttribute<BindableAttribute>(type.GetTypeInfo(), release)?.IsBindable ?? false,
                Canonical = typeAttribute.Canonical,
                ValidationAttributes = ReflectionHelper.GetAttributes<ValidationAttribute>(type.GetTypeInfo(), release).ToArray(),
            };

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
        /// <item>FHIR <c>code</c> types with required bindings are modelled in the POCO as a <see cref="Code{T}"/>,
        /// the mapping name for these will be <c>code&lt;name of enum&gt;</c></item>
        /// <item>The System/CQL primitives from <see cref="Hl7.Fhir.ElementModel.Types"/> all have their names
        /// prepended with "System.", e.g. "System.Integer".</item>
        /// <item>.NET primitive types have their <see cref="Type.FullName"/> name prepended with "Net.", e.g. "Net.System.Int32".</item>
        /// </list>
        /// </remarks>
        public string Name { get; }

        /// <summary>
        /// The .NET class that implements the FHIR datatype/resource
        /// </summary>
        public Type NativeType { get; }

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
        /// The element is of an atomic .NET type, not a FHIR generated POCO.
        /// </summary>
        public bool IsPrimitive { get; init; } = false;

        /// <summary>
        /// Is <c>true</c> when this class represents a code with a required binding.
        /// </summary>
        /// <remarks>See <see cref="Name"></see>.</remarks>
        public bool IsCodeOfT  =>
            NativeType is { IsGenericType: true, ContainsGenericParameters: false } &&
            NativeType.GetGenericTypeDefinition() == typeof(Code<>);

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
        public string? DefinitionPath { get; init; }

        /// <summary>
        /// Indicates whether this class can be used for binding.
        /// </summary>
        public bool IsBindable { get; init; } = false;

        /// <summary>
        /// The canonical for the StructureDefinition defining this type
        /// </summary>
        /// <remarks>Will be null for backbone types.</remarks>
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

        // Enumerate this class' properties using reflection and create PropertyMappings.
        // Is used when no external mapping has been passed to the constructor.
        private IEnumerable<PropertyMapping> defaultPropertyMapper()
        {
            foreach (var property in ReflectionHelper.FindPublicProperties(NativeType))
            {
                if (!PropertyMapping.TryCreate(property, out var propMapping, this)) continue;
                yield return propMapping!;
            }
        }

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
        public bool IsPatientClass => typeof(IPatient).IsAssignableFrom(NativeType);

        /// <summary>
        /// Whether the reflected type has a member that represent a primitive value.
        /// </summary>
        public bool HasPrimitiveValueMember => PropertyMappings.Any(pm => pm.RepresentsValueElement);

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
        string IStructureDefinitionSummary.TypeName =>
            this switch
            {
                { IsCodeOfT: true } => "code",
                { IsBackboneType: true } => NativeType.CanBeTreatedAsType(typeof(BackboneElement)) ?
                            "BackboneElement"
                            : "Element",
                _ => Name
            };

        /// <inheritdoc />
        bool IStructureDefinitionSummary.IsAbstract =>
           ((IStructureDefinitionSummary)this).TypeName == "BackboneElement" || NativeType.GetTypeInfo().IsAbstract;

        /// <inheritdoc />
        bool IStructureDefinitionSummary.IsResource => IsResource;

        /// <inheritdoc />
        IReadOnlyCollection<IElementDefinitionSummary> IStructureDefinitionSummary.GetElements() =>
            PropertyMappings.Where(pm => !pm.RepresentsValueElement).ToList();

        #endregion

        /// <summary>
        /// Gets a delegate that, when called, creates an instance for the <see cref="NativeType"/> represented by this mapping.
        /// </summary>
        public Func<object> Factory => LazyInitializer.EnsureInitialized(ref _factory, NativeType.BuildFactoryMethod)!;

        private Func<object>? _factory;


        /// <summary>
        /// Gets a delegate that, when called, creates an instance of a List of the <see cref="NativeType"/> represented by this mapping.
        /// </summary>
        public Func<IList> ListFactory => LazyInitializer.EnsureInitialized(ref _listFactory, NativeType.BuildListFactoryMethod)!;

        private Func<IList>? _listFactory;


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
            typeof(Enum)
        ];

        private static ClassMapping buildCqlClassMapping(Type t, FhirRelease release) =>
            new PocoClassMapping("System." + t.Name, t, release);

        private static ClassMapping buildNetPrimitiveClassMapping(Type t, FhirRelease release) =>
            new PocoClassMapping("Net." + t.FullName, t, release) { IsPrimitive = true };
    }
}

#nullable restore