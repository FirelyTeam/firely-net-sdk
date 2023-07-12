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
using System.Reflection;

namespace Hl7.Fhir.Introspection
{
    /// <summary>
    /// A container for the metadata of a FHIR valueset as present on the .NET Enum.
    /// </summary>
    public class EnumMapping
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
        public static bool TryGetMappingForEnum(Type t, FhirRelease release, out EnumMapping? mapping)
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
        public static bool TryCreate(Type type, out EnumMapping? result, FhirRelease release = (FhirRelease)int.MaxValue)
        {
            result = default;
            if (!type.IsEnum) return false;

            if (ClassMapping.GetAttribute<FhirEnumerationAttribute>(type.GetTypeInfo(), release) is not { } typeAttribute) return false;

            result = new EnumMapping(typeAttribute.BindingName, typeAttribute.Valueset, type, release, (typeAttribute.DefaultCodeSystem is not null) ? string.Intern(typeAttribute.DefaultCodeSystem) : null);
            return true;
        }

        private EnumMapping(string name, string? canonical, Type nativeType, FhirRelease release, string? defaultCodeSystem)
        {
            Name = name;
            Canonical = canonical;
            NativeType = nativeType;
            Release = release;
            _mappings = new(valueFactory: () => mappingInitializer(defaultCodeSystem));
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
        public string Name { get; private set; }

        /// <summary>
        /// The canonical of the ValueSet where this enum was derived from.
        /// </summary>
        public string? Canonical { get; }

        /// <summary>
        /// The code system of most of the member of the ValueSet
        /// </summary>

        /// <summary>
        /// The .NET class that implements the FHIR datatype/resource
        /// </summary>
        public Type NativeType { get; private set; }

        /// <summary>
        /// The list of enum members.
        /// </summary>
        public IReadOnlyDictionary<string, EnumMemberMapping> Members => _mappings.Value;

        private readonly Lazy<IReadOnlyDictionary<string, EnumMemberMapping>> _mappings;

        public string CqlTypeSpecifier => "{http://hl7.org/fhir}" + Name;


        private IReadOnlyDictionary<string, EnumMemberMapping> mappingInitializer(string? defaultCS)
        {
            var result = new Dictionary<string, EnumMemberMapping>();

            foreach (var member in ReflectionHelper.FindEnumFields(NativeType))
            {
                var success = EnumMemberMapping.TryCreate(member, out var mapping, (FhirRelease)int.MaxValue, defaultCS);

                if (success) result.Add(mapping!.Code, mapping);
            }

            return result;
        }
    }
}

#nullable restore