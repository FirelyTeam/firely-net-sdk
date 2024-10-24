using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Model;

public abstract partial class Base: IReadOnlyDictionary<string,object>, IDictionary<string, object>
{
    private object get(string key) =>
        this.TryGetValue(key, out var value)
            ? value
            : throw new KeyNotFoundException($"Element '{key}' is not a known FHIR element or has no value.");

    public IReadOnlyDictionary<string, object> AsReadOnlyDictionary() => this;
    public IDictionary<string, object> AsDictionary() => this;

    #region IReadOnlyDictionary

    IEnumerable<string> IReadOnlyDictionary<string, object>.Keys => GetElementPairs().Select(kvp => kvp.Key);
    IEnumerable<object> IReadOnlyDictionary<string, object>.Values => GetElementPairs().Select(kvp => kvp.Value);
    int IReadOnlyCollection<KeyValuePair<string, object>>.Count => GetElementPairs().Count();
    object IReadOnlyDictionary<string, object>.this[string key] => get(key);

    bool IReadOnlyDictionary<string, object>.TryGetValue(string key, out object value) =>
        TryGetValue(key, out value!);

    bool IReadOnlyDictionary<string, object>.ContainsKey(string key) => TryGetValue(key, out _);

    IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator() =>
        GetElementPairs().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetElementPairs().GetEnumerator();

    #endregion

    #region IDictionary

    void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item) =>
        AsDictionary().Add(item.Key, item.Value);

    void ICollection<KeyValuePair<string, object>>.Clear()
    {
        // Slow....
        foreach (var kvp in this)
            SetValue(kvp.Key, null);
    }

    bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item) =>
        TryGetValue(item.Key, out _); // we don't care about the item, we cannot have the same key twice.

    void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
        foreach (var kvp in this)
            array[arrayIndex++] = kvp;
    }

    bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item) =>
        AsDictionary().Remove(item.Key);

    int ICollection<KeyValuePair<string, object>>.Count => AsReadOnlyDictionary().Count;
    bool ICollection<KeyValuePair<string, object>>.IsReadOnly => false;
    ICollection<object> IDictionary<string, object>.Values => AsReadOnlyDictionary().Values.ToList();
    ICollection<string> IDictionary<string, object>.Keys => AsReadOnlyDictionary().Keys.ToList();
    bool IDictionary<string, object>.TryGetValue(string key, out object value) => TryGetValue(key, out value!);

    object IDictionary<string, object>.this[string key]
    {
        get => this.AsReadOnlyDictionary()[key];
        set => SetValue(key, value);
    }

    void IDictionary<string, object>.Add(string key, object value)
    {
        if (TryGetValue(key, out _))
            throw new ArgumentException($"An element with the key '{key}' already exists in the dictionary.");

        SetValue(key, value);
    }

    bool IDictionary<string, object>.ContainsKey(string key) => TryGetValue(key, out _);

    bool IDictionary<string, object>.Remove(string key)
    {
        var existed = TryGetValue(key, out _);
        SetValue(key, null);
        return existed;
    }

    #endregion
}