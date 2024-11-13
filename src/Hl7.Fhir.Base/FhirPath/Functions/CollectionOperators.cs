/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hl7.FhirPath.Functions
{
    internal static class CollectionOperators
    {
        public static bool? BooleanEval(this IEnumerable<IScopedNode> focus)
        {
            if (!focus.Any()) return null;

            if (focus.Count() == 1 && focus.Single().Value is bool boolean)
            {
                return boolean;
            }

            // Otherwise, we have "some" content, which we'll consider "true"
            else
                return true;
        }


        public static bool Not(this IEnumerable<IScopedNode> focus)
            => focus.Count() > 1
            ? throw Error.InvalidOperation($"Operator {nameof(Not)} is not applicable for collections with more than one item.")
            : !focus.BooleanEval().Value;

        [TemporarilyChanged] // We cast all of them to scoped nodes for now. This will not be necessary once we define a clear Equality operator for IScopedNode
        public static IEnumerable<IScopedNode> DistinctUnion(this IEnumerable<IScopedNode> a, IEnumerable<IScopedNode> b)
            => a.Union(b, EqualityOperators.TypedElementEqualityComparer).Select(ite => ite.ToScopedNode());

        public static IEnumerable<IScopedNode> Item(this IEnumerable<IScopedNode> focus, int index)
            => focus.Skip(index).Take(1);

        public static IScopedNode Last(this IEnumerable<IScopedNode> focus)
            => focus.Reverse().First();

        public static IEnumerable<IScopedNode> Tail(this IEnumerable<IScopedNode> focus)
            => focus.Skip(1);

        public static bool Contains(this IEnumerable<IScopedNode> focus, IScopedNode value)
            => focus.Contains(value, EqualityOperators.TypedElementEqualityComparer);

        [TemporarilyChanged] // We cast all of them to scoped nodes for now. This will not be necessary once we define a clear Equality operator for IScopedNode
        public static IEnumerable<IScopedNode> Distinct(this IEnumerable<IScopedNode> focus)
            => focus.Distinct(EqualityOperators.TypedElementEqualityComparer).Select(ite => ite.ToScopedNode());

        public static bool IsDistinct(this IEnumerable<IScopedNode> focus)
            => focus.Distinct(EqualityOperators.TypedElementEqualityComparer).Count() == focus.Count();

        public static bool SubsetOf(this IEnumerable<IScopedNode> focus, IEnumerable<IScopedNode> other)
            => focus.All(fitem => other.Contains(fitem));

        [TemporarilyChanged] // We cast all of them to scoped nodes for now. This will not be necessary once we define a clear Equality operator for IScopedNode
        public static IEnumerable<IScopedNode> Intersect(this IEnumerable<IScopedNode> focus, IEnumerable<IScopedNode> other)
            => focus.Intersect(other, EqualityOperators.TypedElementEqualityComparer).Select(ite => ite.ToScopedNode());

        public static IEnumerable<IScopedNode> Exclude(this IEnumerable<IScopedNode> focus, IEnumerable<IScopedNode> other)
            => focus.Where(f => !other.Contains(f));

        public static int IndexOf(this IEnumerable<IScopedNode> focus, IScopedNode item, int start = 0)
        {
            var typedElements = focus as IScopedNode[] ?? focus.ToArray();
            for (int i = start; i < typedElements.Length; i++)
            {
                if (EqualityOperators.TypedElementEqualityComparer.Equals(typedElements[i], item))
                {
                    return i;
                }
            }
            return -1;
        }

        public static int LastIndexOf(this IEnumerable<IScopedNode> focus, IScopedNode item, int to = -1)
        {
            var typedElements = focus as IScopedNode[] ?? focus.ToArray();
            to = to < 0 ? typedElements.Count() - 1 : to;
            for (int i = to; i >= 0; i--)
            {
                if (EqualityOperators.TypedElementEqualityComparer.Equals(typedElements[i], item))
                {
                    return i;
                }
            }
            return -1;
        }
        

        public static IEnumerable<IScopedNode> Navigate(this IEnumerable<IScopedNode> elements, string name)
            => elements.SelectMany(e => e.Navigate(name));

        public static IEnumerable<IScopedNode> Navigate(this IScopedNode element, string name)
        {
            if (char.IsUpper(name[0]))
            {
                // If we are at a resource, we should match a path that is possibly not rooted in the resource
                // (e.g. doing "name.family" on a Patient is equivalent to "Patient.name.family")   
                // Also we do some poor polymorphism here: Resource.meta.lastUpdated is also allowed.
#pragma warning disable CS0612 // Type or member is obsolete
                if (element.InstanceType == name && element.Type.HasFlag(NodeType.Resource))
#pragma warning restore CS0612 // Type or member is obsolete
                {
                    return new List<IScopedNode>() { element };
                }
            }

            return element.Children(name);
        }

        public static string FpJoin(this IEnumerable<IScopedNode> collection, string separator = null)
        {
            //if the collection is empty return the empty result
            if (!collection.Any())
                return string.Empty;

            //only join collections with string values inside
            if (!collection.All(c => c.Value is string))
                throw Error.InvalidOperation("Join function can only be performed on string collections.");

            var values = collection.Select(n => n.Value);
            return string.Join(separator, values);
        }
    }
}
