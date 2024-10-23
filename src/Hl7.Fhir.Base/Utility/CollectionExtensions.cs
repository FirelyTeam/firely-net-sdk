#nullable enable
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Utility;

internal static class CollectionExtensions
{
#if NETSTANDARD2_0
    public static TValue? GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) =>
        dictionary.GetValueOrDefault(key, default!);

    public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
    {
        if (dictionary is null) throw new ArgumentNullException(nameof(dictionary));
        return dictionary.TryGetValue(key, out TValue? value) ? value : defaultValue;
    }
#endif
}