#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;

namespace Hl7.Fhir.Model;

public static class BaseExtensions
{
    [Obsolete("Use GetElementPairs() instead. Note that with GetElementPairs(), the elements are not guaranteed to " +
              "be the same type, as they reflect the type in the actual POCO definition.")]
    public static IEnumerable<Base> Children(this Base instance)
    {
        foreach (var element in instance.GetElementPairs())
        {
            switch (element.Key, element.Value)
            {
                case ("div", XHtml xhtml):
                    yield return new FhirString(xhtml.Value);
                    break;
                case ("id", string id):
                    yield return new FhirString(id);
                    break;
                case ("url", string url):
                    yield return new FhirUri(url);
                    break;
                case (_, IEnumerable<Base> list):
                    foreach (var item in list)
                        yield return item;
                    break;
                case("value", _) when instance is PrimitiveType:
                    yield break;
                default:
                    yield return (Base)element.Value;
                    break;
            }
        }
    }
}