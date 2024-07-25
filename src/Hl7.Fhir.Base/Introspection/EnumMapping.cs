/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading;

namespace Hl7.Fhir.Introspection
{
    public class PocoEnumMapping : EnumMapping
    {
        public PocoEnumMapping(string name, Type nativeType, FhirRelease release, Func<IReadOnlyDictionary<string,EnumMemberMapping>> valueFactory)
            :base(name, nativeType, release, valueFactory)
        {
            // Nothing
        }

        public PocoEnumMapping(string name, Type nativeType, FhirRelease release, string? defaultCodeSystem) : base(name, nativeType, release, defaultCodeSystem)
        {
            // Nothing
        }

        public PocoEnumMapping(string name, Type nativeType, FhirRelease release, IReadOnlyDictionary<string, EnumMemberMapping> mappings) : base(name, nativeType, release, mappings)
        {
            // Nothing
        }
    }

    /// <summary>
    /// A container for the metadata of a FHIR valueset as present on the .NET Enum.
    /// </summary>
    public abstract class EnumMapping
    {
        private static readonly ConcurrentDictionary<(Type, FhirRelease), EnumMapping?> _mappedEnums = new();

        public static void Clear() => _mappedEnums.Clear();

        /// <summary>
        /// Gets the <see cref="EnumMapping"/> for the given <see cref="Type"/>. Calling this function multiple
        /// times for the same type and release will return the same <see cref="EnumMapping"/>.
        /// </summary>
        /// <returns>true if the mapping was found or false if the type did not represent a FHIR valueset.</returns>
        /// <remarks>For enums shared across FHIR versions, there may be metadata present for different versions
        /// of FHIR, the <paramref name="release"/> is used to select which subset of metadata to extract. </remarks>
        /// <seealso cref="TryCreate(Type, out EnumMapping?, FhirRelease)"/>
        public static bool TryGetMappingForEnum(Type t, FhirRelease release, [NotNullWhen(true)] out EnumMapping? mapping)
        {
            mapping = _mappedEnums.GetOrAdd((t, release), createMapping);
            return mapping is not null;

            static EnumMapping? createMapping((Type, FhirRelease) typeAndRelease) =>
                TryCreate(typeAndRelease.Item1, out var m, typeAndRelease.Item2) ? m : null;
        }

        /// <summary>
        /// Inspects the given enum type, extracting metadata from its attributes and creating a new <see cref="EnumMapping"/>.
        /// </summary>
        /// <remarks>For classes shared across FHIR versions, there may be metadata present for different versions
        /// of FHIR, the <paramref name="release"/> is used to select which subset of metadata to extract.</remarks>
        public static bool TryCreate(Type type, [NotNullWhen(true)] out EnumMapping? result, FhirRelease release = (FhirRelease)int.MaxValue)
        {
            result = default;
            if (!type.IsEnum) return false;

            if (ReflectionHelper.GetAttribute<FhirEnumerationAttribute>(type.GetTypeInfo(), release) is not { } typeAttribute) return false;

            result = new PocoEnumMapping(typeAttribute.BindingName, type, release,
                (typeAttribute.DefaultCodeSystem is not null) ? string.Intern(typeAttribute.DefaultCodeSystem) : null)
            {
                Canonical = typeAttribute.Valueset
            };

            return true;
        }

        internal EnumMapping(string name, Type nativeType, FhirRelease release, Func<IReadOnlyDictionary<string,EnumMemberMapping>> valueFactory)
        {
            Name = name;
            NativeType = nativeType;
            Release = release;
            _mapper = valueFactory;
        }

        internal EnumMapping(string name, Type nativeType, FhirRelease release, string? defaultCodeSystem)
        : this(name, nativeType, release, () => defaultMappingInitializer(defaultCodeSystem,nativeType))
        {
            // Nothing
        }

        internal EnumMapping(string name, Type nativeType, FhirRelease release, IReadOnlyDictionary<string,EnumMemberMapping> mappings)
            :this(name, nativeType, release, () => mappings)
        {
            // Nothing
        }

        /// <summary>
        /// The FHIR release which this mapping reflects.
        /// </summary>
        /// <remarks>The mapping will contain the metadata that applies to this version (or older), using the
        /// newest metadata when multiple exist.</remarks>
        public FhirRelease? Release { get; }

        /// <summary>
        /// Name of the mapping, derived from the valueset's name or id.
        /// </summary>
        /// <remarks>
        /// This is the FHIR name 
        /// </remarks>
        public string Name { get; }

        /// <summary>
        /// The canonical of the ValueSet where this enum was derived from.
        /// </summary>
        public string? Canonical { get; init;  }

        /// <summary>
        /// The code system of most of the member of the ValueSet
        /// </summary>

        /// <summary>
        /// The .NET class that implements the FHIR datatype/resource
        /// </summary>
        public Type NativeType { get; }

        /// <summary>
        /// The list of enum members.
        /// </summary>
        public IReadOnlyDictionary<string, EnumMemberMapping> Members =>
            LazyInitializer.EnsureInitialized(ref _mappings, _mapper)!;

        private readonly Func<IReadOnlyDictionary<string, EnumMemberMapping>> _mapper;
        private IReadOnlyDictionary<string, EnumMemberMapping>? _mappings;

        [Obsolete("This property is not used in the FHIR .NET SDK anymore. It may be removed in a future version.")]
        public string CqlTypeSpecifier => "{http://hl7.org/fhir}" + Name;

        private static IReadOnlyDictionary<string, EnumMemberMapping> defaultMappingInitializer(string? defaultCodeSystem, Type nativeType)
        {
            var result = new Dictionary<string, EnumMemberMapping>();

            foreach (var member in ReflectionHelper.FindEnumFields(nativeType))
            {
                var success = EnumMemberMapping.TryCreate(member, out var mapping, (FhirRelease)int.MaxValue, defaultCodeSystem);

                if (success) result.Add(mapping!.Code, mapping);
            }

            return result;
        }
    }
}

#nullable restore