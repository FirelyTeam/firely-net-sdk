#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model;

public static class BaseExtensions
{
    [Obsolete("Use GetElementPairs() instead. Note that with GetElementPairs(), the elements are not guaranteed to " +
              "be the same type, as they reflect the type in the actual POCO definition.")]
    public static IEnumerable<Base> Children(this Base instance)
    {
        foreach (var element in instance.EnumerateElements())
        {
            switch (element.Key, element.Value)
            {
                case ("div", XHtml xhtml):
                    yield return new FhirString(xhtml.Value);
                    break;
                case ("id", FhirUri id):
                    yield return new FhirString(id.Value);
                    break;
                case (_, IEnumerable<Base> list):
                    foreach (var item in list)
                        yield return item;
                    break;
                case ("value", _) when instance is PrimitiveType:
                    yield break;
                default:
                    yield return (Base)element.Value;
                    break;
            }
        }
    }

    public static T DeepCopy<T>(this T source) where T : Base => (T)source.DeepCopyInternal();

    public static void CopyTo<T>(this T source, T target) where T : Base => source.CopyToInternal(target);
    
    internal static IEnumerable<T> DeepCopyInternal<T>(this IEnumerable<T> source) where T : Base
    {
        return source.Select(item => item.DeepCopy());
    }
    
    internal static void CopyToInternal(this Dictionary<string, object> source, Dictionary<string, object> target)
    {
        foreach ((string key, object value) in source)
        {
            target[key] = value switch
            {
                Base baseValue => baseValue.DeepCopy(),
                IEnumerable<Base> baseList => baseList.DeepCopyInternal(),
                _ => throw new InvalidOperationException($"Unexpected type in overflow: key {key} is of type {value.GetType()}, but either Base or IEnumerable<Base> was expected.")
            };
        }
    }
}