/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Hl7.Fhir.Introspection
{
    internal class EnumMappingCollection
    {
        public EnumMappingCollection()
        {
            // Nothing
        }

        public EnumMappingCollection(IEnumerable<EnumMapping> mappings)
        {
            AddRange(mappings);
        }

        public void Add(EnumMapping mapping)
        {
            var propKey = mapping.Name;
            if (_byName.ContainsKey(propKey))
                throw Error.InvalidOperation($"There are multiple enumerations named '{propKey}'. The name must be unique.");
            _byName[propKey] = mapping;

            _byType[mapping.NativeType] = mapping;

            var canonical = mapping.Canonical;
            if (canonical is not null)
            {
                if (_byCanonical.ContainsKey(canonical))
                    throw Error.InvalidOperation($"There are multiple enumerations with the canonical '{canonical}'. Canonicals must be unique.");

                _byCanonical[canonical] = mapping;
            }
        }

        public void AddRange(IEnumerable<EnumMapping> mappings)
        {
            foreach (var mapping in mappings)
                Add(mapping);
        }

        /// <summary>
        /// List of the enumerations, keyed by name.
        /// </summary>
        public IReadOnlyDictionary<string, EnumMapping> ByName => _byName;
        private readonly ConcurrentDictionary<string, EnumMapping> _byName = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// List of the enums, keyed by canonical.
        /// </summary>
        public IReadOnlyDictionary<string, EnumMapping> ByCanonical => _byCanonical;
        private readonly ConcurrentDictionary<string, EnumMapping> _byCanonical = new();

        /// <summary>
        /// List of the enums, keyed by canonical.
        /// </summary>
        public IReadOnlyDictionary<Type, EnumMapping> ByType => _byType;
        public readonly ConcurrentDictionary<Type, EnumMapping> _byType = new();
    }
}

#nullable restore