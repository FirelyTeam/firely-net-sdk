using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel;

#nullable enable

internal class ReferencedResourceCache : IEnumerable<ScopedNode.BundledResource>
{
    private Dictionary<string, ScopedNode?> _items;
    
    public ReferencedResourceCache(IEnumerable<KeyValuePair<string, ScopedNode?>> items)
    {
        _items = new Dictionary<string, ScopedNode?>();
        var it = 0;
        foreach (var item in items)
        {
            // default to a generated key because BundledResources.Count should be consistent with the amount of resources in the bundle
            _items.Add(item.Key ?? $"item[{it}]", item.Value);
            it++;
        }
    }
    
    internal IEnumerable<ScopedNode> Resources => _items.Values.OfType<ScopedNode>();

    internal ScopedNode? ResolveReference(string reference)
    {
        return _items.TryGetValue(reference, out var node) ? node : null;
    }

    public IEnumerator<ScopedNode.BundledResource> GetEnumerator() => _items.Select(i => new ScopedNode.BundledResource(i.Key, i.Value)).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

#nullable restore