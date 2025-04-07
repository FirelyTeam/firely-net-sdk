/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model;

public static partial class BaseExtensions
{
    [Obsolete("Use EnumerateElements() instead. Note that with EnumerateElements(), the elements 'div' and 'id' are not FhirStrings, but XHtml and FhirUri respectively.")]
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

    public static IEnumerable<T> DeepCopy<T>(this IEnumerable<T> source) where T : Base => source.DeepCopyInternal();
    
    internal static IEnumerable<T> DeepCopyInternal<T>(this IEnumerable<T> source) where T : Base
    {
        return source.Select(item => item.DeepCopy()).ToList();
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

    internal static DynamicPrimitive ToDynamicPrimitive(this Base instance)
    {
        var primitive = new DynamicPrimitive();
        
        foreach(var element in instance.EnumerateElements())
        {
            if(element.Key == "value")
                primitive.ObjectValue = element.Value;
            else
                primitive.SetValue(element.Key, element.Value);
        }

        return primitive;
    }

    internal static DynamicDataType ToDynamicDataType(this Base instance)
    {
        var dt = new DynamicDataType() { DynamicTypeName = instance.TypeName };
        
        foreach(var element in instance.EnumerateElements())
        {
            dt.SetValue(element.Key, element.Value);
        }

        if (instance is PrimitiveType primitive)
            dt.SetValue("value", primitive);

        return dt;
    }

    internal static IList ToDynamicDataType(this IList list)
    {
        var entries = new List<DynamicDataType>();
        
        foreach(object? element in list)
        {
            if(element is Base b)
            {
                entries.Add(b.ToDynamicDataType());
            }
        }

        return entries;
    }
}