#if NOT_USED

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Model;

public class BaseDictionary(Base wrapped) : IReadOnlyDictionary<string,object>
{
    private static object wrap(object value) =>
        value switch
        {
            Base b => new BaseDictionary(b),
            _ => value
        };

    #region IReadOnlyDictionary

    IEnumerable<string> IReadOnlyDictionary<string, object>.Keys => wrapped.GetElementPairs().Select(kvp => kvp.Key);
    IEnumerable<object> IReadOnlyDictionary<string, object>.Values => wrapped.GetElementPairs().Select(kvp => wrap(kvp.Value));
    int IReadOnlyCollection<KeyValuePair<string, object>>.Count => wrapped.GetElementPairs().Count();

    object IReadOnlyDictionary<string, object>.this[string key] => wrap(wrapped[key]);

    bool IReadOnlyDictionary<string, object>.TryGetValue(string key, out object value)
    {
        if (wrapped.TryGetValue(key, out var unwrapped))
        {
            value = wrap(unwrapped);
            return true;
        }

        value = null!;
        return false;
    }

    bool IReadOnlyDictionary<string, object>.ContainsKey(string key) => wrapped.TryGetValue(key, out _);

    IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator() =>
        wrapped.GetElementPairs().Select(kvp => KeyValuePair.Create(kvp.Key, wrap(kvp.Value))).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        wrapped.GetElementPairs().Select(kvp => KeyValuePair.Create(kvp.Key, wrap(kvp.Value))).GetEnumerator();

    #endregion

}

#endif