/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Hl7.Fhir.Introspection;

internal class ClassMappingCollection : ICollection<ClassMapping>
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

        // Custom mappings intentionally do not participate in the type index.
        // Multiple custom resources/backbones may share the same runtime type
        // (e.g. DynamicResource / DynamicDataType), so indexing them by type would
        // make later imports overwrite earlier ones and would make type-based lookup
        // ambiguous.
        if (!mapping.IsCustomMapping)
            _byType[mapping.NativeType] = mapping;

        var canonical = mapping.Canonical;
        if (canonical is not null)
            _byCanonical[canonical] = mapping;
    }

    /// <summary>
    /// Add every mapping in the collection to the current collection.
    /// </summary>
    /// <param name="mappings"></param>
    public void AddRange(IEnumerable<ClassMapping> mappings)
    {
        foreach (var mapping in mappings)
            Add(mapping);
    }


    public void Clear()
    {
        _byName.Clear();
        _byCanonical.Clear();
        _byType.Clear();
    }

    public bool Contains(ClassMapping item) => _byName.Values.Contains(item);

    public void CopyTo(ClassMapping[] array, int arrayIndex) => _byName.Values.CopyTo(array, arrayIndex);

    public bool Remove(ClassMapping item)
    {
        if (!_byName.TryRemove(item.Name, out _)) return false;

        // Remove the primary type entry and any alias entries (from RegisterTypeAlias) that
        // were pointing to this mapping so that stale lookups cannot be returned.
        foreach (var kvp in _byType)
        {
            if (ReferenceEquals(kvp.Value, item))
                _byType.TryRemove(kvp.Key, out _);
        }

        foreach (var kvp in _byCanonical)
        {
            if (ReferenceEquals(kvp.Value, item))
                _byCanonical.TryRemove(kvp.Key, out _);
        }

        return true;
    }

    public int Count => _byName.Count;

    public bool IsReadOnly => false;

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
    /// Registers a type alias mapping a derived type to an existing <see cref="ClassMapping"/>.
    /// Unlike <see cref="Add"/>, this only updates the type lookup and does not affect the
    /// name or canonical dictionaries. This is used for types that derive from FHIR POCOs
    /// but don't have their own <see cref="FhirTypeAttribute"/>.
    /// </summary>
    public void RegisterTypeAlias(Type type, ClassMapping mapping)
    {
        _byType[type] = mapping;
    }

    /// <summary>
    /// Registers a canonical alias for an existing <see cref="ClassMapping"/>.
    /// Unlike <see cref="Add"/>, this only updates the canonical lookup and does not affect the
    /// name or type dictionaries. This is used when a mapping is first imported by name and later
    /// associated with a canonical identifier.
    /// </summary>
    public void RegisterCanonicalAlias(string canonical, ClassMapping mapping)
    {
        _byCanonical[canonical] = mapping;
    }

    /// <summary>
    /// List of the class mappings, keyed by canonical.
    /// </summary>
    public IReadOnlyDictionary<Type, ClassMapping> ByType => _byType;
    private readonly ConcurrentDictionary<Type, ClassMapping> _byType = new();

    IEnumerator<ClassMapping> IEnumerable<ClassMapping>.GetEnumerator() => _byName.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_byName.Values).GetEnumerator();
}
