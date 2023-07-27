/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Hl7.Fhir.Introspection
{
    internal class ClassMappingCollection
    {
        public ClassMappingCollection()
        {
            // Nothing
        }

        public ClassMappingCollection(IEnumerable<ClassMapping> mappings)
        {
            AddRange(mappings);
        }

        /// <summary>
        /// Adds the mapped type to the collection, updating the indexed
        /// collections. Note: a newer mapping for the same canonical/name will overwrite
        /// the old one. This way, it is possible to substitute mappings if necessary.
        /// </summary>
        public void Add(ClassMapping mapping)
        {
            var propKey = mapping.Name;
            _byName[propKey] = mapping;

            _byType[mapping.NativeType] = mapping;

            var canonical = mapping.Canonical;
            if (canonical is not null)
                _byCanonical[canonical] = mapping;
        }

        public void AddRange(IEnumerable<ClassMapping> mappings)
        {
            foreach (var mapping in mappings)
                Add(mapping);
        }

        /// <summary>
        /// List of the class mappings, keyed by name.
        /// </summary>
        public IReadOnlyDictionary<string, ClassMapping> ByName => _byName;
        private readonly ConcurrentDictionary<string, ClassMapping> _byName = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// List of the class mappings, keyed by canonical.
        /// </summary>
        public IReadOnlyDictionary<string, ClassMapping> ByCanonical => _byCanonical;
        private readonly ConcurrentDictionary<string, ClassMapping> _byCanonical = new();

        /// <summary>
        /// List of the class mappings, keyed by canonical.
        /// </summary>
        public IReadOnlyDictionary<Type, ClassMapping> ByType => _byType;
        public readonly ConcurrentDictionary<Type, ClassMapping> _byType = new();
    }
}

#nullable restore