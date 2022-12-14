/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath.Functions
{
    internal static class CollectionOperators
    {
        public static bool? BooleanEval(this IEnumerable<ITypedElement> focus)
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


        public static bool Not(this IEnumerable<ITypedElement> focus)
            => focus.Count() > 1
            ? throw Error.InvalidOperation($"Operator {nameof(Not)} is not applicable for collections with more than one item.")
            : !focus.BooleanEval().Value;

        public static IEnumerable<ITypedElement> DistinctUnion(this IEnumerable<ITypedElement> a, IEnumerable<ITypedElement> b)
            => a.Union(b, EqualityOperators.TypedElementEqualityComparer);

        public static IEnumerable<ITypedElement> Item(this IEnumerable<ITypedElement> focus, int index)
            => focus.Skip(index).Take(1);

        public static ITypedElement Last(this IEnumerable<ITypedElement> focus)
            => focus.Reverse().First();

        public static IEnumerable<ITypedElement> Tail(this IEnumerable<ITypedElement> focus)
            => focus.Skip(1);

        public static bool Contains(this IEnumerable<ITypedElement> focus, ITypedElement value)
            => focus.Contains(value, EqualityOperators.TypedElementEqualityComparer);

        public static IEnumerable<ITypedElement> Distinct(this IEnumerable<ITypedElement> focus)
            => focus.Distinct(EqualityOperators.TypedElementEqualityComparer);

        public static bool IsDistinct(this IEnumerable<ITypedElement> focus)
            => focus.Distinct(EqualityOperators.TypedElementEqualityComparer).Count() == focus.Count();

        public static bool SubsetOf(this IEnumerable<ITypedElement> focus, IEnumerable<ITypedElement> other)
            => focus.All(fitem => other.Contains(fitem));

        public static IEnumerable<ITypedElement> Intersect(this IEnumerable<ITypedElement> focus, IEnumerable<ITypedElement> other)
            => focus.Intersect(other, EqualityOperators.TypedElementEqualityComparer);

        public static IEnumerable<ITypedElement> Exclude(this IEnumerable<ITypedElement> focus, IEnumerable<ITypedElement> other)
            => focus.Where(f => !other.Contains(f));

        public static IEnumerable<ITypedElement> Navigate(this IEnumerable<ITypedElement> elements, string name)
            => elements.SelectMany(e => e.Navigate(name));

        public static IEnumerable<ITypedElement> Navigate(this ITypedElement element, string name)
        {
            if (char.IsUpper(name[0]))
            {
                // If we are at a resource, we should match a path that is possibly not rooted in the resource
                // (e.g. doing "name.family" on a Patient is equivalent to "Patient.name.family")   
                // Also we do some poor polymorphism here: Resource.meta.lastUpdated is also allowed.
                var baseClasses = new[] { "Resource", "DomainResource" };
                if (element.InstanceType == name || baseClasses.Contains(name))
                {
                    return new List<ITypedElement>() { element };
                }
            }

            return element.Children(name);
        }

        public static string FpJoin(this IEnumerable<ITypedElement> collection, string separator)
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
