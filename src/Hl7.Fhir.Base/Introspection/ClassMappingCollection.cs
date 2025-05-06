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
        _byType.TryRemove(item.NativeType, out _);
        if (item.Canonical is not null)
            _byCanonical.TryRemove(item.Canonical, out _);

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
    /// List of the class mappings, keyed by canonical.
    /// </summary>
    public IReadOnlyDictionary<Type, ClassMapping> ByType => _byType;
    private readonly ConcurrentDictionary<Type, ClassMapping> _byType = new();

    IEnumerator<ClassMapping> IEnumerable<ClassMapping>.GetEnumerator() => _byName.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_byName.Values).GetEnumerator();
}