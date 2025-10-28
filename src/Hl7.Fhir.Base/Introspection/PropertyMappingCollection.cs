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
using System.Linq;
using System.Threading;

namespace Hl7.Fhir.Introspection;

/// <summary>
/// A list of <see cref="PropertyMapping"/>s, indexed by name and order and choice.
/// </summary>
internal class PropertyMappingCollection : ICollection<PropertyMapping>
{
    public PropertyMappingCollection()
    {
        // Nothing
    }

    public PropertyMappingCollection(IEnumerable<PropertyMapping> mappings)
    {
        AddRange(mappings);
    }

    /// <summary>
    /// Adds the mapped type to the collection, updating the indexed
    /// collections. Note: a newer mapping for the same canonical/name will overwrite
    /// the old one. This way, it is possible to substitute mappings if necessary.
    /// </summary>
    public void Add(PropertyMapping mapping)
    {
        _byName[mapping.Name] = mapping;
        clearCaches();
    }

    private void clearCaches()
    {
        _byOrder = null;
        _choice = null;
    }

    /// <summary>
    /// Add every mapping in the collection to the current collection.
    /// </summary>
    /// <param name="mappings"></param>
    public void AddRange(IEnumerable<PropertyMapping> mappings)
    {
        foreach (var mapping in mappings)
            _byName[mapping.Name] = mapping;

        clearCaches();
    }

    public void Clear()
    {
        _byName.Clear();
       clearCaches();
    }

    public bool Contains(PropertyMapping item) => _byName.Values.Contains(item);

    public void CopyTo(PropertyMapping[] array, int arrayIndex) => _byName.Values.CopyTo(array, arrayIndex);

    public bool Remove(PropertyMapping item)
    {
        if (!_byName.TryRemove(item.Name, out _)) return false;
        _byOrder?.Remove(item);
        _choice?.Remove(item);

        return true;
    }

    public int Count => _byName.Count;

    public bool IsReadOnly => false;

    /// <summary>
    /// List of the PropertyMappings, keyed by name.
    /// </summary>
    public IReadOnlyDictionary<string, PropertyMapping> ByName => _byName;
    private readonly ConcurrentDictionary<string, PropertyMapping> _byName = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// List of the properties, in the order of appearance.
    /// </summary>
    public IReadOnlyList<PropertyMapping> ByOrder => LazyInitializer.EnsureInitialized(ref _byOrder,
        () => ByName.Values.OrderBy(pm => pm.Order).ToList())!;
    private List<PropertyMapping>? _byOrder;

    /// <summary>
    /// The list of properties that represent choice elements.
    /// </summary>
    public IReadOnlyList<PropertyMapping> ChoiceProperties => LazyInitializer.EnsureInitialized(ref _choice,
        () => ByName.Values.Where(pm => pm.Choice == ChoiceType.DatatypeChoice).ToList())!;
    private List<PropertyMapping>? _choice;

    IEnumerator<PropertyMapping> IEnumerable<PropertyMapping>.GetEnumerator() => _byName.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_byName.Values).GetEnumerator();
}