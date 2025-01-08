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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hl7.FhirPath.Functions
{
    internal static class CollectionOperators
    {
        public static bool? BooleanEval(this IEnumerable<PocoNode> focus)
        {
            PocoNode[] enumerable = focus.ToArray();
            if (!enumerable.Any()) return null;

            if (enumerable.Length == 1 && enumerable.Single() is PrimitiveNode {Value: bool boolean})
            {
                return boolean;
            }

            // Otherwise, we have "some" content, which we'll consider "true"
            return true;
        }


        public static bool Not(this IEnumerable<PocoNode> focus)
            => focus.Count() > 1
            ? throw Error.InvalidOperation($"Operator {nameof(Not)} is not applicable for collections with more than one item.")
            : !focus.BooleanEval().Value;
        
        public static IEnumerable<PocoNode> DistinctUnion(this IEnumerable<PocoNode> a, IEnumerable<PocoNode> b)
            => a.Union<PocoNode>(b, EqualityOperators.TypedElementEqualityComparer);

        public static IEnumerable<PocoNode> Item(this IEnumerable<PocoNode> focus, int index)
            => focus.Skip(index).Take(1);

        public static PocoNode Last(this IEnumerable<PocoNode> focus)
            => focus.Reverse().First();

        public static IEnumerable<PocoNode> Tail(this IEnumerable<PocoNode> focus)
            => focus.Skip(1);

        public static bool Contains(this IEnumerable<PocoNode> focus, PocoNode value)
            => focus.Contains(value, EqualityOperators.TypedElementEqualityComparer);
        
        public static IEnumerable<PocoNode> Distinct(this IEnumerable<PocoNode> focus)
            => focus.Distinct<PocoNode>(EqualityOperators.TypedElementEqualityComparer);

        public static bool IsDistinct(this IEnumerable<PocoNode> focus)
            => focus.Distinct(EqualityOperators.TypedElementEqualityComparer).Count() == focus.Count();

        public static bool SubsetOf(this IEnumerable<PocoNode> focus, IEnumerable<PocoNode> other)
            => focus.All(fitem => other.Contains(fitem));
        
        public static IEnumerable<PocoNode> Intersect(this IEnumerable<PocoNode> focus, IEnumerable<PocoNode> other)
            => focus.Intersect<PocoNode>(other, EqualityOperators.TypedElementEqualityComparer);

        public static IEnumerable<PocoNode> Exclude(this IEnumerable<PocoNode> focus, IEnumerable<PocoNode> other)
            => focus.Where(f => !other.Contains(f));

        public static int IndexOf(this IEnumerable<PocoNode> focus, PocoNode item, int start = 0)
        {
            var typedElements = focus as PocoNode[] ?? focus.ToArray();
            for (int i = start; i < typedElements.Length; i++)
            {
                if (EqualityOperators.TypedElementEqualityComparer.Equals(typedElements[i], item))
                {
                    return i;
                }
            }
            return -1;
        }

        public static int LastIndexOf(this IEnumerable<PocoNode> focus, PocoNode item, int to = -1)
        {
            var typedElements = focus as PocoNode[] ?? focus.ToArray();
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
        

        public static IEnumerable<PocoNode> Navigate(this IEnumerable<PocoNode> elements, string name)
            => elements.SelectMany(e => e.Navigate(name));

        public static IEnumerable<PocoNode> Navigate(this PocoNode element, string name)
        {
            if (char.IsUpper(name[0]))
            {
                // If we are at a resource, we should match a path that is possibly not rooted in the resource
                // (e.g. doing "name.family" on a Patient is equivalent to "Patient.name.family")   
                if(element.Is(name))
                {
                    return new List<PocoNode>() { element };
                }
            }

            return element.Child(name) ?? Enumerable.Empty<PocoNode>();
        }

        public static string FpJoin(this IEnumerable<PocoNode> collection, string separator = null)
        {
            //if the collection is empty return the empty result
            if (!collection.Any())
                return string.Empty;

            //only join collections with string values inside
            if (!collection.All(c => c.GetValue() is string))
                throw Error.InvalidOperation("Join function can only be performed on string collections.");

            var values = collection.Select(n => n.GetValue());
            return string.Join(separator, values);
        }
    }
}
