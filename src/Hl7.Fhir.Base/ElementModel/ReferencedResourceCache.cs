using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel;

#nullable enable

internal class ReferencedResourceCache : IEnumerable<ScopedNode.BundledResource>
{
    private Dictionary<string, ScopedNode> _items;
    private List<ScopedNode> _unreferenceableItems; // some resources may not have a reference, but are included nonetheless.
    
    public ReferencedResourceCache(IEnumerable<KeyValuePair<string?, ScopedNode>> items)
    {
        _items = new Dictionary<string, ScopedNode>();
        _unreferenceableItems = [];
        foreach (var item in items)
        {
            if (item.Key is not null)
                _items[item.Key] = item.Value;
            else
                _unreferenceableItems.Add(item.Value);
        }
    }
    
    internal IEnumerable<ScopedNode> Resources => _items.Values.Concat(_unreferenceableItems);

    internal ScopedNode? ResolveReference(string reference)
    {
        return _items.TryGetValue(reference, out var node) ? node : null;
    }

    public IEnumerator<ScopedNode.BundledResource> GetEnumerator() => 
        _items
            .Select(i => new ScopedNode.BundledResource(i.Key, i.Value))
            .Concat(
                _unreferenceableItems.Select(i => new ScopedNode.BundledResource(null, i))
            )
            .GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

#nullable restore