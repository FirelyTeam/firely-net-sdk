using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class Children : Assertion, IMergeableAssertion
    {
        public readonly Child[] ChildAssertions;

        public Children(params Child[] children) : this(children.AsEnumerable())
        {
        }

        public Children(IEnumerable<Child> children)
        {
            ChildAssertions = children.ToArray();
            _hashedChildren = new Dictionary<string, Child>();

            foreach (var child in children)
                _hashedChildren.Add(child.Name, child);
        }

        public Child Lookup(string name) =>
            _hashedChildren.TryGetValue(name, out var child) ? child : null;

        private readonly Dictionary<string, Child> _hashedChildren;

        public override IEnumerable<Assertions> Collect() => new Assertions(this).Collection;

        public IMergeableAssertion Merge(IMergeableAssertion other)
        {
            if (other is Children cd)
            {
                var mergedChildren = from name in names()
                       let left = this.Lookup(name)
                       let right = cd.Lookup(name)
                       select merge(left, right);
                return new Children(mergedChildren);
            }
            else
                throw Error.InvalidOperation($"Internal logic failed: tried to merge Children with an {other.GetType().Name}");

            IEnumerable<string> names() => ChildAssertions.Select(c => c.Name)
                                            .Union(cd.ChildAssertions.Select(c => c.Name)).Distinct();

            Child merge(Child l, Child r)
            {
                if (l == null) return r;
                if (r == null) return l;
                return (Child)l.Merge(r);
            }
        }

        public override JToken ToJson() =>
            new JProperty("children", new JObject() { ChildAssertions.Select(ca => ca.ToJson()) });


        public class Child : Assertion, IMergeableAssertion
        {
            public readonly string Name;
            public readonly Assertion Assertion;

            public Child(string name, Assertion assertion)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Assertion = assertion ?? throw new ArgumentNullException(nameof(assertion));
            }

            public override IEnumerable<Assertions> Collect() => Assertions.Empty.Collection;

            public IMergeableAssertion Merge(IMergeableAssertion other)
            {
                if (other is Child ca)
                {
                    if (Name != ca.Name)
                        throw Error.InvalidOperation($"Internal logic failed: tried to merge two children with a different name ('{Name}' and '{ca.Name}')");

                    return new Child(Name, new ElementSchema(Assertion, ca.Assertion));
                }
                else
                    throw Error.InvalidOperation($"Internal logic failed: tried to merge a Child with an {other.GetType().Name}");
            }

            public override JToken ToJson() =>
                new JProperty(Name, Assertion.ToJson().MakeNestedProp());
        }
    }
}
