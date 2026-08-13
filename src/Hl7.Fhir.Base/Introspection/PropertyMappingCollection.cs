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
        _valueElements = null;
        _mandatoryElements = null;
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

        // Note: removing `item` from the cached lists is not enough - since the name dictionary is
        // case-insensitive, the mapping just evicted may be a *different* instance that happens to
        // share `item`'s name, which would leave that evicted mapping behind in the caches. Drop
        // the caches instead, so they are rebuilt from the name dictionary on the next read.
        clearCaches();

        return true;
    }

    public int Count => _byName.Count;

    public bool IsReadOnly => false;

    /// <summary>
    /// List of the PropertyMappings, keyed by name.
    /// </summary>
    public IReadOnlyDictionary<string, PropertyMapping> ByName => _byName;
    private readonly ConcurrentDictionary<string, PropertyMapping> _byName = new(StringComparer.OrdinalIgnoreCase);

    // The lazily computed lists below only call LazyInitializer.EnsureInitialized() when the field
    // is still null, so that reading them on the warm path allocates nothing; see the note on
    // ClassMapping.PropertyMappingsInternal for the reasoning. All three are dropped by
    // clearCaches() when the collection changes.

    /// <summary>
    /// List of the properties, in the order of appearance.
    /// </summary>
    public IReadOnlyList<PropertyMapping> ByOrder =>
        _byOrder ?? LazyInitializer.EnsureInitialized(ref _byOrder,
            () => ByName.Values.OrderBy(pm => pm.Order).ToList())!;

    private List<PropertyMapping>? _byOrder;

    /// <summary>
    /// The list of properties that represent choice elements.
    /// </summary>
    public IReadOnlyList<PropertyMapping> ChoiceProperties =>
        _choice ?? LazyInitializer.EnsureInitialized(ref _choice,
            () => ByName.Values.Where(pm => pm.Choice == ChoiceType.DatatypeChoice).ToList())!;

    private List<PropertyMapping>? _choice;

    /// <summary>
    /// The property that represents the value of a FHIR primitive, or <c>null</c> when this
    /// collection has no such property.
    /// </summary>
    /// <remarks>Determining which properties are value elements requires a scan of the collection,
    /// and this is read for every element encountered during (de)serialization, so the scan is done
    /// once and its (near-always empty or single-entry) result cached. Picking the single value
    /// element out of that cached result is what still happens per read - including the throw when
    /// a malformed mapping declares more than one value element.</remarks>
    public PropertyMapping? PrimitiveValueProperty => valueElements.SingleOrDefault();

    /// <summary>
    /// Whether this collection contains a property that represents the value of a FHIR primitive.
    /// </summary>
    public bool HasPrimitiveValueMember => valueElements.Count > 0;

    private List<PropertyMapping> valueElements =>
        _valueElements ?? LazyInitializer.EnsureInitialized(ref _valueElements,
            () => ByName.Values.Where(pm => pm.RepresentsValueElement).ToList())!;

    private List<PropertyMapping>? _valueElements;

    /// <summary>
    /// The properties that represent elements with a minimum cardinality higher than 0.
    /// </summary>
    /// <remarks>Validating an instance means verifying that none of its mandatory elements is missing.
    /// Only a handful of the elements of a type are mandatory, but the whole collection would have to be
    /// scanned - per validated instance - to find out which, so the outcome of that scan is cached here.
    /// Whether an element is mandatory is fixed when its mapping is built (see
    /// <see cref="PropertyMapping.ValidationAttributes"/>), so this cache only needs to be invalidated
    /// when the collection itself changes, which <see cref="clearCaches"/> does.</remarks>
    public IReadOnlyList<PropertyMapping> MandatoryElements =>
        _mandatoryElements ?? LazyInitializer.EnsureInitialized(ref _mandatoryElements,
            () => ByName.Values.Where(pm => pm.MandatoryCardinality.Length > 0).ToList())!;

    private List<PropertyMapping>? _mandatoryElements;

    IEnumerator<PropertyMapping> IEnumerable<PropertyMapping>.GetEnumerator() => _byName.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_byName.Values).GetEnumerator();
}