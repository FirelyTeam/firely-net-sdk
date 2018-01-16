using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class Children : IAssertion, IMergeable, ICollectable
    {
        private readonly Lazy<IReadOnlyDictionary<string,IAssertion>> _childList;
        
        public Children(params (string name, IAssertion assertion)[] children)
        {
            _childList = new Lazy<IReadOnlyDictionary<string, IAssertion>>(()=>toROD(children));                    
        }

        private IReadOnlyDictionary<string, IAssertion> toROD(IEnumerable<(string,IAssertion)> children)
        {
            var lookup = new Dictionary<string, IAssertion>();
            foreach (var (name, assertion) in children)
                lookup.Add(name, assertion);
            return lookup;
        }


        public Children(IReadOnlyDictionary<string, IAssertion> children)
        {
            _childList = new Lazy<IReadOnlyDictionary<string, IAssertion>>(() => children);
        }

        public Children(Func<IReadOnlyDictionary<string, IAssertion>> childGenerator)
        {
            _childList = new Lazy<IReadOnlyDictionary<string, IAssertion>>(childGenerator);
        }

        public IReadOnlyDictionary<string, IAssertion> ChildList => _childList.Value;

        public IAssertion Lookup(string name) =>
            ChildList.TryGetValue(name, out var child) ? child : null;

        public IEnumerable<Assertions> Collect() => new Assertions(this).Collection;

        public IMergeable Merge(IMergeable other)
        {
            if (other is Children cd)
            {
                var mergedChildren = from name in names()
                       let left = this.Lookup(name)
                       let right = cd.Lookup(name)
                       select (name,merge(left, right));
                return new Children(() => toROD(mergedChildren));
            }
            else
                throw Error.InvalidOperation($"Internal logic failed: tried to merge Children with an {other.GetType().Name}");

            IEnumerable<string> names() => ChildList.Keys.Union(cd.ChildList.Keys).Distinct();

            IAssertion merge(IAssertion l, IAssertion r)
            {
                if (l == null) return r;
                if (r == null) return l;

                return new ElementSchema(l, r);
            }
        }

        public JToken ToJson() =>
            new JProperty("children", new JObject() { ChildList.Select(child =>
                new JProperty(child.Key, child.Value.ToJson().MakeNestedProp())) });
    }
}
